Difficulty: Intermediate
Prerequisites:
    - C#
    - Language Version 13
    - Composition
    - Inheritance
    - Testability
    - Abstractions
---

# C# Abstractions, the lies they tell us, and the fact your likely still doing it wrong

## Abstract

In this session, we'll talk about the fundamental theorem of software engineering.
Techniques to get there inheritance and composition
what you need an abstraction
why you don't need an abstraction
why you're likely building your interfaces wrong
what you can do to get more lift
all based on SRP, LSP, ISP from the principles you are tired of hearing about SOLID

## Short Abstract
Most of us build abstractions like we're hoarding for winter, then act surprised when the codebase turns into a dependency escape room.
In this session, we'll cut through the noise on abstraction's two major techniques — inheritance and composition — and why most of us reach for the wrong one out of habit. Using Single Responsibility, Liskov Substitution, and Interface Segregation as actual design tools, we'll explore when abstraction earns its keep and when it's just complexity with good PR. Add, modern C# techniques that make clean boundaries possible without the chaos.
You'll leave with sharper instincts. Your codebase will thank you.

## Long Abstract

## Abstraction: Stop Pretending You've Got This

### The Pitch

We are going to side step the abstraction mess most of us pretend we've solved.

Let's talk about what abstraction *actually* is: the art of hiding complexity behind a boundary so the rest of your system doesn't have to care. Sounds simple. It isn't. Because we have two major weapons in our arsenal — **inheritance** and **composition** — and most of us are wielding them like a toddler with a lightsaber.

Inheritance looked smart six months ago. Composition turned into wiring chaos. And somewhere in between, your interfaces became a junk drawer of method signatures that nobody can explain and everybody's afraid to touch.

Let's be honest: a lot of us build abstractions like we're hoarding for winter, then act surprised when the codebase turns into a dependency escape room. We reach for inheritance when we want abstraction, and we reach for composition when inheritance burned us — and then we do *both* wrong and call it architecture.

So here's what we're actually going to do. We're going to talk about *when* to inherit, *when* to compose, and *why* most of us default to the wrong one out of habit. We'll use Single Responsibility, Liskov Substitution, and Interface Segregation as practical tools — not buzzwords — to draw abstraction boundaries that actually mean something.

And because this is C#, we're not stuck with the patterns your 2009 textbook recommended. Modern C# gives us better primitives for expressing abstraction cleanly — and we're going to use them.

We'll go through the muck together. You'll leave with sharper instincts, a clearer sense of when abstraction earns its keep versus when it's just complexity with a good publicist, and systems that evolve without collapsing under their own cleverness.

---

### What You'll Learn

- **What abstraction actually is** — and why "I'll just make an interface" is not an answer
- **Inheritance vs. Composition** — when each technique earns its place, and when it's just habit
- **Why most of us do it wrong** — including the speaker, who has the scars to prove it
- **How to use SRP, LSP, and ISP as real design tools** — not something you recite in a job interview
- **Modern C# techniques** for expressing clean abstractions that don't rot under pressure
- **How to draw boundaries that hold** — so your system evolves without collapsing under its own cleverness

---

## Who Should Attend

If you've ever stared at a base class wondering why you thought that was a good idea, opened an interface with fourteen methods and felt nothing but regret, or explained your abstraction layer to a coworker and watched the hope drain from their eyes — this session is for you.

---

Want me to add a speaker bio blurb or a short version of the abstract for CFP character limits?