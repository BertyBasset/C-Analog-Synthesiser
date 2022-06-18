using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Synth.Utils;

namespace UI {
    public partial class Keyboard : UserControl {
        public event EventHandler? NoteChanged;
        public event EventHandler? KeyStateChanged;

        public enum KeyState {
            Up,
            Down
        }

        private KeyState _CurrentKeyState = KeyState.Up;
        public KeyState CurrentKeyState {
            get { return _CurrentKeyState; }
            internal set { 
                _CurrentKeyState = value;


                if (_CurrentKeyState == KeyState.Down) {
                    var label = GetLabelByNote(_CurrentNote);
                    if (label != null)
                        label.BackColor = Color.Gray;
                } else {
                    // Reset all others
                    foreach (var ctl in this.Controls) {
                        if (ctl.GetType() == typeof(Label)) {
                            var l = (Label)ctl;
                                if (l.Height < 100)
                                    l.BackColor = Color.Black;
                                else
                                    l.BackColor = Color.White;
                        }
                    }
                }
            }
        }

        public Label? GetLabelByNote(Note Note) {
            switch (Note.ID) {
                case 16: return lblC2;
                case 17: return lblC2s;
                case 18: return lblD2;
                case 19: return lblD2s;
                case 20: return lblE2;
                case 21: return lblF2;
                case 22: return lblF2s;
                case 23: return lblG2;
                case 24: return lblG2s;
                case 25: return lblA3;
                case 26: return lblA3s;
                case 27: return lblB3;
                case 28: return lblC3;
                case 29: return lblC3s;
                case 30: return lblD3;
                case 31: return lblD3s;
                case 32: return lblE3;
                case 33: return lblF3;
                case 34: return lblF3s;
                case 35: return lblG3;
                case 36: return lblG3s;
                case 37: return lblA4;
                case 38: return lblA4s;
                case 39: return lblB4;
                case 40: return lblC4;
                case 41: return lblC4s;
                case 42: return lblD4;
                case 43: return lblD4s;
                case 44: return lblE4;
                case 45: return lblF4;
                case 46: return lblF4s;
                case 47: return lblG4;
                case 48: return lblG4s;
                case 49: return lblA5;
                case 50: return lblA5s;
                case 51: return lblB5;
                case 52: return lblC5;

                default: return null;
            }
        }

        private Note _CurrentNote = Note.GetByDesc("A2");
        public Note CurrentNote {
            get { return _CurrentNote; }
            internal set { 
                _CurrentNote = value;
                Debug.Assert(NoteChanged != null);

                EventHandler handler = NoteChanged;
                handler?.Invoke(this, new EventArgs());
            }
        }


        public Keyboard() {
            InitializeComponent();
            AddEventHandlers();
        }

