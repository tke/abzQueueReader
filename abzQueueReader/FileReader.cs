using System;
using System.IO;
using System.Text;

namespace abzQueueReader
{
    public class FileReader
    {
        byte[] data;
        public byte[] Data { get { return data; } }
        int lastPos;

        public void loadFile(string filename)
        {
            data = File.ReadAllBytes(filename);
            lastPos = 0;
        }

        public void seek(int newPos)
        {
            lastPos = newPos;
        }

        private void checkForEndOfFile()
        {
            if (lastPos >= data.Length)
                throw new System.IO.EndOfStreamException();
        }

        public byte readByte()
        {
            return readByte(true);
        }
        public byte readByte(bool consume)
        {
            checkForEndOfFile();
            byte retVal = data[lastPos];
            if (consume) lastPos += 1;
            return retVal;
        }

        public bool readBoolean()
        {
            return readBoolean(true);
        }

        public bool readBoolean(bool consume)
        {
            checkForEndOfFile();
            bool retVal = BitConverter.ToBoolean(data, lastPos);
            if (consume) lastPos += 1;
            return retVal;
        }

        //public char readChar()
        //{
        //    return readChar(true);
        //}
        //public char readChar(bool consume)
        //{
        //    checkForEndOfFile();
        //    char retVal = BitConverter.ToChar(data, lastPos);
        //    if (consume) lastPos += 2;
        //    return retVal;
        //}

        public double readDouble()
        {
            return readDouble(true);
        }
        public double readDouble(bool consume)
        {
            checkForEndOfFile();
            double retVal = BitConverter.ToDouble(data, lastPos);
            if (consume) lastPos += 8;
            return retVal;
        }

        //public short readInt16()
        //{
        //    return readInt16(true);
        //}
        //public short readInt16(bool consume)
        //{
        //    checkForEndOfFile();
        //    short retVal = BitConverter.ToInt16(data, lastPos);
        //    if (consume) lastPos += 2;
        //    return retVal;
        //}

        public int readInt32()
        {
            return readInt32(true);
        }
        public int readInt32(bool consume)
        {
            checkForEndOfFile();
            int retVal = BitConverter.ToInt32(data, lastPos);
            if (consume) lastPos += 4;
            return retVal;
        }

        //public long readInt64()
        //{
        //    return readInt64(true);
        //}
        //public long readInt64(bool consume)
        //{
        //    checkForEndOfFile();
        //    long retVal = BitConverter.ToInt64(data, lastPos);
        //    if (consume) lastPos += 8;
        //    return retVal;
        //}

        //public float readSingle()
        //{
        //    return readSingle(true);
        //}
        //public float readSingle(bool consume)
        //{
        //    checkForEndOfFile();
        //    float retVal = BitConverter.ToSingle(data, lastPos);
        //    if (consume) lastPos += 4;
        //    return retVal;
        //}

        public string readString()
        {
            return readString(Encoding.Default, true);
        }
        public string readString(Encoding enc)
        {
            return readString(enc, true);
        }
        public string readString(bool consume)
        {
            return readString(Encoding.Default, consume);
        }
        public string readString(Encoding enc, bool consume)
        {
            checkForEndOfFile();
            int strLen = readInt32(consume);
            string retVal = enc.GetString(data, lastPos + (consume ? 0 : 4), strLen);
            if (consume) lastPos += strLen;
            return retVal;
        }

        //public ushort readUInt16()
        //{
        //    return readUInt16(true);
        //}
        //public ushort readUInt16(bool consume)
        //{
        //    checkForEndOfFile();
        //    ushort retVal = BitConverter.ToUInt16(data, lastPos);
        //    if (consume) lastPos += 2;
        //    return retVal;
        //}

        public uint readUInt32()
        {
            return readUInt32(true);
        }
        public uint readUInt32(bool consume)
        {
            checkForEndOfFile();
            uint retVal = BitConverter.ToUInt32(data, lastPos);
            if (consume) lastPos += 4;
            return retVal;
        }

        public ulong readUInt64()
        {
            return readUInt64(true);
        }
        public ulong readUInt64(bool consume)
        {
            checkForEndOfFile();
            ulong retVal = BitConverter.ToUInt64(data, lastPos);
            if (consume) lastPos += 8;
            return retVal;
        }

        public DateTime readPascalDate()
        {
            return readPascalDate(true);
        }
        public DateTime readPascalDate(bool consume)
        {
            checkForEndOfFile();
            double days = readDouble(consume);
            DateTime retVal = new DateTime(1899, 12, 30, 0, 0, 0) + TimeSpan.FromDays(days);
            return retVal;
        }
    }
}