using System.Diagnostics;
using BetterEventSystem.Exceptions;

namespace BetterEventSystem {

    public class EventArgs {
        public object sender;
        public object data;
        
        public EventArgs(object data) {
            this.sender = sender;
            this.data = data;
        }
    }

    [Serializable]
    public class Event {
        public bool AllowAsync;
        public string Name;
        List<Action<EventArgs, Action<EventArgs>>> _middleware = new List<Action<EventArgs, Action<EventArgs>>>();
        List<Action<EventArgs>> _listeners = new List<Action<EventArgs>>();

        public Event(String name, bool allowAsync = true, bool register = true) {
            this.Name = name;
            this.AllowAsync = allowAsync;
            if (register) { EventSystem.Register(this); }
        }

        public void AddListener(Action<EventArgs> listener) {
            _listeners.Add(listener);
        }
        
        public void AddMiddleware(Action<EventArgs, Action<EventArgs>> middleware) {
            _middleware.Add(middleware);
        }
        
        public void RemoveListener(Action<EventArgs> listener) {
            _listeners.Remove(listener);
        }
        
        private void RemoveMiddleware(Action<EventArgs, Action<EventArgs>> middleware) {
            _middleware.Remove(middleware);
        }
        
        public void RemoveAllListeners() {
            _listeners.Clear();
        }
        
        public void RemoveAllMiddleware() {
            _middleware.Clear();
        }
        
        public void RemoveAll() {
            RemoveAllListeners();
            RemoveAllMiddleware();
        }

        private EventArgs RunMiddleware(EventArgs args) {
            List<Action<EventArgs, Action<EventArgs>>> _middleware_temp = _middleware;

            // iterate through each middleware and run it, passing the args to the next middleware
            Action<EventArgs> next = new Action<EventArgs>(eArgs => {
                if (_middleware_temp.Count > 0) {
                    _middleware_temp.RemoveAt(0);
                }

                args = eArgs;
            });

            while (_middleware_temp.Count > 0) {
                _middleware_temp[0](args, next);
            }

            return args;
        }

        public void BroadcastAsync(object data = null) {
            EventArgs args = new EventArgs(data);
            args = RunMiddleware(args);
            foreach (var item in _listeners) {
                if (AllowAsync) {
                    Task.Run(() => item(args));
                } else {
                    throw new BroadcastException("async not allowed in event");
                }
            }
        }

        public void BroadcastSync(object data = null) {
            EventArgs args = new EventArgs(data);
            args = RunMiddleware(args);
            foreach (var item in _listeners) {
                item.Invoke(args);
            }
        }

        public void Broadcast(object data = null) {
            if (AllowAsync) {
                BroadcastAsync(data);
            } else {
                BroadcastSync(data);
            }
        }
    }
}