# Cubik Jump 

<img 
    style="display: block; 
           margin-left: auto;
           margin-right: auto;
           width: 100%;"
    src="https://imgur.com/TwwRiZY.png" 
    alt="Cubik Jump Portada">
</img>


## Tabla de contenidos

1. [Datos y Resumen del Juego](#datos-y-resumen-del-juego)
1. [Descripción del Juego](#descripción-del-juego)
1. [Manual de Usuario](#manual-de-usuario)
1. [Mecánicas del juego](#mecánicas-del-juego)
    1. [Salto](#salto)
    1. [Curva de dificultad](#curva-de-dificultad)
    1. [Obstáculos](#obstáculos)
    1. [Música y sonidos](#música-y-sonidos)
    1. [Tipo de Cámara/Escena](#tipo-de-camaraescena)
1. [Elementos de la escena](#elementos-de-la-escena)
    1. [Montañas](#montañas)
    1. [Arboles](#arboles)
    1. [Suelo](#suelo)
    2. [Hierba](#hierba)
    1. [Obstáculos](#obstáculos)
    1. [Jugador](#jugador)
    1. [Managers](#managers)
    1. [GameArea](#gamearea)
    1. [Textos y botones](#textos-y-botones)
    1. [Sistema de partículas](#sistema-de-partículas)
    1. [Post-Process Volume](#post-process-volume)
1. [Interfaz](#interfaz)
1. [Estados del juego](#estados-del-juego)
1. [Detalles de la producción](#detalles-de-la-producción)

<br/>

## Datos y Resumen del Juego

* **Título:** Cubik Jump

* **Concepto principal:** cubo 3D que que realiza saltos

* **Características principales:** visualmente atractivo, simple, entretenido, adictivo.

* **Género:** Endless-Runner, Plataformas.

* **Propósito y público objetivo:** el proposito principal es el aprendizaje y el secundario es el entretenimiento. El público objetivo son todos.

* **Jugabilidad:** el objetivo es conseguir el mayor número de puntos esquivando todos los obstáculos posibles, para ello deberás saltar.

* **Estilo visual:** escena 3D, cubos, bloom.

* **Alcance:** actualmente será educacional. El futuro es incierto pero prometedor.

* **Plataforma:** disponible en Windows y Android.

* **Categoría:** Juego Indie

<br/>

## Descripción del Juego

<br/>

> Cubik Jump es un `Endless Runner` (subgénero de `plataformas`).
>
> El objetivo del juego es conseguir la máxima puntuación esquivando todos los obstáculos posibles. Esta
> puntuación se basa en el tiempo, cada segundo son 10 puntos.

<br/>

## Manual de Usuario

<center>
    <img src="https://imgur.com/PNzhX0l.png" alt="Cubik Jump Tutorial"></img>
</center>

<br/>

## Mecánicas del juego

<br/>

### Salto

> Para saltar necesitarás presionar el espacio o click izquierdo en Windows, y tocar la pantalla en Android.
> Una mecánica simple pero efectiva.

<br/>

### Curva de dificultad

> Existe una curva de dificultad en función de los puntos, actualmente esta curva funciona de la siguiente manera:

<br/>

<center>
            
|  Curva    | Nivel de Dificultad |
| ------------- |:---------:|
| A partir de 0         | 0 |
| Entre 250 y 500       | 1 |
| Entre 500 y 750       | 2 |
| Entre 750 y 1000      | 3 |
| A partir de 1000      | 4 |

***En un futuro se implementará una curva de dificultad dinámica.**
</center>

<br/>

### Obstáculos

> Los obstáculos que aparecen de manera aleatoria suponen un desafío para el jugador puesto que tendrá que saltar los máximos posibles.

<br/>

### Música y sonidos

> Se ha utilizado una track sin copyright para el fondo: [Theme Track](https://youtu.be/wOMwO5T3yT4?t=2040) y para el deathsound del cubo he utilizado este sonido: [Death Sound](https://pixabay.com/sound-effects/pixel-death-66829/)

<br/>

### Tipo de Camara/Escena

<br/>

> En este caso he optado por un camara 3D, aprovechandome de la profundidad de los objetos en 3D y un fondo plano.

<br/>

## Elementos de la escena.

<br/>

### Montañas

> Representadas en color blanco mediante un Material, aparecen en el fondo de la escena.
> Tienen un BoxCollider y un RigidBody para poder moverse por la escena creando la sensación de movimiento.
> También cuenta con el script que le da movimiento.

<br/>

### Arboles

> Representados en color verde con un tono oscuro mediante un Material, aparecen donde empieza la hierba.
> Tienen un BoxCollider y un RigidBody para poder moverse por la escena creando la sensación de movimiento.
> También cuenta con el script que le da movimiento.

<br/>

### Suelo

> El suelo está formado por un BoxCollider y un RigidBody para que el jugador pueda situarse encima de él, además de tener un Material para darle un color Naranja.

<br/>

### Hierba

> En este caso, la hierba tambien tiene un BoxCollider y un RigidBody para evitar que el fondo se caiga. Tiene un material con un color verde claro.

<br/>

### Obstáculos

> Objeto con un RigidBody un BoxCollider para poder detectar las colisiones con el jugador.
> Está representado con un color morado de tono oscuro.
> Contiene el script de movimiento.

<br/>

### Jugador

> El jugador es el objeto más importante de la escena pues es el protagonista, está representado con un color naranja mediante el material, además de tener un RigisBody y un BoxCollider para implementar todo el tema de las físicas.
> Tiene un Animator que controla la animación de salto.
> Contiene un script que controla todas las físicas referentes al jugador.
> Por último tiene un audio source con el death sound.

<br/>

### Managers

> UIManager para la parte de la interfaz y GameManager para la parte relacionada con el inicio, fin, pausa y demás.

<br/>

### GameArea

> Simplemente tiene un Box Collider con el Trigger activado, lo utilizo para saber cuando un elemento sale del area y así destruirlo.

<br/>

### Textos y botones

> Todos los TMP_Text y Button de la escena.

<br/>

### Sistema de partículas

> Cada elemento en pantalla que tiene movimiento, tiene un sistema de particulas que se reproduce cuando el jugador muere.

<br/>

### Post-Process Volume

> Genera un efecto visual de brillo gracias a la propiedad bloom que está activada.



<br/>

## Interfaz

**Puntuación:** Muestra la puntuación actual que lleva el jugador.

**Puntuación Record:** Aparece en el menu de pausa/game over y se muestra el mayor record realizado.

**Botones de Jugar y Salir**: Se dispone de dos botones en el menu de pausa/game over; jugar y salir.

***

**Fuente Utilizada**: [Slice](https://www.dafont.com/slice-cmunk.font)

<br/>

## Estados del juego

> El juego solamente contiene una escena, lo unico que varía son los GameObjects que se encuentran es ella.

<br/>

## Detalles de la producción

**Fecha de inicio:** 09/12/2022

**Fecha de finalización:** 23/12/2022 (Versión Estable) - Actualidad

**Presupuesto:** 0€
