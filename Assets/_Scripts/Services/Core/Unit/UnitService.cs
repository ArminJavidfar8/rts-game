using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core.Unit
{
    public class UnitService : IUnitService
    {
        private List<BaseUnit> _activeUnits;
        private IPoolService _poolService;
        private IEventService _eventService;

        public UnitService(IPoolService poolService, IEventService eventService)
        {
            _eventService = eventService;
            _poolService = poolService;

            _activeUnits = new List<BaseUnit>();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
        }

        public BaseUnit SpawUnit(IBaseUnitData unit, Vector3 position, string tag)
        {
            GameObject spawnedUnit = _poolService.GetGameObject(unit.PrefabName);
            spawnedUnit.tag = tag;
            spawnedUnit.transform.position = position;
            BaseUnit spawnedBaseUnit = spawnedUnit.GetComponent<BaseUnit>();
            spawnedBaseUnit.SetData(unit);
            _activeUnits.Add(spawnedBaseUnit);
            return spawnedBaseUnit;
        }

        private void UnitDied(BaseUnit unit)
        {
            _poolService.ReleaseGameObject(unit.gameObject);
            _activeUnits.Remove(unit);
        }

        public BaseUnit GetNearestTarget(BaseUnit source, int range, string targetTag)
        {
            float nearestDistance = range;
            BaseUnit nearestTarget = null;
            foreach (BaseUnit target in _activeUnits)
            {
                float distance = Vector3.Distance(target.transform.position, source.transform.position);
                if (target.tag == targetTag && distance <= nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = target;
                }
            }
            return nearestTarget;
        }

        public List<BaseUnit> GetNearestTargets(BaseUnit source, int range, string targetTag)
        {
            List<BaseUnit> targets = new List<BaseUnit>();
            foreach (BaseUnit target in _activeUnits)
            {
                if (target.tag == targetTag && Vector3.Distance(target.transform.position, source.transform.position) <= range)
                {
                    targets.Add(target);
                }
            }
            return targets;
        }
    }
}