using Newtonsoft.Json;
using System.IO;

namespace GarthToland.Store.Services
{
    public interface IDataFileService
    {
        T GetObjectFromJsonFile<T>(string fileName) where T : new();
        void WriteObjectToFile<T>(string fileName, T items) where T : new();
    }

    public class DataFileService : IDataFileService
    {
        public T GetObjectFromJsonFile<T>(string fileName) where T : new()
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new T();
                }

                var items = JsonConvert.DeserializeObject<T>(json);

                return items;
            }
        }

        public void WriteObjectToFile<T>(string fileName, T obj) where T : new()
        {
            using (var fileStream = new FileStream(fileName, FileMode.Truncate))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                var json = JsonConvert.SerializeObject(obj);
                streamWriter.WriteLine(json);
            }
        }
    }
}
