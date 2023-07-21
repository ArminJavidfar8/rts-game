using Data.Skill;
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
        [SerializeField] private int _killingScore;
        [SerializeField] private float _fireRate;
        [SerializeField] private BaseSkill[] _skills;
        public string Name => _name;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public int FireRange => _fireRange;
        public float MaxHealth => _maxHealth;
        public float WeaponDamage => _weaponDamage;
        public float FireRate => _fireRate;
        public int KillingScore => _killingScore;
        public IEnumerable<BaseSkill> Skills => _skills;
    }
}