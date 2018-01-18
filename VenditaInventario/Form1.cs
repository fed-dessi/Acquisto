using ExcelDataReader;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VenditaInventario
{
    public partial class Vendita : Form
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;Version= 3;");

        DataTable dt = new DataTable();

        decimal totIndice1 = 0, totIndice2 = 0, totIndice3 = 0, totMax = 0;

        public Vendita()
        {
            InitializeComponent();

            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void Vendita_Load(object sender, EventArgs e)
        {
            String directory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            directory += "\\inventario.sqlite";


            if (!File.Exists(directory))
            {
                try
                {
                    sqlite_conn.Open();

                    SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

                    sqlite_cmd.CommandText = "CREATE TABLE inventario (id integer primary key, nome varchar(300), autore varchar(50), casa varchar(50), codice varchar(50), prezzo varchar(50), anno varchar(50), indice varchar(4));";
                    sqlite_cmd.ExecuteNonQuery();

                    sqlite_conn.Close();

                    MessageBox.Show("Nessun inventario trovato, ne è stato creato uno nuovo", "Inventario Creato!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                catch (SQLiteException ex)
                {
                    MessageBox.Show("Vendita_load Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex.StackTrace);
                }
            }

            populateTable();
        }

        public void populateTable()
        {
            try
            {

                SQLiteDataAdapter sqlite_adapter = new SQLiteDataAdapter("SELECT * FROM inventario", sqlite_conn);
                DataTable sqlite_table = new DataTable();
                sqlite_adapter.Fill(dt);

                tabellaRicerca.DataSource = dt;
                tabellaRicerca.ClearSelection();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void cancella()
        {
            libriTotLvl1.Text = "0.00€";
            libriTotLvl2.Text = "0.00€";
            libriTotLvl3.Text = "0.00€";
            importoMax.Text = "0.00€";
            tabellaVendita.Rows.Clear();

            totIndice1 = 0;
            totIndice2 = 0;
            totIndice3 = 0;
            totMax = 0;
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
                                    totIndice1 += Convert.ToDecimal(row[4]) * (decimal)0.1;
                                    totMax += Convert.ToDecimal(row[4]) * (decimal)0.1;
                                    //Imposto i valori dei label
                                    libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                                case 2:
                                    totIndice2 += Convert.ToDecimal(row[4]) * (decimal)0.2;
                                    totMax += Convert.ToDecimal(row[4]) * (decimal)0.2;
                                    //Imposto i valori dei label
                                    libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                                case 3:
                                    totIndice3 += Convert.ToDecimal(row[4]) * (decimal)0.3;
                                    totMax += Convert.ToDecimal(row[4]) * (decimal)0.3;
                                    //Imposto i valori dei label
                                    libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Libro non trovato/da non comprare!", "Libro non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sqlite_dataReader.Close();

                    sqlite_conn.Close();
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
                                    totIndice1 += (Convert.ToDecimal(row[4]) * (decimal)0.1);
                                    totMax += (Convert.ToDecimal(row[4]) * (decimal)0.1);
                                    //Imposto i valori dei label
                                    libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                                case 2:
                                    totIndice2 += (Convert.ToDecimal(row[4]) * (decimal)0.2);
                                    totMax += (Convert.ToDecimal(row[4]) * (decimal)0.2);
                                    //Imposto i valori dei label
                                    libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                                case 3:
                                    totIndice3 += (Convert.ToDecimal(row[4]) * (decimal)0.3);
                                    totMax += (Convert.ToDecimal(row[4]) * (decimal)0.3);
                                    //Imposto i valori dei label
                                    libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                                    importoMax.Text = Convert.ToString(totMax) + "€";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Libro non trovato/da non comprare!", "Libro non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sqlite_dataReader.Close();

                    sqlite_conn.Close();
                }
                else
                {
                    MessageBox.Show("Controlla il numero di cifre del codice", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void isbnVendita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                inserimento(isbnVendita.Text.Trim());
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

            foreach (DataColumn column in dt.DefaultView.Table.Columns)
            {
                sb.AppendFormat("CONVERT({0}, System.String) Like '%{1}%' OR ", column.ColumnName, query.Trim());
            }

            //Rimuovo gli ultimi tre caratteri cosi rimuovo le lettere 'OR ' e il RowFilter accetta la stringa
            sb.Remove(sb.Length - 3, 3);

            dt.DefaultView.RowFilter = sb.ToString();

            tabellaRicerca.DataSource = dt;


        }

        private void textboxRicerca_TextChanged(object sender, EventArgs e)
        {
            ricerca(textboxRicerca.Text);
        }

        private void tabellaRicerca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string idRicerca = tabellaRicerca.Rows[e.RowIndex].Cells[0].Value.ToString();

            inserimento(idRicerca);

            tabControl1.TabPages[0].Show();
            tabControl1.SelectedIndex = 0;

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

                tabControl1.TabPages[0].Show();
                tabControl1.SelectedIndex = 0;

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
            

            DataTable excelTable = new DataTable();

            string filePath = (string)e.Argument;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {

                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet result = reader.AsDataSet();
                    excelTable = result.Tables[0];
                }

            }

            String nome = null, autore = null, casa = null, codice = null, prezzo = null, anno = null, indice = null;

            sqlite_conn.Open();

            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            sqlite_cmd.CommandText = "DELETE FROM inventario";

            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = "INSERT INTO inventario (nome, autore, casa, codice, prezzo, anno, indice) Values (@Nome, @Autore, @Casa, @Codice, @Prezzo, @Anno, @Indice)";

            //Abbiamo ora una dataTable (excelTable) che possiamo utilizzare per carpire i valori come se fosse una matrice.
            //Usiamo due for Loops per scorrere prima le colonne (for j) e poi le righe (for i)
            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                for (int j = 0; j < excelTable.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            nome = excelTable.Rows[i][j].ToString();
                            break;
                        case 1:
                            autore = excelTable.Rows[i][j].ToString();
                            break;
                        case 2:
                            casa = excelTable.Rows[i][j].ToString();
                            break;
                        case 3:
                            codice = excelTable.Rows[i][j].ToString();
                            break;
                        case 4:
                            prezzo = excelTable.Rows[i][j].ToString();
                            break;
                        case 5:
                            anno = excelTable.Rows[i][j].ToString();
                            break;
                        case 6:
                            indice = excelTable.Rows[i][j].ToString();
                            break;
                    }
                }
                sqlite_cmd.Parameters.AddWithValue("@Nome", nome);
                sqlite_cmd.Parameters.AddWithValue("@Autore", autore);
                sqlite_cmd.Parameters.AddWithValue("@Casa", casa);
                sqlite_cmd.Parameters.AddWithValue("@Codice", codice);
                sqlite_cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                sqlite_cmd.Parameters.AddWithValue("@Anno", anno);
                sqlite_cmd.Parameters.AddWithValue("@Indice", indice);

                sqlite_cmd.ExecuteNonQuery();

                GC.Collect();
                
                backgroundWorker1.ReportProgress((int)Math.Round((double)(i * 100) / excelTable.Rows.Count));
            }

            sqlite_conn.Close();

            populateTable();
            

            
        }

        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnNuovoCliente_Click(object sender, EventArgs e)
        {
            cancella();
        }

        private void btnCancella_Click(object sender, EventArgs e)
        {
            //Poiche rimuovo la riga dalla tabella allora scompare anche dalle righe selezionate
            //per riuscire a continuare il loop sul resto delle righe il mio for deve partire 'dall'alto'
            for (int i = tabellaVendita.SelectedRows.Count -1; i >= 0; i--)
            {
                if (!tabellaVendita.SelectedRows[i].Cells[6].Value.ToString().Equals("") && tabellaVendita.SelectedRows[i].Cells[6].Value.ToString() != null)
                { 
                    //Controllo che indice sto cancellando cosi rimuovo il libro dal totale
                    //bisogna usare lo switch prima poiche' altrimenti cancellerei i dati prima di usarli
                    switch (Convert.ToInt32(tabellaVendita.SelectedRows[i].Cells[6].Value.ToString()))
                    {
                        case 1:
                            totIndice1 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.1);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.1);
                            //Imposto i valori dei label
                            libriTotLvl1.Text = Convert.ToString(totIndice1) + "€";
                            importoMax.Text = Convert.ToString(totMax) + "€";
                            break;
                        case 2:
                            totIndice2 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.2);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.2);
                            //Imposto i valori dei label
                            libriTotLvl2.Text = Convert.ToString(totIndice2) + "€";
                            importoMax.Text = Convert.ToString(totMax) + "€";
                            break;
                        case 3:
                            totIndice3 -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.3);
                            totMax -= (Convert.ToDecimal(tabellaVendita.SelectedRows[i].Cells[4].Value.ToString()) * (decimal)0.3);
                            //Imposto i valori dei label
                            libriTotLvl3.Text = Convert.ToString(totIndice3) + "€";
                            importoMax.Text = Convert.ToString(totMax) + "€";
                            break;
                    }
                }
                tabellaVendita.Rows.RemoveAt(tabellaVendita.SelectedRows[i].Index);
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

        private void importaInventarioxlsmToolStripMenuItem_Click(object sender, EventArgs e)
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
        }
    }
}
