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
   public partial class Login : Form
   {
      public static class LoginInfo 
      {
         public static string userLogged;

      }
      public Login()
      {
         InitializeComponent();
      }

      private void Login_Load(object sender, EventArgs e)
      {

      }

      private void loginBtn_Click(object sender, EventArgs e)
      {
         string  connStr = ConfigurationManager.ConnectionStrings["MyConn1"].ToString();
         SqlConnection conn = new SqlConnection(connStr);

         using (SqlConnection con = conn)
         {
            try
            {
               con.Open();
               using (SqlCommand command = new SqlCommand("SELECT * FROM [testCoord].[dbo].[transaction];", con))
               using (SqlDataReader reader = command.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     MessageBox.Show(reader[0].ToString() + " , " + reader[1].ToString() + " , " + reader[2].ToString());

                  }
               }

            }
            catch
            {
               MessageBox.Show("Error en la conexión a la Base de datos");
            }
         }
         
      }
   }
}
