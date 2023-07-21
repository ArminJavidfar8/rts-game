using Common;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
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
            bool leftClicked = Input.GetMouseButtonDown(0);
            bool rightClicked = Input.GetMouseButtonDown(1);
            if (leftClicked || rightClicked)
            {
                CheckClick(leftClicked, rightClicked);
            }
        }

        private void CheckClick(bool leftClicked, bool rightClicked)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, _rayDistance, LayerMask.GetMask(Constants.LayerNames.GROUND, Constants.LayerNames.UNIT, Constants.LayerNames.UI), QueryTriggerInteraction.Ignore);
            Collider hitCollider = hit.collider;
            if (hitCollider != null)
            {
                if (hitCollider.CompareTag(Constants.Tags.PLAYER))
                {
                    EventTypes eventType = EventTypes.OnPlayerUnitLeftClicked;
                    if (rightClicked)
                    {
                        eventType = EventTypes.OnPlayerUnitRightClicked;
                    }
                    _eventSystem.BroadcastEvent(eventType, hitCollider.GetComponent<BaseUnit>());
                }
                else if (hitCollider.CompareTag(Constants.Tags.ENEMY))
                {
                    EventTypes eventType = EventTypes.OnEnemyUnitLeftClicked;
                    if (rightClicked)
                    {
                        eventType = EventTypes.OnEnemyUnitRightClicked;
                    }
                    _eventSystem.BroadcastEvent(eventType, hitCollider.GetComponent<BaseUnit>());
                }
                else if (hitCollider.CompareTag(Constants.Tags.GROUND))
                {
                    EventTypes eventType = EventTypes.OnGroundLeftClicked;
                    if (rightClicked)
                    {
                        eventType = EventTypes.OnGroundRightClicked;
                    }
                    _eventSystem.BroadcastEvent(eventType, hit.point);
                }
            }
        }
    }
}