using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_2022
{
    class Person
    {
        private string fnamn;
        private string enamn;
        private int ålder;

        
        public Person(string fnamn = " ", string enamn = " ", int ålder = 0)
        {
            this.fnamn = fnamn;
            this.enamn = enamn;
            this.ålder = ålder;
        }
        public bool Over26()
        {
            if (ålder > 26)
            {
                return true;
            }
            return false;
        }
        public string getFnamn() { return fnamn; }
        public string getEnamn() { return enamn; }
        public int getÅlder() { return ålder; }

        public void SetÅlder(int newÅlder)
        {
            if (newÅlder < 0)
                ålder = newÅlder;
            else
            {
                Console.WriteLine("Du måste ange en ålder som är över 0");
            }

            
         
        }



    }
}
