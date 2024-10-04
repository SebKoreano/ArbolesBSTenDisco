using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolesBSTenDisco
{
    class DiskTreeNode
    {
        public int Key;
        public long LeftPos;
        public long RightPos;
        public bool IsDeleted;

        public DiskTreeNode(int key)
        {
            Key = key;
            LeftPos = -1;
            RightPos = -1;
            IsDeleted = false;
        }

        // Serializar el nodo a un formato adecuado para escribir en disco
        public byte[] ToBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(ms);
                writer.Write(Key);
                writer.Write(LeftPos);
                writer.Write(RightPos);
                writer.Write(IsDeleted);
                return ms.ToArray();
            }
        }

        // Deserializar un nodo desde un array de bytes
        public static DiskTreeNode FromBytes(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryReader reader = new BinaryReader(ms);
                int key = reader.ReadInt32();
                long leftPos = reader.ReadInt64();
                long rightPos = reader.ReadInt64();
                bool isDeleted = reader.ReadBoolean();
                return new DiskTreeNode(key) { LeftPos = leftPos, RightPos = rightPos, IsDeleted = isDeleted };
            }
        }
    }
}
