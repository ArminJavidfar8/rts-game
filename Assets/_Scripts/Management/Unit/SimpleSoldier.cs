using Services.Abstraction;
using Services.Abstraction.PoolSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public class SimpleSoldier : BaseUnit
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
    }
}