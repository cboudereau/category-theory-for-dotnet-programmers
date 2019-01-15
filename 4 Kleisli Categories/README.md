# 4 Kleisli Categories

The Writer example (log every function call) is useful to hide the log for the caller (in the function signature). 
Only adapted functions return a string (the log) inside the Writer.
To adapt 2 functions, we need a kleisli composition (fish operator) that help us to use standard function (upper and words) inside the Writer.

## Personal notes (not in this book, but useful I think to get it)
https://softwareengineering.stackexchange.com/questions/165356/equivalent-of-solid-principles-for-functional-programming/171534

Kleisli composition can help you if you already are a [SOLID principles](https://en.wikipedia.org/wiki/SOLID) lover.