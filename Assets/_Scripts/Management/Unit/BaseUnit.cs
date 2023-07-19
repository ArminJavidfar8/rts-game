using Data.Unit;
using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public abstract class BaseUnit : MonoBehaviour, IDamageable, IShooter
    {
        private float _maxHealth;
        private float _health;
        private float _fireRange;

        public string Name => name;
        protected float MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
        protected float Health { get => _health; private set => _health = value; }
        protected float FireRange { get => _fireRange; private set => _fireRange = value; }

        public void Initialize(BaseUnitData unitData)
        {
            MaxHealth = Health = unitData.MaxHealth;
            FireRange = unitData.FireRange;
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
    }
}