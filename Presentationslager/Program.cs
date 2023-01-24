using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Affärslager;
using Entiteter;

namespace Presentationslager
{
    class Program
    {
        // Kolla patriks kod för rumsbokning - Sax


        static void Main(string[] args)
        {
            new Program().Main();
        }

        private Program()
        {
            kontroller = new Kontroller();
        }

        private void Main()
        {

            Console.WriteLine("Inloggning för Expiditering");
            while (true)
            {
                try
                {
                    if (Inloggning())
                    {
                        Console.WriteLine("You are now logged in {0}.", kontroller.Autentisering.Namn);
                        Menyn();
                        // For now the MainMenu() isn't used to choose anything.
                    }
                    else
                    {
                        Console.WriteLine("Failed to log in.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
        }

        private bool Inloggning()

        {
            string idToParse = "";
            int id;
            while (!int.TryParse(idToParse, out id))
            {
                Console.WriteLine("Vänligen ange anställningsnummer: ");
                idToParse = Console.ReadLine();
            }
            Console.WriteLine("Ange lösenord: ");
            string password = Console.ReadLine();

            return kontroller.Inloggning(id, password);
        }





        static uint RättSiffra(string label) // typ av int accepterar endast positiva tal
        {
            Console.Write(label);
            uint siffra; // variabelnamn för tal som är mer än 0
            while (!uint.TryParse(Console.ReadLine(), out siffra)) // sålänge som påståendet inte stämmer: 
            {
                Console.WriteLine("Endast de angivna alternativen accepteras!");
                Console.Write(label);
            }
            return siffra;
        }

        private void Menyn()
        {
            Console.WriteLine("Biblioteket");
            bool stängNer = false; // Variabel för att avsluta programmet vid specifikt menyval
            while (!stängNer)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("**HUVUDMENY**");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1: Skapa Bokning");
                Console.WriteLine("");
                Console.WriteLine("2: Utlämning av böcker");
                Console.WriteLine("");
                Console.WriteLine("3: Inlämning av böcker");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4: Avsluta programmet");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;

                switch (RättSiffra("Svara med en siffra för att göra ett val: ")) // om användaren matar in ett alfabetiskt värde kommer "RättSiffra" märka det och presentera ett felmeddelande.
                {
                    case 1:
                        //Console.WriteLine("Skriv från datum: ");
                        //Console.WriteLine("Skriv till datum: ");

                        kontroller.HämtaTillgängligaBöcker();

                        //foreach (Bok b in kontroller.HämtaTillgängligaBöcker())
                        //{
                        //    Console.WriteLine($"{b.ISBN}, {b.Titel}");
                        //}
                        //Console.ReadLine();

                        continue;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:
                        stängNer = true;
                        break;
                    default:
                        Console.WriteLine("Inkorrekt inmatning, välj ett av ovanstående alternativ"); //om användaren anger ett tal som inte finns som ett altetrnativ kommer ett felmeddelande presenteras.
                        Console.ReadLine();
                        break;
                }

            }
        }

        private Kontroller kontroller;
    }
}
