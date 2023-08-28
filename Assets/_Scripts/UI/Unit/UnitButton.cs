using Extensions;
using Managements;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Unit
{
    public class UnitButton : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _unitNameLabel;

        private IBaseUnitData _unitData;
        private IEventService _eventService;

        private void Awake()
        {
            SetDependencies();
        }

        public void Initialize(IBaseUnitData unitData)
        {
            _unitData = unitData;
            SetUI();
        }

        public void SetDependencies()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void SetUI()
        {
            _unitNameLabel.text = _unitData.Name;
            _selectButton.onClick.AddListener(SelectButtonClicked);
        }

        private void SelectButtonClicked()
        {
            _eventService.BroadcastEvent(EventTypes.OnUnitButtonClicked, _unitData);
        }
    }
}