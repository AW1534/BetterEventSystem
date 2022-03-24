using System;

namespace BetterEventSystem.Exceptions {
    [Serializable]
    public class BroadcastException : Exception {
        public BroadcastException(string text = " ") : base(text) { }
    }
}