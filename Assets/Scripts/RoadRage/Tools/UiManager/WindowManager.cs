using RoadRage.Tools.UiManager;
using UniRx;
using UnityEngine;

namespace Tools.UiManager
{
public class WindowManager : MonoBehaviour, IWindowManager
    {
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Transform _nonActiveParent;
        private AssetBundle _assetBundle;
        private IWindowFinder _finder;

        private WindowStack _stack;
        public Canvas MenuCanvas => _menuCanvas;
        public ReadOnlyReactiveProperty<Window> LastWindow => _stack.LastWindow;
        
        private void Awake()
        {
           // Debugger.Add(this);
         //   Debugger.Log("Awake");
            
            _finder = new WindowFinder(_nonActiveParent);
            _stack = new WindowStack(_menuCanvas.transform, _nonActiveParent);
        }
        public void ClearStack()
        {
        //    Debugger.Log("Clearing stack");
            WindowStack stack = _stack;
            
            stack.Clear();

            foreach (var window in _nonActiveParent.GetComponentsInChildren<Window>(true))
                if (!window.IsUndestroyable)
                    _finder.UnloadWindow(window);
        }
        
        public T FindWindow<T>() where T : Window
        {
            T window = _finder.FindWindow<T>();
            return window;
        }
        public T GetWindow<T>() where T : Window
        {
            T window = _finder.GetWindow<T>();
            window.Setup(this);
            return window;
        }
        public void First(Window window) => First(window, window.Priority);
        public void First(Window window, WindowPriority priority)
        {
      //      Debugger.Log($"First window in stack '{window.Name}'" );
            window.Priority = priority;
            _stack.First(window);
        }
        
        public void Show(Window window) => Show(window, window.Priority);
        
        public void Show(Window window, WindowPriority priority)
        {
            string wn = window.Name;
           // Debugger.Log($"Show window: '{wn}' with priority: '{priority}'");
            if (window.IsShowing)
            {
           //     Debugger.LogWarning($"Window '{wn}' is already showing");
                return;
            }
            window.Priority = priority;

            _stack.Add(window);
            
            var canvas = window.GetComponent<Canvas>();
            if (canvas != null)
                canvas.overrideSorting = false;
            
            window.Show();
        }
        
        public void Hide(Window window)
        {
            if (window == null) return;
            
       //     Debugger.Log($"Hide window '{window.Name}'");
            if (!window.IsShowing)
            {
           //     Debugger.LogWarning("Window '{0}' is already hidden", window.Name);
                return;
            }

            _stack.Remove(window);
            window.Hide();
        }
        public T Show<T>(WindowPriority? priority = null) where T : Window
        {
            T window = GetWindow<T>();
            Show(window, priority ?? window.Priority);
            return window;
        }
        
        public T First<T>(WindowPriority? priority = null) where T : Window
        {
            T window = GetWindow<T>();
            First(window, priority ?? window.Priority);
            return window;
        }
        
        public T Hide<T>() where T : Window
        { 
            T window = GetWindow<T>();
            Hide(window);
            return window;
        }
    }
}