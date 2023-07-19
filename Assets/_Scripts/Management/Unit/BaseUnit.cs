using Data.Unit;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IDamageable, IShooter, IServiceUser
    {
        [SerializeField] private GameObject _selectedIndicator;
        private float _maxHealth;
        private float _health;
        private float _fireRange;

        private IEventService _eventService;

        public string Name => name;
        protected float MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
        protected float Health { get => _health; private set => _health = value; }
        protected float FireRange { get => _fireRange; private set => _fireRange = value; }

        public void Initialize(BaseUnitData unitData)
        {
            SetDependencies();

            MaxHealth = Health = unitData.MaxHealth;
            FireRange = unitData.FireRange;

            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitClicked, UnitClicked);
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

        private void UnitClicked(BaseUnit clickedUnit)
        {
            _selectedIndicator.SetActive(clickedUnit == this);
        }
    }
}