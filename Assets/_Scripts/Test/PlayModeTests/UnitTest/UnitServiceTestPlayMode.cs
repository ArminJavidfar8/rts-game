using System.Collections.Generic;
using Common;
using Data.Unit;
using Extensions;
using Managements.Unit;
using NUnit.Framework;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Abstraction.PoolSystem;
using Services.Core.EventSystem;
using Services.Core.PoolSystem;
using Services.Core.ResourceSystem;
using Services.Core.Unit;
using UnityEngine;

namespace Test.PlayModeTests.UnitTest
{
    public class UnitServiceTestPlayMode
    {
        private IResourceService _resourceService;
        private IUnitService _unitService;

        [SetUp]
        public void Init()
        {
            IPoolService poolService = new PoolService();
            IEventService eventService = new EventService();
            _resourceService = new ResourceService();
            _unitService = new UnitService(poolService, eventService);
        }

        [Test]
        public void NoTargetAroundTest()
        {
            BaseUnit playerUnit = CreatePlayer();

            BaseUnit foundTarget = _unitService.GetNearestTarget(playerUnit, 100, Constants.Tags.ENEMY);

            Assert.IsNull(foundTarget);
        }

        [Test]
        public void OneTargetAroundTest()
        {
            BaseUnit playerUnit = CreatePlayer();

            BaseUnit enemyUnit = CreateEnemy();

            BaseUnit foundTarget = _unitService.GetNearestTarget(playerUnit, 100, Constants.Tags.ENEMY);

            Assert.IsNotNull(foundTarget);
        }

        private BaseUnit CreatePlayer()
        {
            IEnumerable<IBaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            BaseUnit baseUnit = _unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.PLAYER);
            return baseUnit;
        }

        private BaseUnit CreateEnemy()
        {
            IEnumerable<IBaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            BaseUnit baseUnit = _unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.ENEMY);
            return baseUnit;
        }
    }
}