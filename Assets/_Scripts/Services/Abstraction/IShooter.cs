using Managements.Unit;
using System.Collections;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IShooter
    {
        void SetTargetByUser(BaseUnit target);
        IEnumerator ShootContinuously(BaseUnit target);
        void Shoot(IDamageable target, Vector3 targetPosition, float damage);
        IDamageable FindDamagable();
    }
}