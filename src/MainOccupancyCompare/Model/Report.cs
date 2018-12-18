using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Model
{
    class Report
    {
        Dictionary<string, Section> _sections;
        Section _currentSection = null;

        public Report()
        {
            _sections = new Dictionary<string, Section>();
        }

        public IEnumerable<string> SectionNames()
        {
            return _sections.Keys;
        }

        public bool HasCurrentSection() => _currentSection != null;
        public void ProcessRow(IEnumerable<string> vs)
        {
            if( IsRowAFM35SectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.MainOccupancy, vs);
            }
            else if (IsRowAFM25SectionBreak(vs))
            {
                AddNewSection("MO-FM25", vs);
            }
            else if( IsRowALLBOccupancySectionBreak(vs))
            {
                AddNewSection("ALLB", vs);
            }
            else if (IsRowAppIndicativeFundingSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.AppsIndicativeFunding, vs);
            }
            else if (IsRowMathsAndEnglishSectionBreak(vs))
            {
                AddNewSection("M&E", vs);
            }
            else if (IsRowFS1618TraineeshipBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FS1618TraineeshipBudget, vs);
            }
            else if (IsRowFSNonLevyAppBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSNonLevyAppBudget, vs);
            }
            else if (IsRowFSAEBNPDeliverySectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAEBNPDelivery, vs);
            }
            else if (IsRowFSAEBPDeliverySectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAEBPDelivery, vs);
            }
            else if (IsRowFSALBBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSALBBudget, vs);
            }
            else if (IsRowFSCIApp1618BeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp1618BeforeMayBudget, vs);
            }
            else if(IsRowFSCIApp1618TBLBeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp1618TBLBeforeMayBudget, vs);
            }
            else if(IsRowFSCIApp1618NPBeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp1618NPBeforeMayBudget, vs);
            }
            else if(IsRowFSCIApp1923BeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp1923BeforeMayBudget, vs);
            }
            else if(IsRowFSCIApp1923TBLBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp1923TBLBudget, vs);
            }
            else if(IsRowFSCIApp24BeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp24BeforeMayBudget, vs);
            }
            else if(IsRowFSCIApp24TBLBeforeMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIApp24TBLBeforeMayBudget, vs);
            }
            else if(IsRowFSCIAppAdultNonLevyNPDeliverySectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSCIAppAdultNonLevyNPDelivery, vs);
            }
            else if(IsRowFSAppLevy1618AfterMayBudgetSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAppLevy1618AfterMayBudget, vs);
            }
            else if(IsRowFSAppLevyAdultSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAppLevyAdult, vs);
            }
            else if(IsRowFSAppNonLevy1618SectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAppNonLevy1618, vs);
            }
            else if(IsRowFSAppNonLevyAdultSectionBreak(vs))
            {
                AddNewSection(LearnerRowColumns.FSAppNonLevyAdult, vs); 
            }
            //else if (IsRowFS1924TraineeshipsSectionBreak(vs))
            //{
            //    AddNewSection(LearnerRowColumns.FS1924Traineeships, vs);
            //}
            //else if (IsRowFSAEBOtherSectionBreak(vs))
            //{
            //    AddNewSection(LearnerRowColumns.FSAEBOther, vs);
            //}            
            else if (HasCurrentSection())
            {
                _currentSection.CheckAndAddRow(vs);
            }
            // else it will be discarded - which is fine
        }

        internal Section Section(string sectionName)
        {
            return _sections[sectionName];
        }

        internal bool HasSection(string sectionName)
        {
            return _sections.Keys.Contains(sectionName);
        }

        private Section AddNewSection(string title, IEnumerable<string> vs)
        {
            Section result = new Section(title)
            {
            };
            result.BuildColumnsDescriptors(vs);            
            _sections.Add(title, result);
            _currentSection = result;
            return result;
        }

        private bool IsRowAFM35SectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FM35ColumnCount &&
                vs.First() == LearnerRowColumns.LearnerRef;
            return result;
        }
        private bool IsRowAFM25SectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FM25ColumnCount &&
                vs.First() == LearnerRowColumns.LearnerRef;
            return result;
        }
        private bool IsRowALLBOccupancySectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.ALLBOccupancyColumnCount &&
                vs.First() == LearnerRowColumns.LearnerRef;
            return result;
        }

        private bool IsRowAppIndicativeFundingSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.AppsIndicativeFundingColumnCount &&
                vs.First() == LearnerRowColumns.LearnerRef;
            return result;
        }

        private bool IsRowMathsAndEnglishSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.MathsAndEnglishColumnCount &&
                vs.First() == LearnerRowColumns.FundingLineType;
            return result;
        }

        private bool IsRowFS1618TraineeshipBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FS1618TraineeshipBudget;
            return result;
        }

        private bool IsRowFSNonLevyAppBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSNonLevyAppBudget;
            return result;
        }

        private bool IsRowFSAEBNPDeliverySectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAEBNPDelivery;
            return result;
        }

        private bool IsRowFSAEBPDeliverySectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAEBPDelivery;
            return result;
        }

        private bool IsRowFSALBBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSALBBudget;
            return result;
        }

        private bool IsRowFSCIApp1618BeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp1618BeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIApp1618TBLBeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp1618TBLBeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIApp1618NPBeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp1618NPBeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIApp1923BeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp1923BeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIApp1923TBLBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp1923TBLBudget;
            return result;
        }

        private bool IsRowFSCIApp24BeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp24BeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIApp24TBLBeforeMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIApp24TBLBeforeMayBudget;
            return result;
        }

        private bool IsRowFSCIAppAdultNonLevyNPDeliverySectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSCIAppAdultNonLevyNPDelivery;
            return result;
        }

        private bool IsRowFSAppLevy1618AfterMayBudgetSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAppLevy1618AfterMayBudget;
            return result;
        }

        private bool IsRowFSAppLevyAdultSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAppLevyAdult;
            return result;
        }

        private bool IsRowFSAppNonLevy1618SectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAppNonLevy1618;
            return result;
        }

        private bool IsRowFSAppNonLevyAdultSectionBreak(IEnumerable<string> vs)
        {
            bool result = vs.Count() == LearnerRowColumns.FundingSummaryColumnCount &&
                vs.First() == LearnerRowColumns.FSAppNonLevyAdult;
            return result;
        }
    }
}
