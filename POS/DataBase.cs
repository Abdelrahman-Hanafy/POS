using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace POS
{
    public class DataBase
    {
        private static DataBase DB;
        private SqlConnection conn;

        private DataBase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            //create instanace of database connection
            conn = new SqlConnection(connStr);

            //open connection
            conn.Open();
        }

        public static DataBase ConnectDB()
        {
            if (DB == null)
            {
                DB = new DataBase();
            }

            return DB;
        }

        public DataTable fetchHall()
        {
            string query = "exec fetchHall ";
            return fillTable(query);
        }

        public DataTable fetchPrices(string ev)
        {
            string query = $"exec fetchPrices {ev} ";
            return fillTable(query);
        }

        public DataTable fetchSeats(string ev)
        {
            string query = $"exec fetchSeats {ev} ";
            return fillTable(query);
        }

        public DataTable fetchBlocks(string hall)
        {
            string query = $"exec fetchBlocks {hall}";
            return fillTable(query);
        }

        public DataTable fetchBlock(string id)
        {
            string query = $"exec fetchBlock {id}";
            return fillTable(query);
        }

        public DataTable fetchEvents()
        {
            string query = $"exec fetchEvents ";
            return fillTable(query);
        }

        public void addPrice(int p, string e, string b)
        {
            string query = $"exec addPrice {p},{b},{e}";
            exec(query);
        }

        public void addEvent(string h, string n, string d, string t, string r)
        {
            string query = $"exec addEvent {h},'{n}','{d}','{t}',{r}";
            exec(query);
        }

        public void addTicket(string r, string b, string x, string y)
        {
            string query = $"exec addTicket {r},{b},{x},{y}";
            exec(query);
        } 

        public DataTable addReservation(string n, string m, string c, string ev)
        {
            string query = $"exec addReservation '{n}','{m}',{c},{ev}";
            return fillTable(query);
        }

        private void exec(string query)
        {
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        private DataTable fillTable(string query)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);

            return dt;

        }

    }
}