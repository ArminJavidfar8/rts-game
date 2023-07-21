using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.UserInteraction
{
    public class ZoomHandler : MonoBehaviour, IServiceUser
    {
        private IEventService _eventSystem;
        private int _threshold;
        private int _currentZoom;

        private void Start()
        {
            SetDependencies();
            _threshold = 15;
        }

        void Update()
        {
            CheckMouseToZoom();
        }

        public void SetDependencies()
        {
            _eventSystem = EventService.Instance;
        }

        private void CheckMouseToZoom()
        {
            float scrollVaue = Input.GetAxis("Mouse ScrollWheel");
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