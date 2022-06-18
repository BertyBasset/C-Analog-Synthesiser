using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Synth.Modules.Sources;
using Synth.Modules.Properties;
using Synth.Utils;


namespace UI {



    public partial class frmSelectFourierCoefficients : Form {
        // Might need to have Phase in here as well ?


        // This will change to an array of array so we can dynamically move between waves
        // Aslo, we may introduce non intergral overtones ??

        // Allow maximum of 10 coefficients, 1 fundamental and up to 9 overtones


        Synth.SynthEngine synth;

        static float[] _Coefficients = new float[] { 0f };

        #region Initiallise
        public static float[] Show(float[] Coefficients) {
            _Coefficients = Coefficients;
            var f = new frmSelectFourierCoefficients();
            f.ShowDialog();
            return _Coefficients;
        }

        public frmSelectFourierCoefficients() {
            InitializeComponent();

            synth = new Synth.SynthEngine(new Synth.Config() { Channels = 2, SampleRate = 16000 });
            synth.Oscillators.Add(new Oscillator(WaveForm.GetByType(WaveformType.Harmonic)));
            if (chkPlayPreview.Checked)
                synth.Start();

            Init();
            AddEventHandlers();
        }

        private void Init() {
            cboPresets.DataSource = UI.Data.FourierCoefficient.GetSampleList(true);




            for (int i = 0; i < _Coefficients.Length; i++) {
                if (i > 9)
                    break;
                switch (i) {
                    case 0: sldH0.Value = (int)(_Coefficients[i] * 100); break;
                    case 1: sldH1.Value = (int)(_Coefficients[i] * 100); break;
                    case 2: sldH2.Value = (int)(_Coefficients[i] * 100); break;
                    case 3: sldH3.Value = (int)(_Coefficients[i] * 100); break;
                    case 4: sldH4.Value = (int)(_Coefficients[i] * 100); break;
                    case 5: sldH5.Value = (int)(_Coefficients[i] * 100); break;
                    case 6: sldH6.Value = (int)(_Coefficients[i] * 100); break;
                    case 7: sldH7.Value = (int)(_Coefficients[i] * 100); break;
                    case 8: sldH8.Value = (int)(_Coefficients[i] * 100); break;
                    case 9: sldH9.Value = (int)(_Coefficients[i] * 100); break;
                    default: break;
                }
            }

            SliderChanged(sldH0, txtH0);
            SliderChanged(sldH1, txtH1);
            SliderChanged(sldH2, txtH2);
            SliderChanged(sldH3, txtH3);
            SliderChanged(sldH4, txtH4);
            SliderChanged(sldH5, txtH5);
            SliderChanged(sldH6, txtH6);
            SliderChanged(sldH7, txtH7);
            SliderChanged(sldH8, txtH8);
            SliderChanged(sldH9, txtH9);

            timPreview.Enabled = true;
        }

