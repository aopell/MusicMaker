using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MusicMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<string> notesToAdd = new List<string>();
        public static List<string> guiNotesToAdd = new List<string>();


        public void addMultiple()
        {
            Form2 f = new Form2();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (string s in notesToAdd)
                {
                    if (textBox1.Lines.Count() != 0)
                    {
                        textBox1.Text += "\r\n";
                    }

                    textBox1.Text += s;
                }

                notesToAdd.Clear();

            }
        }

        public void addWithGUI()
        {
            Form3 f = new Form3();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (string s in guiNotesToAdd)
                {
                    if (textBox1.Lines.Count() != 0)
                    {
                        textBox1.Text += "\r\n";
                    }

                    textBox1.Text += s;
                }

                notesToAdd.Clear();

            }
        }

        BackgroundWorker worker = new BackgroundWorker();

        public void play()
        { 
            Note.stop = false;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
            progressBar1.Value = 0;
            //textBox1.Select(1, 10);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            playing = false;
            button1.Text = "Play";
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.GetFirstCharIndexFromLine(e.ProgressPercentage + NoNoteLines);
                textBox1.SelectionLength = textBox1.Lines[e.ProgressPercentage + NoNoteLines].Length;
                textBox1.ScrollToCaret();
                progressBar1.Value = (int)Decimal.Round(Decimal.Divide(e.ProgressPercentage + 1, textBox1.Lines.Count() - NoNoteLines) * 100);
                label3.Text = "Played " + (e.ProgressPercentage + 1) + " Notes";
            }
            catch
            {

            }
        }

        int NoNoteLines = 0;

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                int nonnotelines = 0;
                NoNoteLines = 0;

                foreach(string s in textBox1.Lines)
                {
                    if(s.StartsWith("#") || s == "")
                    {
                        nonnotelines++;
                        NoNoteLines++;
                    }
                }

                if(nonnotelines == textBox1.Lines.Count())
                {
                    worker.CancelAsync();
                    break;
                }

                for (int i = 0; i < textBox1.Lines.Count(); i++)
                {
                    if(worker.CancellationPending)
                    {
                        break;
                    }

                    worker.ReportProgress(i - nonnotelines);

                    if (textBox1.Lines[i] == "#STOP")
                    {
                        worker.CancelAsync();
                        break;
                    }
                    else if(textBox1.Lines[i].StartsWith("#"))
                    {
                        continue;
                    }
                    else if(textBox1.Lines[i] == "")
                    {
                        continue;
                    }

                    try
                    {
                        Note.NoteCode noteName = (Note.NoteCode)Enum.Parse(typeof(Note.NoteCode), textBox1.Lines[i].Split('-')[0]);
                        Note.NoteType noteType = (Note.NoteType)Enum.Parse(typeof(Note.NoteType), textBox1.Lines[i].Split('-')[1]);
                        Note.Play(noteName, noteType);
                    }
                    catch (ArgumentException ex)
                    {
                        string badNote = ex.ToString().Split('\'')[1];
                        MessageBox.Show("Note " + ((i + 1) - nonnotelines) + "/" + (textBox1.Lines.Count() - nonnotelines) + " '" + badNote + "' was not valid.\nMusic will stop once OK is pressed.");
                        worker.CancelAsync();
                        return;
                        //textBox1.Lines[i] += " (INVALID)";
                    }
                }
            }

            e.Cancel = true;
            return;
        }

        bool playing = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (playing)
            {
                playing = false;
                button1.Text = "Play";
                if (worker.IsBusy)
                {
                    worker.CancelAsync();
                }
            }
            else
            {
                playing = true;
                button1.Text = "Stop";
                play();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Lines.Count() != 0)
            {
                textBox1.Text += "\r\n";
            }

            textBox1.Text += comboBox1.SelectedItem + "-" + comboBox2.SelectedItem;
        }

        string defaultText;

        private void Form1_Load(object sender, EventArgs e)
        {
            defaultText = textBox1.Text;
            this.CenterToScreen();
            comboBox1.SelectedValue = "C4";
            comboBox2.SelectedValue = "Q";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Plain Text File (.txt)|*.txt";
            saveFileDialog1.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            saveFileDialog1.FileName = "My Song";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addWithGUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Note.stop = true;
        }

        public void save()
        {
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, textBox1.Lines);
            }
        }

        public void open()
        {
            textBox1.Text = "";
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.MyMusic.ToString();
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (string s in File.ReadAllLines(openFileDialog1.FileName))
                {
                    if (textBox1.Lines.Count() != 0)
                    {
                        textBox1.Text += "\r\n";
                    }

                    textBox1.Text += s;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            play();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(textBox2.Text, textBox3.Text);
        }

        private void addMultipleNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addMultiple();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(textBox1.Text != defaultText)
            {
                DialogResult dr = MessageBox.Show("Do you want to save your work?", "Save Dialog", MessageBoxButtons.YesNoCancel);
                if(dr == DialogResult.Yes)
                {
                    save();
                }
                else if(dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(worker.IsBusy)
            {
                worker.CancelAsync();
            }
        }

        private void fileToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {

        }

        private void addUsingGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addWithGUI();
        }

        private void gUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addWithGUI();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void playToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            play();
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            save();
        }

        private void playToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            play();
        }

        private void addWithGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addWithGUI();
        }

        private void clearAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
