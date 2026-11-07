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

<div v-click class="sdd-title">
What it cost me to let AI drive the spec
</div>

<div v-click class="sdd-subtitle">
  Who checks the spec?
</div>

<!--
Timing: 0.5 minutes.

I like talking about my failures. They’re a surprisingly reliable source of material.

“Who checks the spec?” I thought my review process had an answer. Then one replay claim gave the test writer the wrong answer. I’ll follow that claim through the review and show which checks I could move into the build.
-->

---
transition: slide-up
---

# Agenda

<ol class="sdd-route" role="list" aria-label="Five stops in the talk">
  <li v-click><span aria-hidden="true">01</span>The spec returns</li>
  <li v-click><span aria-hidden="true">02</span>One claim at work</li>
  <li v-click><span aria-hidden="true">03</span>My precious tokens</li>
  <li v-click><span aria-hidden="true">04</span>Who checks the spec?</li>
  <li v-click><span aria-hidden="true">05</span>Make it your workflow</li>
</ol>

<!--
Timing: 0.5 minutes.

Start with why I brought the spec back. Follow one replay requirement through the documents I asked AI to maintain. Then tell the story of where they disagreed, what review cost, and which checks I could take out of that loop. Explain the concerns I am solving in this process so you know how to consume it in your processes.

Advance to the disclaimer, then the section card.
-->

---
layout: center
class: text-center
transition: fade
---

# Disclaimer

> Don't go to work tomorrow and pitch this process.

<!--
Timing: 0.25 minutes.

Try this on one change you already have to make. One claim is enough to see what the extra upkeep buys you.

I am going to tell you a very real story about how this can go wrong, this is not a silver bullet, I think AI empowers us to use a good artifact from the past.  Not without pitfalls!
-->

---
layout: center
class: text-center
transition: fade
---

# The Spec Returns

<p class="text-xl">The relic of our ancient past comes back with new problems to manage</p>

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
    <figcaption><strong>The Specification</strong>The whole system. In a binder.</figcaption>
  </figure>
  <figure v-click="4" class="before-times-panel" style="--panel-index: 3;">
    <div class="before-times-art">
      <img src="/images/development-before-times.png" alt="Rodney gestures at a ticket board while the binder's pages spill out of a shredder." />
    </div>
    <figcaption><strong>Agile</strong>The binder becomes a backlog.</figcaption>
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
---

# A Spec, 1998

<div class="sdd-story">
  <img src="/images/binder-requirement.jpg" alt="Rodney opens a thick requirements binder and points to a highlighted line under a desk lamp." width="1254" height="1254" />
  <div>
    <p class="sdd-eyebrow">Illustrative 1990s-style specification</p>
    <blockquote class="sdd-archive-quote">
      <p><code>REQ-3.2.4.2</code></p>
      <p>If the event store is unavailable, the replay utility SHALL report an environment error and exit with code 3.</p>
    </blockquote>
    <p>Verified by <code>TC-3.2.4.2</code></p>
  </div>
</div>

<!--
Timing: 1 minute.

This is an illustrative 1990s-style Functional Requirements Specification. Picture the whole binder: purpose and scope, requirements, constraints, design, verification, and approval. This page is one requirement inside it. The numbered claims, SHALL language, upstream source, and downstream test references are the useful parts. I’ve put the replay requirement in the style of a 1990s binder. The binder page is fictional.

Read REQ-3.2.4.2 out loud. That is a test. Written by someone who never wrote a test.

-->

---
transition: fade-out
---

# What the Binder Got Right

<ul class="sdd-visual-grid sdd-four sdd-binder-tabs" role="list" aria-label="What the binder captured">
  <li><strong>Purpose</strong> Why it matters</li>
  <li><strong>Stable IDs</strong> What to reference</li>
  <li><strong>Boundaries</strong> What stays in scope</li>
  <li><strong>Traceability</strong> Which test checks it</li>
</ul>

<p v-click class="sdd-reveal-line">Keeping it current was the hard part.</p>
<p v-click class="sdd-reveal-line"><strong>AI can help with the upkeep.</strong></p>

<!--
Timing: 0.5 minutes.

Go back to the replay requirement. We can point to its ID, see what the utility must report if it can’t access the store, and find the test that is supposed to check it. The binder made those connections visible while they were current.

Click for the maintenance cost. When the behavior changed, somebody had to update the requirement, the examples, and the design decisions. The binder fell behind.

Click to turn into the AI-powered part. “I brought the spec back because AI can help keep it current as the code changes.”
-->

---
transition: fade-out
class: illustrated
---

# The Spec Came Back

