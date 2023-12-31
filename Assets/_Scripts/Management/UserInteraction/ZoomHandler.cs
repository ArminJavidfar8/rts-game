using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managements.UserInteraction
{
    public class ZoomHandler : MonoBehaviour
    {
        [SerializeField] private InputAction _scrollAction;
        private IEventService _eventSystem;
        private int _threshold;
        private int _currentZoom;

        private void Awake()
        {
            SetDependencies();
        }

        private void Start()
        {
            SetDependencies();
            _threshold = 15;
        }

        private void OnEnable()
        {
            _scrollAction.Enable();
            _scrollAction.performed += ScrollActionPerformed;
        }

        private void OnDisable()
        {
            _scrollAction.Disable();
            _scrollAction.performed -= ScrollActionPerformed;
        }

        private void ScrollActionPerformed(InputAction.CallbackContext context)
        {
            CheckMouseToZoom();
        }

        public void SetDependencies()
        {
            _eventSystem = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void CheckMouseToZoom()
        {
            float scrollVaue = Mouse.current.scroll.ReadValue().y;
            int scroll = 0;
            if (scrollVaue > 0)
            {
                scroll = 1;
            }
            else if (scrollVaue < 0)
            {
                scroll = -1;
            }
            if (scroll != 0 && _currentZoom + scroll > -_threshold && _currentZoom + scroll < _threshold)
            {
                _currentZoom += scroll;
                _eventSystem.BroadcastEvent(EventTypes.OnMouseScrolled, scroll);
            }
        }
    }
}