using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ManalizaDesktopApp.Models;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ManalizaDesktopApp.Services;
using ManalizaDesktopApp.ViewModels;
using System.Data;
using System.Collections.ObjectModel;
using System.IO.Ports;

namespace ManalizaDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        string AssignId;
        string recieved_data;
   
        

        public MainWindow()
        {
            client = new FirebaseClient(ifc);
            
            //Connect_btn.Content = "Connect";

        }

        
        //Authentication operation
        IFirebaseClient client;
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "nGwCV083BrmPjCO3QS89d5MxE2D3JhIGBFjgu0aE",
            BasePath = "https://manalizaapplication-default-rtdb.firebaseio.com/"
        };
        private object serial;

        //Grid content is cleared and data is pulled from Firebase // Manaliza Form Loaded Function
        public void formManalizaLoad(object sender, EventArgs args)
        {
            
            GetGridDataViewModel.FillPlaqueGrid(PlaqueGridOrder);
            GetGridDataViewModel.FillCompanyGrid(CompanyGridOrder);
            GetGridDataViewModel.FillCustomerGrid(CustomerGridOrder);
            GetGridDataViewModel.FillStockTypeGrid(StockTypeGridOrder);
            GetGridDataViewModel.FillStockGrid(StockGridOrder);
            RecordToGridViewModel.FillGrid(gridTemplateData);
            lastreceipt();            
            dbSqliteConnection.ConnectionTest();            
            
            FirebaseResponse response = client.Get(@"Drivers");
            Dictionary<string, Driver> data = JsonConvert.DeserializeObject<Dictionary<string, Driver>>(response.Body.ToString());
            PopulateDataGrid(data);

            void PopulateDataGrid(Dictionary<string, Driver> record)
            {

                DataTable dataTable = new DataTable();



                DataGridTextColumn Company = new DataGridTextColumn();
                Company.Header = "Company";
                Company.Width = 180;
                DataGridTextColumn Customer = new DataGridTextColumn();
                Customer.Header = "Customer";
                Customer.Width = 180;
                DataGridTextColumn Stock = new DataGridTextColumn();
                Stock.Header = "Stock";
                Stock.Width = 80;
                DataGridTextColumn Plaque = new DataGridTextColumn();
                Plaque.Header = "Plaque";
                Plaque.Width = 80;
                DataGridTextColumn FirstName = new DataGridTextColumn();
                FirstName.Header = "FirstName";
                FirstName.Width = 100;
                DataGridTextColumn LastName = new DataGridTextColumn();
                LastName.Header = "LastName";
                LastName.Width = 100;
                DataGridTextColumn CompanyCode = new DataGridTextColumn();
                CompanyCode.Header = "CompanyCode";
                CompanyCode.Width = 180;
                DataGridTextColumn CustomerCode = new DataGridTextColumn();
                CustomerCode.Header = "CustomerCode";
                CustomerCode.Width = 180;
                DataGridTextColumn StockCode = new DataGridTextColumn();
                CustomerCode.Header = "StockCode";
                CustomerCode.Width = 180;
                CustomerCode.Visibility = Visibility.Hidden;


                dataTable.Columns.Add("Company", typeof(string));
                dataTable.Columns.Add("Customer", typeof(string));
                dataTable.Columns.Add("Stock", typeof(string));
                dataTable.Columns.Add("Plaque", typeof(string));
                dataTable.Columns.Add("FirstName", typeof(string));
                dataTable.Columns.Add("LastName", typeof(string));
                dataTable.Columns.Add("CompanyCode", typeof(string));
                dataTable.Columns.Add("CustomerCode", typeof(string));
                dataTable.Columns.Add("StockCode", typeof(string));


                dataGridViewOrder.ItemsSource = dataTable.DefaultView;

                dataTable.Rows.Clear();

                Driver driver = new Driver();
                foreach (var item in record)
                {
                    if (item.Value.PlaceOfOperation == "Ertepe Şantiyesi")
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Company"] = item.Value.Company;
                        dataRow["Customer"] = item.Value.Customer;
                        dataRow["Stock"] = item.Value.Stock;
                        dataRow["Plaque"] = item.Value.Plaque;
                        dataRow["FirstName"] = item.Value.FirstName;
                        dataRow["LastName"] = item.Value.LastName;
                        dataRow["CompanyCode"] = item.Value.CompanyCode;
                        dataRow["CustomerCode"] = item.Value.CustomerCode;
                        dataRow["StockCode"] = item.Value.StockCode;
                        dataTable.Rows.Add(dataRow);
                    }
                    else { }
                }

                dataTable.Columns.Add();
            }

        }

        //GridView refresh timer
        public void dataGridViewOrder_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMinutes(1);
            dt.Tick += dtTicker;
            dt.Start();
        }

        //Grid content is cleared and data is pulled from Firebase
        public async void dtTicker(object sender, EventArgs e)
        {

            dbSqliteConnection.ConnectionTest();
            RecordToGridViewModel.FillGrid(gridTemplateData);
            FirebaseResponse response = client.Get(@"Drivers");
            Dictionary<string, Driver> data = JsonConvert.DeserializeObject<Dictionary<string, Driver>>(response.Body.ToString());
            PopulateDataGrid(data);

            void PopulateDataGrid(Dictionary<string, Driver> record)
            {
                DataTable dataTable = new DataTable();

                DataGridTextColumn Company = new DataGridTextColumn();
                Company.Header = "Company";
                Company.Width = 180;
                DataGridTextColumn Customer = new DataGridTextColumn();
                Customer.Header = "Customer";
                Customer.Width = 180;
                DataGridTextColumn Stock = new DataGridTextColumn();
                Stock.Header = "Stock";
                Stock.Width = 80;
                DataGridTextColumn Plaque = new DataGridTextColumn();
                Plaque.Header = "Plaque";
                Plaque.Width = 80;
                DataGridTextColumn FirstName = new DataGridTextColumn();
                FirstName.Header = "FirstName";
                FirstName.Width = 100;
                DataGridTextColumn LastName = new DataGridTextColumn();
                LastName.Header = "LastName";
                LastName.Width = 100;

                dataTable.Columns.Add("Company", typeof(string));
                dataTable.Columns.Add("Customer", typeof(string));
                dataTable.Columns.Add("Stock", typeof(string));
                dataTable.Columns.Add("Plaque", typeof(string));
                dataTable.Columns.Add("FirstName", typeof(string));
                dataTable.Columns.Add("LastName", typeof(string));


                dataGridViewOrder.ItemsSource = dataTable.DefaultView;

                dataTable.Rows.Clear();

                Driver driver = new Driver();
                foreach (var item in record)
                {
                    if (item.Value.PlaceOfOperation == "Ertepe Şantiyesi")
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Company"] = item.Value.Company;
                        dataRow["Customer"] = item.Value.Customer;
                        dataRow["Stock"] = item.Value.Stock;
                        dataRow["Plaque"] = item.Value.Plaque;
                        dataRow["FirstName"] = item.Value.FirstName;
                        dataRow["LastName"] = item.Value.LastName;
                        dataTable.Rows.Add(dataRow);
                    }
                    else { }
                }

                dataTable.Columns.Add();
            }
        }

        //The selected button is cleared in this set of GotFocus Event
        public void txtPlaque_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaqueGridOrder.Visibility = Visibility.Visible;
            
            if (txtPlaque.Text == "Plaka")
            {
                txtPlaque.Clear();

            }
            else { }
        }
        public void txtCompany_GotFocus(object sender, RoutedEventArgs e)
        {
            CompanyGridOrder.Visibility = Visibility.Visible;
            if (txtCompany.Text == "Firma")
            {
                txtCompany.Clear();
            }
            else { }

        }
        public void txtCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            CustomerGridOrder.Visibility = Visibility.Visible;
            if (txtCustomer.Text == "Musteri")
            {
                txtCustomer.Clear();
            }
            else { }
        }
        public void txtPerson_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPerson.Text == "Sevkiyat Sorumlusu")
            {
                txtPerson.Clear();
            }
            else { }
        }
        public void txtDeliveryPlace_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDeliveryPlace.Text == "Sevkiyat Yapılacak Yer")
            {
                txtDeliveryPlace.Clear();
            }
            else { }
        }
        public void txtDriver_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDriver.Text == "Sürücü")
            {
                txtDriver.Clear();
            }
            else { }
        }
        public void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text == "Açıklama")
            {
                txtDescription.Clear();
            }
            else { }
        }
        public void txtFirstWeighing_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstWeighing.Text == "Birinci Tartım")
            {
                txtFirstWeighing.Clear();
            }
            else { }
        }
        public void cbDestination_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDestination.Text = "";
        }
        public void txtStockType_GotFocus(object sender, RoutedEventArgs e)
        {
            StockTypeGridOrder.Visibility = Visibility.Visible;

            if (txtStockType.Text == "Malzeme Tipi")
            {
                txtStockType.Clear();

            }
            else { }
        }
        public void txtStock_GotFocus(object sender, RoutedEventArgs e)
        {
            StockGridOrder.Visibility = Visibility.Visible;

            if (txtStock.Text == "Malzeme")
            {
                txtStock.Clear();

            }
            else { }
        }
        public void txtSecondWeighing_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSecondWeighing.Text == "İkinci Tartım")
            {
                txtSecondWeighing.Clear();
            }
            else { }
        }

        // New weighing button function
        private void btnNewWeighing_Click(object sender, RoutedEventArgs e)
        {
            lastreceipt();
        }

        // Calculating last receipt
        public void lastreceipt()
        {
            Definitions definitions = new Definitions();
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select * from HAVUZ order by SNO desc LIMIT 1", con);
            SQLiteDataReader dr = com.ExecuteReader();
            




            if (dr.Read())
            {
                definitions.sondeg = Convert.ToInt32(dr["SNO"].ToString()) + 1;
                AssignId = definitions.sondeg.ToString("000");
            }
            else if (Convert.IsDBNull(dr))
            {
                AssignId = ("001");
            }
            else
            {
                AssignId = ("001");
            }
            con.Close();
            AssignId.ToString();
            
            txtPlaque.Text = "Plaka";
            txtCompany.Text = "Firma";
            txtCustomer.Text = "Musteri";
            txtStockType.Text = "Malzeme Tipi";
            txtStock.Text = "Malzeme";
            txtDeliveryPlace.Text = "Sevkiyat Yapılacak Yer";
            txtPerson.Text = "Sevkiyat Sorumlusu";
            txtDriver.Text = "Sürücü";
            txtDescription.Text = "Açıklama";
            txtFirstWeighing.Text = "Birinci Tartım";
            txtSecondWeighing.Text = "İkinci Tartım";
            txtNetWeight.Text = "Net Ağırlık";
            txtDestination.Text = "Gideceği Yer";

        }

        //First weighing button function
        public void btnFirstWeighing_Click(object sender, RoutedEventArgs e)
        {
            Plaques plaques = new Plaques();

            plaques.PlaqueNumber = txtPlaque.Text;
            plaques.Tare = txtFirstWeighing.Text;
            plaques.Driver = txtDriver.Text;
            if (InsertSqliteViewModel.AddPlaqueSqlite(plaques))
            {
                MessageBox.Show("Kayıt başarılı");
            }
            else
            {
                MessageBox.Show("Kayıt başarısız");
            }
        }

        //Last weighing button function //insert SQLite
        public void btnLastWeighing_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            SQLiteCommand sQLiteCommand = new SQLiteCommand("SELECT PLAKA FROM HAVUZ ORDER BY SNO DESC LIMIT 1", sQLiteConnection);
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(sQLiteCommand);
            DataTable dataTable = new DataTable();
            sQLiteDataAdapter.Fill(dataTable);
           
            DataTableReader reader = new DataTableReader(dataTable);
            while (reader.Read())
            {

                if (reader["PLAKA"].ToString() == txtPlaque.Text)
                {

                    MessageBoxResult result = MessageBox.Show("Aracın kaydı oluşturulmuş. Kayıt silinsin mi?", "Uyarı", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            

                            Definitions definitions = new Definitions();

                            definitions.sondeg = int.Parse(AssignId.ToString());
                            definitions.Plaque = txtPlaque.Text;
                            definitions.Company = txtCompany.Text;
                            definitions.Customer = txtCustomer.Text;
                            definitions.StockType = txtStockType.Text;
                            definitions.Stock = txtStock.Text;
                            definitions.DeliveryPlace = txtDeliveryPlace.Text;
                            definitions.Person = txtPerson.Text;
                            definitions.Destination = txtDestination.Text;
                            definitions.Driver = txtDriver.Text;
                            definitions.carikod = txtCompanyCode.Text;
                            definitions.DateOfEntry = DateTime.Now;
                            definitions.musterikod = txtCustomerCode.Text;
                            definitions.stokkod = txtStockCode.Text;
                            definitions.Driver = txtDriver.Text;
                            definitions.FirstWeighing = int.Parse(txtFirstWeighing.Text);
                            definitions.SecondWeighing = int.Parse(txtSecondWeighing.Text);
                            definitions.NetWeight = int.Parse(txtNetWeight.Text);
                            if (InsertSqliteViewModel.AddRecordSqlite(definitions))
                            {
                                MessageBox.Show("Kayıt başarılı");
                            }
                            else
                            {
                                MessageBox.Show("Kayıt başarısız");
                            }

                            break;
                        case MessageBoxResult.No:
                            
                            break;
                    }

                }
                else 
                {
                    Definitions definitions = new Definitions();

                    definitions.sondeg = int.Parse(AssignId.ToString());
                    definitions.Plaque = txtPlaque.Text;
                    definitions.Company = txtCompany.Text;
                    definitions.Customer = txtCustomer.Text;
                    definitions.StockType = txtStockType.Text;
                    definitions.Stock = txtStock.Text;
                    definitions.DeliveryPlace = txtDeliveryPlace.Text;
                    definitions.Person = txtPerson.Text;
                    definitions.Destination = txtDestination.Text;
                    definitions.Driver = txtDriver.Text;
                    definitions.DateOfEntry = DateTime.Now;
                    definitions.carikod = txtCompanyCode.Text;
                    definitions.musterikod = txtCustomerCode.Text;
                    definitions.stokkod = txtStockCode.Text;
                    definitions.Driver = txtDriver.Text;
                    definitions.FirstWeighing = int.Parse(txtFirstWeighing.Text);
                    definitions.SecondWeighing = int.Parse(txtSecondWeighing.Text);
                    definitions.NetWeight = int.Parse(txtNetWeight.Text);


                    if (InsertSqliteViewModel.AddRecordSqlite(definitions))
                    {
                        MessageBox.Show("Kayıt başarılı");
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız");
                    }
                }
                
            }
        }

        //Transfer of data in the SQLite to textboxes
        public void gridTemplateData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {
                txtCompany.Text = selectedrow["FIRMA"].ToString();
                txtCustomer.Text = selectedrow["MUSTERI"].ToString();
                txtStockType.Text = selectedrow["MALZ_TIPI"].ToString();
                txtStock.Text = selectedrow["MALZEME"].ToString();
                txtDestination.Text = selectedrow["GIDECEGI_YER"].ToString();
                txtPerson.Text = selectedrow["SEVKIYAT_SR"].ToString();
                txtDeliveryPlace.Text = selectedrow["SEVK_YAP_YER"].ToString();
                txtCompanyCode.Text = selectedrow["FIRMA_KOD"].ToString();
                txtCustomerCode.Text = selectedrow["MUSTERI_KOD"].ToString();
                txtPerson.Text = "ULAŞ AKBAĞ";
                txtDeliveryPlace.Text = "ŞİLE-İSTANBUL";
            }
            else
            {
                MessageBox.Show("Lütfen başka satır seçiniz.");
            }
        }

        //Transfer of data in the Firebase to textboxes
        public void dataGridViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            DataGrid grd = dataGridViewOrder;
            DataRowView selectedrowedit = grd.SelectedValue as DataRowView;
            if (selectedrowedit != null)
            {
                txtPlaque.Text = selectedrowedit["Plaque"].ToString();
                txtCompany.Text = selectedrowedit["Company"].ToString();
                txtCustomer.Text = selectedrowedit["Customer"].ToString();
                txtDriver.Text = selectedrowedit["FirstName"].ToString();
                txtCompanyCode.Text = selectedrowedit["CompanyCode"].ToString();
                txtCustomerCode.Text = selectedrowedit["CustomerCode"].ToString();
                //txtStockCode.Text = selectedrowedit["StockCode"].ToString();
                txtStock.Text = selectedrowedit["Stock"].ToString();
                if (selectedrowedit.Row["Stock"].ToString().EndsWith("KİL") == true)
                {
                    txtStockType.Text = "KİL";
                }
                else
                {
                    txtStockType.Text = "KUM";
                }
                txtPerson.Text = "ULAŞ AKBAĞ";
                txtDeliveryPlace.Text = "ŞİLE-İSTANBUL";
            }
            else
            {
                MessageBox.Show("Lütfen başka satır seçiniz.");
            }
        }

        //Events that occur when exiting the textboxes
        public void txtPlaque_LostFocus(object sender, RoutedEventArgs e)
        {
            PlaqueGridOrder.Visibility = Visibility.Hidden;
            if (txtPlaque.Text == "")
            {
                txtPlaque.Text = "Plaka";

            }
            else { }
        }
        public void txtCompany_LostFocus(object sender, RoutedEventArgs e)
        {
            CompanyGridOrder.Visibility = Visibility.Hidden;
            if (txtCompany.Text == "")
            {
                txtCompany.Text = "Firma";

            }
            else { }
        }
        public void txtCustomer_LostFocus(object sender, RoutedEventArgs e)
        {
            CustomerGridOrder.Visibility = Visibility.Hidden;
            if (txtCustomer.Text == "")
            {
                txtCustomer.Text = "Musteri";

            }
            else { }
        }
        public void txtStockType_LostFocus(object sender, RoutedEventArgs e)
        {
            StockTypeGridOrder.Visibility = Visibility.Hidden;
            if (txtStockType.Text == "")
            {
                txtStockType.Text = "Malzeme Tipi";

            }
            else { }
        }
        public void txtStock_LostFocus(object sender, RoutedEventArgs e)
        {
            StockGridOrder.Visibility = Visibility.Hidden;
            if (txtStock.Text == "")
            {
                txtStock.Text = "Malzeme";

            }
            else { }
        }
        public void txtSecondWeighing_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSecondWeighing.Text == "")
            {
                txtSecondWeighing.Text = "İkinci Tartım";

            }
            else { }
        }
        public void txtFirstWeighing_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFirstWeighing.Text == "")
            {
                txtFirstWeighing.Text = "Birinci Tartım";

            }
            else { }
        }

        //Actions when items in textboxes are changed
        public void txtPlaque_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select PLAKA_NO, DARA, SURUCU from plaka Where PLAKA_NO like '%" + txtPlaque.Text + "%'", con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = com;
            dt.Clear();
            adp.Fill(dt);
            DataTableReader dr = new DataTableReader(dt);
            while(dr.Read())
            {
                PlaqueGridOrder.ItemsSource = null;
                PlaqueGridOrder.ItemsSource = dt.DefaultView;
            }
            con.Close();
        }
        public void txtCompany_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select cari_unvan1, cari_muh_kod from FIRMALAR Where cari_unvan1 like '%" + txtCompany.Text + "%'", con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = com;
            dt.Clear();
            adp.Fill(dt);
            DataTableReader dr = new DataTableReader(dt);
            while (dr.Read())
            {
                CustomerGridOrder.ItemsSource = null;
                CustomerGridOrder.ItemsSource = dt.DefaultView;
            }
            con.Close();
        }
        public void txtCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select cari_unvan1, cari_muh_kod from CARI_HESAPLAR Where cari_unvan1 like '%" + txtCustomer.Text + "%'", con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = com;
            dt.Clear();
            adp.Fill(dt);
            DataTableReader dr = new DataTableReader(dt);
            while (dr.Read())
            {
                CustomerGridOrder.ItemsSource = null;
                CustomerGridOrder.ItemsSource = dt.DefaultView;
            }
            con.Close();
        }
        public void txtStockType_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select san_isim from STOK_ANA_GRUPLARI Where san_isim like '%" + txtStockType.Text + "%'", con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = com;
            dt.Clear();
            adp.Fill(dt);
            DataTableReader dr = new DataTableReader(dt);
            while (dr.Read())
            {
                StockTypeGridOrder.ItemsSource = null;
                StockTypeGridOrder.ItemsSource = dt.DefaultView;
            }
            con.Close();
        }
        public void txtStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(Services.dbSqliteConnection.dbAddress);
            con.Open();
            SQLiteCommand com = new SQLiteCommand("select ISIM,MLZ_KOD from STOK_ALT_GRUPLARI Where ISIM like '%" + txtStock.Text + "%'", con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = com;
            dt.Clear();
            adp.Fill(dt);
            DataTableReader dr = new DataTableReader(dt);
            while (dr.Read())
            {
                StockGridOrder.ItemsSource = null;
                StockGridOrder.ItemsSource = dt.DefaultView;
            }
            con.Close();
        }
        public void txtCounter_TextChanged(object sender, TextChangedEventArgs e)
        {

            SerialPort port = new SerialPort("COM1",
        9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.DataReceived += Port_DataReceived;
           

        }

        public void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = new SerialPort("COM1",
        9600, Parity.None, 8, StopBits.One);
            if (port.IsOpen == true)
            {
                txtCounter.Text = port.ReadExisting().ToString();
            }
            else { }
        }

        public void txtFirstWeighing_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtFirstWeighing.Text != "Birinci Tartım" && txtSecondWeighing.Text != "İkinci Tartım" && txtFirstWeighing.Text != "" && txtSecondWeighing.Text != "" && txtCounter.Text != "")
            {
               
                int x = int.Parse(txtSecondWeighing.Text);
                int y = int.Parse(txtFirstWeighing.Text);
                int z = x - y;
                txtNetWeight.Text = z.ToString();
            }
            else { }
            
        }
        public void txtSecondWeighing_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFirstWeighing.Text != "Birinci Tartım" && txtSecondWeighing.Text != "İkinci Tartım" && txtFirstWeighing.Text != "" && txtSecondWeighing.Text != "" && txtCounter.Text != "")
            {

                int x = int.Parse(txtSecondWeighing.Text);
                int y = int.Parse(txtFirstWeighing.Text);
                int z = x - y;
                txtNetWeight.Text = z.ToString();
            }
            else { }
        }

        //Transferring data in grids to textboxes
        public void PlaqueGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {
                txtPlaque.Text = selectedrow["PLAKA_NO"].ToString();
                txtFirstWeighing.Text = selectedrow["DARA"].ToString();
                txtDriver.Text = selectedrow["SURUCU"].ToString();

            }
            else
            { }
        }
        public void CompanyGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {
                
                txtCompany.Text = selectedrow["cari_unvan1"].ToString();
                txtCompanyCode.Text = selectedrow["cari_muh_kod"].ToString();

            }
            else
            { }
        }
        public void CustomerGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {

                txtCustomer.Text = selectedrow["cari_unvan1"].ToString();
                txtCustomerCode.Text = selectedrow["cari_muh_kod"].ToString();

            }
            else
            { }
        }
        public void StockTypeGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {
                txtStockType.Text = selectedrow["san_isim"].ToString();
                txtStockCode.Text = selectedrow["san_isim"].ToString();
            }
            else
            { }
        }
        public void StockGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            DataRowView selectedrow = grid.SelectedItem as DataRowView;
            if (selectedrow != null)
            {
                txtStock.Text = selectedrow["ISIM"].ToString();
                txtStockCode.Text= selectedrow["MLZ_KOD"].ToString();
            }
            else
            { }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintPage printPage = new PrintPage();
            printPage.Show();
        }






    }

   

}

