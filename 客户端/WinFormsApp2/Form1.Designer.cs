namespace WinFormsApp2
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
            this.sendtb = new System.Windows.Forms.RichTextBox();
            this.msgtb = new System.Windows.Forms.RichTextBox();
            this.sends = new System.Windows.Forms.Button();
            this.clo = new System.Windows.Forms.Button();
            this.cne = new System.Windows.Forms.Button();
            this.porttb = new System.Windows.Forms.TextBox();
            this.iptb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendtb
            // 
            this.sendtb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendtb.Location = new System.Drawing.Point(287, 259);
            this.sendtb.Name = "sendtb";
            this.sendtb.Size = new System.Drawing.Size(472, 155);
            this.sendtb.TabIndex = 17;
            this.sendtb.Text = "";
            this.sendtb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendtb_KeyDown);
            // 
            // msgtb
            // 
            this.msgtb.Location = new System.Drawing.Point(287, 37);
            this.msgtb.Name = "msgtb";
            this.msgtb.Size = new System.Drawing.Size(472, 206);
            this.msgtb.TabIndex = 16;
            this.msgtb.Text = "";
            // 
            // sends
            // 
            this.sends.Location = new System.Drawing.Point(91, 320);
            this.sends.Name = "sends";
            this.sends.Size = new System.Drawing.Size(75, 33);
            this.sends.TabIndex = 15;
            this.sends.Text = "发送";
            this.sends.UseVisualStyleBackColor = true;
            this.sends.Click += new System.EventHandler(this.sends_Click);
            // 
            // clo
            // 
            this.clo.Location = new System.Drawing.Point(154, 156);
            this.clo.Name = "clo";
            this.clo.Size = new System.Drawing.Size(75, 33);
            this.clo.TabIndex = 14;
            this.clo.Text = "关闭";
            this.clo.UseVisualStyleBackColor = true;
            this.clo.Click += new System.EventHandler(this.clo_Click);
            // 
            // cne
            // 
            this.cne.Location = new System.Drawing.Point(41, 156);
            this.cne.Name = "cne";
            this.cne.Size = new System.Drawing.Size(75, 33);
            this.cne.TabIndex = 13;
            this.cne.Text = "连接";
            this.cne.UseVisualStyleBackColor = true;
            this.cne.Click += new System.EventHandler(this.cne_Click);
            // 
            // porttb
            // 
            this.porttb.Location = new System.Drawing.Point(91, 86);
            this.porttb.Name = "porttb";
            this.porttb.Size = new System.Drawing.Size(138, 23);
            this.porttb.TabIndex = 12;
            this.porttb.Text = "6666";
            // 
            // iptb
            // 
            this.iptb.Location = new System.Drawing.Point(91, 37);
            this.iptb.Name = "iptb";
            this.iptb.Size = new System.Drawing.Size(138, 23);
            this.iptb.TabIndex = 11;
            this.iptb.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "IP";
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
            this.Controls.Add(this.porttb);
            this.Controls.Add(this.iptb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "客户端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox sendtb;
        private RichTextBox msgtb;
        private Button sends;
        private Button clo;
        private Button cne;
        private TextBox porttb;
        private TextBox iptb;
        private Label label2;
        private Label label1;
    }
}