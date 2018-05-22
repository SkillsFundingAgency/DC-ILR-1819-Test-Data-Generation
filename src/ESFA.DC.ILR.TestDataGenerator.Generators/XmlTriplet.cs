using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DCT.ILR.Model;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace DCT.TestDataGenerator
{
    public class XmlTriplet
    {
        public XmlTriplet(int scale)
        {
            Learners = new List<MessageLearner>(10 * scale);
            Progressions = new List<MessageLearnerDestinationandProgression>(10 * scale);
            FileRuleLearners = new List<FileRuleLearner>(10 * scale);
        }

        public int UKPRN { get; set; }

        public List<MessageLearner> Learners { get; set; }

        public List<MessageLearnerDestinationandProgression> Progressions { get; set; }

        public List<FileRuleLearner> FileRuleLearners { get; set; }
    }
}
