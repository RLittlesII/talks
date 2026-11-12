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

From the repository root, set up the persistent local server, then deploy edits:

```bash
pnpm local:setup
pnpm local:deploy
```

Open `http://localhost:3030/talks/`. Presentation URLs retain the `/talks/` base path and hash-based presenter routes used on GitHub Pages. See the root README for lifecycle commands, storage, and rollback. The existing `generate-site.sh` remains the shared assembly entry point for both local hosting and GitHub Pages.

The site always uses a dark theme, including browser controls, card surfaces, and focus indicators. Theme colors are defined as custom properties at the top of `style.css`.
