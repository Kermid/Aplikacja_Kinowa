using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static Aplikacja_Kinowa.Program;

namespace Aplikacja_Kinowa
{
    internal class Program
    {
        public class Sala
        {
            public int numerSala;
            public int[,] miejsca;
            public int rzad = 0;
            public int kolumna = 0;
            public Seans seans;
            public int iloscMiejsc;
            public List<Seans> listaSeansow = new List<Seans>();
            public bool[,] Rezerwacja;
            public class Seans
            {

                public string tytul;
                public int minWiek;
                public string czasTrwaniaSeansu;
                

            }



        }

        public static void DodawanieSeans(Sala sala)
        {
            Console.WriteLine("*******************");
            Sala.Seans seans = new Sala.Seans();
            
                Console.WriteLine("Podaj tytul filmu.");
                seans.tytul = Console.ReadLine();
                Console.WriteLine("Podaj godzine seansu (przedzial).");
                seans.czasTrwaniaSeansu = Console.ReadLine();
                Console.WriteLine("Podaj minimalny wiek ograniczajacy.");
            try
            {
                seans.minWiek = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Wpisana wartość musi być liczbą całkowitą.");
            }
            sala.listaSeansow.Add(seans);
        }

        public static void Wyswietlanie(Sala sala)
        {
            
            Console.WriteLine("*******************");
            //Sala.Seans seans = new Sala.Seans();

            Console.WriteLine($"\nSala: {sala.numerSala} Ilość miejsc: {sala.iloscMiejsc}");
            //Console.WriteLine($"Tytul filmu: {seans.tytul} Długość seansu: {seans.czasTrwaniaSeansu} Wiek: {seans.minWiek}");
            for (int i = 0; i < sala.listaSeansow.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Tytuł: {sala.listaSeansow[i].tytul} Minimalny wiek: {sala.listaSeansow[i].minWiek} Czas trwania seansu: {sala.listaSeansow[i].czasTrwaniaSeansu}");
            }


        }