<div class="sdd-spec-panels">
  <figure v-click="1" class="sdd-spec-panel">
    <img src="/images/specification-old-busted-hotness.png" alt="Rodney in a patterned sweater beside a dusty specification binder." style="object-position: left;" />
    <figcaption>
      <strong>Old busted</strong>
      <b>The specification</b>
      Write it. File it. Forget it.
    </figcaption>
  </figure>
  <figure v-click="2" class="sdd-spec-panel">
    <img src="/images/specification-new-hotness.jpg" alt="Rodney in a hoodie surrounded by chat bubbles and crumpled drafts." />
    <figcaption>
      <strong>New hotness</strong>
      <b>AI chat</b>
      Prompt it. Patch it. Repeat.
    </figcaption>
  </figure>
  <figure v-click="3" class="sdd-spec-panel">
    <img src="/images/specification-old-busted-hotness.png" alt="Rodney shows the specification to a robot beside a laptop, with diagrams, code, and test checkmarks." style="object-position: right;" />
    <figcaption>
      <strong>Old busted hotness</strong>
      <b>AI helps maintain the spec</b>
      Update the agreement alongside the code.
    </figcaption>
  </figure>
</div>

<div v-click="4" class="sdd-spec-punchline">
  I can ask the agent to maintain the spec in the same change as the code.
</div>

<!--
Timing: 1.5 minutes.

Click to reveal the dusty binder. Pause, then click to reveal AI chat. Click again to reveal AI maintaining the spec. The fourth click reveals the closing line. “The old busted spec has a new job. I call it old busted hotness.”

The middle panel is what happened when I asked AI for code and kept the decisions in chat. I had to keep repeating what mattered. In the right panel I can ask the agent to update the spec, the examples, and the design decisions as the code changes. The next change can start with the reviewed files. The ability to keep those files current is why I use a spec again.

“I give the agent room to work because we’ve agreed what the software should do and what evidence I’ll review. I spend less time steering each edit and more time making the decisions that matter.”

Advance directly to “My Specification, 2026.” Open the first of the files I ask the agent to maintain.
-->

---
transition: slide-up
---

# My Specification, 2026

**`spec.md`** · The versioned agreement for a replay command

<div grid="~ cols-2 gap-8" m="t-4">
<div>

**Intent and boundaries**

- **Goal** · Let an operator request another attempt at a stored event.
- **Claim `AC-011`** · Report a structured failure when replay cannot proceed.
- **Constraint** · If the store refuses the write, no request is recorded.
- **Out of scope** · The runner decides whether and when to apply a request.

</div>
<div>

**Design and evidence**

- **§ 7 · Technical design** · Store failures map to environment error, category **3**.
- **§ 8 · Testing strategy** · Exercise the refusal path.
- **§ 9 · Traceability** · Link `AC-011` to its scenario and test.

</div>
</div>

**SDD** gives the agent an agreement to work from and update with me.

<!--
Timing: 1.5 minutes.

Open the scrubbed replay spec. The command records a request for the runner. An accepted request does not mean the runner has carried it out. Walk through the intent, then point to the technical design’s category 3. We’ll look at the category the revised claim actually gave the test writer after we see how these pieces fit.

The display ID AC-011 and file names are adapted. The document sections, behavior, and category distinction come from the reviewed replay artifacts.
-->

---
transition: slide-up
---

# Frontmatter Is Part of the Contract

**`spec.md`** · Agreement and delivery are tracked separately

<div grid="~ cols-2 gap-8" m="t-4">
<div>

```yaml
---
id: F2
epic: "0001"
type: feature
spec_status: draft
status: blocked
---
```

Reviewed snapshot · IDs adapted

</div>
<div style="align-self: center;">
  <p style="border-left: 3px solid var(--sdd-copper); padding-left: 1rem;">
    <code>spec_status</code><br />
    <strong>Agreement in draft</strong>
  </p>
  <p style="border-left: 3px solid var(--sdd-copper); padding-left: 1rem;">
    <code>status</code><br />
    <strong>Delivery blocked</strong>
  </p>
</div>
</div>

<!--
Timing: 1 minute.

These are selected fields from the reviewed replay snapshot. The agreement was in draft and delivery was blocked. I can ask the agent to keep delivery metadata current while I review changes to the agreement. I also spent review time on frontmatter in this incident. I don’t have a field-level example from that review to show.

Next, compare the scenario that came out of the review with the technical design.

For comparison with the later schema, see Specht src/tool/schema/v0.3.0/frontmatter/feature.schema.json.
-->

---
layout: center
class: text-center
transition: fade
---

# One Claim at Work

<!--
Timing: 0.25 minutes.

“Let’s follow the replay claim into the feature file and the design behind it.”

Pause, then advance.
-->

---
layout: two-cols
layoutClass: gap-8
class: claim-example
---

# Create the Feature File

<p class="sdd-feature-lead">What should the operator see if the store refuses the write?</p>

::right::

**`replay.feature` · Follow-up excerpt**

```gherkin
Feature: Event replay

  @AC-011
  Scenario: The store refuses the write
    Given a stored event
    And the store stays busy
    When I request a replay
    Then I get a structured failure
    And the exit code is 3
```

<!--
Timing: 1.5 minutes.

This is the scenario added in the follow-up review, with names and wording shortened. The original scenario also covers an unreadable store. The busy case means busy past both write attempts. The Then clauses pin the structured failure and category 3. The AC-011 tag links the scenario to the agreement. Given sets up the store refusal, When runs the replay command, and Then gives the test writer something observable.

