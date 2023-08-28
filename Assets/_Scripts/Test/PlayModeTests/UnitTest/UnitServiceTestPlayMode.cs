using System.Collections;
using System.Collections.Generic;
using Common;
using Data.Unit;
using Extensions;
using Managements.Unit;
using NUnit.Framework;
using Services.Abstraction;
using Services.Core.Unit;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.PlayModeTests.UnitTest
{
    public class UnitServiceTestPlayMode
    {
        [Test]
        public void NoTargetAroundTest()
        {
            IUnitService unitService = UnitService.Instance;

            BaseUnit playerUnit = CreatePlayer();

            BaseUnit foundTarget = unitService.GetNearestTarget(playerUnit, 100, Constants.Tags.ENEMY);

            Assert.IsNull(foundTarget);
        }


        [Test]
        public void OneTargetAroundTest()
        {
            IUnitService unitService = UnitService.Instance;

            BaseUnit playerUnit = CreatePlayer();

            BaseUnit enemyUnit = CreateEnemy();

            BaseUnit foundTarget = unitService.GetNearestTarget(playerUnit, 100, Constants.Tags.ENEMY);

            Assert.IsNotNull(foundTarget);
        }

        private BaseUnit CreatePlayer()
        {
            IEnumerable<IBaseUnitData> units = Resources.Load<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            IUnitService unitService = UnitService.Instance;
            BaseUnit baseUnit = unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.PLAYER);
            return baseUnit;
        }

        private BaseUnit CreateEnemy()
        {
            IEnumerable<IBaseUnitData> units = Resources.Load<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            IUnitService unitService = UnitService.Instance;
            BaseUnit baseUnit = unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.ENEMY);
            return baseUnit;
        }
    }
}