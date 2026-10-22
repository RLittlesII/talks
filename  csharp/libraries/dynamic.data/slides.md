---
theme: dracula
title: Bridging IEnumerable and IObservable with Dynamic Data
description: How Dynamic Data brings reactive operators to collections
class: text-center
transition: slide-left
drawings:
  persist: false
duration: 35min
presenter: true
---

# Bridging IEnumerable and IObservable with Dynamic Data

Reactive collections without the ceremony

<div class="mt-6 text-sm opacity-80">

Use <kbd>→</kbd> / <kbd>←</kbd> to navigate slides

</div>

<!--
- Welcome the audience.
- Hook: We're going to talk about a problem you've hit but may not have been able to name.
- Goal: By the end you'll understand why ObservableCollection lets you down, and how Dynamic Data fills the gap.
-->

---
layout: two-cols
---

# I'm Rodney

<img src="https://github.com/rlittlesii.png" class="rounded-full w-60 h-60 mx-auto shadow-xl" />

::right::

<div class="flex flex-col justify-center h-full ml-4">

- **Rodney Littles, II**
- Senior Software Engineer @ .NET
- Former Microsoft MVP
- ReactiveUI Maintainer
- [Twitch.tv/rlittlesii](https://twitch.tv/rlittlesii)
- [@rlittlesii](https://twitter.com/rlittlesii)
- [GitHub/rlittlesii](https://github.com/rlittlesii)

</div>

---

# Agenda

- The Problem with `ObservableCollection`
- What is Dynamic Data?
- Core Data Structures
- Loading Data
- Operators
- Composing the Full Pipeline

<!--
- We move from the "Why" (the ObservableCollection gap) to the "How" (Dynamic Data operators).
- The demo ViewModel at the end ties every concept together in ~60 lines.
-->

---
layout: center
class: text-center
---

# Disclaimer

> Don't go to work tomorrow and rip out every `ObservableCollection` based on this talk

<!--
- This is educational. Apply it where it fits.
- The concepts here have a learning curve — budget for it.
-->

---

# The Problem

You bound a list. An item's property changed. Your collection-level sort or filter didn't update.

```csharp
public ObservableCollection<Hero> Heroes { get; } = new();

// Adding works fine...
Heroes.Add(new Hero { Name = "Batman", Alignment = "Good" });

// This does not raise CollectionChanged.
Heroes[0].Alignment = "Bad";
```

<v-click>

**Why?** `ObservableCollection<T>` implements `INotifyCollectionChanged` — it fires when items are *added* or *removed*, not when properties on those items change.

</v-click>

<!--
- This is the moment every developer hits.
- They assign a new value on an item and wonder why the UI is stale.
- The confusion comes from conflating two different notification contracts.
- An item's INPC can update a bound cell. It does not make ObservableCollection re-run collection-level filtering or sorting.
-->

---

# INPC vs INC

<div class="grid grid-cols-2 gap-8 mt-4">

<div>

### `INotifyPropertyChanged`

Fires when a **property on an object** changes.

```csharp
public string Alignment
{
    get => _alignment;
    set => this.RaiseAndSetIfChanged(
               ref _alignment, value);
}
```

Scope: **one object**

</div>

<div>

### `INotifyCollectionChanged`

Fires when items are **added, removed, or moved** in a collection.

```csharp
// ObservableCollection raises this
// when you call .Add() or .Remove()
// — not when item properties change
```

Scope: **the collection itself**

</div>

</div>

<v-click>

**The gap:** Item property changes are invisible to the collection. Dynamic Data closes it.

</v-click>

<!--
- INPC tells you "something on this object changed".
- INC tells you "the shape of this list changed".
- They operate at different levels. Nothing in the BCL wires them together automatically.
-->

---

# What is Dynamic Data?

> A library that bridges `IEnumerable` and `IObservable` by projecting **change sets** out of collections.

<div class="grid grid-cols-2 gap-8 mt-4">

<div>

### Reactive collections

- Observe adds, updates, removals, moves, and refreshes
- Compose Filter, Transform, Sort, AutoRefresh, Batch, Join…
- Works across .NET

</div>

<div>

<v-click>

### Find it

**NuGet:** `DynamicData`

**GitHub:** [reactiveMarbles/DynamicData](https://github.com/reactiveMarbles/DynamicData)

Use with **ReactiveUI** or on its own.

</v-click>

</div>

</div>

<v-click>

Created by **Roland Pheasant** for high-frequency day-trading applications.

</v-click>

<!--
- Roland needed to handle thousands of price updates per second, update sorted lists, and filter by multiple criteria without thrashing the UI.
- The answer was to make collection mutations a first-class observable concern.
- DynamicData is a standalone library; ReactiveUI is not required.
- The repository was formerly reactive-ui/DynamicData.
- David Driscoll also created a JavaScript port; the collection change-set concepts carry across ecosystems.
-->

---

# Two Core Structures

<div class="grid grid-cols-2 gap-8 mt-4">

<div>

### `SourceList<T>`

An ordered **list**.

Use when order matters and you have no natural key.

```csharp
var list = new SourceList<Hero>();
```

</div>

<div>

### `SourceCache<T, TKey>`

A keyed **dictionary**.

Use when items have a unique identifier. Supports upserts and lookups by key.

```csharp
var cache = new SourceCache<Hero, int>(
    hero => hero.Id);
```

</div>

</div>

<v-click>

The key in `SourceCache` can be any type: a property, a `ValueTuple`, or a struct. Dynamic Data doesn't care — it uses it for identity.

</v-click>

<!--
- Pick SourceCache when your items have an ID — it's what you'll use most of the time.
- SourceList is great for ordered scenarios like command history or log entries.
-->

---

# Declaring a SourceCache

```csharp {2|all}
private readonly SourceCache<Hero, int> _heroCache
    = new SourceCache<Hero, int>(hero => hero.Id);
```

<v-click>

**Composite key** — combine properties into a `ValueTuple`:

```csharp
private readonly SourceCache<Hero, (string Name, string Universe)> _heroCache
    = new(hero => (hero.Name, hero.Universe));
```

</v-click>

<v-click>

The cache is private. The outside world only sees what you project from it.

</v-click>

<!--
- The key selector lambda runs once per item when it enters the cache.
- Composite keys work because ValueTuple implements structural equality.
- I have not tested it with C#15 Discriminated Unions
-->

---

# The Hard Part

> “For the longest time I didn't understand how to use Dynamic Data. I understood observables and how to get data **out**. I just couldn't find the door marked **IN**.”

```csharp
public HeroesViewModel(IEnumerable<Hero> initialHeroes)
{
    // TOP: define the pipeline that gets data out
    _heroCache.Connect()
        .ObserveOn(RxApp.MainThreadScheduler)
        .Bind(out _heroes)
        .Subscribe()
        .DisposeWith(Disposables);

    // BOTTOM: load data into the source cache
    _heroCache.AddOrUpdate(initialHeroes);
}
```

**Build the pipeline first. Then load the cache.** The subscription sees the initial data and every later change.

<!--
- I understood observables and the output pipeline, but the missing piece was how data enters the object.
- Connect defines the stream of changes coming out of the source cache. Subscribe activates it.
- AddOrUpdate puts the initial items into the cache. Later calls to AddOrUpdate, Remove, or EditDiff flow through the same pipeline.
- In a view model constructor, set up the pipeline at the top and load the data at the bottom so the subscription is ready for the initial load.
-->

---

# Loading Data: AddOrUpdate vs EditDiff

<div class="grid grid-cols-2 gap-8 mt-4">

<div>

### `AddOrUpdate` — upserts

```csharp
// Adds or upserts items.
// Previous contents stay.
_heroCache.AddOrUpdate(newHeroes);
```

Good for streaming in new records one at a time.

</div>

<div>

### `EditDiff` — diffs

```csharp
// Compares incoming vs existing.
// Adds what's new, removes what's gone.
_heroCache.EditDiff(
    incoming,
    (a, b) => a.Id == b.Id);
```

Good for search results where the entire result set replaces the previous one.

</div>

</div>

<!--
- AddOrUpdate is cumulative. EditDiff is a replace-with-diff operation.
- The equality function on EditDiff lets you define what "same item" means — useful when your DTO doesn't implement IEquatable.
- Keys determine identity; the equality function decides whether a matching item needs an update. This Id-only example assumes same-ID items are equivalent. Compare their content when same-ID values can change.
-->

---

# Loading Data: EditDiff in Practice

```csharp {all|1-3|4|5|6-7|all}
this.WhenAnyValue(x => x.SearchText)
    .Throttle(TimeSpan.FromMilliseconds(300), RxApp.TaskpoolScheduler)
    .SelectMany(term => _api.FindHeroes(term))
    .Where(results => results is not null)
    .Select(results => results.Select(r => r.ToDomain()))
    .Subscribe(heroes =>
        _heroCache.EditDiff(heroes, (a, b) => a.Id == b.Id));
```

<v-click>

Every keystroke (debounced) → API call → diff the cache → change set flows downstream automatically.

</v-click>

<!--
- This is the classic search pipeline.
- Notice there's no list manipulation, no Clear(), no foreach. Just pipe data in.
- The cache figures out what changed and projects only the delta.
-->

---

# Getting Data Out

```csharp {all|1|2|3|4|5|6|all}
_heroCache.Connect()
    .RefCount()
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _heroes)
    .Subscribe()
    .DisposeWith(Disposables);
```

<div class="mt-4 text-sm">

| Method | Purpose |
|---|---|
| `Connect()` | Start the observable change stream |
| `RefCount()` | Multicast — keep alive while subscribers exist |
| `Bind(out _heroes)` | Sync changes into a `ReadOnlyObservableCollection<T>` |

</div>

<!--
- Connect() turns the cache into a hot observable of change sets.
- RefCount() means the upstream only runs while something is subscribed.
- Bind() is the bridge to the UI — the ReadOnlyObservableCollection is what you bind in XAML.
- The _heroes field is immutable to the outside; mutations go through the cache.
-->

---

# DynamicData's Operator API

DynamicData adds its own operators to the **change-set stream**.

| Familiar job | LINQ / Rx | DynamicData |
| --- | --- | --- |
| Filter items | `Where(...)` | `Filter(...)` |
| Project items | `Select(...)` | `Transform(...)` |
| Order items | LINQ `OrderBy(...)` | `Sort(...)` |

Rx operators see each emitted **change set**. DynamicData's collection operators track the **item changes inside it**.

<v-click>

**Easy to mix up when you're new:** similar purpose, different names and overloads. Check the receiver type and which API IntelliSense is showing you.

</v-click>

<!--
- This is a vocabulary bridge before we walk through the operators.
- After Connect(), Rx Where receives an IChangeSet, not a Hero. Rejecting that emission drops the entire batch of changes.
- DynamicData Filter evaluates heroes and maintains the filtered collection as items are added, updated, or removed. Transform projects the items while preserving change-set semantics.
- The table compares familiar jobs, not interchangeable calls. LINQ OrderBy orders an enumerable; DynamicData Sort maintains collection order over time.
- Names can also overlap: DynamicData has a cache-aware RefCount, unlike Rx RefCount on a connectable observable. Inspect the receiver and overload.
- API reference: https://www.reactiveui.net/api/DynamicData/DynamicData/ObservableCacheEx/
-->

---

# Filter — Static

Simple predicate. Fixed at pipeline construction time.

```csharp
_heroCache.Connect()
    .Filter(hero => hero.Alignment == "Good")
    .Bind(out _heroes)
    .Subscribe();
```

<v-click>

Same filtering intent as LINQ `.Where()`, while maintaining a live result as items are added, updated, or removed.

</v-click>

<v-click>

`Connect(hero => hero.Alignment == "Good")` also accepts a predicate. In my testing, changing a value captured by that predicate does **not** re-filter existing items; use the observable `Filter` overload for that.

</v-click>

<!--
- Fine for filters that don't change at runtime.
- Connect(predicate) is another fixed-predicate option. Changing captured state alone does not emit a change set or re-evaluate existing items in my testing.
- For user-controlled filters, you need the dynamic overload on the next slide.
-->

---

# Filter — Dynamic

The filter predicate *is itself an observable*. When it emits a new function, the entire list re-evaluates.

```csharp {all|1-3|5-9|all}
IObservable<Func<Hero, bool>> alignmentFilter =
    this.WhenAnyValue(x => x.SelectedAlignment)
        .Select(alignment => (Func<Hero, bool>)(h => h.Alignment == alignment));

_heroCache.Connect()
    .Filter(alignmentFilter)
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _heroes)
    .Subscribe();
```

<v-click>

User changes a picker → `SelectedAlignment` changes → new predicate emits → list re-filters. **Zero event handlers. Zero state flags.**

</v-click>

<!--
- This is one of the most powerful features in Dynamic Data.
- The filter driving the list is itself a stream. When you change what you're filtering by, the list reacts automatically.
- You're crossing two observable streams — the data stream and the filter stream — and Dynamic Data handles the composition.
-->

---

# Transform

Projects each item in the cache to a new type — like LINQ `Select`, but reactive.

```csharp
_heroCache.Connect()
    .Transform(hero => new HeroViewModel(hero))
    .Bind(out _heroViewModels)
    .Subscribe();
```

<v-click>

Also useful for extracting distinct values for a picker:

```csharp
_heroCache.Connect()
    .Transform(hero => hero.Alignment)
    .DistinctValues(alignment => alignment)
    .Bind(out _alignments)
    .Subscribe();
```

</v-click>

<!--
- Transform is how you go from domain models to view models reactively.
- The transformed collection stays in sync — if a Hero is removed from the cache, its HeroViewModel is removed from the bound collection automatically.
- DistinctValues is great for building filter option lists from the data itself.
-->

---

# Sort — Static and Dynamic

**Static** — a fixed comparer set at pipeline construction:

```csharp
_heroCache.Connect()
    .Sort(SortExpressionComparer<Hero>.Ascending(h => h.Name))
    .Bind(out _heroes)
    .Subscribe();
```

<v-click>

**Dynamic** — the comparer *is an observable*. Change the observable, change the sort:

```csharp
IObservable<IComparer<Hero>> sortObservable =
    this.WhenAnyValue(x => x.SelectedTeam)
        .Select(team => SortExpressionComparer<Hero>
            .Ascending(h => h.TeamId == team ? 0 : 1)
            .ThenByAscending(h => h.Name));

_heroCache.Connect()
    .Sort(sortObservable)
    .Bind(out _heroes)
    .Subscribe();
```

</v-click>

<!--
- Dynamic sort is the same pattern as dynamic filter — just with an IComparer instead of a Func<T, bool>.
- When the user changes a dropdown, the sort observable emits a new comparer, and the list re-sorts in place.
-->

---

# AutoRefresh

Opt in to property change notifications **inside** the cache.

```csharp {all|2|all}
_heroCache.Connect()
    .AutoRefresh(hero => hero.RealName)
    .Sort(SortExpressionComparer<Hero>.Ascending(h => h.RealName))
    .Bind(out _heroes)
    .Subscribe();
```

<v-click>

Without `AutoRefresh`: changing `hero.RealName` at runtime — nothing happens.

With `AutoRefresh`: changing `hero.RealName` triggers the downstream sort, filter, or transform automatically.

</v-click>

<v-click>

It's **opt-in** by design. Listening to every INPC property on every item by default would be far too noisy.

</v-click>

<!--
- This is the feature that finally closes the INPC gap we talked about at the start.
- AutoRefresh on a specific property means only changes to that property trigger a downstream recalculation.
- The result: you get list view item animations in Xamarin/MAUI that you'd never see with a plain ObservableCollection.
-->

---

# WhenPropertyChanged

Listen for a property change across **all items** in the cache and react with an Rx pipeline.

```csharp
_heroCache
    .WhenPropertyChanged(hero => hero.RealName)
    .Where(change => !string.IsNullOrEmpty(change.Value))
    .Select(change => change.Sender)
    .InvokeCommand(this, vm => vm.LogHeroNameCommand);
```

<v-click>

This is pure Rx. `WhenPropertyChanged` returns an `IObservable<PropertyValue<Hero, string>>` — you can compose it with any Rx operator.

</v-click>

<!--
- WhenPropertyChanged is how you bridge per-item INPC into an Rx pipeline without foreach loops.
- You're subscribing to property changes across the entire collection in a single operator.
-->

---

# Batching

Group rapid mutations before they hit the UI. Prevents thrash when many items change in quick succession.

```csharp
_heroCache.Connect()
    .AutoRefresh(h => h.RealName)
    .Batch(TimeSpan.FromSeconds(1.5))
    .Sort(SortExpressionComparer<Hero>.Ascending(h => h.RealName))
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _heroes)
    .Subscribe();
```

<v-click>

100 property changes in 1 second → one re-sort and one UI update instead of 100.

</v-click>

<!--
- Batch is positioned before Sort/Bind so that accumulated changes are processed as a single change set.
- Essential when data arrives faster than your frame rate.
-->

---

# Join — Group Affiliation

Join heroes and villains by group, while keeping **multiple characters per group**.

<div class="flex items-center gap-3 text-sm font-semibold tracking-wide mt-3 mb-1">
  <span class="text-[var(--cyan)]">HEROES</span>
  <span class="opacity-50">→</span>
  <span class="text-[var(--purple)]">GROUP AFFILIATION</span>
  <span class="opacity-50">←</span>
  <span class="text-[var(--pink)]">VILLAINS</span>
</div>

```csharp {all|1|3-4|5-6|8-9|10-13|14-16|all}
record GroupMatchup(string Group, Hero[] Heroes, Villain[] Villains);

var heroGroups = _heroCache.Connect()
    .GroupWithImmutableState(h => h.GroupAffiliation);
var villainGroups = _villainCache.Connect()
    .GroupWithImmutableState(v => v.GroupAffiliation);

heroGroups
    .InnerJoin(villainGroups, group => group.Key,
        (heroes, villains) => new GroupMatchup(
            heroes.Key,
            heroes.Items.ToArray(),
            villains.Items.ToArray()))
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _matchups)
    .Subscribe();
```

<v-click>

Katana + Rick Flag and Harley Quinn + Deadshot in **Task Force X** → one matchup containing both sides. Adding or removing a character updates that group.

</v-click>

<!--
- Both source caches stay keyed by each character's unique Id; GroupWithImmutableState produces one snapshot per affiliation.
- InnerJoin matches those group keys and emits only affiliations represented on both sides.
- Replacing a character in its cache re-groups it if its affiliation changes. For in-place property changes, use AutoRefresh or GroupOnPropertyWithImmutableState.
-->

---

# Available Joins

| Operator | Groups in the result | Many variant |
| --- | --- | --- |
| `InnerJoin` | Heroes **and** villains | `InnerJoinMany` |
| `LeftJoin` | All hero affiliations | `LeftJoinMany` |
| `RightJoin` | All villain affiliations | `RightJoinMany` |
| `FullJoin` | All affiliations from either side | `FullJoinMany` |

<div class="mt-6 text-sm opacity-80">

The <code>*JoinMany</code> variants handle multiple right-side items per left key. This example groups both sides first, then uses <code>InnerJoin</code>.

</div>

<!--
- Inner retains only matching group keys; left and right retain every key from the named side; full retains the union.
- Outer joins supply Optional values for a missing side, so the result selector must handle that case.
- DynamicData also offers InnerJoinMany, LeftJoinMany, RightJoinMany, and FullJoinMany when several right-side items map to one left key.
-->

---

# Composing the Full Pipeline

Everything together in a view model constructor:

```csharp {all|1|2|3|4|5|6|7|8|9-10|all}
_heroCache.Connect()
    .AutoRefresh(h => h.RealName)
    .Filter(alignmentFilter)
    .Transform(h => new HeroViewModel(h))
    .Batch(TimeSpan.FromSeconds(1))
    .Sort(SortExpressionComparer<HeroViewModel>.Ascending(h => h.RealName))
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _heroes)
    .Subscribe()
    .DisposeWith(Disposables);
```

<v-click>

Read it left to right: *watch for property changes → filter by alignment → project to view models → batch → sort → deliver to the UI thread → bind → subscribe → clean up on disposal.*

</v-click>

<!--
- This is the complete pipeline. ~10 lines to get property-level reactive updates, dynamic filtering, type projection, dynamic sorting, and batching.
- Keep Sort last among change-set operators, immediately before ObserveOn and Bind, so nothing downstream disturbs the final order.
- You'd need 3x this in imperative code and you'd have to debug event handler lifecycle issues.
-->

---

# The Value Proposition

<div class="grid grid-cols-2 gap-8 mt-4">

<div>

**Without Dynamic Data**

- Manual event subscriptions
- Explicit state flags (`_isSorted`, `_currentFilter`)
- `foreach` loops to update items
- `Clear()` and re-add on every filter change
- Bugs when you forget to unsubscribe

</div>

<div>

**With Dynamic Data**

- Operators compose into a pipeline
- State lives in the observable chain
- No loops, no flags, no manual events
- Requirements change → add/swap one operator
- Disposal handles cleanup

</div>

</div>

<v-click>

The search + filter + sort + auto-refresh view model from the demo: **~60 lines**.

</v-click>

<!--
- The real payoff is when requirements change. "Can we also sort by team?" is one operator.
- In imperative code that question means touching event handlers, state flags, and the update loop.
-->

---

# Resources

<div class="grid grid-cols-2 gap-8 mt-6">

<div>

**Dynamic Data**

- GitHub: [reactiveMarbles/DynamicData](https://github.com/reactiveMarbles/DynamicData)
- Created by **Roland Pheasant**
- NuGet: `DynamicData`
- Snippets: `RolandPheasant/DynamicData.Snippets`

</div>

<div>

**Community**

- ReactiveUI Slack → `#dynamic-data`
- David Driscoll's SignalR + Dynamic Data talk — ReactiveUI Conf (YouTube)
- ReactiveUI docs: `reactiveui.net`

</div>

</div>

<v-click>

The maintainers are extremely approachable. Put an issue on the repo. Join the Slack. Ask questions — that's how the community grows.

</v-click>

<!--
- Roland is active on GitHub and Slack.
- The Slack channel is the fastest way to get answers.
- David Driscoll's talk is a great next step — he shows Dynamic Data + SignalR streaming data from a server in real time.
-->

---

# OpenSky Credit

Matthias Schäfer, Martin Strohmeier, Vincent Lenders, Ivan Martinovic, and Matthias Wilhelm.

**[Bringing Up OpenSky: A Large-scale ADS-B Sensor Network for Research](https://s3.opensky-network.org/website-public-files/publications/ipsn2014.pdf)**

In *Proceedings of the 13th IEEE/ACM International Symposium on Information Processing in Sensor Networks (IPSN)*, pages 83–94, April 2014.

[The OpenSky Network](https://opensky-network.org)

<!--
- OpenSky's requested citation for publicly available presentations using its data.
- Citation guidance: https://opensky-network.org/about/publications
-->

---
layout: center
class: text-center
---

# Questions?

[@rlittlesii](https://twitter.com/rlittlesii) · [github.com/rlittlesii](https://github.com/rlittlesii) · [twitch.tv/rlittlesii](https://twitch.tv/rlittlesii)

[https://github.com/RLittlesII/Transporter](https://github.com/RLittlesII/Transporter)

<!--
- Open the floor.
- If anyone wants to dig into a specific operator or see the full demo repo, happy to share links.
-->