We’ll see the earlier draft test expect category 1. First, where did the design draw the boundary?

Source: https://cucumber.io/docs/gherkin/reference/
-->

---
transition: slide-up
---

# The Design Decision

<p class="sdd-eyebrow"><code>spec.md</code> · Design § 7, <code>R3</code> / <code>R13</code></p>

<div class="sdd-gate">
  <p class="sdd-gate-owner"><strong>Replay</strong> tries to record a request</p>
  <ul class="sdd-visual-grid sdd-two" role="list" aria-label="Outcomes of recording the replay request">
    <li v-click="1" class="sdd-exception"><strong>Store refuses</strong> Environment error · <b>3</b></li>
    <li v-click="2"><strong>Request recorded</strong> Accepted · <b>0</b></li>
  </ul>
</div>

<p v-click="2" class="sdd-caption">The runner handles the attempt later.</p>

<!--
Timing: 1.25 minutes.

This is the technical design in the spec itself. Reveal the store refusal first. If the store stays busy or becomes unreadable, the environment prevented the write. The request was never recorded. Reveal the accepted path. The CLI reports that a request was recorded and exits 0. The runner decides eligibility when it applies the request later.

That split is why I care about 1 versus 3. Category 1 is a general failure, for example an event that can’t be found. Category 3 says the environment prevented the operation.
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

Bind the Given to a replay intake that refuses the write and the When to the command. The Then checks the error and exit code.

A useful red test executes and fails on the agreed behavior. An unimplemented scaffold hasn’t reached that point. Get the expectation right before using the test to drive implementation. Then make that same assertion pass and refactor. This is the workflow I wanted. In the review we’ll see what happened before we had that executable test.
-->

---
transition: slide-up
clicks: 4
---

# AC-011 Across the Artifacts

<ul class="sdd-handoff-flow" role="list" aria-label="The replay claim, scenario, and design">
  <li>
    <strong>Spec document</strong>
    <code>spec.md</code>
    <p><code class="sdd-claim-reference" :class="{ 'sdd-claim-highlight': $clicks >= 1 }">AC-011</code><br>Report a structured replay failure.</p>
  </li>
  <li v-click="2">
    <strong>Feature file</strong>
    <code>replay.feature</code>
    <p><code class="sdd-claim-reference sdd-claim-highlight">@AC-011</code><br>Store refuses the write.</p>
  </li>
  <li v-click="3">
    <strong>Technical design</strong>
    <code>spec.md § 7</code>
    <p><code class="sdd-claim-reference sdd-claim-highlight">R3 / R13</code><br>Environment error, category <b>3</b>.</p>
  </li>
</ul>

<v-click :at="4">

**The links can line up while the artifacts disagree about the behavior.**

</v-click>

<!--
Timing: 1.5 minutes.

Click 1 highlights the replay failure claim. Click 2 reveals the category-3 scenario from the follow-up review. Click 3 shows the original design rule behind that outcome. This is the set of artifacts I wanted to keep aligned. The design lives in the same spec file as the claim, in a different section.

Click 4 and ask what happens if the agent updates one of these pieces and carries the wrong expectation forward. Next, tell the review story.
-->

---
transition: fade
---

# My Precious Tokens

<div class="sdd-story sdd-token-story">
  <blockquote>
    <p>“I burned a week’s worth of tokens trying to review a pull request.”</p>
  </blockquote>
  <img src="/images/review-comment-pile.jpg" alt="Rodney and the robot agent peer over a laptop surrounded by piles of review comments." width="1254" height="1254" />
</div>

<!--
Timing: 1 minute.

This is my account of a private-project PR. I had asked AI to maintain the spec as the implementation changed. AI revised the documentation, and the agreement drifted. The review kept coming back to metadata, wording, and broken links. Here is what the surviving artifacts tell us about that review.
-->

---
transition: slide-up
class: artifacts
---

# The Files Behind the Workflow

**Review snapshot · paths shortened**

<div role="group" aria-label="The replay feature contains a spec, its scenarios and draft test stubs. Lessons are in the documentation folder.">

```text
repo/
├── specs/
│   └── replay/
│       ├── spec.md           Claim, design, traceability
│       ├── replay.feature    Scenarios
│       └── scaffold-stubs/   Draft tests
└── docs/
    └── lessons/              Review findings
```

</div>

<!--
Timing: 0.5 minutes.

Use the tree to orient the room. The old layout put the replay spec, Gherkin scenarios, and test scaffolds together. The lessons lived in a separate documentation folder. The paths on screen are scrubbed. The checker came later.
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

Come back to the rule in the technical design. A busy or unreadable store that prevents the write is an environment failure, category 3. A general failure such as an event that can’t be found belongs to category 1. This is the expected answer before we look at what the revised claim said.
-->

---
transition: fade-out
---

# The Spec Started Drifting

<p class="sdd-eyebrow">Actual replay review · Store busy or unreadable</p>

<ol class="sdd-handoff-flow sdd-drift-path" role="list" aria-label="How the revised replay claim reached the test scaffold">
  <li><strong>Revised <code>AC-011</code></strong> Exit category <b>1</b></li>
  <li v-click="1">
    <span class="sdd-handoff-arrow" aria-hidden="true">→</span>
    <strong>Test scaffold</strong> Expected category <b>1</b>
  </li>
