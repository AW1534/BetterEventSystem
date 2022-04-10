# API Reference
```{seealso}
This is the API reference for the BetterEventSystem.
If you are looking for a guide, please refer to the [starter guide](./intro.md).
```

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
| **Name**               | **Return Type** | **Parameters**                                      | **Description**                                                                                                                        |
|------------------------|-----------------|-----------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------|
| AddListener            | `void`          | `Action<EventArgs>` listener                        | Add a listener to the Event                                                                                                            |
| RemoveListener         | `void`          | `Action<EventArgs>` listener                        | Remove a listener from the event                                                                                                       |
| AddPreprocessor        | `void`          | `Action<EventArgs, Action<EventArgs>>` preprocessor | Add a preprocessor to the event                                                                                                        |
| RemovePreprocessor     | `void`          | `Action<EventArgs, Action<EventArgs>>` preprocessor | Remove a preprocessor from the event                                                                                                   |
| RemoveAllListeners     | `void`          | N/A                                                 | Remove all listeners from the event                                                                                                    |
| RemoveAllPreprocessors | `void`          | N/A                                                 | Remove all preprocessor from the event                                                                                                 |
| RemoveAll              | `void`          | N/A                                                 | Remove all listeners and preprocessor                                                                                                  |
| Broadcast              | `EventArgs`     | `object` data = null                                | Broadcast the event to all listeners, passing the data to all inside the `EventArgs.data` property, returns the final EventArgs object |
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
| **Name** | **Return Type** | **Parameters**                    | **Description**                                            |
|----------|-----------------|-----------------------------------|------------------------------------------------------------|
| GetEvent | `Event`         | `String` name, `bool` safe = true | Get an event by name, creating the event if `safe` is true |
| Register | `Event`         | `Event` event                     | Register an event                                          |
---

### Properties
| **Name** | **Type**      | **Description**          |
|----------|---------------|--------------------------|
| Events   | `List<Event>` | A list of all the events |

## Event Args
The `EventArgs` class allows you to pass data to listeners.

### Methods
| **Name** | **Return Type** | **Parameters**       | **Description** |
|----------|-----------------|----------------------|--|
| cancel   | `void`          | `bool` cancel = true | cancel the execution of the layers. [learn more](./help.md)|

---

### Properties
| **Name** | **Type** | **Description**                                                                   |
|----------|----------|-----------------------------------------------------------------------------------|
| data     | `object` | contains the data sent to the broadcast method, can be modified during middleware |

