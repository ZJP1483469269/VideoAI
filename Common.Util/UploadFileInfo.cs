using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TLKJ.Utils
{

    public class UploadFileInfo
    {
        private String id;
        private String resid;
        private String orgid;
        private String filename;
        private String url;
        private String filedesc;
        private long createdatetime = DateUtils.getDayTimeNum();
        private long filelength;
        public String ID
        {
            get { return id; }
            set { id = value; }
        }
        public String ResID
        {
            get { return resid; }
            set { resid = value; }
        }
        public long FileLength
        {
            get { return filelength; }
            set { filelength = value; }
        }

        public String ORG_ID
        {
            get { return orgid; }
            set { orgid = value; }
        }
        public String FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        public String Ext
        {
            get
            {
                if (filename == null)
                    return null;
                else
                    return Path.GetExtension(filename).ToLower();
            }
        }
        public String FileDesc
        {
            get { return filedesc; }
            set { filedesc = value; }
        }
    }
}