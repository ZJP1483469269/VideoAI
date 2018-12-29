
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TLKJ.Utils;
using Renci.SshNet;
using TLKJAI;
using System.Threading;

namespace TLKJ_IVS
{
    public class UploadTask
    {       
        private static UploadTask instance = null;

        private UploadTask()
        {
            
        }

        public static UploadTask getInstance()
        {
            if (instance == null)
            {
                instance = new UploadTask();
            }
            return instance;
        }
          
        public static void Execute()
        {
            AnalyseTask Task = AnalyseTask.getInstance();
            int iANALYSE_ALLOW = INIConfig.ReadInt("ANALYSE", "DFS_ALLOW");
            while (!ApplicationEvent.isUploadAbort)
            {
                Boolean Find_File_Flag = false;
                log4net.WriteLogFile("UploadTask.Execute..", LogType.DEBUG);
                String cFileName = null;
                
                if (iANALYSE_ALLOW > 0)
                {
                    cFileName = Task.getAnalyseFile(); 
                    if (String.IsNullOrWhiteSpace(cFileName))
                    {
                        log4net.WriteLogFile("UploadTask.Execute..未发现图片", LogType.DEBUG);
                    }
                    else
                    {
                        Task.UpdateAnalyseFile(cFileName);
                        if (!Find_File_Flag)
                        {
                            Find_File_Flag = true;
                        }
                    }
                }

                if (Find_File_Flag)
                {
                    try
                    {
                        Thread.Sleep(500);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile("UploadTask.Sleep." + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        Thread.Sleep(1000 * 3);
                    }
                    catch (Exception ex)
                    {
                        log4net.WriteLogFile("UploadTask.Sleep." + ex.Message);
                    }
                }
            }
        } 
    }
}
