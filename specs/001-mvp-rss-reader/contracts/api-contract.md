# API Contract: MVP RSS Reader

## Subscription API

### GET /api/subscriptions

Returns the current session subscription list.

Response 200

```json
[
  {
    "url": "https://example.com/feed",
    "addedAt": "2026-06-02T12:00:00Z"
  }
]
```

### POST /api/subscriptions

Adds a new subscription URL to the current session.

Request body

```json
{
  "url": "https://example.com/feed"
}
```

Responses

- `201 Created` — subscription added successfully; returns the created subscription object
- `400 Bad Request` — request body is missing or `url` is empty

### Contract notes

- The MVP does not expose delete or update operations.
- The API does not fetch or parse feed content; it only manages a list of URLs.
- Duplicate URLs are allowed in the subscription list for MVP simplicity.
