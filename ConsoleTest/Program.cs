using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using ConsoleTest.GeneticAlgorithm;
using Reature.GeneticAlgorithm;
using Reature.Perceptrons;
using Reature.NumbersGeneration;
using Reature.NeuralNetworks;
using Reature.Mathematics;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Reature Framework");
            WriteLine();

            TestMatrixs();

            ReadKey();
        }
        static void TestGeneticAlgorithm()
        {
            string target = "Be or not to be?";
            IEvolutionController evolutionController = new EvolutionController(target);
            Population population = new Population(evolutionController, 300);
            population.MutationChance = 10;

            population.CurrentGeneration.Evaluate();
            while (true)
            {
                ReadKey();

                
                population.CurrentGeneration.Evaluate();

                IDna db = population.CurrentGeneration.Best;
                WriteLine(new string(db.GetGenes() as char[]) + " ||| " + db.Fitness);

                population.CreateNewGeneration();
                
            }
        }
        static void TestPerceptron()
        {
            Dictionary<float[], float> trainingData = new Dictionary<float[], float>();
            trainingData.Add(new float[] { -1, -1 }, -1);
            trainingData.Add(new float[] { -1, 1 }, -1);
            trainingData.Add(new float[] { 1, 1 }, 1);

            Perceptron brain = new Perceptron(2, ActivationFunctions.BinaryStep, true)
            {
                LearningRate = 0.1f,
            };

            brain.StartAutomaticTraining(trainingData, 1000);

            WriteLine("Trening perceptrona zakończony!");
            WriteLine("");
            WriteLine("--------");

            while (true)
            {
                float x0 = float.Parse(ReadLine());
                float x1 = float.Parse(ReadLine());
                float answer = brain.GetGuess(new float[] { x0, x1, });

                WriteLine(answer);
                WriteLine("--------");
            }
        }
        static void TestNeuralNetworks()
        {
            Neuron brain = new Neuron(2, ActivationFunctions.BinaryStep)
            {
                LearningRate = 0.001f,
                AddBias = true,
            };
            brain.TrainingEnd += Brain_TrainingEnd;

            var td = new Dictionary<float[], float>();
            td.Add(new float[] { -1, -1 }, -1);
            td.Add(new float[] { -1, 1 }, -1);
            td.Add(new float[] { 1, 1 }, 1);

            while (true)
            {
                var ctd = td.ElementAt(RandomGenerator.GetInt(td.Count-1));
                WriteLine(brain.Train(ctd.Key, ctd.Value));
            }
        }
        static void TestRandomNumbers()
        {
            for (int i = 0; i < 100; i++)
            {
                WriteLine(RandomGenerator.GetFloat(100));
            }
        }
        static void TestMatrixs()
        {
            Matrix m = new Matrix(2, 3);
            WriteLine(m.ToString());
            WriteLine();

            m.Randomize(10);
            WriteLine(m.ToString());
            WriteLine();

            m.Add(RandomGenerator.GetInt(10));
            WriteLine(m.ToString());
            WriteLine();

            m.Multiply(2);
            WriteLine(m.ToString());
            WriteLine();

            m.Foreach(f => f * 0.5f);
            WriteLine(m.ToString());
            WriteLine();

            m.Transpoze();
            WriteLine(m.ToString());
            WriteLine();
        }

        private static void Brain_TrainingEnd(float[] inputs, float answer, float guess, float error)
        {
            WriteLine(error);
        }
    }
}
