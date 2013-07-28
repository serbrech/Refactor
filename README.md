Refactoring exercise
--------------------

When I started, I first wanted to write some tests around the SodaMachine class.

So the first step was to make this class testable in some way. this required
small, non-invasive changes :

-   Injecting the console input and output as TextReader and TextWriter

-   and add a mechanism to quit the infinite loop.

Once this was in place, I wrote some basic tests to get me started on the
duplication problem.

I found a bug in the order fanta branch by doing this (see [8d964466][bug]).

Before restructuring the code I worked on getting the similar branches of the
switch exactly the same. Once that was done, I could easily extract a method to
remove the duplication (OrderSoda: [57531f91][OrderSoda])

Finally, I didn't like the main switch from the Start method, so I hid it in a
factory class.  
I saw the use of the *dynamically typed factory method* to route a command in a
*CQRS* sample and found it very interesting. It provides a much better structure
for a minimal "magic" factor.


[OrderSoda]: https://github.com/serbrech/Refactor/commit/57531f91078ecda33f6afd0c3eebceea05bac258
[bug]: https://github.com/serbrech/Refactor/commit/8d964466d8a2
