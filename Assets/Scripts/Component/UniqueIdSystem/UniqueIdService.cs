using UnityEngine;

namespace Component.UniqueIdSystem
{
    public static class UniqueIdService
    {
        public static double GetUniqueId()
        {
            int count = Random.Range(5, 15);
            double result = Random.Range(99.0f, 99999.0f);
            for (int i = 0; i < count; i++)
            {
                result += Random.Range(99.0f, 99999.0f);
            }

            return result;
        }
        
        public static int GetUniqueIdInt()
        {
            int count = Random.Range(5, 15);
            int result = Random.Range(1, 99999);
            for (int i = 0; i < count; i++)
            {
                result += Random.Range(1, 99999);
            }

            return result;
        }
    }
}