using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using UnityEngine;

namespace Managements.Cammera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float _panSpeed;
        [SerializeField] private float _zoomSpeed;
        private Vector2 _panValue;
        private IEventService _eventService;

        private void Awake()
        {
            SetDependencies();
        }

        private void Start()
        {
            _eventService.RegisterEvent<Vector2>(EventTypes.OnMousePanned, MousePanned);
            _eventService.RegisterEvent<int>(EventTypes.OnMouseScrolled, MouseScrolled);
        }

        private void Update()
        {
            SetPosition();
        }

        public void SetDependencies()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
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

        private void MouseScrolled(int scrollValue)
        {
            transform.position += transform.forward * scrollValue * _zoomSpeed * Time.deltaTime;
        }
    }
}