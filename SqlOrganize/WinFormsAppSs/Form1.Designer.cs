namespace WinFormsAppSs
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            aPELLIDODataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nOMBRESDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nDOCDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            fECHANACIMDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sujetoBindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sujetoBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { aPELLIDODataGridViewTextBoxColumn, nOMBRESDataGridViewTextBoxColumn, nDOCDataGridViewTextBoxColumn, fECHANACIMDataGridViewTextBoxColumn });
            dataGridView1.DataSource = sujetoBindingSource1;
            dataGridView1.Location = new Point(83, 112);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(566, 150);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // aPELLIDODataGridViewTextBoxColumn
            // 
            aPELLIDODataGridViewTextBoxColumn.DataPropertyName = "APELLIDO";
            aPELLIDODataGridViewTextBoxColumn.HeaderText = "APELLIDO";
            aPELLIDODataGridViewTextBoxColumn.Name = "aPELLIDODataGridViewTextBoxColumn";
            // 
            // nOMBRESDataGridViewTextBoxColumn
            // 
            nOMBRESDataGridViewTextBoxColumn.DataPropertyName = "NOMBRES";
            nOMBRESDataGridViewTextBoxColumn.HeaderText = "NOMBRES";
            nOMBRESDataGridViewTextBoxColumn.Name = "nOMBRESDataGridViewTextBoxColumn";
            // 
            // nDOCDataGridViewTextBoxColumn
            // 
            nDOCDataGridViewTextBoxColumn.DataPropertyName = "NDOC";
            nDOCDataGridViewTextBoxColumn.HeaderText = "NDOC";
            nDOCDataGridViewTextBoxColumn.Name = "nDOCDataGridViewTextBoxColumn";
            // 
            // fECHANACIMDataGridViewTextBoxColumn
            // 
            fECHANACIMDataGridViewTextBoxColumn.DataPropertyName = "FECHA_NACIM";
            fECHANACIMDataGridViewTextBoxColumn.HeaderText = "FECHA_NACIM";
            fECHANACIMDataGridViewTextBoxColumn.Name = "fECHANACIMDataGridViewTextBoxColumn";
            // 
            // sujetoBindingSource1
            // 
            sujetoBindingSource1.DataSource = typeof(Sujeto);
            sujetoBindingSource1.CurrentChanged += sujetoBindingSource1_CurrentChanged;
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
            ((System.ComponentModel.ISupportInitialize)sujetoBindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn aPELLIDODataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nOMBRESDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nDOCDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fECHANACIMDataGridViewTextBoxColumn;
        private BindingSource sujetoBindingSource1;
    }
}