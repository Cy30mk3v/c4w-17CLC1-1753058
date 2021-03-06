﻿using System;
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
                    if(listView1.SelectedItems[i].Text=="..")
                    {
                        continue;
                    }
                    DialogResult result = MessageBox.Show("     Do you want to delete this?", "Check", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        return;
                    string key = listView1.SelectedItems[i].Name;
                    //MessageBox.Show(key);
                    FileAttributes attr = File.GetAttributes(key);
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        File.Delete(key);
                        
                        continue;
                    }
                    else

                    {
                 
                        if (!Directory.EnumerateFileSystemEntries(key).Any())
                        {
                            if (Directory.GetDirectories(key).Length != 0)
                            {
                                
                                {
                                    DeleteDirectory(key);
                                   
                                }
                            }
                            else
                            {
                                DeleteDirectory(key);
                               
                            }
                        }
                        else
                        {
                            if (GetDirectorySize(key)!=0)

                            {
                                string name = "Folder: " + listView1.Items[key].Text + " contains data, continues?";
                                DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    DeleteDirectory(key);

                                }
                            }
                            else
                            {
                                DeleteDirectory(key);
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

                    if (listView2.SelectedItems[i].Text == "..")
                    {
                        continue;
                    }
                    DialogResult result = MessageBox.Show("     Do you want to delete this?", "Check", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        return;
                    string key = listView2.SelectedItems[i].Name;
                   
                    FileAttributes attr = File.GetAttributes(key);
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        File.Delete(key);
                        
                        continue;
                    }
                    else
                    {
                        if (!Directory.EnumerateFileSystemEntries(key).Any())
                        {
                            if (Directory.GetDirectories(key).Length != 0)
                            {

                                {
                                    DeleteDirectory(key);

                                }
                            }
                            else
                            {
                                DeleteDirectory(key);

                            }
                        }
                        else
                        {
                            if (GetDirectorySize(key) != 0)

                            {
                                string name = "Folder: " + listView2.Items[key].Text + " contains data, continues?";
                                DialogResult dr = MessageBox.Show(name, "Check", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    DeleteDirectory(key);

                                }
                            }
                            else
                            {
                                DeleteDirectory(key);
                            }

                        }
                    }


                }
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public void copyFunction()
        {
            copy_1 = 1;
            cut = 0;
            if (run == 1)
            {
                for(int i=0;i<listView1.SelectedItems.Count;i++)
                {
                    buffer.Add(listView1.SelectedItems[i]);
                }
            }
            else
            {
                for (int i = 0; i < listView2.SelectedItems.Count; i++)
                {
                    buffer.Add(listView2.SelectedItems[i]);
                }
            }

        }

        public void cutFunction()
        {
            copyFunction();
            copy_1 = 0;
            cut = 1;
        }

        public void pasteFunction()
        {
            if(run==1)
            {
                if (copy_1 == 1)
                {
                    int c = listView1.SelectedItems.Count;
                    bool Over_Write, Over_Write_A, Skip, Skip_A, Quit, choose_1by1;
                    Over_Write = Over_Write_A = Skip = Skip_A = Quit = choose_1by1 = false;
                    int count = 0;
                    for (int i = 0; i < c; i++)
                    {
                        if (checkName1(listView1.SelectedItems[i].Text) == true)
                        {
                            count++;
                        }
                    }
                    if (count > 1)
                    {
                        Check_Copy_Cut_Movecs form = new Check_Copy_Cut_Movecs();
                        form.ShowDialog();
                        form.getText(out cut2);
                        if (cut2 == "Overwrite All")
                        {
                            Over_Write_A = true;
                        }

                        if (cut2 == "Skip All")
                        {
                            Skip_A = true;
                        }

                        if (cut2 == "Choose one by one")
                        {
                            choose_1by1 = true;
                        }
                        if (cut2 == "Quit")
                        {
                            return;
                        }

                    }

                    if (Over_Write_A == true)
                    {

                        for (int i = 0; i < c; i++)
                        {
                            string dest = textBox2.Text;
                            if (listView1.SelectedItems[i].Text == "..")
                            {
                                continue;
                            }
                            if (checkTextBox(listView1.SelectedItems[i].Name))
                            {
                                MessageBox.Show("Can't move this because overwrite parent directory!");
                                continue;
                            }
                            if (checkName1(listView1.SelectedItems[i].Text))
                            {


                                //MessageBox.Show(listView1.SelectedItems[i].Text);
                                FileAttributes attr = File.GetAttributes(listView1.SelectedItems[i].Name);

                                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    // MessageBox.Show(listView1.SelectedItems[i].Name);
                                    //MessageBox.Show(textBox2.Text);
                                    Directory.Delete(textBox2.Text + @"\" + listView1.SelectedItems[i].Text, true);
                                    Directory.Move(listView1.SelectedItems[i].Name, textBox2.Text + @"\" + listView1.SelectedItems[i].Text);

                                }
                                else
                                {
                                    File.Delete(textBox2.Text + @"\" + listView1.SelectedItems[i].Text);
                                    File.Move(listView1.SelectedItems[i].Name, textBox2.Text + @"\" + listView1.SelectedItems[i].Text);
                                }

                            }
                            else
                            {
                                FileAttributes attr = File.GetAttributes(listView1.SelectedItems[i].Name);

                                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    Directory.Move(listView1.SelectedItems[i].Name, textBox2.Text + @"\" + listView1.SelectedItems[i].Text);
                                }
                                else
                                {
                                    File.Move(listView1.SelectedItems[i].Name, textBox2.Text + @"\" + listView1.SelectedItems[i].Text);
                                }
                            }

                        }
                        
                        return;
                    }
                }
                else
                {

                }
            }
            else
            {

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
            string add = @"\New folder";
            path += add;
            //MessageBox.Show(add);
            if(Directory.Exists(path))
            {
                path = path + " (";
                for(int i =1;i<10;i++)
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
            button11.PerformClick();
            return path;
            
        }
       public double size = 0;
        public double GetDirectorySize(string directory)
        {
           
            foreach (string dir in Directory.GetDirectories(directory))
            {
                GetDirectorySize(dir);
            }

            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
            {
                size += file.Length;
            }

            return size;
        }


        bool checkName1(string Name)
        {
            string[] name;
            int c;
            if (run == 1)
            {
                //name = new string[listView2.Items.Count];
                c=listView2.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    //MessageBox.Show(Name);
                    //MessageBox.Show(listView2.Items[i].Text);
                    if (Name == listView2.Items[i].Text)
                    {
                        //MessageBox.Show("aaa");
                        return true;
                    }
                   
                }
                return false;
            }
            else
            {
                //name = new string[listView1.Items.Count];
                c = listView1.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    if (Name == listView1.Items[i].Text)
                        return true;
                }
                return false;
            }
            
        }

        bool checkName2(string Name)
        {
            //string[] name;
            int c;
            if (run == 2)
            {
                //name = new string[listView2.Items.Count];
                c = listView2.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    //MessageBox.Show(Name);
                    //MessageBox.Show(listView2.Items[i].Text);
                    if (Name == listView2.Items[i].Text)
                    {
                        //MessageBox.Show("aaa");
                        return true;
                    }

                }
                return false;
            }
            else
            {
                //name = new string[listView1.Items.Count];
                c = listView1.Items.Count;
                for (int i = 0; i < c; i++)
                {
                    if (Name == listView1.Items[i].Text)
                        return true;
                }
                return false;
            }

        }

        bool checkIFRoot(string path)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (path == comboBox1.Items[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        string getFolderfromPath(string path)
        {
            int index = path.LastIndexOf(@"\");
            string folder_name = path.Substring(index + 1);
            return folder_name;
        }
        bool checkTextBox(string path)
        {
            
            
            if(run==1)
            {
                DirectoryInfo d1 = new DirectoryInfo(textBox1.Text);
                DirectoryInfo d2 = new DirectoryInfo(textBox2.Text);
                
                if (d1.Parent == d2 && getFolderfromPath(path) == getFolderfromPath(textBox1.Text))
                    return true;
                return false;
            }
            else
            {
                DirectoryInfo d1 = new DirectoryInfo(textBox1.Text);
                DirectoryInfo d2 = new DirectoryInfo(textBox2.Text);
                if (d1 == d2.Parent && getFolderfromPath(path) == getFolderfromPath(textBox2.Text))
                    return true;
                return false;
            }
        }

       

        

    }
}



























