# Project Context & Session History

This file maintains the ongoing context of the TimeTracker project.
**LLM Instruction:** Read this file first to understand the current state, conventions, and algorithms. Update this file after significant milestones to maintain a continuous development context.

---

## Current Project State

- **Project Name:** TimeTracker
- **Framework:** .NET 10.0 Windows Forms
- **Namespace:** TimeTracker
- **Data Location:** `Documents/TimeTrackerData/`

---

## Architecture

```
TimeTracker2/
├── Forms/
│   ├── MainMenu.cs, MainMenu.Designer.cs  – Main UI, listbox, tray integration
│   ├── Login.cs, Login.Designer.cs        – First-run registration
│   └── Add.cs, Add.Designer.cs            – Add new project
├── Helpers/
│   ├── FileHelper.cs       – File I/O in Documents/TimeTrackerData
│   ├── DatabaseManager.cs  – Business logic, tracking, projects
│   ├── ExcelExportHelper.cs – Excel timesheet generation (ClosedXML)
│   └── StartupHelper.cs    – Registry Run key for auto-start
├── Enum/
│   └── FolderEnum.cs       – Authentication, Projects, Trackings, DefaultProject
└── Program.cs              – Entry point, session events, auth check
```

---

## Data Model

### TrackingEntry
```csharp
{ ProjectName: string, Timestamp: DateTime }
```
- Stored as one JSON object per line in `Trackings.txt`
- Lines may contain concatenated JSON (e.g. `{...}{...}`); parser splits on `}{`

### FolderEnum Files
| Enum            | File                  | Content                          |
|-----------------|-----------------------|----------------------------------|
| Authentication  | Authentication.txt    | User name (single line)          |
| Projects        | Projects.txt          | One project name per line        |
| DefaultProject  | DefaultProject.txt    | Last selected project name       |
| Trackings       | Trackings.txt         | JSON lines per tracking entry    |

### Special Values
- `PauseProjectName = "Pause"` – Written when PC locks or session ends; excluded from export and project list

---

## Algorithms & Formulas

### 1. Session Duration (per project, per day)

A **session** starts when user switches TO a project and ends when the next tracking entry is written (any project, including Pause).

```
sessionEnd = nextTracking.Timestamp  if next exists
           = DateTime.Now           if this is the latest entry

duration = sessionEnd - sessionStart
```

### 2. Today's Timer (GetProjectInfo)

For a single project, today only:

```
allTrackings = GetAllTrackings() ordered by Timestamp
todayEntries = entries where Date == Today AND ProjectName == project

for each startEntry in todayEntries:
  nextEntry = first entry in allTrackings with Timestamp > startEntry.Timestamp
  if nextEntry exists:
    totalDuration += nextEntry.Timestamp - startEntry.Timestamp
  else:
    totalDuration += DateTime.Now - startEntry.Timestamp  // current session
```

### 3. Excel Export: Hours per Project per Date (GetAllProjectHoursByDate)

**Important:** Use the **full** timeline (including Pause) to find session end. Exclude Pause only for project list.

```
allTrackingsFull     = GetAllTrackings() ordered by Timestamp
trackingsExclPause   = allTrackingsFull where ProjectName != "Pause"
datesWithRecords     = distinct dates in allTrackingsFull

for each startEntry in trackingsExclPause:
  nextTracking = first in allTrackingsFull with Timestamp > startEntry.Timestamp
  sessionEnd   = nextTracking.Timestamp or DateTime.Now

  // Cap session at end of start day if gap > 1 day (prevents attributing time across weekends/app-off periods)
  if (sessionEnd.Date - start.Date).TotalDays > 1:
    sessionEnd = end of start.Date
    only attribute to start.Date

  for currentDate from start.Date to sessionEnd.Date:
    if currentDate NOT in datesWithRecords:
      skip  // Do NOT attribute hours to days with no records
    else:
      dayStart = currentDate 00:00:00
      dayEnd   = currentDate 23:59:59.9999999
      effectiveStart = max(start, dayStart)
      effectiveEnd   = min(sessionEnd, dayEnd)
      if effectiveStart < effectiveEnd:
        result[(project, currentDate)] += (effectiveEnd - effectiveStart).TotalHours

// Fill gaps: add 0 for (project, date) in full range
rangeFirstDate = min(timestamp.Date) in allTrackingsFull
rangeLastDate  = max(timestamp.Date) in allTrackingsFull
if last entry has no next and is not Pause:
  rangeLastDate = max(rangeLastDate, DateTime.Today)

for d in [rangeFirstDate .. rangeLastDate]:
  for each project:
    if (project, d) not in result:
      result[(project, d)] = 0
```

### 4. Trackings File Parsing (Concatenated JSON)

Lines may contain multiple JSON objects (e.g. `{"ProjectName":"A",...}{"ProjectName":"B",...}`):

```
split line by "}{"
for i = 0 to parts.Length - 1:
  json = (i == 0) ? parts[i] + "}" : "{" + parts[i]
  if parts.Length > 1 and 0 < i < last: json = "{" + parts[i] + "}"
  deserialize json to TrackingEntry, add to list
```

### 5. TrackProject (Write New Entry)

```
if lastTrackedProject != newProject:
  append JSON line to Trackings.txt
```

### 6. Auto-Start (StartupHelper)

- **Registry:** `HKCU\Software\Microsoft\Windows\CurrentVersion\Run`
- **Value name:** `"TimeTracker"`
- **Value:** `Application.ExecutablePath`
- Enable: `SetValue(ValueName, path)`
- Disable: `DeleteValue(ValueName)`

---

## Key Features Implemented

1. **Login**
   - First run: prompt for name, save to Authentication.txt
   - Subsequent runs: skip login, open MainMenu

2. **Project Switching**
   - Select project in listbox or tray → `TrackProject` + `SetDefaultProject` + `UpdateTimer`

3. **Session Awareness (Program.cs)**
   - `SessionLock` → `TrackProject("Pause")`
   - `SessionUnlock` → `TrackProject(GetDefaultProject())`
   - `SessionEnding` → `TrackProject("Pause")`

4. **System Tray**
   - NotifyIcon with context menu: projects (checked = default), Show, Start with Windows, Exit
   - Double-click tray → restore window
   - Minimize button → Hide + ShowInTaskbar=false
   - Form Resize (Minimized) → same behavior

5. **Excel Export**
   - SaveFileDialog → ExcelExportHelper.ExportToExcel
   - Projects as rows, dates as columns (`d-MMM`), hours to 2 decimals
   - Header: grey background, bold. Column A: bold. Hours: `0.00` format.

6. **Auto-Start with Windows**
   - Tray menu toggle; uses StartupHelper (Registry Run key)

---

## UI Theme

| Element      | Value                        |
|-------------|------------------------------|
| Background  | `#0f172a` (Navy)             |
| Panels/List | `#1e293b` (Slate)            |
| Selected    | `#0ea5e9` (Sky blue)         |
| Text        | `#f1f5f9`                    |

---

## Development Conventions

- **File Access:** Use `FileHelper` and `FolderEnum`
- **Forms:** `FormBorderStyle.None`, draggable, `CenterScreen`
- **Dependencies:** ClosedXML for Excel

---

## Recent Changes (2026-03)

- System tray: NotifyIcon, context menu, minimize-to-tray, double-click restore
- Auto-start: StartupHelper, Registry Run key
- Excel export: ExcelExportHelper, GetAllProjectHoursByDate
- Trackings parser: handle concatenated JSON on same line
- Hour calculation: use full timeline (incl. Pause) for session end; only attribute hours to dates with records
- Excel: show 0 for dates in range with no records (no column removal)
