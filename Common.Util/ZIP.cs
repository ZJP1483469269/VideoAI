using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using Ionic.Zip;

namespace TLKJ.Utils
{
    /// <summary>
    /// Zip 的摘要说明
    /// </summary>
    public class Zip
    {
        public Boolean ZipFile(string cFileName, string cExportFileName)
        { 
            try
            {
                ZipFile zExport = new ZipFile();
                zExport.AddFile(cFileName);
                zExport.Save(cExportFileName);
                return true;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                return false;
            }
        }

        public Boolean ZipDir(string cDir, string cExportFileName)
        {
            try
            {
                ZipFile zExport = new ZipFile();
                zExport.AddDirectory(cDir);
                zExport.Save(cExportFileName);
                return true;
            }
            catch (Exception ex)
            {
                log4net.WriteLogFile(ex.Message);
                return false;
            }
        }
    }
}