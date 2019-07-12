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
            return (path);
        }

        void Delete(string path)
        {
            if (Directory.EnumerateFiles(path).Count() == 0)
            {

            }
            else
            {
                MessageBoxButtons bt = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Folder has files. Do you still want to continue? ","Warning",bt);
                if (result == DialogResult.Yes)
                {
                    //Delete
                }
                else
                {
                    return;
                }
            }
        }
    }
}



























