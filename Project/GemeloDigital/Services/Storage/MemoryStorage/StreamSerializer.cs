using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital.Services.MemoryStorage
{
    internal class StreamSerializer
    {
        byte[] buffer;
        Stream stream;

        internal StreamSerializer(Stream _stream, int _initialBufferSize)
        {
            stream = _stream;
            buffer = new byte[_initialBufferSize];
        }

        void EnsureCapacity(int size)
        {
            if(buffer.Length < size)  { buffer = new byte[size]; }
        }

        internal void AddInt(int n)
        {
            int size = sizeof(int);
            EnsureCapacity(size);
            BitConverter.TryWriteBytes(buffer, n);
            stream.Write(buffer, 0, size);
        }
        internal void AddSingle(float f)
        {
            int size = sizeof(float);
            EnsureCapacity(size);
            BitConverter.TryWriteBytes(buffer, f);
            stream.Write(buffer, 0, size);
        }

        internal void AddVector3(Vector3 v)
        {
            AddSingle(v.X);
            AddSingle(v.Y);
            AddSingle(v.Z);
        }

        internal void AddString(string s)
        {
            int byteCount = Encoding.UTF8.GetByteCount(s);
            AddInt(byteCount);

            EnsureCapacity(byteCount);
            Encoding.UTF8.GetBytes(s, buffer);
            stream.Write(buffer, 0, byteCount);
        }

        internal void AddReference<T>(T o) where T:SimulatedObject
        {
            string s;
            if(o == null) { s = ""; }
            else { s = o.Id; }
            AddString(s);
        }

        internal void AddReferenceList<T>(List<T> objects) where T:SimulatedObject
        {
            AddInt(objects.Count);
            foreach(T o in objects)
            {
                AddReference(o);
            }
        }

        internal int GetInt()
        {
            int size = sizeof(int);
            EnsureCapacity(size);
            int readed = stream.Read(buffer, 0, size);
            Debug.Assert(readed == size);

            return BitConverter.ToInt32(buffer, 0);
        }

        internal float GetFloat()
        {
            int size = sizeof(float);
            EnsureCapacity(size);
            int readed = stream.Read(buffer, 0, size);
            Debug.Assert(readed == size);

            return BitConverter.ToSingle(buffer, 0);
        }

        internal string GetString()
        {
            int byteCount = GetInt();
            
            EnsureCapacity(byteCount);
            int readed = stream.Read(buffer, 0, byteCount);
            Debug.Assert(readed == byteCount);

            return Encoding.UTF8.GetString(buffer, 0, byteCount);
        }

        internal string GetReference()
        {
            return GetString();
        }

        internal Vector3 GetVector3()
        {
            float x = GetFloat();
            float y = GetFloat();
            float z = GetFloat();

            return new Vector3(x, y, z);
        }

        internal List<string> GetReferenceList()
        {
            int count = GetInt();
            List<string> list = new List<string>();
            for(int i = 0; i < count; i++)
            {
                string id = GetReference();
                list.Add(id);
            }

            return list;
        }

    }
}
