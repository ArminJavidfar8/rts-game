using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public class RangeChecker : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;

        private Action<BaseUnit> _onUnitEnteredRange;
        private Action<BaseUnit> _onUnitExitedRange;

        public void SetRadius(float range)
        {
            _sphereCollider.radius = range;
        }
        public void OnGetFromPool(Action<BaseUnit> onUnitEnteredRange, Action<BaseUnit> onUnitExitedRange)
        {
            _onUnitEnteredRange = onUnitEnteredRange;
            _onUnitExitedRange = onUnitExitedRange;
        }

        public void OnReleaseToPool()
        {
            _onUnitEnteredRange = null;
            _onUnitExitedRange = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            BaseUnit baseUnit = other.GetComponent<BaseUnit>();
            if (baseUnit != null)
            {
                _onUnitEnteredRange?.Invoke(baseUnit);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BaseUnit baseUnit = other.GetComponent<BaseUnit>();
            if (baseUnit != null)
            {
                _onUnitExitedRange?.Invoke(baseUnit);
            }
        }
    }
}