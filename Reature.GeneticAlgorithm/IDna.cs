using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.GeneticAlgorithm
{
    /// <summary>
    /// Interfejs reprezentujący DNA.
    /// </summary>
    public interface IDna
    {
        /// <summary>
        /// Fitness tego DNA.
        /// </summary>
        int Fitness { get; }
        /// <summary>
        /// ID tego DNA. Jest przydzielane podczas tworzenia generacji.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Generacja która utworzyła to DNA.
        /// </summary>
        Generation Generation { get; set; }

        /// <summary>
        /// Zwraca geny tego DNA.
        /// </summary>
        /// <returns>Zwraca geny tego DNA.</returns>
        object GetGenes();
        /// <summary>
        /// Oblicza fitness tego obiektu i przypisuje go do Fitness.
        /// </summary>
        void CalculateFitness();
        /// <summary>
        /// Mutuje geny tego DNA.
        /// </summary>
        /// <param name="chance">Szansa na mutację w przedziale od 0 do 100.</param>
        void Mutate(byte chance);
    }
}
