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
        void LoadListView(ListView list,out string selectedFolder, ImageList image, ComboBox cb, TextBox tb, ListView other)
        {
            if (list.Name == "listView1")
            {
                run = 1;
                //MessageBox.Show("Yay");
            }
            else
            {
                run = 2;
            }
            
            string path = list.SelectedItems[0].Name;
            tb.Text = path;
            selectedFolder = path;
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {

                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(path);
                    string[] folderArray = Directory.GetDirectories(path);
                    list.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        //Nếu đường dẫn đã chọn trùng với các thư mục gốc
                        if (path == cb.Items[i].ToString())
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
                        list.Items.Add(temp);
                    }


                    
                    foreach (string file in fileArray)
                    {
                        
                        //Tên hiện bên ngoài (Text): Là tên file
                        FileInfo fi = new FileInfo(file);
                        if((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
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
                        list.Items.Add(temp);
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
                        list.Items.Add(temp);
                    }
                    
                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    //Không có quyền truy cập
                    tb.Text = Directory.GetParent(path).ToString();
                    MessageBox.Show("Not Authorized!");
                }

                list.Items[0].Selected = true;

                other.HideSelection = true;
                list.HideSelection = false;
                list.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
                // this.textBox2.Text = Directory.GetParent(path).ToString();
                System.Diagnostics.Process.Start(path);
        }

        void LoadListViewR(ListView list, string selectedFolder, ImageList image, ComboBox cb, TextBox tb, ListView other)
        {
            if (list.Name == "listView1")
            {
                run = 1;
                //MessageBox.Show("Yay");
            }
            else
            {
                run = 2;
            }
            string path = selectedFolder;
            tb.Text = path;
            //selectedFolder = path;
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {

                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(path);
                    string[] folderArray = Directory.GetDirectories(path);
                    list.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        //Nếu đường dẫn đã chọn trùng với các thư mục gốc
                        if (path == cb.Items[i].ToString())
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
                        list.Items.Add(temp);
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
                        list.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        FileInfo fi = new FileInfo(folder);
                        if ((fi.Attributes.HasFlag(FileAttributes.Hidden) && show_hidden == false))
                        {
                            continue;
                        }
                        ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
                        temp.ImageIndex = sysIcons.GetIconIndex(folder);
                        temp.SubItems.Add(" ");
                        temp.SubItems.Add("<DIR>");
                        var date = Directory.GetLastWriteTime(folder);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        list.Items.Add(temp);
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    //Không có quyền truy cập
                    tb.Text = Directory.GetParent(path).ToString();
                    MessageBox.Show("Not Authorized!");
                }

                list.Items[0].Selected = true;

                other.HideSelection = true;
                list.HideSelection = false;
                list.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
                // this.textBox2.Text = Directory.GetParent(path).ToString();
                System.Diagnostics.Process.Start(path);
        }
        void copy()
        { 
}
        void LoadLSfromTextBox(ListView list, TextBox tb, out string selectedFolder, ImageList image, ComboBox cb, ListView other)
        {
            if (tb.Name == "textBox1")
            {
                run = 1;
            }
            else
            {
                run = 2;
            }
            string path = tb.Text;
            selectedFolder = path;
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                try
                {

                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] fileArray = Directory.GetFiles(path);
                    string[] folderArray = Directory.GetDirectories(path);
                    list.Items.Clear();
                    bool flag = false;
                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        //Nếu đường dẫn đã chọn trùng với các thư mục gốc
                        if (path == cb.Items[i].ToString())
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
                        list.Items.Add(temp);
                    }



                    foreach (string file in fileArray)
                    {
                        //Tên hiện bên ngoài (Text): Là tên file
                        FileInfo fi = new FileInfo(file);
                        long f1 = fi.Length / 1024;
                        ListViewItem temp = new ListViewItem(Path.GetFileName(file));
                        //Name ở đây là đường dẫn
                      
                        


                        temp.ImageIndex = sysIcons.GetIconIndex(file);
                        temp.Name = file;
                        temp.SubItems.Add(Path.GetExtension(file));
                        temp.SubItems.Add(f1.ToString());
                        var date = System.IO.File.GetLastWriteTime(file);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        list.Items.Add(temp);
                    }
                    foreach (string folder in folderArray)
                    {
                        ListViewItem temp = new ListViewItem(Path.GetFileNameWithoutExtension(folder));
                        //Name ở đây là đường dẫn
                        temp.Name = folder;
                        temp.ImageIndex = sysIcons.GetIconIndex(folder);
                        temp.SubItems.Add(" ");
                        temp.SubItems.Add("<DIR>");
                        var date = Directory.GetLastWriteTime(folder);
                        temp.SubItems.Add(date.ToString("dd/MM/yy HH:mm:ss"));
                        list.Items.Add(temp);
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    //Không có quyền truy cập
                    MessageBox.Show("Not Authorized!");
                }

                list.Items[0].Selected = true;

                other.HideSelection = true;
                list.HideSelection = false;
                list.Items[0].Focused = true;

            }

            else
                //Chạy chương trình
                System.Diagnostics.Process.Start(path);
        }
    }
}
