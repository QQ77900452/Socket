namespace WinFormsApp1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.iptb = new System.Windows.Forms.TextBox();
            this.prottb = new System.Windows.Forms.TextBox();
            this.cne = new System.Windows.Forms.Button();
            this.clo = new System.Windows.Forms.Button();
            this.sends = new System.Windows.Forms.Button();
            this.msgtb = new System.Windows.Forms.RichTextBox();
            this.sendtb = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // iptb
            // 
            this.iptb.Location = new System.Drawing.Point(93, 32);
            this.iptb.Name = "iptb";
            this.iptb.Size = new System.Drawing.Size(138, 23);
            this.iptb.TabIndex = 2;
            this.iptb.Text = "127.0.0.1";
            // 
            // prottb
            // 
            this.prottb.Location = new System.Drawing.Point(93, 81);
            this.prottb.Name = "prottb";
            this.prottb.Size = new System.Drawing.Size(138, 23);
            this.prottb.TabIndex = 3;
            this.prottb.Text = "6666";
            // 
            // cne
            // 
            this.cne.Location = new System.Drawing.Point(43, 151);
            this.cne.Name = "cne";
            this.cne.Size = new System.Drawing.Size(75, 33);
            this.cne.TabIndex = 4;
            this.cne.Text = "连接";
            this.cne.UseVisualStyleBackColor = true;
            this.cne.Click += new System.EventHandler(this.cne_Click);
            // 
            // clo
            // 
            this.clo.Location = new System.Drawing.Point(156, 151);
            this.clo.Name = "clo";
            this.clo.Size = new System.Drawing.Size(75, 33);
            this.clo.TabIndex = 5;
            this.clo.Text = "关闭";
            this.clo.UseVisualStyleBackColor = true;
            this.clo.Click += new System.EventHandler(this.clo_Click);
            // 
            // sends
            // 
            this.sends.Location = new System.Drawing.Point(93, 315);
            this.sends.Name = "sends";
            this.sends.Size = new System.Drawing.Size(75, 33);
            this.sends.TabIndex = 6;
            this.sends.Text = "发送";
            this.sends.UseVisualStyleBackColor = true;
            this.sends.Click += new System.EventHandler(this.sends_Click);
            // 
            // msgtb
            // 
            this.msgtb.Location = new System.Drawing.Point(289, 32);
            this.msgtb.Name = "msgtb";
            this.msgtb.Size = new System.Drawing.Size(472, 206);
            this.msgtb.TabIndex = 7;
            this.msgtb.Text = "";
            // 
            // sendtb
            // 
            this.sendtb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendtb.Location = new System.Drawing.Point(289, 254);
            this.sendtb.Name = "sendtb";
            this.sendtb.Size = new System.Drawing.Size(472, 155);
            this.sendtb.TabIndex = 8;
            this.sendtb.Text = "";
            this.sendtb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendtb_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sendtb);
            this.Controls.Add(this.msgtb);
            this.Controls.Add(this.sends);
            this.Controls.Add(this.clo);
            this.Controls.Add(this.cne);
            this.Controls.Add(this.prottb);
            this.Controls.Add(this.iptb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "服务器端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox iptb;
        private TextBox prottb;
        private Button cne;
        private Button clo;
        private Button sends;
        private RichTextBox msgtb;
        private RichTextBox sendtb;
    }
}