using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class to abstract Notes on a keyboard, and provide corresponding frequecny

namespace Synth.Utils {
    public class Note {
        public int ID;
        public string Desc = "";
        public float Frequency= 0;

        public override string ToString() {
            return Desc;
        }

        public static Note GetByDesc(string Desc) {
            return (GetNoteList()).Where(n => n.Desc.ToUpper() == Desc.ToUpper()).First();
        }

        public static Note GetByID(int ID) {
            return (GetNoteList()).Where(n => n.ID == ID).First();
        }

        public static List<Note> GetNoteList() { 
            var notes = new List<Note>();

            notes.Add(new Note() { ID = 1, Desc = "A0" }); ;
            notes.Add(new Note() { ID = 2, Desc = "A♯0/B♭0" });
            notes.Add(new Note() { ID = 3, Desc = "B0" }); ;

            notes.Add(new Note() { ID = 4, Desc = "C1" }); ;
            notes.Add(new Note() { ID = 5, Desc = "C♯1/D♭1" });
            notes.Add(new Note() { ID = 6, Desc = "D1" });
            notes.Add(new Note() { ID = 7, Desc = "D♯1E♭1" });
            notes.Add(new Note() { ID = 8, Desc = "E1" });
            notes.Add(new Note() { ID = 9, Desc = "F1" });
            notes.Add(new Note() { ID = 10, Desc = "F♯1G♭1" });
            notes.Add(new Note() { ID = 11, Desc = "G1" });
            notes.Add(new Note() { ID = 12, Desc = "G♯1A♭1" });
            notes.Add(new Note() { ID = 13, Desc = "A2" });
            notes.Add(new Note() { ID = 14, Desc = "A♯2B♭2" });
            notes.Add(new Note() { ID = 15, Desc = "B2" });

            notes.Add(new Note() { ID = 16, Desc = "C2" }); ;
            notes.Add(new Note() { ID = 17, Desc = "C♯2/D♭2" });
            notes.Add(new Note() { ID = 18, Desc = "D2" });
            notes.Add(new Note() { ID = 19, Desc = "D♯2E♭2" });
            notes.Add(new Note() { ID = 20, Desc = "E2" });
            notes.Add(new Note() { ID = 21, Desc = "F2" });
            notes.Add(new Note() { ID = 22, Desc = "F♯2G♭2" });
            notes.Add(new Note() { ID = 23, Desc = "G2" });
            notes.Add(new Note() { ID = 24, Desc = "G♯2A♭2" });
            notes.Add(new Note() { ID = 25, Desc = "A3" });
            notes.Add(new Note() { ID = 26, Desc = "A♯3B♭3" });
            notes.Add(new Note() { ID = 27, Desc = "B3" });

            notes.Add(new Note() { ID = 28, Desc = "C3" }); ;
            notes.Add(new Note() { ID = 29, Desc = "C♯3/D♭3" });
            notes.Add(new Note() { ID = 30, Desc = "D3" });
            notes.Add(new Note() { ID = 31, Desc = "D♯3E♭3" });
            notes.Add(new Note() { ID = 32, Desc = "E3" });
            notes.Add(new Note() { ID = 33, Desc = "F3" });
            notes.Add(new Note() { ID = 34, Desc = "F♯3G♭3" });
            notes.Add(new Note() { ID = 35, Desc = "G3" });
            notes.Add(new Note() { ID = 36, Desc = "G♯3A♭2" });
            notes.Add(new Note() { ID = 37, Desc = "A4" });
            notes.Add(new Note() { ID = 38, Desc = "A♯4B♭4" });
            notes.Add(new Note() { ID = 39, Desc = "B4" });

            notes.Add(new Note() { ID = 40, Desc = "C4" }); ;
            notes.Add(new Note() { ID = 41, Desc = "C♯4/D♭4" });
            notes.Add(new Note() { ID = 42, Desc = "D4" });
            notes.Add(new Note() { ID = 43, Desc = "D♯4E♭4" });
            notes.Add(new Note() { ID = 44, Desc = "E4" });
            notes.Add(new Note() { ID = 45, Desc = "F4" });
            notes.Add(new Note() { ID = 46, Desc = "F♯4G♭4" });
            notes.Add(new Note() { ID = 47, Desc = "G4" });
            notes.Add(new Note() { ID = 48, Desc = "G♯4A♭4" });
            notes.Add(new Note() { ID = 49, Desc = "A5" });
            notes.Add(new Note() { ID = 50, Desc = "A♯5B♭5" });
            notes.Add(new Note() { ID = 51, Desc = "B5" });
            notes.Add(new Note() { ID = 52, Desc = "C5" });

            // Set frequencies taking fixed frequency of 440Hz for A4
            var A4f = 440;       // Fix this frequency
            var A4ID = 49;       // ID from list above
            foreach (var note in notes) {
                int steps = note.ID - A4ID;
                note.Frequency = (float)(A4f * Math.Pow(Math.Pow(2, 1f / 12f), steps));
            }

            return notes;
        }
    }
}
