# Project Context & Session History

This file maintains the ongoing context of the TimeTracker2 project. 
**LLM Instruction:** Read this file first to understand the current state, conventions, and recent changes. Update this file after significant milestones to maintain a continuous development context.

## Current Project State
- **Project Name:** TimeTracker2
- **Framework:** .NET 10.0 Windows Forms
- **Architecture:** 
  - `Helpers/`: Contains `FileHelper` (file I/O) and `DatabaseManager` (business logic).
  - `Enum/`: Contains `FolderEnum` for centralized file naming.
  - `UI/`: `Login.cs` and `MainMenu.cs`.

## Key Features Implemented
1. **Login System:**
   - Custom dark-themed UI with rounded panels.
   - Validation for empty names.
   - Saves user name to `Documents/TimeTrackerData/Authentication.txt`.
2. **Persistence Layer (`FileHelper`):**
   - Handles file creation, reading, and appending in a dedicated Documents folder.
   - Uses `FolderEnum` for type-safe file access.
   - Automatically creates missing files on read.
3. **Main Menu:**
   - Dark-themed UI matching the Login form.
   - Custom-drawn `ListBox` for tasks with modern styling.
   - Auto-login logic in `Program.cs` based on existing authentication data.

## Development Conventions
- **UI Theme:** Dark mode (Navy: `#0f172a`, Slate: `#1e293b`, Accent: `#0ea5e9`).
- **File Access:** Always use `FileHelper` and `FolderEnum`.
- **Form Handling:** `FormBorderStyle.None` or `FixedSingle`, `CenterScreen` start position.

## Recent Changes (2026-03-03)
- Created `FolderEnum` and refactored `FileHelper` to use it.
- Implemented auto-redirect in `Program.cs`.
- Added custom drawing to `MainMenu` ListBox for better UI/UX.
- Added dummy data and auto-selection to the task list.
