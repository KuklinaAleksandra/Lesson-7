using System;

namespace Lesson_7
{
    class Program
    {
        static int Size_X = 5;
        static int Size_Y = 5;
        static int Size_Win = 4;

        static char[,] field = new char[Size_X, Size_Y];

        static char Player_Dot = 'X';
        static char Ai_Dot = '0';
        static char Empty_Dot = '.';
        static Random random = new Random();

        private static void PrintField()
        {
            Console.Clear();
            Console.WriteLine("------");
            for (int i=0; i<Size_Y;i++)
            {
                Console.Write("|");
                for (int j=0; j<Size_X; j++)
                {
                    Console.Write(field[i, j] + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------");
        }
        private static void InitFild()
        {
            for (int i=0;i<Size_Y;i++)
            {
                for (int j=0; j<Size_X; j++)
                {
                    field[i, j] = Empty_Dot;
                }
            }
        }
        
        private static void playerMove()
        {
            int x, y;
            do
            {
                Console.WriteLine("Введите координаты вашего хода" + Size_Y);
                    Console.WriteLine("Координат по строке");
                x = Int32.Parse(Console.ReadLine()) - 1;
                Console.WriteLine("Координат по столбцу ");
                Console.WriteLine("Введите координаты вашего хода" + Size_X);
                y = Int32.Parse(Console.ReadLine()) - 1;
            }
            while (!IsCellValid(y, x));
            SetSym(y, x, Player_Dot);
        }

        // ход компьютера

        private static void AiMove()
        {
            int x, y;
            // блокировка ходов человека
            for (int v=0;v< Size_Y; v++)
            {
                for (int h =0; h<Size_X; h++)
                {
                    //анализ наличие поля для проверки 
                    if (h + Size_Win <= Size_X)
                    {
                        if (CheckLineHorisont(v, h, Player_Dot) == Size_Win-1)
                        {
                            if (MoveAiLineHorisont(v, h, Ai_Dot)) return;
                        }
                            
                        if (v-Size_Win>-2)
                        {
                            if (CheckDiaUp(v, h, Player_Dot)==Size_Win-1)
                            {
                                if (MoveAiDiaUp(v, h, Ai_Dot)) return;
                            }
                        }
                        if(v+Size_Win<=Size_Y)
                        {
                            if (CheckDiaDown(v,h,Player_Dot)==Size_Win-1)
                            {
                                if (MoveAiDiaDown(v, h, Ai_Dot)) return;
                            }
                        }

                    }
                    if (v+Size_Win <=Size_Y)
                    {
                        if(CheckLineVertical(v,h, Player_Dot)==Size_Win-1)
                        {
                            if (MoveAiLineVertical(v, h, Ai_Dot)) return;
                        }
                    }
                }
            }
            for (int v=0; v<Size_Y; v++)
            {
                for (int h=0; h< Size_X; h++)
                {
                    if (h+Size_Win <=Size_X)
                    {
                        if (CheckLineHorisont(v,h, Player_Dot)==Size_Win-1)
                        {
                            if (MoveAiLineHorisont(v, h, Ai_Dot)) return;
                        }

                        if (v-Size_Win >-2)
                        {
                            if (CheckDiaUp(v,h, Player_Dot)==Size_Win -1)
                            {
                                if (MoveAiDiaUp(v, h, Ai_Dot)) return;
                            }
                        }
                        if (v+Size_Win<=Size_Y)
                        {
                            if (CheckDiaDown(v,h,Player_Dot)== Size_Win-1)
                            {
                                if (MoveAiDiaDown(v, h, Ai_Dot)) return;
                            }
                        }

                    }
                }
            }
            do
            {
                x = random.Next(0, Size_X);
                y = random.Next(0, Size_Y);
            } while (!IsCellValid(y, x));
            SetSym(y, x, Player_Dot);
        }

        private static void SetSym(int y, int x, char sym)
        {
            field[y, x] = sym;
        }
        private static bool IsCellValid(int y, int x)
        {
            if (x<0||y<0||x>Size_X-1||y>Size_Y-1)
            {
                return false;
            }
            return field[y, x] == Empty_Dot;
        }

        private static bool IsFieldFull()
        {
            for (int i=0; i<Size_Y; i++)
            {
                for (int j = 0; j< Size_X; j++)
                {
                    if (field[i,j]==Empty_Dot)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool MoveAiLineHorisont (int v, int h, char dot)
        {
            for (int j=h; j<Size_Win; j++)
            {
                if ((field[v,j]== Empty_Dot))
                {
                    field[v, j] = dot;
                    return true;
                }
            }return false;
        }

        private static bool MoveAiLineVertical(int v, int h, char dot)
        {
            for (int i=v; i< Size_Win; i++)
            {
                if ((field[i,h]== Empty_Dot))
                {
                    field[i, h] = dot;
                    return true;
                }
            }
            return false;
        }
         private static bool MoveAiDiaUp(int v, int h, char dot)
        {
            for (int i=0, j=0;j< Size_Win; i--, j++)
            {
                if ((field[v+i,h+j]==Empty_Dot))
                {
                    field[v + i, h + j] = dot;
                    return true;
                }
            } return false;
        }

        // проверка заполнения всей линии по диаганали вниз

        private static bool MoveAiDiaDown(int v, int h, char dot)
        {
            for (int i=0; i<Size_Win;i++)
            {
                if ((field[i+v,i+h]== Empty_Dot))
                {
                    field[i + v, i + h] = dot;
                    return true;
                }
            }
            return false;
        }
        
        
        
        //проверка победы
        private static bool CheckWin(char dot)
        {
            for (int v=0; v<Size_Y; v++)
            {
            for (int h=0; h< Size_X; h++)
                {
                    //анализ наличия поля для проверки
                    if (h+ Size_Win <=Size_X)
                    {
                        if (CheckLineHorisont(v, h, dot) >= Size_Win)
                            return true;
                        if(v-Size_Win>-2)
                        {
                            if (CheckDiaUp(v, h, dot) >= Size_Win)
                                return true;
                        }
                        if (v+Size_Win<Size_Y)
                        {
                            if (CheckDiaDown(v, h, dot) >= Size_Win)
                                return true;
                        }
                            
                    }
                    if (v+Size_Win<Size_Y)
                    {
                        if (CheckLineVertical(v, h, dot) >= Size_Win)
                            return true;
                    }
                }
            }
            return false;
        }
        
        
        
        
        // проверка заполнения 
        private static int CheckDiaUp(int v, int h, char dot)
        {
            int count = 0;
            for (int i = 0, j=0; j < Size_Win; i--, j++)
            {
                if ((field[i + v, j + h] == dot)) count++;
            }
            return count;
        }
        private static int CheckDiaDown(int v, int h, char dot)
        {
            int count = 0;
            for (int i = 0; i < Size_Win; i++)
            {
                if ((field[i+v, i+h] == dot)) count++;
            }
            return count;
        }

        private static int CheckLineHorisont(int v, int h, char dot)
        {
            int count = 0;
            for (int j = h; j < Size_Win + h; j++)
            {
                if ((field[v, j] == dot)) count++;
            }
            return count;
        }

        private static int CheckLineVertical (int v, int h, char dot)
        {
            int count = 0;
            for(int i=v; i<Size_Win +v; i++)
            {
                if ((field[i, h] == dot)) count++;
            }
            return count;
        }


        static void Main(string[] args)
        {
            InitFild();
            PrintField();
            do
            {
                playerMove();
                Console.WriteLine("Ваш ход ");
                PrintField();
                if (CheckWin(Player_Dot))
                {
                    Console.WriteLine("Вы выйграли");
                    break;
                }
                else if (IsFieldFull())
                    break;
                AiMove();
                Console.WriteLine("Ход противника");
                PrintField();
                if (CheckWin(Ai_Dot))
                {
                    Console.WriteLine("Выйграл противник");
                    break;
                }
                else if (IsFieldFull())
                    break;

            } while (true);
            Console.WriteLine("Конец игры");
        }
    }
}
