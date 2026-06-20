Difficulty: Intermediate
Prerequisites:
    - C#
    - MVVM
    - Reactive Extensions
---

# Bridging IEnumerable and IObservable with Dynamic Data

## Abstract
You've bound a list, updated an item's property, and watched the UI do nothing. That gap in `ObservableCollection<T>` is by design: binding a list requires `INotifyCollectionChanged` — not just `INotifyPropertyChanged` — and out of the box the two don't compose. Dynamic Data, created by Roland Pheasant and now a dependency of ReactiveUI, bridges `IEnumerable` and `IObservable` by projecting change sets out of your collections. With a `SourceCache<T, TKey>` or `SourceList<T>` at the core, you get reactive operators — Filter, Transform, Sort, AutoRefresh, Batch — that can be driven by other observables. Want the list to re-sort every time a picker selection changes? That's one operator. Want property changes inside list items to trigger a new sort pass? That's one operator. In this session we'll demonstrate a few working view models that, connect to a data source, and pipe changes all the way to the UI — without a single `foreach`, event handler, or manual `ObservableCollection` reassignment.