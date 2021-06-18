using ManalizaDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManalizaDesktopApp.ViewModels
{
    public class PlaqueListingViewModel
    {

        // The method by which the given SQL entered from the interface is written to
        public static bool FillGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select PLAKA_NO, DARA from plaka", con);
            


            try
            {
                SQLiteDataAdapter adp = new SQLiteDataAdapter(com);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                grid.ItemsSource = null;
                grid.ItemsSource = dt.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //con.Dispose();
            }
            if (i > 0) return true; else return false;

        }   
    }
}
