namespace Demo_VisionMaster.Views
{
    partial class BlodConfigView
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
            this.mvdRenderActivex1 = new VisionDesigner.Controls.MVDRenderControl();
            this.mvdRenderActivex2 = new VisionDesigner.Controls.MVDRenderControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Init = new System.Windows.Forms.Button();
            this.DeInit = new System.Windows.Forms.Button();
            this.LoadImg = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.ImportXml = new System.Windows.Forms.Button();
            this.ExportXml = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioROIPolygon = new System.Windows.Forms.RadioButton();
            this.radioROIAnnulus = new System.Windows.Forms.RadioButton();
            this.radioROIRect = new System.Windows.Forms.RadioButton();
            this.radioROIAll = new System.Windows.Forms.RadioButton();
            this.comboBoxImage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioMaskPolygon = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ParamNameBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValueBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // mvdRenderActivex1
            // 
            this.mvdRenderActivex1.AdaptMode = VisionDesigner.Controls.MVD_RENDER_ADAPTIVE_MODE.MvdAdaptiveModeNormal;
            this.mvdRenderActivex1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.mvdRenderActivex1.ControlLanguage = VisionDesigner.Controls.MVD_RENDER_MENU_LANG_TYPE.MvdRenderMenuLangDefault;
            this.mvdRenderActivex1.EnableRetainShape = false;
            this.mvdRenderActivex1.EraserColor = System.Drawing.Color.Red;
            this.mvdRenderActivex1.EraserWidth = ((short)(10));
            this.mvdRenderActivex1.ImageInfoTool = true;
            this.mvdRenderActivex1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mvdRenderActivex1.InteractType = VisionDesigner.Controls.MVD_RENDER_INTERACT_TYPE.MvdRenderInteractStandard;
            this.mvdRenderActivex1.Location = new System.Drawing.Point(12, 12);
            this.mvdRenderActivex1.Name = "mvdRenderActivex1";
            this.mvdRenderActivex1.Size = new System.Drawing.Size(418, 457);
            this.mvdRenderActivex1.TabIndex = 16;
            this.mvdRenderActivex1.MVD_ShapeChangeEvent += new VisionDesigner.Controls.MVDRenderControl.MVDShapeChangeEventHandler(this.mvdRenderActivex1_MVD_ShapeChangeEvent);
            // 
            // mvdRenderActivex2
            // 
            this.mvdRenderActivex2.AdaptMode = VisionDesigner.Controls.MVD_RENDER_ADAPTIVE_MODE.MvdAdaptiveModeNormal;
            this.mvdRenderActivex2.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.mvdRenderActivex2.ControlLanguage = VisionDesigner.Controls.MVD_RENDER_MENU_LANG_TYPE.MvdRenderMenuLangDefault;
            this.mvdRenderActivex2.EnableRetainShape = false;
            this.mvdRenderActivex2.EraserColor = System.Drawing.Color.Red;
            this.mvdRenderActivex2.EraserWidth = ((short)(10));
            this.mvdRenderActivex2.ImageInfoTool = true;
            this.mvdRenderActivex2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mvdRenderActivex2.InteractType = VisionDesigner.Controls.MVD_RENDER_INTERACT_TYPE.MvdRenderInteractStandard;
            this.mvdRenderActivex2.Location = new System.Drawing.Point(436, 12);
            this.mvdRenderActivex2.Name = "mvdRenderActivex2";
            this.mvdRenderActivex2.Size = new System.Drawing.Size(441, 457);
            this.mvdRenderActivex2.TabIndex = 17;
            this.mvdRenderActivex2.MVD_ShapeChangeEvent += new VisionDesigner.Controls.MVDRenderControl.MVDShapeChangeEventHandler(this.mvdRenderActivex2_MVD_ShapeChangeEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Init);
            this.groupBox1.Controls.Add(this.DeInit);
            this.groupBox1.Controls.Add(this.LoadImg);
            this.groupBox1.Controls.Add(this.Run);
            this.groupBox1.Controls.Add(this.ImportXml);
            this.groupBox1.Controls.Add(this.ExportXml);
            this.groupBox1.Location = new System.Drawing.Point(12, 501);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 134);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // Init
            // 
            this.Init.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Init.Location = new System.Drawing.Point(6, 22);
            this.Init.Name = "Init";
            this.Init.Size = new System.Drawing.Size(75, 25);
            this.Init.TabIndex = 0;
            this.Init.Text = "Init";
            this.Init.UseVisualStyleBackColor = true;
            this.Init.Click += new System.EventHandler(this.Init_Click);
            // 
            // DeInit
            // 
            this.DeInit.Enabled = false;
            this.DeInit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DeInit.Location = new System.Drawing.Point(87, 22);
            this.DeInit.Name = "DeInit";
            this.DeInit.Size = new System.Drawing.Size(75, 25);
            this.DeInit.TabIndex = 1;
            this.DeInit.Text = "DeInit";
            this.DeInit.UseVisualStyleBackColor = true;
            this.DeInit.Click += new System.EventHandler(this.DeInit_Click);
            // 
            // LoadImg
            // 
            this.LoadImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LoadImg.Location = new System.Drawing.Point(6, 53);
            this.LoadImg.Name = "LoadImg";
            this.LoadImg.Size = new System.Drawing.Size(75, 25);
            this.LoadImg.TabIndex = 3;
            this.LoadImg.Text = "Load Image";
            this.LoadImg.UseVisualStyleBackColor = true;
            this.LoadImg.Click += new System.EventHandler(this.LoadImg_Click);
            // 
            // Run
            // 
            this.Run.Enabled = false;
            this.Run.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Run.Location = new System.Drawing.Point(87, 53);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 25);
            this.Run.TabIndex = 2;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // ImportXml
            // 
            this.ImportXml.Enabled = false;
            this.ImportXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ImportXml.Location = new System.Drawing.Point(6, 86);
            this.ImportXml.Name = "ImportXml";
            this.ImportXml.Size = new System.Drawing.Size(75, 25);
            this.ImportXml.TabIndex = 4;
            this.ImportXml.Text = "Import Xml";
            this.ImportXml.UseVisualStyleBackColor = true;
            this.ImportXml.Click += new System.EventHandler(this.ImportXml_Click);
            // 
            // ExportXml
            // 
            this.ExportXml.Enabled = false;
            this.ExportXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ExportXml.Location = new System.Drawing.Point(87, 84);
            this.ExportXml.Name = "ExportXml";
            this.ExportXml.Size = new System.Drawing.Size(75, 25);
            this.ExportXml.TabIndex = 5;
            this.ExportXml.Text = "Export Xml";
            this.ExportXml.UseVisualStyleBackColor = true;
            this.ExportXml.Click += new System.EventHandler(this.ExportXml_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioROIPolygon);
            this.groupBox2.Controls.Add(this.radioROIAnnulus);
            this.groupBox2.Controls.Add(this.radioROIRect);
            this.groupBox2.Controls.Add(this.radioROIAll);
            this.groupBox2.Location = new System.Drawing.Point(188, 501);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 53);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ROI";
            // 
            // radioROIPolygon
            // 
            this.radioROIPolygon.AutoSize = true;
            this.radioROIPolygon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioROIPolygon.Location = new System.Drawing.Point(179, 23);
            this.radioROIPolygon.Name = "radioROIPolygon";
            this.radioROIPolygon.Size = new System.Drawing.Size(63, 17);
            this.radioROIPolygon.TabIndex = 3;
            this.radioROIPolygon.TabStop = true;
            this.radioROIPolygon.Text = "Polygon";
            this.radioROIPolygon.UseVisualStyleBackColor = true;
            this.radioROIPolygon.Click += new System.EventHandler(this.radioROIPolygon_Click);
            // 
            // radioROIAnnulus
            // 
            this.radioROIAnnulus.AutoSize = true;
            this.radioROIAnnulus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioROIAnnulus.Location = new System.Drawing.Point(107, 22);
            this.radioROIAnnulus.Name = "radioROIAnnulus";
            this.radioROIAnnulus.Size = new System.Drawing.Size(63, 17);
            this.radioROIAnnulus.TabIndex = 2;
            this.radioROIAnnulus.TabStop = true;
            this.radioROIAnnulus.Text = "Annulus";
            this.radioROIAnnulus.UseVisualStyleBackColor = true;
            this.radioROIAnnulus.Click += new System.EventHandler(this.radioROIAnnulus_Click);
            // 
            // radioROIRect
            // 
            this.radioROIRect.AutoSize = true;
            this.radioROIRect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioROIRect.Location = new System.Drawing.Point(54, 22);
            this.radioROIRect.Name = "radioROIRect";
            this.radioROIRect.Size = new System.Drawing.Size(48, 17);
            this.radioROIRect.TabIndex = 1;
            this.radioROIRect.TabStop = true;
            this.radioROIRect.Text = "Rect";
            this.radioROIRect.UseVisualStyleBackColor = true;
            this.radioROIRect.Click += new System.EventHandler(this.radioROIRect_Click);
            // 
            // radioROIAll
            // 
            this.radioROIAll.AutoSize = true;
            this.radioROIAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioROIAll.Location = new System.Drawing.Point(7, 22);
            this.radioROIAll.Name = "radioROIAll";
            this.radioROIAll.Size = new System.Drawing.Size(36, 17);
            this.radioROIAll.TabIndex = 0;
            this.radioROIAll.TabStop = true;
            this.radioROIAll.Text = "All";
            this.radioROIAll.UseVisualStyleBackColor = true;
            this.radioROIAll.Click += new System.EventHandler(this.radioROIAll_Click);
            // 
            // comboBoxImage
            // 
            this.comboBoxImage.FormattingEnabled = true;
            this.comboBoxImage.Location = new System.Drawing.Point(268, 576);
            this.comboBoxImage.Name = "comboBoxImage";
            this.comboBoxImage.Size = new System.Drawing.Size(76, 21);
            this.comboBoxImage.TabIndex = 21;
            this.comboBoxImage.SelectedIndexChanged += new System.EventHandler(this.comboBoxImage_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(190, 580);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Output Image:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioMaskPolygon);
            this.groupBox3.Location = new System.Drawing.Point(364, 560);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(79, 56);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mark";
            // 
            // radioMaskPolygon
            // 
            this.radioMaskPolygon.AutoSize = true;
            this.radioMaskPolygon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioMaskPolygon.Location = new System.Drawing.Point(8, 24);
            this.radioMaskPolygon.Name = "radioMaskPolygon";
            this.radioMaskPolygon.Size = new System.Drawing.Size(63, 17);
            this.radioMaskPolygon.TabIndex = 0;
            this.radioMaskPolygon.TabStop = true;
            this.radioMaskPolygon.Text = "Polygon";
            this.radioMaskPolygon.UseVisualStyleBackColor = true;
            this.radioMaskPolygon.Click += new System.EventHandler(this.radioMaskPolygon_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 641);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(865, 148);
            this.richTextBox1.TabIndex = 23;
            this.richTextBox1.Text = "";
            this.richTextBox1.DoubleClick += new System.EventHandler(this.richTextBox1_DoubleClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamNameBoxCol,
            this.ParamValueBoxCol});
            this.dataGridView1.Location = new System.Drawing.Point(883, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(489, 777);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ParamNameBoxCol
            // 
            this.ParamNameBoxCol.HeaderText = "ParamName";
            this.ParamNameBoxCol.Name = "ParamNameBoxCol";
            this.ParamNameBoxCol.ReadOnly = true;
            this.ParamNameBoxCol.Width = 190;
            // 
            // ParamValueBoxCol
            // 
            this.ParamValueBoxCol.HeaderText = "ParamValue";
            this.ParamValueBoxCol.Name = "ParamValueBoxCol";
            this.ParamValueBoxCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ParamValueBoxCol.Width = 195;
            // 
            // BlodConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 801);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.comboBoxImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mvdRenderActivex2);
            this.Controls.Add(this.mvdRenderActivex1);
            this.Name = "BlodConfigView";
            this.Text = "BlodConfigView";
            this.Load += new System.EventHandler(this.BlodConfigView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivex1;
        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivex2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Init;
        private System.Windows.Forms.Button DeInit;
        private System.Windows.Forms.Button LoadImg;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Button ImportXml;
        private System.Windows.Forms.Button ExportXml;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioROIPolygon;
        private System.Windows.Forms.RadioButton radioROIAnnulus;
        private System.Windows.Forms.RadioButton radioROIRect;
        private System.Windows.Forms.RadioButton radioROIAll;
        private System.Windows.Forms.ComboBox comboBoxImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioMaskPolygon;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamNameBoxCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValueBoxCol;
    }
}