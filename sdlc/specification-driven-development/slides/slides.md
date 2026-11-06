---
theme: ./theme
colorSchema: dark
htmlAttrs:
  lang: en
title: Specification Driven Development
drawings:
  persist: false
  presenterOnly: true
record: false
contextMenu: false
transition: slide-left
comark: true
duration: 50min
---

# Specification Driven Development

<div v-click class="sdd-subtitle">
  Who checks the spec?
</div>

<!--
Timing: 0.5 minutes.

I like talking about my failures. They’re a surprisingly reliable source of conference material.

Click and ask “Who checks the spec?” I thought my review process had an answer. Then I tried it on a real change. I want to show what I changed so I could spend less time steering each edit.
-->

---
layout: two-cols
layoutClass: gap-16
class: agenda
transition: slide-up
---

# Agenda

::right::

<v-clicks>

- **The spec returns** — AI takes on the upkeep
- **One claim at work** — the spec, feature file, and architecture decision
- **My precious tokens** — the review that went sideways
- **Who checks the spec?** — what the build can catch
- **Make it your workflow** — what to review before signing off

</v-clicks>

<!--
Timing: 0.5 minutes.

Start with why I brought the spec back. Follow one checkout requirement through the documents I ask AI to maintain. Then tell the story of what happened when it maintained them poorly, and show the checks I built in response. Reserve the last three minutes for questions.

Advance to the section card before starting the history.
-->

---
layout: center
class: text-center
transition: fade
---

# The Spec Returns

<!--
Timing: 0.25 minutes.

“Let’s start with how we wrote down what software should do.”

Pause, then advance.
-->

---
transition: fade-out
class: illustrated
---

# In the Before Times

<div class="before-times-panels">
  <figure v-click="1" class="before-times-panel">
    <div class="before-times-art">
      <img src="/images/development-before-times.png" alt="Rodney writes out a program by hand under a desk lamp." />
    </div>
    <figcaption><strong>Paper</strong>Think it through. Write it down.</figcaption>
  </figure>
  <figure v-click="2" class="before-times-panel" style="--panel-index: 1;">
    <div class="before-times-art">
      <img src="/images/development-before-times.png" alt="Rodney holds up a punched card at a keypunch machine." />
    </div>
    <figcaption><strong>Punch cards</strong>Make every card count.</figcaption>
  </figure>
  <figure v-click="3" class="before-times-panel" style="--panel-index: 2;">
    <div class="before-times-art">
      <img src="/images/development-before-times.png" alt="Rodney presents an oversized specification binder full of diagrams and tabs." />
    </div>
    <figcaption><strong>The '90s spec</strong>The whole system. In a binder.</figcaption>
  </figure>
  <figure v-click="4" class="before-times-panel" style="--panel-index: 3;">
    <div class="before-times-art">
      <img src="/images/development-before-times.png" alt="Rodney gestures at a ticket board while the binder's pages spill out of a shredder." />
    </div>
    <figcaption><strong>The Agile era</strong>The binder becomes a backlog.</figcaption>
  </figure>
</div>

<div v-click="4" class="before-times-punchline">
  Somewhere along the way, we lost the whole picture.
</div>

<!--

Timing: 0.5 minutes.

Reveal paper, punch cards, the binder, and the Agile-era backlog in four clicks. The last click also reveals the punchline. Paper and punched cards often shared a workflow. Formal specs predate the '90s.

When running the program was expensive, we thought it through first. Eventually we had the binder. Behold the binder. Then, in some shops, the binder disappeared and the tickets survived. Agile did not ask us to shred the specification. We scattered the intent. Let's open the binder and see what was useful.

Source: https://agilemanifesto.org/

-->

---
transition: fade-out
class: archive
---

# A Spec, 1998

Purpose, requirements, constraints, design, verification, and approval. Open to one requirement:

```text
3.2.4  Discount Application

REQ-3.2.4.1  The system SHALL apply a valid discount code to the
             order subtotal prior to payment authorization.
REQ-3.2.4.2  The system SHALL reject a discount code whose expiry
             date precedes the current date and SHALL display
             message MSG-017 ("This code has expired").
REQ-3.2.4.3  The system SHALL NOT apply more than one discount
             code per order.
REQ-3.2.4.4  Orders with a subtotal of $75.00 or greater SHALL
             receive standard shipping at no charge.

Source:       BR-12 (Q3 Promotions)
Verified by:  TC-3.2.4.1, TC-3.2.4.2, TC-3.2.4.3, TC-3.2.4.4
Out of scope: Stacking, gift cards, loyalty points (see 3.2.7)
```

<!--

Timing: 1 minute.

This is an illustrative 1990s-style Functional Requirements Specification. Picture the whole binder: purpose and scope, requirements, constraints, design, verification, and approval. This page is one requirement inside it. The numbered claims, SHALL language, upstream source, and downstream test references are the useful parts. It is a fictional checkout example, not a historical document or a claim of compliance with a particular standard.

Read REQ-3.2.4.2 out loud. That is a test. Written by someone who never wrote a test.

-->

---
transition: fade-out
---

# What the Binder Got Right

