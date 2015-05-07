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
         MessageBox.Show("Datos Cambiados");
      }
   }
}
