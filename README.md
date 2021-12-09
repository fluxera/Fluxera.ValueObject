[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.ValueObject?branchName=main)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=63&branchName=main)

# Fluxera.ValueObject
A value objects library.

This library helps in implementing **Value Object** classes in the context of **Domain-Driven Design**. 
A **Value Object** has several traits, some of which this library provides. 

A **Value Object**

- is **immutable**. Every property must be read-only (i.e. no setter allowed) after instantiation.
- contains **domain logic and behaviours**. It should encapsulate the domain complexity within it.
- uses the **Ubiquitous Language** of the domain. A **Value Object** is an elegant way of embracing the language of the domain in the codebase.
- exposes, uses and combines **functions** to provide domain value. Functions usually return new instances of a **Value Object**. _Closure of Operations_ describes an operation whose return type is the same as the type of it's arguments.

```C#
// The following function doesn't change any given Amount instance, it just returns a new one.
public Amount Add(Amount amount) 
{
    if(this.Currency != amount.Currency)
    {
        throw new InvalidOperationException("Cannot add amounts with different currencies.");
    }

    Amount result = new Amount(this.Quantity + amount.Quantity, this.Currency);
    return result;
}
```

- uses **all** of it's **attibutes** for **Equality** and **Uniqueness**.
- is **automatically validated** upon instantiation using **domain validation** and throws exception if a validation fails.

## Usage

### ```ValueObject<TValueObject>```

By having your **Value Object** derive from the ```ValueObject<TValueObject>``` base class it properly implements **Equality** 
(```Equals()```) and **Uniqueness** (```GetHashCode()```). Automatically **all** public properties are used for the calculations 
**without** you having to write a single line of code.

This default implementation uses reflection to aquire the metadata and to get the values to use. You can provide your own
implementation simply by overriding the ```GetEqualityComponents()``` method.

A simple implementation would look like the this:

```C#
public class Amount : ValueObject<Amount>
{
    public Amount(decimal quantity, Currency currency)
    {
        this.Quantity = quantity;
        this.Currency = currency;
    }

    public decimal Quantity { get; }

    public Currency Currency { get; }

    public Amount Add(Amount amount)
    {
        if(this.Currency != amount.Currency)
        {
            throw new InvalidOperationException("Cannot add amounts with different currencies.");
        }

        Amount result = new Amount(this.Quantity + amount.Quantity, this.Currency);
        return result;
    }
}
```

If you decide not to use the relection-based approach, you can simply override ```GetEqualityComponents()``` and
return the values manually. Keep in mind, that **all attibutes** should be used for **Equality** and **Uniqueness**.

```C#
protected override IEnumerable<object> GetEqualityComponents()
{
    yield return this.Quantity;
    yield return this.Currency;
}
```

### Collections

There are implementations of ```IList<T>```, ```ISet<T>``` and ```IDictionary<TKey, TValue>``` that determine
equality based on content and not on the collection reference.

When your **Value Object** contains a collection, this collection needs to be wrapped in one of the
available value collection to support the correct way for equality.

If you use the default behavior you can just wrap the collection in the constructor like below.
The default equality behavior will automatically pick the value up.

```C#
public class Confederation : ValueObject<Confederation>
{
    public Confederation(string name, IList<Country> memberCountries)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrEmpty(memberCountries, nameof(memberCountries));

        this.Name = name;
        this.MemberCountries = memberCountries.AsValueList(); // Wrap the list in a value list.
    }

    public string Name { get; }

    public IList<Country> MemberCountries { get; }
}
```

If you prefer to override ```GetEqualityComponents()``` and return the values manually you can wrap the list later.

```C#
protected override IEnumerable<object> GetEqualityComponents()
{
    yield return this.Name;
    yield return this.MemberCountries.AsValueList(); // Wrap the list in a value list.
}
```

**Important** Collections **must** be wrapped in value collections to ensure the correct equality behavior.

#### ```ValueList<T>```

A list with equality based on the content instead on the list's reference, i.e. two different list instances 
containing the same items in the same order will be equal.

```C#
// Wrap an IList in a ValueList. 

IList<Country> countries = new List<Country> 
{
    Country.Create("DE"),
    Country.Create("US"),
};

IList<Country> valueList = countries.AsValueList();
```

#### ```ValueSet<T>```

A set with equality based on the content instead on the set's reference, i.e two different set instances containing 
the same items will be equal regardless of their order.

```C#
// Wrap an ISet in a ValueSet. 

ISet<Country> countries = new HashSet<Country>
{
    Country.Create("DE"), 
    Country.Create("US"),
};

ISet<Country> valueSet = countries.AsValueSet();
```

#### ```ValueDictionary<TKey, TValue>```

A dictionary with equality based on the content instead on the dictionary's reference, i.e. two different dictionary 
instances containing the same items will be equal.

```C#
// Wrap an IDictionary in a ValueDictionary. 

IDictionary<int, Country> countries = new Dictionary<int, Country>
{
    { 1, Country.Create("DE") }, 
    { 4, Country.Create("US") },
};

IDictionary<int, Country> valueDictionary = countries.AsValueDictionary();
```
