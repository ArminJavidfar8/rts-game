using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    [CreateAssetMenu(fileName = "SimpleStaticUnit", menuName = "rts-game/Unit/SimpleStatic")]
    public class SimpleStaticUnitData : BaseUnitData, IBaseUnitData
    {
        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _fireRate;

        public override float WeaponDamage => _weaponDamage;
        public override float FireRate => _fireRate;
        public override float Speed => 0;
    }
}