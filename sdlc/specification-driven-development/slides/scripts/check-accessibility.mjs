import assert from 'node:assert/strict'
import { createRequire } from 'node:module'
import { mkdir, writeFile } from 'node:fs/promises'
import { resolve } from 'node:path'

// Keep audit dependencies outside the presentation's runtime dependencies.
const require = createRequire(resolve(process.env.SDD_A11Y_DEPS || process.cwd(), 'package.json'))
const { chromium } = require('playwright')
const axePath = process.env.SDD_AXE_SCRIPT || require.resolve('axe-core/axe.min.js')
const base = process.env.SDD_A11Y_URL || 'http://localhost:3032'
const output = resolve(process.env.SDD_A11Y_OUTPUT || '.cache/sdd-accessibility')
const slideCount = 42
const dividers = [
  [4, 'The Spec Returns', "Before we get to replay, let's see what we kept from the binder."],
  [11, 'The Claim', 'Find the same designation in the feature file.'],
  [22, 'My Response', 'I kept paying for another review. What could I make the build catch?'],
  [31, 'What to Consider', 'Now imagine AC-011 shows up in your repo.'],
]
await mkdir(output, { recursive: true })
const browser = await chromium.launch({
  headless: true,
  ...(process.env.SDD_CHROMIUM_EXECUTABLE ? { executablePath: process.env.SDD_CHROMIUM_EXECUTABLE } : {}),
})
const context = await browser.newContext({
  viewport: { width: 1280, height: 800 },
  reducedMotion: 'reduce',
  permissions: ['screen-wake-lock'],
})
const page = await context.newPage()
const results = []
const checks = []
const semantics = []
const errors = []
const interactionsOnly = process.argv.includes('--interactions-only')
page.on('pageerror', error => errors.push(error.message))

async function openWithControls(path) {
  const url = new URL(base + path)
  url.searchParams.set('controls', '1')
  // Hash-only navigation otherwise carries the previous check's reading state.
  await page.goto('about:blank')
  await page.goto(url.href, { waitUntil: 'networkidle' })
}

function atStep(no, clicks = null) {
  return url => {
    const route = url.hash ? new URL(url.hash.slice(1), url.origin) : url
    return route.pathname.endsWith('/' + no) && route.searchParams.get('clicks') === clicks
  }
}

async function ready(no) {
  await page.getByRole('main', { name: new RegExp('^Slide ' + no + ':') }).waitFor()
  const slide = page.locator('#slide-content .slidev-page-' + no)
  await slide.locator('h1').waitFor()
  if (await slide.locator('.mermaid').count()) {
    await slide.locator('.mermaid svg[height]:not([height="100%"])').waitFor()
    await page.waitForTimeout(200)
  }
  return slide
}

async function presenterReady(presenter, no) {
  await presenter.locator(`.grid-section.main .slidev-page-${no} h1`).waitFor()
  await ready(no)
}

async function audit(no, mode) {
  await page.addScriptTag({ path: axePath })
  const result = await page.evaluate(async () => {
    const report = await axe.run(document, {
      runOnly: { type: 'tag', values: ['wcag2a', 'wcag2aa', 'wcag21a', 'wcag21aa', 'best-practice'] },
    })
    const summarize = rules => rules.map(({ id, impact, help, helpUrl, nodes }) => ({
      id, impact, help, helpUrl,
      nodes: nodes.map(({ html, target, failureSummary }) => ({ html, target, failureSummary })),
    }))
    return {
      engine: axe.version,
      violations: summarize(report.violations),
      incomplete: summarize(report.incomplete),
      passes: report.passes.map(rule => rule.id),
    }
  })
  results.push({ slide: no, mode, ...result })
  console.log('Slide ' + no + ' ' + mode + ': ' + result.violations.length + ' violations')
}

