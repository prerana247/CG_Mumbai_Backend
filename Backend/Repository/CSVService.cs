using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Backend.Repository
{
    public class CSVService : ICSVService
    {

        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>();
            return records;
        }

        public void WriteCSV<T>(List<T> records)
        {
            using (var writer = new StreamWriter("C:\\Users\\ABC\\Desktop\\Demo\\Task.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

    }
}
