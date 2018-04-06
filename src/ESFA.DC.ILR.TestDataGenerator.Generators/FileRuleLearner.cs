using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.TestDataGenerator
{
    public class FileRuleLearner
    {
        public string Filename { get; set; }

        public string RuleName { get; set; }

        public bool Valid { get; set; }

        public string LearnRefNumber { get; set; }

        public int ValidLines { get; set; }

        public int InvalidLines { get; set; }

        public bool ExclusionRecord { get; set; }
    }
}
