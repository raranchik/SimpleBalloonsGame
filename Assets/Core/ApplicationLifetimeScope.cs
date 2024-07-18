using Core.Balloons;
using Core.Balloons.Click;
using Core.Balloons.Field;
using Core.Balloons.Generator;
using Core.Balloons.Kinds;
using Core.Balloons.Kinds.Colored;
using Core.Balloons.Move;
using Core.Balloons.Presets;
using Core.Base.CoroutineRunner;
using Core.Base.Factory;
using Core.Base.Map;
using Core.Base.Pool;
using Core.Base.Random;
using Core.Base.Save;
using Core.Camera;
using Core.Input.Base;
using Core.Input.Base.Click;
using Core.Player;
using Core.Score;
using Core.Ui.Samples;
using UnityEngine;
using VContainer;
using VContainer.Unity;

#if UNITY_EDITOR || UNITY_WEBGL
using Core.Input.Desktop;
#endif

#if UNITY_ANDROID || UNITY_IOS
using Core.Input.Touch;
#endif

namespace Core
{
    public class ApplicationLifetimeScope : LifetimeScope
    {
        [SerializeField] private BalloonPresetBase[] m_BalloonPresets;
        [SerializeField] private ColoredBalloonView m_ColoredBalloonPrefab;
        [SerializeField] private PlayerScorePanelView m_PlayerScorePanelPrefab;
        [SerializeField] private PlayerScorePanelView m_CurrentPlayer;
        [SerializeField] private BestPlayerScorePanelView m_BestPlayer;
        [SerializeField] private MainMenuWindow m_MainMenu;
        [SerializeField] private CreateNewPlayerPanel m_CreateNewPlayer;
        [SerializeField] private LeaderboardWindow m_Leaderboard;
        [SerializeField] private GameScorePanelView m_GameScore;
        [SerializeField] private ExitMainMenuButton m_ExitMainMenu;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterRoutineRunner(builder);
            RegisterRandom(builder);
            RegisterMainCamera(builder);
            RegisterField(builder);
            RegisterInput(builder);
            RegisterBalloons(builder);
            RegisterScore(builder);
            RegisterUi(builder);
            builder.Register<SaveHelper>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayersController>()
                .AsSelf();
            base.Configure(builder);
        }

        private void RegisterUi(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_CurrentPlayer);
            builder.RegisterComponent(m_BestPlayer);
            builder.RegisterComponent(m_MainMenu);
            builder.RegisterComponent(m_CreateNewPlayer);
            builder.RegisterComponent(m_Leaderboard);
            builder.RegisterComponent(m_GameScore);
            builder.RegisterComponent(m_ExitMainMenu);
            builder.RegisterEntryPoint<UiController>()
                .WithParameter("prefab", m_PlayerScorePanelPrefab);
        }

        private void RegisterScore(IContainerBuilder builder)
        {
            builder.Register<ScoreController>(Lifetime.Singleton);
        }

        private void RegisterInput(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<UiInputHelper>()
                .AsSelf();
            builder.RegisterEntryPoint<InputsRunner>();
            RegisterClickInput(builder);
        }

        private void RegisterClickInput(IContainerBuilder builder)
        {
            builder.Register<ClickInputNotifier>(Lifetime.Singleton)
                .As<IClickInputNotifier>()
                .AsSelf();
#if UNITY_ANDROID || UNITY_IOS
            builder.Register<OneTouchInputHandler>(Lifetime.Singleton)
                .As<IInputHandler>()
                .AsSelf();
#endif
#if UNITY_EDITOR || UNITY_WEBGL
            builder.Register<OneClickInputHandler>(Lifetime.Singleton)
                .As<IInputHandler>()
                .AsSelf();
#endif
        }

        private void RegisterMainCamera(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainCameraMarker>();
            builder.Register<CameraHelper>(Lifetime.Singleton);
            builder.RegisterEntryPoint<MainCameraController>();
        }

        private void RegisterField(IContainerBuilder builder)
        {
            builder.Register<BalloonsFieldHelper>(Lifetime.Singleton);
            builder.RegisterEntryPoint<BalloonsFieldBoundaryController>();
        }

        private void RegisterRoutineRunner(IContainerBuilder builder)
        {
            var runner = new PlainCoroutineRunner();
            builder.RegisterInstance(runner)
                .As<ICoroutineRunner>();
        }

        private void RegisterRandom(IContainerBuilder builder)
        {
            builder.Register<RandomDecorator>(Lifetime.Singleton);
        }

        private void RegisterBalloons(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BalloonsGeneratorContext>()
                .AsSelf();
            builder.RegisterEntryPoint<BalloonsGeneratorStrategiesMap>()
                .As<BaseMap<string, IBalloonsGeneratorStrategy>>();

            builder.Register<BalloonsClickBehavioursContext>(Lifetime.Singleton);

            builder.Register<BalloonsPresetsMap>(Lifetime.Singleton)
                .As<BaseMap<string, IBalloonPreset>>();
            builder.RegisterEntryPoint<BalloonsPresetsInitializer>()
                .WithParameter("presets", m_BalloonPresets);

            builder.Register<BalloonsAliveMap>(Lifetime.Singleton)
                .As<BaseMap<GameObject, IBalloon>>();

            builder.RegisterEntryPoint<BalloonsOnClickListener>()
                .AsSelf();

            RegisterBalloonsMovement(builder);
            RegisterColoredBalloons(builder);
        }

        private void RegisterBalloonsMovement(IContainerBuilder builder)
        {
            builder.Register<BalloonsSinMoveArgsMap>(Lifetime.Singleton)
                .As<BaseMap<IBalloon, BalloonSinMoveArgs>>();
            builder.RegisterEntryPoint<BalloonsSinMoveController>();
        }

        private void RegisterColoredBalloons(IContainerBuilder builder)
        {
            builder.Register<ColoredBalloonsMap>(Lifetime.Singleton)
                .As<BaseMap<GameObject, IColoredBalloon>>();

            builder.Register<ColoredBalloonsClickBehaviourStrategy>(Lifetime.Singleton)
                .As<IBalloonsClickBehaviorStrategy>()
                .AsSelf();

            builder.RegisterEntryPoint<ColoredBalloonsGeneratorStrategy>()
                .As<IBalloonsGeneratorStrategy>()
                .AsSelf();

            var factory =
                new SimplePrefabFactoryWithPostCreateAction<ColoredBalloonView>(m_ColoredBalloonPrefab,
                    OnCreateBalloon);
            var pool = new SimplePool<IColoredBalloon>(5, 5, factory.Create);
            builder.RegisterInstance(pool)
                .As<IPool<IColoredBalloon>>();
        }

        private void OnCreateBalloon(IBalloon balloon)
        {
            balloon.SetActive(false);
        }
    }
}