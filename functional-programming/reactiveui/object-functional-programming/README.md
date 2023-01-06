Difficulty: Intermediate
Prerequisites:
    - Reactive Extensions
    - MVVM
    - Asynchronous Programming
---

# Functional Programming in an Object-Oriented world

## Abstract

C# is an [Object Oriented Language](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop) and as such has design patterns that are specific for its object-oriented nature.  It has to handle things like nullability, state mutation all while responding to external state.  After reading [Domain Modeling Made Functional: Tackle Software Complexity with Domain-Driven Design and F#](https://www.goodreads.com/book/show/34921689-domain-modeling-made-functional) and playing around with [Language-Ext](https://github.com/louthy/language-ext#readme) I realized I was doing it all wrong!  We'll explore, Monads, Functional Paradigms, Object Oriented approaches to bridge functional gaps and some learnings of my own from implementing a large-scale reactive system.