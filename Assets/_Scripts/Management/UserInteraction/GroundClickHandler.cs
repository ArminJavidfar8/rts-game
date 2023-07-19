using Common;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.UserInteraction
{
    public class GroundClickHandler : MonoBehaviour, IServiceUser
    {
        private Camera _mainCamera;
        private int _rayDistance;
        private string _groundLayerName;

        private IEventService _eventSystem;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rayDistance = 200;
            _groundLayerName = Constants.LayerNames.GROUND;

            InjectDependencies();
        }

        public void InjectDependencies()
        {
            _eventSystem = EventService.Instance;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetClickPosition();
            }
        }

        private void GetClickPosition()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, _rayDistance, LayerMask.GetMask(Constants.LayerNames.GROUND), QueryTriggerInteraction.Ignore);
            if (hit.collider != null)
            {
                _eventSystem.BroadcastEvent(EventTypes.OnGroundClicked, hit.point);
            }
        }
    }
}