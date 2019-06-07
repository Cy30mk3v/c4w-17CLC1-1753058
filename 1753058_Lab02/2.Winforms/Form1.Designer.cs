namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateForm = new System.Windows.Forms.Button();
            this.TextCol = new System.Windows.Forms.TextBox();
            this.TextRow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CreateForm
            // 
            this.CreateForm.Location = new System.Drawing.Point(267, 198);
            this.CreateForm.Name = "CreateForm";
            this.CreateForm.Size = new System.Drawing.Size(119, 30);
            this.CreateForm.TabIndex = 0;
            this.CreateForm.Text = "Tạo Form";
            this.CreateForm.UseVisualStyleBackColor = true;
            this.CreateForm.Click += new System.EventHandler(this.CreateForm_Click);
            // 
            // TextCol
            // 
            this.TextCol.Location = new System.Drawing.Point(135, 95);
            this.TextCol.Name = "TextCol";
            this.TextCol.Size = new System.Drawing.Size(100, 26);
            this.TextCol.TabIndex = 1;
            this.TextCol.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // TextRow
            // 
            this.TextRow.Location = new System.Drawing.Point(448, 95);
            this.TextRow.Name = "TextRow";
            this.TextRow.Size = new System.Drawing.Size(100, 26);
            this.TextRow.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Số cột: ";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Số dòng: ";
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(640, 319);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextRow);
            this.Controls.Add(this.TextCol);
            this.Controls.Add(this.CreateForm);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateForm;
        private System.Windows.Forms.TextBox TextCol;
        private System.Windows.Forms.TextBox TextRow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

