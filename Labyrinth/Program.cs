using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Program
    {

        public class CartPoint
        {
            public int x;
            public int y;

            public CartPoint(int xStart, int yStart)
            {
                x = xStart;
                y = yStart;
            }
        }

        public class TreasurePoint : CartPoint
        {
            public char _treasure;

            public TreasurePoint(int xPos, int yPos, char treasure) : base(xPos, yPos)
            {
                _treasure = treasure;
            }
        }

        public class LabPoint
        {
            public Boolean visited;
            public Boolean barrier;
            public Char treasure;

            public LabPoint()
            {
                visited = false;
                barrier = false;
                treasure = ' ';
            }
        }

        static LabPoint[,] Laby = new LabPoint[7, 7];

        //Start position
        static CartPoint OdiPos = new CartPoint(6, 5);

        static CartPoint[] Barriers = { new CartPoint(1,5),
                                        new CartPoint(2,6),
                                        new CartPoint(4,5),
                                        new CartPoint(4,6),
                                        new CartPoint(5,2),
                                        new CartPoint(6,4) };

        static TreasurePoint[] Treasures = { new TreasurePoint(2,2,'L'),
                                             new TreasurePoint(5,3,'A'),
                                             new TreasurePoint(2,1,'I'),
                                             new TreasurePoint(3,1,'O'),
                                             new TreasurePoint(4,0,'S') };

        static String treasuresFound = "";
        static int increm = 0;

        static void Main(string[] args)
        {
            initMaze();
            MoveTo(OdiPos);

            Console.WriteLine("Finished, treasures found : " + treasuresFound);
            Console.ReadLine();
        }

        static void initMaze()
        {
            foreach (CartPoint c in Barriers)
            {
                LabPoint l = Laby[c.x, c.y];
                if (l == null) l = new LabPoint();
                l.barrier = true;
                Laby[c.x, c.y] = l;
            }
            foreach (TreasurePoint t in Treasures)
            {
                LabPoint l = Laby[t.x, t.y];
                if (l == null) l = new LabPoint();
                if (l.barrier) Console.WriteLine("ERROR: " + t.x + "," + t.y + " is barrier and has treasure !");
                l.treasure = t._treasure;
                Laby[t.x, t.y] = l;
            }
        }

        static void MoveTo(CartPoint pos)
        {
            increm++;

            printWithIncrem("-> (" + pos.x + "," + pos.y + ")");

            if (!isValidAndUnvisited(pos))
            {
                printWithIncrem("-> X");
                increm--;
                return;
            }

                collectTreasure(pos);
                //already marked as visited ..

                PrintMaze();

                MoveTo(new CartPoint(pos.x + 1, pos.y));
                MoveTo(new CartPoint(pos.x, pos.y + 1));
                MoveTo(new CartPoint(pos.x - 1, pos.y));
                MoveTo(new CartPoint(pos.x, pos.y - 1));
            
        }

        static void collectTreasure(CartPoint pos)
        {
            LabPoint l = Laby[pos.x, pos.y];

            if (!l.treasure.Equals(' '))
            {
                Console.WriteLine(" Found treasure : " + l.treasure);
                treasuresFound += l.treasure;
                l.treasure = ' '; // remove treasure :D
            }

            Laby[pos.x, pos.y] = l;
        }

        static Boolean isValidAndUnvisited(CartPoint pos)
        {
            if ((pos.x >= Laby.GetLength(0)) || (pos.x < 0) || (pos.y >= Laby.GetLength(1)) || (pos.y < 0)) return false;
            LabPoint l = Laby[pos.x, pos.y];
            if (l == null) l = new LabPoint();

            if (l.barrier) return false;
            if (l.visited) return false;
            else
            {
                l.visited = true;
                Laby[pos.x, pos.y] = l;
                return true;
            }
        }

        static void PrintMaze()
        {
            Console.WriteLine("\t0 1 2 3 4 5 6");
            for (int y = 0; y < Laby.GetLength(1); y++)
            {
                Console.Write(" " + y + "\t");
                for (int x = 0; x < Laby.GetLength(0); x++)
                {
                    if (Laby[x, y] == null) Console.Write("  ");
                    else
                    {
                        if (Laby[x, y].barrier) Console.Write("# ");
                        else if (Laby[x, y].visited) Console.Write("* ");
                        else if (!Laby[x, y].treasure.Equals(' ')) Console.Write(Laby[x, y].treasure + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void printWithIncrem(String txt)
        {
            for(int i=0;i<increm;i++) { Console.Write("\t"); }
            Console.WriteLine(txt);
        }

    }

 }
