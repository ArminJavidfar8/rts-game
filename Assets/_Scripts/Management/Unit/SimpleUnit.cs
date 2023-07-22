using Services.Abstraction;
using Services.Abstraction.PoolSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public class SimpleUnit : BaseUnit, IMoveable
    {
        private Coroutine _continuousShootCoroutine;
        public override void SetTargetByUser(BaseUnit target)
        {
            if (_continuousShootCoroutine != null)
            {
                StopCoroutine(_continuousShootCoroutine);
            }
            _continuousShootCoroutine = StartCoroutine(ShootContinuously(target));
        }

        public override void RemoveTargetByUser(BaseUnit target)
        {
            if (_continuousShootCoroutine != null)
            {
                StopCoroutine(_continuousShootCoroutine);
            }
        }

        private IEnumerator ShootContinuously(BaseUnit target)
        {
            WaitForSeconds shootingWait = new WaitForSeconds(FireRate);
            while (Health > 0 && !target.IsDead)
            {
                Shoot(target, target.transform.position, WeaponDamage);
                yield return shootingWait;
            }
        }

    }
}