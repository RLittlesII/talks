# Welcome to [Slidev](https://github.com/slidevjs/slidev)!

To start the slide show:

- `npm install`
- `npm run dev`
- visit <http://localhost:3030>

Edit the [slides.md](./slides.md) to see the changes.

Learn more about Slidev at the [documentation](https://sli.dev/).

## Delivery notes

The deck is timed for 50 minutes: 45 minutes of teaching and walkthrough, two minutes of closing takeaways, and three minutes for questions. Per-slide timing and narration are in the Slidev speaker notes.

| Slides | Segment | Minutes |
| --- | --- | --- |
| 1–6 | The specification returns | 6 |
| 7–10 | How agents use the spec | 8 |
| 11–14 | The token horror story | 10 |
| 15–20 | Building a way out | 12 |
| 21–27 | Keeping the workflow focused | 9 |
| 28–30 | Start today and questions | 5 |

The horror story now uses actual evidence from hooked PRs [#214](https://github.com/RLittlesII/hooked/pull/214) and [#215](https://github.com/RLittlesII/hooked/pull/215): a contradictory claim copied into a scaffold, missing traceability, a mismatched scenario tag, and corrupted lessons. See [the evidence notes](./pr-evidence.md) for pinned artifacts and correction history. The separate checkout validation walkthrough remains illustrative; it is not output from those PRs. The SPEC031 diagnostic comes from Specht's README and is split for readability. The walkthrough is slide-only; no live demo or installation is required during delivery.

The current [Specht quick start](https://github.com/RLittlesII/specht#quick-start) documents installation through authenticated GitHub Packages. Use that link for installation: the older setup page still contains a stale publication notice. Authentication details and initialization behavior are in the adoption slide's speaker notes.

The closing principle is independent of Specht: use deterministic processes to catch mechanically detectable drift, while humans review intent and tests verify behavior.

“SDD Inception” frames the tool's origin: SDD was used to build software, exposed drift in that process, and was then used to build Specht for SDD.
