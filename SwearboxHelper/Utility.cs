using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SwearboxHelper
{
    class Utility
    {
        public static readonly string ForbiddenWords = "ForbiddenWords.txt";

        public static bool CheckForbiddenWords(string sentence)
        {
            foreach (var swear in File.ReadAllLines(ForbiddenWords).ToList())
                foreach (var word in sentence.Split(' '))
                    if (word == swear) return true;
            return false;
        }
    }
}
