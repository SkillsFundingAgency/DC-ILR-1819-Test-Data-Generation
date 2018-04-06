using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DCT.ILR.Model
{
    public partial class MessageLearner
    {
        [XmlIgnore]
        public long? PrevUKPRNNullable
        {
            get { return prevUKPRNFieldSpecified ? (long?)prevUKPRNField : null;  }
        }

        [XmlIgnore]
        public DateTime? DateOfBirthNullable
        {
            get { return dateOfBirthFieldSpecified ? (DateTime?)dateOfBirthField : null; }
        }
    }
}
