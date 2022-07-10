# How we want this to work

- Accept a value which we will check for validity.
- If a valid value, we return Some<T>
- If an invalid value, we return None<T>
- How do we define an invalid value?
  - null is the only default invalid value
  - Option<int>.From(intValue, i => i <= 0)
    - In this example, if the value is less than or equal to 0 it is invalid.
    - We define invalid values either as params of the original type, or params of functions - no mixture?
    - Option<int>.From(intValue).WithInvalidValues(69, 420, 1337).WithInvalidCallback(i => i < 0)
    - Option<int>.From(intValue, i => i <= 0, 69, 420, 1337)