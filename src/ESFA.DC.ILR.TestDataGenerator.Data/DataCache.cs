using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DCT.TestDataGenerator
{
    public class DataCache : ILearnerCreatorDataCache
    {
        const int _MaximumLearnRefLength = 12;
        const string _ESFContractNumber = "ESF-1234567";

        SerializableDataCache _serializable;

        public DataCache()
        {
            _serializable = new SerializableDataCache();
            _serializable.CreateFromStaticData();
        }

        public int MaximumLearnRefLength()
        {
            return _MaximumLearnRefLength;
        }

        public IEnumerable<ApprenticeshipProgrammeTypeAim> ApprenticeshipAims(ProgType pt)
        {
            return _serializable._apprenticeShipAims.Values.Where(s => s.ProgType == pt);
        }

        public string ESFContractNumber()
        {
            return _ESFContractNumber;
        }

        public IEnumerable<string> GCSEGrades()
        {
            return _serializable._gcseGrades;
        }

        public IEnumerable<string> GCSEDOrBelow()
        {
            return _serializable._gcseDGrades;
        }

        public LearnAimFunding LearnAimFundingWithValidity(FundModel fm, LearnDelFAMCode sofCode, DateTime learnStartDate)
        {
            return _serializable._learnAimFundingModels.Where(s => s.AimFunding.FundModel == fm && s.SourceOfFunding == sofCode).First().AimFunding;
        }

        public LearnAimFunding LearnAimWithCategory(LearnDelCategory category)
        {
            return _serializable._learnAimWithCategory[category];
        }

        public LearnAimFunding LearnAimWithLearnAimType(LearnAimType aimType)
        {
            return _serializable._learnAimWithLearnAimType[aimType];
        }        

        public LearnAimFunding LearnAimWithLevel(FullLevel level, FundModel fm)
        {
            return _serializable._learnAimFundingModelFullLevel.Where(s => s.Level == level && s.AimFunding.FundModel == fm).First().AimFunding;
        }

        public Organisation OrganisationWithLegalType(LegalOrgType type)
        {
            return _serializable._organisations[type];
        }

        public LearningDelivery LearningDeliveryWithCommonComponent( CommonComponent cc )
        {
            return _serializable._learningDelivery.Where(s => s.FrameworkCommonComponent == (int)cc).First();
        }

        public IEnumerable<string> InvalidPostcode()
        {
            List<string> invalid0 = new List<string> { "XXX", "x", "xx" };
            List<string> invalid1 = new List<string> { "X1", "333", "X" };
            List<string> invalid2 = new List<string> { "LC", "IL", "LK", "ML", "LO", "VL" };
            List<string> result = new List<string>(50);

            foreach (var i0 in invalid0)
            {
                result.Add(i0 + "2 3AA");
            }

            foreach (var i1 in invalid1)
            {
                result.Add("AA" + i1 + " 3AA");
            }

            foreach (var i2 in invalid2)
            {
                result.Add("AA1A 1" + i2);
            }

            return result;
        }

        public IEnumerable<LLDDCatValidity> LLDDCatValidity()
        {
            return _serializable._llddCatValidity;
        }
    }
}
