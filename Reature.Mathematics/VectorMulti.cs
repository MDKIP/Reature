using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.NumbersGeneration;

namespace Reature.Mathematics
{
    /// <summary>
    /// Reprezentuje wielowymiarowy wektor.
    /// </summary>
    public struct VectorMulti : ICloneable
    {
        /// <summary>
        /// Tworzy nowy wielowymiarowy wektor.
        /// </summary>
        /// <param name="dimensionsNumber">Liczba wymiarów.</param>
        public VectorMulti(int dimensionsNumber)
        {
            if (dimensionsNumber <= 0)
            {
                throw new ArgumentException("dimensionsNumber nie może być mniejszy od 0.");
            }

            DimensionsNumber = dimensionsNumber;

            Data = new float[dimensionsNumber];
        }

        /// <summary>
        /// Dane które reprezentują ten wektor.
        /// </summary>
        public float[] Data { get; private set; }
        /// <summary>
        /// Liczba wymiarów na których operuje ten wektor.
        /// </summary>
        public int DimensionsNumber { get; private set; }

        /// <summary>
        /// Dodaje do tego wektora inny wektor.
        /// </summary>
        /// <param name="vector">Wektor do dodania.</param>
        public void Add(VectorMulti vector)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] += vector.Data[i];
            }
        }
        /// <summary>
        /// Wykonuje podaną operację na każdej danej.
        /// </summary>
        /// <param name="operation">Operacja do wykonania.</param>
        public void Foreach(ArrayOperationHandler<float> operation)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = operation.Invoke(Data[i], i++);
            }
        }
        // Zaimplementowane z ICloneable
        /// <summary>
        /// Klonuje ten matrix.
        /// </summary>
        /// <returns>Zwraca klona tego matrixa.</returns>
        public object Clone()
        {
            VectorMulti clon = new VectorMulti(DimensionsNumber);

            for (int i = 0; i < Data.Length; i++)
            {
                clon.Data[i] = Data[i];
            }

            return clon;
        }
    }
}
