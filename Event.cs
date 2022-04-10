using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetterEventSystem.Exceptions;

namespace BetterEventSystem {

    public class EventArgs {
        public object data;
        

        // for getters, setters
        private bool _cancelled;

        public bool cancelled {
            get => _cancelled;
            set => cancel(value);   // this doesnt do anything special, but maybe in future the cancel method will do something other than just set the value.
        }

        public void cancel(bool cancel = true) {
            _cancelled = cancel;
        }
        
        public EventArgs(object data) {
            this.data = data;
        }
    }

    [Serializable]
    public class Event {
        public bool AllowAsync;
        public string Name;
        private List<Action<EventArgs, Action<EventArgs>>> _preprocessors = new List<Action<EventArgs, Action<EventArgs>>>();
        private List<Action<EventArgs>> _listeners = new List<Action<EventArgs>>();
        private List<Action<EventArgs, Action<EventArgs>>> _postprocessors = new List<Action<EventArgs, Action<EventArgs>>>();
        

        public Event(string name, bool allowAsync = true, bool register = true) {
            this.Name = name;
            this.AllowAsync = allowAsync;
            if (register) { EventSystem.Register(this); }
        }

        #region Add Items
        
        public void AddPreprocessor(Action<EventArgs, Action<EventArgs>> preprocessor) {
            _preprocessors.Add(preprocessor);
        }

        public void AddListener(Action<EventArgs> listener) {
            _listeners.Add(listener);
        }

        public void AddPostprocessor(Action<EventArgs, Action<EventArgs>> postprocessor) {
            _postprocessors.Add(postprocessor);
        }
        
        #endregion

        #region Remove Items
        
        private void RemovePreprocessor(Action<EventArgs, Action<EventArgs>> preprocessor) {
            _preprocessors.Remove(preprocessor);
        }
        
        public void RemoveListener(Action<EventArgs> listener) {
            _listeners.Remove(listener);
        }

        private void RemovePostprocessor(Action<EventArgs, Action<EventArgs>> postprocessor) {
            _postprocessors.Remove(postprocessor);
        }
        
        public void RemoveAllPreprocessors() {
            _preprocessors.Clear();
        }
        
        public void RemoveAllListeners() {
            _listeners.Clear();
        }

        public void RemoveAllPostprocessors() {
            _postprocessors.Clear();
        }
        
        public void RemoveAll() {
            RemoveAllPreprocessors();
            RemoveAllListeners();
            RemoveAllPostprocessors();
        }
        
        #endregion

        private EventArgs RunPreprocessors(EventArgs args) {
            List<Action<EventArgs, Action<EventArgs>>> _preprocessor_temp = _preprocessors;

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

        private void RunListeners(EventArgs args, bool async) {
            if (async) {
                foreach (var item in _listeners) {
                    if (AllowAsync) {
                        Task.Run(() => item(args));
                    } else {
                        throw new BroadcastException("async not allowed in event");
                    }
                }
            } else {
                foreach (var item in _listeners) {
                    item.Invoke(args);
                }
            }
        }

        private EventArgs RunPostprocessors(EventArgs args) {
            List<Action<EventArgs, Action<EventArgs>>> _postprocessor_temp = _postprocessors;
            
            // iterate through each postprocessor and run it, passing the args to the next postprocessor
            Action<EventArgs> next = new Action<EventArgs>(eArgs => {
                if (_postprocessor_temp.Count > 0) {
                    _postprocessor_temp.RemoveAt(0);
                }
                
                args = eArgs;
            });
            
            while (_postprocessor_temp.Count > 0) {
                _postprocessor_temp[0](args, next);
            }

            return args;
        }

        public EventArgs BroadcastAsync(object data = null) {
            EventArgs args = new EventArgs(data);
            args = RunPreprocessors(args);
            if (args.cancelled) { return args; }
            RunListeners(args, true);
            if (args.cancelled) { return args; }
            return RunPostprocessors(args);
        }

        public EventArgs BroadcastSync(object data = null) {
            EventArgs args = new EventArgs(data);
            args = RunPreprocessors(args);
            if (args.cancelled) { return args; }
            RunListeners(args, false);
            if (args.cancelled) { return args; }
             return RunPostprocessors(args);
        }

        public EventArgs Broadcast(object data = null) {
            EventArgs result;
            if (AllowAsync) {
                result = BroadcastAsync(data);
            } else {
                result = BroadcastSync(data);
            }

            return result;
        }
    }
}