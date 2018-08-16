using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.GeneticAlgorithm;

namespace ConsoleTest.GeneticAlgorithm
{
    public class Dna : IDna
    {
        public Dna(string target, int length)
        {
            this.target = target;
            genes = new char[length];

            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = GetRandomChar();
            }
        }

        private string target;
        private char[] genes;
        private int globalRandomModifier;

        // Zaimplementowane z IDna
        public int Fitness { get; private set; }
        public int Id { get; set; }
        public Generation Generation { get; set; }
        public void CalculateFitness()
        {
            Fitness = 0;
            for (int i = 0; i < target.Length; i++)
            {
                if (genes[i] == target[i])
                {
                    Fitness++;
                }
            }
        }
        public object GetGenes()
        {
            return genes;
        }
        public void Mutate(byte chance)
        {
            int randomModifier = globalRandomModifier;
            for (int i = 0; i < genes.Length; i++)
            {
                if (new Random(randomModifier).Next(100) < chance)
                {
                    genes[i] = GetRandomChar();
                }
                randomModifier++;
            }
            globalRandomModifier++;
        }

        static int randomModifier = 0;
        static public char GetRandomChar()
        {
            string chars = " $%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";

            Random rand = new Random(randomModifier);
            randomModifier++;

            int index = rand.Next(chars.Length);
            return chars[index];
        }
    }
}
