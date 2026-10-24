import { defineShortcutsSetup } from '@slidev/types'
import { useNav } from '@slidev/client'

export default defineShortcutsSetup((navigation, shortcuts) => {
  // The audience uses the labelled slide selector and reading-layout button.
  // Keep the authoring shortcuts in the presenter view.
  if (!useNav().isPlaying.value)
    return shortcuts
  return shortcuts.filter(shortcut =>
    !['toggle_dark', 'toggle_overview', 'hide_overview', 'goto'].includes(shortcut.name || ''),
  ).map(shortcut => {
    const original = shortcut.fn
    if (!['next_space', 'next_right', 'next_page_key', 'prev_space', 'prev_left', 'prev_page_key'].includes(shortcut.name || ''))
      return shortcut
    return {
      ...shortcut,
      fn: () => {
        if (document.documentElement.classList.contains('sdd-reading'))
          return shortcut.name?.startsWith('prev') ? navigation.prevSlide() : navigation.nextSlide()
        return original?.()
      },
    }
  })
})
