using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reature.Perceptrons;
using Reature.Mathematics;

namespace Reature.NeuralNetworks
{
    /// <summary>
    /// Klasa reprezentująca prostą sieć neuronową składającą się z 3 warstw (inputy, 1 warstwa ukryta, outputy).
    /// </summary>
    public class SimpleNeuralNetwork
    {
        /// <summary>
        /// Tworzy nową 3 warstwową sieć neuronową z określonym rozmiarem każdej warstwy.
        /// </summary>
        /// <param name="inputsLayerSize">Ilość inputów.</param>
        /// <param name="hiddenLayerSize">Ilość ukrytych neuronów.</param>
        /// <param name="outputsLayerSize">Ilość outputów.</param>
        public SimpleNeuralNetwork(int inputsLayerSize, int hiddenLayerSize, int outputsLayerSize)
        {
            InputsLayerSize = inputsLayerSize;
            HiddenLayerSize = hiddenLayerSize;
            OutputsLayerSize = outputsLayerSize;
            HiddenLayerActivationFunction = ActivationFunctions.Sigmoid;
            OutputLayerActivationFunction = ActivationFunctions.Sigmoid;
            HiddenLayerDerivativeFunction = ActivationFunctions.Derivatives.Sigmoid;
            OutputLayerDerivativeFunction = ActivationFunctions.Derivatives.Sigmoid;

            weightsIh = new Matrix(HiddenLayerSize, InputsLayerSize);
            weightsHo = new Matrix(OutputsLayerSize, HiddenLayerSize);
            weightsIh.Randomize(-1, 1);
            weightsHo.Randomize(-1, 1);
            biasH = new Matrix(HiddenLayerSize, 1);
            biasO = new Matrix(OutputsLayerSize, 1);
            biasH.Randomize(-1, 1);
            biasO.Randomize(-1, 1);
        }
        /// <summary>
        /// Tworzy nową 3 warstwową sieć neuronową z określonym rozmiarem każdej warstwy i określonymi funkcjami aktywacyjnymi.
        /// </summary>
        /// <param name="inputsLayerSize">Ilość inputów.</param>
        /// <param name="hiddenLayerSize">Ilość ukrytych neuronów.</param>
        /// <param name="outputsLayerSize">Ilość outputów.</param>
        /// <param name="hiddenLayerActivationFunction">Funkcja aktywacyjna dla neuronów ukrytej warstwy.</param>
        /// <param name="outputLayerActivationFunction">Funkcja aktywacyjna dla neuronów warstwy outputów.</param>
        /// <param name="hiddenLayerDerivativeFunction">Pochodna dla funckcji aktywacyjnej warstwy ukrytej.</param>
        /// <param name="outputLayerDerivativeFunction">Pochodna dla funckcji aktywacyjnej warstwy outputów.</param>
        public SimpleNeuralNetwork(int inputsLayerSize, int hiddenLayerSize, int outputsLayerSize, ActivationFunctionHandler hiddenLayerActivationFunction, ActivationFunctionHandler outputLayerActivationFunction, FloatOperationHandler hiddenLayerDerivativeFunction, FloatOperationHandler outputLayerDerivativeFunction)
        {
            InputsLayerSize = inputsLayerSize;
            HiddenLayerSize = hiddenLayerSize;
            OutputsLayerSize = outputsLayerSize;
            HiddenLayerActivationFunction = hiddenLayerActivationFunction;
            OutputLayerActivationFunction = outputLayerActivationFunction;
            HiddenLayerDerivativeFunction = hiddenLayerDerivativeFunction;
            OutputLayerDerivativeFunction = outputLayerDerivativeFunction;

            weightsIh = new Matrix(HiddenLayerSize, InputsLayerSize);
            weightsHo = new Matrix(OutputsLayerSize, HiddenLayerSize);
            weightsIh.Randomize(-1, 1);
            weightsHo.Randomize(-1, 1);
            biasH = new Matrix(HiddenLayerSize, 1);
            biasO = new Matrix(OutputsLayerSize, 1);
            biasH.Randomize(-1, 1);
            biasO.Randomize(-1, 1);
        }

