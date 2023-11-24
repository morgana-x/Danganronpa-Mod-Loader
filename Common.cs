using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanganronpaAnotherModLoader
{
    internal class Common
    {
        public class AlphanumComparatorFast : IComparer
        {
            public int Compare(object x, object y)
            {
                string s1 = x as string;
                if (s1 == null)
                    return 0;

                string s2 = y as string;
                if (s2 == null)
                    return 0;

                int len1 = s1.Length, len2 = s2.Length;
                int marker1 = 0, marker2 = 0;

                // Walk through two the strings with two markers.
                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1], ch2 = s2[marker2];

                    // Some buffers we can build up characters in for each chunk.
                    char[] space1 = new char[len1], space2 = new char[len2];
                    int loc1 = 0, loc2 = 0;

                    // Walk through all following characters that are digits or
                    // characters in BOTH strings starting at the appropriate marker.
                    // Collect char arrays.
                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                            ch1 = s1[marker1];
                        else
                            break;

                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                            ch2 = s2[marker2];
                        else
                            break;

                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    // If we have collected numbers, compare them numerically.
                    // Otherwise, if we have strings, compare them alphabetically.
                    string str1 = new string(space1), str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                        result = str1.CompareTo(str2);


                    if (result != 0)
                        return result;

                }
                return len1 - len2;
            }
        }
        public static string[] CleanAddress(string[] sentence, string folder)
        {
            Array.Sort(sentence, new AlphanumComparatorFast());

            for (int i = 0; i < sentence.Length; i++)
            {
                sentence[i] = sentence[i].Replace(folder + "\\", null).Replace("\\", "/");
            }

            return sentence;
        }
        // Reads all the folders in the root (no files or subdirectories) and sorts them alphanumerically.
        public static string[] ReadOnlyRootDirs(string folder)
        {
            string[] temp = Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly);
            Array.Sort(temp, new AlphanumComparatorFast());
            return temp;
        }
    }
}
