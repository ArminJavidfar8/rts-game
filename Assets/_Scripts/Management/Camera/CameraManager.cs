using DG.Tweening;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Cammera
{
    public class CameraManager : MonoBehaviour, IServiceUser
    {
        [SerializeField] private float _panSpeed;
        private Vector2 _panValue;
        private IEventService _eventSystem;
        private void Start()
        {
            SetDependencies();
            _eventSystem.RegisterEvent<Vector2>(EventTypes.OnMousePanned, MousePanned);
        }

        private void Update()
        {
            SetPosition();
        }

        public void SetDependencies()
        {
            _eventSystem = EventService.Instance;
        }

        private void SetPosition()
        {
            Vector3 position = transform.position;
            position.x += _panValue.x * _panSpeed * Time.deltaTime;
            position.z += _panValue.y * _panSpeed * Time.deltaTime;
            transform.position = position;
        }

        private void MousePanned(Vector2 panValue)
        {
            _panValue = panValue;
        }

    }
}