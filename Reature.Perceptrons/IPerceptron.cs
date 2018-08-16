using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.Perceptrons
{
    /// <summary>
    /// Interfejs reprezentujący pojedynczy perceptron.
    /// </summary>
    public interface IPerceptron
    {
        /// <summary>
        /// Wywoływany gdy pojedynczy trening zostanie zakończony.
        /// </summary>
        event TrainingEndHandler TrainingEnd;

        /// <summary>
        /// Szybkość uczenia się perceptronu. Nie może być za duża.
        /// </summary>
        float LearningRate { get; set; }

        /// <summary>
        /// Prosi perceptron o zgadnięcie według jego wag.
        /// </summary>
        /// <param name="inputs">Inputy.</param>
        /// <returns>Zwraca liczbę zmiennoprzecinkową która jest odpowiedzią perceptrona.</returns>
        float GetGuess(float[] inputs);
        /// <summary>
        /// Trenuje perceptron.
        /// </summary>
        /// <param name="inputs">Inputy do treningu.</param>
        /// <param name="answer">Poprawna odpowiedź dla inputów.</param>
        /// <returns>Wskazuje na to czy perceptron zgadł poprawną odpowiedź.</returns>
        bool Train(float[] inputs, float answer);
        /// <summary>
        /// Modyfikuje wagi biorąc pod uwagę błąd. 
        /// </summary>
        /// <param name="error">Błąd.</param>
        /// <param name="inputs">Inputy które zostały użyte do wygenerowania błędu.</param>
        /// <returns>Zwraca zmiany jakie nastąpiły w wagach.</returns>
        float[] ModifyWeightsByError(float error, float[] inputs);
        /// <summary>
        /// Rozpoczyna automatyczny trening.
        /// </summary>
        /// <param name="trainingData">Dane do treningu w formacie dictionary. Klucz to inputy, wartość to odpowiedź dla inputów.</param>
        /// <param name="iterations">Liczba małych treningów jakie ma wykonać perceptron.</param>
        /// <param name="saveOrder">Wskazuje na to czy dane do treningu mają zachować swoją kolejność podczas treningu czy mają być losowo użyte. W przypadku gdy dane mają zachować kolejność iteracje nie będą brane pod uwagę.</param>
        void StartAutomaticTraining(Dictionary<float[], float> trainingData, int iterations, bool saveOrder = false);
    }
}
