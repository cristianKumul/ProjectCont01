namespace WindowsFormsApplication3
{
   partial class ConfDataBase
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
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.serverNametxt = new System.Windows.Forms.TextBox();
         this.dataBaseNametxt = new System.Windows.Forms.TextBox();
         this.btnCambiar = new System.Windows.Forms.Button();
         this.label3 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.userConnection = new System.Windows.Forms.TextBox();
         this.passwdConnection = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
         this.label1.Location = new System.Drawing.Point(62, 38);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(106, 20);
         this.label1.TabIndex = 0;
         this.label1.Text = "SERVIDOR :";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
         this.label2.Location = new System.Drawing.Point(12, 85);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(156, 20);
         this.label2.TabIndex = 1;
         this.label2.Text = "BASE DE DATOS :";
         this.label2.Click += new System.EventHandler(this.label2_Click);
         // 
         // serverNametxt
         // 
         this.serverNametxt.Location = new System.Drawing.Point(167, 37);
         this.serverNametxt.Name = "serverNametxt";
         this.serverNametxt.Size = new System.Drawing.Size(203, 20);
         this.serverNametxt.TabIndex = 2;
         // 
         // dataBaseNametxt
         // 
         this.dataBaseNametxt.Location = new System.Drawing.Point(167, 85);
         this.dataBaseNametxt.Name = "dataBaseNametxt";
         this.dataBaseNametxt.Size = new System.Drawing.Size(203, 20);
         this.dataBaseNametxt.TabIndex = 3;
         // 
         // btnCambiar
         // 
         this.btnCambiar.Location = new System.Drawing.Point(295, 206);
         this.btnCambiar.Name = "btnCambiar";
         this.btnCambiar.Size = new System.Drawing.Size(75, 23);
         this.btnCambiar.TabIndex = 4;
         this.btnCambiar.Text = "Cambiar";
         this.btnCambiar.UseVisualStyleBackColor = true;
         this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
         this.label3.Location = new System.Drawing.Point(86, 126);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(82, 20);
         this.label3.TabIndex = 5;
         this.label3.Text = "Usuario  :";
         this.label3.Click += new System.EventHandler(this.label3_Click);
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
         this.label4.Location = new System.Drawing.Point(70, 163);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(98, 20);
         this.label4.TabIndex = 6;
         this.label4.Text = "Password  :";
         // 
         // userConnection
         // 
         this.userConnection.Location = new System.Drawing.Point(167, 128);
         this.userConnection.Name = "userConnection";
         this.userConnection.Size = new System.Drawing.Size(203, 20);
         this.userConnection.TabIndex = 7;
         // 
         // passwdConnection
         // 
         this.passwdConnection.Location = new System.Drawing.Point(167, 163);
         this.passwdConnection.Name = "passwdConnection";
         this.passwdConnection.PasswordChar = '*';
         this.passwdConnection.Size = new System.Drawing.Size(203, 20);
         this.passwdConnection.TabIndex = 8;
         // 
         // ConfDataBase
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(410, 241);
         this.Controls.Add(this.passwdConnection);
         this.Controls.Add(this.userConnection);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.btnCambiar);
         this.Controls.Add(this.dataBaseNametxt);
         this.Controls.Add(this.serverNametxt);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Name = "ConfDataBase";
         this.Text = "ConfDataBase";
         this.Load += new System.EventHandler(this.ConfDataBase_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox serverNametxt;
      private System.Windows.Forms.TextBox dataBaseNametxt;
      private System.Windows.Forms.Button btnCambiar;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox userConnection;
      private System.Windows.Forms.TextBox passwdConnection;
   }
}