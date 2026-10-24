<script setup lang="ts">
import { computed, nextTick, onBeforeUnmount, onMounted, ref, watch } from 'vue'
import { useNav } from '@slidev/client'

const nav = useNav()
const reading = ref(false)
const selectedSlide = ref(nav.currentSlideNo.value)
const fullscreen = ref(false)
const navigationElement = ref<HTMLElement>()
const title = computed(() => nav.currentSlideRoute.value.meta?.slide?.title || 'Specification Driven Development')
const announcement = computed(() =>
  'Slide ' + nav.currentSlideNo.value + ' of ' + nav.total.value + ': ' + title.value
  + (!reading.value && nav.clicksTotal.value ? '. Step ' + Math.min(nav.clicks.value, nav.clicksTotal.value) + ' of ' + nav.clicksTotal.value : ''),
)
let observer: MutationObserver | undefined
let diagramObserver: ResizeObserver | undefined
let navigationObserver: ResizeObserver | undefined
const observedDiagrams = new WeakSet<Element>()

function decorate() {
  if (!nav.isPlaying.value)
    return
  const container = document.getElementById('slide-content')
  const slide = container?.querySelector('.slidev-page-' + nav.currentSlideNo.value)
  const heading = slide?.querySelector('h1')
  container?.querySelectorAll<HTMLElement>('.slidev-page').forEach(page => {
    const active = page.dataset.slidevNo === String(nav.currentSlideNo.value)
    page.inert = !active
    if (active)
      page.removeAttribute('aria-hidden')
    else
      page.setAttribute('aria-hidden', 'true')
  })
  if (container) {
    container.setAttribute('role', 'main')
    container.setAttribute('tabindex', '-1')
    container.setAttribute('aria-label', 'Slide ' + nav.currentSlideNo.value + ': ' + title.value)
  }
  if (heading) {
    heading.id = 'sdd-heading-' + nav.currentSlideNo.value
    heading.setAttribute('tabindex', '-1')
  }
  slide?.querySelectorAll('thead th').forEach(th => th.setAttribute('scope', 'col'))
  slide?.querySelectorAll<HTMLElement>('.mermaid').forEach(diagram => {
    if (!observedDiagrams.has(diagram)) {
      diagramObserver?.observe(diagram)
      // Arrow keys scroll a focused diagram rather than changing the slide.
      diagram.addEventListener('keydown', event => event.stopPropagation())
      diagram.addEventListener('keyup', event => event.stopPropagation())
      observedDiagrams.add(diagram)
    }
    const svg = diagram.shadowRoot?.querySelector('svg')
    if (!svg)
      return
    if (!svg.hasAttribute('data-sdd-height')) {
      svg.setAttribute('data-sdd-height', svg.getAttribute('height') || '')
      svg.setAttribute('data-sdd-style', svg.getAttribute('style') || '')
    }
    if (reading.value) {
      const [, , width, height] = (svg.getAttribute('viewBox') || '').split(' ').map(Number)
      if (width > 0 && height > 0) {
        svg.setAttribute('style', 'width:' + width + 'px;height:' + height + 'px;max-width:none')
        svg.setAttribute('height', String(height))
      }
      diagram.setAttribute('tabindex', '0')
      diagram.setAttribute('role', 'region')
      diagram.setAttribute('aria-label', 'Scrollable diagram: ' + (svg.querySelector('title')?.textContent || title.value))
    }
    else {
      const originalStyle = svg.getAttribute('data-sdd-style')
      if (originalStyle)
        svg.setAttribute('style', originalStyle)
      else
        svg.removeAttribute('style')
      svg.setAttribute('height', svg.getAttribute('data-sdd-height') || '')
      diagram.removeAttribute('tabindex')
      diagram.removeAttribute('role')
      diagram.removeAttribute('aria-label')
    }
  })
  document.title = title.value + ' — Specification Driven Development'
}

async function update() {
  document.documentElement.classList.toggle('sdd-audience', nav.isPlaying.value)
  document.documentElement.classList.toggle('sdd-reading', nav.isPlaying.value && reading.value)
  await nextTick()
  decorate()
}

async function toggleFullscreen() {
  if (document.fullscreenElement)
    await document.exitFullscreen()
  else
    await document.documentElement.requestFullscreen()
}

function updateFullscreen() {
  fullscreen.value = !!document.fullscreenElement
}

watch([nav.currentSlideNo, nav.clicks, nav.isPlaying, reading], update, { flush: 'post' })
watch([reading, nav.currentSlideNo, nav.clicksTotal], () => {
  if (reading.value && nav.clicks.value < nav.clicksTotal.value)
    nav.go(nav.currentSlideNo.value, nav.clicksTotal.value)
}, { flush: 'post' })
watch(nav.currentSlideNo, () => {
  selectedSlide.value = nav.currentSlideNo.value
  if (reading.value)
    window.scrollTo({ top: 0, left: 0, behavior: 'instant' })
}, { flush: 'post' })
onMounted(() => {
  navigationObserver = new ResizeObserver(() => {
    const height = navigationElement.value?.getBoundingClientRect().height || 0
    document.documentElement.style.setProperty('--sdd-navigation-height', height + 'px')
  })
  if (navigationElement.value)
    navigationObserver.observe(navigationElement.value)
  diagramObserver = new ResizeObserver(decorate)
  update()
  observer = new MutationObserver(decorate)
  const slideshow = document.getElementById('slideshow')
  if (slideshow)
    observer.observe(slideshow, { childList: true, subtree: true })
  document.addEventListener('fullscreenchange', updateFullscreen)
})
onBeforeUnmount(() => {
  observer?.disconnect()
  diagramObserver?.disconnect()
  navigationObserver?.disconnect()
  document.documentElement.style.removeProperty('--sdd-navigation-height')
  document.removeEventListener('fullscreenchange', updateFullscreen)
  document.documentElement.classList.remove('sdd-audience', 'sdd-reading')
})
</script>

<template>
  <Teleport to="body">
    <div v-if="nav.isPlaying.value" ref="navigationElement" class="sdd-navigation">
      <nav aria-label="Presentation">
        <button type="button" :disabled="reading ? nav.currentSlideNo.value <= 1 : !nav.hasPrev.value" aria-label="Previous step or slide" @click="reading ? nav.prevSlide() : nav.prev()">
          Previous
        </button>
        <button type="button" :disabled="reading ? nav.currentSlideNo.value >= nav.total.value : !nav.hasNext.value" aria-label="Next step or slide" @click="reading ? nav.nextSlide() : nav.next()">
          Next
        </button>
        <label for="sdd-slide-select">Slide</label>
        <select id="sdd-slide-select" v-model.number="selectedSlide" @keydown.stop @keyup.stop>
          <option v-for="slide in nav.slides.value" :key="slide.no" :value="slide.no">
            {{ slide.no }}. {{ slide.meta?.slide?.title }}
          </option>
        </select>
        <button type="button" aria-label="Go to selected slide" @click="nav.go(selectedSlide)">
          Go
        </button>
        <button type="button" :aria-pressed="reading" @click="reading = !reading">
          Reading layout
        </button>
        <button type="button" :aria-pressed="fullscreen" @click="toggleFullscreen">
          Fullscreen
        </button>
        <a :href="'/presenter/' + nav.currentSlideNo.value">Presenter view</a>
      </nav>
      <p class="sdd-status" role="status" aria-live="polite" aria-atomic="true">{{ announcement }}</p>
    </div>
  </Teleport>
</template>
