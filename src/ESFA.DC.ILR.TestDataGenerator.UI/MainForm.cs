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
using System.Xml.Xsl;
using System.Xml;
using System.IO;

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

            bool parse = int.TryParse(uiMultiplication.Text, out int scale);
            if ( !parse)
            {
                scale = 1;
            }

            string ukprnText = uiUKPRN.Text;
            string[] ukprns;
            if(ukprnText.Contains(","))
            {
                ukprns = ukprnText.Split(',');
            }
            else
            {
                ukprns = new string[] { ukprnText };
            }
            foreach (var s in ukprns)
            {
                int UKPRN = int.Parse(s);
                string ns = fileNamespace.Text;
                XmlGenerator generator = new XmlGenerator(_rfp, UKPRN);
                var result = generator.CreateAllXml(arv, scale, ns);
                string folder = @"d:\ilr\";
                FileWriter.WriteXmlFiles(folder, generator.FileContent(), ns);
                FileWriter.OutputControlFile(folder, result);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Generate XDS input from OPA with XSLT.
            string xmlInput = @"d:\xds_case.xml";
            string xsltInput = @"C:\FISDownloads\ComponentSets17181718ILRFISDCSSILR17182018030762964\rulebases\val\ilr validation learner - input_xslt.xsl";
            GenerateXdsInput(xsltInput, xmlInput, true);
        }

        public string GenerateXdsInput(string xsltPath, string xmlPath, bool _xDSLogging)
        {
            XslCompiledTransform xsltProcessor = new XslCompiledTransform();
            xsltProcessor.Load(xsltPath);

            string content = File.ReadAllText(xmlPath);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            string xdsInput;
            using (System.IO.StringWriter stringWriter = new System.IO.StringWriter())
            {
                xsltProcessor.Transform(xmlDocument.CreateNavigator(), (XsltArgumentList)null, (TextWriter)stringWriter);
                xdsInput = stringWriter.ToString();
                //Switch to "Dump" XDS
                if (_xDSLogging)
                {
                    System.IO.File.WriteAllText(@"d:\xds_output.xml", xdsInput);
                }
            }

            return xdsInput;
        }

        private void uiGenerateULNs_Click(object sender, EventArgs e)
        {
            string folder = @"d:\ilr\";
            bool parse = int.TryParse(uiMultiplication.Text, out int scale);
            if (!parse)
            {
                scale = 1;
            }
            const int maxFileSize = 5000000;
            int generation = 1;
            List<string> ulns = new List<string>(maxFileSize);
            for (int index = 0; index != scale; ++index)
            {
                try
                {
                    string uln = ListOfULNs.ULN(index).ToString();
                    ulns.Add( $"{uln}");
                    if ((index+1) % maxFileSize == 0)
                    {
                        string filename = Path.Combine(folder, $"ulns{generation++}.txt");
                        File.WriteAllLines(filename, ulns);
                        ulns.Clear();
                    }
                }
                catch { }
            }
            string filenameEnd = Path.Combine(folder, $"ulns{generation++}.txt");
            File.WriteAllLines(filenameEnd, ulns);
            ulns.Clear();
        }

        private void uiSetAllActive_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow existingRow in uiParameters.Rows)
            {
                existingRow.Cells["Active"].Value = uiSetAllActive.Checked;
            }
        }

        private void uiSetAllValid_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow existingRow in uiParameters.Rows)
            {
                existingRow.Cells["Valid"].Value = uiSetAllValid.Checked;
            }
        }
    }
}
