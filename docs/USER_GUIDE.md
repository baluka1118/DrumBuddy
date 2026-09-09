# DrumBuddy User Guide

A practical guide to installing, configuring, and using DrumBuddy — including recording, the library, manual editing, MIDI playback, and hearing your sheets in external apps such as GarageBand on macOS.

---

## Table of Contents

1. [[#What is DrumBuddy?]]
2. [[#System Requirements]]
3. [[#Installation]]
4. [[#First Launch: Account vs Guest Mode]]
5. [[#App Overview]]
6. [[#MIDI Input: Electronic Drums & Keyboard Mode]]
7. [[#Configuration]]
8. [[#Recording Sheets]]
9. [[#Manual Editor]]
10. [[#Library]]
11. [[#Playing Sheets via MIDI Output]]
12. [[#Hearing Sheets in GarageBand (macOS)]]
13. [[#Comparing Sheets]]
14. [[#Import & Export]]
15. [[#Cloud Sync]]
16. [[#Where Your Data Is Stored]]
17. [[#Building from Source (Developers)]]
18. [[#Troubleshooting]]
19. [[#Known Limitations]]

---

## What is DrumBuddy?

DrumBuddy is a cross-platform desktop app for drummers. It lets you:

- **Record** beats from an electronic drum kit (or keyboard) onto a digital sheet
- **Create** sheets manually in a step-based editor
- **Store** sheets in a local library
- **Compare** two sheets to see timing and note differences
- **Export** to JSON, MIDI, MusicXML, or PDF
- **Play** sheets to an external MIDI output (for example, GarageBand via the macOS IAC Driver)
- **Sync** sheets to the cloud when signed in with an account

> **Note:** DrumBuddy does not include built-in drum sample playback. During recording you hear a **metronome** only. To hear actual drum sounds, route MIDI output to a DAW or drum module (see [[#Playing Sheets via MIDI Output]]).

**Current rhythm limitations:** 4/4 time signature only; smallest note division is a **16th note**.

---

## System Requirements

| Platform | Requirement |
|----------|-------------|
| **Windows** | Windows 10 or later (x64) |
| **Linux** | Modern distribution with AppImage support (x64) |
| **macOS** | macOS 11+; Apple Silicon (`arm64`) or Intel (`x64`) |
| **Optional** | Electronic drum kit with USB MIDI, or a computer keyboard for keyboard mode |
| **Optional** | macOS IAC Driver (for MIDI output to GarageBand or other apps) |

For **building from source**, you need the [.NET 9 SDK](https://dotnet.microsoft.com/download).

---

## Installation

### Windows

1. Download the latest **`DrumBuddy-Setup-x.y.z.exe`** from [GitHub Releases](https://github.com/sz-balage/DrumBuddy/releases).
2. Run the installer and follow the setup wizard.
3. Launch DrumBuddy from the Start Menu or desktop shortcut.

> Windows SmartScreen may warn about an unknown publisher. Choose **More info → Run anyway** if you trust the download source.

### Linux

1. Download **`DrumBuddy-linux-x64.AppImage`** from [GitHub Releases](https://github.com/sz-balage/DrumBuddy/releases).
2. Make it executable:
   ```bash
   chmod +x DrumBuddy-linux-x64.AppImage
   ```
3. Run it:
   ```bash
   ./DrumBuddy-linux-x64.AppImage
   ```

### macOS (pre-built release)

Because DrumBuddy is not distributed through the Mac App Store, you may need to remove quarantine attributes before the first launch.

#### Apple Silicon (M-series)

1. Download **`DrumBuddy-osx-arm64.dmg`** from [GitHub Releases](https://github.com/sz-balage/DrumBuddy/releases).
2. Open the disk image and drag **DrumBuddy.app** into **Applications**.
3. In Terminal:
   ```bash
   cd /Applications
   sudo xattr -cr DrumBuddy.app
   sudo xattr -rd com.apple.quarantine DrumBuddy.app
   sudo codesign --force --deep --sign - DrumBuddy.app
   ```
4. Launch DrumBuddy from Applications.

#### Intel Macs

Download **`DrumBuddy-osx-x64.dmg`** and follow the same quarantine steps above.

> On first launch, macOS may ask for permission to access folders used for exports and user data.

---

## First Launch: Account vs Guest Mode

When DrumBuddy starts, you see the sign-in screen. You have two options:

### Sign in with an account

- Enables **cloud sync** for sheets marked for sync
- Requires network access to the DrumBuddy API
- Your username appears in the app header when connected

### Continue as a guest (offline mode)

- Click **Continue as a guest (offline mode)**
- All sheets are stored **locally only**
- Cloud sync options are hidden
- Ideal for trying the app or working without an account

Guest mode is fully functional for recording, editing, exporting, and MIDI playback. Only server sync requires an account.

---

## App Overview

After sign-in (or guest login), use the **navigation pane** on the left:

| Section | Purpose |
|---------|---------|
| **Home** | Quick links to main features |
| **Record** | Record a new sheet from MIDI or keyboard input |
| **Library** | Browse, edit, export, compare, and play saved sheets |
| **Manual** | Open the step-based manual editor |
| **Configuration** | Drum MIDI mapping, metronome volume, input mode, theme |

The header shows your username (or **Guest**) and indicates whether **MIDI** or **Keyboard** input mode is active.

If no MIDI device is connected and keyboard mode is off, a banner reminds you to connect a device or enable keyboard mode in Configuration.

---

## MIDI Input: Electronic Drums & Keyboard Mode

### Connecting an electronic drum kit

1. Connect your drum module to the computer via USB (or a MIDI interface).
2. Power on the module.
3. On first use, DrumBuddy prompts you to **choose a MIDI input device** if more than one is available.
4. Your selection is remembered for future sessions.

If the connection drops, a **No connection** banner appears. Reconnect the device and use **Choose again** in Configuration → Settings → Input Settings.

### Keyboard mode (no drum kit required)

1. Open **Configuration** → **Settings** tab.
2. Under **Input Settings**, toggle **Keyboard Mode** on (MIDI Mode off).
3. Return to **Record** or use editing dialogs — hit the mapped keys on your computer keyboard instead of pads.

Keyboard mappings are shown in Configuration when keyboard mode is enabled. Use **Revert keyboard mappings** to restore defaults.

### Remapping drums to MIDI notes

1. Open **Configuration** → **Drum Mapping** tab.
2. Click **Remap** next to a drum (for example, Snare).
3. Hit the pad or key you want to assign.
4. Repeat for each drum. Unmapped drums are highlighted.

Supported drums: Kick, Snare, Tom1, Tom2, Floor Tom, Hi-Hat (closed/open/pedal), Ride, Crash1, Crash2.

---

## Configuration

Open **Configuration** from the navigation pane.

### Drum Mapping tab

- View and remap each drum to a MIDI note (or keyboard key in keyboard mode)
- **Revert to default mappings** restores factory mapping
- While remapping, the header shows which drum is being learned

### Settings tab

| Setting | Description |
|---------|-------------|
| **Metronome Volume** | Loudness of click during recording (0–10000) |
| **Input Mode** | Toggle between MIDI Mode and Keyboard Mode |
| **Choose again** | Re-run MIDI input device selection |
| **Theme** | Light, Dark, or System |

Settings are saved automatically.

---

## Recording Sheets

1. Open **Record** from the navigation pane.
2. Set **BPM** (tempo) with the numeric control.
3. Optionally load an existing sheet as a **reference overlay** (if available in the sheet picker).
4. Click **Record** to start.
   - A short countdown plays before recording begins
   - The metronome clicks in time with the BPM
   - Play your pattern on the kit or keyboard
5. Use **Pause** / **Resume** as needed.
6. Click **Stop** when finished.
7. Save the sheet with a name and description.

While recording, hits are written into measures on the digital sheet in real time. You can scroll through measures as you play.

> Recording requires either a connected MIDI device or keyboard mode enabled.

---

## Manual Editor

The manual editor builds sheets measure-by-measure on a grid.

### Opening the editor

- From **Manual** in the navigation pane — create a new sheet
- From **Library** — use **Manual edit** on a selected sheet

### Grid layout

- **Rows** = drum voices (kick, snare, toms, cymbals, etc.)
- **Columns** = 16th-note steps within the current measure
- Toggle cells on/off to place notes

### Toolbar actions

| Action | Description |
|--------|-------------|
| **BPM** | Set tempo for the sheet |
| **Add measure** | Append a new measure |
| **Back / Forward** | Navigate between measures |
| **Save** | Persist changes to the library |
| **Play MIDI** | Send the current sheet to the MIDI output device (see below) |

While MIDI is playing, BPM editing is disabled. Click **Stop** (same button) to cancel playback.

---

## Library

The library lists all sheets stored on this device.

### Per-sheet actions

| Action | Description |
|--------|-------------|
| **View & PDF export** | Open sheet viewer; export as PDF |
| **Compare** | Compare this sheet against another |
| **Play MIDI** | Play the sheet through your MIDI output device |
| **Export to** | JSON, MIDI file, or MusicXML |
| **Turn on/off sync** | (Account only) Sync sheet with the server |
| **Context menu** | Rename, edit metadata, duplicate, delete, manual edit, etc. |

### Batch operations

Select multiple sheets to **delete** or **export** in bulk.

### Sorting and filtering

Use the sort dropdown (name, tempo, length) and the search box to filter by name or description.

### Play MIDI from the library

1. Click **Play MIDI** on a sheet row.
2. If no MIDI output device is configured, a device chooser appears (see [[#Playing Sheets via MIDI Output]]).
3. While playing, the button changes to **Stop**.
4. Playback stops automatically when the sheet ends, or when you click **Stop**, navigate away, or open Record/Manual.

---

## Playing Sheets via MIDI Output

DrumBuddy can send your sheet as **live MIDI** to an output device. This is how you hear drum sounds in GarageBand, Logic, or a hardware drum module.

### How it works

- Notes are sent on **MIDI channel 10** (General MIDI drums)
- Note numbers match standard GM drum mapping (kick = 36, snare = 38, etc.)
- The same MIDI data is used for **Export to MIDI file**

### Where to play from

- **Manual Editor** — **Play MIDI** button in the toolbar
- **Library** — **Play MIDI** on each sheet row

### Choosing an output device

On first playback (or when multiple outputs exist), DrumBuddy shows a **MIDI output device chooser**. Your choice is saved as the last-used output device.

On macOS, select **IAC Driver Bus 1** (or similar) to route MIDI into GarageBand or another DAW.

If no output devices appear:

1. Open **Audio MIDI Setup** (macOS) or ensure your MIDI interface is connected
2. Enable the **IAC Driver** (macOS — see next section)
3. Restart DrumBuddy and try again

---

## Hearing Sheets in GarageBand (macOS)

GarageBand does not open MIDI files for live playback the way a sequencer does. The recommended workflow is **live MIDI routing** from DrumBuddy into an armed Software Instrument track.

### Step 1: Enable the IAC Driver

1. Open **Audio MIDI Setup** (Spotlight → “Audio MIDI Setup”)
2. Menu: **Window → Show MIDI Studio**
3. Double-click **IAC Driver**
4. Check **Device is online**
5. Ensure at least one port exists (default: **Bus 1**)

> This is configured in **Audio MIDI Setup**, not in GarageBand’s preferences.

### Step 2: Configure GarageBand

1. Open GarageBand and create or open a project
2. Add a **Software Instrument** track (green icon) — use a drum kit sound
   - Avoid yellow **Drummer** tracks; they do not accept external MIDI the same way
3. Show **Record Enable** on track headers:
   - **Track → Configure Track Header → Record Enable**
4. Click the **Record Enable** button (red circle) on the drum Software Instrument track to **arm** it

GarageBand routes external MIDI to the **armed** Software Instrument track. There is no per-track “MIDI from IAC” setting — arming the track is what matters.

### Step 3: Play from DrumBuddy

1. In DrumBuddy, open **Library** or the **Manual Editor**
2. Click **Play MIDI**
3. Select **IAC Driver Bus 1** if prompted
4. You should hear the drum kit on the armed GarageBand track

### Tips

- Only one sheet plays at a time; starting a new playback stops the previous one
- If you hear nothing, confirm the IAC Driver is online, the track is armed, and the instrument is a drum kit
- MIDI volume follows the receiving instrument in GarageBand, not DrumBuddy

---

## Comparing Sheets

Comparing helps you see how closely a performance matches a reference pattern.

1. In **Library**, select a sheet
2. Click **Compare**
3. Choose a second sheet as the comparison target
4. Review side-by-side notation; differences are highlighted

Typical workflow: use a **base sheet** (the target groove) and a **played sheet** (your recording).

---

## Import & Export

### Export formats

| Format | Use case |
|--------|----------|
| **JSON** | Full DrumBuddy sheet; best for backup or re-import |
| **MIDI** | Use in other DAWs as a MIDI file (not live routing) |
| **MusicXML** | Exchange with notation software |
| **PDF** | Printable sheet music (via View & PDF export) |

Export from the library row **Export to** menu or from the sheet viewer.

### Import

Use **Import** on the library toolbar to load JSON sheets exported from DrumBuddy. If a name collision occurs, you can overwrite or cancel.

---

## Cloud Sync

Available only when **signed in** (not in guest mode).

- **Turn on sync** uploads a sheet to the server
- **Turn off sync** removes it from the server but keeps the local copy
- Sync status icons on each row indicate cloud state and in-progress sync

Guest users store everything locally with no sync UI.

---

## Where Your Data Is Stored

DrumBuddy uses a local **SQLite** database for sheets and configuration.

| OS | Path |
|----|------|
| **macOS** | `~/Library/Application Support/DrumBuddy/sheet_db.db` |
| **Windows** | `%APPDATA%\DrumBuddy\sheet_db.db` |
| **Linux** | `~/.config/DrumBuddy/sheet_db.db` (or XDG equivalent under Application Data) |

The database is created automatically on first launch. Back up this file to preserve your library.

---

## Building from Source (Developers)

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- On macOS with Homebrew: `brew install dotnet`

### Clone and run (macOS Apple Silicon example)

```bash
git clone https://github.com/sz-balage/DrumBuddy.git
cd DrumBuddy

# If using Homebrew dotnet on Apple Silicon:
export DOTNET_ROOT="/opt/homebrew/opt/dotnet/libexec"

dotnet run --project DrumBuddy.Desktop/DrumBuddy.Desktop.csproj -c Debug -r osx-arm64
```

### Other runtimes

| Platform | `-r` runtime identifier |
|----------|-------------------------|
| macOS Intel | `osx-x64` |
| Windows | `win-x64` |
| Linux | `linux-x64` |

### Full stack (API + database)

To run the server and PostgreSQL locally, use `docker-compose-dev.yml` in the repository root. The desktop app works in guest mode without Docker.

---

## Troubleshooting

### No MIDI input devices found

- Check USB cable and drum module power
- Confirm the module appears in your OS MIDI settings
- In DrumBuddy: **Configuration → Settings → Choose again**
- Or enable **Keyboard Mode** to test without a kit

### No MIDI output devices found

- **macOS:** Enable IAC Driver in Audio MIDI Setup → MIDI Studio
- Reconnect USB MIDI interfaces
- Restart DrumBuddy after enabling devices

### GarageBand: no sound when playing MIDI

- Verify **IAC Driver** is online (Device is online checked)
- Use a **Software Instrument** track with a drum sound, not a Drummer track
- **Arm** the track (Record Enable)
- In DrumBuddy, confirm you selected IAC Bus as the output device

### App blocked on macOS (“damaged” or won’t open)

Run the quarantine removal commands in [[#macOS (pre-built release)]].

### Playback stops or button stuck after playing

Ensure you are on a recent build. Playback completion is handled on the UI thread; if issues persist, click **Stop** or switch pages to reset state.

### Guest sheets not syncing

Expected behavior. Sign in with an account and use **Turn on sync** per sheet.

### SmartScreen / security warnings (Windows)

The distributed builds may not be code-signed with a commercial certificate. Download only from the official GitHub releases page.

---

## Known Limitations

- **4/4 time signature only**
- **16th notes** are the smallest division (no triplets, 32nds, etc. in the editor/recorder)
- **No built-in drum audio** — metronome only during recording; use MIDI output or export for drum sounds
- **MIDI output** uses General MIDI drum mapping on channel 10
- macOS release builds require manual quarantine removal (no paid Apple Developer ID)

---

## Further Help

- [Official documentation](https://docs.drumbuddy.hu/)
- [GitHub Issues](https://github.com/sz-balage/DrumBuddy/issues) — bugs and feature requests
- Contact: Szabó Balázs — szabobazsi11182@gmail.com

---

*This guide covers DrumBuddy desktop features including local MIDI playback enhancements. For the latest release notes, see [GitHub Releases](https://github.com/sz-balage/DrumBuddy/releases).*
