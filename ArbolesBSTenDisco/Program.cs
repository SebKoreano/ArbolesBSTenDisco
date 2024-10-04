using System;
using static System.Net.Mime.MediaTypeNames;

namespace ArbolesBSTenDisco
{
    internal class Program
    {
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BST.bin");

        static void Main(string[] args)
        {
            // Crear una instancia de Program para acceder a filePath
            Program program = new Program();

            // Instanciar el árbol binario de búsqueda en disco
            DiskBinarySearchTree BST = new DiskBinarySearchTree(program.filePath);

            Insertar([10,20,30,40,1,2,3,4,5], BST);

            Imprimir(BST);

            Buscar(10,BST);

            Eliminar(10, BST);

            Imprimir(BST);

            Buscar(10, BST);

            Insertar([10], BST);

            Imprimir(BST);

            Buscar(10, BST);

            Terminar();
        }

        static void Insertar(int[] numeros, DiskBinarySearchTree BST)
        {
            // Pruebas de inserción
            Console.WriteLine("\nInsertando nodos en el BST...");
            foreach (int n in numeros)
            {
                Console.WriteLine($"Insert: {n}");
                BST.Insert(n);
            }
        }

        static void Imprimir(DiskBinarySearchTree BST)
        {
            // Mostrar todos los elementos en orden ascendente (inOrder traversal)
            Console.WriteLine("\nBST en orden ascendente (inOrder traversal):");
            BST.InOrderTraversal();
        }

        static bool Buscar(int n, DiskBinarySearchTree BST)
        {
            // Prueba de búsqueda
            Console.WriteLine($"\nBuscando el nodo con valor {n}:");
            bool found = BST.Search(n);
            Console.WriteLine(found ? "Nodo encontrado." : "Nodo no encontrado.");
            return found;
        }

        static void Eliminar(int num,  DiskBinarySearchTree BST)
        {
            // Prueba de eliminación
            Console.WriteLine($"\nEliminando el nodo con valor {num}...");
            BST.Delete(num);
        }

        static void Terminar()
        {
            // Evitar que la consola se cierre inmediatamente
            Console.WriteLine("\nPruebas completadas.");
            Console.ReadLine();
        }
    }


}