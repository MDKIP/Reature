using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.NumbersGeneration;

namespace Reature.GeneticAlgorithm
{
    /// <summary>
    /// Klasa reprezentująca pojedynczą generację.
    /// </summary>
    public class Generation
    {
        /// <summary>
        /// Tworzy nową generację z określoną ilością obiektów oraz kontrolerem ewolucyjnym.
        /// </summary>
        /// <param name="evolutionController">Kontroler ewolucyjny.</param>
        /// <param name="size">Ilość obiektów.</param>
        public Generation(IEvolutionController evolutionController, uint size)
        {
            Objects = new IDna[size];
            this.evolutionController = evolutionController ?? throw new ArgumentNullException("evolutionController nie może być null.");

            for (int i = 0; i < size; i++)
            {
                Objects[i] = evolutionController.GetRandomDna();
                Objects[i].Generation = this;
                Objects[i].Id = i;
            }
        }
        /// <summary>
        /// Tworzy nową generację z obiektami oraz kontrolerem ewolucyjnym.
        /// </summary>
        /// <param name="evolutionController">Kontroler ewolucyjny.</param>
        /// <param name="objects">Objekty należące do tej generacji.</param>
        public Generation(IEvolutionController evolutionController, IDna[] objects)
        {
            Objects = objects;
            this.evolutionController = evolutionController ?? throw new ArgumentNullException("evolutionController nie może być null.");

            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].Generation = this;
                Objects[i].Id = i;
            }
        }

        /// <summary>
        /// Obiekty kontrolowane przez tą generację.
        /// </summary>
        public IDna[] Objects { get; }
        /// <summary>
        /// Dna z największym fitnesem. Jest pusty przed wywołaniem metody Evaluate(). 
        /// </summary>
        public IDna Best { get; private set; }
        /// <summary>
        /// Suma wszystkich fitnessów. Wykorzystywana do losowania dna według fitnessu.
        /// </summary>
        public long FitnessSum { get; private set; }
        /// <summary>
        /// Wskazuje na to czy metoda Evalute() została wywołana.
        /// </summary>
        public bool WasEvaluated { get; private set; }

        private IEvolutionController evolutionController;

        /// <summary>
        /// Oblicza fitness dla pojedynczych obiektów oraz wybiera najlepszego i przypisuje go do Best.
        /// </summary>
        public void Evaluate()
        {
            // Oblicznie fitnessów i wybieranie najlepszego.
            Best = evolutionController.GetRandomDna();
            foreach (IDna dna in Objects)
            {
                dna.CalculateFitness();
                if (dna.Fitness > Best.Fitness)
                {
                    Best = dna;
                }
            }

            // Sumowanie fitnessu wszystkich obiektów.
            foreach (IDna dna in Objects)
            {
                FitnessSum += dna.Fitness;
            }

            WasEvaluated = true;
        }
        /// <summary>
        /// Mutuje wszystkie DNA.
        /// </summary>
        /// <param name="chance">Szansa na mutację w przedziale od 0 do 100.</param>
        public void Mutate(byte chance)
        {
            foreach (IDna dna in Objects)
            {
                dna.Mutate(chance);
            }
        }
        /// <summary>
        /// Zwraca losowe DNA. DNA z większym fitnessem mają większe szanse na wylosowanie.
        /// </summary>
        /// <returns>Zwraca losowe DNA.</returns>
        public IDna GetDnaBasedOnFitness()
        {
            if (!WasEvaluated)
            {
                throw new Exception("Nie można wybrać DNA bazując na fitnessie kiedy jeszcze nie jest on obliczony. Aby zapobiec błędowi wywołaj metodę Evaluate().");
            }

            // Oblicznie prawdopodobieństwa wylosowania.
            float[] probabilites = new float[Objects.Length];
            for (int i = 0; i < Objects.Length; i++)
            {
                    probabilites[i] = (float)Objects[i].Fitness / (float)FitnessSum;
            }

            // Losowanie.
            int index = 0;
            float r = RandomGenerator.GetFloat(0.01f, 0.9f);
            while (r > 0)
            {
                r = r - probabilites[index];
                index++;
            }
            index--;
            return Objects[index];
        }
        /// <summary>
        /// Zwraca totalnie randomowe DNA.
        /// </summary>
        /// <returns>Zwraca totalnie randomowe DNA.</returns>
        public IDna GetTotalRandomDna()
        {
            return Objects[RandomGenerator.GetInt(Objects.Length - 1)];
        }
    }
}
