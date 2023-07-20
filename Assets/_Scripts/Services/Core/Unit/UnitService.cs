using Common;
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
        private IPoolService _poolService;
        private IEventService _eventService;

        #region Singleton
        private static UnitService _instance;
        public static UnitService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitService();
                }
                return _instance;
            }
        }
        #endregion

        private UnitService()
        {
            SetDependencies();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
        }

        public void SetDependencies()
        {
            _poolService = PoolService.Instance;
            _eventService = EventService.Instance;
        }

        public void SpawUnit(BaseUnitData unit, Vector3 position, string tag)
        {
            GameObject spawnedUnit = _poolService.GetGameObject(unit.Name);
            spawnedUnit.tag = tag;
            spawnedUnit.transform.position = position;
            spawnedUnit.GetComponent<BaseUnit>().SetData(unit);
        }

        private void UnitDied(BaseUnit unit)
        {
            _poolService.ReleaseGameObject(unit.gameObject);
        }

    }
}