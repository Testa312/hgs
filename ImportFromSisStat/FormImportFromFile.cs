using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlacialComponents.Controls;
using System.IO;
namespace ImportFromSisStat
{
    public partial class FormImportFromFile : Form
    {
        List<point> lspt = new List<point>();
        public FormImportFromFile()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath,Encoding.Default);
                List<GLItem> lsItem = new List<GLItem>();

                glacialList1.Items.Clear();
                int c = 0;
                foreach (string l in lines)
                {
                    string[] values = l.Split(',');
                    if (values.Length < 9) continue;
                    if (values[0].Contains("测点")) continue;

                    GLItem itemn = new GLItem(glacialList1);
                    lsItem.Add(itemn);
                    point pt = new point();
                    double max,min;
                    if (checkBox_min.Checked && double.TryParse(values[3], out min))
                    {
                        pt.ll = min;
                        itemn.SubItems["Min"].Text = min.ToString(); 
                    }
                    if (checkBox_min.Checked && double.TryParse(values[5], out max))
                    {
                        pt.hl = max;
                        itemn.SubItems["Max"].Text = max.ToString();
                    }
                    pt.pn  = itemn.SubItems["PN"].Text =values[0];
                    
                    pt.ed = itemn.SubItems["ED"].Text = values[1];

                    c++;
                }
                glacialList1.Items.AddRange(lsItem.ToArray());
                toolStripStatusLabel1.Text = "点数：" + c.ToString();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            glacialList1.Items.Clear();
            glacialList1.Invalidate();
        }
    }
}
