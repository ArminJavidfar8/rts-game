using Data.Unit;
using Services.Abstraction;
using Services.Core.ResourceSystem;
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
            BaseUnitData[] units = _resourceService.GetResource<UnitsCollection>("Collections/UnitsCollection").Units;
            int length = units.Length;
            for (int i = 0; i < length; i++)
            {
                UnitButton unitButton = Instantiate(_unitButtonPrefab, transform);
                unitButton.Initialize(units[i]);
            }
        }
    }
}