using System.Collections.Generic;
using System.IO;

namespace Backend.Repository
{
    public interface ICSVService
    {
        IEnumerable<T> ReadCSV<T>(Stream file);
        void WriteCSV<T>(List<T> records);
    }
}