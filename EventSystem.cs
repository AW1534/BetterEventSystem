using System;
using System.Collections.Generic;

namespace BetterEventSystem {
    public static class EventSystem {

        public static void Main(string[] args) {
            Console.WriteLine(
                "BetterEventSystem is not intended to be run as a standalone application.\n" +
                "It is meant to be used as a library.\n" +
                "Please see the documentation for more information. \n \n https://bes.rtfd.io/"
            );
        }

        public static List<Event> Events = new List<Event>();

        public static Event GetEvent(String eventName, bool safe = true) {
            foreach (var item in Events) {
                if (item.Name == eventName) {
                    return item;
                }
            }

            if (safe) {
                return new Event(
                    name: eventName,
                    allowAsync: true,
                    register: true
                ); // these are the default parameters, but i want to be explicit
                
            } else {
                throw new NullReferenceException("Event \"" + eventName + "\" not found");
            }
        }

        public static Event Register(Event e) {
            Events.Add(e);
            return e;
        }
    }
}