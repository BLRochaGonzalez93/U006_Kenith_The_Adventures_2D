# U006_Kenith_The_Adventures_2D

[English](README.en.md) | [Español](README.md)

## Resumen

Prototipo survival/crafting jugable desarrollado en Unity con C#. Kenith es el protagonista de una experiencia 2D top-down centrada en sobrevivir, recolectar recursos, enfrentarse a enemigos y bosses, completar misiones y fabricar objetos progresivamente mejores.

El proyecto tiene una narrativa e historia documentada, un tutorial inicial, sistema de vida, daño, estadísticas, inventario, crafteo, recolección de recursos, pickups, coleccionables, diálogos, UI y una estructura pensada para ampliar el contenido.

## Tecnologías

- Unity
- C#
- Sistema de físicas 2D de Unity
- Collider2D / Rigidbody2D
- Tilemap
- Animator
- Particle System
- UI básica
- AudioSource
- ScriptableObjects
- Gathering System
- Crafting System
- NavMesh / pathfinding propio
- Git LFS
- GitHub Releases

## Características principales

- Survival/crafting 2D con vista top-down.
- Protagonista principal: Kenith.
- Movimiento e interacción en 2D.
- Ataque cuerpo a cuerpo.
- Ataque a distancia previsto para próximas versiones.
- Enemigos con IA básica.
- Bosses.
- Obstáculos y trampas.
- Recolección de recursos.
- Pickups y coleccionables.
- Crafteo de objetos.
- Inventario y equipamiento.
- Sistema de vida.
- Sistema de daño.
- Sistema de niveles, experiencia y estadísticas.
- Misiones.
- Tutorial inicial.
- Menú principal.
- Pausa.
- Partículas.
- Sonido y música.
- UI.
- Animaciones.
- Diálogos y narrativa.
- Build jugable para Windows.

## Capturas

> Pendiente de añadir capturas finales.

Ruta prevista:

![Gameplay](../Media/screenshots/gameplay-01.png)

## Arquitectura

La lógica principal se organiza en varias carpetas dentro de `Assets/Scripts`:

- `Character Panel` — inventario, equipamiento, slots, estadísticas y paneles de personaje.
- `Character Stats` — estadísticas y modificadores.
- `Crafting System` — recetas, bancos de crafteo y ventanas de fabricación.
- `Enemy` — enemigos, vida, drops, proyectiles y barras de vida.
- `Interactions` — movimiento, recolección, armas, diálogos, tutorial, clima y raycasts.
- `Items` — objetos equipables, usables, contenedores, efectos e interacción.
- `UI` — barras de vida, experiencia, ciclo de día y tutorial.

Scripts destacados:

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

## Código recomendado para revisar

- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/PlayerMovement.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/PlayerMovement.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/GatheringGenerator.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Interactions/GatheringGenerator.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting System/CraftingRecipe.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting%20System/CraftingRecipe.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting System/CraftingWindow.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Crafting%20System/CraftingWindow.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character Panel/Inventory.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character%20Panel/Inventory.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character Panel/EquipmentPanel.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Character%20Panel/EquipmentPanel.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyChase.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyChase.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyHealth.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/Enemy/EnemyHealth.cs)
- [`PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/UI/LifeStaExpSystem.cs`](./PRJ_SurvivalCrafting2DGameFinal/Assets/Scripts/UI/LifeStaExpSystem.cs)

## Build

La build está disponible en GitHub Releases.

[`Releases/Download.md`](../Releases/Download.md)

[Descargar build U006-v1.0.0](https://github.com/BLRochaGonzalez93/U006_Kenith_The_Adventures_2D/releases/tag/U006-v1.0.0)

## Estado

**Prototipo survival/crafting jugable.**

El proyecto incluye supervivencia, combate, enemigos, bosses, recolección de recursos, inventario, crafteo, estadísticas, vida, daño, experiencia, misiones, tutorial, UI, narrativa, diálogos, partículas, sonido y música.

Pendiente de posibles mejoras:

- Añadir ataque a distancia.
- Añadir llaves y puertas.
- Añadir más recursos recolectables.
- Añadir más recetas de crafteo.
- Añadir más enemigos.
- Añadir más bosses.
- Ampliar el sistema de misiones.
- Mejorar IA enemiga.
- Añadir guardado de progreso.
- Mejorar feedback de daño.
- Mejorar balance de estadísticas y niveles.
- Ampliar narrativa y diálogos.
- Añadir más animaciones.

## Aprendizajes

Este proyecto me permitió trabajar el diseño de un prototipo survival top-down con sistemas conectados de movimiento, combate, recursos, crafteo, inventario, experiencia y estadísticas.

También me ayudó a practicar interacción 2D, gestión de vida y daño, recolección de recursos, pickups, coleccionables y progresión del personaje.

Además, pude trabajar una base de crafteo e inventario mediante recetas, objetos equipables, objetos usables, efectos y contenedores.

El proyecto también me sirvió para organizar enemigos, bosses, misiones, narrativa, diálogos y tutorial, integrando varios sistemas de gameplay en una única experiencia más amplia.
