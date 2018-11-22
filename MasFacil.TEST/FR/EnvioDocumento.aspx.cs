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
//Librerias 
using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json;

public partial class FR_EnvioDocumento : System.Web.UI.Page
{
    #region VARIABLES

    csStamps stampDoc = new csStamps();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    private string Pag_sUsrCve;

    private string folio_Selection;
    //private string conConfSec;
    private string listTipDatoCptoCve;

    //Variables staticas Valor Moneda NACIONAL
    private static string Pag_sMonDef;

    //Variable statica Valor Correo a Copiar por Def. Doc
    private static string valMailCopyServer = "";

    //Variables staticas Conexion Timbrado
    private static string Pag_sUrlApi;
    private static string Pag_sKeyApi;
    private static string Pag_sUsrApi;
    private static string Pag_sNamApi;
    //Tablas Temporales
    private static DataTable dtClieToSend = new DataTable();
    DataRow drClieToSend;
    private static DataTable dtLogProc = new DataTable();
    DataRow drLogProc;

    private static int contador = 1;
    private static string filtroSp = "";
    private static string Val_Fec_Inicio = "";
    private static string Val_Fec_Fin = "";
    //Variables Servidor de Mensajeria
    private static string valSmtpServer = "";
    private static string valMailServer = "";
    private static string valPswServer = "";
    //Variables de Mensajes de Error
    private static string msgWarningsPB = "";
    private static string msgErrorsPB = "";



