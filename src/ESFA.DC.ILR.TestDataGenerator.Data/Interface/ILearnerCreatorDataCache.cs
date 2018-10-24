using System;
using System.Collections.Generic;
using System.Linq;

namespace DCT.TestDataGenerator
{
    public interface ILearnerCreatorDataCache
    {
        /// <summary>
        /// What is the maximum length of a learner ref number?
        /// </summary>
        /// <returns></returns>
        int MaximumLearnRefLength();

        IEnumerable<ApprenticeshipProgrammeTypeAim> ApprenticeshipAims(ProgType type);

        // find the specific learning aim that would match for this prog / fwork / pway. Designed to create more complex data with more variation in the learning delivery
        ApprenticeshipProgrammeTypeAim ApprenticeshipAims(ProgType type, long FworkCode, long PwayCode, int index);

        LearnAimFunding LearnAimFundingWithValidity(FundModel fm, LearnDelFAMCode sofCode, DateTime learnStartDate);

        LearnAimFunding LearnAimWithCategory(LearnDelCategory category);

        Organisation OrganisationWithLegalType(LegalOrgType specialistDesignatedCollege);

        LearnAimFunding LearnAimWithLearnAimType(LearnAimType aimType);

        string ESFContractNumber();

        LearnAimFunding LearnAimWithLevel(FullLevel level, FundModel fm);

        IEnumerable<string> GCSEGrades();

        IEnumerable<string> GCSEDOrBelow();

        LearningDelivery LearningDeliveryWithCommonComponent(CommonComponent cc);

        IEnumerable<string> InvalidPostcode();

        IEnumerable<string> NonExistPostcode();

        IEnumerable<string> ValidPostcode();

        IEnumerable<LLDDCatValidity> LLDDCatValidity();

        string PostcodeWithAreaCostFactor();

        string PostcodeDisadvantagedArea();
    }
}
