using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class MathUtils
    {
        public static class Numbers
        {
            public static int GetRandom(int min, int max)
            {
                return Random.Range(min, max + 1);
            }
        }

        public static class NumbersWithDecimals
        {
            public static float GetRandom(float min, float max)
            {
                return Random.Range(min, max);
            }
        }
    }
}
