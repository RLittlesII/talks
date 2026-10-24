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
four diagrams have accessible titles and descriptions.

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
| 31–33 | Start today and questions | 5 |

The horror story uses actual review evidence from a private project: a contradictory claim copied into a scaffold, missing traceability, a mismatched scenario tag, and corrupted lessons. See [the evidence notes](./pr-evidence.md) for pinned artifacts and correction history. The separate checkout validation walkthrough remains illustrative; it is not output from those PRs. The SPEC031 diagnostic is adapted from Specht's README, split for readability, with claim prefixes shown as `AC-`. The walkthrough is slide-only; no live demo or installation is required during delivery.

The current [Specht quick start](https://github.com/RLittlesII/specht#quick-start) documents installation through authenticated GitHub Packages. Use that link for installation: the older setup page still contains a stale publication notice. Authentication details and initialization behavior are in the adoption slide's speaker notes.

The closing principle is independent of Specht: use deterministic processes to catch mechanically detectable drift, while humans review intent and tests verify behavior.

“SDD Inception” frames the tool's origin: SDD was used to build software, exposed drift in that process, and was then used to build Specht for SDD.

The audience-facing deck identifies the source as a private-project review incident. It omits the repository name, private GitHub URLs, PR numbers, and commit hashes. Original provenance stays in the separate evidence notes; the concrete claim IDs, findings, and correction remain in the slides.

Slide 12, “The Files Behind the Workflow,” introduces feature-local `.spec/` agreements, scenarios, ADRs, product decisions, and lessons, and distinguishes them from `.specht/` tool configuration and templates. Agents consume the records; the checker enforces selected explicit rules.

Slide 13, “What Is a Claim?”, defines the stable requirement ID, testable behavior, scenarios, and traceability before the actual drift evidence on slides 14–16. The replay wording is explanatory phrasing; the following drift slide retains the actual findings.

Audience-facing claim IDs use `AC-` (acceptance criterion). The original evidence uses `B-`; prefixes are mapped consistently in teaching examples, PR excerpts, and the diagnostic, with original IDs retained in the evidence notes.

Slide 30 addresses brownfield adoption using Rodney's current experience on a large existing project: start with available requirements, clarify and specify areas when changing them or fixing bugs, and fill gaps incrementally. Whole-system backfilling is not a prerequisite. Existing behavior is evidence to examine, not automatically the intended requirement.
