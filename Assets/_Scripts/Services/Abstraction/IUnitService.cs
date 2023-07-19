using Managements.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IUnitService
    {
        void SpawUnit(BaseUnit unit, Vector3 position);
    }
}