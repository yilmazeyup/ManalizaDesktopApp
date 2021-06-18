using ManalizaDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManalizaDesktopApp.ViewModels
{
    public class InsertSqliteViewModel
    {
        
        // The method by which the given SQL entered from the interface is written to
        public static bool AddRecordSqlite(Definitions definitions)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("Insert into HAVUZ (SNO, GIR_TARIH, SEVKIYAT_SR, MALZEME_TIPI, MLZ_GRPKD, MALZEME_KOD, MALZEME_ADI, PLAKA, SURUCU, I_TARTIM, II_TARTIM," +
                "NET_AG, MAD_SEVK_NO, MUSTERI, SEVK_YAP_YER, GIDECEK_YER, FIRMA_KOD, FIRMA_ADI, MUSTERI_KOD, RUHSAT_NO, ISLEM_TIP, EVRAK_TIP, YEDEK_I, YEDEK_II, YEDEK_III, YEDEK_IV) values" +
                "(@SNO, @GIR_TARIH, @SEVKIYAT_SR, @MALZEME_TIPI, @MLZ_GRPKD, @MALZEME_KOD, @MALZEME_ADI, @PLAKA, @SURUCU, @I_TARTIM, @II_TARTIM," +
                "@NET_AG, @MAD_SEVK_NO, @MUSTERI, @SEVK_YAP_YER, @GIDECEK_YER, @FIRMA_KOD, @FIRMA_ADI, @MUSTERI_KOD, @RUHSAT_NO, @ISLEM_TIP, @EVRAK_TIP, @YEDEK_I, @YEDEK_II, @YEDEK_III, @YEDEK_IV)", con);

            
            com.Parameters.AddWithValue("@SNO", definitions.sondeg);
            com.Parameters.AddWithValue("@GIR_TARIH", definitions.DateOfEntry.Date);
            com.Parameters.AddWithValue("@SEVKIYAT_SR", definitions.Person);
            com.Parameters.AddWithValue("@MALZEME_TIPI", definitions.StockType);
            com.Parameters.AddWithValue("@MLZ_GRPKD", definitions.stokmuhkod);
            com.Parameters.AddWithValue("@MALZEME_KOD", definitions.stokkod);
            com.Parameters.AddWithValue("@MALZEME_ADI", definitions.Stock);
            com.Parameters.AddWithValue("@PLAKA", definitions.Plaque);
            com.Parameters.AddWithValue("@SURUCU", definitions.Driver);
            com.Parameters.AddWithValue("@I_TARTIM", definitions.FirstWeighing);
            com.Parameters.AddWithValue("@II_TARTIM", definitions.SecondWeighing);
            com.Parameters.AddWithValue("@NET_AG", definitions.NetWeight);
            com.Parameters.AddWithValue("@MAD_SEVK_NO",definitions.MineReferralNumber );
            com.Parameters.AddWithValue("@MUSTERI", definitions.Customer);
            com.Parameters.AddWithValue("@SEVK_YAP_YER", definitions.DeliveryPlace);
            com.Parameters.AddWithValue("@GIDECEK_YER", definitions.Destination);
            com.Parameters.AddWithValue("@FIRMA_KOD", definitions.carikod);
            com.Parameters.AddWithValue("@FIRMA_ADI", definitions.Company);
            com.Parameters.AddWithValue("@MUSTERI_KOD",definitions.musterikod);
            com.Parameters.AddWithValue("@RUHSAT_NO",11478);
            com.Parameters.AddWithValue("@ISLEM_TIP", "Cikis");
            com.Parameters.AddWithValue("@EVRAK_TIP", 1);
            com.Parameters.AddWithValue("@YEDEK_I", definitions.kriter_i);
            com.Parameters.AddWithValue("@YEDEK_II", definitions.kriter_ii);
            com.Parameters.AddWithValue("@YEDEK_III", definitions.kriter_iii);
            com.Parameters.AddWithValue("@YEDEK_IV", definitions.kriter_iv);

            try
            {
                con.Open();
                i = (sbyte)com.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Dispose();
            }
            if (i > 0) return true; else return false;            
            
        }

        public static bool AddPlaqueSqlite(Plaques plaques)
        {
            sbyte i = 0;
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand com = new SQLiteCommand("Insert into plaka (PLAKA_NO, DARA, SURUCU) values (@PLAKA_NO, @DARA, @SURUCU)", con);


            com.Parameters.AddWithValue("@PLAKA_NO", plaques.PlaqueNumber);
            com.Parameters.AddWithValue("@DARA", plaques.Tare);
            com.Parameters.AddWithValue("@SURUCU", plaques.Driver);
           

            try
            {
                con.Open();
                i = (sbyte)com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Dispose();
            }
            if (i > 0) return true; else return false;

        }
    }
}
