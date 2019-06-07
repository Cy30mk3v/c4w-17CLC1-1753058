using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int col = 1;
        int row = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void CreateForm_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            FlowLayoutPanel flowPanel = new FlowLayoutPanel();
            flowPanel.AutoSize = true;


            flowPanel.Size = new System.Drawing.Size(1, 1);
            this.Controls.Add(flowPanel);
            this.col = Convert.ToInt32(TextCol.Text);
            this.row = Convert.ToInt32(TextRow.Text);
            if (!int.TryParse(TextCol.Text, out col))
            {
                this.col = 1;
            }
            
            if (!int.TryParse(TextRow.Text, out row))
            {
                this.row = 1;
            }

            int result = this.col * this.row;
            int top = 0;
            int left = 0;
            List<Button> buttons = new List<Button>();
           
            
            for (int i = 0; i < result; i++)
            {
                Button newButton = new Button();
                if ((i+1) % col == 0 && i>0)
                {
                    flowPanel.SetFlowBreak(newButton, true);
                }
                newButton.Width = 40;
                newButton.Height = 40;
                newButton.Left = left;
                newButton.Top = top;
                newButton.Text = (Convert.ToString(i));
                buttons.Add(newButton);
                newButton.Click += new EventHandler(this.buttonClick);
                flowPanel.Controls.Add(newButton);
                
                left += newButton.Width + left;

            }

        }
        void buttonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int n = Convert.ToInt32(btn.Text);
            
            int x = n % this.col;
            int y = (n - x) / this.col;
            string Col1 = x.ToString();
            string Row1 = y.ToString();
          
            MessageBox.Show(Col1 + "," + Row1);
        }

    }
}

