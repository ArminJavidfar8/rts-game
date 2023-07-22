using Common;
using Data.Skill;
using Data.Unit;
using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using Services.Core.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managements.Unit
{
    public class PlayerUnitManager : MonoBehaviour, IServiceUser
    {
        private IBaseUnitData _selectedUnitData;
        private BaseUnit _selectedPlayerUnit;
        private BaseSkill _selectedSkill;
        private IEventService _eventService;
        private IUnitService _unitService;

        private void Start()
        {
            SetDependencies();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnPlayerUnitLeftClicked, PlayerUnitLeftClicked);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnEnemyUnitRightClicked, EnemyUnitRightClicked);
            _eventService.RegisterEvent<Vector3>(EventTypes.OnGroundLeftClicked, GroundLeftClicked);
            _eventService.RegisterEvent<Vector3>(EventTypes.OnGroundRightClicked, GroundRightClicked);
            _eventService.RegisterEvent<IBaseUnitData>(EventTypes.OnUnitButtonClicked, UnitButtonSelected);
            _eventService.RegisterEvent<BaseSkill>(EventTypes.OnSkillButtonClicked, SkillButtonClicked);
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
            _unitService = UnitService.Instance;
        }

        private void PlayerUnitLeftClicked(BaseUnit clickedUnit)
        {
            if (_selectedPlayerUnit != null)
            {
                _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
            }
            _selectedPlayerUnit = clickedUnit;
            _selectedSkill = null;
            _selectedUnitData = null;
            _eventService.BroadcastEvent(EventTypes.OnPlayerUnitSelected, _selectedPlayerUnit);
        }

        private void EnemyUnitRightClicked(BaseUnit clickedUnit)
        {
            if (_selectedPlayerUnit != null)
            {
                if (_selectedSkill != null)
                {
                    _selectedPlayerUnit.RemoveTargetByUser(clickedUnit);
                    _selectedSkill.Execute(_selectedPlayerUnit, clickedUnit);
                    _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
                    _selectedSkill = null;
                    _selectedPlayerUnit = null;
                }
                else
                {
                    _selectedPlayerUnit.SetTargetByUser(clickedUnit);
                }
            }
            _selectedUnitData = null;
        }

        private void GroundLeftClicked(Vector3 position)
        {
            if (_selectedUnitData == null)
            {
                if (_selectedPlayerUnit != null)
                {
                    _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
                    _selectedPlayerUnit = null;
                    _selectedSkill = null;
                }
            }
            else
            {
                if (_selectedPlayerUnit != null)
                {
                    _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
                    _selectedPlayerUnit = null;
                }
                _unitService.SpawUnit(_selectedUnitData, position, Constants.Tags.PLAYER);
                _selectedUnitData = null;
                _selectedSkill = null;
            }
        }

        private void GroundRightClicked(Vector3 position)
        {
            if (_selectedPlayerUnit != null && _selectedPlayerUnit is IMoveable)
            {
                _selectedPlayerUnit.Rotate(position);
                _selectedPlayerUnit.Move(position);
                _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
            }
            _selectedPlayerUnit = null;
            _selectedSkill = null;
            _selectedUnitData = null;
        }

        private void UnitButtonSelected(IBaseUnitData selectedUnitData)
        {
            _selectedUnitData = selectedUnitData;
        }

        private void SkillButtonClicked(BaseSkill skill)
        {
            if (skill.NeedsTarget)
            {
                _selectedSkill = skill;
                Debug.Log("Select a target");
            }
            else
            {
                skill.Execute(_selectedPlayerUnit);
                _eventService.BroadcastEvent(EventTypes.OnPlayerUnitDeselected, _selectedPlayerUnit);
                _selectedUnitData = null;
                _selectedSkill = null;
                _selectedPlayerUnit = null;
            }
        }

        private void UnitDied(BaseUnit unit)
        {
            if (unit == _selectedPlayerUnit)
            {
                _selectedPlayerUnit = null;
                _selectedSkill = null;
            }
        }

    }
}