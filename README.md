# Screwing around with a trivial BlockChain implementation

Why? Curiosity.

It's not fancy. But it was fun!

## Notes

- I implemented a symbol blockchain (**not** a distributed ledger - that's way TF more complicated.)
- This is all in-memory. No consideration for durable storage was made.
- This implementation is data / serializer agnostic. Just provide a byte array. It's up to the consuming application to care about serialization.
- I used immutable ojbects and immutable arrays because it just felt right. 
  -  Does this add any actual application security? I don't think so. But I think it's a clean programming style for this sort of thing.
    - Immutable objects lend themselves to multithreaded applications without the need for complex locking mechanisms.
    - They advertise that you're not supposed to change property values after the object has been created.
- I used a guid id, because that's in my recent muscle memory. However, it might make more sense to use an incrementing integer as order is critical to block chains.
- Note that I *don't* implement any kind of locking or concurrency protections. This code **is not reentrant**.
  - How would I make it rentrant?
    - The simple way would be to put a lock around the body of the `Add(byte[] data)` method. This isn't the whole ball of wax though. Care must be given to ensure that the calls to the `Blocks` property are handled properly. This might end up needing to ba `ReaderWriterLockSlim` or similar mechanism.