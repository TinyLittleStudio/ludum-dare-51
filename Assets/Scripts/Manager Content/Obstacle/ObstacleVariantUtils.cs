namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class ObstacleVariantUtils
    {
        public static bool IsAge(this ObstacleVariant obstacleVariant, int age)
        {
            if (obstacleVariant != null)
            {
                return obstacleVariant.GetAge() == age;
            }

            return false;
        }
    }
}
