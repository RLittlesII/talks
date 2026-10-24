import { readFile, writeFile, copyFile } from 'node:fs/promises'

const [outputDir, ...talkDirs] = process.argv.slice(2)
const siteDir = new URL('../site/', import.meta.url)
const repository = 'https://github.com/rlittlesii/talks'
const escape = value => String(value).replace(/[&<>"']/g, char => ({
  '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;',
})[char])

const talks = await Promise.all(talkDirs.map(async directory => {
  const pkg = JSON.parse(await readFile(`${directory}/package.json`, 'utf8'))
  return {
    name: pkg.name,
    title: pkg.talk?.title || pkg.name,
    description: pkg.talk?.description || 'Explore the slides and examples from this talk.',
    category: pkg.talk?.category || 'More talks',
    source: `${repository}/tree/master/${directory.replace(/^\.\//, '').split('/').map(encodeURIComponent).join('/')}`,
  }
}))

talks.sort((a, b) => a.category.localeCompare(b.category, 'en') || a.title.localeCompare(b.title, 'en'))
const categories = [...new Set(talks.map(talk => talk.category))]
const plural = (count, noun) => `${count} ${noun}${count === 1 ? '' : 's'}`

const card = talk => {
  const title = escape(talk.title)
  const url = `/talks/${encodeURIComponent(talk.name)}/`
  return `<li class="talk-card">
    <article>
      <h3><a href="${url}">${title}</a></h3>
      <p>${escape(talk.description)}</p>
      <div class="talk-actions">
        <a class="button" href="${url}" aria-label="Presentation for ${title}">Presentation <span aria-hidden="true">↗</span></a>
        <a class="presenter" href="${url}#/presenter/1" aria-label="Presenter view for ${title}">Presenter view <span aria-hidden="true">↗</span></a>
        <a class="source" href="${escape(talk.source)}" aria-label="Source for ${title}">Source</a>
      </div>
    </article>
  </li>`
}

const replacements = {
  stats: `${plural(talks.length, 'talk')} across ${plural(categories.length, 'topic')}`,
  topics: categories.map((category, index) => `<li><a href="#topic-${index + 1}"><span>${escape(category)}</span><span class="topic-count">${talks.filter(talk => talk.category === category).length}</span></a></li>`).join('\n'),
  talks: categories.map((category, index) => {
    const group = talks.filter(talk => talk.category === category)
    return `<section class="topic-section" aria-labelledby="topic-${index + 1}">
      <div class="section-heading"><h2 id="topic-${index + 1}">${escape(category)}</h2><span>${plural(group.length, 'talk')}</span></div>
      <ul class="talk-list">${group.map(card).join('\n')}</ul>
    </section>`
  }).join('\n') || '<p class="empty-state">Published decks are on their way. Explore the full collection below for source decks, samples, and demos.</p>',
}

const template = await readFile(new URL('index.html', siteDir), 'utf8')
await writeFile(`${outputDir}/index.html`, template.replace(/\{\{(stats|topics|talks)\}\}/g, (_, key) => replacements[key]))
await copyFile(new URL('style.css', siteDir), `${outputDir}/style.css`)
console.log(`==> Generated index with ${plural(talks.length, 'talk')} in ${plural(categories.length, 'topic')}`)
