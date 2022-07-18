/*
 * Load config files as the first step when the program is ran
 * Name of the config file must be config with extension json and placed in the 
 * same location as the executable file.
 * 
 * If there is no config file, the program will create a new one with default values.
 */

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
        public static readonly string defaultGameTitle = "umamusume";
        public static readonly int defaultTranslatorScreenX = 10;
        public static readonly int defaultTranslatorScreenY = 344;
        public static readonly int defaultTranslatorScreenWidth = 445;
        public static readonly int defaultTranslatorScreenHeight = 440;
        public static readonly int defaultTranslatorChoiceScreenX = 10;
        public static readonly int defaultTranslatorChoiceScreenY = 344;
        public static readonly int defaultTranslatorChoiceScreenWidth = 445;
        public static readonly int defaultTranslatorChoiceScreenHeight = 440;
        public static readonly bool defaultTranslatorChoiceMode = false;
        public static readonly bool defaultTranslatorMode = false;
        public static readonly string defaultTranslationApiKey = "";
        public string language { get; set; }
        public int screenX { get; set; }
        public int screenY { get; set; }
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }
        public string gameTitle { get; set; }
        public bool translatorMode { get; set; }
        public int translatorScreenX { get; set; }
        public int translatorScreenY { get; set; }
        public int translatorScreenWidth { get; set; }
        public int translatorScreenHeight { get; set; }
        public int translatorChoiceScreenX { get; set; }
        public int translatorChoiceScreenY { get; set; }
        public int translatorChoiceScreenWidth { get; set; }
        public int translatorChoiceScreenHeight { get; set; }
        public string translationApiKey { get; set; }
        public bool translatorChoiceMode { get; set; }

        /*
         * If there already is a config file, use it
         * If not, create a new config file with default values
         */
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
                    gameTitle = defaultGameTitle,
                    translatorMode = defaultTranslatorMode,
                    translatorScreenX = defaultTranslatorScreenX,
                    translatorScreenY = defaultTranslatorScreenY,
                    translatorScreenWidth = defaultTranslatorScreenWidth,
                    translatorScreenHeight = defaultTranslatorScreenHeight,
                    translatorChoiceScreenX = defaultTranslatorChoiceScreenX,
                    translatorChoiceScreenY = defaultTranslatorChoiceScreenY,
                    translatorChoiceScreenWidth = defaultTranslatorChoiceScreenWidth,
                    translatorChoiceScreenHeight = defaultTranslatorChoiceScreenHeight,
                    translationApiKey = defaultTranslationApiKey,
                    translatorChoiceMode = defaultTranslatorChoiceMode
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
