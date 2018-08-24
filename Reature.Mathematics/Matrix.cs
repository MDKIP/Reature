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
    public struct Matrix : ICloneable
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
        public void Transpose()
        {
            float[,] newData = new float[Columns, Rows];

            Foreach((f, x, y) => 
            {
                newData[y, x] = f;
                return f;
            });

            int h = Rows;
            Rows = Columns;
            Columns = h;

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
        /// Losuje wszystkie elementy matrixu.
        /// </summary>
        /// <param name="maxValue">Maksymalna wartość pojedynczego elementu matrixu.</param>
        /// <param name="minValue">Minimalna wartość pojedynczego elementu matrixu.</param>
        public void Randomize(float minValue, float maxValue)
        {
            Foreach(f => RandomGenerator.GetFloat(minValue, maxValue));
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
        /// Dodaje do tego matrixu inny matrix.
        /// </summary>
        /// <param name="m">Matrix do dodania.</param>
        public void Add(Matrix m)
        {
            if (Rows != m.Rows || Columns != m.Columns)
            {
                throw new ArgumentException("Wielkość podanego Matrixu jest zła.");
            }

            Foreach((f, x, y) => f + m.Data[x, y]);
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
        /// Mnoży 2 matrixy przez siebie. Jeżeli matrixy mają takie same rozmiary wykonany zostanie produkt hadamara. Jeżeli nie mają takich samych rozmiarów ale liczba kolumn tego matrixu odpowiada liczbie rzędów matrixu m matrixy zostaną pomnożone.
        /// </summary>
        /// <param name="m">Matrix do mnożenia.</param>
        public void Multiply(Matrix m)
        {
            if (Rows == m.Rows || Columns == m.Columns)
            {
                Foreach((f, x, y) => f * m.Data[x, y]);
            }
            else if (Columns == m.Rows)
            {
                int c = Columns;
                Foreach((f, x, y) => 
                {
                    float sum = 0;
                    for (int i = 0; i < c; i++)
                    {
                        sum += f * m.Data[i, y];
                    }
                    return sum;
                });
            }
            else
            {
                throw new ArgumentException("Rozmiary matrixów nie są prawidłowe.");
            }
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
        /// Zwraca ten matriks jako jednowymiarową tablicę.
        /// </summary>
        /// <returns>Zwraca ten matriks jako jednowymiarową tablicę.</returns>
        public float[] ToOneDimensionArray()
        {
            float[] output = new float[Rows * Columns];

            Foreach((f, x, y) =>
            {
                output[x + y * 2] = f;
                return f;
            });

            return output;
        }
        /// <summary>
        /// Zwraca tekst reprezentujący matrix. Jego format to wartość; a przy każdym rzedzie zaczyna od nowej linji.
        /// </summary>
        /// <returns>Zwraca tekst reprezentujący matrix.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    sb.Append($"{Data[x, y]}; ");

                    if (y == Columns - 1 && x < Rows - 1)
                    {
                        sb.Append(Environment.NewLine);
                    }
                }
            }

            return sb.ToString();
        }
        // Zaimplementowane z ICloneable
        /// <summary>
        /// Klonuje ten matrix.
        /// </summary>
        /// <returns>Zwraca klona tego matrixa.</returns>
        public object Clone()
        {
            Matrix clon = new Matrix(Rows, Columns);

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    clon.Data[x, y] = Data[x, y];
                }
            }

            return clon;
        }

        /// <summary>
        /// Przekształca jednowymiarową tablicę na matrix.
        /// </summary>
        /// <param name="array">Jednowymiarowa tablica.</param>
        /// <returns>Zwraca matrix utworzony z jednowymiarowej tablicy.</returns>
        static public Matrix FromOneDimensionArray(float[] array)
        {
            Matrix output = new Matrix(1, array.Length);

            output.Foreach((f, x, y) => array[y]);

            return output;
        }
        /// <summary>
        /// Klonuje podany matrix a potem go obraca.
        /// </summary>
        /// <param name="m">Matrix.</param>
        /// <returns>Zwraca obrócony matrix nie wpływając na matrix z inputu.</returns>
        static public Matrix Transpose(Matrix m)
        {
            Matrix c = (Matrix)m.Clone();
            c.Transpose();
            return c;
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
        /// Klonuje matrix a następnie wykonuje na nim operację.
        /// </summary>
        /// <param name="m">Matrix.</param>
        /// <param name="operation">Operacja do wykonania.</param>
        /// <returns>Zwraca matrix z wykonaną na nim operacją.</returns>
        static public Matrix Foreach(Matrix m, FloatOperationHandler operation)
        {
            Matrix output = (Matrix)m.Clone();
            output.Foreach(operation);
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
        /// <summary>
        /// Odejmuje dwa matrixy od siebie. 
        /// </summary>
        /// <param name="a">Pierwszy matrix.</param>
        /// <param name="b">Drugi matrix do odjęcia</param>
        /// <returns>Zwraca różnicę 2 matrixów jako matrix.</returns>
        static public Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                throw new Exception("Rozmiary matrixów nie są takie same.");
            }

            Matrix output = new Matrix(a.Rows, a.Columns);

            output.Foreach((f, x, y) => a.Data[x, y] - b.Data[x, y]);

            return output;
        }
        /// <summary>
        /// Mnoży matrixy przez wartość.
        /// </summary>
        /// <param name="m">Matrix.</param>
        /// <param name="value">Wartość do mnożenia.</param>
        /// <returns>Zwraca matrix pomnożony przez wartość.</returns>
        static public Matrix Multiply(Matrix m, float value)
        {
            Matrix c = (Matrix)m.Clone();
            c.Foreach(f => f * value);
            return c;
        }
        /// <summary>
        /// Mnoży matrixy przez siebie. Jeżeli liczba kolumn a będzie równa liczbie rzędów b to zwróci pomnożone matrixy. Jeżeli matrixy będą mieć takie same rozmiary to zostanie zwrócony produkt hadamara.
        /// </summary>
        /// <param name="a">Matrix a.</param>
        /// <param name="b">Matrix b.</param>
        /// <returns>Jeżeli liczba kolumn a będzie równa liczbie rzędów b to zwróci pomnożone matrixy. Jeżeli matrixy będą mieć takie same rozmiary to zostanie zwrócony produkt hadamara.</returns>
        static public Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Rows == b.Rows && a.Columns == b.Columns)
            {
                Matrix output = new Matrix(a.Rows, a.Columns);

                output.Foreach((f, x, y) => a.Data[x, y] * b.Data[x, y]);

                return output;
            }
            else if (a.Columns == b.Rows)
            {
                Matrix output = new Matrix(a.Rows, b.Columns);

                output.Foreach((f, x, y) =>
                {
                    float sum = 0;
                    for (int i = 0; i < a.Columns; i++)
                    {
                        sum += a.Data[x, i] * b.Data[i, y];
                    }
                    return sum;
                });

                return output;
            }
            else
            {
                throw new ArgumentException("Rozmiary matrixów nie są prawidłowe.");
            }
        }
    }
}
