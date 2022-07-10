# Maybe | Just | Nothing

> This is a functional paradigm which I use to avoid null usage. It is a relatively simple wrapper around a value, with some extension methods to enable quick, chained invalidity testing of values WITHOUT needing to ever use the null keyword. This wrapper handles the checking and then you never deal with a null value again - only `Just<T>` or `Nothing<T>` depending on whether the value contained is valid or not.

## Usage

> First of all, assume we have a PersonRepository with a `Get(int id)` method. 1 and 2 are valid people, 3 and above will actually return `null`.
>
> We don't like null, but that's okay - just throw the result into an Option with some optional invalid value checks and do what you need with it.

> Here is the very very basic usage of this.

```cs
var result = _personRepository.Get(1); // Valid person

return Option<Person>.From(result).GetOrThrow(new PersonNotFoundException());
```

> Now, our result is perfectly valid here so therefore when `Option<Person>.From(result)` is called, it is evaluated and the resulting object is of type `Just<Person>`. Now, we know this is just a person.
> If this result came back with `null` the type of the resulting object would be `Nothing<Person>`. This does not track the state of the passed-in object, so when we see null, that value is not kept anywhere (who needs null anyway?).

### All of the usage options

```cs
var just1 = Option<string>.From("hello");		// we only compare this against null
var just2 = Option<string>.From("hello", "world"); 	// "world" is now defined as an invalid value
var just3 = Option<string>.From(
	"hello",
	x => x == "world"
);							// "world" is again defined as an invalid value, but via a predicate
var just4 = Option<string>.From(
	"hello",
	x => x == x.ToUpper(),
	"world",
	"universe"
);							// "world" and "universe" are invalid values, as are any fully upper-case strings...

// When any of the above are actually satisfied, we get back a Nothing<T> like so

var nothing = Option<string>.From(
	"HELLO",
	x => x == x.ToUpper()
);							// This is now Nothing<T> as the value was uppercase, thus satisfied the predicate

// ---
// Once the Maybe is defined (be it Just a value or Nothing) we can retrieve its value whilst defining a default behaviour if the value is invalid.
```

| Method 					| `Just<T>` behaviour 			| `Nothing<T> behaviour 					|
| :-------------------------------------------: | :-----------------------------------: | :-----------------------------------------------------------: |
| `Get()` 					| Returns the value 			| Throws `MaybeValueNotFoundException` 				|
| `GetOrElse(T defaultValue)` 			| Returns the value 			| Returns the passed `defaultValue` 				|
| `GetOrCall(Func<T> defaultFunc)` 		| Returns the value 			| Returns the result of running the passed `defaultFunc` 	|
| `GetOrCallAsync(Func<Task<T>> defaultFunc)` 	| Returns the value wrapped in a Task 	| Returns the result of running the passed `async defaultFunc` 	|
| `GetOrThrow(Exception exception)` 		| Returns the value 			| Throws the passed `exception` 				|
| `IsValid()` 					| Returns true 				| Returns false 						|
| `IsInvalid() 					| Returns false 			| Returns true 							|
