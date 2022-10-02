using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class ObstacleVariant : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private int age;

        public int GetAge()
        {
            return this.age;
        }

        public int SetAge(int age)
        {
            return this.age;
        }

        public override string ToString() => $"ObstacleVaraint ()";
    }
}
