using Common;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;
using Services.Core.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core.Score
{
    public class ScoreService : IScoreService, IServiceUser
    {
        private int _score;
        private IEventService _eventService;

        #region Singleton
        private static ScoreService _instance;
        public static ScoreService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreService();
                }
                return _instance;
            }
        }
        #endregion

        private ScoreService()
        {
            SetDependencies();
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
        }

        public void SetDependencies()
        {
            _eventService = EventService.Instance;
        }

        private void UnitDied(BaseUnit unit)
        {
            if (unit.tag == Constants.Tags.ENEMY)
            {
                _score += unit.KillingScore;
                _eventService.BroadcastEvent(EventTypes.OnScoreUpdated, _score);
            }
        }

    }
}