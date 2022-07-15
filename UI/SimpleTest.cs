using Microsoft.Extensions.Configuration;
using Synth;
using Synth.Modules.Sources;
using Synth.Modules.Properties;
using System.Diagnostics;
using FftSharp;


/* To Do:
 *  Oscillator selector                                                  DONE
 *  Duty                                                                 DONE
 *  Modify Fine Tune to go up/down one semitone                          DONE
 *  Live Graph                                                           DONE
 *  WaveTable file select                                                DONE
 *  Harmonics Oscillator                                                 DONE
 *  UI for Fourier Coeffs                                                DONE
 *  Preview for Wavetable and Fourier                                    DONE
 *  Fourier dropdown                                                     DONE
 *  Keyboard control + keystroke trigger                                 DONE
 *  Re-investigate super-saw with multiple phase gens - Go 7 oscs!!      DONE
 *  Get more wavetables                                                  DONE
 *  Put public Note property in SynthEngine i.e. all oscs share the note DONE
 *  Separate Tune and Fine Tune properties                               DONE
 *  Rename keyboard virtual keyboard control                             DONE
 *  Move key events into virtual keyboard control                        DONE
 *  Live FFT                                                             DONE
 *  Sync                                                                 DONE
 *  FM                                                                   DONE
 *  Phase Distortion                                                     DONE
 *  Add KBD switch                                                       DONE
 *  Change Sync and FM from Dest to Source                               DONE
 *  Delete Patch                                                         DONE
 *  Save Patch                                                           DONE
 *  Select newly saved patch in ddl                                      DONE
 *  Sort patch on load                                                   DONE    
 *  Include WaveTable and Harmonics in patch save/recall                 DONE
 *  Weird wavtable filename bug - old dodge patches                      DONE
 *  Midi In                                                              DONE
 *  See how to plug in +/-1 (PW range) modulation into Phase Distprtion  DONE does it anyway
 *  Double check sine wave distortion - looks wrong                      DONE
 *  Oscillators Write Up Part 1                                          DONE
 *  SynthEngine pitchbend property (similar to Note)                     DONE
 *  Pitch Bend event                                                     DONE
 *  Modwheel event                                                       DONE
 *  Oscillators Write Up Part 2                                          DONE
 *  Internal Setabble knee point sin ovveride                            DONE
 *  Phase distortion for harmonic wave                                   REMOVED
 *  Fourier Glitch                                                       DONE
 *  Record Video with Audio                                              DONE
 *  Weird glitch on PD around 70%                                        DONE
 *  Remove Volumes from Oscillators
 *  Add n way mixer
 *  DEfault Wavetable to first available wave                            -
 
 *  Oscillator video demos - WP Part 3
 *  Resurrect Glide!     Maybe Note to CV module in SynthEngine netween Note and f                                                       
 *  
 *  Part 2 - Modulators
 *  Modulators
 *  LFOs
 *  SH
 *  VCAs
 *  ADSRs
 *  
 *  Part 3 - Shapers
 *  Filters
 *  
 *  Part 4 - Effects
 *  Phasers
 *  Reverb
 *    
 *  
 *  
 * */


