using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace UmamusumeSkillOCR
{
    

    class Config
    {
        private static readonly string configPath = @"config.json";

        public static readonly string[] availableLanguages = { "Korean", "English" };
        public static readonly string defaultLanguage = "English";
        public static readonly int defaultScreenX = 10;
        public static readonly int defaultScreenY = 344;
        public static readonly int defaultScreenWidth = 445;
        public static readonly int defaultScreenHeight = 440;
        public static readonly string defaultGameTitle = "BlueStacks";
        public string language { get; set; }
        public int screenX { get; set; }
        public int screenY { get; set; }
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }
        public string gameTitle { get; set; }

        public static Config loadProgramConfig()
        {
            Config loadProgramConfig;
            if (File.Exists(configPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                loadProgramConfig = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
            }
            else
            {
                loadProgramConfig = new Config()
                {
                    language = defaultLanguage,
                    screenX = defaultScreenX,
                    screenY = defaultScreenY,
                    screenWidth = defaultScreenWidth,
                    screenHeight = defaultScreenHeight,
                    gameTitle = defaultGameTitle
                };
                File.WriteAllText(configPath, JsonConvert.SerializeObject(loadProgramConfig));
            }

            return loadProgramConfig;
        }

        public void saveProgramConfig()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(this));
        }
    }
}
