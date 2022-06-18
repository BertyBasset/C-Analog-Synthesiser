using Microsoft.Extensions.Configuration;
using Synth;
using Synth.Modules.Sources;
using Synth.Modules.Properties;
using System.Diagnostics;


/* To Do:
 *  Oscillator selector                                                 DONE
 *  Duty                                                                DONE
 *  Modify Fine Tune to go up/down one semitone                         DONE
 *  Live Graph                                                          DONE
 *  WaveTable file select                                               DONE
 *  Harmonic Oscillator                                                 DONE
 *  UI for Fourier Coeffs                                               DONE
 *  Preview for Wavetable and Fourier                                   DONE
 *  Fourier dropdown                                                    DONE
 *  Keyboard control + keystroke trigger                                DONE
 *  Re-investigate super-saw with multiple phase gens - Go 7 oscs!!     DONE
 *  Get more wavetables                                                 DONE
 *  Keyboard class (Synth library not UI) i.e. all oscs share the same keyboard object
 *    Portamento
 *    Midi in
 *  Separate Tune and Fine Tune properties
 *  Patches
 *  Phase Distortion
 *  Sync
 *  Maybe use Naudio pot controls ?         Copy to Github first
 *  FM
 *  Live FFT
 *  Bit Crush/Distortion/Overdrive
 * */





namespace UI {
    public partial class SimpleTest : Form {
        #region Init Synth
        Synth.SynthEngine synth;

        List<Synth.Utils.Note> _NoteList = Synth.Utils.Note.GetNoteList();

