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

namespace Test.EditModeTests.UnitTest
{
    public class UnitServiceTest
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
        public void UnitServiceEditModeTest1()
        {
            IEnumerable<IBaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            BaseUnit baseUnit = _unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.PLAYER);

            Assert.AreEqual(simpleUnitData.MaxHealth, baseUnit.Health);
        }
    }
}