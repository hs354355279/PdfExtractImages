namespace PdfExtractImages
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            label2 = new Label();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            listBox1 = new ListBox();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(584, 12);
            button1.Name = "button1";
            button1.Size = new Size(92, 49);
            button1.TabIndex = 0;
            button1.Text = "页面转图片";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 15);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 2;
            label2.Text = "请选择文件";
            // 
            // button2
            // 
            button2.Location = new Point(14, 12);
            button2.Name = "button2";
            button2.Size = new Size(98, 23);
            button2.TabIndex = 3;
            button2.Text = "选择pdf文件";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(14, 41);
            button3.Name = "button3";
            button3.Size = new Size(98, 23);
            button3.TabIndex = 4;
            button3.Text = "选择保存位置";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 44);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 5;
            label1.Text = "请选择位置";
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(14, 70);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(774, 361);
            listBox1.TabIndex = 6;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Location = new Point(696, 12);
            button4.Name = "button4";
            button4.Size = new Size(92, 49);
            button4.TabIndex = 7;
            button4.Text = "提取嵌入图片";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "PDF提取图片";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label2;
        private Button button2;
        private Button button3;
        private Label label1;
        private ListBox listBox1;
        private Button button4;
    }
}
