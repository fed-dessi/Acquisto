namespace VenditaInventario
{
    partial class Vendita
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vendita));
            this.tabPages = new System.Windows.Forms.TabControl();
            this.venditaTab = new System.Windows.Forms.TabPage();
            this.backgroundVendita = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.importoLordo = new System.Windows.Forms.Label();
            this.btnNuovoCliente = new System.Windows.Forms.Button();
            this.labelProgressbar = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancella = new System.Windows.Forms.Button();
            this.importoMax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.libriTotLvl3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.libriTotLvl2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.libriTotLvl1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInserisci = new System.Windows.Forms.Button();
            this.isbnVendita = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabellaVendita = new System.Windows.Forms.DataGridView();
            this.nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prezzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ricercaTab = new System.Windows.Forms.TabPage();
            this.btnAggiornaRicerca = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabellaRicerca = new System.Windows.Forms.DataGridView();
            this.textboxRicerca = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.quantitaLabel = new System.Windows.Forms.Label();
            this.ricercaStatistiche = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataFinalePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dataInizialePicker = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importaInventarioxlsmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPages.SuspendLayout();
            this.venditaTab.SuspendLayout();
            this.backgroundVendita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaVendita)).BeginInit();
            this.ricercaTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaRicerca)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.venditaTab);
            this.tabPages.Controls.Add(this.ricercaTab);
            this.tabPages.Controls.Add(this.tabPage1);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 24);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1381, 718);
            this.tabPages.TabIndex = 0;
            // 
            // venditaTab
            // 
            this.venditaTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.venditaTab.Controls.Add(this.backgroundVendita);
            this.venditaTab.Controls.Add(this.tabellaVendita);
            this.venditaTab.Location = new System.Drawing.Point(4, 22);
            this.venditaTab.Name = "venditaTab";
            this.venditaTab.Padding = new System.Windows.Forms.Padding(3);
            this.venditaTab.Size = new System.Drawing.Size(1373, 692);
            this.venditaTab.TabIndex = 0;
            this.venditaTab.Text = "Vendita";
            // 
            // backgroundVendita
            // 
            this.backgroundVendita.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.backgroundVendita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.backgroundVendita.Controls.Add(this.label8);
            this.backgroundVendita.Controls.Add(this.importoLordo);
            this.backgroundVendita.Controls.Add(this.btnNuovoCliente);
            this.backgroundVendita.Controls.Add(this.labelProgressbar);
            this.backgroundVendita.Controls.Add(this.progressBar1);
            this.backgroundVendita.Controls.Add(this.btnCancella);
            this.backgroundVendita.Controls.Add(this.importoMax);
            this.backgroundVendita.Controls.Add(this.label4);
            this.backgroundVendita.Controls.Add(this.libriTotLvl3);
            this.backgroundVendita.Controls.Add(this.label7);
            this.backgroundVendita.Controls.Add(this.libriTotLvl2);
            this.backgroundVendita.Controls.Add(this.label5);
            this.backgroundVendita.Controls.Add(this.libriTotLvl1);
            this.backgroundVendita.Controls.Add(this.label3);
            this.backgroundVendita.Controls.Add(this.label1);
            this.backgroundVendita.Controls.Add(this.btnInserisci);
            this.backgroundVendita.Controls.Add(this.isbnVendita);
            this.backgroundVendita.Controls.Add(this.label2);
            this.backgroundVendita.Location = new System.Drawing.Point(-4, 0);
            this.backgroundVendita.Name = "backgroundVendita";
            this.backgroundVendita.Size = new System.Drawing.Size(430, 716);
            this.backgroundVendita.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(319, 525);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 18;
            this.label8.Text = "Lordo";
            // 
            // importoLordo
            // 
            this.importoLordo.AutoSize = true;
            this.importoLordo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importoLordo.ForeColor = System.Drawing.Color.Red;
            this.importoLordo.Location = new System.Drawing.Point(318, 559);
            this.importoLordo.Name = "importoLordo";
            this.importoLordo.Size = new System.Drawing.Size(71, 25);
            this.importoLordo.TabIndex = 17;
            this.importoLordo.Text = "0.00€";
            // 
            // btnNuovoCliente
            // 
            this.btnNuovoCliente.BackColor = System.Drawing.Color.ForestGreen;
            this.btnNuovoCliente.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnNuovoCliente.FlatAppearance.BorderSize = 0;
            this.btnNuovoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuovoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuovoCliente.ForeColor = System.Drawing.Color.White;
            this.btnNuovoCliente.Location = new System.Drawing.Point(116, 214);
            this.btnNuovoCliente.Name = "btnNuovoCliente";
            this.btnNuovoCliente.Size = new System.Drawing.Size(163, 62);
            this.btnNuovoCliente.TabIndex = 16;
            this.btnNuovoCliente.Text = "Nuovo Cliente";
            this.btnNuovoCliente.UseVisualStyleBackColor = false;
            this.btnNuovoCliente.Click += new System.EventHandler(this.btnNuovoCliente_Click);
            // 
            // labelProgressbar
            // 
            this.labelProgressbar.AutoSize = true;
            this.labelProgressbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgressbar.ForeColor = System.Drawing.Color.White;
            this.labelProgressbar.Location = new System.Drawing.Point(127, 624);
            this.labelProgressbar.Name = "labelProgressbar";
            this.labelProgressbar.Size = new System.Drawing.Size(0, 24);
            this.labelProgressbar.TabIndex = 15;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(28, 651);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(347, 23);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Visible = false;
            // 
            // btnCancella
            // 
            this.btnCancella.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancella.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnCancella.FlatAppearance.BorderSize = 0;
            this.btnCancella.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancella.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancella.ForeColor = System.Drawing.Color.White;
            this.btnCancella.Location = new System.Drawing.Point(226, 124);
            this.btnCancella.Name = "btnCancella";
            this.btnCancella.Size = new System.Drawing.Size(163, 62);
            this.btnCancella.TabIndex = 13;
            this.btnCancella.Text = "Cancella";
            this.btnCancella.UseVisualStyleBackColor = false;
            this.btnCancella.Click += new System.EventHandler(this.btnCancella_Click);
            // 
            // importoMax
            // 
            this.importoMax.AutoSize = true;
            this.importoMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importoMax.ForeColor = System.Drawing.Color.Red;
            this.importoMax.Location = new System.Drawing.Point(83, 559);
            this.importoMax.Name = "importoMax";
            this.importoMax.Size = new System.Drawing.Size(71, 25);
            this.importoMax.TabIndex = 12;
            this.importoMax.Text = "0.00€";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(10, 525);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Importo Massimo da Pagare";
            // 
            // libriTotLvl3
            // 
            this.libriTotLvl3.AutoSize = true;
            this.libriTotLvl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.libriTotLvl3.ForeColor = System.Drawing.Color.Lime;
            this.libriTotLvl3.Location = new System.Drawing.Point(125, 467);
            this.libriTotLvl3.Name = "libriTotLvl3";
            this.libriTotLvl3.Size = new System.Drawing.Size(49, 18);
            this.libriTotLvl3.TabIndex = 10;
            this.libriTotLvl3.Text = "0.00€";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(11, 467);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Livello 3:";
            // 
            // libriTotLvl2
            // 
            this.libriTotLvl2.AutoSize = true;
            this.libriTotLvl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.libriTotLvl2.ForeColor = System.Drawing.Color.Gold;
            this.libriTotLvl2.Location = new System.Drawing.Point(125, 406);
            this.libriTotLvl2.Name = "libriTotLvl2";
            this.libriTotLvl2.Size = new System.Drawing.Size(49, 18);
            this.libriTotLvl2.TabIndex = 8;
            this.libriTotLvl2.Text = "0.00€";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Livello 2:";
            // 
            // libriTotLvl1
            // 
            this.libriTotLvl1.AutoSize = true;
            this.libriTotLvl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.libriTotLvl1.ForeColor = System.Drawing.Color.OrangeRed;
            this.libriTotLvl1.Location = new System.Drawing.Point(124, 346);
            this.libriTotLvl1.Name = "libriTotLvl1";
            this.libriTotLvl1.Size = new System.Drawing.Size(49, 18);
            this.libriTotLvl1.TabIndex = 6;
            this.libriTotLvl1.Text = "0.00€";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Livello 1:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "ISBN:";
            // 
            // btnInserisci
            // 
            this.btnInserisci.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnInserisci.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnInserisci.FlatAppearance.BorderSize = 0;
            this.btnInserisci.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInserisci.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserisci.ForeColor = System.Drawing.Color.White;
            this.btnInserisci.Location = new System.Drawing.Point(18, 124);
            this.btnInserisci.Margin = new System.Windows.Forms.Padding(0);
            this.btnInserisci.Name = "btnInserisci";
            this.btnInserisci.Size = new System.Drawing.Size(163, 62);
            this.btnInserisci.TabIndex = 4;
            this.btnInserisci.Text = "Inserisci";
            this.btnInserisci.UseVisualStyleBackColor = false;
            this.btnInserisci.Click += new System.EventHandler(this.btnInserisci_Click);
            // 
            // isbnVendita
            // 
            this.isbnVendita.Location = new System.Drawing.Point(18, 66);
            this.isbnVendita.Name = "isbnVendita";
            this.isbnVendita.Size = new System.Drawing.Size(264, 20);
            this.isbnVendita.TabIndex = 2;
            this.isbnVendita.KeyUp += new System.Windows.Forms.KeyEventHandler(this.IsbnVendita_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Importo dai vari Livelli:";
            // 
            // tabellaVendita
            // 
            this.tabellaVendita.AllowUserToAddRows = false;
            this.tabellaVendita.AllowUserToDeleteRows = false;
            this.tabellaVendita.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabellaVendita.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tabellaVendita.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tabellaVendita.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabellaVendita.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nome,
            this.autore,
            this.casa,
            this.codice,
            this.prezzo,
            this.anno,
            this.indice});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tabellaVendita.DefaultCellStyle = dataGridViewCellStyle2;
            this.tabellaVendita.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tabellaVendita.Location = new System.Drawing.Point(423, 0);
            this.tabellaVendita.Name = "tabellaVendita";
            this.tabellaVendita.RowHeadersVisible = false;
            this.tabellaVendita.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabellaVendita.Size = new System.Drawing.Size(954, 696);
            this.tabellaVendita.TabIndex = 0;
            this.tabellaVendita.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.tabellaVendita_RowsAdded);
            // 
            // nome
            // 
            this.nome.HeaderText = "Nome";
            this.nome.Name = "nome";
            this.nome.ReadOnly = true;
            // 
            // autore
            // 
            this.autore.HeaderText = "Autore";
            this.autore.Name = "autore";
            this.autore.ReadOnly = true;
            // 
            // casa
            // 
            this.casa.HeaderText = "Casa";
            this.casa.Name = "casa";
            this.casa.ReadOnly = true;
            // 
            // codice
            // 
            this.codice.HeaderText = "Codice";
            this.codice.Name = "codice";
            this.codice.ReadOnly = true;
            // 
            // prezzo
            // 
            this.prezzo.HeaderText = "Prezzo";
            this.prezzo.Name = "prezzo";
            this.prezzo.ReadOnly = true;
            // 
            // anno
            // 
            this.anno.HeaderText = "Anno";
            this.anno.Name = "anno";
            this.anno.ReadOnly = true;
            // 
            // indice
            // 
            this.indice.HeaderText = "Indice";
            this.indice.Name = "indice";
            this.indice.ReadOnly = true;
            // 
            // ricercaTab
            // 
            this.ricercaTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ricercaTab.Controls.Add(this.btnAggiornaRicerca);
            this.ricercaTab.Controls.Add(this.button1);
            this.ricercaTab.Controls.Add(this.label6);
            this.ricercaTab.Controls.Add(this.tabellaRicerca);
            this.ricercaTab.Controls.Add(this.textboxRicerca);
            this.ricercaTab.Location = new System.Drawing.Point(4, 22);
            this.ricercaTab.Name = "ricercaTab";
            this.ricercaTab.Padding = new System.Windows.Forms.Padding(3);
            this.ricercaTab.Size = new System.Drawing.Size(1373, 692);
            this.ricercaTab.TabIndex = 1;
            this.ricercaTab.Text = "Ricerca";
            // 
            // btnAggiornaRicerca
            // 
            this.btnAggiornaRicerca.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAggiornaRicerca.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.btnAggiornaRicerca.FlatAppearance.BorderSize = 0;
            this.btnAggiornaRicerca.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAggiornaRicerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAggiornaRicerca.ForeColor = System.Drawing.Color.White;
            this.btnAggiornaRicerca.Location = new System.Drawing.Point(1017, 25);
            this.btnAggiornaRicerca.Name = "btnAggiornaRicerca";
            this.btnAggiornaRicerca.Size = new System.Drawing.Size(163, 62);
            this.btnAggiornaRicerca.TabIndex = 18;
            this.btnAggiornaRicerca.Text = "Aggiorna Tabella";
            this.btnAggiornaRicerca.UseVisualStyleBackColor = false;
            this.btnAggiornaRicerca.Click += new System.EventHandler(this.btnAggiornaRicerca_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Goldenrod;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(809, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 62);
            this.button1.TabIndex = 17;
            this.button1.Text = "Modifica Riga";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(321, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Ricerca testo:";
            // 
            // tabellaRicerca
            // 
            this.tabellaRicerca.AllowUserToAddRows = false;
            this.tabellaRicerca.AllowUserToDeleteRows = false;
            this.tabellaRicerca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabellaRicerca.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabellaRicerca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabellaRicerca.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tabellaRicerca.Location = new System.Drawing.Point(0, 110);
            this.tabellaRicerca.Name = "tabellaRicerca";
            this.tabellaRicerca.RowHeadersVisible = false;
            this.tabellaRicerca.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabellaRicerca.Size = new System.Drawing.Size(1377, 586);
            this.tabellaRicerca.TabIndex = 1;
            this.tabellaRicerca.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabellaRicerca_CellDoubleClick);
            this.tabellaRicerca.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tabellaRicerca_PreviewKeyDown);
            // 
            // textboxRicerca
            // 
            this.textboxRicerca.Location = new System.Drawing.Point(406, 49);
            this.textboxRicerca.Name = "textboxRicerca";
            this.textboxRicerca.Size = new System.Drawing.Size(254, 20);
            this.textboxRicerca.TabIndex = 0;
            this.textboxRicerca.TextChanged += new System.EventHandler(this.textboxRicerca_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1373, 692);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Statistiche";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.quantitaLabel);
            this.panel1.Controls.Add(this.ricercaStatistiche);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.dataFinalePicker);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.dataInizialePicker);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1372, 695);
            this.panel1.TabIndex = 0;
            // 
            // quantitaLabel
            // 
            this.quantitaLabel.AutoSize = true;
            this.quantitaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantitaLabel.ForeColor = System.Drawing.Color.Red;
            this.quantitaLabel.Location = new System.Drawing.Point(197, 163);
            this.quantitaLabel.Name = "quantitaLabel";
            this.quantitaLabel.Size = new System.Drawing.Size(0, 24);
            this.quantitaLabel.TabIndex = 7;
            // 
            // ricercaStatistiche
            // 
            this.ricercaStatistiche.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ricercaStatistiche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ricercaStatistiche.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.ricercaStatistiche.ForeColor = System.Drawing.Color.White;
            this.ricercaStatistiche.Location = new System.Drawing.Point(724, 51);
            this.ricercaStatistiche.Name = "ricercaStatistiche";
            this.ricercaStatistiche.Size = new System.Drawing.Size(229, 78);
            this.ricercaStatistiche.TabIndex = 6;
            this.ricercaStatistiche.Text = "Ricerca";
            this.ricercaStatistiche.UseVisualStyleBackColor = false;
            this.ricercaStatistiche.Click += new System.EventHandler(this.button2_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(66, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(340, 25);
            this.label12.TabIndex = 5;
            this.label12.Text = "Seleziona le date di riferimento";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(395, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Fine:";
            // 
            // dataFinalePicker
            // 
            this.dataFinalePicker.CustomFormat = "dd/MM/yyyy";
            this.dataFinalePicker.Location = new System.Drawing.Point(458, 78);
            this.dataFinalePicker.Name = "dataFinalePicker";
            this.dataFinalePicker.Size = new System.Drawing.Size(200, 20);
            this.dataFinalePicker.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(67, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "Inizio:";
            // 
            // dataInizialePicker
            // 
            this.dataInizialePicker.CustomFormat = "dd/MM/yyyy";
            this.dataInizialePicker.Location = new System.Drawing.Point(130, 79);
            this.dataInizialePicker.Name = "dataInizialePicker";
            this.dataInizialePicker.Size = new System.Drawing.Size(200, 20);
            this.dataInizialePicker.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(67, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Libri venduti:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1381, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importaInventarioxlsmToolStripMenuItem,
            this.toolStripSeparator1,
            this.chiudiToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importaInventarioxlsmToolStripMenuItem
            // 
            this.importaInventarioxlsmToolStripMenuItem.Name = "importaInventarioxlsmToolStripMenuItem";
            this.importaInventarioxlsmToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.importaInventarioxlsmToolStripMenuItem.Text = "Importa inventario (.xlsm)";
            this.importaInventarioxlsmToolStripMenuItem.Click += new System.EventHandler(this.importaInventarioxlsmToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // chiudiToolStripMenuItem
            // 
            this.chiudiToolStripMenuItem.Name = "chiudiToolStripMenuItem";
            this.chiudiToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.chiudiToolStripMenuItem.Text = "Chiudi";
            this.chiudiToolStripMenuItem.Click += new System.EventHandler(this.chiudiToolStripMenuItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Vendita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 742);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Vendita";
            this.Text = "Vendita";
            this.Load += new System.EventHandler(this.Vendita_Load);
            this.tabPages.ResumeLayout(false);
            this.venditaTab.ResumeLayout(false);
            this.backgroundVendita.ResumeLayout(false);
            this.backgroundVendita.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaVendita)).EndInit();
            this.ricercaTab.ResumeLayout(false);
            this.ricercaTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaRicerca)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage venditaTab;
        private System.Windows.Forms.TabPage ricercaTab;
        private System.Windows.Forms.Panel backgroundVendita;
        private System.Windows.Forms.Button btnCancella;
        private System.Windows.Forms.Label importoMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label libriTotLvl3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label libriTotLvl2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label libriTotLvl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInserisci;
        private System.Windows.Forms.TextBox isbnVendita;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView tabellaVendita;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importaInventarioxlsmToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem chiudiToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn autore;
        private System.Windows.Forms.DataGridViewTextBoxColumn casa;
        private System.Windows.Forms.DataGridViewTextBoxColumn codice;
        private System.Windows.Forms.DataGridViewTextBoxColumn prezzo;
        private System.Windows.Forms.DataGridViewTextBoxColumn anno;
        private System.Windows.Forms.DataGridViewTextBoxColumn indice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView tabellaRicerca;
        private System.Windows.Forms.TextBox textboxRicerca;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelProgressbar;
        private System.Windows.Forms.Button btnNuovoCliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label importoLordo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAggiornaRicerca;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ricercaStatistiche;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dataFinalePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dataInizialePicker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label quantitaLabel;
    }
}

