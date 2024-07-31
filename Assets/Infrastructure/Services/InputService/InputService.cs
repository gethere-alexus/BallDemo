using UnityEngine;

namespace Infrastructure.Services.InputService
{
    public class InputService : MonoBehaviour, IInputService
    {
        private bool _test;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            InputModule = new InputModule();
            
        }

        private void OnEnable() => 
            InputModule.Enable();

        private void OnDisable() => 
            InputModule.Disable();

        public InputModule InputModule { get; private set; }
    }
}