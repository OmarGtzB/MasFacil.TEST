﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Telerik.Web.UI;
using System.Data.OleDb;
using Telerik.Windows;
using System.Web.UI.HtmlControls;
using System.Data.Entity;
using System.Globalization;
using Telerik.Web.UI.GridExcelBuilder;
using Telerik.Web.UI.ExportInfrastructure;


public partial class DC_ExportaLP : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    wsRpt.Service oWSRpt = new wsRpt.Service();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private int Pag_valTrue;
    private int Pag_valFalse;
    private string ext;
    // DataTable customerOrders = new DataTable();

    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        arregloExport.Value = "";
        imgImport.ImageUrl = null;

        pnlTxt.Visible = false;
        pnlExcel.Visible = false;
    }
    
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (arregloExport.Value != "")
        {
            ExportListaPrecioDetalle();
        }
        
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        pnlInicio.Visible = true;
        pnlTxt.Visible = false;
        pnlExcel.Visible = false;
        rbtnTxt.Checked = false;
        rbtExcel.Checked = false;

        arregloExport.Value = "";
        arregloExport.Value = "";

        imgImport.ImageUrl = null;
        imgImportExcel.ImageUrl = null;


        //rbtExcel

    }

    protected void rBtnCancelarExcel_Click(object sender, ImageButtonClickEventArgs e)
    {
        pnlInicio.Visible = true;
        pnlTxt.Visible = false;
        pnlExcel.Visible = false;

        rbtnTxt.Checked = false;

        rbtExcel.Checked = false;
    }

    protected void rBtnGuardarExcel_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (arregloExport.Value != "")
        {
            LeeExcel();
        }
    }

    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".txt" || ext == ".TXT")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                string valor = Convert.ToBase64String(data);
                arregloExport.Value = valor;
                imgImport.ImageUrl = "~/Imagenes/IcoAyuda/txt.jpg";
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                //ShowAlert(sMSGTip, sResult);
                arregloExport.Value = "";
                imgImport.ImageUrl = "";
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("2", ex.ToString());
        }
    }

    protected void RadAsyncUpload1_FileUploadedExcel(object sender, FileUploadedEventArgs e)
    {
        try
        {
            ext = e.File.GetExtension();
            HdvalExt.Value = ext;
            if (ext == ".xlsx" || ext == ".xls")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                //imgPerfil.DataValue = data;
                string valor = Convert.ToBase64String(data);

                arregloExport.Value = valor;
                imgImportExcel.ImageUrl = "~/Imagenes/IcoAyuda/icoExcel.png";
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                //ShowAlert(sMSGTip, sResult);
                arregloExport.Value = "";
                imgImportExcel.ImageUrl = "";
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("2", ex.ToString());
        }
    }

    protected void rbtnTxt_CheckedChanged(object sender, EventArgs e)
    {
        MuestraPnl();
    }

    protected void rbtExcel_CheckedChanged(object sender, EventArgs e)
    {
        MuestraPnl();
    }


    #endregion

    #region FUNCIONES


    private int GuardaDatosExcelDetalle(DataTable dsTransDet, string rutaexcel)
    {
        int valtrue = 0, valfalse = 0, sum = 0;

        try
        {
            DataSet ds = new DataSet();

            foreach (DataRow drConfCpto in dsTransDet.Rows)
            {

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ListaPreciosDetalle";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, "5");
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["artCve"]));
                ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["lisPreCve"]));
                ProcBD.AgregarParametrosProcedimiento("@lisPreSec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["lisPreSec"]));

                if (Convert.ToString(drConfCpto["lisPreCanMin"]) != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@lisPreCanMin", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["lisPreCanMin"]));
                }

                if (Convert.ToString(drConfCpto["lisPreCanMax"]) != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@lisPreCanMax", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["lisPreCanMax"]));
                }

                if (Convert.ToString(drConfCpto["lisPrecio"]) != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@lisPrecio", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["lisPrecio"]));
                }

                ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, Convert.ToString(drConfCpto["uniMedCve"]));
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));

                if (Convert.ToString(drConfCpto["DesCve"]) != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["DesCve"]));
                }

                if (Convert.ToString(drConfCpto["impCve"]) != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, Convert.ToString(drConfCpto["impCve"]));
                }


                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    if (sEjecEstatus == "1")
                    {

                        valtrue = Convert.ToInt32(CantItemsElimTrue.Value);
                        valtrue += 1;
                        CantItemsElimTrue.Value = valtrue.ToString();

                    }
                    else
                    {
                        valfalse = Convert.ToInt32(CantItemsElimTrue.Value);
                        valfalse += 1;
                        CantItemsElimTrue.Value = valfalse.ToString();
                    }
                }
            }
            //string script = "window.close();";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarpagina", script, true);
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        //File.Delete(rutaexcel);


        Pag_valTrue = valtrue;
        Pag_valFalse = valfalse;

        arregloExport.Value = "";
        imgImport.ImageUrl = null;
        imgImport.DataBind();

        return 0;


    }

    private string GeneraExcel()
    {

        string UrlPdf;
        UrlPdf = "BULK-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "." + ext;

        byte[] bytes = Convert.FromBase64String(arregloExport.Value);

        string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;

        System.IO.File.WriteAllBytes(apppath, bytes);

        return apppath;

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void ShowAlertExcel(string Estatus, string MSG)
    {
        RadWindowManager1.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }




    #endregion

    #region METODOS

    public void ExportListaPrecioDetalle()
    {
        if (arregloExport.Value != "")
        {
            try
            {
                string UrlPdf;
                UrlPdf = "BULK-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".txt";

                byte[] bytes = Convert.FromBase64String(arregloExport.Value);

                string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;

                System.IO.File.WriteAllBytes(apppath, bytes);

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ListaPreciosImport";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@path", DbType.String, 300, ParameterDirection.Input, apppath);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {

                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    ShowAlert(sEjecEstatus, sEjecMSG);
                    arregloExport.Value = "";
                    imgImport.ImageUrl = null;
                    imgImport.DataBind();
                }

                File.Delete(apppath);

            }
            catch (Exception ex)
            {
                ShowAlert("2", ex.ToString());
            }
            arregloExport.Value = "";
        }

    }

    public void ExportListaPrecio()
    {
        if (arregloExport.Value != "")
        {
            try
            {
                string UrlPdf;
                UrlPdf = "BULK-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".txt";

                byte[] bytes = Convert.FromBase64String(arregloExport.Value);
                string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
                System.IO.File.WriteAllBytes(apppath, bytes);

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ListaPreciosImport";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@path", DbType.String, 300, ParameterDirection.Input, apppath);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    ShowAlert(sEjecEstatus, sEjecMSG);
                    arregloExport.Value = "";
                    imgImportExcel.ImageUrl = null;
                    imgImportExcel.DataBind();
                }
                File.Delete(apppath);
            }
            catch (Exception ex)
            {
                ShowAlert("2", ex.ToString());
            }
            arregloExport.Value = "";
        }

    }

    private void MuestraPnl()
    {

        if (rbtnTxt.Checked == true)
        {
            pnlTxt.Visible = true;
            pnlInicio.Visible = false;

        }
        else if (rbtExcel.Checked == true)
        {

            pnlExcel.Visible = true;
            pnlInicio.Visible = false;
        }

    }

    private void LeeExcel()
    {

        string rutaExcel = "";
        rutaExcel += GeneraExcel();
        OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder();
        cb.DataSource = rutaExcel;

        if (HdvalExt.Value == ".XLS" || HdvalExt.Value == ".xls")
        {
            cb.Provider = "Microsoft.Jet.OLEDB.4.0";
            cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
        }
        else if (HdvalExt.Value == ".XLSX" || HdvalExt.Value == ".xlsx")
        {
            cb.Provider = "Microsoft.ACE.OLEDB.12.0";
            cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
        }

        //Lectura de datos:

        try
        {
            DataTable dt = new DataTable("Datos");
            using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
            {
                //Abrimos la conexión
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [Datos$]";
                    //Guardamos los datos en el DataTable
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    GuardaDatosExcelDetalle(dt, rutaExcel);
                    EnviaMsg();

                    arregloExport.Value = "";
                    imgImportExcel.ImageUrl = null;
                    imgImportExcel.DataBind();

                }
                //Cerramos la conexión
                conn.Close();

            }
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }

        try
        {
            File.Delete(rutaExcel);
        }
        catch (Exception ex)
        {
            ShowAlertExcel("2",ex.ToString());
        }
        

        ////Creación de Hojas
        //using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))

        //{
        //    //Abrimos la conexión
        //    conn.Open();
        //    //Creamos la ficha
        //    using (OleDbCommand cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = @"CREATE TABLE [Coches]
        //                    (
        //                        IdCoche INTEGER,
        //                        Marca TEXT,
        //                        Modelo TEXT,
        //                        FxCompra DATETIME
        //                    )";
        //        cmd.ExecuteNonQuery();
        //    }
        //    //Cerramos la conexión
        //    conn.Close();
        //}



        //Inserción de datos
        //using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
        //{
        //    //Abrimos la conexión
        //    conn.Open();
        //    //Creamos la ficha
        //    using (OleDbCommand cmd = conn.CreateCommand())
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = @"INSERT INTO [Coches$]

        //                    (  IdCoche,
        //                        Marca,
        //                        Modelo,
        //                        FxCompra   
        //                    ) 
        //                    VALUES
        //                    (
        //                    @IdCoche,
        //                    @Marca,
        //                    @Modelo,
        //                    @FxCompra,
        //                    )";

        //        cmd.Parameters.AddWithValue("@IdCoche", 1);
        //        cmd.Parameters.AddWithValue("@Marca", "Ferrari");
        //        cmd.Parameters.AddWithValue("@Modelo", "599 GTB");
        //        cmd.Parameters.AddWithValue("@FxCompra", DateTime.Now);
        //        cmd.ExecuteNonQuery();
        //    }

        //    //Cerramos la conexión
        //    conn.Close();
        //}

        //Excel.Application xlApp;
        //Excel.Workbook xlWorkBook;
        //Excel.Worksheet xlWorkSheet;
        //Excel.Range range;

        //string str;
        //int rCnt;
        //int cCnt;
        //int rw = 0;
        //int cl = 0;

        //string path = "";
        //path += GeneraExcel();
        //xlApp = new Excel.Application();
        //xlWorkBook = xlApp.Workbooks.Open(@path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //range = xlWorkSheet.UsedRange;
        //rw = range.Rows.Count; //5

        ////cl = range.Columns.Count; //9

        //cl = 7;

        //for (rCnt = 1; rCnt <= rw; rCnt++)
        //{
        //    for (cCnt = 1; cCnt <= cl; cCnt++)
        //    {
        //        str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
        //        if (str != null)
        //        {
        //            //MessageBox.Show(str);
        //            CargaDsLP(str, rw);
        //        }
        //    }
        //}

        //EnviaMsg();

        //xlWorkBook.Close(true, null, null);
        //xlApp.Quit();

        //Marshal.ReleaseComObject(xlWorkSheet);
        //Marshal.ReleaseComObject(xlWorkBook);
        //Marshal.ReleaseComObject(xlApp);

        //File.Delete(@path);

        //var Workbook = new XLWorkbook();
        //foreach (var worksheet in Workbook.Worksheets(@"C:\ExcelFile.xlsx"))
        //{
        //    foreach (var row in worksheet.Rows)
        //    {
        //        foreach (var cell in row.Cells)
        //        {

        //        }
        //    }
        //}


    }

    private void EnviaMsg()
    {

        string sEstatusAlert = "";
        string sMsgAlert = "Registros Agregados: " + " " + Pag_valTrue.ToString();
        sMsgAlert += "</br>";
        sMsgAlert += "Registros No Agregados: " + " " + Pag_valFalse.ToString();


        if (Pag_valTrue > 0)
        {
            sEstatusAlert = "1";
        }
        else
        {
            sEstatusAlert = "2";
        }

        ShowAlertExcel(sEstatusAlert, sMsgAlert);

    }
    #endregion



























}