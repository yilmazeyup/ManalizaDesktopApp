using ManalizaDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManalizaDesktopApp.ViewModels
{
    public class GetGridDataViewModel
    {
        // The method by which the given SQL entered from the interface is written to
        public static bool FillPlaqueGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);            
            SQLiteCommand com = new SQLiteCommand("select PLAKA_NO, DARA, SURUCU from plaka", con);
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
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
        public static bool FillCompanyGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select cari_unvan1, cari_muh_kod from FIRMALAR", con);
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
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
        public static bool FillCustomerGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select cari_unvan1, cari_muh_kod from CARI_HESAPLAR", con);
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
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
        public static bool FillStockTypeGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select san_isim from STOK_ANA_GRUPLARI", con);
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
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }
        public static bool FillStockGrid(DataGrid grid)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("select ISIM,MLZ_KOD from STOK_ALT_GRUPLARI", con);
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
                con.Dispose();
            }
            if (i > 0) return true; else return false;
        }


    }
}
