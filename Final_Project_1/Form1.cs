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
        string curPath1;
        string curPath2;
        int count = 0;
        public Form1()
        {
            
            InitializeComponent();
            this.Text = "Total Commander (UNREGISTERED) - 17CLC1";
            //Khởi tạo allDrive để liệt kê mọi ổ đĩa có trong máy
            DriveInfo[] allDrive = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrive)
            {
                if (d.IsReady)
                    //Nếu ổ tồn tại, thêm lần lượt vào 2 ô combo box
                    comboBox1.Items.Add(d);
                    comboBox2.Items.Add(d);
                
            }
            //Gán các giá trị mặc định ban đầu được chọn là cái đầu tiên (index = 0)
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }


        
        private void Form1_Load(object sender, EventArgs e)
        {

            

        }

        void loadDrive(ComboBox cb, ListView LS)
        {
            if (!Directory.Exists(cb.Text))
            {
                return;
            }
            else
            {

                cb.Text = cb.SelectedItem.ToString();
                DirectoryInfo d = new DirectoryInfo(cb.Text);
                
                FileInfo[] Files = d.GetFiles();
                LS.Items.Clear();

                foreach (FileInfo file in Files)
                {
                    LS.Items.Add(file.FullName);
                }
                return;
            }
        }
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count==0)
                return;
            string path = this.listView1.SelectedItems[0].Text;
            
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {
                    selectedFolder1 = path;
                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(selectedFolder1);
                    string[] folderArray = Directory.GetDirectories(selectedFolder1);
                    listView1.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (path == comboBox1.Items[i].ToString())
                        {
                            flag = true;
                            break;

                        }
                    }

                    if (flag == true)
                    {
                        listView1.Items.Add(path);

                    }
                    else
                    {
                        listView1.Items.Add(Directory.GetParent(path).ToString());
                    }
                    listView1.Items[0].Name = "..";
                    foreach (string file in fileArray)
                    {
                        listView1.Items.Add(file);
                    }
                    foreach (string folder in folderArray)
                    {
                        listView1.Items.Add(folder);
                    }

                    return;
                }
                catch(UnauthorizedAccessException)
                {
                    MessageBox.Show("Not Authorized!");
                }
                
                
            }
            else
                System.Diagnostics.Process.Start(path);
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
                return;
            string path = this.listView2.SelectedItems[0].Text;

            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                selectedFolder2 = path;
                DirectoryInfo d2 = new DirectoryInfo(path);
                string[] fileArray2 = Directory.GetFiles(selectedFolder2);
                string[] folderArray2 = Directory.GetDirectories(selectedFolder2);
                listView2.Items.Clear();
                
                bool flag = false;
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (path == comboBox2.Items[i].ToString())
                    {
                        flag = true;
                        break;

                    }
                }
                if (flag == true)
                {
                    listView2.Items.Add(path);
                }
                else
                {
                    listView2.Items.Add(Directory.GetParent(path).ToString());
                }
                
                foreach (string file in fileArray2)
                {
                    listView2.Items.Add(file);
                }
                foreach (string folder in folderArray2)
                {
                    listView2.Items.Add(folder);
                }
                return;
            }
            else
                System.Diagnostics.Process.Start(path);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.Text = this.comboBox1.SelectedItem.ToString();
            selectedFolder1 = this.comboBox1.SelectedItem.ToString();
            DirectoryInfo d = new DirectoryInfo(comboBox1.Text);
            curPath1 = selectedFolder1;
            FileInfo[] Files = d.GetFiles();
            listView1.Items.Clear();
            string[] fileArray = Directory.GetFiles(selectedFolder1);
            string[] folderArray = Directory.GetDirectories(selectedFolder1);
            foreach (string file in fileArray)
            {
                listView1.Items.Add(file);
            }
            foreach (string folder in folderArray)
            {
                listView1.Items.Add(folder);
            }
            return;


        } 


        private void ComboBox1_SelectedValueChanged(object sender, EventArgs s)
        {
           
        }

        
        private void ComboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("            Developed by Vo Tran Chi Hung\n                  Ma so sinh vien: 1753058\n                             FIT-HCMUS");
            

        }

        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Text = this.comboBox2.SelectedItem.ToString();
            selectedFolder2 = this.comboBox2.Text;
            DirectoryInfo d1 = new DirectoryInfo(comboBox2.Text);
            curPath2 = selectedFolder2;
            FileInfo[] Files = d1.GetFiles();
            listView2.Items.Clear();
            string[] fileArray1 = Directory.GetFiles(selectedFolder2);
            string[] folderArray1 = Directory.GetDirectories(selectedFolder2);
            foreach (string file in fileArray1)
            {
                listView2.Items.Add(file);
            }
            foreach (string folder in folderArray1)
            {
                listView2.Items.Add(folder);
            }
            return;
        }
    }
}
