using Common;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Managements.UserInteraction
{
    public class ClickHandler : MonoBehaviour, IServiceUser
    {
        [SerializeField] private InputAction _rightClickAction;
        [SerializeField] private InputAction _leftClickAction;
        private Camera _mainCamera;
        private int _rayDistance;
        private int _layerMask;
        private IEventService _eventSystem;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rayDistance = 200;
            _layerMask = LayerMask.GetMask(Constants.LayerNames.GROUND, Constants.LayerNames.UNIT);

            SetDependencies();
        }
        private void OnEnable()
        {
            _rightClickAction.Enable();
            _rightClickAction.performed += RightClickActionPerformed;
            _leftClickAction.Enable();
            _leftClickAction.performed += LeftClickActionPerformed;
        }
        private void OnDisable()
        {
            _rightClickAction.Disable();
            _rightClickAction.performed -= RightClickActionPerformed;
            _leftClickAction.Disable();
            _leftClickAction.performed -= LeftClickActionPerformed;
        }

        public void SetDependencies()
        {
            _eventSystem = EventService.Instance;
        }

        private void RightClickActionPerformed(InputAction.CallbackContext context)
        {
            CheckClick(false, true);
        }

        private void LeftClickActionPerformed(InputAction.CallbackContext context)
        {
            CheckClick(true, false);
        }

        private void CheckClick(bool leftClicked, bool rightClicked)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if (!IsPointerIsOverUI(mousePosition))
            {
                Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
                Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _layerMask, QueryTriggerInteraction.Ignore);
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

        private bool IsPointerIsOverUI(Vector2 screenPos)
        {
            GameObject hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
            return hitObject != null && hitObject.layer == LayerMask.NameToLayer(Constants.LayerNames.UI);
        }

        private GameObject UIRaycast(PointerEventData pointerData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Count < 1 ? null : results[0].gameObject;
        }

        private PointerEventData ScreenPosToPointerData(Vector2 screenPos)
        {
            EventSystem e = EventSystem.current;
            PointerEventData p = new PointerEventData(e);
            p.position = screenPos;
            return p;
        }
    }
}