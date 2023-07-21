using Common;
using Data.Unit;
using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public class AutoFireStaticWeapon : BaseUnit
    {
        [SerializeField] private RangeChecker _rangeChecker;

        private List<BaseUnit> _targets;
        private Coroutine _findTargetAndShootCoroutine;

        public override void SetData(BaseUnitData unitData)
        {
            base.SetData(unitData);
            _rangeChecker.SetRadius(FireRange);
        }
        public override void OnGetFromPool()
        {
            base.OnGetFromPool();
            _targets = new List<BaseUnit>();
            _rangeChecker.OnGetFromPool(OnUnitEnteredRange, OnUnitExitedRange);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
            _findTargetAndShootCoroutine = StartCoroutine(FindTargetAndShoot());
        }

        private void UnitDied(BaseUnit unit)
        {
            if (unit.tag != tag)
            {
                _targets.Remove(unit);
            }
        }

        public override void OnReleaseToPool()
        {
            base.OnReleaseToPool();
            _rangeChecker.OnReleaseToPool();
            _eventService.UnRegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
            StopCoroutine(_findTargetAndShootCoroutine);
        }

        private void OnUnitEnteredRange(BaseUnit unit)
        {
            if (unit.tag != tag)
            {
                _targets.Add(unit);
            }
        }

        private void OnUnitExitedRange(BaseUnit unit)
        {
            if (unit.tag != tag)
            {
                _targets.Remove(unit);
            }
        }

        private IEnumerator FindTargetAndShoot()
        {
            yield return new WaitForSeconds(1);
            WaitForSeconds findingTargetWait = new WaitForSeconds(0.5f);
            WaitForSeconds shootingWait = new WaitForSeconds(FireRate);
            while (true)
            {
                while (_targets.Count > 0 && _targets[0] != null && !_targets[0].IsDead)
                {
                    Shoot(_targets[0], _targets[0].transform.position, WeaponDamage);
                    yield return shootingWait;
                }
                yield return findingTargetWait;
            }
        }
    }
}