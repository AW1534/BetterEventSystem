using System;
using System.Collections.Generic;

namespace BetterEventSystem {
    public static class EventHandler {
        public static List<Event> events = new List<Event>();

        public static Event getEvent(String eventName) {
            foreach (var item in events) {
                if (item.name == eventName) {
                    return item;
                }
            }

            return null;
        }

        private static Event register(Event e) {
            events.Add(e);
            return e;
        }
    }
}