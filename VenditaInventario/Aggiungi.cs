using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace VenditaInventario
{
    public partial class Aggiungi : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Aggiungi()
        {
            InitializeComponent();
        }

        private void btnAggiungi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(aggiungiIsbnTextbox.Text) && !string.IsNullOrEmpty(aggiungiIsbnTextbox.Text) && !string.IsNullOrWhiteSpace(aggiungiPrezzoTextbox.Text) && !string.IsNullOrEmpty(aggiungiPrezzoTextbox.Text) && !string.IsNullOrWhiteSpace(aggiungiTitoloTextbox.Text) && !string.IsNullOrEmpty(aggiungiTitoloTextbox.Text))
                {
                    using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;foreign keys=true;Version= 3;"))
                    {
                        sqlite_conn.Open();

                        using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                        {

                            sqlite_cmd.CommandText = "INSERT INTO inventario (nome, autore, casa, codice, prezzo, anno, indice) Values (@Nome, @Autore, @Casa, @Codice, @Prezzo, @Anno, @Indice)";

                            sqlite_cmd.Parameters.AddWithValue("@Nome", aggiungiTitoloTextbox.Text.ToUpper());
                            sqlite_cmd.Parameters.AddWithValue("@Autore", aggiungiAutoreTextbox.Text.ToUpper());
                            sqlite_cmd.Parameters.AddWithValue("@Casa", aggiungiCasaTextbox.Text.ToUpper());
                            sqlite_cmd.Parameters.AddWithValue("@Codice", aggiungiIsbnTextbox.Text);
                            sqlite_cmd.Parameters.AddWithValue("@Prezzo", aggiungiPrezzoTextbox.Text);
                            sqlite_cmd.Parameters.AddWithValue("@Anno", aggiungiAnnoTextbox.Text);
                            sqlite_cmd.Parameters.AddWithValue("@Indice", aggiungiIndiceCombo.Text);

                            sqlite_cmd.ExecuteNonQuery();

                            sqlite_cmd.Dispose();
                        }

                        sqlite_conn.Close();
                    }

                    MessageBox.Show("Libro Aggiunto!", "Aggiunto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    Close();
                }
                else
                {
                    MessageBox.Show("Controlla i valori. Sono necessari almeno:\nTitolo\nISBN\nPrezzo", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } catch(Exception ex)
            {
                MessageBox.Show("btnAggiungi_Click Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
            }
        }
    }
}
