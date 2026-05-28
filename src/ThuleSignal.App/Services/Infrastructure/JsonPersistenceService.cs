using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using ThuleSignal.App.Dto;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class JsonPersistenceService
    {
        private readonly JsonSerializerOptions _options;

        public JsonPersistenceService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };
        }

        public void SavePlaylist(PlaylistDto playlistDto, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(playlistDto, _options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"[JSON State] Стан плейлиста '{playlistDto.Name}' успішно серіалізовано у файл: {filePath}");
        }

        public PlaylistDto LoadPlaylist(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл стану не знайдено: {filePath}");

            string jsonString = File.ReadAllText(filePath);
            var dto = JsonSerializer.Deserialize<PlaylistDto>(jsonString, _options);
            
            if (dto == null) throw new InvalidOperationException("Не вдалося десеріалізувати стан програми.");
            
            Console.WriteLine($"[JSON State] Стан успішно зчитано з файлу на диску.");
            return dto;
        }
    }
}