</ol>

<v-click :at="1">

```csharp
// Then a structured failure is reported and the exit code is category 1
// — no catch-all exception handler is needed or present
Assert.Fail("not implemented — scaffold only");
```

</v-click>

<p v-click="2" class="sdd-exception-line">Design § 7 (<code>R3</code> / <code>R13</code>) still said <strong>category 3</strong>.</p>
<p v-click="3">A reviewer caught the contradiction in the unimplemented scaffold.</p>

<!--
Timing: 1.5 minutes.

Start with the revised claim. It put a busy or unreadable store in category 1. Click to see the test scaffold inherit that expectation. Read the Then comment and point to Assert.Fail. This is a draft expectation in a stub that failed as unimplemented.

Click to compare it with the spec’s design. R3 and R13 still mapped the same failure to category 3. Click again for the review finding. A reviewer caught the contradiction. The draft shows how the wrong expectation traveled from the spec into the test scaffold.

Source provenance: pr-evidence.md, “A wrong claim propagated into a scaffold.”
-->

---
transition: slide-up
---

# The References Drifted Too

<p class="sdd-eyebrow">Actual replay review</p>

<ul class="sdd-visual-grid sdd-two" role="list" aria-label="Separate findings from the original and follow-up pull requests">
  <li><strong>Original PR</strong> Five accepted claims, zero traceability rows</li>
  <li><strong>Follow-up PR</strong> Wrong scenario tag, missing from both rows</li>
</ul>

```diff [$ git diff]
-    @AC-011
+    @AC-003
```

<p class="sdd-reveal-line" v-click>Repair the tag and restore the link in <code>AC-003</code>’s row.</p>

<!--
Timing: 1.5 minutes.

These are separate findings. The first PR lacked traceability rows for five accepted claims. The follow-up had a scenario linked to the wrong existing claim and absent from both matrix rows. The diff shows the verified correction with IDs rendered as AC-. It moved the tag and added the scenario to AC-003’s row. A checker can see an absent row or an inconsistent link. A reviewer still has to read the behavior and decide where the scenario belongs.

Hold onto one more detail for later. Even the lessons from the review contained mistakes.

Source provenance: pr-evidence.md, “Missing rows and a mismatched claim tag.”
-->

---
class: sdd-review-loop
transition: fade
---

# Author. Reviewer. Repeat.

<div class="sdd-review-loop-diagram">
  <img v-click.hide="1" src="/images/review-loop.svg#author" width="868" height="238" alt="The author updates the branch." />
  <img v-click="[1, 2]" src="/images/review-loop.svg#reviewer" width="868" height="238" alt="The author requests review. The reviewer checks the changes." />
  <img v-click="2" src="/images/review-loop.svg" width="868" height="238" alt="The author updates the branch and requests a review. The reviewer checks the changes and requests revisions. The author revises and sends it back, burning more tokens on every pass." />
</div>

<p class="sdd-review-loop-summary">The author updates the branch and requests a review. The reviewer checks the changes and requests revisions. The author revises and sends it back, burning more tokens on every pass.</p>

<h2 v-click="3">Highlights</h2>

<ul class="sdd-review-highlights" role="list">
  <li v-click="3"><strong>200+</strong> comments posted to a single PR</li>
  <li v-click="4"><strong>10+</strong> reviews never posted to GitHub</li>
  <li v-click="5"><strong>20+</strong> AI reviews, each with multiple rounds</li>
  <li v-click="6"><strong>500+</strong> commits on the branch</li>
</ul>

<!--
Timing: 1.5 minutes.

Start with the author updating the branch. Click once to send it to the reviewer, then click for the change request and the cost of another pass.

Click through the four highlights. The counts are my account of the review and branch history. Reviews and the rounds inside them are separate. The longest review I recall took 18 rounds. The original PR closed, and the follow-up eventually merged.

After the commit count, say “Wait. I can build a thing to check these rules.” I could turn an explicit rule we kept rediscovering into a check that ran before PR review. Advance to the next section card.

Source provenance: pr-evidence.md preserves the reviewed artifacts and an earlier account of the review effort.
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

Return to the review findings. Missing rows are an explicit rule I can put in the build. Deciding whether the spec means category 1 or 3 still needs a person.
-->

---
transition: slide-up
---

# Meet Specht

<img src="/images/specht-book-logo-dark.svg" alt="Specht logo: a woodpecker perched on a book above the SPECHT wordmark" class="sdd-logo" />

<div style="max-width: 610px;">

Specht checks spec structure and links as the code changes.

<v-click>

<figure>
  <img src="/images/agent-specht-check-plan.svg" alt="Illustrative agent checklist for AC-011. The agent is checking the Gherkin tag and design. Specht and Format are queued after the edits and replay test and before opening the PR." style="width: 100%; height: auto;" />
  <figcaption style="margin-top: 0.5rem;">Example agent plan for <code>AC-011</code>.</figcaption>