- **Purpose** explains why the work matters.
- **Stable IDs** let us refer to the same requirement.
- **Boundaries** spell out what to preserve and what to leave out.
- **Traceability** links requirements to the tests that check them.

<v-click>

<div m="t-6">

Keeping it current was the hard part.

</div>

</v-click>

<v-click>

**In 2026, AI can help with the upkeep.**

</v-click>

<!--

Timing: 0.75 minutes.

Go back to the expired-code requirement. We can point to its ID, see what checkout must do before payment, and find the test that is supposed to check it. The binder made those connections visible while they were current.

Click for the maintenance cost. When the behavior changed, somebody had to update the requirement, the examples, and the design decisions. The binder fell behind.

Click to turn into the AI-powered part. “I brought the spec back because AI can help keep it current as the code changes.”

-->

---
transition: fade-out
class: illustrated
clicks: 1
---

# The Spec Is Back

<div class="sdd-spec-panels">
  <figure>
    <img src="/images/specification-old-busted-hotness.png" alt="Rodney in a patterned sweater beside a dusty specification binder." style="object-position: left;" />
    <figcaption>
      <strong>Old busted</strong>
      <b>The specification</b>
      Write it. File it. Forget it.
    </figcaption>
  </figure>
  <figure v-click="1">
    <img src="/images/specification-old-busted-hotness.png" alt="Rodney in a hoodie surrounded by chat bubbles and crumpled drafts." />
    <figcaption>
      <strong>New hotness</strong>
      <b>AI chat</b>
      Prompt it. Patch it. Repeat.
    </figcaption>
  </figure>
  <figure v-click="1">
    <img src="/images/specification-old-busted-hotness.png" alt="Rodney shows the specification to a robot beside a laptop, with diagrams, code, and test checkmarks." style="object-position: right;" />
    <figcaption>
      <strong>Old busted hotness</strong>
      <b>AI helps maintain the spec</b>
      Update the agreement alongside the code.
    </figcaption>
  </figure>
</div>

<div v-click="1" style="margin-top: 1rem; text-align: center; font-style: italic;">
  I can ask the agent to maintain the spec in the same change as the code.
</div>

<!--

Timing: 1.5 minutes.

Start with the dusty binder. “The old busted spec has a new job. I call it old busted hotness.” Pause, then click once to reveal the other two panels together.

The middle panel is what happened when I asked AI for code and kept the decisions in chat. I had to keep repeating what mattered. In the right panel I can ask the agent to update the spec, the examples, and the design decisions as the code changes. The next change can start with the reviewed files. The ability to keep those files current is why I use a spec again.

“I give the agent room to work because we’ve agreed what the software should do and what evidence I’ll review. I spend less time steering each edit and more time making the decisions that matter.”

Advance directly to “My Specification, 2026.” Open the first of the files I ask the agent to maintain.

-->

---
transition: slide-up
---

# My Specification, 2026

**`.spec/README.md`** · The versioned agreement for checkout

<div grid="~ cols-2 gap-8" m="t-4">
<div>

**Intent and boundaries**

- **Goal** · Keep checkout pricing clear.
- **Claim `AC-002`** · Reject an expired code and show a message.
- **Constraint** · Reject before payment starts.
- **Out of scope** · Stacking discount codes.

</div>
<div>

**Design and evidence**

- **§ 7 · Technical design** · Link to the payment decision in `ADR-0001`.
- **§ 8 · Testing strategy** · Exercise checkout with an expired code.
- **§ 9 · Traceability** · Link `AC-002` to its scenario and test.

</div>
</div>

**SDD** gives the agent a reviewed agreement to work from and update with me.

<!--
Timing: 1.5 minutes.

Open the spec document. This is the 1998 expiry requirement carried forward as AC-002. Read the goal, the claim, and the before-payment constraint. The spec gives the agent a place to start and tells it where to record evidence or propose a change. The headings on screen are selected from my full feature template.

When the behavior changes, I can review the spec and code diffs together. Next show the metadata that distinguishes agreeing on the behavior from finishing the work. Then open the feature file and the design decision that this spec points to.

Source: Specht src/tool/templates/v0.3.0/feature.md. The checkout example is illustrative.
-->

---
transition: slide-up
---

# Frontmatter Is Part of the Contract

**`.spec/README.md`** · Agreement and delivery are tracked separately

<div grid="~ cols-2 gap-8" m="t-4">
<div>

```yaml
---
id: F2
epic: "0001"
type: feature
spec_status: approved
status: in-progress
depends_on: []
blocks: []
---
```

Excerpt for the checkout feature.

</div>
<div style="align-self: center;">
  <p style="border-left: 3px solid var(--sdd-copper); padding-left: 1rem;">
    <code>spec_status</code><br />
    <strong>Agreement approved</strong>
  </p>
  <p style="border-left: 3px solid var(--sdd-copper); padding-left: 1rem;">
    <code>status</code><br />
    <strong>Delivery in progress</strong>
  </p>
</div>
</div>

<!--
Timing: 1 minute.

The YAML block is the frontmatter of the same spec. Here we have approved what checkout should do, and delivery is still in progress. I can ask an agent to keep delivery metadata current while I review changes to the agreement. These are selected fields from the full schema.

