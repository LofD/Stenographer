using NAudio.CoreAudioApi;
using NAudio.Gui;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using Vosk;

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
        private const uint VK_F9 = 0x78;
        private const int WM_HOTKEY = 0x0312;

        private Model _model;
        private VoskRecognizer _recognizer;
        private WaveInEvent _waveIn;
        private WaveFileWriter _writer;

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            // TODO: remove this line if you don't want to save the audio to a file
            _writer.Write(e.Buffer, 0, e.BytesRecorded);


            if (_recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                string json = _recognizer.Result();
                string text = JObject.Parse(json)["text"]?.ToString();
                AppendText("👂 " + text + "\r\n");
            }
        }

        private void AppendText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => textBox1.AppendText(text)));
            }
            else
            {
                textBox1.AppendText(text);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        [HandleProcessCorruptedStateExceptions]
        protected async void initVoskModel()
        {
            string modelName = Properties.Settings.Default.ModelName;
            try
            {
                Vosk.Vosk.SetLogLevel(0);

                //_model = new Model("big-model"); // путь к папке с моделью


                progressBar1.Style = ProgressBarStyle.Marquee;
                await Task.Run(() => _model = new Model(modelName));

                

                progressBar1.Style = ProgressBarStyle.Blocks;

                _recognizer = new VoskRecognizer(_model, 16000.0f);

                _waveIn = new WaveInEvent
                {
                    DeviceNumber = 0,
                    WaveFormat = new WaveFormat(16000, 1),
                    BufferMilliseconds = 50
                };

                _waveIn.DataAvailable += OnDataAvailable;
                _writer = new WaveFileWriter("test.wav", _waveIn.WaveFormat);
                _waveIn.StartRecording();

                AppendText("Началось распознавание...\r\n");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch  (AccessViolationException ex)
            {
                MessageBox.Show("Ошибка доступа к модели: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                // Здесь твое действие при нажатии F9
                MessageBox.Show("F9 нажата!");
            }
            base.WndProc(ref m);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            initVoskModel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _waveIn?.StopRecording();
            _waveIn?.Dispose();
            _recognizer?.Dispose();
            _model?.Dispose();
            _writer?.Dispose();

            Clipboard.SetText(textBox1.Text);

            AppendText("Распознавание остановлено.\r\n");
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_NONE, VK_F9);


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
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(1000, "Программа свернута", "Работает в фоновом режиме", ToolTipIcon.Info);
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
    }
}
