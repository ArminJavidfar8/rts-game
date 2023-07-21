using Managements.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.AI
{
    public class SimpleSoldierAI : BaseUnitAI
    {
        public override void Initialize(BaseUnit connectedUnit)
        {
            base.Initialize(connectedUnit);
            StartCoroutine(LookForPlayerUnit());
        }

        private IEnumerator LookForPlayerUnit()
        {
            while (!_connectedUnit.IsDead)
            {
                BaseUnit foundPlayer = FindPlayerForAI();
                if (foundPlayer != null)
                {
                    MoveToPlayer(foundPlayer);
                }
                yield return new WaitForSeconds(3);
                if (foundPlayer != null && !foundPlayer.IsDead && !_connectedUnit.IsDead)
                {
                    _connectedUnit.SetTargetByUser(foundPlayer);
                    yield return new WaitForSeconds(10);
                }
            }
        }
    }
}