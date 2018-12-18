using MainOccupancyCompare.Model;
using MainOccupancyCompare.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainOccupancyCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uiBrowse1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if( DialogResult.OK == dlg.ShowDialog() )
                {
                    uiFilepath1.Text = dlg.FileName;
                }
            }
        }

        private void uiBrowse2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    uiFilepath2.Text = dlg.FileName;
                }
            }
        }

        private void FoundSomething(CompareFinding cf)
        {
            if (cf.FindingType == CompareFindingType.SectionComplete)
            {
                uiProgress.Value++;
            }
            else
            {
                ListViewItem lvi = uiFindingsList.Items.Add(cf.FindingType.ToString());
                lvi.SubItems.Add(cf.Source.ToString());
                switch (cf.Source)
                {
                    case Source.LeftHandSide:
                        lvi.SubItems.Add(cf.SectionName);
                        lvi.SubItems.Add(cf.LHSKey);
                        lvi.SubItems.Add(cf.ColumnName);
                        lvi.SubItems.Add(cf.LHSValue);
                        lvi.SubItems.Add(cf.RHSValue);
                        break;
                    default:
                        lvi.SubItems.Add(cf.SectionName);
                        lvi.SubItems.Add(cf.RHSKey);
                        lvi.SubItems.Add(cf.ColumnName);
                        lvi.SubItems.Add(cf.RHSValue);
                        lvi.SubItems.Add(cf.LHSValue);
                        break;
                }
            }
        }

        private void uiCompare_Click(object sender, EventArgs e)
        {
            uiFindingsList.Items.Clear();
            uiProgress.Value = 0;
            ReportBuilderService builderService = new ReportBuilderService();
            Report lhs = builderService.BuildReport(uiFilepath1.Text);
            Report rhs = builderService.BuildReport(uiFilepath2.Text);
            uiProgress.Maximum = lhs.SectionNames().Count() + rhs.SectionNames().Count();
            builderService.CompareReports(lhs, rhs, FoundSomething);
            uiProgress.Value = 0;
        }

        private void uiClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder clip = new StringBuilder();
            ListView.ColumnHeaderCollection column = uiFindingsList.Columns;
            char quote = '"';
            for(int i = 0;i != column.Count; ++i )
            {
                clip.Append($"{quote}{column[i].Text}{quote},");
            }
            clip.AppendLine();
            for(int lvi =0;lvi != uiFindingsList.Items.Count; ++lvi)
            {
                for (int j = 0; j != uiFindingsList.Items[lvi].SubItems.Count; ++j)
                {
                    clip.Append($"{quote}{uiFindingsList.Items[lvi].SubItems[j].Text}{quote},");
                }
                clip.AppendLine();
            }
            Clipboard.SetText(clip.ToString());
        }
    }
}
