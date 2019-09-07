using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Neuronus
{
    class Neiron
    {
        double[,] weight = new double[200, 200];
        public char myChar { get { return myChar; } private set { myChar = value; } }
        public Neiron(char inputChar) => myChar = inputChar;

        public void Train(byte[,] input)
        {
            //TO DO
        }

        public void Recognize(byte[,] input)
        {
            //TO DO
        }
    }
}