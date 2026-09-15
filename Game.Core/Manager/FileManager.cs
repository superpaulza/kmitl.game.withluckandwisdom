using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace withLuckAndWisdomProject
{
    public class FileManager
    {
        // Writable on every platform: the current directory is NOT writable
        // on Android ("/") or iOS (app bundle). Personal maps to the
        // app-private files dir on Android, Documents on iOS, and the
        // user's Documents folder on desktop.
        private static readonly string saveToDir =
            Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            // Settings persists public fields, not properties.
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
        };

        private static string GetPath(string fileName)
        {
            return Path.Combine(saveToDir, fileName);
        }

        public static bool Exists(string fileName)
        {
            try
            {
                return File.Exists(GetPath(fileName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while checking save file: " + ex.Message);
                return false;
            }
        }

        public static T ReadFromJson<T>(string fileName) where T : class
        {
            try
            {
                string path = GetPath(fileName);
                if (!File.Exists(path))
                    return null;
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(json, jsonOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while loading save file: " + ex.Message);
                return null;
            }
        }

        public static void WriteToJson<T>(string fileName, T obj)
        {
            try
            {
                Directory.CreateDirectory(saveToDir);
                string json = JsonSerializer.Serialize(obj, jsonOptions);
                File.WriteAllText(GetPath(fileName), json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while saving file: " + ex.Message);
            }
        }
    }
}
