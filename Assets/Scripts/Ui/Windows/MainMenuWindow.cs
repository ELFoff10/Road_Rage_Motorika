using Tools.UiManager;
using Ui.Common.Tools;
using UnityEngine;

namespace Ui.Windows
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private UiButton _playButton;
        [SerializeField] private UiButton _statusButton;
        [SerializeField] private UiButton _optionsButton;
        [SerializeField] private UiButton _quitButton;
        [SerializeField] private AudioSource _clickClip;

        protected override void OnActivate()
        {
            base.OnActivate();
            _playButton.OnClick += OnPlayButton;
            _statusButton.OnClick += OnStatusButton;
            _optionsButton.OnClick += OnOptionsButton;
            _quitButton.OnClick += OnQuitButton;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _playButton.OnClick -= OnPlayButton;
            _statusButton.OnClick -= OnStatusButton;
            _optionsButton.OnClick -= OnOptionsButton;
            _quitButton.OnClick -= OnQuitButton;
        }

        private void OnPlayButton()
        {
            
        }
        
        private void OnStatusButton()
        {
           // _manager.Show<>()
        }
        
        private void OnOptionsButton()
        {
            
        }
        
        private void OnQuitButton()
        {
            
        }

        private void PlayClip() => _clickClip.Play();
    }
}