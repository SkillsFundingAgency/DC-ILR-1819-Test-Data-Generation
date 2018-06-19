namespace DCT.TestDataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DCT.ILR.Model;

    public class Helpers
    {
        /// <summary>
        /// Achievement dates are an apprenticeship (or traineeship) concept and will fail other rules if set when they shouldn't be
        /// </summary>
        public enum SetAchDate
        {
            /// <summary>
            /// Traineeships and FM36 apprenticeships should set the AchDate
            /// </summary>
            SetAchDate,
            DoNotSetAchDate
        }

        public enum AgeRequired
        {
            Exact1Day,
            Exact4,
            Exact13,
            Exact15,
            Less16,
            Less16And30Days,
            Exact16,
            Less18,
            Exact18,
            Less19,
            Exact19,
            Less24,
            Exact24,
            More24,
            Less25,
            Exact25,
            Less100,
            Less115
        }

        public enum BasedOn
        {
            /// <summary>
            /// The normal FE Accademic year starts on 01-August
            /// </summary>
            AYStart,

            /// <summary>
            /// The FE AY ends on the 31-July
            /// </summary>
            EndAYYear,

            /// <summary>
            /// The school AY is based on the end of August
            /// </summary>
            SchoolAYStart,

            /// <summary>
            /// Dates in rules are often not based on the year but on the dates of learning delivery
            /// </summary>
            LearnDelStart
        }

        public enum MakeOlderOrYoungerWhenInvalid
        {
            Older,
            OlderTwo,
            YoungerLots,
            Younger,
            NoChange
        }

        public static string ValueOrFunction(string val)
        {
            if (val.StartsWith("["))
            {
                if (val.EndsWith("]"))
                {
                    val = val.Substring(1, val.Length - 2);
                }

                var command = val.Split('|');
                switch (command[0])
                {
                    case "Age":
                        int ageInYears = int.Parse(command[1]);
                        DateTime now = DateTime.Now;
                        now -= TimeSpan.FromDays(365 * ageInYears);
                        return now.ToString("yyyy-MM-dd");
                    case "AY":
                        // "AY|JUN|06"
                        int month = ConvertToMonth(command[1]);
                        int year = DateTime.Now.Year;
                        if (month >= 8)
                        {
                            --year;
                        }

                        DateTime d = new DateTime(year, month, int.Parse(command[2]));
                        return d.ToString("yyyy-MM-dd");
                    case "AY+1":
                        // "AY|JUN|06"
                        int month1 = ConvertToMonth(command[1]);
                        int year1 = DateTime.Now.Year;
                        ++year1;
                        if (month1 >= 8)
                        {
                            --year1;
                        }

                        DateTime d1 = new DateTime(year1, month1, int.Parse(command[2]));
                        return d1.ToString("yyyy-MM-dd");
                    case "AY-1":
                        // "AY|JUN|06"
                        int month2 = ConvertToMonth(command[1]);
                        int year2 = DateTime.Now.Year;
                        --year2;
                        if (month2 >= 8)
                        {
                            --year2;
                        }

                        DateTime d2 = new DateTime(year2, month2, int.Parse(command[2]));
                        return d2.ToString("yyyy-MM-dd");
                    case "GEN":
                    case "OPT":
                    case "VALID4PROP":
                        return val;
                    case "EMPTY":
                        return null;
                    case "FMTOAIM":
                        return val;
                    case "DEFAULT":
                        return val;

                    default:
                        throw new ArgumentException($"ValueOrFunction detected func {command[0]} extracted from {val} but there is no command handler for that");
                }
            }
            else
            {
                return val;
            }
        }

        public static void MutateLearningDeliveryMonitoringLDMToNewCode(MessageLearner learner, LearnDelFAMCode code)
        {
            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.LDM.ToString()).First();
            fam.LearnDelFAMCode = ((int)code).ToString("D3");
        }

        /// <summary>
        /// Learner should have been created with LearnerTypeRequired.Apprenticeships to ensure that the programme and app fin records and similar have been created properly
        /// </summary>
        /// <param name="learner">Learner to mutate to FM35 from FM36</param>
        public static void MutateApprenticeshipToOlderWithFundingFlag(MessageLearner learner, LearnDelFAMCode ffi)
        {
            learner.LearningDelivery[0].FundModel = (int)FundModel.Adult;
            MoveEmploymentBeforeLearnStart(learner);
            learner.LearningDelivery[0].AppFinRecord = null;

            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
            fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
            fam.LearnDelFAMDateFromSpecified = false;
            fam.LearnDelFAMCode = ((int)ffi).ToString();

            for (int i = 1; i != learner.LearningDelivery.Length; ++i)
            {
                learner.LearningDelivery[i].LearnStartDate = learner.LearningDelivery[0].LearnStartDate;
                learner.LearningDelivery[i].FundModel = (int)FundModel.Adult;
                var ld1Fams = learner.LearningDelivery[i].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = fam.LearnDelFAMType,
                    LearnDelFAMCode = fam.LearnDelFAMCode
                });
                learner.LearningDelivery[i].LearningDeliveryFAM = ld1Fams.ToArray();
            }
        }

        /// <summary>
        /// Learner should have been created with LearnerTypeRequired.Apprenticeships to ensure that the programme and app fin records and similar have been created properly
        /// </summary>
        /// <param name="learner">Learner to mutate to a standards based apprenticeship</param>
        public static void MutateApprenticeshipToStandard(MessageLearner learner, FundModel newFundingModel)
        {
            learner.LearningDelivery[1].LearnStartDate = learner.LearningDelivery[0].LearnStartDate;
            learner.LearningDelivery[0].FundModel = (int)newFundingModel;
            learner.LearningDelivery[1].FundModel = (int)newFundingModel;
            MoveEmploymentBeforeLearnStart(learner);
            learner.LearningDelivery[0].AppFinRecord[0].AFinDate = learner.LearningDelivery[0].LearnStartDate;
            var appfins = learner.LearningDelivery[0].AppFinRecord.ToList();
            appfins.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = 500,
                AFinAmountSpecified = true,
                AFinType = LearnDelAppFinType.TNP.ToString(),
                AFinCode = (int)LearnDelAppFinCode.TotalAssessmentPrice,
                AFinCodeSpecified = true,
                AFinDate = appfins[0].AFinDate,
                AFinDateSpecified = true
            });

            learner.LearningDelivery[0].AppFinRecord = appfins.ToArray();
            learner.LearningDelivery[0].EPAOrgID = "EPA0032";

            // older Standards had different rules (pre 01-May-2017) and no ACT record (for example)
            if (newFundingModel == FundModel.OtherAdult)
            {
                var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
                fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
                fam.LearnDelFAMDateFromSpecified = false;
                fam.LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Fully).ToString();
                var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = fam.LearnDelFAMType,
                    LearnDelFAMCode = fam.LearnDelFAMCode
                });
                learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
            }
        }

        public static void AddLearningDeliveryRestartFAM(MessageLearner learner)
        {
            AddLearningDeliveryRestartFAM(learner.LearningDelivery[0]);
        }

        public static void AddLearningDeliveryRestartFAM(MessageLearnerLearningDelivery ld)
        {
            var ld0Fams = ld.LearningDeliveryFAM.ToList();
            ld0Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = LearnDelFAMType.RES.ToString(),
                LearnDelFAMCode = ((int)LearnDelFAMCode.RES).ToString()
            });
            ld.LearningDeliveryFAM = ld0Fams.ToArray();
        }

        public static void SafeAddlearningDeliveryFAM(MessageLearnerLearningDelivery ld, LearnDelFAMType type, LearnDelFAMCode code, DateTime? from, DateTime? to)
        {
            List<MessageLearnerLearningDeliveryLearningDeliveryFAM> fams = null;
            if (ld.LearningDeliveryFAM == null)
            {
                fams = new List<MessageLearnerLearningDeliveryLearningDeliveryFAM>(1);
            }
            else
            {
                fams = ld.LearningDeliveryFAM.ToList();
            }

            if (fams.Count(s => s.LearnDelFAMType == type.ToString() && s.LearnDelFAMCode == ((int)code).ToString()) == 0)
            {
                var fam = new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = type.ToString(),
                    LearnDelFAMCode = ((int)code).ToString(),
                };
                if (from.HasValue)
                {
                    fam.LearnDelFAMDateFrom = from.Value;
                    fam.LearnDelFAMDateFromSpecified = true;
                }

                if (to.HasValue)
                {
                    fam.LearnDelFAMDateTo = to.Value;
                    fam.LearnDelFAMDateToSpecified = true;
                }

                fams.Add(fam);
            }

            ld.LearningDeliveryFAM = fams.ToArray();
        }

        public static void SetLearningDeliveryEndDates(MessageLearnerLearningDelivery ld, DateTime endDate, SetAchDate modifyAch)
        {
            ld.LearnPlanEndDate = endDate;
            ld.LearnActEndDate = endDate;
            ld.LearnActEndDateSpecified = true;
            ld.CompStatus = (int)CompStatus.Completed;
            ld.Outcome = (int)Outcome.Achieved;
            ld.OutcomeSpecified = true;
            if (modifyAch == SetAchDate.SetAchDate)
            {
                ld.AchDate = ld.LearnActEndDate;
                ld.AchDateSpecified = true;
            }
            else
            {
                ld.AchDateSpecified = false;
            }
        }

        public static void MutateApprenticeToTrainee(MessageLearner learner, ILearnerCreatorDataCache dataCache)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)ProgType.Traineeship;
                ld.ProgTypeSpecified = true;
                ld.FworkCodeSpecified = false;
                ld.PwayCodeSpecified = false;
                ld.FundModel = (int)FundModel.Adult;
                ld.FundModel = (int)FundModel.Adult;
                ld.LearnPlanEndDate = ld.LearnStartDate.AddDays(60);
            }

            var fam = learner.LearningDelivery[0].LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.ACT.ToString()).First();
            fam.LearnDelFAMType = LearnDelFAMType.FFI.ToString();
            fam.LearnDelFAMDateFromSpecified = false;
            fam.LearnDelFAMCode = ((int)LearnDelFAMCode.FFI_Co).ToString();

            var ld1Fams = learner.LearningDelivery[1].LearningDeliveryFAM.ToList();
            ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
            {
                LearnDelFAMType = fam.LearnDelFAMType,
                LearnDelFAMCode = fam.LearnDelFAMCode
            });
            learner.LearningDelivery[1].LearningDeliveryFAM = ld1Fams.ToArray();
            ApprenticeshipProgrammeTypeAim pta = dataCache.ApprenticeshipAims(ProgType.Traineeship).First();

            learner.LearningDelivery[1].LearnAimRef = pta.LearningDelivery.LearnAimRef;

            MoveEmploymentBeforeLearnStart(learner);
            learner.LearnerEmploymentStatus[0].EmploymentStatusMonitoring[0].ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity16Less;
            learner.LearningDelivery[0].AppFinRecord = null;
        }

        public static void AddProviderSpecLearnerMonitoring(MessageLearner learner, ProvSpecLearnMonOccur type, string text)
        {
            List<MessageLearnerProviderSpecLearnerMonitoring> ifam = null;
            if (learner.ProviderSpecLearnerMonitoring == null)
            {
                ifam = new List<MessageLearnerProviderSpecLearnerMonitoring>(1);
            }
            else
            {
                ifam = learner.ProviderSpecLearnerMonitoring.ToList();
            }

            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = text,
                ProvSpecLearnMonOccur = type.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
        }

        public static void MoveEmploymentBeforeLearnStart(MessageLearner learner)
        {
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate;
            learner.LearnerEmploymentStatus[0].DateEmpStatApp = learner.LearnerEmploymentStatus[0].DateEmpStatApp.AddDays(-40);
        }

        public static void MutateDOB(MessageLearner learner, bool valid, AgeRequired ar, BasedOn whatTypeOfAge, MakeOlderOrYoungerWhenInvalid direction)
        {
            switch (whatTypeOfAge)
            {
                case BasedOn.SchoolAYStart:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|31]"));
                    break;
                case BasedOn.AYStart:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|AUG|01]"));
                    break;
                case BasedOn.EndAYYear:
                    learner.DateOfBirth = DateTime.Parse(Helpers.ValueOrFunction("[AY|JUL|31]"));
                    break;
                case BasedOn.LearnDelStart:
                    learner.DateOfBirth = learner.LearningDelivery.Min(s => s.LearnStartDate);
                    break;
                default:
                    throw new NotImplementedException($"MutateDOB base date to compute age {whatTypeOfAge} has not been implementated");
            }

            switch (ar)
            {
                case AgeRequired.Exact1Day:
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                    break;
                case AgeRequired.Exact4:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-4);
                    break;
                case AgeRequired.Exact13:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-13);
                    break;
                case AgeRequired.Exact15:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-15);
                    break;
                case AgeRequired.Exact16:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-16);
                    break;
                case AgeRequired.Exact18:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-18);
                    break;
                case AgeRequired.Exact19:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-19);
                    break;
                case AgeRequired.Exact24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    break;
                case AgeRequired.Exact25:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-25);
                    break;
                case AgeRequired.Less16And30Days:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-16);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-30);
                    break;
                case AgeRequired.Less19:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-19);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less25:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-25);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less100:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-100);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.Less115:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-115);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                    break;
                case AgeRequired.More24:
                    learner.DateOfBirth = learner.DateOfBirth.AddYears(-24);
                    learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                    break;
                default:
                    throw new NotImplementedException($"MutateDOB exact type of age required {ar} has not been implementated");
            }

            if (!valid)
            {
                switch (direction)
                {
                    case MakeOlderOrYoungerWhenInvalid.Older:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(-1);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.OlderTwo:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(-2);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.Younger:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(1);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.YoungerLots:
                        learner.DateOfBirth = learner.DateOfBirth.AddDays(63);
                        break;
                    case MakeOlderOrYoungerWhenInvalid.NoChange:
                        break;
                }
            }
        }

        internal static void AddOrChangeLearningDeliverySourceOfFunding(MessageLearnerLearningDelivery ld, LearnDelFAMCode sof)
        {
            var ifam = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType == LearnDelFAMType.SOF.ToString());
            if (ifam.Count() > 0)
            {
                ifam.First().LearnDelFAMCode = ((int)sof).ToString();
            }
            else
            {
                var ld1Fams = ld.LearningDeliveryFAM.ToList();
                ld1Fams.Add(new MessageLearnerLearningDeliveryLearningDeliveryFAM()
                {
                    LearnDelFAMType = LearnDelFAMType.SOF.ToString(),
                    LearnDelFAMCode = ((int)sof).ToString()
                });
                ld.LearningDeliveryFAM = ld1Fams.ToArray();
            }
        }

        internal static void SetApprenticeshipAims(MessageLearner learner, ApprenticeshipProgrammeTypeAim pta)
        {
            foreach (var ld in learner.LearningDelivery)
            {
                ld.ProgType = (int)pta.ProgType;
                if (ld.ProgType == (int)ProgType.ApprenticeshipStandard)
                {
                    ld.StdCode = pta.StdCode;
                    ld.StdCodeSpecified = true;
                    ld.FworkCodeSpecified = false;
                    ld.PwayCodeSpecified = false;
                }
                else
                {
                    ld.FworkCode = pta.FworkCode;
                    ld.PwayCode = pta.PwayCode;
                }
            }

            learner.LearningDelivery[1].LearnAimRef = pta.LearningDelivery.LearnAimRef;
        }

        internal static void RemoveLearningDeliveryFFIFAM(MessageLearner learner)
        {
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != LearnDelFAMType.FFI.ToString());
                ld.LearningDeliveryFAM = ld0Fams.ToArray();
            }
        }

        internal static void RemoveLearningDeliveryFAM(MessageLearner learner, LearnDelFAMType type)
        {
            foreach (MessageLearnerLearningDelivery ld in learner.LearningDelivery)
            {
                var ld0Fams = ld.LearningDeliveryFAM.Where(s => s.LearnDelFAMType != type.ToString());
                ld.LearningDeliveryFAM = ld0Fams.ToArray();
            }
        }

        internal static void RemoveLearnerFAM(MessageLearner learner, LearnerFAMType type)
        {
            var ifam = learner.LearnerFAM.Where(s => s.LearnFAMType != type.ToString());
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddOrChangeLearnerFAM(MessageLearner learner, LearnerFAMType type, LearnerFAMCode code)
        {
            var ifam = learner.LearnerFAM.Where(s => s.LearnFAMType != type.ToString()).ToList();
            ifam.Add(new MessageLearnerLearnerFAM()
            {
                LearnFAMType = type.ToString(),
                LearnFAMCode = (int)code,
                LearnFAMCodeSpecified = true
            });
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddLearnerFAM(MessageLearner learner, LearnerFAMType type, LearnerFAMCode code)
        {
            var ifam = learner.LearnerFAM.ToList();
            ifam.Add(new MessageLearnerLearnerFAM()
            {
                LearnFAMType = type.ToString(),
                LearnFAMCode = (int)code,
                LearnFAMCodeSpecified = true
            });
            learner.LearnerFAM = ifam.ToArray();
        }

        internal static void AddOrChangeProviderSpecLearnerMonitoring(MessageLearner learner, ProvSpecLearnMonOccur type)
        {
            var ifam = learner.ProviderSpecLearnerMonitoring.Where(s => s.ProvSpecLearnMonOccur != type.ToString()).ToList();
            ifam.Add(new MessageLearnerProviderSpecLearnerMonitoring()
            {
                ProvSpecLearnMon = $"{learner.ULN}",
                ProvSpecLearnMonOccur = type.ToString()
            });
            learner.ProviderSpecLearnerMonitoring = ifam.ToArray();
        }

        private static int ConvertToMonth(string v)
        {
            switch (v)
            {
                case "JAN": return 1;
                case "FEB": return 2;
                case "MAR": return 3;
                case "APR": return 4;
                case "MAY": return 5;
                case "JUN": return 6;
                case "JUL": return 7;
                case "AUG": return 8;
                case "SEP": return 9;
                case "OCT": return 10;
                case "NOV": return 11;
                case "DEC": return 12;
                default:
                    throw new ArgumentException($"ConvertToMonth detected month {v} but doesn't understand how to convert to a numerical month value");
            }
        }
    }
}
