Practicing with monads in C#

The code is based on Mario Fusco's talk "Monadic Java" (http://de.slideshare.net/mariofusco/monadic-java).

Currently, the examples include an "Optional" and "Validation" monads and an extension method (from here:  http://blogs.msdn.com/b/pfxteam/archive/2010/11/21/10094564.aspx) that turns a Task into a monad. An "Optional" monad is a class that wraps a value, which may be null. The point is to write code focusing on the logic and without checking the return value of every property for nulls.

With the introduction of the "Elvis operator" in C# 6, an Optional class would not be required in C#, but implementing it is a fun exercise.

The Task monad is not necessary either because there is another way of composing Tasks by using "async" and "await".

However, Validation looks like it might be useful by making the code more concise.