        private void AddEventHandlers() {
            lblC2.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblC2.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblC2.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "C2");
            lblC2.MouseUp+= (o, e) => MouseUpWhiteKey((Label?)o);
            lblC2s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblC2s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblC2s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "C♯2/D♭2");
            lblC2s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblD2.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblD2.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblD2.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "D2");
            lblD2.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblD2s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblD2s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblD2s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "D♯2E♭2");
            lblD2s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblE2.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblE2.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblE2.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "E2");
            lblE2.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF2.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblF2.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblF2.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "F2");
            lblF2.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF2s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblF2s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblF2s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "F♯2G♭2");
            lblF2s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblG2.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblG2.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblG2.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "G2");
            lblG2.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblG2s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblG2s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblG2s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "G♯2A♭2");
            lblG2s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblA3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblA3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblA3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "A3");
            lblA3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblA3s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblA3s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblA3s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "A♯3B♭3");
            lblA3s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblB3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblB3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblB3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "B3");
            lblB3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);

            lblC3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblC3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblC3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "C3");
            lblC3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblC3s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblC3s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblC3s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "C♯3/D♭3");
            lblC3s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblD3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblD3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblD3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "D3");
            lblD3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblD3s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblD3s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblD3s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "D♯3E♭3");
            lblD3s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblE3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblE3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblE3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "E3");
            lblE3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblF3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblF3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "F3");
            lblF3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF3s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblF3s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblF3s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "F♯3G♭3");
            lblF3s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblG3.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblG3.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblG3.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "G3");
            lblG3.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblG3s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblG3s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblG3s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "G♯3A♭2");
            lblG3s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblA4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblA4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblA4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "A4");
            lblA4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblA4s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblA4s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblA4s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "A♯4B♭4");
            lblA4s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblB4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblB4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblB4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "B4");
            lblB4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
                
            lblC4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblC4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblC4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "C4");
            lblC4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblC4s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblC4s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblC4s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "C♯4/D♭4");
            lblC4s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblD4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblD4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblD4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "D4");
            lblD4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblD4s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblD4s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblD4s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "D♯4E♭4");
            lblD4s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblE4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblE4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblE4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "E4");
            lblE4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblF4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblF4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "F4");
            lblF4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblF4s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblF4s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblF4s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "F♯4G♭4");
            lblF4s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblG4.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblG4.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblG4.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "G4");
            lblG4.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblG4s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblG4s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblG4s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "G♯4A♭4");
            lblG4s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblA5.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblA5.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblA5.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "A5");
            lblA5.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
            lblA5s.MouseEnter += (o, e) => MouseEnterBlackKey((Label?)o);
            lblA5s.MouseLeave += (o, e) => MouseLeaveBlackKey((Label?)o);
            lblA5s.MouseDown += (o, e) => MouseDownBlackKey((Label?)o, "A♯5B♭5");
            lblA5s.MouseUp += (o, e) => MouseUpBlackKey((Label?)o);
            lblB5.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblB5.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblB5.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "B5");
            lblB5.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);

            lblC5.MouseEnter += (o, e) => MouseEnterWhiteKey((Label?)o);
            lblC5.MouseLeave += (o, e) => MouseLeaveWhiteKey((Label?)o);
            lblC5.MouseDown += (o, e) => MouseDownWhiteKey((Label?)o, "C5");
            lblC5.MouseUp += (o, e) => MouseUpWhiteKey((Label?)o);
        }

        void MouseEnterWhiteKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.LightGray;
        }

        void MouseLeaveWhiteKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.White;
        }

        void MouseDownWhiteKey(Label? label, string NoteDesc) {
            Debug.Assert(label != null);
            CurrentNote = Note.GetByDesc(NoteDesc);
            label.BackColor = Color.Gray;
            _CurrentKeyState = KeyState.Down;
            _NoteChanged();
            _KeyStateChanged();
        }

        void MouseUpWhiteKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.White;
            _CurrentKeyState = KeyState.Up;
            _KeyStateChanged();
        }

        void MouseEnterBlackKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.Gray;
        }

        void MouseLeaveBlackKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.Black;
        }

        void MouseDownBlackKey(Label? label, string NoteDesc) {
            Debug.Assert(label != null);
            CurrentNote = Note.GetByDesc(NoteDesc);
            label.BackColor = Color.LightGray;
            _CurrentKeyState = KeyState.Down;
            _NoteChanged();
            _KeyStateChanged();
        }

        void MouseUpBlackKey(Label? label) {
            Debug.Assert(label != null);
            label.BackColor = Color.Black;
            _CurrentKeyState = KeyState.Up;
            _KeyStateChanged();
        }

        // These to fire events
        void _KeyStateChanged() {
            Debug.Assert(KeyStateChanged != null);   
            EventHandler handler = KeyStateChanged;
            handler?.Invoke(this, new EventArgs());
        }

        void _NoteChanged() {
            Debug.Assert(NoteChanged != null);
            EventHandler handler = NoteChanged;
            handler?.Invoke(this, new EventArgs());
        }
    }
}
