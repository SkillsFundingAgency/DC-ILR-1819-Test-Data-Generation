using System;
using System.Linq;
using System.Xml.Serialization;

namespace DCT.ILR.Model
{
    public partial class MessageLearnerLearningDelivery
    {
        [XmlIgnore]
        public DateTime? LearnStartDateNullable
        {
            get { return learnStartDateFieldSpecified ? (DateTime?)learnStartDateField : null; }
        }

        [XmlIgnore]
        public DateTime? LearnPlanEndDateNullable
        {
            get { return learnPlanEndDateFieldSpecified ? (DateTime?)learnPlanEndDateField : null; }
        }

        [XmlIgnore]
        public DateTime? LearnActEndDateNullable
        {
            get { return learnActEndDateFieldSpecified ? (DateTime?)learnActEndDateField : null; }
        }
    }
}
