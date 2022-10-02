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

        private void OnScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            OnSceneHandle(scene, loadSceneMode);
        }

        private void OnSceneHandle(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene != null)
            {
                switch (scene.name)
                {
                    case IDs.SCENE_ID__LEVEL_001:
                    case IDs.SCENE_ID__LEVEL_002:
                    case IDs.SCENE_ID__LEVEL_003:

                        if (Profile != null)
                        {
                            Profile.ChargeReset();
                            Profile.Charge(0);

                            Profile.IsTutorial = true;
                        }

                        break;

                    case IDs.SCENE_ID__LEVEL_004:
                    case IDs.SCENE_ID__LEVEL_005:
                    case IDs.SCENE_ID__LEVEL_006:

                        if (Profile != null)
                        {
                            Profile.ChargeReset();
                            Profile.Charge(1);

                            Profile.IsTutorial = false;
                        }

                        break;

                    case IDs.SCENE_ID__LEVEL_007:
                    case IDs.SCENE_ID__LEVEL_008:

                        if (Profile != null)
                        {
                            Profile.ChargeReset();
                            Profile.Charge(0);

                            Profile.IsTutorial = false;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        public void InvokeMenu()
        {
            Context = Context.MENU;

            if (World.Current != null)
            {
                World.Current.InvokeDelete();
                World.Current = null;
            }

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

            if (World.Current == null)
            {
                World.Current = new World();
                World.Current.InvokeCreate();
            }

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

            if (Profile == null)
            {
                Profile = new Profile();
            }

            SceneUtils.Inject(OnScene);

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

        public Profile Profile { get; private set; }

        public override string ToString() => $"Manager ()";

        public static Manager GetManager()
        {
            return Manager.MANAGER;
        }
    }
}
