using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS
{
    public partial class _Default : Page
    {
        DataBase db;
        DataTable hallTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (IsPostBack)
            {
                hallTable = (DataTable)Session["hallTable"];
            }
            else
            {
                Panel1.Visible = false;
                updateHalls();
            }
            

            
        }

        protected void Halls_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = Halls.SelectedValue;
            //Show(id);
            if(id != "-1")
            {
                Panel1.Visible = true;
                Panel1.Width = 300;
                Panel1.Height = 300;
                Panel1.BorderColor = Color.Red;
                
            }
            else
            {
                Panel1.Visible = false;
            }
            
        }



        private void updateHalls()
        {
            hallTable = db.fetchHall();
            Session["hallTable"] = hallTable;
            Halls.DataSource = hallTable;
            Halls.DataTextField = "Name";
            Halls.DataValueField = "ID";
            Halls.DataBind();
            Halls.Items.Insert(0, new ListItem("<--- Please Select --->", "-1"));
        }

        public void Show( string message)
        {
            //Response.Write($"{message}");
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }
}