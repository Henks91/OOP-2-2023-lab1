using Affärslager;
using Entiteter;
using System;
using System.Collections.Generic;

namespace Presentationslager
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Applikation();
        }

        private Program() //instansierar en kontroller som sedan anropas för att nå metoder vid körning.
        {
            kontroller = new Kontroller();
        }
        private Kontroller kontroller;
        #region Inlogg 
        private void Applikation()
        {
            Console.WriteLine("Inloggning för Expiditapplikation");
            while (true)
            {
                try
                {
                    if (Inloggning())
                    {
                        Console.Clear();
                        Console.WriteLine("Inloggningen lyckades, välkommen ", kontroller.Autentisering.Namn);
                        Console.WriteLine();
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
        #endregion   
        #region Inmatningskontroll
        static uint inmatninguINT(string inmatningssträng)  // kontrollerar inmatning från användaren
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
        #endregion
        #region Bokningsmenyn
        private void Menyn()
        {
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

                switch (inmatninguINT("Svara med en siffra för att göra ett val: ")) // anropar inmatningsmetoden som hanterar inmatningen av val
                {
                    case 1:

                        Console.Clear();
                        Console.WriteLine("Från vilket datum vill du boka boken/böckerna (YYYY-MM-DD): ");
                        string input = Console.ReadLine();
                        if (input != "")
                        {
                            DateTime från;
                            DateTime.TryParse(input, out från);
                            while (från == DateTime.MinValue)
                            {
                                Console.Write("Försök igen, format (YYYY-MM-DD): ");
                                DateTime.TryParse(Console.ReadLine(), out från);
                            }

                            Console.WriteLine("Ange medlemsnummer: ");

                            int medlemsnr = int.Parse(Console.ReadLine());
                            Medlem medlem = kontroller.Hittamedlem(medlemsnr);

                            Console.Clear();

                            List<Bok> ProvBok = new List<Bok>();
                            bool avslut = false;
                            IList<Bok> tillgänglig = kontroller.HämtaTillgängligaBöcker();  //hämtar tillgängliga böcker från BokRepository
                            int i = 1;

                            Console.WriteLine("**** Tillgängliga böcker ****");
                            foreach (Bok bU in tillgänglig)
                            {
                                Console.Write("{0}. ", i++);
                                BokUtskrift(bU);
                            }
                            while (!avslut)
                            {                                                           
                                Console.Write("\nAnge titel på bok som ska läggas till i bokningen: ");
                                string boknamn = Console.ReadLine().ToLower();

                                Bok b = kontroller.HittaBok(boknamn); //jämför inmatat boknamn mot Boktitlar i BokRepository

                                if (b != null)
                                {
                                    Console.WriteLine($"{b.Titel[0].ToString().ToUpper()}{b.Titel.Substring(1)} har lagts till i bokning");
                                    ProvBok.Add(b);                                   
                                }

                                Console.WriteLine("\nVill du lägga till en till bok i bokningen? \n Skriv 'J' för 'JA' och 'N' för 'NEJ': ");
                                string val = Console.ReadLine().ToUpper();
                                if (val == "N")
                                {
                                    avslut = true;
                                }
                            }
                            Bokning bc = kontroller.SkapaBokning(medlem, från, ProvBok); 
                            Console.WriteLine($"\nDin bokning har: {bc.BokningsNr} som bokningsnummer.");
                        }
                        break;

                    case 2:

                        Console.Clear();

                        Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); 
                        int svar = int.Parse(Console.ReadLine());
                        Bokning bokning = kontroller.UtlämningAvBöcker(svar);
                        if (bokning.StartLån < DateTime.Now)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Din bokning ****");
                            BokningUtskrift(bokning);
                            Console.WriteLine("\nTryck på ENTER för att komma vidare till menyn.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"Du kan hämta ut din bok tidigast: {bokning.StartLån}");
                        }

                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Ange bokningsnummer eller medlemsnummer för att visa bokning: "); 
                        int svar1 = int.Parse(Console.ReadLine());

                        Bokning bokning1 = kontroller.LämnaTillbakaBok(svar1);
                        if (bokning1.UppHämtad == false)
                        {
                            Console.Clear();
                            Console.WriteLine("Du kan inte lämna tillbaka en bokning som inte hämtats ut...");
                        }
                        else if (bokning1.Återlämnad == true)
                        {
                            Console.Clear();
                            Console.WriteLine("Du kan inte lämna tillbaka en bokning som redan lämnats in...");
                        }
                        else
                        {
                            Console.WriteLine("**** Din bokning ****");
                            BokningUtskrift(bokning1);
                            Console.WriteLine();
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
        #endregion
        #region Utskrifter
        private void BokUtskrift(Bok b)
        {
            Console.WriteLine($"Titel: {b.Titel[0].ToString().ToUpper()}{b.Titel.Substring(1)}");
        }

        private void BokningUtskrift(Bokning bo) 
        {
            bool x = true;
            while (x)
            {
                Console.WriteLine($" Bokningsnummer: {bo.BokningsNr}" + " \n" +
                            $" Bokad av: {bo.Expidit.AnstNr} " + " \n" +
                            $" Medlemsnummer: {bo.Medlem.MedlemsNr}" + " \n" +
                            $" Planerat uthyrningsdatum: {bo.StartLån}" + "\n" +
                            $" Planerat återlämningsdatum: {bo.StartLån.AddDays(+14)}");
                foreach (Bok b in bo.BokadeBöcker)
                {
                    BokUtskrift(b);
                }
                x = false;
            }
        }

        private void FakturaUtskrift(Faktura f)
        {
            Console.WriteLine($"\n**** Faktura ****\n" +
                $"Fakturan är skapad av: {f.Expidit.Namn}\n" +
                $"Fakturan avser {f.Bokning.Medlem.Namn} med medlemsnummer: {f.Bokning.Medlem.MedlemsNr}\n" +
                "$Titel på böcker som fakturan avvser: ");
            foreach (Bok b in f.Bokning.BokadeBöcker)
            {
                BokUtskrift(b);
            }
            Console.WriteLine($"Återlämningsdatum för bokningen var: {f.FaktiskÅterTid}\n" +
                $"Du skall betala: {f.TotalPris} kr");
        }
        #endregion Utskrifter
    }
}