We have agreed on the claim. Next create the feature file and record how checkout enforces the payment boundary.

Source: Specht src/tool/schema/v0.3.0/frontmatter/feature.schema.json.
-->

---
layout: center
class: text-center
transition: fade
---

# One Claim at Work

<!--
Timing: 0.25 minutes.

“Let’s turn that checkout claim into a feature file and record the design decision behind it.”

Pause, then advance.
-->

---
layout: two-cols
layoutClass: gap-8
class: claim-example
---

# Create the Feature File

**From the spec**

`AC-002` says checkout must reject an expired code before payment.

**BDD makes the behavior concrete**

- What should the shopper see?
- Does the cart total change?
- Does payment start?

::right::

**`checkout.feature` · Gherkin**

```gherkin
Feature: Discount codes

  @AC-002
  Scenario: Reject an expired code
    Given a cart worth $50
    And SAVE10 expired yesterday
    When I check out with SAVE10
    Then the total remains $50
    And an expired-code message appears
    And no payment authorization starts
```

<!--
Timing: 1.5 minutes.

Open checkout.feature. The feature is the checkout capability. The feature file is where we write examples of its behavior. Gherkin gives us Given for the starting state, When for the checkout action, and Then for what we expect to observe. The AC-002 tag links this scenario to the agreement.

Creating this file forces me to settle the answers before I implement anything. Checkout keeps the $50 total, shows the expiry message, and leaves payment untouched. That’s behavior-driven development, or BDD. Next record which part of the system owns that decision.

Source: https://cucumber.io/docs/gherkin/reference/
-->

---
transition: slide-up
---

# Record the Architecture Decision

**`.spec/adr/0001-reject-before-payment.md`**

- **Context** · `AC-002` requires checkout to reject expired codes before payment.
- **Decision** · Checkout checks expiry before it calls the payment gateway.
- **Reason** · Checkout owns discount rules. The gateway handles authorization.
- **Evidence** · The scenario checks that an expired code triggers zero authorization calls.

<!--
Timing: 1.25 minutes.

Open the architecture decision record from the spec's technical design section. The spec says what must happen, and the scenario makes it observable. The decision records where to enforce it and why.

When the agent implements the feature, the boundary gives it a place to put the change. If we move that responsibility later, I want the reasoning updated in the same change. This is an illustrative checkout decision. Now use the scenario to drive the first test.
-->

---
level: 2
---

# Use the Feature File to Drive TDD

<ol class="sdd-handoff-flow" role="list" aria-label="How a Gherkin example drives implementation">
  <li>
    <strong>Make it executable</strong>
    Bind the scenario’s setup, action, and expected results
  </li>
  <li v-click>
    <span class="sdd-handoff-arrow" aria-hidden="true">→</span>
    <strong>Run it red</strong>
    Run it and see the behavior assertion fail
  </li>
  <li v-click>
    <span class="sdd-handoff-arrow" aria-hidden="true">→</span>
    <strong>Make it green</strong>
    Implement until the same test passes, then refactor
  </li>
</ol>

<p v-click class="sdd-handoff-return"><span aria-hidden="true">↶</span> If the example exposes a gap, clarify the agreement first.</p>

<!--
Timing: 1.5 minutes.

This is where I use the Gherkin example for test-driven development. Step definitions connect the Given and When to the checkout setup and action. The Then definitions assert the $50 total, the expired-code message, and zero payment authorization calls.

Reveal red. The scenario executes, but the existing checkout still accepts the expired code. That assertion failure is the red I’m looking for. An undefined step means I still need to wire up the example.

Reveal green. The architecture decision tells me where to make the change. Implement the expiry check before the gateway call, then refactor while the same test stays green. If the example exposes a gap, agree on the behavior before changing the code.
-->

---
transition: slide-up
---

# What I Ask AI to Maintain

**When checkout behavior changes, I review the code and its documentation together.**

- **Spec document** · Update the approved behavior and its traceability.
- **Feature file** · Revise or add the Gherkin scenario that exercises it.
- **Architecture decision** · Update the reasoning when the design changes.

<v-click>

**Then AI maintained the files poorly, and the agreement drifted.**

</v-click>

<!--
Timing: 1.5 minutes.

This is the part that makes the spec practical for me again. AI can prepare the related edits in one change. I still decide what the software should do and review the diff. If we agree to reject at the exact expiry instant, the claim and scenario need updating. I revisit the architecture record if the payment boundary changes.

Pause before the reveal. I tried this on a real project. The agent revised the spec, and the test author copied a requirement that no longer matched the design. Other links went missing. That is the review where I burned through my tokens.
-->

---
layout: center
class: text-center
transition: fade
---

# My Precious Tokens

<div style="margin-top: 2rem; font-size: 1.8rem; line-height: 1.5; color: var(--sdd-copper);">
  “I burned a week's worth of Claude Max tokens<br/>trying to review a pull request.”
</div>

<div style="margin-top: 1.5rem; color: var(--sdd-muted);">AI revised the documentation, and the agreement drifted.</div>

<!--
Timing: 1 minute.

This is my account of a private-project PR. I had asked AI to maintain the spec as the implementation changed. The review kept coming back to metadata, wording, and broken links. Here is what the surviving artifacts tell us about that review.
-->