        void InitSynth() {
            // Get Synth module config, and inject into constructor
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            var synthConfig = config.GetSection("Synth").Get<Synth.Config>();
            synth = new Synth.SynthEngine(synthConfig);



            synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 1f});
            synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 0f });
            synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 0f });


        }
        #endregion

        #region Init Form
        public SimpleTest() {

            InitializeComponent();

            InitUI();
            AddEventHandlers();

            InitSynth();

            Debug.Assert(synth != null);

            lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
            lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
            lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);

            SetToolTips();

            timDisplay.Enabled = true;
            keyboard.Focus();
        }

        private void SetToolTips() {
            toolTip1.SetToolTip(picSine, "Sine Wave");
            toolTip1.SetToolTip(picSaw, "Saw Wave");
            toolTip1.SetToolTip(picTriangle, "Triangle Wave");
            toolTip1.SetToolTip(picSquare, "Square Wave");
            toolTip1.SetToolTip(picNoise, "White Noise");
            toolTip1.SetToolTip(picWaveTable, "Wave Table");
            toolTip1.SetToolTip(picHarmonic, "Harmonics");
            toolTip1.SetToolTip(picSuperSaw, "Super Saw");
            toolTip1.SetToolTip(picSine1, "Sine Wave");
            toolTip1.SetToolTip(picSaw1, "Saw Wave");
            toolTip1.SetToolTip(picTriangle1, "Triangle Wave");
            toolTip1.SetToolTip(picSquare1, "Square Wave");
            toolTip1.SetToolTip(picNoise1, "White Noise");
            toolTip1.SetToolTip(picWaveTable1, "Wave Table");
            toolTip1.SetToolTip(picHarmonic1, "Harmonics");
            toolTip1.SetToolTip(picSuperSaw1, "Super Saw");
            toolTip1.SetToolTip(picSine2, "Sine Wave");
            toolTip1.SetToolTip(picSaw2, "Saw Wave");
            toolTip1.SetToolTip(picTriangle2, "Triangle Wave");
            toolTip1.SetToolTip(picSquare2, "Square Wave");
            toolTip1.SetToolTip(picNoise2, "White Noise");
            toolTip1.SetToolTip(picWaveTable2, "Wave Table");
            toolTip1.SetToolTip(picHarmonic2, "Harmonics");
            toolTip1.SetToolTip(picSuperSaw2, "Super Saw");
        }

        private void InitUI() {
            ddlNote.DataSource = _NoteList;
            ddlNote.SelectedIndex = 24;     // Go to A2

            
            ddlSuperSaw.DataSource = Data.SuperSaw.GetSampleList(true);
            ddlSuperSaw1.DataSource = Data.SuperSaw.GetSampleList(true);
            ddlSuperSaw2.DataSource = Data.SuperSaw.GetSampleList(true);
        }

        private void AddEventHandlers() {
            ddlNote.SelectedIndexChanged += DdlNote_SelectedIndexChanged;

            sldWaveForm.ValueChanged += SldWaveForm_ValueChanged;
            sldOctave.ValueChanged += SldOctave_ValueChanged;
            sldTune.ValueChanged += SldTune_ValueChanged;
            sldFineTune.ValueChanged += SldTune_ValueChanged;
            cmdReset.Click += CmdReset_Click;
            sldPWM.ValueChanged += SldPWM_ValueChanged;
            sldLevel.ValueChanged += SldLevel_ValueChanged;
            ddlSuperSaw.SelectedIndexChanged += DdlSuperSaw_SelectedIndexChanged;

            sldWaveForm1.ValueChanged += SldWaveForm1_ValueChanged;
            sldOctave1.ValueChanged += SldOctave1_ValueChanged;
            sldTune1.ValueChanged += SldTune1_ValueChanged;
            sldFineTune1.ValueChanged += SldTune1_ValueChanged;
            cmdReset1.Click += CmdReset1_Click;
            sldPWM1.ValueChanged += SldPWM1_ValueChanged;
            sldLevel1.ValueChanged += SldLevel1_ValueChanged;
            ddlSuperSaw1.SelectedIndexChanged += DdlSuperSaw1_SelectedIndexChanged;

            sldWaveForm2.ValueChanged += SldWaveForm2_ValueChanged;
            sldOctave2.ValueChanged += SldOctave2_ValueChanged;
            sldTune2.ValueChanged += SldTune2_ValueChanged;
            sldFineTune2.ValueChanged += SldTune2_ValueChanged;
            cmdReset2.Click += CmdReset2_Click;
            sldPWM2.ValueChanged += SldPWM2_ValueChanged;
            sldLevel2.ValueChanged += SldLevel2_ValueChanged;
            ddlSuperSaw2.SelectedIndexChanged += DdlSuperSaw2_SelectedIndexChanged;

            // Refresh display every 2s
            timDisplay.Tick += TimDisplay_Tick;
            cmdPauseGraph.Click += CmdPauseGraph_Click;

            cmdSelectOscSetting.Click += CmdOsctSetting_Click;
            cmdSelectOscSetting1.Click += CmdOsctSetting1_Click;
            cmdSelectOscSetting2.Click += CmdOsctSetting2_Click;

            keyboard.NoteChanged += keyboard_NoteChanged;
            keyboard.KeyStateChanged += keyboard_KeyStateChanged;

            this.KeyUp += SimpleTest_KeyUp;

        }


        #endregion

        #region Protected Overrides
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            // This captures keys for the entire form. We can process, or let the base method handle
            // Capture following keys. Only set note for now. We'll worry about keypress later
            //    
            //    
            //    a   s       f   g   h       k   l        '
            //  \   z   x   c   v   b   n   m   ,   .   /
            //  C2  D2  E2  F2  G2  A3  B3  C3  D3  E3  F3 

     
            Synth.Utils.Note? note = null;
            switch (keyData) {
                case Keys.OemPipe:
                    note = Synth.Utils.Note.GetByDesc("C2"); break;
                case Keys.A:
                    note = Synth.Utils.Note.GetByDesc("C♯2/D♭2"); break;
                case Keys.Z:
                    note = Synth.Utils.Note.GetByDesc("D2"); break;
                case Keys.S:
                    note = Synth.Utils.Note.GetByDesc("D♯2E♭2"); break;
                case Keys.X:
                    note = Synth.Utils.Note.GetByDesc("E2"); break;
                case Keys.C:
                    note = Synth.Utils.Note.GetByDesc("F2"); break;
                case Keys.F:
                    note = Synth.Utils.Note.GetByDesc("F♯2G♭2"); break;
                case Keys.V:
                    note = Synth.Utils.Note.GetByDesc("G2"); break;
                case Keys.G:
                    note = Synth.Utils.Note.GetByDesc("G♯2A♭2"); break;
                case Keys.B:
                    note = Synth.Utils.Note.GetByDesc("A3"); break;
                case Keys.H:
                    note = Synth.Utils.Note.GetByDesc("A♯3B♭3"); break;
                case Keys.N:
                    note = Synth.Utils.Note.GetByDesc("B3"); break;
                case Keys.M:
                    note = Synth.Utils.Note.GetByDesc("C3"); break;
                case Keys.K:
                    note = Synth.Utils.Note.GetByDesc("C♯3/D♭3"); break;
                case Keys.Oemcomma:
                    note = Synth.Utils.Note.GetByDesc("D3"); break;
                case Keys.L:
                    note = Synth.Utils.Note.GetByDesc("D♯3E♭3"); break;
                case Keys.OemPeriod:
                    note = Synth.Utils.Note.GetByDesc("E3"); break;
                case Keys.OemQuestion:
                    note = Synth.Utils.Note.GetByDesc("F3"); break;
                case Keys.Oem3:
                    note = Synth.Utils.Note.GetByDesc("F♯3G♭3"); break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            keyboard.CurrentNote = note;
            keyboard.CurrentKeyState = Keyboard.KeyState.Down;
            return true;
        }
        #endregion

        #region Event Handlers
        private void keyboard_KeyStateChanged(object? sender, EventArgs e) {
            // We don't do anything with this at the moment
            // We will when we introduce a Gate
        }

        private void keyboard_NoteChanged(object? sender, EventArgs e) {
            ddlNote.SelectedIndex = keyboard.CurrentNote.ID - 1;
        }

        private void CmdPauseGraph_Click(object? sender, EventArgs e) {
            timDisplay.Enabled = !timDisplay.Enabled;
            if (timDisplay.Enabled)
                cmdPauseGraph.Text = "Pause";
            else
                cmdPauseGraph.Text = "Show";
        }

        private void TimDisplay_Tick(object? sender, EventArgs e) {
            timDisplay.Enabled = false;
            DrawDisplay();
            timDisplay.Enabled = true;
        }

        private void DdlNote_SelectedIndexChanged(object? sender, EventArgs e) {
            foreach (var o in synth.Oscillators)
                o.Frequency.Note = (Synth.Utils.Note)ddlNote.SelectedItem;

            lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
            lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
            lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
        }

        private void SldWaveForm_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[0].WaveFormSelectByID(sldWaveForm.Value);
            lblWaveform.Text = (WaveForm.GetByID(sldWaveForm.Value)).Name;

            lblWaveTable.Visible = sldWaveForm.Value == (int)WaveformType.WaveTable;
            cmdSelectOscSetting.Enabled = sldWaveForm.Value == (int)WaveformType.WaveTable || sldWaveForm.Value == (int)WaveformType.Harmonic;
            ddlSuperSaw.Visible = sldWaveForm.Value == (int)WaveformType.SuperSaw;
        }

        private void SldOctave_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[0].Frequency.Octave = sldOctave.Value;
            lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
        }

        private void SldTune_ValueChanged(object? sender, EventArgs e) {
            // sldFineTune value -100 to +100, so for +/- 1 semitone:  Value / 1200f
            synth.Oscillators[0].Frequency.Tune = (float)sldFineTune.Value / 1200f + (float)sldTune.Value / 12f;
            lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
        }
        private void CmdReset_Click(object? sender, EventArgs e) {
            sldFineTune.Value = 0;
        }

        private void SldPWM_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[0].Duty = (float)sldPWM.Value / 100f;
        }

        private void SldLevel_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[0].Amplitude = sldLevel.Value / 100f;
        }
        private void CmdOsctSetting_Click(object? sender, EventArgs e) {
            bool originalSynthStateStarted = synth.Started;
            synth.Stop();

            switch ((WaveformType)sldWaveForm.Value) {
                case WaveformType.WaveTable:
                    var fileName = frmSelectWavetable.Show(synth.Oscillators[0].WaveTableFileName);
                    if (fileName != "") {
                        lblWaveTable.Text = truncateFileName(fileName, 14);
                        synth.Oscillators[0].WaveTableFileName = fileName;
                    }
                    break;
                case WaveformType.Harmonic:
                    var coefficients = frmSelectFourierCoefficients.Show(synth.Oscillators[0].FourierCoefficients);
                    if (!coefficients.All(c => c==0)) {         // All coeffs 0 is Cancel pressed
                        synth.Oscillators[0].FourierCoefficients = coefficients;
                    }
                    break;
                default: break;  // Do Nothing
            }

            if (originalSynthStateStarted)
                synth.Start();
        }

        private void DdlSuperSaw_SelectedIndexChanged(object? sender, EventArgs e) {
            if (ddlSuperSaw.SelectedIndex <= 0)
                return;

            var sawSettings = (Data.SuperSaw)ddlSuperSaw.SelectedItem;
            synth.Oscillators[0].FrequencyRatios = sawSettings.FrequencyRatios;
        }


        private void SldWaveForm1_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[1].WaveFormSelectByID(sldWaveForm1.Value);
            lblWaveform1.Text = (WaveForm.GetByID(sldWaveForm1.Value)).Name;

            lblWaveTable1.Visible = sldWaveForm1.Value == (int)WaveformType.WaveTable;
            cmdSelectOscSetting1.Enabled = sldWaveForm1.Value == (int)WaveformType.WaveTable || sldWaveForm1.Value == (int)WaveformType.Harmonic;
            ddlSuperSaw1.Visible = sldWaveForm1.Value == (int)WaveformType.SuperSaw;
        }

        private void SldOctave1_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[1].Frequency.Octave = sldOctave1.Value;
            lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
        }
        private void SldTune1_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[1].Frequency.Tune = (float)sldFineTune1.Value / 1200f + (float)sldTune1.Value / 12f;
            lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
        }
        private void CmdReset1_Click(object? sender, EventArgs e) {
            sldFineTune1.Value = 0;
        }

        private void SldPWM1_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[1].Duty = (float)sldPWM1.Value / 100f;
        }
        private void SldLevel1_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[1].Amplitude = sldLevel1.Value / 100f;
        }

        private void CmdOsctSetting1_Click(object? sender, EventArgs e) {
            bool originalSynthStateStarted = synth.Started;
            synth.Stop();

            switch ((WaveformType)sldWaveForm1.Value) {
                case WaveformType.WaveTable:
                    var fileName = frmSelectWavetable.Show(synth.Oscillators[1].WaveTableFileName);
                    if (fileName != "") {
                        lblWaveTable1.Text = truncateFileName(fileName, 14);
                        synth.Oscillators[1].WaveTableFileName = fileName;
                    }
                    break;
                case WaveformType.Harmonic:
                    var coefficients = frmSelectFourierCoefficients.Show(synth.Oscillators[1].FourierCoefficients);
                    if (!coefficients.All(c => c == 0)) {               // All coeffs 0 is Cancel pressed
                        synth.Oscillators[1].FourierCoefficients = coefficients;
                    }
                    break;
                default: break;  // Do Nothing
            }

            if (originalSynthStateStarted)
                synth.Start();
        }

        private void DdlSuperSaw1_SelectedIndexChanged(object? sender, EventArgs e) {
            if (ddlSuperSaw1.SelectedIndex <= 0)
                return;

            var sawSettings = (Data.SuperSaw)ddlSuperSaw1.SelectedItem;
            synth.Oscillators[1].FrequencyRatios = sawSettings.FrequencyRatios;
        }


        private void SldWaveForm2_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[2].WaveFormSelectByID(sldWaveForm2.Value);
            lblWaveform2.Text = (WaveForm.GetByID(sldWaveForm2.Value)).Name;

            lblWaveTable2.Visible = sldWaveForm2.Value == (int)WaveformType.WaveTable;
            cmdSelectOscSetting2.Enabled = sldWaveForm2.Value == (int)WaveformType.WaveTable || sldWaveForm2.Value == (int)WaveformType.Harmonic;
            ddlSuperSaw2.Visible = sldWaveForm2.Value == (int)WaveformType.SuperSaw;
        }

        private void SldOctave2_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[2].Frequency.Octave = sldOctave2.Value;
            lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
        }

        private void SldTune2_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[2].Frequency.Tune = (float)sldFineTune2.Value / 1200f + (float)sldTune2.Value / 12f; ;
            lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
        }
        private void CmdReset2_Click(object? sender, EventArgs e) {
            sldFineTune2.Value = 0;
        }

        private void SldPWM2_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[2].Duty = (float)sldPWM2.Value / 100f;
        }
        private void SldLevel2_ValueChanged(object? sender, EventArgs e) {
            synth.Oscillators[2].Amplitude = sldLevel2.Value / 100f;
        }

        private void CmdOsctSetting2_Click(object? sender, EventArgs e) {
            bool originalSynthStateStarted = synth.Started;
            synth.Stop();

            switch ((WaveformType)sldWaveForm2.Value) {
                case WaveformType.WaveTable:
                    var fileName = frmSelectWavetable.Show(synth.Oscillators[2].WaveTableFileName);
                    if (fileName != "") {
                        lblWaveTable2.Text = truncateFileName(fileName, 14);
                        synth.Oscillators[2].WaveTableFileName = fileName;
                    }
                    break;
                case WaveformType.Harmonic:
                    var coefficients = frmSelectFourierCoefficients.Show(synth.Oscillators[2].FourierCoefficients);
                    if (!coefficients.All(c => c == 0)) {                   // All coeffs 0 is Cancel pressed
                        synth.Oscillators[2].FourierCoefficients = coefficients;
                    }
                    break;
                default: break;  // Do Nothing
            }

            if (originalSynthStateStarted)
                synth.Start();
        }
        private void DdlSuperSaw2_SelectedIndexChanged(object? sender, EventArgs e) {
            if (ddlSuperSaw2.SelectedIndex <= 0)
                return;

            var sawSettings = (Data.SuperSaw)ddlSuperSaw2.SelectedItem;
            synth.Oscillators[2].FrequencyRatios = sawSettings.FrequencyRatios;
        }


        // Global 'Key Up' handler. We've set Form KeyPreview property to true so that form rather than controls can capture this event
        private void SimpleTest_KeyUp(object? sender, KeyEventArgs e) {
            keyboard.CurrentKeyState = Keyboard.KeyState.Up;
        }
        #endregion

        #region Private Methods
        string FormatFrequency(float frequency) {
            if (frequency < 1000)
                return $"{frequency.ToString("0.##")} Hz";
            else
                return $"{(frequency/ 1000).ToString("0.##")} kHz";

        }
        string truncateFileName(string fileName, int Length) {
            fileName.Substring(fileName.LastIndexOf('\\') + 1);

            if (fileName.Length <= Length)
                return fileName;

            fileName = fileName.Substring(fileName.Length - Length);
            return ".." + fileName;
        }
        #endregion

        #region Graph Display
        private void DrawDisplay() {
            var p = new Pen(Color.Green);

            var graphData = synth.GetGraphData(picWaveGraph.Width);

            Graphics g = picWaveGraph.CreateGraphics();
            Point[] points = new Point[graphData.Length];
            for(int i = 0; i < graphData.Length; i++) {
                points[i] = new Point(i, (int)(graphData[i] * picWaveGraph.Height* .9f + picWaveGraph.Height / 2));
            }

            g.Clear(Color.Black);
            g.DrawLines(p, points);

            graphData = null;           // Nullify these to ensure they get caught by GC
        }

        #endregion

        #region Stop Start
        private void cmdStart_Click(object sender, EventArgs e) {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
            synth.Start();

        }

        private void cmdStop_Click(object sender, EventArgs e) {
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;

            synth.Stop();
        }
        #endregion

    }
}