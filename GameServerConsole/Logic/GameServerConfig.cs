using System;
using System.IO;
using System.Text;
using GameServerConsole.Properties;
using Newtonsoft.Json.Linq;

namespace Chronobreak.GameServerConsole.Logic
{
    public class GameServerConfig
    {
        private string _defaultJson => Encoding.UTF8.GetString(Resources.GameServerSettings);
        public string ClientLocation { get; private set; } = "C:\\Chronobreak\\Chronobreak_Client";
        public bool AutoStartClient { get; private set; } = true;

        public static GameServerConfig LoadFromJson(string json)
        {
            GameServerConfig result = new();
            result.LoadConfig(json);
            return result;
        }

        public static GameServerConfig LoadFromFile(string path)
        {
            GameServerConfig result = new();
            if (File.Exists(path))
            {
                result.LoadConfig(File.ReadAllText(path));
            }
            return result;
        }

        public void Load(string json = "", string filePath = "")
        {
            if (!string.IsNullOrEmpty(json))
            {
                LoadConfig(json);
                return;
            }

            if (File.Exists(filePath))
            {
                LoadConfig(File.ReadAllText(filePath));
                return;
            }

            try
            {
                var settingsDirectory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(settingsDirectory))
                {
                    throw new Exception($"Creating Config File failed. Invalid Path: {filePath}");
                }

                Directory.CreateDirectory(settingsDirectory);
                File.WriteAllText(filePath, _defaultJson);
            }
            catch { }
        }

        public void LoadConfig(string json)
        {
            var data = JObject.Parse(json);
            AutoStartClient = (bool)data.SelectToken("autoStartClient");
            ClientLocation = (string)data.SelectToken("clientLocation");
        }
    }
}
