using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class LearnStartDate_06
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
            return "LearnStartDate_06";
        }

        public string LearnerReferenceNumberStub()
        {
            return "LstartDt06";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptions },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.Apprenticeships, DoMutateLearner = Mutate19HigherLevelApprenticeship5, DoMutateOptions = MutateGenerationOptions, ExclusionRecord = true },
            };
        }

        private void MutateCommon(MessageLearner learner, bool valid)
        {
            Helpers.MutateApprenticeshipToOlderWithFundingFlag(learner, LearnDelFAMCode.FFI_Fully);
            Helpers.MutateDOB(learner, valid, Helpers.AgeRequired.Exact19, Helpers.BasedOn.LearnDelStart, Helpers.MakeOlderOrYoungerWhenInvalid.NoChange);
        }

        private void Mutate19HigherLevelApprenticeship5(MessageLearner learner, bool valid)
        {
            ApprenticeshipProgrammeTypeAim pta = _dataCache.ApprenticeshipAims(ProgType.HigherApprenticeshipLevel5).First();
            MutateCommon(learner, valid);
            learner.LearningDelivery[0].LearnStartDate = new DateTime(2017, 05, 01);
            Helpers.SetApprenticeshipAims(learner, pta);
            var empstat = learner.LearnerEmploymentStatus.ToList();
            empstat.Add(new MessageLearnerLearnerEmploymentStatus()
            {
                DateEmpStatApp = learner.LearningDelivery[0].LearnStartDate.AddDays(1),
                DateEmpStatAppSpecified = true,
                EmpStatSpecified = true,
                EmpStat = 10,
                EmpIdSpecified = true,
                EmpId = 154549452,
                EmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[]
                {
                    new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                    {
                        ESMType = EmploymentStatusMonitoringType.EII.ToString(),
                        ESMCode = (int)EmploymentStatusMonitoringCode.EmploymentIntensity20Plus,
                        ESMCodeSpecified = true
                    },
                    new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring()
                    {
                        ESMType = EmploymentStatusMonitoringType.LOE.ToString(),
                        ESMCode = (int)EmploymentStatusMonitoringCode.Employed12Plus,
                        ESMCodeSpecified = true
                    }
                }
            });

            learner.LearnerEmploymentStatus = empstat.Skip(1).ToArray();

            if (!valid)
            {
                learner.LearningDelivery[1].LearnStartDate = new DateTime(2018, 08, 31).AddDays(1);
            }
        }

        private void MutateApprenticeshipStandard(MessageLearner learner, bool valid)
        {
            Mutate19HigherLevelApprenticeship5(learner, valid);
            if (!valid)
            {
                learner.LearningDelivery[1].ProgType = (int)ProgType.ApprenticeshipStandard;
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.LD.IncludeHHS = true;
            options.LD.IncludeHEFields = true;
            options.EmploymentRequired = true;
        }
    }
}