namespace UI;
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

        // Inject 3 oscillators into synth engine
        synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 1f});
        synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 0f});
        synth.Oscillators.Add(new Oscillator() { WaveForm = WaveForm.GetByType(WaveformType.Sine), Amplitude = 0f});


        // Temporarfy hard coded mod wheel 
        synth.Oscillators[0].Duty.Modulator = synth.ModWheel;
        synth.Oscillators[1].Duty.Modulator = synth.ModWheel;
        synth.Oscillators[2].Duty.Modulator = synth.ModWheel;


    }
    #endregion

    #region Init Form
    public SimpleTest() {
        InitializeComponent();

        InitUI();
        AddEventHandlers();

        InitSynth();
        LoadPatchList();

        Debug.Assert(synth != null);

        lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
        lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
        lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);

        SetToolTips();
        ddlSyncSource.SelectedIndex = 0;
        ddlSyncSource1.SelectedIndex = 0;
        ddlSyncSource2.SelectedIndex = 0;

        ddlModSource.SelectedIndex = 0;
        ddlModSource1.SelectedIndex = 0;
        ddlModSource2.SelectedIndex = 0;

        timDisplay.Enabled = true;
        virtualKeyboard.Focus();
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

    private void LoadPatchList() { 
        var patches = Patch.GetPatchList(true);
        ddlPatches.DataSource = patches;
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
        sldFineTune.ValueChanged += SldFineTune_ValueChanged;
        cmdReset.Click += CmdReset_Click;
        sldPWM.ValueChanged += SldPWM_ValueChanged;
        sldLevel.ValueChanged += SldLevel_ValueChanged;
        ddlSuperSaw.SelectedIndexChanged += DdlSuperSaw_SelectedIndexChanged;
        ddlSyncSource.SelectedIndexChanged += DdlSyncSource_SelectedIndexChanged;
        sldModAmount.ValueChanged += SldModAmount_ValueChanged;
        ddlModSource.SelectedIndexChanged += DdlModSource_SelectedIndexChanged;
        chkKbd.CheckedChanged += ChkKbd_CheckedChanged;

        sldWaveForm1.ValueChanged += SldWaveForm1_ValueChanged;
        sldOctave1.ValueChanged += SldOctave1_ValueChanged;
        sldTune1.ValueChanged += SldTune1_ValueChanged;
        sldFineTune1.ValueChanged += SldFineTune1_ValueChanged;
        cmdReset1.Click += CmdReset1_Click;
        sldPWM1.ValueChanged += SldPWM1_ValueChanged;
        sldLevel1.ValueChanged += SldLevel1_ValueChanged;
        ddlSuperSaw1.SelectedIndexChanged += DdlSuperSaw1_SelectedIndexChanged;
        ddlSyncSource1.SelectedIndexChanged += DdlSyncSource1_SelectedIndexChanged;
        sldModAmount1.ValueChanged += SldModAmount1_ValueChanged;
        ddlModSource1.SelectedIndexChanged += DdlModSource1_SelectedIndexChanged;
        chkKbd1.CheckedChanged += ChkKbd1_CheckedChanged;

        sldWaveForm2.ValueChanged += SldWaveForm2_ValueChanged;
        sldOctave2.ValueChanged += SldOctave2_ValueChanged;
        sldTune2.ValueChanged += SldTune2_ValueChanged;
        sldFineTune2.ValueChanged += SldFineTune2_ValueChanged;
        cmdReset2.Click += CmdReset2_Click;

        sldPWM2.ValueChanged += SldPWM2_ValueChanged;
        sldLevel2.ValueChanged += SldLevel2_ValueChanged;
        ddlSuperSaw2.SelectedIndexChanged += DdlSuperSaw2_SelectedIndexChanged;
        ddlSyncSource2.SelectedIndexChanged += DdlSyncSource2_SelectedIndexChanged;
        sldModAmount2.ValueChanged += SldModAmount2_ValueChanged;
        ddlModSource2.SelectedIndexChanged += DdlModSource2_SelectedIndexChanged;
        chkKbd2.CheckedChanged += ChkKbd2_CheckedChanged;


        // Refresh display every 2s
        timDisplay.Tick += TimDisplay_Tick;
        cmdPauseGraph.Click += CmdPauseGraph_Click;

        cmdSelectOscSetting.Click += CmdOsctSetting_Click;
        cmdSelectOscSetting1.Click += CmdOsctSetting1_Click;
        cmdSelectOscSetting2.Click += CmdOsctSetting2_Click;

        virtualKeyboard.NoteChanged += keyboard_NoteChanged;
        virtualKeyboard.KeyStateChanged += keyboard_KeyStateChanged;
        virtualKeyboard.PitchWheelChanged += (o, e) => {
            synth.PitchWheel = virtualKeyboard.CurrentPitchWheel;
            lblFrequency.Invoke(new Action(() => lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency)));
            lblFrequency1.Invoke(new Action(() => lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency)));
            lblFrequency2.Invoke(new Action(() => lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency)));
        };
        virtualKeyboard.ModWheelChanged += (o, e) => 
            synth.ModWheel.Value = virtualKeyboard.CurrentModWheel;

        this.KeyUp += SimpleTest_KeyUp;

        cmdInitPatch.Click += CmdInitPatch_Click;
        ddlPatches.SelectedIndexChanged += DdlPatches_SelectedIndexChanged;
        cmdSavePatch.Click += CmdSavePatch_Click;
        cmdDeletePatch.Click += CmdDeletePatch_Click;
    }

    #endregion

    #region Virtual Keyboard keypress detection
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
        // This captures keys for the entire form. We can process, or let the base method handle

        if (virtualKeyboard.ProcessKeyDown(keyData))
            return true;
        else
            return base.ProcessCmdKey(ref msg, keyData);
    }

    // Global 'Key Up' handler. We've set Form KeyPreview property to true so that form rather than controls can capture this event
    private void SimpleTest_KeyUp(object? sender, KeyEventArgs e) {
        virtualKeyboard.CurrentKeyState = VirtualKeyboard.KeyState.Up;
    }
    #endregion

    #region Event Handlers
    private void keyboard_KeyStateChanged(object? sender, EventArgs e) {
        // We don't do anything with this at the moment
        // We will when we introduce a Gate
    }

    private void keyboard_NoteChanged(object? sender, EventArgs e) {
        ddlNote.SelectedIndex = virtualKeyboard.CurrentNote.ID - 1;
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
        synth.Note = (Synth.Utils.Note)ddlNote.SelectedItem;

        // We can't set label text directly as the NAudio Midi  stuff appears to be running on a different thread to the UI controls
        // Therefore a cross task Action is required
        lblFrequency.Invoke(new Action(() => lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency)));
        lblFrequency1.Invoke(new Action(() => lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency)));
        lblFrequency2.Invoke(new Action(() => lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency)));

    }

    private void SldWaveForm_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[0].WaveFormSelectByID(sldWaveForm.Value);
        lblWaveform.Text = (WaveForm.GetByID(sldWaveForm.Value)).Name;

        lblWaveTable.Visible = sldWaveForm.Value == (int)WaveformType.WaveTable;
        cmdSelectOscSetting.Enabled = sldWaveForm.Value == (int)WaveformType.WaveTable || sldWaveForm.Value == (int)WaveformType.Harmonic;
        ddlSuperSaw.Visible = sldWaveForm.Value == (int)WaveformType.SuperSaw;

        if (sldWaveForm.Value == (int)WaveformType.SuperSaw || sldWaveForm.Value == (int)WaveformType.Harmonic)
            sldPWM.Enabled = false;
        else
            sldPWM.Enabled = true;


        if (sldWaveForm.Value == (int)WaveformType.Square)
            lblPWM.Text = "PWM:";
        else
            lblPWM.Text = "Phase Dist:";
    }

    private void SldOctave_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[0].Frequency.Octave = sldOctave.Value;
        lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
    }

    private void SldTune_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[0].Frequency.Tune = (float)sldTune.Value / 12f;
        lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
    }

    private void SldFineTune_ValueChanged(object? sender, EventArgs e) {
        // sldFineTune value -100 to +100, so for +/- 1 semitone:  Value / 1200f
        synth.Oscillators[0].Frequency.FineTune = (float)sldFineTune.Value / 1200f;
        lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
    }

    private void CmdReset_Click(object? sender, EventArgs e) {
        sldFineTune.Value = 0;
    }

    private void SldPWM_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[0].Duty.Value = (float)sldPWM.Value / 1000f;
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
    private void DdlSyncSource_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlSyncSource.SelectedIndex) {
            case 1:
                synth.Oscillators[1].SyncDestination = synth.Oscillators[0]; break;
            case 2:
                synth.Oscillators[2].SyncDestination = synth.Oscillators[0]; break;
            default:
                if (synth.Oscillators[1].SyncDestination == synth.Oscillators[0])
                    synth.Oscillators[1].SyncDestination = null;
                if (synth.Oscillators[2].SyncDestination == synth.Oscillators[0])
                    synth.Oscillators[2].SyncDestination = null;
                break;
        }
    }
    private void SldModAmount_ValueChanged(object? sender, EventArgs e) {
        // Not sure what max value should be, so let's cap at 1 for now
        synth.Oscillators[0].Frequency.ModulationAmount = sldModAmount.Value;
    }

    private void DdlModSource_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlModSource.SelectedIndex) {
            case 1:
                synth.Oscillators[0].Frequency.Modulator = synth.Oscillators[1]; break;
            case 2:
                synth.Oscillators[0].Frequency.Modulator = synth.Oscillators[2]; break;
            default:
                synth.Oscillators[0].Frequency.Modulator = null; break;
        }
    }

    private void ChkKbd_CheckedChanged(object? sender, EventArgs e) {
        synth.Oscillators[0].Frequency.Kbd = chkKbd.Checked;
        lblFrequency.Text = FormatFrequency(synth.Oscillators[0].Frequency.PreModFrequency);
    }


    private void SldWaveForm1_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[1].WaveFormSelectByID(sldWaveForm1.Value);
        lblWaveform1.Text = (WaveForm.GetByID(sldWaveForm1.Value)).Name;

        lblWaveTable1.Visible = sldWaveForm1.Value == (int)WaveformType.WaveTable;
        cmdSelectOscSetting1.Enabled = sldWaveForm1.Value == (int)WaveformType.WaveTable || sldWaveForm1.Value == (int)WaveformType.Harmonic;
        ddlSuperSaw1.Visible = sldWaveForm1.Value == (int)WaveformType.SuperSaw;

        if (sldWaveForm1.Value == (int)WaveformType.SuperSaw || sldWaveForm1.Value == (int)WaveformType.Harmonic)
            sldPWM1.Enabled = false;
        else
            sldPWM1.Enabled = true;

        if (sldWaveForm1.Value == (int)WaveformType.Square)
            lblPWM1.Text = "PWM:";
        else
            lblPWM1.Text = "Phase Dist:";
    }

    private void SldOctave1_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[1].Frequency.Octave = sldOctave1.Value;
        lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
    }
    private void SldTune1_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[1].Frequency.Tune = (float)sldTune1.Value / 12f;
        lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
    }

    private void SldFineTune1_ValueChanged(object? sender, EventArgs e) {
        // sldFineTune value -100 to +100, so for +/- 1 semitone:  Value / 1200f
        synth.Oscillators[1].Frequency.FineTune = (float)sldFineTune1.Value / 1200f;
        lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
    }

    private void CmdReset1_Click(object? sender, EventArgs e) {
        sldFineTune1.Value = 0;
    }

    private void SldPWM1_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[1].Duty.Value = (float)sldPWM1.Value / 100f;
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

    private void DdlSyncSource1_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlSyncSource1.SelectedIndex) {
            case 1:
                synth.Oscillators[0].SyncDestination = synth.Oscillators[1]; break;
            case 2:
                synth.Oscillators[2].SyncDestination = synth.Oscillators[1]; break;
            default:
                if (synth.Oscillators[0].SyncDestination == synth.Oscillators[1])
                    synth.Oscillators[0].SyncDestination = null;
                if (synth.Oscillators[2].SyncDestination == synth.Oscillators[1])
                    synth.Oscillators[2].SyncDestination = null;
                break;
        }
    }
    private void SldModAmount1_ValueChanged(object? sender, EventArgs e) {
        // Not sure what max value should be, so let's cap at 1 for now
        synth.Oscillators[1].Frequency.ModulationAmount = sldModAmount1.Value;
    }

    private void DdlModSource1_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlModSource1.SelectedIndex) {
            case 1:
                synth.Oscillators[1].Frequency.Modulator = synth.Oscillators[0]; break;
            case 2:
                synth.Oscillators[1].Frequency.Modulator = synth.Oscillators[2]; break;
            default:
                synth.Oscillators[1].Frequency.Modulator = null; break;
        }
    }

    private void ChkKbd1_CheckedChanged(object? sender, EventArgs e) {
        synth.Oscillators[1].Frequency.Kbd = chkKbd1.Checked;
        lblFrequency1.Text = FormatFrequency(synth.Oscillators[1].Frequency.PreModFrequency);
    }


    private void SldWaveForm2_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[2].WaveFormSelectByID(sldWaveForm2.Value);
        lblWaveform2.Text = (WaveForm.GetByID(sldWaveForm2.Value)).Name;

        lblWaveTable2.Visible = sldWaveForm2.Value == (int)WaveformType.WaveTable;
        cmdSelectOscSetting2.Enabled = sldWaveForm2.Value == (int)WaveformType.WaveTable || sldWaveForm2.Value == (int)WaveformType.Harmonic;
        ddlSuperSaw2.Visible = sldWaveForm2.Value == (int)WaveformType.SuperSaw;

        if (sldWaveForm2.Value == (int)WaveformType.SuperSaw || sldWaveForm2.Value == (int)WaveformType.Harmonic)
            sldPWM2.Enabled = false;
        else
            sldPWM2.Enabled = true;

        if (sldWaveForm2.Value == (int)WaveformType.Square)
            lblPWM2.Text = "PWM:";
        else
            lblPWM2.Text = "Phase Dist:";
    }

    private void SldOctave2_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[2].Frequency.Octave = sldOctave2.Value;
        lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
    }

    private void SldTune2_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[2].Frequency.Tune = (float)sldTune2.Value / 12f;
        lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
    }

    private void SldFineTune2_ValueChanged(object? sender, EventArgs e) {
        // sldFineTune value -100 to +100, so for +/- 1 semitone:  Value / 1200f
        synth.Oscillators[2].Frequency.FineTune = (float)sldFineTune2.Value / 1200f;
        lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
    }

    private void CmdReset2_Click(object? sender, EventArgs e) {
        sldFineTune2.Value = 0;
    }

    private void SldPWM2_ValueChanged(object? sender, EventArgs e) {
        synth.Oscillators[2].Duty.Value = (float)sldPWM2.Value / 100f;
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

    void DdlSyncSource2_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlSyncSource2.SelectedIndex) {
            case 1:
                synth.Oscillators[0].SyncDestination = synth.Oscillators[2]; break;
            case 2:
                synth.Oscillators[1].SyncDestination = synth.Oscillators[2]; break;
            default:
                if (synth.Oscillators[0].SyncDestination == synth.Oscillators[2])
                    synth.Oscillators[0].SyncDestination = null;
                if (synth.Oscillators[1].SyncDestination == synth.Oscillators[2])
                    synth.Oscillators[1].SyncDestination = null;
                break;
        }
    }

    private void SldModAmount2_ValueChanged(object? sender, EventArgs e) {
        // Not sure what max value should be, so let's cap at 1 for now
        synth.Oscillators[2].Frequency.ModulationAmount = sldModAmount2.Value;
    }

    private void DdlModSource2_SelectedIndexChanged(object? sender, EventArgs e) {
        switch (ddlModSource2.SelectedIndex) {
            case 1:
                synth.Oscillators[2].Frequency.Modulator = synth.Oscillators[0]; break;
            case 2:
                synth.Oscillators[2].Frequency.Modulator = synth.Oscillators[1]; break;
            default:
                synth.Oscillators[2].Frequency.Modulator = null; break;
        }
    }

    private void ChkKbd2_CheckedChanged(object? sender, EventArgs e) {
        synth.Oscillators[2].Frequency.Kbd = chkKbd2.Checked;
        lblFrequency2.Text = FormatFrequency(synth.Oscillators[2].Frequency.PreModFrequency);
    }

    private void CmdSavePatch_Click(object? sender, EventArgs e) {
        // Get Patch Name

        var savedPatch = Patch.SaveNewPatch(this, synth);           // Pass form across so control settings can be scraped

        // Patch object returned if sucesfull. Select from ddl if so, otherwise, just return
        if (savedPatch == null)
            return;

        LoadPatchList();
        foreach (var p in ddlPatches.Items) {
            if (((Patch)p).PatchName.Trim().ToLower() == ((Patch)savedPatch).PatchName.Trim().ToLower()) {
                ddlPatches.SelectedItem = p;
                break;
            }
        }
    }

    private void DdlPatches_SelectedIndexChanged(object? sender, EventArgs e) {
        var p = (Patch)ddlPatches.SelectedItem;
        if (p.PatchName != "")
            Patch.RecallPatch(this, p, synth);
    }

    private void CmdInitPatch_Click(object? sender, EventArgs e) {
        var p = Patch.GetInitPatch();

        Patch.RecallPatch(this, p, synth);
    }

    private void CmdDeletePatch_Click(object? sender, EventArgs e) {
        if (ddlPatches.SelectedIndex < 1) {
            MessageBox.Show("Please select a patch to delete from the list of Patches", "Delete Patch", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
            ddlPatches.Focus();
            return;
        }

        if(((Patch)ddlPatches.SelectedItem).IsInit) {
            MessageBox.Show("'Init' patch must always be present, so unable to delete", "Delete Patch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        // Returns true if sucesfully deleted
        if (Patch.DeletePatch(((Patch)ddlPatches.SelectedItem).PatchName))
            LoadPatchList();            
            
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

    #region Graph Display Wave + Spectrum
    private async void DrawDisplay() {
        // Get sample wave data from synth engine
        var graphData = synth.GetGraphData(picWaveGraph.Width);

        var p = new Pen(Color.Lime);
        Graphics g = picWaveGraph.CreateGraphics();
        Point[] points = new Point[graphData.Length];
        for(int i = 0; i < graphData.Length; i++) {
            points[i] = new Point(i, (int)(graphData[i] * picWaveGraph.Height* .9f + picWaveGraph.Height / 2));
        }

        g.Clear(Color.Black);
        g.DrawLines(p, points);


        // Get and draw spectrum
        var s = await Task.Run(() => GetSpectrum(Array.ConvertAll(graphData, x => (double)x)));
        double maxCoeff = s.MaxBy(x => x);
        if (maxCoeff < .01)
            maxCoeff = 0.01;

        Point[] spectrum = new Point[s.Length];
        for (int i = 0; i < s.Length; i++)
            spectrum[i] = new Point(i*7, picWaveGraph.Height - (int)((s[i] * picWaveGraph.Height * .95/maxCoeff)  -1));

        p = new Pen(Color.CornflowerBlue);
        g.DrawLines(p, spectrum);

        graphData = null;           // Nullify these to ensure they get caught by GC
    }

    private async Task<double[]> GetSpectrum(double[] signal) {
        // Uses nuget package from https://github.com/swharden/FftSharp

        // Begin with an array containing sample data
        //double[] signal = FftSharp.SampleData.SampleAudio1();

        // Shape the signal using a Hanning window
        var window = new FftSharp.Windows.Hanning();
        window.ApplyInPlace(signal);

        // Calculate the FFT as an array of complex numbers
        // Complex[] fftRaw = FftSharp.Transform.FFT(signal);

        // or get the magnitude (units²) or power (dB) as real numbers
        double[] fftMag = FftSharp.Transform.FFTmagnitude(signal);
        return fftMag;
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
