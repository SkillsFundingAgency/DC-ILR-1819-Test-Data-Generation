using DCT.ILR.Model;
using DCT.TestDataGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFA.DC.ILR._1819.Anonymiser.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uiProcess_Click(object sender, EventArgs e)
        {
            DCT.ILR.Model.Message message;
            string result = string.Empty;
            System.Xml.Serialization.XmlSerializer reader =
                        new System.Xml.Serialization.XmlSerializer(typeof(DCT.ILR.Model.Message));

            
            using (TextReader sr = new StreamReader(uiPath.Text))
            {
                message = (DCT.ILR.Model.Message)reader.Deserialize(sr);
            }

            try
            {
                var learnerNull = message.Learner.Where(l => l == null).Any();

                var learnerDeliveryNull = message.Learner.Where(l => l.LearningDelivery != null).SelectMany(l => l.LearningDelivery).Where(ld => ld == null).Any();

                var ldpNull = message.LearnerDestinationandProgression.Where(ldp => ldp == null).Any();

            }
            catch(Exception exception)
            {

            }


            string ukprnText = _ukprns.Text;
            string[] ukprns;
            if (ukprnText.Contains(","))
            {
                ukprns = ukprnText.Split(',');
            }
            else
            {
                ukprns = new string[] { ukprnText };
            }

            foreach (var ukprn in ukprns)
            {
                var r = CreateNewFileName(uiPath.Text, ukprn) + ".anon";
            }

            Dictionary<string, long> ldCount = new Dictionary<string, long>();
            HashSet<string> postcodes = new HashSet<string>();
            HashSet<string> aimRefs = new HashSet<string>();
            long ULNIndex = 0;
            List<MessageLearner> newLearners = new List<MessageLearner>(message.Learner.Length);
            foreach ( MessageLearner learner in message.Learner )
            {         
                learner.AddLine1 = "18 Address line road";
                learner.GivenNames = "Mary Jane";
                learner.FamilyName = "Sméth";
                //learner.PostcodePrior = "ZZ99 9ZZ";
                learner.Postcode = "ZZ99 9ZZ";
                if (!postcodes.Contains(learner.PostcodePrior))
                {
                    postcodes.Add(learner.PostcodePrior);
                }

                learner.TelNo = "07855555555";
                learner.Email = "myemail@myemail.com";
                if (!string.IsNullOrEmpty(learner.NINumber))
                {
                    learner.NINumber = "LJ000000A";
                }
                long uln = 0;

                bool ok = false;
                while (!ok)
                {
                    try
                    {
                        uln = ListOfULNs.ULN(ULNIndex);
                        ok = true;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                    ++ULNIndex;
                }
                var Aug13 = new DateTime(2013, 08, 01);
                if (learner.LearningDelivery != null)
                {
                    foreach (var ld in learner.LearningDelivery)
                    {
                        if( ld.LearnStartDate < Aug13 )
                        {
                            AddToLDCount("StartedBeforeAug13", ldCount);
                        }
                        AddToLDCount(ld.FundModel.ToString(), ldCount);
                        if( ld.FundModel == 35 && ld.LearnAimRef == "ZPROG001")
                        {
                            AddToLDCount("FM35 and ZPROG", ldCount);
                        }
                        if (ld.FundModel == 36 && ld.LearnAimRef == "ZPROG001")
                        {
                            AddToLDCount("FM36 and ZPROG", ldCount);
                        }
                        if(!aimRefs.Contains(ld.LearnAimRef))
                        {
                            aimRefs.Add(ld.LearnAimRef);
                        }
                    }
                }
                else
                {
                    AddToLDCount("No LDs", ldCount);
                }
                newLearners.Add(learner);
                if (message.LearnerDestinationandProgression != null)
                {
                    List<MessageLearnerDestinationandProgression> dpOutcomes = message.LearnerDestinationandProgression?.Where(dp => dp.ULNSpecified && dp.ULN == learner.ULN).ToList();
                    if (dpOutcomes?.Count > 0)
                    {
                        if (dpOutcomes.Count > 1)
                        {
                            AddToLDCount($"ULN {uln} has {dpOutcomes.Count} dpOutcomes", ldCount);
                            dpOutcomes = new List<MessageLearnerDestinationandProgression> { dpOutcomes.First() };
                        }
                        dpOutcomes.ForEach(d => 
                        {
                            if (string.IsNullOrEmpty(d.LearnRefNumber))
                            {
                                AddToLDCount($"ULN {uln} has null learnrefnumber in dpoutcome", ldCount);
                            }
                            d.ULN = uln;
                            d.LearnRefNumber = uln.ToString();
                        });
                    }
                }
                learner.ULN = uln;
                learner.LearnRefNumber = learner.ULN.ToString();
            }
            message.Learner = newLearners.ToArray();

            foreach (var ukprn in ukprns)
            {
                message.LearningProvider.UKPRN = int.Parse(ukprn);
                message.Header.Source.UKPRN = message.LearningProvider.UKPRN;
                using (TextWriter sw = new StreamWriter(CreateNewFileName(uiPath.Text, ukprn) + ".anon"))
                {
                    reader.Serialize(sw, message);
                }
            }

            AddGridRow("Learners", $"{message.Learner.Count()}");
            AddGridRow("Postcodes", $"{postcodes.Count}");
            AddGridRow("Aim refs", $"{aimRefs.Count}");
            foreach ( var key in ldCount.Keys )
            {
                AddGridRow($"{key}", ldCount[key].ToString());
            }
            
        }

        private string CreateNewFileName(string fullpath, string ukprn)
        {
            //ILR-   0-3 4
            //10003915 4-11 8
            //-1819-20181012-100000-01.xml  12 39 28
            var result = fullpath.Substring(0, fullpath.Length - 36) + ukprn + fullpath.Substring(fullpath.Length - 28);
            return result;
        }

        private void AddToLDCount(string fm,Dictionary<string,long> ldCount)
        {
            if (ldCount.ContainsKey(fm))
                ldCount[fm]++;
            else
                ldCount.Add(fm, 1);
        }

        private void AddGridRow(string attribute, string value)
        {
            var index = uiGrid.Rows.Add();
            var row = uiGrid.Rows[index];
            row.Cells["Attribute"].Value = attribute;
            row.Cells["Value"].Value = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if( DialogResult.OK == dlg.ShowDialog() )
            {
                uiPath.Text = dlg.FileName;
            }
        }
    }
}
