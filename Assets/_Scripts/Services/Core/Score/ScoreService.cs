using Common;
using Extensions;
using Managements.Unit;
using Services.Abstraction;
using Services.Abstraction.EventSystem;

namespace Services.Core.Score
{
    public class ScoreService : IScoreService
    {
        private int _score;
        private IEventService _eventService;

        private ScoreService(IEventService eventService)
        {
            _eventService = eventService;
            _eventService.RegisterEvent<BaseUnit>(EventTypes.OnUnitDied, UnitDied);
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