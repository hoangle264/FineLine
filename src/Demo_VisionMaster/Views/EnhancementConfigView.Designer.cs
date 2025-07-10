namespace Demo_VisionMaster.Views
{
    partial class EnhancementConfigView
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
            this.mvdRenderActivexIn = new VisionDesigner.Controls.MVDRenderControl();
            this.mvdRenderActivexOut = new VisionDesigner.Controls.MVDRenderControl();
            this.rtbInfoMessage = new System.Windows.Forms.RichTextBox();
            this.btnImpXml = new System.Windows.Forms.Button();
            this.btnExpXml = new System.Windows.Forms.Button();
            this.btnRunTool = new System.Windows.Forms.Button();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.btnDeInit = new System.Windows.Forms.Button();
            this.btnInitTool = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ParamNameBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValueBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegionGroupBox = new System.Windows.Forms.GroupBox();
            this.MaskCheckBox = new System.Windows.Forms.CheckBox();
            this.PolygonROIRadioButton = new System.Windows.Forms.RadioButton();
            this.AnnulROIRadioButton = new System.Windows.Forms.RadioButton();
            this.RectROIRadioButton = new System.Windows.Forms.RadioButton();
            this.AllROIRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.RegionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mvdRenderActivexIn
            // 
            this.mvdRenderActivexIn.AdaptMode = VisionDesigner.Controls.MVD_RENDER_ADAPTIVE_MODE.MvdAdaptiveModeNormal;
            this.mvdRenderActivexIn.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.mvdRenderActivexIn.ControlLanguage = VisionDesigner.Controls.MVD_RENDER_MENU_LANG_TYPE.MvdRenderMenuLangDefault;
            this.mvdRenderActivexIn.EnableRetainShape = false;
            this.mvdRenderActivexIn.EraserColor = System.Drawing.Color.Red;
            this.mvdRenderActivexIn.EraserWidth = ((short)(10));
            this.mvdRenderActivexIn.ImageInfoTool = true;
            this.mvdRenderActivexIn.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mvdRenderActivexIn.InteractType = VisionDesigner.Controls.MVD_RENDER_INTERACT_TYPE.MvdRenderInteractStandard;
            this.mvdRenderActivexIn.Location = new System.Drawing.Point(12, 12);
            this.mvdRenderActivexIn.Name = "mvdRenderActivexIn";
            this.mvdRenderActivexIn.Size = new System.Drawing.Size(569, 511);
            this.mvdRenderActivexIn.TabIndex = 13;
            this.mvdRenderActivexIn.MVD_ShapeChangeEvent += new VisionDesigner.Controls.MVDRenderControl.MVDShapeChangeEventHandler(this.mvdRenderActivexIn_MVD_ShapeChangeEvent);
            // 
            // mvdRenderActivexOut
            // 
            this.mvdRenderActivexOut.AdaptMode = VisionDesigner.Controls.MVD_RENDER_ADAPTIVE_MODE.MvdAdaptiveModeNormal;
            this.mvdRenderActivexOut.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.mvdRenderActivexOut.ControlLanguage = VisionDesigner.Controls.MVD_RENDER_MENU_LANG_TYPE.MvdRenderMenuLangDefault;
            this.mvdRenderActivexOut.EnableRetainShape = false;
            this.mvdRenderActivexOut.EraserColor = System.Drawing.Color.Red;
            this.mvdRenderActivexOut.EraserWidth = ((short)(10));
            this.mvdRenderActivexOut.ImageInfoTool = true;
            this.mvdRenderActivexOut.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mvdRenderActivexOut.InteractType = VisionDesigner.Controls.MVD_RENDER_INTERACT_TYPE.MvdRenderInteractStandard;
            this.mvdRenderActivexOut.Location = new System.Drawing.Point(587, 12);
            this.mvdRenderActivexOut.Name = "mvdRenderActivexOut";
            this.mvdRenderActivexOut.Size = new System.Drawing.Size(566, 511);
            this.mvdRenderActivexOut.TabIndex = 14;
            // 
            // rtbInfoMessage
            // 
            this.rtbInfoMessage.Location = new System.Drawing.Point(9, 529);
            this.rtbInfoMessage.Name = "rtbInfoMessage";
            this.rtbInfoMessage.Size = new System.Drawing.Size(1144, 160);
            this.rtbInfoMessage.TabIndex = 15;
            this.rtbInfoMessage.Text = "";
            this.rtbInfoMessage.DoubleClick += new System.EventHandler(this.rtbInfoMessage_DoubleClick);
            // 
            // btnImpXml
            // 
            this.btnImpXml.Enabled = false;
            this.btnImpXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImpXml.Location = new System.Drawing.Point(374, 706);
            this.btnImpXml.Name = "btnImpXml";
            this.btnImpXml.Size = new System.Drawing.Size(71, 31);
            this.btnImpXml.TabIndex = 21;
            this.btnImpXml.Text = "Import Xml";
            this.btnImpXml.UseVisualStyleBackColor = true;
            this.btnImpXml.Click += new System.EventHandler(this.btnImpXml_Click);
            // 
            // btnExpXml
            // 
            this.btnExpXml.Enabled = false;
            this.btnExpXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExpXml.Location = new System.Drawing.Point(301, 706);
            this.btnExpXml.Name = "btnExpXml";
            this.btnExpXml.Size = new System.Drawing.Size(71, 31);
            this.btnExpXml.TabIndex = 20;
            this.btnExpXml.Text = "Export Xml";
            this.btnExpXml.UseVisualStyleBackColor = true;
            this.btnExpXml.Click += new System.EventHandler(this.btnExpXml_Click);
            // 
            // btnRunTool
            // 
            this.btnRunTool.Enabled = false;
            this.btnRunTool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRunTool.Location = new System.Drawing.Point(155, 706);
            this.btnRunTool.Name = "btnRunTool";
            this.btnRunTool.Size = new System.Drawing.Size(71, 31);
            this.btnRunTool.TabIndex = 19;
            this.btnRunTool.Text = "Run";
            this.btnRunTool.UseVisualStyleBackColor = true;
            this.btnRunTool.Click += new System.EventHandler(this.btnRunTool_Click);
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadImg.Location = new System.Drawing.Point(82, 706);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(71, 31);
            this.btnLoadImg.TabIndex = 18;
            this.btnLoadImg.Text = "Load Image";
            this.btnLoadImg.UseVisualStyleBackColor = true;
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // btnDeInit
            // 
            this.btnDeInit.Enabled = false;
            this.btnDeInit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeInit.Location = new System.Drawing.Point(228, 706);
            this.btnDeInit.Name = "btnDeInit";
            this.btnDeInit.Size = new System.Drawing.Size(71, 31);
            this.btnDeInit.TabIndex = 17;
            this.btnDeInit.Text = "DeInit";
            this.btnDeInit.UseVisualStyleBackColor = true;
            this.btnDeInit.Click += new System.EventHandler(this.btnDeInit_Click);
            // 
            // btnInitTool
            // 
            this.btnInitTool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInitTool.Location = new System.Drawing.Point(9, 706);
            this.btnInitTool.Name = "btnInitTool";
            this.btnInitTool.Size = new System.Drawing.Size(71, 31);
            this.btnInitTool.TabIndex = 16;
            this.btnInitTool.Text = "Init";
            this.btnInitTool.UseVisualStyleBackColor = true;
            this.btnInitTool.Click += new System.EventHandler(this.btnInitTool_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamNameBoxCol,
            this.ParamValueBoxCol});
            this.dataGridView1.Location = new System.Drawing.Point(1159, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(319, 677);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ParamNameBoxCol
            // 
            this.ParamNameBoxCol.HeaderText = "ParamName";
            this.ParamNameBoxCol.Name = "ParamNameBoxCol";
            this.ParamNameBoxCol.Width = 116;
            // 
            // ParamValueBoxCol
            // 
            this.ParamValueBoxCol.HeaderText = "ParamValue";
            this.ParamValueBoxCol.Name = "ParamValueBoxCol";
            this.ParamValueBoxCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ParamValueBoxCol.Width = 116;
            // 
            // RegionGroupBox
            // 
            this.RegionGroupBox.Controls.Add(this.MaskCheckBox);
            this.RegionGroupBox.Controls.Add(this.PolygonROIRadioButton);
            this.RegionGroupBox.Controls.Add(this.AnnulROIRadioButton);
            this.RegionGroupBox.Controls.Add(this.RectROIRadioButton);
            this.RegionGroupBox.Controls.Add(this.AllROIRadioButton);
            this.RegionGroupBox.Location = new System.Drawing.Point(1158, 695);
            this.RegionGroupBox.Name = "RegionGroupBox";
            this.RegionGroupBox.Size = new System.Drawing.Size(320, 42);
            this.RegionGroupBox.TabIndex = 23;
            this.RegionGroupBox.TabStop = false;
            this.RegionGroupBox.Text = "Region";
            // 
            // MaskCheckBox
            // 
            this.MaskCheckBox.AutoSize = true;
            this.MaskCheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaskCheckBox.Location = new System.Drawing.Point(260, 18);
            this.MaskCheckBox.Name = "MaskCheckBox";
            this.MaskCheckBox.Size = new System.Drawing.Size(50, 17);
            this.MaskCheckBox.TabIndex = 4;
            this.MaskCheckBox.Text = "Mark";
            this.MaskCheckBox.UseVisualStyleBackColor = true;
            this.MaskCheckBox.Click += new System.EventHandler(this.MaskCheckBox_Click);
            // 
            // PolygonROIRadioButton
            // 
            this.PolygonROIRadioButton.AutoSize = true;
            this.PolygonROIRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PolygonROIRadioButton.Location = new System.Drawing.Point(188, 18);
            this.PolygonROIRadioButton.Name = "PolygonROIRadioButton";
            this.PolygonROIRadioButton.Size = new System.Drawing.Size(63, 17);
            this.PolygonROIRadioButton.TabIndex = 3;
            this.PolygonROIRadioButton.Text = "Polygon";
            this.PolygonROIRadioButton.UseVisualStyleBackColor = true;
            this.PolygonROIRadioButton.Click += new System.EventHandler(this.PolygonROIRadioButton_Click);
            // 
            // AnnulROIRadioButton
            // 
            this.AnnulROIRadioButton.AutoSize = true;
            this.AnnulROIRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AnnulROIRadioButton.Location = new System.Drawing.Point(116, 18);
            this.AnnulROIRadioButton.Name = "AnnulROIRadioButton";
            this.AnnulROIRadioButton.Size = new System.Drawing.Size(63, 17);
            this.AnnulROIRadioButton.TabIndex = 2;
            this.AnnulROIRadioButton.Text = "Annulus";
            this.AnnulROIRadioButton.UseVisualStyleBackColor = true;
            this.AnnulROIRadioButton.Click += new System.EventHandler(this.AnnulROIRadioButton_Click);
            // 
            // RectROIRadioButton
            // 
            this.RectROIRadioButton.AutoSize = true;
            this.RectROIRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RectROIRadioButton.Location = new System.Drawing.Point(62, 18);
            this.RectROIRadioButton.Name = "RectROIRadioButton";
            this.RectROIRadioButton.Size = new System.Drawing.Size(48, 17);
            this.RectROIRadioButton.TabIndex = 1;
            this.RectROIRadioButton.Text = "Rect";
            this.RectROIRadioButton.UseVisualStyleBackColor = true;
            this.RectROIRadioButton.Click += new System.EventHandler(this.RectROIRadioButton_Click);
            // 
            // AllROIRadioButton
            // 
            this.AllROIRadioButton.AutoSize = true;
            this.AllROIRadioButton.Checked = true;
            this.AllROIRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AllROIRadioButton.Location = new System.Drawing.Point(14, 18);
            this.AllROIRadioButton.Name = "AllROIRadioButton";
            this.AllROIRadioButton.Size = new System.Drawing.Size(36, 17);
            this.AllROIRadioButton.TabIndex = 0;
            this.AllROIRadioButton.TabStop = true;
            this.AllROIRadioButton.Text = "All";
            this.AllROIRadioButton.UseVisualStyleBackColor = true;
            this.AllROIRadioButton.Click += new System.EventHandler(this.AllROIRadioButton_Click);
            // 
            // EnhancementConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 749);
            this.Controls.Add(this.RegionGroupBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnImpXml);
            this.Controls.Add(this.btnExpXml);
            this.Controls.Add(this.btnRunTool);
            this.Controls.Add(this.btnLoadImg);
            this.Controls.Add(this.btnDeInit);
            this.Controls.Add(this.btnInitTool);
            this.Controls.Add(this.rtbInfoMessage);
            this.Controls.Add(this.mvdRenderActivexOut);
            this.Controls.Add(this.mvdRenderActivexIn);
            this.Name = "EnhancementConfigView";
            this.Text = "EnhancementConfigView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.RegionGroupBox.ResumeLayout(false);
            this.RegionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivexIn;
        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivexOut;
        private System.Windows.Forms.RichTextBox rtbInfoMessage;
        private System.Windows.Forms.Button btnImpXml;
        private System.Windows.Forms.Button btnExpXml;
        private System.Windows.Forms.Button btnRunTool;
        private System.Windows.Forms.Button btnLoadImg;
        private System.Windows.Forms.Button btnDeInit;
        private System.Windows.Forms.Button btnInitTool;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamNameBoxCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValueBoxCol;
        private System.Windows.Forms.GroupBox RegionGroupBox;
        private System.Windows.Forms.CheckBox MaskCheckBox;
        private System.Windows.Forms.RadioButton PolygonROIRadioButton;
        private System.Windows.Forms.RadioButton AnnulROIRadioButton;
        private System.Windows.Forms.RadioButton RectROIRadioButton;
        private System.Windows.Forms.RadioButton AllROIRadioButton;
    }
}