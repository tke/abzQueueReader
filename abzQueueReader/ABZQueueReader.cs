using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace abzQueueReader
{
    public class ABZQueueReader : FileReader
    {
        public const byte FILE_PAUSED = 0x02;
        public const byte FILE_NODE = 0x01;
        public const byte COLLECTION_PAUSED = 0x63;
        public const byte COLLECTION_NODE = 0x00;


        static readonly Int32[] SupportedVersions = new Int32[] { 9, 10, 11 };
        private string currentFileName;

        private int fileVersion;
        public int FileVersion { get { return fileVersion; } }

        private UInt64 bytesTransfered;
        public UInt64 BytesTransfered { get { return bytesTransfered; } }

        private BindingList<CollectionNode> collections;
        public BindingList<CollectionNode> Collections
        {
            get
            {
                if (collections == null)
                    collections = new BindingList<CollectionNode>();
                return collections;
            }
        }

        public new void loadFile(string fileName)
        {
            currentFileName = fileName;
            refreshQueue();
        }

        public void refreshQueue()
        {
            base.loadFile(currentFileName);
            Collections.Clear();

            if (Encoding.Default.GetString(this.Data, 0, 3) != "Ver")
                throw new Exception("Invalid file header.");
            this.seek(3);

            fileVersion = this.readByte();

            if (!IsSupported())
                throw new Exception("Unsupported file version '" + fileVersion + "'.");

            bytesTransfered = this.readUInt64();
            CollectionNode currentCollectionNode = null;
            byte nextIndex = 0xFF;

            while (true)
            {
                try { nextIndex = this.readByte(false); }
                catch (EndOfStreamException) { break; }
                if (nextIndex == 0xFF)
                    return;
                else if (nextIndex == COLLECTION_PAUSED || nextIndex == COLLECTION_NODE)
                {
                    currentCollectionNode = new CollectionNode(this);
                    collections.Add(currentCollectionNode);
                }
                else if (nextIndex == FILE_PAUSED || nextIndex == FILE_NODE)
                    currentCollectionNode.Files.Add(new FileNode(this));
                else throw new Exception("Unknown index type found");
            }
        }

        private bool IsSupported()
        {
            foreach (int i in SupportedVersions)
                if (fileVersion == i) return true;
            return false;
        }
    }
}