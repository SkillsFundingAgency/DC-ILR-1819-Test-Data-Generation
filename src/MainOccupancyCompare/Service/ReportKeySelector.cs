using MainOccupancyCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Service
{
    interface ReportKeySelector
    {
        string Key(IEnumerable<ColumnDescriptor> columns);
    }
}
