using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Helper
{
    public static class GenerateRandomPasswordHelper
    {

        public static string GetPassword(int size=15)
        {
            string alphaCaps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alphaLow = "abcdefghijklmnopqrstuvwxyz";
            string numerics = "1234567890";
            string special = "@#$-=/";

            string allChars = alphaLow + special + numerics + alphaCaps;


            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i=0; i <= size; i++)
            {
                int num = random.Next(allChars.Length);
                builder.Append(allChars[num]);

            }
           
            return builder.ToString();

        }



    }
}
