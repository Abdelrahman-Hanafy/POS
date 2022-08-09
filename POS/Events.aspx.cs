using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace POS
{
    public partial class Events : Page
    {

        DataBase db;
        DataTable evnts;
        Bitmap bmp;
        Image i;

        protected void Page_Load(object sender, EventArgs e)
        {

            db = DataBase.ConnectDB();
            if (!IsPostBack)
            {
                evnts = db.fetchEvents();
                Session["evnts"] = evnts;
                halls.DataSource = evnts;
                halls.DataTextField = "Name";
                halls.DataValueField = "ID";
                halls.DataBind();

                halls.Items.Insert(0, new ListItem("<-- select -->", "-1"));

            }
            if (IsPostBack)
            {
                i = (Image)Session["hall"];
                evnts = (DataTable)Session["evnts"];
            }

        }

        protected void halls_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = halls.SelectedValue;
            DataRow h = eventHall(id);
            string name = h["Name"].ToString();
            if (id == "-1")
            {
                hall.Src = @"";
                return;
            }

            try
            {
                string url = @"E:\Work\Bibliotheca\FormProjects\POS\POS\Image\" + name + ".jpg";
                i = Image.FromFile(url);
                Session["hall"] = i;
                bmp = new Bitmap(i);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                hall.Src = "data:image/gif;base64," + base64Data;
                
            }
            catch (Exception)
            {
                Show("No Digram for this iamge");
                return;
            }


            blocks.DataSource = db.fetchBlocks(h["ID"].ToString());
            blocks.DataTextField = "ID";
            blocks.DataValueField = "ID";
            blocks.DataBind();

            blocks.Items.Insert(0, new ListItem("<-- select -->", "-1"));
            

        }

        protected void blocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = blocks.SelectedValue;
            bmp = new Bitmap(i);

            if(id == "-1")
            {
                ticketPrice.Enabled = false;
                halls_SelectedIndexChanged(sender, e);
                return;
            }

            foreach (DataRow row in db.fetchBlock(id).Rows)
            {
                int l = int.Parse(row["Left"].ToString()),
                    t = int.Parse(row["Top"].ToString()),
                    w = int.Parse(row["Right"].ToString()) - l,
                    h = int.Parse(row["Bottom"].ToString()) - t;


                using (Graphics gg = Graphics.FromImage(bmp))
                {
                    gg.DrawRectangle(new Pen(Color.Red), l, t, w, h);
                }
            }


            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            hall.Src = "data:image/gif;base64," + base64Data;

            ticketPrice.Enabled = true;
        }



        public DataRow eventHall(string id)
        {
            DataRow r = evnts.Select($"ID = {id}")[0];
            DataRow h =  db.fetchHall().Select($"ID = {r["HallID"].ToString()}")[0];
            return h;

        }

        public void Show(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }

        protected void asg_Click(object sender, EventArgs e)
        {
            try
            {
                int price = int.Parse(ticketPrice.Text);
                string b = blocks.SelectedValue;
                string h = halls.SelectedValue;

                db.addPrice(price,h,b);
            }
            catch (FormatException )
            {
                Show("Please provide int price");
            }
            catch (SqlException )
            {
                Show("Aleady assigned bolck");
            }

            
        }
    }
}