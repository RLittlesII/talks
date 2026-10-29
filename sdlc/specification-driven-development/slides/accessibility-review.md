# SDD deck accessibility review

Review date: October 23, 2026 (America/Chicago).

**Browser review complete for the audience-facing web deck. VoiceOver testing
remains pending at the presenter's explicit request. This is not a full
Section 508 conformance claim or an Accessibility Conformance Report.**

## Scope and evidence

The review covers all 33 slides in the production build, the audience navigation,
and the fluid reading layout. Presenter/editor interfaces and PDF/PPTX exports
are separate delivery surfaces and are outside this browser review.

The baseline is the WCAG 2.0 A/AA content requirements referenced by
[Section 508 E205.4](https://www.section508.gov/ict-accessibility/).
Additional automated WCAG 2.1 A/AA and best-practice rules were enabled, but this
does not establish WCAG 2.1 conformance.

- axe-core 4.14.0 with Playwright 1.62.1 and Chromium 151.0.7922.34:
  99 states, covering each slide before reveals, after all reveals,
  and in reading layout at a 640 × 400 CSS-pixel viewport.
- Structural assertions: one exposed main landmark and slide heading, English
  language, unique document IDs, loaded/described images, column-header scopes,
  and resolvable diagram titles and descriptions.
- Keyboard checks: reveal/advance with Enter, explicit selection with Go, visible
  code-copy focus, reading-position reset, outgoing-slide exclusion, diagram
  scrolling, fullscreen, boundary-button states, and leaving the final control.
- Safari 26.4: native keyboard focus using Option-Tab, Space/Down/Return in the
  slide chooser, keyboard activation of Go, and real 200% browser zoom.
  Selecting an option left the current slide unchanged until Go was activated.
  Safari's page menu displayed 200%.
  Enlarged slide text, code, and table content remained reachable by scrolling.
  The temporary test tabs were closed and zoom was restored.
- Chromium accessibility-tree inspection confirmed the diagram's accessible
  name and description. Browser accessibility trees were inspected; no spoken
  screen-reader output was tested.

The repeatable check is [scripts/check-accessibility.mjs](scripts/check-accessibility.mjs).
It saves JSON results and the exposed slide accessibility trees under the selected
output directory (by default, `.cache/sdd-accessibility` from the working directory).
The final automated run reports zero axe violations and eight passed interaction
checks.

## Fixes made during review

1. Added a named main landmark, slide-specific document titles, explicit table
   header scopes, and a polite announcement of the current slide and reveal step.
2. Removed inactive slides from the accessibility tree and focus order, including
   during transitions.
3. Replaced the audience toolbar with labelled native controls. The upstream
   toolbar is hidden in audience mode because it contains nested buttons.
   Presenter tools retain their separate interface.
4. Added an explicit Go button, so changing the selected option does not itself
   change the slide or move focus.
5. Added reading layout for browser zoom. Text wraps, columns stack, and the page
   scrolls vertically. Diagrams retain full-size labels inside named, focusable
   scroll regions. Reading layout reveals all content and its Previous/Next
   controls move between whole slides. Titles retain their text size as the
   viewport narrows, so responsive sizing does not counteract browser zoom.
6. Isolated select and diagram keys from Slidev navigation shortcuts. Code-copy
   controls become fully visible on keyboard focus.
7. Kept closed jump controls out of the accessibility tree and disabled unused
   audience recording, drawing, and custom context-menu features.

## Contrast and visual review

The lowest measured text contrast is **6.32:1**, above the 4.5:1 normal-text
minimum. The palette measurements are in [README.md](README.md#sdd-charcoal-theme).
The check includes code and inline identifiers; active navigation uses the same
foreground/background palette. Disabled controls are exempt from text-contrast
requirements.

Axe marks some fitted-slide contrast results as incomplete because of overlapping
transformed slide containers. These are not counted as automated passes. They
were resolved with computed foreground/background measurements and visual review,
including the SVG labels and navigation. The charcoal canvas, excerpt surfaces,
teal links, and copper emphasis all retain readable contrast.

The illustrations have descriptive alt text and visible captions. Their incidental
drawn lettering does not carry requirements or instructions. Diff examples retain
their literal plus/minus signs and explanatory prose. Links remain underlined.
There is no information conveyed only by color, position, shape, or sound.
There is no audio/video, automatic slide advancement, flashing content, or
time-limited audience interaction. Reveals are user-operated; reduced-motion
preferences suppress slide transitions.

## WCAG 2.0 A/AA browser checklist

“Reviewed” describes the browser evidence above, not a complete conformance
determination. “AT pending” means spoken assistive-technology verification remains
open. “N/A” means the relevant content or interaction is absent in this deck.

| Criterion | Browser result and evidence |
| --- | --- |
| 1.1.1 Non-text Content | Reviewed: three described images, four named/described diagrams. AT pending. |
| 1.2.1 Audio-only and Video-only | N/A: no time-based media. |
| 1.2.2 Captions (Prerecorded) | N/A: no prerecorded audio/video. |
| 1.2.3 Audio Description or Media Alternative | N/A: no prerecorded video. |
| 1.2.4 Captions (Live) | N/A: no live media within the web deck. |
| 1.2.5 Audio Description (Prerecorded) | N/A: no prerecorded video. |
| 1.3.1 Info and Relationships | Reviewed: headings, lists, native controls, table headers, SVG descriptions. AT pending. |
| 1.3.2 Meaningful Sequence | Reviewed: DOM and accessibility-tree order match the slide's reading order. AT pending. |
| 1.3.3 Sensory Characteristics | Reviewed: instructions and graph descriptions do not require sensory cues alone. |
| 1.4.1 Use of Color | Reviewed: links, diff signs, labels, and explanations provide additional cues. |
| 1.4.2 Audio Control | N/A: no audio playback. |
| 1.4.3 Contrast (Minimum) | Reviewed: measured text minimum 6.32:1; manual resolution of axe incomplete results. |
| 1.4.4 Resize Text | Reviewed: reading layout plus actual Safari 200% zoom; all slides checked at narrow viewport. |
| 1.4.5 Images of Text | Reviewed: instructional text and code remain text; logos and incidental artwork are exceptions. |
| 2.1.1 Keyboard | Reviewed: navigation, chooser/Go, reading toggle, code copy, diagram scrolling, fullscreen, links. |
| 2.1.2 No Keyboard Trap | Reviewed: controls can be entered and exited; native menus can be dismissed. |
| 2.2.1 Timing Adjustable | N/A: no audience time limits or automatic progression. |
| 2.2.2 Pause, Stop, Hide | Reviewed: only user-triggered brief transitions; reduced motion is honored. |
| 2.3.1 Three Flashes or Below Threshold | Reviewed: no flashing content. |
| 2.4.1 Bypass Blocks | Reviewed: a named main landmark and a separate navigation landmark. |
| 2.4.2 Page Titled | Reviewed: browser title follows the active slide. |
| 2.4.3 Focus Order | Reviewed: content then controls; inactive slides are inert; navigation preserves control focus. |
| 2.4.4 Link Purpose (In Context) | Reviewed: descriptive repository, installation, and presenter links. |
| 2.4.5 Multiple Ways | Reviewed: sequential navigation and the complete titled slide chooser. |
| 2.4.6 Headings and Labels | Reviewed: one active slide heading and explicit control names. |
| 2.4.7 Focus Visible | Reviewed: visible outline, focused copy-button opacity, and forced-colors focus test. |
| 3.1.1 Language of Page | Reviewed: English is declared. |
| 3.1.2 Language of Parts | N/A: no foreign-language prose; code, names, and technical terms are exceptions. |
| 3.2.1 On Focus | Reviewed: focusing controls does not trigger navigation or fullscreen. |
| 3.2.2 On Input | Reviewed: selecting an option waits for Go; reveal/layout actions require explicit activation. |
| 3.2.3 Consistent Navigation | Reviewed: the same audience controls appear on every slide. |
| 3.2.4 Consistent Identification | Reviewed: control names and states remain consistent. |
| 3.3.1 Error Identification | N/A: there is no invalid audience data-entry state. |
| 3.3.2 Labels or Instructions | Reviewed: the chooser has a native label and explicit Go action. |
| 3.3.3 Error Suggestion | N/A: no free-form submission or validation errors. |
| 3.3.4 Error Prevention | N/A: no legal, financial, destructive, or stored-data submission. |
| 4.1.1 Parsing | Reviewed: unique IDs and no exposed nested interactive controls; hidden upstream authoring markup is not used for audience navigation. |
| 4.1.2 Name, Role, Value | Reviewed: native controls, pressed/disabled states, named regions, and status updates. AT pending. |

## Remaining work before a full conformance claim

VoiceOver testing was explicitly deferred. The remaining manual session should
cover heading/landmark navigation, reveal announcements, table-header reading,
diagram descriptions and scrolling, focus changes, and returning from fullscreen
on representative dense and illustrated slides. Test another screen reader/browser
combination if the delivery audience requires it.

PDF and PPTX files require their own tagging, reading-order, language, link, and
alternative-text review if they are distributed. Passing the web checks does not
transfer to those exports.

## Repeat the browser checks

Build and serve the production output from this deck directory:

```sh
npm run build
./node_modules/.bin/vite preview --host 127.0.0.1 --port 3032
```

Install audit-only dependencies in a temporary directory, then run the check in a
second terminal from the deck directory:

```sh
npm install --prefix /tmp/sdd-a11y-tools --no-save --ignore-scripts axe-core@4.14.0 playwright@1.62.1
node /tmp/sdd-a11y-tools/node_modules/playwright/cli.js install chromium
SDD_A11Y_DEPS=/tmp/sdd-a11y-tools node scripts/check-accessibility.mjs
```

Optional environment variables: `SDD_A11Y_URL`, `SDD_A11Y_OUTPUT`,
`SDD_AXE_SCRIPT`, `SDD_CHROMIUM_EXECUTABLE`, and `SDD_A11Y_SCREENSHOTS`.
The `--interactions-only`
option reruns the functional assertions without the 99-state scan.

On macOS, headless Chromium does not operate native select popups with key events;
the script tests shortcut isolation and the Go action, while the native chooser
keyboard sequence is verified separately in Safari. Keep that native check when
repeating a full browser review.

## October 28, 2026 content revision

Rechecked all 33 slides after the table and narrative polish. Production build
and table lint pass. A rendered bounding-box check found no text, table, or
code overflow across the 33 fully revealed slides; the revised teaching and
closing slides were also visually inspected.

The existing Playwright/axe harness completed 99 states with zero axe violations
and all eight interaction checks. Structural and 640-pixel reading-layout
assertions completed. Its final no-browser-errors assertion failed because
headless Chromium reported `Wake Lock permission request denied`; this run is
therefore not recorded as a fully passing harness run. The browser was Chromium
151.0.7922.34. Temporary evidence: `/tmp/sdd-polish-audit/audit.json` and slide
screenshots in the same directory. No new Safari or VoiceOver review was run.

### Narrative follow-up

After moving the origin request before the tool introduction and tightening the
process/human-role transitions, the production build and table lint passed again.
All 33 fully revealed slides passed the bounding-box overflow check. Slides 12,
16–18, 23–25, and 27–32 were checked with axe in fully revealed and 640-pixel
reading layouts: 26 states, zero violations. Revised transition slides were
visually inspected. These targeted checks do not replace the full interaction
harness or its wake-lock limitation recorded above. The deck remains 33 slides
with 50 minutes allocated in speaker notes.

### Actor and artifact primer

Slide 8 now defines humans, AI agents, checking programs, features, specifications,
and claims; slide 10 defines Gherkin/scenarios and the role of bindings and a
runner. The primer replaces one diagram, leaving three diagrams in 33 slides.
The diagram keyboard test now uses slide 28.

Production build and table lint pass. Slides 8–10 and 12 have no detected
overflow and were checked in fitted and 640-pixel reading layouts: eight axe
states, zero violations. The updated actor map and Gherkin example were visually
inspected. All eight interaction checks completed, including diagram scrolling
on slide 28; the harness still fails its final no-browser-errors assertion on
the previously recorded headless wake-lock denial. This is not a fully passing
interaction-harness run. The timing allocation remains 50 minutes.
