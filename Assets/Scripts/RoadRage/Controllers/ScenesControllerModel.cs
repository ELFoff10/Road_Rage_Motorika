using NewScripts.Enums;
using RoadRage.StateMachines;
using RoadRage.Tools.UiManager;
using SpaceShooter.RoadRageMotorika.MultiScene;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace RoadRage.Controllers
{
    public class ScenesControllerModel : IInitializable
    {
        private ScenesStateEnum _scene = ScenesStateEnum.Base;
        [Inject] private readonly IMultiSceneManager _multiSceneManager;
        [Inject] private readonly ICoreStateMachine _coreStateMachine;
        [Inject] private readonly IWindowManager _windowManager;
        
        public void Initialize()
        {
            _coreStateMachine.ScenesState.SkipLatestValueOnSubscribe().Subscribe(CurrentSceneSwitches);
        }
        private void CurrentSceneSwitches(ScenesStateEnum scene)
        {
            _windowManager.Show<FadeWindow>(WindowPriority.LoadScene).CloseFade(EndCloseFade);
            _scene = scene;
        }

        private void EndCloseFade()
        {
            switch (_scene)
            {
                case ScenesStateEnum.Menu:
                    _multiSceneManager.LoadScene(_scene, NextSceneEndLoad);
                    break;
            }
        }
        
        private void EndOpenFade()
        {
            _coreStateMachine.OnSceneEndLoadFade();
        }
        
        private void NextSceneEndLoad()
        {
            _multiSceneManager.UnloadLastScene();
            _multiSceneManager.SetActiveLastLoadScene();
            _windowManager.Show<FadeWindow>().OpenFade(EndOpenFade);
        }
    }
}