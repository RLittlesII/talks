# Representation in Modern C#

This document explores the fundamental principles of abstraction and the key features of modern C# used for representing those abstractions and defining system boundaries.

## The Fundamental Theorem of Software Engineering (FTSE)

The [Fundamental Theorem of Software Engineering](https://en.wikipedia.org/wiki/Fundamental_theorem_of_software_engineering) is the cornerstone of why we abstract:

> "We can solve any problem by introducing an extra level of indirection." — David Wheeler

### The Catch
> "...except for the problem of too many levels of indirection."

### The Balancing Act
Abstractions are not free. We must balance the benefits of indirection against the costs of complexity:
- **The Only Reason to Abstract**: To solve a specific problem by introducing a necessary level of indirection.
- **The Cost of Abuse**: Over-abstraction leads to "dependency escape rooms," high cognitive load, and unnecessary complexity.
- **The Cost of Non-Adherence**: Tightly coupled code is rigid, fragile, and nearly impossible to test or extend without breaking unrelated parts of the system.

---

## C# Abstractions: The "What"

### Interfaces

#### What they are
Contracts that define a set of signatures (methods, properties, events, indexers). They represent a "can-do" relationship or a specific role.

- **Value**: They enable total decoupling of the consumer from the implementation.
- **Modern Twist**: Since C# 8.0, they can contain Default Interface Methods (DIMs).

#### What they are not
- They are **not** classes; they cannot be instantiated.
- They are **not** holders of instance state (no fields).
- They are **not** restricted to a single hierarchy; a class can implement many.

#### When to use
- Use when defining a capability that can be shared across unrelated classes.
- Use when you need to support multiple inheritance of behavior.
- Use for defining external API contracts where implementation is provided by others.

---

### Abstract Classes

#### What they are
Base classes that cannot be instantiated. They provide a common definition and can contain both abstract members (no implementation) and concrete members (with implementation).

- **Value**: They provide a way to share code and default behavior while still enforcing a contract.
- **Hierarchy**: They represent an "is-a" relationship (even if partial).

#### What they are not
- They are **not** instantiable; you cannot `new` an abstract class.
- They are **not** just for method signatures; they can hold state (fields), define constructors, and have access modifiers (protected/internal).
- They are **not** "multiple"; a C# class can only inherit from one base class (abstract or otherwise).

#### When to use
- Use when related types share state (fields), constructors, or non-public members.
- Use when you want to provide a common base implementation that derived types can build upon.
- Use when you need to evolve a hierarchy over time (adding a non-abstract member doesn't break derived classes).
- Use to force a specific structure or lifecycle on derived types.

---

## Modern C# Features

## Default Interface Methods (DIMs)

### What they are
Default Interface Methods allow interfaces to include a default implementation for members. This feature, introduced in C# 8.0, enables interfaces to evolve without breaking existing implementations.

- **The value it adds**: It provides a way to add new functionality to an interface that has already been widely implemented. It also allows for "traits" or "mixins," where behavior can be shared across unrelated classes through an interface.
- **The drawback**: It can lead to ambiguity (the "diamond problem") if a class implements multiple interfaces that provide conflicting default implementations. Additionally, default methods are only accessible through a reference to the interface, not through a reference to the implementing class (unless the class explicitly overrides the method).

### What they are not
- They are **not** a replacement for abstract classes; interfaces still cannot hold instance state (fields).
- They are **not** extension methods; DIMs can be overridden by implementing classes and participate in polymorphism.

### When to use
- Use when evolving a public interface that is already implemented by external parties.
- Use to provide "optional" convenience methods in an interface that implementers can choose to override or keep.
- Use when designing mixin-like functionality.

---

## Generics

### Open Generics

### What they are
Open Generics (or "unbound generic types") are generic type definitions where the type parameters have not yet been specified (e.g., `List<>`, `IRepository<>`). They act as a blueprint for creating "closed" generic types.

### What they are not
- They are **not** instantiable types. You cannot create an instance of `new List<>()`.
- They are **not** "generic parameters" (`T`), which are placeholders within a generic definition.

### When to use
- **Dependency Injection**: Registering a generic service that should be specialized upon request (e.g., `services.AddTransient(typeof(ILogger<>), typeof(Logger<>))`).
- **Reflection**: When you need to inspect the structure of a generic type independently of its arguments, or when using `MakeGenericType` to construct types at runtime.
- **Framework Development**: When building systems that need to handle any `T` but require knowledge of the generic container itself.

---

## Extension Methods

### What they are
Extension methods allow you to "add" methods to existing types without creating a new derived type, recompiling, or otherwise modifying the original type. They are static methods that are called as if they were instance methods on the extended type.

### What they are not
- They are **not** instance methods; they cannot access `private` or `protected` members of the type they extend.
- They are **not** virtual; they cannot be overridden by subtypes. If a type has an instance method with the same signature, the instance method always takes precedence.

### When to use
- Use when you want to extend a type you do not own (e.g., types in the .NET Base Class Library or third-party libraries).
- Use to provide "fluent" APIs (like LINQ).
- Use to keep a class's core API small while providing many helper methods (Interface Segregation).

---

## Interface Overlap

### What they are
Interface overlap occurs when a type implements multiple interfaces that share the same member signatures (either directly or via inheritance).

- **It's okay**: Overlap is a perfectly valid and common scenario in C#. The compiler handles it by mapping the shared signature to a single implementation in the class (implicit implementation), or it allows you to provide distinct implementations for each interface (explicit implementation).
- **It's part of ISP**: The Interface Segregation Principle (ISP) encourages creating small, focused interfaces. When a class fulfills multiple roles, it naturally implements multiple interfaces that might overlap in their requirements. This is a sign of a decoupled, role-based design.

### What it is not
- It is **not** multiple inheritance of state; it is purely a shared contract.
- It is **not** a conflict that needs to be avoided; rather, it's a way to view a single object through different "lenses" or roles.

### When to use
- Use when a single class needs to satisfy multiple different consumers, each requiring a specific subset of functionality.
- Use explicit interface implementation when you need to disambiguate members or when you want to hide certain members from the class's public API to keep it clean.
