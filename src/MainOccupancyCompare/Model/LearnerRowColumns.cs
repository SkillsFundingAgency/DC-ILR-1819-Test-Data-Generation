using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOccupancyCompare.Model
{
    class LearnerRowColumns
    {
        public const int FM35ColumnCount = 134;
        public const int FM25ColumnCount = 25;
        public const int ALLBOccupancyColumnCount = 109;
        public const int MathsAndEnglishColumnCount = 10;

        public const int FundingSummaryColumnCount = 17;
        public const int AppsIndicativeFundingColumnCount = 234;

        private List<ColumnDescriptor> _columns;

        public const string AppsIndicativeFunding = "AppsIndicativeFunding";
        public const string MainOccupancy = "MainOccupancy";

        public const string LearnerRef = "Learner reference number";
        public const string FundingLineType = "Funding line type";

        public const string FS1618TraineeshipBudget = "16-18 Traineeships"; //"16-18 Traineeships Budget";

        public const string FSCIApp1618BeforeMayBudget      = "16-18 Apprenticeship Frameworks for starts before 1 May 2017"; //"Carry-in Apprenticeships Budget(for starts before 1 May 2017 and non-procured delivery)";
        public const string FSCIApp1618TBLBeforeMayBudget   = "16-18 Trailblazer Apprenticeships for starts before 1 May 2017";
        public const string FSCIApp1618NPBeforeMayBudget    = "16-18 Non-Levy Contracted Apprenticeships - Non-procured delivery";
        public const string FSCIApp1923BeforeMayBudget      = "19-23 Apprenticeship Frameworks for starts before 1 May 2017";
        public const string FSCIApp1923TBLBudget            = "19-23 Trailblazer Apprenticeships for starts before 1 May 2017";
        public const string FSCIApp24BeforeMayBudget        = "24+ Apprenticeship Frameworks for starts before 1 May 2017";
        public const string FSCIApp24TBLBeforeMayBudget     = "24+ Trailblazer Apprenticeships for starts before 1 May 2017";
        public const string FSCIAppAdultNonLevyNPDelivery   = "Adult Non-Levy Contracted Apprenticeships - Non-procured delivery";

        public const string FSAppLevy1618AfterMayBudget     = "16-18 Levy Contracted Apprenticeships";
        public const string FSAppLevyAdult                  = "Adult Levy Contracted Apprenticeships";
        public const string FSAppNonLevy1618                = "16-18 Non-Levy Contracted Apprenticeships";
        public const string FSAppNonLevyAdult               = "Adult Non-Levy Contracted Apprenticeships";
        // public const string FS1924Traineeships              = "19-24 Traineeships";

        public const string FSNonLevyAppBudget = "Non-Levy Contracted Apprenticeships Budget - Procured delivery";

        public const string FSAEBNPDelivery = "Adult Education Budget - Non-procured delivery";
        public const string FSAEBPDelivery = "Adult Education Budget - Procured delivery from 1 Nov 2017";

//        public const string FSAEB1924Trainee                = "19-24 Traineeships";
        // public const string FSAEBOther                      = "AEB - Other Learning";

        //        public const string FSLevyAppAfterMayBudget = "Levy Contracted Apprenticeships Budget for starts on or after 1 May 2017";



        public const string FSALBBudget = "Advanced Loans Bursary";

        public const string ULN = "Unique learner number";
        public const string DOB = "Date of birth";
        public const string PostcodePrior = "Postcode prior to enrolment";
        public const string PremergerUKPRN = "Pre-merger UKPRN";

        internal string ColumnTitle(int v)
        {
            return _columns[v].Title;
        }

        public const string CampusId= "Campus identifier";
        public const string MonitorA = "Provider specified learner monitoring(A)";
        public const string MonitorB = "Provider specified learner monitoring(B)";
        public const string MonitorC = "Aim sequence number";
        public const string MonitorD = "Learning aim reference";
        public const string AimTitle = "Learning aim title";
        public const string SSAimId = "Software supplier aim identifier";
        public const string FundRateESOL = "Applicable funding rate from ESOL hours";
        public const string FundRate = "Applicable funding rate";
        public const string Weight = "Applicable programme weighting";
        public const string AimValue = "Aim value";
        public const string NVQ = "Notional NVQ level";
        public const string SSA = "Tier 2 sector subject area";
        public const string ProgType = "Programme type";
        public const string FWorkCode = "Framework code";
        public const string Pathway = "Apprenticeship pathway";
        public const string AimType = "Aim type";
        public const string ComponentCode = "Framework component type code";
        public const string FundModel = "Funding model";
        public const string PriorLearning = "Funding adjustment for prior learning";
        public const string OtherAdjustment = "Other funding adjustment";
        public const string OLearningStartDate = "Original learning start date";
        public const string LearningStartDate = "Learning start date";
        public const string LearningPlannedEndDate = "Learning planned end date";
        //, Completion status,Learning actual end date, Outcome, Achievement date,Additional delivery hours,Learning delivery funding and monitoring type - source of funding,Learning delivery funding and monitoring type - full or co funding indicator,Learning delivery funding and monitoring type - eligibility for enhanced apprenticeship funding,Learning delivery funding and monitoring type - learning support funding(highest applicable),Learning delivery funding and monitoring type - LSF date applies from(earliest),Learning delivery funding and monitoring type - LSF date applies to(latest),Learning delivery funding and monitoring type - learning delivery monitoring(A),Learning delivery funding and monitoring type - learning delivery monitoring(B),Learning delivery funding and monitoring type - learning delivery monitoring(C),Learning delivery funding and monitoring type - learning delivery monitoring(D),Learning delivery funding and monitoring type - restart indicator, Provider specified delivery monitoring(A),Provider specified delivery monitoring(B),Provider specified delivery monitoring(C),Provider specified delivery monitoring(D),Funding line type,Planned number of on programme instalments, Transitional planned number of programme instalments from 1 August 2013,Transitional start proportion,Achievement element(potential or actual earned cash),Achievement percentage(aggregated maximum value),Non-Govt contribution, Sub contracted or partnership UKPRN, Delivery location postcode, Area uplift,Disadvantage uplift, Employer identifier,Large employer factor,Capping factor, Traineeship work placement or work preparation,Higher apprenticeship prescribed HE aim,Date used for employment factor lookups,Date used for other factor lookups,August on programme earned cash,August balancing payment earned cash,August aim achievement earned cash,August job outcome earned cash,August learning support earned cash,September on programme earned cash,September balancing payment earned cash,September aim achievement earned cash,September job outcome earned cash,September learning support earned cash,October on programme earned cash,October balancing payment earned cash,October aim achievement earned cash,October job outcome earned cash,October learning support earned cash,November on programme earned cash,November balancing payment earned cash,November aim achievement earned cash,November job outcome earned cash,November learning support earned cash,December on programme earned cash,December balancing payment earned cash,December aim achievement earned cash,December job outcome earned cash,December learning support earned cash,January on programme earned cash,January balancing payment earned cash,January aim achievement earned cash,January job outcome earned cash,January learning support earned cash,February on programme earned cash,February balancing payment earned cash,February aim achievement earned cash,February job outcome earned cash,February learning support earned cash,March on programme earned cash,March balancing payment earned cash,March aim achievement earned cash,March job outcome earned cash,March learning support earned cash,April on programme earned cash,April balancing payment earned cash,April aim achievement earned cash,April job outcome earned cash,April learning support earned cash,May on programme earned cash,May balancing payment earned cash,May aim achievement earned cash,May job outcome earned cash,May learning support earned cash,June on programme earned cash,June balancing payment earned cash,June aim achievement earned cash,June job outcome earned cash,June learning support earned cash,July on programme earned cash,July balancing payment earned cash,July aim achievement earned cash,July job outcome earned cash,July learning support earned cash,Total on programme earned cash,Total balancing payment earned cash,Total aim achievement earned cash,Total job outcome earned cash,Total learning support earned cash,Total earned cash,OFFICIAL - SENSITIVE
    }
}
