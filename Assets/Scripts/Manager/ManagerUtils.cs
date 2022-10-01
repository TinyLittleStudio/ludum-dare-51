using System.IO;
using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class ManagerUtils
    {
        public delegate void OnTick(int tick);
        public delegate void OnTickTime(int tick, float tickTime, float tickTimeTotal);
    }
}
