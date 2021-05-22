using System;

public class RvSequencesAndSeries
{
    public static double geometricSum(double firstTerm, double ratio, int numTerms)
    {
        if (ratio == 1)
        {
            return firstTerm * numTerms;
        }
        if (numTerms == 0)
        {
            return 0;
        }
        return firstTerm*(1-Math.Pow(ratio, numTerms))/(1-ratio);
    }
}