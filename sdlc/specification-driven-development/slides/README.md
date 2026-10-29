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

The deck is timed for 50 minutes: 45 minutes of teaching and walkthrough, two minutes of closing takeaways, and three minutes for questions. Per-slide timing and narration are in the Slidev speaker notes.

| Slides | Segment | Minutes |
| --- | --- | --- |
| 1–6 | The specification returns | 6 |
| 7–10 | How agents use the spec | 8 |
| 11–16 | The token horror story | 10 |
| 17–22 | Building a way out | 11 |
| 23–30 | Keeping the workflow focused | 10 |
| 31–33 | Monitor, build checks, and questions | 5 |

The horror story uses actual review evidence from a private project: a contradictory claim copied into a scaffold, missing traceability, a mismatched scenario tag, and corrupted lessons. See [the evidence notes](./pr-evidence.md) for pinned artifacts and correction history. The separate checkout validation walkthrough remains illustrative; it is not output from those PRs. The checkout excerpts use the canonical acceptance-criteria and traceability table columns. The expected SPEC031 message follows the schema 0.2.1 rule reference, wrapped for readability, and keeps `AC-002` throughout the failure and repair. These examples assume an `AC-` claim grammar in the manifest; Specht's default uses `B-`. The examples are excerpts, not complete runnable fixtures or captured CLI output. Slide 22 connects SPEC031, SPEC021, SPEC030, and SPEC060 to concrete edits and distinguishes them from the incident's valid-but-wrong tag and contradictory claim. The walkthrough is slide-only; no live demo or installation is required during delivery.

The current [Specht quick start](https://github.com/RLittlesII/specht#quick-start) documents installation through authenticated GitHub Packages. Use that link for installation: the older setup page still contains a stale publication notice. This is an optional resource for questions; installation is outside the teaching close.

The close focuses on monitoring the SDD process: retain the revision and validation/test results at each handoff, assign gaps to people, and turn repeated findings into deterministic checks. Human responsibilities are explicit on slide 25. BDD establishes shared examples; TDD drives implementation and the tooling itself. AI helps maintain the specification, claims, and verification artifacts, while programs check explicit rules and humans own intent and acceptance.

“SDD Inception” frames the tool's origin: while trying to fix the affected repository, Rodney asked the agent to think of the spec like a JSON Schema, and Specht grew from that request. Slide 16 includes his reported review effort: over 200 PR comments, about half the reviews away from GitHub, 20+ AI reviews taking 10+ rounds each, and a recalled high of 18 rounds. These figures are his account, separate from the pinned artifact evidence.

The audience-facing deck identifies the source as a private-project review incident. It omits the repository name, private GitHub URLs, PR numbers, and commit hashes. Original provenance stays in the separate evidence notes; the concrete claim IDs, findings, and correction remain in the slides.

Slide 12, “The Files Behind the Workflow,” introduces feature-local `.spec/` agreements, scenarios, ADRs, product decisions, and lessons, and distinguishes them from `.specht/` tool configuration and templates. Agents consume the records; the checker enforces selected explicit rules.

Slide 13, “What Is a Claim?”, defines the stable requirement ID, testable behavior, scenarios, and traceability before the actual drift evidence on slides 14–16. The replay wording is explanatory phrasing; the following drift slide retains the actual findings.

Audience-facing claim IDs use `AC-` (acceptance criterion). The original evidence uses `B-`; prefixes are mapped consistently in teaching examples, PR excerpts, and the diagnostic, with original IDs retained in the evidence notes.

Slide 30 addresses brownfield adoption using Rodney's current experience on a large existing project: start with available requirements, clarify and specify areas when changing them or fixing bugs, and fill gaps incrementally. Whole-system backfilling is not a prerequisite. Existing behavior is evidence to examine, not automatically the intended requirement.

## Table linting

Slide tables use leading/trailing pipes, padded columns, matching cell counts,
and blank lines around rendered tables. Table excerpts inside Markdown and diff
code fences follow the same column shapes. Verified with markdownlint-cli2 0.24.0 (markdownlint 0.42.0), using table rules
MD055, MD056, MD058, and MD060 (`aligned` style): zero issues. Fenced examples
were extracted and linted separately after removing diff markers. Slidev frontmatter, reveal components, and speaker notes
are outside this table-only lint scope.

## Narrative and delivery arc

The 33-slide, 50-minute sequence follows six beats:

1. Slides 1–6 recover the useful parts of specifications and introduce AI as
   a new reader and author whose edits also need checking.
2. Slides 7–10 connect SDD to BDD and TDD, define the actors and artifacts,
   assign agent jobs, then follow a checkout claim into a Gherkin scenario,
   bindings, executable assertions, and implementation.
3. Slides 11–16 show the actual review nightmare: the artifacts, contradictory
   claim, drifting references, and reported review effort.
4. Slide 17 gives the initiating request: fix the affected repository by thinking
   of the spec like a JSON Schema. Slide 18 introduces the resulting Specht tool;
   slides 19–22 return to checkout to explain one repair and the rules' limits.
5. Slides 23–28 return to the SDD process: a passing check is not acceptance,
   repeated failures need human diagnosis, people own decisions, testers challenge
   behavior, lessons must change procedures, and handoffs carry inspectable evidence.
6. Slides 29–33 cover maintenance, incremental brownfield adoption, monitoring,
   building focused checks, and questions. The closing asks what to verify and
   who acts on the evidence.

Bridge from the real replay incident back to checkout explicitly: the missing-row
mechanism is the same; the worked example is illustrative. Keep the schema
request before the tool introduction. Distinguish a reproducible structural
finding from semantic judgment, and describe a human escalation route so the
proposed workflow does not reproduce the original unbounded review loop.

## Vocabulary before the workflow

Slide 8 distinguishes humans, AI agents, and checking programs from the artifacts
they work on: a feature is a scoped product capability, a specification is its
versioned agreement, and a claim is one numbered, testable requirement. Slide 9
assigns authoring, testing, implementation, and review jobs to agents. Slide 10
explains Gherkin, its Feature/Scenario/Given/When/Then structure, claim tags,
and the step definitions and runner needed to execute assertions against code.
A feature, its `.feature` file, a scenario, and an executable test are related
but distinct. Slide 12 then maps these concepts to repository artifacts.

The actor primer replaces the earlier artifact-chain diagram; the deck retains
33 slides, three diagrams, and the 50-minute timing allocation.
