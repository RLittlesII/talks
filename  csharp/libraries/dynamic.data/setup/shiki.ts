// Darcula-inspired syntax colors, adjusted for AA contrast on charcoal.
const darcula = {
  name: 'darcula-muted',
  type: 'dark' as const,
  colors: {
    'editor.background': '#2b2b2b',
    'editor.foreground': '#a9b7c6',
  },
  tokenColors: [
    { scope: ['comment', 'punctuation.definition.comment'], settings: { foreground: '#969696' } },
    { scope: ['keyword', 'storage'], settings: { foreground: '#cc9a71' } },
    { scope: ['string'], settings: { foreground: '#8aaa79' } },
    { scope: ['constant.numeric', 'constant.language'], settings: { foreground: '#91b4d5' } },
    { scope: ['entity.name.function', 'support.function'], settings: { foreground: '#b6ad86' } },
    { scope: ['entity.name.type', 'support.type', 'support.class'], settings: { foreground: '#b2a0c7' } },
    { scope: ['variable', 'punctuation'], settings: { foreground: '#a9b7c6' } },
  ],
}

export default () => ({
  theme: { dark: darcula, light: darcula },
})
