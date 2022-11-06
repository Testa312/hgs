using System;
using System.Linq;
namespace HGS
{
    public static class Dtw
    {
        public static double GetScore(double[] arrayA, double[] arrayB)
        {
            var (aLength, bLength) = (arrayA.Length + 1, arrayB.Length + 1);

            var spanA = arrayA;
            var spanB = arrayB;

            double[][] dtw = new double[aLength][];
            dtw[0] = new double[bLength];

            double[] currentDtwRow;
            var previousDtwRow = dtw[0];

            for (var i = 1; i < aLength; i++)
            {
                currentDtwRow = new double[bLength];

                for (var j = 1; j < bLength; j++)
                {
                    var cost = Math.Abs(spanA[i - 1] - spanB[j - 1]);

                    double lastMin;
                    if (i == 1 && j == 1)
                        lastMin = previousDtwRow[j - 1];
                    else if (i == 1)
                        lastMin = currentDtwRow[j - 1];
                    else if (j == 1)
                        lastMin = previousDtwRow[j];
                    else
                        lastMin = Math.Min(previousDtwRow[j], Math.Min(currentDtwRow[j - 1], previousDtwRow[j - 1]));

                    currentDtwRow[j] = cost + lastMin;
                }
                dtw[i] = currentDtwRow;
                previousDtwRow = currentDtwRow;
            }

            return dtw[aLength - 1][bLength - 1];
        }

        public static float GetScoreF(float[] arrayA, float[] arrayB)
        {
            var (aLength, bLength) = (arrayA.Length + 1, arrayB.Length + 1);
            var spanA = arrayA;
            var spanB = arrayB;

            float[][] dtw = new float[aLength][];
            dtw[0] = new float[bLength];

            float[] currentDtwRow;
            var previousDtwRow = dtw[0];

            for (var i = 1; i < aLength; i++)
            {
                currentDtwRow = new float[bLength];

                for (var j = 1; j < bLength; j++)
                {
                    var cost = Math.Abs(spanA[i - 1] - spanB[j - 1]);

                    float lastMin;
                    if (i == 1 && j == 1)
                        lastMin = previousDtwRow[j - 1];
                    else if (i == 1)
                        lastMin = currentDtwRow[j - 1];
                    else if (j == 1)
                        lastMin = previousDtwRow[j];
                    else
                        lastMin = Math.Min(previousDtwRow[j], Math.Min(currentDtwRow[j - 1], previousDtwRow[j - 1]));

                    currentDtwRow[j] = cost + lastMin;
                }
                dtw[i] = currentDtwRow;
                previousDtwRow = currentDtwRow;
            }
            return dtw[aLength - 1][bLength - 1];
        }
    }
}
