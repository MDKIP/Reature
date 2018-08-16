using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.GeneticAlgorithm
{
    /// <summary>
    /// Klasa reprezentująca populację która kontroluje generacje.
    /// </summary>
    public class Population
    {
        /// <summary>
        /// Tworzy nową populację oraz w niej nową losową generację o określonym rozmiarze.
        /// </summary>
        /// <param name="evolutionController">Kontroler ewolucji.</param>
        /// <param name="generationSize">Wielkość generacji które będą należeć do tej generacji.</param>
        public Population(IEvolutionController evolutionController, uint generationSize)
        {
            GenerationsSize = generationSize;
            this.evolutionController = evolutionController ?? throw new ArgumentNullException("evolutionController nie może być null.");

            CurrentGeneration = new Generation(evolutionController, generationSize);
        }

        /// <summary>
        /// Obecna czyli ostatnia generacja.
        /// </summary>
        public Generation CurrentGeneration { get; private set; }
        /// <summary>
        /// Wielkość generacji należących do tej populacji.
        /// </summary>
        public uint GenerationsSize { get; private set; }
        /// <summary>
        /// Szansa na mutację w przedziale od 0 do 100.
        /// </summary>
        public byte MutationChance { get; set; } = 0;

        private IEvolutionController evolutionController;

        /// <summary>
        /// Tworzy nową generację krzyżując obiekty z obecnej generacji.
        /// </summary>
        /// <returns>Zwraca nową generację.</returns>
        public Generation CreateNewGeneration()
        {
            if (!CurrentGeneration.WasEvaluated)
            {
                CurrentGeneration.Evaluate();
            }

            IDna[] objects = new IDna[GenerationsSize];
            for (uint i = 0; i < GenerationsSize; i++)
            {
                IDna parentA = CurrentGeneration.GetDnaBasedOnFitness();
                IDna parentB = CurrentGeneration.GetDnaBasedOnFitness();
                objects[i] = evolutionController.Crossover(parentA, parentB);
                IDna db = objects[i];
                Console.WriteLine(new string(db.GetGenes() as char[]) + " ||| " + db.Fitness);
            }

            CurrentGeneration = new Generation(evolutionController, objects);
            CurrentGeneration.Mutate(MutationChance > 100 ? (byte)100 : MutationChance);
            return CurrentGeneration;
        }
    }
}
