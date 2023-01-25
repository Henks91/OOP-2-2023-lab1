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

        //public int uniktBokNR = 1;
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
                Console.ForegroundColor = ConsoleColor.White;

                switch (inmatninguINT("Svara med en siffra för att göra ett val: ")) // om användaren matar in ett alfabetiskt värde kommer "RättSiffra" märka det och presentera ett felmeddelande.
                {
                    case 1:
                        List<Bok> ProvBok = new List<Bok>();
                        Console.WriteLine("Från vilket datum vill du boka boken/böckerna: ");
                        DateTime från = DateTime.Parse(Console.ReadLine());
                        DateTime tillbaka = från.AddDays(+14);
                        Console.WriteLine($"Ditt återlämningsdatum är: {tillbaka}");
                        kontroller.BokTillBokning(ProvBok);

                        Console.Clear();
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
                            DateTime faktiskTid = default(DateTime);


                            Console.WriteLine("Ange medlemsnummer: ");
                            
                            int medlemsnr = int.Parse(Console.ReadLine());
                            Medlem medlem = kontroller.Hittamedlem(medlemsnr);

                            Console.Clear();

                            IList<Bok> tillgänglig = kontroller.HämtaTillgängligaBöcker();
                            int i = 1;
                            Console.WriteLine("** Tillgängliga böcker **");
                            foreach (Bok b in tillgänglig)
                            {
                                Console.Write("{0}. ", i++);
                                BokUtskrift(b);
                            }
                            bool avslut = false;
                            //List<Bok> ProvBok = new List<Bok>();
                            while (!avslut)
                            {
                                
                                Console.Write("Ange namn på bok som ska läggas till i bokningen: ");
                                string boknamn = Console.ReadLine();
                                
                                Bok b = kontroller.HittaBok(boknamn);
                                
                                if (b != null)
                                {                                    
                                    Console.WriteLine($"{b.Titel} har lagts till i bokning");
                                    ProvBok.Add(b);                                 

                                }
                                Console.WriteLine("Viil du lägga till en till bok i bokningen? \n Skriv 'J' för 'JA' och 'N' för 'NEJ': ");
                                string val = Console.ReadLine().ToUpper();
                                if (val == "N")
                                {
                                   
                                    avslut = true;
                                    
                                }
                            }
                            //Console.WriteLine("Från vilket datum vill du boka boken/böckerna: ");
                            //DateTime från = DateTime.Parse(Console.ReadLine());
                            //DateTime tillbaka = från.AddDays(+14);
                            //Console.WriteLine($"Ditt återlämningsdatum är: {tillbaka}");
                            //kontroller.BokTillBokning(ProvBok);
                            

                            Expidit ee = kontroller.Autentisering;

                            Bokning bc= kontroller.SkapaBokning(ee, medlem, från , tillbaka, faktiskTid, ProvBok); // bara faktisktid som behöver hanteras när vi fixar återlämning av bok
                            Console.WriteLine($"Din bokning har: {bc.BokningsNr} som bokningsnummer.");
                        }
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); // bara snabbtest, funkar nu men inte testat utförligt, färdig 23:55
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

                Console.WriteLine($"Bokningsnummer: {bo.BokningsNr}" + " \n" +
                            $" Bokad av: {bo.Expidit.AnstNr} " + " \n" +
                            $" Medlemsnummer: {bo.Medlem.MedlemsNr}" + " \n" +
                            $" Planerat uthyrningsdatum: {bo.UtTid}" + " \n" +
                            $" Planerat återlämningsdatum {bo.ÅterTid}" + " \n");
                            //$" Aktuellt återlämningsdatum {bo.FaktisktUtTid}"); Ska vara med vid återlämning av böcker
                foreach (Bok b in bo.BokadeBöcker)
                {
                    Console.WriteLine(b.Titel, b.ISBN);
                }

                x = false;
            }
        }



        private Kontroller kontroller;
    }
}
