using System;
using static System.Console;

namespace Conway_s_Game_of_Life
{
    class Program
    {
        class Game
        {
            private uint _xSize;
            private uint _ySize;
            private char _simv = '#';
            public Game() : this(new uint[2] { 32, 32 }) { }
            public Game(uint[] fieldSize)
            {
                _xSize = fieldSize[0];
                _ySize = fieldSize[1];
            }
            public void Run()
            {
                DateTime start;

                bool[,] print      = new bool[_ySize, _xSize];
                bool[,] printClone = new bool[_ySize, _xSize];
                bool[,] printBufer = new bool[_ySize, _xSize];
                bool[,] printZeros = new bool[_ySize, _xSize];

                int neighboursCounter;

                Random rnd = new Random();
                
                for (int i = 0; i < _ySize; i++) 
                { 
                    for (int j = 0; j < _xSize; j++) 
                    {
                        print[i, j] = rnd.Next(0, 3) == 2;
                        printBufer[i, j] = false;
                        printClone[i, j] = false;
                        printZeros[i, j] = false;
                    } 
                }
                do
                {
                    start = DateTime.Now;

                    for (int i = 1; i < _ySize - 1; i++)
                    {
                        for (int j = 1; j < _xSize - 1; j++)
                        {
                            neighboursCounter = (print[i - 1, j - 1] ? 1 : 0)         // 1
                                              + (print[i - 1, j    ] ? 1 : 0)         // 2
                                              + (print[i - 1, j + 1] ? 1 : 0)         // 3    //1 2 3
                                              + (print[i    , j - 1] ? 1 : 0)         // 4    //4 # 5
                                              + (print[i    , j + 1] ? 1 : 0)         // 5    //6 7 8
                                              + (print[i + 1, j - 1] ? 1 : 0)         // 6
                                              + (print[i + 1, j    ] ? 1 : 0)         // 7
                                              + (print[i + 1, j + 1] ? 1 : 0);        // 8
                            

                            // cell is 'live' and have 2 or 3 'live' neighbours

                            if (print[i, j] && (neighboursCounter == 2 || neighboursCounter == 3)) printBufer[i, j] = true;

                            // cell is 'live' and have less then 2 or more then 3 neighbours

                            else if (print[i, j] && neighboursCounter != 2 && neighboursCounter != 3) printBufer[i, j] = false;

                            // cell is 'dead' and have 3 neighbours

                            else if (!(print[i, j]) && neighboursCounter == 3) printBufer[i, j] = true;
                        }
                    }
                    printClone = (bool[,]) print     .Clone();
                    print      = (bool[,]) printBufer.Clone();
                    printBufer = (bool[,]) printZeros.Clone();

                    WriteLine((DateTime.Now - start).Milliseconds);
                    PrintField(print);
                    
                    //ReadKey();
                }
                while (true);
            }
            public void PrintField(bool[,] field)
            {
                DateTime start;
                string output = "";
                for (int i = 0; i < _ySize; i++)
                {
                    for (int j = 0; j < _xSize; j++)
                    {
                        output += field[i, j] ? _simv : ' ';
                    }
                    output += i == _ySize - 1 ? "" : "\n";
                }
                Clear();
                Write(output);
                start = DateTime.Now;
                while ((DateTime.Now - start).Milliseconds < 70) continue;
            }
            public static void Rules()
            {
                WriteLine("Every cell can be 'live'('#') or 'dead'(' ')");
                WriteLine("Every cell interacts with its eight neighbours");
                WriteLine("1 2 3\n4 # 5\n6 7 8");
                WriteLine("At each step in time, the following transitions occur:");
                WriteLine("  1) Any live cell with fewer than two live neighbours dies");
                WriteLine("  2) Any live cell with two or three live neighbours lives on to the next generation");
                WriteLine("  3) Any live cell with more than three live neighbours dies");
                WriteLine("  4) Any dead cell with exactly three live neighbours becomes a live cell");
                Write("Press any button to start random generated Game...");
                ReadKey();
                Clear();
            }
        }

        static void Main(string[] args)
        {
            uint[] windowSize = new uint[2] { (uint)WindowWidth, (uint)WindowHeight };

            Game newGame = new(windowSize);

            Game.Rules();

            newGame.Run();
        }
    }
}
