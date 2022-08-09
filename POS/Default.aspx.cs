using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS
{
    public partial class _Default : Page
    {
        DataBase db;
        DataTable hallTable;
        DataTable evntTable;

        int defaultPrice = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DataBase.ConnectDB();

            if (IsPostBack)
            {

                hallTable = (DataTable)Session["hallTable"];
                evntTable = (DataTable)Session["evntTable"];
            }
            else
            {
                updateHalls();
            }
            

            
        }

        protected void Halls_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string id = Halls.SelectedValue;
            DataRow r = evntTable.Select($"ID = {id}")[0];
            string sh = hallTable.Select($"ID = {r["HallID"]}")[0]["ID"].ToString();

            DataTable blks = db.fetchPrices(id);

            List<int> ls = new List<int>(), ts = new List<int>(), ws = new List<int>(), hs = new List<int>(), ps = new List<int>(); 
            foreach (DataRow row in db.fetchBlocks(sh).Rows)
            {
                try
                {
                    DataRow blk = blks.Select($"BlockId = {row["ID"]}")[0];
                    ps.Add(int.Parse(blk["Price"].ToString()));
                }
                catch (Exception)
                {
                    ps.Add(defaultPrice);
                }
                

                int l = int.Parse(row["Left"].ToString()),
                     t = int.Parse(row["Top"].ToString()),
                     w = int.Parse(row["Width"].ToString()),
                     h = int.Parse(row["Height"].ToString());

                ls.Add(l);
                ts.Add(t);
                ws.Add(w);
                hs.Add(h);
            }
            string serializedls = (new JavaScriptSerializer()).Serialize(ls);
            string serializedts = (new JavaScriptSerializer()).Serialize(ts);
            string serializedws = (new JavaScriptSerializer()).Serialize(ws);
            string serializedhs = (new JavaScriptSerializer()).Serialize(hs);
            string serializedps = (new JavaScriptSerializer()).Serialize(ps);

            ScriptManager.RegisterStartupScript(Page, GetType(), "Javascript", "javascript:init(" + serializedls + ","+ serializedts + "," + serializedws + "," + serializedhs + "," + serializedps+ "); ", true);

        }



        private void updateHalls()
        {

            hallTable = db.fetchHall();
            Session["hallTable"] = hallTable;

            evntTable = db.fetchEvents();
            Session["evntTable"] = evntTable;

            Halls.DataSource = evntTable;
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