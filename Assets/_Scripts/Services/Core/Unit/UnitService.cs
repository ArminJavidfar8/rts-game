using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using Services.Core.EventSystem;
using Services.Core.PoolSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core.Unit
{
    public class UnitService : IUnitService, IServiceUser
    {
        private BaseUnit _selectedUnit;

        private IEventService _eventService;
        private IPoolService _poolService;

        public UnitService()
        {
            InjectDependencies();

            _eventService.RegisterEvent<Vector3>(EventTypes.OnGroundClicked, GroundClicked);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitSelected, UnitSelected);
        }

        public void InjectDependencies()
        {
            _eventService = EventService.Instance;
            _poolService = PoolService.Instance;
        }

        public void SpawUnit(BaseUnit unit, Vector3 position)
        {
            GameObject spawnedUnit = _poolService.GetGameObject(unit.Name);
            spawnedUnit.transform.position = position;
        }

        private void GroundClicked(Vector3 position)
        {
            SpawUnit(_selectedUnit, position);
        }

        private void UnitSelected(BaseUnit selectedUnit)
        {
            _selectedUnit = selectedUnit;
        }

    }
}