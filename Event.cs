using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterEventSystem.Exceptions;

namespace BetterEventSystem {

    public class EventArgs {
        public object data;
        public bool cancel;
        
        public EventArgs(object data) {
            this.data = data;
        }
    }

    [Serializable]
    public class Event {
        public bool AllowAsync;
        public string Name;
        List<Action<EventArgs, Action<EventArgs>>> _preprocessor = new List<Action<EventArgs, Action<EventArgs>>>();
        List<Action<EventArgs>> _listeners = new List<Action<EventArgs>>();

        public Event(string name, bool allowAsync = true, bool register = true) {
            this.Name = name;
            this.AllowAsync = allowAsync;
            if (register) { EventSystem.Register(this); }
        }

        public void AddListener(Action<EventArgs> listener) {
            _listeners.Add(listener);
        }
        
        public void AddPreprocessor(Action<EventArgs, Action<EventArgs>> preprocessor) {
            _preprocessor.Add(preprocessor);
        }
        
        public void RemoveListener(Action<EventArgs> listener) {
            _listeners.Remove(listener);
        }
        
        private void RemovePreprocessor(Action<EventArgs, Action<EventArgs>> preprocessor) {
            _preprocessor.Remove(preprocessor);
        }
        
        public void RemoveAllListeners() {
            _listeners.Clear();
        }
        
        public void RemoveAllPreprocessor() {
            _preprocessor.Clear();
        }
        
        public void RemoveAll() {
            RemoveAllListeners();
            RemoveAllPreprocessor();
        }

        private EventArgs RunPreprocessor(EventArgs args) {
            List<Action<EventArgs, Action<EventArgs>>> _preprocessor_temp = _preprocessor;

            // iterate through each preprocessor and run it, passing the args to the next preprocessor
            Action<EventArgs> next = new Action<EventArgs>(eArgs => {
                if (_preprocessor_temp.Count > 0) {
                    _preprocessor_temp.RemoveAt(0);
                }

                args = eArgs;
            });

            while (_preprocessor_temp.Count > 0) {
                _preprocessor_temp[0](args, next);
            }

            return args;
        }

        public void BroadcastAsync(object data = null) {
            EventArgs args = new EventArgs(data);
            args = RunPreprocessor(args);
            if (args.cancel) { return; }
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
            args = RunPreprocessor(args);
            if (args.cancel) { return; }
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