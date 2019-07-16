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
    public partial class rename : Form
    {

        public string new_Path;
        public rename()
        {
            InitializeComponent();
        }

        private void Rename_Load(object sender, EventArgs e)
        {
            button1.Text = "Rename";
        }

        public void setLabel1(string name)
        {
            label1.Text = name + ", input new name:";
        }

      

        private void Button1_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Empty, type again!");
                
            }
            else
            {
                new_Path = textBox1.Text;
                this.Close();
            }
        }
    }
}
