
## Description

A simple `Maybe` class with support for LINQ query syntax via
`Select` and `SelectMany` extension methods.

## Methods

S = static method
<br>
E = extension method

* (S) Just
* (S) Nothing
* IsJust
* IsNothing
* FromJust
* OrElse
* (S) Return
* (E) Bind
* (E) Select
* (E) SelectMany (2 overloads)
* (E) Lookup (IDictionary&lt;TKey, TValue&gt; extension method)

## Example

```C#
var mr =
    from value in dictionary.Lookup(key)
    from result in SomeMethodThatReturnsMaybe(value)
    select SomeTransform(result);
```

In the code above, we lookup a key in a dictionary and get
back a Maybe which will either be a Just containing the value
corresponding to the key or a Nothing. If the dictionary lookup returned Nothing then the whole expression will short circuit and `mr`
will be Nothing. Otherwise, the value inside Just
will be bound to the range variable `value`. We then call
`SomeMethodThatReturnsMaybe` passing the raw value from the dictionary.
Again, this will return Just or Nothing. If Nothing, then we again
short circuit and `mr` will be Nothing. Otherwise,
the value in the Just returned by `SomeMethodThatReturnsMaybe` will be
bound to range variable `result`.
We then pass `result` to `SomeTransform` which is assumed to return
a raw value rather than a Maybe. The raw value returned by
`SomeTransform` will automatically be put back inside the Maybe monad
to yield the final value of `mr`.

Without the support of `Select` and `SelectMany` and without using `Bind`,
the code would look something like the following:

```C#
var mr = Maybe.Nothing<TypeReturnedBySomeTransform>();
var mvalue = dictionary.Lookup(key);
if (mvalue.IsJust) {
    var value = mvalue.FromJust;
    var mresult = SomeMethodThatReturnsMaybe(value)
    if (mresult.IsJust) {
        var result = mresult.FromJust;
        var transformedResult = SomeTransform(result);
        mr = Maybe.Just(transformedResult);
    }
}
```

With `Bind`, it would look something like the following:

```C#
var mr = dictionary.Lookup(key).Bind(v =>
    SomeMethodThatReturnsMaybe(value).Bind(result =>
        Maybe.Return(SomeTransform(result))));
```

The version of the code that uses LINQ query syntax avoids the increasing
indentation. This would be even more noticeable if the expression involved
more steps.

## Links

* [C# Language Specification 5.0](http://www.microsoft.com/en-gb/download/confirmation.aspx?id=7029)
    * Section 17.6 "Query expressions"
