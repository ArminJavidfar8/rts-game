using Data.Unit;
using DG.Tweening;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IPoolable, IDamageable, IShooter, IMoveable, IServiceUser
    {
        [SerializeField] private GameObject _selectedIndicator;

        private BaseUnitData _unitData;
        private float _maxHealth;
        private float _health;
        private float _fireRange;
        private float _speed;
        private float _rotationSpeed;

        private bool _isSelected;
        private Tween _moveTween;
        private Tween _rotateTween;
        private IEventService _eventService;

        public string Name => name;
        private float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        private float Health { get => _health; set => _health = value; }
        private float FireRange { get => _fireRange; set => _fireRange = value; }
        private float Speed { get => _speed; set => _speed = value; }
        private float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }
        private bool IsSelected { get => _isSelected; set { _isSelected = value; _selectedIndicator.SetActive(value); } }

        public void Initialize()
        {
            SetDependencies();
        }

        public void SetData(BaseUnitData unitData)
        {
            _unitData = unitData;
            ResetData();
        }

        public void OnGetFromPool()
        {
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitClicked, UnitClicked);
            _eventService.RegisterEvent<Vector3>(EventTypes.OnGroundClicked, GroundClicked);
        }

        public void OnReleaseToPool()
        {
            // ResetData should actually be in OnGetFromPool. But because in OnGetFromPool, SetData is not called yed, I put the reset code here.
            ResetData();

            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnUnitClicked, UnitClicked);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        public virtual void Die()
        {
            
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public void Shoot(IDamageable target, int damage)
        {
            target.TakeDamage(damage);
        }

        public IDamageable FindDamagable()
        {
            throw new System.NotImplementedException();
        }

        public void Move(Vector3 target, float speed)
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
            }
            _moveTween = transform.DOMove(target, speed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        public void Rotate(Vector3 targetPosition, float speed)
        {
            if (_rotateTween != null)
            {
                _rotateTween.Kill();
            }
            _rotateTween = transform.DOLookAt(targetPosition, speed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        private void ResetData()
        {
            MaxHealth = Health = _unitData.MaxHealth;
            FireRange = _unitData.FireRange;
            Speed = _unitData.Speed;
            RotationSpeed = _unitData.RotationSpeed;
        }

        private void UnitClicked(BaseUnit clickedUnit)
        {
            IsSelected = clickedUnit == this;
        }

        private void GroundClicked(Vector3 position)
        {
            if (IsSelected)
            {
                Rotate(position, RotationSpeed);
                Move(position, Speed);
            }
            IsSelected = false;
        }
    }
}