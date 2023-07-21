using Data.Skill;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Skill
{
    public class SkillButton : MonoBehaviour, IServiceUser
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _skillNameLabel;

        private BaseSkill _baseSkill;
        private IEventService _eventService;
        public void Initialize(BaseSkill baseSkill)
        {
            SetDependencies();
            _baseSkill = baseSkill;
            SetUI();
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        private void SetUI()
        {
            _skillNameLabel.text = _baseSkill.Name;
            _selectButton.onClick.AddListener(SelectButtonClicked);
        }

        private void SelectButtonClicked()
        {
            _eventService.BroadcastEvent(EventTypes.OnSkillButtonClicked, _baseSkill);
        }
    }
}