using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    public class BaseUnitData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private int _fireRange;

        public string Name => _name;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public int FireRange => _fireRange;
        public float MaxHealth => _maxHealth;
        public float WeaponDamage => _weaponDamage;
    }
}