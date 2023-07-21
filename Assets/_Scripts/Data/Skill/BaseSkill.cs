using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Skill
{
    public abstract class BaseSkill : ScriptableObject
    {
        [SerializeField] private bool _needsTarget;
        
        public string Name => name;
        public bool NeedsTarget => _needsTarget;

        public virtual void Execute(BaseUnit skillUser) { }
        public virtual void Execute(BaseUnit skillUser, BaseUnit target) { }
    }
}