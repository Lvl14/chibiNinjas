# chibiNinjas
Mobile Game Developed By Alex &amp; Núria

Unity: https://store.unity.com/es

Android sdk: https://dl.google.com/android/repository/tools_r25.2.3-windows.zip

Go: https://unity3d.com/es/learn/tutorials/topics/mobile-touch/building-your-unity-game-android-device-testing
Seguir desde : Setting up the Android SDK Tools



# Instrucciones GIT

## Pasos previos

### Programario

Git:
> https://git-scm.com/

Git ficheros grandes
> https://git-lfs.github.com/

### Descarga del repositorio

Primero toca descargar el contenido del repositorio. Pasos:

1. Instalar normalmente el git y el git-lfs.
2. Tener una carpeta nueva y vacia donde mejor vaya.
3. Abrir con el explorador, tal cual, botón derecho en pleno centro y darle a "Git Gui"
4. Clicar en "Clone Existing Repository"
5. En "Source Location" copiar "https://github.com/Lvl14/chibiNinjas.git"
6. En "Target Directory" buscar la carpeta nueva y añadir un "/itlightens" (o cualquier otro nombre que os guste más)
7. Clic en "Clone" y esperar un rato a que se descargue.
8. Cuando termine salir de la nueva ventana que aparece

## Organización

Ahora deberiais tener dentro todas las carpetas ("Asserts", "Library", ...) en la subcarpeta chibiNinjas.

Con esto debería poder abrirse como proyecto en Unity3D.

## Comandos terminal GIT

Antes de empezar seria bueno que por el grupo de whatsapp que sea conveniente os reserveis los ficheros ya existentes que se deban cambiar, para evitar modificar en paralelo el mismo.
Ahora se especifican comandos para el terminal:

### Comprobar estado repositorio
Este comando os dirà que cambios o ficheros nuevos teneis pendientes para subir al repositorio.
> git status

### Actualizar repositorio
Si no teneis cambios pendientes de subir, podreis actualizar sin problemas.
> git pull origin develop

### Subir cambios
* Si estais seguros de que todo lo que aparece en el "status" se debe subir (el punto final es importante):

> git add --all .

	* Si no se deben subir todos pongo una ruta de ejemplo para fichero concreto:

> git add bin/data/scenes/ms3.xml

	* Pongo una ruta de ejemplo para todos los ficheros nuevos y cambiados de una carpeta:

> git add bin/data/scenes

* Una vez añadidos, con un mensaje de ejemplo entre comillas simples (recomendado algo claro y descriptivo por si hay que buscarlo):

> git commit -m 'Descripción del contenido de la subida'

* Si os dejais de añadir algo y os dais cuenta o quereis separar las subidas por organización, repetir el paso 1-2 por cada parte.
* Mandar los cambios al repositorio (os pedirá usuario y contraseña de github "https://github.com/orgs/Lvl14/people"):

> git push origin dev
