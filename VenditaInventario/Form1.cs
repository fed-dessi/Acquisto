using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VenditaInventario
{
    public partial class Vendita : Form
    {
        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;Version= 3;");

        DataTable dt = new DataTable();

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
                    }
                    else
                    {
                        MessageBox.Show("Libro non trovato/da non comprare!", "Libro non Trovato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sqlite_dataReader.Close();

                    sqlite_conn.Close();
                }
                else if (isbn.Length < 13)
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
                

                backgroundWorker1.ReportProgress((int)Math.Round((double)(i * 100) / excelTable.Rows.Count));
            }

            sqlite_conn.Close();

            populateTable();
            

            
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

        
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelProgressbar.Text = "Importo..: " + Convert.ToString(e.ProgressPercentage) + "%";
            //La percentuale e' una proprieta di e
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Foglio Excel importato!", "Importato", MessageBoxButtons.OK, MessageBoxIcon.Information);

            progressBar1.Visible = false;

            labelProgressbar.ResetText();
        }
    }
}
