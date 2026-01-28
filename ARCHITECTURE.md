# System Architecture

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                     MOBILE PUZZLE GAME                       │
│                     Unity C# Prototype                       │
└─────────────────────────────────────────────────────────────┘

┌──────────────── PRESENTATION LAYER ────────────────────────┐
│                                                              │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│  │  MainMenu    │  │  LevelMap    │  │ PuzzleGame   │     │
│  │   Scene      │  │    Scene     │  │    Scene     │     │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘     │
│         │                  │                  │              │
│  ┌──────▼───────┐  ┌──────▼───────┐  ┌──────▼───────┐     │
│  │ MainMenuUI   │  │ LevelMapUI   │  │  PuzzleUI    │     │
│  │              │  │              │  │              │     │
│  │ - Start      │  │ - Scroll     │  │ - Timer      │     │
│  │ - Settings   │  │ - Buttons    │  │ - Complete   │     │
│  │ - Exit       │  │ - Unlock     │  │ - Results    │     │
│  └──────────────┘  └──────┬───────┘  └──────────────┘     │
│                            │                                 │
│                     ┌──────▼───────┐                        │
│                     │ LevelButton  │                        │
│                     │  Component   │                        │
│                     └──────────────┘                        │
│                                                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────── GAME LOGIC LAYER ──────────────────────────┐
│                                                              │
│  ┌──────────────────────────────────────────────────┐      │
│  │           PuzzleManager (Game Controller)        │      │
│  │                                                   │      │
│  │  - Swap Logic         - Timer System             │      │
│  │  - Win Detection      - Level Flow               │      │
│  │  - Shuffle Algorithm  - State Management         │      │
│  └──────────────┬───────────────┬───────────────────┘      │
│                 │               │                           │
│         ┌───────▼──────┐  ┌────▼──────┐                   │
│         │ PuzzleGrid   │  │PuzzlePiece│                   │
│         │              │  │           │                   │
│         │ - Create     │  │ - Click   │                   │
│         │ - Layout     │  │ - Swap    │                   │
│         │ - Cleanup    │  │ - Visual  │                   │
│         └──────────────┘  └───────────┘                   │
│                                                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────── CORE SYSTEMS LAYER ────────────────────────┐
│                                                              │
│  ┌─────────────────────┐      ┌──────────────────────┐    │
│  │   GameManager       │      │   SceneLoader        │    │
│  │   (Singleton)       │      │   (Singleton)        │    │
│  │                     │      │                      │    │
│  │ - Database Access   │      │ - Scene Navigation   │    │
│  │ - Current Level     │      │ - Load Scenes        │    │
│  │ - Best Times        │      │ - Quit Game          │    │
│  │ - Photo Retrieval   │      │                      │    │
│  └──────────┬──────────┘      └──────────────────────┘    │
│             │                                               │
│             │                                               │
└─────────────┼───────────────────────────────────────────────┘
              │
┌─────────────▼─── DATA LAYER ────────────────────────────────┐
│                                                              │
│  ┌──────────────────┐         ┌──────────────────┐         │
│  │  PhotoDatabase   │         │  LevelDatabase   │         │
│  │ (ScriptableObj)  │         │ (ScriptableObj)  │         │
│  │                  │         │                  │         │
│  │ - Photo List     │         │ - Level List     │         │
│  │ - Get by ID      │         │ - Get by ID      │         │
│  │ - Refresh        │         │ - Add Level      │         │
│  └────────┬─────────┘         └────────┬─────────┘         │
│           │                            │                    │
│    ┌──────▼─────────┐          ┌──────▼─────────┐         │
│    │   PhotoData    │          │   LevelData    │         │
│    │                │          │                │         │
│    │ - ID           │          │ - Level ID     │         │
│    │ - Name         │          │ - Grid Size    │         │
│    │ - Sprite       │          │ - Photo ID     │         │
│    └────────────────┘          └────────────────┘         │
│                                                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────── PERSISTENCE LAYER ─────────────────────────┐
│                                                              │
│  ┌──────────────────────────────────────────────────┐      │
│  │              PlayerPrefs                          │      │
│  │                                                   │      │
│  │  Key: "Level_{id}_BestTime"                      │      │
│  │  Value: float (seconds)                          │      │
│  │                                                   │      │
│  │  - Save best time per level                      │      │
│  │  - Track completion                              │      │
│  │  - Unlock system data                            │      │
│  └──────────────────────────────────────────────────┘      │
│                                                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────── EDITOR TOOLS LAYER ────────────────────────┐
│  (Unity Editor Only)                                        │
│                                                              │
│  ┌──────────────────┐  ┌──────────────────┐               │
│  │PhotoDatabase     │  │LevelDatabase     │               │
│  │Editor            │  │Editor            │               │
│  │                  │  │                  │               │
│  │- Refresh Button  │  │- Add Level Button│               │
│  │- Auto-load       │  │- Create Assets   │               │
│  └──────────────────┘  └──────────────────┘               │
│                                                              │
│  ┌──────────────────────────────────────────┐              │
│  │     LevelDataEditor                      │              │
│  │                                          │              │
│  │ - Grid Size Dropdown                    │              │
│  │ - Photo Selection Dropdown              │              │
│  │ - Photo Preview                         │              │
│  │ - Validation                            │              │
│  └──────────────────────────────────────────┘              │
│                                                              │
└──────────────────────────────────────────────────────────────┘
```

## Data Flow Diagrams

### Level Selection Flow
```
User               LevelMapUI         GameManager       SceneLoader
  │                    │                   │                 │
  │  Click Level 1     │                   │                 │
  ├──────────────────► │                   │                 │
  │                    │  SetCurrentLevel  │                 │
  │                    ├──────────────────►│                 │
  │                    │                   │  LoadPuzzleGame │
  │                    │                   ├────────────────►│
  │                    │                   │                 │
  │                    │    Load Scene     │                 │
  │                    │◄──────────────────┴─────────────────┘
  │                    │                                     
  │   PuzzleGame Scene Opens                                
  └────────────────────┘                                     
