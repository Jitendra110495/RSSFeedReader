---
description: "Task list for MVP RSS Reader implementation"
---

# Tasks: MVP RSS Reader

**Input**: Design documents from `/specs/001-mvp-rss-reader/`

**Prerequisites**: plan.md (✓), spec.md (✓), research.md (✓), data-model.md (✓), contracts/api-contract.md (✓)

**Organization**: Tasks organized by user story (US1, US2) to enable independent implementation and testing. Setup and Foundational phases must complete before user story work begins.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies on incomplete tasks)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2)
- Include exact file paths in all descriptions

## Path Conventions

- **Backend**: `backend/RSSFeedReader.Api/` for ASP.NET Core Web API source
- **Frontend**: `frontend/RSSFeedReader.UI/` for Blazor WebAssembly source
- **Shared tests**: `backend/RSSFeedReader.Api.Tests/` and `frontend/RSSFeedReader.UI.Tests/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [X] T001 Create ASP.NET Core Web API project at `backend/RSSFeedReader.Api`
- [X] T002 Create Blazor WebAssembly project at `frontend/RSSFeedReader.UI`
- [X] T003 [P] Configure CORS policy in `backend/RSSFeedReader.Api/Program.cs` to allow frontend origin
- [X] T004 [P] Configure HTTP client and API base URL in `frontend/RSSFeedReader.UI/Program.cs`
- [X] T005 [P] Create `backend/RSSFeedReader.Api/Properties/launchSettings.json` with port 5151
- [X] T006 [P] Create `frontend/RSSFeedReader.UI/Properties/launchSettings.json` with port 5213

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete.

- [X] T007 Create in-memory subscription store service in `backend/RSSFeedReader.Api/Services/SubscriptionService.cs`
- [X] T008 [P] Create `Subscription` model in `backend/RSSFeedReader.Api/Models/Subscription.cs` with `url` and `addedAt` properties
- [X] T009 [P] Create `AddSubscriptionRequest` DTO in `backend/RSSFeedReader.Api/Models/AddSubscriptionRequest.cs`
- [X] T010 Create `SubscriptionsController` in `backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs` with GET and POST endpoints
- [X] T011 Implement validation middleware in `backend/RSSFeedReader.Api/Program.cs` to reject empty URLs with 400 Bad Request
- [X] T012 [P] Create error handling and logging configuration in `backend/RSSFeedReader.Api/Program.cs`
- [X] T013 Update frontend launchSettings to match backend API URL (`http://localhost:5151/api/`)
- [X] T014 Create `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` with API base URL

**Checkpoint**: Foundation ready — subscription API is operational and callable; user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Add a feed subscription (Priority: P1) 🎯 MVP

**Goal**: User can enter a subscription URL and see it immediately appear in the subscription list.

**Independent Test**: Add a valid URL, confirm it appears in the list. Works independently of US2.

### Implementation for User Story 1

