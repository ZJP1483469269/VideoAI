using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLKJ.WEB;
using TLKJ.Utils;
using TLKJ.DAO;


public partial class import_service : PageEx
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        String cAppDir = Server.MapPath("~/");
        String cFileDir = cAppDir + "Upload\\";
        if (!Directory.Exists(cFileDir))
        {
            Directory.CreateDirectory(cFileDir);
        }
        cFileDir = cAppDir + "Upload\\temp\\";
        if (!Directory.Exists(cFileDir))
        {
            Directory.CreateDirectory(cFileDir);
        }
        String cFileName = cFileDir + "import.dat";
        if (File.Exists(cFileName))
        {
            File.Delete(cFileName);
        }
        String cFileText = null;
        int iCode = 0;
        try
        {
            this.Upload.PostedFile.SaveAs(cFileName);
            cFileText = FileLib.ReadTextFile(cFileName);
            List<ExportObject> rows = PackUtil.Explain(cFileText);
            iCode = PackUtil.UpdateDB(rows, cAppDir);
        }
        catch (Exception ex)
        {

        }
    }
}