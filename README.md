# Mobile Puzzle Game Prototype

Unity-based mobile puzzle game with swap mechanics. Players solve puzzles by swapping image pieces to recreate the original picture.

## Features

- **Main Menu**: Start game, access settings, exit
- **Level Map**: Linear level progression with unlock system
- **Puzzle Gameplay**: Grid-based puzzle with swap mechanics
- **Timer System**: Track completion time for each level
- **Best Time Tracking**: Save and display best times using PlayerPrefs
- **Editor Tools**: Custom editors for managing photos and levels

## Project Structure

```
Assets/
├── Scenes/              # Unity scenes (created in Unity Editor)
│   ├── MainMenu.unity
│   ├── LevelMap.unity
│   └── PuzzleGame.unity
├── Scripts/
│   ├── Data/           # ScriptableObject data classes
│   ├── Editor/         # Custom Unity editor scripts
│   ├── Managers/       # Game and scene managers
│   ├── Puzzle/         # Puzzle game logic
│   └── UI/             # UI controllers
├── Resources/
│   ├── PuzzleImages/   # Puzzle photos (sprites)
│   └── Data/           # ScriptableObject assets
│       ├── PhotoDatabase.asset
│       ├── LevelDatabase.asset
│       └── Levels/     # Individual level data assets
└── Prefabs/
    ├── UI/             # UI prefabs (created in Unity Editor)
    └── Puzzle/         # Puzzle piece prefabs
```

## Setup Instructions

### 1. Open in Unity Editor
- Unity version: 6000.3.5f2 or compatible
- Open the project in Unity Editor

### 2. Setup Photo Database
1. Navigate to `Assets/Resources/Data/PhotoDatabase`
2. In Inspector, click "Refresh Images" to load all images from `Resources/PuzzleImages`

### 3. Create Levels
1. Navigate to `Assets/Resources/Data/LevelDatabase`
2. Click "Add New Level" button to create level data
3. Select the created level asset
4. Configure:
   - Grid size (3x3, 4x4, 5x4, 6x4, 6x5, etc.)
   - Select a photo from the dropdown
   - Level ID is auto-assigned

### 4. Create Scenes (In Unity Editor)
You need to manually create these scenes in Unity:

#### MainMenu Scene
- Canvas with MainMenuUI component
- 3 Buttons: Start, Settings, Exit
- Settings Panel (initially hidden)

#### LevelMap Scene
- Canvas with LevelMapUI component
- ScrollRect with Vertical Layout Group
- Level button prefab with LevelButton component
- Back button

#### PuzzleGame Scene
- Canvas with PuzzleUI component
- PuzzleGrid with GridLayoutGroup
- Timer text (TextMeshPro)
- Completion panel with results
- PuzzleManager in the scene

### 5. Add Scenes to Build Settings
- File > Build Settings
- Add MainMenu, LevelMap, and PuzzleGame scenes

## How to Play

1. Start from Main Menu
2. Click "Start" to open Level Map
3. Select an unlocked level
4. Tap two pieces to swap them
5. Complete the puzzle to unlock next level
6. Try to beat your best time!

## Development Notes

- **Mobile Optimization**: Uses Canvas Scaler for screen adaptation
- **Touch Input**: Button-based input system compatible with mobile
- **Solvable Puzzles**: Shuffle algorithm ensures all puzzles are solvable
- **Linear Progression**: Levels unlock sequentially
- **Persistent Data**: Best times saved with PlayerPrefs

## Adding New Features

### Adding New Photos
1. Add image files to `Assets/Resources/PuzzleImages/`
2. Select `PhotoDatabase` asset
3. Click "Refresh Images" button

### Creating New Levels
1. Select `LevelDatabase` asset
2. Click "Add New Level"
3. Configure the new level data

### Extending Grid Sizes
Edit `LevelDataEditor.cs` to add new grid size presets to the `gridSizeOptions` array.

## Technical Details

- **Data Management**: ScriptableObject pattern for data persistence
- **Scene Management**: Singleton SceneLoader for transitions
- **Game State**: Singleton GameManager for global state
- **Win Detection**: Checks if all pieces match their correct positions
- **Animation**: Lerp-based smooth piece swapping

## License

See LICENSE file for details.
