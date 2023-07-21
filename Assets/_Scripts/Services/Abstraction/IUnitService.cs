using Data.Unit;
using Managements.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IUnitService
    {
        void SpawUnit(BaseUnitData unitData, Vector3 position, string tag);
        BaseUnit GetNearestTarget(BaseUnit source, int range, string targetTag);
        List<BaseUnit> GetNearestTargets(BaseUnit source, int range, string targetTag);
    }
}