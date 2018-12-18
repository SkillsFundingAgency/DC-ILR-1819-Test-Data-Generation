using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Model
{
    public class CompareFinding
    {
        public Source Source { get; set; }
        public CompareFindingType FindingType { get; set; }
        public string SectionName { get; set; }
        public string LHSKey { get; set; }
        public string RHSKey { get; set; }
        public string ColumnName { get; set; }
        public string LHSValue { get; set; }
        public string RHSValue { get; set; }
    }

    public enum CompareFindingType
    {
        ExtraRow,
        ExtraSection,
        RowStringTrimDifference,
        RowDetailDifference,
        ExtraColumn,
        PrecisionDifference,
        RowNumberDetailDifference,
        SectionComplete,
        DatePrecisionDifference,
        RowDateDetailDifference
    }

    public enum Source
    {
        LeftHandSide,
        RightHandSide
    }
}
