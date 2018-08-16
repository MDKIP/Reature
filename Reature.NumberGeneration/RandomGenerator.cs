using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Reature.NumbersGeneration
{
    /// <summary>
    /// Generator liczb losowych.
    /// </summary>
    static public class RandomGenerator
    {
        static RandomGenerator()
        {
            bytesGenerator = RandomNumberGenerator.Create();
        }

        static private RandomNumberGenerator bytesGenerator;

        /// <summary>
        /// Zwraca losowy bajt w przedziale od 0 do 255.
        /// </summary>
        /// <returns>Zwraca losowy bajt w przedziale od 0 do 255.</returns>
        static public byte GetByte()
        {
            byte[] bytes = new byte[1];

            bytesGenerator.GetBytes(bytes);

            return bytes[0];
        }
        /// <summary>
        /// Zwraca losowy bajt w przedziale od 0 do określonego maksimum.
        /// </summary>
        /// <param name="maxValue">Liczba maksymalna.</param>
        /// <returns>Zwraca losowy bajt w przedziale od 0 do określonego maksimum.</returns>
        static public byte GetByte(byte maxValue)
        {
            byte b;
            do
            {
                b = GetByte();
            }
            while (b > maxValue);

            return b;
        }
        /// <summary>
        /// Zwraca losowy bajt w przedziale od określonego minimum do określonego maksimum.
        /// </summary>
        /// <param name="minValue">Liczba minimalna.</param>
        /// <param name="maxValue">Liczba maksymalna.</param>
        /// <returns>Zwraca losowy bajt w przedziale od określonego minimum do określonego maksimum.</returns>
        static public byte GetByte(byte minValue, byte maxValue)
        {
            byte b;
            do
            {
                b = GetByte();
            }
            while (b < minValue || b > maxValue);

            return b;
        }
        /// <summary>
        /// Zwraca randomową liczbę całkowitą większą niż 0.
        /// </summary>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę całkowitą.</returns>
        static public int GetInt(int maxValue, int randomLevel = 3)
        {
            return GetInt(0, maxValue, randomLevel);
        }
        /// <summary>
        /// Zwraca randomową liczbę całkowitą.
        /// </summary>
        /// <param name="minValue">Wartość minimalna.</param>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę całkowitą.</returns>
        static public int GetInt(int minValue, int maxValue, int randomLevel = 3)
        {
            if (randomLevel < 1)
            {
                randomLevel = 1;
            }
            else if (randomLevel > 10)
            {
                randomLevel = 10;
            }

            byte[] bytes = new byte[randomLevel];
            bytesGenerator.GetBytes(bytes);

            int seed = bytes[0];
            for (int i = 1; i < randomLevel; i++)
            {
                seed *= bytes[i];
            }

            Random rand = new Random(seed);
            int output = rand.Next(minValue, maxValue);

            return output;
        }
        /// <summary>
        /// Zwraca randomową liczbę zmiennoprzecinkową.
        /// </summary>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę zmiennoprzecinkową.</returns>
        static public float GetFloat(float maxValue, int randomLevel = 3)
        {
            return GetFloat(0, maxValue);
        }
        /// <summary>
        /// Zwraca randomową liczbę zmiennoprzecinkową.
        /// </summary>
        /// <param name="minValue">Wartość minimalna.</param>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę zmiennoprzecinkową.</returns>
        static public float GetFloat(float minValue, float maxValue, int randomLevel = 3)
        {
            int seed = GetInt(10000, randomLevel);

            Random rand = new Random(seed);
            float output = NumbersManipulator.Map((float)rand.NextDouble(), 0.0f, 1.0f, minValue, maxValue);

            return output;
        }
        /// <summary>
        /// Zwraca randomową liczbę zmiennoprzecinkową większą niż 0 oraz mniejszą niż 1.
        /// </summary>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę zmiennoprzecinkową.</returns>
        static public double GetDouble(int randomLevel = 3)
        {
            return GetDouble(0.0, 1.0, randomLevel);
        }
        /// <summary>
        /// Zwraca randomową liczbę zmiennoprzecinkową większą niż 0.
        /// </summary>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę zmiennoprzecinkową.</returns>
        static public double GetDouble(double maxValue, int randomLevel = 3)
        {
            return GetDouble(0, maxValue);
        }
        /// <summary>
        /// Zwraca randomową liczbę zmiennoprzecinkową.
        /// </summary>
        /// <param name="minValue">Wartość minimalna.</param>
        /// <param name="maxValue">Wartość maksymalna.</param>
        /// <param name="randomLevel">Stopień randomowości.</param>
        /// <returns>Zwraca randomową liczbę zmiennoprzecinkową.</returns>
        static public double GetDouble(double minValue, double maxValue, int randomLevel = 3)
        {
            int seed = GetInt(10000, randomLevel);

            Random rand = new Random(seed);
            double output = NumbersManipulator.Map(rand.NextDouble(), 0.0, 1.0, minValue, maxValue);

            return output;
        }
    }
}
