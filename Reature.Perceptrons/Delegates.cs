using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reature.Perceptrons
{
    public delegate float ActivationFunctionHandler(float weightedSum);
    public delegate void TrainingEndHandler(float[] inputs, float answer, float guess, float error);
}
