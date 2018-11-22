using System;
using System.IO;

using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class TEST_LeerExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Import();
    }

    private void Import()
    {

        //string x = Server.MapPath("exceldata.xls");

        string x = "C:\\Users\\windows10\\Desktop\\excel_aspnet\\" + "exceldata.xls";
        DataSet datos1 = new DataSet();
        OleDbDataAdapter adaptador1 = new OleDbDataAdapter(); 

        string cadenaCon = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + x + ";" + "Extended Properties=Excel 8.0;";

        OleDbConnection con = new OleDbConnection(cadenaCon);
        con.Open();

        adaptador1 = new OleDbDataAdapter("SELECT * FROM sierra", con);
        datos1 = new DataSet();
        adaptador1.Fill(datos1, "XLData");
        //rejilla1.DataSource = datos1.Tables[0];
        //rejilla1.DataBind();
        con.Close();


    }

}