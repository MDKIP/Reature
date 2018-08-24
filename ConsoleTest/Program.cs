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

            TestNeuralNetworks();

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
            SimpleNeuralNetwork brain = new SimpleNeuralNetwork(2, 25, 1,
                    ActivationFunctions.Sigmoid, ActivationFunctions.Sigmoid,
                    ActivationFunctions.Derivatives.Sigmoid, ActivationFunctions.Derivatives.Sigmoid)
            {
                AddBias = true,
                LearningRate = 0.01f,
            };

            /*// XOR TRAINING
            Dictionary<float[], float[]> trainingData = new Dictionary<float[], float[]>();
            trainingData.Add(new float[] { 0, 0 }, new float[] { 0, });
            trainingData.Add(new float[] { 0, 1 }, new float[] { 1, });
            trainingData.Add(new float[] { 1, 0 }, new float[] { 1, });
            trainingData.Add(new float[] { 1, 1 }, new float[] { 0, });

            for (int i = 0; i < 100000; i++)
            {
                var ctd = trainingData.ElementAt(RandomGenerator.GetInt(trainingData.Count));
                brain.Backpropagation(ctd.Key, ctd.Value);
            }
            WriteLine("Training end!");
            

            Get(0, 0);
            Get(0, 1);
            Get(1, 0);
            Get(1, 1);
            */

            
            Dictionary<float[], float[]> trainingData = new Dictionary<float[], float[]>();
            trainingData.Add(new float[] { 0, 0.0f }, new float[] { 0.0f, });
            trainingData.Add(new float[] { 0, 0.1f }, new float[] { 0.1f, });
            trainingData.Add(new float[] { 0, 0.2f }, new float[] { 0.2f, });
            trainingData.Add(new float[] { 0, 0.3f }, new float[] { 0.3f, });
            trainingData.Add(new float[] { 0, 0.4f }, new float[] { 0.4f, });
            trainingData.Add(new float[] { 0, 0.5f }, new float[] { 0.5f, });
            trainingData.Add(new float[] { 0, 0.6f }, new float[] { 0.6f, });
            trainingData.Add(new float[] { 0, 0.7f }, new float[] { 0.7f, });
            trainingData.Add(new float[] { 0, 0.8f }, new float[] { 0.8f, });
            trainingData.Add(new float[] { 0, 0.9f }, new float[] { 0.9f, });
            trainingData.Add(new float[] { 0, 1.0f }, new float[] { 1.0f, });
            int length = trainingData.Count;
            for (int i = 0; i < 100000; i++)
            {
                var ctd = trainingData.ElementAt(RandomGenerator.GetInt(length));
                brain.Backpropagation(ctd.Key, ctd.Value);
            }
            WriteLine("Training end!");
            WriteLine();
            WriteLine("0.0");
            Get(0, 0.0f);
            WriteLine("0.2");
            Get(0, 0.2f);
            WriteLine("0.4");
            Get(0, 0.4f);
            WriteLine("0.6");
            Get(0, 0.6f);
            WriteLine("0.8");
            Get(0, 0.8f);
            

            void Get(float x0, float x1)
            {
                float[] output = brain.Feedforward(new float[] { x0, x1 });
                WriteLine(Math.Round(output[0], 1));
                WriteLine();
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

            m.Transpose();
            WriteLine(m.ToString());
            WriteLine();

            Matrix m2 = new Matrix(2, 3);
            m2.Randomize(10);
            Matrix hadamarProduct = Matrix.Multiply(m, m2);
            WriteLine(hadamarProduct.ToString());
            WriteLine();
        }
    }
}
