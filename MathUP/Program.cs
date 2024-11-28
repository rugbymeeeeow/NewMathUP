using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в программу!");

            int[] sup = SupplierEnter();
            int[] cos = ConsumerEnter();
            int[,] SupCosTogether = GetTogether(sup.Length, cos.Length);

            Console.WriteLine("Выберите нужный вам метод: 1 - Северо-западного угла, 2 - минимальных стоимостей");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Вы ввели что-то не то! Пожалуйста, выберите 1 или 2.");
            }

            if (choice == 1)
            {
                NorthWestCorner(SupCosTogether, sup, cos);
            }
            else if (choice == 2)
            {
                Minimum(SupCosTogether, sup, cos);
            }
        }

        public static int[] SupplierEnter()
        {
            Console.WriteLine("Введите возможности поставщиков (числа через пробел):");
            return Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        }

        public static int[] ConsumerEnter()
        {
            Console.WriteLine("Введите возможности потребителей (числа через пробел):");
            return Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        }

        public static int[,] GetTogether(int Supplier, int Consumer)
        {
            int[,] together = new int[Supplier, Consumer];
            Console.WriteLine("Введите матрицу затрат:");
            for (int i = 0; i < Supplier; i++)
            {
                for (int j = 0; j < Consumer; j++)
                {
                    Console.WriteLine($"Введите значение для {i + 1} {j + 1}:");
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out int result))
                        {
                            together[i, j] = result;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели не число! Пожалуйста, попробуйте снова.");
                        }
                    }
                }
            }
            return together;
        }

        public static void NorthWestCorner(int[,] massive, int[] su, int[] cos)
        {
            Console.WriteLine("Вы выбрали метод Северо-западного угла");
            int numSuppliers = massive.GetLength(0);
            int numConsumers = massive.GetLength(1);
            int[,] all = new int[numSuppliers, numConsumers];
            int score = 0;
            int i = 0; int j = 0;

            while (i < numSuppliers && j < numConsumers)
            {
                int minimum = Math.Min(su[i], cos[j]);
                score += minimum * massive[i, j];

                all[i, j] = minimum;
                su[i] -= minimum;
                cos[j] -= minimum;

                if (su[i] == 0) i++;

                if (cos[j] == 0) j++;
            }

            Console.WriteLine("Распределение:");

            for (int x = 0; x < numSuppliers; x++)
            {
                for (int y = 0; y < numConsumers; y++)
                {

                    Console.Write(all[x, y] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Общий счет: " + score);
            Console.ReadLine();
        }
        public static void Minimum(int[,] massive, int[] su, int[] cos)
        {
            Console.WriteLine("Вы выбрали метод минимальных стоимостей");

            int numSuppliers = massive.GetLength(0);
            int numConsumers = massive.GetLength(1);
            int[,] all = new int[numSuppliers, numConsumers];
            int score = 0;
            int i = 0;
            int j = 0;

            while (i < numSuppliers && j < numConsumers)
            {
                int minValue = int.MaxValue;
                int minI = -1, minJ = -1;

                for (int x = 0; x < numSuppliers; x++)
                {
                    for (int y = 0; y < numConsumers; y++)
                    {
                        if (massive[x, y] < minValue && su[x] > 0 && cos[y] > 0)
                        {
                            minValue = massive[x, y];
                            minI = x;
                            minJ = y;
                        }
                    }
                }

                if (minI == -1 || minJ == -1) break;

                int minimum = Math.Min(su[minI], cos[minJ]);
                score += minimum * minValue;
                all[minI, minJ] = minimum;

                su[minI] -= minimum;
                cos[minJ] -= minimum;

                if (su[minI] == 0) i++;
                if (cos[minJ] == 0) j++;
            }

            Console.WriteLine("Распределение:");
            for (int x = 0; x < numSuppliers; x++)
            {
                for (int y = 0; y < numConsumers; y++)
                {
                    Console.Write(all[x, y] + " ");
                }
                Console.WriteLine(); 
            }

            Console.WriteLine("Общий счет: " + score);
            Console.ReadLine();
        }

    }
}