```

### Puzzle Swap Flow
```
User          PuzzlePiece    PuzzleManager    PuzzleGrid
  │                │              │                │
  │  Tap Piece A   │              │                │
  ├───────────────►│              │                │
  │                │ OnClicked    │                │
  │                ├─────────────►│                │
  │                │              │ (Select A)     │
  │                │              │                │
  │  Tap Piece B   │              │                │
  ├───────────────►│              │                │
  │                │ OnClicked    │                │
  │                ├─────────────►│                │
  │                │              │ SwapPieces     │
  │                │              │ (A ↔ B)        │
  │                │              │                │
  │                │              │ CheckWin       │
  │                │              ├───────────────►│
  │                │              │◄───────────────┤
  │                │              │ (All correct?) │
  │                │              │                │
  │      If Win:   │              │                │
  │   Show Panel   │              │                │
  │◄───────────────┴──────────────┘                │
```

### Database Workflow
```
Developer         PhotoDatabase      LevelDatabase     LevelData
    │                  │                   │               │
    │  Add images to   │                   │               │
    │ Resources/Puzzle │                   │               │
    │     Images       │                   │               │
    ├─────────────────►│                   │               │
    │                  │                   │               │
    │ Click "Refresh   │                   │               │
    │    Images"       │                   │               │
    ├─────────────────►│                   │               │
    │                  │ Load All Sprites  │               │
    │                  │ Create PhotoData  │               │
    │◄─────────────────┤ for each          │               │
    │  (Photos Loaded) │                   │               │
    │                  │                   │               │
    │ Click "Add New   │                   │               │
    │     Level"       │                   │               │
    ├──────────────────┴──────────────────►│               │
    │                                       │ Create Asset  │
    │                                       ├──────────────►│
    │                                       │               │
    │ Configure Level  │                   │               │
    ├──────────────────┴──────────────────►│               │
    │  - Grid Size     │                   │               │
    │  - Photo         │                   │               │
    │                  │                   │               │
    │ Level Ready!     │                   │               │
    └──────────────────┴───────────────────┴───────────────┘
```

## Component Responsibilities

### GameManager (Singleton)
- **Lifetime**: Entire game session (DontDestroyOnLoad)
- **Responsibilities**:
  - Load and cache databases
  - Track current level selection
  - Provide photo sprites by ID
  - Manage best times (PlayerPrefs)
  - Level unlock logic

### SceneLoader (Singleton)
- **Lifetime**: Entire game session (DontDestroyOnLoad)
- **Responsibilities**:
  - Load scenes by name
  - Convenience methods (LoadMainMenu, etc.)
  - Scene reloading
  - Application quit (platform-aware)

### PuzzleManager
- **Lifetime**: PuzzleGame scene only
- **Responsibilities**:
  - Initialize puzzle from level data
  - Handle piece selection and swapping
  - Manage timer
  - Detect win condition
  - Save completion time
  - Navigate to next level/restart

### PuzzleGrid
- **Lifetime**: PuzzleGame scene only
- **Responsibilities**:
  - Create grid layout dynamically
  - Slice photo into pieces (sprites)
  - Calculate cell sizes
  - Manage piece collection
  - Clean up resources (prevent memory leaks)

### PuzzlePiece
- **Lifetime**: PuzzleGame scene only
- **Responsibilities**:
  - Display piece sprite
  - Handle click events
  - Show highlight when selected
  - Track current vs correct position

## Key Design Decisions

### Why ScriptableObjects?
- ✅ **Editor-friendly**: Easy to create and edit in Inspector
- ✅ **Persistent**: Data survives scene changes
- ✅ **Reusable**: Can be referenced by multiple objects
- ✅ **Version control friendly**: Text-based YAML format

### Why Singletons?
- ✅ **Global access**: GameManager and SceneLoader need to be accessible from anywhere
- ✅ **Persistence**: Should survive scene transitions
- ✅ **Single instance**: Only one instance should exist

### Why Component-Based?
- ✅ **Modularity**: Each component has clear responsibility
- ✅ **Reusability**: Components can be reused in different contexts
- ✅ **Testability**: Easier to test individual components
- ✅ **Maintainability**: Changes are localized

### Why PlayerPrefs for Persistence?
- ✅ **Simple**: Easy to use API
- ✅ **Cross-platform**: Works on all Unity platforms
- ✅ **Sufficient**: Perfect for simple data like best times
- ✅ **No setup required**: Works out of the box

## Extension Points

### Adding New Features

**New Grid Sizes**:
- Edit `LevelDataEditor.cs` → `gridSizeOptions` array
- Add new preset (e.g., "7x7", "8x8")

**New Photo Sources**:
- Extend `PhotoDatabase.cs`
- Add methods for runtime loading
- Could load from web, device storage, etc.

**Sound Effects**:
- Add `AudioSource` to managers
- Play sounds on swap, completion, etc.
- Add volume controls to Settings

**Power-ups/Hints**:
- Extend `PuzzleManager.cs`
- Add hint system (show correct position)
- Add shuffle reduction, etc.

**Multiple Worlds/Themes**:
- Create `WorldData.cs` ScriptableObject
- Group levels by world
- Different visual themes per world

**Online Leaderboards**:
- Replace PlayerPrefs with online backend
- Add user authentication
- Compare times globally

This architecture provides a solid foundation for all these extensions and more!
