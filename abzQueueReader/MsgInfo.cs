using System;
using System.Collections.Generic;
using System.Text;

namespace abzQueueReader
{
    public struct MsgInfo
    {
        public MsgInfo(string i, string f, string s, int r, DateTime l)
        {
            id = i;
            free = f;
            size = s;
            retryCount = r;
            lastRetry = l;
        }
        private string id;
        public string ID { get { return id; } }
        private string free;
        public string Free { get { return free; } }
        private string size;
        public string Size { get { return size; } }
        private int retryCount;
        public int RetryCount { get { return retryCount; } }
        private DateTime lastRetry;
        public DateTime LastRetry { get { return lastRetry; } }
    }
}
