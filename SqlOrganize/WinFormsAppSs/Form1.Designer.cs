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
            dataGridViewDTOSJUDI = new DataGridView();
            dTOJUDDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dESCRIPCIONDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cantidad_personal = new DataGridViewTextBoxColumn();
            form1DTOSJUDIBindingSource = new BindingSource(components);
            form1DTOSJUDIBindingSource1 = new BindingSource(components);
            dataGridViewPERSONAL = new DataGridView();
            nOMBRESDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            APELLIDO = new DataGridViewTextBoxColumn();
            TIPODOC = new DataGridViewTextBoxColumn();
            NRODOC = new DataGridViewTextBoxColumn();
            NROIDPEMP = new DataGridViewTextBoxColumn();
            _Id = new DataGridViewTextBoxColumn();
            personalBindingSource = new BindingSource(components);
            form1PERSONALBindingSource = new BindingSource(components);
            buttonSave = new Button();
            personalBindingSource1 = new BindingSource(components);
            linkLabel1 = new LinkLabel();
            menuStrip1 = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDTOSJUDI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)form1DTOSJUDIBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)form1DTOSJUDIBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPERSONAL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)personalBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)form1PERSONALBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)personalBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewDTOSJUDI
            // 
            dataGridViewDTOSJUDI.AllowUserToOrderColumns = true;
            dataGridViewDTOSJUDI.AutoGenerateColumns = false;
            dataGridViewDTOSJUDI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDTOSJUDI.Columns.AddRange(new DataGridViewColumn[] { dTOJUDDataGridViewTextBoxColumn, dESCRIPCIONDataGridViewTextBoxColumn, cantidad_personal });
            dataGridViewDTOSJUDI.DataSource = form1DTOSJUDIBindingSource;
            dataGridViewDTOSJUDI.Location = new Point(12, 31);
            dataGridViewDTOSJUDI.Name = "dataGridViewDTOSJUDI";
            dataGridViewDTOSJUDI.RowTemplate.Height = 25;
            dataGridViewDTOSJUDI.Size = new Size(389, 349);
            dataGridViewDTOSJUDI.TabIndex = 0;
            dataGridViewDTOSJUDI.CellContentClick += dataGridViewDTOSJUDI_CellContentClick;
            dataGridViewDTOSJUDI.SelectionChanged += dataGridViewDTOSJUDI_SelectionChanged;
            // 
            // dTOJUDDataGridViewTextBoxColumn
            // 
            dTOJUDDataGridViewTextBoxColumn.DataPropertyName = "DTOJUD";
            dTOJUDDataGridViewTextBoxColumn.HeaderText = "DTOJUD";
            dTOJUDDataGridViewTextBoxColumn.Name = "dTOJUDDataGridViewTextBoxColumn";
            // 
            // dESCRIPCIONDataGridViewTextBoxColumn
            // 
            dESCRIPCIONDataGridViewTextBoxColumn.DataPropertyName = "DESCRIPCION";
            dESCRIPCIONDataGridViewTextBoxColumn.HeaderText = "DESCRIPCION";
            dESCRIPCIONDataGridViewTextBoxColumn.Name = "dESCRIPCIONDataGridViewTextBoxColumn";
            // 
            // cantidad_personal
            // 
            cantidad_personal.DataPropertyName = "cantidad_personal";
            cantidad_personal.HeaderText = "cantidad_personal";
            cantidad_personal.Name = "cantidad_personal";
            // 
            // form1DTOSJUDIBindingSource
            // 
            form1DTOSJUDIBindingSource.DataSource = typeof(Form1_DTOSJUDI);
            // 
            // form1DTOSJUDIBindingSource1
            // 
            form1DTOSJUDIBindingSource1.DataSource = typeof(Form1_DTOSJUDI);
            // 
            // dataGridViewPERSONAL
            // 
            dataGridViewPERSONAL.AllowUserToOrderColumns = true;
            dataGridViewPERSONAL.AutoGenerateColumns = false;
            dataGridViewPERSONAL.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPERSONAL.Columns.AddRange(new DataGridViewColumn[] { nOMBRESDataGridViewTextBoxColumn, APELLIDO, TIPODOC, NRODOC, NROIDPEMP, _Id });
            dataGridViewPERSONAL.DataSource = personalBindingSource;
            dataGridViewPERSONAL.Location = new Point(407, 31);
            dataGridViewPERSONAL.Name = "dataGridViewPERSONAL";
            dataGridViewPERSONAL.RowTemplate.Height = 25;
            dataGridViewPERSONAL.Size = new Size(555, 349);
            dataGridViewPERSONAL.TabIndex = 1;
            dataGridViewPERSONAL.CellContentClick += dataGridViewPERSONAL_CellContentClick;
            dataGridViewPERSONAL.CellEndEdit += dataGridViewPERSONAL_CellEndEdit;
            // 
            // nOMBRESDataGridViewTextBoxColumn
            // 
            nOMBRESDataGridViewTextBoxColumn.DataPropertyName = "NOMBRES";
            nOMBRESDataGridViewTextBoxColumn.HeaderText = "NOMBRES";
            nOMBRESDataGridViewTextBoxColumn.Name = "nOMBRESDataGridViewTextBoxColumn";
            // 
            // APELLIDO
            // 
            APELLIDO.DataPropertyName = "APELLIDO";
            APELLIDO.HeaderText = "APELLIDO";
            APELLIDO.Name = "APELLIDO";
            // 
            // TIPODOC
            // 
            TIPODOC.DataPropertyName = "TIPODOC";
            TIPODOC.HeaderText = "TIPODOC";
            TIPODOC.Name = "TIPODOC";
            // 
            // NRODOC
            // 
            NRODOC.DataPropertyName = "NRODOC";
            NRODOC.HeaderText = "NRODOC";
            NRODOC.Name = "NRODOC";
            // 
            // NROIDPEMP
            // 
            NROIDPEMP.DataPropertyName = "NROIDPEMP";
            NROIDPEMP.HeaderText = "NROIDPEMP";
            NROIDPEMP.Name = "NROIDPEMP";
            // 
            // _Id
            // 
            _Id.DataPropertyName = "_Id";
            _Id.HeaderText = "_Id";
            _Id.Name = "_Id";
            _Id.ReadOnly = true;
            // 
            // personalBindingSource
            // 
            personalBindingSource.DataMember = "Personal";
            personalBindingSource.DataSource = form1DTOSJUDIBindingSource;
            // 
            // form1PERSONALBindingSource
            // 
            form1PERSONALBindingSource.DataSource = typeof(Form1_PERSONAL);
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(638, 386);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "Guardar";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // personalBindingSource1
            // 
            personalBindingSource1.DataMember = "Personal";
            personalBindingSource1.DataSource = form1DTOSJUDIBindingSource;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(17, 409);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(60, 15);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "linkLabel1";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(998, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 450);
            Controls.Add(linkLabel1);
            Controls.Add(buttonSave);
            Controls.Add(dataGridViewPERSONAL);
            Controls.Add(dataGridViewDTOSJUDI);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Departamento Judicial - Personal";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDTOSJUDI).EndInit();
            ((System.ComponentModel.ISupportInitialize)form1DTOSJUDIBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)form1DTOSJUDIBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPERSONAL).EndInit();
            ((System.ComponentModel.ISupportInitialize)personalBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)form1PERSONALBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)personalBindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridViewDTOSJUDI;
        private DataGridView dataGridViewPERSONAL;
        private Button buttonSave;
        private BindingSource form1DTOSJUDIBindingSource1;
        private DataGridViewTextBoxColumn aPELLIDOSDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lEGAJODataGridViewTextBoxColumn;
        private BindingSource form1PERSONALBindingSource;
        private BindingSource form1DTOSJUDIBindingSource;
        private BindingSource personalBindingSource;
        private BindingSource personalBindingSource1;
        private DataGridViewTextBoxColumn dTOJUDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dESCRIPCIONDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cantidad_personal;
        private DataGridViewTextBoxColumn nOMBRESDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn APELLIDO;
        private DataGridViewTextBoxColumn TIPODOC;
        private DataGridViewTextBoxColumn NRODOC;
        private DataGridViewTextBoxColumn NROIDPEMP;
        private DataGridViewTextBoxColumn _Id;
        private LinkLabel linkLabel1;
        private MenuStrip menuStrip1;
    }
}