using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace mojKalendarfinal
{
    public partial class Kalendar1 : System.Web.UI.Page
    {
        public void customizeCalendar()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Preferences WHERE UserName=@UserName";
            com.Parameters.AddWithValue("@UserName", User.Identity.Name);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                calEvents.Width = Unit.Percentage(Convert.ToDouble(reader[1].ToString()));
                calEvents.Height = Unit.Pixel(Convert.ToInt32(reader[1].ToString()) * 10);
                gvEvents.Width = calEvents.Width;

                calEvents.BackColor = System.Drawing.Color.FromName(reader[2].ToString());
                gvEvents.AlternatingRowStyle.BackColor = calEvents.BackColor;

                calEvents.TitleStyle.BackColor = System.Drawing.Color.FromName(reader[3].ToString());
                gvEvents.HeaderStyle.BackColor = calEvents.TitleStyle.BackColor;

                calEvents.DayStyle.Font.Size = FontUnit.Parse(reader[4].ToString());
                calEvents.TitleStyle.Font.Size = FontUnit.Parse(reader[4].ToString());
                gvEvents.Font.Size = calEvents.DayStyle.Font.Size;

                calEvents.DayStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), reader[5].ToString());
                calEvents.DayHeaderStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), reader[5].ToString());
                gvEvents.RowStyle.BorderStyle = calEvents.DayStyle.BorderStyle;

                calEvents.DayNameFormat = (DayNameFormat)Enum.Parse(typeof(DayNameFormat), reader[6].ToString());

                calEvents.DayHeaderStyle.Font.Size = FontUnit.Parse(reader[7].ToString());

                calEvents.TodayDayStyle.BackColor = System.Drawing.Color.FromName(reader[8].ToString());
                gvEvents.PagerStyle.BackColor = calEvents.TodayDayStyle.BackColor;

                calEvents.SelectedDayStyle.BackColor = System.Drawing.Color.FromName(reader[9].ToString());
                gvEvents.SelectedRowStyle.BackColor = calEvents.SelectedDayStyle.BackColor;

                if (Convert.ToBoolean(reader[10]))
                    calEvents.NextPrevFormat = NextPrevFormat.FullMonth;
                else calEvents.NextPrevFormat = NextPrevFormat.CustomText;

                rblSize.SelectedIndex = rblSize.Items.IndexOf(rblSize.Items.FindByValue(calEvents.Width.Value.ToString()));
                ddlBackground.SelectedIndex = ddlBackground.Items.IndexOf(new ListItem(calEvents.BackColor.Name.ToString()));
                ddlHeader.SelectedIndex = ddlHeader.Items.IndexOf(new ListItem(calEvents.TitleStyle.BackColor.Name.ToString()));
                rblCalendarTextSize.SelectedIndex = rblCalendarTextSize.Items.IndexOf(rblCalendarTextSize.Items.FindByValue(calEvents.DayStyle.Font.Size.Type.ToString()));
                ddlDayBorderStyle.SelectedIndex = ddlDayBorderStyle.Items.IndexOf(new ListItem(calEvents.DayStyle.BorderStyle.ToString()));
                rblWeekdayName.SelectedIndex = rblWeekdayName.Items.IndexOf(rblWeekdayName.Items.FindByValue(calEvents.DayNameFormat.ToString()));
                rblWeekdayTextSize.SelectedIndex = rblWeekdayTextSize.Items.IndexOf(rblWeekdayTextSize.Items.FindByValue(calEvents.DayHeaderStyle.Font.Size.Type.ToString()));
                ddlTodayColor.SelectedIndex = ddlTodayColor.Items.IndexOf(new ListItem(calEvents.TodayDayStyle.BackColor.Name.ToString()));
                ddlSelectedDayColor.SelectedIndex = ddlSelectedDayColor.Items.IndexOf(new ListItem(calEvents.SelectedDayStyle.BackColor.Name.ToString()));
                if (calEvents.NextPrevFormat.Equals(NextPrevFormat.FullMonth))
                    chbShowMonths.Checked = true;
                else
                    chbShowMonths.Checked = false;
            }
            catch(SqlException er)
            {
                lblWelcome.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();
            else
            {
                lblWelcome.Text = "Welcome to your calendar<br/>" + User.Identity.Name;
                fillGridView(rblViewEvents.SelectedIndex);
                if (!Page.IsPostBack)
                {
                    customizeCalendar();
                    string[] listaNaBoi = Enum.GetNames(typeof(System.Drawing.KnownColor));
                    List<string> korisniBoi = new List<string>();
                    bool add = false;
                    foreach (string boja in listaNaBoi)
                    {
                        if (boja.Equals("AliceBlue"))
                            add = true;
                        if (boja.Equals("ButtonFace"))
                            break;
                        if (add)
                            korisniBoi.Add(boja);
                    }
                    ddlBackground.DataSource = korisniBoi;
                    ddlBackground.DataBind();
                    ddlHeader.DataSource = korisniBoi;
                    ddlHeader.DataBind();
                    ddlTodayColor.DataSource = korisniBoi;
                    ddlTodayColor.DataBind();
                    ddlSelectedDayColor.DataSource = korisniBoi;
                    ddlSelectedDayColor.DataBind();
                    ListItemCollection styles = new ListItemCollection();
                    foreach (string s in Enum.GetNames(typeof(BorderStyle)))
                        styles.Add(s);
                    ddlDayBorderStyle.DataSource = styles;
                    ddlDayBorderStyle.DataBind();
                    string month = DateTime.Now.Month.ToString();
                    imgSide.ImageUrl = "~/Images/" + month + ".jpg";

                    for (int i = 0; i < 24; i++)
                        ddlStartHours.Items.Add(new ListItem(i.ToString("D2")));
                    for (int i = 0; i < 60; i++)
                        ddlStartMinutes.Items.Add(new ListItem(i.ToString("D2")));
                }
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnCustomize_Click(object sender, EventArgs e)
        {
            pnlEvent.Visible = false;
            rblViewEvents.Visible = false;
            gvEvents.Visible = false;
            pnlNoEvents.Visible = false;
            btnAddEvent.Text = "Add event";
            btnViewEvents.Text = "View events";
            if (pnlCustomize.Visible)
            {
                pnlCustomize.Visible = false;
                btnCustomize.Text = "Customize calendar";
            }
            else
            {
                pnlCustomize.Visible = true;
                btnCustomize.Text = "Stop customizing";
            }
            customizeCalendar();
        }

        protected void rblSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.Width = Unit.Percentage(Convert.ToDouble(rblSize.SelectedValue));
            calEvents.Height = Unit.Pixel(Convert.ToInt32(rblSize.SelectedValue) * 10);
        }

        protected void ddlBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.BackColor = System.Drawing.Color.FromName(ddlBackground.SelectedItem.Text);
        }

        protected void ddlHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.TitleStyle.BackColor = System.Drawing.Color.FromName(ddlHeader.SelectedItem.Text);
        }

        protected void rblCalendarTextSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.DayStyle.Font.Size = FontUnit.Parse(rblCalendarTextSize.Text);
            calEvents.TitleStyle.Font.Size = FontUnit.Parse(rblCalendarTextSize.Text);
        }

        protected void ddlDayBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.DayStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), ddlDayBorderStyle.SelectedItem.Text);
            calEvents.DayHeaderStyle.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), ddlDayBorderStyle.SelectedItem.Text);
        }

        protected void rblWeekdayName_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.DayNameFormat = (DayNameFormat)Enum.Parse(typeof(DayNameFormat), rblWeekdayName.SelectedValue);
        }

        protected void rblWeekdayTextSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.DayHeaderStyle.Font.Size = FontUnit.Parse(rblWeekdayTextSize.SelectedValue);
        }

        protected void ddlTodayColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.TodayDayStyle.BackColor = System.Drawing.Color.FromName(ddlTodayColor.SelectedItem.Text);
        }

        protected void ddlSelectedDayColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            calEvents.SelectedDayStyle.BackColor = System.Drawing.Color.FromName(ddlSelectedDayColor.SelectedItem.Text);
        }

        protected void chbShowMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (chbShowMonths.Checked)
                calEvents.NextPrevFormat = NextPrevFormat.FullMonth;
            else calEvents.NextPrevFormat = NextPrevFormat.CustomText;
        }

        protected void calEvents_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            string month = e.NewDate.Month.ToString();
            imgSide.ImageUrl = "~/Images/" + month + ".jpg";
        }

        protected void btnPreferences_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "UPDATE Preferences SET CalendarSize=@CalendarSize,BackgroundColor=@BackgroundColor,HeaderColor=@HeaderColor,CalendarTextSize=@CalendarTextSize,DayBorderStyle=@DayBorderStyle,WeekdayNameStyle=@WeekdayNameStyle,WeekdayNameSize=@WeekdayNameSize,TodayColor=@TodayColor,SelectedDayColor=@SelectedDayColor,ShowPreviousNextMonths=@ShowPreviousNextMonths WHERE UserName=@UserName";
            com.Parameters.AddWithValue("@UserName", User.Identity.Name);
            com.Parameters.AddWithValue("@CalendarSize", rblSize.SelectedValue);
            com.Parameters.AddWithValue("@BackgroundColor", ddlBackground.SelectedItem.Text);
            com.Parameters.AddWithValue("@HeaderColor", ddlHeader.SelectedItem.Text);
            com.Parameters.AddWithValue("@CalendarTextSize", rblCalendarTextSize.SelectedValue);
            com.Parameters.AddWithValue("@DayBorderStyle", ddlDayBorderStyle.SelectedItem.Text);
            com.Parameters.AddWithValue("@WeekdayNameStyle", rblWeekdayName.SelectedValue);
            com.Parameters.AddWithValue("@WeekdayNameSize", rblWeekdayTextSize.SelectedValue);
            com.Parameters.AddWithValue("@TodayColor", ddlTodayColor.SelectedItem.Text);
            com.Parameters.AddWithValue("@SelectedDayColor", ddlSelectedDayColor.SelectedItem.Text);
            com.Parameters.AddWithValue("@ShowPreviousNextMonths", chbShowMonths.Checked);
            try
            {
                con.Open();
                com.ExecuteNonQuery();
            }
            catch(SqlException er)
            {
                lblWelcome.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnSetDefaults_Click(object sender, EventArgs e)
        {
            rblSize.SelectedIndex = 1;
            ddlBackground.SelectedIndex = ddlBackground.Items.IndexOf(new ListItem("White"));
            ddlHeader.SelectedIndex = ddlHeader.Items.IndexOf(new ListItem("Gray"));
            rblCalendarTextSize.SelectedIndex = 0;
            ddlDayBorderStyle.SelectedIndex = 0;
            rblWeekdayName.SelectedIndex = 0;
            rblWeekdayTextSize.SelectedIndex = 0;
            ddlTodayColor.SelectedIndex = ddlTodayColor.Items.IndexOf(new ListItem("White"));
            ddlSelectedDayColor.SelectedIndex = ddlSelectedDayColor.Items.IndexOf(new ListItem("LightGray"));
            chbShowMonths.Checked = false;

            rblSize_SelectedIndexChanged(sender, e);
            ddlBackground_SelectedIndexChanged(sender, e);
            ddlHeader_SelectedIndexChanged(sender, e);
            rblCalendarTextSize_SelectedIndexChanged(sender, e);
            ddlDayBorderStyle_SelectedIndexChanged(sender, e);
            rblWeekdayName_SelectedIndexChanged(sender, e);
            rblWeekdayTextSize_SelectedIndexChanged(sender, e);
            ddlTodayColor_SelectedIndexChanged(sender, e);
            ddlSelectedDayColor_SelectedIndexChanged(sender, e);
            chbShowMonths_CheckedChanged(sender, e);
        }

        protected void calEvents_SelectionChanged(object sender, EventArgs e)
        {
            btnAddEvent.Enabled = true;
            lblDate.Text = calEvents.SelectedDate.ToShortDateString();
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            pnlCustomize.Visible = false;
            rblViewEvents.Visible = false;
            gvEvents.Visible = false;
            pnlNoEvents.Visible = false;
            btnCustomize.Text = "Customize calendar";
            btnViewEvents.Text = "View events";
            if (pnlEvent.Visible)
            {
                pnlEvent.Visible = false;
                btnAddEvent.Text = "Add event";
            }
            else
            {
                pnlEvent.Visible = true;
                lblDate.Text = calEvents.SelectedDate.ToShortDateString();
                btnAddEvent.Text = "Cancel event";
            }
        }

        protected void btnSaveEvent_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Events (UserName,EventName,Date,Description,Time) VALUES (@UserName,@EventName,@Date,@Description,@Time)";
            com.Parameters.AddWithValue("@UserName",User.Identity.Name);
            com.Parameters.AddWithValue("@EventName", txtEventName.Text);
            com.Parameters.AddWithValue("@Date", calEvents.SelectedDate);
            com.Parameters.AddWithValue("@Description", txtDescription.Text.Replace("\n", "<br/>"));
            com.Parameters.AddWithValue("@Time", ddlStartHours.SelectedItem.Text+":"+ddlStartMinutes.SelectedItem.Text);
            try
            {
                con.Open();
                com.ExecuteNonQuery();

            }
            catch(SqlException er)
            {
                lblWelcome.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
                txtEventName.Text = "";
                txtDescription.Text = "";
                ddlStartHours.SelectedIndex = 0;
                ddlStartMinutes.SelectedIndex = 0;
            }
        }

        protected void btnClearEvent_Click(object sender, EventArgs e)
        {
            txtEventName.Text = "";
            txtDescription.Text = "";
            ddlStartHours.SelectedIndex = 0;
            ddlStartMinutes.SelectedIndex = 0;
        }

        protected void btnViewEvents_Click(object sender, EventArgs e)
        {
            pnlCustomize.Visible = false;
            pnlEvent.Visible = false;
            btnAddEvent.Text = "Add event";
            btnCustomize.Text = "Customize calendar";
            if (rblViewEvents.Visible)
            {
                rblViewEvents.Visible = false;
                gvEvents.Visible = false;
                pnlNoEvents.Visible = false;
                btnViewEvents.Text = "View events";
            }
            else
            {
                rblViewEvents.Visible = true;
                gvEvents.Visible = true;
                if (gvEvents.Rows.Count == 0)
                    pnlNoEvents.Visible = true;
                else pnlNoEvents.Visible = false;
                btnViewEvents.Text = "Hide events";
            }
            fillGridView(rblViewEvents.SelectedIndex);
        }
        public void fillGridView(int option)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            if (option == 0)
            {
                com.CommandText = "SELECT * FROM Events WHERE UserName=@UserName AND Date=@Date ORDER BY Date,Time";
                com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                com.Parameters.AddWithValue("@Date", calEvents.TodaysDate);
                lblEmpty.Text = "You have no events for today.";
            }
            else if(option==1)
            {
                com.CommandText = "SELECT * FROM Events WHERE UserName=@UserName AND Date<=@DateNextWeek AND Date>=@Date ORDER BY Date,Time";
                DateTime nextweek = DateTime.Now.AddDays(7);
                com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                com.Parameters.AddWithValue("@Date", calEvents.TodaysDate);
                com.Parameters.AddWithValue("@DateNextWeek", nextweek);
                lblEmpty.Text = "You have no events for next week.";
            }
            else if(option==2)
            {
                com.CommandText = "SELECT * FROM (SELECT TOP 5 * FROM Events WHERE UserName=@UserName AND Date>=@Date ORDER BY Date ASC) AS Temp ORDER BY Date,Time";
                com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                com.Parameters.AddWithValue("@Date", calEvents.TodaysDate);
                lblEmpty.Text = "You have no upcoming events.";
            }
            else if(option == 3)
            {
                com.CommandText = "SELECT * FROM Events WHERE UserName=@UserName ORDER BY Date,Time";
                com.Parameters.AddWithValue("@UserName", User.Identity.Name);
                lblEmpty.Text = "You have no events.";
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = com;
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                adapter.Fill(ds, "Events");
                gvEvents.DataSource = ds.Tables["Events"];
                gvEvents.DataBind();
                ViewState["dataset"] = ds;
            }
            catch(SqlException er)
            {
                lblWelcome.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
            }
        }
        protected void rblViewEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGridView(rblViewEvents.SelectedIndex);
            gvEvents.Visible = true;
            if (gvEvents.Rows.Count == 0)
                pnlNoEvents.Visible = true;
            else pnlNoEvents.Visible = false;
        }

        protected void gvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KalendarDB"].ConnectionString;
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "DELETE FROM Events WHERE UserName=@UserName AND EventName=@EventName";
            com.Parameters.AddWithValue("@UserName", User.Identity.Name);
            com.Parameters.AddWithValue("@EventName", gvEvents.Rows[e.RowIndex].Cells[0].Text);
            try
            {
                con.Open();
                com.ExecuteNonQuery();

            }
            catch(SqlException er)
            {
                lblWelcome.Text = "Oops, something went wrong. If someone from FINKI comes show them this error number: " + er.Number;
            }
            finally
            {
                con.Close();
                fillGridView(rblViewEvents.SelectedIndex);
            }
        }

        protected void gvEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvents.PageIndex = e.NewPageIndex;
            gvEvents.SelectedIndex = -1;
            DataSet ds = (DataSet)ViewState["dataset"];
            gvEvents.DataSource = ds.Tables["Events"];
            gvEvents.DataBind();
        }

        protected void gvEvents_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataSet ds = (DataSet)ViewState["dataset"];
            DataView dv = ds.Tables["Events"].DefaultView;
            if (ViewState["nasoka"] == null)
                ViewState["nasoka"] = "ASC";
            if(ViewState["nasoka"].ToString().Equals("DESC"))
            {
                dv.Sort = e.SortExpression + " DESC";
                ViewState["nasoka"] = "ASC";
            }
            else
            {
                dv.Sort = e.SortExpression + " " + " ASC";
                ViewState["nasoka"] = "DESC";
            }
            gvEvents.DataSource = dv;
            gvEvents.DataBind();
        }
    }
}