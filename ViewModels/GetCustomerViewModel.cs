using ManalizaDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManalizaDesktopApp.ViewModels
{
    public class GetCustomerViewModel
    {
        public static bool ReadCustomer(Customer customer)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select  cari_vdaire_no, cari_muh_kod from CARI_HESAPLAR", con);
            try
            {
                SQLiteDataAdapter adp = new SQLiteDataAdapter(com);
                DataTable dt = new DataTable();
                //DataColumn Plaka = new DataColumn("Plaque", typeof(string));
                //DataColumn Company = new DataColumn("Company", typeof(string));
                //DataColumn Customer = new DataColumn("Customer", typeof(string));
                //DataColumn StockType = new DataColumn("StockType", typeof(string));
                //DataColumn Stock = new DataColumn("Stock", typeof(string));
                //DataColumn Destination = new DataColumn("Destination", typeof(string));
                //DataColumn Person = new DataColumn("Person", typeof(string));
                //DataColumn DeliveryPlace = new DataColumn("DeliveryPlace", typeof(string));               
                adp.Fill(dt);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
    }
}
