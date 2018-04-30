using System;
using System.Collections.Generic;
using System.Linq;

namespace DCT.TestDataGenerator
{
    public class ApprenticeshipProgrammeTypeAim
    {
        public ProgType ProgType;
        public int FworkCode;
        public int PwayCode;
        public List<Validity> Validity;
        public int StdCode; // for apprenticeship standards
        public LearningDelivery LearningDelivery;
        public DateTime EffectiveFrom;
        public DateTime? EffectiveTo;
        public List<FrameworkCommonComponent> FrameworkCommonComponents;
        public List<StandardCommonComponent> StandardCommonComponents;
    }
}
