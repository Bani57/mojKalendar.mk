using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
namespace mojKalendarfinal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "SELECT Password FROM Users WHERE UserName=@UserName";
            com.Parameters.AddWithValue("@UserName", txtUsername.Text);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    CryptographyReference.Cryptography cryptography = new CryptographyReference.Cryptography();
                    string password = cryptography.Decrypt(reader[0].ToString());
                    if (txtPassword.Text.Equals(password))
                        FormsAuthentication.RedirectFromLoginPage(txtUsername.Text,true);
                    else
                        lblError.Text = "Wrong username or password";
                }
                else
                    lblError.Text = "Wrong username or password";
                
            }
            catch(SqlException er)
            {
                lblError.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }
    }
}