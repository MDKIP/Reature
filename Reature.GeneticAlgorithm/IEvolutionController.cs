using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.GeneticAlgorithm
{
    /// <summary>
    /// Interfejs kontrolujący ewolucję.
    /// </summary>
    public interface IEvolutionController
    {
        /// <summary>
        /// Zwraca randomowo utworzone DNA.
        /// </summary>
        /// <returns>Zwraca DNA.</returns>
        IDna GetRandomDna();
        /// <summary>
        /// Krzyżuje dwóch rodziców czego wynikiem jest dziecko.
        /// </summary>
        /// <param name="parentA">Rodzic A.</param>
        /// <param name="parentB">Rodzic B.</param>
        /// <returns>Zwraca dziecko.</returns>
        IDna Crossover(IDna parentA, IDna parentB);
    }
}
