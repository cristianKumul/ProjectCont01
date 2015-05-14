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
      
      public string userLogged {get;set;}
      public bool LoginStatus { get; set; }


      
      public Login()
      {
         InitializeComponent();
      }

      private void Login_Load(object sender, EventArgs e)
      {
         this.LoginStatus = false;
         this.userLogged = "NONE";


      }

      private void loginBtn_Click(object sender, EventArgs e)
      {
         string  connStr = ConfigurationManager.ConnectionStrings["MyConn1"].ToString();
         SqlConnection conn = new SqlConnection(connStr);
         Cryptografia cryp = new Cryptografia();
         
         string bd = conn.Database;
         string test = "SELECT [IdUsuario],[Password] FROM [" + bd + "].[dbo].[Usuarios]  [IdUsuario]='" + userLogin.Text + "' AND [Password]='" + cryp.Encriptar("malm82", passLogin.Text, 0) + "';";
         //MessageBox.Show(passLogin.Text);
         //MessageBox.Show(test);   

         using (SqlConnection con = conn)
         {
            try
            {
               con.Open();
               using (SqlCommand command = new SqlCommand("SELECT [IdUsuario],[Password] FROM [" + bd + "].[dbo].[Usuarios]  WHERE   [IdUsuario]='" + userLogin.Text + "' AND [Password]='" + cryp.Encriptar("malm82", passLogin.Text, 0) + "';", con))

                  try
                  {
                     using (SqlDataReader reader = command.ExecuteReader())
                     {

                        while(reader.Read())
                        {
                           //MessageBox.Show(reader[0].ToString() + " , " + reader[1].ToString());
                           this.userLogged = reader[0].ToString();
                           if (this.userLogged == userLogin.Text)
                           {
                              this.LoginStatus = true;
                           }
                           break;
                        }

                     }
                  }
                  catch
                  {
                     MessageBox.Show("Error en autenticación");
                  }

            }
            catch
            {
               MessageBox.Show("Error en la conexión a la Base de datos");
            }
         }

         this.Close();
      }
   }
}
