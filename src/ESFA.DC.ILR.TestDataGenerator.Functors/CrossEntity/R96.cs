using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class R96
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
            return "R96";
        }

        public string LearnerReferenceNumberStub()
        {
            return "R96";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateOptions }
            };
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                var lds = learner.LearningDelivery.ToList();
                lds[0].LearnAimRef = "ZWRKX001";
                lds[0].ProgTypeSpecified = true;
                lds[0].ProgType = (int)ProgType.Traineeship;
                var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>();

                   ldwp.Add(new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = lds[0].LearnStartDate,
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 500,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271388
                    });

                ldwp.Add(new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = lds[0].LearnStartDate,
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 450,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271389
                    });
                lds[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
            }
        }

        private void MutateProgType(MessageLearner learner, bool valid)
        {
            learner.DateOfBirth = learner.LearningDelivery[0].LearnStartDate.AddYears(-19).AddMonths(-3);
            if (!valid)
            {
                learner.LearningDelivery[0].FworkCodeSpecified = true;
                learner.LearningDelivery[0].FworkCode = 403;
                learner.LearningDelivery[0].LearnActEndDateSpecified = true;
                learner.LearningDelivery[0].LearnActEndDate = new DateTime(2017, 11, 30);
            }
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EmploymentRequired = true;
            options.LD.GenerateMultipleLDs = 2;
        }

        private void MutateOptions(GenerationOptions options)
        {
        }
    }
}
