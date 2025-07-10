namespace Demo_VisionMaster.UserControls
{
    partial class CameraDisplayControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbNameCamera = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbIsconnect = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbLenght = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbResult = new System.Windows.Forms.Label();
            this.pbShowImage = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsconnect)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(719, 429);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(40)))), ((int)(((byte)(48)))));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00001F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Controls.Add(this.lbNameCamera, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbIsconnect, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(713, 36);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lbNameCamera
            // 
            this.lbNameCamera.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lbNameCamera, 3);
            this.lbNameCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNameCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNameCamera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lbNameCamera.Location = new System.Drawing.Point(3, 0);
            this.lbNameCamera.Name = "lbNameCamera";
            this.lbNameCamera.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbNameCamera.Size = new System.Drawing.Size(397, 36);
            this.lbNameCamera.TabIndex = 0;
            this.lbNameCamera.Text = "Name Camera";
            this.lbNameCamera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.label2.Location = new System.Drawing.Point(540, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 36);
            this.label2.TabIndex = 1;
            this.label2.Text = "Connection";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbIsconnect
            // 
            this.pbIsconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbIsconnect.Location = new System.Drawing.Point(674, 3);
            this.pbIsconnect.Name = "pbIsconnect";
            this.pbIsconnect.Size = new System.Drawing.Size(36, 30);
            this.pbIsconnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIsconnect.TabIndex = 2;
            this.pbIsconnect.TabStop = false;
            this.pbIsconnect.Paint += new System.Windows.Forms.PaintEventHandler(this.pbIsconnect_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbLenght);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbResult);
            this.panel1.Controls.Add(this.pbShowImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 381);
            this.panel1.TabIndex = 3;
            // 
            // lbLenght
            // 
            this.lbLenght.AutoSize = true;
            this.lbLenght.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLenght.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbLenght.Location = new System.Drawing.Point(83, 45);
            this.lbLenght.Name = "lbLenght";
            this.lbLenght.Size = new System.Drawing.Size(31, 16);
            this.lbLenght.TabIndex = 6;
            this.lbLenght.Text = "500";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(20, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lenght:";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult.ForeColor = System.Drawing.Color.Red;
            this.lbResult.Location = new System.Drawing.Point(14, 10);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(57, 31);
            this.lbResult.TabIndex = 4;
            this.lbResult.Text = "NG";
            // 
            // pbShowImage
            // 
            this.pbShowImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbShowImage.Image = global::Demo_VisionMaster.Properties.Resources.gallery__1_;
            this.pbShowImage.Location = new System.Drawing.Point(0, 0);
            this.pbShowImage.Name = "pbShowImage";
            this.pbShowImage.Size = new System.Drawing.Size(713, 381);
            this.pbShowImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbShowImage.TabIndex = 3;
            this.pbShowImage.TabStop = false;
            // 
            // CameraDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CameraDisplayControl";
            this.Size = new System.Drawing.Size(719, 429);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsconnect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.PictureBox pbShowImage;
        private System.Windows.Forms.Label lbLenght;
        private System.Windows.Forms.Label lbNameCamera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbIsconnect;
    }
}
