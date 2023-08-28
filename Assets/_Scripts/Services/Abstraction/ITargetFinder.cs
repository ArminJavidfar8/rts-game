using Managements.Unit;
using System.Collections.Generic;

namespace Services.Abstraction
{
    public interface ITargetFinder
    {
        BaseUnit GetNearestTarget(int range);
        List<BaseUnit> GetTargets(int range);
    }
}