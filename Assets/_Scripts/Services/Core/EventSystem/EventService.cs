using Services.Abstraction.EventSystem;
using System;
using System.Collections.Generic;

namespace Services.Core.EventSystem
{
    public class EventService : IEventService
    {
        private readonly Dictionary<int, Delegate> _events;

        private static EventService _eventSystem;
        public static EventService Instance
        {
            get 
            {
                if (_eventSystem == null)
                {
                    _eventSystem = new EventService();
                }
                return _eventSystem;
            }
        }
        private EventService()
        {
            _events = new Dictionary<int, Delegate>();
        }

        public void RegisterEvent(int id, Action action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action)_events[id] + action;
            }
            else
            {
                _events[id] = action;
            }
        }

        public void RegisterEvent<T1>(int id, Action<T1> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1>)_events[id] + action;
            }
            else
            {
                _events[id] = action;
            }
        }

        public void RegisterEvent<T1, T2>(int id, Action<T1, T2> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2>)_events[id] + action;
            }
            else
            {
                _events[id] = action;
            }
        }

        public void RegisterEvent<T1, T2, T3>(int id, Action<T1, T2, T3> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2, T3>)_events[id] + action;
            }
            else
            {
                _events[id] = action;
            }
        }

        public void RegisterEvent<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2, T3, T4>)_events[id] + action;
            }
            else
            {
                _events[id] = action;
            }
        }

        public void UnRegisterEvent(int id, Action action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action)_events[id] - action;
            }
        }

        public void UnRegisterEvent<T1>(int id, Action<T1> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1>)_events[id] - action;
            }
        }

        public void UnRegisterEvent<T1, T2>(int id, Action<T1, T2> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2>)_events[id] - action;
            }
        }

        public void UnRegisterEvent<T1, T2, T3>(int id, Action<T1, T2, T3> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2, T3>)_events[id] - action;
            }
        }

        public void UnRegisterEvent<T1, T2, T3, T4>(int id, Action<T1, T2, T3, T4> action)
        {
            if (_events.ContainsKey(id))
            {
                _events[id] = (Action<T1, T2, T3, T4>)_events[id] - action;
            }
        }

        public void BroadcastEvent(int id)
        {
            if (_events.ContainsKey(id))
            {
                Action action = _events[id] as Action;
                action?.Invoke();
            }
        }
        public void BroadcastEvent<T1>(int id, T1 param1)
        {
            if (_events.ContainsKey(id))
            {
                Action<T1> action = _events[id] as Action<T1>;
                action?.Invoke(param1);
            }
        }

        public void BroadcastEvent<T1, T2>(int id, T1 param1, T2 param2)
        {
            if (_events.ContainsKey(id))
            {
                Action<T1, T2> action = _events[id] as Action<T1, T2>;
                action?.Invoke(param1, param2);
            }
        }

        public void BroadcastEvent<T1, T2, T3>(int id, T1 param1, T2 param2, T3 param3)
        {
            if (_events.ContainsKey(id))
            {
                Action<T1, T2, T3> action = _events[id] as Action<T1, T2, T3>;
                action?.Invoke(param1, param2, param3);
            }
        }

        public void BroadcastEvent<T1, T2, T3, T4>(int id, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (_events.ContainsKey(id))
            {
                Action<T1, T2, T3, T4> action = _events[id] as Action<T1, T2, T3, T4>;
                action?.Invoke(param1, param2, param3, param4);
            }
        }
    }
}
