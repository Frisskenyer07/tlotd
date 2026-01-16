using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thelegendofthedragon
{
    internal class Program
    {
        static bool GameOver(bool game)
        {
            Console.Clear();
            Console.WriteLine("MEGHALTÁL!");

            string[] meghalasimenu = { "Újraéledés", "Kilépés" };
            int selected = 0;
            ConsoleKey key;
            // do while kell mert elősször kiírom a menüt aztán nézem meg mit nyomott a felh
            do
            {
                //menuabra
                for (int i = 0; i < meghalasimenu.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {meghalasimenu[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {meghalasimenu[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selected = Math.Max(0, selected - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        selected = Math.Min(meghalasimenu.Length - 1, selected + 1);
                        break;
                }

                Console.SetCursorPosition(0, Console.CursorTop - meghalasimenu.Length);
            } while (key != ConsoleKey.Enter);

            
            if (selected == 0) // Újraéledés
            {
                Console.Clear();
                Console.WriteLine("hamarosan újraéledsz...");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                Console.Clear();
                return true; 
            }
            else // Kilépés
            {
                Console.Clear();
                Console.WriteLine("gg");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                Environment.Exit(0);
                return (game);
            }
        }
        struct FightResult
        {
            //python class szerű cucc ami technikailag itt 2 variablet ad vissza mert kell nekem 2 mer alapból 1-et ad
            public int ujHp;
            public bool gyozelem;
            //meg azért public hogy a masik funcion is lassa mer kell neki
        }

        static FightResult Mobs(int mobattack, int mobhp, int lvl, int hp, List<string> inventory)
        {
            //sebzesek
            int damage = 5;

            if (lvl >= 2 && inventory.Contains("vaskard"))
            {
                damage = 10;
            }

            if (lvl >= 3 && inventory.Contains("ezüstkard"))
            {
                damage = 20;
            }

            if (lvl >= 4 && inventory.Contains("gyémántkard"))
            {
                damage = 40;
            }

            //hp
            while (mobhp > 0 && hp > 0)
            {
                mobhp -= damage;
                hp -= mobattack;
            }

            bool won = mobhp <= 0 && hp > 0;

            Console.WriteLine(won ? "győztél" : "Vesztettél");
            Console.WriteLine("nyomj entert a továbblépéshez");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            Console.Clear();

            return new FightResult
            {
                ujHp = hp,
                gyozelem = won
            };
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //listak
            List<string> shopitemek = new List<string> { "vas kard (-10hp)  100$", "ezüst kard (-20hp)  200$", "gyémánt kard (-40hp)  300$", "+30hp  100$", "kilépés" };
            List<string> shopitemek_copy = new List<string> { "vas kard (-10hp)  100$", "ezüst kard (-20hp)  200$", "gyémánt kard (-40hp)  300$", "+30hp  100$", "kilépés" };
            int penz = 0;
            List<string> mobok = new List<string> { "slime 10hp (sebzés: -2hp)", "skeleton 20hp (sebzés: -5)", "goblin 30hp (sebzés: -10hp)", "pók 50hp (sebzés: -15hp)", "!!EPICBOSSFIGHT!! sárkány 200hp (sebzés: -20hp)", "titkos entitás hp:??? (sebzés: ???)", "kilépés"};
            List<string> inventory = new List<string>();
            int lvl = 1;
            int hp = 70;
            //title
            Console.WriteLine(@"
                +--------------------------------------------------+
                |      _                              _            |
                |     | |    ___  __ _  ___ _ __   __| |           |
                |     | |   / _ \/ _` |/ _ \ '_ \ / _` |           |
                |     | |__|  __/ (_| |  __/ | | | (_| |           |
                |     |_____\___|\__, |\___|_| |_|\__,_|           |
                |                |___/                             |
                |                                                  |
                |           O F   T H E   D R A G O N .            |
                +--------------------------------------------------+
                ");
            Console.WriteLine("\nNyomj entert a játékhoz");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            Console.Clear();
            //ez itt főmenü szerű dolog
            bool game = true;

            while (game)
            {
                string knight = @"
                   ,   A           {}
                  / \, | ,        .--.
                 |    =|= >      /.--.\                 +------------------------+
                  \ /` | `       |====|                 |  LEGEND OF THE DRAGON. |
                   `   |         |`::`|                 +------------------------+
                       |     .-;`\..../`;.-.
                      /\\\\/  /  |...::...|  \
                      |:'\ |   /'''::'''\   |
                       \ /\;-,/\   ::   /\--/
                       |\ <` >  >._::_.<,<__>
                       | `""`  /   ^^   \
                       |       |        |
                       |       |        |
                 ";

                Console.WriteLine(knight);
                
                Console.WriteLine("(TIPP: a shopban visszanyered az elvesztet hp-d minden kör után)");
                string[] menuItems = {"Shop","Dungeon","Kilépés"};

                int selected = 0;
                ConsoleKey key;
                // do while kell mert elősször kiírom a menüt aztán nézem meg mit nyomott a felh
                do
                {
                    Console.Clear();
                    Console.WriteLine(knight);
                    Console.WriteLine();

                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        if (i == selected)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"> {menuItems[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"  {menuItems[i]}");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("(↑ ↓ navigálás, Enter választ)");

                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selected = Math.Max(0, selected - 1);
                            break;

                        case ConsoleKey.DownArrow:
                            selected = Math.Min(menuItems.Length - 1, selected + 1);
                            break;
                    }

                } 
                while (key != ConsoleKey.Enter);

                int valasztas = selected + 1;

                //shop meg dungeon
                switch (valasztas)
                {
                    //shop
                    case 1:
                        Console.Clear();
                        bool shop = true;
                        while (shop)
                        {
                            string shopSign = @"
                                 ║       ║
                                 ║       ║
                               ╔═════════════╗
                               ║     S H O P ║
                               ╚═════════════╝
                                ";
                            
                            //kiirja a shopot
                            int selected_ = 0;
                            ConsoleKey key_;
                            // do while kell mert elősször kiírom a menüt aztán nézem meg mit nyomott a felh
                            do
                            {
                                Console.Clear();
                                Console.WriteLine(shopSign);
                                Console.WriteLine();
                                Console.WriteLine("egyenleged " + penz + "$");
                                Console.WriteLine();

                                for (int i = 0; i < shopitemek.Count; i++)
                                {
                                    if (i == selected_)
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine($"> {shopitemek[i]}");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  {shopitemek[i]}");
                                    }
                                }

                                key_ = Console.ReadKey(true).Key;

                                switch (key_)
                                {
                                    case ConsoleKey.UpArrow:
                                        selected_ = Math.Max(0, selected_ - 1);
                                        break;

                                    case ConsoleKey.DownArrow:
                                        selected_ = Math.Min(shopitemek.Count - 1, selected_ + 1);
                                        break;
                                }

                            } 
                            while (key_ != ConsoleKey.Enter);


                            bool exitShop = (selected_ == shopitemek.Count - 1);
                            int shopvalasztas = selected_ + 1;
                            //kilepes mar switch nem mükszik ugyh ez kell
                            if (exitShop)
                            {
                                Console.WriteLine("hp regenerálva");

                                if (inventory.Contains("hpboost"))
                                    hp = 100;
                                else
                                    hp = 70;

                                shop = false;
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                Console.Clear();
                                break;
                            }

                            switch (shopvalasztas)
                            {
                                case 1:
                                    if (penz >= 100)
                                    {
                                        penz -= 100;
                                        inventory.Clear();
                                        shopitemek.Remove("vas kard (-10hp)  100$");
                                        inventory.Add("vaskard");
                                        lvl += 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("sorry nincs keret");
                                    }
                                    Console.WriteLine("nyomj entert a továbblépéshez");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                    Console.Clear();
                                    break;
                                case 2:
                                    if (penz >= 200)
                                    {
                                        penz -= 200;
                                        inventory.Clear();
                                        shopitemek.Remove("ezüst kard (-20hp)  200$");
                                        inventory.Add("ezüstkard");
                                        lvl += 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("sorry nincs keret");
                                    }
                                    Console.WriteLine("nyomj entert a továbblépéshez");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                    Console.Clear();
                                    break;
                                case 3:
                                    if (penz >= 300)
                                    {
                                        penz -= 300;
                                        inventory.Clear();
                                        shopitemek.Remove("gyémánt kard (-40hp)  300$");
                                        inventory.Add("gyémántkard");
                                        lvl += 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("sorry nincs keret");
                                    }
                                    Console.WriteLine("nyomj entert a továbblépéshez");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                    Console.Clear();
                                    break;
                                case 4:
                                    if (penz >= 100)
                                    {
                                        penz -= 100;
                                        shopitemek.Remove("+30hp  100$");
                                        inventory.Add("hpboost");
                                        hp += 30;
                                        lvl += 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("sorry nincs keret");
                                    }
                                    Console.WriteLine("nyomj entert a továbblépéshez");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                    Console.Clear();
                                    break;
                                
                            }
                        }

                        break;
                    //cave
                    case 2:
                        Console.Clear();
                        string caveEntrance = @"
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣷⣄⠙⠛⠛⠿⠿⠿⠟⢁⡄⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⠀⢀⠘⠛⠿⢿⣿⠿⠻⣿⣿⣿⣿⣶⣶⣶⣦⣴⣿⣷⠀⠀⠀
                        ⠀⠀⠀⠀⠀⠀⣠⣶⣿⣿⣷⣶⣤⣤⣴⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡀
                        ⠀⠀⠀⠀⠐⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⢋⣀⠈⢿⣿⡟⢻⣿⣿
                        ⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⣠⣤⣼⣿⣿
                        ⠀⠀⠀⢀⣠⣤⠈⠿⠿⠟⢋⣠⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                        ⠀⠀⠐⣿⣿⣿⣷⣶⣤⣶⣿⣿⣿⣿⣿⣿⠟⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
                        ⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠈⠙⠻⠿⠿⠿⣿⡿⣿⣿⣿
                        ⠀⠀⠀⣰⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿⠛⠀⠀⠀⠀⠀⠀⢤⣶⣦⣀⣤⣿⣿⣿
                        ⠀⣠⣾⣿⡿⠿⠿⠿⠛⠋⠙⠋⠀⠸⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿
                        ⠀⣿⣿⣿⣿⣶⣶⣶⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⢋⣡
                        ⠀⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠛⠛⠛⠛⠛⠛
                        ";
                        Console.WriteLine(caveEntrance);
                        bool cave = true;

                        while (cave)
                        {
                            int selectedCave = 0;
                            ConsoleKey caveKey;

                            // Buildmenu
                            List<string> caveMenu = new List<string>(mobok);
                            
                            // do while kell mert elősször kiírom a menüt aztán nézem meg mit nyomott a felh
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("egyenleg: " + penz);
                                Console.WriteLine("health: " + hp);
                                Console.WriteLine("level: " + lvl);
                                Console.WriteLine("fegyver(ek): " + string.Join(", ", inventory));
                                Console.WriteLine();

                                //menu
                                for (int i = 0; i < caveMenu.Count; i++)
                                {
                                    if (i == selectedCave)
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkGray;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.WriteLine($"> {caveMenu[i]}");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  {caveMenu[i]}");
                                    }
                                }

                                // 
                                caveKey = Console.ReadKey(true).Key;

                                switch (caveKey)
                                {
                                    case ConsoleKey.UpArrow:
                                        selectedCave = Math.Max(0, selectedCave - 1);
                                        break;
                                    case ConsoleKey.DownArrow:
                                        selectedCave = Math.Min(caveMenu.Count - 1, selectedCave + 1);
                                        break;
                                }

                            } while (caveKey != ConsoleKey.Enter);

                            // 
                            bool exitCave = (selectedCave == caveMenu.Count + 2);
                            int cavevalasztas = selectedCave + 1;

                            if (exitCave)
                            {
                                Console.WriteLine("Vissza a town-ba");
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                Console.Clear();
                                cave = false;
                                continue;
                            }

                            //cave fights
                            switch (cavevalasztas)
                            {
                                case 1:
                                    Console.Clear();
                                    string slime = @"
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⠤⠄⠒⠀⠉⠉⠉⠉⠉⠁⠐⠢⢄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠔⠊⠁⠀⣠⣤⠖⠒⢖⣶⣖⠒⠒⠂⠤⣀⠀⠈⠑⠤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠔⠁⠀⢀⡤⠒⢿⣏⡀⢀⣀⣜⡿⠃⠀⠀⠀⠀⠀⠉⠢⢄⠀⠈⢲⡀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⡠⠊⠀⢀⡠⠊⠁⠀⠀⠀⠉⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⣆⠀⠈⢳⡀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⢠⠊⠀⢀⠔⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣇⠀⠀⢇⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⡔⠁⢀⠔⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢧⠀⠀⢧⠀⠀⠀⠀
                                    ⠀⠀⠀⢀⠎⠀⡠⢡⣴⠛⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣆⠀⠘⣆⠀⠀⠀
                                    ⠀⠀⢀⠎⠀⡔⢰⣿⣧⣴⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢆⠀⠈⢦⠀⠀
                                    ⠀⠀⡎⠀⡜⢠⣿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⣷⣄⠈⢆⠀⢇⠀⠀
                                    ⠀⡸⠀⣸⠀⠘⠿⠿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣇⣸⣿⡇⠘⡄⠈⡄⠀
                                    ⢀⠃⢠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⡇⠀⢱⠀⢱⠀
                                    ⠸⠀⡜⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣶⣤⣄⣀⣀⣀⣠⣤⣤⣴⣶⣧⡀⠀⠀  ⠙⠛⠛⠋⠀⠀⠀⡇⠀⡇⠀⠀
                                    ⡇⠀⠇⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⡇⢱⠀⠀
                                    ⡇⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⡇⠘⠀⠀
                                    ⠇⠸⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⡿⠟⠛⠉⠉⠉⠛⠛⢿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⡇⠀⠇⠀
                                    ⢸⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠱⣻⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⢧⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⢰
                                    ⠀⢇⠹⡀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢝⢿⣿⣧⣀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⡿⡣⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡆⠀⣹
                                    ⠀⠈⢢⠱⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠬⣛⠻⠿⢷⣶⣶⣶⣾⡿⠟⢛⡥⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠄⠀⡎⠀
                                    ⠀⠀⠀⠑⢌⠢⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠁⠒⠒⠒⠐⠒⠒⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠔⠋⢠⠞⠁⠀
                                    ⠀⠀⠀⠀⠀⠑⠢⣑⠤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡠⠔⠁⣀⡴⠊⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠉⠒⠬⢒⠤⢄⣀⣀⣀⣀⣀⣀⣀⣀⠠⠤⠤⠤⠀⠤⠤⠤⠔⠂⠉⢀⡴⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠑⠒⠒⠦⠤⠤⠤⠤⠤⠤⠤⠤⠤⠤⠤⠤⠤⠤⠚⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";
                                    Console.WriteLine(slime);
                                    //fv
                                    FightResult result = Mobs(2, 10, lvl, hp, inventory);
                                    hp = result.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit

                                        break;
                                    }
                                    if (result.gyozelem)
                                    {
                                        penz += 30;
                                    }

                                    break;
                                case 2:
                                    Console.Clear();
                                    string csonti = @"
                                                                  _.--""""-._
                                      .                         .""         "".
                                     / \    ,^.         /(     Y             |      )\
                                    /   `---. |--'\    (  \__..'--   -   -- -'""""-.-'  )
                                    |        :|    `>   '.     l_..-------.._l      .'
                                    |      __l;__ .'      ""-.__.||_.-'v'-._||`""----""
                                     \  .-' | |  `              l._       _.'
                                      \/    | |                   l`^^'^^'j
                                            | |                _   \_____/     _
                                            j |               l `--__)-'(__.--' |
                                            | |               | /`---``-----'""1 |  ,-----.
                                            | |               )/  `--' '---'   \'-'  ___  `-.
                                            | |              //  `-'  '`----'  /  ,-'   I`.  \
                                          _ L |_            //  `-.-.'`-----' /  /  |   |  `. \
                                         '._' / \         _/(   `/   )- ---' ;  /__.J   L.__.\ :
                                          `._;/7(-.......'  /        ) (     |  |            | |
                                          `._;l _'--------_/        )-'/     :  |___.    _._./ ;
                                            | |                 .__ )-'\  __  \  \  I   1   / /
                                            `-'                /   `-\-(-'   \ \  `.|   | ,' /
                                                               \__  `-'    __/  `-. `---'',-'
                                                                  )-._.-- (        `-----'
                                                                 )(  l\ o ('..-.
                                                           _..--' _'-' '--'.-. |
                                                    __,,-'' _,,-''            \ \
                                                   f'. _,,-'                   \ \
                                                  ()--  |                       \ \
                                                    \.  |                       /  \
                                                      \ \                      |._  |
                                                       \ \                     |  ()|
                                                        \ \                     \  /
                                                         ) `-.                   | |
                                                        // .__)                  | |
                                                     _.//7'                      | |
                                                   '---'                         j_| `
                                                                                (| |
                                                                                 |  \
                                                                                 |lllj
                                                                                 |||||";
                                    Console.WriteLine(csonti);
                                    //fv
                                    FightResult r = Mobs(5, 20, lvl, hp, inventory);
                                    hp = r.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit
                                        break;
                                    }
                                    if (r.gyozelem)
                                    {
                                        penz += 40;
                                    }

                                    break;
                                case 3:
                                    Console.Clear();
                                    string goblin = @"
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⣿⣿⣶⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⢀⡼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢧⡀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠢⣤⣀⡀⠀⠀⠀⢿⣧⣄⡉⠻⢿⣿⣿⡿⠟⢉⣠⣼⡿⠀⠀⠀⠀⣀⣤⠔⠀
                                    ⠀⠀⠈⢻⣿⣶⠀⣷⠀⠉⠛⠿⠶⡴⢿⡿⢦⠶⠿⠛⠉⠀⣾⠀⣶⣿⡟⠁⠀⠀
                                    ⠀⠀⠀⠀⠻⣿⡆⠘⡇⠘⠷⠠⠦⠀⣾⣷⠀⠴⠄⠾⠃⢸⠃⢰⣿⠟⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠋⢠⣾⣥⣴⣶⣶⣆⠘⣿⣿⠃⣰⣶⣶⣦⣬⣷⡄⠙⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⢋⠛⠻⠿⣿⠟⢹⣆⠸⠇⣰⡏⠻⣿⠿⠟⠛⡙⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠈⢧⡀⠠⠄⠀⠈⠛⠀⠀⠛⠁⠀⠠⠄⢀⡼⠁⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠈⢻⣦⡀⠃⠀⣿⡆⢰⣿⠀⠘⢀⣴⡟⠁⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣦⡀⠘⠇⠸⠃⢀⣴⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⢿⣿⣷⣄⣠⣾⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⠟⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";
                                    Console.WriteLine(goblin);
                                    //fv
                                    FightResult ry = Mobs(10, 30, lvl, hp, inventory);
                                    hp = ry.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit
                                        break;
                                    }
                                    if (ry.gyozelem)
                                    {
                                        penz += 50;
                                    }

                                    break;
                                case 4:
                                    Console.Clear();
                                    string pok = @"
                                       ____                      ,
                                      /---.'.__             ____//
                                           '--.\           /.---'
                                      _______  \\         //
                                    /.------.\  \|      .'/  ______
                                   //  ___  \ \ ||/|\  //  _/_----.\__
                                  |/  /.-.\  \ \:|< >|// _/.'..\   '--'
                                     //   \'. | \'.|.'/ /_/ /  \\
                                    //     \ \_\/"" ' ~\-'.-'    \\
                                   //       '-._| :H: |'-.__     \\
                                  //           (/'==='\)'-._\     ||
                                  ||                        \\    \|
                                  ||                         \\    '
                                  |/                          \\
                                                               ||
                                                               ||
                                                               \\
                                                                '";
                                    Console.WriteLine(pok);
                                    //fv
                                    FightResult rx = Mobs(15, 50, lvl, hp, inventory);
                                    hp = rx.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit
                                        break;
                                    }
                                    if (rx.gyozelem)
                                    {
                                        penz += 60;
                                    }

                                    break;
                                case 5:
                                    Console.Clear();
                                    string sarkany = @"
                                                  __
                                              _.-'.-'-.__
                                           .-'.       '-.'-._ __.--._
                                    -..'\,-,/..-  _         .'   \   '----._
                                     ). /_ _\' ( ' '.         '-  '/'-----._'-.__
                                     '.<a} {a>.'     '-r   _      .-.       '-._ \
                                     '.\. Y .).'       ( .'  .      .\          '\'.
                                     .-')'|'/'-.        \)    )      '',_      _.c_.\
                                       .<, ,>.          |   _/\        . ',   :   : \\
                                      .' \_/ '.        /  .'   |          '.     .'  \)
                                                      / .-'    '-.        : \   _;   ||
                                                     / /    _     \_      '.'\ ' /   ||
                                                    /.'   .'        \_      .|   \   \|
                                                   / /   /      __.---'      '._  ;  ||
                                                  /.'  _:-.____< ,_           '.\ \  ||
                                                 // .-'     '-.__  '-'-\_      '.\/_ \|
                                                ( };====.===-==='        '.    .  \\: \
                                                 \\ '._        /          :   ,'   )\_ \
                                                  \\   '------/            \ .    /   )/
                                                   \|        _|             )Y    |   /
                                                    \\      \             .','   /  ,/
                                                     \\    _/            /     _/
                                                      \\   \           .'    .'
                                                       '| '1          /    .'
                                                         '. \        |:    /
                                                           \ |       /', .'
                                                            \(      ( ;z'
                                                             \:      \ '(_
                                                              \_,     '._ '-.___
                                                                          '-' -.\";
                                    Console.WriteLine(sarkany);
                                    //fv
                                    FightResult rf = Mobs(20, 200, lvl, hp, inventory);
                                    hp = rf.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit 
                                        break;
                                    }
                                    if (rf.gyozelem)
                                    {
                                        penz += 70;
                                    }

                                    break;
                                
                                case 6:
                                    Console.Clear();
                                    Console.WriteLine(@"
                                                             _.'.__
                                                          _.'      .
                                    ':'.               .''   __ __  .
                                      '.:._          ./  _ ''     ""-'.__
                                    .'''-: """"""-._    | .                ""-""._
                                     '.     .    ""._.'                       ""
                                        '.   ""-.___ .        .'          .  :o'.
                                          |   .----  .      .           .'     (
                                           '|  ----. '   ,.._                _-'
                                            .' .---  |.""""  .-:;.. _____.----'
                                            |   .-""""""""    |      '
                                          .'  _'         .'    _'
                                         |_.-'            '-.'
                                    ");
                                    FightResult rg = Mobs(50, 150, lvl, hp, inventory);
                                    hp = rg.ujHp;

                                    if (hp <= 0)
                                    {
                                        GameOver(game);
                                        hp = 70;      // reset hp
                                        lvl = 1;
                                        penz = 0;
                                        shopitemek.Clear();
                                        shopitemek.AddRange(shopitemek_copy); //shopreset + inventory reset
                                        inventory.Clear();
                                        cave = false; // exit 
                                        break;
                                    }
                                    if (rg.gyozelem)
                                    {
                                        penz += 1000;
                                    }

                                    break;
                                //kilepes
                                case 7:
                                    Console.WriteLine("vissza a town-ba");
                                    Console.WriteLine("nyomj entert a továbblépéshez");
                                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                    Console.Clear();
                                    cave = false;
                                    break;
                                    
                                default:
                                    Console.WriteLine("nincs ilyen opció");
                                    break;
                            }

                            Console.WriteLine("Nyomj entert a továbblépéshez");
                            
                        }
                        break;
                        
                    case 3:
                        game = false;
                        break;
                    default:
                        Console.WriteLine("nincs ilyen opció");
                        break;
                }
            }
        }

    }
        
}

    

