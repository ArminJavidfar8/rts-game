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

namespace Test.EditModeTests.UnitTest
{
    public class UnitServiceTest
    {
        [Test]
        public void UnitServiceEditModeTest1()
        {
            IEnumerable<IBaseUnitData> units = Resources.Load<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            IBaseUnitData simpleUnitData = units.GetItemAt<IBaseUnitData>(0);

            IUnitService unitService = UnitService.Instance;
            BaseUnit baseUnit = unitService.SpawUnit(simpleUnitData, Vector3.zero, Constants.Tags.PLAYER);

            Assert.AreEqual(simpleUnitData.MaxHealth, baseUnit.Health);
        }
    }
}