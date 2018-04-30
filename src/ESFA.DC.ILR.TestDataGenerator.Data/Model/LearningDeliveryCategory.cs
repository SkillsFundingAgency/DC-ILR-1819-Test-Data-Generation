using System;
using System.Collections.Generic;
using System.Text;

namespace DCT.TestDataGenerator
{
    public class LearningDeliveryCategory
    {
        public string LearnAimRef { get; set; }
        public int CategoryRef { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
