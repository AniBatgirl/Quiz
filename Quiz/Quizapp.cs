using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngouriMath;

namespace Quiz
{
     class Quizapp
    {
        double score;
        public Quizapp(int score)
        {
            this.score = score;
            Priklad currentPriklad;
        
        }
        public void GenerovatPriklad()
        {
            string Priklad;
            char[] znamenka = new char[] { '+', '-', '*', '/' };
            int obtiznost = (int)Math.Round(1 + score / 10);
            double multiplier = 1.2 * score;


            Random a = new Random();
            int aRnd = a.Next(0, (int)(multiplier * 10));

            Random b = new Random();
            int bRnd = b.Next(0, (int)(multiplier * 10));

            Random c = new Random();
            int cRnd = c.Next(0, (int)(multiplier * 10));

            Random d = new Random();
            int dRnd = d.Next(0, (int)(multiplier * 10));


        }
    }
}

