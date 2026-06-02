# Data Model: MVP RSS Reader

## Entities

### Subscription
- **Description**: Represents a feed source the user has added.
- **Fields**:
  - `url` (string) — the feed URL entered by the user
  - `addedAt` (DateTime?) — timestamp when the subscription was added (optional for MVP)

### Subscription List
- **Description**: The current in-memory collection of subscriptions active during the user session.
- **Representation**: A simple list or array of `Subscription` instances.

## Relationships

- A `Subscription List` contains zero or more `Subscription` entries.
- There are no parent/child relationships beyond the list container.

## Validation rules

- `Subscription.url` MUST be non-empty.
- `Subscription.url` MAY be any well-formed URL, but strict URL validation is optional and deferred for MVP.
- Duplicate URLs are allowed in the MVP subscription list.

## State transitions

- **Add subscription**: A new `Subscription` is appended to the `Subscription List` when the user submits a non-empty URL.
- **Session retention**: Subscriptions remain in memory until the application session ends or the page reloads.

## Notes

- Persistence is intentionally excluded from the MVP.
- Feed item entities and fetching state are deferred to Extended-MVP.
