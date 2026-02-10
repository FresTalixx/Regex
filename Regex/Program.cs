using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

var regex = new Regex("(-\\d+\\.?\\d?)");

var str = "-42  0  007  123456  -9 DA -0.5  +3.14 -15 -999999 25 -1 13";

foreach (Match match in regex.Matches(str))
{
    if (match.Success)
    {
        Console.WriteLine(match.Groups[1].Value);
    }
}


Console.WriteLine("Password checker");
var passRexex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

Console.WriteLine("Enter your pass:");
var enteredPass = Console.ReadLine();

foreach (Match match in passRexex.Matches(enteredPass))
{
    if (match.Success)
    {
        Console.WriteLine("Your pass is reliable!");
        break;
    }
}

Console.WriteLine("Your pass is NOT reliable!");

Console.WriteLine("TASK 3");

Console.WriteLine("Enter pay for day: ");
var PayForDay = Console.ReadLine();

Console.WriteLine("Enter amount of day: ");
var AmountOfDays = Console.ReadLine();

Console.WriteLine("Enter fine for pay delay for 1 day: ");
var FineForPayDelayOneDay = Console.ReadLine();

Console.WriteLine("Enter amount of fined delayed days: ");
var FineForPayDelayDays = Console.ReadLine();


var Paycheck = new Paycheck()
{
    AmountOfDays = int.Parse(AmountOfDays),
    PayForDay = int.Parse(PayForDay),
    FinePerDayForPayDelay = int.Parse(FineForPayDelayOneDay),
    DaysDelayToPay = int.Parse(FineForPayDelayDays),
};


Console.WriteLine("Select type of serialization (0 - XML, 1 - JSON, 2 - BINARY) or" +
    "deserelization (3 - XML, 4 - JSON, 5 - BINARY): ");
var choiceStr = Console.ReadLine();

int.TryParse(choiceStr, out int choice);

switch (choice)
{
    case 0: {
            Console.WriteLine("Enter filename with .xml extention: ");
            var filename = Console.ReadLine();

            var XMLserializer = new XmlSerializer(typeof(Paycheck));
            using var writer = new StreamWriter(filename);
            XMLserializer.Serialize(writer, Paycheck);
            break;
        }
    case 1:
        {
            Console.WriteLine("Enter filename with .json extention: ");
            var filename = Console.ReadLine();

            using var jsonFilestream = new FileStream(filename, FileMode.Create);
            JsonSerializer.Serialize(jsonFilestream, Paycheck);
            break;
        }
    case 2:
        {
            Console.WriteLine("Enter filename with .bin extention: ");
            var filename = Console.ReadLine();

            using var fs = new FileStream(filename, FileMode.Create);

            using var writer = new BinaryWriter(fs);

            writer.Write(Paycheck.PayForDay);
            writer.Write(Paycheck.AmountOfDays);
            writer.Write(Paycheck.FinePerDayForPayDelay);
            writer.Write(Paycheck.DaysDelayToPay);
            writer.Write(Paycheck.SumToPayNoFine);
            writer.Write(Paycheck.TotalFine);
            writer.Write(Paycheck.TotalSumToPay);

            break;

        }
    case 3:
    {
            Console.WriteLine("Enter filename with .xml extention: ");
            var filename = Console.ReadLine();

            var XMLserializer = new XmlSerializer(typeof(Paycheck));
            using var Reader = new StreamReader(filename);

            var content = (Paycheck)XMLserializer.Deserialize(Reader);

            Console.WriteLine($"Deserialized content of file ${filename}: \n");
            foreach (var prop in content.GetType().GetProperties())
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(content)}");
            }

            break;
    }

    case 4:
    {
            Console.WriteLine("Enter filename with .json extention: ");
            var filename = Console.ReadLine();

            using var reader = new FileStream(filename, FileMode.Open);
            var paycheck = JsonSerializer.Deserialize<Paycheck>(reader);

            foreach (var prop in paycheck.GetType().GetProperties() )
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(paycheck)}");
            }


            break;
    }
    case 5:
        {
            Console.WriteLine("Enter filename with .bin extention: ");
            var filename = Console.ReadLine();

            using var fs = new FileStream(filename, FileMode.Open);
            using var reader = new BinaryReader(fs);

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Console.WriteLine("PayForDay: " + reader.ReadInt32());
                Console.WriteLine("AmountOfDays: " + reader.ReadInt32());
                Console.WriteLine("FinePerDayForPayDelay: " + reader.ReadInt32());
                Console.WriteLine("DaysDelayToPay: " + reader.ReadInt32());
                Console.WriteLine("SumToPayNoFine: " + reader.ReadInt32());
                Console.WriteLine("TotalFine: " + reader.ReadInt32());
                Console.WriteLine("TotalSumToPay: " + reader.ReadInt32());
            }

            break;
        }
}
