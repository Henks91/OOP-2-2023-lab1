using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OOP_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Henric", "Johansson", 32);
            Person person2 = new Person("Daniel","Noun", 27);
            Person person3 = new Person("Carl", "Wånsander", 26);
            Person person4 = new Person("Fredrik", "Linhardt", 26);

            person1.SetÅlder(25);
            Console.WriteLine(person1.getFnamn() +"\t"+ person1.getEnamn() +"\t"+ person1.getÅlder());    
            Console.WriteLine(person1.Over26());

            Console.WriteLine(person2.getFnamn() +"\t"+ person2.getEnamn() +"\t"+ person2.getÅlder());
            Console.WriteLine(person2.Over26());

            Console.WriteLine(person3.getFnamn() +"\t"+ person3.getEnamn() +"\t"+ person3.getÅlder());
            Console.WriteLine(person3.Over26());

            Console.WriteLine(person4.getFnamn() +"\t"+ person4.getEnamn() +"\t"+ person4.getÅlder());
            Console.WriteLine(person4.Over26());

            
            

            Console.ReadLine();
        }
    }
}
