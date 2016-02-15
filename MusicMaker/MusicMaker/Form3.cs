using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicMaker
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        bool sharped = false;
        bool octave = false;
        bool play = false;
        string currentNoteName = "Q";

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            label3.Text = "Mouse Click X: " + e.X + "\nMouse Click Y: " + e.Y;
            int x = e.X;
            int y = e.Y;

            #region ALL THE IFs
            //Starts at 64 increases by 23 each time in the X
            //Starts at 295 decreases by 21. The next starting Y is 12 less
            //X starts at 102; Y starts at 164
            if (x > 64 && x < 87 && y > 274 && y < 295)
            {
                label1.Text = note("G", 2);
            }
            else if (x > 87 && x < 110 && y > 262 && y < 283)
            {
                label1.Text = note("A", 2);
            }
            else if (x > 110 && x < 133 && y > 250 && y < 271)
            {
                label1.Text = note("B", 2);
            }
            else if (x > 133 && x < 156 && y > 238 && y < 259)
            {
                label1.Text = note("C", 3);
            }
            else if (x > 156 && x < 179 && y > 226 && y < 247)
            {
                label1.Text = note("D", 3);
            }
            else if (x > 179 && x < 202 && y > 214 && y < 235)
            {
                label1.Text = note("E", 3);
            }
            else if (x > 202 && x < 225 && y > 202 && y < 223)
            {
                label1.Text = note("F", 3);
            }
            else if (x > 225 && x < 248 && y > 190 && y < 211)
            {
                label1.Text = note("G", 3);
            }
            else if (x > 248 && x < 271 && y > 178 && y < 199)
            {
                label1.Text = note("A", 3);
            }
            else if (x > 271 && x < 294 && y > 166 && y < 187)
            {
                label1.Text = note("B", 3);
            }
            else if (x > 294 && x < 317 && y > 154 && y < 175)
            {
                label1.Text = note("C", 4);
            }
            //Starts at 102 increases by 23 each time in the X
            //Starts at 164 decreases by 21. The next starting Y is 12 less
            else if (x > 102 && x < 125 && y > 143 && y < 164)
            {
                label1.Text = note("D", 4);
            }
            else if (x > 125 && x < 148 && y > 131 && y < 152)
            {
                label1.Text = note("E", 4);
            }
            else if (x > 148 && x < 171 && y > 119 && y < 140)
            {
                label1.Text = note("F", 4);
            }
            else if (x > 171 && x < 194 && y > 107 && y < 128)
            {
                label1.Text = note("G", 4);
            }
            else if (x > 194 && x < 217 && y > 95 && y < 116)
            {
                label1.Text = note("A", 4);
            }
            else if (x > 217 && x < 240 && y > 83 && y < 104)
            {
                label1.Text = note("B", 4);
            }
            else if (x > 240 && x < 263 && y > 71 && y < 92)
            {
                label1.Text = note("C", 5);
            }
            else if (x > 263 && x < 286 && y > 59 && y < 80)
            {
                label1.Text = note("D", 5);
            }
            else if (x > 286 && x < 309 && y > 47 && y < 68)
            {
                label1.Text = note("E", 5);
            }
            else if (x > 309 && x < 332 && y > 35 && y < 56)
            {
                label1.Text = note("F", 5);
            }
            else if (x > 332 && x < 355 && y > 23 && y < 44)
            {
                label1.Text = note("G", 5);
            }
            else
            {
                label1.Text = "Please Select a Note";
            }
            #endregion
        }

        private void playNote(string notename)
        {
            Note.Play((Note.NoteCode)Enum.Parse(typeof(Note.NoteCode), notename), Note.NoteType.Q);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "Mouse X: " + e.X + "\nMouse Y: " + e.Y;
        }

        private string note(string noteName, byte noteVal)
        {
            if (sharped && (noteName == "A" || noteName == "C" || noteName == "D" || noteName == "F" || noteName == "G"))
            {
                noteName += "S";
            }
            if (octave)
            {
                noteName += (noteVal + 1);
            }
            else
            {
                noteName += noteVal;
            }

            label1.Text = noteName;

            if (play)
            {
                Task.Factory.StartNew(() => playNote(noteName));
            }
            else
            {
                listBox1.Items.Add(noteName + "-" + currentNoteName);
            }

            return noteName;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.FlatStyle == FlatStyle.Standard)
            {
                button1.FlatStyle = FlatStyle.Flat;
                octave = true;
            }
            else
            {
                button1.FlatStyle = FlatStyle.Standard;
                octave = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.FlatStyle == FlatStyle.Standard)
            {
                button3.FlatStyle = FlatStyle.Standard;
                button2.FlatStyle = FlatStyle.Flat;
                play = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.FlatStyle == FlatStyle.Standard)
            {
                button2.FlatStyle = FlatStyle.Standard;
                button3.FlatStyle = FlatStyle.Flat;
                play = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.FlatStyle == FlatStyle.Standard)
            {
                button4.FlatStyle = FlatStyle.Flat;
                sharped = true;
            }
            else
            {
                button4.FlatStyle = FlatStyle.Standard;
                sharped = false;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Q.PerformClick();
        }

        private void Form3_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (string s in listBox1.Items)
            {
                Form1.guiNotesToAdd.Add(s);
            }

            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
            }
        }

        private void Form3_ResizeBegin(object sender, EventArgs e)
        {

        }

        private void SetButtonSelections(string selected)
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(Button) && (string)c.Tag == "note")
                {
                    Button b = (Button)c;
                    if (b.Name == selected)
                    {
                        b.FlatStyle = FlatStyle.Flat;
                    }
                    else
                    {
                        b.FlatStyle = FlatStyle.Standard;
                    }
                }
            }
        }

        private void noteButton_click(object sender, EventArgs e)
        {
            SetButtonSelections(((Button)sender).Name);
            currentNoteName = ((Button)sender).Name;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => playAll());
        }

        private void restButtonClick(object sender, EventArgs e)
        {
            listBox1.Items.Add("R-" + ((Button)sender).Name);
        }

        private void playAll()
        {
            foreach (string s in listBox1.Items)
            {
                Note.NoteCode noteName = (Note.NoteCode)Enum.Parse(typeof(Note.NoteCode), s.Split('-')[0]);
                Note.NoteType noteType = (Note.NoteType)Enum.Parse(typeof(Note.NoteType), s.Split('-')[1]);
                Note.Play(noteName, noteType);
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                S.PerformClick();
            }
            else if (e.KeyCode == Keys.D2)
            {
                E.PerformClick();
            }
            else if (e.KeyCode == Keys.D3)
            {
                DE.PerformClick();
            }
            else if (e.KeyCode == Keys.D4)
            {
                Q.PerformClick();
            }
            else if (e.KeyCode == Keys.D5)
            {
                DQ.PerformClick();
            }
            else if (e.KeyCode == Keys.D6)
            {
                H.PerformClick();
            }
            else if (e.KeyCode == Keys.D7)
            {
                DH.PerformClick();
            }
            else if (e.KeyCode == Keys.D8)
            {
                W.PerformClick();
            }
            else if (e.KeyCode == Keys.S)
            {
                if (button4.FlatStyle == FlatStyle.Standard)
                {
                    button4.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if (button1.FlatStyle == FlatStyle.Standard)
                {
                    button1.PerformClick();
                }
            }
        }

        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                button4.PerformClick();
            }
            else if (e.KeyCode == Keys.A)
            {
                button1.PerformClick();
            }
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            Q.PerformClick();
        }
    }
}
