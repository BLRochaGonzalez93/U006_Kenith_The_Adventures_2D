# U006_Kenith_The_Adventures_2D

[English](README.en.md) | [Español](README.md)

## Summary

Playable survival/crafting prototype developed in Unity with C#. Kenith is the protagonist of a 2D top-down experience focused on surviving, gathering resources, fighting enemies and bosses, completing quests and crafting progressively better items.

The project has documented narrative and story, an initial tutorial, health, damage, stats, inventory, crafting, resource gathering, pickups, collectibles, dialogues, UI and a structure designed to expand its content.

## Technologies

- Unity
- C#
- Unity 2D physics system
- Collider2D / Rigidbody2D
- Tilemap
- Animator
- Particle System
- Basic UI
- AudioSource
- ScriptableObjects
- Gathering System
- Crafting System
- NavMesh / custom pathfinding
- Git LFS
- GitHub Releases

## Main features

- 2D top-down survival/crafting.
- Main protagonist: Kenith.
- 2D movement and interaction.
- Melee attack.
- Ranged attack planned for future versions.
- Enemies with basic AI.
- Bosses.
- Obstacles and traps.
- Resource gathering.
- Pickups and collectibles.
- Item crafting.
- Inventory and equipment.
- Health system.
- Damage system.
- Levels, experience and stats system.
- Quests.
- Initial tutorial.
- Main menu.
- Pause.
- Particles.
- Sound and music.
- UI.
- Animations.
- Dialogues and narrative.
- Playable Windows build.

## Screenshots

> Final screenshots pending.

Planned path:

![Gameplay](./Media/screenshots/gameplay-01.png)

## Architecture

The main logic is organized into several folders inside `Assets/Scripts`:

- `Character Panel` — inventory, equipment, slots, stats and character panels.
- `Character Stats` — stats and modifiers.
- `Crafting System` — recipes, crafting benches and crafting windows.
- `Enemy` — enemies, health, drops, projectiles and health bars.
- `Interactions` — movement, gathering, weapons, dialogues, tutorial, weather and raycasts.
- `Items` — equippable items, usable items, containers, effects and interaction.
- `UI` — health bars, experience, day cycle and tutorial.

Highlighted scripts:

- `PlayerMovement`
- `GatheringGenerator`
- `Weapon`
- `XP`
- `EnemyChase`
- `EnemyHealth`
- `EnemyDrop`
- `CraftingRecipe`
- `CraftingWindow`
- `CraftingBenchWindows`
- `Inventory`
- `EquipmentPanel`
- `Character`
- `Item`
- `InteractableItem`
- `EquippableItem`
- `UsableItem`
- `LifeStaExpSystem`
- `LifeStaExpBarsUI`
- `SimpleTutorial`

More information:

[`Docs/Architecture.md`](./Docs/Architecture.md)

## Recommended code to review

- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/PlayerMovement.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/PlayerMovement.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/GatheringGenerator.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/GatheringGenerator.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting System/CraftingRecipe.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting%20System/CraftingRecipe.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting System/CraftingWindow.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting%20System/CraftingWindow.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character Panel/Inventory.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character%20Panel/Inventory.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character Panel/EquipmentPanel.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character%20Panel/EquipmentPanel.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyChase.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyChase.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyHealth.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyHealth.cs)
- [`Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/UI/LifeStaExpSystem.cs`](./Project/PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/UI/LifeStaExpSystem.cs)

## Build

The build is available through GitHub Releases.

[`Releases/Download.md`](./Releases/Download.md)

[Download build U006-v1.0.0](https://github.com/BLRochaGonzalez93/U006_Kenith_The_Adventures_2D/releases/tag/U006-v1.0.0)

## Status

**Playable survival/crafting prototype.**

The project includes survival, combat, enemies, bosses, resource gathering, inventory, crafting, stats, health, damage, experience, quests, tutorial, UI, narrative, dialogues, particles, sound and music.

Possible pending improvements:

- Add ranged attack.
- Add keys and doors.
- Add more gatherable resources.
- Add more crafting recipes.
- Add more enemies.
- Add more bosses.
- Expand the quest system.
- Improve enemy AI.
- Add progress saving.
- Improve damage feedback.
- Improve stat and level balancing.
- Expand narrative and dialogues.
- Add more animations.

## Learnings

This project allowed me to work on the design of a top-down survival prototype with connected systems for movement, combat, resources, crafting, inventory, experience and stats.

It also helped me practice 2D interaction, health and damage management, resource gathering, pickups, collectibles and character progression.

In addition, I worked on a crafting and inventory base through recipes, equippable items, usable items, effects and containers.

The project also helped me organize enemies, bosses, quests, narrative, dialogues and tutorial, integrating several gameplay systems into one broader experience.
