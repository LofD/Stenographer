using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class OverlayForm : Form
    {

        private Action ToggleRecording;
        private Func<Boolean> GetIsRecording;
        public OverlayForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        public void updateText(string text)
        {
            if (resultLabel.InvokeRequired)
            {
                this.Invoke(new Action(() => resultLabel.Text = text));
            }
            else
            {
                resultLabel.Text = text;
            }
        }

        public void SetToggleAction(Action ToggleRecording, Func<Boolean> GetIsRecording)
        {
            this.ToggleRecording = ToggleRecording;
            this.GetIsRecording = GetIsRecording;
        }

        private void resultLabel_Click(object sender, EventArgs e)
        {
            if (GetIsRecording())
            {
                ToggleRecording();
            }
            Hide();
        }

        public void ShowOverlay()
        {
            resultLabel.Text = "";
            Show();
        }
    }
}
