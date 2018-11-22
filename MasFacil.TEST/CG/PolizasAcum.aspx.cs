using System;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;

public partial class CG_PolizasAcum : System.Web.UI.Page
{

    #region VARIABLES
    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_smaUser;
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

    //=====> EVENTOS CONTROLES


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnPolizaImp_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
        InicioPagina();
    }

    //GRET, Se agrega evento para combo A.Contable,Descripcion,Concepto,Fecha
    protected void rCboPrefijo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizasFltr();
    }
    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizasFltr();
    }

    protected void rCboAContable_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizasFltr();
    }

    protected void rCboDescripcion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizasFltr();
    }

    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        LlenaGridPolizasFltr();
    }
    //

    #endregion


    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_smaUser = LM.sValSess(this.Page, 1);
    }
    public void InicioPagina()
    {
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        hdfBtnAccion.Value = "";
        TituloPagina();
        ControlesAccion();

        //FnCtlsFillIn.RadComboBox_ConceptosSeguridad(Pag_sConexionLog, Pag_sCompania, "CG", Pag_smaUser, 1, ref rCboConcepto, true, false, "");
        //FnCtlsFillIn.RadComboBox_Situaciones(Pag_sConexionLog, Pag_sCompania, 1, ref rCboSituacion, true, false, "");
        //rDateFecha.SelectedDate = null;

        LlenaGridPolizas();

        //GRET, Se agrega llamada a metodos para llenado de combo Prefijo,A.Contable,Descripcion, Concepto
        FnCtlsFillIn.RabComboBox_Prefijo(Pag_sConexionLog, Pag_sCompania, ref rCboPrefijo, true, false);
        FnCtlsFillIn.RabComboBox_AContable(Pag_sConexionLog, Pag_sCompania, ref rCboAContable, true, false);
        FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_CG(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);
        FnCtlsFillIn.RabComboBox_Descripcion(Pag_sConexionLog, Pag_sCompania, ref rCboDescripcion, true, false);
        //

        rGdvPolizas.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvPolizas.AllowMultiRowSelection = true;
        PermisoBotones();
    }

    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Poliza", "PnlMPFormTituloApartado");
    }


    private void LlenaGridPolizas()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
 
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPolizas, ds);
    }

    //GRET, se agrega metodo para llenar Grid mediante Filtros
    private void LlenaGridPolizasFltr()
    {
        DataSet ds = new DataSet();

        string Val_Fec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);

        if (RdDateFecha.SelectedDate != null)
        {
            Val_Fec = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
        }
        else
        {
            Val_Fec = "";
        }

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ACUMAsientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (rCboPrefijo.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@Pref", DbType.String, 10, ParameterDirection.Input, rCboPrefijo.SelectedValue);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@Pref", DbType.String, 10, ParameterDirection.Input, "");
        }

        if (rCboAContable.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, rCboAContable.SelectedValue);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, "");
        }

        if (rCboDescripcion.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@Desc", DbType.String, 300, ParameterDirection.Input,rCboDescripcion.SelectedValue);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@Desc", DbType.String, 300, ParameterDirection.Input, "");
        }

        if (rCboConcepto.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input,rCboConcepto.SelectedValue);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, "");
        }

        if (Val_Fec != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@PolFec", DbType.String, 100, ParameterDirection.Input, Val_Fec);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@PolFec", DbType.String, 100, ParameterDirection.Input, "");
        }


        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPolizas, ds);
    }
    //

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        //rBtnValidacion.Image.Url = "~/Imagenes/IcoBotones/IcoBtnValidacion.png";
        //rBtVerErr.Image.Url = "~/Imagenes/IcoBotones/IcoBtnVerErrores.png";
        //rBtnGenera.Image.Url = "~/Imagenes/IcoBotones/IcoBtnProcesar.png";
        //rBtnAplica.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnPolizaImp.Image.Url = "~/Imagenes/IcoBotones/IcoBtnImprimir.png";

        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucion(true);
        }
        else
        {
            ControlesAccionEjecucion(false);
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {

        if (Result == true)
        {
   

           //POLIZA IMPRIMIR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
            {
                EjecutaAccion();
            }
        }
    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";

        //IMPRIME POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            ImprimePoliza();
        }

    }

    //GRET, se agrega metodo para limpiar filtros
    private void EjecutaAccionLimpiar()
    {
        rCboPrefijo.ClearSelection();
        rCboAContable.ClearSelection();
        rCboDescripcion.ClearSelection();
        rCboConcepto.ClearSelection();
        RdDateFecha.Clear();
    }

    //

    private void ImprimePoliza()
    {
        string UrlPdf = "";
        string fil = "";
        string doc = "poliza";

        //DSdatos Grales
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ACUMAsientoContableRegGraf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        int contador = 0;
        foreach (GridDataItem dItem in rGdvPolizas.SelectedItems)
        {
            Int64 iAsiContEncId = Convert.ToInt64(dItem["asiContEncId"].Text);
            fil += iAsiContEncId.ToString();
        }


        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_ACUMAsientoContableRegGraf";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@fltrIds", DbType.String, 2048, ParameterDirection.Input, fil);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds) && FnValAdoNet.bDSRowsIsFill(ds1))
        {
            byte[] bytes = oWSRpt.byte_FormatoDoc(ds, ds1);

            UrlPdf = doc + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
            "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

            string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
            System.IO.File.WriteAllBytes(apppath, bytes);

            string script = @"<script type='text/javascript'> openRadWinRptPol('" + UrlPdf + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
        }

    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion


    #region FUNCIONES

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvPolizas.SelectedItems.Count;
        string[] GvVAS;

        //IMPRIME POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }


        return sResult;
    }

    private Boolean ValidaAccionesSituacionesOpe(ref string sMSGTip, ref string sResult)
    {
        Boolean bResult = true;

        var dataItem = rGdvPolizas.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string sSitCve = dataItem["polSit"].Text;
            int itransId = Convert.ToInt32(dataItem["asiContEncId"].Text);
            int iAccId = Convert.ToInt32(hdfBtnAccion.Value);

            if (FNValida.bAcciones_ValidaAccionesSituacionesAisCont(Pag_sConexionLog, Pag_sCompania, iAccId, sSitCve, itransId, ref sMSGTip, ref sResult) == false)
            {
                return false;
            }
        }

        return bResult;
    }

    #endregion

}