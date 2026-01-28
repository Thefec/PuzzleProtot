# Unity Scene Setup Guide

This guide will help you create the three required scenes for the puzzle game.

## Prerequisites
- Unity Editor 6000.3.5f2 or compatible
- TextMeshPro package installed (should be included by default)

---

## Scene 1: MainMenu

### Setup Steps:
1. **Create Scene**
   - File > New Scene
   - Save as `Assets/Scenes/MainMenu.unity`

2. **Create Canvas**
   - Right-click in Hierarchy > UI > Canvas
   - Set Canvas Scaler:
     - UI Scale Mode: Scale With Screen Size
     - Reference Resolution: 1080 x 1920 (portrait)
     - Match: 0.5

3. **Add MainMenuUI Component**
   - Add `MainMenuUI` script to Canvas

4. **Create Buttons**
   - Create Panel (optional, for background)
   - Add three buttons:
     - **Start Button**: "START GAME"
     - **Settings Button**: "SETTINGS"
     - **Exit Button**: "EXIT"
   - Arrange vertically in center

5. **Create Settings Panel**
   - Create Panel under Canvas
   - Add background Image (semi-transparent)
   - Add "Close" button
   - Initially set inactive (uncheck in Inspector)

6. **Link Components**
   - Select Canvas
   - In MainMenuUI component, assign:
     - Start Button
     - Settings Button
     - Exit Button
     - Settings Panel

7. **Add GameManager**
   - Create Empty GameObject: "GameManager"
   - Add `GameManager` component
   - Assign PhotoDatabase (from Resources/Data/)
   - Assign LevelDatabase (from Resources/Data/)

---

## Scene 2: LevelMap

### Setup Steps:
1. **Create Scene**
   - File > New Scene
   - Save as `Assets/Scenes/LevelMap.unity`

2. **Create Canvas**
   - Same settings as MainMenu
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1080 x 1920

3. **Add Title**
   - Add TextMeshPro text at top: "SELECT LEVEL"

4. **Create Scroll View**
   - Right-click Canvas > UI > Scroll View
   - Configure:
     - Anchor: Stretch both ways
     - Leave space for title and back button
     - Vertical scrolling only
     - Content: Vertical Layout Group
       - Spacing: 10
       - Child Force Expand: Width
       - Padding: 20

5. **Create Level Button Prefab**
   - Create Button in Scene (not in ScrollView yet)
   - Add these components:
     - Image (background)
     - TextMeshPro for level number
     - TextMeshPro for best time
     - Image for lock icon (child object)
   - Add `LevelButton` script
   - Assign all text and image references
   - Drag to `Assets/Prefabs/UI/` to create prefab
   - Delete from scene

6. **Add Back Button**
   - Create Button at top-left: "< BACK"

7. **Add LevelMapUI Component**
   - Create Empty GameObject: "LevelMapManager"
   - Add `LevelMapUI` component
   - Assign:
     - Level Button Prefab
     - Level Button Container (Content of ScrollView)
     - Back Button
     - Scroll Rect component

---

## Scene 3: PuzzleGame

### Setup Steps:
1. **Create Scene**
   - File > New Scene
   - Save as `Assets/Scenes/PuzzleGame.unity`

2. **Create Canvas**
   - Same settings as other scenes
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1080 x 1920

3. **Create Puzzle Grid Container**
   - Create Panel: "PuzzleGridPanel"
   - Anchor: Center, with margins
   - Add `GridLayoutGroup` component:
     - Cell Size: Will be set dynamically
     - Spacing: 5
     - Child Alignment: Middle Center
   - Add `PuzzleGrid` component

4. **Create Puzzle Piece Prefab**
   - Create Image in scene
   - Add these components:
     - Image (for piece sprite)
     - Image (for highlight border) - different color, larger
     - Button component
     - `PuzzlePiece` script
   - Assign references in PuzzlePiece
   - Drag to `Assets/Prefabs/Puzzle/` to create prefab
   - Delete from scene

5. **Create Timer UI**
   - Create TextMeshPro text at top: "00:00"
   - Style: Large, bold

6. **Create Back Button**
   - Create Button at top-left: "< BACK"

7. **Create Completion Panel**
   - Create Panel (full screen, semi-transparent background)
   - Add child Panel for dialog box
   - Add TextMeshPro texts:
     - "PUZZLE COMPLETE!"
     - "Time: 00:00" (completionTimeText)
     - "Best: 00:00" (bestTimeText)
     - "NEW RECORD!" (newRecordText) - hidden initially
   - Add three buttons:
     - "NEXT LEVEL"
     - "RESTART"
     - "LEVEL MAP"
   - Set entire panel inactive initially

8. **Add PuzzleUI Component**
   - Create Empty GameObject: "PuzzleUIManager"
   - Add `PuzzleUI` component
   - Assign all references:
     - Timer Text
     - Back Button
     - Completion Panel
     - Completion Time Text
     - Best Time Text
     - New Record Text
     - Next Level Button
     - Restart Button
     - Return to Map Button

9. **Add PuzzleManager**
   - Create Empty GameObject: "PuzzleManager"
   - Add `PuzzleManager` component
   - Assign:
     - PuzzleGrid component
     - PuzzleUI component

10. **Link Puzzle Grid**
    - Select PuzzleGrid GameObject
    - Assign:
      - Piece Prefab
      - Grid Container (itself or parent panel)

---

## Scene Build Settings

1. **Open Build Settings**
   - File > Build Settings

2. **Add Scenes**
   - Drag and drop or "Add Open Scenes":
     - MainMenu (index 0)
     - LevelMap (index 1)
     - PuzzleGame (index 2)

3. **Set Platform**
   - Switch Platform to Android or iOS

4. **Player Settings** (for mobile)
   - Company Name
   - Product Name: "Puzzle Game"
   - Default Orientation: Portrait
   - Other Settings:
     - Scripting Backend: IL2CPP (for best performance)
     - API Compatibility Level: .NET Standard 2.1

---

## Testing the Setup

1. **Test PhotoDatabase**
   - Select `Assets/Resources/Data/PhotoDatabase`
   - Click "Refresh Images"
   - Verify 3 photos appear

2. **Create Test Levels**
   - Select `Assets/Resources/Data/LevelDatabase`
   - Click "Add New Level" (do this 3 times)
   - Configure each level:
     - Level 1: 3x3 grid, Puzzle1
     - Level 2: 4x4 grid, Puzzle2
     - Level 3: 5x4 grid, Puzzle3

3. **Test Game Flow**
   - Play MainMenu scene
   - Click Start > Should go to LevelMap
   - Click Level 1 > Should go to PuzzleGame
   - Solve puzzle or test UI

---

## Common Issues

### Scene Not Loading
- Check Build Settings - all scenes added?
- Check scene names match exactly in code

### Missing References
- Ensure all public fields in scripts are assigned
- Check Inspector for "None (Missing)" references

### Database Empty
- PhotoDatabase: Click "Refresh Images"
- LevelDatabase: Add levels using Inspector

### TextMeshPro Not Available
- Window > Package Manager
- Install "TextMesh Pro"
- Import TMP Essentials

### Buttons Not Working
- Ensure EventSystem exists in scene
- Check Button component has onClick listener
- Verify CanvasGroup not blocking raycasts

---

## Tips

- Use Prefab variants for different button styles
- Add sound effects by extending button scripts
- Use animations for smoother transitions
- Test on actual mobile device for touch input
- Use Unity Remote for quick mobile testing

---

This completes the scene setup. Once all scenes are created and configured, the game should be fully functional!
