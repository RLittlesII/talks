---
title: Specification Driven Development
description: Follow a checkout claim from agreed intent through the code, tests, and evidence you review
difficulty: Beginner
prerequisites:
    - Artificial Intelligence
    - Software Engineering
    - TDD
    - BDD
---

## Abstract

Old and busted. The 1998 requirements spec. New hotness. Vibe coding. Then I spent a week’s worth of tokens reviewing a change and rediscovering rules we already knew. I wanted to give the agent room to work and come back to evidence I could actually inspect.

Follow one checkout claim from a versioned agreement to a Gherkin scenario, a failing test, a scoped code change, and a passing run. See how that review experience led me to build Specht, how I use the same approach on Specht itself, and how Roslyn and `dotnet format` handle repeatable checks and cleanup. We’ll close by changing the expiry boundary and deciding what needs fresh approval and evidence.

### Short Abstract

I give agents room to work by agreeing on behavior and deciding what evidence I’ll review. Follow one checkout claim from a spec to a test and sign-off, then see how Specht, Roslyn, and `dotnet format` handle repeatable work.
