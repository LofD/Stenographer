namespace WinFormsApp1
{
    partial class OverlayForm
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
            resultLabel = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // resultLabel
            // 
            resultLabel.Dock = DockStyle.Top;
            resultLabel.Font = new Font("Times New Roman", 72F, FontStyle.Bold, GraphicsUnit.Point, 204);
            resultLabel.ForeColor = Color.White;
            resultLabel.Location = new Point(0, 0);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(800, 301);
            resultLabel.TabIndex = 0;
            resultLabel.Text = "Placeholder";
            resultLabel.TextAlign = ContentAlignment.MiddleCenter;
            resultLabel.Click += resultLabel_Click;
            // 
            // OverlayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(resultLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OverlayForm";
            Opacity = 0.8D;
            Text = "Form2";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Load += Form2_Load;
            Click += resultLabel_Click;
            ResumeLayout(false);
        }

        #endregion

        private Label resultLabel;
        private System.Windows.Forms.Timer timer1;
    }
}