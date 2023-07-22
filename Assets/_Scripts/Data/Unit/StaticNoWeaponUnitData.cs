using Services.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Unit
{
    [CreateAssetMenu(fileName = "StaticNoWeaponUnit", menuName = "rts-game/Unit/StaticNoWeapon")]
    public class StaticNoWeaponUnitData : BaseUnitData, IBaseUnitData
    {
        public override float Speed => 0;
        public override float WeaponDamage => 0;
        public override float FireRate => 0;
    }
}