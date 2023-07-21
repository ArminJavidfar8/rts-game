using Extensions;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    public class ScorePanel : MonoBehaviour, IServiceUser
    {
        [SerializeField] private TMP_Text _scoreLabel;

        private IEventService _eventService;
        private void Start()
        {
            SetDependencies();
            _eventService.RegisterEvent<int>(EventTypes.OnScoreUpdated, ScoreUpdated);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        private void ScoreUpdated(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}