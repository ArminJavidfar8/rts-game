using Data.Skill;
using Extensions;
using Managements;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Skill
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _skillNameLabel;

        private BaseSkill _baseSkill;
        private IEventService _eventService;

        private void Awake()
        {
            SetDependencies();
        }
        public void Initialize(BaseSkill baseSkill)
        {
            _baseSkill = baseSkill;
            SetUI();
        }

        public void SetDependencies()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
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