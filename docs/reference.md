# API Reference
```{seealso}
This is the API reference for the BetterEventSystem.
If you are looking for a guide, please refer to the [starter guide](./intro.md).
```

*This section is under construction.*

## Event
all the following properties and methods are available in the `Event.` class.

### Constructor
| **Name**   | **Type**  | **Default** | **Description**                                           |
|------------|-----------|-------------|-----------------------------------------------------------|
| name       | `String`  | N/A         | The name of the Event                                     |
| allowAsync | `Boolean` | `true`      | Whether or not to allow async calls to the listeners      |
| register   | `Boolean` | `true`      | Whether or not to register the event in the Event System. |
---

### Methods
| **Name**            | **Return Type** | **Parameters**                                    | **Description**                                                                                    |
|---------------------|-----------------|---------------------------------------------------|----------------------------------------------------------------------------------------------------|
| AddListener         | `void`          | `Action<EventArgs>` listener                      | Add a listener to the Event                                                                        |
| RemoveListener      | `void`          | `Action<EventArgs>` listener                      | Remove a listener from the event                                                                   |
| AddMiddleware       | `void`          | `Action<EventArgs, Action<EventArgs>>` middleware | Add a middleware to the event                                                                      |
| RemoveMiddleware    | `void`          | `Action<EventArgs, Action<EventArgs>>` middleware | Remove a middleware from the event                                                                 |
| RemoveAllListeners  | `void`          | N/A                                               | Remove all listeners from the event                                                                |
| RemoveAllMiddleware | `void`          | N/A                                               | Remove all middleware from the event                                                               |
| RemoveAll           | `void`          | N/A                                               | Remove all listeners and middleware                                                                |
| Broadcast           | `void`          | `object` data = null                              | Broadcast the event to all listeners, passing the data to all inside the `EventArgs.data` property |
---

### Properties
| **Name**   | **Type**  | **Description**                                      |
|------------|-----------|------------------------------------------------------|
| Name       | `String`  | The name of the event                                |
| AllowAsync | `Boolean` | Whether or not to allow async calls to the listeners |

## Event System
The event system is the heart of the BetterEventSystem.
It is a static class that contains all the events and their listeners.
The following properties and methods are available in the `EventSystem.` class.

### Methods
| **Name**  | **Return Type** | **Parameters**                    | **Description**                                            |
|-----------|-----------------|-----------------------------------|------------------------------------------------------------|
| Get Event | `Event`         | `String` name, `bool` safe = true | Get an event by name, creating the event if `safe` is true |
| Register  | `Event`         | `Event` event                     | Register an event                                          |
---

### Properties
| **Name** | **Type**      | **Description**          |
|----------|---------------|--------------------------|
| Events   | `List<Event>` | A list of all the events |