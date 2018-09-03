using System;

namespace DCT.TestDataGenerator
{
    public class LearningDeliveryOptions
    {
        public bool IncludeSOF { get; set; }

        public bool IncludeHEFields { get; set; }

        public bool IncludeFFI { get; set; }

        public bool IncludeHHS { get; set; }

        public bool IncludeLDM { get; set; }

        public bool IncludeContract { get; set; }

        /// <summary>
        /// Gets or sets The ADL indicator "advanced learner loan indicator" for use with FM99
        /// </summary>
        public bool IncludeADL { get; set; }

        public bool IncludeOutcome { get; set; }

        public bool IncludeRES { get; set; }

        public bool IncludeZESF0001 { get; set; }

        public DateTime? OverrideLearnStartDate { get; set; }

        public int? OverrideLDM { get; set; }

        /// <summary>
        /// Setting this to a number greater than 1 will create several component learning deliveries (or stand alone for non- apprenticeship)
        /// For apprenticeship learners, the ZPROG001 learning delivery is not counted in this number. That LD is always created as well as at lest one component LD
        /// </summary>
        public int GenerateMultipleLDs { get; internal set; }
    }
}
