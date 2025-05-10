# Inventory and Shop Game

## Overview

This game implements an inventory and shop system, allowing players to manage items, buy from a shop, and sell items from their inventory. It was developed as part of a game design exercise.

## Gameplay

The game features:

* **Inventory:** A personal storage space for items collected by the player.
* **Shop:** A place to buy and sell items.
* **Items:** Items have properties like type, icon, description, buying/selling price, weight, and rarity.
* **Currency:** Players use currency to buy items.
* **Weight Limit:** The inventory has a maximum weight capacity.
* **Resource Gathering:** Players can gather random items.
* **Buying and Selling:** Players can buy items from the shop and sell them from their inventory.

## Gameplay Video

[Gameplay Video](https://drive.google.com/file/d/1M25B6WUGU_0CWZ4JYoQN7g41YAyPKSLQ/view?usp=sharing)

## Architecture

The game architecture follows a modified Model-View-Controller (MVC) pattern, adapted for Unity. Here's a visual overview:


## Design Patterns and Principles

The following design patterns and programming principles were used:

* **MVC (Modified):** Used to structure the game logic and UI. The `InventoryModel`, `InventoryView`, and `InventoryController` classes exemplify this.
* **Singleton:** The `EventService` class uses the Singleton pattern to provide a global, centralized event system.
* **Observer (via Event Service):** The `EventService` also enables the Observer pattern, allowing components to subscribe to events (e.g., the `InventoryView` subscribing to inventory change events).
* **Factory (Implicit):** The `InventoryController` acts like a factory when creating `ItemModel` instances.
* **SOLID Principles:**
    * **Single Responsibility Principle (SRP):** Classes generally have a single, well-defined purpose. For example, `InventoryModel` focuses on data and logic, while `InventoryView` focuses on UI.
    * **Open/Closed Principle (OCP):** The event system allows for extending functionality (adding new events and subscribers) without modifying existing classes.
    * **Liskov Substitution Principle (LSP):** Not heavily used in this project, but the item system is designed to allow for different item types.
    * **Interface Segregation Principle (ISP):** Not a major factor in this project.
    * **Dependency Inversion Principle (DIP):** The use of the `EventService` helps to decouple components, reducing direct dependencies.

![image](https://github.com/user-attachments/assets/9acb9bcc-8d96-4fab-a548-83a6326f8bc9)

