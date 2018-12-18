using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Model
{
    class LearnerRow
    {
        public string Key { get; set; }
        public Dictionary<string,string> Cell { get; set; }

        public LearnerRow(IEnumerable<string> ss, IEnumerable<ColumnDescriptor> columns, IEnumerable<ColumnDescriptor> keyColumns)
        {
            Cell = new Dictionary<string, string>();
            IEnumerator<ColumnDescriptor> i = columns.GetEnumerator();
            i.MoveNext();
            foreach(var s in ss)
            {
                Cell.Add(i.Current.Title, s);
                i.MoveNext();
            }
            BuildKey(keyColumns);
        }

        private void BuildKey(IEnumerable<ColumnDescriptor> keyColumns)
        {
            Key = string.Empty;
            foreach (var c in keyColumns)
            {
                Key += Cell[c.Title] + "_";
            }
        }
    }
}
