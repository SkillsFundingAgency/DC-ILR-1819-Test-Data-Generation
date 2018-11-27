using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R72
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.July;
        }

        public string RuleName()
        {
            return "R72";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R72";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
               // new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptions },
                //new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.OtherAdult, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            var ld0 = learner.LearningDelivery[0];
            ld0.ProgType = (int)ProgType.ApprenticeshipStandard;
            ld0.ProgTypeSpecified = true;
            ld0.LearnStartDate = new DateTime(2017, 10, 10);
            ld0.LearnActEndDate = ld0.LearnStartDate.AddYears(1);
            ld0.AimType = 1;
            ld0.LearnAimRef = "L5055456";

            Helpers.AddAfninRecord(learner, "PMR", 1, 700);
            Helpers.AddAfninRecord(learner, "PMR", 2, 400);
            Helpers.AddAfninRecord(learner, "PMR", 3, 200);
            Helpers.AddAfninRecord(learner, "TNP", 1, 600);
            Helpers.AddAfninRecord(learner, "TNP", 2, 300);

            ld0.AppFinRecord[0].AFinDate = ld0.LearnStartDate;
            ld0.AppFinRecord[1].AFinDate = ld0.LearnStartDate.AddDays(300);
            ld0.AppFinRecord[2].AFinDate = ld0.LearnStartDate.AddDays(310);
            ld0.AppFinRecord[3].AFinDate = ld0.LearnStartDate.AddDays(360);
            ld0.AppFinRecord[4].AFinDate = ld0.LearnStartDate.AddDays(360);
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            if (!valid)
            {
                var ld = learner.LearningDelivery[0];
                ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                ld.ProgTypeSpecified = true;
                ld.AimTypeSpecified = true;
                ld.AimType = 3;
                ld.LearnAimRef = "60325999";
            }
        }

        private void AddFinRec(MessageLearner learner, bool valid, int finAmt, string finType, int finCode, DateTime finDt, MessageLearnerLearningDeliveryAppFinRecord[] finRec)
        {
            var appfin = finRec.ToList();
            appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
            {
                AFinAmount = finAmt,
                AFinAmountSpecified = true,
                AFinType = finType,
                AFinCode = finCode,
                AFinCodeSpecified = true,
                AFinDate = finDt,
                AFinDateSpecified = true
            });
              learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
            if (valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                    ld.ProgTypeSpecified = true;
                    ld.AimTypeSpecified = true;
                    ld.StdCode = 12;
                    ld.StdCodeSpecified = true;
                    ld.FworkCodeSpecified = false;
                    ld.PwayCodeSpecified = false;
                }

                var appfin = new List<MessageLearnerLearningDeliveryAppFinRecord>();
                appfin.Add(new MessageLearnerLearningDeliveryAppFinRecord()
                {
                    AFinAmount = 500,
                    AFinAmountSpecified = true,
                    AFinType = LearnDelAppFinType.TNP.ToString(),
                    AFinCode = (int)LearnDelAppFinCode.TotalAssessmentPrice,
                    AFinCodeSpecified = true,
                    AFinDate = learner.LearningDelivery[0].LearnStartDate,
                    AFinDateSpecified = true
                });

                learner.LearningDelivery[0].AppFinRecord = appfin.ToArray();
                learner.LearningDelivery[0].EPAOrgID = "EPA1234";
            }

            if (!valid)
            {
                foreach (var ld in learner.LearningDelivery)
                {
                    ld.ProgType = (int)ProgType.ApprenticeshipStandard;
                    ld.ProgTypeSpecified = true;
                    ld.AimTypeSpecified = true;
                    ld.StdCode = 12;
                    ld.StdCodeSpecified = true;
                }
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            //options.LD.OverrideLearnStartDate = DateTime.Parse("2017-Sep-01");
            //options.LD.IncludeHHS = true;
            //_options = options;
        }
    }
}
