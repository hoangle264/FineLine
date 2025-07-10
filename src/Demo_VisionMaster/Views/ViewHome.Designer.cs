namespace Demo_VisionMaster.Views
{
    partial class ViewHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHome));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.btnStopApp = new System.Windows.Forms.Button();
            this.btnInitApp = new System.Windows.Forms.Button();
            this.CameraDisplay1 = new Demo_VisionMaster.UserControls.CameraDisplayControl();
            this.CameraDisplay2 = new Demo_VisionMaster.UserControls.CameraDisplayControl();
            this.CameraDisplay3 = new Demo_VisionMaster.UserControls.CameraDisplayControl();
            this.CameraDisplay4 = new Demo_VisionMaster.UserControls.CameraDisplayControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CameraDisplay1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.CameraDisplay2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CameraDisplay3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CameraDisplay4, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.300117F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.34994F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.34994F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1664, 1003);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1654, 63);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTrigger);
            this.panel1.Controls.Add(this.btnStopApp);
            this.panel1.Controls.Add(this.btnInitApp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 57);
            this.panel1.TabIndex = 0;
            // 
            // btnTrigger
            // 
            this.btnTrigger.BackColor = System.Drawing.Color.Transparent;
            this.btnTrigger.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTrigger.FlatAppearance.BorderSize = 0;
            this.btnTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrigger.Image = global::Demo_VisionMaster.Properties.Resources.camera__1_;
            this.btnTrigger.Location = new System.Drawing.Point(150, 0);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(75, 57);
            this.btnTrigger.TabIndex = 4;
            this.btnTrigger.UseVisualStyleBackColor = false;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click_1);
            // 
            // btnStopApp
            // 
            this.btnStopApp.BackColor = System.Drawing.Color.Transparent;
            this.btnStopApp.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStopApp.FlatAppearance.BorderSize = 0;
            this.btnStopApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopApp.Image = global::Demo_VisionMaster.Properties.Resources.stop;
            this.btnStopApp.Location = new System.Drawing.Point(75, 0);
            this.btnStopApp.Name = "btnStopApp";
            this.btnStopApp.Size = new System.Drawing.Size(75, 57);
            this.btnStopApp.TabIndex = 3;
            this.btnStopApp.UseVisualStyleBackColor = false;
            this.btnStopApp.Click += new System.EventHandler(this.btnStopApp_Click_1);
            // 
            // btnInitApp
            // 
            this.btnInitApp.BackColor = System.Drawing.Color.Transparent;
            this.btnInitApp.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnInitApp.FlatAppearance.BorderSize = 0;
            this.btnInitApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitApp.Image = global::Demo_VisionMaster.Properties.Resources.play;
            this.btnInitApp.Location = new System.Drawing.Point(0, 0);
            this.btnInitApp.Name = "btnInitApp";
            this.btnInitApp.Size = new System.Drawing.Size(75, 57);
            this.btnInitApp.TabIndex = 2;
            this.btnInitApp.UseVisualStyleBackColor = false;
            this.btnInitApp.Click += new System.EventHandler(this.btnInitApp_Click_1);
            // 
            // CameraDisplay1
            // 
            this.CameraDisplay1.BackColor = System.Drawing.Color.Black;
            this.CameraDisplay1.Connection = false;
            this.CameraDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraDisplay1.Image = null;
            this.CameraDisplay1.Lenght = 0D;
            this.CameraDisplay1.Location = new System.Drawing.Point(3, 76);
            this.CameraDisplay1.Name = "CameraDisplay1";
            this.CameraDisplay1.NameCamera = "Camera 1";
            this.CameraDisplay1.Result = false;
            this.CameraDisplay1.Size = new System.Drawing.Size(826, 458);
            this.CameraDisplay1.TabIndex = 5;
            // 
            // CameraDisplay2
            // 
            this.CameraDisplay2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CameraDisplay2.Connection = false;
            this.CameraDisplay2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraDisplay2.Image = null;
            this.CameraDisplay2.Lenght = 0D;
            this.CameraDisplay2.Location = new System.Drawing.Point(835, 76);
            this.CameraDisplay2.Name = "CameraDisplay2";
            this.CameraDisplay2.NameCamera = "Camera 2";
            this.CameraDisplay2.Result = false;
            this.CameraDisplay2.Size = new System.Drawing.Size(826, 458);
            this.CameraDisplay2.TabIndex = 6;
            // 
            // CameraDisplay3
            // 
            this.CameraDisplay3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CameraDisplay3.Connection = false;
            this.CameraDisplay3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraDisplay3.Image = null;
            this.CameraDisplay3.Lenght = 0D;
            this.CameraDisplay3.Location = new System.Drawing.Point(3, 540);
            this.CameraDisplay3.Name = "CameraDisplay3";
            this.CameraDisplay3.NameCamera = "Camera 3";
            this.CameraDisplay3.Result = false;
            this.CameraDisplay3.Size = new System.Drawing.Size(826, 460);
            this.CameraDisplay3.TabIndex = 7;
            // 
            // CameraDisplay4
            // 
            this.CameraDisplay4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CameraDisplay4.Connection = false;
            this.CameraDisplay4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraDisplay4.Image = null;
            this.CameraDisplay4.Lenght = 0D;
            this.CameraDisplay4.Location = new System.Drawing.Point(835, 540);
            this.CameraDisplay4.Name = "CameraDisplay4";
            this.CameraDisplay4.NameCamera = "Camera 4";
            this.CameraDisplay4.Result = false;
            this.CameraDisplay4.Size = new System.Drawing.Size(826, 460);
            this.CameraDisplay4.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(830, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 57);
            this.panel2.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CadetBlue;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(746, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 57);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // ViewHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(1664, 1003);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewHome";
            this.Text = "HomeView";
            this.Load += new System.EventHandler(this.ViewHome_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private UserControls.CameraDisplayControl CameraDisplay1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Button btnStopApp;
        private System.Windows.Forms.Button btnInitApp;
        private UserControls.CameraDisplayControl CameraDisplay2;
        private UserControls.CameraDisplayControl CameraDisplay3;
        private UserControls.CameraDisplayControl CameraDisplay4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
    }
}