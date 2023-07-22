using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    [CreateAssetMenu(fileName = "SimpleUnit", menuName = "rts-game/Unit/Simple")]
    public class SimpleUnitData : BaseUnitData, IBaseUnitData
    {
        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _speed;
        [SerializeField] private float _fireRate;

        public override float Speed => _speed;
        public override float WeaponDamage => _weaponDamage;
        public override float FireRate => _fireRate;
    }
}