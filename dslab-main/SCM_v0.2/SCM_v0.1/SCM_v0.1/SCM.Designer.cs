
namespace xplane_data_test
{
    partial class SCM
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProtocolKL = new System.Windows.Forms.Label();
            this.ProtocolBox = new System.Windows.Forms.ComboBox();
            this.Start_B = new System.Windows.Forms.Button();
            this.End_B = new System.Windows.Forms.Button();
            this.SetB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProtocolKL
            // 
            this.ProtocolKL.AutoSize = true;
            this.ProtocolKL.Font = new System.Drawing.Font("굴림", 15F);
            this.ProtocolKL.Location = new System.Drawing.Point(39, 21);
            this.ProtocolKL.Name = "ProtocolKL";
            this.ProtocolKL.Size = new System.Drawing.Size(230, 30);
            this.ProtocolKL.TabIndex = 0;
            this.ProtocolKL.Text = "Protocol Kind : ";
            // 
            // ProtocolBox
            // 
            this.ProtocolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProtocolBox.Font = new System.Drawing.Font("굴림", 15F);
            this.ProtocolBox.FormattingEnabled = true;
            this.ProtocolBox.Items.AddRange(new object[] {
            "STANAG 4586",
            "MAVLink",
            "MSP"});
            this.ProtocolBox.Location = new System.Drawing.Point(275, 21);
            this.ProtocolBox.Name = "ProtocolBox";
            this.ProtocolBox.Size = new System.Drawing.Size(239, 38);
            this.ProtocolBox.TabIndex = 1;
            this.ProtocolBox.SelectedIndexChanged += new System.EventHandler(this.ProtocolBox_SelectedIndexChanged);
            // 
            // Start_B
            // 
            this.Start_B.Location = new System.Drawing.Point(564, 22);
            this.Start_B.Name = "Start_B";
            this.Start_B.Size = new System.Drawing.Size(147, 38);
            this.Start_B.TabIndex = 2;
            this.Start_B.Text = "작동 시작";
            this.Start_B.UseVisualStyleBackColor = true;
            this.Start_B.Click += new System.EventHandler(this.Start_B_Click);
            // 
            // End_B
            // 
            this.End_B.Location = new System.Drawing.Point(564, 21);
            this.End_B.Name = "End_B";
            this.End_B.Size = new System.Drawing.Size(147, 38);
            this.End_B.TabIndex = 3;
            this.End_B.Text = "작동 종료";
            this.End_B.UseVisualStyleBackColor = true;
            this.End_B.Visible = false;
            this.End_B.Click += new System.EventHandler(this.End_B_Click);
            // 
            // SetB
            // 
            this.SetB.Location = new System.Drawing.Point(749, 24);
            this.SetB.Name = "SetB";
            this.SetB.Size = new System.Drawing.Size(147, 36);
            this.SetB.TabIndex = 5;
            this.SetB.Text = "설정";
            this.SetB.UseVisualStyleBackColor = true;
            this.SetB.Click += new System.EventHandler(this.Setting_Click);
            // 
            // SCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 607);
            this.Controls.Add(this.SetB);
            this.Controls.Add(this.End_B);
            this.Controls.Add(this.Start_B);
            this.Controls.Add(this.ProtocolBox);
            this.Controls.Add(this.ProtocolKL);
            this.Name = "SCM";
            this.Text = " SCM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ProtocolKL;
        private System.Windows.Forms.ComboBox ProtocolBox;
        private System.Windows.Forms.Button Start_B;
        private System.Windows.Forms.Button End_B;
        private System.Windows.Forms.Button SetB;
    }
}

