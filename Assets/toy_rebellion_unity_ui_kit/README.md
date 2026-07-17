# Toy Rebellion – Unity Canvas UI Starter Kit

This is a functional asset starter kit, not a flattened game screen.

## Included
- SVG/ panel and button assets
- PNG preview
- 9-slice friendly backgrounds
- Separate icons
- No baked-in text

## Unity import
1. Drag the PNG or SVG assets into `Assets/UI/ToyRebellion`.
2. For PNG files, select each texture and set:
   - Texture Type: Sprite (2D and UI)
   - Sprite Mode: Single
   - Mesh Type: Full Rect
   - Filter Mode: Bilinear
   - Compression: None or High Quality
3. For scalable assets, install Unity's Vector Graphics package and use the SVG files.
4. For panel, tab, banner, button, currency box and progress assets:
   - Open Sprite Editor
   - Set borders around 24–32 pixels
   - Set the Image component Type to `Sliced`
5. Put TextMeshPro labels over the blank assets. Do not bake text into sprites.

## Suggested Canvas hierarchy
Canvas
└── UpgradeScreen
    ├── Background
    ├── TopBar
    │   ├── BackButton
    │   ├── TitleBanner
    │   └── CurrencyBox
    ├── ToyTabs
    │   ├── PlushieTab
    │   ├── ActionFigureTab
    │   └── RobotTab
    └── UpgradeList
        ├── HealthUpgradeCard
        │   ├── Icon
        │   ├── NameText
        │   ├── ValueText
        │   ├── LevelText
        │   ├── ProgressBar
        │   └── UpgradeButton
        └── DamageUpgradeCard

## Mobile setup
- Canvas Scaler: Scale With Screen Size
- Reference Resolution: 1080 x 1920
- Match: 0.5
- Use a Vertical Layout Group for upgrade cards.
- Use Layout Element components rather than fixed positioning wherever possible.

## Important
The preview image is only a layout reference. Build the Canvas from the separate assets.
