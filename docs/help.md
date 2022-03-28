# Help
Here are some things about BetterEventSystem that i think might be confusing, so i wrote this page to help you.

Don't be afraid to use the contents section at the right of this page to find what you need.

If you still have questions, check the [API reference](/reference.md) or feel free to [contact me](https://github.com/AW1534)

---
## Middleware
### What is a Middleware?
Middleware is a function that is called before the event is broadcasted. It can be used to modify the data before your listeners are called.

### How do I create a middleware?
to add a middleware to an event, use the `AddMiddleware` method. your middleware function must take two parameters, the first is an EventData, the second is an Action<EventArgs>.

Once you have finished modifying the data, you must call the second parameter to continue the event. If you don't, the middleware will be called in an infinite loop until you do.

If you don't want to modify the data, just call the second parameter anyway.

Example:
```c#
// add middleware to the event
EventSystem.GetEvent("my_event").AddMiddleware((e, next) => {
    Console.WriteLine("my_event is about to be triggered, this is a middleware");
    // cast the data to a dictionary, as we send it as a Dictionary. If your data is not a dictionary, you must cast it to whatever you want to send.
    // But keep in mind if it is not a dictionary, the following code will not work.
    Dictionary<string, string> data = e.data as Dictionary<string, string>; 
    foreach (var item in data.Values.ToList()) {
        Console.WriteLine("data: " + item); // this will print all the values in the dictionary
    }
    data.Add("middleware", "true"); // add a new key to the dictionary
    e.data = data;  // set our changed data
    next(e); // pass our data to the next middleware or the event listener
});
```

### What is the difference between a middleware and a listener?
A middleware is a function that is called before the event is broadcasted. It can be used to modify the data before your listeners are called.
A listener is a function that is called when the event is broadcasted.

A listener cannot modify data, but a middleware can.

