using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Unit
{
    public class UnitUI : MonoBehaviour, IServiceUser
    {
        [SerializeField] private Image _healthbarFill;

        private Camera _mainCamera;
        private BaseUnit _unit;
        private IDamageable _damageable;
        private IEventService _eventService;

        private void Update()
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        public void Initialize(BaseUnit unit)
        {
            SetDependencies();
            _mainCamera = Camera.main;
            _unit = unit;
            _damageable = unit;
        }

        public void OnGetFromPool()
        {
            _eventService.RegisterEvent<IDamageable, float>(EventTypes.OnDamagableHealthChanged, DamagableHealthChanged);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitSelected, PlayerUnitSelected);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitDeselected, PlayerUnitDeselected);
        }

        public void OnReleaseToPool()
        {
            _eventService.UnRegisterEvent<IDamageable, float>(EventTypes.OnDamagableHealthChanged, DamagableHealthChanged);
            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitSelected, PlayerUnitSelected);
            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitDeselected, PlayerUnitDeselected);
        }

        private void DamagableHealthChanged(IDamageable damagable, float healthPercent)
        {
            if (_damageable == damagable)
            {
                gameObject.SetActive(true);
                _healthbarFill.fillAmount = healthPercent;
            }
        }

        private void PlayerUnitSelected(BaseUnit selectedUnit)
        {
            if (selectedUnit == _unit)
            {
                gameObject.SetActive(true);
            }
        }

        private void PlayerUnitDeselected(BaseUnit deselectedUnit)
        {
            if (deselectedUnit == _unit)
            {
                gameObject.SetActive(false);
            }
        }

    }
}