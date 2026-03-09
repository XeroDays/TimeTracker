# TimeTracker

A modern, dark-themed Timesheet Tracker built with .NET 10.0 and Windows Forms. Track project time with automatic session handling, system tray integration, and Excel export.

## Overview

TimeTracker is a desktop application that helps you track time spent on different projects. Switch projects to start the timer, and the app records sessions automatically. Data is stored locally in your Documents folder. When you lock the PC or shut down, the current project is paused so time is not over-counted.
<img width="660" height="451" alt="image" src="https://github.com/user-attachments/assets/423043a8-7e05-4d51-9325-7a620cb2a81e" />

## Key Features

- **Modern UI/UX** – Dark-themed interface with navy, slate, and sky blue accents; custom-drawn controls and borderless draggable windows.
- **Project Switching** – Select a project to start tracking; switching updates the timer and persists the default project.
- **System Tray** – App icon in the notification area with context menu to switch projects, show/hide window, and toggle Start with Windows.
- **Minimize to Tray** – Minimizing hides the window from the taskbar; double-click tray icon to restore.
- **Auto-Start with Windows** – Enable from the tray context menu; uses Registry Run key for the current user.
- **Session Awareness** – When you lock the PC or end the session, the app records "Pause" so time is not attributed to the last project during away time.
- **Excel Export** – Export a timesheet with projects as rows, dates as columns, and hours per project per day (2 decimal places).
- **Persistent Storage** – File-based storage in `Documents/TimeTrackerData/` (JSON tracking entries and text config files).

## How the Timer Works

- Each **tracking entry** is `{ ProjectName, Timestamp }` stored as JSON.
- When you switch projects, a new entry is written. The session for a project runs from that timestamp until the next entry (or until now if it is the latest).
- **Duration** for a session = `nextEntry.Timestamp - startEntry.Timestamp` (or `DateTime.Now - startEntry.Timestamp` if it is the current session).
- **Pause** – Lock, sleep, or shutdown triggers a "Pause" entry. The previous project’s session ends at that moment.
- **Days with no records** – A session spanning multiple days (e.g., Friday evening to Monday morning) only attributes time to dates that have at least one tracking entry. Days in between with no records show 0 hours, not 24.
- **Excel date range** – All dates from the first to the last tracking (including today if a session is still running) appear as columns; dates with no records show 0.00.

## Data Storage

All data is stored under `Documents/TimeTrackerData/`:

| File          | Purpose                           |
|---------------|-----------------------------------|
| Authentication | User name (one-time registration) |
| Projects      | List of project names             |
| DefaultProject| Last selected project             |
| Trackings     | JSON lines: `{"ProjectName":"…","Timestamp":"…"}` |

## Excel Export

- **Projects** in the first column (bold).
- **Dates** as columns in `d-MMM` format (e.g., 5-Mar, 6-Mar).
- **Hours** per project per date, formatted to 2 decimals (0.00). Missing data shows 0.
- Header row: grey background, bold. Project names: bold.

## Getting Started

1. Clone the repository.
2. Open the solution in Visual Studio or your preferred IDE.
3. Build and run the project.
4. Enter your name on first launch to initialize your profile.
5. Add projects and start tracking by selecting a project from the list.

---

## Credits

**Author:** [Xerodays](https://github.com/Xerodays)

---

## License

This project is licensed under the MIT License.

```
MIT License

Copyright (c) Xerodays

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
