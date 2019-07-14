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
using System.Threading;

namespace FileExplorer
{
    
    public partial class Form1 : Form
    {
        private SystemIconsImageList sysIcons = new SystemIconsImageList();
        string selectedFolder1, selectedFolder2;
        string curPath1, curPath2;
        int run;
        int count = 0;
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
            listView2.ContextMenuStrip = contextMenuStrip1;
            listView1.ContextMenuStrip = contextMenuStrip1;
            listView1.MultiSelect = true;
            listView2.MultiSelect = true;
            //listView1.HideSelection = false;
            run = 1;
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
                //textBox1.Text = path;
                //string readText = System.IO.File.ReadAllText(path);
                FileAttributes attr = File.GetAttributes(path);
                
                
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string name = path;
                    Form temp = new Form();
                    TextBox text = new TextBox();
                    RichTextBox rich = new RichTextBox();
                    DirectoryInfo di = new DirectoryInfo(path);
                    rich.Size = temp.Size;
                    
                    try
                    {
                        string volume;
                        double volume_temp = DirectorySize(di,true);
                        string date = Directory.GetLastWriteTime(name).ToString("dd/MM/yy HH:mm:ss");
                        volume = string.Format("{0:#,###0}", volume_temp);

                        rich.AppendText("Path: " + path + "\n");
                        rich.AppendText("Date: " + date + "\n");
                        rich.AppendText("Volume: " + volume + " KB" + "\n");
                        rich.ReadOnly = true;
                        
                    }
                    catch (UnauthorizedAccessException)
                    {
                        rich.Clear();
                        rich.AppendText("Not authorized!");
                    }
                    temp.Controls.Add(rich);
                    temp.Show();
                }
                else
                {
                    StreamReader streamReader = File.OpenText(path);
                    Form temp = new Form();
                    TextBox text = new TextBox();
                    RichTextBox rich = new RichTextBox();
                    rich.ReadOnly = true;
                    rich.Size = temp.Size;
                    string line;
                    


                    rich.Clear();
                    rich.AppendText(File.ReadAllText(path));
                    temp.Controls.Add(rich);
                    temp.Show();


                }
                
