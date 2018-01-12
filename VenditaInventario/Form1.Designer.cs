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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.venditaTab = new System.Windows.Forms.TabPage();
            this.backgroundVendita = new System.Windows.Forms.Panel();
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
            this.ricercaTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importaInventarioxlsmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chiudiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prezzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.venditaTab.SuspendLayout();
            this.backgroundVendita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaVendita)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.venditaTab);
            this.tabControl1.Controls.Add(this.ricercaTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1413, 742);
            this.tabControl1.TabIndex = 0;
            // 
            // venditaTab
            // 
            this.venditaTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.venditaTab.Controls.Add(this.backgroundVendita);
            this.venditaTab.Controls.Add(this.tabellaVendita);
            this.venditaTab.Location = new System.Drawing.Point(4, 22);
            this.venditaTab.Name = "venditaTab";
            this.venditaTab.Padding = new System.Windows.Forms.Padding(3);
            this.venditaTab.Size = new System.Drawing.Size(1405, 716);
            this.venditaTab.TabIndex = 0;
            this.venditaTab.Text = "Vendita";
            // 
            // backgroundVendita
            // 
            this.backgroundVendita.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.backgroundVendita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
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
            // 
            // importoMax
            // 
            this.importoMax.AutoSize = true;
            this.importoMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importoMax.ForeColor = System.Drawing.Color.Red;
            this.importoMax.Location = new System.Drawing.Point(90, 552);
            this.importoMax.Name = "importoMax";
            this.importoMax.Size = new System.Drawing.Size(0, 25);
            this.importoMax.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 491);
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
            this.libriTotLvl3.Location = new System.Drawing.Point(128, 433);
            this.libriTotLvl3.Name = "libriTotLvl3";
            this.libriTotLvl3.Size = new System.Drawing.Size(0, 18);
            this.libriTotLvl3.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(14, 433);
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
            this.libriTotLvl2.Location = new System.Drawing.Point(128, 372);
            this.libriTotLvl2.Name = "libriTotLvl2";
            this.libriTotLvl2.Size = new System.Drawing.Size(0, 18);
            this.libriTotLvl2.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(15, 372);
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
            this.libriTotLvl1.Location = new System.Drawing.Point(127, 312);
            this.libriTotLvl1.Name = "libriTotLvl1";
            this.libriTotLvl1.Size = new System.Drawing.Size(0, 18);
            this.libriTotLvl1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 312);
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
            this.isbnVendita.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isbnVendita_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Importo Acquisto";
            // 
            // tabellaVendita
            // 
            this.tabellaVendita.AllowUserToAddRows = false;
            this.tabellaVendita.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabellaVendita.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabellaVendita.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabellaVendita.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nome,
            this.autore,
            this.casa,
            this.codice,
            this.prezzo,
            this.anno,
            this.indice});
            this.tabellaVendita.Location = new System.Drawing.Point(423, 0);
            this.tabellaVendita.Name = "tabellaVendita";
            this.tabellaVendita.ReadOnly = true;
            this.tabellaVendita.RowHeadersVisible = false;
            this.tabellaVendita.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabellaVendita.Size = new System.Drawing.Size(986, 716);
            this.tabellaVendita.TabIndex = 0;
            // 
            // ricercaTab
            // 
            this.ricercaTab.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ricercaTab.Location = new System.Drawing.Point(4, 22);
            this.ricercaTab.Name = "ricercaTab";
            this.ricercaTab.Padding = new System.Windows.Forms.Padding(3);
            this.ricercaTab.Size = new System.Drawing.Size(1405, 716);
            this.ricercaTab.TabIndex = 1;
            this.ricercaTab.Text = "Ricerca";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1413, 24);
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
            // 
            // nome
            // 
            this.nome.HeaderText = "Nome";
            this.nome.Name = "nome";
            // 
            // autore
            // 
            this.autore.HeaderText = "Autore";
            this.autore.Name = "autore";
            // 
            // casa
            // 
            this.casa.HeaderText = "Casa";
            this.casa.Name = "casa";
            // 
            // codice
            // 
            this.codice.HeaderText = "Codice";
            this.codice.Name = "codice";
            // 
            // prezzo
            // 
            this.prezzo.HeaderText = "Prezzo";
            this.prezzo.Name = "prezzo";
            // 
            // anno
            // 
            this.anno.HeaderText = "Anno";
            this.anno.Name = "anno";
            // 
            // indice
            // 
            this.indice.HeaderText = "Indice";
            this.indice.Name = "indice";
            // 
            // Vendita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 766);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Vendita";
            this.Text = "Vendita";
            this.Load += new System.EventHandler(this.Vendita_Load);
            this.tabControl1.ResumeLayout(false);
            this.venditaTab.ResumeLayout(false);
            this.backgroundVendita.ResumeLayout(false);
            this.backgroundVendita.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabellaVendita)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
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
    }
}

