using System;

namespace CoverScript.Simple
{
    public static class MathHelper
    {
        public static int GetBigger(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
}
