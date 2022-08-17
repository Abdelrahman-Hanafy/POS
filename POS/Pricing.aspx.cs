using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace POS
{
    public partial class Events : Page
    {

        static DataBase db;
        static string sev;
        DataTable evnts;

        protected void Page_Load(object sender, EventArgs e)
        {

            db = DataBase.ConnectDB();
            if (!IsPostBack)
            {
                evnts = db.fetchEvents();
                Session["evnts"] = evnts;
                events.DataSource = evnts;
                events.DataTextField = "Name";
                events.DataValueField = "ID";
                events.DataBind();

                events.Items.Insert(0, new ListItem("<-- select -->", "-1"));

            }
            if (IsPostBack)
            {
                evnts = (DataTable)Session["evnts"];
            }

        }

        protected void events_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = events.SelectedValue;
            sev = id;
            DataRow sh = eventHall(id);

            List<int> ls = new List<int>(), ts = new List<int>(), ws = new List<int>(), hs = new List<int>(), 
                ids = new List<int>(), aas = new List<int>(), rs = new List<int>(), bs = new List<int>();
            foreach (DataRow row in db.fetchBlocks(sh["ID"].ToString()).Rows)
            {
                int l = int.Parse(row["Left"].ToString()),
                     t = int.Parse(row["Top"].ToString()),
                     r = int.Parse(row["Right"].ToString()),
                     b = int.Parse(row["Bottom"].ToString()),
                     w = int.Parse(row["Width"].ToString()),
                     h = int.Parse(row["Height"].ToString()),
                     d = int.Parse(row["ID"].ToString()),
                     a = int.Parse(row["Angle"].ToString());

                ls.Add(l);
                ts.Add(t);
                rs.Add(r);
                bs.Add(b);
                ws.Add(w);
                hs.Add(h);
                ids.Add(d);
                aas.Add(a);
                

            }
            string serializedls = (new JavaScriptSerializer()).Serialize(ls);
            string serializedts = (new JavaScriptSerializer()).Serialize(ts);
            string serializedrs = (new JavaScriptSerializer()).Serialize(rs);
            string serializedbs = (new JavaScriptSerializer()).Serialize(bs);
            string serializedws = (new JavaScriptSerializer()).Serialize(ws);
            string serializedhs = (new JavaScriptSerializer()).Serialize(hs);
            string serializedids = (new JavaScriptSerializer()).Serialize(ids);
            string serializediaas = (new JavaScriptSerializer()).Serialize(aas);

            ScriptManager.RegisterStartupScript(Page, GetType(), "Javascript", "javascript:init(" + serializedids + "," + serializedls + "," + serializedts + "," + serializedws + "," + serializedhs + "," + serializediaas + "," + serializedrs + "," + serializedbs + "); ", true);
                       

        }

        public DataRow eventHall(string id)
        {
            DataRow r = evnts.Select($"ID = {id}")[0];
            DataRow h =  db.fetchHall().Select($"ID = {r["HallID"]}")[0];
            return h;

        }

        [WebMethod]
        public static string asg_Click(string blk,string row, int price)
        {
            
            //return "done";
            try
            {
                db.addPrice(price, sev, blk,row);
                return "done";
            }
            catch (Exception)
            {
                return "error";
            }
            
        }

        [WebMethod]
        public static string edt_Click(string blk, string row, int price)
        {

            //return "done";
            try
            {
                db.altPrice(price, sev, blk, row);
                return "done";
            }
            catch (Exception)
            {
                return "error";
            }

        }
    }
}