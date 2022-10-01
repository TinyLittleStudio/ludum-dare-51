namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class World
    {
        public static World Current { get; set; }

        private const float DURATION = 10.0f;

        private float time;
        private float timeTotal;

        public float Time => time;
        public float TimeTotal => timeTotal;

        private int age;

        private Parallax parallax;

        public World()
        {
            this.time = 0.0f;
            this.timeTotal = World.DURATION;

            this.age = 0;
        }

        public void Reset()
        {
            this.time = 0.0f;
            this.timeTotal = World.DURATION;

            this.age = 0;

            if (parallax != null)
            {
                parallax.InvokeDelete();
                parallax = null;
            }

            if (parallax == null)
            {
                parallax = new Parallax();
                parallax.InvokeCreate();
            }
        }

        public void InvokeCreate()
        {
            Manager.GetManager().Inject(OnTick);
        }

        public void InvokeDelete()
        {
            Manager.GetManager().Deject(OnTick);
        }

        public void OnTick(int tick)
        {
            if (Player == null)
            {
                Player = PlayerUtils.Instantiate();
            }

            if (IsPaused)
            {
                return;
            }

            if (Manager.GetManager().Profile == null)
            {
                return;
            }

            if (Manager.GetManager().Profile.IsGameOver())
            {
                return;
            }

            time += Settings.TICK_TIME;

            if (time >= timeTotal)
            {
                if (Manager.GetManager().Profile != null)
                {
                    Manager.GetManager().Profile.Charge(-1);

                    Event.Fire(
                        new Event(IDs.EVENT_ID__WORLD_TURN_OFF)
                    );

                    SetAge(GetAge() + 1);

                    Watch.NewWatch(2.0f, (int tick, bool isFinished) =>
                    {
                        if (isFinished)
                        {
                            if (!Manager.GetManager().Profile.IsGameOver())
                            {
                                time = 0.0f;
                                timeTotal = World.DURATION;
                            }
                            else
                            {
                                SceneUtils.Reload();
                            }

                            Event.Fire(
                                new Event(IDs.EVENT_ID__WORLD_TURN_ON)
                            );

                            IsPaused = false;
                        }
                    });

                    IsPaused = true;
                }
            }
        }

        public int GetAge()
        {
            return this.age;
        }

        public int SetAge(int age)
        {
            Event.Fire(
                new Event(IDs.EVENT_ID__WORLD)
                    .WithArgument<int>(IDs.EVENT_ARGUMENT_ID__AGE, age)
            );

            return this.age = age;
        }

        public Parallax GetParallax()
        {
            return this.parallax;
        }

        public Player Player { get; private set; }

        public bool IsPaused { get; private set; }

        public override string ToString() => $"World ()";
    }
}
