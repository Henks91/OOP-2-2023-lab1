using Affärslager;
using Entiteter;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Presentationslager
{
    class Program
    {
        // Kolla patriks kod för rumsbokning - Sax

        private static int uniktBokNR = 1;
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
                        Console.WriteLine("Inloggningen lyckades, välkommen ", kontroller.Autentisering.Namn);
                        Menyn();
                    }
                    else
                    {
                        Console.WriteLine("Inloggning misslyckades, var god försök igen!");
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





        static uint inmatninguINT(string inmatningssträng) //
        {
            Console.Write(inmatningssträng);
            uint switchVal; 
            while (!uint.TryParse(Console.ReadLine(), out switchVal)) 
            {
                Console.WriteLine("Felaktig datatyp i ditt svar, endast siffror!");
                Console.Write(inmatningssträng);
            }
            return switchVal;
        }

        private void Menyn()
        {


            bool stängNer = false; // Variabel för att avsluta programmet vid specifikt menyval
            while (!stängNer)
            {


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("EXPIDITIONENS BOKHANTERINGS MENY");
                Console.WriteLine("1: Skapa Bokning");
                Console.WriteLine("");
                Console.WriteLine("2: Utlämning av böcker");
                Console.WriteLine("");
                Console.WriteLine("3: Inlämning av böcker");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4: Avsluta programmet");
                

                switch (inmatninguINT("Svara med en siffra för att göra ett val: ")) // om användaren matar in ett alfabetiskt värde kommer "RättSiffra" märka det och presentera ett felmeddelande.
                {
                    case 1:
                        Console.Write("\nFrån yyyy-mm-dd: ");
                        string input = Console.ReadLine();
                        if (input != "")
                        {
                            DateTime utTiden = DateTime.MinValue;
                            DateTime.TryParse(input, out utTiden);
                            while (utTiden == DateTime.MinValue)
                            {
                                Console.Write($"Försök igen, format (YYYY-MM-DD): ");
                                DateTime.TryParse(Console.ReadLine(), out utTiden);
                            }

                            DateTime återTiden = DateTime.MinValue;
                            while (återTiden == DateTime.MinValue)
                            {
                                Console.Write($"Till (YYYY-MM-DD hh:mm): ");
                                DateTime.TryParse(Console.ReadLine(), out återTiden);
                            }
                            DateTime faktiskTid = DateTime.Now;


                            Console.WriteLine("Ange medlemsnummer: ");
                            
                            int medlemsnr = int.Parse(Console.ReadLine());
                            Medlem medlem = kontroller.Hittamedlem(medlemsnr);


                            IList<Bok> tillgänglig = kontroller.HämtaTillgängligaBöcker();
                            int i = 1;
                            Console.WriteLine("** Tillgängliga böcker **");
                            foreach (Bok b in tillgänglig)
                            {
                                Console.Write("{0}. ", i++);
                                BokUtskrift(b);
                            }
                            
                            Console.Write("Ange namn på bok som ska läggas till i bokningen: ");
                            string boknamn = Console.ReadLine();
                            
                            if (boknamn != string.Empty)
                            {
                                Console.WriteLine($"{boknamn} har lagts till i bokning");

                                foreach (Bok b in boknamn)
                                {
                                    IList<Bok> bokadeBöcker = kontroller.BokTillBokning(bokadeBöcker);
                                } 
                                
                            }
                            

                            Expidit ee = kontroller.Autentisering;

                                kontroller.SkapaBokning(uniktBokNR++, ee, medlem, utTiden, återTiden, faktiskTid, bokadeBöcker); // bara faktisktid som behöver hanteras när vi fixar återlämning av bok
                            
                        }
                        break;

                    case 2:
                        Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); // bara snabbtest, funkar nu men inte testat utförligt, färdig 2355
                        int svar = int.Parse(Console.ReadLine());
                        Bokning boknn = kontroller.VisaBokning(svar);
                        if (svar != null)
                        {
                            Console.WriteLine("** Din bokning **");

                                BokningUtskrift(boknn);
                        }

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
        private void BokUtskrift(Bok b)
        {
            Console.WriteLine(b.Titel, b.ISBN);
        }

        private void BokningUtskrift(Bokning bo) // snabb while loop, för undvika skapa string variabel + konvertera int variabler till string i foreach
        {
            bool x = true;
            while (x)
            {
                Console.WriteLine($"Bokningsnummer: {bo.BokningsNr}" + " " +
                            $" Bokningshanterar: {bo.Expidit.AnstNr} " + " " +
                            $"Medlemsnummer: {bo.Medlem.MedlemsNr}" + " " +
                            $" Planerat uthyrningsdatum: {bo.UtTid}" + " " +
                            $" Planerat återlämningsdatum {bo.ÅterTid}" + " " +
                            $" Aktuellt återlämningsdatum {bo.FaktisktUtTid}");
                x = false;
            }


        }



        private Kontroller kontroller;
    }
}
