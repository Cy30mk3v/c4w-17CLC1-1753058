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
    public partial class Check_Copy_Cut_Movecs : Form
    {
        public Check_Copy_Cut_Movecs()
        {
            InitializeComponent();
            label1.Text = "Files you choose are have the same name in this folder. Please choose one option";
            
            button1.Text = "Choose";
            //comboBox1.Items.Add("Overwrite");
            comboBox1.Items.Add("Overwrite All");
            //comboBox1.Items.Add("Skip");
            comboBox1.Items.Add("Skip All");
            comboBox1.Items.Add("Choose one by one");
            comboBox1.Items.Add("Quit");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void getText(out string seleted)
        {
            seleted = comboBox1.Text;
        }
    }
}
