using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMaker
{
    public static class Note
    {
        public static bool stop = false;

        public enum NoteCode
        {
            //Only notes DS1 and above can be played
            A0, AS0, B0,
            C1, CS1, D1, DS1, E1, F1, FS1, G1, GS1, A1, AS1, B1,
            C2, CS2, D2, DS2, E2, F2, FS2, G2, GS2, A2, AS2, B2,
            C3, CS3, D3, DS3, E3, F3, FS3, G3, GS3, A3, AS3, B3,
            C4, CS4, D4, DS4, E4, F4, FS4, G4, GS4, A4, AS4, B4,
            C5, CS5, D5, DS5, E5, F5, FS5, G5, GS5, A5, AS5, B5,
            C6, CS6, D6, DS6, E6, F6, FS6, G6, GS6, A6, AS6, B6,
            C7, CS7, D7, DS7, E7, F7, FS7, G7, GS7, A7, AS7, B7,
            C8, CS8, D8, DS8, E8, F8, FS8, G8, GS8, A8, AS8, B8,
            R
        }

        public enum NoteType
        {
            S = 125,
            E = 250,
            DE = 375,
            Q = 500,
            DQ = 750,
            H = 1000,
            DH = 1500,
            W = 2000,
            SR = -125,
            ER = -250,
            QR = -500,
            HR = -1000,
            WR = -2000
        }

        public static int ConvertToHertz(NoteCode note)
        {
            float f0 = 16.35f;
            float a = 1.05946309436f;
            //Equation is fn = f0 * (a)^n
            int n = Convert.ToInt32(note);
            return (int)(f0 * (Math.Pow(a, n)));
        }

        public static void Play (NoteCode name, NoteType length)
        {
            int noteValue = Convert.ToInt32(length);

            if(noteValue < 0 && !stop)
            {
                System.Threading.Thread.Sleep(Math.Abs(noteValue));
            }
            else if (noteValue >= 0 && !stop)
            {
                Console.Beep(ConvertToHertz(name), noteValue);
            }
            else
            {
                return;
            }
        }
    }
}
