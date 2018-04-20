using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public class MathGrade_04
        : ILearnerMultiMutator
    {
        private ILearnerCreatorDataCache _dataCache;
        private List<LearnerFAMCode> _mcf;

        public FilePreparationDateRequired FilePreparationDate()
        {
            return FilePreparationDateRequired.None;
        }

        public string RuleName()
        {
            return "MathGrade_04";
        }

        public string LearnerReferenceNumberStub()
        {
            return "Math_04";
        }

        public IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache cache)
        {
            _dataCache = cache;
            _mcf = new List<LearnerFAMCode>(4) { LearnerFAMCode.MCF_ExcemptOverseasEquivalent, LearnerFAMCode.MCF_MetOtherInstitution, LearnerFAMCode.MCF_MetUKEquivalent };
            var result = new List<LearnerTypeMutator>();
            foreach (var g in _mcf)
            {
                result.Add(new LearnerTypeMutator() { LearnerType = LearnerTypeRequired.YP1619, DoMutateLearner = Mutate, DoMutateOptions = MutateGenerationOptions });
            }

            return result;
        }

        private void Mutate(MessageLearner learner, bool valid)
        {
            Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.ECF, LearnerFAMCode.ECF_ExcemptLearningDifficulty);
            Helpers.AddOrChangeLearnerFAM(learner, LearnerFAMType.MCF, _mcf.First());

            if (!valid)
            {
                learner.MathGrade = _dataCache.GCSEGrades().First();
            }

            _mcf.RemoveAt(0);
        }

        private void MutateGenerationOptions(GenerationOptions options)
        {
            options.EngMathsGardeRequired = true;
            options.FAM.MathsConditionOfFundingRequired = true;
            options.FAM.EducationHealthCarePlanRequired = true;
        }
    }
}
