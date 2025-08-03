# AsteroidsNeo
A modern reimagining of the classic 1979 **Asteroids** game. Pilot a ship through space, blast asteroids, and survive as long as you can. Retro-inspired visuals, smooth controls, and modular code. The primary goal was not just to recreate the gameplay but to build it upon a professional, scalable, and maintainable software architecture using modern design patterns and best practices. Licensed under MPL-2.0.

## Features
- Classic asteroid-splitting gameplay
- Smooth ship physics
- Retro visuals and sound (royalty-free)
- MPL-2.0 licensed

## Core Architectural Pillars
The game's architecture is built on several key design patterns and principles to ensure that the codebase is robust, decoupled, and easy to extend.

1. State Pattern (FSM) for Game Flow
   
   The overall flow of the game is managed by a Finite State Machine (FSM). A central GameManager controls the transitions between distinct states (MainMenuState, GameplayState, GameOverState). Each state is its class, responsible for the specific logic of that phase (e.g., enabling player input, starting the asteroid spawner). This approach avoids a single, monolithic update loop and keeps the logic for each game phase cleanly separated.

2. Event-Driven Architecture
   
   To prevent tight coupling between different systems, the project relies heavily on an event-driven (or Observer) pattern. A static GameEvents class acts as a central event bus.

3. Data-Driven Design with ScriptableObjects
   
   All game balance parameters and configurations are externalized from the code into ScriptableObjects. This includes:-
- SpaceShipSO: Defines the player's speed, rotation, fire rate, and health.
- AsteroidSO: Defines an asteroid's size, score value, speed, and splitting behavior.
- AmmoSO: Defines a bullet's speed and lifetime.
- AudioSO: Contains references to all music and sound effect clips.

  This data-driven approach allows for rapid iteration and balancing by designers without needing to change or recompile any code.

4. Factory and Object Pooling Patterns
   
   To ensure high performance, especially for a WebGL build, the creation of high-turnover game objects (bullets, asteroids, explosion effects) is managed by a Factory and Object Pooling system. Instead of repeatedly calling Instantiate() and Destroy(), which causes performance-killing garbage collection, the system recycles objects. Factories (AsteroidFactory, AmmoFactory) are the public-facing API for creating objects. Internally, they request an object from a corresponding ObjectPool, which manages a list of active and inactive objects.

5. SOLID Principles
   
   The SOLID principles were a guiding philosophy throughout the project:-
- S (Single Responsibility): The player is broken down into modular components, each with one job: PlayerMovement handles physics, PlayerHealth handles damage, PlayerShooter handles firing, and PlayerFeedback handles effects.
- O (Open/Closed): The event system is a prime example. New systems can subscribe to events like OnPlayerDied without modifying the PlayerHealth script that fires the event.
- L (Liskov Substitution): The IDamageable interface is implemented by both the PlayerHealth and AsteroidBehaviour, allowing a bullet (ammo) to treat them interchangeably.
- I (Interface Segregation): We used small, specific interfaces like IDamageable, IMovable, and IInputProvider rather than a single, large "IGameObject" interface.
- D (Dependency Inversion): The PlayerController does not depend on concrete keyboard input; it depends on the IInputProvider abstraction. This allows us to easily swap in different input methods (like a gamepad or AI) without changing the controller.

6. Structured and Modular Codebase

   The codebase is organized into namespaces that mirror the folder structure (SuperLaggy.AsteroidsNeo.Core, SuperLaggy.AsteroidsNeo.UI, SuperLaggy.AsteroidsNeo.Core.Player, etc.) to prevent naming collisions and improve clarity. The architecture emphasizes creating small, modular components that are responsible for a single task and communicate through well-defined interfaces or events.

## Controls
- `WASD` / `Arrow keys` – Move
- `Space` / 'LMB' – Fire
- `Esc` – Pause

## License
Licensed under the **Mozilla Public License 2.0**.
