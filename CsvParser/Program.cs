using System;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text;

namespace CsvParser
{
    class Program
    {
        static void Main(string[] args)
        {
            TextFieldParser[] files = new TextFieldParser[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                TextFieldParser csvParser = new TextFieldParser(args[i]);
                csvParser.TextFieldType = FieldType.Delimited;
                csvParser.SetDelimiters(",");

                files[i] = csvParser;
            }

            double buffer;
            double sum = 0;
            int averager = 0;

            foreach (TextFieldParser csvParser in files)
            {
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    foreach (string field in fields)
                    {
                        if (double.TryParse(field, out buffer))
                        {
                            sum += buffer;
                            averager++;
                        }
                    }
                }
            }
            

            Console.WriteLine($"Total sum: {sum}\nAverage num: {sum/averager}");          

            StringBuilder output = new StringBuilder();
            output.AppendLine("Total sum, Average num");
            output.Append(sum.ToString()).Append(", ").Append((sum/averager).ToString());

            File.WriteAllText(@"C:\Temp\Data.csv", output.ToString());
            Console.ReadKey();
        }
    }
}
