using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.NumbersGeneration;

namespace Reature.Mathematics
{
    /// <summary>
    /// Klasa reprezentująca pojedynczy matrix.
    /// </summary>
    public class Matrix : ICloneable
    {
        /// <summary>
        /// Tworzy nowy matrix.
        /// </summary>
        /// <param name="rows">Liczba rzędów nowego matrixu.</param>
        /// <param name="columns">Liczba kolumn nowego matrixu.</param>
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Data = new float[Rows, Columns];
            Foreach(f => 0);
        }

        /// <summary>
        /// Dane matrixu w postaci liczb zmiennoprzecinkowych.
        /// </summary>
        public float[,] Data { get; private set; }
        /// <summary>
        /// Ilość rzędów jakie posiada matrix.
        /// </summary>
        public int Rows { get; private set; }
        /// <summary>
        /// Ilość kolumn jakie posiada matrix.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Obraca matrix.
        /// </summary>
        public void Transpoze()
        {
            float[,] newData = new float[Columns, Rows];

            Foreach((f, x, y) => 
            {
                newData[y, x] = f;
                return f;
            });

            int temp = Rows;
            Rows = Columns;
            Columns = temp;

            Data = newData;
        }
        /// <summary>
        /// Losuje wszystkie elementy matrixu.
        /// </summary>
        /// <param name="maxValue">Maksymalna wartość pojedynczego elementu matrixu.</param>
        public void Randomize(float maxValue)
        {
            Foreach(f => RandomGenerator.GetFloat(maxValue));
        }
        /// <summary>
        /// Dodaje do wszystkich elementów matrixu wartość.
        /// </summary>
        /// <param name="value">Wartość do dodania.</param>
        public void Add(float value)
        {
            Foreach(f => f + value);
        }
        /// <summary>
        /// Mnoży każdy element matrixu przez wartość.
        /// </summary>
        /// <param name="value">Wartość.</param>
        public void Multiply(float value)
        {
            Foreach(f => f * value);
        }
        /// <summary>
        /// Iteruje przez wszytkie dane matrixu i wykonuje na nich operację.
        /// </summary>
        /// <param name="operation">Operacja do wykonania.</param>
        public void Foreach(FloatOperationHandler operation)
        {
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    Data[x, y] = operation.Invoke(Data[x, y]);
                }
            }
        }
        /// <summary>
        /// Iteruje przez wszytkie dane matrixu i wykonuje na nich operację.
        /// </summary>
        /// <param name="operation">Operacja do wykonania.</param>
        public void Foreach(MatrixOperationHandler operation)
        {
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    Data[x, y] = operation.Invoke(Data[x, y], x, y);
                }
            }
        }
        /// <summary>
        /// Zwraca tekst reprezentujący matrix. Jego format to wartość; a przy każdym rzedzie zaczyna od nowej linji.
        /// </summary>
        /// <returns>Zwraca tekst reprezentujący matrix.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            Foreach((f, x, y) =>
            {
                sb.Append($"{f}; ");

                if (y == Columns-1 && x < Rows-1)
                {
                    sb.Append(Environment.NewLine);
                }

                return f;
            });

            return sb.ToString();
        }
        // Zaimplementowane z ICloneable
        public object Clone()
        {
            Matrix clon = new Matrix(Rows, Columns);

            clon.Foreach((f, x, y) => 
            {
                return Data[x, y];
            });

            return clon;
        }

        /// <summary>
        /// Zwraca matrix z randomowymi wartościami.
        /// </summary>
        /// <param name="rows">Liczba rzędów matrixu.</param>
        /// <param name="columns">Liczba kolumn matrixu.</param>
        /// <returns>Zwraca matrix z randomowymi wartościami.</returns>
        static public Matrix GetRandomMatrix(int rows, int columns)
        {
            Matrix output = new Matrix(rows, columns);
            output.Randomize(100);
            return output;
        }
        /// <summary>
        /// Dodaje dwa matrixy do siebie. 
        /// </summary>
        /// <param name="a">Pierwszy matrix do dodania.</param>
        /// <param name="b">Drugi matrix do dodania</param>
        /// <returns>Zwraca sumę 2 matrixów jako matrix.</returns>
        static public Matrix Add(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new Exception("Rozmiary matrixów nie są takie same.");
            }

            Matrix output = new Matrix(a.Rows, a.Columns);

            output.Foreach((f, x, y) => a.Data[x, y] + b.Data[x, y]);

            return output;
        }
    }
}
