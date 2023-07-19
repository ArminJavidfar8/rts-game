using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    public class BaseUnitData : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _speed;
        [SerializeField] private int _fireRange;

        public float Speed => _speed;
        public int FireRange => _fireRange;
        public float MaxHealth => _maxHealth;
        public float WeaponDamage => _weaponDamage;
    }
}