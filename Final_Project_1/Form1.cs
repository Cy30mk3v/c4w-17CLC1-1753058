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
using System.Runtime.InteropServices;

namespace FileExplorer
{
    
    public partial class Form1 : Form
    {
        private SystemIconsImageList sysIcons = new SystemIconsImageList();
        string selectedFolder1, selectedFolder2;
        string curPath1, curPath2;
        int run;
       
        bool show_hidden;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Total Commander (UNREGISTERED) - 17CLC1";
            //Khởi tạo allDrive để liệt kê mọi ổ đĩa có trong máy
            DriveInfo[] allDrive = DriveInfo.GetDrives();
            this.showHiddenToolStripMenuItem.Checked = false;
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
            listView1.LargeImageList = sysIcons.LargeIconsImageList;
            listView1.SmallImageList = sysIcons.SmallIconsImageList;
            listView2.SmallImageList = sysIcons.SmallIconsImageList;
            listView2.LargeImageList = sysIcons.LargeIconsImageList;
            //createFolder();
        }


        

       

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadListView(listView1, out selectedFolder1, imageList1, comboBox1, textBox1, listView2);
        }
    

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            run = 1;
        }

       


        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadListView(listView1, out selectedFolder1, imageList1, comboBox1, textBox1, listView2);
            }
        }

       

        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (run == 1)
            {
                string path = this.listView1.SelectedItems[0].Name;
                textBox1.Text = path;
                //string readText = System.IO.File.ReadAllText(path);
                FileAttributes attr = File.GetAttributes(path);
                Form Form_2 = new Form();
                TextBox tb = new TextBox();
                
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string name = path;
                    DirectoryInfo di = new DirectoryInfo(path);
                    string date = Directory.GetLastWriteTime(name).ToString("dd/MM/yy HH:mm:ss");
                    string volume = (di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length)).ToString();
                }
                else
                {

                }
                Form_2.Controls.Add(tb);
                run = 1;
            }
            else
            {
                string path = this.listView2.SelectedItems[0].Name;
                textBox2.Text = path;
               
                string readText = File.ReadAllText(path);
                MessageBox.Show(readText);
                Form form2 = new Form();
                TextBox txt = new TextBox();
                txt.Text = readText;
                form2.Controls.Add(txt);
                run = 2;
            }

                //else
                    //Chạy chương trình
                   // System.Diagnostics.Process.Start(path);
            
        }

        private void ListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadListView(listView2, out selectedFolder2, imageList2, comboBox2, textBox2, listView1);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                LoadLSfromTextBox(listView1, textBox1, out selectedFolder1, imageList1, comboBox1, listView2);
            }
            
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                LoadLSfromTextBox(listView2, textBox2, out selectedFolder2, imageList2, comboBox2, listView1);
                
            }
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            run = 2;
        }

        private void ListView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadListView(listView2, out selectedFolder2, imageList2, comboBox2, textBox2, listView1);
            }
        }

        private void NewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = createFolder();
            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(name));
            temp.Name = Name;
            temp.ImageIndex = sysIcons.GetIconIndex(name);
            temp.SubItems.Add(" ");
            temp.SubItems.Add("<DIR>");
            var date = Directory.GetLastWriteTime(name);
            temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
            if (run == 1)
            {
               
                listView1.Items.Add(temp);
                listView1.Update();
            }
            else
            {
                listView2.Items.Add(temp);
                listView2.Update();
            }
            
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (run == 1)
            {
                Delete(selectedFolder1);
            }
            else
            {
                Delete(selectedFolder2);
            }
        }

        private void ShowHiddenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showHiddenToolStripMenuItem.Checked)
            {
                show_hidden = true;
                
                LoadListViewR(listView1,selectedFolder1, imageList1, comboBox1, textBox1, listView2);
                LoadListViewR(listView2, selectedFolder2, imageList2, comboBox2, textBox2, listView1);
            }
            else
            {
                show_hidden = false;
                LoadListViewR(listView1, selectedFolder1, imageList1, comboBox1, textBox1, listView2);
                LoadListViewR(listView2, selectedFolder2, imageList2, comboBox2, textBox2, listView1);
            }
            
            return;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            listView1.View = View.List;
            listView2.View = View.List;
            listView1.Refresh();
            listView2.Refresh();

        }

        private void Button8_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView2.View = View.Details;
            listView1.Refresh();
            listView2.Refresh();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            listView2.View = View.Tile;
            listView1.Refresh();
            listView2.Refresh();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            listView2.View = View.LargeIcon;
            listView1.Refresh();
            listView2.Refresh();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            listView1.Refresh();
            listView2.Refresh();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Text = this.comboBox2.SelectedItem.ToString();
            selectedFolder2 = this.comboBox2.SelectedItem.ToString();
            textBox2.Text = selectedFolder2;
            DirectoryInfo d = new DirectoryInfo(comboBox2.Text);
            curPath2 = selectedFolder2;
            FileInfo[] Files = d.GetFiles();
            listView2.Items.Clear();
            string[] fileArray = Directory.GetFiles(selectedFolder2);
            string[] folderArray = Directory.GetDirectories(selectedFolder2);

            foreach (string file in fileArray)
            {
                FileInfo fi = new FileInfo(file);
                if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                {
                    continue;
                }
                long length = fi.Length / 1024;
                string length_temp = length.ToString();
                var date = System.IO.File.GetLastWriteTime(file);
                ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                temp.Name = file;
                temp.ImageIndex = sysIcons.GetIconIndex(file);
                temp.SubItems.Add(Path.GetExtension(file));
                temp.SubItems.Add(length_temp);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView2.Items.Add(temp);
            }
            imageList2.Images.Add("f", DefaultIcons.FolderLarge.ToBitmap());
            foreach (string folder in folderArray)
            {
                FileInfo fi = new FileInfo(folder);
                if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                {
                    continue;
                }
                ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                temp.Name = folder;
                temp.ImageIndex = sysIcons.GetIconIndex(folder);
                temp.SubItems.Add(" ");

                temp.SubItems.Add("<DIR>");
                var date = Directory.GetLastWriteTime(folder);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView2.Items.Add(temp);
            }
            this.listView2.Items[0].Selected = true;
            this.listView2.Items[0].Focused = true;
            return;
        }

        
        

        

        
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox1.Text = this.comboBox1.SelectedItem.ToString();
            selectedFolder1 = this.comboBox1.SelectedItem.ToString();
            textBox1.Text = selectedFolder1;
            DirectoryInfo d = new DirectoryInfo(comboBox1.Text);
            curPath1 = selectedFolder1;
            FileInfo[] Files = d.GetFiles();
            listView1.Items.Clear();
            string[] fileArray = Directory.GetFiles(selectedFolder1);
            string[] folderArray = Directory.GetDirectories(selectedFolder1);
            //ImageList list = new ImageList();
            foreach (string file in fileArray)
            {
                FileInfo fi = new FileInfo(file);
                
                if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                {
                    continue;
                }
                long length = fi.Length / 1024;
                string length_temp = length.ToString();
                var date = System.IO.File.GetLastWriteTime(file);
                ListViewItem temp = new ListViewItem(Path.GetFileName(file));

                temp.ImageIndex = sysIcons.GetIconIndex(file);
                
                
                temp.Name = file;
                temp.SubItems.Add(Path.GetExtension(file));
                temp.SubItems.Add(length_temp);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView1.Items.Add(temp);
            }
            //imageList1.Images.Clear();
            //imageList1.Images.Add("f", DefaultIcons.FolderLarge.ToBitmap());
            foreach (string folder in folderArray)
            {
                FileInfo fi = new FileInfo(folder);
                if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                {
                    continue;
                }
                ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                temp.Name = folder;
                temp.SubItems.Add(" ");
                //var icon = DefaultIcons.FolderLarge;
                temp.ImageIndex = sysIcons.GetIconIndex(folder);
                temp.SubItems.Add("<DIR>");
                var date = Directory.GetLastWriteTime(folder);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView1.Items.Add(temp);
            }
            this.listView1.Items[0].Selected = true;
            this.listView1.Items[0].Focused = true;
            this.listView1.HideSelection = false;
            run = 1;
            return;
        }
    }

       
 
}
