namespace ProiectBD_InchiriereApartamente
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnAutentificare = new System.Windows.Forms.Button();
            this.tbParola = new System.Windows.Forms.TextBox();
            this.rtbUsername = new System.Windows.Forms.RichTextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblParola = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iesireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schimbaUsernameSauParolaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajutorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAutentificare
            // 
            this.btnAutentificare.BackgroundImage = global::ProiectBD_InchiriereApartamente.Properties.Resources.GenericButton;
            this.btnAutentificare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutentificare.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutentificare.ForeColor = System.Drawing.Color.Black;
            this.btnAutentificare.Location = new System.Drawing.Point(406, 330);
            this.btnAutentificare.Margin = new System.Windows.Forms.Padding(4);
            this.btnAutentificare.Name = "btnAutentificare";
            this.btnAutentificare.Size = new System.Drawing.Size(133, 30);
            this.btnAutentificare.TabIndex = 3;
            this.btnAutentificare.Text = "Autentificare";
            this.btnAutentificare.UseVisualStyleBackColor = true;
            this.btnAutentificare.Click += new System.EventHandler(this.btnAutentificare_Click);
            // 
            // tbParola
            // 
            this.tbParola.BackColor = System.Drawing.Color.Goldenrod;
            this.tbParola.Location = new System.Drawing.Point(392, 253);
            this.tbParola.Name = "tbParola";
            this.tbParola.PasswordChar = '*';
            this.tbParola.Size = new System.Drawing.Size(150, 26);
            this.tbParola.TabIndex = 2;
            // 
            // rtbUsername
            // 
            this.rtbUsername.BackColor = System.Drawing.Color.Goldenrod;
            this.rtbUsername.Location = new System.Drawing.Point(392, 147);
            this.rtbUsername.Name = "rtbUsername";
            this.rtbUsername.Size = new System.Drawing.Size(150, 27);
            this.rtbUsername.TabIndex = 1;
            this.rtbUsername.Text = "";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Goldenrod;
            this.lblUsername.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(424, 98);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(85, 23);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // lblParola
            // 
            this.lblParola.AutoSize = true;
            this.lblParola.BackColor = System.Drawing.Color.Goldenrod;
            this.lblParola.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParola.Location = new System.Drawing.Point(443, 200);
            this.lblParola.Name = "lblParola";
            this.lblParola.Size = new System.Drawing.Size(57, 23);
            this.lblParola.TabIndex = 4;
            this.lblParola.Text = "Parola";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuStrip1.BackgroundImage = global::ProiectBD_InchiriereApartamente.Properties.Resources.GenericButton;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iesireToolStripMenuItem,
            this.schimbaUsernameSauParolaToolStripMenuItem,
            this.ajutorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(901, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // iesireToolStripMenuItem
            // 
            this.iesireToolStripMenuItem.Name = "iesireToolStripMenuItem";
            this.iesireToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.iesireToolStripMenuItem.Text = "Iesire";
            this.iesireToolStripMenuItem.Click += new System.EventHandler(this.iesireToolStripMenuItem_Click);
            // 
            // schimbaUsernameSauParolaToolStripMenuItem
            // 
            this.schimbaUsernameSauParolaToolStripMenuItem.Name = "schimbaUsernameSauParolaToolStripMenuItem";
            this.schimbaUsernameSauParolaToolStripMenuItem.Size = new System.Drawing.Size(190, 20);
            this.schimbaUsernameSauParolaToolStripMenuItem.Text = "Schimba username sau parola";
            this.schimbaUsernameSauParolaToolStripMenuItem.Click += new System.EventHandler(this.schimbaUsernameSauParolaToolStripMenuItem_Click);
            // 
            // ajutorToolStripMenuItem
            // 
            this.ajutorToolStripMenuItem.Name = "ajutorToolStripMenuItem";
            this.ajutorToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.ajutorToolStripMenuItem.Text = "Ajutor";
            this.ajutorToolStripMenuItem.Click += new System.EventHandler(this.ajutorToolStripMenuItem_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(901, 373);
            this.Controls.Add(this.lblParola);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.rtbUsername);
            this.Controls.Add(this.tbParola);
            this.Controls.Add(this.btnAutentificare);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.Text = "Fereastra Logare";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAutentificare;
        private System.Windows.Forms.TextBox tbParola;
        private System.Windows.Forms.RichTextBox rtbUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblParola;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iesireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schimbaUsernameSauParolaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajutorToolStripMenuItem;
    }
}

