using Data.Skill;
using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    public abstract class BaseUnitData : ScriptableObject, IBaseUnitData
    {
        [SerializeField] private string _name;
        [SerializeField] private string _prefabName;
        [SerializeField] private float _maxHealth;
        [SerializeField] private int _killingScore;
        [SerializeField] private int _fireRange;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private BaseSkill[] _skills;

        public string Name => _name;
        public string PrefabName => _prefabName;
        public float RotationSpeed => _rotationSpeed;
        public int FireRange => _fireRange;
        public float MaxHealth => _maxHealth;
        public int KillingScore => _killingScore;
        public IEnumerable<BaseSkill> Skills => _skills;

        public abstract float Speed { get; }
        public abstract float WeaponDamage { get; }
        public abstract float FireRate { get; }
    }
}