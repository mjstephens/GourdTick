# GourdTick
Customizable ticking framework for Unity projects. Define tick groups, define the order in which the groups are called, and define how frequently they are ticked. This could be on Update/FixedUpdate/LateUpdate or at an interval of your choosing. 

Custom grouping provides simpler control over the execution order of your systems, and even just using the default tick groups instead of Unity's builtin Update functions can result in slight performance improvements in certain scenarios.

## Implementation
Import the package by downloading and adding to the Packages folder, or import with the git link through the Package Manager window.

After importing, you can use the default tick groups TickUpdate, TickFixedUpdate, and TickLateUpdate right away. Or, if you'd like to define custom tick groups, you can do so by creating an extension of the **TickSettings** class and adding your own groups.

To expose classes to the tick system, you can inherit from the **ITickable** interface; define a group to which the class belongs, subscribe and unsubscribe from the Ticker as needed, and implement the Tick() callback however you want. Or if the class is a simple MonoBehaviour, you can just inherit from the included **TickableBehaviour** class to save some boilerplate.

For complete samples of custom tick groups and ITickable implementation, download the included "Example Implementation" sample from the Package Manager window.
