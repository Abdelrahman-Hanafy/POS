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
        Bitmap bmp;
        Image i;
        //Graphics g;
        //Pen curser;

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

                
                //selectedHall.Visible = false;
            }
            if (IsPostBack)
            {
                i = (Image)Session["hall"];
            }

        }

        protected void halls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(halls.SelectedValue == "-1")
            {
                selectedHall.ImageUrl = @"";
                return;
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "Javascript", "javascript:draw(); ", true);

            try
            {
                i = Image.FromFile(@"E:\Work\Bibliotheca\FormProjects\POS\POS\Image\" + halls.SelectedItem.Text + ".jpg");
                Session["hall"] = i;
            }
            catch (Exception)
            {
                Show("No Digram for this iamge");
                return;
            }
            

            selectedHall.ImageUrl =@"Image\"+halls.SelectedItem.Text + ".jpg";
            selectedHall.HotSpots.Clear();
            foreach (DataRow row in db.fetchBlocks(halls.SelectedValue).Rows)
            {

                RectangleHotSpot ht = new RectangleHotSpot();
                ht.Bottom = int.Parse(row["Bottom"].ToString());
                ht.Left = int.Parse(row["Left"].ToString());
                ht.Right = int.Parse(row["Right"].ToString());
                ht.Top = int.Parse(row["Top"].ToString());

                ht.HotSpotMode = HotSpotMode.PostBack;
                ht.PostBackValue = row["ID"].ToString();

                selectedHall.HotSpots.Add(ht);
            }

            


        }

        
        protected void selectedHall_Click(object sender, ImageMapEventArgs e)
        {
            string id = e.PostBackValue;
            bmp = new Bitmap(i);

            foreach (DataRow row in db.fetchBlock(id).Rows)
            {
                int l = int.Parse(row["Left"].ToString()),
                    t = int.Parse(row["Top"].ToString()),
                    w = int.Parse(row["Right"].ToString()) - l,
                    h = int.Parse(row["Bottom"].ToString()) - t;


                using (Graphics gg = Graphics.FromImage(bmp))
                {
                    gg.DrawRectangle(new Pen(Color.Red), l,t, w, h);
                }
            }
            

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(ms.ToArray());
            Image1.Src = "data:image/gif;base64," + base64Data;
        }

        public void Show(string message)
        {
            Response.Write("<script>alert('" + message + "');</script>");
        }
    }
}