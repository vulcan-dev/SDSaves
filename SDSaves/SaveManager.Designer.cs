namespace SDSaves
{
    partial class SaveManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveManager));
            this.container = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.input_saveName = new System.Windows.Forms.TextBox();
            this.btn_createSave = new System.Windows.Forms.Button();
            this.button_panel = new System.Windows.Forms.TableLayoutPanel();
            this.container.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.container.Controls.Add(this.panel1);
            this.container.Controls.Add(this.input_saveName);
            this.container.Controls.Add(this.btn_createSave);
            this.container.Controls.Add(this.button_panel);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(416, 609);
            this.container.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 40);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(322, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "v0.3";
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_title.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(-13, 0);
            this.lbl_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.lbl_title.Size = new System.Drawing.Size(416, 39);
            this.lbl_title.TabIndex = 7;
            this.lbl_title.Text = "SurrounDead Save Manager";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // input_saveName
            // 
            this.input_saveName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(51)))), ((int)(((byte)(61)))));
            this.input_saveName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input_saveName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_saveName.ForeColor = System.Drawing.Color.White;
            this.input_saveName.Location = new System.Drawing.Point(-3, 574);
            this.input_saveName.Margin = new System.Windows.Forms.Padding(0);
            this.input_saveName.MaximumSize = new System.Drawing.Size(261, 35);
            this.input_saveName.MaxLength = 24;
            this.input_saveName.MinimumSize = new System.Drawing.Size(261, 35);
            this.input_saveName.Multiline = true;
            this.input_saveName.Name = "input_saveName";
            this.input_saveName.Size = new System.Drawing.Size(261, 35);
            this.input_saveName.TabIndex = 3;
            this.input_saveName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input_saveName.WordWrap = false;
            // 
            // btn_createSave
            // 
            this.btn_createSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(51)))), ((int)(((byte)(61)))));
            this.btn_createSave.FlatAppearance.BorderSize = 0;
            this.btn_createSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_createSave.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_createSave.ForeColor = System.Drawing.Color.White;
            this.btn_createSave.Location = new System.Drawing.Point(262, 574);
            this.btn_createSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_createSave.Name = "btn_createSave";
            this.btn_createSave.Size = new System.Drawing.Size(154, 35);
            this.btn_createSave.TabIndex = 4;
            this.btn_createSave.Text = "Create New";
            this.btn_createSave.UseVisualStyleBackColor = false;
            // 
            // button_panel
            // 
            this.button_panel.AutoScroll = true;
            this.button_panel.BackColor = System.Drawing.Color.Transparent;
            this.button_panel.ColumnCount = 3;
            this.button_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.button_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.button_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.button_panel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(51)))), ((int)(((byte)(61)))));
            this.button_panel.Location = new System.Drawing.Point(4, 42);
            this.button_panel.Margin = new System.Windows.Forms.Padding(0);
            this.button_panel.Name = "button_panel";
            this.button_panel.RowCount = 1;
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.button_panel.Size = new System.Drawing.Size(408, 519);
            this.button_panel.TabIndex = 6;
            // 
            // SaveManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SDSaves.Properties.Resources.sd_background;
            this.ClientSize = new System.Drawing.Size(416, 609);
            this.Controls.Add(this.container);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "SaveManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SurrounDead Save Manager";
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.TableLayoutPanel button_panel;
        private System.Windows.Forms.Button btn_createSave;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TextBox input_saveName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}

