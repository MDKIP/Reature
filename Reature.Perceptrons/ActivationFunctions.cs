using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.Perceptrons
{
    /// <summary>
    /// Zawiera funkcje aktywacyjne. Każda z metod tej klasy jest zgodna z ActivationFunctionHandler.
    /// </summary>
    static public class ActivationFunctions
    {
        /// <summary>
        /// Funkcja aktywacyjna dla perceptronu. Zwraca po prostu ważoną sumę bez żadnych zmian.
        /// </summary>
        /// <param name="weightedSum">Ważona suma.</param>
        /// <returns>Zwraca po prostu ważoną sumę bez żadnych zmian.</returns>
        static public float Identity(float weightedSum)
        {
            return weightedSum;
        }
        /// <summary>
        /// Funkcja aktywacyjna dla perceptronu. Zwraca 1 gdy ważona suma jest większa niż lub równa 0 i zwraca -1 gdy ważona suma jest mniejsza niż 0.
        /// </summary>
        /// <param name="weightedSum">Ważona suma.</param>
        /// <returns>Zwraca 1 gdy ważona suma jest większa niż 0 i zwraca -1 gdy ważona suma jest mniejsza niż 0.</returns>
        static public float BinaryStep(float weightedSum)
        {
            if (weightedSum >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Funkcja aktywacyjna dla perceptronu.
        /// </summary>
        /// <param name="weightedSum">Suma ważona.</param>
        /// <returns>Zwraca sigmoid.</returns>
        static public float Sigmoid(float weightedSum)
        {
            return (float) (1 / (1 + Math.Pow(Math.E, -weightedSum)));
        }
        /// <summary>
        /// Funkcja aktywacyjna dla perceptronu. Zwraca ważoną sumę gdy ważona suma jest większa niż lub równa 0 i zwraca 0 gdy ważona suma jest mniejsza niż 0.
        /// </summary>
        /// <param name="weightedSum">Ważona suma.</param>
        /// <returns>Zwraca ważoną sumę gdy ważona suma jest większa niż lub równa 0 i zwraca 0 gdy ważona suma jest mniejsza niż 0.</returns>
        static public float RectifiedLinearUnit(float weightedSum)
        {
            if (weightedSum >= 0)
            {
                return weightedSum;
            }
            else
            {
                return 0;
            }
        }
    }
}
