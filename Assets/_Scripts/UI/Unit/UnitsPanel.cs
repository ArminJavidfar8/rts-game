using Common;
using Data.Unit;
using Managements;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Unit
{
    public class UnitsPanel : MonoBehaviour
    {
        [SerializeField] private UnitButton _unitButtonPrefab;

        private IResourceService _resourceService;

        private void Awake()
        {
            SetDependencies();
        }

        void Start()
        {
            SetUnitsList();
        }

        public void SetDependencies()
        {
            _resourceService = ServiceHolder.ServiceProvider.GetService<IResourceService>();
        }

        private void SetUnitsList()
        {
            IEnumerable<IBaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            foreach (IBaseUnitData unit in units)
            {
                UnitButton unitButton = Instantiate(_unitButtonPrefab, transform);
                unitButton.Initialize(unit);
            }
        }
    }
}