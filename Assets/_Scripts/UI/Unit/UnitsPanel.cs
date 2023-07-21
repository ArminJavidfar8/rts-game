using Common;
using Data.Unit;
using Services.Abstraction;
using Services.Core.ResourceSystem;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Unit
{
    public class UnitsPanel : MonoBehaviour, IServiceUser
    {
        [SerializeField] private UnitButton _unitButtonPrefab;

        private IResourceService _resourceService;

        void Start()
        {
            SetDependencies();

            SetUnitsList();
        }

        public void SetDependencies()
        {
            _resourceService = ResourceService.Instance;
        }

        private void SetUnitsList()
        {
            IEnumerable<BaseUnitData> units = _resourceService.GetResource<UnitsCollection>(Constants.Paths.UNITS_COLLECTION).Units;
            foreach (BaseUnitData unit in units)
            {
                UnitButton unitButton = Instantiate(_unitButtonPrefab, transform);
                unitButton.Initialize(unit);
            }
        }
    }
}