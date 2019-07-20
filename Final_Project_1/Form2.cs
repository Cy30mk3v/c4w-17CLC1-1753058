using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
    public partial class Form2 : Form
    {

        public string cb
        {
            get
            {
                return this.comboBox1.Text;
            }
        }
        public Form2(string file)
        {
            InitializeComponent();
            this.label1.Text = "File: " + file + " has the same name, skip or overwrite";
            
            this.comboBox1.Items.Add("Skip");
            this.comboBox1.Items.Add("Overwrite");
            this.comboBox1.Items.Add("Quit");
            this.button1.Text = "Choose";

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //ComboBox1_SelectedIndexChanged(sender, e);
            //form.cut1 = this.comboBox1.Text;
            this.Close();
        }

        public void getText(out string choose)
        {
            choose = comboBox1.Text;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
         
           // MessageBox.Show(comboBox1.Text);
        }
        
    }
}
