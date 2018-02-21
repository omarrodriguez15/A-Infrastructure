using System;
using System.IO;
using Newtonsoft.Json;

namespace AInfrastructure
{
    public interface IStorageIo
    {
        T GetObject<T>(string filePath);
        bool WriteObject<T>(string filePath, T obj);
    }

    public class StorageIo : IStorageIo
    {
        protected readonly ILogger _logger;

        protected JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Objects
        };


        public StorageIo()
        {
        }

        public StorageIo(ILogger logger)
        {
            _logger = logger;
        }

        public T GetObject<T>(string filePath)
        {
            var defaultObj = default(T);

            if (!File.Exists(filePath))
            {
                return defaultObj;
            }

            var fileContent = File.ReadAllText(filePath);

            if(string.IsNullOrEmpty(fileContent))
            {
                return defaultObj; 
            }

            return JsonConvert.DeserializeObject<T>(fileContent, _serializerSettings);
        }

        public bool WriteObject<T>(string filePath, T obj)
        {
            var result = false;

            try
            {
                var objStr = JsonConvert.SerializeObject(obj, _serializerSettings);
                File.WriteAllText(filePath, objStr);
                result = true;
            }
            catch (IOException ex)
            {
                _logger?.LogException(ex, $"FilePath: {filePath}");
            }

            return result;
        }
    }
}
