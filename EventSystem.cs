
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

        public static Dictionary<String, Event> Events = new Dictionary<String, Event>();

        public static Event GetEvent(string eventName, bool safe = true) {
            // check if the eventName is in the dictionary
            if (Events.ContainsKey(eventName)) {
                return Events[eventName];
            }
            
            // if the eventName is not in the dictionary, either create it throw an exception
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
            Events.Add(e.Name, e);
            return e;
        }

        public static void Unregister(Event e) {
            Events.Remove(e.Name);
        }
    }
}