        /// <summary>
        /// Szybkość nauki sieci neuronowej.
        /// </summary>
        public float LearningRate { get; set; } = 0.01f;
        /// <summary>
        /// Ilość inputów.
        /// </summary>
        public int InputsLayerSize { get; private set; }
        /// <summary>
        /// Ilość ukrytych neuronów.
        /// </summary>
        public int HiddenLayerSize { get; private set; }
        /// <summary>
        /// Ilość outputów.
        /// </summary>
        public int OutputsLayerSize { get; private set; }
        /// <summary>
        /// Wskazuje na to czy ma być dodawany bias.
        /// </summary>
        public bool AddBias { get; set; } = true;
        /// <summary>
        /// Funkcja aktywacyjna dla ukrytej warstwy. Domyślnie ustawiona na Sigmoid.
        /// </summary>
        public ActivationFunctionHandler HiddenLayerActivationFunction { get; private set; }
        /// <summary>
        /// Funkcja aktywacyjna dla warstwy outputów. Domyślnie ustawiona na Sigmoid.
        /// </summary>
        public ActivationFunctionHandler OutputLayerActivationFunction { get; private set; }
        /// <summary>
        /// Pochodna dla funckcji aktywacyjnej warstwy ukrytej.
        /// </summary>
        public FloatOperationHandler HiddenLayerDerivativeFunction { get; private set; }
        /// <summary>
        /// Pochodna dla funckcji aktywacyjnej warstwy outputów.
        /// </summary>
        public FloatOperationHandler OutputLayerDerivativeFunction { get; private set; }

        private Matrix weightsIh;
        private Matrix weightsHo;
        private Matrix biasH;
        private Matrix biasO;

        /// <summary>
        /// Wykonuje algorytm feedforwardu dla sieci neuronowej czyli sieć dostaje inputy i zwraca output taki jaki myśli że by był.
        /// </summary>
        /// <param name="inputs">Inputy dla sieci neuronowej.</param>
        /// <returns>Zwraca output jaki sieć neuronowa myśli że by był dla podanych inputów.</returns>
        public float[] Feedforward(float[] inputs)
        {
            if (inputs.Length != InputsLayerSize)
            {
                throw new ArgumentException("Liczba inputów jest nieprawidłowa z liczbą inputów jaką przyjmuje sieć neuronowa.");
            }

            Matrix mInputs = Matrix.Transpose(Matrix.FromOneDimensionArray(inputs));

            Matrix hiddenOutput = Matrix.Multiply(weightsIh, mInputs);
            if (AddBias)
            {
                hiddenOutput.Add(biasH);
            }
            hiddenOutput.Foreach((f) => HiddenLayerActivationFunction.Invoke(f));

            Matrix outputOutput = Matrix.Multiply(weightsHo, hiddenOutput);
            if (AddBias)
            {
                outputOutput.Add(biasO);
            }
            outputOutput.Foreach((f) => OutputLayerActivationFunction.Invoke(f));

            return outputOutput.ToOneDimensionArray();
        }
        /// <summary>
        /// Wykonuje algorytm wstecznej propagacji błędów dla sieci neuronowej.
        /// </summary>
        /// <param name="inputs">Inputy.</param>
        /// <param name="answers">Poprawne outputy.</param>
        public void Backpropagation(float[] inputs, float[] answers)
        {
            if (inputs.Length != InputsLayerSize)
            {
                throw new ArgumentException("Liczba inputów jest nieprawidłowa z liczbą inputów jaką przyjmuje sieć neuronowa.");
            }
            if (answers.Length != OutputsLayerSize)
            {
                throw new ArgumentException("Liczba odpowiedzi jest nieprawidłowa z liczbą outputów sieci neuronowej.");
            }

            Matrix mInputs = Matrix.Transpose(Matrix.FromOneDimensionArray(inputs));
            Matrix mAnswers = Matrix.Transpose(Matrix.FromOneDimensionArray(answers));

            Matrix hiddenOutput = Matrix.Multiply(weightsIh, mInputs);
            if (AddBias)
            {
                hiddenOutput.Add(biasH);
            }
            hiddenOutput.Foreach((f) => HiddenLayerActivationFunction.Invoke(f));

            Matrix outputOutput = Matrix.Multiply(weightsHo, hiddenOutput);
            if (AddBias)
            {
                outputOutput.Add(biasO);
            }
            outputOutput.Foreach((f) => OutputLayerActivationFunction.Invoke(f));

            Matrix outputError = Matrix.Subtract(mAnswers, outputOutput);
            Matrix tWeightsHo = Matrix.Transpose(weightsHo);
            Matrix hiddenError = Matrix.Multiply(tWeightsHo, outputError);

            Matrix gradientOutput = Matrix.Foreach(outputOutput, OutputLayerDerivativeFunction);
            gradientOutput.Multiply(outputError);
            gradientOutput.Multiply(LearningRate);

            Matrix gradientHidden = Matrix.Foreach(hiddenOutput, HiddenLayerDerivativeFunction);
            gradientHidden.Multiply(hiddenError);
            gradientHidden.Multiply(LearningRate);

            Matrix tHiddenOutput = Matrix.Transpose(hiddenOutput);
            Matrix deltaWeightsHo = Matrix.Multiply(gradientOutput, tHiddenOutput);
            weightsHo.Add(deltaWeightsHo);

            Matrix tInputs = Matrix.Transpose(mInputs);
            Matrix deltaWeightsIh = Matrix.Multiply(gradientHidden, tInputs);
            weightsIh.Add(deltaWeightsIh);
        }
    }
}
