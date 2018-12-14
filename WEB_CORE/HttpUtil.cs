using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using TLKJ.Utils;

namespace TLKJ.WebSys
{
    public class HttpUtil
    {
        public string HttpPost(String cUrl, Dictionary<string, string> vData, String[] FileList)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            //1.HttpWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(cUrl);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                String stringKeyHeader = "\r\n--" + boundary +
                                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                    "\r\n\r\n{1}\r\n";
                foreach (byte[] formitembytes in from string key in vData.Keys
                                                 select string.Format(stringKeyHeader, key, vData[key])
                                                     into formitem
                                                     select Encoding.UTF8.GetBytes(formitem))
                {
                    stream.Write(formitembytes, 0, formitembytes.Length);
                }

                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                for (int i = 0; (FileList != null) && (i < FileList.Length); i++)
                {
                    stream.Write(boundarybytes, 0, boundarybytes.Length);
                    string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(FileList[i]));
                    byte[] headerbytes = UTF8Encoding.Default.GetBytes(header);
                    stream.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(FileList[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                stream.Write(endbytes, 0, endbytes.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }

        }
        public string HttpPost(String cUrl, Dictionary<string, string> vData)
        {
            return HttpPost(cUrl, vData, null);

        }

        public string HttpPostFile(string cUrl, string cFileName)
        {
            String[] FileList = new string[1];
            FileList[0] = cFileName;
            return HttpPostFile(cUrl, FileList);
        }

        public string HttpPostFile(string cUrl, string[] FileList)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            //1.HttpWebRequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(cUrl);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                for (int i = 0; i < FileList.Length; i++)
                {
                    stream.Write(boundarybytes, 0, boundarybytes.Length);
                    string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(FileList[i]));
                    byte[] headerbytes = UTF8Encoding.Default.GetBytes(header);
                    stream.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(FileList[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                stream.Write(endbytes, 0, endbytes.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }

        private CacheDB vCache = new CacheDB();

        public string HttpGet(string cUrl, Dictionary<String, String> vParmList)
        {
            String cQueryString = cUrl;
            foreach (KeyValuePair<string, string> kv in vParmList)
            {
                if (cQueryString.IndexOf("?") > -1)
                {
                    cQueryString = cQueryString + "&" + kv.Key + "=" + StringEx.getString(kv.Value);
                }
                else
                {
                    cQueryString = cQueryString + "?" + kv.Key + "=" + StringEx.getString(kv.Value);
                }
            }

            String cUrlMD = MDUtil.Get32MD5(cQueryString);
            Object vObj = CacheDB.GetCache(cUrlMD);
            String cPageVal = "";
            if (vObj != null)
            {
                cPageVal = (String)vObj;
            }
            else
            {


                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(cQueryString);
                req.Method = "GET";
                req.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream myResponseStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                cPageVal = sr.ReadToEnd();
                sr.Close();
                myResponseStream.Close();
                CacheDB.SetCache(cUrlMD, cPageVal);
            }
            return cPageVal;
        }
    }
}