    private static Int64 docCmpTrnId = 0;

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {

        if (SM.IsActiveSession(this.Page))
        {
            //Recuperar Valores de Sesion
            Valores_InicioPag();
            if (!IsPostBack)
            {
                //Iniciar Formulario
                InicioPagina();

            }
        }

    }
    
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        LimpiarUi();
    }
    
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {

        rBtnCancelar.Enabled = false;
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png";
        rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png";
        //System.Threading.Thread.Sleep(2000);

        msgWarningsPB = "";
        msgErrorsPB = "";
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            if (stampDocs())
            {
                
                rGdv_Documentos.DataSource = dtLogProc;
                rGdv_Documentos.DataBind();

                ShowAlert("1", "Envio Masivo Finalizado");

            }
            else
            {
                ShowAlert("3", "Error: respuesta False");
            }




        }
        else
        {
            ShowAlert("2", msgValidacion);
            rCboTipoDoc.SelectedIndex = 0;
            rCboTipoDoc.Focus();

        }

        rBtnCancelar.Enabled = true;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelar.png";
        rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardar.png";
        //getQrFact();
    }
    
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rCboCliente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SubClientesCombo();
    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            //LlenaGridDocumentosFiltros();
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
            Val_Fec_Inicio = "";
        }
        
    }

    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        Val_Fec_Fin = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        Val_Fec_Fin = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            //LlenaGridDocumentosFiltros();
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
            Val_Fec_Fin = "";
        }
        
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_sUsrCve = Convert.ToString(Session["user"]);
    }

    private void Valores_PB()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Pag_sUrlApi = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

        DataSet dsI = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDI = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDI.NombreProcedimiento = "sp_parametros";
        ProcBDI.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDI.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDI.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBDI.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 2);

        dsI = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDI.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsI.Tables.Count > 0)
        {
            if (dsI.Tables[0].Rows.Count > 0)
            {
                Pag_sKeyApi = dsI.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

        DataSet dsP = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDP = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDP.NombreProcedimiento = "sp_parametros";
        ProcBDP.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDP.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDP.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");

        ProcBDP.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 3);

        dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDP.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsP.Tables.Count > 0)
        {
            if (dsP.Tables[0].Rows.Count > 0)
            {
                Pag_sUsrApi = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }


        DataSet dsN = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDN = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDN.NombreProcedimiento = "sp_parametros";
        ProcBDN.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDN.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDN.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PAYBOOK");
        ProcBDN.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 4);

        dsN = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDN.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsN.Tables.Count > 0)
        {
            if (dsN.Tables[0].Rows.Count > 0)
            {
                Pag_sNamApi = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }


    }
    
    private void getDsDocs()
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rCboTipoDoc.SelectedValue);

            ProcBD.AgregarParametrosProcedimiento("@docRegFolio", DbType.String, 10, ParameterDirection.Input, rTxtFolio.Text);

            string cliente;
            cliente = rCboCliente.SelectedValue + rCboSubCliente.SelectedValue;
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliente);


            if (Val_Fec_Inicio != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@fchIni", DbType.String, 100, ParameterDirection.Input, Val_Fec_Inicio + " 00:00:00");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@fchIni", DbType.String, 100, ParameterDirection.Input, "1900-01-01 00:00:00");
            }


            if (Val_Fec_Fin != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@fchFin", DbType.String, 100, ParameterDirection.Input, Val_Fec_Fin + " 23:59:59");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@fchFin", DbType.String, 100, ParameterDirection.Input, "2070-12-31 00:00:00");
            }




            //---------------- Parametros Opcionales

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //Conseguir Clientes en vez de Tipos para su posterior releida y obtencion del filtro

                    dtClieToSend.Clear();
                    dtLogProc.Clear();

                    int CountItems = 0;

                    foreach (DataRow fila in ds.Tables[0].Rows)
                    {

                        string docRegCve = fila["cliCve"].ToString();

                        DataRow[] filteredRows = (dtClieToSend.Select("cliCve like '%" + docRegCve + "%'"));

                        if (filteredRows.Length > 0)
                        {
                            // MessageBox.Show("ya se agrego");
                        }
                        else
                        {
                            DataRow rowTmp = dtClieToSend.NewRow();
                            rowTmp["cliCve"] = docRegCve;
                            dtClieToSend.Rows.Add(rowTmp);
                        }

                        CountItems += 1;

                    }

                    CountItems = 0;


                    //Recuperar Pdfs por cliente


                    setSitTrnCmlDoc(1, 1);
                    chkSitTrnTmbUsr();

                    foreach (DataRow row in dtClieToSend.Rows)
                    {
                        contador = 0;

                        string noRepeat = "";

                        foreach (DataRow regDs in ds.Tables[0].Rows)
                        {

                            string docRegId = regDs["docRegId"].ToString();
                            string docRegClie = regDs["cliCve"].ToString();
                            string docRegCve = regDs["docCve"].ToString();

                            string docRegMail = regDs["emailTo"].ToString();

                            if (row["cliCve"].ToString() == docRegClie)
                            {
                                filtroSp += docRegId;

                                Session["filtroSp"] = filtroSp;
                                Session["clieToSend"] = docRegMail;

                                if (noRepeat != docRegId)
                                {

                                    //callApiSat();

                                    string fileNameX = "";
                                    string pathToSaveTxt = "";
                                    string pathToSavePdf = "";
                                    string pathToSaveXml = "";

                                    string apppathClass = MapPath("~");
                                    apppathClass += "Temp\\";

                                    fileNameX = "MGMDOC" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                                        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

                                    pathToSaveTxt = apppathClass + fileNameX + ".txt";
                                    pathToSavePdf = apppathClass + fileNameX + ".pdf";
                                    pathToSaveXml = apppathClass + fileNameX + ".xml";
                                    
                                    //setSitTrnCmlDoc(1, 1);
                                    //chkSitTrnTmbUsr();
                                    stampDoc.stampDoc(Session["filtroSp"].ToString(), rCboTipoDoc.SelectedValue, Pag_sCompania, Pag_sConexionLog, Session["clieToSend"].ToString(), pathToSaveTxt, pathToSavePdf, pathToSaveXml, ref dtLogProc, Convert.ToInt32( docCmpTrnId), Pag_sUsrCve);
                                    //setSitTrnCmlDoc(2, 0);

                                    noRepeat = docRegId;
                                }

                                filtroSp = "";
                                Session["filtroSp"] = "";

                            }


                            contador++;

                            CountItems++;

                        }

                        CountItems = 0;

                    }


                    setSitTrnCmlDoc(2, 0);

                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());

            throw;
        }


    }
    
    private void delFilesCmp(string pathToDel)
    {

        try
        {
            if (pathToDel != "")
            {
                File.Delete(pathToDel);
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            throw;
        }


    }
    
    private void callJsPayBook()
    {

        MessageBox.Show(jsonPaybook.Value.ToString());

        string script = @"<script type='text/javascript'> myFunction(); </script>";

        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

        MessageBox.Show(jsonPaybook.Value.ToString());
    }
    
    private void InicioPagina()
    {
        ControlesAccion();
        LlenaComboTiposDato();

        if (dtClieToSend.Columns.Count == 0)
        {
            dtClieToSend.Columns.Add("cliCve");
        }

        if (dtLogProc.Columns.Count == 0)
        {
            dtLogProc.Columns.Add("docRegId");
            dtLogProc.Columns.Add("docCve");
            dtLogProc.Columns.Add("docCmpMsgTxt");
            dtLogProc.Columns.Add("docCmpMsgUrlSit");
        }

        getValServer();
        Valores_PB();
        Valores_Com();
        LimpiarUi();

        getLastLog();

        blockUi(Convert.ToInt32(chkSitTrnTmbUsr()));
        
    }
    
    public void ClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboCliente, ds, "cliCveClie", "clieNom", true, false, "");
        ((Literal)rCboCliente.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboCliente.Items.Count);

    }
    
    public void DocumentosCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Documentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenCFDIOpc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboTipoDoc, ds, "docCve", "docDes", true, false, "");
        ((Literal)rCboTipoDoc.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipoDoc.Items.Count);

    }

    private void Valores_Com()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "TIPOCAMBIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Pag_sMonDef = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }

    }

    private void getValServer()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                valSmtpServer = ds.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }
        
        DataSet dsI = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDI = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDI.NombreProcedimiento = "sp_parametros";
        ProcBDI.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDI.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDI.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");

        ProcBDI.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 2);

        dsI = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDI.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsI.Tables.Count > 0)
        {
            if (dsI.Tables[0].Rows.Count > 0)
            {
                valMailServer = dsI.Tables[0].Rows[0]["parmValStr"].ToString();

            }
        }
        
        DataSet dsP = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDP = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBDP.NombreProcedimiento = "sp_parametros";
        ProcBDP.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBDP.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBDP.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "ENVDOC");

        ProcBDP.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 3);

        dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDP.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsP.Tables.Count > 0)
        {
            if (dsP.Tables[0].Rows.Count > 0)
            {
                valPswServer = dsP.Tables[0].Rows[0]["parmValStr"].ToString();
            }
        }
        
    }

    public void SubClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCveClie", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboSubCliente, ds, "cliCveSubClie", "clieNom", true, false, "");
        ((Literal)rCboSubCliente.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSubCliente.Items.Count);
        rCboSubCliente.Enabled = true;

    }

    private void sendDocs(DataSet ds , string fileName , string UrlsDocs)
    {

        if (Session["clieToSend"].ToString() != "")
        {
            try
            {
                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = valSmtpServer;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                client.Credentials = new System.Net.NetworkCredential(valMailServer, valPswServer);

                string mailToMM = Session["clieToSend"].ToString();


                MailMessage mm = new MailMessage(valMailServer, mailToMM, "Envio MGM", "Reportes Enviados por Managment");

                //Copia

                if (valMailCopyServer != "")
                {
                    MailAddress copy = new MailAddress(valMailCopyServer);
                    mm.CC.Add(copy);
                }
                


                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                System.Net.Mail.Attachment attachment;
                //Agrega PDF

                MemoryStream ms = new MemoryStream();
                using (Stream input = File.OpenRead(fileName))
                {
                    input.CopyTo(ms);
                }
                ms.Position = 0;
                mm.Attachments.Add(new Attachment(ms, new FileInfo(fileName).Name));

                MemoryStream msPDF = new MemoryStream();
                using (Stream input = File.OpenRead(UrlsDocs))
                {
                    input.CopyTo(msPDF);
                }
                msPDF.Position = 0;
                mm.Attachments.Add(new Attachment(msPDF, new FileInfo(UrlsDocs).Name));


                //attachment = new System.Net.Mail.Attachment(UrlsDocs);
                //mm.Attachments.Add(attachment);
                //Agregar XML
                //attachment = new System.Net.Mail.Attachment(fileName);
                //mm.Attachments.Add(attachment);
                //send

                client.Send(mm);

                //ms.Close();
                //fileStrmPDF.Close();
                //msPDF.Close();

                //Grabar errores en tabla
                DataRow rowTmp = dtLogProc.NewRow();
                rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                rowTmp["docDes"] = "Enviado";
                rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit2.png";
                dtLogProc.Rows.Add(rowTmp);

                //Borrar xml y pdf de Directorio o Guardar

                delFilesCmp(fileName);
                delFilesCmp(UrlsDocs);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());

                //Grabar errores en tabla
                DataRow rowTmp = dtLogProc.NewRow();
                rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                rowTmp["docDes"] = "Error de Envio -> " + ex.Message.ToString();
                rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                dtLogProc.Rows.Add(rowTmp);

                delFilesCmp(fileName);
                delFilesCmp(UrlsDocs);
                
                throw;
            }

        }
        else
        {

            //Grabar errores en tabla
            DataRow rowTmp = dtLogProc.NewRow();
            rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
            rowTmp["docDes"] = "Error de Envio -> Correo no Definido en Contacto Cliente";
            rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
            dtLogProc.Rows.Add(rowTmp);

        }

    }

    private void ControlesAccion()
    {
        //===> CONTROLES POR ACCION
        // LIMPIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
        {
            LimpiarUi();
        }

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
        }


        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            
        }
        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
       )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
        else
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }


    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            //---------------
            //getDsDocs();
            ShowAlert("1", "Docuentos enviados Correctamente");

        }
        else
        {
            ShowAlert("2", msgValidacion);
            rCboTipoDoc.SelectedIndex = 0;
            rCboTipoDoc.Focus();

        }
    }
    
    private void LimpiarUi()
    {
        rCboTipoDoc.ClearSelection();
        rTxtFolio.Text = "";
        rCboCliente.ClearSelection();
        rCboSubCliente.ClearSelection();
        RdDateFecha_Inicio.Clear();
        RdDateFecha_Final.Clear();

        Val_Fec_Fin = "";
        Val_Fec_Inicio = "";

        dtLogProc.Rows.Clear();
        rGdv_Documentos.DataSource = dtLogProc;
        rGdv_Documentos.DataBind();

    }
    
    private void LlenaComboTiposDato()
    {

        DocumentosCombo();
        ClientesCombo();
        
    }
    
    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";

        if (rCboTipoDoc.SelectedValue == "")
        {
            sResult = "Escoge un Documento para Continuar";
        }
        else
        {
            sResult = "";
        }
        
        return sResult;
    }

    private bool compararFechas()
    {

        if (RdDateFecha_Inicio.SelectedDate > RdDateFecha_Final.SelectedDate)
        {
            return false;
        }
        else if (RdDateFecha_Final.SelectedDate < RdDateFecha_Inicio.SelectedDate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public static bool SendAnSMSMessage()
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sync.paybook.com/v1/sessions?api_key=79cef7b5b162a928c24206cc26ae1279");
        httpWebRequest.ContentType = "text/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{ \"api_key\": \"79cef7b5b162a928c24206cc26ae1279\", " +
                              "  \"id_user\": \"589ccee30b212ab4278b923d\" " +
                              "}";

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            //Now you have your response.
            //or false depending on information in the response
            return true;
        }
    }

    public static string callApiSatPaybook()
    {

        string tokenUsrPB;

        tokenUsrPB = getTokenPB("79cef7b5b162a928c24206cc26ae1279", "58a2449b0b212aa02c8b4649");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sync.paybook.com/v1/invoicing/mx/invoices/check");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{ \"api_key\": \"79cef7b5b162a928c24206cc26ae1279\", " +
                              "  \"id_user\": \"589ccee30b212ab4278b923d\" " +
                              "}";


            string jsonXml = " { \"api_key\": \"79cef7b5b162a928c24206cc26ae1279\", " +
                              "  \"id_user\": \"589ccee30b212ab4278b923d\", " +
                              "  \"token\": \"" + tokenUsrPB + "\", " +
                              "  \"username\": \"usrMGMDESA01\", " +
                              "  \"id_provider\": \"acme\", " +
                              "  \"invoice_data\": { " +
                                    " \"serie\": \"A\", " +
                                    " \"folio\": \"35\", " +
                                    " \"fecha\": \"2017-02-13T11:52:18\", " +
                                    " \"formaDePago\": \"Parcialidad 1 de 30\", " +
                                    " \"condicionesDePago\": \"Valido por 30 días\", " +
                                    " \"subTotal\": \"2168.22\", " +
                                    " \"descuento\":\"20\", " +
                                    " \"motivoDescuento\":\"Promocion mensual\", " +
                                    " \"tipoCambio\":\"18.22\", " +
                                    " \"moneda\":\"MXN\", " +
                                    " \"total\":\"2270.1\", " +
                                    " \"tipoDeComprobante\":\"ingreso\", " +
                                    " \"metodoDePago\":\"02\", " +
                                    " \"lugarExpedicion\":\"Ciudad de México\", " +
                                    " \"numCtaPago\":\"5219022\", " +
                                    " \"emisor\": { " +
                                        " \"nombre\":\"Alejandro Hernandez Rodriguez\", " +
                                        " \"rfc\":\"AAA010101AAA\", " +
                                        " \"domicilioFiscal\":{ " +
                                            " \"calle\":\"Calle 25\", " +
                                            " \"municipio\":\"Monterrey\", " +
                                            " \"estado\":\"Nuevo Leon\", " +
                                            " \"pais\":\"Mexico\"," +
                                            " \"codigoPostal\":\"64450 \" " +
                                        " }, " +
                                        " \"expedidoEn\":{ " +
                                            " \"calle\":\"Calle 25\", " +
                                            " \"municipio\":\"Monterrey\", " +
                                            " \"estado\":\"Nuevo Leon\", " +
                                            " \"pais\":\"Mexico\", " +
                                            " \"codigoPostal\":\"64450 \"" +
                                        " }, " +
                                        " \"regimenFiscal\":[{\"regimen\":\"Empleado Honorarios\"}] " +
                                    " }, " +

                                    " \"receptor\":{ " +
                                        " \"rfc\":\"AOOM8309271A8\", " +
                                        " \"nombre\": \"Pedro Perez Hernandez\", " +
                                        " \"domicilio\":{ " +
                                            " \"calle\":\"Calle 25\", " +
                                            " \"municipio\":\"Monterrey\", " +
                                            " \"estado\":\"Nuevo Leon\", " +
                                            " \"pais\": \"Mexico\", " +
                                            " \"codigoPostal\":\"64450 \" " +
                                        " } " +
                                    " }, " +

                                    " \"conceptos\": [ " +
                                        " { " +
                                            " \"cantidad\": \"10.5\", " +
                                            " \"unidad\": \"Kg\", " +
                                            " \"descripcion\": \"Alambre calibre 22\", " +
                                            " \"noIdentificacion\": \"SK3218932190\"," +
                                            " \"valorUnitario\": \"10\", " +
                                            " \"importe\": \"105\" " +
                                        " }, " +
                                        " { " +
                                            " \"cantidad\": \"5\", " +
                                            " \"unidad\": \"Mt\", " +
                                            " \"descripcion\": \"Producto Importado\", " +
                                            " \"valorUnitario\": \"100\", " +
                                            " \"importe\": \"500\", " +
                                            " \"noIdentificacion\":\"SKU120312954\" " +
                                        " }, " +
                                        " { " +
                                            " \"cantidad\": \"1\", " +
                                            " \"unidad\": \"2\", " +
                                            " \"descripcion\": \"Pago PRedial Vivienda\", " +
                                            " \"noIdentificacion\": \"H22\", " +
                                            " \"valorUnitario\": \"1563.22\", " +
                                            " \"importe\": \"1563.22\", " +
                                            " \"cuentaPredial\": { " +
                                                " \"numero\":\"PRE03185430011\" " +
                                            " } " +
                                        " } " +
                                    " ], " +
                                    " \"impuestos\": { " +
                                        " \"totalImpuestosRetenidos\":\"12.33\", " +
                                        " \"totalImpuestosTrasladados\":\"114.21\", " +
                                        " \"retenciones\":[ " +
                                            " { " +
                                                " \"impuesto\":\"ISR\", " +
                                                " \"importe\":\"12.33\" " +
                                            " } " +
                                        " ], " +
                                        " \"traslados\":[ " +
                                            " { " +
                                                " \"impuesto\":\"IVA\", " +
                                                " \"tasa\":\"10\", " +
                                                " \"importe\":\"114.21\" " +
                                            " } " +
                                        " ] " +
                                    " } " +
                                " } " +
                             " } ";


            streamWriter.Write(jsonXml);
        }

        string xmlPB = "";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            //Now you have your response.
            //or false depending on information in the response

            //Obtener XML


            JsonTextReader reader = new JsonTextReader(new StringReader(responseText));

            string token = "";
            bool getVal = false;

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);

                    if (getVal)
                    {
                        xmlPB = reader.Value.ToString();
                        getVal = false;
                    }

                    if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "xml")
                    {
                        getVal = true;
                    }

                }
                else
                {
                    //Console.WriteLine("Token: {0}", reader.TokenType);
                    //MessageBox.Show(reader.TokenType.ToString());

                }

            }

        }

        return xmlPB;

    }

    private static string getTokenPB(string apiKeyPB, string usrPB)
    {
        string tokenPB = "";

        string stringPB = "";

        string urlPB = "https://sync.paybook.com/v1/sessions?api_key=";
        urlPB += apiKeyPB;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlPB);
        httpWebRequest.ContentType = "text/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{ \"api_key\": \"" + apiKeyPB + "\", " +
                              "  \"id_user\": \"" + usrPB + "\" " +
                              "}";

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var responseText = streamReader.ReadToEnd();
            //Now you have your response.
            //or false depending on information in the response

            JsonTextReader reader = new JsonTextReader(new StringReader(responseText));

            string token = "";
            bool getVal = false;

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    //MessageBox.Show(System.String.Format( "Token: {0}, Value: {1}", reader.TokenType, reader.Value));

                    stringPB += System.String.Format("Token: {0}, Value: {1}", reader.TokenType, reader.Value);

                    if (getVal)
                    {
                        tokenPB = reader.Value.ToString();
                        getVal = false;
                    }

                    if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "token")
                    {
                        getVal = true;
                    }




                    stringPB += "\n";

                }
                else
                {
                    //Console.WriteLine("Token: {0}", reader.TokenType);
                    //MessageBox.Show(reader.TokenType.ToString());
                    stringPB += reader.TokenType.ToString();
                    stringPB += "\n";

                }

            }

            //MessageBox.Show(tokenPB);

            return tokenPB;
        }

    }

    private string callApiSat()
    {
        string UrlsDocs = "";
        string UrlPdf = "";
        string UrlXml = "";
        string jsonPB = "";
        string fil = Session["filtroSp"].ToString();

        string saltoLinea = "\r\n";

        //msgErrorsPB = "";
        //msgWarningsPB = "";

        //A
        
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDoc";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDoc.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@fltrDocRegIds", DbType.String, 2048, ParameterDirection.Input, fil);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0 && chkDocReadyStmp(ds.Tables[0].Rows[0]["docRegId"].ToString()))
            {
                //Crear JSON

                //Iniciar session y obtencion de token
                string tokenUsrPB;
                tokenUsrPB = getTokenPB(Pag_sKeyApi, Pag_sUsrApi);

                //Llamado de ENDPOINT INVOICES PB
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Pag_sUrlApi + "invoicing/mx/invoices");

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sync.paybook.com/v1/invoicing/mx/invoices/check");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    string jsonXml = bldJsonPB(tokenUsrPB, ds);
                    streamWriter.Write(jsonXml);
                }

                string xmlPB = "";

                //Controlar errores 4XX's

                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();

                        //Obtener XML
                        JsonTextReader reader = new JsonTextReader(new StringReader(responseText));

                        bool getVal = false;
                        bool getWar = false;
                        bool getErr = false;



                        while (reader.Read())
                        {
                            if (reader.Value != null)
                            {
                                // Obtener XML dea respuesta JSON

                                if (getVal)
                                {
                                    xmlPB = reader.Value.ToString();
                                    getVal = false;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "xml")
                                {
                                    getVal = true;
                                }

                                // Obtener Warnings de respuesta JSON

                                if (getWar && reader.TokenType.ToString() == "String")
                                {

                                    msgWarningsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                    msgWarningsPB += " - ";
                                    msgWarningsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                    msgWarningsPB += " : ";
                                    msgWarningsPB += reader.Value.ToString();
                                    msgWarningsPB += " || ";

                                    msgWarningsPB += saltoLinea;

                                    //Grabar errores en tabla
                                    DataRow rowTmp = dtLogProc.NewRow();
                                    rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                    rowTmp["docDes"] = reader.Value.ToString();
                                    rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit1.png";
                                    dtLogProc.Rows.Add(rowTmp);

                                    getWar = false;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "warnings")
                                {
                                    getWar = true;
                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "status")
                                {
                                    getErr = false;
                                }

                                // Obtener Errores de respuesta JSON

                                if ((getErr && reader.TokenType.ToString() == "String") || (getErr && reader.TokenType.ToString() == "Boolean"))
                                {

                                    if (reader.Value.ToString() == "False" || reader.Value.ToString() == "AAA010101AAA")
                                    {
                                        msgErrorsPB += "";
                                        getErr = false;
                                    }
                                    else
                                    {
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                        msgErrorsPB += " - ";
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        msgErrorsPB += " : ";
                                        msgErrorsPB += reader.Value.ToString();
                                        msgErrorsPB += " || ";
                                        msgErrorsPB += saltoLinea;

                                        //Grabar errores en tabla
                                        DataRow rowTmp = dtLogProc.NewRow();
                                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        rowTmp["docDes"] = "ERROR: " + reader.Value.ToString();
                                        rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                                        dtLogProc.Rows.Add(rowTmp);

                                        getErr = false;
                                    }


                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "errors")
                                {
                                    getErr = true;
                                }


                                // Obtener Codigo y mensaje de respuesta JSON

                                if ((getErr && reader.TokenType.ToString() == "String") || (getErr && reader.TokenType.ToString() == "Boolean"))
                                {

                                    if (reader.Value.ToString() == "False")
                                    {
                                        msgErrorsPB += "";
                                        getErr = false;
                                    }
                                    else
                                    {
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                        msgErrorsPB += " - ";
                                        msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        msgErrorsPB += " : ";
                                        msgErrorsPB += reader.Value.ToString();
                                        msgErrorsPB += " || ";
                                        msgErrorsPB += saltoLinea;

                                        //Grabar errores en tabla
                                        DataRow rowTmp = dtLogProc.NewRow();
                                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                        rowTmp["docDes"] = "ERROR: " + reader.Value.ToString();
                                        rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                                        dtLogProc.Rows.Add(rowTmp);

                                        getErr = false;
                                    }


                                }

                                if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "message")
                                {
                                    getErr = true;
                                }


                            }
                            else
                            {
                                //Console.WriteLine("Token: {0}", reader.TokenType);
                                //MessageBox.Show(reader.TokenType.ToString());

                            }

                        }

                    }





                    //Guardar XML
                    string fileName = "";

                    string apppath = MapPath("~");
                    apppath += "Temp\\";

                    UrlXml = "MGMXML" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".xml";

                    fileName = @apppath + UrlXml;

                    FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(stream);

                    writer.WriteLine(xmlPB);
                    writer.Close();

                    //Leer XML

                    string xmlRespuesta = "";

                    string docCmpUUID = "";
                    string docCmpFch = "";
                    string docCmpNoCFDemi = "";
                    string docCmpNoCFDsat = "";
                    string docCmpSelloEmi = "";
                    string docCmpSelloSat = "";
                    string docCmpCertificado = "";
                    string docCmpCadena = "";

                    string docCmpXML = "";
                    Byte[] bytesCmpXml = File.ReadAllBytes(fileName);
                    docCmpXML = Convert.ToBase64String(bytesCmpXml);

                    string docCmpPDF = "";
                
                    
                    XNamespace cfdi = XNamespace.Get("http://www.sat.gob.mx/cfd/3");
                    XNamespace implocal = XNamespace.Get("http://www.sat.gob.mx/implocal");
                    XNamespace tfd = XNamespace.Get("http://www.sat.gob.mx/TimbreFiscalDigital");
                    XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");


                    if (xmlPB != "")
                    {

                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docDes"] = "Timbrado Correctamente";
                        rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit2.png";
                        dtLogProc.Rows.Add(rowTmp);



                        XDocument rdf = XDocument.Load(new StringReader(xmlPB));

                        //Variables a guardar en tb_DocumentoComprobantes


                        //Recorremos nodo CFDI nameSpace Comprobante
                        foreach (var comprobante in rdf.Descendants(cfdi + "Comprobante"))
                        {
                            //Guardar Datos de Facturacion
                            docCmpCertificado = comprobante.Attribute("certificado").Value.ToString();
                            docCmpNoCFDemi = comprobante.Attribute("noCertificado").Value.ToString();
                        }

                        //Recorremos nodo TIMBRE nameSpace Comprobante
                        foreach (var comprobante in rdf.Descendants(tfd + "TimbreFiscalDigital"))
                        {
                            docCmpUUID = comprobante.Attribute("UUID").Value.ToString();
                            docCmpFch = comprobante.Attribute("FechaTimbrado").Value.ToString();
                            docCmpNoCFDsat = comprobante.Attribute("noCertificadoSAT").Value.ToString();
                            docCmpSelloEmi = comprobante.Attribute("selloCFD").Value.ToString();
                            docCmpSelloSat = comprobante.Attribute("selloSAT").Value.ToString();
                        }



                        XName nameX = XName.Get("certificado");



                        docCmpCadena = "||1.0|";
                        docCmpCadena += docCmpUUID;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpFch;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpSelloEmi;
                        docCmpCadena += "|";
                        docCmpCadena += docCmpNoCFDsat;
                        docCmpCadena += "||";


                        //Imagen QR de la Factura
                        string urlApiqr;
                        urlApiqr = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=";
                        //scapestring ?
                        urlApiqr += "%3F";
                        urlApiqr += "re";
                        //scapestring =
                        urlApiqr += "%3D";
                        urlApiqr += "AAA010101AAA";
                        //scapestring &
                        urlApiqr += "%26";
                        urlApiqr += "rr";
                        urlApiqr += "%3D";
                        urlApiqr += ds.Tables[0].Rows[0]["clieRegFis"].ToString();
                        urlApiqr += "%26";
                        urlApiqr += "tt";
                        urlApiqr += "%3D";
                        urlApiqr += ds.Tables[0].Rows[0]["docRegTotImpNeto"].ToString();
                        urlApiqr += "%26";
                        urlApiqr += "id";
                        urlApiqr += "%3D";
                        urlApiqr += docCmpUUID;


                        WebClient wc = new WebClient();
                        //byte[] bytesImg = wc.DownloadData("https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=QRvicZamudio");
                        byte[] bytesImg = wc.DownloadData(urlApiqr);
                        //MemoryStream ms = new MemoryStream(bytesImg);
                        //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                        string base64StringImgQr = Convert.ToBase64String(bytesImg, 0, bytesImg.Length);


                        //Guardar Datos de Facturacion para posterior creacion de PDF


                        DataSet dsDC = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDDC = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBDDC.NombreProcedimiento = "sp_DocumentoComprobantes";

                        ProcBDDC.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                        ProcBDDC.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBDDC.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(ds.Tables[0].Rows[0][0].ToString()));

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpUUID", DbType.String, 50, ParameterDirection.Input, docCmpUUID);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpFch", DbType.String, 24, ParameterDirection.Input, docCmpFch);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpNoCFDemi", DbType.String, 24, ParameterDirection.Input, docCmpNoCFDemi);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpNoCFDsat", DbType.String, 24, ParameterDirection.Input, docCmpNoCFDsat);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpSelloEmi", DbType.String, 256, ParameterDirection.Input, docCmpSelloEmi);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpSelloSat", DbType.String, 256, ParameterDirection.Input, docCmpSelloSat);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpCertificado", DbType.String, 2048, ParameterDirection.Input, docCmpCertificado);
                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpCadena", DbType.String, 2048, ParameterDirection.Input, docCmpCadena);

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpQr", DbType.Binary, 0, ParameterDirection.Input, base64StringImgQr);

                        ProcBDDC.AgregarParametrosProcedimiento("@docCmpXML", DbType.Binary, 0, ParameterDirection.Input, docCmpXML);
                        //ProcBDDC.AgregarParametrosProcedimiento("@docCmpQr", DbType.Binary, 0, ParameterDirection.Input, base64StringImgQr);

                        try
                        {
                            dsDC = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDDC.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            throw;
                        }



                    }
                    else
                    {
                        msgErrorsPB += " XML no Generado por Errores -> Envio Cancelado";
                        msgErrorsPB += saltoLinea;

                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docDes"] = "Envio Cancelado -> XML no Generado por Errores ";
                        rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit5.png";
                        dtLogProc.Rows.Add(rowTmp);

                    }



                    //sendDoc();------------------------------------------------------------------------------


                    DataSet ds1 = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD1.NombreProcedimiento = "sp_RptDataSetDoc";
                    ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 5);
                    ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD1.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rCboTipoDoc.SelectedValue);

                    ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    //Rellenar otra vez ds con datos de facturacion

                    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    try
                    {

                        //getds1
                        string apppathPDF = MapPath("~");
                        apppathPDF += "Temp\\";

                        byte[] bytes = oWSRpt.byte_FormatoDoc(ds1, ds);


                        UrlPdf = "MGMPDF" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
                        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

                        //UrlsDocs = @"C:\Users\WINDOWS10\Documents\MGMVZam\BrykManagement\BrykManagement\FR\Docs\" + UrlPdf;
                        UrlsDocs = @apppathPDF + UrlPdf;


                        System.IO.File.WriteAllBytes(UrlsDocs, bytes);


                        String filePDFtoSave = Convert.ToBase64String(bytes);

                        savePDF(ds.Tables[0].Rows[0]["docRegId"].ToString(), filePDFtoSave);

                        //Enviar por correo

                        if (chkDocSendStmp(ds.Tables[0].Rows[0]["docCve"].ToString()) && xmlPB != "")
                        {
                            sendDocs(ds, fileName, UrlsDocs);
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());

                        /*
                        //Grabar errores en tabla
                        DataRow rowTmp = dtLogProc.NewRow();
                        rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                        rowTmp["docDes"] = "Error de PDF -> " + ex.ToString();
                        rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                        dtLogProc.Rows.Add(rowTmp);

                        */

                        return "";
                        throw;
                    }


                }
                catch (WebException wex)
                {

                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var readerStream = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = readerStream.ReadToEnd();

                                //Obtener error del arreglo error400


                                JsonTextReader reader = new JsonTextReader(new StringReader(error));

                                bool getVal = false;

                                while (reader.Read())
                                {
                                    if (reader.Value != null)
                                    {
                                        //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);

                                        if (getVal)
                                        {

                                            msgErrorsPB += ds.Tables[0].Rows[0]["docCve"].ToString();
                                            msgErrorsPB += " - ";
                                            msgErrorsPB += ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                            msgErrorsPB += " (No enviado): ";
                                            msgErrorsPB += reader.Value.ToString();
                                            msgErrorsPB += " || ";
                                            msgErrorsPB += saltoLinea;


                                            //Grabar errores en tabla
                                            DataRow rowTmp = dtLogProc.NewRow();
                                            rowTmp["docCve"] = ds.Tables[0].Rows[0]["docCve"].ToString() + " - " + ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                                            rowTmp["docDes"] = "No enviado ->" + reader.Value.ToString();
                                            rowTmp["imgSit"] = "~/Imagenes/IcoDocProceso/imgSit9.png";
                                            dtLogProc.Rows.Add(rowTmp);

                                            getVal = false;
                                        }

                                        if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "message")
                                        {
                                            getVal = true;
                                        }

                                    }
                                }


                                //MessageBox.Show(error);

                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }


                    //throw;
                }



            }

        }


        setSitTrnCmlDoc(2, 0);

        return jsonPB;
    }

    private string bldJsonPB(string tokenUsrPB, DataSet ds)
    {
        string jsonToSend = "";

        string jsonFch = ds.Tables[0].Rows[0]["docRegFec"].ToString();

        DateTime fchjsonFch = Convert.ToDateTime(jsonFch);

        jsonFch = fchjsonFch.Year + "-" + fchjsonFch.Month.ToString().PadLeft(2, '0') + "-" + fchjsonFch.Day.ToString().PadLeft(2, '0') + "T";

        jsonFch += fchjsonFch.Hour.ToString().PadLeft(2, '0') + ":" + fchjsonFch.Minute.ToString().PadLeft(2, '0') + ":" + fchjsonFch.Second.ToString().PadLeft(2, '0');

        decimal jsonDesc = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpDesc"].ToString());
        jsonDesc += Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpDescPart"].ToString());
        decimal jsonSubTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpBrut"].ToString());

        //jsonSubTotal -= jsonDesc;

        decimal jsonTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpNeto"].ToString());

        decimal jsonImpu = Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpuSBrut"].ToString());
        jsonImpu += Convert.ToDecimal(ds.Tables[0].Rows[0]["docRegTotImpuSNeto"].ToString());


        string jsonTipCmb = getTipCmb(ds.Tables[0].Rows[0]["monCve"].ToString() , jsonFch);

        jsonToSend = " { \"api_key\": \""+ Pag_sKeyApi+"\", " +
                              "  \"id_user\": \""+Pag_sUsrApi+"\", " +
                              "  \"token\": \"" + tokenUsrPB + "\", " +
                              "  \"username\": \""+Pag_sNamApi+"\", " +
                              "  \"id_test_site\": \"56cf5728784806f72b8b4568\", " +
                              "  \"id_provider\": \"acme\", " +
                              "  \"invoice_data\": { " +
                                    " \"serie\": \"MGM\", " +
                                    " \"folio\": \"" + ds.Tables[0].Rows[0]["docRegFolio"].ToString() + "\", " +
                                    " \"fecha\": \"" + jsonFch + "\", " +
                                    " \"formaDePago\": \"" + ds.Tables[0].Rows[0]["conPagDes"].ToString() + "\", " +
                                    " \"condicionesDePago\": \"Valido\", " +
                                    " \"subTotal\": \"" + jsonSubTotal.ToString() + "\", " +
                                    " \"descuento\":\"" + jsonDesc.ToString() + "\", " +

                                    " \"motivoDescuento\":\"Descuento Promocional\", " +
                                    " \"tipoCambio\":\"1\", " +
                                    " \"moneda\":\"" + ds.Tables[0].Rows[0]["monCve"].ToString() + "\", " +
                                    " \"total\":\"" + jsonTotal.ToString() + "\", " +
                                    " \"tipoDeComprobante\":\"ingreso\", " +
                                    " \"metodoDePago\":\"" + ds.Tables[0].Rows[0]["satMetPagCve"].ToString() + "\", " +
                                    " \"lugarExpedicion\":\"" + ds.Tables[0].Rows[0]["ParLugExp"].ToString() + "\", " +
                                    " \"numCtaPago\":\"1234\", " +
                                    " \"emisor\": { " +
                                        " \"nombre\":\"" + ds.Tables[0].Rows[0]["ciaRSoc"].ToString() + "\", " +
                                        " \"rfc\":\"AAA010101AAA\", " +
                                        " \"domicilioFiscal\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["domClle"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["provDes"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["entFDes"].ToString() + "\", " +
                                            " \"pais\":\"" + ds.Tables[0].Rows[0]["paisDes"].ToString() + "\"," +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["domCP"].ToString() + "\" " +
                                        " }, " +
                                        " \"expedidoEn\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["domClleExpd"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["provDesExpd"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["entFDesExpd"].ToString() + "\", " +
                                            " \"pais\":\"" + ds.Tables[0].Rows[0]["paisDesExpd"].ToString() + "\", " +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["domCPExpd"].ToString() + "\"" +
                                        " }, " +
                                        " \"regimenFiscal\":[{\"regimen\":\"no aplica\"}] " +
                                    " }, " +

                                    " \"receptor\":{ " +
                                        " \"rfc\":\"" + ds.Tables[0].Rows[0]["clieRegFis"].ToString() + "\", " +
                                        " \"nombre\": \"" + ds.Tables[0].Rows[0]["clieNom"].ToString() + "\", " +
                                        " \"domicilio\":{ " +
                                            " \"calle\":\"" + ds.Tables[0].Rows[0]["domClleCli"].ToString() + "\", " +
                                            " \"municipio\":\"" + ds.Tables[0].Rows[0]["provDesCli"].ToString() + "\", " +
                                            " \"estado\":\"" + ds.Tables[0].Rows[0]["entFDesCli"].ToString() + "\", " +
                                            " \"pais\": \"" + ds.Tables[0].Rows[0]["paisDesCli"].ToString() + "\", " +
                                            " \"codigoPostal\":\"" + ds.Tables[0].Rows[0]["domCPCli"].ToString() + "\" " +
                                        " } " +
                                    " }, " +

                                    " \"conceptos\": [ ";

        int countCpts = 1;

        foreach (DataRow rowJS in ds.Tables[0].Rows)
        {


            jsonToSend += " { " +
                            " \"cantidad\": \"" + rowJS["docRegPartCant"].ToString() + "\", " +
                            " \"unidad\": \"" + rowJS["uniMedCve"].ToString() + "\", " +
                            " \"descripcion\": \"" + rowJS["artDes"].ToString() + "\", " +
                            " \"valorUnitario\": \"" + rowJS["docRegPartPrec"].ToString() + "\", " +
                            " \"importe\": \"" + rowJS["docRegPartImpBrut"].ToString() + "\", " +
                            " \"noIdentificacion\": \"" + rowJS["artCve"].ToString() + "\" " +
                        " } ";

            if (countCpts != ds.Tables[0].Rows.Count)
            {
                jsonToSend += " ,";
            }
            countCpts++;
        }






        jsonToSend += " ], " +


        " \"impuestos\": { " +
            " \"totalImpuestosRetenidos\":\"00.00\", " +
            " \"totalImpuestosTrasladados\":\"" + jsonImpu.ToString() + "\", " +
            " \"retenciones\":[ " +
                " { " +
                    " \"impuesto\":\"ISR\", " +
                    " \"importe\":\"00.00\" " +
                " } " +
            " ], " +
            " \"traslados\":[ ";


        jsonToSend += bldJsonImpuPB(ds);

        jsonToSend += " ] " +
      " } " +
  " } " +
" } ";

        return jsonToSend;
    }

    private string bldJsonImpuPB(DataSet dsDatos)
    {
        string rstrImpuGlb = "";

        string prmDocCve = dsDatos.Tables[0].Rows[0]["docCve"].ToString();

        decimal impBase = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpBrut"].ToString());
        decimal impDescPart = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpDescPart"].ToString());
        decimal impDescGlb = Convert.ToDecimal(dsDatos.Tables[0].Rows[0]["docRegTotImpDesc"].ToString());

        impBase -= impDescPart;
        impBase -= impDescGlb;

        //Añadir impuestos globales
        DataSet dsImpu = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoImpuestos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, prmDocCve);
        dsImpu = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        int counterOne = 0;
        int counterTwo = 0;

        if (dsImpu.Tables.Count > 0)
        {

            if (dsImpu.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow rowImpu in dsImpu.Tables[0].Rows)
                {

                    string lblTasa;
                    decimal decTasa;

                    lblTasa = rowImpu["impAbr"].ToString();
                    decTasa = Convert.ToDecimal(rowImpu["impTas"].ToString());

                    impBase *= decTasa;
                    impBase /= 100;

                    if (counterOne != 0 && counterOne < dsImpu.Tables[0].Rows.Count)
                    {
                        rstrImpuGlb += " , ";
                    }

                    rstrImpuGlb += " { " +
                                " \"impuesto\":\"" + lblTasa + "\", " +
                                " \"tasa\":\"" + decTasa.ToString() + "\", " +
                                " \"importe\":\"" + impBase.ToString("0.##") + "\" " +
                            " } ";

                    counterOne++;

                }

            }



        }


        if (dsImpu.Tables[0].Rows.Count > 0 && dsDatos.Tables[0].Rows.Count > 0)
        {
            rstrImpuGlb += " , ";
        }


        if (dsDatos.Tables[0].Rows.Count > 0)
        {

            foreach (DataRow rowImpuPart in dsDatos.Tables[0].Rows)
            {

                if (counterTwo != 0 && counterTwo < dsDatos.Tables[0].Rows.Count)
                {
                    rstrImpuGlb += " , ";
                }

                rstrImpuGlb +=

                        " { " +
                            " \"impuesto\":\"IEPS\", " +
                            " \"tasa\":\"10\", " +
                            " \"importe\":\"" + rowImpuPart["docRegPartImpuSBrut"].ToString() + "\" " +
                        " } ";

                counterTwo++;
            }

        }

        //Añadir impuestos Partidas****



        return rstrImpuGlb;

    }

    public string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Convert Image to byte[]
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }

    private bool stampDocs()
    {
        bool resultFunct = false;

        try
        {
            getDsDocs();
            resultFunct = true;
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            resultFunct = false;
            throw;
        }

        return resultFunct;

    }
    
    #endregion
    


    private bool chkDocReadyStmp(string docRegId)
    {
        bool response = true;

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoComprobantes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    response = false;
                }else
                {
                    response = true;
                }
            }else
            {
                response = true;
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            response = true;
            throw;
        }
        
        return response;
    }


    private bool chkDocSendStmp(string docCve)
    {
        bool response = false;

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoGeneraCFDI";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["docGenCFDIMail"].ToString() == "1")
                    {
                        valMailCopyServer = ds.Tables[0].Rows[0]["docGenCFDICorreo"].ToString();
                        response = true;
                    }
                    else
                    {
                        valMailCopyServer = "";
                        response = false;
                    }
                    
                }
                else
                {
                    valMailCopyServer = "";
                    response = false;
                }
            }
            else
            {
                valMailCopyServer = "";
                response = false;
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
            response = false;
            throw;
        }

        return response;
    }


    private void savePDF(string docRegId, string docCmpPDF)
    {

        try
        {


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoComprobantes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBD.AgregarParametrosProcedimiento("@docCmpPDF", DbType.Binary, 0, ParameterDirection.Input, docCmpPDF);
            //ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (true)
            {

            }
        }
        catch (Exception)
        {

            throw;
        }



    }


    private string getTipCmb(string monCve, string docRegFec)
    {
        string response = "";

        if (monCve == Pag_sMonDef)
        {
            response = "1";
        }else
        {
            try
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Monedas";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, monCve);
                ProcBD.AgregarParametrosProcedimiento("@monTCFec", DbType.String, 100, ParameterDirection.Input, docRegFec);
                //ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));

                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (ds.Tables.Count > 0 )
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response = ds.Tables[0].Rows[0]["monTCC"].ToString();
                    }else
                    {
                        response = "1";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                response = "1";
                throw;
            }


        }

        
        return response;
    }


    private string chkSitTrnTmbUsr()
    {

        string response = "";

        try
        {

            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteTransacciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_sUsrCve);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (dsP.Tables.Count > 0)
            {
                if (dsP.Tables[0].Rows.Count > 0)
                {
                    response = "1";
                    docCmpTrnId = Convert.ToInt64(dsP.Tables[0].Rows[0]["docCmpTrnId"].ToString());
                }
                else
                {

                    response = "0";
                    docCmpTrnId = 0;

                }
                
            }
            else
            {
                response = "0";
            }


        }
        catch (Exception ex)
        {
            response = "0";
            throw;
        }


        return response;

    }




    private void blockUi(int opc)
    {

        //Si esta una traccion del usuario activa , bloquear funcion de timbrado
        if (opc == 1)
        {

            rBtnCancelar.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png";
            rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png";
            //Mandar a llamar mensajes de la transaccion de timbrado
            loadGridMsg();

        }
        else if (opc == 0)
        {
            rBtnCancelar.Enabled = true;
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelar.png";
            rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardar.png";

        }


    }


    private void loadGridMsg()
    {
        try
        {
            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteMensajes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@docCmpTrnId", DbType.String, 10, ParameterDirection.Input, docCmpTrnId);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (dsP.Tables.Count > 0)
            {
                rGdv_Documentos.DataSource = dsP;
                
            }
            else
            {
                rGdv_Documentos.DataSource = null;
            }

            rGdv_Documentos.DataBind();

        }
        catch (Exception ex)
        {
            rGdv_Documentos.DataSource = null;
            rGdv_Documentos.DataBind();
            throw;
        }

    }


    private void setSitTrnCmlDoc(int opc, int sit)
    {

        try
        {
            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteTransacciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_sUsrCve);

            ProcBD.AgregarParametrosProcedimiento("@docCmpTrnSit", DbType.Int64, 0, ParameterDirection.Input, sit);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (dsP != null)
            {

            }


        }
        catch (Exception)
        {

            throw;
        }


    }


    protected void rBtnBitacora_Click(object sender, ImageButtonClickEventArgs e)
    {
        //Recuperar ultimo
        getLastLog();
        
    }


    private void getLastLog()
    {

        try
        {

            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteTransacciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_sUsrCve);
            
            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (dsP != null)
            {
                if (dsP.Tables.Count > 0)
                {
                    if (dsP.Tables[0].Rows.Count > 0)
                    {

                        rGdv_Documentos.DataSource = dsP;


                    }
                }

            }


            rGdv_Documentos.DataBind();


        }
        catch (Exception)
        {

            throw;
        }

    }


}