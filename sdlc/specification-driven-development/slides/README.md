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

| Use                     | Color     | Contrast |
| ----------------------- | --------- | -------- |
| Main text               | `#d5d7d2` | 10.80:1  |
| Teal headings and links | `#83bdb7` | 7.41:1   |
| Copper emphasis         | `#dca58a` | 7.30:1   |
| Secondary text          | `#acb5b2` | 7.46:1   |

Code text has 9.22:1 contrast against its excerpt background (`#2b3031`);
teal inline code has 6.32:1 contrast against that background.
Commands, CLI output, and diffs share a terminal frame. The `$` prompt
stays out of copied commands. Diff additions and removals retain their
`+`/`-` markers and have text contrast of 8.53:1 and 9.14:1, respectively.
Links stay underlined, keyboard focus has a visible
outline, and transitions honor reduced-motion preferences. Fonts
use local system fallbacks. Illustrations have descriptive alt text. The
infographics use selectable text in reading order, and Mermaid diagrams have
accessible titles and descriptions.

The audience controls provide Previous/Next steps, a titled slide chooser with
an explicit Go action, fullscreen, and a presenter link. **Reading layout** makes
the slide fluid for browser zoom: text wraps, columns stack, and diagrams can be
scrolled with the keyboard.

The [accessibility review](accessibility-review.md) records browser scans,
structural and keyboard checks, a Safari 200% zoom review, contrast results, and
the repeatable check script. VoiceOver testing remains pending at the
presenter's request. PDF/PPTX exports need separate review if distributed.

## Delivery notes

The 40-slide deck runs for 50 minutes. Use the section cards to pause and cue the next part. Each of the four cards takes 15 seconds within its section.

| Slides | Section                      | Minutes |
| ------ | ---------------------------- | ------: |
| 1–10   | The spec returns             | 7.5     |
| 11–15  | One claim at work            | 6       |
| 16–21  | My Precious Tokens           | 6.5     |
| 22–31  | Who checks the spec?         | 16      |
| 32–39  | Make it your workflow        | 11      |
| 40     | Questions                    | 3       |

| Slide | Cue the next section                                                                           |
| ----: | ---------------------------------------------------------------------------------------------- |
| 4     | “Let’s start with how we wrote down what software should do.”                                  |
| 11    | “Let’s turn that checkout claim into a feature file and record the design decision behind it.” |
| 22    | “I kept paying for another review. What could I make the build catch?”                         |
| 32    | “You’ve seen what I get back. What would your team need before signing off?”                   |

Slide 7 turns from the cost of keeping the binder current to the reason Rodney brought the spec back. AI can help maintain it as code changes. On slide 8, click once for each panel: the binder, AI chat, then AI maintaining the spec. Click a fourth time for the closing line. Slide 15 turns from what AI maintains to what it maintained poorly. Slide 16 starts the private replay incident. On slide 21, reveal the request for review, then the return path before showing the review counts. Use the [private evidence notes](./pr-evidence.md) to distinguish reviewed artifacts from Rodney’s reported review effort.

Slide 23 reveals the four SDD Inception boxes in order. Each incoming arrow appears with its destination. The three bullets appear with boxes 2–4. Backward navigation reverses the reveals, and reading layout shows the complete diagram and bullets.

## Follow AC-002

- **Follow the documents into the test.** Slides 9–10 open the spec and its frontmatter. Slides 12–13 create `checkout.feature` in Gherkin and record why checkout checks expiry before calling the payment gateway. Slide 14 binds the scenario and runs it red, then makes the same test pass. On slide 15, clicks 1–3 highlight `AC-002` in the spec, the Gherkin tag, and the ADR. Click 4 replaces the scenario tag with `@AC-020`, which the spec doesn’t declare.
- **Check the links.** Slide 24 uses an illustrated agent checklist for `AC-002` to show Specht and Format queued after the edits and test. Slide 25 runs Specht against the same `@AC-020` tag shown on slide 15 and shows the `SPEC4002` diagnostic. Click once to show the passing rerun after restoring `@AC-002`. Point to the `Missing` test in the matrix to transition into TDD. On slide 26, reveal the failing `AC-002` test, the expiry guard diff, and the passing rerun with one click each. Slide 27 shows a mistyped C# claim link. The checkout excerpts are illustrative. The manifest assumes an `AC-` grammar. Schema 0.3.0 permits an approved agreement with a `Missing` test during delivery. `SPEC7001` reports missing coverage at delivery status `done`.
- **Inspect the evidence.** Slide 28 separates Roslyn’s build-time checks from discovery and execution. `specht evidence` reconciles the supplied artifacts against current inputs. Slides 29–30 show the public Specht claim `0201-F7/B-030` and its own formatting workflow. The formatter applies eligible C# fixes locally, and CI verifies the policy.
- **Accept and repeat.** Reveal the three passing checkout assertions on slide 31, then click for the sign-off. Slides 33–36 cover lessons, a revision-specific handoff, the costs of maintenance, and where approved intent lives. On slide 37, ask what happens at the exact expiry instant, then reveal the proposed rule and the need for fresh approval and evidence on separate clicks. The friend’s three-day story is optional speaker narration on slide 35. Slide 38 asks what to review when the code has changed since the last test run. Slide 39 returns to the opening question and invites the audience to try the workflow with one claim.

This is a slide-only walkthrough. Specht source links are pinned to `v0.4.2-preview`. Use the [Specht quick start](https://github.com/RLittlesII/specht#quick-start) for installation questions.

## Historical annotation

The earlier walkthrough showed a real GitHub Actions warning from Specht PR #277. Under the older schema, `SPEC060` flagged the approved mirror spec for marking claim `B-010`’s test `Missing`. The Specht step succeeded. An integration test failed later in the job. The current checkout walkthrough uses schema 0.3.0 and a different missing-row finding. [Original PR annotation](https://github.com/RLittlesII/specht/pull/277/files#annotation_94350672913), [Actions run](https://github.com/RLittlesII/specht/actions/runs/40158738468)

![Specht warning SPEC060 on line 184 of the approved mirror spec, where B-010 has a missing test](./public/images/specht-spec060-github-warning.png)
