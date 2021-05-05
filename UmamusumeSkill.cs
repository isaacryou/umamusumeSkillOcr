using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace UmamusumeSkillOCR
{
    [XmlType("skill")]
    public class skillInfo
    {
        public string name { get; set; }
        public string trName { get; set; }
        public string trDescription { get; set; }
        public string trCondition { get; set; }
    }

    [XmlType("bannedTextList")]
    public class bannedTextInfo
    {
        public string bannedText { get; set; }
    }

    class UmamusumeSkill
    {
        private readonly string XMLPathSkillKr = @"Skill List\skillInfoKr.xml";
        private readonly string XMLPathSkillEn = @"Skill List\skillInfoEn.xml";
        private readonly string XMLPathBannedText = @"bannedText.xml";
        private Regex skillNameRegex = new Regex("[,!・′;・！.0〇 ]");

        public string[] languageList = { "Korean", "English" };

        public List<skillInfo> getAllSkills(Config programConfig)
        {
            List<skillInfo> infos;
            string xmlPath;

            if (programConfig.language == "English")
            {
                xmlPath = XMLPathSkillEn;
            }
            else
            {
                xmlPath = XMLPathSkillKr;
            }

            if (File.Exists(xmlPath))
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "skillList";

                XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<skillInfo>), xRoot);
                System.IO.StreamReader file = new System.IO.StreamReader(xmlPath);
                infos = (List<skillInfo>)reader.Deserialize(file);
                return infos;
            }

            return null;
        }

        public List<String> getAllBannedTexts()
        {
            List<String> bannedTexts = new List<String>();

            if (File.Exists(XMLPathBannedText))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPathBannedText);

                foreach(XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    bannedTexts.Add(node.InnerText);
                }

                return bannedTexts;
            }

            return null;
        }

        public string trimSkillName(String skillName)
        {
            skillNameRegex.Replace(skillName, "");

            return skillNameRegex.Replace(skillName, "");
        }

        public int computeSkillNameSimilarity(String ocrSkillName, String skillName)
        {
            int ocrSkillNameLength = ocrSkillName.Length;
            int skillNameLength = skillName.Length;
            int[,] d = new int[ocrSkillNameLength + 1, skillNameLength + 1];

            if (ocrSkillNameLength == 0)
            {
                return skillNameLength;
            } else if (skillNameLength == 0)
            {
                return ocrSkillNameLength;
            }

            for (int i = 0; i <= ocrSkillNameLength; d[i,0] = i++) { }
            for (int j = 0; j <= skillNameLength; d[0,j] = j++) { }

            for (int i = 1; i <= ocrSkillNameLength; i++)
            {
                for (int j = 1; j <= skillNameLength; j++)
                {
                    int cost = (skillName[j - 1] == ocrSkillName[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                            Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                            d[i - 1, j - 1] + cost
                        );
                }
            }

            return d[ocrSkillNameLength, skillNameLength];
        }

        public string getAllDetectedSkillList(List<String> extractedTextLines, List<skillInfo> allSkills, List<String> allBannedTexts)
        {
            string finalText = "";

            HashSet<int> skillIndexToShow = getAllDetectedSkillIndex(extractedTextLines, allSkills, allBannedTexts);

            skillInfo skillToShow;

            foreach (var skillIndex in skillIndexToShow)
            {
                skillToShow = allSkills[skillIndex];

                finalText += skillToShow.name + Environment.NewLine;
                finalText += skillToShow.trName + Environment.NewLine;
                finalText += skillToShow.trDescription + Environment.NewLine;
                finalText += skillToShow.trCondition + Environment.NewLine;
                finalText += Environment.NewLine;
            }

            return finalText;
        }

        public HashSet<int> getAllDetectedSkillIndex(List<String> extractedTextLines, List<skillInfo> allSkills, List<String> allBannedTexts)
        {
            List<int> indexToRemove = new List<int>();
            int extractedInt;
            String textLine;
            HashSet<int> skillIndexToShow = new HashSet<int>();
            HashSet<int> similarSkillsIndex = new HashSet<int>();
            int lowestScore = -1;
            int currentScore;
            int index = 0;
            int threshold;
            string trimmedTextLine;
            string trimmedSkillName;
            String finalExtractedTextLines = "";
            bool textRemoved = false;

            for (int i = 0; i < extractedTextLines.Count; i++)
            {
                textLine = extractedTextLines[i];

                if (int.TryParse(textLine, out extractedInt))
                {
                    indexToRemove.Add(i);
                    textRemoved = true;
                }

                if (!textRemoved)
                {
                    extractedTextLines[i] = trimSkillName(extractedTextLines[i]);
                    finalExtractedTextLines += extractedTextLines[i] + Environment.NewLine;
                }

                textRemoved = false;
            }

            if (indexToRemove.Count != 0)
            {
                for (int i = indexToRemove.Count - 1; i >= 0; i--)
                {
                    extractedTextLines.RemoveAt(indexToRemove[i]);
                }
            }

            foreach (var extractedTextLine in extractedTextLines)
            {
                trimmedTextLine = trimSkillName(extractedTextLine);

                if (allBannedTexts != null && allBannedTexts.Contains(trimmedTextLine))
                {
                    continue;
                }

                foreach (var skill in allSkills)
                {
                    trimmedSkillName = trimSkillName(skill.name);

                    threshold = (int)Math.Ceiling(trimmedSkillName.Length * 0.3);

                    currentScore = computeSkillNameSimilarity(trimmedTextLine, trimmedSkillName);

                    if (currentScore <= threshold)
                    {
                        if (lowestScore == -1 || lowestScore > currentScore)
                        {
                            similarSkillsIndex.Clear();
                            lowestScore = currentScore;
                        }

                        if (lowestScore == currentScore)
                        {
                            similarSkillsIndex.Add(index);
                        }

                        if (lowestScore == 0)
                        {
                            break;
                        }
                    }

                    index++;
                }

                if (similarSkillsIndex.Count > 0)
                {
                    skillIndexToShow.UnionWith(similarSkillsIndex);
                }

                lowestScore = -1;
                index = 0;
                similarSkillsIndex.Clear();
            }

            string finalString = "";
            skillInfo skillToShow;

            foreach (var skillIndex in skillIndexToShow)
            {
                skillToShow = allSkills[skillIndex];

                finalString += skillToShow.name + Environment.NewLine;
            }

            return skillIndexToShow;
        }
    }
}
