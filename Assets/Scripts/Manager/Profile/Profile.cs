using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class Profile
    {
        private const int MIN_BATTERY_CHARGES = 0;
        private const int MAX_BATTERY_CHARGES = 5;

        private int batteryCharges;

        public Profile()
        {
            this.batteryCharges = 0;
        }

        public void ChargeReset()
        {
            batteryCharges = 0;

            if (World.Current != null)
            {
                World.Current.Reset();
            }

            Charge(3);
        }

        public void Charge()
        {
            Charge(1);
        }

        public void Charge(int batteryCharges)
        {
            batteryCharges = batteryCharges + this.batteryCharges;

            batteryCharges = Mathf.Clamp(batteryCharges, Profile.MIN_BATTERY_CHARGES, Profile.MAX_BATTERY_CHARGES);

            Event.Fire(
                new Event(IDs.EVENT_ID__PROFILE_BATTERY_CHARGES)
                    .WithArgument<int>(IDs.EVENT_ARGUMENT_ID__BATTERY_CHARGES, batteryCharges)
            );

            if (batteryCharges <= 0)
            {
                Event.Fire(
                    new Event(IDs.EVENT_ID__PROFILE_GAME_OVER)
                );
            }

            this.batteryCharges = batteryCharges;
        }

        public bool IsGameOver()
        {
            return batteryCharges <= 0;
        }

        public override string ToString() => $"Profile ()";
    }
}
