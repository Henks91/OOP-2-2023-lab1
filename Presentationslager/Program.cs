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
            DateTime faktiskUtTid = default(DateTime);
            DateTime tillbaka = default(DateTime);
            bool stängNer = true; // Variabel för att avsluta programmet vid specifikt menyval
            while (stängNer)
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
                Console.WriteLine("4: Logga ut");
                Console.ForegroundColor = ConsoleColor.White;

                switch (inmatninguINT("Svara med en siffra för att göra ett val: ")) // om användaren matar in ett alfabetiskt värde kommer "RättSiffra" märka det och presentera ett felmeddelande.
                {
                    case 1:

                        Console.Clear();
                        Console.WriteLine("Från vilket datum vill du boka boken/böckerna: ");
                        string input = Console.ReadLine();
                        if (input != "")
                        {
                            DateTime från; // = DateTime.MinValue;
                            DateTime.TryParse(input, out från);
                            while (från == DateTime.MinValue)
                            {
                                Console.Write($"Försök igen, format (YYYY-MM-DD): ");
                                DateTime.TryParse(Console.ReadLine(), out från);
                            }
                            
                         
                            //DateTime faktiskUtTid = default(DateTime);  placeras utanför while-loopen ovan på rad 80

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
                            List<Bok> ProvBok = new List<Bok>();
                            bool avslut = false;
                            while (!avslut)
                            {                                
                                Console.Write("Ange namn på bok som ska läggas till i bokningen: ");
                                string boknamn = Console.ReadLine().ToLower();
                                
                                Bok b = kontroller.HittaBok(boknamn);
                                
                                if (b != null)
                                {                                    
                                    Console.WriteLine($"'{b.Titel[0].ToString().ToUpper()}{b.Titel.Substring(1)}' har lagts till i bokning");
                                    ProvBok.Add(b);                                   
                                }
                                Console.WriteLine("Viil du lägga till en till bok i bokningen? \n Skriv 'J' för 'JA' och 'N' för 'NEJ': ");
                                string val = Console.ReadLine().ToUpper();
                                if (val == "N")
                                {                                  
                                    avslut = true;                                   
                                }
                            }
                            kontroller.BokTillBokning(ProvBok);
                            Bokning bc= kontroller.SkapaBokning(medlem, från , tillbaka, faktiskUtTid, ProvBok); // bara faktisktid som behöver hanteras när vi fixar återlämning av bok
                            Console.WriteLine($"Din bokning har: {bc.BokningsNr} som bokningsnummer.");
                        }
                        break;

                    case 2:
                        
                        Console.Clear();
                       
                            Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); // bara snabbtest, funkar nu men inte testat utförligt, färdig 23:55
                            int svar = int.Parse(Console.ReadLine());
                            Bokning bokning = kontroller.VisaBokning(svar);
                            
                            Console.WriteLine("** Din bokning **");
                            BokningUtskrift(bokning);
                            bokning.Upphämtad();
                            bokning.FaktisktUtTid = bokning.UtTid;
                            bokning.ÅterTid = bokning.FaktisktUtTid.AddDays(+14);
                            Console.WriteLine("Boken skall lämnas tillbaka senast: {0}", bokning.ÅterTid);                           
                        
                                                
                        break;

                    case 3:
                        Console.Clear();
                            
                        Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); // bara snabbtest, funkar nu men inte testat utförligt, färdig 23:55
                        int svar1 = int.Parse(Console.ReadLine());

                        Bokning bokning1 = kontroller.VisaBokning(svar1);                        
                        if (bokning1.UppHämtad == false)
                        {
                            Console.WriteLine("Du kan inte lämna tillbaka en bokning som inte hämtats ut...");
                        }
                        else
                        {                           
                            Console.WriteLine("** Din bokning **");
                            BokningUtskrift(bokning1);
                            bokning1.InteUppHämtad();
                            Console.WriteLine("DU SKA BETALA DIN JÄVEL");
                            foreach (Bok b in bokning1.BokadeBöcker)
                            {
                                b.Tillgänglig();
                            }

                            Faktura f = kontroller.SkapaFaktura(bokning1);
                            FakturaUtskrift(f);
                            Console.ReadLine();
                        }                                             
                        break;

                    case 4:
                        Console.Clear();

                        stängNer = false;
                        
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
                Console.WriteLine($" Bokningsnummer: {bo.BokningsNr}" + " \n" +
                            $" Bokad av: {bo.Expidit.AnstNr} " + " \n" +
                            $" Medlemsnummer: {bo.Medlem.MedlemsNr}" + " \n" +
                            $" Planerat uthyrningsdatum: {bo.UtTid}" + " ");
                            //$" Aktuellt återlämningsdatum {bo.FaktisktUtTid}"); Ska vara med vid återlämning av böcker
                foreach (Bok b in bo.BokadeBöcker)
                {
                    Console.WriteLine(b.Titel, b.ISBN);
                }
                x = false;
            }
        }

        private void FakturaUtskrift(Faktura f)
        {
            Console.WriteLine($"**** Faktura ****\n"+
                $"Fakturan är skapad av: {f.Expidit.Namn}\n" +
                $"Fakturan avser {f.Bokning.Medlem.Namn} med medlemsnummer: {f.Bokning.Medlem.MedlemsNr}\n" +
                $"Återlämningsdatum för bokningen var: {f.FaktiskÅterTid}\n"+
                $"Du skall betala: {f.TotalPris} kr");
        }
        private Kontroller kontroller;
    }
}
