using Data.Skill;
using System.Collections.Generic;

namespace Services.Abstraction
{
    public interface IBaseUnitData
    {
        public string Name { get; }
        public string PrefabName { get; }
        public float Speed { get; }
        public float RotationSpeed { get; }
        public int FireRange { get; }
        public float MaxHealth { get; }
        public float WeaponDamage { get; }
        public float FireRate { get; }
        public int KillingScore { get; }
        public IEnumerable<BaseSkill> Skills { get; }
    }
}