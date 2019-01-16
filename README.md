# ImmutableHashDictionary
An implementation of `IImmutableDictionary<TKey, TValue>` that maintains O(1) value lookup, at the cost of mutation performance.

A good choice when working with dictionaries that remain moderately small and/or that are accessed for reading far more frequently than for modification.

This package also includes an `ImmutableHashDictionary` static factory class and an `ImmutableHashDictionary<TKey, TValue>.Builder` class that provide API''s identical to the `ImmutableDictionary` and `ImmutableDictionary<TKey, TValue>.Builder` classes.

Targets .NET Standard 2.0.

C#8 (Nullable Reference Types) Compliant.

## Benchmarks
The following benchmarks demonstrate the performance advantages and disadvantages of the `ImmutableHashDictionary<TKey, TValue>` class over the `Dictionary<TKey, TValue>` and `ImmutableDictionary<TKey, TValue>` classes.

The performance of value lookup by key for `ImmutableHashDictionary<TKey, TValue>` is, as desired, on par with that of `Dictionary<TKey, TValue>` and far exceeds that of `ImmutableDictionary<TKey, TValue>`, even for very large collections.

The performance of single add and remove operations for `ImmutableHashDictionary<TKey, TValue>` is, as expected, comparable to that of `ImmutableDictionary<TKey, TValue>` for smaller collections, but does not scale well (*O(N) vs O(log N)*).

Interestingly, the performance of ranged add and remove operations for `ImmutableHashDictionary<TKey, TValue>` tends to far exceed that of `ImmutableDictionary<TKey, TValue>`, and tends to be comparable to that of `Dictionary<TKey, TValue>` suggesting that the performance costs of mutating an `ImmutableHashDictionary` can be mitigated by performing mutations in batches, as much as possible.

- [value = this[key]](Benchmarks/GetValue.md)
- [.CreateRange(keyValuePairs)](Benchmarks/CreateRange.md)
- [.Add(key, value)](Benchmarks/Add.md)
- [.Add(key, value) *(when .Count is prime)*](Benchmarks/AddWhenCountIsPrime.md)
- [.AddRange(keyValuePairs)](Benchmarks/AddRange.md)
- [.SetItem(key, value) *(replacement)*](Benchmarks/SetItemReplace.md)
- [.SetItems(keyValuePairs) *(replacement)*](Benchmarks/SetItemsReplace.md)
- [.Remove(key)](Benchmarks/Remove.md)
- [.RemoveRange(keys)](Benchmarks/RemoveRange.md)
