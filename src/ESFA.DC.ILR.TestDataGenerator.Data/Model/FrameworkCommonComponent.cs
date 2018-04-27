using System;
using System.Collections.Generic;
using System.Text;

namespace DCT.TestDataGenerator
{
    public class FrameworkCommonComponent
    {
        public ProgType ProgType { get; set; }
        public int FworkCode { get; set; }
        public int PwayCode { get; set; }
        public int CommonComponent { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
