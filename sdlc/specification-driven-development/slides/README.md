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
use local system fallbacks. Illustrations retain descriptive alt text, and all
three diagrams have accessible titles and descriptions.

The audience controls provide Previous/Next steps, a titled slide chooser with
an explicit Go action, fullscreen, and a presenter link. **Reading layout** makes
the slide fluid for browser zoom: text wraps, columns stack, and diagrams can be
scrolled with the keyboard.

The [accessibility review](accessibility-review.md) records browser scans,
structural and keyboard checks, a Safari 200% zoom review, contrast results, and
the repeatable check script. VoiceOver testing remains pending at the
presenter's request. PDF/PPTX exports need separate review if distributed.

## Delivery notes

The 38-slide deck runs for 50 minutes. Use the section cards to pause and cue the next part. Each of the four new cards takes 15 seconds within its section.

| Slides | Section                      | Minutes |
| ------ | ---------------------------- | ------: |
| 1–9    | The spec returns             | 7.5     |
| 10–14  | One claim at work            | 6       |
| 15–20  | My Precious Tokens           | 6.5     |
| 21–30  | Who checks the spec?         | 16      |
| 31–37  | Make it your workflow        | 11      |
| 38     | Questions                    | 3       |

| Slide | Cue the next section                                                               |
| ----: | ---------------------------------------------------------------------------------- |
| 3     | “Let’s start with how we wrote down what software should do.”                      |
| 10    | “You’ve seen the agreement. Let’s follow one checkout claim into a test.”          |
| 21    | “I kept paying for another review. What could I make the build catch?”             |
| 31    | “You’ve seen what I get back. What would your team need before signing off?”       |

On slide 7, pause on the binder and say “old busted hotness.” Click once to reveal AI, then open the checkout spec. Slide 15 switches to the private replay incident. Slide 20 keeps the pause before the realization that a program could check the recurring rules. Use the [private evidence notes](./pr-evidence.md) to keep the reviewed artifacts separate from Rodney’s reported review effort.

Slide 22 reveals the four SDD Inception boxes in order. Each incoming arrow appears with its destination. The three bullets appear with boxes 2–4. Backward navigation reverses the reveals, and reading layout shows the complete diagram and bullets.

## Follow AC-002

- **Agree and drive the test.** Slides 8–14 open the feature spec, introduce Gherkin in `checkout.feature`, and show how writing the scenario makes the expected behavior explicit. Bind and run the example red, then make the same test pass. Its assertions check the unchanged $50 total, expired-code message, and zero payment authorization calls.
- **Check the links.** Slides 24–26 restore the missing matrix row, work through red and green, and show a mistyped C# claim link. The checkout excerpts are illustrative. The manifest assumes an `AC-` grammar. Schema 0.3.0 permits an approved agreement with a `Missing` test during delivery. `SPEC7001` reports missing coverage at delivery status `done`.
- **Inspect the evidence.** Slide 27 separates Roslyn’s build-time checks from discovery and execution. `specht evidence` reconciles the supplied artifacts against current inputs. Slides 28–29 show the public Specht claim `0201-F7/B-030` and its own formatting workflow. The formatter applies eligible C# fixes locally, and CI verifies the policy.
- **Accept and repeat.** Sign off on checkout at slide 30. Slides 32–35 cover lessons, a revision-specific handoff, the costs of maintenance, and where approved intent lives. Slide 36 proposes an exact expiry boundary and requires fresh approval and evidence. The friend’s three-day story is optional speaker narration on slide 34.

This is a slide-only walkthrough. Specht source links are pinned to `v0.4.2-preview`. Use the [Specht quick start](https://github.com/RLittlesII/specht#quick-start) for installation questions.

## Historical annotation

The earlier walkthrough showed a real GitHub Actions warning from Specht PR #277. Under the older schema, `SPEC060` flagged the approved mirror spec for marking claim `B-010`’s test `Missing`. The Specht step succeeded. An integration test failed later in the job. The current checkout walkthrough uses schema 0.3.0 and a different missing-row finding. [Original PR annotation](https://github.com/RLittlesII/specht/pull/277/files#annotation_94350672913), [Actions run](https://github.com/RLittlesII/specht/actions/runs/40158738468)

![Specht warning SPEC060 on line 184 of the approved mirror spec, where B-010 has a missing test](./public/images/specht-spec060-github-warning.png)
