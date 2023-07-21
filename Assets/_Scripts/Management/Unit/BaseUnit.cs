using Common;
using Data.Skill;
using Data.Unit;
using DG.Tweening;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using Services.Core.EventSystem;
using Services.Core.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.Unit;
using UnityEngine;

namespace Managements.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IPoolable, IDamageable, IShooter, ITargetFinder, IServiceUser
    {
        [SerializeField] private GameObject _selectedIndicator;
        [SerializeField] private UnitUI _unitUI;

        private BaseUnitData _unitData;
        private float _health;
        private Tween _moveTween;
        private Tween _rotateTween;

        protected IEventService _eventService;
        protected IUnitService _unitService;

        public string Name => name;
        private float MaxHealth => _unitData.MaxHealth;
        public float Health 
        { 
            get => _health; 
            private set 
            { 
                _health = value;
                _eventService.BroadcastEvent(EventTypes.OnDamagableHealthChanged, this, Health / MaxHealth);
            } 
        }
        public float FireRate => _unitData.FireRate;
        public float FireRange => _unitData.FireRange;
        private float Speed => _unitData.Speed;
        private float RotationSpeed => _unitData.RotationSpeed;
        public float WeaponDamage => _unitData.WeaponDamage;

        public bool IsDead => Health <= 0;

        public IEnumerable<BaseSkill> Skills => _unitData.Skills;

        public void Initialize() 
        {
            _unitUI.Initialize(this);
            SetDependencies();
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
            _unitService = UnitService.Instance;
        }

        public virtual void SetData(BaseUnitData unitData)
        {
            _unitData = unitData;
            ResetData();
        }

        public virtual void OnGetFromPool()
        {
            _unitUI.OnGetFromPool();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitSelected, PlayerUnitSelected);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitDeselected, PlayerUnitDeselected);
        }

        public virtual void OnReleaseToPool()
        {
            _unitUI.OnReleaseToPool();
            // ResetData should actually be in OnGetFromPool. But because in OnGetFromPool, SetData is not called yed, I put ResetData here.
            ResetData();
            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitSelected, PlayerUnitSelected);
            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitDeselected, PlayerUnitDeselected);
        }

        public virtual void Die()
        {
            _eventService.BroadcastEvent(EventTypes.OnUnitDied, this);
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void SetTargetByUser(BaseUnit target) { }

        public virtual void RemoveTargetByUser(BaseUnit target) { }

        public void Shoot(IDamageable target, Vector3 targetPosition, float damage)
        {
            if (tag == Constants.Tags.ENEMY)
            {
                return;
            }
            transform.DOLookAt(targetPosition, RotationSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() => target.TakeDamage(damage));
        }

        public void Move(Vector3 target)
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
            }
            _moveTween = transform.DOMove(target, Speed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        public void Rotate(Vector3 targetPosition)
        {
            if (_rotateTween != null)
            {
                _rotateTween.Kill();
            }
            _rotateTween = transform.DOLookAt(targetPosition, RotationSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        private void PlayerUnitSelected(BaseUnit selectedUnit)
        {
            if (selectedUnit == this)
            {
                _selectedIndicator.SetActive(true);
            }
        }

        private void PlayerUnitDeselected(BaseUnit deselectedUnit)
        {
            if (deselectedUnit == this)
            {
                _selectedIndicator.SetActive(false);
            }
        }

        private void ResetData()
        {
            _selectedIndicator.SetActive(false);
            Health = MaxHealth;
        }

        public BaseUnit GetNearestTarget(int range)
        {
            string targetTag = tag == Constants.Tags.PLAYER ? Constants.Tags.ENEMY : Constants.Tags.PLAYER;
            return _unitService.GetNearestTarget(this, range, targetTag);
        }

        public List<BaseUnit> GetTargets(int range)
        {
            string targetTag = tag == Constants.Tags.PLAYER ? Constants.Tags.ENEMY : Constants.Tags.PLAYER;
            return _unitService.GetNearestTargets(this, range, targetTag);
        }
    }
}