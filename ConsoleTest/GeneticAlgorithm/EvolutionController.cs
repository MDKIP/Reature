using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.GeneticAlgorithm;

namespace ConsoleTest.GeneticAlgorithm
{
    public class EvolutionController : IEvolutionController
    {
        public EvolutionController(string target)
        {
            this.target = target;
        }

        private string target;

        // Zaimplementowane z IEvolutionController
        public IDna Crossover(IDna parentA, IDna parentB)
        {
            IDna child = new Dna(target, target.Length);

            char[] childGenes = child.GetGenes() as char[];
            char[] aGenes = parentA.GetGenes() as char[];
            char[] bGenes = parentB.GetGenes() as char[];

            int midpoint = childGenes.Length / 2;
            for (int i = 0; i < childGenes.Length; i++)
            {
                if (i < midpoint)
                {
                    childGenes[i] = aGenes[i];
                }
                else
                {
                    childGenes[i] = bGenes[i];
                }
            }

            return child;
        }
        public IDna GetRandomDna()
        {
            return new Dna(target, target.Length);
        }
    }
}
