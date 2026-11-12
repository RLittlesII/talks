# PR evidence for the token horror story

Presenter provenance: these source links support the case study. The audience-facing deck uses anonymous original/follow-up PR labels and does not direct attendees to the private repository.

The story slides use hooked PRs [#214](https://github.com/RLittlesII/hooked/pull/214) and [#215](https://github.com/RLittlesII/hooked/pull/215). The findings below distinguish reviewed artifacts, review observations, and subsequent corrections. The account of a week's worth of tokens and the review-effort figures below come from Rodney; these PRs do not measure token consumption or capture reviews performed away from GitHub.

The slides display claim prefixes as `AC-` (acceptance criterion): source `B-011` becomes `AC-011`, for example. The tag correction and diagnostic are labeled as adapted identifiers. All original identifiers below remain unchanged.

Slide 9 shortens the `B-010` and `B-011` traceability rows at the reviewed
snapshot. Both rows were marked `Scaffolded`. [Reviewed matrix rows](https://github.com/RLittlesII/hooked/blob/4e95b5b7bdddeab266f8d0f7631b0a13307ca76d/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L2650-L2651)

The replay walkthrough displays the agreement as `.spec/README.md` and its
scenarios as the adjacent `replay.feature`, using the presenter's current layout.
The historical review used `epics/0005-cli/F5b-event-inspection-recovery/spec.md`
and `event-inspection-recovery.feature`. The original source links below retain
those names. “The Files Behind the Workflow” illustrates the current `.spec/`
structure with replay names, rather than reconstructing the historical directory
or exposing either repository's identity to the audience.

## Illustrative replay analyzer and evidence examples

Slides 27–29 keep replay as the running example. The XML links, display IDs,
and `Replay.Tests.csproj` are illustrative wiring and filenames,
not captured changes or commands from the historical review. Slide 29 corrects
the earlier illustrative `AC-099` link to `AC-011` and follows that claim from
`.spec/README.md` through its scenario, command, and test.

Analyzer link syntax comes from
the [SPCT0004 documentation](https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/codeanalysis/docs/SPCT0004.md).

## Frontmatter was part of the review problem

On November 5, 2026, Rodney clarified that frontmatter was also part of the
specification-review problem that burned his tokens. This is his account of
the incident. The evidence retained here does not identify the particular
frontmatter key, invalid value, or corrective diff; the slides do not invent
one. Slide 10 shows selected fields from the reviewed replay snapshot, with
feature identifiers adapted. The snapshot shows draft agreement and blocked
delivery. Those values are context, not a claim that either was the metadata defect.

For comparison with the later checker schema, see the
[v0.3.0 feature frontmatter schema](https://github.com/RLittlesII/specht/blob/ab13f3f6320cb14c7d8143c0d8630be279f2460c/src/tool/schema/v0.3.0/frontmatter/feature.schema.json).

## A wrong claim propagated into a scaffold — slide 19

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

## From the review correction to executable coverage

The follow-up snapshot at `4e95b5b` records the review correction to category 3
for store or configuration preconditions and busy or unreadable writes. The
feature file adds the corresponding category-3 scenario. The command
implementation at `29437f3e` routes a store precondition refusal through the
environment-error branch. The delivery record at `89fc9735` describes enabling
the previously deferred acceptance scenario and updating § 9. Its Given
substitutes a replay intake that returns `StoreBusy`. The acceptance test checks
the command's exit category and error without taking a database lock. Separate
intake unit tests exercise busy retry and unreadable-store refusal. Slides 26,
30, and 39 use these later artifacts. They do not present a historical
red-to-green run from the draft scaffold.

Slide 30 shows the added `WriteFailure` call and `EnvironmentError` return from
the command diff, with indentation and wrapping adjusted for projection. The command file
has identical contents at `29437f3e` and `89fc9735`. [Implementation diff](https://github.com/RLittlesII/hooked/commit/29437f3ea3af95b62ffc2f9221b6b061e14209f2), [command at the acceptance revision](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/src/Tool/Cli/Features/Events/ReplayEventCommand.cs#L159-L168)

The busy-or-unreadable scenario runs with an intake that reports `StoreBusy`.
`ReplaySteps.cs` supplies that refusal and checks the operator error.
`ListingSteps.cs` supplies the shared exit-category assertion. The traceability
row records a November 5 pass. [Scenario](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/epics/0005-cli/F5b-event-inspection-recovery/event-inspection-recovery.feature#L292-L301), [refusal setup](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/test/Acceptance/Features/Cli/Events/ReplaySteps.cs#L95-L102), [operator error check](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/test/Acceptance/Features/Cli/Events/ReplaySteps.cs#L173-L175), [exit-category check](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/test/Acceptance/Features/Cli/Events/ListingSteps.cs#L23-L31), [delivery record](https://github.com/RLittlesII/hooked/blob/89fc97358d280c24605cf9c57d7059bdc4c9738f/epics/0005-cli/F5b-event-inspection-recovery/spec.md#L1982)

On November 11, 2026, a fresh build of `test/Acceptance/Acceptance.csproj`
from an isolated archive of `89fc9735` ran the busy-store scenario with the
project's pinned .NET SDK. The focused xUnit run recorded one test with result
`Pass`, zero failed, and zero skipped. The scenario also passed in the full
acceptance run, which reported 622 passed and 85 skipped. Slide 30 shows the
focused result. To reproduce it from that revision after restore and build, run
the acceptance assembly with the exact scenario filter:

```sh
dotnet test/Acceptance/bin/Debug/net11.0/Hooked.Acceptance.dll \
  -method 'Hooked.Acceptance.Features.Cli.Events.EventInspectionRecoveryFeature.EventsReplayExitsInTheEnvironment_ErrorCategoryWhenTheStoreRefusesTheRequestForBeingBusyOrUnreadable' \
  -result-xml replay-acceptance.xml
```

## Missing rows and a mismatched claim tag — slide 20

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

## Reported review effort — slide 21

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

Specht can check explicit structural contracts such as required rows and reference validity. Do not imply that it can automatically decide the correct exit category, identify every semantically incorrect tag, or verify the truth of arbitrary lesson prose. Slide 25 applies the pinned checker rules to the findings retrospectively. It does not present a captured run on either PR.

## An early result from sharing the talk — slide 34

On November 5, 2026, Rodney reported that he gave the first iteration of this
talk to a buddy, who showed him working software three days later. The speaker notes
use this as a concrete personal account of someone applying the approach.
No project details, independent assessment, comparison baseline, measured
speedup, or use of Specht were supplied. The three days describe the interval
between sharing the talk and seeing the software, not verified development time.
