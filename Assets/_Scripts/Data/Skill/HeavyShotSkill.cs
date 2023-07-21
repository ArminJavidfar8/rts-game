using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Skill
{
    [CreateAssetMenu(fileName = "HeavyShot", menuName = "rts-game/Skills/HeavyShot")]
    public class HeavyShotSkill : BaseSkill
    {
        [SerializeField] private int _weaponDamage;

        public override void Execute(BaseUnit skillUser, BaseUnit target)
        {
            Debug.Log("HeavyShot Used");
            target.TakeDamage(_weaponDamage);
        }
    }
}