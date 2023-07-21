using Services.Core.Score;
using Services.Core.Unit;

namespace Services
{
    public interface IService
    {

    }
    public class ServiceInitializer
    {
        public ServiceInitializer()
        {
            _ = UnitService.Instance;
            _ = ScoreService.Instance;
        }
    }
}