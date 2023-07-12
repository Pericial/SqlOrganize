namespace WinFormsAppMy.Forms.InformeCoordinacionDistrital
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            informeCoordinacionDistritalBindingSource = new BindingSource(components);
            personanombresDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            personaapellidosDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)informeCoordinacionDistritalBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { personanombresDataGridViewTextBoxColumn, personaapellidosDataGridViewTextBoxColumn });
            dataGridView1.DataSource = informeCoordinacionDistritalBindingSource;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(800, 450);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // informeCoordinacionDistritalBindingSource
            // 
            informeCoordinacionDistritalBindingSource.DataSource = typeof(InformeCoordinacionDistrital);
            // 
            // personanombresDataGridViewTextBoxColumn
            // 
            personanombresDataGridViewTextBoxColumn.DataPropertyName = "persona__nombres";
            personanombresDataGridViewTextBoxColumn.HeaderText = "persona__nombres";
            personanombresDataGridViewTextBoxColumn.Name = "personanombresDataGridViewTextBoxColumn";
            // 
            // personaapellidosDataGridViewTextBoxColumn
            // 
            personaapellidosDataGridViewTextBoxColumn.DataPropertyName = "persona__apellidos";
            personaapellidosDataGridViewTextBoxColumn.HeaderText = "persona__apellidos";
            personaapellidosDataGridViewTextBoxColumn.Name = "personaapellidosDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)informeCoordinacionDistritalBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn personanombresDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn personaapellidosDataGridViewTextBoxColumn;
        private BindingSource informeCoordinacionDistritalBindingSource;
    }
}