BetterEventSystem
---
To create an event, simply create a new instance of the event class and pass it the name of the event.
`new Event("my_event")`. then, you can add listeners to the event by calling `Event.AddEvent(listener)` on the event instance.

*note: listeners must take one argument, of the `BetterEventSystem.EventArgs` type*

---
To get the event instance, simply call `EventSystem.GetEvent("my_event")`. this will also create the event if it doesnt exist (disable this by setting the `safe` paramater to true.

---
Finally, to broadcast / trigger the event, simply call `Event.Broadcast()`.

---
## Example:
```c#
using BetterEventSystem;

// create the event, and add a listener
new Event("my_event").AddListener((e) => {
    Console.WriteLine("my_event was triggered");
});

// add another listener
EventSystem.GetEvent("my_event").AddListener((e) => {
    Console.WriteLine("my_event was triggered, but I'm a different listener");
});

// this event doesnt exist, but it will be created with default values. you can edit the Event after creation if you want
EventSystem.GetEvent("my_other_event").AddListener((e) => {
    Console.WriteLine("my_other_event was triggered");
});


Console.Write("Press any key to trigger events...");
Console.ReadKey();
Console.Clear();

// trigger the event
EventSystem.GetEvent("my_event").Broadcast();
EventSystem.GetEvent("my_other_event").Broadcast();
```
---
Output:
```text
my_other_event was triggered
my_event was triggered, but I'm a different listener
my_event was triggered
```