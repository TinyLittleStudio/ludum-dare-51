using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class Manager
    {
        private static readonly Manager MANAGER = new Manager();

        private event ManagerUtils.OnTick OnTick;
        private event ManagerUtils.OnTickTime OnTickTime;

        private float tick;
        private float tickTime;

        private Manager()
        {
            this.tick = 0.0f;
            this.tickTime = 0.0f;
        }

        public void InvokeMenu()
        {
            Context = Context.MENU;

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.MENU)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT_MENU)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            SceneUtils.NewScene(IDs.SCENE_ID__MENU, (Scene a, Scene b) =>
            {
                Event.Fire(
                    new Event(IDs.EVENT_ID__SCENE_MENU)
                );
            });
        }

        public void InvokeGame()
        {
            Context = Context.GAME;

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT_GAME)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            SceneUtils.NewScene(IDs.SCENE_ID__GAME, (Scene a, Scene b) =>
            {
                Event.Fire(
                    new Event(IDs.EVENT_ID__SCENE_GAME)
                );
            });
        }

        public void Launch()
        {
            if (IsMenu())
            {
                return;
            }

            if (IsGame())
            {
                return;
            }

            InvokeMenu();
        }

        public void Update()
        {
            tickTime += Time.deltaTime;

            while (tickTime >= Settings.TICK_TIME)
            {
                tick++;

                tickTime -= Settings.TICK_TIME;

                if (tick % (1.0f / Settings.TICK_TIME) == 0)
                {
                    tick = 0;
                }

                if (OnTick != null)
                {
                    OnTick.Invoke((int)tick);
                }

                if (OnTickTime != null)
                {
                    OnTickTime.Invoke((int)tick, tickTime, Settings.TICK_TIME);
                }
            }
        }

        public void Inject(ManagerUtils.OnTick onTick)
        {
            if (OnTick == null || !OnTick.GetInvocationList().Contains(onTick))
            {
                OnTick += onTick;
            }
        }

        public void Deject(ManagerUtils.OnTick onTick)
        {
            if (OnTick != null && OnTick.GetInvocationList().Contains(onTick))
            {
                OnTick -= onTick;
            }
        }

        public void Inject(ManagerUtils.OnTickTime onTickTime)
        {
            if (OnTickTime == null || !OnTickTime.GetInvocationList().Contains(onTickTime))
            {
                OnTickTime += onTickTime;
            }
        }

        public void Deject(ManagerUtils.OnTickTime onTickTime)
        {
            if (OnTickTime != null && OnTickTime.GetInvocationList().Contains(onTickTime))
            {
                OnTickTime -= onTickTime;
            }
        }

        public bool IsMenu()
        {
            return Context == Context.MENU;
        }

        public bool IsGame()
        {
            return Context == Context.GAME;
        }

        public Context Context { get; private set; }

        public override string ToString() => $"Manager ()";

        public static Manager GetManager()
        {
            return Manager.MANAGER;
        }
    }
}
