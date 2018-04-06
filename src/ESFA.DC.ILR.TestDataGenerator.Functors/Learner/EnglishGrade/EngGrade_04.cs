using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class EngGrade_04
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private List<LearnerFAMCode> _ecf;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _ecf = new List<LearnerFAMCode>(4) { LearnerFAMCode.ECF_ExcemptOverseasEquivalent, LearnerFAMCode.ECF_MetOtherInstitution, LearnerFAMCode.ECF_MetUKEquivalent };
            var result = new List<LearnerTypeMutator>();
            foreach (var g in _ecf)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        public string RuleName()
        {
            return "Eng_04";
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.MCF, LearnerFAMCode.MCF_ExcemptLearningDifficulty);
            Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.ECF, _ecf.First());

            if (!valid)
            {
                learner.EngGrade = _dataCache.GCSEGrades().First();
            }

            _ecf.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EngMathsGardeRequired = true;
            options.FAM.EnglishConditionOfFundingRequired = true;
            options.FAM.EducationHealthCarePlanRequired = true;
        }
    }
}
