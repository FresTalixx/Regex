using System.Text.RegularExpressions;

var regex = new Regex("(-\\d+\\.?\\d?)");

var str = "-42  0  007  123456  -9 DA -0.5  +3.14 -15 -999999 25 -1 13";

foreach (Match match in regex.Matches(str))
{
    if (match.Success)
    {
        Console.WriteLine(match.Groups[1].Value);
    }
}