# Fluxera.ValueObject
A value objects library.

This library helps in implementing **Value Object** classes in the context of **Domain-Driven Design**. 
A **Value Object** has several traits, some of which this library provides. 

A **Value Object**

- is **immutable**. Every property must be read-only (i.e. no setter allowed) after instantiation.
- contains **domain logic and behaviours**. It should encapsulate the domain complexity within it.
- uses the **Ubiquitous Language** of the domain. A **Value Object** is an elegant way of embracing the language of the domain in the codebase.
- exposes, uses and combines **functions** to provide domain value. Functions usually return new instances of a **Value Object**. _Closure of Operations_ describes an operation whose return type is the same as the type of it's arguments.

```c#
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

By having your **Value Object** derive from the ```ValueObject<TValueObject>``` base class it properly
implements **Equality** (```Equals()```) and **Uniqueness** (```GetHashCode()```). Automatically
**all** public properties are used for the calculations **without** you having
to write a single line of code.

This default implementation uses reflection to aquire the metadata and to get the values to use. You can provide yourf own
implementation simply by overriding the ```GetEqualityComponents()``` method.

A simple implementation would look like the this:

```c#
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
return the values yourself. Keep in mind, that **all attibutes** should be used for **Equality** and **Uniqueness**.

```c#
protected override IEnumerable<object> GetEqualityComponents()
{
    yield return this.Quantity;
    yield return this.Currency;
}
```
