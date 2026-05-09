# U006_Kenith_The_Adventures_2D

[English](README.en.md) | [Español](README.md)

## Resumen

**Kenith: The Adventures 2D** es un prototipo survival/crafting jugable desarrollado en Unity con C#. El jugador controla a Kenith en una experiencia 2D con vista top-down, centrada en sobrevivir, recolectar recursos, combatir enemigos y fabricar objetos cada vez más importantes y de mayor calidad.

El proyecto combina exploración, recolección, crafteo, enemigos, bosses, misiones, tutorial inicial, narrativa documentada y progresión mediante niveles y estadísticas. La derrota se produce si el jugador pierde toda su vida.

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

- Gameplay survival/crafting 2D con vista top-down.
- Protagonista principal: Kenith.
- Movimiento horizontal en entorno 2D.
- Ataque cuerpo a cuerpo.
- Ataque a distancia previsto para futuras versiones.
- Enemigos con IA básica.
- Bosses y encuentros especiales.
- Obstáculos y trampas.
- Recolección de recursos.
- Pickups y coleccionables.
- Sistema de crafteo.
- Sistema de inventario y equipamiento.
- Sistema de vida y daño.
- Sistema de niveles, experiencia y estadísticas.
- Misiones y objetivos.
- Tutorial inicial.
- Menú principal.
- Sistema de pausa.
- Partículas, sonido y música.
- UI, animaciones, diálogos y narrativa.
- Build jugable para Windows.

## Visuales

> Pendiente de añadir capturas e imágenes finales.

Nombres previstos para el pack visual:

- `keniththeadventures-logo.png`
- `keniththeadventures-cover.png`
- `keniththeadventures-banner.png`
- `keniththeadventures-thumbnail-01-survival-gameplay.png`
- `keniththeadventures-thumbnail-02-resource-gathering.png`
- `keniththeadventures-thumbnail-03-crafting-system.png`
- `keniththeadventures-thumbnail-04-boss-encounter.png`

## Arquitectura

El proyecto organiza sus scripts en varias áreas funcionales dentro de `Assets/Scripts`:

- **Character Panel** — inventario, equipamiento, slots, estadísticas y tooltips.
- **Character Stats** — definición de estadísticas y modificadores.
- **Crafting System** — recetas, ventanas de crafteo e interfaz de fabricación.
- **Enemy** — enemigos, vida, drops, proyectiles y barras de vida.
- **Interactions** — movimiento, recolección, armas, diálogos, tutoriales, raycasts y eventos.
- **Items** — items equipables, usables, contenedores, efectos y objetos interactuables.
- **UI** — ciclo de día, barras de vida/experiencia y tutorial simple.

Scripts destacados del proyecto:

- `PlayerMovement`
- `GatheringGenerator`
- `Weapon`
- `WeaponParent`
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

La build está disponible en GitHub Releases.

[Descargar build U006-v1.0.0](https://github.com/BLRochaGonzalez93/U006_Kenith_The_Adventures_2D/releases/tag/U006-v1.0.0)

## Estado

**Prototipo survival/crafting jugable.**

El proyecto incluye una fase principal con supervivencia, combate, enemigos, bosses, recolección, inventario, crafteo, estadísticas, vida, daño, misiones, tutorial, UI, narrativa, diálogos, partículas, sonido y música.

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

Este proyecto me permitió trabajar el diseño de un prototipo survival top-down, combinando movimiento, combate, recolección, progresión y sistemas de crafteo.

También me sirvió para practicar movimiento e interacción 2D, sistemas de vida, daño, experiencia, estadísticas e inventario.

Además, pude trabajar una base de crafteo e inventario con recetas, contenedores, objetos equipables y objetos usables.

El proyecto también me ayudó a organizar enemigos, bosses, misiones, narrativa, diálogos y tutorial dentro de una experiencia más amplia y compleja que un prototipo arcade simple.
