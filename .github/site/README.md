# Talks site

The front page groups published Slidev decks by topic. Its template and styles live in this directory; `.github/scripts/render-talk-index.mjs` fills the template during site assembly.

Add optional display metadata to a deck's `package.json`:

```json
"talk": {
  "title": "A descriptive talk title",
  "description": "A short summary of what the audience will learn.",
  "category": "C# & .NET"
}
```

Decks without metadata still appear under **More talks**, using their package name as the title. Only decks with cached builds are included, so the site links to available presentations.

From the repository root, assemble the site from cached builds and serve a preview:

```bash
.github/scripts/generate-site.sh .cache/talks .cache/site-preview/talks
python3 -m http.server 8765 --directory .cache/site-preview
```

Open `http://localhost:8765/talks/`. Presentation URLs retain the `/talks/` base path and hash-based presenter routes used on GitHub Pages.

The site always uses a dark theme, including browser controls, card surfaces, and focus indicators. Theme colors are defined as custom properties at the top of `style.css`.
