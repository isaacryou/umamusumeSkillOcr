using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Google.Cloud.Translation.V2;

namespace UmamusumeSkillOCR
{
    class Translator
    {
        private TranslationClient translationClient;

        public Translator(string _apiKey)
        {
            translationClient = TranslationClient.CreateFromApiKey(_apiKey);
        }

        public string removeAllSpaceAndNewLine(string _textToEdit)
        {
            string result = Regex.Replace(_textToEdit, @"\s+|\n","");

            return result;
        }

        public string getTranslatedText(string _textToTranslate, string _languageToTranslate)
        {
            TranslationResult result;

            if (_languageToTranslate.Equals("Korean"))
            {
                result = translationClient.TranslateText(_textToTranslate, LanguageCodes.Korean);
            }
            else
            {
                result = translationClient.TranslateText(_textToTranslate, LanguageCodes.English);
            }

            return result.TranslatedText;
        }


        
    }
}
