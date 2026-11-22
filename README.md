# ⚙️ Rusted Steam Gear

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Networking: Photon PUN2](https://www.photonengine.com/pun](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922)

**Rusted Steam Gear** es un prototipo de videojuego multijugador *Top-Down Shooter* ambientado en un universo *Steampunk*, desarrollado como proyecto universitario. El juego implementa la arquitectura de red **Photon PUN2** para ofrecer partidas estilo *Battle Royale* en red local u online para hasta 4 jugadores en una misma build, donde el último jugador en pie es coronado como el "Campeón del Yermo".

---

## 🎮 Características Principales

- **Modo Battle Royale (4 Jugadores)**: Enfrentamientos todos contra todos en tiempo real donde solo uno sobrevive.
- **Ambientación Steampunk**: Diseño de personajes, escenarios y armas basados en una estética retrofuturista a vapor y engranajes.
- **Multijugador en Red Local y Online**: Integración con **Photon PUN2** que habilita matchmaking, creación de salas (lobbies) y sincronización de red entre computadoras distintas.
- **Entorno Destruible**: Murallas e infraestructura del mapa rompibles mediante impacto continuo de proyectiles.
- **Sistema de Combate Dinámico**: Movimiento fluido, sistema de salud, apuntado directo con cursor y habilidad de *dash* para evasión rápida.

---

## 🕹️ Controles

| Acción | Tecla / Control |
| :--- | :--- |
| **Movimiento** | `W` `A` `S` `D` |
| **Apuntar** | Movimiento del Cursor (Mouse) |
| **Disparar** | Clic Izquierdo del Mouse |
| **Dash (Esquiva)** | `Shift` |
| **Menú / Pausa** | `Esc` |

---

## 📦 Instalación y Descarga

El prototipo se encuentra compilado y listo para jugar a través de **Itch.io**:

👉 **[Descargar Rusted Steam Gear en Itch.io](https://kenaiichikito.itch.io/rusted-steam-gear)**

### Pasos para jugar en multijugador:
1. Descarga el paquete de juego desde la página oficial en Itch.io.
2. Descomprime el archivo ejecutable (`.zip`) en tu equipo.
3. Asegúrate de que todos los jugadores utilicen la misma versión de la *build*.
4. Ejecuta el juego, conéctate al servidor de salas (Lobby) y crea o únete a una partida.

---

## 🛠️ Tecnologías Utilizadas

- **Motor de Desarrollo**: Unity (C# / ShaderLab)
- **Servicios de Red**: Photon PUN2 (Photon Unity Networking)
- **Distribución**: Itch.io

---

## 👥 Créditos y Equipo de Desarrollo

Este proyecto fue desarrollado colaborativamente por:

* **Martin Gomez ([@KenaiiChikito](https://github.com/KenaiiChikito))**
  - Integración y arquitectura de red con Photon PUN2 (conexión de lobby, creación de salas y sincronización multijugador entre equipos).
  - Programación de gameplay: sistema de movimiento, esquiva (*dash*), salud/daño, mecánicas de disparo y jugabilidad general.

* **KokersArts ([@KokearArts](https://github.com/kokerarts))**
  - Diseño y modelado 3D del escenario jugable, personaje principal, accesorios y armamento.
  - Creación de animaciones, efectos visuales (VFX) y efectos de sonido (SFX).

---

## 📄 Licencia

Este proyecto está bajo la Licencia **MIT** - consulta el archivo `LICENSE` para más detalles.
