using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace VenditaInventario
{
    public partial class Vendita : Form
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;foreign keys=true;Version= 3;");

        DataTable dtRicerca = new DataTable();

        decimal totIndice1 = 0, totIndice2 = 0, totIndice3 = 0, totMax = 0, totLordo = 0, totMaxBuono = 0, costoOriginale = 0;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        

        public Vendita()
        {
            //defining the culture
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

            //Importa inventario da file excel
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            //Importa modifiche da file database
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += backgroundWorker2_WorkCompleted;
            backgroundWorker2.ProgressChanged += backgroundWorker2_ProgressChanged;

            //Export Inventario
            backgroundWorker3.DoWork += backgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += backgroundWorker3_WorkCompleted;
            backgroundWorker3.ProgressChanged += backgroundWorker3_ProgressChanged;
        }
        
        private void Vendita_Load(object sender, EventArgs e)
        {
            
            String directory = Path.GetDirectoryName(Application.ExecutablePath) + "\\inventario.sqlite";

            log.Info("Program StartUp");

            if (!File.Exists(directory))
            {
                try
                {
                    sqlite_conn.Open();

                    SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                    sqlite_cmd.CommandText = "CREATE TABLE inventario (id integer primary key, nome varchar(300), autore varchar(50), casa varchar(50), codice varchar(50) unique not null, prezzo varchar(50), anno varchar(50), indice varchar(4));";
                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_cmd.CommandText = "CREATE TABLE statistiche (id integer primary key autoincrement, libriID integer, venditaID integer, FOREIGN KEY (libriID) REFERENCES inventario(id), FOREIGN KEY (venditaID) REFERENCES vendita(id));";
                    sqlite_cmd.ExecuteNonQuery();
                    sqlite_cmd.CommandText = "CREATE TABLE vendita (id integer primary key autoincrement, data varchar(50), metodo varchar(4), costo decimal(50));";
                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_conn.Close();

                    MessageBox.Show("Nessun inventario trovato, ne è stato creato uno nuovo", "Inventario Creato!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (SQLiteException ex)
                {
                    MessageBox.Show("Vendita_load Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex.StackTrace);
                    log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                }
            }
            //Inizio il controllo per una nuova versione e il task di update
            update();

            //Controllo se devo effettuare modifiche al database
            String databaseUpdateFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\databaseUpdate.txt";

            if(File.Exists(databaseUpdateFile))
            {
                log.Info("Trovato un aggiornamento per il database");
                databaseUpdate();
                try
                {
                    File.Delete(databaseUpdateFile);
                }
                catch(Exception ex)
                {
                    log.Error("Errore nella cancellazione del file. " + ex.Message, ex);
                }
                
            }
            uploadStatistiche();
            populateTable();
        }

        private async void update()
        {
            //Controllo se la rete e' disponibile
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    //impostazione delle variabili
                    String URLString = "https://www.libridicartaonline.it/acquistoAggiornamento.xml";
                    String downloadURL = null;
                    var currentVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                    var remoteVersion = new Version();
                    int result = 0;

                    //Stiamo scaricando l'xml da un sito https quindi va impostato il protocollo da usare
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback((s, ce, ch, ssl) => true);
              
                    XmlDocument xml = new XmlDocument();
                
                    xml.Load(URLString);
                    XmlNodeList xnList = xml.SelectNodes("/aggiornamento/links");
                    foreach (XmlNode xn in xnList)
                    {
                        string version = xn["version"].InnerText;
                        downloadURL = xn["URL"].InnerText;
                        remoteVersion = new Version(version);
                        result = remoteVersion.CompareTo(currentVersion);
                    }

                    if (result > 0) //Esiste una nuova versione
                    {
                        DialogResult dialogResult = MessageBox.Show("È disponibile una nuova versione per il download, scaricare ora?\nVersione corrente: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " -> Nuova versione: " + remoteVersion.ToString(), "Aggiornamento disponibile", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        { 
                            log.Info("Esiste una nuova versione, inizio del download.");
                            Updater frm = new Updater();
                            frm.SetURL = downloadURL;
                            frm.ShowDialog();
                        }
                    }

                    log.Info("Nessuna nuova versione disponibile");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("update() Error", "Updater error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                }
            } else
            {
                log.Info("Nessuna connessione ad internet, riprovo l'update tra 60 minuti..");
                await Task.Delay(3600000);
                log.Info("Fine attesa, controllo versione..");
                update();
            }
        }

        private void databaseUpdate()
        {
            try
            {
                log.Info("Inizio modifica database");
                sqlite_conn.Open();

                SQLiteTransaction transaction = sqlite_conn.BeginTransaction();

                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                sqlite_cmd.CommandText = "PRAGMA foreign_keys = 0";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE inventario_temp AS SELECT * FROM inventario";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "DROP TABLE inventario;";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "CREATE TABLE inventario (id INTEGER PRIMARY KEY, nome VARCHAR(300), autore VARCHAR(50), casa VARCHAR(50), codice VARCHAR(50) UNIQUE NOT NULL, prezzo VARCHAR(50), anno VARCHAR(50), indice VARCHAR(4))";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO inventario (id, nome, autore, casa, codice, prezzo, anno, indice) SELECT id, nome, autore, casa, codice, prezzo, anno, indice FROM inventario_temp";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "DROP TABLE inventario_temp";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "PRAGMA foreign_keys = 1";
                sqlite_cmd.ExecuteNonQuery();



                transaction.Commit();
                transaction.Dispose();

                sqlite_conn.Close();

                log.Info("Fine modifica database");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " - " + ex.StackTrace);
                log.Error("Errore nella modifica del database. " + ex.Message, ex);
            }
        }

        private async void uploadStatistiche()
        {
            try
            {
                //Controllo se esiste una connessione ad internet
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "inventario.sqlite");
                    string destFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "uploadStatistiche.sqlite");
                    File.Copy(sourceFile, destFile, true);
                    
                    log.Info("Inizio upload statistiche..");
                    //Controllo se questo computer ha mai inviato delle statistiche
                    if (directoryExists())
                    { 
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.libridicartaonline.it/statistiche/"+ Environment.MachineName + "/statistiche.sqlite");
                        request.KeepAlive = false;
                        request.Credentials = new NetworkCredential("guest@libridicartaonline.it", "guestpassword!15");
                        request.Method = WebRequestMethods.Ftp.UploadFile;

                        using (Stream fileStream = File.OpenRead(Path.GetDirectoryName(Application.ExecutablePath) + "\\uploadStatistiche.sqlite"))
                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            fileStream.CopyTo(ftpStream);
                        }
                    }
                    else
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.libridicartaonline.it/statistiche/" + Environment.MachineName);
                        request.Method = WebRequestMethods.Ftp.MakeDirectory;
                        request.KeepAlive = false;
                        request.Credentials = new NetworkCredential("guest@libridicartaonline.it", "guestpassword!15");
                        using (var resp = (FtpWebResponse)request.GetResponse())
                        {
                            log.Info("Creazione della directory per questa macchina..: " + resp.StatusCode);
                        }
                        uploadStatistiche();
                    }
                    
                } else
                {
                    log.Info("Nessuna connessione ad internet, riprovo l'upload tra 60 minuti..");
                    await Task.Delay(3600000);
                    log.Info("Fine attesa, tento upload..");
                    uploadStatistiche();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("uploadStatistiche() Error", "Uploader error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
            finally
            {
                log.Info("Upload statistiche completato!");
                log.Info("Prossimo upload tra 60 minuti");
                await Task.Delay(3600000);
                uploadStatistiche();
            }
        }

        public bool directoryExists()
        {
            /* Create an FTP Request */
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://ftp.libridicartaonline.it/statistiche/"+ Environment.MachineName+"/");
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential("guest@libridicartaonline.it", "guestpassword!15");
            /* Specify the Type of FTP Request */
            ftpRequest.KeepAlive = false;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            try
            {
                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            /* Resource Cleanup */
            finally
            {
                ftpRequest = null;
            }
        }

        //Upload delle statistiche prima della chiusura
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                return;
            }
            else
            {
                uploadStatistiche();
                return;
            }
        }

        private void apriModifica()
        {
            string isbn = tabellaRicerca.CurrentRow.Cells[4].Value.ToString();


            try
            {

                Modifica frm = new Modifica();

                using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;foreign keys=true;Version= 3;"))
                {

                    sqlite_conn.Open();

                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        sqlite_cmd.CommandText = "SELECT id, nome, autore, casa, codice, prezzo, anno, indice FROM inventario WHERE codice='" + isbn + "'";
                        sqlite_cmd.ExecuteNonQuery();

                        SQLiteDataReader sqlite_dataReader = sqlite_cmd.ExecuteReader();

                        

                        if (sqlite_dataReader.Read())
                        {
                            
                            frm.SetId = Convert.ToString(sqlite_dataReader[0]);
                            frm.modificaTitoloTextbox.Text = (String)sqlite_dataReader[1];
                            frm.modificaAutoreTextbox.Text = (String)sqlite_dataReader[2];
                            frm.modificaCasaTextbox.Text = (String)sqlite_dataReader[3];
                            frm.modificaIsbnTextbox.Text = (String)sqlite_dataReader[4];
                            frm.modificaPrezzoTextbox.Text = (String)sqlite_dataReader[5];
                            frm.modificaAnnoTextbox.Text = (String)sqlite_dataReader[6];
                            frm.modificaIndiceCombo.Text = (String)sqlite_dataReader[7];


                        }
                        sqlite_dataReader.Close();
                        sqlite_conn.Close();

                        
                    }
                }

                frm.ShowDialog();

            }

            catch (SQLiteException ex)
            {
                MessageBox.Show("apriModifica() Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }

        public void populateTable()
        {
            try
            {
                SQLiteDataAdapter sqlite_adapter = new SQLiteDataAdapter("SELECT * FROM inventario", sqlite_conn);

                dtRicerca.Clear();
                
                sqlite_adapter.Fill(dtRicerca);

                tabellaRicerca.DataSource = dtRicerca;
                tabellaRicerca.ClearSelection();

                GC.Collect();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }

        private void cancella()
        {
            libriTotLvl1.Text = "0.00€";
            libriTotLvl2.Text = "0.00€";
            libriTotLvl3.Text = "0.00€";
            importoMaxBuono.Text = "0.00€";
            costoTextbox.Text = "0.00€";
            importoLordo.Text = "0.00€";
            costoOriginaleLabel.Text = "0.00€";
            tabellaVendita.Rows.Clear();

            totIndice1 = 0;
            totIndice2 = 0;
            totIndice3 = 0;
            totMax = 0;
            totMaxBuono = 0;
            totLordo = 0;
            costoOriginale = 0;

            importoMaxBuono.Visible = false;
            label17.Visible = false;
            buonoPanel.Visible = false;
            cbBuono.Checked = false;
            cbBuono.Visible = false;
            costoTextbox.Enabled = false;
        }

        private void inserimento(String isbn)
        {
            try
            {
                if (isbn.Length == 13)
                {

                    sqlite_conn.Open();

                    SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                    sqlite_cmd.CommandText = "SELECT nome, autore, casa, codice, prezzo, anno, indice FROM inventario WHERE codice='" + isbn + "'";

                    SQLiteDataReader sqlite_dataReader = sqlite_cmd.ExecuteReader();

                    if (sqlite_dataReader.Read())
                    {
                        String[] row = new String[7];

                        for (int i = 0; i < 7; i++)
                        {
                            row[i] = (String)sqlite_dataReader[i];
                        }

                        tabellaVendita.Rows.Add(row);
                        tabellaVendita.ClearSelection();

                        //Addiziono i valori nei vari label per il prezzo
                        if (!row[6].Equals("") && row[6] != null)
                        {
                            switch (Convert.ToInt32(row[6]))
                            {
                                case 1:
                                    totIndice1 += Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.1;
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.1);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.1);
                                    //Imposto i valori dei label
                                    libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                                    break;
                                case 2:
                                    totIndice2 += Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2;
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2);
                                    //Imposto i valori dei label
                                    libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                                    break;
                                case 3:
                                    totIndice3 += Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3;
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3);
                                    //Imposto i valori dei label
                                    libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                                    break;
                            }
                            totMaxBuono = Math.Round(totMax * (decimal)1.1, 2);

                            costoTextbox.Text = Convert.ToString(Math.Round(totMax, 2)) + "€";
                            importoMaxBuono.Text = Convert.ToString(totMaxBuono) + "€";
                            totLordo += Convert.ToDecimal(row[4].Replace(",", "."));
                            importoLordo.Text = Convert.ToString(Math.Round(totLordo, 2)) + "€";
                            costoOriginaleLabel.Text = Convert.ToString(Math.Round(costoOriginale, 2)) + "€";
                        }

                        cbBuono.Visible = true;
                        costoTextbox.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("ISBN [" + isbnVendita.Text + "] non trovato/da non comprare!", "Libro non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sqlite_dataReader.Close();

                    sqlite_conn.Close();

                    isbnVendita.ResetText();
                }
                else if (isbn.Length < 10)
                {
                    sqlite_conn.Open();

                    SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                    sqlite_cmd.CommandText = "SELECT nome, autore, casa, codice, prezzo, anno, indice FROM inventario WHERE id='" + isbn + "'";

                    SQLiteDataReader sqlite_dataReader = sqlite_cmd.ExecuteReader();

                    if (sqlite_dataReader.Read())
                    {
                        String[] row = new String[7];

                        for (int i = 0; i < 7; i++)
                        {
                            row[i] = (String)sqlite_dataReader[i];
                        }

                        tabellaVendita.Rows.Add(row);
                        tabellaVendita.ClearSelection();

                        //Addiziono i valori nei vari label per il prezzo
                        if(!row[6].Equals("") && row[6] != null)
                        { 
                            switch (Convert.ToInt32(row[6]))
                            {
                                case 1:
                                    totIndice1 += (Convert.ToDecimal(row[4].Replace(",",".")) * (decimal)0.1);
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.1);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.1);
                                    //Imposto i valori dei label
                                    libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                                    break;
                                case 2:
                                    totIndice2 += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2);
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.2);
                                    //Imposto i valori dei label
                                    libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                                    break;
                                case 3:
                                    totIndice3 += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3);
                                    totMax += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3);
                                    costoOriginale += (Convert.ToDecimal(row[4].Replace(",", ".")) * (decimal)0.3);
                                    //Imposto i valori dei label
                                    libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                                    break;
                            }
                            totMaxBuono = Math.Round(totMax * (decimal)1.1, 2);
                            //Imposto i label
                            costoTextbox.Text = Convert.ToString(Math.Round(totMax, 2)) + "€";
                            importoMaxBuono.Text = Convert.ToString(totMaxBuono) + "€";
                            totLordo += Convert.ToDecimal(row[4].Replace(",", "."));
                            importoLordo.Text = Convert.ToString(totLordo) + "€";
                            costoOriginaleLabel.Text = Convert.ToString(Math.Round(costoOriginale, 2)) + "€";
                        }
                        cbBuono.Visible = true;
                        costoTextbox.Enabled = true;
                        
                    }
                    else
                    {
                        MessageBox.Show("ISBN ["+ isbnVendita.Text +"] non trovato/da non comprare!", "Libro non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sqlite_dataReader.Close();

                    sqlite_conn.Close();

                    isbnVendita.ResetText();
                }
                else
                {
                    MessageBox.Show("Controlla il numero di cifre del codice", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.StackTrace);
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }

        private void rimozioneRiga()
        {
            //Poiche rimuovo la riga dalla tabella allora scompare anche dalle righe selezionate
            //per riuscire a continuare il loop sul resto delle righe il mio for deve partire 'dall'alto'
            for (int i = tabellaVendita.SelectedRows.Count - 1; i >= 0; i--)
            {
                if (!tabellaVendita.SelectedRows[i].Cells[6].Value.ToString().Equals("") && tabellaVendita.SelectedRows[i].Cells[6].Value.ToString() != null)
                {
                    //Controllo che indice sto cancellando cosi rimuovo il libro dal totale
                    //bisogna usare lo switch prima poiche' altrimenti cancellerei i dati prima di usarli
                    switch (Convert.ToInt32(tabellaVendita.SelectedRows[i].Cells[6].Value.ToString()))
                    {
                        case 1:
                            totIndice1 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.1);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.1);
                            if (costoOriginale > 0 && (costoOriginale - (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.1)) > 0)
                            {
                                costoOriginale -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.1);
                            }
                            else
                            {
                                costoOriginale = totMax;
                            }
                            //Imposto i valori dei label
                            libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                            break;
                        case 2:
                            totIndice2 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.2);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.2);
                            if (costoOriginale > 0 && (costoOriginale - (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.2)) > 0)
                            {
                                costoOriginale -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.2);
                            }
                            else
                            {
                                costoOriginale = totMax;
                            }
                            //Imposto i valori dei label
                            libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                            break;
                        case 3:
                            totIndice3 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.3);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.3);
                            if (costoOriginale > 0 && (costoOriginale - (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.3)) > 0)
                            {
                                costoOriginale -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", ".")) * (decimal)0.3);
                            }
                            else
                            {
                                costoOriginale = totMax;
                            }
                            //Imposto i valori dei label
                            libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                            break;
                    }
                }
                totMaxBuono = Math.Round(totMax * (decimal)1.1, 2);
                //Imposto i label
                costoTextbox.Text = Convert.ToString(Math.Round(totMax, 2)) + "€";
                importoMaxBuono.Text = Convert.ToString(totMaxBuono) + "€";
                totLordo -= Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString().Replace(",", "."));
                importoLordo.Text = Convert.ToString(Math.Round(totLordo, 2)) + "€";
                costoOriginaleLabel.Text = Convert.ToString(Math.Round(costoOriginale, 2)) + "€";
                //Rimozione della riga dalla tabella
                tabellaVendita.Rows.RemoveAt(tabellaVendita.SelectedRows[i].Index);
            }
            //Nascondo il comboBox se non ho libri nella tabella
            if (tabellaVendita.Rows.Count == 0)
            {
                cancella();
            }
        }

        /* Duplice funzione: Osserviamo se abbiamo premuto invio, in questo caso avvio inserimento() con quello che ho inserito,
         * nel frattempo osserva se raggiungo le 13 cifre di un ISBN standard, in quel caso avvio inserimento() con il valore di isbnVendita
         */
        private void IsbnVendita_KeyUp(object sender, KeyEventArgs e)
        {
            if (isbnVendita.Text.Length == 13)
            {
                inserimento(isbnVendita.Text.Trim());
            } else if (e.KeyCode == Keys.Enter)
            {
                if (isbnVendita.Text.Length >= 10)
                {
                    inserimento(isbnVendita.Text.Trim());
                }
            }
        }

        private void btnInserisci_Click(object sender, EventArgs e)
        {
            inserimento(isbnVendita.Text.Trim());
        }

        private void ricerca(String query)
        {
            //Per ricercare tutte le colonne programmaticamente aggiungo il nome delle colonne programmaticamente
            //Non tutte le colonne sono del tipo String(VARCHAR) dal DB quindi le converto dentro il RowFilter automaticamente
            StringBuilder sb = new StringBuilder();

            foreach (DataColumn column in dtRicerca.DefaultView.Table.Columns)
            {
                sb.AppendFormat("CONVERT({0}, System.String) Like '%{1}%' OR ", column.ColumnName, query.Trim());
            }

            //Rimuovo gli ultimi tre caratteri cosi rimuovo le lettere 'OR ' e il RowFilter accetta la stringa
            sb.Remove(sb.Length - 3, 3);

            dtRicerca.DefaultView.RowFilter = sb.ToString();

            tabellaRicerca.DataSource = dtRicerca;


        }

        //Filtro la tabella della ricerca per cercare in ogni colonna il contenuto di textboxRicerca
        private void textboxRicerca_TextChanged(object sender, EventArgs e)
        {
            ricerca(textboxRicerca.Text);
        }

        //Aggiungiamo un libro alla lista con un doppio click sulla tabella della ricerca
        private void tabellaRicerca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string idRicerca = tabellaRicerca.Rows[e.RowIndex].Cells[0].Value.ToString();

            inserimento(idRicerca);

            tabPages.TabPages[0].Show();
            tabPages.SelectedIndex = 0;

            isbnVendita.Focus();
            tabellaRicerca.ClearSelection();
        }

        //Dobbiamo usare PreviewKey in questo caso perche' per definizione il programma dopo aver registrato 
        //il bottone invio scorre la selezione alla riga sottostante, invece in questo modo prima scorre il metodo
        //e poi scende con il puntatore
        private void tabellaRicerca_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string idRicerca = tabellaRicerca.Rows[tabellaRicerca.CurrentRow.Index].Cells[0].Value.ToString();

                inserimento(idRicerca);

                tabPages.TabPages[0].Show();
                tabPages.SelectedIndex = 0;

                isbnVendita.Focus();
                tabellaRicerca.ClearSelection();
            }
        }

        /* Usiamo un background worker su un thread separato per leggere i dati da un foglio excel:
         * Ci aspettiamo che il foglio sia composto da soli dati e che abbia 7 colonne.
         * Lasciamo che sia il metodo a sfogliarlo fino alla fine.
         */
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            log.Info("Inizio Importo");

            try
            {
                //Otteniamo il nome del file da utilizzare per l'inserimento
                string selectedPath = (string)e.Argument;

                //Creaiamo un nuovo excelPackage con using cosi che venga rimosso automaticamente
                using (var package = new ExcelPackage(new FileInfo(@selectedPath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    int rowCount = worksheet.Dimension.Rows;
                    //Bisogna ottenere l'ultima riga "vera", quella contenente dei valori, e non le righe che hanno una formattazione ma sono vuote
                    while (rowCount > 1)
                    {
                        if (!string.IsNullOrEmpty((string)worksheet.Cells[rowCount, 1].Value))
                        {
                            break;
                        }
                        rowCount--;
                    }

                    sqlite_conn.Open();

                    SQLiteTransaction transaction = sqlite_conn.BeginTransaction();

                    for (int i = 1; i < rowCount + 1; i++)
                    {
                        using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                        {
                            sqlite_cmd.CommandText = "INSERT INTO inventario(nome, autore, casa, codice, prezzo, anno, indice) Values (@Nome, @Autore, @Casa, @Codice, @Prezzo, @Anno, @Indice) ON CONFLICT(codice) DO UPDATE SET nome = excluded.nome, autore = excluded.autore, casa = excluded.casa, prezzo = excluded.prezzo, anno = excluded.anno, indice = excluded.indice";

                            sqlite_cmd.Parameters.AddWithValue("@Nome", worksheet.Cells[i, 1].Value);//nome
                            sqlite_cmd.Parameters.AddWithValue("@Autore", worksheet.Cells[i, 2].Value);//autore
                            sqlite_cmd.Parameters.AddWithValue("@Casa", worksheet.Cells[i, 3].Value);//casa
                            sqlite_cmd.Parameters.AddWithValue("@Codice", worksheet.Cells[i, 4].Value);//codice
                            sqlite_cmd.Parameters.AddWithValue("@Prezzo", worksheet.Cells[i, 5].Value);//prezzo
                            sqlite_cmd.Parameters.AddWithValue("@Anno", worksheet.Cells[i, 6].Value);//anno
                            sqlite_cmd.Parameters.AddWithValue("@Indice", worksheet.Cells[i, 7].Value);//indice

                            sqlite_cmd.ExecuteNonQuery();
                        }

                        backgroundWorker1.ReportProgress((int)Math.Round((double)(i * 100) / rowCount));

                    }

                    transaction.Commit();
                    transaction.Dispose();

                    sqlite_conn.Close();
                }
                log.Info("Fine Aggiunta libri al database.");
                populateTable();

            } catch(Exception ex)
            {
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
            

            
        }

        //Questo metodo segnala il progresso del backgroundWorker cosi possiamo usare la progressBar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelProgressbar.Text = "Importo..: " + Convert.ToString(e.ProgressPercentage) + "%";
            //La percentuale e' una proprieta di e
            progressBar1.Value = e.ProgressPercentage;
        }

        //Questo metodo viene chiamato alla fine del backgroundWorker
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Foglio Excel importato!", "Importato", MessageBoxButtons.OK, MessageBoxIcon.Information);

            progressBar1.Visible = false;

            labelProgressbar.ResetText();

            log.Info("Fine Importo");
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnNuovoCliente_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Registrare i libri?", "Nuova vendita", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (cbBuono.Checked) { 
                    if(totMaxBuono < 1000)
                    {
                        registraStatistica();
                    }
                    else
                    {
                        MessageBox.Show("Controlla l'importo massimo dato al cliente.\n(Deve essere minore di 1000€)", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else if(!cbBuono.Checked)
                {
                    if (totMax < 1000)
                    {
                        registraStatistica();
                    }
                    else
                    {
                        MessageBox.Show("Controlla l'importo massimo dato al cliente.\n(Deve essere minore di 1000€)", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            
        }

        private void registraStatistica()
        {
            try
            {
                String data = null;

                sqlite_conn.Open();

                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                SQLiteCommand sqlite_cmd2 = sqlite_conn.CreateCommand();

                //Controlliamo se abbiamo gia' inserito altre quantita' oggi

                data = DateTime.Now.ToString("dd/MM/yyyy");
                long rowID = 0;

                SQLiteTransaction transaction = null;
                transaction = sqlite_conn.BeginTransaction();

                sqlite_cmd2.CommandText = "INSERT INTO [vendita] (data, costo, metodo) Values (@Data, @Costo, @Metodo); SELECT last_insert_rowid();";
                sqlite_cmd2.Parameters.AddWithValue("@Data", data);

                if (cbBuono.Checked)
                {
                    sqlite_cmd2.Parameters.AddWithValue("@Metodo", "B");
                    sqlite_cmd2.Parameters.AddWithValue("@Costo", totMaxBuono);
                }
                else
                {
                    sqlite_cmd2.Parameters.AddWithValue("@Metodo", "C");
                    sqlite_cmd2.Parameters.AddWithValue("@Costo", totMax);
                }

                rowID = (long)sqlite_cmd2.ExecuteScalar();
                foreach (DataGridViewRow row in tabellaVendita.Rows)
                {
                    if (!row.Cells[6].Value.Equals(""))
                    {
                        long libriID = 0;


                        SQLiteCommand sqlite_cmd3 = sqlite_conn.CreateCommand();
                        sqlite_cmd3.CommandText = "SELECT id FROM inventario WHERE codice='" + row.Cells[3].Value + "'";
                        SQLiteDataReader r = sqlite_cmd3.ExecuteReader();
                        if (r.Read())
                        {
                            libriID = (long)r[0];
                        }
                        r.Close();

                        sqlite_cmd.CommandText = "INSERT INTO [statistiche] (libriID, venditaID) Values (@Libro, @Vendita)";
                        sqlite_cmd.Parameters.AddWithValue("@Libro", libriID);
                        sqlite_cmd.Parameters.AddWithValue("@Vendita", rowID);

                        sqlite_cmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                transaction.Dispose();


                sqlite_conn.Close();

                MessageBox.Show("Registrati!", "Registro statistiche", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
            }
            cancella();
        }

        private void btnCancella_Click(object sender, EventArgs e)
        {
            rimozioneRiga();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            apriModifica();
        }

        private void tabellaVendita_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                rimozioneRiga();
            }
        }

        private void tabellaStatistiche_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            if (tabellaStatistiche.Rows[e.RowIndex].Cells[4].Value.Equals("Contanti"))
            { 
                tabellaStatistiche.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            }
            else 
            {
                tabellaStatistiche.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
            }
            
        }

        private void importaModificheInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog importFile = new OpenFileDialog();
            
            importFile.Filter = "Database file (*.sqlite)|*.sqlite;";
            importFile.FilterIndex = 0;
            importFile.RestoreDirectory = true;

            if (importFile.ShowDialog() == DialogResult.OK)
            {
                labelImporto.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;

                backgroundWorker2.RunWorkerAsync(importFile.FileName);
            }
        }

        //Importa modifiche database
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                string selectedFileName = (string)e.Argument;

                sqlite_conn.Open();

                SQLiteTransaction transaction = null;
                transaction = sqlite_conn.BeginTransaction();

                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "ATTACH DATABASE '" + selectedFileName + "' AS import;";
                sqlite_cmd.ExecuteNonQuery();

                SQLiteCommand sqlite_cmd2 = sqlite_conn.CreateCommand();
                sqlite_cmd2.CommandText = "CREATE TABLE IF NOT EXISTS temp_inv AS SELECT id, nome, autore, casa, codice, prezzo, anno, indice FROM import.inventario;";
                sqlite_cmd2.ExecuteNonQuery();

                SQLiteCommand sqlite_cmd3 = sqlite_conn.CreateCommand();
                sqlite_cmd3.CommandText = "UPDATE inventario SET nome = (SELECT nome FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "autore = (SELECT autore FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "casa = (SELECT casa FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "codice = (SELECT codice FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "prezzo = (SELECT prezzo FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "anno = (SELECT anno FROM temp_inv WHERE temp_inv.id = inventario.id)," +
                                                            "indice = (SELECT indice FROM temp_inv WHERE temp_inv.id = inventario.id);";
                sqlite_cmd3.ExecuteNonQuery();

                SQLiteCommand sqlite_cmd4 = sqlite_conn.CreateCommand();
                sqlite_cmd4.CommandText = "DROP TABLE IF EXISTS temp_inv;";
                sqlite_cmd4.ExecuteNonQuery();

                transaction.Commit();
                transaction.Dispose();

                sqlite_conn.Close();

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " - " + ex.StackTrace);
                log.Error("Error in importaModificheInventarioToolStripMenuItem_Click - " + ex);
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void backgroundWorker2_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Visible = false;
            labelImporto.Visible = false;

            MessageBox.Show("Modifiche Importate!", "Modifiche Importate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aggiungiLibroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Aggiungi frm = new Aggiungi();
                
                frm.ShowDialog();

            }

            catch (SQLiteException ex)
            {
                MessageBox.Show("aggiungiLibroToolStripMenuItem_Click Error", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
                log.Error("aggiungiLibroToolStripMenuItem_Click Error - Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }

        private void btnRimuoviRiga_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vuoi cancellare il libro selezionato?", "Rimozione", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string id = tabellaRicerca.CurrentRow.Cells[0].Value.ToString();

                try
                {
                
                    using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;foreign keys=true;Version= 3;"))
                    {

                        sqlite_conn.Open();

                        using (SQLiteCommand sqlite_cmd2 = sqlite_conn.CreateCommand())
                        {
                            sqlite_cmd2.CommandText = "SELECT libriID FROM statistiche WHERE libriID='" + id + "'";
                            SQLiteDataReader sqlite_dataReader = sqlite_cmd2.ExecuteReader();
                            if (sqlite_dataReader.Read())
                            {
                                MessageBox.Show("Impossibile rimuovere il libro selezionato poiche' e' presente in una o piu' vendite.\nPer non comparlo piu' usare la modifica e impostare l'indice a VUOTO!", "Impossibile rimuovere libro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                                {
                                    sqlite_cmd.CommandText = "DELETE FROM inventario WHERE id='" + id + "'";
                                    sqlite_cmd.ExecuteNonQuery();

                                    sqlite_cmd.Dispose();
                                }
                                MessageBox.Show("Libro rimosso!", "Rimosso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            sqlite_cmd2.Dispose();
                        }

                        
                        sqlite_conn.Close();
                    }
                    
                }

                catch (SQLiteException ex)
                {
                    MessageBox.Show("btnRimuoviRiga Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex.Message + ex.StackTrace);
                    log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                }
            }
        }

        private void ImportaInventarioxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Files( .xls, .xlsx)|*.xls; *.xlsx";
            file.ShowDialog();

            string filePath = file.FileName;

            if (filePath != null && !filePath.Equals(""))
            {
                progressBar1.Visible = true;

                //Faccio partiore il Worker
                backgroundWorker1.RunWorkerAsync(filePath);

            }
        }

        private void ImportaModificheDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog importFile = new OpenFileDialog();

            importFile.Filter = "Database file (*.sqlite)|*.sqlite;";
            importFile.FilterIndex = 0;
            importFile.RestoreDirectory = true;

            if (importFile.ShowDialog() == DialogResult.OK)
            {
                labelImporto.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;

                backgroundWorker2.RunWorkerAsync(importFile.FileName);
            }
        }

        private void ExportInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (var savefile = new SaveFileDialog())
            {
                savefile.FileName = "exportInventario.xlsx";
                savefile.Filter = "File Excel(*.xlsx)|*.xlsx";
                DialogResult result = savefile.ShowDialog();

                if (result == DialogResult.OK)
                {
                    labelImporto.Visible = true;
                    progressBar1.Visible = true;
                    progressBar1.Style = ProgressBarStyle.Marquee;
                    progressBar1.MarqueeAnimationSpeed = 30;

                    backgroundWorker3.RunWorkerAsync(savefile.FileName);
                }
            }
        }

        private void AggiungiLibroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Aggiungi frm = new Aggiungi();

                frm.ShowDialog();

            }

            catch (SQLiteException ex)
            {
                MessageBox.Show("aggiungiLibroToolStripMenuItem_Click Error", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
                log.Error("aggiungiLibroToolStripMenuItem_Click Error - Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }

        private void costoTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (costoOriginale == 0)
                    {
                        costoOriginale = totMax;
                    }
                    totMax = Convert.ToDecimal(costoTextbox.Text.Replace("€", string.Empty).Replace(",", "."));
                    totMaxBuono = totMax * (decimal)1.1;

                    importoMaxBuono.Text = Convert.ToString(totMaxBuono) + "€";
                    costoOriginaleLabel.Text = Convert.ToString(costoOriginale) + "€";
                    costoTextbox.Text = costoTextbox.Text + "€";
                    buonoPanel.Visible = true;
                    importoOriginaleLabel.Visible = true;
                    costoOriginaleLabel.Visible = true;
                }
                catch (Exception ex)
                {
                    log.Error("Errore in costoTextbox_KeyPress");
                    log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                    Debug.WriteLine(ex);
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        
        private void cbBuono_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBuono.Checked)
            {
                importoMaxBuono.Visible = true;
                label17.Visible = true;
                buonoPanel.Visible = true;
            }
            if (!cbBuono.Checked)
            {
                importoMaxBuono.Visible = false;
                label17.Visible = false;
                buonoPanel.Visible = false;
            }
        }

        private void btnAggiornaRicerca_Click(object sender, EventArgs e)
        {
            populateTable();
        }

        //statistiche
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tabellaStatistiche.Rows.Clear();
                String ISBN = null, data= null, titolo=null, prezzo=null, metodo=null, indiceString=null;
                int quantitaContanti = 0, quantitaBuoni = 0;
                int? lastVenditaID = null;

                decimal costoContanti = 0, costoBuoni = 0;
                sqlite_conn.Open();

                SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                DateTime dataIniziale = DateTime.ParseExact(dataInizialePicker.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime dataFinale = DateTime.ParseExact(dataFinalePicker.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //Controlliamo che la data scelta sia minore uguale alla data finale oppure ad oggi
                while (dataIniziale.Ticks <= dataFinale.Ticks && dataIniziale.Ticks <= DateTime.Now.Ticks)
                {
                    
                    sqlite_cmd.CommandText = "SELECT vendita.id, vendita.costo, vendita.metodo, inventario.nome, inventario.codice, inventario.prezzo, inventario.indice FROM vendita, statistiche, inventario WHERE data='" + dataIniziale.ToString("dd/MM/yyyy") + "' AND vendita.id = statistiche.venditaID AND inventario.id = statistiche.libriID";
                    SQLiteDataReader r = sqlite_cmd.ExecuteReader();
                    while (r.Read())
                    {
                        
                        if (r.GetString(2).Equals("B"))
                        {
                            quantitaBuoni++;
                            metodo = "Buono"; //Metodo
                            if(!(r.GetInt32(0) == lastVenditaID))
                            {
                                costoBuoni += r.GetDecimal(1);
                                lastVenditaID= r.GetInt32(0);
                            }
                        }
                        else
                        {
                            quantitaContanti++;
                            metodo = "Contanti"; //Metodo
                            if (!(r.GetInt32(0) == lastVenditaID))
                            {
                                costoContanti += r.GetDecimal(1);
                                lastVenditaID = r.GetInt32(0);
                            }
                        }

                        ISBN = r.GetString(4); //ISBN
                        titolo = r.GetString(3); //Titolo
                        prezzo = r.GetString(5); //Prezzo
                        indiceString = r.GetString(6); //Indice

                        data = dataIniziale.ToString("dd/MM/yyyy"); //data

                        tabellaStatistiche.Rows.Add(data, ISBN, titolo, prezzo, metodo, indiceString);
                    }
                    r.Close();

                    //Aggiorniamo la data in modo da aggiungere un giorno
                    dataIniziale = dataIniziale.AddDays(1);
                }
                quantitaContantiLabel.Text = Convert.ToString(quantitaContanti);
                quantitaBuoniLabel.Text = Convert.ToString(quantitaBuoni);
                costoTotaleContantiLabel.Text = Convert.ToString(Math.Round(costoContanti, 2).ToString().Replace(",",".")).Replace(",", ".") + "€";
                costoTotaleBuoniLabel.Text = Convert.ToString(Math.Round(costoBuoni, 2).ToString().Replace(",", ".")).Replace(",", ".") + "€";

                tabellaStatistiche.ClearSelection();

                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                log.Error("Errore in Statistiche");
                log.Error("Messaggio: " + ex.Message + " Stacktrace: "+ ex.StackTrace);
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
            }
        }

        private void tabellaVendita_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if(!tabellaVendita.Rows[e.RowIndex].Cells[6].Value.ToString().Equals("") && tabellaVendita.Rows[e.RowIndex].Cells[6].Value != null)
            { 
                switch (Convert.ToInt32(tabellaVendita.Rows[e.RowIndex].Cells[6].Value.ToString()))
                {
                    case 1:
                        tabellaVendita.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                        break;
                    case 2:
                        tabellaVendita.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gold;
                        break;
                    case 3:
                        tabellaVendita.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
                        break;
                }
            }
        }

        
        //Inventario export
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            log.Info("Inizio Export");
            try
            {
                //Selezioniamo il path e nome da usare per il file di export
                string selectedPath = (string)e.Argument;

                //Creaiamo un nuovo excelPackage con using cosi che venga rimosso automaticamente
                using (var package = new ExcelPackage(new FileInfo(@selectedPath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventario");
                    //Prendiamo tutti i dati dalla dataTable dtRicerca e li carichiamo dentro la worksheet
                    worksheet.Cells["A1"].LoadFromDataTable(dtRicerca, false, TableStyles.None);
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    //Poiche' abbiamo gia' definito un nome per il file possiamo semplicemente chiamare save 
                    package.Save();
                }

                log.Info("Fine Export. File salvato: " + selectedPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " - " + ex.StackTrace);
                log.Error("Error in backgroundWorker3_DoWork - " + ex);
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void backgroundWorker3_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Visible = false;
            labelImporto.Visible = false;

            MessageBox.Show("Inventario Esportato!", "Export inventario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
