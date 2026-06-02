# Research: MVP RSS Reader

## Decision

The MVP implementation will use an ASP.NET Core Web API backend and a Blazor WebAssembly frontend, matching the project TechStack. The app will store subscriptions in memory only and will not fetch or parse feed content.

## Rationale

- The stakeholder documents emphasize a minimal proof-of-concept focused on subscription management.
- ASP.NET Core + Blazor WebAssembly is the selected technology stack and supports the intended future path to feed fetching, persistence, and richer UI.
- In-memory storage keeps the MVP simple while enabling a later migration to persistence.
- Allowing duplicate URLs simplifies the MVP and avoids adding deduplication logic before the core behavior is stable.

## Alternatives considered

- Frontend-only implementation: rejected because the specified architecture calls for a backend API and separation of concerns.
- Adding persistence in MVP: rejected to preserve rapid delivery and to keep scope aligned with the current user stories.
- URL validation beyond non-empty input: deferred because the MVP requirements explicitly allow assuming valid URLs and focus on basic subscription flow.
- Deduplication of duplicate URLs: deferred because duplicates are an acceptable MVP limitation and the priority is on immediate UI feedback.

## Outcome

The plan will proceed with a minimal backend API contract for adding and listing subscriptions, plus a simple Blazor UI to collect and display subscription URLs.
