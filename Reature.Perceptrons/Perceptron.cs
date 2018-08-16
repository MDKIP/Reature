using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.NumbersGeneration;

namespace Reature.Perceptrons
{
    /// <summary>
    /// Klasa reprezentująca pojedynczy perceptron.
    /// </summary>
    public class Perceptron : IPerceptron
    {
        /// <summary>
        /// Tworzy nową instancję klasy Perceptron.
        /// </summary>
        /// <param name="inputsNumber">Liczba inputów jaką perceptron może przyjąć.</param>
        /// <param name="activationFunction">Funkcja aktywacyjna dla tego perceptrona.</param>
        /// <param name="addBias">Wskazuje na to czy bias ma być dodawany.</param>
        public Perceptron(byte inputsNumber, ActivationFunctionHandler activationFunction, bool addBias)
        {
            this.inputsNumber = inputsNumber;
            this.activationFunction = activationFunction;
            this.addBias = addBias;

            LearningRate = 0.001f;
            weights = new float[inputsNumber];
            for (byte i = 0; i < weights.Length; i++)
            {
                weights[i] = RandomGenerator.GetFloat(-1, 1);
            }
        }
        /// <summary>
        /// Tworzy nową instancję klasy Perceptron.
        /// </summary>
        /// <param name="inputsNumber">Liczba inputów jaką perceptron może przyjąć.</param>
        /// <param name="activationFunction">Funkcja aktywacyjna dla tego perceptrona.</param>
        /// <param name="addBias">Wskazuje na to czy bias ma być dodawany.</param>
        /// <param name="weights">Wagi jakie perceptron otrzyma na początku.</param>
        public Perceptron(byte inputsNumber, ActivationFunctionHandler activationFunction, bool addBias, float[] weights)
        {
            this.inputsNumber = inputsNumber;
            this.activationFunction = activationFunction;
            this.addBias = addBias;

            LearningRate = 0.001f;
            this.weights = weights;
        }

        

        private float[] weights;
        private byte inputsNumber;
        private bool addBias;
        private ActivationFunctionHandler activationFunction;

        protected void OnTrainigEnd(float[] inputs, float answer, float guess, float error)
        {
            TrainingEnd?.Invoke(inputs, answer, guess, error);
        }

        // Zaimplementowane z IPerceptron.
        /// <summary>
        /// Wywoływany gdy pojedynczy trening zostanie zakończony.
        /// </summary>
        public event TrainingEndHandler TrainingEnd;
        /// <summary>
        /// Szybkość uczenia się perceptronu. Nie może być za duża.
        /// </summary>
        public float LearningRate { get; set; }
        /// <summary>
        /// Prosi perceptron o zgadnięcie według jego wag.
        /// </summary>
        /// <param name="inputs">Inputy.</param>
        /// <returns>Zwraca liczbę zmiennoprzecinkową która jest odpowiedzią perceptrona.</returns>
        public float GetGuess(float[] inputs)
        {
            if (inputs.Length != inputsNumber)
            {
                throw new Exception("Liczba inputów była niezgodna z liczbą inputów jakie perceptron może przyjąć.");
            }

            float weightedSum = 0;
            for (int i = 0; i < inputsNumber; i++)
            {
                weightedSum += weights[i] * inputs[i];
            }
            if (addBias)
            {
                weightedSum += 1;
            }

            return activationFunction.Invoke(weightedSum);
        }
        /// <summary>
        /// Trenuje perceptron.
        /// </summary>
        /// <param name="inputs">Inputy do treningu.</param>
        /// <param name="answer">Poprawna odpowiedź dla inputów.</param>
        /// <returns>Wskazuje na to czy perceptron zgadł poprawną odpowiedź.</returns>
        public bool Train(float[] inputs, float answer)
        {
            if (inputs.Length != inputsNumber)
            {
                throw new Exception("Liczba inputów była niezgodna z liczbą inputów jakie perceptron może przyjąć.");
            }

            float guess = GetGuess(inputs);

            if (answer != guess)
            {
                float error = answer - guess;

                ModifyWeightsByError(error, inputs);

                OnTrainigEnd(inputs, answer, guess, error);
                return false;
            }
            else
            {
                OnTrainigEnd(inputs, answer, guess, 0);
                return true;
            }
        }
        /// <summary>
        /// Modyfikuje wagi biorąc pod uwagę błąd. 
        /// </summary>
        /// <param name="error">Błąd.</param>
        /// <param name="inputs">Inputy które zostały użyte do wygenerowania błędu.</param>
        /// <returns>Zwraca zmiany jakie nastąpiły w wagach.</returns>
        public float[] ModifyWeightsByError(float error, float[] inputs)
        {
            if (inputs.Length != inputsNumber)
            {
                throw new Exception("Liczba inputów była niezgodna z liczbą inputów jakie perceptron może przyjąć.");
            }

            float[] weightsChange = new float[inputsNumber];

            for (byte i = 0; i < inputsNumber; i++)
            {
                weightsChange[i] = inputs[i] * error * LearningRate;
                weights[i] += weightsChange[i];
            }

            return weightsChange;
        }
        /// <summary>
        /// Rozpoczyna automatyczny trening.
        /// </summary>
        /// <param name="trainingData">Dane do treningu w formacie dictionary. Klucz to inputy, wartość to odpowiedź dla inputów.</param>
        /// <param name="iterations">Liczba małych treningów jakie ma wykonać perceptron.</param>
        /// /// <param name="saveOrder">Wskazuje na to czy dane do treningu mają zachować swoją kolejność podczas treningu czy mają być losowo użyte.</param>
        public void StartAutomaticTraining(Dictionary<float[], float> trainingData, int iterations, bool saveOrder = false)
        {
            int lengthOfTrainingData = trainingData.Count;

            for (int i = 0; i < iterations; i++)
            {
                if (saveOrder && iterations >= lengthOfTrainingData)
                {
                    break;
                }

                KeyValuePair<float[], float> currentTrainingData;
                if (saveOrder)
                {
                    currentTrainingData = trainingData.ElementAt(i);
                }
                else
                {
                    currentTrainingData = trainingData.ElementAt(RandomGenerator.GetInt(lengthOfTrainingData-1));
                }

                Train(currentTrainingData.Key, currentTrainingData.Value);
            }
        }
    }
}
