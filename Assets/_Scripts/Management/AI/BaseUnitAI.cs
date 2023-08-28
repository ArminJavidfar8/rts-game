using Common;
using Managements.Unit;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using UnityEngine;

namespace Managements.AI
{
    public abstract class BaseUnitAI : MonoBehaviour
    {
        private IUnitService _unitService;
        protected BaseUnit _connectedUnit;

        public virtual void Initialize(BaseUnit connectedUnit)
        {
            SetDependencies();
            _connectedUnit = connectedUnit;
        }

        public void SetDependencies()
        {
            _unitService = ServiceHolder.ServiceProvider.GetService<IUnitService>();
        }

        protected BaseUnit FindPlayerForAI()
        {
            return _unitService.GetNearestTarget(_connectedUnit, 100, Constants.Tags.PLAYER);
        }

        protected void MoveToPlayer(BaseUnit playerUnit)
        {
            float distance = Vector3.Distance(playerUnit.transform.position, _connectedUnit.transform.position);
            if (distance > _connectedUnit.FireRange)
            {
                _connectedUnit.Move(_connectedUnit.FireRange * 1f / distance * playerUnit.transform.position);
            }
        }
    }
}