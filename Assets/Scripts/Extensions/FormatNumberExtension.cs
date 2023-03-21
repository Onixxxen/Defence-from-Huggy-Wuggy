using UnityEngine;

public static class FormatNumberExtension
{
    private static string[] _formatsName = new[] { "", "K", "M", "T"};

    public static string FormatNumber(float number)
    {
        if (number == 0)
            return "0";

        int i = 0;
        while(i+1 < _formatsName.Length && number >= 1000)
        {
            number /= 1000f;
            i++;
        }

        return number.ToString("#.##" + _formatsName[i]);
    }
}
