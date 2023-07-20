using Data.Unit;
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
        private BaseUnitData _selectedUnitData;

        private IEventService _eventService;
        private IPoolService _poolService;

        public UnitService()
        {
            SetDependencies();

            _eventService.RegisterEvent<Vector3>(EventTypes.OnGroundClicked, GroundClicked);
            _eventService.RegisterEvent<BaseUnitData>(EventTypes.OnUnitButtonSelected, UnitSelected);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
            _poolService = PoolService.Instance;
        }

        private void GroundClicked(Vector3 position)
        {
            if (_selectedUnitData == null) return;
            SpawUnit(_selectedUnitData, position);
            _selectedUnitData = null;
        }

        public void SpawUnit(BaseUnitData unit, Vector3 position)
        {
            GameObject spawnedUnit = _poolService.GetGameObject(unit.Name);
            spawnedUnit.transform.position = position;
            spawnedUnit.GetComponent<BaseUnit>().SetData(unit);
        }

        private void UnitSelected(BaseUnitData selectedUnitData)
        {
            _selectedUnitData = selectedUnitData;
        }

    }
}