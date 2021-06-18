using ManalizaDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManalizaDesktopApp
{
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Window
    {
        public PrintPage()
        {
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT * FROM HAVUZ ORDER BY SNO DESC LIMIT 1", sQLiteConnection);
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sQLiteCommand);
            DataTable dataTable = new DataTable();
            sQLiteDataAdapter.Fill(dataTable);
            
            DataTableReader reader = new DataTableReader(dataTable);
            while (reader.Read())
            {
                txtSNO.Text = reader["SNO"].ToString();
                txtDateOfEntry.Text = reader["GIR_TARIH"].ToString();
                txtReleaseDate.Text = reader["GIR_TARIH"].ToString();
                txtPlaque.Text = reader["PLAKA"].ToString();
                txtStock.Text = reader["MALZEME_ADI"].ToString();
                txtCompany.Text = reader["FIRMA_ADI"].ToString();
                txtLicenseNumber.Text = reader["RUHSAT_NO"].ToString();
                txtPerson.Text = reader["SEVKIYAT_SR"].ToString();
                txtDeliveryPlace.Text = reader["SEVK_YAP_YER"].ToString();                
                txtCustomer.Text = reader["MUSTERI"].ToString();
                txtDestination.Text = reader["GIDECEK_YER"].ToString();
                txtFirstWeighing.Text = reader["I_TARTIM"].ToString();
                txtSecondWeighing.Text = reader["II_TARTIM"].ToString();
                txtNetWeight.Text = reader["NET_AG"].ToString();
            }

            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();                
                printDialog.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
                //if ((bool)printDialog.ShowDialog().GetValueOrDefault())
                //{
                //    FlowDocument flowDocument = new FlowDocument();
                //    foreach (string line in myTextBox.Text.Split('\n'))
                //    {
                //        Paragraph myParagraph = new Paragraph();
                //        myParagraph.Margin = new Thickness(0);
                //        myParagraph.Inlines.Add(new Run(line));
                //        flowDocument.Blocks.Add(myParagraph);
                //    }
                //    DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                //    printDialog.PrintDocument(paginator, this.presenter.Title);
                //}
                //if (printDialog.ShowDialog() == true)
                //{


                //    //printDialog.PrintVisual(print, "Weighing Receipt");
                //}
            }
            finally
            {
                this.IsEnabled = true;
            }
            
        }
        
        
    }
}
