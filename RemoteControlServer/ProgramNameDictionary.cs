using System;
using System.Collections.Generic;

namespace RemoteControlServer
{
    class ProgramNameDictionary 
    {
        private String[] notepads = { "блокнот", "notepad" };
        private String[] browserIE = { "браузер", "explorer" };
        private String[] browserChrome = { "chrome", "хром" };
        private String[] words = { "ворд", "word" };
        private Dictionary<String, String[]> dictionary = new Dictionary<string, string[]>();

        public void initialDictionary()
        {
            dictionary.Add("notepad", notepads);
            dictionary.Add("iexplore", browserIE);
            dictionary.Add("winword", words);
            dictionary.Add("chrome", browserChrome);
        }

        public String FindProgramName(String program)
        {
            String programName = "";
            foreach(KeyValuePair<String, String[]> kvp in dictionary)
            {
                foreach(String name in kvp.Value)
                {
                    if(program.Equals(name))
                    {
                        programName = kvp.Key;
                    }
                }
            }
            return programName;
        }
    }
}
