namespace WindowsFormsApplication3
{
   partial class Login
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
         this.loginBtn = new System.Windows.Forms.Button();
         this.userLbl = new System.Windows.Forms.Label();
         this.passwordLbl = new System.Windows.Forms.Label();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // loginBtn
         // 
         this.loginBtn.Location = new System.Drawing.Point(171, 130);
         this.loginBtn.Name = "loginBtn";
         this.loginBtn.Size = new System.Drawing.Size(75, 23);
         this.loginBtn.TabIndex = 0;
         this.loginBtn.Text = "Entrar";
         this.loginBtn.UseVisualStyleBackColor = true;
         this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
         // 
         // userLbl
         // 
         this.userLbl.AutoSize = true;
         this.userLbl.Location = new System.Drawing.Point(33, 45);
         this.userLbl.Name = "userLbl";
         this.userLbl.Size = new System.Drawing.Size(62, 13);
         this.userLbl.TabIndex = 1;
         this.userLbl.Text = "USUARIO :";
         this.userLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         // 
         // passwordLbl
         // 
         this.passwordLbl.AutoSize = true;
         this.passwordLbl.Location = new System.Drawing.Point(19, 93);
         this.passwordLbl.Name = "passwordLbl";
         this.passwordLbl.Size = new System.Drawing.Size(76, 13);
         this.passwordLbl.TabIndex = 2;
         this.passwordLbl.Text = "PASSWORD :";
         this.passwordLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(110, 42);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(136, 20);
         this.textBox1.TabIndex = 3;
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(110, 90);
         this.textBox2.Name = "textBox2";
         this.textBox2.PasswordChar = '*';
         this.textBox2.Size = new System.Drawing.Size(136, 20);
         this.textBox2.TabIndex = 4;
         // 
         // Login
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(276, 171);
         this.Controls.Add(this.textBox2);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.passwordLbl);
         this.Controls.Add(this.userLbl);
         this.Controls.Add(this.loginBtn);
         this.Name = "Login";
         this.Text = "Login";
         this.Load += new System.EventHandler(this.Login_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button loginBtn;
      private System.Windows.Forms.Label userLbl;
      private System.Windows.Forms.Label passwordLbl;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.TextBox textBox2;
   }
}