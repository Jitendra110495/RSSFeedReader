# Quickstart: MVP RSS Reader

## Prerequisites

- .NET 8 SDK or later installed
- A code editor that supports C# and Blazor development

## Developer setup

1. Open the repository root in your editor.
2. Create the backend project under `backend/RSSFeedReader.Api`.
3. Create the frontend project under `frontend/RSSFeedReader.UI`.

## MVP implementation outline

1. Add a minimal ASP.NET Core Web API backend with a `SubscriptionsController`.
   - `GET /api/subscriptions` returns the current in-memory subscription list.
   - `POST /api/subscriptions` accepts `{ "url": "..." }` and adds a new subscription.
2. Store subscriptions in a simple in-memory list for the current session.
3. Add a Blazor WebAssembly frontend with a main page containing:
   - an input field for the feed URL
   - an add button
   - a list that displays current subscription URLs
4. Configure the frontend to call the backend API using a configurable base URL.

## Running locally

1. Run the backend project:
   ```powershell
   dotnet run --project backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj
   ```
2. Run the frontend project:
   ```powershell
   dotnet run --project frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj
   ```
3. Open the frontend URL shown in the terminal (typically `http://localhost:5213`).

## Validation

- Enter a feed URL in the UI and submit it.
- Verify the URL appears immediately in the subscription list.
- Confirm the app does not perform feed fetching or parsing.
