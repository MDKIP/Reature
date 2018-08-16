using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.NumbersGeneration
{
    static public class NumbersManipulator
    {
        /// <summary>
        /// Zwraca sumę arytmetyczną z kolekcji byte.
        /// </summary>
        /// <param name="data">Kolekcja do wykonania operacji.</param>
        /// <returns>Zwraca sumę arytmetyczną.</returns>
        static public byte Average(IEnumerable<byte> data)
        {
            int sum = 0;
            foreach (byte d in data)
            {
                sum += d;
            }
            return (byte)(sum / data.Count());
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public byte Map(byte currentValue, byte currentMinValue, byte currentMaxValue, byte minValue, byte maxValue)
        {
            return (byte)Map((float)currentValue, (float)currentMinValue, (float)currentMaxValue, (float)minValue, (float)maxValue);
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public int Map(int currentValue, int currentMinValue, int currentMaxValue, int minValue, int maxValue)
        {
            return (int)Map((float)currentValue, (float)currentMinValue, (float)currentMaxValue, (float)minValue, (float)maxValue);
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public long Map(long currentValue, long currentMinValue, long currentMaxValue, long minValue, long maxValue)
        {
            return (long)Map((float)currentValue, (float)currentMinValue, (float)currentMaxValue, (float)minValue, (float)maxValue);
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public float Map(float currentValue, float currentMinValue, float currentMaxValue, float minValue, float maxValue)
        {
            return minValue + ((maxValue - minValue) / (currentMaxValue - currentMinValue)) * (currentValue - currentMinValue);
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public double Map(double currentValue, double currentMinValue, double currentMaxValue, double minValue, double maxValue)
        {
            return minValue + ((maxValue - minValue) / (currentMaxValue - currentMinValue)) * (currentValue - currentMinValue);
        }
        /// <summary>
        /// Konwertuje jedną liczbę z jednego przedziału na drugi przedział.
        /// </summary>
        /// <param name="currentValue">Wartość do przekonwertowania.</param>
        /// <param name="currentMinValue">Obecna minimalna granica.</param>
        /// <param name="currentMaxValue">Obecna maksymalna granica</param>
        /// <param name="minValue">Przyszła minimalna granica.</param>
        /// <param name="maxValue">Przyszła maksymalna granica.</param>
        /// <returns></returns>
        static public decimal Map(decimal currentValue, decimal currentMinValue, decimal currentMaxValue, decimal minValue, decimal maxValue)
        {
            return minValue + ((maxValue - minValue) / (currentMaxValue - currentMinValue)) * (currentValue - currentMinValue);
        }
    }
}