- [X] T015 [US1] Create `Subscriptions.razor` page in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` with form and list display
- [X] T016 [US1] Implement subscription form input field in `Subscriptions.razor` to accept feed URL
- [X] T017 [US1] Create add subscription button and wire click handler in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [X] T018 [US1] Create HTTP service in `frontend/RSSFeedReader.UI/Services/SubscriptionApiService.cs` to call backend POST endpoint
- [X] T019 [US1] Implement `AddSubscription()` method in `SubscriptionApiService.cs` to POST URL to `/api/subscriptions`
- [X] T020 [US1] Handle API response and update local subscription list in `Subscriptions.razor` after successful add
- [X] T021 [US1] Add validation to prevent empty URL submission in `Subscriptions.razor` (client-side)
- [X] T022 [US1] Display success feedback or error message for add operation in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [X] T023 [US1] Implement backend POST `/api/subscriptions` endpoint in `SubscriptionsController` to accept and store subscription
- [X] T024 [US1] Ensure backend sets `addedAt` timestamp (ISO 8601 UTC) in `SubscriptionService.cs` on subscription creation
- [X] T025 [US1] Return 201 Created with subscription object in response from POST endpoint in `SubscriptionsController`
- [X] T026 [US1] Create unit tests for `SubscriptionService.cs` in `backend/RSSFeedReader.Api.Tests/Services/SubscriptionServiceTests.cs`
- [X] T027 [P] [US1] Create integration tests for POST `/api/subscriptions` in `backend/RSSFeedReader.Api.Tests/Controllers/SubscriptionsControllerTests.cs`

**Checkpoint**: US1 is fully functional and independently testable; user can add subscriptions and see them appear.

---

## Phase 4: User Story 2 - View current subscriptions (Priority: P2)

**Goal**: User can see all current subscription URLs in a formatted list on the main UI.

**Independent Test**: Add subscriptions then view the list; confirm each URL is displayed. Works independently of US1 after Foundational phase.

### Implementation for User Story 2

- [ ] T028 [US2] Display subscription list in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` with columns for URL and added time
- [ ] T029 [US2] Create HTTP service method in `SubscriptionApiService.cs` to call backend GET `/api/subscriptions` endpoint
- [ ] T030 [US2] Implement `GetSubscriptions()` method in `SubscriptionApiService.cs` to fetch current list
- [ ] T031 [US2] Load and display subscription list on page load in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [ ] T032 [US2] Format `addedAt` timestamp for display (human-readable date/time) in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor`
- [ ] T033 [US2] Implement backend GET `/api/subscriptions` endpoint in `SubscriptionsController` to return current in-memory subscription list
- [ ] T034 [US2] Return 200 OK with subscription array in response from GET endpoint in `SubscriptionsController`
- [ ] T035 [P] [US2] Add client-side subscription list refresh after new subscription added in `Subscriptions.razor`
- [ ] T036 [US2] Create integration tests for GET `/api/subscriptions` in `backend/RSSFeedReader.Api.Tests/Controllers/SubscriptionsControllerTests.cs`

**Checkpoint**: US2 is fully functional and independently testable; user can view all current subscriptions.

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Quality improvements and finalization that affect multiple user stories

- [ ] T037 [P] Update `frontend/RSSFeedReader.UI/Layout/NavMenu.razor` to remove template demo links and add subscription management link
- [ ] T038 [P] Remove Blazor template demo pages from `frontend/RSSFeedReader.UI/Pages/` (Home.razor, Counter.razor, Weather.razor)
- [ ] T039 [P] Update `frontend/RSSFeedReader.UI/App.razor` routing to remove demo page routes
- [ ] T040 Verify no ambiguous routes in Blazor project (each page has unique `@page` directive) in `frontend/RSSFeedReader.UI/Pages/`
- [ ] T041 Clean build both backend and frontend projects to verify no compilation errors
- [ ] T042 Run backend unit and integration tests and verify passing in `backend/RSSFeedReader.Api.Tests/`
- [ ] T043 [P] Run manual E2E validation: add subscription URL via UI and confirm it appears in list
- [ ] T044 [P] Verify subscription list updates within 2 seconds after submission (SC-002)
- [ ] T045 [P] Verify app supports at least five subscriptions without data loss (SC-003)
- [ ] T046 [P] Verify no external feed fetching or parsing occurs during subscription addition (SC-004)
- [ ] T047 Test duplicate URL handling: add same URL twice and confirm both appear in list
- [ ] T048 Test empty URL validation: attempt to add empty/whitespace-only URL and confirm it is rejected
- [ ] T049 Add documentation to `quickstart.md` with setup and validation steps (reference existing doc)
- [ ] T050 Create/update `backend/RSSFeedReader.Api/README.md` with build and run instructions
- [ ] T051 Create/update `frontend/RSSFeedReader.UI/README.md` with build and run instructions

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies — can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion — **BLOCKS all user stories**
- **User Stories (Phases 3–4)**: All depend on Foundational completion
  - US1 (T015–T027) and US2 (T028–T036) can run in parallel once Foundational is done
- **Polish (Phase 5)**: Depends on all desired user stories being complete

### User Story Dependencies

- **US1 (P1)**: Can start after Foundational — no dependencies on other stories
- **US2 (P2)**: Can start after Foundational — may integrate with US1 but is independently testable

### Within Each User Story

- US1: Contracts (T015–T022) → Services (T023–T025) → Tests (T026–T027)
- US2: Display layer (T028–T032) → Services (T033–T034) → Tests (T035–T036)

### Parallel Opportunities

- **All Setup tasks marked [P]** can run in parallel (T003, T004, T005, T006)
- **All Foundational model/DTO tasks marked [P]** can run in parallel (T008, T009, T012)
- **All backend tests marked [P]** can run in parallel (T027, T036)
- **All Polish tasks marked [P]** can run in parallel (T037, T038, T039, T043, T044, T045, T046, T050, T051)
- Once Foundational phase completes, **US1 and US2 can be worked in parallel** by different team members

---

## Parallel Example: User Story 1

```bash
# Team member A starts with form UI
Task: T015 - Create Subscriptions.razor page
Task: T016 - Implement subscription form input field
Task: T017 - Create add subscription button

