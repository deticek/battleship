using System;

class Game
{

    static Random r = new Random();
    static int vp; //velikost polja
    static int sp; //stevilo poskusov
    static string[,] map;
    static string[,] pmap;
    static string vnos = "";
    static int sl=0; //stevilo ladi

    static void createmap()
    {
        for(int i = 0; i != vp; i++)
        {
            for(int j = 0; j != vp; j++)
            {
                map[i,j] = "-";
                pmap[i,j] = "-";
            }
        }
    }

    static void izpismape()
    {

        Console.Write("   ");
        for (int j = 0; j < vp; j++)
        {
            char stolpec = (char)('A' + j);
            Console.Write(stolpec + " ");
        }
        Console.WriteLine();


        for (int i = 0; i != vp; i++)
        {
            Console.Write((i + 1).ToString().PadLeft(2) + " ");
            for (int j = 0; j != vp; j++)
            {
                Console.Write(map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    static void izpis()
    {

        Console.Write("   ");
    for (int j = 0; j < vp; j++)
    {
        char stolpec = (char)('A' + j);
        Console.Write(stolpec + " ");
    }
        Console.WriteLine();
    

        for (int i = 0; i != vp; i++)
        {
            Console.Write((i + 1).ToString().PadLeft(2) + " ");
            for (int j = 0; j != vp; j++)
            {
                Console.Write(pmap[i, j]+" ");
            }
            Console.WriteLine();
        }
    }

    static string pridobivnos()
    {
        Console.WriteLine($"Imate še {sp} poskusov");
        do
        {
            Console.Write("Vnesite koordinatoe npr: A1: ");
            string v = Console.ReadLine().Trim().ToUpper();

            if (v.Length < 2)
            {
                Console.WriteLine("Napačen vnos. Podati morete prvo črko potem pa številko!");
                continue;
            }

            char c = v[0];
            if (c < 'A' || c >= 'A' + vp)
            {
                Console.WriteLine("Ta stolpec ne obstaja!");
                continue;
            }

            int n;
            if (!int.TryParse(v.Substring(1), out n) || n < 1 || n > vp)
            {
                Console.WriteLine("Ta vrsta ne obstaja!");
                continue;
            }

            vnos += v + " ";
            return v;

        } while (true);
    }   

    static void igraj()
    {
        izpis();
        int st=0;
        do
        {
            string o = pridobivnos();
            Console.Clear();
            int x = (char)(o[0]) - 'A';
            int y = (int.Parse(o.Substring(1)))-1;

            if (pmap[y, x] == "X" || pmap[y,x]=="O")
            {
                izpis();
                Console.WriteLine("To mesto ste že odkrili!");
                continue;
            }

            if (map[y, x] == "L")
            {
                pmap[y, x] = "O";
                izpis();
                Console.WriteLine("Bravo zadeli ste ladijo!");
                st++;
                    if (st == sl)
                    {
                        Console.WriteLine("Bravo!!!! Odkrili ste vse ladice :D");
                        return;
                    }
                continue;
            }
            else
            {
                sp--;
                
                pmap[y, x] = "X";
                izpis();
                Console.WriteLine("Na tem mestu ni ladje!");

                if (sp == 0)
                {
                    Console.WriteLine("Zmajnkalo vam je poskusov!");
                    return;
                }
            }
        } while (true);

    }

    static bool fraj(int x, int y, int m, bool v)
    {
        if (v)
    {
        if (y + m > vp) return false;

        for (int i = 0; i < m; i++)
        {
            if (map[y + i, x] != "-") return false;
        }
    }
    else
    {
        if (x + m > vp) return false;

        for (int i = 0; i < m; i++)
        {
            if (map[y, x + i] != "-") return false;
        }
    }
        return true;
    }

    static void postavi(int m)
    {
        bool placed = false;
        do
        {
            int x,y;
            bool v = r.Next(2) == 0;

            if (v)
            {
                x = r.Next(vp);
                y = r.Next(vp - m + 1);
            }
            else
            {
                x = r.Next(vp - m + 1);
                y = r.Next(vp);
            }

            if (fraj(x, y, m, v))
            {
                for (int i = 0; i != m; i++)
                {
                    if (v)
                    {
                        map[y + i, x] = "L";
                        sl++;
                    }
                    else
                    {
                        map[y, x + i] = "L";
                        sl++;
                    }
                }
                placed = true;
            }

            
        } while (!placed);
    }

    static void postaviladice(string a)
    {
        if (a == "a")
        {
            for (int i = 0; i != 5; i++)
            {
                postavi(1);
            }
        }
        else
        {
            for (int i = 4; i != 1; i--)
            {
                postavi(i);
            }
        }

    }

    static bool yesorno(string t)
    {
        do
        {
            Console.WriteLine(t);
            string n = Console.ReadLine().Trim().ToUpper();

            if (!(n == "Y" || n == "N"))
            {
                Console.WriteLine("Podati morate y ali n");
                continue;
            }
            else
            {
                if (n == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } while (true);
    }

    static void Main()
    {
        string o;
        Console.WriteLine("Dobrodosli v potapaljanje ladic!");
        do
        {
            Console.Write("Kako veliko želite polje. Najmajnša možno je 7 in največ 26: ");
            string n = Console.ReadLine().Trim();
            if (!(int.TryParse(n, out vp)))
            {
                Console.WriteLine("Podati morete številko!");
            }
            else
            {
                if (vp < 7 || vp>26)
                {
                    Console.WriteLine("Podati morete številko med 7 in 25!");
                }
                else
                {
                        break;
                }
            }
        } while (true);

        do
        {
            Console.WriteLine("Kakšne postavitev ladic želite? \n a) 5x po 1mestno \n b) 1x po 4 mestna, 1x po 3 mestna in 1x po 2 mestna");
            o = Console.ReadLine().Trim();
            if (!(o == "a" || o == "b"))
            {
                Console.WriteLine("Podati morete a ali b!");
            }
            else
            {
                break;
            }
        } while (true);

        do
        {
            Console.WriteLine("Koliko poskusov želite?");
            string m = Console.ReadLine().Trim();

            if (!(int.TryParse(m, out sp)))
            {
                Console.WriteLine("Podati morete številko!");
            }
            else
            {
                break;
            }
        } while (true);

        Console.Clear();
        map = new string[vp, vp];
        pmap = new string[vp, vp];
        createmap();
        postaviladice(o);
        igraj();

        string k;
        bool od=yesorno("Ali želite videti zgenerirano polje? \n Podajte y ali n ");
        if (od)
        {
            izpismape();
        }

          od=yesorno("Ali želite videti vaše vnose? \n Podatje y ali n ");
        if (od)
        {
            Console.WriteLine(vnos);
        }
    }
}