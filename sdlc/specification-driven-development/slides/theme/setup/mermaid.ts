import { defineMermaidSetup } from '@slidev/types'

// Mermaid renders inside a shadow root; configure its colors here, not in CSS.
export default defineMermaidSetup(() => ({
  theme: 'base',
  fontFamily: 'Arial, Helvetica, sans-serif',
  themeVariables: {
    fontSize: '22px',
    darkMode: true,
    primaryColor: '#2b3031',
    primaryTextColor: '#d5d7d2',
    primaryBorderColor: '#83bdb7',
    secondaryColor: '#202425',
    secondaryTextColor: '#d5d7d2',
    secondaryBorderColor: '#83bdb7',
    tertiaryColor: '#2b3031',
    tertiaryTextColor: '#d5d7d2',
    tertiaryBorderColor: '#83bdb7',
    lineColor: '#83bdb7',
    textColor: '#d5d7d2',
    edgeLabelBackground: '#202425',
  },
}))
