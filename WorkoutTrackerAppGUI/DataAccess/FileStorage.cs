using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WorkoutTrackerAppGUI.Entities;

namespace WorkoutTrackerAppGUI.DataAccess
{
    public class FileStorage
    {
        // Attributes
        private readonly string _storagePath;

        // Constructor
        public FileStorage(string storagePath)
        {
            if (string.IsNullOrWhiteSpace(storagePath))
                throw new ArgumentException("Storage path cannot be null or empty.", nameof(storagePath));

            _storagePath = storagePath;
            EnsureStoragePathExists();
        }

        // Methods

        // Save data to a file
        public void SaveToFile<T>(string fileName, List<T> data)
        {
            string filePath = Path.Combine(_storagePath, fileName);
            string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, jsonData);
        }

        // Load data from a file
        public List<T> LoadFromFile<T>(string fileName)
        {
            string filePath = Path.Combine(_storagePath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File '{fileName}' does not exist.");

            string jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        // Append a single entity to an existing file
        public void AppendToFile<T>(string fileName, T entity)
        {
            List<T> data = new List<T>();

            try
            {
                data = LoadFromFile<T>(fileName);
            }
            catch (FileNotFoundException)
            {
                // If the file doesn't exist, start with an empty list.
            }

            data.Add(entity);
            SaveToFile(fileName, data);
        }

        // Clear all data in a file
        public void ClearFile(string fileName)
        {
            string filePath = Path.Combine(_storagePath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Ensure storage path exists
        private void EnsureStoragePathExists()
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }
    }
}
