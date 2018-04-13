using DCT.TestDataGenerator.Functor;
using DCT.TestDataGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILRTestDataGenerator
{
    public partial class MainForm : Form
    {
        RuleToFunctorParser _rfp;
        ILearnerCreatorDataCache _cache;
        int _functorCount;
        int _totalLearners;
        public MainForm()
        {
            InitializeComponent();
        }

        private void uiOuputFile_Click(object sender, EventArgs e)
        {
            List<ActiveRuleValidity> arv = new List<ActiveRuleValidity>();
            foreach (DataGridViewRow row in uiParameters.Rows)
            {
                string ruleName = (string)row.Cells["RuleName"].Value;
                bool valid = (bool)row.Cells["Valid"].Value;
                bool active = (bool)row.Cells["Active"].Value;
                if (active)
                {
                    arv.Add(new ActiveRuleValidity() { RuleName = ruleName, Valid = valid });
                }
            }

            bool parse = uint.TryParse(uiMultiplication.Text, out uint scale);
            if ( !parse)
            {
                scale = 1;
            }


            int UKPRN = int.Parse(uiUKPRN.Text);
            string ns = fileNamespace.Text;
            XmlGenerator generator = new XmlGenerator(_rfp, UKPRN);
            var result = generator.CreateAllXml(arv, scale, ns);
            string folder = @"d:\";
            FileWriter.WriteXmlFiles(folder, generator.FileContent(), ns);
            FileWriter.OutputControlFile(folder, result);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SetUpFunctorDataGrid();
            fileNamespace.SelectedIndex = 0;
        }
        public void AddFunctor(ILearnerMultiMutator i)
        {
            bool added = false;
            ++_functorCount;
            foreach (DataGridViewRow existingRow in uiParameters.Rows)
            {
                if (existingRow.Cells["RuleName"].Value.ToString() == i.RuleName())
                {
                    foreach (var funcy in i.LearnerMutators(_cache))
                    {
                        existingRow.Cells["BaseLearner"].Value += " " + funcy.LearnerType.ToString();
                        ++_totalLearners;
                    }
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                int rowid = uiParameters.Rows.Add();
                var row = uiParameters.Rows[rowid];
                row.Cells["RuleName"].Value = i.RuleName();
                row.Cells["Valid"].Value = false;
                row.Cells["Active"].Value = false;
                foreach (var funcy in i.LearnerMutators(_cache))
                {
                    row.Cells["BaseLearner"].Value += " " + funcy.LearnerType.ToString();
                    ++_totalLearners;
                }
            }

        }
        private void SetUpFunctorDataGrid()
        {
            _cache = new DataCache(); 
            _rfp = new RuleToFunctorParser(_cache);
            _functorCount = 0;
            _totalLearners = 0;
            _rfp.CreateFunctors(AddFunctor);
            uiRuleData.Text = $"Discrete rules {uiParameters.Rows.Count} functors detected: {_functorCount} total learners: {_totalLearners}";

        }

        private void uiUKPRN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