---
transition: slide-up
class: artifacts
---

# The Files Behind the Workflow

**My repository layout**

<div role="group" aria-label="Checkout contains feature code and a dot-spec folder with the README, scenarios, architecture decisions, product decisions, and lessons. Dot-specht sits at the repository root.">

```text
repo/
├── checkout/
│   ├── …                     Feature code
│   └── .spec/
│       ├── README.md         Agreement and claim links
│       ├── checkout.feature  Scenarios
│       ├── adr/              Design decisions
│       ├── decisions/        Product decisions
│       └── lessons/          What to change next time
└── .specht/                  Checker config, schemas, templates
```

</div>

<!--
Timing: 0.5 minutes.

Use the checkout tree to orient the room. The agreement, tagged scenarios, decisions, and lessons live beside the feature. The repository’s .specht folder holds the checker configuration. The private project used the same kinds of artifacts for a replay command.

Sources: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/setup.md and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/what-it-checks.md
-->

---
transition: slide-up
---

# The Claim in This Incident

**Replay requirement · Intended behavior**

> If the store is busy or unreadable, report an environment failure and exit with category **3**.

Watch what happens to claim `AC-011` when the spec is revised.

<!--
Timing: 0.5 minutes.

For the next three slides, switch from the checkout example to the real replay incident. A busy store is an environment failure, category 3. A rejected request is category 1. The slide paraphrases the intended behavior and displays the private B-011 ID as AC-011. The evidence notes retain the original IDs and wording.
-->

---
transition: fade-out
---

# The Spec Started Drifting

**Actual review evidence: a replay command**

| Same failure: store busy or unreadable | Exit category |
| -------------------------------------- | ------------- |
| Revised claim `AC-011`                 | **1**         |
| Design § 7, `R3` / `R13`               | **3**         |
| Test stub's expected result            | **1**         |

```cs
// Then a structured failure is reported and the exit code is category 1
```

The incorrect claim propagated into the test scaffold.


<!--
Timing: 1.5 minutes.

The revised claim put the busy or unreadable store in category 1. The design still mapped it to category 3. The test author copied category 1 into the scaffold. The quoted line is verbatim. The scaffold body failed as unimplemented, and a reviewer flagged the contradiction. Pause on how quickly the wrong expectation propagated.

Source provenance: pr-evidence.md, “A wrong claim propagated into a scaffold.”
-->

---
transition: slide-up
---

# The References Drifted Too

**Actual review evidence: the original and follow-up PRs**

- **Original PR:** five accepted claims, zero traceability rows
- **Follow-up PR:** scenario tagged `@AC-011`; the spec says `AC-003` governs it
- **Follow-up PR:** the scenario appeared under neither claim in § 9

**The verified correction — IDs shown as `AC-`**

```diff [$ git diff]
-    @AC-011
+    @AC-003
```

The correction also added the scenario to `AC-003`'s traceability row.

<v-click>

**A wrong reading can spread into the claim, the test, and a false sense of confidence.**

</v-click>


<!--
Timing: 1.5 minutes.

These are separate findings. The first PR lacked traceability rows for five accepted claims. The follow-up had a scenario linked to the wrong existing claim and absent from both matrix rows. The correction moved the tag and added the scenario to AC-003’s row. A checker can see an absent row or an inconsistent link. A reviewer still has to read the behavior and decide where the scenario belongs.

Hold onto one more detail for later. Even the lessons from the review contained mistakes.

Source provenance: pr-evidence.md, “Missing rows and a mismatched claim tag.”
-->

---
layout: center
class: text-center
transition: fade
---

# Author. Reviewer. Repeat.

**My account of the review effort by the time it was done**

| Review effort            | Scale                              |
| ------------------------ | ---------------------------------- |
| Comments on the PR       | More than 200                      |
| Reviews away from GitHub | About half                         |
| AI reviews               | 20+                                |
| Rounds per AI review     | 10+; highest I recall: 18          |

<div style="margin-top: 1rem; font-size: 1.3rem; color: var(--sdd-copper);">Burning tokens like cash to keep warm.</div>

<v-click>

**Wait. I can build a thing to check these rules.**

</v-click>

<!--
Timing: 1.5 minutes.

The numbers are my account, including reviews away from GitHub. A review could take several rounds. The private PRs preserve the concrete findings and fixes. The original closed, and the follow-up eventually merged.

Pause on “burning tokens like cash to keep warm.” Then reveal the realization. I could turn an explicit rule we kept rediscovering into a check that ran before PR review. Advance to the next section card.

Source provenance: pr-evidence.md preserves the reviewed artifacts and the reported effort separately.
-->

---
layout: center
class: text-center
transition: fade
---

# Who Checks the Spec?

<!--
Timing: 0.25 minutes.

“I kept paying for another review. What could I make the build catch?”

Pause, then advance.
-->

---
transition: slide-up
clicks: 4
---

# SDD Inception

