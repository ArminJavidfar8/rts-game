using Common;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.UserInteraction
{
    public class ClickHandler : MonoBehaviour, IServiceUser
    {
        private Camera _mainCamera;
        private int _rayDistance;

        private IEventService _eventSystem;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rayDistance = 200;

            SetDependencies();
        }

        public void SetDependencies()
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
            Physics.Raycast(ray, out RaycastHit hit, _rayDistance, LayerMask.GetMask(Constants.LayerNames.GROUND, Constants.LayerNames.UNIT), QueryTriggerInteraction.Collide);
            Collider hitCollider = hit.collider;
            if (hitCollider != null)
            {
                if (hitCollider.CompareTag(Constants.Tags.UNIT))
                {
                    _eventSystem.BroadcastEvent(EventTypes.OnUnitClicked, hitCollider.GetComponent<BaseUnit>());
                }
                else if (hitCollider.CompareTag(Constants.Tags.GROUND))
                {
                    _eventSystem.BroadcastEvent(EventTypes.OnGroundClicked, hit.point);
                }
            }
        }
    }
}