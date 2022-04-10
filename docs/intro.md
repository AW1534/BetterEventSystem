# Starter Guide
*How to get started with BetterEventSystem*

```{seealso}
This is a short guide to help you get a project up and running.
you should then go to the [API reference](/reference.md) to extend the functionality of your project.
```

## Installation
Installation is simple. just install `Addikted.BetterEventSystem` From your nuget package manager.

## Usage
```{note}
before you start using BetterEventSystem, you need to add the `Addikted.BetterEventSystem` namespace to your project.
```

---
### Creating a new event
```c#
new Event("event_name")
```
An event will be created with the name `event_name`.

---
### Getting an event
```c#
EventSystem.GetEvent("event_name")
```
This will find and return an event with the name `event_name`, if it does not exist it will create it.

---
### Adding a listener
```c#
EventSystem.GetEvent("event_name").AddListener((e) => {
    //do something
});
```
A listener is a function that is called when the event is fired.
All listeners are called in the order they were added.

---
### Broadcasting / emitting an event
```c#
EventSystem.GetEvent("event_name").Broadcast(data);
```
you can pass any object you want to the event, and it will be passed to all listeners/preprocessors through the EventArgs.data property.

All done, check out the [API reference](/reference.md) to see how to extend the functionality of your project.