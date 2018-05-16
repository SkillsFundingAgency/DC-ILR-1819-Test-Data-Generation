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

        IEnumerable<LLDDCatValidity> LLDDCatValidity();

        string PostcodeWithAreaCostFactor();

        string PostcodeDisadvantagedArea();
    }
}
