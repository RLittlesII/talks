# PR evidence for the token horror story

Presenter provenance: these source links support the case study. The audience-facing deck uses anonymous original/follow-up PR labels and does not direct attendees to the private repository.

The story slides use hooked PRs [#214](https://github.com/RLittlesII/hooked/pull/214) and [#215](https://github.com/RLittlesII/hooked/pull/215). The findings below distinguish reviewed artifacts, review observations, and subsequent corrections. The account of a week's worth of Claude Max tokens and the review-effort figures below come from Rodney; these PRs do not measure token consumption or capture reviews performed away from GitHub.

The slides display claim prefixes as `AC-` (acceptance criterion): source `B-011` becomes `AC-011`, for example. The tag correction and diagnostic are labeled as adapted identifiers. All original identifiers below remain unchanged.

## Frontmatter was part of the review problem

On November 5, 2026, Rodney clarified that frontmatter was also part of the
specification-review problem that burned his tokens. This is his account of
the incident. The evidence retained here does not identify the particular
frontmatter key, invalid value, or corrective diff; the slides do not invent
one. Slides 8–9 teach the chosen specification contract using the versioned
0.3.0 template and frontmatter schema. Their checkout metadata is an illustrative
excerpt, separate from the historical findings below.

## A wrong claim propagated into a scaffold — slide 18

At reviewed commit [`fdbae93`](https://github.com/RLittlesII/hooked/commit/fdbae932c81169920f2e3d2eba0f8b2f676e838f), `0005-F5b` claim `B-011` included store/config precondition and busy-timeout failures in exit category **1**. The same spec's design mapped `StoreBusy` and `StoreUnreadable` to category **3**.

- [Claim at the reviewed commit](https://github.com/RLittlesII/hooked/blob/fdbae932c81169920f2e3d2eba0f8b2f676e838f/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L66)
- [Design's refusal mapping](https://github.com/RLittlesII/hooked/blob/fdbae932c81169920f2e3d2eba0f8b2f676e838f/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L821)
- [Review finding on the contradictory claim](https://github.com/RLittlesII/hooked/pull/214#discussion_r4147821721)
- [Review finding on the inherited scaffold expectation](https://github.com/RLittlesII/hooked/pull/214#discussion_r4147831386)

The [stub at that commit](https://github.com/RLittlesII/hooked/blob/fdbae932c81169920f2e3d2eba0f8b2f676e838f/epics/0005-cli/F5b-event-inspection-recovery/scaffold-stubs/ReplayCommandHandler.Unit.Tests.cs#L147) contains:

```cs
// Given IReplayRequestIntake.RecordAsync returns ReplayRequestRefused
// (e.g. StoreBusy, EventNotFound race, StoreUnreadable) rather than
// throwing — the port is total by design (§ 7 LSP row, amended for S-2)

// When the handler processes the rejection

// Then a structured failure is reported and the exit code is category 1
// — no catch-all exception handler is needed or present
Assert.Fail("not implemented — scaffold only");
```

This is a specification-to-scaffold propagation error. It is not evidence of a passing test or a released runtime defect. The review caught the contradiction; it does not establish that every reviewer accepted the incorrect claim.

## Missing rows and a mismatched claim tag — slide 19

In #214, the [F4 review finding](https://github.com/RLittlesII/hooked/pull/214#discussion_r4150139963) identified five accepted claims (`B-001`–`B-005`) with no traceability rows. The requirement was one row per claim, permitting an explicit `Missing` value rather than omitting a row. This is distinct from deleting the claims themselves.

In #215, at reviewed commit [`4e95b5b`](https://github.com/RLittlesII/hooked/commit/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d):

- The [secret-resolution scenario](https://github.com/RLittlesII/hooked/blob/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d/epics/0005-cli/F5b-event-inspection-recovery/event-inspection-recovery.feature#L110) was tagged `@B-011`.
- The [B-011 claim](https://github.com/RLittlesII/hooked/blob/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L72) assigned that behavior to `B-003`.
- The scenario appeared under neither the [B-003 row](https://github.com/RLittlesII/hooked/blob/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L2642) nor the [B-011 row](https://github.com/RLittlesII/hooked/blob/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L2651).

The [review finding](https://github.com/RLittlesII/hooked/pull/215#discussion_r4212758817) specifies all three inconsistencies. [Commit `90ab1fe`](https://github.com/RLittlesII/hooked/commit/90ab1fed669bcae50b5bafad159bc59583a2f1cd) changes the tag and adds the scenario to B-003's traceability row, with related citation repairs:

```diff
-    @B-011
+    @B-003
```

The [follow-up reply](https://github.com/RLittlesII/hooked/pull/215#discussion_r4212833570) identifies the same correction. Both IDs existed: reference resolution alone would not decide which claim expresses the scenario's meaning.

## Reported review effort — slide 20

Rodney supplied this account on October 28, 2026:

- By the time the PR was done, it had more than 200 review comments.
- About half the reviews happened away from GitHub.
- There were 20+ AI reviews, each taking 10+ rounds.
- The highest round count he recalls was 18.

Reviews and rounds within a review are distinct units. The highest count is a
recollection. These figures include workflow information outside GitHub and are
attributed to Rodney, rather than presented as an independently reconstructed
GitHub count. The artifact history below supplies the documented outcomes.

## The visible review and repair cycle

PR #214 was opened September 30, 2026 and closed without merging on October 2. Its retained commit history includes review corrections, traceability repairs, and a lesson-register audit. Selected commits:

| Commit | Recorded change |
| --- | --- |
| [`d25739a`](https://github.com/RLittlesII/hooked/commit/d25739a4a4) | Address PR #214 review feedback |
| [`0aa4962`](https://github.com/RLittlesII/hooked/commit/0aa4962e78) | Fix F3 DI-seam count and add traceability rows for B-013–B-018 |
| [`d953cac`](https://github.com/RLittlesII/hooked/commit/d953cac46391bdf2fbfdcecd30bb6cc44819fc3a) | Record lesson-register integrity collapse and remove the hand-maintained lesson range |

Rodney's [closing comment](https://github.com/RLittlesII/hooked/pull/214#issuecomment-5962821855) says:

> Closing this Pull Request. #214 will forever be a reminder of what to do when AI goes rogue!

PR #215 was opened October 3, received further change requests, and merged October 7, 2026. The [October 7 review](https://github.com/RLittlesII/hooked/pull/215#discussion_r4212758817) and corrective commit above show that artifact-chain problems remained after earlier repairs.

GitHub reply counts and references to review rounds are not a verified count of internal agent iterations. The retained history contains squashed or rewritten material. The PRs show the corrections and eventual outcome; Rodney supplies the internal workflow and token-cost account.

## The lessons drifted too — slide 32

The [L-0007 review](https://github.com/RLittlesII/hooked/pull/214#discussion_r4169482854) reports that searches for the lesson's claimed `Table C` / `row 2 of` incident found only the commit adding the lesson. It also identifies the lesson's incorrect treatment of stable `C-n` IDs as positional references. Attribute this history audit to the review; the slide revision did not independently repeat a search of every git branch.

A [later reply](https://github.com/RLittlesII/hooked/pull/214#discussion_r4169839937) states that L-0012 recorded this finding while L-0007 itself remained unchanged. The [L-0009 reply](https://github.com/RLittlesII/hooked/pull/214#discussion_r4169840082) makes the same distinction for misquoted port signatures: tracked, not fixed.

[The committed L-0012 record](https://github.com/RLittlesII/hooked/blob/d953cac46391bdf2fbfdcecd30bb6cc44819fc3a/docs/lessons/L-0012-lesson-register-integrity-collapse-during-pr-214-review.md) describes fabricated incidents, misquoted code, reversed facts, incomplete corrections, and lessons not being applied during review. It is a contemporary incident record, not proof that every proposed prevention measure was implemented.

Slide 32 focuses on the actual L-0009 finding, then supplies an illustrative response: verify and correct the lesson, update a signature-change skill to compare quotes with source and run affected checks, and require the next reviewer to inspect the cited revision, paths, diff, and results. The skill procedure and next-run review are teaching examples, not evidence that this exact procedure was implemented or proved effective in the original project. The L-0007 evidence remains here for provenance.

## Connection to Specht

The origin is **SDD Inception**: Rodney was trying to fix the problem in the repository where the drift had occurred. In the fix request, he asked the agent to “think of the spec like a JSON Schema.” Specht grew out of that request, and SDD guided its development. On November 5, 2026, Rodney emphasized its purpose as an analysis tool he built to keep tabs on the specification process. This origin account was supplied by Rodney on October 28, 2026. The schema analogy means enforcing a checkable specification contract; it does not claim that schema validation establishes the meaning or truth of arbitrary claims. These PRs evidence the motivating failures; they do not document every step of Specht's construction.

Specht can check explicit structural contracts such as required rows and reference validity. Do not imply that it can automatically decide the correct exit category, identify every semantically incorrect tag, or verify the truth of arbitrary lesson prose. The separate checkout walkthrough in slides 24–27 remains an illustrative explanation of the checker, not a replay of either hooked PR.

## An early result from sharing the talk — slide 34

On November 5, 2026, Rodney reported that he gave the first iteration of this
talk to a buddy, who showed him working software three days later. The speaker notes
use this as a concrete personal account of someone applying the approach.
No project details, independent assessment, comparison baseline, measured
speedup, or use of Specht were supplied. The three days describe the interval
between sharing the talk and seeing the software, not verified development time.
