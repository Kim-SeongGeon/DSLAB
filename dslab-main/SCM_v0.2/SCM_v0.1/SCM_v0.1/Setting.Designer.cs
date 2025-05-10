
namespace xplane_data_test
{
    partial class Setting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainP = new System.Windows.Forms.Panel();
            this.listL = new System.Windows.Forms.Label();
            this.VehicleB = new System.Windows.Forms.Button();
            this.CPB = new System.Windows.Forms.ComboBox();
            this.VsersionL = new System.Windows.Forms.Label();
            this.IncompatL = new System.Windows.Forms.Label();
            this.CompatL = new System.Windows.Forms.Label();
            this.SignatureL = new System.Windows.Forms.Label();
            this.VersionCB = new System.Windows.Forms.ComboBox();
            this.SignatureCB = new System.Windows.Forms.ComboBox();
            this.CompatCB = new System.Windows.Forms.ComboBox();
            this.IncompatCB = new System.Windows.Forms.ComboBox();
            this.SaveB = new System.Windows.Forms.Button();
            this.CompIDCB = new System.Windows.Forms.ComboBox();
            this.CompIDL = new System.Windows.Forms.Label();
            this.SystemIDCB = new System.Windows.Forms.ComboBox();
            this.SystemIDL = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.MainP.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listL);
            this.panel1.Location = new System.Drawing.Point(12, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 837);
            this.panel1.TabIndex = 0;
            // 
            // MainP
            // 
            this.MainP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainP.Controls.Add(this.SystemIDCB);
            this.MainP.Controls.Add(this.SystemIDL);
            this.MainP.Controls.Add(this.CompIDCB);
            this.MainP.Controls.Add(this.CompIDL);
            this.MainP.Controls.Add(this.IncompatCB);
            this.MainP.Controls.Add(this.CompatCB);
            this.MainP.Controls.Add(this.SignatureCB);
            this.MainP.Controls.Add(this.VersionCB);
            this.MainP.Controls.Add(this.SignatureL);
            this.MainP.Controls.Add(this.CompatL);
            this.MainP.Controls.Add(this.IncompatL);
            this.MainP.Controls.Add(this.VsersionL);
            this.MainP.Location = new System.Drawing.Point(385, 53);
            this.MainP.Name = "MainP";
            this.MainP.Size = new System.Drawing.Size(1100, 837);
            this.MainP.TabIndex = 1;
            // 
            // listL
            // 
            this.listL.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listL.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listL.Location = new System.Drawing.Point(0, 0);
            this.listL.Name = "listL";
            this.listL.Size = new System.Drawing.Size(336, 55);
            this.listL.TabIndex = 0;
            this.listL.Text = "무인기 목록";
            this.listL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VehicleB
            // 
            this.VehicleB.Location = new System.Drawing.Point(1175, 12);
            this.VehicleB.Name = "VehicleB";
            this.VehicleB.Size = new System.Drawing.Size(310, 34);
            this.VehicleB.TabIndex = 2;
            this.VehicleB.Text = "무인기 추가";
            this.VehicleB.UseVisualStyleBackColor = true;
            this.VehicleB.Click += new System.EventHandler(this.VehicleB_Click);
            // 
            // CPB
            // 
            this.CPB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CPB.Font = new System.Drawing.Font("굴림", 15F);
            this.CPB.FormattingEnabled = true;
            this.CPB.Items.AddRange(new object[] {
            "STANAG 4586",
            "MAVLink",
            "MSP"});
            this.CPB.Location = new System.Drawing.Point(848, 9);
            this.CPB.Name = "CPB";
            this.CPB.Size = new System.Drawing.Size(308, 38);
            this.CPB.TabIndex = 3;
            this.CPB.SelectedIndexChanged += new System.EventHandler(this.CPB_SelectedIndexChanged);
            // 
            // VsersionL
            // 
            this.VsersionL.AutoSize = true;
            this.VsersionL.Font = new System.Drawing.Font("굴림", 20F);
            this.VsersionL.Location = new System.Drawing.Point(39, 57);
            this.VsersionL.Name = "VsersionL";
            this.VsersionL.Size = new System.Drawing.Size(155, 40);
            this.VsersionL.TabIndex = 0;
            this.VsersionL.Text = "Version";
            // 
            // IncompatL
            // 
            this.IncompatL.AutoSize = true;
            this.IncompatL.Font = new System.Drawing.Font("굴림", 20F);
            this.IncompatL.Location = new System.Drawing.Point(39, 138);
            this.IncompatL.Name = "IncompatL";
            this.IncompatL.Size = new System.Drawing.Size(293, 40);
            this.IncompatL.TabIndex = 1;
            this.IncompatL.Text = "IncompatFlags";
            // 
            // CompatL
            // 
            this.CompatL.AutoSize = true;
            this.CompatL.Font = new System.Drawing.Font("굴림", 20F);
            this.CompatL.Location = new System.Drawing.Point(39, 219);
            this.CompatL.Name = "CompatL";
            this.CompatL.Size = new System.Drawing.Size(265, 40);
            this.CompatL.TabIndex = 2;
            this.CompatL.Text = "CompatFlags";
            // 
            // SignatureL
            // 
            this.SignatureL.AutoSize = true;
            this.SignatureL.Font = new System.Drawing.Font("굴림", 20F);
            this.SignatureL.Location = new System.Drawing.Point(39, 462);
            this.SignatureL.Name = "SignatureL";
            this.SignatureL.Size = new System.Drawing.Size(194, 40);
            this.SignatureL.TabIndex = 3;
            this.SignatureL.Text = "Signature";
            // 
            // VersionCB
            // 
            this.VersionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VersionCB.Font = new System.Drawing.Font("굴림", 20F);
            this.VersionCB.FormattingEnabled = true;
            this.VersionCB.Items.AddRange(new object[] {
            "MAVLink1",
            "MAVLink2"});
            this.VersionCB.Location = new System.Drawing.Point(452, 57);
            this.VersionCB.Name = "VersionCB";
            this.VersionCB.Size = new System.Drawing.Size(289, 48);
            this.VersionCB.TabIndex = 4;
            // 
            // SignatureCB
            // 
            this.SignatureCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SignatureCB.Font = new System.Drawing.Font("굴림", 20F);
            this.SignatureCB.FormattingEnabled = true;
            this.SignatureCB.Items.AddRange(new object[] {
            "미사용",
            "사용"});
            this.SignatureCB.Location = new System.Drawing.Point(452, 462);
            this.SignatureCB.Name = "SignatureCB";
            this.SignatureCB.Size = new System.Drawing.Size(289, 48);
            this.SignatureCB.TabIndex = 5;
            // 
            // CompatCB
            // 
            this.CompatCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CompatCB.Font = new System.Drawing.Font("굴림", 20F);
            this.CompatCB.FormattingEnabled = true;
            this.CompatCB.Items.AddRange(new object[] {
            "미사용",
            "사용"});
            this.CompatCB.Location = new System.Drawing.Point(452, 219);
            this.CompatCB.Name = "CompatCB";
            this.CompatCB.Size = new System.Drawing.Size(289, 48);
            this.CompatCB.TabIndex = 6;
            // 
            // IncompatCB
            // 
            this.IncompatCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IncompatCB.Font = new System.Drawing.Font("굴림", 20F);
            this.IncompatCB.FormattingEnabled = true;
            this.IncompatCB.Items.AddRange(new object[] {
            "미사용",
            "사용"});
            this.IncompatCB.Location = new System.Drawing.Point(452, 138);
            this.IncompatCB.Name = "IncompatCB";
            this.IncompatCB.Size = new System.Drawing.Size(289, 48);
            this.IncompatCB.TabIndex = 7;
            // 
            // SaveB
            // 
            this.SaveB.Location = new System.Drawing.Point(385, 9);
            this.SaveB.Name = "SaveB";
            this.SaveB.Size = new System.Drawing.Size(145, 37);
            this.SaveB.TabIndex = 4;
            this.SaveB.Text = "Save";
            this.SaveB.UseVisualStyleBackColor = true;
            this.SaveB.Click += new System.EventHandler(this.SaveB_Click);
            // 
            // CompIDCB
            // 
            this.CompIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CompIDCB.Font = new System.Drawing.Font("굴림", 20F);
            this.CompIDCB.FormattingEnabled = true;
            this.CompIDCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CompIDCB.Location = new System.Drawing.Point(452, 381);
            this.CompIDCB.Name = "CompIDCB";
            this.CompIDCB.Size = new System.Drawing.Size(289, 48);
            this.CompIDCB.TabIndex = 9;
            // 
            // CompIDL
            // 
            this.CompIDL.AutoSize = true;
            this.CompIDL.Font = new System.Drawing.Font("굴림", 20F);
            this.CompIDL.Location = new System.Drawing.Point(39, 381);
            this.CompIDL.Name = "CompIDL";
            this.CompIDL.Size = new System.Drawing.Size(274, 40);
            this.CompIDL.TabIndex = 8;
            this.CompIDL.Text = "ComponentID";
            // 
            // SystemIDCB
            // 
            this.SystemIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SystemIDCB.Font = new System.Drawing.Font("굴림", 20F);
            this.SystemIDCB.FormattingEnabled = true;
            this.SystemIDCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.SystemIDCB.Location = new System.Drawing.Point(452, 300);
            this.SystemIDCB.Name = "SystemIDCB";
            this.SystemIDCB.Size = new System.Drawing.Size(289, 48);
            this.SystemIDCB.TabIndex = 11;
            // 
            // SystemIDL
            // 
            this.SystemIDL.AutoSize = true;
            this.SystemIDL.Font = new System.Drawing.Font("굴림", 20F);
            this.SystemIDL.Location = new System.Drawing.Point(39, 300);
            this.SystemIDL.Name = "SystemIDL";
            this.SystemIDL.Size = new System.Drawing.Size(193, 40);
            this.SystemIDL.TabIndex = 10;
            this.SystemIDL.Text = "SystemID";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1497, 902);
            this.Controls.Add(this.SaveB);
            this.Controls.Add(this.CPB);
            this.Controls.Add(this.VehicleB);
            this.Controls.Add(this.MainP);
            this.Controls.Add(this.panel1);
            this.Name = "Setting";
            this.Text = "Setting";
            this.panel1.ResumeLayout(false);
            this.MainP.ResumeLayout(false);
            this.MainP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel MainP;
        private System.Windows.Forms.Label listL;
        private System.Windows.Forms.Button VehicleB;
        private System.Windows.Forms.ComboBox CPB;
        private System.Windows.Forms.ComboBox IncompatCB;
        private System.Windows.Forms.ComboBox CompatCB;
        private System.Windows.Forms.ComboBox SignatureCB;
        private System.Windows.Forms.ComboBox VersionCB;
        private System.Windows.Forms.Label SignatureL;
        private System.Windows.Forms.Label CompatL;
        private System.Windows.Forms.Label IncompatL;
        private System.Windows.Forms.Label VsersionL;
        private System.Windows.Forms.Button SaveB;
        private System.Windows.Forms.ComboBox SystemIDCB;
        private System.Windows.Forms.Label SystemIDL;
        private System.Windows.Forms.ComboBox CompIDCB;
        private System.Windows.Forms.Label CompIDL;
    }
}