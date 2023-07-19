using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.EventSystem
{
    public interface IEventService
    {
        void RegisterEvent(int id, Action action);
        void RegisterEvent<T1>(int id, Action<T1> action);
        void RegisterEvent<T1, T2>(int id, Action<T1, T2> action);
        void RegisterEvent<T1, T2, T3>(int id, Action<T1, T2, T3> action);
        void RegisterEvent<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> action);
        void UnRegisterEvent(int id, Action action);
        void UnRegisterEvent<T1>(int id, Action<T1> action);
        void UnRegisterEvent<T1, T2>(int id, Action<T1, T2> action);
        void UnRegisterEvent<T1, T2, T3>(int id, Action<T1, T2, T3> action);
        void UnRegisterEvent<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> action);
        void BroadcastEvent(int id);
        void BroadcastEvent<T1>(int id, T1 param1);
        void BroadcastEvent<T1, T2>(int id, T1 param1, T2 param2);
        void BroadcastEvent<T1, T2, T3>(int id, T1 param1, T2 param2, T3 param3);
        void BroadcastEvent<T1, T2, T3, T4>(int id, T1 param1, T2 param2, T3 param3, T4 param4);
    }
}