# Team member B starts with backend API
Task: T023 - Implement backend POST /api/subscriptions endpoint
Task: T024 - Ensure backend sets addedAt timestamp

# Both can proceed independently and integrate when complete
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL — blocks all stories)
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE**: Test User Story 1 independently
   - Add a subscription URL via UI
   - Confirm it appears immediately in the list
   - Verify `addedAt` timestamp is present
5. If US1 works correctly, proceed to US2 for Extended-MVP

### Extended-MVP (User Story 1 + 2)

1. Verify US1 from above
2. Complete Phase 4: User Story 2
3. **VALIDATE**: Test US2 independently
   - Refresh the page
   - Confirm all subscriptions are still displayed
   - Verify timestamps are readable
4. Complete Phase 5: Polish
5. **FULL VALIDATION**: Run E2E tests covering both US1 and US2 together

---

## Success Criteria Traceability

| Success Criterion | Related Tasks | Pass Condition |
|-------------------|---------------|---|
| **SC-001**: Users can add a subscription and see the new URL appear in the subscription list on the same screen | T015–T025 | T043 E2E validation: URL appears after submission |
| **SC-002**: The subscription list updates within 2 seconds after the user submits a new feed URL | T017, T018, T019, T020 | T044 timing test: update ≤2s |
| **SC-003**: The app supports at least five subscription entries in a session without loss of visible data | T028–T035, T007 | T045 validation: add 5+ URLs, all visible |
| **SC-004**: The MVP does not perform any external feed fetch or parsing operations during normal subscription addition | Spec constraint enforced by architecture | T046 validation: no external API calls |

---

## Test Strategy

### Unit Tests (Phase 2 Foundational & Phase 3 US1)

- [ ] `SubscriptionService.cs` tests:
  - Add subscription with valid URL → returns subscription with `addedAt` set
  - Add subscription with empty URL → throws or returns error
  - Get subscriptions → returns current list
  - Duplicate URLs → both stored

### Integration Tests (Phase 3 US1 & Phase 4 US2)

- [ ] POST `/api/subscriptions` with valid URL → 201 + subscription with `addedAt`
- [ ] POST `/api/subscriptions` with empty URL → 400 Bad Request
- [ ] GET `/api/subscriptions` → 200 + subscription array
- [ ] POST then GET → returns newly added subscription

### End-to-End Tests (Phase 5 Polish)

- [ ] User adds URL via UI → appears in list within 2s
- [ ] Page refresh → subscriptions still visible (session retention)
- [ ] Add 5+ subscriptions → all visible without scrolling issues
- [ ] Duplicate URL → both entries shown separately

---

## Total Task Count

- **Setup**: 6 tasks (T001–T006)
- **Foundational**: 8 tasks (T007–T014)
- **User Story 1**: 13 tasks (T015–T027)
- **User Story 2**: 9 tasks (T028–T036)
- **Polish & Testing**: 15 tasks (T037–T051)
- **TOTAL: 51 tasks**

---

## MVP Scope Recommendation

**Minimum Implementation for Proof-of-Concept**:

1. Complete Phase 1 (Setup)
2. Complete Phase 2 (Foundational)
3. Complete Phase 3 (User Story 1: Add subscription)
4. Tasks T043–T046 (critical E2E validation)

**This minimal subset delivers the core MVP value**: users can add feed subscription URLs and see them appear immediately. Total: ~19 core tasks.

---

## Suggested Execution Timeline

- **Day 1–2**: Phases 1–2 (Setup + Foundational) = ~2 days, 1 person
- **Day 3–4**: Phase 3 (User Story 1) = ~2 days, 1–2 people
- **Day 5–6**: Phase 4 (User Story 2) + Phase 5 (Polish) = ~2 days, 1–2 people

**Total**: ~6 days for full MVP + Extended-MVP
**Minimum (US1 only)**: ~3–4 days
