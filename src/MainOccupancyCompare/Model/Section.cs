using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Model
{
    class Section
    {
        private List<ColumnDescriptor> _columns;
        private List<ColumnDescriptor> _keyColumns;
        private List<LearnerRow> _rows;
        private bool _buildColumnDescriptorsRequired = false;
        public string Name { get; private set; }

        public Section(string n)
        {
            Name = n;
        }

        public void BuildColumnsDescriptors(IEnumerable<string> vs)
        {
            _columns = new List<ColumnDescriptor>(vs.Count());
            _keyColumns = new List<ColumnDescriptor>(4);
            _rows = new List<LearnerRow>();
            int i = 0;
            _buildColumnDescriptorsRequired = false;
            foreach (string s in vs)
            {
                if (string.IsNullOrEmpty(s))
                {
                    _columns.Add(new ColumnDescriptor()
                    {
                        Title = $"ColumnDescriptor{i}",
                        DataType = typeof(string)
                    });
                    _buildColumnDescriptorsRequired = true;
                }
                else
                {
                    _columns.Add(new ColumnDescriptor()
                    {
                        Title = s,
                        DataType = typeof(string)
                    });
                }
                ++i;
            }
            if( !_buildColumnDescriptorsRequired )
            {
                BuildKeyColumns();
            }
        }

        internal void CheckAndAddRow(IEnumerable<string> vs)
        {
            if (vs.Count() == 0 || string.IsNullOrEmpty(vs.First()))
            {
            }
            else
            {
                if(_buildColumnDescriptorsRequired)
                {
                    for( int i=1; i != vs.Count(); ++i)
                    {
                        _columns[i].Title = vs.ElementAt(i);
                    }
                    BuildKeyColumns();
                    _buildColumnDescriptorsRequired = false;
                }
                LearnerRow row = new LearnerRow(vs, _columns, _keyColumns);
                _rows.Add(row);
            }
        }

        private void BuildKeyColumns()
        {
            switch( Name )
            {
                case LearnerRowColumns.AppsIndicativeFunding:
                    {
                        _keyColumns.Add(_columns[0]);
                        _keyColumns.Add(_columns[7]);
                    }
                    break;
                case LearnerRowColumns.MainOccupancy:
                    {
                        _keyColumns.Add(_columns[0]);
                        _keyColumns.Add(_columns[8]);
                    }
                    break;
                default:
                    _keyColumns.Add(_columns[0]);
                    break;
            }
        }

        internal IEnumerable<string> RowKeys()
        {
            return _rows.Select(s => s.Key);
        }

        internal bool HasRow(string key)
        {
            return _rows.Where(s => s.Key == key).Count() > 0;
        }

        internal LearnerRow Row(string key)
        {
            return _rows.Where(s => s.Key == key).First();
        }
    }
}