        private void AddEventHandlers() {
            cmdSelect.Click += CmdSelect_Click;
            CmdCancel.Click += CmdCancel_Click;

            sldH0.ValueChanged += SldH0_ValueChanged;
            sldH1.ValueChanged += SldH1_ValueChanged;
            sldH2.ValueChanged += SldH2_ValueChanged;
            sldH3.ValueChanged += SldH3_ValueChanged;
            sldH4.ValueChanged += SldH4_ValueChanged;
            sldH5.ValueChanged += SldH5_ValueChanged;
            sldH6.ValueChanged += SldH6_ValueChanged;
            sldH7.ValueChanged += SldH7_ValueChanged;
            sldH8.ValueChanged += SldH8_ValueChanged;
            sldH9.ValueChanged += SldH9_ValueChanged;

            cmdZero0.Click += CmdZero0_Click;
            cmdZero1.Click += CmdZero1_Click;
            cmdZero2.Click += CmdZero2_Click;
            cmdZero3.Click += CmdZero3_Click;
            cmdZero4.Click += CmdZero4_Click;
            cmdZero5.Click += CmdZero5_Click;
            cmdZero6.Click += CmdZero6_Click;
            cmdZero7.Click += CmdZero7_Click;
            cmdZero8.Click += CmdZero8_Click;
            cmdZero9.Click += CmdZero9_Click;

            txtH0.Click += (o, e) => txtH0.SelectAll();
            txtH1.Click += (o, e) => txtH1.SelectAll();
            txtH2.Click += (o, e) => txtH2.SelectAll();
            txtH3.Click += (o, e) => txtH3.SelectAll();
            txtH4.Click += (o, e) => txtH4.SelectAll();
            txtH5.Click += (o, e) => txtH5.SelectAll();
            txtH6.Click += (o, e) => txtH6.SelectAll();
            txtH7.Click += (o, e) => txtH7.SelectAll();
            txtH8.Click += (o, e) => txtH8.SelectAll();
            txtH9.Click += (o, e) => txtH9.SelectAll();
            txtH0.GotFocus += (o, e) => txtH0.SelectAll();
            txtH1.GotFocus += (o, e) => txtH1.SelectAll();
            txtH2.GotFocus += (o, e) => txtH2.SelectAll();
            txtH3.GotFocus += (o, e) => txtH3.SelectAll();
            txtH4.GotFocus += (o, e) => txtH4.SelectAll();
            txtH5.GotFocus += (o, e) => txtH5.SelectAll();
            txtH6.GotFocus += (o, e) => txtH6.SelectAll();
            txtH7.GotFocus += (o, e) => txtH7.SelectAll();
            txtH8.GotFocus += (o, e) => txtH8.SelectAll();
            txtH9.GotFocus += (o, e) => txtH9.SelectAll();

            txtH0.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH1.Focus(); };
            txtH1.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH2.Focus(); };
            txtH2.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH3.Focus(); };
            txtH3.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH4.Focus(); };
            txtH4.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH5.Focus(); };
            txtH5.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH6.Focus(); };
            txtH6.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH7.Focus(); };
            txtH7.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH8.Focus(); };
            txtH8.KeyPress += (o, e) => { if (e.KeyChar == '\r') txtH9.Focus(); };
            txtH9.KeyPress += (o, e) => { if (e.KeyChar == '\r') cmdZero0.Focus(); };

            txtH0.LostFocus += (o, e) => TextChanged(txtH0, sldH0);
            txtH1.LostFocus += (o, e) => TextChanged(txtH1, sldH1);
            txtH2.LostFocus += (o, e) => TextChanged(txtH2, sldH2);
            txtH3.LostFocus += (o, e) => TextChanged(txtH3, sldH3);
            txtH4.LostFocus += (o, e) => TextChanged(txtH4, sldH4);
            txtH5.LostFocus += (o, e) => TextChanged(txtH5, sldH5);
            txtH6.LostFocus += (o, e) => TextChanged(txtH6, sldH6);
            txtH7.LostFocus += (o, e) => TextChanged(txtH7, sldH7);
            txtH8.LostFocus += (o, e) => TextChanged(txtH8, sldH8);
            txtH9.LostFocus += (o, e) => TextChanged(txtH9, sldH9);

            timPreview.Tick += (o, e) => { timPreview.Enabled = false;  UpdateWaveForm(); };

            this.FormClosing += (o, e) => { synth.Stop(); };
            chkPlayPreview.CheckedChanged += (o, e) => { if (chkPlayPreview.Checked) synth.Start(); else synth.Stop(); };

            cboPresets.SelectedIndexChanged += CboPresets_SelectedIndexChanged;

        }


   
        #endregion

        #region Event Handlers
        private void CboPresets_SelectedIndexChanged(object? sender, EventArgs e)         {
            if (cboPresets.SelectedIndex <= 0)
                return;

            var sample = (Data.FourierCoefficient)cboPresets.SelectedItem;

            sldH0.Value = (int)(sample.Coefficients[0] * 100f);
            sldH1.Value = (int)(sample.Coefficients[1] * 100f);
            sldH2.Value = (int)(sample.Coefficients[2] * 100f);
            sldH3.Value = (int)(sample.Coefficients[3] * 100f);
            sldH4.Value = (int)(sample.Coefficients[4] * 100f);
            sldH5.Value = (int)(sample.Coefficients[5] * 100f);
            sldH6.Value = (int)(sample.Coefficients[6] * 100f);
            sldH7.Value = (int)(sample.Coefficients[7] * 100f);
            sldH8.Value = (int)(sample.Coefficients[8] * 100f);
            sldH9.Value = (int)(sample.Coefficients[9] * 100f);

            UpdateWaveForm();
        }

        private void CmdZero0_Click(object? sender, EventArgs e) {
            sldH0.Value = 0;
        }

        private void SldH0_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH0, txtH0);
            UpdateWaveForm();
        }

        private void CmdZero1_Click(object? sender, EventArgs e) {
            sldH1.Value = 0;
        }

        private void SldH1_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH1, txtH1);
            UpdateWaveForm();
        }

        private void CmdZero2_Click(object? sender, EventArgs e) {
            sldH2.Value = 0;
        }

        private void SldH2_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH2, txtH2);
            UpdateWaveForm();
        }

        private void CmdZero3_Click(object? sender, EventArgs e) {
            sldH3.Value = 0;
        }

        private void SldH3_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH3, txtH3);
            UpdateWaveForm();
        }

        private void CmdZero4_Click(object? sender, EventArgs e) {
            sldH4.Value = 0;
        }

        private void SldH4_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH4, txtH4);
            UpdateWaveForm();
        }

        private void CmdZero5_Click(object? sender, EventArgs e) {
            sldH5.Value = 0;
        }

        private void SldH5_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH5, txtH5);
            UpdateWaveForm();
        }

        private void CmdZero6_Click(object? sender, EventArgs e) {
            sldH6.Value = 0;
        }

        private void SldH6_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH6, txtH6);
            UpdateWaveForm();
        }

        private void CmdZero7_Click(object? sender, EventArgs e) {
            sldH7.Value = 0;
        }

        private void SldH7_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH7, txtH7);
            UpdateWaveForm();
        }

        private void CmdZero8_Click(object? sender, EventArgs e) {
            sldH8.Value = 0;
        }

        private void SldH8_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH8, txtH8);
            UpdateWaveForm();
        }

        private void CmdZero9_Click(object? sender, EventArgs e) {
            sldH9.Value = 0;
        }

        private void SldH9_ValueChanged(object? sender, EventArgs e) {
            SliderChanged(sldH9, txtH9);
            UpdateWaveForm();
        }


        private void CmdCancel_Click(object? sender, EventArgs e) {
            //_Coefficients = null;
            _Coefficients = new float[10];      // Might need to resurrect previous line
            this.Close();
        }

        private void CmdSelect_Click(object? sender, EventArgs e) {
            // Set _Coefficients
            _Coefficients = getArray();

     
            this.Close();
        }

        float[] getArray() { 
            float[] arr = new float[10];
            arr[0] = sldH0.Value / 100f;
            arr[1] = sldH1.Value / 100f;
            arr[2] = sldH2.Value / 100f;
            arr[3] = sldH3.Value / 100f;
            arr[4] = sldH4.Value / 100f;
            arr[5] = sldH5.Value / 100f;
            arr[6] = sldH6.Value / 100f;
            arr[7] = sldH7.Value / 100f;
            arr[8] = sldH8.Value / 100f;
            arr[9] = sldH9.Value / 100f;

            return arr;
        }


        #endregion

        #region Private Methods
        private void SliderChanged(TrackBar slider, TextBox textBox) {
            textBox.Text = (slider.Value / 100f).ToString("0.00");
            textBox.Focus();
            textBox.SelectAll();
        }

        private new void TextChanged(TextBox textBox, TrackBar slider) {
            float value;
            if (float.TryParse(textBox.Text, out value)) {
                if (value > 1)
                    value = 1;
                if (value < -1)
                    value = -1;
                int val = (int)(value * 100);
                if (val > 100)
                    val = 100;
                if (val < -100)
                    val = -100;
                slider.Value = val;
                textBox.Text = value.ToString("0.00");
            } else {
                slider.Value = 0;
                textBox.Text = 0.ToString("0.00");
            }
        }

        private void UpdateWaveForm() {
            synth.Oscillators[0].FourierCoefficients = getArray();



            var p = new Pen(Color.Green);

            Graphics g = picPreview.CreateGraphics();
            Point[] points = new Point[picPreview.Width];
            for (int i = 0; i < picPreview.Width; i++) {
                float y = 0;
                float phaseRads = ((720f / picPreview.Width) * i)*(float)Math.PI / 180f ;

                y += (float)Math.Sin(phaseRads) * sldH0.Value /100f;
                y += (float)Math.Sin(phaseRads * 2f) * sldH1.Value / 100f;
                y += (float)Math.Sin(phaseRads * 3f) * sldH2.Value / 100f;
                y += (float)Math.Sin(phaseRads * 4f) * sldH3.Value / 100f;
                y += (float)Math.Sin(phaseRads * 5f) * sldH4.Value / 100f;
                y += (float)Math.Sin(phaseRads * 6f) * sldH5.Value / 100f;
                y += (float)Math.Sin(phaseRads * 7f) * sldH6.Value / 100f;
                y += (float)Math.Sin(phaseRads * 8f) * sldH7.Value / 100f;
                y += (float)Math.Sin(phaseRads * 9f) * sldH8.Value / 100f;
                y += (float)Math.Sin(phaseRads * 10f) * sldH9.Value / 100f;

                points[i] = new Point(i, (int)(y * picPreview.Height * .3f + picPreview.Height / 2));
            }

            g.Clear(Color.Black);
            g.DrawLines(p, points);
        }
        #endregion
    }
    
}
