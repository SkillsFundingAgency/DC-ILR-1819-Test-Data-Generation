namespace DCT.TestDataGenerator
{
    using System;

    public class ListOfULNs
    {
        //        /*
        //For reference, here is the description of the algorithm as per the WBS25 ULN Generation document:
        //Calculate the check digit:
        //a. Form the sum
        //    10×firstdigit + 9x second digit + 8xthird digit + 7xfourth digit + 6xfifth digit + 5xsixth digit + 4xseventh digit + 3xeighth digit + 2×ninth digit,
        //    and find the remainder of this sum after division by 11. The remainder will be a number in the range 0 to 10.
        //    b. If the remainder is 0, reject the seed, select another seed and re-continue at Step a.
        //c. Otherwise, subtract the remainder from 10. The result will be in the range 0 to 9  and provides the check digit
        //d. Append the check digit to the 9 digit seed to create the 10 digit ULN.
        //         */
        public static long ULN(long index)
        {
            index += 99000000;
            string s = index.ToString();
            s = s.PadRight(9, '0');
            long result = 0;
            long multiplier = 10;
            for (int i = 0; i != s.Length; ++i)
            {
                result += multiplier-- * (s[i] - '0');
            }

            long mod11 = result % 11;
            if (mod11 == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            long check = 10 - mod11;
            s += check.ToString();

            return long.Parse(s);
        }
    }
}
