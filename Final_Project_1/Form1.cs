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
        public static class DefaultIcons
        {
            private static readonly Lazy<Icon> _lazyFolderIcon = new Lazy<Icon>(FetchIcon, true);

            public static Icon FolderLarge
            {
                get { return _lazyFolderIcon.Value; }
            }

            private static Icon FetchIcon()
            {
                var tmpDir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())).FullName;
                var icon = ExtractFromPath(tmpDir);
                Directory.Delete(tmpDir);
                return icon;
            }

            private static Icon ExtractFromPath(string path)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(
                    path,
                    0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_LARGEICON);
                return System.Drawing.Icon.FromHandle(shinfo.hIcon);
            }

            //Struct used by SHGetFileInfo function
            [StructLayout(LayoutKind.Sequential)]
            private struct SHFILEINFO
            {
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            };

            [DllImport("shell32.dll")]
            private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

            private const uint SHGFI_ICON = 0x100;
            private const uint SHGFI_LARGEICON = 0x0;
            private const uint SHGFI_SMALLICON = 0x000000001;
        }
        string selectedFolder1, selectedFolder2;
        string curPath1, curPath2;
        int run;
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


        private void HelpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("            Developed by Vo Tran Chi Hung\n                  Ma so sinh vien: 1753058\n                             FIT-HCMUS");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            run = 1;
            string path = this.listView1.SelectedItems[0].Name;
            textBox1.Text = path;
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
                    DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                    
                    if (flag != true)
                    {
                        //Nếu đã chọn quay lại nhưng không phải thư mục gốc
                        ListViewItem temp = new ListViewItem("..");
                        temp.Name = (Directory.GetParent(path).ToString());
                        listView1.Items.Add(temp);
                    }


                    ImageList list = new ImageList();
                    foreach (string file in fileArray)
                    {
                        //Tên hiện bên ngoài (Text): Là tên file
                        FileInfo fi = new FileInfo(file);
                        long f1 = fi.Length / 1024;
                        ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                        //Name ở đây là đường dẫn

                        if (!imageList1.Images.ContainsKey(fi.Extension))
                        {

                            imageList1.Images.Add(fi.Extension, Icon.ExtractAssociatedIcon(file));
                        }
                        int index = imageList1.Images.Keys.IndexOf(fi.Extension);
                        temp.ImageIndex = index;
                        temp.Name = file;
                        temp.SubItems.Add(Path.GetExtension(file));
                        temp.SubItems.Add(f1.ToString());
                        var date = System.IO.File.GetLastWriteTime(file);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView1.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
                        temp.ImageKey = "f";
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
                    MessageBox.Show("Not Authorized!");
                }
              
                this.listView1.Items[0].Selected = true;
                
                this.listView1.HideSelection = false;
                this.listView1.Items[0].Focused = true;
                run = 1;
            }
            
            else
                //Chạy chương trình
                System.Diagnostics.Process.Start(path);
        }
    

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            run = 1;
        }

       
        private void Label1_Click(object sender, EventArgs e)
        {
            //label1.Text = selectedFolder1;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listView1.SelectedItems.Count == 0)
                    return;
                run = 1;
                string path = this.listView1.SelectedItems[0].Name;
                textBox1.Text = path;
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
                            long f1 = fi.Length / 1024;
                            ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                            //Name ở đây là đường dẫn
                            temp.Name = file;
                            if (!imageList1.Images.ContainsKey(fi.Extension))
                            {

                                imageList1.Images.Add(fi.Extension, Icon.ExtractAssociatedIcon(file));
                            }
                            int index = imageList1.Images.Keys.IndexOf(fi.Extension);
                            temp.ImageIndex = index;
                            
                            temp.SubItems.Add(Path.GetExtension(file));
                            temp.SubItems.Add(f1.ToString());
                            var date = System.IO.File.GetLastWriteTime(file);
                            temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                            listView1.Items.Add(temp);
                        }
                        foreach (string folder in folderArray)
                        {
                            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                            //Name ở đây là đường dẫn
                            temp.Name = folder;
                            temp.ImageKey = "f";
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
                        this.textBox1.Text = Directory.GetParent(path).ToString();
                        MessageBox.Show("Not Authorized!");
                    }

                    this.listView1.Items[0].Selected = true;

                    this.listView2.HideSelection = true;
                    this.listView1.HideSelection = false;
                    this.listView1.Items[0].Focused = true;

                }

                else
                    //Chạy chương trình
                    // this.textBox2.Text = Directory.GetParent(path).ToString();
                    System.Diagnostics.Process.Start(path);
            }
        }

        void loadListView(ListView LS)
        {

        }

        private void ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (run == 1)
            {
                string path = this.listView1.SelectedItems[0].Name;
                textBox1.Text = path;
                string readText = System.IO.File.ReadAllText(path);
                Form form2 = new Form();
                //Console.WriteLine(readText);
                MessageBox.Show(readText);
                TextBox txt = new TextBox();
                txt.Text = readText;
                form2.Controls.Add(txt);
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
            if (listView2.SelectedItems.Count == 0)
                return;
            run = 2;
            string path = this.listView2.SelectedItems[0].Name;
            textBox2.Text = path;
            FileAttributes attr = File.GetAttributes(path);
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
                        FileInfo f = new FileInfo(file);
                        long f1 = f.Length / 1024;
                        ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                        //Name ở đây là đường dẫn
                        Icon iconFile = System.Drawing.Icon.ExtractAssociatedIcon(file);
                        //temp.ImageKey = 
                        temp.Name = file;
                        temp.SubItems.Add(Path.GetExtension(file));
                        temp.SubItems.Add(f1.ToString());
                        var date = System.IO.File.GetLastWriteTime(file);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        listView2.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
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
                    this.textBox2.Text = Directory.GetParent(path).ToString();
                    MessageBox.Show("Not Authorized!");
                }

                this.listView2.Items[0].Selected = true;

                this.listView1.HideSelection = true;
                this.listView2.HideSelection = false;
                this.listView2.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
               // this.textBox2.Text = Directory.GetParent(path).ToString();
                System.Diagnostics.Process.Start(path);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                run = 1;
                string path = this.textBox1.Text;
                
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
                            FileInfo f = new FileInfo(file);
                            long f1 = f.Length / 1024;
                            ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                            //Name ở đây là đường dẫn
                            Icon iconFile = System.Drawing.Icon.ExtractAssociatedIcon(file);
                            //temp.ImageKey = 
                            temp.Name = file;
                            temp.SubItems.Add(Path.GetExtension(file));
                            temp.SubItems.Add(f1.ToString());
                            var date = System.IO.File.GetLastWriteTime(file);
                            temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                            listView1.Items.Add(temp);
                        }
                        foreach (string folder in folderArray)
                        {
                            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                            //Name ở đây là đường dẫn
                            temp.Name = folder;
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
                        MessageBox.Show("Not Authorized!");
                    }


                }
                else
                    //Chạy chương trình
                    System.Diagnostics.Process.Start(path);
            }
            
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                run = 2;
                string path = this.textBox2.Text;
                
                FileAttributes attr = File.GetAttributes(path);
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
                            if (path == comboBox2.Items[i].ToString())
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
                            FileInfo f = new FileInfo(file);
                            long f1 = f.Length / 1024;
                            ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                            //Name ở đây là đường dẫn
                            Icon iconFile = System.Drawing.Icon.ExtractAssociatedIcon(file);
                            //temp.ImageKey = 
                            temp.Name = file;
                            temp.SubItems.Add(Path.GetExtension(file));
                            temp.SubItems.Add(f1.ToString());
                            var date = System.IO.File.GetLastWriteTime(file);
                            temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                            listView2.Items.Add(temp);
                        }
                        foreach (string folder in folderArray)
                        {
                            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                            //Name ở đây là đường dẫn
                            temp.Name = folder;
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
                        MessageBox.Show("Not Authorized!");
                    }

                    this.listView2.Items[0].Selected = true;

                    this.listView1.HideSelection = true;
                    this.listView2.HideSelection = false;
                    this.listView2.Items[0].Focused = true;

                }

                else
                    //Chạy chương trình
                    System.Diagnostics.Process.Start(path);
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
                if (listView2.SelectedItems.Count == 0)
                    return;
                run = 2;
                string path = this.listView2.SelectedItems[0].Name;
                textBox2.Text = path;
                FileAttributes attr = File.GetAttributes(path);
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
                            FileInfo f = new FileInfo(file);
                            long f1 = f.Length / 1024;
                            ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                            //Name ở đây là đường dẫn
                            Icon iconFile = System.Drawing.Icon.ExtractAssociatedIcon(file);
                            //temp.ImageKey = 
                            temp.Name = file;
                            temp.SubItems.Add(Path.GetExtension(file));
                            temp.SubItems.Add(f1.ToString());
                            var date = System.IO.File.GetLastWriteTime(file);
                            temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                            listView2.Items.Add(temp);
                        }
                        foreach (string folder in folderArray)
                        {
                            ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                            //Name ở đây là đường dẫn
                            temp.Name = folder;
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
                        this.textBox2.Text = Directory.GetParent(path).ToString();
                        MessageBox.Show("Not Authorized!");
                    }

                    this.listView2.Items[0].Selected = true;

                    this.listView1.HideSelection = true;
                    this.listView2.HideSelection = false;
                    this.listView2.Items[0].Focused = true;

                }

                else
                    //Chạy chương trình
                    // this.textBox2.Text = Directory.GetParent(path).ToString();
                    System.Diagnostics.Process.Start(path);
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
                long length = fi.Length / 1024;
                string length_temp = length.ToString();
                var date = System.IO.File.GetLastWriteTime(file);
                ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                temp.Name = file;
                temp.SubItems.Add(Path.GetExtension(file));
                temp.SubItems.Add(length_temp);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView2.Items.Add(temp);
            }
            foreach (string folder in folderArray)
            {
                ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                temp.Name = folder;
                temp.SubItems.Add(" ");

                temp.SubItems.Add("<DIR>");
                var date = Directory.GetLastWriteTime(folder);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView2.Items.Add(temp);
            }
            this.listView2.Items[0].Selected = true;
            //this.listView2.HideSelection = false;
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
                long length = fi.Length / 1024;
                string length_temp = length.ToString();
                var date = System.IO.File.GetLastWriteTime(file);
                ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                
                if(!imageList1.Images.ContainsKey(fi.Extension))
                {
                   
                    imageList1.Images.Add(fi.Extension, Icon.ExtractAssociatedIcon(file));
                }
                int index = imageList1.Images.Keys.IndexOf(fi.Extension);
                temp.ImageIndex = index;
                
                
                temp.Name = file;
                temp.SubItems.Add(Path.GetExtension(file));
                temp.SubItems.Add(length_temp);
                temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                listView1.Items.Add(temp);
            }
            //imageList1.Images.Clear();
            imageList1.Images.Add("f", DefaultIcons.FolderLarge.ToBitmap());
            foreach (string folder in folderArray)
            {
                ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                temp.Name = folder;
                temp.SubItems.Add(" ");
                //var icon = DefaultIcons.FolderLarge;
                temp.ImageKey = "f";
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
