using System;
using System.ComponentModel;

namespace abzQueueReader
{
    public class CollectionNode : ABZNode
    {
        private string parts;
        public string Parts
        {
            get
            {
                return parts;
            }
        }

        private bool unrar;
        public bool Unrar
        {
            get
            {
                return unrar;
            }
        }

        private string md5;
        public string MD5
        {
            get
            {
                return md5;
            }
        }

        private string downloadFolder;
        public string DownloadFolder
        {
            get
            {
                return downloadFolder;
            }
        }

        private string unrarPassword;
        public string UnrarPassword
        {
            get
            {
                return unrarPassword;
            }
        }

        private string unrarFolder;
        public string UnrarFolder
        {
            get
            {
                return unrarFolder;
            }
        }

        private int status;
        public int Status
        {
            get
            {
                return status;
            }
        }

        private bool deleteRars;
        public bool DeleteRars
        {
            get
            {
                if (queueVersion >= 10)
                    return deleteRars;
                else
                    throw new NotSupportedException();
            }
        }

        private string execute;
        public string Execute
        {
            get
            {
                if (queueVersion >= 11)
                    return execute;
                else
                    throw new NotSupportedException();
            }
        }

        private BindingList<FileNode> files;
        public BindingList<FileNode> Files
        {
            get
            {
                if (files == null)
                    files = new BindingList<FileNode>();
                return files;
            }
        }

        public bool Paused
        {
            get
            {
                return Index == ABZQueueReader.COLLECTION_PAUSED;
            }
        }

        protected override void readData(FileReader sourceFile)
        {
            parts = sourceFile.readString();
            unrar = sourceFile.readBoolean();
            md5 = sourceFile.readString();
            downloadFolder = sourceFile.readString();
            unrarPassword = sourceFile.readString();
            unrarFolder = sourceFile.readString();
            status = sourceFile.readInt32();
            if (queueVersion >= 10)
                deleteRars = sourceFile.readBoolean();
            if (queueVersion >= 11)
                execute = sourceFile.readString();
        }

        public CollectionNode(ABZQueueReader sourceFile) : base(sourceFile) { }
    }
}