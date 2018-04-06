using System;

namespace DCT.TestDataGenerator
{
    public class GenerationOptions
    {
        public GenerationOptions()
        {
            LD = new LearningDeliveryOptions();
            FAM = new LearnerFAMOptions();
        }

        public enum AimTypes
        {
            ApprenticeshipFM36ValidAims,
            StandAloneAims,
            CommunityLearningFM10ValidAims,
            NonfundedValidAims,
            YP1619Aims,
            AdultFM35ValidAims,
            OtherYP1619Aims,
            ESFValidAims,
            OtherAdultFM81ValidAims
        }

        public AimTypes AimDefaultType { get; set; }

        public bool NIRequired { get; set; }

        public bool DOBRequired { get; set; }

        public bool Age19 { get; set; }

        public bool EngMathsGardeRequired { get; set; }

        public bool SetACTToFullyFunded { get; set; }

        public bool EmploymentRequired { get; set; }

        public bool CreateDestinationAndProgression { get; set; }

        public bool AccomodationRequired { get; set; }

        public bool LLDDHealthProblemRequired { get; set; }

        public bool ProviderSpecialMonitoringRequired { get; set; }

        public int? OverrideUKPRN { get; set; }

        public LearningDeliveryOptions LD { get; set; }

        public LearnerFAMOptions FAM { get; set; }
    }
}