```mermaid {key: $clicks, scale: 0.75, themeCSS: ['.sdd-step-1', '.sdd-step-2, .flowchart-link[data-id=edgeAB]', '.sdd-step-3, .flowchart-link[data-id=edgeBC]', '.sdd-step-4, .flowchart-link[data-id=edgeCD]'].map((selector, index) => selector + ' { visibility: ' + ($clicks > index ? 'visible' : 'var(--sdd-unrevealed-diagram-content, hidden)') + '; }').join(' ')}
graph LR
accTitle: How Specht grew out of SDD
accDescr: AI maintained the specification as the software changed. The specification and its links drifted. SDD guided the development of Specht so the build could check explicit rules.
A["Ask AI to<br/>maintain the spec"] edgeAB@--> B["Spec and<br/>links drift"]
B edgeBC@--> C["Use SDD<br/>to build Specht"]
C edgeCD@--> D["Check explicit<br/>rules in the build"]
class A sdd-step-1
class B sdd-step-2
class C sdd-step-3
class D sdd-step-4
```

<v-clicks :at="2">

- Fix the repository where the drift happened
- My request: **“Think of the spec like a JSON Schema.”**
- **And Specht was born.**

</v-clicks>

<!--
Timing: 1.25 minutes.

Reveal the four boxes from left to right. The drift and repository-fix bullet appear together, then the schema request, then Specht. Pause on “Think of the spec like a JSON Schema.” I built Specht to check the parts of upkeep we could write down as rules, such as required fields and links. I still need to read the spec to decide whether the behavior is right. I used the same SDD approach to build the tool.

Return to AC-002. The replay claim’s missing-row problem is the same one we can see on a checkout claim without reopening the private code.
-->

---
transition: slide-up
---

# Meet Specht

<img src="/images/specht-book-logo-dark.svg" alt="Specht logo: a woodpecker perched on a book above the SPECHT wordmark" class="sdd-logo" />

<div style="max-width: 530px;">

A .NET analysis tool I built to keep tabs on the specification process.

<v-clicks>

- YAML frontmatter against its schema
- Required sections and their order
- Claim IDs, references, and traceability
- Declared dependency consistency

</v-clicks>

<div style="margin-top: 1.5rem; color: var(--sdd-copper);">Run after edits. Carry findings into the next handoff.</div>

