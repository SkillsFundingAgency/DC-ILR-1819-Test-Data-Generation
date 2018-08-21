﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class WorkPlaceStartDate_03
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private GenerationOptions _options;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "WorkPlaceStartDate_03";
        }

        public string LearnerReferenceNumberStub()
        {
            return "WrkPlace03";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            return new List<LearnerTypeMutator>()
            {
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearner, DoMutateOptions = MutateGenerationOptionsSOF },
                new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.CommunityLearning, DoMutateLearner = MutateLearnerAimRef, DoMutateOptions = MutateGenerationOptionsSOF, ExclusionRecord = true }
            };
        }

        private void MutateLearnerAimRef(MessageLearner learner, bool valid)
        {
            learner.LearningDelivery[0].LearnAimRef = "Z0007834";
            if (valid)
            {
                MutateWorkPlacement(learner);
            }
        }

        private void MutateLearner(MessageLearner learner, bool valid)
        {
            if (!valid)
            {
                MutateWorkPlacement(learner);
            }
        }

        private void MutateWorkPlacement(MessageLearner learner)
        {
                var ldwp = new List<MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement>
                {
                    new MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement()
                    {
                        WorkPlaceStartDateSpecified = true,
                        WorkPlaceStartDate = learner.LearningDelivery[0].LearnStartDate.AddDays(30),
                        WorkPlaceHoursSpecified = true,
                        WorkPlaceHours = 1000,
                        WorkPlaceModeSpecified = true,
                        WorkPlaceMode = 1,
                        WorkPlaceEmpIdSpecified = true,
                        WorkPlaceEmpId = 900271388
                    }
                };
                learner.LearningDelivery[0].LearningDeliveryWorkPlacement = ldwp.ToArray();
        }

        private void MutateGenerationOptionsSOF(GenerationOptions options)
        {
            options.LD.IncludeSOF = true;
        }
    }
}