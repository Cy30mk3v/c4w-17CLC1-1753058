using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileExplorer
{
    public partial class Form1 : Form
    {

        string []path;
        string selectedFolder1;
        string selectedFolder2;
        DriveInfo Selected_Folder1;
        
        int count = 0;
        public Form1()
        {
            
            InitializeComponent();
            this.Text = "Total Commander (UNREGISTERED) - 17CLC1";
            DriveInfo[] allDrive = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrive)
            {
                if (d.IsReady)
                    comboBox1.Items.Add(d);
                    comboBox2.Items.Add(d);
                
            }
            
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

            MessageBox.Show(comboBox1.Text);
            if(!Directory.Exists(comboBox1.Text))
            {
                return;
            }
            DirectoryInfo d = new DirectoryInfo(comboBox1.Text);
            FileInfo[] Files = d.GetFiles();
            listView1.Items.Clear();
            foreach (FileInfo file in Files)
            {
                listView1.Items.Add(file.FullName);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
            
        } 


        private void ComboBox1_SelectedValueChanged(object sender, EventArgs s)
        {
           
        }

        void getDirectory()
        {
            
        }
        private void ComboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
