using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;


namespace WindowsFormsApplication3
{
   public partial class ConfDataBase : Form
   {
     

      public ConfDataBase()
      {
         InitializeComponent();
         var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         var connStringSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
         string connectionStringVal = connStringSection.ConnectionStrings["MyConn1"].ConnectionString.ToString();
         string[] split = connectionStringVal.Split(new Char[] { '=', ';', '=', ';', '=', ';' });
         serverNametxt.Text = split[1];
         dataBaseNametxt.Text = split[3];  
         


         
      }

      private void label2_Click(object sender, EventArgs e)
      {

      }

      private void ConfDataBase_Load(object sender, EventArgs e)
      {

      }

      private void btnCambiar_Click(object sender, EventArgs e)
      {
         
         var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
         var connStringSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
         connStringSection.ConnectionStrings["MyConn1"].ConnectionString = "Server=" + serverNametxt.Text + ";Database=" + dataBaseNametxt.Text + ";Trusted_Connection=Yes;";
         config.Save();

         ConfigurationManager.RefreshSection("connectionStrings");
         

         if (serverNametxt.Text != "" && dataBaseNametxt.Text != "")
         {
            MessageBox.Show("Datos Cambiados");
            this.Close();
         }
         else
         {
            MessageBox.Show("Ingrese el servidor y la Base de datos");
         }
         
      }
   }
}
