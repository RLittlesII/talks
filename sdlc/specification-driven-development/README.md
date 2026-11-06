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

Old and busted. The 1998 requirements spec. I brought it back because AI can help keep it current as the code changes. Then I spent a week’s worth of tokens reviewing a change after the agent revised the spec poorly.

Follow one checkout claim through the spec document, a Gherkin feature file, and an architecture decision. Run the example red and use it to drive the code change. Then see how a review full of drift led me to build Specht for the parts of spec upkeep a build can check, and how Roslyn and `dotnet format` handle repeatable checks and cleanup.

### Short Abstract

AI made it practical for me to maintain a spec again. Follow one checkout claim through the spec, feature file, and architecture decision, then see what went wrong in a real review and how Specht helps catch broken links.
