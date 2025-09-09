using NAudio.CoreAudioApi;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using Vosk;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        private const uint MOD_NONE = 0x0000;
        private const uint VK_F4 = 0x73;
        private const int WM_HOTKEY = 0x0312;

        private Model _model;

        private VoskRecognizer _recognizer;
        private WaveInEvent _waveIn;

        private Boolean isModelLoaded = false;
        private Boolean isRecording = false;

        private OverlayForm overlay;
        private string resultText = "";

        public Boolean GetIsRecording()
        {
            return isRecording;
        }

        private void AppendResultText(string text)
        {
            resultText += " " + text;
            OnResultTextUpdate();
        }
        private void ResetText()
        {
            resultText = "";
            OnResultTextUpdate();
        }

        private void OnResultTextUpdate()
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => textBox1.Text = resultText));
            }
            else
            {
                textBox1.Text = resultText;
            }

            overlay.updateText(resultText);
        }

        private void SetIsModelLoaded(Boolean value)
        {
            isModelLoaded = value;
            toggleRecordButton.Enabled = value;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (_recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                string json = _recognizer.Result();
                AppendResultText(JObject.Parse(json)["text"]?.ToString() ?? "");
            }
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            string json = _recognizer.FinalResult();
            AppendResultText(JObject.Parse(json)["text"]?.ToString() ?? "");
            if (textBox1.Text != "")
            {
                Clipboard.SetText(textBox1.Text.Trim());
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        protected async void initVoskModel()
        {
            loadModelButton.Enabled = false;
            SetIsModelLoaded(false);
            string modelName = Properties.Settings.Default.ModelName;
            _model?.Dispose();
            Vosk.Vosk.SetLogLevel(0);
            progressBar1.Style = ProgressBarStyle.Marquee;
            await Task.Run(() => _model = new Model(modelName));
            progressBar1.Style = ProgressBarStyle.Blocks;
            SetIsModelLoaded(true);
            loadModelButton.Enabled = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                ToggleRecording();
            }
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            overlay = new OverlayForm();
            overlay.SetToggleAction(ToggleRecording, GetIsRecording);
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_NONE, VK_F4);

            string path = @".\models";
            modelsSelect.Items.Add("--");
            modelsSelect.SelectedIndex = 0;
            if (Directory.Exists(path))
            {
                string[] folders = Directory.GetDirectories(path);

                foreach (string folder in folders)
                {
                    modelsSelect.Items.Add(folder);
                }
            }

            string modelName = Properties.Settings.Default.ModelName;
            int selectedIndex = modelsSelect.Items.IndexOf(modelName);
            if (selectedIndex != -1)
            {
                modelsSelect.SelectedIndex = selectedIndex;
            }

            initVoskModel();
            Hide();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(1000, "Title", "Text", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //create enumerator
            var enumerator = new MMDeviceEnumerator();
            //cycle through all audio devices
            for (int i = 0; i < WaveIn.DeviceCount; i++)
                label1.Text += i.ToString() + ": " + (enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)[i]);
            //clean up
            enumerator.Dispose();
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ModelName = modelsSelect.SelectedItem?.ToString() ?? "--";
            Properties.Settings.Default.Save();
        }

        private void ToggleRecording()
        {
            if (!isModelLoaded)
            {
                return;
            }

            if (isRecording)
            {
                _waveIn.StopRecording();
                toggleRecordButton.Text = "Start";
                loadModelButton.Enabled = true;
            }
            else
            {
                ResetText();
                loadModelButton.Enabled = false;
                _recognizer?.Dispose();
                _recognizer = new VoskRecognizer(_model, 16000.0f);
                _waveIn?.Dispose();
                _waveIn = new WaveInEvent
                {
                    DeviceNumber = 0,
                    WaveFormat = new WaveFormat(16000, 1),
                    BufferMilliseconds = 100
                };

                _waveIn.DataAvailable += OnDataAvailable;
                _waveIn.RecordingStopped += OnRecordingStopped;
                _waveIn.StartRecording();
                toggleRecordButton.Text = "Stop";
                overlay.ShowOverlay();
            }
            isRecording = !isRecording;
        }

        private void loadModelButton_Click(object sender, EventArgs e)
        {
            initVoskModel();
        }

        private void toggleRecordButton_Click(object sender, EventArgs e)
        {
            if (!isModelLoaded)
            {
                return;
            }
            ToggleRecording();
        }
    }
}
