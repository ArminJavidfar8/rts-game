using Data.Unit;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IUnitService
    {
        void SpawUnit(BaseUnitData unitData, Vector3 position, string tag);
    }
}