# Talks

Talks, slide decks, and demos by **Rodney Littles II**, covering .NET, reactive and functional programming, mobile development, and software delivery.

**[Browse the published talks site](https://rlittlesii.github.io/talks/)**

## Browse by topic

- [C# & .NET](#c--net)
- [Reactive & functional programming](#reactive--functional-programming)
- [Mobile development](#mobile-development)
- [Build automation & DevOps](#build-automation--devops)
- [Software development practices](#software-development-practices)
- [Samples & demos](#samples--demos)

Talk titles link to their abstracts. **Presentation** opens a published deck; **Presenter view** opens its speaker notes and controls. Keynote and PowerPoint links open the deck files in this repository.

## C# & .NET

| Talk | What it covers | Materials |
| --- | --- | --- |
| [C# Abstractions](./%20csharp/language/abstractions/README.md) | Inheritance, composition, and choosing abstractions that keep modern C# code composable. | [Presentation](https://rlittlesii.github.io/talks/composition/) · [Presenter view](https://rlittlesii.github.io/talks/composition/#/presenter/1) · [Slide source](./%20csharp/language/abstractions/composition/slides.md) |
| [Bridging IEnumerable and IObservable with Dynamic Data](./%20csharp/libraries/dynamic.data/README.md) | Filter, transform, sort, and refresh reactive collections as their data changes. | [Presentation](https://rlittlesii.github.io/talks/dynamic.data/) · [Presenter view](https://rlittlesii.github.io/talks/dynamic.data/#/presenter/1) · [Slide source](./%20csharp/libraries/dynamic.data/slides.md) |
| [Null, The absence of a C# reference](./%20csharp/language/nullability/README.md) | How C# handles null, and practices for preventing null reference failures in production. | [Keynote](./%20csharp/language/nullability/csharp.nullability.key) |

## Reactive & functional programming

| Talk | What it covers | Materials |
| --- | --- | --- |
| [Taming Mutable State: Applying Functional Programming in an Object-Oriented Language](./functional-programming/reactiveui/object-functional-programming/README.md) | Immutability, monads, and railway-oriented programming in an object-oriented language. | [Keynote](./functional-programming/reactiveui/object-functional-programming/taming-mutable-state.key) |
| [Go Reactive with Reactive Extensions and ReactiveUI](./functional-programming/reactiveui/dotnet-conf/README.md) | Use Rx and ReactiveUI to handle application concerns declaratively. | [Keynote](./functional-programming/reactiveui/dotnet-conf/slides/going-reactive-xamarin.key) · [PowerPoint](./functional-programming/reactiveui/dotnet-conf/slides/going-reactive-with-xamarin-dotnet-conf.pptx) |
| [Taming Mutable State using ReactiveUI](./functional-programming/reactiveui/mutable-state/README.md) | React to state mutations in a declarative, composable way. | [Keynote: Intro](./functional-programming/reactiveui/mutable-state/slides/intro-reactiveui.key) · [Keynote: Full](./functional-programming/reactiveui/mutable-state/slides/taming-mutable-state.key) |
| [SocialQ](./functional-programming/reactiveui/socialq/README.md) | A real-world application architecture using ReactiveUI for social distance queuing. | [Keynote](./functional-programming/reactiveui/socialq/socialq.key) |

## Mobile development

| Talk | What it covers | Materials |
| --- | --- | --- |
| [Xamarin, MAUI and the reactive MVVM between them](./mobile/jetbrains/README.md) | Xamarin, MAUI, and reactive MVVM with ReactiveUI. | [Keynote](./mobile/jetbrains/slides/xamarin-vacation-prep.key) |
| [Laugh Pharmacy: Using SignalR and Blazor to deliver jokes to MAUI](./mobile/signalr/README.md) | Stream jokes to MAUI with SignalR, Blazor, and Reactive Extensions. | Abstract only |
| [Prism + ReactiveUI a match made for mobile](./mobile/monkey-fest/es/README.md) | Combine Prism application structure with ReactiveUI state management. | Abstract only |
| [Xamarin.Forms at scale](./mobile/monkey-fest/en/README.md) | Manage team complexity, testing, and delivery with MVVM. | [Keynote](./mobile/monkey-fest/en/Xamarin-Forms-Scale.key) |

## Build automation & DevOps

| Talk | What it covers | Materials |
| --- | --- | --- |
| [Mobile Dev Ops at Scale](./mobile/devops/README.MD) | Version, build, test, sign, and release mobile applications across an enterprise. | [PowerPoint](./mobile/devops/slides/Mobile.Dev.Ops.Scale.pptx) |
| [Fastlane Demo](./mobile/fastlane/README.md) | Automate iOS build configuration with fastlane. | [Keynote](./mobile/fastlane/slides/fastlane.key) |

## Software development practices

| Talk | What it covers | Materials |
| --- | --- | --- |
| [Behavior Driven Practices](./sdlc/bdd/README.MD) | Use behavior-driven design and development to clarify boundaries and reduce friction. | [Keynote](./sdlc/bdd/slides/behavior.driven.software.key) |
| [Specification Driven Development](./sdlc/specification-driven-development/README.md) | Write specifications an agent can build from, backed by tests, lessons, and skills. | [Presentation](https://rlittlesii.github.io/talks/specification-driven-development/) · [Presenter view](https://rlittlesii.github.io/talks/specification-driven-development/#/presenter/1) · [Slide source](./sdlc/specification-driven-development/slides/slides.md) |

## Samples & demos

| Sample | What it demonstrates |
| --- | --- |
| [MSBuild Central Package Version Sdk Example](./%20csharp/dependencies/management/CentralPackageVersion/README.md) | Central package versioning for a Xamarin.Forms application. |
| [Mediator Sample](./mediator/README.md) | .NET Core MVC and the MediatR library. |

## Presenting & local development

### Always-available local talks (macOS)

With Homebrew, Node.js, pnpm, and `jq` installed, run this once from the repository root:

```bash
pnpm local:setup
```

Setup installs Caddy if needed, builds all Slidev decks, and registers a user-level
Homebrew service that starts at login. Open [the local talks index](http://localhost:3030/talks/)
or [Specification Driven Development](http://localhost:3030/talks/specification-driven-development/).
VS Code and Terminal can be closed. The Mac must be awake and you must be logged in.
The server binds only to this computer's IPv4 and IPv6 loopback interfaces.

After editing, publish the latest local sources explicitly:

```bash
pnpm local:deploy
```

Only changed decks rebuild. A deployment becomes visible after every deck and the
index succeed; failures leave the last successful release available. Refresh open
browser tabs after deployment. No Git push is required.

| Command | Purpose |
| --- | --- |
| `pnpm local:status` | Show service state, current/previous releases, logs, and HTTP readiness. |
| `pnpm local:stop` | Stop serving and disable startup at login. |
| `pnpm local:start` | Start serving and enable startup at login again. |
| `pnpm local:rollback` | Switch to the previous release; run again to switch back. |
| `pnpm test:local` | Exercise deployment behavior in an isolated temporary directory. |

Build caches, releases, Caddy configuration, and logs live under
`~/Library/Application Support/Talks/`. Releases are retained; older directories
under `releases/` can be removed when neither `current` nor `previous` points to
them. The `current` symlink switches atomically, so deployment does not restart
Caddy. All new automation is Bash; Node/pnpm are needed to build, while serving
uses only Caddy. An existing unrelated Homebrew Caddy service is left unchanged.
Use these commands to manage the custom service rather than a plain
`brew services restart caddy`, which can select Homebrew's default configuration.

Port **3030** belongs to the static server. For live editing, use **3031**:

```bash
pnpm --filter specification-driven-development run dev --port 3031
```

Setup/start report any process occupying 3030 without stopping it. If an abruptly
terminated deploy leaves `.deploy-lock`, check its `pid` file and confirm that
process is no longer running before removing the lock directory. Advanced testing
can override storage with an absolute `TALKS_LOCAL_HOME`; use the same value for
all commands. Only one Caddy service/port is supported per user.

Presenter view uses the deck URL followed by `#/presenter/1`. Use two windows of
the same browser profile for audience/presenter synchronization. Keynote and
PowerPoint talks are not converted automatically; some decks still use remote
images, so fully offline presentation is not guaranteed.

<details>
<summary>Present a published deck from any laptop</summary>

Open **Presentation** on the audience display and **Presenter view** on your laptop in two windows of the same browser profile. Slide navigation stays in sync, and presenter view includes your speaker notes. No local checkout or development server is needed. See [Slidev's presenter guide](https://sli.dev/guide/ui#presenter-mode) for the controls.

Published decks use hash routing (`#/presenter/1`) so direct links and refreshes work on GitHub Pages.

</details>

<details>
<summary>Run a Slidev deck locally or publish a new one</summary>

From the repository root, install the workspace dependencies:

```bash
pnpm install --frozen-lockfile
```

Start the deck you want to work on:

```bash
pnpm --filter composition run dev --port 3031
pnpm --filter dynamic.data run dev --port 3031
pnpm --filter specification-driven-development run dev --port 3031
```

Slidev decks are built and published to GitHub Pages on each push to `master`. The site automatically lists every published Slidev workspace. Keep `presenter: true` in a deck's `slides.md` headmatter to include presenter mode in the published build.

The other talks above provide source decks or abstracts; they will appear on the site when converted to Slidev workspaces.

</details>
