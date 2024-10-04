# Árbol Binario de Búsqueda en Disco (BST)

Este proyecto implementa un Árbol Binario de Búsqueda (BST) que persiste en disco, permitiendo operaciones de inserción, búsqueda y eliminación de nodos. A diferencia de las implementaciones tradicionales en memoria, este BST almacena sus nodos en un archivo binario, lo que garantiza su persistencia entre ejecuciones.

## Descripción

El árbol binario de búsqueda es una estructura de datos eficiente para almacenar datos ordenados y permite realizar búsquedas rápidas. En esta implementación, cada nodo se almacena en un archivo en disco, lo que lo hace persistente incluso después de que la aplicación se cierra.

### Características

- **Inserción**: Agrega nodos al árbol y los guarda en disco.
- **Búsqueda**: Permite buscar nodos por su clave.
- **Eliminación Lógica**: Marca los nodos como eliminados sin necesidad de reorganizar el archivo.
- **Recorrido en Orden**: Imprime los nodos en orden ascendente, omitiendo los nodos eliminados.

## Estructura del Proyecto

- **`DiskTreeNode`**: Clase que representa los nodos del árbol. Incluye métodos para serializar y deserializar nodos.
- **`DiskBinarySearchTree`**: Clase que implementa el BST, gestionando la inserción, búsqueda y eliminación de nodos, así como el recorrido en orden.

