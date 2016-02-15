using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicMaker
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox1.Lines.Count(); i++)
            {
                string s = textBox1.Lines[i];
                s = s.Replace("#","S");
                if(s == "R")
                {
                    s += "-" + comboBox2.SelectedItem.ToString() + "R";
                }
                else
                {
                    s += comboBox1.SelectedIndex + "-" + comboBox2.SelectedItem;
                }
                Form1.notesToAdd.Add(s);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(textBox2.Text, textBox3.Text);
        }
    }
}
