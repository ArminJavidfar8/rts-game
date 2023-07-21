using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Skill
{
    [CreateAssetMenu(fileName = "HeavyShotToNearest", menuName = "rts-game/Skills/HeavyShotToNearest")]
    public class HeavyShotToNearestSkill : BaseSkill
    {
        [SerializeField] private int _weaponDamage;
        [SerializeField] private int _range;

        public override void Execute(BaseUnit skillUser)
        {
            BaseUnit target = skillUser.GetNearestTarget(_range);
            if (target != null)
            {
                Debug.Log("HeavyShotToNearest Used");
                target.TakeDamage(_weaponDamage);
            }
        }
    }
}