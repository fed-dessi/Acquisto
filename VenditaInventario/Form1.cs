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


        public Vendita()
        {
            InitializeComponent();
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

                    sqlite_cmd.CommandText = "CREATE TABLE inventario (id integer primary key autoincrement, nome varchar(300), autore varchar(50), casa varchar(50), codice varchar(50), prezzo varchar(50), anno varchar(50), indice varchar(4));";
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
        }

        private void inserimento(String isbn)
        {
            try
            { 
                if(isbn.Length == 13)
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
                else
                {
                    MessageBox.Show("Controlla il numero di cifre del codice", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(SQLiteException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void isbnVendita_KeyUp(object sender, KeyEventArgs e)
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
    }
}
