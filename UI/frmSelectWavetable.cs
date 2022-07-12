using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Synth;
using Synth.Modules.Sources;
using Synth.Modules.Properties;
using Synth.Utils;

namespace UI {
    public partial class frmSelectWavetable : Form {
        Synth.SynthEngine synth; 
        

        static string _FileName = "";
        string path = "";


        #region Initiallise
        public static string Show(string fileName) {
            _FileName = fileName == "-" ? "" : fileName;
            var f = new frmSelectWavetable();
            f.ShowDialog();
            return _FileName;
        }

        public frmSelectWavetable() {
            InitializeComponent();

            synth= new Synth.SynthEngine(new Synth.Config() { Channels = 2, SampleRate = 16000 });
            synth.Oscillators.Add(new Oscillator(WaveForm.GetByType(WaveformType.WaveTable)));
            if(chkPlayPreview.Checked)
                synth.Start();

            Init();



            AddEventHandlers();

            timShowPreviousWave.Enabled = true;
        }

        private void Init() {
            path = Directory.GetCurrentDirectory() + "\\wavetables";

            ListDirectory(tvWaveTables, path);
            tvWaveTables.ExpandAll();
            ShowPreviouslySelectedWave();
        }



        private void AddEventHandlers() {
            cmdSelect.Click += CmdSelect_Click;
            CmdCancel.Click += CmdCancel_Click;
            tvWaveTables.NodeMouseClick += TvWaveTables_NodeMouseClick;
            tvWaveTables.NodeMouseDoubleClick += TvWaveTables_NodeMousefloatClick;
            tvWaveTables.KeyUp += TvWaveTables_KeyUp;
            timShowPreviousWave.Tick += (o, e) => { ShowPreviouslySelectedWave(); timShowPreviousWave.Enabled = false; };
            this.FormClosing += (o, e) => {synth.Stop(); };
            chkPlayPreview.CheckedChanged += (o, e) => { if (chkPlayPreview.Checked) synth.Start(); else synth.Stop(); };


        }

        #endregion

        #region Event Handlers


        private void TvWaveTables_KeyUp(object? sender, KeyEventArgs e) {
            var node = tvWaveTables.SelectedNode;
            if (node == null)
                return;

            if (node.Text.ToLower().EndsWith(".wav")) {
                string fileName = node.Parent.Text + "\\" + node.Text;
                var w = WavReader.readWav(path + "\\" + fileName);
                DrawGraph(w, fileName);
            }
        }
        private void TvWaveTables_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e) {
            var node = e.Node;
            if (node == null) 
                return;

            if (node.Text.ToLower().EndsWith(".wav")) { 
                string fileName = node.Parent.Text + "\\" + node.Text;
                var w = WavReader.readWav(path + "\\" + fileName);
                DrawGraph(w, fileName);
            }
        }

        private void TvWaveTables_NodeMousefloatClick(object? sender, TreeNodeMouseClickEventArgs e) {
            var a = e.Node;
            if (a == null) 
                return;

            if (a.Text.ToLower().EndsWith(".wav")) {
                _FileName = a.Parent.Text + "\\" + a.Text;
                this.Close();  
            }

        }

        private void CmdCancel_Click(object? sender, EventArgs e) {
            _FileName = "";
            this.Close();
        }

        private void CmdSelect_Click(object? sender, EventArgs e) {
            var a = tvWaveTables.SelectedNode;
            if(a == null) 
                return;

            if (a.Text.ToLower().EndsWith(".wav")) {
                _FileName = a.Parent.Text + "\\" + a.Text;
                this.Close();
            }

        }

        #endregion

        #region Private Methods


        private void ListDirectory(TreeView treeView, string path) {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo) {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name) { Tag = directoryInfo.Name + "."  + file.Name});
            return directoryNode;
        }

        void ShowPreviouslySelectedWave() {
            // If we are passing in a filename, select it
            if (!string.IsNullOrEmpty(_FileName)) {
                var parentNodeName = _FileName.Substring(0, _FileName.IndexOf('\\'));
                var nodeName = _FileName.Substring(_FileName.IndexOf('\\') + 1);

                // Save Dir.FileName to file node .Tag property

                var root = tvWaveTables.Nodes[0];
                foreach (TreeNode dir in root.Nodes)                 {
                    foreach (TreeNode file in dir.Nodes) {
                        if (file.Tag.Equals(parentNodeName + "." + nodeName)) {
                            tvWaveTables.SelectedNode = file;
                            string fileName = tvWaveTables.SelectedNode.Parent.Text + "\\" + tvWaveTables.SelectedNode.Text;
                            var w = WavReader.readWav(path + "\\" + fileName);
                            DrawGraph(w, fileName);
                        }
                    }
                }
            }
        }

        private async void DrawGraph(float[] wave, string fileName) {
            if (fileName != "" && chkPlayPreview.Checked)
                synth.Oscillators[0].WaveTableFileName = fileName;

            var p = new Pen(Color.Green);

            Graphics g = picPreview.CreateGraphics();
            Point[] points = new Point[wave.Length];
            for (int i = 0; i < wave.Length; i++) {
                points[i] = new Point(i, (int)(wave[i] * picPreview.Height * .4f + picPreview.Height / 2));
            }

            g.Clear(Color.Black);
            g.DrawLines(p, points);

            // Get and draw spectrum

            var unmodified = wave.Select(w => (double)w).ToArray();
            var l1 = unmodified.Length;
            var l2 = (int)Math.Pow(2, Math.Ceiling(Math.Log2(l1)));
            var modified = new double[l2];

            for (int i = 0; i < modified.Length; i++)
                modified[i] = unmodified[(int)((double)i / modified.Length * l1)];

            var s = await Task.Run(() => GetSpectrum(modified));
            double maxCoeff = s.MaxBy(x => x);
            if (maxCoeff < .01)
                maxCoeff = 0.01;

            Point[] spectrum = new Point[s.Length];
            for (int i = 0; i < s.Length; i++)
                spectrum[i] = new Point(i * 7, picPreview.Height - (int)((s[i] * picPreview.Height * .95 / maxCoeff) - 1));

            p = new Pen(Color.CornflowerBlue);
            g.DrawLines(p, spectrum);

        }
        private async Task<double[]> GetSpectrum(double[] signal)
        {
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
    }
}
