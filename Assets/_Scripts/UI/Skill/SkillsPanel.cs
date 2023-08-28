using Data.Skill;
using Extensions;
using Managements;
using Managements.Unit;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Skill
{
    public class SkillsPanel : MonoBehaviour
    {
        [SerializeField] private SkillButton _skillButtonPrefab;

        private BaseUnit _selectedUnit;
        private List<SkillButton> _createdButtons;
        private IEventService _eventService;

        private void Awake()
        {
            SetDependencies();
        }

        private void Start()
        {
            _createdButtons = new List<SkillButton>();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitSelected, PlayerUnitSelected);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitDeselected, PlayerUnitDeselected);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
        }

        public void SetDependencies()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void PlayerUnitSelected(BaseUnit playerUnit)
        {
            _selectedUnit = playerUnit;
            SetSkillsList(playerUnit.Skills);
        }

        private void PlayerUnitDeselected(BaseUnit obj)
        {
            _selectedUnit = null;
            RemoveSkillButtons();
        }

        private void SetSkillsList(IEnumerable<BaseSkill> skills)
        {
            RemoveSkillButtons();
            foreach (BaseSkill skill in skills)
            {
                SkillButton skillButton = Instantiate(_skillButtonPrefab, transform);
                skillButton.Initialize(skill);
                _createdButtons.Add(skillButton);
            }
        }

        private void RemoveSkillButtons()
        {
            if (_createdButtons.Count > 0)
            {
                int count = _createdButtons.Count;
                for (int i = 0; i < count; i++)
                {
                    Destroy(_createdButtons[i].gameObject);
                }
                _createdButtons.Clear();
            }
        }

        private void UnitDied(BaseUnit unit)
        {
            if (_selectedUnit == unit)
            {
                RemoveSkillButtons();
            }
        }

    }
}