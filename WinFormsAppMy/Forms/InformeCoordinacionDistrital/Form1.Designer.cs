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
            region = new DataGridViewTextBoxColumn();
            distrito = new DataGridViewTextBoxColumn();
            cens = new DataGridViewTextBoxColumn();
            sede__nombre = new DataGridViewTextBoxColumn();
            comision__identificacion = new DataGridViewTextBoxColumn();
            tramo = new DataGridViewTextBoxColumn();
            cuatrimestre_ingreso = new DataGridViewTextBoxColumn();
            persona__apellidos = new DataGridViewTextBoxColumn();
            persona__nombres = new DataGridViewTextBoxColumn();
            persona__numero_documento = new DataGridViewTextBoxColumn();
            persona__genero = new DataGridViewTextBoxColumn();
            persona__fecha_nacimiento = new DataGridViewTextBoxColumn();
            persona__telefono = new DataGridViewTextBoxColumn();
            persona__email = new DataGridViewTextBoxColumn();
            tiene_dni = new DataGridViewTextBoxColumn();
            tiene_cuil = new DataGridViewTextBoxColumn();
            tiene_partida = new DataGridViewTextBoxColumn();
            tiene_certificado = new DataGridViewTextBoxColumn();
            estado_ingreso = new DataGridViewTextBoxColumn();
            asignatura111 = new DataGridViewTextBoxColumn();
            asignatura112 = new DataGridViewTextBoxColumn();
            asignatura113 = new DataGridViewTextBoxColumn();
            asignatura114 = new DataGridViewTextBoxColumn();
            asignatura115 = new DataGridViewTextBoxColumn();
            asignatura121 = new DataGridViewTextBoxColumn();
            asignatura122 = new DataGridViewTextBoxColumn();
            asignatura123 = new DataGridViewTextBoxColumn();
            asignatura124 = new DataGridViewTextBoxColumn();
            asignatura125 = new DataGridViewTextBoxColumn();
            asignatura211 = new DataGridViewTextBoxColumn();
            asignatura212 = new DataGridViewTextBoxColumn();
            asignatura213 = new DataGridViewTextBoxColumn();
            asignatura214 = new DataGridViewTextBoxColumn();
            asignatura215 = new DataGridViewTextBoxColumn();
            asignatura221 = new DataGridViewTextBoxColumn();
            asignatura222 = new DataGridViewTextBoxColumn();
            asignatura223 = new DataGridViewTextBoxColumn();
            asignatura224 = new DataGridViewTextBoxColumn();
            asignatura225 = new DataGridViewTextBoxColumn();
            asignatura311 = new DataGridViewTextBoxColumn();
            asignatura312 = new DataGridViewTextBoxColumn();
            asignatura313 = new DataGridViewTextBoxColumn();
            asignatura314 = new DataGridViewTextBoxColumn();
            asignatura315 = new DataGridViewTextBoxColumn();
            asignatura321 = new DataGridViewTextBoxColumn();
            asignatura322 = new DataGridViewTextBoxColumn();
            asignatura323 = new DataGridViewTextBoxColumn();
            asignatura324 = new DataGridViewTextBoxColumn();
            asignatura325 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)informeCoordinacionDistritalBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { region, distrito, cens, sede__nombre, comision__identificacion, tramo, cuatrimestre_ingreso, persona__apellidos, persona__nombres, persona__numero_documento, persona__genero, persona__fecha_nacimiento, persona__telefono, persona__email, tiene_dni, tiene_cuil, tiene_partida, tiene_certificado, estado_ingreso, asignatura111, asignatura112, asignatura113, asignatura114, asignatura115, asignatura121, asignatura122, asignatura123, asignatura124, asignatura125, asignatura211, asignatura212, asignatura213, asignatura214, asignatura215, asignatura221, asignatura222, asignatura223, asignatura224, asignatura225, asignatura311, asignatura312, asignatura313, asignatura314, asignatura315, asignatura321, asignatura322, asignatura323, asignatura324, asignatura325, dataGridViewTextBoxColumn1 });
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
            // region
            // 
            region.DataPropertyName = "region";
            region.HeaderText = "region";
            region.Name = "region";
            // 
            // distrito
            // 
            distrito.DataPropertyName = "distrito";
            distrito.HeaderText = "distrito";
            distrito.Name = "distrito";
            // 
            // cens
            // 
            cens.DataPropertyName = "cens";
            cens.HeaderText = "cens";
            cens.Name = "cens";
            // 
            // sede__nombre
            // 
            sede__nombre.DataPropertyName = "sede__nombre";
            sede__nombre.HeaderText = "sede__nombre";
            sede__nombre.Name = "sede__nombre";
            sede__nombre.ReadOnly = true;
            // 
            // comision__identificacion
            // 
            comision__identificacion.DataPropertyName = "comision__identificacion";
            comision__identificacion.HeaderText = "comision__identificacion";
            comision__identificacion.Name = "comision__identificacion";
            comision__identificacion.ReadOnly = true;
            // 
            // tramo
            // 
            tramo.DataPropertyName = "tramo";
            tramo.HeaderText = "tramo";
            tramo.Name = "tramo";
            tramo.ReadOnly = true;
            // 
            // cuatrimestre_ingreso
            // 
            cuatrimestre_ingreso.DataPropertyName = "cuatrimestre_ingreso";
            cuatrimestre_ingreso.HeaderText = "cuatrimestre_ingreso";
            cuatrimestre_ingreso.Name = "cuatrimestre_ingreso";
            cuatrimestre_ingreso.ReadOnly = true;
            // 
            // persona__apellidos
            // 
            persona__apellidos.DataPropertyName = "persona__apellidos";
            persona__apellidos.HeaderText = "persona__apellidos";
            persona__apellidos.Name = "persona__apellidos";
            // 
            // persona__nombres
            // 
            persona__nombres.DataPropertyName = "persona__nombres";
            persona__nombres.HeaderText = "persona__nombres";
            persona__nombres.Name = "persona__nombres";
            // 
            // persona__numero_documento
            // 
            persona__numero_documento.DataPropertyName = "persona__numero_documento";
            persona__numero_documento.HeaderText = "persona__numero_documento";
            persona__numero_documento.Name = "persona__numero_documento";
            // 
            // persona__genero
            // 
            persona__genero.DataPropertyName = "persona__genero";
            persona__genero.HeaderText = "persona__genero";
            persona__genero.Name = "persona__genero";
            // 
            // persona__fecha_nacimiento
            // 
            persona__fecha_nacimiento.DataPropertyName = "persona__fecha_nacimiento";
            persona__fecha_nacimiento.HeaderText = "persona__fecha_nacimiento";
            persona__fecha_nacimiento.Name = "persona__fecha_nacimiento";
            // 
            // persona__telefono
            // 
            persona__telefono.DataPropertyName = "persona__telefono";
            persona__telefono.HeaderText = "persona__telefono";
            persona__telefono.Name = "persona__telefono";
            // 
            // persona__email
            // 
            persona__email.DataPropertyName = "persona__email";
            persona__email.HeaderText = "persona__email";
            persona__email.Name = "persona__email";
            // 
            // tiene_dni
            // 
            tiene_dni.DataPropertyName = "tiene_dni";
            tiene_dni.HeaderText = "tiene_dni";
            tiene_dni.Name = "tiene_dni";
            // 
            // tiene_cuil
            // 
            tiene_cuil.DataPropertyName = "tiene_cuil";
            tiene_cuil.HeaderText = "tiene_cuil";
            tiene_cuil.Name = "tiene_cuil";
            // 
            // tiene_partida
            // 
            tiene_partida.DataPropertyName = "tiene_partida";
            tiene_partida.HeaderText = "tiene_partida";
            tiene_partida.Name = "tiene_partida";
            // 
            // tiene_certificado
            // 
            tiene_certificado.DataPropertyName = "tiene_certificado";
            tiene_certificado.HeaderText = "tiene_certificado";
            tiene_certificado.Name = "tiene_certificado";
            // 
            // estado_ingreso
            // 
            estado_ingreso.DataPropertyName = "estado_ingreso";
            estado_ingreso.HeaderText = "estado_ingreso";
            estado_ingreso.Name = "estado_ingreso";
            // 
            // asignatura111
            // 
            asignatura111.DataPropertyName = "asignatura111";
            asignatura111.HeaderText = "asignatura111";
            asignatura111.Name = "asignatura111";
            // 
            // asignatura112
            // 
            asignatura112.DataPropertyName = "asignatura112";
            asignatura112.HeaderText = "asignatura112";
            asignatura112.Name = "asignatura112";
            // 
            // asignatura113
            // 
            asignatura113.DataPropertyName = "asignatura113";
            asignatura113.HeaderText = "asignatura113";
            asignatura113.Name = "asignatura113";
            // 
            // asignatura114
            // 
            asignatura114.DataPropertyName = "asignatura114";
            asignatura114.HeaderText = "asignatura114";
            asignatura114.Name = "asignatura114";
            // 
            // asignatura115
            // 
            asignatura115.DataPropertyName = "asignatura115";
            asignatura115.HeaderText = "asignatura115";
            asignatura115.Name = "asignatura115";
            // 
            // asignatura121
            // 
            asignatura121.DataPropertyName = "asignatura121";
            asignatura121.HeaderText = "asignatura121";
            asignatura121.Name = "asignatura121";
            // 
            // asignatura122
            // 
            asignatura122.DataPropertyName = "asignatura122";
            asignatura122.HeaderText = "asignatura122";
            asignatura122.Name = "asignatura122";
            // 
            // asignatura123
            // 
            asignatura123.DataPropertyName = "asignatura123";
            asignatura123.HeaderText = "asignatura123";
            asignatura123.Name = "asignatura123";
            // 
            // asignatura124
            // 
            asignatura124.DataPropertyName = "asignatura124";
            asignatura124.HeaderText = "asignatura124";
            asignatura124.Name = "asignatura124";
            // 
            // asignatura125
            // 
            asignatura125.DataPropertyName = "asignatura125";
            asignatura125.HeaderText = "asignatura125";
            asignatura125.Name = "asignatura125";
            // 
            // asignatura211
            // 
            asignatura211.DataPropertyName = "asignatura211";
            asignatura211.HeaderText = "asignatura211";
            asignatura211.Name = "asignatura211";
            // 
            // asignatura212
            // 
            asignatura212.DataPropertyName = "asignatura212";
            asignatura212.HeaderText = "asignatura212";
            asignatura212.Name = "asignatura212";
            // 
            // asignatura213
            // 
            asignatura213.DataPropertyName = "asignatura213";
            asignatura213.HeaderText = "asignatura213";
            asignatura213.Name = "asignatura213";
            // 
            // asignatura214
            // 
            asignatura214.DataPropertyName = "asignatura214";
            asignatura214.HeaderText = "asignatura214";
            asignatura214.Name = "asignatura214";
            // 
            // asignatura215
            // 
            asignatura215.DataPropertyName = "asignatura215";
            asignatura215.HeaderText = "asignatura215";
            asignatura215.Name = "asignatura215";
            // 
            // asignatura221
            // 
            asignatura221.DataPropertyName = "asignatura221";
            asignatura221.HeaderText = "asignatura221";
            asignatura221.Name = "asignatura221";
            // 
            // asignatura222
            // 
            asignatura222.DataPropertyName = "asignatura222";
            asignatura222.HeaderText = "asignatura222";
            asignatura222.Name = "asignatura222";
            // 
            // asignatura223
            // 
            asignatura223.DataPropertyName = "asignatura223";
            asignatura223.HeaderText = "asignatura223";
            asignatura223.Name = "asignatura223";
            // 
            // asignatura224
            // 
            asignatura224.DataPropertyName = "asignatura224";
            asignatura224.HeaderText = "asignatura224";
            asignatura224.Name = "asignatura224";
            // 
            // asignatura225
            // 
            asignatura225.DataPropertyName = "asignatura225";
            asignatura225.HeaderText = "asignatura225";
            asignatura225.Name = "asignatura225";
            // 
            // asignatura311
            // 
            asignatura311.DataPropertyName = "asignatura311";
            asignatura311.HeaderText = "asignatura311";
            asignatura311.Name = "asignatura311";
            // 
            // asignatura312
            // 
            asignatura312.DataPropertyName = "asignatura312";
            asignatura312.HeaderText = "asignatura312";
            asignatura312.Name = "asignatura312";
            // 
            // asignatura313
            // 
            asignatura313.DataPropertyName = "asignatura313";
            asignatura313.HeaderText = "asignatura313";
            asignatura313.Name = "asignatura313";
            // 
            // asignatura314
            // 
            asignatura314.DataPropertyName = "asignatura314";
            asignatura314.HeaderText = "asignatura314";
            asignatura314.Name = "asignatura314";
            // 
            // asignatura315
            // 
            asignatura315.DataPropertyName = "asignatura315";
            asignatura315.HeaderText = "asignatura315";
            asignatura315.Name = "asignatura315";
            // 
            // asignatura321
            // 
            asignatura321.DataPropertyName = "asignatura321";
            asignatura321.HeaderText = "asignatura321";
            asignatura321.Name = "asignatura321";
            // 
            // asignatura322
            // 
            asignatura322.DataPropertyName = "asignatura322";
            asignatura322.HeaderText = "asignatura322";
            asignatura322.Name = "asignatura322";
            // 
            // asignatura323
            // 
            asignatura323.DataPropertyName = "asignatura323";
            asignatura323.HeaderText = "asignatura323";
            asignatura323.Name = "asignatura323";
            // 
            // asignatura324
            // 
            asignatura324.DataPropertyName = "asignatura324";
            asignatura324.HeaderText = "asignatura324";
            asignatura324.Name = "asignatura324";
            // 
            // asignatura325
            // 
            asignatura325.DataPropertyName = "asignatura325";
            asignatura325.HeaderText = "asignatura325";
            asignatura325.Name = "asignatura325";
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "sede__nombre";
            dataGridViewTextBoxColumn1.HeaderText = "sede__nombre";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
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
        private BindingSource informeCoordinacionDistritalBindingSource;
        private DataGridViewTextBoxColumn alumnotienedniDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn alumnotienepartidaDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn alumnotienecertificadoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn region;
        private DataGridViewTextBoxColumn distrito;
        private DataGridViewTextBoxColumn cens;
        private DataGridViewTextBoxColumn sede__nombre;
        private DataGridViewTextBoxColumn comision__identificacion;
        private DataGridViewTextBoxColumn tramo;
        private DataGridViewTextBoxColumn cuatrimestre_ingreso;
        private DataGridViewTextBoxColumn persona__apellidos;
        private DataGridViewTextBoxColumn persona__nombres;
        private DataGridViewTextBoxColumn persona__numero_documento;
        private DataGridViewTextBoxColumn persona__genero;
        private DataGridViewTextBoxColumn persona__fecha_nacimiento;
        private DataGridViewTextBoxColumn persona__telefono;
        private DataGridViewTextBoxColumn persona__email;
        private DataGridViewTextBoxColumn tiene_dni;
        private DataGridViewTextBoxColumn tiene_cuil;
        private DataGridViewTextBoxColumn tiene_partida;
        private DataGridViewTextBoxColumn tiene_certificado;
        private DataGridViewTextBoxColumn estado_ingreso;
        private DataGridViewTextBoxColumn asignatura111;
        private DataGridViewTextBoxColumn asignatura112;
        private DataGridViewTextBoxColumn asignatura113;
        private DataGridViewTextBoxColumn asignatura114;
        private DataGridViewTextBoxColumn asignatura115;
        private DataGridViewTextBoxColumn asignatura121;
        private DataGridViewTextBoxColumn asignatura122;
        private DataGridViewTextBoxColumn asignatura123;
        private DataGridViewTextBoxColumn asignatura124;
        private DataGridViewTextBoxColumn asignatura125;
        private DataGridViewTextBoxColumn asignatura211;
        private DataGridViewTextBoxColumn asignatura212;
        private DataGridViewTextBoxColumn asignatura213;
        private DataGridViewTextBoxColumn asignatura214;
        private DataGridViewTextBoxColumn asignatura215;
        private DataGridViewTextBoxColumn asignatura221;
        private DataGridViewTextBoxColumn asignatura222;
        private DataGridViewTextBoxColumn asignatura223;
        private DataGridViewTextBoxColumn asignatura224;
        private DataGridViewTextBoxColumn asignatura225;
        private DataGridViewTextBoxColumn asignatura311;
        private DataGridViewTextBoxColumn asignatura312;
        private DataGridViewTextBoxColumn asignatura313;
        private DataGridViewTextBoxColumn asignatura314;
        private DataGridViewTextBoxColumn asignatura315;
        private DataGridViewTextBoxColumn asignatura321;
        private DataGridViewTextBoxColumn asignatura322;
        private DataGridViewTextBoxColumn asignatura323;
        private DataGridViewTextBoxColumn asignatura324;
        private DataGridViewTextBoxColumn asignatura325;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}