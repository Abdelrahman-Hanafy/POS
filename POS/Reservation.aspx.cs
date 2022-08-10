﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS
{
    public partial class _Default : Page
    {
        static DataBase db;
        DataTable hallTable;
        DataTable evntTable;

        int defaultPrice = 50;
        static string id= "-1";

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

            id = Halls.SelectedValue;
            if(id == "-1")
            {

                return;
            }

            DataRow r = evntTable.Select($"ID = {id}")[0];
            string sh = hallTable.Select($"ID = {r["HallID"]}")[0]["ID"].ToString();

            DataTable blks = db.fetchPrices(id);
            DataTable seats = db.fetchSeats(id);

            List<int> ls = new List<int>(), ts = new List<int>(), ws = new List<int>(), hs = new List<int>(), ps = new List<int>(), ids = new List<int>();
            List<string> ss = new List<string>();
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
                     h = int.Parse(row["Height"].ToString()),
                     d = int.Parse(row["ID"].ToString());

                DataRow[] seatdata = seats.Select($"BlockId = {d}");
                string data = "";
                foreach (DataRow seat in seatdata)
                {
                    data += seat["SeatX"] + "," + seat["SeatY"] + ";";

                }

                ls.Add(l);
                ts.Add(t);
                ws.Add(w);
                hs.Add(h);
                ids.Add(d);
                ss.Add(data);
            }

            string serializedls = (new JavaScriptSerializer()).Serialize(ls);
            string serializedts = (new JavaScriptSerializer()).Serialize(ts);
            string serializedws = (new JavaScriptSerializer()).Serialize(ws);
            string serializedhs = (new JavaScriptSerializer()).Serialize(hs);
            string serializedps = (new JavaScriptSerializer()).Serialize(ps);
            string serializedids = (new JavaScriptSerializer()).Serialize(ids);
            string serializediss = (new JavaScriptSerializer()).Serialize(ss);

            ScriptManager.RegisterStartupScript(Page, GetType(), "Javascript", "javascript:init("+ serializediss + "," + serializedids + "," + serializedls + "," + serializedts + "," + serializedws + "," + serializedhs + "," + serializedps + "); ", true);

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


        [WebMethod]
        public static string Reserve( string n,string m,string c ,string cells )
        {
            string[] cell = cells.Split(';'); // for each cell block,x,y;
            DataTable tb = db.addReservation(n,m,c,id);
            string rev = tb.Rows[0]["ID"].ToString();

            foreach(string cl in cell)
            {
                if (cl.Length < 1) continue;

                string[] data = cl.Split(',');
                db.addTicket(rev,data[0],data[1],data[2]);
            }

            return rev;

        }
    }
}