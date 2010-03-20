using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace abzQueueReader
{
    public class FileNode : ABZNode
    {
        private UInt32 crc;
        public UInt32 CRC
        {
            get
            {
                return crc;
            }
        }

        private byte downloaded;
        public byte Downloaded
        {
            get
            {
                return downloaded;
            }
        }

        private byte incomplete;
        public byte Incomplete
        {
            get
            {
                return incomplete;
            }
        }

        private string parts;
        public string Parts
        {
            get
            {
                return parts;
            }
        }

        private string group;
        public string Group
        {
            get
            {
                return group;
            }
        }

        private string poster;
        public string Poster
        {
            get
            {
                return poster;
            }
        }

        private Int32 status;
        public Int32 Status
        {
            get
            {
                return status;
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

        private UInt32 msgParts;
        public UInt32 MsgParts
        {
            get
            {
                return msgParts;
            }
        }

        private BindingList<MsgInfo> availableMsgParts;
        public BindingList<MsgInfo> AvailableMsgParts
        {
            get
            {
                if (availableMsgParts == null)
                    availableMsgParts = new BindingList<MsgInfo>();
                return availableMsgParts;
            }
        }

        private bool decoding;
        public bool Decoding
        {
            get
            {
                return decoding;
            }
        }

        public bool Paused
        {
            get
            {
                return Index == ABZQueueReader.FILE_PAUSED;
            }
        }

        public FileNode(ABZQueueReader sourceFile) : base(sourceFile) { }
        public FileNode(FileReader fr, int fv) : base(fr, fv) { }

        protected override void readData(FileReader sourceFile)
        {
            crc = sourceFile.readUInt32();
            downloaded = sourceFile.readByte();
            incomplete = sourceFile.readByte();
            parts = sourceFile.readString();
            group = sourceFile.readString();
            poster = sourceFile.readString();
            status = sourceFile.readInt32();
            md5 = sourceFile.readString();
            msgParts = sourceFile.readUInt32();
            readAvailableMsgParts(sourceFile);
            decoding = sourceFile.readBoolean();
        }

        private void readAvailableMsgParts(FileReader sourceFile)
        {
            uint availablemsgcount = sourceFile.readUInt32();

            string[] ids = new string[availablemsgcount];
            string[] free = new string[availablemsgcount];
            string[] size = new string[availablemsgcount];
            int[] retc = new int[availablemsgcount];
            DateTime[] lrt = new DateTime[availablemsgcount];

            for (int i = 0; i < availablemsgcount; i++)
                ids[i] = sourceFile.readString();
            for (int i = 0; i < availablemsgcount; i++)
                free[i] = sourceFile.readString();
            for (int i = 0; i < availablemsgcount; i++)
                size[i] = sourceFile.readString();
            for (int i = 0; i < availablemsgcount; i++)
            {
                retc[i] = sourceFile.readInt32();
                lrt[i] = sourceFile.readPascalDate();
            }
            for (int i = 0; i < availablemsgcount; i++)
                AvailableMsgParts.Add(new MsgInfo(ids[i], free[i], size[i], retc[i], lrt[i]));
        }
    }
}