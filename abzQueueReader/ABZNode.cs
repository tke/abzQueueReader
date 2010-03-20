using System;

namespace abzQueueReader
{
    public abstract class ABZNode
    {
        protected int queueVersion;

        private byte index;
        public byte Index
        {
            get
            {
                return index;
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
        }

        private string size;
        public string Size
        {
            get
            {
                return size;
            }
        }

        private UInt64 realSize;
        public UInt64 RealSize
        {
            get
            {
                return realSize;
            }
        }

        private UInt64 finishedSize;
        public UInt64 FinishedSize
        {
            get
            {
                return finishedSize;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        internal ABZNode(ABZQueueReader sourceFile): this(sourceFile, sourceFile.FileVersion)     {        }

        internal ABZNode(FileReader sourceFile, int fv)
        {
            queueVersion = fv;
            this.index = sourceFile.readByte();
            this.title = sourceFile.readString();
            this.size = sourceFile.readString();
            this.realSize = sourceFile.readUInt64();
            this.finishedSize = sourceFile.readUInt64();
            this.date = sourceFile.readPascalDate();
            this.readData(sourceFile);
        }

        protected abstract void readData(FileReader sourceFile);
    }
}