        public static void DodawanieSala(List<Sala> listaSal)
        {
            Console.WriteLine("*******************");
            Sala sala = new Sala();

            Console.WriteLine("Podaj numer sali");
            try
            {
                sala.numerSala = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Podawana liczba musi być liczbą całkowitą.");
                return;
            }

                bool czySalaIstnieje = listaSal.Any(x => x.numerSala == sala.numerSala);

            if (!czySalaIstnieje)
            {
                Console.WriteLine("Podaj liczbe miejsc w sali (rzad, kolumna).");
                try
                {
                    sala.rzad = int.Parse(Console.ReadLine());
                    sala.kolumna = int.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("Wpisana wartość musi być liczbą całkowitą.");
                }

                if (sala.rzad < 0 || sala.kolumna < 0)
                {
                    Console.WriteLine("Wartość rzędu i kolumny musi być wieksza od 0");
                    return;
                }
                sala.Rezerwacja = new bool[sala.rzad, sala.kolumna];
                sala.miejsca = new int[sala.rzad, sala.kolumna];
                sala.iloscMiejsc = sala.rzad * sala.kolumna;
                /*for (int i = 0; i < sala.miejsca.Length; i++)
                {
                    sala.miejsca[i].
                }*/
                listaSal.Add(sala);

                int numerMiejsca = 1;
                for (int i = 0; i < sala.rzad; i++)
                {


                    for (int j = 0; j < sala.kolumna; j++)
                    {

                        sala.miejsca[i, j] = numerMiejsca;
                        numerMiejsca++;
                    }
                }

                Console.WriteLine("Wartości zostały dodane do tablicy.");
            }
            else
            {
                Console.WriteLine("Sala z takim numerem już istnieje.");
            }
        
            

        }
        public static void UsuwanieFilm(List<Sala> listaSal)
        {
            Console.WriteLine("*******************");
            /* foreach (Sala sala in listaSal)
             {
                 Wyswietlanie(sala);

             }*/
            while (true)
            {
                foreach (Sala sala in listaSal)
                {
                    Wyswietlanie(sala);
                }
                
                if(listaSal.Count > 0)
                {
                    Console.WriteLine("Podaj nr sali w ktorej chcesz usunać film.");
                    int wyborSali = int.Parse(Console.ReadLine());

                    if (wyborSali > 0 && wyborSali <= listaSal.Count)
                {
                    Sala sala = listaSal[wyborSali - 1];
                    Console.WriteLine($"Filmy na sali: {wyborSali}");
                    if (wyborSali > 0 && wyborSali <= listaSal.Count)
                    {
                        for (int i = 0; i < sala.listaSeansow.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. Tytuł: {sala.listaSeansow[i].tytul} Minimalny wiek: {sala.listaSeansow[i].minWiek} Czas trwania seansu: {sala.listaSeansow[i].czasTrwaniaSeansu}");
                        }

                        Console.WriteLine("Podaj numer filmu który chcesz usunąć.");
                        int numerUsuwanegoFilmu = int.Parse(Console.ReadLine());
                        if (numerUsuwanegoFilmu > 0 && numerUsuwanegoFilmu <= sala.listaSeansow.Count)
                        {
                            sala.listaSeansow.RemoveAt(numerUsuwanegoFilmu - 1);
                        }
                        else if (sala.listaSeansow.Count == 0)
                        {
                            Console.WriteLine("Nie ma filmów do usunięcia w tej sali.");
                            break;
                        }
                        else if (numerUsuwanegoFilmu < 0 && numerUsuwanegoFilmu > sala.listaSeansow.Count)
                        {
                            Console.WriteLine("Numer filmu poza zakresem.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Losowy błąd");
                            break;
                        }
                        Console.WriteLine("Czy chcesz usunąć kolejny film T/N?");
                        if (Console.ReadLine().ToUpper() == "N")
                            break;
                    }
                }
                   
                   
                else
                {
                    Console.WriteLine("Nieprawidłowy numer sali.");
                    break;
                }
                
            }
                else if (listaSal.Count== 0)
                {
                    Console.WriteLine("Nie ma sal żeby usunąć z nich film.");
                    break;
                }
            }
        }
        public static void Rezerwacja(List<Sala> listaSal)
        {
           
            
            if (listaSal.Count > 0)
            {
                Console.WriteLine("*******************");

                Console.WriteLine("Podaj numer sali w której chcesz zarezerwować miejsce siedzące.");
                foreach (Sala sala in listaSal)
                {
                    Wyswietlanie(sala);
                }
                int wyborSali = int.Parse(Console.ReadLine());
                if (wyborSali > 0 && wyborSali <= listaSal.Count)
                {
                    Sala sala = listaSal[wyborSali - 1];
                    Console.WriteLine($"Filmy na sali: {wyborSali}");
                    for (int i = 0; i < sala.listaSeansow.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. Tytuł: {sala.listaSeansow[i].tytul} Minimalny wiek: {sala.listaSeansow[i].minWiek} Czas trwania seansu: {sala.listaSeansow[i].czasTrwaniaSeansu}");
                    }
                    if (sala.listaSeansow.Count > 0)
                    {


                        Console.WriteLine("Podaj numer filmu który chcesz zarezerwować");
                        int numerRezerwowanegoFilmu = int.Parse(Console.ReadLine());
                        if (numerRezerwowanegoFilmu > 0 && numerRezerwowanegoFilmu <= sala.listaSeansow.Count)
                        {
                            Console.WriteLine($"Wybrano film: {sala.listaSeansow[numerRezerwowanegoFilmu - 1].tytul}");
                            WyswietlanieMiejsc(sala);

                            Console.WriteLine("Podaj numer rzędu, w którym chcesz zarezerwować miejsce:");
                            int numerRzedu = int.Parse(Console.ReadLine()) - 1;
                            Console.WriteLine("Podaj numer kolumny, w której chcesz zarezerwować miejsce:");
                            int numerKolumny = int.Parse(Console.ReadLine()) - 1;


                            if (numerRzedu >= 0 && numerRzedu < sala.rzad && numerKolumny >= 0 && numerKolumny < sala.kolumna)
                            {
                                if (!sala.Rezerwacja[numerRzedu, numerKolumny])
                                {
                                    sala.Rezerwacja[numerRzedu, numerKolumny] = true;
                                    Console.WriteLine($"Miejsce {sala.miejsca[numerRzedu,numerKolumny]} zostało zarezerwowane.");
                                }
                                else
                                {
                                    Console.WriteLine("To miejsce jest już zarezerwowane.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nieprawidłowy numer rzędu lub kolumny.");
                            }
                        }
                        else if (sala.listaSeansow.Count == 0)
                        {
                            Console.WriteLine("Nie ma filmów w tej sali");
                        }
                        else if (numerRezerwowanegoFilmu < 0)
                        {
                            Console.WriteLine("Numer filmu nie może być ujemny");
                        }
                        else if (numerRezerwowanegoFilmu > sala.listaSeansow.Count)
                        {
                            Console.WriteLine("Nieprawidłowy numer filmu");
                        }
                    }
                    else if (sala.listaSeansow.Count == 0)
                    {
                        Console.WriteLine("Nie ma filmów do rezerwacji");
                    }
                }
                else if (wyborSali > listaSal.Count)
                {
                    Console.WriteLine("Nieprawidłowy numer sali.");
                }
                
                
            }
            else if (listaSal.Count == 0)
            {
                Console.WriteLine("Nie ma sal do rezerwacji.");
            }

        }
        public static void ModyfikacjaSala(List<Sala> listaSal)
        {
            Console.WriteLine("*******************");
            foreach (Sala sala in listaSal)
            {
                Wyswietlanie(sala);
            }
            
            

            if (listaSal.Count > 0)
            {
                Console.WriteLine("Podaj numer sali którą chcesz zmodyfikować");
                int wyborSali = int.Parse(Console.ReadLine());
                if (wyborSali > 0 && wyborSali <= listaSal.Count)
                {
                    wyborSali--;
                    Console.WriteLine("Podaj nowa wartość rzędów.");
                    listaSal[wyborSali].rzad = int.Parse(Console.ReadLine());
                    Console.WriteLine("Podaj nowaą wartość kolumn");
                    listaSal[wyborSali].kolumna = int.Parse(Console.ReadLine());
                    int numerMiejsca = 1;
                    listaSal[wyborSali].miejsca = new int[listaSal[wyborSali].rzad, listaSal[wyborSali].kolumna];
                    //listaSal[wyborSali].numerSala = wyborSali+1;
                    for (int i = 0; i < listaSal[wyborSali].rzad; i++)
                    {


                        for (int j = 0; j < listaSal[wyborSali].kolumna; j++)
                        {

                            listaSal[wyborSali].miejsca[i, j] = numerMiejsca;
                            numerMiejsca++;
                        }
                    }
                    Console.WriteLine("Wartości zostały zmienione w tablicy.");
                }
                else if (wyborSali >listaSal.Count)
                {
                    Console.WriteLine("Nieprawidłowy numer sali.");
                }
                else if (listaSal.Count == 0)
                {
                    Console.WriteLine("Nie ma sal do zmodyfikowania.");
                }

            }
            else if (listaSal.Count == 0)
            {
                Console.WriteLine("Nie ma sal do zmodyfikowania.");
            }
        }
        public static void WyswietlanieMiejsc(Sala sala)
        {
            Console.WriteLine("*******************");
            Console.WriteLine($"\nMiejsca w sali {sala.numerSala}:");

            for (int i = 0; i < sala.rzad; i++)
            {
                for (int j = 0; j < sala.kolumna; j++)
                {
                    Console.Write(sala.miejsca[i, j] + " ");

                }
                Console.WriteLine();
            }
            
        }
        public static void ModyfikacjaFilm(List<Sala> listaSal)
        {
           
            
            if (listaSal.Count > 0)
            {
                Console.WriteLine("*******************");
                Console.WriteLine("Podaj numer sali w której chcesz zmodyfikować film.");
                foreach (Sala sala in listaSal)
                {
                    Wyswietlanie(sala);
                }
                int wyborSali = int.Parse(Console.ReadLine());
                if (wyborSali > 0 && wyborSali <= listaSal.Count)
                {
                    Sala sala = listaSal[wyborSali - 1];
                    for (int i = 0; i < sala.listaSeansow.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. Tytuł: {sala.listaSeansow[i].tytul} Minimalny wiek: {sala.listaSeansow[i].minWiek} Czas trwania seansu: {sala.listaSeansow[i].czasTrwaniaSeansu}");
                    }
                    Console.WriteLine("Podaj numer filmu który chcesz zmodyfikować.");
                    int numerModyfikowanegoFilmu = int.Parse(Console.ReadLine()) - 1;
                    if (numerModyfikowanegoFilmu >= 0 && numerModyfikowanegoFilmu <= listaSal.Count)
                    {
                        while (true)
                        {
                            Console.WriteLine("Czy chcesz zmienić tytuł T/N?");
                            if (Console.ReadLine().ToUpper() == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Podaj nowy tytuł do zmiany.");
                                sala.listaSeansow[numerModyfikowanegoFilmu].tytul = Console.ReadLine();

                            }
                        }
                        while (true)
                        {
                            Console.WriteLine("Czy chcesz zmienić czas trwania seansu T/N?");
                            if (Console.ReadLine().ToUpper() == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Podaj nowy przedzial seansu.");
                                sala.listaSeansow[numerModyfikowanegoFilmu].czasTrwaniaSeansu = Console.ReadLine();
                            }
                        }
                        while (true)
                        {
                            Console.WriteLine("Czy chcesz zmienić minimalny wiek seansu T/N?");
                            if (Console.ReadLine().ToUpper() == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Podaj nowy wiek minimalny do zmiany.");
                                sala.listaSeansow[numerModyfikowanegoFilmu].minWiek = int.Parse(Console.ReadLine());
                            }
                        }
                    }
                    else if (numerModyfikowanegoFilmu < 0 || numerModyfikowanegoFilmu > listaSal.Count)
                    {
                        Console.WriteLine("Numer filmu jest poza przedziałem.");
                        
                    }
                    else
                    {
                        Console.WriteLine("błąd");
                    }
                }

            }
            else if (listaSal.Count == 0) 
            {
                Console.WriteLine("Nie ma sal.");
            }
        } 
            static void UsuwanieSala(List<Sala> listaSal)
            {
            Console.WriteLine("*******************");
            while (true)
                {
                    foreach (Sala sala in listaSal)
                    {
                        Wyswietlanie(sala);

                    }
                    
                    if (listaSal.Count > 0)
                    {
                    Console.WriteLine("Podaj numer sali ktorą chcesz usunąć.");
                    int wyborUsuwanieSala = int.Parse(Console.ReadLine());

                    if (wyborUsuwanieSala > 0 && wyborUsuwanieSala <= listaSal.Count)
                        {
                            wyborUsuwanieSala--;
                            listaSal.RemoveAt(wyborUsuwanieSala);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie ma sal.");
                        break;
                    }


                }
            }


           static void Main(string[] args)

            { 
                
                List<Sala> listaSal = new List<Sala>();
                List<Sala.Seans> listaSeansow = new List<Sala.Seans>();
                while (true)
                {
                    Console.WriteLine("*******************");
                    Console.WriteLine("1-Dodaj sale.");
                    Console.WriteLine("2-Dodaj film do sali.");
                    Console.WriteLine("3-Usuwanie.");
                    Console.WriteLine("4-Modyfikacja.");
                    Console.WriteLine("5-Lista.");
                    Console.WriteLine("6-Rezerwacja.");
                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        while (true)
                        {
                            DodawanieSala(listaSal);

                            Console.WriteLine("Czy dodać kolejną salę T/N?");
                            if (Console.ReadLine().ToUpper() == "N")
                                break;
                        }
                        break;
                    case "2":
                        while (true)
                        {
                            if (listaSal.Count != 0)
                            {
                                Console.WriteLine("Podaj numer sali do której chcesz dodać seans.");
                                try
                                {
                                    int wybranaSala = int.Parse(Console.ReadLine());
                                    DodawanieSeans(listaSal.ElementAt(wybranaSala - 1));
                                }
                                catch(FormatException)
                                {
                                    Console.WriteLine("Wpisana wartość musi być liczbą całkowitą.");
                                    return;
                                }

                                
                                Console.WriteLine("Czy dodać kolejny seans T/N?");
                                if (Console.ReadLine().ToUpper() == "N")
                                    break;
                            }
                            else
                            {
                                Console.WriteLine("NIE MA SAL ZEBY DODAC DO NICH SEANS!!!");
                                break;
                            }
                        }

                        break;

                    case "3":
                        Console.WriteLine("*******************");
                        Console.WriteLine("1-Usunięcie sali.");
                        Console.WriteLine("2-Usunięcie Filmu.");
                        string wyborUsuwanie = Console.ReadLine();
                        switch (wyborUsuwanie)
                        {
                            case "1":
                                UsuwanieSala(listaSal);

                                break;
                            case "2":
                                UsuwanieFilm(listaSal);
                                break;
                            default:
                                Console.WriteLine("Nieprawidłowa opcja.");

                                break;
                        }

                        break;
                    case "4":
                        Console.WriteLine("*******************");
                        Console.WriteLine("1-Modyfikacja sali.");
                        Console.WriteLine("2-Modyfikacja filmu.");
                        string wyborModyfikacja = Console.ReadLine();
                        switch (wyborModyfikacja)
                        {
                            case "1":
                                ModyfikacjaSala(listaSal);

                                break;
                            case "2":
                                ModyfikacjaFilm(listaSal);
                                break;
                            default:
                                Console.WriteLine("Nieprawidłowa opcja.");

                                break;
                        }

                        break;
                    case "5":
                        Console.WriteLine("*******************");
                        Console.WriteLine("1-Lista sal i filmów.");
                        Console.WriteLine("2-Układ sali i miejsc.");
                        string wyborLista = Console.ReadLine();
                        switch(wyborLista)
                        {
                            case "1":
                                foreach (Sala sala in listaSal)
                                {
                                    Wyswietlanie(sala);


                                }
                                break;
                            case "2":
                                foreach (Sala sala in listaSal)
                                {
                                    WyswietlanieMiejsc(sala);
                                }

                                break;
                            default:
                                Console.WriteLine("Nieprawidłowa opcja.");

                                break;

                        }
                        break;
                    case "6":
                        Rezerwacja(listaSal);
                        break;

                    default :
                        Console.WriteLine("Nieprawidłowa opcja.");

                        break;
                        


                }
                }
            }
        }
    }

