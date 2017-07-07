using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace mojKalendarfinal
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void insertUser()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Users (UserName,Password,FirstName,LastName,Email,Address) VALUES(@UserName,@Password,@FirstName, @LastName,@Email, @Address)";
            com.Parameters.AddWithValue("@UserName", txtUsername.Text);
            CryptographyReference.Cryptography cryptography = new CryptographyReference.Cryptography();
            com.Parameters.AddWithValue("@Password", cryptography.Encrypt(txtPassword.Text));
            com.Parameters.AddWithValue("@FirstName", txtFName.Text);
            com.Parameters.AddWithValue("@LastName", txtLName.Text);
            com.Parameters.AddWithValue("@Email", txtEmail.Text);
            com.Parameters.AddWithValue("@Address", txtAddress.Text);
            SqlCommand com2 = new SqlCommand();
            com2.Connection = con;
            com2.CommandText = "INSERT INTO Preferences (UserName,CalendarSize,BackgroundColor,HeaderColor,CalendarTextSize,DayBorderStyle,WeekdayNameStyle,WeekdayNameSize,TodayColor,SelectedDayColor,ShowPreviousNextMonths) VALUES (@UserName,'50','White','Gray','Large','NotSet','Short','Large','White','LightGray',0)";
            com2.Parameters.AddWithValue("@UserName", txtUsername.Text);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
                com2.ExecuteNonQuery();
                Response.Redirect("~/Kalendar.aspx");
            }
            catch (SqlException er)
            {
                if (er.Message.StartsWith("Cannot insert duplicate key"))
                    StatusMessage.Text = "That username is already taken, try another one.";
                else
                    StatusMessage.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
            }
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            insertUser();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}