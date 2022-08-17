using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace POS
{
    public partial class About : Page
    {
        DataBase db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();
            if (!IsPostBack)
            {
                halls.DataSource = db.fetchHall();
                halls.DataTextField = "Name";
                halls.DataValueField = "ID";
                halls.DataBind();

                halls.Items.Insert(0, new ListItem("<-- select -->","-1"));

            }

        }

        protected void halls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(halls.SelectedValue == "-1")
            {
                selectedHall.ImageUrl = @"";
                return;
            }

            try
            {
                selectedHall.ImageUrl = @"Image\" + halls.SelectedItem.Text + ".jpg";
            }
            catch (Exception)
            {
                Show("No Digram for this iamge");
                return;
            }

        }


        protected void add_Click(object sender, EventArgs e)
        {
            string h = halls.SelectedValue,
                n= name.Text, d = date.SelectedDate.ToString("MM/dd/yyyy"), t =time.Text, r=duration.Text ;

            try
            {
                DataTable tmp = db.fetchEvent(h);
                foreach(DataRow row in tmp.Rows)
                {


                    if (row["Date"].ToString() == d && TimeSpan.Parse(row["END"].ToString()) >= TimeSpan.Parse(t))
                    {
                        throw new Exception();
                    }
                    
                }
                db.addEvent(h, n, d, t, r);
                Show($"You reserved {h} for {n} ON {d} at {t} for {r} hours");


                name.Text = "";
                time.Text = "";
                duration.Text = "";
                date.SelectedDates.Clear();
            }
            catch (Exception)
            {
                Show("Conseder changing the Hall or the Time as It is not free in that day");
            }
            

        }

        public void Show(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }

    }
}