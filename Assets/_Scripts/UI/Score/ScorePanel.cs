using Extensions;
using Managements;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.EventSystem;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreLabel;

        private IEventService _eventService;
        private void Awake()
        {
            SetDependencies();
        }
        private void Start()
        {
            _eventService.RegisterEvent<int>(EventTypes.OnScoreUpdated, ScoreUpdated);
        }

        public void SetDependencies()
        {
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
        }

        private void ScoreUpdated(int score)
        {
            _scoreLabel.text = score.ToString();
        }
    }
}