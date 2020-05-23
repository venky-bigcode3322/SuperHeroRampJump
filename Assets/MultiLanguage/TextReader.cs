using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class TextReader : MonoBehaviour
    {

        private TextAsset multiLanguageTextAsset;
        private string allLangCombinedText;
        private string[] splitTextsIntoLines;

        
        public  List<Language> allLanguages = new List<Language>();

        public static int numberOfLanguages;

        public string resourceFileName = "MultiLanguageData";

        public static TextReader Instance;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ReadTextAndSplitFromResources();
        }

        /// <summary>
        /// Reads the text and split from resources.
        /// </summary>
        void ReadTextAndSplitFromResources()
        {
            multiLanguageTextAsset = Resources.Load(resourceFileName) as TextAsset;
            allLangCombinedText = multiLanguageTextAsset.text;
            splitTextsIntoLines = allLangCombinedText.Split(new char[] { '\n' });
            CountLanguagesAndSetTexts();
        }

        /// <summary>
        /// Counts the number of languages and set the data to get.
        /// </summary>
        void CountLanguagesAndSetTexts()
        {
            string[] languageNames = splitTextsIntoLines[0].Split(new char[] { '\t' });
            numberOfLanguages = languageNames.Length - 1;
            for (int i = 0; i < numberOfLanguages; i++)
            {
                Language language = new Language(languageNames[i + 1]);
                allLanguages.Add(language);
            }

            string[] textDetails;
            for (int i = 1; i < splitTextsIntoLines.Length; i++)
            {
                textDetails = splitTextsIntoLines[i].Split(new char[] { '\t' });
                for (int j = 0; j < numberOfLanguages; j++)
                {
                    //print ("Got Key:"+textDetails[0]+" indx:"+j);

                    //print("this-------" + Application.loadedLevelName);

                    if (!allLanguages[j].keyValuePair.ContainsKey(textDetails[0]))
                    allLanguages[j].SetKeyValue(textDetails[0], textDetails[j + 1]);
                    
                }
            }
        }
    }

    [System.Serializable]
    public class Language
    {
        public string name;
        public Dictionary<string, string> keyValuePair = new Dictionary<string, string>();

        public Language(string languageName)
        {
            name = languageName.ToUpper().Trim();
        }

        public string ValueOfKey(string key)
        {
            if (keyValuePair.ContainsKey(key))
                return keyValuePair[key];
            return "";
        }

        public void SetKeyValue(string key, string value)
        {
            keyValuePair.Add(key, value);
        }
    }
}