                run = 1;
            }
            else
            {
                MessageBox.Show(run.ToString());
                string path = this.listView2.SelectedItems[0].Name;
                textBox2.Text = path;
               
                string readText = File.ReadAllText(path);
                MessageBox.Show(readText);
                Form form2 = new Form();
                TextBox txt = new TextBox();
                
            }

                //else
                    //Chạy chương trình
                   // System.Diagnostics.Process.Start(path);
            
        }

        private void ListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadListView(listView2, out selectedFolder2, imageList2, comboBox2, textBox2, listView1);
            run = 2;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                LoadLSfromTextBox(listView1, textBox1, out selectedFolder1, imageList1, comboBox1, listView2);
                run = 1;
            }
            
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                LoadLSfromTextBox(listView2, textBox2, out selectedFolder2, imageList2, comboBox2, listView1);
                run = 2;
            }
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count++;
                run = 1;
            }
            else
            {
                run = 2;
            }
            
        }

        private void ListView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadListView(listView2, out selectedFolder2, imageList2, comboBox2, textBox2, listView1);
                run = 2;
            }
        }

        private void NewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = createFolder();
            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(name));
            temp.Name = name;
            temp.ImageIndex = sysIcons.GetIconIndex(name);
            temp.SubItems.Add(" ");
            temp.SubItems.Add("<DIR>");
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
            delete();
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
           

            string path = textBox1.Text;
            //tb.Text = path;
            selectedFolder1 = path;
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {

                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(path);
                    string[] folderArray = Directory.GetDirectories(path);
                    listView1.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        //Nếu đường dẫn đã chọn trùng với các thư mục gốc
                        if (path == comboBox1.Items[i].ToString())
                        {

                            flag = true;
                            break;
                        }
                    }

                    if (flag != true)
                    {
                        //Nếu đã chọn quay lại nhưng không phải thư mục gốc
                        ListViewItem temp = new ListViewItem("..");
                        temp.Name = (Directory.GetParent(path).ToString());
                        listView1.Items.Add(temp);
                    }



                    foreach (string file in fileArray)
                    {

                        //Tên hiện bên ngoài (Text): Là tên file
                        FileInfo fi = new FileInfo(file);
                        if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                        {
                            continue;
                        }

                        long f1 = fi.Length / 1024;
                        ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                        //Name ở đây là đường dẫn
                        temp.Name = file;



                        temp.ImageIndex = sysIcons.GetIconIndex(file);
                        

                        temp.SubItems.Add(Path.GetExtension(file));
                        temp.SubItems.Add(f1.ToString());
                        var date = System.IO.File.GetLastWriteTime(file);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView1.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        FileInfo fi = new FileInfo(folder);
                        if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                        {
                            continue;
                        }
                        ListViewItem temp = new ListViewItem(Path.GetFileName(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
                        temp.ImageIndex = sysIcons.GetIconIndex(folder);
                        temp.SubItems.Add(" ");
                        temp.SubItems.Add("<DIR>");
                        var date = Directory.GetLastWriteTime(folder);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView1.Items.Add(temp);
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    //Không có quyền truy cập
                    textBox1.Text = Directory.GetParent(path).ToString();
                    MessageBox.Show("Not Authorized!");
                }

                listView1.Items[0].Selected = true;
                
                //listView2.HideSelection = true;
                listView1.HideSelection = false;
                //listView1.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
                // this.textBox2.Text = Directory.GetParent(path).ToString();
                System.Diagnostics.Process.Start(path);
           

            path = textBox2.Text;
            
            selectedFolder2 = path;
            attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {

                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(path);
                    string[] folderArray = Directory.GetDirectories(path);
                    listView2.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        //Nếu đường dẫn đã chọn trùng với các thư mục gốc
                        if (path == comboBox1.Items[i].ToString())
                        {

                            flag = true;
                            break;
                        }
                    }

                    if (flag != true)
                    {
                        //Nếu đã chọn quay lại nhưng không phải thư mục gốc
                        ListViewItem temp = new ListViewItem("..");
                        temp.Name = (Directory.GetParent(path).ToString());
                        listView2.Items.Add(temp);
                    }



                    foreach (string file in fileArray)
                    {

                        //Tên hiện bên ngoài (Text): Là tên file
                        FileInfo fi = new FileInfo(file);
                        if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                        {
                            continue;
                        }

                        long f1 = fi.Length / 1024;
                        ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                        //Name ở đây là đường dẫn
                        temp.Name = file;



                        temp.ImageIndex = sysIcons.GetIconIndex(file);
                        /*f (fi.Extension == ".lnk")
                        {
                            index = imageList3.Images.Keys.IndexOf(fi.FullName);
                            temp.ImageIndex = index;
                        }*/
                        /* else*/

                        //temp.Name = file;

                        temp.SubItems.Add(Path.GetExtension(file));
                        temp.SubItems.Add(f1.ToString());
                        var date = System.IO.File.GetLastWriteTime(file);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView2.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        FileInfo fi = new FileInfo(folder);
                        if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                        {
                            continue;
                        }
                        ListViewItem temp = new ListViewItem(Path.GetFileName(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
                        temp.ImageIndex = sysIcons.GetIconIndex(folder);
                        temp.SubItems.Add(" ");
                        temp.SubItems.Add("<DIR>");
                        var date = Directory.GetLastWriteTime(folder);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView2.Items.Add(temp);
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    //Không có quyền truy cập
                    textBox2.Text = Directory.GetParent(path).ToString();
                    MessageBox.Show("Not Authorized!");
                }

                listView2.Items[0].Selected = true;

                listView1.HideSelection = true;
                listView2.HideSelection = false;
                listView2.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
                // this.textBox2.Text = Directory.GetParent(path).ToString();
                System.Diagnostics.Process.Start(path);
            listView1.Refresh();
            listView2.Refresh();
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                    run = 1;
                }
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ListView2_MouseClick(object sender, MouseEventArgs e)
        {
            run = 2;
        }

        private void NewFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = createFolder();
            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(name));
            temp.Name = name;
            temp.ImageIndex = sysIcons.GetIconIndex(name);
            temp.SubItems.Add(" ");
            temp.SubItems.Add("<DIR>");
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

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void ListView1_ItemMouseHover_1(object sender, ListViewItemMouseHoverEventArgs e)
        {
            //ListViewItem item = this.listView1.GetItemAt(e.)
            //base.Cursor = Cursors.Hand;
        }

        private void ListView2_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            //base.Cursor = Cursors.Hand;
        }

        
      

        private void ListView1_MouseMove_1(object sender, MouseEventArgs e)
        {
            ListViewItem selected = this.listView1.GetItemAt(e.X, e.Y);
            if (selected == null)
            {
                base.Cursor = Cursors.Default;
            }
            else
            {
                base.Cursor = Cursors.Hand;
            }
        }

        private void ListView2_MouseMove_1(object sender, MouseEventArgs e)
        {
            ListViewItem selected = this.listView2.GetItemAt(e.X, e.Y);
            if (selected == null)
            {
                base.Cursor = Cursors.Default;
            }
            else
            {
                base.Cursor = Cursors.Hand;
            }
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
            //this.listView2.Items[0].Selected = true;
            //this.listView2.Items[0].Focused = true;
            
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
            //this.listView1.Items[0].Selected = true;
            //this.listView1.Items[0].Focused = true;
            //this.listView1.HideSelection = false;
            run = 1;
            return;
        }
    }

       
 
}
