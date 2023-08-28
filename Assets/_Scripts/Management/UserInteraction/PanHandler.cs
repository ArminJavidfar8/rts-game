using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managements.UserInteraction
{
    public class PanHandler : MonoBehaviour
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _leftSideThreshold;
        private int _rightSideThreshold;
        private int _upSideThreshold;
        private int _downSideThreshold;
        private float _mousePositionX;
        private float _mousePositionY;
        private Vector2 _lastPanValue;

        private IEventService _eventSystem;

        private void Awake()
        {
            SetDependencies();
        }

        private void Start()
        {
            SetThresholds();
        }

        private void SetThresholds()
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
            int edgeWidth = 50;

            _leftSideThreshold = edgeWidth;
            _rightSideThreshold = _screenWidth - edgeWidth;
            _downSideThreshold = edgeWidth;
            _upSideThreshold = _screenHeight - edgeWidth;
        }

        void Update()
        {
            CheckMouseToPan();
        }

        public void SetDependencies()
        {
            _eventSystem = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void CheckMouseToPan()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            _mousePositionX = mousePosition.x;
            _mousePositionY = mousePosition.y;
            Vector2 panValue = Vector2.zero;
            if (_mousePositionX > 0 && _mousePositionX < _leftSideThreshold)
            {
                panValue.x = -1;
            }
            else if (_mousePositionX > _rightSideThreshold && _mousePositionX < _screenWidth)
            {
                panValue.x = 1;
            }
            if (_mousePositionY > 0 && _mousePositionY < _downSideThreshold)
            {
                panValue.y = -1;
            }
            else if (_mousePositionY > _upSideThreshold && _mousePositionY < _screenHeight)
            {
                panValue.y = 1;
            }
            if (panValue != _lastPanValue)
            {
                _lastPanValue = panValue;
                _eventSystem.BroadcastEvent(EventTypes.OnMousePanned, panValue);
            }
        }

    }
}