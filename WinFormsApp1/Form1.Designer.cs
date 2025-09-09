namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            loadModelButton = new Button();
            textBox1 = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            progressBar1 = new ProgressBar();
            button3 = new Button();
            label1 = new Label();
            settingsBox = new GroupBox();
            saveSettingsButton = new Button();
            modelsSelect = new ComboBox();
            toggleRecordButton = new Button();
            settingsBox.SuspendLayout();
            SuspendLayout();
            // 
            // loadModelButton
            // 
            loadModelButton.Location = new Point(12, 12);
            loadModelButton.Name = "loadModelButton";
            loadModelButton.Size = new Size(121, 23);
            loadModelButton.TabIndex = 0;
            loadModelButton.Text = "Load Model";
            loadModelButton.UseVisualStyleBackColor = true;
            loadModelButton.Click += loadModelButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(388, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(400, 292);
            textBox1.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 41);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(370, 23);
            progressBar1.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(388, 310);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(388, 336);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 5;
            label1.Text = "label1";
            // 
            // settingsBox
            // 
            settingsBox.Controls.Add(saveSettingsButton);
            settingsBox.Controls.Add(modelsSelect);
            settingsBox.Location = new Point(12, 151);
            settingsBox.Name = "settingsBox";
            settingsBox.Size = new Size(306, 287);
            settingsBox.TabIndex = 7;
            settingsBox.TabStop = false;
            settingsBox.Text = "Settings";
            // 
            // saveSettingsButton
            // 
            saveSettingsButton.Location = new Point(225, 258);
            saveSettingsButton.Name = "saveSettingsButton";
            saveSettingsButton.Size = new Size(75, 23);
            saveSettingsButton.TabIndex = 8;
            saveSettingsButton.Text = "Save";
            saveSettingsButton.UseVisualStyleBackColor = true;
            saveSettingsButton.Click += saveSettingsButton_Click;
            // 
            // modelsSelect
            // 
            modelsSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            modelsSelect.FormattingEnabled = true;
            modelsSelect.Location = new Point(6, 22);
            modelsSelect.Name = "modelsSelect";
            modelsSelect.Size = new Size(294, 23);
            modelsSelect.TabIndex = 7;
            // 
            // toggleRecordButton
            // 
            toggleRecordButton.Enabled = false;
            toggleRecordButton.Location = new Point(307, 70);
            toggleRecordButton.Name = "toggleRecordButton";
            toggleRecordButton.Size = new Size(75, 23);
            toggleRecordButton.TabIndex = 8;
            toggleRecordButton.Text = "Start";
            toggleRecordButton.UseVisualStyleBackColor = true;
            toggleRecordButton.Click += toggleRecordButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toggleRecordButton);
            Controls.Add(settingsBox);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(loadModelButton);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Minimized;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            Resize += Form1_Resize;
            settingsBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loadModelButton;
        private TextBox textBox1;
        private NotifyIcon notifyIcon1;
        private ProgressBar progressBar1;
        private Button button3;
        private Label label1;
        private GroupBox settingsBox;
        private ComboBox modelsSelect;
        private Button saveSettingsButton;
        private Button toggleRecordButton;
    }
}
