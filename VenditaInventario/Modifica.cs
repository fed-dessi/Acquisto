using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VenditaInventario
{
    public partial class Modifica : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string id = null;

        public Modifica()
        {
            InitializeComponent();
        }

        public string SetId
        {
            set
            {
                id = value;
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=inventario.sqlite;Version= 3;"))
                {
                    sqlite_conn.Open();

                    using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                    {
                        sqlite_cmd.CommandText = "UPDATE inventario SET nome = @Nome, autore = @Autore, casa = @Casa, codice = @Codice, prezzo = @Prezzo, anno = @Anno, indice = @Indice WHERE id='" + id + "'";

                        sqlite_cmd.Parameters.AddWithValue("@Nome", modificaTitoloTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Autore", modificaAutoreTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Casa", modificaCasaTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Codice", modificaIsbnTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Prezzo", modificaPrezzoTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Anno", modificaAnnoTextbox.Text);
                        sqlite_cmd.Parameters.AddWithValue("@Indice", modificaIndiceCombo.Text);
                        sqlite_cmd.ExecuteNonQuery();

                        sqlite_cmd.Dispose();
                    }


                    sqlite_conn.Close();
                }

                MessageBox.Show("Libro Modificato", "Modificato", MessageBoxButtons.OK, MessageBoxIcon.Information);

                id = null;

                Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("btnModifica_Click Error", "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
                log.Error(ex.StackTrace);
            }
        }
    }
}
