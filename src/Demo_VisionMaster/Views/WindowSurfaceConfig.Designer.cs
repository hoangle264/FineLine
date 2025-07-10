namespace Demo_VisionMaster.Views
{
    partial class WindowSurfaceConfig
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ParamNameBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamValueBoxCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWeight0 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWeight150 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeight30 = new System.Windows.Forms.TextBox();
            this.txtWeight120 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWeight60 = new System.Windows.Forms.TextBox();
            this.txtWeight90 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbInfoMessage = new System.Windows.Forms.RichTextBox();
            this.btnImpXml = new System.Windows.Forms.Button();
            this.btnExpXml = new System.Windows.Forms.Button();
            this.btnRunTool = new System.Windows.Forms.Button();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.btnDeInit = new System.Windows.Forms.Button();
            this.btnInitTool = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.RegionGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButtonAunulusROI = new System.Windows.Forms.RadioButton();
            this.radioButtonCircleROI = new System.Windows.Forms.RadioButton();
            this.radioButtonRectROI = new System.Windows.Forms.RadioButton();
            this.radioButtonAllROI = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonPolygonMask = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.RegionGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.mvdRenderActivexIn.Size = new System.Drawing.Size(573, 495);
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
            this.mvdRenderActivexOut.Location = new System.Drawing.Point(591, 12);
            this.mvdRenderActivexOut.Name = "mvdRenderActivexOut";
            this.mvdRenderActivexOut.Size = new System.Drawing.Size(613, 495);
            this.mvdRenderActivexOut.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamNameBoxCol,
            this.ParamValueBoxCol});
            this.dataGridView1.Location = new System.Drawing.Point(1210, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(321, 787);
            this.dataGridView1.TabIndex = 15;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtWeight0);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtWeight150);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtWeight30);
            this.groupBox2.Controls.Add(this.txtWeight120);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtWeight60);
            this.groupBox2.Controls.Add(this.txtWeight90);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(1033, 607);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 192);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Weight";
            // 
            // txtWeight0
            // 
            this.txtWeight0.Location = new System.Drawing.Point(76, 13);
            this.txtWeight0.Name = "txtWeight0";
            this.txtWeight0.Size = new System.Drawing.Size(69, 20);
            this.txtWeight0.TabIndex = 14;
            this.txtWeight0.Text = "1.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Weight 0";
            // 
            // txtWeight150
            // 
            this.txtWeight150.Location = new System.Drawing.Point(76, 159);
            this.txtWeight150.Name = "txtWeight150";
            this.txtWeight150.Size = new System.Drawing.Size(69, 20);
            this.txtWeight150.TabIndex = 14;
            this.txtWeight150.Text = "1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Weight 30";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(7, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Weight 150";
            // 
            // txtWeight30
            // 
            this.txtWeight30.Location = new System.Drawing.Point(76, 42);
            this.txtWeight30.Name = "txtWeight30";
            this.txtWeight30.Size = new System.Drawing.Size(69, 20);
            this.txtWeight30.TabIndex = 14;
            this.txtWeight30.Text = "1.0";
            // 
            // txtWeight120
            // 
            this.txtWeight120.Location = new System.Drawing.Point(76, 130);
            this.txtWeight120.Name = "txtWeight120";
            this.txtWeight120.Size = new System.Drawing.Size(69, 20);
            this.txtWeight120.TabIndex = 14;
            this.txtWeight120.Text = "1.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Weight 60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(7, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Weight 120";
            // 
            // txtWeight60
            // 
            this.txtWeight60.Location = new System.Drawing.Point(76, 72);
            this.txtWeight60.Name = "txtWeight60";
            this.txtWeight60.Size = new System.Drawing.Size(69, 20);
            this.txtWeight60.TabIndex = 14;
            this.txtWeight60.Text = "1.0";
            // 
            // txtWeight90
            // 
            this.txtWeight90.Location = new System.Drawing.Point(76, 101);
            this.txtWeight90.Name = "txtWeight90";
            this.txtWeight90.Size = new System.Drawing.Size(69, 20);
            this.txtWeight90.TabIndex = 14;
            this.txtWeight90.Text = "1.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(13, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Weight 90";
            // 
            // rtbInfoMessage
            // 
            this.rtbInfoMessage.Location = new System.Drawing.Point(12, 616);
            this.rtbInfoMessage.Name = "rtbInfoMessage";
            this.rtbInfoMessage.Size = new System.Drawing.Size(1015, 183);
            this.rtbInfoMessage.TabIndex = 18;
            this.rtbInfoMessage.Text = "";
            this.rtbInfoMessage.DoubleClick += new System.EventHandler(this.rtbInfoMessage_DoubleClick);
            // 
            // btnImpXml
            // 
            this.btnImpXml.Enabled = false;
            this.btnImpXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImpXml.Location = new System.Drawing.Point(356, 579);
            this.btnImpXml.Name = "btnImpXml";
            this.btnImpXml.Size = new System.Drawing.Size(68, 31);
            this.btnImpXml.TabIndex = 24;
            this.btnImpXml.Text = "Import Xml";
            this.btnImpXml.UseVisualStyleBackColor = true;
            this.btnImpXml.Click += new System.EventHandler(this.btnImpXml_Click);
            // 
            // btnExpXml
            // 
            this.btnExpXml.Enabled = false;
            this.btnExpXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExpXml.Location = new System.Drawing.Point(287, 579);
            this.btnExpXml.Name = "btnExpXml";
            this.btnExpXml.Size = new System.Drawing.Size(68, 31);
            this.btnExpXml.TabIndex = 23;
            this.btnExpXml.Text = "Export Xml";
            this.btnExpXml.UseVisualStyleBackColor = true;
            this.btnExpXml.Click += new System.EventHandler(this.btnExpXml_Click);
            // 
            // btnRunTool
            // 
            this.btnRunTool.Enabled = false;
            this.btnRunTool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRunTool.Location = new System.Drawing.Point(149, 579);
            this.btnRunTool.Name = "btnRunTool";
            this.btnRunTool.Size = new System.Drawing.Size(68, 31);
            this.btnRunTool.TabIndex = 22;
            this.btnRunTool.Text = "Run";
            this.btnRunTool.UseVisualStyleBackColor = true;
            this.btnRunTool.Click += new System.EventHandler(this.btnRunTool_Click);
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadImg.Location = new System.Drawing.Point(81, 579);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(68, 31);
            this.btnLoadImg.TabIndex = 21;
            this.btnLoadImg.Text = "Load Image";
            this.btnLoadImg.UseVisualStyleBackColor = true;
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // btnDeInit
            // 
            this.btnDeInit.Enabled = false;
            this.btnDeInit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeInit.Location = new System.Drawing.Point(218, 579);
            this.btnDeInit.Name = "btnDeInit";
            this.btnDeInit.Size = new System.Drawing.Size(68, 31);
            this.btnDeInit.TabIndex = 20;
            this.btnDeInit.Text = "DeInit";
            this.btnDeInit.UseVisualStyleBackColor = true;
            this.btnDeInit.Click += new System.EventHandler(this.btnDeInit_Click);
            // 
            // btnInitTool
            // 
            this.btnInitTool.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInitTool.Location = new System.Drawing.Point(13, 579);
            this.btnInitTool.Name = "btnInitTool";
            this.btnInitTool.Size = new System.Drawing.Size(68, 31);
            this.btnInitTool.TabIndex = 19;
            this.btnInitTool.Text = "Init";
            this.btnInitTool.UseVisualStyleBackColor = true;
            this.btnInitTool.Click += new System.EventHandler(this.btnInitTool_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOffset);
            this.groupBox1.Location = new System.Drawing.Point(444, 566);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(127, 44);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Offset";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(22, 15);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(68, 20);
            this.txtOffset.TabIndex = 14;
            this.txtOffset.Text = "0";
            // 
            // RegionGroupBox
            // 
            this.RegionGroupBox.Controls.Add(this.radioButtonAunulusROI);
            this.RegionGroupBox.Controls.Add(this.radioButtonCircleROI);
            this.RegionGroupBox.Controls.Add(this.radioButtonRectROI);
            this.RegionGroupBox.Controls.Add(this.radioButtonAllROI);
            this.RegionGroupBox.Location = new System.Drawing.Point(577, 568);
            this.RegionGroupBox.Name = "RegionGroupBox";
            this.RegionGroupBox.Size = new System.Drawing.Size(210, 42);
            this.RegionGroupBox.TabIndex = 26;
            this.RegionGroupBox.TabStop = false;
            this.RegionGroupBox.Text = "ROI";
            // 
            // radioButtonAunulusROI
            // 
            this.radioButtonAunulusROI.AutoSize = true;
            this.radioButtonAunulusROI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonAunulusROI.Location = new System.Drawing.Point(147, 18);
            this.radioButtonAunulusROI.Name = "radioButtonAunulusROI";
            this.radioButtonAunulusROI.Size = new System.Drawing.Size(50, 17);
            this.radioButtonAunulusROI.TabIndex = 3;
            this.radioButtonAunulusROI.Text = "Annu";
            this.radioButtonAunulusROI.UseVisualStyleBackColor = true;
            this.radioButtonAunulusROI.Click += new System.EventHandler(this.radioButtonAunulusROI_Click);
            // 
            // radioButtonCircleROI
            // 
            this.radioButtonCircleROI.AutoSize = true;
            this.radioButtonCircleROI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonCircleROI.Location = new System.Drawing.Point(108, 18);
            this.radioButtonCircleROI.Name = "radioButtonCircleROI";
            this.radioButtonCircleROI.Size = new System.Drawing.Size(37, 17);
            this.radioButtonCircleROI.TabIndex = 2;
            this.radioButtonCircleROI.Text = "Cir";
            this.radioButtonCircleROI.UseVisualStyleBackColor = true;
            this.radioButtonCircleROI.Click += new System.EventHandler(this.radioButtonCircleROI_Click);
            // 
            // radioButtonRectROI
            // 
            this.radioButtonRectROI.AutoSize = true;
            this.radioButtonRectROI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonRectROI.Location = new System.Drawing.Point(60, 18);
            this.radioButtonRectROI.Name = "radioButtonRectROI";
            this.radioButtonRectROI.Size = new System.Drawing.Size(48, 17);
            this.radioButtonRectROI.TabIndex = 1;
            this.radioButtonRectROI.Text = "Rect";
            this.radioButtonRectROI.UseVisualStyleBackColor = true;
            this.radioButtonRectROI.Click += new System.EventHandler(this.radioButtonRectROI_Click);
            // 
            // radioButtonAllROI
            // 
            this.radioButtonAllROI.AutoSize = true;
            this.radioButtonAllROI.Checked = true;
            this.radioButtonAllROI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonAllROI.Location = new System.Drawing.Point(14, 18);
            this.radioButtonAllROI.Name = "radioButtonAllROI";
            this.radioButtonAllROI.Size = new System.Drawing.Size(36, 17);
            this.radioButtonAllROI.TabIndex = 0;
            this.radioButtonAllROI.TabStop = true;
            this.radioButtonAllROI.Text = "All";
            this.radioButtonAllROI.UseVisualStyleBackColor = true;
            this.radioButtonAllROI.Click += new System.EventHandler(this.radioButtonAllROI_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonPolygonMask);
            this.groupBox3.Location = new System.Drawing.Point(804, 566);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(113, 44);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mark";
            // 
            // radioButtonPolygonMask
            // 
            this.radioButtonPolygonMask.AutoSize = true;
            this.radioButtonPolygonMask.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonPolygonMask.Location = new System.Drawing.Point(6, 18);
            this.radioButtonPolygonMask.Name = "radioButtonPolygonMask";
            this.radioButtonPolygonMask.Size = new System.Drawing.Size(45, 17);
            this.radioButtonPolygonMask.TabIndex = 4;
            this.radioButtonPolygonMask.Text = "Poly";
            this.radioButtonPolygonMask.UseVisualStyleBackColor = true;
            this.radioButtonPolygonMask.Click += new System.EventHandler(this.radioButtonPolygonMask_Click);
            // 
            // SurfaceConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 809);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.RegionGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnImpXml);
            this.Controls.Add(this.btnExpXml);
            this.Controls.Add(this.btnRunTool);
            this.Controls.Add(this.btnLoadImg);
            this.Controls.Add(this.btnDeInit);
            this.Controls.Add(this.btnInitTool);
            this.Controls.Add(this.rtbInfoMessage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.mvdRenderActivexOut);
            this.Controls.Add(this.mvdRenderActivexIn);
            this.Name = "SurfaceConfigView";
            this.Text = "SurfaceConfigView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.RegionGroupBox.ResumeLayout(false);
            this.RegionGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivexIn;
        private VisionDesigner.Controls.MVDRenderControl mvdRenderActivexOut;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamNameBoxCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamValueBoxCol;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtWeight0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWeight150;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWeight30;
        private System.Windows.Forms.TextBox txtWeight120;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWeight60;
        private System.Windows.Forms.TextBox txtWeight90;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbInfoMessage;
        private System.Windows.Forms.Button btnImpXml;
        private System.Windows.Forms.Button btnExpXml;
        private System.Windows.Forms.Button btnRunTool;
        private System.Windows.Forms.Button btnLoadImg;
        private System.Windows.Forms.Button btnDeInit;
        private System.Windows.Forms.Button btnInitTool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.GroupBox RegionGroupBox;
        private System.Windows.Forms.RadioButton radioButtonAunulusROI;
        private System.Windows.Forms.RadioButton radioButtonCircleROI;
        private System.Windows.Forms.RadioButton radioButtonRectROI;
        private System.Windows.Forms.RadioButton radioButtonAllROI;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonPolygonMask;
    }
}