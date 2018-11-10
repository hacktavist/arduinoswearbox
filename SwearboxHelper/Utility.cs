using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SwearboxHelper
{
    public class Utility
    {
        public static readonly string ForbiddenWords = "ForbiddenWords.txt";
        public static int numberOfSwears;

        public static int CheckForbiddenWords(string sentence)
        {
            numberOfSwears = 0;
            foreach (var swear in File.ReadAllLines(ForbiddenWords).ToList())
                foreach (var word in sentence.Split(' '))
                    if (word == swear)
                    {
                        numberOfSwears++;
                    }
            return numberOfSwears;
        }
    }
}
