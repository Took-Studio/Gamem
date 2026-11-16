# Gamem 3.3.0 is out!

![Banner](https://i.imgur.com/qarfoxG.png)

_You can download it on [NuGet](https://www.nuget.org/packages/Gamem)!_

## NEW STRUCT!1!!11!!!1!!
`RangeGm` has been finally released!!!

### What is RangeGm?
![Range](https://i.imgur.com/3kXgrua.png)
This is a simple `range` that can have two numbers (X and Y, where X is **always** smaller than Y)

e.g. you can create a range for health (0; 100)
```cs
using Gamem;

RangeGm health = new(0, 100);

// You can also use some tools!
// e.g. Contains

RangeGm range2 = new(25, 35);

health.Contains(range2); // true, cause range 25-35 is "inside" the 0-100 range
```
### Range overlapping

You can also check whether two ranges overlap each other. This can be useful for collision detection, interval calculations and other math operations

```cs
RangeGm first = new(0, 50);
RangeGm second = new(25, 75);

first.Overlaps(second) // true
```

### Math operations

RangeGm supports basic math operators!

```cs
RangeGm range = new(10, 20);

RangeGm a = range + 5; // (15; 25)
RangeGm b = range - 5; // (5; 15)
RangeGm c = range * 2; // (20, 40)
RangeGm d = range / 2; // (5; 10)
```

### Where can I use it?

**RangeGm can be useful in many different things in gamedev that needs ranges**
- Health and mana system
- Damage ranges
- Random number generation for ranges
- Coordinate ranges
- Math calculations
- Collision and overlap checks

**Enjoy RangeGm!!!**

_it took 2 months to make it, cuz I need to studyyyy_