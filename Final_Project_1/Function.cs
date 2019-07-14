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
        public static IEnumerable<string> GetFiles(string root, string searchPattern)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern);
                }
                catch { }
                if (next != null && next.Length != 0)
                    foreach (var file in next) yield return file;
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch { }
            }
        }
        static long DirectorySize(DirectoryInfo dInfo, bool includeSubDir)
        {
            long totalSize = dInfo.EnumerateFiles()
                         .Sum(file => file.Length);
            if (includeSubDir)
            {
                totalSize += dInfo.EnumerateDirectories()
                         .Sum(dir => DirectorySize(dir, true));
            }
            return totalSize;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }
            Thread.Sleep(1);
            Directory.Delete(target_dir, false);

        }


        void delete()
        {
            if (run == 1)
            {

                int c = listView1.SelectedItems.Count;
                for (int i = 0; i < c; i++)
                {
                    DialogResult result = MessageBox.Show("     Do you want to delete this?", "Check", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        return;
                    string key = listView1.SelectedItems[0].Name;
                    MessageBox.Show(key);
                    FileAttributes attr = File.GetAttributes(key);
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        File.Delete(key);
                        listView1.Items[key].Remove();
                        listView1.Update();
                        continue;
                    }
                    else

                    {
                        if (!Directory.EnumerateFiles(key).Any())
                        {
                            if (Directory.GetDirectories(key) != null)
                            {
                                string name = "Folder: " + listView1.Items[key].Text + " contains data, continues?";
                                DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    DeleteDirectory(key);
                                    listView1.Items[key].Remove();
                                    listView1.Update();
                                }
                            }
                        }
                        else
                        {

                            string name = "Folder: " + listView1.Items[key].Text + " contains data, continues?";
                            DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                DeleteDirectory(key);
                                listView1.Items[key].Remove();
                                listView1.Update();
                            }

                        }
                    }


                }
            }
            else
            {
                int c = listView2.SelectedItems.Count;
                for (int i = 0; i < c; i++)
                {
                    DialogResult result = MessageBox.Show("     Do you want to delete this?", "Check", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        return;
                    string key = listView2.SelectedItems[0].Name;
                   
                    FileAttributes attr = File.GetAttributes(key);
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        File.Delete(key);
                        listView2.Items[key].Remove();
                        listView2.Update();
                        continue;
                    }
                    else
                    {
                        if (!Directory.EnumerateFiles(key).Any())
                        {
                            if (Directory.GetDirectories(key) != null)
                            {
                                string name = "Folder: " + listView1.Items[key].Text + " contains data, continues?";
                                DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    DeleteDirectory(key);
                                    listView2.Items[key].Remove();
                                    listView2.Update();
                                }
                            }
                        }
                        else
                        {

                            string name = "Folder: " + listView1.Items[key].Text + " contains data, continues?";
                            DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                DeleteDirectory(key);
                                listView2.Items[key].Remove();
                                listView2.Update();
                            }

                        }
                    }


                }
            }
        }
        string createFolder()
        {
            string path;
            //MessageBox.Show(selectedFolder1);
            if (run == 1)
            {
                path = selectedFolder1;
            }
            else
            {
                path = selectedFolder2;
            }
            string add = @"\New Folder";
            path += add;
            //MessageBox.Show(add);
            if(Directory.Exists(path))
            {
                path = path + " (";
                for(int i =1;i<100;i++)
                {
                    path = path + i.ToString() + ")";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                        break;
                    }
                    else
                    {
                        path = path.Remove(path.Length - 2);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            return path;
            
        }

       
    }
}



