try {
  for (let no = 1; !interactionsOnly && no <= slideCount; no++) {
    for (const [mode, clicks] of [['initial', 0], ['revealed', 999]]) {
      await openWithControls('/' + no + '?clicks=' + clicks)
      const slide = await ready(no)
      assert.equal(await page.getByRole('main').count(), 1)
      assert.equal(await slide.locator('h1').count(), 1)
      assert.match(await page.title(), new RegExp('Specification Driven Development'))
      await audit(no, mode)
      if (mode === 'revealed') {
        const structure = await slide.evaluate(element => {
          const ids = [...document.querySelectorAll('[id]')].map(node => node.id)
          return {
            language: document.documentElement.lang,
            duplicateIds: ids.filter((id, index) => ids.indexOf(id) !== index),
            images: [...element.querySelectorAll('img')].map(image => ({
              alt: image.alt,
              decorative: image.hasAttribute('alt') && image.getAttribute('role') === 'presentation',
              loaded: image.complete && image.naturalWidth > 0,
            })),
            headers: [...element.querySelectorAll('thead th')].map(header => header.getAttribute('scope')),
            diagrams: [...element.querySelectorAll('.mermaid')].map(host => {
              const svg = host.shadowRoot.querySelector('svg')
              const root = svg.getRootNode()
              return {
                title: root.getElementById(svg.getAttribute('aria-labelledby'))?.textContent,
                description: root.getElementById(svg.getAttribute('aria-describedby'))?.textContent,
              }
            }),
          }
        })
        assert.equal(structure.language, 'en')
        assert.deepEqual(structure.duplicateIds, [])
        assert.ok(structure.images.every(image => image.loaded && (image.alt.trim() || image.decorative)))
        assert.ok(structure.headers.every(scope => scope === 'col'))
        assert.ok(structure.diagrams.every(diagram => diagram.title && diagram.description))
        semantics.push({ slide: no, ...structure })
        const tree = await page.getByRole('main').ariaSnapshot()
        await writeFile(resolve(output, 'slide-' + no + '.aria.txt'), tree)
        if (process.env.SDD_A11Y_SCREENSHOTS)
          await page.screenshot({ path: resolve(output, 'slide-' + no + '.png') })
      }
    }
    await page.getByRole('button', { name: 'Reading layout', exact: true }).click()
    const headingSize = await page.getByRole('main').getByRole('heading', { level: 1 }).evaluate(heading => getComputedStyle(heading).fontSize)
    await page.setViewportSize({ width: 640, height: 400 })
    const slide = await ready(no)
    assert.equal(await page.evaluate(() => document.documentElement.classList.contains('sdd-reading')), true)
    assert.equal(await slide.locator('h1').evaluate(heading => getComputedStyle(heading).fontSize), headingSize, 'Responsive headings must not cancel browser text enlargement')
    assert.ok(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1), 'Reading layout must not force page-wide horizontal scrolling')
    assert.ok(await page.locator('#slide-content').evaluate(element =>
      element.getBoundingClientRect().width <= innerWidth + 1,
    ), 'The reading canvas must stay within the viewport; diagrams scroll inside their region')
    for (const diagram of await slide.locator('.mermaid').all()) {
      assert.equal(await diagram.getAttribute('tabindex'), '0')
      assert.ok(await diagram.getAttribute('aria-label'))
      const dimensions = await diagram.locator('svg').evaluate(svg => ({
        height: svg.getBoundingClientRect().height,
        expected: Number(svg.getAttribute('viewBox').split(' ')[3]),
      }))
      assert.ok(Math.abs(dimensions.height - dimensions.expected) < 2, 'Reading diagrams must use their full text scale')
    }
    await audit(no, 'reading-640px')
    await page.setViewportSize({ width: 1280, height: 800 })
  }

  await page.goto(base.replace(/\/?#$/, '') + '/', { waitUntil: 'networkidle' })
  await ready(1)
  await page.mouse.move(32, 799)
  assert.equal(await page.locator('.sdd-navigation').count(), 0)
  assert.equal(await page.locator('#slide-container > div:has(> nav)').isVisible(), false)
  assert.equal(await page.locator('#page-root').evaluate(element => element.clientHeight), 800)
  if (!interactionsOnly)
    await audit(1, 'audience')
  await page.keyboard.press('ArrowRight')
  await page.waitForURL(atStep(1, '1'))
  await page.keyboard.press('PageDown')
  await page.waitForURL(atStep(1, '2'))
  await page.keyboard.press('PageDown')
  await ready(2)
  assert.equal(await page.locator('.sdd-navigation').count(), 0)
  assert.match(await page.getByRole('status').innerText(), /^Slide 2 of /)
  checks.push('The regular audience link fills the viewport, keeps both toolbars hidden, and supports keyboard navigation')

  await openWithControls('/1')
  const cover = await ready(1)
  assert.equal(await page.getByRole('button', { name: 'Previous step or slide' }).isDisabled(), true)
  await page.getByRole('button', { name: 'Next step or slide' }).focus()
  await page.keyboard.press('Enter')
  await page.waitForURL(atStep(1, '1'))
  await cover.getByText('What it cost me to let AI drive the spec').waitFor()
  await page.keyboard.press('Enter')
  await page.waitForURL(atStep(1, '2'))
  await cover.getByText('Who checks the spec?', { exact: true }).waitFor()
  await page.keyboard.press('Enter')
  await ready(2)
  assert.ok((await page.locator('.sdd-status').innerText()).startsWith(`Slide 2 of ${slideCount}: Agenda`))
  checks.push('Keyboard Enter reveals both cover cues before advancing to the agenda and updating the live status')

  await page.getByLabel('Slide', { exact: true }).focus()
  await page.keyboard.press('ArrowDown')
  // macOS headless Chromium does not drive OS select popups. Test the shortcut
  // boundary here; the native Space/Down/Return path is checked in Safari.
  assert.ok(atStep(2)(new URL(page.url())))
  await page.getByLabel('Slide', { exact: true }).selectOption('3')
  assert.ok(atStep(2)(new URL(page.url())))
  await page.getByRole('button', { name: 'Go to selected slide' }).focus()
  await page.keyboard.press('Enter')
  await ready(3)
  checks.push('The selector isolates arrow keys; only activating Go changes the slide')

  await openWithControls('/2')
  await ready(2)
  await page.getByRole('button', { name: 'Reading layout', exact: true }).click()
  await page.waitForFunction(() => [...document.querySelectorAll('.slidev-page-2 li')].every(item => getComputedStyle(item).opacity === '1'))
  await page.getByRole('button', { name: 'Next step or slide' }).focus()
  await page.keyboard.press('Enter')
  await ready(3)
  await page.getByRole('button', { name: 'Previous step or slide' }).focus()
  await page.keyboard.press('Enter')
  await ready(2)
  await page.getByRole('main').focus()
  await page.keyboard.press('ArrowRight')
  await ready(3)
  await page.getByRole('main').focus()
  await page.keyboard.press('ArrowLeft')
  await ready(2)

  await openWithControls('/5?clicks=2')
  const history = await ready(5)
  const readingLayout = page.getByRole('button', { name: 'Reading layout', exact: true })
  const punchline = history.getByText('Somewhere along the way, we lost the whole picture.')
  await readingLayout.click()
  await punchline.waitFor()
  assert.equal(await history.getByRole('img').count(), 4)
  assert.ok(atStep(5, '2')(new URL(page.url())))
  await readingLayout.click()
  await punchline.waitFor({ state: 'hidden' })
  assert.equal(await history.getByRole('img').count(), 2)
  await page.keyboard.press('PageDown')
  await page.waitForURL(atStep(5, '3'))
  await page.keyboard.press('PageUp')
  await page.waitForURL(atStep(5, '2'))
  checks.push('Reading layout preserves the reveal step and clicker keys work with a control focused')

  const presenter = await context.newPage()
  presenter.on('pageerror', error => errors.push(error.message))
  for (const [no, title, cue] of dividers) {
    await presenter.goto(base + '/presenter/' + no, { waitUntil: 'networkidle' })
    await presenterReady(presenter, no)
    await presenter.locator('.grid-section.note').getByText(cue, { exact: false }).waitFor()
    const heading = page.getByRole('heading', { name: title, exact: true })
    await heading.waitFor()
    await readingLayout.click()
    await heading.waitFor()
    assert.ok(await page.locator('html.sdd-reading').count())

    await presenter.keyboard.press('PageDown')
    await presenterReady(presenter, no + 1)
    await presenter.keyboard.press('PageUp')
    await presenterReady(presenter, no)
    await presenter.keyboard.press('PageUp')
    await presenterReady(presenter, no - 1)
    await presenter.keyboard.press('PageDown')
    await presenterReady(presenter, no)

    await readingLayout.click()
    await heading.waitFor()
  }
  await presenter.close()
  checks.push('All four divider cues appear in Presenter view and clicker keys cross each boundary in sync with the audience and reading views')

  await openWithControls('/27?clicks=999')
  await ready(27)
  await page.keyboard.press('Tab')
  await page.waitForFunction(() => {
    const button = document.activeElement
    if (!button?.matches('.slidev-code-copy[title="Copy"]'))
      return false
    const style = getComputedStyle(button)
    return style.opacity === '1' && style.outlineStyle !== 'none'
  })
  checks.push('Code Copy is reachable by Tab and visibly focused')
  await page.getByRole('button', { name: 'Reading layout', exact: true }).click()
  await page.evaluate(() => window.scrollTo(0, document.documentElement.scrollHeight))
  await page.getByLabel('Slide', { exact: true }).selectOption('17')
  await page.getByRole('button', { name: 'Go to selected slide' }).click()
  await ready(17)
  assert.equal(await page.evaluate(() => window.scrollY), 0)
  assert.equal(await page.getByRole('main').getByRole('heading', { level: 1 }).count(), 1)
  checks.push('Changing slides resets reading position and removes the outgoing slide from the accessibility tree')

  await openWithControls('/33?clicks=999')
  await ready(33)
  await page.getByRole('button', { name: 'Reading layout', exact: true }).click()
  await page.setViewportSize({ width: 640, height: 400 })
  const diagram = page.getByRole('region', { name: /^Scrollable diagram:/ })
  await diagram.focus()
  await page.keyboard.press('ArrowRight')
  await page.waitForTimeout(200)
  assert.ok(await diagram.evaluate(element => element.scrollLeft > 0))
  assert.ok(atStep(33, '4')(new URL(page.url())))
  await page.setViewportSize({ width: 1280, height: 800 })
  checks.push('A focused enlarged diagram scrolls with arrow keys without changing the slide')

  await page.getByRole('button', { name: 'Fullscreen', exact: true }).click()
  await page.waitForFunction(() => !!document.fullscreenElement)
  await page.getByRole('button', { name: 'Fullscreen', exact: true }).click()
  await page.waitForFunction(() => !document.fullscreenElement)
  checks.push('Fullscreen enters and exits using its labelled button')

  await page.emulateMedia({ forcedColors: 'active' })
  await page.keyboard.press('Tab')
  await page.getByRole('button', { name: 'Next step or slide' }).focus()
  assert.notEqual(await page.getByRole('button', { name: 'Next step or slide' }).evaluate(button => getComputedStyle(button).outlineStyle), 'none')
  await page.emulateMedia({ forcedColors: 'none' })
  checks.push('Keyboard focus remains visible in forced-colors mode')

  await openWithControls('/' + slideCount + '?clicks=999')
  await ready(slideCount)
  await page.setViewportSize({ width: 1024, height: 600 })
  await page.waitForTimeout(100)
  assert.ok(await page.evaluate(() =>
    document.getElementById('slide-content').getBoundingClientRect().bottom
    <= document.querySelector('.sdd-navigation').getBoundingClientRect().top + 1,
  ), 'Wrapping navigation must not overlap the fitted slide')
  await page.setViewportSize({ width: 1280, height: 800 })
  assert.equal(await page.getByRole('button', { name: 'Next step or slide' }).isDisabled(), true)
  await page.getByRole('link', { name: 'Presenter view', exact: true }).focus()
  await page.keyboard.press('Tab')
  assert.equal(await page.getByRole('link', { name: 'Presenter view', exact: true }).evaluate(link => link !== document.activeElement), true)
  checks.push('The last slide has a disabled Next button; Tab can leave the final control')

  const failures = results.flatMap(result => result.violations.map(violation => ({ slide: result.slide, mode: result.mode, ...violation })))
  const report = { base, browser: browser.version(), checks, semantics, errors, pages: results.length, failures, results }
  await writeFile(resolve(output, interactionsOnly ? 'interactions.json' : 'audit.json'), JSON.stringify(report, null, 2))
  assert.deepEqual(errors, [], 'No uncaught browser errors')
  assert.equal(failures.length, 0, 'axe violations must be resolved; inspect audit.json')
  console.log('PASS: ' + results.length + ' page states, ' + checks.length + ' interaction checks')
}
finally {
  // Preserve partial evidence if a functional assertion fails.
  await writeFile(resolve(output, interactionsOnly ? 'interaction-states.json' : 'states.json'), JSON.stringify({ checks, semantics, errors, results }, null, 2))
  await browser.close()
}
