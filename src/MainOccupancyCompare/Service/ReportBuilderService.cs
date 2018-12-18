using CsvHelper;
using MainOccupancyCompare.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Service
{
    class ReportBuilderService
    {
        public Report BuildReport(string path)
        {
            Report result = new Report();
            CsvHelper.Configuration.Configuration configuration = new CsvHelper.Configuration.Configuration();
            configuration.IgnoreQuotes = false;
            using (TextReader textReader = File.OpenText(path))
            {
                var parser = new CsvParser(textReader,configuration);
                while (true)
                {
                    var row = parser.Read();
                    if (row == null)
                    {
                        break;
                    }
                    for( int i =0; i != row.Length; ++i )
                    {
                        row[i] = row[i].Replace("  "," ");
                        row[i] = row[i].Replace("–", "-");
                    }
                    result.ProcessRow(row);
                }
            }
            return result;
        }

        public void CompareReports(Report lhs, Report rhs, Action<CompareFinding> action)
        {
            foreach( var sectionName in lhs.SectionNames() )
            {
                if( rhs.HasSection(sectionName))
                {
                    CompareSection(lhs.Section(sectionName), rhs.Section(sectionName), action);
                }
                else
                {
                    CompareFinding finding = new CompareFinding()
                    {
                        Source = Source.LeftHandSide,
                        FindingType = CompareFindingType.ExtraSection,
                        LHSKey = sectionName,
                        SectionName = sectionName
                    };
                    action.Invoke(finding);
                }
                action.Invoke(new CompareFinding()
                {
                    FindingType = CompareFindingType.SectionComplete
                });
            }
            foreach (var sectionName in rhs.SectionNames())
            {
                if (!lhs.HasSection(sectionName))
                {
                    CompareFinding finding = new CompareFinding()
                    {
                        Source = Source.RightHandSide,
                        FindingType = CompareFindingType.ExtraSection,
                        RHSKey = sectionName,
                        SectionName = sectionName
                    };
                    action.Invoke(finding);
                }
                action.Invoke(new CompareFinding()
                {
                    FindingType = CompareFindingType.SectionComplete
                });
            }
        }

        public bool ReportSectionCountSame(Report lhs, Report rhs)
        {
            return lhs.SectionNames().Count() == rhs.SectionNames().Count();
        }


        public void CompareSection(Section lhs, Section rhs, Action<CompareFinding> action)
        {
            foreach( var key in lhs.RowKeys() )
            {
                if( rhs.HasRow(key) )
                {
                    DetailedRowCompare(lhs.Row(key), rhs.Row(key), lhs.Name, action);
                }
                else
                {
                    CompareFinding finding = new CompareFinding()
                    {
                        Source = Source.LeftHandSide,
                        FindingType = CompareFindingType.ExtraRow,
                        LHSKey = key,
                        SectionName = lhs.Name
                    };
                    action.Invoke(finding);
                }

            }
            foreach (var key in rhs.RowKeys())
            {
                if (!lhs.HasRow(key))
                {
                    CompareFinding finding = new CompareFinding()
                    {
                        Source = Source.RightHandSide,
                        FindingType = CompareFindingType.ExtraRow,
                        RHSKey = key,
                        SectionName = rhs.Name
                    };
                    action.Invoke(finding);
                }
            }
        }

        private void DetailedRowCompare(LearnerRow lhsRow, LearnerRow rhsRow, string sectionName, Action<CompareFinding> action)
        {
            bool columnCountSame = DetailedRowCompareColumnCount(lhsRow, rhsRow, sectionName, action);
            if (columnCountSame)
            {
                foreach (var field in lhsRow.Cell.Keys)
                {
                    var lhsValue = lhsRow.Cell[field];
                    var rhsValue = rhsRow.Cell[field];

                    if (lhsValue == rhsValue)
                        continue;

                    var lhsValueTrim = lhsRow.Cell[field].Trim();
                    var rhsValueTrim = rhsRow.Cell[field].Trim();
                    float lhsAsFloat = 0;
                    float rhsAsFloat = 0;
                    bool floatValues = false;
                    DateTime lhsAsDate = DateTime.Now;
                    DateTime rhsAsDate = DateTime.Now;
                    bool dateValues = false;

                    floatValues = float.TryParse(lhsValue, out lhsAsFloat) && float.TryParse(rhsValue, out rhsAsFloat);
                    dateValues = DateTime.TryParse(lhsValue, out lhsAsDate) && DateTime.TryParse(rhsValue, out rhsAsDate);
                    bool lengthDifference = lhsValue.Length > rhsValue.Length || lhsValue.Length < rhsValue.Length;
                    if (floatValues)
                    {
                        CheckForFloatDifferences(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, lhsAsFloat, rhsAsFloat, lengthDifference);
                    }
                    if (dateValues)
                    {
                        CheckForDateDifferences(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, lhsAsDate, rhsAsDate, lengthDifference);
                    }
                    if(lengthDifference)
                    { 
                        if (lhsValueTrim.Length == rhsValueTrim.Length)
                        {
                            action.Invoke(new CompareFinding()
                            {
                                FindingType = CompareFindingType.RowStringTrimDifference,
                                LHSKey = lhsRow.Key,
                                RHSKey = rhsRow.Key,
                                ColumnName = field,
                                Source = Source.LeftHandSide,
                                LHSValue = lhsValue,
                                RHSValue = rhsValue,
                                SectionName = sectionName
                            });
                        }
                        else
                        {
                            if (floatValues && lhsAsFloat != rhsAsFloat)
                            {
                                action.Invoke(new CompareFinding()
                                {
                                    FindingType = CompareFindingType.RowNumberDetailDifference,
                                    LHSKey = lhsRow.Key,
                                    RHSKey = rhsRow.Key,
                                    ColumnName = field,
                                    Source = Source.LeftHandSide,
                                    LHSValue = lhsValue,
                                    RHSValue = rhsValue,
                                    SectionName = sectionName
                                });
                            }
                            else
                            {
                                action.Invoke(new CompareFinding()
                                {
                                    FindingType = CompareFindingType.RowDetailDifference,
                                    LHSKey = lhsRow.Key,
                                    RHSKey = rhsRow.Key,
                                    ColumnName = field,
                                    Source = Source.LeftHandSide,
                                    LHSValue = lhsValue,
                                    RHSValue = rhsValue,
                                    SectionName = sectionName
                                });
                            }
                        }
                    }
                    else if (floatValues && lhsAsFloat != rhsAsFloat)
                    {
                        action.Invoke(new CompareFinding()
                        {
                            FindingType = CompareFindingType.RowNumberDetailDifference,
                            LHSKey = lhsRow.Key,
                            RHSKey = rhsRow.Key,
                            ColumnName = field,
                            Source = Source.LeftHandSide,
                            LHSValue = lhsValue,
                            RHSValue = rhsValue,
                            SectionName = sectionName
                        });
                    }
                    else
                    {
                        action.Invoke(new CompareFinding()
                        {
                            FindingType = CompareFindingType.RowDetailDifference,
                            LHSKey = lhsRow.Key,
                            RHSKey = rhsRow.Key,
                            ColumnName = field,
                            Source = Source.LeftHandSide,
                            LHSValue = lhsValue,
                            RHSValue = rhsValue,
                            SectionName = sectionName
                        });
                    }
                }
            }
        }

        private void CheckForDateDifferences(LearnerRow lhsRow, LearnerRow rhsRow, string sectionName, Action<CompareFinding> action, string field, string lhsValue, string rhsValue, DateTime lhsAsDate, DateTime rhsAsDate, bool lengthDifference)
        {
            if (lengthDifference && lhsAsDate == rhsAsDate)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.DatePrecisionDifference);
            }
            else if (lhsAsDate == rhsAsDate)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.PrecisionDifference);
            }
            else if (lhsAsDate != rhsAsDate)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.RowDateDetailDifference);
            }
        }

        private static void CheckForFloatDifferences(LearnerRow lhsRow, LearnerRow rhsRow, string sectionName, Action<CompareFinding> action, string field, string lhsValue, string rhsValue, float lhsAsFloat, float rhsAsFloat, bool lengthDifference)
        {
            if (lengthDifference && lhsAsFloat == rhsAsFloat)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.PrecisionDifference);
            }
            else if (lhsAsFloat == rhsAsFloat)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.PrecisionDifference);
            }
            else if (lhsAsFloat != rhsAsFloat)
            {
                RaiseDifferenceAction(lhsRow, rhsRow, sectionName, action, field, lhsValue, rhsValue, CompareFindingType.RowNumberDetailDifference);
            }
        }

        private static void RaiseDifferenceAction(LearnerRow lhsRow, LearnerRow rhsRow, string sectionName, Action<CompareFinding> action, string field, string lhsValue, string rhsValue, CompareFindingType difference)
        {
            action.Invoke(new CompareFinding()
            {
                FindingType = difference,
                LHSKey = lhsRow.Key,
                RHSKey = rhsRow.Key,
                ColumnName = field,
                Source = Source.LeftHandSide,
                LHSValue = lhsValue,
                RHSValue = rhsValue,
                SectionName = sectionName
            });
        }


        private static bool DetailedRowCompareColumnCount(LearnerRow lhsRow, LearnerRow rhsRow, string sectionName, Action<CompareFinding> action)
        {
            if (lhsRow.Cell.Values.Count() > rhsRow.Cell.Values.Count())
            {
                action.Invoke(new CompareFinding()
                {
                    FindingType = CompareFindingType.ExtraColumn,
                    LHSKey = lhsRow.Key,
                    RHSKey = rhsRow.Key,
                    Source = Source.LeftHandSide,
                    SectionName = sectionName
                });
            }
            else if (rhsRow.Cell.Values.Count() > lhsRow.Cell.Values.Count())
            {
                action.Invoke(new CompareFinding()
                {
                    FindingType = CompareFindingType.ExtraColumn,
                    LHSKey = lhsRow.Key,
                    RHSKey = rhsRow.Key,
                    Source = Source.RightHandSide,
                    SectionName = sectionName
                });
            }
            return lhsRow.Cell.Values.Count() == rhsRow.Cell.Values.Count();
        }
    }
}
