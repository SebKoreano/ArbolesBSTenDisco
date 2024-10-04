using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArbolesBSTenDisco
{
    public class DiskBinarySearchTree
    {
        private readonly string filePath;
        private long rootPos;

        public DiskBinarySearchTree(string path)
        {
            filePath = path;
            rootPos = -1;
        }

        public void Insert(int key)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (rootPos == -1)
                {
                    DiskTreeNode newNode = new DiskTreeNode(key);
                    rootPos = WriteNode(fs, newNode);
                }
                else
                {
                    InsertRecursive(fs, rootPos, key);
                }
            }
        }

        private void InsertRecursive(FileStream fs, long nodePos, int key)
        {
            DiskTreeNode node = ReadNode(nodePos, fs);
            if (key < node.Key)
            {
                if (node.LeftPos == -1)
                {
                    DiskTreeNode newNode = new DiskTreeNode(key);
                    node.LeftPos = WriteNode(fs, newNode);
                    WriteNode(fs, node, nodePos);
                }
                else
                {
                    InsertRecursive(fs, node.LeftPos, key);
                }
            }
            else if (key > node.Key)
            {
                if (node.RightPos == -1)
                {
                    DiskTreeNode newNode = new DiskTreeNode(key);
                    node.RightPos = WriteNode(fs, newNode);
                    WriteNode(fs, node, nodePos);
                }
                else
                {
                    InsertRecursive(fs, node.RightPos, key);
                }
            }
            else if (key == node.Key && node.IsDeleted)// key == node.Key
            {
                // Si el nodo está eliminado, lo desmarco
                node.IsDeleted = false;
                WriteNode(fs, node, nodePos);
            }
        }

        public bool Search(int key)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return SearchRecursive(fs, rootPos, key);
            }
        }

        private bool SearchRecursive(FileStream fs, long nodePos, int key)
        {
            if (nodePos == -1) return false;

            DiskTreeNode node = ReadNode(nodePos, fs);
            if (node.IsDeleted) return false;

            if (node.Key == key)
                return true;
            else if (key < node.Key)
                return SearchRecursive(fs, node.LeftPos, key);
            else
                return SearchRecursive(fs, node.RightPos, key);
        }

        public void Delete(int key)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                DeleteRecursive(fs, rootPos, key);
            }
        }


        private void DeleteRecursive(FileStream fs, long nodePos, int key)
        {
            if (nodePos == -1) return;

            DiskTreeNode node = ReadNode(nodePos, fs);
            if (node.Key == key)
            {
                node.IsDeleted = true;
                WriteNode(fs, node, nodePos);
            }
            else if (key < node.Key)
            {
                DeleteRecursive(fs, node.LeftPos, key);
            }
            else
            {
                DeleteRecursive(fs, node.RightPos, key);
            }
        }

        // Escribir un nodo en el archivo
        private long WriteNode(FileStream fs, DiskTreeNode node)
        {
            long pos = fs.Length;
            fs.Seek(pos, SeekOrigin.Begin);
            byte[] data = node.ToBytes();
            fs.Write(data, 0, data.Length);
            return pos;
        }

        private void WriteNode(FileStream fs, DiskTreeNode node, long pos)
        {
            fs.Seek(pos, SeekOrigin.Begin);
            byte[] data = node.ToBytes();
            fs.Write(data, 0, data.Length);
        }

        // Leer un nodo del archivo
        private DiskTreeNode ReadNode(long pos, FileStream fs)
        {
            fs.Seek(pos, SeekOrigin.Begin);
            byte[] data = new byte[21]; // Tamaño del nodo serializado
            fs.Read(data, 0, data.Length);
            return DiskTreeNode.FromBytes(data);
        }

        // Método para leer un nodo desde el archivo en una posición específica
        private DiskTreeNode ReadNode(long pos)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(pos, SeekOrigin.Begin);

                byte[] buffer = new byte[24]; // Tamaño del nodo en bytes
                fs.Read(buffer, 0, buffer.Length);

                return DiskTreeNode.FromBytes(buffer);
            }
        }

        // Método para realizar un recorrido en orden
        public void InOrderTraversal()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                InOrderTraversalRecursive(fs, rootPos);
            }
        }

        private void InOrderTraversalRecursive(FileStream fs, long nodePos)
        {
            if (nodePos == -1) return;

            DiskTreeNode node = ReadNode(nodePos, fs);
            InOrderTraversalRecursive(fs, node.LeftPos);
            if (!node.IsDeleted)  // Solo imprime si no está eliminado
            {
                Console.WriteLine($"Clave: {node.Key}");
            }
            if (node.IsDeleted)
            {
                Console.WriteLine($"Esta eliminado: {node.Key}");
            }
            InOrderTraversalRecursive(fs, node.RightPos);
        }

    }

}
