namespace WindowsFormsApp1
{
    partial class Form1
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
            this.Hands = new System.Windows.Forms.PictureBox();
            this.Cat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Hands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cat)).BeginInit();
            this.SuspendLayout();
            // 
            // Hands
            // 
            this.Hands.BackColor = System.Drawing.Color.Transparent;
            this.Hands.Location = new System.Drawing.Point(0, 0);
            this.Hands.Name = "Hands";
            this.Hands.Size = new System.Drawing.Size(630, 323);
            this.Hands.TabIndex = 1;
            this.Hands.TabStop = false;
            // 
            // Cat
            // 
            this.Cat.BackColor = System.Drawing.Color.Transparent;
            this.Cat.Location = new System.Drawing.Point(0, 0);
            this.Cat.Name = "Cat";
            this.Cat.Size = new System.Drawing.Size(630, 323);
            this.Cat.TabIndex = 0;
            this.Cat.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 323);
            this.Controls.Add(this.Cat);
            this.Controls.Add(this.Hands);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Hands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cat)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox Hands;
        private System.Windows.Forms.PictureBox Cat;
    }
}

