using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface ITargetFinder
    {
        BaseUnit GetNearestTarget(int range);
        List<BaseUnit> GetTargets(int range);
    }
}