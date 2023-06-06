using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSortWithVisualization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] visualizerSize = { 29, 120 };

            Random rnd = new Random();
            int[] arr = new int[visualizerSize[1]];
            int[] LSD = new int[arr.Length];
            int[] newDispl = new int[visualizerSize[1]];
            int[] curDispl = new int[visualizerSize[1]];
            int temp = 0;

            /*
             * Possible colors:
             * 0 : Reset Color
             * 1 : Blue
             * 2 : Red
             * 3 : Green
             * 4 : Cyan
             * 5 : Dark Blue
             * 6 : Foreground Red
             */

            int toInsert = 0;
            bool fromStart = false;
            int insertIndex = -1;
            bool insertFlag = false;

            int toInsertValue = 0;
            int toInsertLSD = 0;

            int baseNum = 1;

            bool sorted = false;

            for (int x = 0; x < arr.Length; x++)
                arr[x] = rnd.Next(visualizerSize[0]) + 1;

            // this line just sets the window size to always display in a 
            // 120 * 30 characters in size
            Console.SetWindowSize(visualizerSize[1], visualizerSize[0] + 1);

            #region Visualizing initial display
            for (int a = visualizerSize[0]; a > 0; a--) // dictate number of rows
            {
                for (int b = 0; b < arr.Length; b++) // dictate number of columns
                {
                    if (arr[b] >= a)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
            }
            Console.Write("To be sorted using radix sort... Press any key to begin...");
            Console.ReadKey();
            //Console.Clear(); 
            #endregion

            while (!sorted)
            {
                sorted = true;
                for (int x = 0; x < arr.Length; x++)
                {
                    LSD[x] = arr[x] / baseNum % 10;
                    if (LSD[x] > 0)
                        sorted = false;
                }

                if (sorted)
                    break;

                for (int x = 1; x < arr.Length; x++)
                {
                    insertIndex = -1;
                    insertFlag = false;
                    newDispl[x] = 4;
                    #region Highlighting cell to be checked
                    for (int a = 0; a < arr.Length; a++)
                    {
                        for (int b = visualizerSize[0]; b > 0; b--)
                        {
                            if (newDispl[a] != curDispl[a])
                            {
                                Console.SetCursorPosition(a, b - 1);
                                switch (newDispl[a])
                                {
                                    case 0:
                                        Console.ResetColor();
                                        break;
                                    case 1:
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        break;
                                    case 2:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        break;
                                    case 3:
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        break;
                                    case 4:
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        break;
                                    case 5:
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        break;
                                    case 6:
                                        Console.BackgroundColor = ConsoleColor.Red;
                                        break;
                                }

                                if (arr[a] > visualizerSize[0] - b)
                                    Console.Write("*");
                                else
                                    Console.Write(" ");
                            }
                        }

                        curDispl[a] = newDispl[a];
                        newDispl[a] = 0;
                    }
                    Console.SetCursorPosition(0, 29);
                    Console.Write("Pass {0} : Initializing . . .                                          ", x);
                    //Console.ReadKey();
                    //Thread.Sleep(200);
                    //Console.Clear(); 
                    #endregion

                    for (int y = x - 1; y >= 0; y--)
                    {
                        // searching
                        newDispl[x] = 1;
                        newDispl[y] = 2;
                        #region Highlighting cell being searched
                        for (int a = 0; a < arr.Length; a++)
                        //for (int a = visualizerSize[0]; a > 0; a--) // dictate number of rows
                        {
                            for (int b = visualizerSize[0]; b > 0; b--)
                            //for (int b = 0; b < arr.Length; b++) // dictate number of columns
                            {
                                if (newDispl[a] != curDispl[a])
                                {
                                    Console.SetCursorPosition(a, b - 1);
                                    switch (newDispl[a])
                                    {
                                        case 0:
                                            Console.ResetColor();
                                            break;
                                        case 1:
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            break;
                                        case 2:
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            break;
                                        case 3:
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            break;
                                        case 4:
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            break;
                                        case 5:
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            break;
                                        case 6:
                                            Console.BackgroundColor = ConsoleColor.Red;
                                            break;
                                    }

                                    if (arr[a] > visualizerSize[0] - b)
                                        Console.Write("*");
                                    else
                                        Console.Write(" ");
                                }
                            }

                            curDispl[a] = newDispl[a];
                            newDispl[a] = 0;
                        }
                        Console.SetCursorPosition(0, 29);
                        Console.Write("Pass {0} : Searching. . .                                              ", x);
                        //Console.ReadKey();
                        //Thread.Sleep(200);
                        //Console.Clear(); 
                        #endregion
                        if (LSD[x] < LSD[y])
                        {
                            insertFlag = true;
                            insertIndex = y;
                        }
                        else
                            break;
                    }

                    if (insertFlag)
                    {
                        toInsertValue = arr[x];
                        toInsertLSD = LSD[x];
                        arr[x] = -1;
                        LSD[x] = -1;

                        newDispl[x] = 6;
                        #region Preparing to move
                        for (int a = 0; a < arr.Length; a++)
                        //for (int a = visualizerSize[0]; a > 0; a--) // dictate number of rows
                        {
                            for (int b = visualizerSize[0]; b > 0; b--)
                            //for (int b = 0; b < arr.Length; b++) // dictate number of columns
                            {
                                if (newDispl[a] != curDispl[a])
                                {
                                    Console.SetCursorPosition(a, b - 1);
                                    switch (newDispl[a])
                                    {
                                        case 0:
                                            Console.ResetColor();
                                            break;
                                        case 1:
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            break;
                                        case 2:
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            break;
                                        case 3:
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            break;
                                        case 4:
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            break;
                                        case 5:
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            break;
                                        case 6:
                                            Console.BackgroundColor = ConsoleColor.Red;
                                            break;
                                    }

                                    if (arr[a] > visualizerSize[0] - b)
                                        Console.Write("*");
                                    else
                                        Console.Write(" ");
                                }
                            }

                            curDispl[a] = newDispl[a];
                            newDispl[a] = 0;
                        }
                        Console.SetCursorPosition(0, 29);
                        Console.Write("Pass {0} : Moving value to memory. . .                                 ", x);
                        //Console.ReadKey();
                        //Thread.Sleep(200);
                        //Console.Clear(); 
                        #endregion

                        for (int y = x - 1; y >= insertIndex; y--)
                        {
                            arr[y + 1] = arr[y];
                            LSD[y + 1] = LSD[y];
                            arr[y] = -1;
                            LSD[y] = -1;


                            newDispl[y + 1] = 5;
                            newDispl[y] = 6;

                            #region Moving
                            for (int a = 0; a < arr.Length; a++)
                            //for (int a = visualizerSize[0]; a > 0; a--) // dictate number of rows
                            {
                                for (int b = visualizerSize[0]; b > 0; b--)
                                //for (int b = 0; b < arr.Length; b++) // dictate number of columns
                                {
                                    if (newDispl[a] != curDispl[a])
                                    {
                                        Console.SetCursorPosition(a, b - 1);
                                        switch (newDispl[a])
                                        {
                                            case 0:
                                                Console.ResetColor();
                                                break;
                                            case 1:
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                break;
                                            case 2:
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                break;
                                            case 3:
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                break;
                                            case 4:
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                break;
                                            case 5:
                                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                break;
                                            case 6:
                                                Console.BackgroundColor = ConsoleColor.Red;
                                                break;
                                        }

                                        if (arr[a] > visualizerSize[0] - b)
                                            Console.Write("*");
                                        else
                                            Console.Write(" ");
                                    }
                                }

                                curDispl[a] = newDispl[a];
                                newDispl[a] = 0;
                            }
                            Console.SetCursorPosition(0, 29);
                            Console.Write("Pass {0} : Moving . . .                                                ", x);
                            //Console.ReadKey();
                            //Thread.Sleep(200);
                            //Console.Clear(); 
                            #endregion
                        }

                        arr[insertIndex] = toInsertValue;
                        LSD[insertIndex] = toInsertLSD;
                        newDispl[insertIndex] = 4;
                        #region Placing
                        for (int a = 0; a < arr.Length; a++)
                        //for (int a = visualizerSize[0]; a > 0; a--) // dictate number of rows
                        {
                            for (int b = visualizerSize[0]; b > 0; b--)
                            //for (int b = 0; b < arr.Length; b++) // dictate number of columns
                            {
                                if (newDispl[a] != curDispl[a])
                                {
                                    Console.SetCursorPosition(a, b - 1);
                                    switch (newDispl[a])
                                    {
                                        case 0:
                                            Console.ResetColor();
                                            break;
                                        case 1:
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            break;
                                        case 2:
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            break;
                                        case 3:
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            break;
                                        case 4:
                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                            break;
                                        case 5:
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            break;
                                        case 6:
                                            Console.BackgroundColor = ConsoleColor.Red;
                                            break;
                                    }

                                    if (arr[a] > visualizerSize[0] - b)
                                        Console.Write("*");
                                    else
                                        Console.Write(" ");
                                }
                            }

                            curDispl[a] = newDispl[a];
                            newDispl[a] = 0;
                        }
                        Console.SetCursorPosition(0, 29);
                        Console.Write("Pass {0} : Placing value in its proper place . . .                     ", x);
                        //Console.ReadKey();
                        //Thread.Sleep(200);
                        //Console.Clear(); 
                        #endregion
                    }
                }


                baseNum *= 10;
            }

            Console.SetCursorPosition(0, 29);
            Console.Write("Done!!!!!!!!!                                              ");
            Console.ReadKey();
        }
    }
}

