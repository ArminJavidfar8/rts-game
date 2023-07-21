using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Skill
{
    [CreateAssetMenu(fileName = "ShotToNearests", menuName = "rts-game/Skills/ShotToNearests")]
    public class ShotToNearestsSkill : BaseSkill
    {
        [SerializeField] private int _weaponDamage;
        [SerializeField] private int _range;

        public override void Execute(BaseUnit skillUser)
        {
            List<BaseUnit> targets = skillUser.GetTargets(_range);
            foreach (BaseUnit target in targets)
            {
                Debug.Log("ShotToNearests Used");
                target.TakeDamage(_weaponDamage);
            }
        }
    }
}