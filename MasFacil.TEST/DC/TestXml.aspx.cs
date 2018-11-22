using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;
using System.Data.Entity;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

public partial class DC_TestXml : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.Clean FNCtlsClear = new MGMControls.Clean();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private DataSet DS_XML;
    DataTable dtInfoXML;
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                //obtenDatosXML();
            }
        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }



    private void obtenDatosXML()
    {
        try
        {
            //Obtener datos del XML Para llenado de Reporte
            DS_XML = DSInfXML();

            EjecutaSpAcciones(DS_XML);
            CptoDS(DS_XML);


            //Crear tabla dinamina
            CreaDataTableDinamica(DS_XML);
            //Llenar tabla dinamina
           LlenaDataTableDinamica(DS_XML);
        }
        catch (Exception ex)
        {
            //GENERA LOG Error
           // CrearArchivoLog("Error  ", "Metodo ObtenDatosXML. " + ex.Message.ToString(), DateTime.Now);
        }
    }

    private void CreaDataTableDinamica(DataSet DSInf)
    {
        dtInfoXML = new DataTable();
        dtInfoXML.Columns.Add("idRecibo", System.Type.GetType("System.String"));
        for (int iTable = 0; iTable <= Convert.ToInt32(DSInf.Tables.Count - 1); iTable++)
        {
            string sTableName = DSInf.Tables[iTable].TableName;
            switch (sTableName)
            {
                case "Emisor":
                case "Nomina":
                case "Receptor":
                case "Comprobante":
                case "Concepto":
                case "TimbreFiscalDigital":
                case "DomicilioFiscal":
                    if (DSInf.Tables[sTableName].Rows.Count > 0)
                    {
                        for (int iColum = 0; iColum <= Convert.ToInt32(DSInf.Tables[sTableName].Columns.Count - 1); iColum++)
                        {
                            string sColumnName = DSInf.Tables[sTableName].Columns[iColum].ColumnName;
                            string sColum_dtInfoXML = sTableName + "_" + sColumnName;
                            dtInfoXML.Columns.Add(sColum_dtInfoXML, System.Type.GetType("System.String"));
                        }
                    }
                    break;
            }
        }
        dtInfoXML.Clear();
    }

    private DataSet DSInfXML()
    {
        //string RutaName_FileXML = Server.MapPath("~/Temp") + "\\" + "ejemploCFDIv33.xml";
        string RutaName_FileXML = Server.MapPath("~/Temp") + "\\" + "AAA010101AAA_95.xml";

        DataSet DS = new DataSet();
        try
        {
            string myXMLfile = RutaName_FileXML;
            System.IO.FileStream fsReadXml = new System.IO.FileStream(myXMLfile, System.IO.FileMode.Open);
            try
            {
                DS.ReadXml(fsReadXml);
            }
            catch (Exception ex)
            {
                //GENERA LOG Error
                //CrearArchivoLog("Error  ", "No se recupero los datos del archivo XML. " + ex.Message.ToString(), DateTime.Now);
            }
            finally
            {
                fsReadXml.Close();
            }
        }
        catch (Exception ex)
        {
            //GENERA LOG Error
            //CrearArchivoLog("Error  ", "No se recupero los datos del archivo XML. " + ex.Message.ToString(), DateTime.Now);
        }
        return DS;
    }


    private void LlenaDataTableDinamica(DataSet DSInf)
    {


        string sDomicilioFiscal = "";
        string sCalle = "";
        string sNoExterior = "";
        string sNoInterior = "";
        string sLocalidad = "";
        string sMunicipio = "";
        string sEstado = "";
        string sPais = "";
        string sCodigoPostal = "";

        DataRow dr_InfXML;
        dr_InfXML = dtInfoXML.NewRow();
        dr_InfXML[0] = "0";
        dtInfoXML.Rows.Add(dr_InfXML);

        for (int iTable = 0; iTable <= Convert.ToInt32(DSInf.Tables.Count - 1); iTable++)
        {
            string sTableName = DSInf.Tables[iTable].TableName;
            switch (sTableName)
            {
                case "Emisor":
                case "Nomina":
                case "Receptor":
                case "Comprobante":
                case "Concepto":
                case "TimbreFiscalDigital":
                case "DomicilioFiscal":
                    if (DSInf.Tables[sTableName].Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = DSInf.Tables[sTableName].Rows[0];
                        for (int iColum = 0; iColum <= Convert.ToInt32(DSInf.Tables[sTableName].Columns.Count - 1); iColum++)
                        {
                            string sColumnName = DSInf.Tables[sTableName].Columns[iColum].ColumnName;
                            string sColum_dtInfoXML = sTableName + "_" + sColumnName;
                            dr_InfXML[sColum_dtInfoXML] = Convert.ToString(dr[sColumnName]);


                            if (sTableName == "DomicilioFiscal")
                            {
                                switch (sColumnName)
                                {
                                    case "calle":
                                        sCalle = Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "noExterior":
                                        sNoExterior = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "noInterior":
                                        sNoInterior = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "localidad":
                                        sLocalidad = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "municipio":
                                        sMunicipio = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "estado":
                                        sEstado = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "pais":
                                        sPais = " " + Convert.ToString(dr[sColumnName]);
                                        break;

                                    case "codigoPostal":
                                        sCodigoPostal = " C.P. " + Convert.ToString(dr[sColumnName]);
                                        break;
                                }
                            }
                        }
                        sDomicilioFiscal = sCalle + sNoExterior + sNoInterior + sMunicipio + sPais + sEstado + sCodigoPostal;
                    }
                    break;
            }
        }
    }

    private DataTable dtDetalleCtosRecibo(string sNombretabla)
    {
        //Crea Tabla de paso para Percepciones o de ducciones
        DataTable dtResultado;
        dtResultado = new DataTable();

        //Validar que exista la tabla Percepcion / Deduccion
        if (DS_XML.Tables.Contains(sNombretabla) == true)
        {

            if (DS_XML.Tables[sNombretabla].Rows.Count > 0)
            {
                for (int iColum = 0; iColum <= Convert.ToInt32(DS_XML.Tables[sNombretabla].Columns.Count - 1); iColum++)
                {
                    string sColumnName = DS_XML.Tables[sNombretabla].Columns[iColum].ColumnName;
                    dtResultado.Columns.Add(sColumnName, System.Type.GetType("System.String"));
                }
                dtResultado.Columns.Add("Tiempo", System.Type.GetType("System.String"));
                dtResultado.Columns.Add("Importe", System.Type.GetType("System.String"));
            }
            dtResultado.Clear();


            //LLenar Tabla de paso para Percepciones o de ducciones
            if (DS_XML.Tables[sNombretabla].Rows.Count > 0)
            {
                for (int iFila = 0; iFila <= Convert.ToInt32(DS_XML.Tables[sNombretabla].Rows.Count - 1); iFila++)
                {
                    DataRow drResultado;
                    drResultado = dtResultado.NewRow();
                    DataRow drNewFila;
                    drNewFila = DS_XML.Tables[sNombretabla].Rows[iFila];
                    for (int iColum = 0; iColum <= Convert.ToInt32(DS_XML.Tables[sNombretabla].Columns.Count - 1); iColum++)
                    {
                        string sColumnName = DS_XML.Tables[sNombretabla].Columns[iColum].ColumnName;
                        if (sColumnName == "Concepto")
                        {
                            String[] sNomConcepto = Convert.ToString(drNewFila[sColumnName]).Split('/');
                            if (sNomConcepto.Length == 2)
                            {
                                drResultado[sColumnName] = sNomConcepto[0].ToString().Trim();
                                drResultado["Tiempo"] = sNomConcepto[1].ToString().Trim();
                            }
                            else
                            {
                                drResultado[sColumnName] = drNewFila[sColumnName];
                            }
                        }
                        else
                        {
                            drResultado[sColumnName] = drNewFila[sColumnName];
                        }
                    }
                    drResultado["Importe"] = (Convert.ToDouble(drNewFila["ImporteGravado"]) + Convert.ToDouble(drNewFila["ImporteExento"]));
                    dtResultado.Rows.Add(drResultado);
                }
            }

        }
        return dtResultado;
    }

    private void EjecutaSpAcciones(DataSet ValCFDI)
    {
        try
        {

            string maUser = LM.sValSess(this.Page, 1);

            DataSet ds = new DataSet();
            
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SATCFDIComprobante_Paso";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);

            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int32, 0, ParameterDirection.Input, 3698);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cSerie", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Serie"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cFolio", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Folio"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cFecha", DbType.String, 100, ParameterDirection.Input, Convert.ToString(ValCFDI.Tables["Comprobante"].Rows[0]["Fecha"]));


            ProcBD.AgregarParametrosProcedimiento("@cSello", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Sello"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cFormaPago", DbType.String, 2, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["FormaPago"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cMoneda", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Moneda"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cTipoCambio", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["TipoCambio"]));
            ProcBD.AgregarParametrosProcedimiento("@cTotal", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Total"]));

            ProcBD.AgregarParametrosProcedimiento("@cTipoDeComprobante", DbType.String, 1, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["TipodeComprobante"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cMetodoPago", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["MetodoPago"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cLugarExpedicion", DbType.String, 10, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["LugarExpedicion"].ToString());

            ProcBD.AgregarParametrosProcedimiento("@cNoCertificado", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["NoCertificado"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cCertificado", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["Certificado"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cConfirmacion", DbType.String, 100, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@cSubtotal", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Subtotal"]));
            ProcBD.AgregarParametrosProcedimiento("@cCondicionesDePago", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["Comprobante"].Rows[0]["CondicionesDePago"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cDescuento", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Comprobante"].Rows[0]["Descuento"]));


            //EMISOR
            ProcBD.AgregarParametrosProcedimiento("@cEmiRFC", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["Rfc"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cEmiNombre", DbType.String, 300, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["Nombre"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cEmiRegimenFiscal", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Emisor"].Rows[0]["RegimenFiscal"].ToString());
          



            ////Receptor
            ProcBD.AgregarParametrosProcedimiento("@cRecRFC", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["Rfc"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cRecNombre", DbType.String, 300, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["Nombre"].ToString());
            //ProcBD.AgregarParametrosProcedimiento("@cRecRecidenciaFiscal", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["ResidenciaFiscal"].ToString());
            //ProcBD.AgregarParametrosProcedimiento("@cRecNumRegIdTrib", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["NumRegIdTrib"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cRecUsoCFDI", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["Receptor"].Rows[0]["UsoCFDI"].ToString());
            
            //USUARIO
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);

            //TimbreFiscalDigital
            ProcBD.AgregarParametrosProcedimiento("@cComTFDUUID", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["UUID"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cComTFDSelloSAT", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["SelloSAT"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cComTFDSelloCFD", DbType.String, 8000, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["SelloCFD"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cComTFDRfcProvCertif", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["RfcProvCertif"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cComTFDNoCertificadoSAT", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["NoCertificadoSAT"].ToString());
            ProcBD.AgregarParametrosProcedimiento("@cComTFDFechaTimbrado", DbType.String, 100, ParameterDirection.Input, ValCFDI.Tables["TimbreFiscalDigital"].Rows[0]["FechaTimbrado"].ToString());





            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                //ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    //InicioPagina();
                    // InicioAlt();
                    
                }
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
            //MessageBox.Show(ex.ToString());
        }
    }






    private void CptoDS(DataSet ValCFDI) 
    {
        string maUser = LM.sValSess(this.Page, 1);
        try
        {
            int val = 0;
            foreach (DataRow row in ValCFDI.Tables["Concepto"].Rows)
            {
                

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_SATCFDIComprobanteConcepto_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int32, 0, ParameterDirection.Input, 3698);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@cCptoClaveProdServ", DbType.String, 8, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["ClaveProdServ"]);
                ProcBD.AgregarParametrosProcedimiento("@cCptoNoIdentificacion", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["NoIdentificacion"]);
                ProcBD.AgregarParametrosProcedimiento("@cCptoCantidad", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(ValCFDI.Tables["Concepto"].Rows[val]["Cantidad"]));
                ProcBD.AgregarParametrosProcedimiento("@cCptoClaveUnidad", DbType.String, 3, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["ClaveUnidad"]);
                ProcBD.AgregarParametrosProcedimiento("@cCptoUnidad", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["Unidad"]);
                ProcBD.AgregarParametrosProcedimiento("@cCptoDescripcion", DbType.String, 50, ParameterDirection.Input, ValCFDI.Tables["Concepto"].Rows[val]["Descripcion"]);
                ProcBD.AgregarParametrosProcedimiento("@cCptoValorUnitario", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal( ValCFDI.Tables["Concepto"].Rows[val]["ValorUnitario"]));
                ProcBD.AgregarParametrosProcedimiento("@cCptoImporte", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal (ValCFDI.Tables["Concepto"].Rows[val]["Importe"]));
                ProcBD.AgregarParametrosProcedimiento("@cCptoDescuento", DbType.Decimal, 0,  ParameterDirection.Input, Convert.ToDecimal( ValCFDI.Tables["Concepto"].Rows[val]["Descuento"]));
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                val++;

            }


        //    if (FnValAdoNet.bDSIsFill(ds))
        //{
        //    string sEjecEstatus, sEjecMSG = "";
        //    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
        //    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

        //    //ShowAlert(sEjecEstatus, sEjecMSG);
        //    if (sEjecEstatus == "1")
        //    {
        //        //InicioPagina();
        //        // InicioAlt();

        //    }
        //}

    }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
            //MessageBox.Show(ex.ToString());
        }
    }


    protected void btnVisializaXML_Click(object sender, EventArgs e)
    {
        DataSet dsDatosGrales = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDocumento";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, 3044);
        dsDatosGrales = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataSet dsDatos = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_SATCFDIComprobante_Paso";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD1.AgregarParametrosProcedimiento("@docRegId", DbType.String, 10, ParameterDirection.Input, 3044);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        dsDatos = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        byte[] bytes = oWSRpt.byte_FormatoDoc(dsDatosGrales, dsDatos);


        string apppath = Server.MapPath("~/Temp") + "\\" + "testtembrado.pdf";
        System.IO.File.WriteAllBytes(apppath, bytes);

        Cdena_Url("testtembrado.pdf", 1);


    }

    public void Cdena_Url(string UrlPdf, int celdas)
    {
        string script = @"<script type='text/javascript'> openRadWin('" + UrlPdf + "','" + celdas + "'); </script>";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
    }
}