[github.com/RLittlesII/specht](https://github.com/RLittlesII/specht)

</div>

<!--
Timing: 1 minute.

Pin the tool and schema so the agent and CI can run the same structural checks. A finding comes back with a path, line, and rule. For checkout, the first thing to check is whether AC-002 has a traceability row at all.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/what-it-checks.md
Logo: Specht’s existing dark-background SVG.
-->

---
transition: slide-up
---

# AC-002 Needs a Traceability Row

**Checkout example · Specht finding**

```text [$ dotnet tool run specht]
SPEC4004 · claim 'AC-002' has no row in § 9
```

<v-click>

**Restore the row and record the test gap.**

| Claim ID | Scenario               | Test    | Status  |
| -------- | ---------------------- | ------- | ------- |
| AC-002   | Reject an expired code | Missing | Missing |

</v-click>

<!--
Timing: 1.5 minutes.

This checkout excerpt uses schema 0.3.0 with AC- IDs configured in the manifest. The displayed finding is abbreviated. Specht points at the claim row in § 3 and expects exactly one row for that claim in § 9.

Click to restore the relationship. The tagged scenario already exists. Record Missing in the test and status cells until we have a test. In schema 0.3.0, SPEC7001 reports Missing coverage when delivery is marked done. The earlier approval of intent can stand while delivery is in progress.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/rules/v0.3.0/SPEC4004.md and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/what-it-checks.md
-->

---
transition: slide-up
---

# From Red to Green

**Checkout example · `@AC-002` rejects an expired code**

```csharp
var result = await checkout.SubmitAsync("SAVE10");

Assert.Equal(50m, result.Total);
Assert.Equal("This code has expired", result.Message);
Assert.Equal(0, payment.AuthorizationCalls);
```

<v-clicks>

- **Red** · The $50 cart accepts the expired code. The total assertion fails.
- **Implement** · Check expiry before applying the discount or starting payment.
- **Green** · The same test passes all three assertions.

</v-clicks>

<!--
Timing: 2 minutes.

Illustrative test body. The omitted setup uses a $50 cart, a fixed clock, SAVE10 expired yesterday, and a payment spy. The scenario expresses the BDD example. Its bindings and runner can execute these observations, or a directly linked xUnit test can express them as shown here.

Reveal the red result, then the scoped implementation, then green on the same test, ExpiredCode_IsRejectedBeforePayment. Stop at the first red assertion and explain that the green run has to get past all three. Replace the Missing test cell with the actual test name after it exists. Next make the links visible in C#.
-->

---
transition: slide-up
---

# Roslyn Checks the Links in C#

**Checkout example · Implementation and test declarations**

```csharp
/// <implements epic="0001" feature="F2" claim="AC-002" />
public Task<CheckoutResult> SubmitAsync(string code);

/// <verifies epic="0001" feature="F2" claim="AC-020" />
public Task ExpiredCode_IsRejectedBeforePayment();
```

<v-click>

**`SPCT0004`** · The test refers to `AC-020`, which the feature does not declare.

</v-click>

<!--
Timing: 2 minutes.

Illustrative C# declaration excerpts from separate files. Specht.CodeAnalysis supplies the selected spec inputs to Roslyn. The analyzer reads the XML links on the C# declarations and checks their targets against the claim catalogue. Here the feature is F2 in epic 0001, and AC-020 fits the configured grammar but does not exist.

Reveal the diagnostic. Compare the test with AC-002 and correct the link. A conditional code action can offer an eligible target. The developer or reviewer still chooses the one that expresses the test’s intent. Next check what actually ran.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/codeanalysis/docs/SPCT0004.md
-->

---
transition: slide-up
---

# A Link and a Run Are Different Evidence

**Checkout example · Build, discover, run, reconcile**

```sh [Terminal]
dotnet build test/Checkout.Tests.csproj
dotnet test test/Checkout.Tests.csproj --no-build --list-tests
dotnet test test/Checkout.Tests.csproj --no-build
specht evidence test/obj/specht-test-evidence
```

| Evidence                     | What I can tell                     |
| ---------------------------- | ----------------------------------- |
| Link resolves                | The test points to the claim        |
| Current run passed or failed | What the discovered test did        |
| Export is stale              | I need evidence for this revision   |

<!--
Timing: 2 minutes.

The commands illustrate the sequence for a configured xUnit v2/VSTest project with the Specht logger. Start with the build so Roslyn can read the declarations. On a clean checkout, test recognition can be provisional until the project supplies current discovery and run evidence. Discovery records cases. Execution records their outcomes. The evidence command reads those artifacts and checks required links, input freshness, and results.

If I edit the assertions after a passing run, I rerun against the new revision. A valid verifies link can survive that edit while the old pass goes stale.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/cli.md#specht-evidence and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/codeanalysis/README.md
-->

---
transition: slide-up
---

# Specht Uses the Same Approach

**`0201-F7/B-030` · Carry the native claim catalogue into test evidence**

| Artifact       | In Specht’s repository                          |
| -------------- | ----------------------------------------------- |
| Claim          | Export the native Gherkin catalogue             |
| Tagged example | `@B-030` · Catalogue exported with the evidence |
| Implementation | `TestEvidenceCatalogExporter`                   |
| Linked test    | Collector test reads the artifact back          |

```xml
<implements epic="0201" feature="F7" claim="B-030" />
<verifies epic="0201" feature="F7" claim="B-030" />
```

<!--
Timing: 2 minutes.

This is the public Specht repository at v0.4.2-preview. B-030 is Specht’s own claim. Its tagged example checks that the export includes the arithmetic claim and both example rows. The exporter projects the catalogue, and the explicitly linked collector test checks that the emitted artifact retains the catalogue. The XML lines come from separate source and test files.

Consumers can choose tagged Gherkin as their native claim input. In that mode the selected .feature files supply the catalogue instead of the Markdown claims table. Checkout used Markdown with a tagged companion. Point out the same stable identity across the two workflows.

Sources:
https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/analyzers/Bindings/Gherkin/.spec/README.md#L89
https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/analyzers/Bindings/Gherkin/.spec/gherkin.feature#L237
https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/analyzers/TestEvidence/TestEvidenceCatalogExporter.cs#L9
https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/test/testevidence.mtp.tests/TestEvidenceMtpCollectorCatalogTests.cs#L11
-->

---
transition: slide-up
---

# Let dotnet format Do the Heavy Lifting

**Specht’s C# policy · One-line XML summaries**

```diff [$ git diff]
-/// <summary>
-/// GitHub Actions related methods.
-/// </summary>
+/// <summary>GitHub Actions related methods.</summary>
```

<v-click>

```sh [Apply eligible fixes locally]
dotnet format specht.slnx --no-restore
```

</v-click>

<v-click>

```sh [CI verifies the policy and reports drift]
dotnet format specht.slnx --verify-no-changes --no-restore
```

</v-click>

<!--
Timing: 2.5 minutes.

This is the actual summary change in Specht PR #702. The repository opts into Roslynator RCS1253 in .editorconfig. Roslyn surfaces configured diagnostics and code fixes. Let dotnet format do the heavy lifting: it uses the configured policy and applies eligible batch fixes across the selected C# files. Review the resulting diff.

The second command is the C# part of the build’s verify-only Format target. CI reports changes it would make and leaves the files as they are. For the broken claim link on the earlier slide, choosing a target remains a separate decision. The test runner still supplies behavior evidence.

Sources: https://github.com/RLittlesII/specht/pull/702 and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/building.md
-->

---
transition: slide-up
---

# I Can Sign Off on AC-002

<v-clicks>

- **Agreed intent** · Reject the expired code before starting payment
- **Inspectable change** · Traceability row, test link, and scoped code diff
- **Current evidence** · $50 total, expired-code message, zero payment calls

</v-clicks>

<v-click>

**I can give the agent room to work and come back to evidence I can check.**

</v-click>

<!--
Timing: 1.5 minutes.

At sign-off I read the approved behavior, look at the implementation and test diff, and verify that the three assertions in ExpiredCode_IsRejectedBeforePayment ran on this revision. The test has replaced Missing in the matrix. Product owns a change to the requirement. The tester challenges the example. Development repairs code and links. The reviewer decides whether the evidence is enough to accept.

“I give the agent room to work because we’ve agreed what the software should do and what evidence I’ll review. I spend less time steering each edit and more time making the decisions that matter.”

If a check fails repeatedly, send the exact finding and attempted fixes to the person who owns the next decision.
-->

---
layout: center
class: text-center
transition: fade
---

# Make It Your Workflow

<!--
Timing: 0.25 minutes.

“You’ve seen what I get back. What would your team need before signing off?”

Pause, then advance.
-->

---
level: 2
---

# Make Lessons Change the Next Run

`L-0009` misquoted the signatures it was teaching.

**Example response**

<ol class="sdd-handoff-flow" role="list" aria-label="How the lesson changes the next run">
  <li v-click>
    <strong>Verify the lesson</strong>
    Compare the signatures with source
  </li>
  <li v-click>
    <span class="sdd-handoff-arrow" aria-hidden="true">→</span>
    <strong>Update the skill</strong>
    Check quotes on signature changes
  </li>
  <li v-click>
    <span class="sdd-handoff-arrow" aria-hidden="true">→</span>
    <strong>Review the next run</strong>
    Reopen the source and inspect results
  </li>
</ol>

<!--
Timing: 2 minutes.

Back to the review nightmare. A lesson about keeping signatures consistent managed to misquote both signatures. Wonderful. Now the next agent has a beautifully documented way to learn the wrong thing.

That is the actual L-0009 finding. A later reply says the finding was recorded in L-0012 while L-0009 stayed unchanged. We tracked the correction without doing it. The broader incident also included L-0007's unsupported historical example; keep that in the evidence notes rather than introducing a second case here.

Reveal the three steps as an example response. First, a person verifies the finding and corrects the lesson. Compare quoted code with its source before teaching from it. On the second click, show the procedure the skill runs when a signature changes.

1. Open the port, affected callers, and relevant lesson.
2. Compare the lesson's quotes with current source.
3. Fix stale quotes, then build and run the affected tests.
4. Return the revision, source paths, diff, and test results.

On the third click, show what the reviewer checks on the next run. Build and test results describe the code. The source comparison verifies the quote.

Return the revision, source paths and symbols, repaired lesson diff, and actual results. The next reviewer opens those symbols at that revision and compares the quote. Still wrong? Return the finding and correct the procedure before repeating. The observed failure becomes a verified lesson, then a skill the next task applies and the reviewer can inspect. For AC-002, apply the same habit when the payment interface changes. Include the current interface, the checkout test that checks zero authorization calls, and the fresh result in the handoff.

Load the procedure for relevant tasks; a skill sitting unread in a folder changes nothing. The checks and next reviewer make its application inspectable. Next, show what that handoff needs to carry.

Source provenance: see pr-evidence.md for the reviewed artifacts, original PR identifiers, and pinned source links.
-->

---
level: 2
---

# Make Every Handoff Inspectable

```mermaid {scale: 0.7}
graph LR
accTitle: Code context for a requirement
accDescr: A claim links to a scenario, its test, and existing code. The existing code links to dependencies and a canonical example.
R[Claim] --> S[Scenario]
S --> T[Test]
T --> C["Existing code"]
C --> D[Dependencies]
C --> E["Canonical example"]
```

<v-clicks>

- Start with the approved revision of `AC-002`
- Link the test and checkout diff, with results from that code revision
- Name who resolves a conflict before the next handoff

</v-clicks>

<!--
Timing: 1.5 minutes.

The next team needs the approved requirement and enough code context to find the affected implementation and dependencies. This graph is a conceptual map, not a Specht feature. Paths and symbols can supply the links.

For a distributed team, 'I have the latest spec' is not enough. Attach the source ID and revision or captured timestamp, code revision, diff, and current results. If intent lives outside Git, those are two baselines. Check the source again before acceptance and reconcile changes with its owner.

Two teams can edit the same requirement or dependent code. Name who resolves the conflict, where the resolution is approved, and which checks must repeat. Another team should be able to inspect the same baseline and see the next decision.
-->

---
transition: slide-up
---

# Upsides and Downsides

<div grid="~ cols-2 gap-8" m="t-4">
<div>

**Upsides**

- Less repeated steering
- Better context for autonomous work
- Faster implementation of agreed behavior
- Lessons improve the next task

</div>
<div>

**Downsides**

- Walls upon walls of text to maintain
- Duplication that AI review can miss
- Keeping a distributed team in sync
- Tokens, build time, and review capacity

</div>
</div>

<!--
Timing: 1.5 minutes.

The tradeoff is explicit in checkout. We spend time writing one good example and keeping the links current. The agent can reuse that decision instead of asking me to reconstruct it each time. Builds and tests take time, and repeated evidence should earn its place.

Optional anecdote. A buddy saw the first version of this talk and showed me working software three days later. That was the interval between our conversations. I have no comparison with how he would otherwise have worked.
-->

---
transition: slide-up
---

# Where the Source of Truth Lives

<div style="color: var(--sdd-copper); font-weight: 700;">Team decisions</div>

| Authority             | How the agent gets context                      |
| --------------------- | ----------------------------------------------- |
| Repository            | Read the approved spec at a commit              |
| Issue or work tracker | Read the approved item and capture its version  |
| Repository publishes  | Read the spec; a build task updates the tracker |

<v-clicks>

- People outside the repository need a readable view and an approval path
- Choose who can change intent and how conflicting edits get resolved

</v-clicks>

Start with one changed area. You do not need to backfill the whole system.

<!--
Timing: 2.25 minutes.

Everything in my example lives in the repository. Your organization may have approved requirements in GitHub Issues, Azure DevOps, or a Trello board. We do not always own that choice. We may have to improvise around the source of truth we actually have.

If the repository owns the requirement, a commit pins the baseline alongside code and tests. The downside is access: people outside the repository, often people who are not developers, need a readable view and a way to propose and approve changes.

If the tracker owns the requirement, retrieve the approved item. Keep its ID, link, and version with the context. If it lacks a useful revision identifier, capture the approved content and timestamp. Local excerpts point back to that source. Check for upstream changes before acceptance and have the owner resolve differences.

Another option I have considered: the repository stays authoritative and a build task creates or updates Azure DevOps work items. Stable mappings let retries update the same item. Show the source revision and any failed or stale publication. The wider team gets a usable view. Route board edits back to the spec owner for approval, or we have made two competing sources of truth. Decide who owns requirement wording, work status, and approval. These are integration options, not Specht features or a pipeline I claim to have built.

For a distributed team, name who resolves conflicts and make the approved version reachable by everyone who needs it. Synchronization is work in every variation.

I am using SDD on a large existing project today. Start from available requirements wherever they live. When fixing a bug or changing an area, clarify its intent, add criteria and scenarios, and retain the checks. Backfill as you go. Examine existing behavior with its owner before treating it as a requirement. Coverage stays partial while you expand.

“Once you know which requirement governs the change, decide what evidence you need before the handoff.”

Source: Rodney's account of current use and proposed process variations, supplied for this talk. No measured benefit or completed tracker integration is claimed.
-->

---
transition: slide-up
---

# When Does AC-002 Expire?

**Proposed change · Reject at the exact expiry instant**

```mermaid {scale: 0.7}
graph LR
accTitle: Recheck the changed expiry claim
accDescr: The product owner approves the exact expiry boundary. The agent updates the scenario, test, and code, then runs checks. The reviewer inspects current results. Failed checks return to the edits.
A["Approve the<br/>expiry boundary"] --> B["Update scenario,<br/>test, and code"]
B --> C["Rerun checks<br/>and tests"]
C --> D["Review current<br/>results"]
C -->|repair| B
```

- Add an example with the fixed clock **exactly at** the expiry timestamp
- Run the relevant checks during the task and again in CI
- Attach the approved spec revision and fresh run results

<!--
Timing: 2.5 minutes.

Yesterday’s expired code did not settle equality at the expiry instant. Ask the domain owner to approve the exact boundary. Show that the approved revision changes first, then the example and test. Keep the same claim ID and retain the yesterday example. Check the boundary against a fixed clock, run the relevant checks during the task, and give the reviewer the result from this revision. CI independently checks the shared change.

A demonstrated red test can be the handoff to implementation. The final handoff needs a green result for the agreed behavior. If the same failure repeats or a required check cannot run, pause and give its owner the baseline, latest finding, and attempted repair.
-->

---
transition: slide-up
---

# What I Review Before I Accept

<v-clicks>

- What we agreed `AC-002` should do
- What the agent changed and which links the build checked
- What the current run showed about price, message, and payment
- Who owns the next decision if the evidence falls short

</v-clicks>

<!--
Timing: 1 minute.

Return to the opening. I still do the thinking about the behavior and inspect the evidence that matters. I can let the agent work through the routine edits because I know what it will bring back. For the audience’s first task, ask them to picture one claim they can follow this way and one recurring finding a program could check.

Pause, then advance to Questions.
-->

---
layout: center
class: text-center
transition: fade-out
---

# Questions

<div style="margin-top: 1.5rem; color: var(--sdd-copper); font-size: 1.2rem;">
  Which recurring review finding could a program check in your workflow?
</div>

<div style="margin-top: 2rem;">

**What would you check, what evidence would you keep, and who would act?**

</div>

<div style="margin-top: 2rem;">

**Live SDD demos on Twitch this December**<br>
[twitch.tv/rlittlesii](https://twitch.tv/rlittlesii)

It'll be boring. AI will be at the helm. &#58;D

</div>

<!--
Timing: 3 minutes. End at 50 minutes including questions.

Thank the audience. Use the prompt if questions need a starting point: a missing ID, broken relationship, stale reference, or dependency mismatch from their own process. Distinguish semantic disagreements from checks with an explicit rule and a reproducible answer.

Before wrapping up, invite the audience to twitch.tv/rlittlesii throughout December. I'll demonstrate this specification-driven approach live, with AI at the helm. It'll be boring because AI will be doing the work. :D

If asked about installing, use the current README link and follow its current installation instructions. If asked whether validation solves wrong requirements, return to the exit-category contradiction in #214. It does not: humans review intent and tests exercise agreed behavior. If asked about the token bill, stick to the reported experience; no measured savings comparison was presented.
-->
