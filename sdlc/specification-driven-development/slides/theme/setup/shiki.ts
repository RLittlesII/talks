import { defineShikiSetup } from '@slidev/types'

export default defineShikiSetup(() => ({
  transformers: [{
    line(node, lineNumber) {
      if (this.options.lang !== 'diff')
        return

      const line = this.source.split('\n')[lineNumber - 1]
      if (!line || /^(?:\+\+\+|---) /.test(line))
        return

      if (line.startsWith('+'))
        this.addClassToHast(node, 'sdd-diff-add')
      else if (line.startsWith('-'))
        this.addClassToHast(node, 'sdd-diff-remove')
    },
  }],
}))
