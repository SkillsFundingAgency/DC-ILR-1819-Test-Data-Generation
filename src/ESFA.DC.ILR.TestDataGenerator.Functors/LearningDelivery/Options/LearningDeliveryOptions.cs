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

        public int GenerateMultipleLDs { get; internal set; }
    }
}
