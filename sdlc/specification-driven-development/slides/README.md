# Welcome to [Slidev](https://github.com/slidevjs/slidev)!

To start the slide show:

- `npm install`
- `npm run dev`
- visit <http://localhost:3030>

Edit the [slides.md](./slides.md) to see the changes.

Learn more about Slidev at the [documentation](https://sli.dev/).

## SDD Charcoal theme

This talk uses its own local theme in `theme/`: a dark charcoal canvas, soft
warm-gray text, muted teal structure, and copper emphasis. The dark background
supports the presenter's preference for lower screen brightness. Examples retain
the flat excerpt style and presentation typography. Colors, typography, and
spacing live in `theme/styles/theme.css`; Mermaid colors live in
`theme/setup/mermaid.ts` because Slidev renders diagrams inside shadow roots.
No theme publication or additional package installation is needed.

The theme targets the visual and interaction requirements in the
[Section 508 web guidance](https://www.section508.gov/develop/guide-accessible-web-design-development/).
Measured contrast against the charcoal background (`#202425`):

| Use | Color | Contrast |
| --- | --- | --- |
| Main text | `#d5d7d2` | 10.80:1 |
| Teal headings and links | `#83bdb7` | 7.41:1 |
| Copper emphasis | `#dca58a` | 7.30:1 |
| Secondary text | `#acb5b2` | 7.46:1 |

Code text has 9.22:1 contrast against its excerpt background (`#2b3031`);
teal inline code has 6.32:1 contrast against that background.
Links stay underlined, diff examples retain their `+`/`-` markers, keyboard focus
has a visible outline, and transitions honor reduced-motion preferences. Fonts
use local system fallbacks. Illustrations retain descriptive alt text, and all
three diagrams have accessible titles and descriptions.

The audience controls provide Previous/Next steps, a titled slide chooser with
an explicit Go action, fullscreen, and a presenter link. **Reading layout** makes
the slide fluid for browser zoom: text wraps, columns stack, and diagrams can be
scrolled with the keyboard.

The [accessibility review](accessibility-review.md) records the 99-state browser
scan, structural checks, keyboard tests, real Safari 200% zoom, contrast results,
and a repeatable check script. VoiceOver testing remains pending at the
presenter's request. PDF/PPTX exports need separate review if distributed.
No full Section 508 conformance claim is made.

## Delivery notes

The deck is timed for 50 minutes: 47 minutes of teaching, walkthrough, and closing takeaways, and three minutes for questions. Per-slide timing and narration are in the Slidev speaker notes.

| Slides | Segment | Minutes |
| --- | --- | --- |
| 1–8 | The specification returns | 8 |
| 9–12 | How agents use the spec | 6 |
| 13–18 | The token horror story | 9 |
| 19–24 | Building a way out | 8.5 |
| 25–31 | Owning and adapting the workflow | 12.5 |
| 32–34 | Earlier verification, close, and questions | 6 |

The horror story uses actual review evidence from a private project: a contradictory claim copied into a scaffold, missing traceability, a mismatched scenario tag, and corrupted lessons. See [the evidence notes](./pr-evidence.md) for pinned artifacts and correction history. The separate checkout validation walkthrough remains illustrative; it is not output from those PRs. The checkout excerpts use the canonical acceptance-criteria and traceability table columns. The expected SPEC031 message follows the schema 0.2.1 rule reference, wrapped for readability, and keeps `AC-002` throughout the failure and repair. These examples assume an `AC-` claim grammar in the manifest; Specht's default uses `B-`. The examples are excerpts, not complete runnable fixtures or captured CLI output. Slide 24 turns SPEC031 into a worked checker requirement: zero matrix rows fails, one passes that rule, and two fails. The examples illustrate BDD agreement and TDD with a valid control, rather than reconstructing Specht's implementation history. Slide 25 distinguishes structural evidence from judging the incident's valid-but-wrong tag and contradictory claim. The walkthrough is slide-only; no live demo or installation is required during delivery.

Use the [Specht quick start](https://github.com/RLittlesII/specht#quick-start) for current installation instructions. This is an optional resource for questions; installation is outside the teaching close.

The close focuses on monitoring the SDD process: retain the revision and validation/test results at each handoff, assign gaps to people, and turn repeated findings into deterministic checks. Human responsibilities are explicit on slide 27. BDD establishes shared examples; TDD drives implementation and the tooling itself. AI helps maintain the specification, claims, and verification artifacts, while programs check explicit rules and humans own intent and acceptance.

“SDD Inception” frames the tool's origin: while trying to fix the affected repository, Rodney asked the agent to think of the spec like a JSON Schema, and Specht grew from that request. Slide 18 includes his reported review effort: over 200 PR comments, about half the reviews away from GitHub, 20+ AI reviews taking 10+ rounds each, and a recalled high of 18 rounds. These figures are his account, separate from the pinned artifact evidence.

The audience-facing deck identifies the source as a private-project review incident. It omits the repository name, private GitHub URLs, PR numbers, and commit hashes. Original provenance stays in the separate evidence notes; the concrete claim IDs, findings, and correction remain in the slides.

Slide 14, “The Files Behind the Workflow,” introduces feature-local `.spec/` agreements, scenarios, ADRs, product decisions, and lessons, and distinguishes them from `.specht/` tool configuration and templates. This is the incident's repository convention. Approved requirements can live elsewhere; the source and revision must remain explicit. Agents consume the records; the checker enforces selected explicit rules.

Slide 15, “The Claim in This Incident,” applies the earlier claim vocabulary to the replay requirement before the actual drift evidence on slides 16–18. The replay wording is explanatory phrasing; the following drift slide retains the actual findings.

Audience-facing claim IDs use `AC-` (acceptance criterion). The original evidence uses `B-`; prefixes are mapped consistently in teaching examples, PR excerpts, and the diagnostic, with original IDs retained in the evidence notes.

Slides 29–32 cover practical tradeoffs and process variations. Slide 29 includes source and code revisions in a distributed-team handoff, with an owner for conflicting edits. Slide 30 balances reduced steering and better autonomous effort against document volume, duplication, synchronization, and token/build/review capacity. The developer thinks through the problem; the agent does the fast implementation work. AI can help screen documentation, while explicit checks and human review still address its omissions and changes in meaning.

Slide 31 distinguishes repository-owned requirements, tracker-owned requirements (GitHub Issues, Azure DevOps, or Trello), and repository-owned requirements published to a tracker by a build task. The last is a proposed integration option, not a built pipeline or Specht capability. Retain stable mappings, source revisions, publication status, and a route for proposed changes. Name the authority for requirement wording, work status, and approval, and make that information accessible to people without repository access. A local copy is task context when the tracker owns the requirement; it must not silently become a competing authority.

Brownfield adoption remains in slide 31's callout and narration, using Rodney's current experience on a large existing project: start with available requirements wherever they live, clarify areas when changing them or fixing bugs, and fill gaps incrementally. Whole-system backfilling is not a prerequisite. Existing behavior is evidence to examine, not automatically the intended requirement.

Slide 32 moves build and test feedback into the agent's work while retaining CI verification of the shared change. It covers the commands and environment needed to obtain evidence, stage-appropriate red tests, task budgets, stale sources, and escalation when checks cannot run or repeated retries stop making progress. These are workflow responsibilities, not Specht orchestration features.

## Table linting

Slide tables use leading/trailing pipes, padded columns, matching cell counts,
and blank lines around rendered tables. Table excerpts inside Markdown and diff
code fences follow the same column shapes. Verified with markdownlint-cli2 0.24.0 (markdownlint 0.42.0), using table rules
MD055, MD056, MD058, and MD060 (`aligned` style): zero issues. Fenced examples
were extracted and linted separately after removing diff markers. Slidev frontmatter, reveal components, and speaker notes
are outside this table-only lint scope.

## Narrative and delivery arc

The 34-slide, 50-minute sequence follows six beats:

1. Slides 1–8 recover the useful parts of specifications and introduce AI as
   a new reader and author whose edits also need checking. Immediately after
   “The Spec Is Back,” slides 7–8 open the modern document: frontmatter, the twelve
   numbered sections, and metadata that distinguishes agreement from delivery.
2. Slides 9–12 connect SDD to BDD and TDD, define the actors and artifacts,
   assign agent jobs, then follow a checkout claim into a Gherkin scenario,
   bindings, executable assertions, and implementation.
3. Slides 13–18 show the actual review nightmare: the artifacts, contradictory
   claim, drifting references, and reported review effort.
4. Slide 19 gives the initiating request: fix the affected repository by thinking
   of the spec like a JSON Schema. Slide 20 introduces the resulting Specht tool;
   slides 21–23 return to checkout for one repair. Slide 24 specifies the checker
   rule itself with failing examples and a passing control.
5. Slides 25–29 return to the SDD process: distinguish evidence from acceptance,
   repeat checks after edits, and assign blocked decisions. The checkout expiry
   boundary and payment ordering examples are in the notes for slide 23.
   Slide 28 turns the actual L-0009 finding into an example lesson, skill
   procedure, and next-review evidence. That response
   is illustrative, not a claim of an implemented or proven historical repair.
   Slide 29 shows what the handoff carries.
6. Slides 30–34 cover upsides and maintenance costs, source-of-truth variations
   and access, incremental brownfield adoption, and earlier build/test feedback.
   The close returns to focused tools and who acts on the evidence.

Bridge from the real replay incident back to checkout explicitly: the missing-row
mechanism is the same; the worked example is illustrative. End slide 18 with the token-burning metaphor and pause before revealing the
realization that a program could check those recurring rules. Keep the schema
request before the tool introduction. Distinguish a reproducible structural
finding from semantic judgment, and describe a human escalation route so the
proposed workflow does not reproduce the original unbounded review loop.

Slide 17 foreshadows mistakes in the lessons so slide 28 returns to an established
thread. Keep the detailed evidence-versus-acceptance distinction on slide 25;
slides 26–27 apply it to the revision loop and human decisions.
Slide 26 puts specification, scenario, test, and code edits before the checks;
every correction repeats validation and test execution. A demonstrated red test
can be an agreed handoff to implementation; final acceptance requires the relevant
tests to pass. Slide 24 takes 1.5 minutes and slide 28 takes 2 minutes for the
worked lessons-to-skills loop. Slides 30–32 take 3, 2.5, and 2 minutes.
The history, actor primer, checkout introduction, and diagnostic walkthrough
are tighter to make room, keeping 50 minutes overall and three for questions.

## Vocabulary before the workflow

Slide 10 distinguishes humans, AI agents, and checking programs from the artifacts
they work on: a feature is a scoped product capability, a specification is its
versioned agreement, and a claim is one numbered, testable requirement. Slide 11
assigns authoring, testing, implementation, and review jobs to agents. Slide 12
explains Gherkin, its Feature/Scenario/Given/When/Then structure, claim tags,
and the step definitions and runner needed to execute assertions against code.
A feature, its `.feature` file, a scenario, and an executable test are related
but distinct. Slide 14 then maps these concepts to repository artifacts.

The actor primer replaces the earlier artifact-chain diagram; the deck retains
34 slides, three diagrams, and the 50-minute timing allocation.

## Slide 19 diagram reveals

Four clicks reveal the Mermaid rectangles from left to right. Each incoming
arrow appears with its destination. The three bullets appear with rectangles
2–4, so the whole sequence takes four clicks. Backward navigation reverses the
reveals; reading layout shows the complete diagram and bullets.

The diagram uses named edges and click-dependent `themeCSS` selectors, with
`key: $clicks` to refresh Mermaid when the step changes. Keep the fence options
as ordinary JavaScript strings: template-literal backticks in a Markdown fence
info string break parsing. The full graph remains present for layout, keeping
the rectangles in fixed positions as they appear.

## Modern specification bridge

Slides 7–8 follow “The Spec Is Back” before TDD/BDD/SDD and agent roles.
“My Specification, 2026” shows all twelve numbered sections from the versioned
0.2.1 feature template, grouped by purpose; Tasks and Scoring remain identified
in narration as additional sections. “Frontmatter Is Part of the Contract”
shows an explicitly abbreviated checkout example using that version's keys and
values. An approved document can still have delivery in progress. These are
Rodney's chosen conventions, not a universal required format for AI.

The 1998 excerpt now names its surrounding document structure, and slide 5
connects the older discipline to the retrieval, versioning, and verification
needed when an agent reads and edits the agreement. The token-story opening
returns to frontmatter, sections, and references. Rodney supplied frontmatter's
role in the incident on November 5, 2026; no specific broken key or historical
diff is asserted. See the evidence notes for this distinction.

Specht is introduced as the analysis tool Rodney built to keep tabs on the
specification process. Frontmatter, section order, identities, traceability,
and dependencies are recurring analysis targets; findings feed repairs and
handoffs. The tool analyzes artifacts; the workflow decides when to run it
and how to respond. The checkout diagnostic remains pinned to schema 0.2.1.

The bridge adds three minutes and two slides. Shorter actor, checkout, artifact,
incident-claim, tool-introduction, and repair narration recovers three minutes.
The deck now has 34 slides and retains 50 minutes including three for questions.

Slide 30 includes Rodney's account of sharing the first iteration with a buddy
and seeing working software three days later. It is one concrete early result
from sharing the approach; the interval is not a measured productivity gain or
a claim about Specht usage. The slide allocates two minutes to tradeoffs and
one minute to the transition into team decisions.
