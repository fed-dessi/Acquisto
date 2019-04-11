using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace VenditaInventario
{
    public partial class Updater : Form
    {
        private string URL = null;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Updater()
        {
            InitializeComponent();
        }

        public string SetURL
        {
            set
            {
                URL = value;
            }
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            try
            {
                //Download Async cosi che si puo' aggiornare la barra
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += wc_DownloadCompleted;
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new Uri(URL),
                        // Param2 = Path to save
                        Path.GetDirectoryName(Application.ExecutablePath) + "\\Setup.exe"
                    );
                }
                URL = null;
            } catch (Exception ex)
            {
                MessageBox.Show("Updater_Load() Error", "Updater Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("Updater_Load() Error");
                log.Error("Messaggio: " + ex.Message + " Stacktrace: " + ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbDownload.Value = e.ProgressPercentage;
        }

        void wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Fine del download devo lanciare l'aggiornamento
            Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + "\\Setup.exe");
            Application.Exit();
        }
    }
}
