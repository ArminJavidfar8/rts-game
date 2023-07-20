using Data.Unit;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Unit
{
    public class UnitButton : MonoBehaviour, IServiceUser
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _unitNameLabel;

        private BaseUnitData _unitData;
        private IEventService _eventService;

        public void Initialize(BaseUnitData unitData)
        {
            SetDependencies();
            _unitData = unitData;
            SetUI();
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        private void SetUI()
        {
            _unitNameLabel.text = _unitData.Name;
            _selectButton.onClick.AddListener(SelectButtonClicked);
        }

        private void SelectButtonClicked()
        {
            _eventService.BroadcastEvent(EventTypes.OnUnitButtonSelected, _unitData);
        }
    }
}