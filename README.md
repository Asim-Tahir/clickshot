# Clickshot

Clickshot is a 3D Unity demo built around touch controls and raycast-based target interaction. The player moves horizontally and uses Unity's `Physics.Raycast` to shoot shootable buttons instantly, without spawning projectile objects.

## Screenshots

<img width="480" alt="Raycast targeting for shoot screenshot" src="https://github.com/user-attachments/assets/35015e2a-a4a7-439f-8963-d9ff19f0e8d9" />
<img width="480" alt="Player movement screenshot" src="https://github.com/user-attachments/assets/c81e7741-4883-4259-8b44-18da0df3e36c" />
<img width="480" alt="After multiple raycast shoots screenshot" src="https://github.com/user-attachments/assets/1dddea35-a41b-4723-a313-db1d44c72047" />


## Gameplay

- Touch the screen briefly to cast a ray from the player's fire point with `Physics.Raycast`. The hit is resolved immediately, and a hit button is activated.
- Hold a touch for at least 0.25 seconds to move horizontally. The side of the screen you hold determines the movement direction.
- A hit button changes to a random color and plays its `Press` animation trigger.
- The camera smoothly follows the player.

## Requirements

- Unity Editor `6000.6.7f1`
- Unity Input System package `1.20.1` (resolved through Unity Package Manager)
- Universal Render Pipeline

## Open and Run

1. Open the project folder in Unity Hub using Unity `6000.6.7f1`.
2. Open `Assets/Scenes/MainScene.unity`.
3. Enter Play mode. Touch input is handled through the Input System's Enhanced Touch API.

## Project Structure

- `Assets/Scenes/` - Main gameplay scene.
- `Assets/Scripts/Player.cs` - Touch movement, camera follow, and shooting.
- `Assets/Scripts/ShootableButton.cs` - Button hit response, animation, and color change.
- `Assets/Scripts/ShootableButtonController.cs` - Observes configured shootable buttons.
- `Packages/manifest.json` - Unity package dependencies.