</figure>

</v-click>

[github.com/RLittlesII/specht](https://github.com/RLittlesII/specht)

</div>

<!--
Timing: 1 minute.

Rather than tell you about how SDD can build things, I built a thing with it to fix a problem with SDD for the SDD talk!

Specht checks YAML frontmatter, required sections and their order, claim IDs and traceability, and declared dependencies. Pin the tool and schema so the agent and CI can run the same structural checks.

Reveal the checklist. This is an illustration based on a real agent task list, adapted to the replay claim. Point to Run Specht and Format after the edits and test, before the pull request. A Specht finding comes back with a path, line, and rule. Next, compare two actual review findings against the rules Specht runs.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/what-it-checks.md
Logo: Specht’s existing dark-background SVG.
-->

---
transition: slide-up
---

# What Specht Could Check

**Two findings from the review**

<ul class="sdd-visual-grid sdd-two" role="list" aria-label="What Specht’s structural rules could establish">
  <li><strong>Missing rows</strong> <code>SPEC4004</code> checks that each claim has one traceability row.</li>
  <li v-click><strong>Wrong existing tag</strong> <code>SPEC4002</code> can resolve <code>@AC-011</code>. The reviewer still needs to choose <code>AC-003</code>.</li>
</ul>

<!--
Timing: 1.5 minutes.

This is how I would apply the pinned Specht rules to the real findings. The original PR omitted rows for five accepted claims. SPEC4004 checks for a row per claim. In the follow-up PR a scenario referenced AC-011, a valid ID. SPEC4002 checks that the tag names a declared claim. It cannot determine which of two valid IDs governs a scenario. I had to follow the behavior past the replay command to the later listing and assign it to AC-003.

These are rule explanations applied after the fact. They aren’t output captured from the historical PRs.

Sources: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/rules/v0.3.0/SPEC4004.md and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/rules/v0.3.0/SPEC4002.md
-->

---
transition: slide-up
---

# From Scaffold to Running Test

**`AC-011` · From the review to an executable check**

<div grid="~ cols-5 gap-6">
<div class="col-span-2 min-w-0">

```gherkin
Given intake reports StoreBusy
When I request a replay
Then the exit code is 3
And the error names StoreBusy
```

</div>
<div class="col-span-3 min-w-0">

<v-click>

**Review correction**
```diff [Claim wording, shortened]
- Store failure → category 1
+ Store failure → category 3
```

</v-click>

<v-click>

**Command mapping**

A store refusal returns an environment error.

</v-click>

<v-click>

**Acceptance test**

The test checks exit **3** and the store error.

</v-click>

</div>
</div>

<!--
Timing: 2 minutes.

This is the later implementation and acceptance coverage, presented with shortened labels. On click 1, the follow-up spec explicitly puts the store/config precondition and the busy or unreadable write under category 3. On click 2, the implemented command maps the precondition refusal to an environment error. On click 3, the formerly deferred scenario runs against the command with StoreBusy supplied by the intake port. It checks exit 3 and the rendered error, “ERROR: Event store unavailable: StoreBusy.”

The acceptance test injects the refusal; it does not take a database lock. A separate intake unit test exercises the busy retry and refusal. The original stub was unimplemented, so I don’t have a historical red-to-green terminal run to show for it.
-->

---
transition: slide-up
---

# Roslyn Checks the Links in C#

**Illustrative analyzer wiring for the replay claim**

```csharp
/// <implements epic="0001" feature="F2" claim="AC-011" />
public Task<int> RunReplayAsync(string eventId);

/// <verifies epic="0001" feature="F2" claim="AC-099" />
public Task BusyStore_ReportsEnvironmentError();
```

<v-click>

**`SPCT0004`** · The test refers to `AC-099`, which the feature does not declare.

</v-click>

<!--
Timing: 2 minutes.

These adapted C# declarations show how I can use the analyzer with the replay claim. The mistyped target is illustrative. Specht.CodeAnalysis supplies the selected spec inputs to Roslyn. The analyzer reads the XML links on the C# declarations and checks their targets against the claim catalogue. Here the feature is F2 in epic 0001, and AC-099 fits the configured grammar but does not exist.

Reveal the diagnostic. Compare the test with AC-011 and correct the link. A conditional code action can offer an eligible target. The developer or reviewer still chooses the one that expresses the test’s intent. Next check what actually ran.

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/codeanalysis/docs/SPCT0004.md
-->

---
transition: slide-up
---

# A Link and a Run Are Different Evidence

<ol class="sdd-handoff-flow" role="list" aria-label="Replay evidence workflow">
  <li><strong>Build</strong> Resolve claim links</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Discover</strong> Find test cases</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Run</strong> Record pass or fail</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Reconcile</strong> Check links and freshness</li>
</ol>

<p v-click class="sdd-exception-line">Stale evidence needs a fresh run.</p>

<!--
Timing: 2 minutes.

Use these commands to illustrate the sequence for a configured xUnit v2/VSTest project with the Specht logger. Start with the build so Roslyn can read the declarations. On a fresh working copy, test recognition can be provisional until the project supplies current discovery and run evidence. Discovery records cases. Execution records their outcomes. The evidence command reads those artifacts and checks required links, input freshness, and results.

Click through discovery, execution, and reconciliation, then reveal the stale evidence cue. If I edit the assertions after a passing run, I rerun against the new revision. A valid verifies link can survive that edit while the old pass goes stale.

Example commands for a configured replay test project

```sh
dotnet build test/Replay.Tests.csproj
dotnet test test/Replay.Tests.csproj --no-build --list-tests
dotnet test test/Replay.Tests.csproj --no-build
specht evidence test/obj/specht-test-evidence
```

Source: https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/docs/cli.md#specht-evidence and https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/codeanalysis/README.md
-->

---
transition: slide-up
---

# Specht Uses the Same Approach

<p class="sdd-eyebrow"><code>0201-F7/B-030</code></p>

<ol class="sdd-handoff-flow" role="list" aria-label="How Specht traces its own catalogue export claim">
  <li><strong>Claim</strong> Export the Gherkin catalogue</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Scenario</strong> <code>@B-030</code></li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Exporter</strong> Writes the catalogue</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Linked test</strong> Reads it back</li>
</ol>

<v-click>

```xml
<implements epic="0201" feature="F7" claim="B-030" />
<verifies epic="0201" feature="F7" claim="B-030" />
```

</v-click>

<!--
Timing: 2 minutes.

This is the public Specht repository at v0.4.2-preview. B-030 is Specht’s own claim. Reveal the scenario, exporter, and linked test, then show the matching XML links. The tagged example checks that the export includes the arithmetic claim and both example rows. TestEvidenceCatalogExporter projects the catalogue, and the explicitly linked collector test checks that the emitted artifact retains the catalogue. The XML lines come from separate source and test files.

Consumers can choose tagged Gherkin as their native claim input. In that mode the selected .feature files supply the catalogue instead of the Markdown claims table. My replay example used Markdown with a tagged companion. Point out the same stable identity across the two workflows.

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

# I Can Sign Off on AC-011

<ul class="sdd-signoff-evidence" role="list" aria-label="What I review">
  <li>Approved claim</li>
  <li>Code diff and test link</li>
  <li>Results for this revision</li>
</ul>

<ul class="sdd-visual-grid sdd-three sdd-signoff-results" role="list" aria-label="Evidence for the replay claim">
  <li v-click><strong>StoreBusy</strong> Intake refusal</li>
  <li v-click><strong>3</strong> Environment error</li>
  <li v-click><strong>Store unavailable</strong> Error reported to the operator</li>
</ul>

<p v-click class="sdd-reveal-line">I can give the agent room to work and come back to evidence I can check.</p>

<!--
Timing: 1.5 minutes.

At sign-off I read the corrected behavior, look at the implementation and test diff, and ask for results from that revision. Reveal the injected StoreBusy refusal, the category 3 assertion, and the reported error. In the later delivery, the previously deferred scenario was bound and the traceability row was updated. Click for the payoff. Product owns a change to the requirement. The tester challenges the example. Development repairs code and links. The reviewer decides whether the evidence is enough to accept.

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
Timing: 1.5 minutes.

Back to the review nightmare. A lesson about keeping signatures consistent managed to misquote both signatures. Wonderful. Now the next agent has a beautifully documented way to learn the wrong thing.

That is the actual L-0009 finding. A later reply says the finding was recorded in L-0012 while L-0009 stayed unchanged. We tracked the correction without doing it. The broader incident also included L-0007's unsupported historical example; keep that in the evidence notes rather than introducing a second case here.

Reveal the three steps as an example response. First, a person verifies the finding and corrects the lesson. Compare quoted code with its source before teaching from it. On the second click, show the procedure the skill runs when a signature changes.

1. Open the port, affected callers, and relevant lesson.
2. Compare the lesson's quotes with current source.
3. Fix stale quotes, then build and run the affected tests.
4. Return the revision, source paths, diff, and test results.

On the third click, show what the reviewer checks on the next run. Build and test results describe the code. The source comparison verifies the quote.

Return the revision, source paths and symbols, repaired lesson diff, and actual results. The next reviewer opens those symbols at that revision and compares the quote. Still wrong? Return the finding and correct the procedure before repeating. The observed failure becomes a verified lesson, then a skill the next task applies and the reviewer can inspect. For AC-011, apply the same habit when the replay intake changes. Include the current interface, the tests for the refusal and CLI mapping, and fresh results in the handoff.

Load the procedure for relevant tasks; a skill sitting unread in a folder changes nothing. The checks and next reviewer make its application inspectable. Next, show what that handoff needs to carry.

Source provenance: see pr-evidence.md for the reviewed artifacts, original PR identifiers, and pinned source links.
-->

---
level: 2
clicks: 4
---

# Make Every Handoff Inspectable

```mermaid {key: $clicks, scale: 0.7, themeCSS: ['.sdd-step-1, .flowchart-link[data-id=edgeRS]', '.sdd-step-2, .flowchart-link[data-id=edgeST]', '.sdd-step-3, .flowchart-link[data-id=edgeTC]', '.sdd-step-4, .flowchart-link[data-id=edgeCD], .flowchart-link[data-id=edgeCE]'].map((selector, index) => selector + ' { visibility: ' + ($clicks > index ? 'visible' : 'var(--sdd-unrevealed-diagram-content, hidden)') + '; }').join(' ')}
graph LR
accTitle: Code context for a requirement
accDescr: A claim links to a scenario, its test, and existing code. The existing code links to dependencies and a canonical example.
R[Claim] edgeRS@--> S[Scenario]
S edgeST@--> T[Test]
T edgeTC@--> C["Existing code"]
C edgeCD@--> D[Dependencies]
C edgeCE@--> E["Canonical example"]
class S sdd-step-1
class T sdd-step-2
class C sdd-step-3
class D,E sdd-step-4
```

<ul>
  <li>Start with the approved revision of <code>AC-011</code></li>
  <li v-click="3">Link the test and replay diff, with results from that code revision</li>
  <li v-click="4">Name who resolves a conflict before the next handoff</li>
</ul>

<!--
Timing: 1.25 minutes.

Start with the approved claim. Reveal the scenario, test, and existing code in order, then show the two links out of the code. The next team needs the approved requirement and enough code context to find the affected implementation and dependencies. This graph is a conceptual map, not a Specht feature. Paths and symbols can supply the links.

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
Timing: 1 minute.

The tradeoff is explicit in the replay review. We spend time writing one good example and keeping the links current. The agent can reuse that decision instead of asking me to reconstruct it each time. Builds and tests take time, and repeated evidence should earn its place.

Optional anecdote if time allows. A buddy saw the first version of this talk and showed me working software three days later. That was the interval between our conversations. I have no comparison with how he would otherwise have worked.

“I thought picking a tool would settle more of this. I still had to decide what the team would write down and how we’d keep it straight.”
-->

---
transition: slide-up
---

# What Goes in the Spec?

<div grid="~ cols-2 gap-8" m="t-4">
<div>

**Prose and examples**

Describe the behavior and link to the approved section from the work item or PR.

</div>
<div>

**Stable claim IDs**

Carry the same ID from the spec into the scenario, test, and review evidence.

</div>
</div>

- **Shared structure** · Agree where intent, scope, and acceptance criteria go.
- **Companion documents** · Decide when a feature file or design decision record earns its own file.

<!--
Timing: 0.75 minutes.

AC-011 is how I chose to link the artifacts. The team can use prose and point reviewers to the approved section, or give individual claims stable IDs when they need to follow a specific claim through tests and evidence. Either way, agree what a reviewer should expect to find in a spec. In the replay example, the design stayed in spec.md and the executable examples lived in a feature file. Give each companion document a job and link it from the spec so an editor knows what else to check when behavior changes.

Once you use IDs across those files, you also have to decide how they’re assigned.
-->

---
transition: slide-up
---

# Two Sessions Picked the Same ID

<p class="sdd-eyebrow">Illustrative example · Both sessions start from the same spec</p>

<ul class="sdd-visual-grid sdd-two" role="list" aria-label="Two sessions assign the same ID to different replay requirements">
  <li><strong>Session A</strong> <code>AC-012</code> for retrying a busy write</li>
  <li v-click><strong>Session B</strong> <code>AC-012</code> for recording who requested replay</li>
</ul>

<v-click>

- **Assignment** · Reserve IDs centrally or give sessions distinct prefixes within the spec.
- **Merge check** · Reject duplicate claims in the combined changes.

</v-click>

<!--
Timing: 1 minute.

I ran into ID collisions when I had more than one agent session going. The values and requirements on screen are illustrative. Both sessions see AC-011 and independently pick the next number. Each branch can look fine in isolation. Once the changes come together, a scenario tagged AC-012 could point to either requirement.

The team needs an allocation rule for the scope where IDs must be unique. A shared allocator can reserve the number before either session writes. If sessions assign IDs independently, use distinct prefixes and keep issued IDs stable. Run the uniqueness check on the combined result against the latest target before merge. For an existing collision, keep the approved ID, assign a new one to the other claim, and update the scenario tags, tests, and traceability links that refer to that claim. These are example safeguards.
-->

---
transition: slide-up
---

# Where the Source of Truth Lives

<ul class="sdd-authority-map" role="list" aria-label="Three ways to deliver approved intent to the agent">
  <li><strong>Repo owns it</strong><span>Approved commit</span><span aria-hidden="true">→</span><span>Agent</span></li>
  <li v-click><strong>Tracker owns it</strong><span>Versioned item</span><span aria-hidden="true">→</span><span>Agent</span></li>
  <li v-click><strong>Repo owns it</strong><span>Approved commit</span><span aria-hidden="true">→</span><span>Agent + published tracker view</span></li>
</ul>

<p v-click class="sdd-reveal-line">Where does the team track work and status?</p>
<p v-click class="sdd-reveal-line">Who can read and approve the current spec?</p>

<!--
Timing: 1.75 minutes.

Start with the repository, then reveal the tracker and the published tracker view. Everything in my example lives in the repository. Your organization may have approved requirements in GitHub Issues, Azure DevOps, or a Trello board. Ask where the team will keep work items. Git can own the approved wording while the tracker owns delivery status. Decide which record governs when two copies disagree. Then ask how everyone who needs to review or approve a change gets access.

If the repository owns the requirement, a commit pins the baseline alongside code and tests. The downside is access: people outside the repository, often people who are not developers, need a readable view and a way to propose and approve changes.

If the tracker owns the requirement, retrieve the approved item. Keep its ID, link, and version with the context. If it lacks a useful revision identifier, capture the approved content and timestamp. Local excerpts point back to that source. Check for upstream changes before acceptance and have the owner resolve differences.

One option I’ve considered is keeping Git authoritative and publishing work items to Azure DevOps so the wider team has a view. Route proposed changes to the requirement wording back to the spec owner for approval. This is a possible integration.

I am using SDD on a large existing project today. Start from available requirements wherever they live. When fixing a bug or changing an area, clarify its intent, add criteria and scenarios, and retain the checks. Backfill as you go. Examine existing behavior with its owner before treating it as a requirement. Coverage stays partial while you expand.

“Now that we know which record governs, when do we update the rest?”

Source: Rodney's account of current use and proposed process variations, supplied for this talk. No measured benefit or completed tracker integration is claimed.
-->

---
transition: slide-up
---

# Keeping the Workflow Current

- **When behavior changes** · The spec owner approves the revision. The person making the change updates the linked work item, examples, and tests.
- **Before handoff** · The reviewer checks that the work item and test results point to the right revisions.
- **After a recurring mistake** · The team turns a verified lesson into a shared instruction or check and reviews it on the next relevant change.

<!--
Timing: 0.75 minutes.

Example team agreement. Keep these rules and a worked example where the team can find them and agents can load them. Give the team a place to propose changes to the rules, too. If Git publishes a view to a tracker, update the same linked item after approval, include the source revision, and flag failed publication for its owner. When a change starts in the tracker, refresh the approved item before accepting the work. If the records disagree, take it to the owner of the authoritative record.

The lesson about misquoted signatures is a reminder to fix the shared guidance people actually use. Ask a teammate to apply the revised instruction on the next relevant task and inspect what comes back. Now let’s try reviewing the replay requirement as a team.
-->

---
transition: slide-up
---

# What If the Store Is Busy Once?

<p class="sdd-feature-lead">The first write hits a busy store. What should replay do?</p>

<div v-click="1" class="sdd-gate">
  <p class="sdd-gate-owner"><strong>Retry once</strong></p>
  <ul class="sdd-visual-grid sdd-two" role="list" aria-label="Outcomes of retrying the replay write">
    <li><strong>Retry succeeds</strong> Request recorded</li>
    <li><strong>Retry still busy</strong> Environment error · <b>3</b></li>
  </ul>
</div>

<p v-click="2" class="sdd-reveal-line">Check the retry at the store and the exit category at the command.</p>

<!--
Timing: 1.75 minutes.

Ask the audience before clicking. It matters whether the store is briefly busy or the write still can’t be recorded after the retry. Reveal the policy from the replay design and implementation. A busy write is retried once. If the second attempt records the request, the command can report acceptance. If both attempts are busy, the store reports a refusal and the command exits 3.

Click for the two test boundaries. The intake unit test exercises both attempts; the command acceptance scenario supplies StoreBusy without creating a lock and checks the result. Ask for results from the revision that implements that policy.
-->

---
transition: slide-up
---

# What I Review Before I Accept

<p class="sdd-feature-lead">The test passed yesterday.<br />The code changed today.</p>

<p v-click class="sdd-reveal-line"><strong>What would you ask for before accepting <code>AC-011</code>?</strong></p>

<!--
Timing: 0.5 minutes.

Use AC-011 to make the review concrete. I check that the current run reports the store refusal and exits 3, then look at the intake coverage for the retry itself.

Pause for one answer. Ask for the approved claim, the current diff and checked links, and results from this revision. If the evidence falls short, name who owns the next decision.
-->

---
transition: slide-up
---

# Who Checks What?

<ol class="sdd-handoff-flow" role="list" aria-label="Who owns each step from agreement to acceptance">
  <li><strong>Team</strong> Agrees on behavior</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Agent</strong> Updates spec, examples, and code</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Build and tests</strong> Build checks links.<br />Tests check behavior.</li>
  <li v-click><span class="sdd-handoff-arrow" aria-hidden="true">→</span><strong>Reviewer</strong> Decides whether to accept</li>
</ol>

<!--
Timing: 0.5 minutes.

Come back to the question from the title slide. Start with the team and reveal the agent, the automated checks, and the reviewer. I asked AI to keep the spec current. The replay claim taught me that it could carry the wrong answer into a second artifact. Specht took repeatable checks out of my review loop. I still had to decide what category the command should return and ask for a test that exercised it.

Bring back the disclaimer. On your next change, pick one claim and follow it from agreement to a test result you can review. Then advance to Questions.
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
