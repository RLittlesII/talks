# Talks

This repository contains a collection of talks, samples, and demos covering various topics in software development, focusing on .NET, Functional Programming, and Mobile DevOps.

🔗 **[Browse the talks site](https://rlittlesii.github.io/talks/)**

## C# & .NET

### [C# Abstractions, the lies they tell us, and the fact your likely still doing it wrong](./%20csharp/language/abstractions/README.md)
Most of us build abstractions like we're hoarding for winter, then act surprised when the codebase turns into a dependency escape room. In this session, we'll cut through the noise on abstraction's two major techniques — inheritance and composition — and why most of us reach for the wrong one out of habit.
- **Slides**: [composition](npm%20run%20dev)
  ```bash
  npm --prefix "./ csharp/language/abstractions/composition" run dev
  ```

### [Null, The absence of a C# reference](./%20csharp/language/nullability/README.md)
Have you ever encountered a "possible deference of null reference" in your code and ignored it? During this talk we will explore what null is, how C# handles it, and some tools and practices to keep you from having failing code in production.
- **Slides**: [Keynote](./%20csharp/language/nullability/csharp.nullability.key)

### [MSBuild Central Package Version Sdk Example](./%20csharp/dependencies/management/CentralPackageVersion/README.md)
This example shows how to setup MSBuild Central Package Versioning for a Xamarin.Forms application.

## Functional Programming

### [Taming Mutable State: Applying Functional Programming in an Object-Oriented Language](./functional-programming/reactiveui/object-functional-programming/README.md)
In this talk, we’ll explore how functional paradigms—like monads, immutability, and railway-oriented programming—can help us write more predictable, maintainable code. We’ll also look at how these concepts align with reactive programming, enabling more robust event-driven systems.
- **Slides**: [Keynote](./functional-programming/reactiveui/object-functional-programming/taming-mutable-state.key)

### [Go Reactive with Reactive Extensions and ReactiveUI](./functional-programming/reactiveui/dotnet-conf/README.md)
This talk will show you why you want to use Reactive Extensions and ReactiveUI to solve concerns in a declarative manner, that makes it easier for you to focus on business value, rather than technical debt.
- **Slides**: [Keynote](./functional-programming/reactiveui/dotnet-conf/slides/going-reactive-xamarin.key) | [PowerPoint](./functional-programming/reactiveui/dotnet-conf/slides/going-reactive-with-xamarin-dotnet-conf.pptx)

### [Taming Mutable State using ReactiveUI](./functional-programming/reactiveui/mutable-state/README.md)
The purpose of this talk is to explore mutable state as a concept, and show developers a new way to react to state mutations and handle them in a declarative and composable manner.
- **Slides**: [Keynote (Intro)](./functional-programming/reactiveui/mutable-state/slides/intro-reactiveui.key) | [Keynote (Full)](./functional-programming/reactiveui/mutable-state/slides/taming-mutable-state.key)

### [SocialQ](./functional-programming/reactiveui/socialq/README.md)
Walk through the architecture of an application designed for Social Distance Queuing. See how ReactiveUI can be used as an MVVM framework to build an in-depth real world application.
- **Slides**: [Keynote](./functional-programming/reactiveui/socialq/socialq.key)

## Mobile Development

### [Xamarin, MAUI and the reactive MVVM between them](./mobile/jetbrains/README.md)
A trip through Xamarin, MVVM, ReactiveUI and show the future state of Xamarin, MAUI.
- **Slides**: [Keynote](./mobile/jetbrains/slides/xamarin-vacation-prep.key)

### [Laugh Pharmacy: Using SignalR and Blazor to deliver jokes to MAUI](./mobile/signalr/README.md)
Dive into the world of real-time joke delivery using SignalR, Blazor, and MAUI. Streaming jokes in real time while using Reactive Extensions to model the asynchronous nature of SignalR.

### [Prism + ReactiveUI a match made for mobile](./mobile/monkey-fest/es/README.md)
Harness the power of ReactiveUI for MVVM state management in a Prism application.

### [Xamarin.Forms at scale](./mobile/monkey-fest/en/README.md)
Solving for team complexity, increasing velocity, and automate testing of your Xamarin project by adding MVVM to the mix.
- **Slides**: [Keynote](./mobile/monkey-fest/en/Xamarin-Forms-Scale.key)

## Build Automation & DevOps

### [Mobile Dev Ops at Scale](./mobile/devops/README.MD)
Good practices around how to Version, Build, Test, Sign and Release your mobile applications across an enterprise.
- **Slides**: [PowerPoint](./mobile/devops/slides/Mobile.Dev.Ops.Scale.pptx)

### [Behavior Driven Practices](./mobile/bdd/README.MD)
How Behavior Driven Design and Development can address concerns and reduce friction. Evolving designs, ensuring objects answer questions, and encapsulating concerns to make boundaries more explicit.
- **Slides**: [Keynote](./mobile/bdd/slides/behavior.driven.software.key)

### [Fastlane Demo](./mobile/fastlane/README.md)
Show the power of fastlane to super charge your ability to interact with iOS build configuration.
- **Slides**: [Keynote](./mobile/fastlane/slides/fastlane.key)

## Samples & Demos

### [Mediator Sample](./mediator/README.md)
Sample code that showcases .NET Core MVC and the MediatR library.
