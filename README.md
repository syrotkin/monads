Practicing with monads in C#

The code is based on Mario Fusco's talk "Monadic Java" (http://de.slideshare.net/mariofusco/monadic-java).

Currently, the only example is an "Optional" monad. An "Optional" monad is a class that wraps a value, which may be null. The point is to write code focusing on the logic and without checking the return value of every property for nulls.

Of course, with the introduction of the "Elvis operator" in C# 6, an Optional class would not be required in C#, but implementing it is a fun exercise.