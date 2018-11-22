using System;
using System.Web.UI;
using System.Data;
using Telerik.Web.UI;


public partial class CG_Polizas : System.Web.UI.Page
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
    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizas();
    }
    protected void rCboSituacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridPolizas();
    }
    protected void rDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        LlenaGridPolizas();
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }
    protected void rBtnValidacion_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString();
        ControlesAccion();
    }
    protected void rBtVerErr_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString();
        ControlesAccion();
    }
    protected void rBtnGenera_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString();
        ControlesAccion();
    }
    protected void rBtnAplica_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString();
        ControlesAccion();
    }
    protected void rBtnPolizaEdit_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    protected void rBtnPolizaImp_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //EjecutaAccionLimpiar();
        InicioPagina();
    }



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

        FnCtlsFillIn.RadComboBox_ConceptosSeguridad(Pag_sConexionLog, Pag_sCompania, "CG", Pag_smaUser, 1, ref rCboConcepto, true, false, "");
        FnCtlsFillIn.RadComboBox_Situaciones(Pag_sConexionLog, Pag_sCompania, 1, ref rCboSituacion, true, false, "");
        rDateFecha.SelectedDate = null;

        LlenaGridPolizas();

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
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnValidacion.Image.Url = "~/Imagenes/IcoBotones/IcoBtnValidacion.png";
        rBtVerErr.Image.Url = "~/Imagenes/IcoBotones/IcoBtnVerErrores.png";
        rBtnGenera.Image.Url = "~/Imagenes/IcoBotones/IcoBtnProcesar.png";
        rBtnAplica.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
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
            //NUEVO  //MODIFICAR //ELIMIAR //VALIDACION   //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString()
                )
            {
                EjecutaAccion();
            }

            //GENERA  //APLICA //POLIZA EDITAR //POLIZA IMPRIMIR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString()
                )
            {
                EjecutaAccion();
            }
        }
    }
    private void EjecutaAccion()
    {
        string sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            Session["RawUrl_Return"] = hdfRawUrl.Value;
            Response.Redirect("~/CG/Poliza.aspx");
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            var dataItem = rGdvPolizas.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                Session["RawUrl_Return"] = hdfRawUrl.Value;
                string sAsiContEncId = dataItem.GetDataKeyValue("asiContEncId").ToString();
                Response.Redirect("~/CG/Poliza.aspx?AC=" + sAsiContEncId);
            }

        }
      

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EjecutaSpAccionEliminar();
        }

        //VALIDACION
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            EjecutaSpAccionValidacion();

        }

        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            var dataItem = rGdvPolizas.SelectedItems[0] as GridDataItem;
            string stransDetId = dataItem.GetDataKeyValue("asiContEncId").ToString();

            RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + "" + "&ProCve=" + "CGASICONT";
            string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        //GENERA / APLICA  
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
        {
            EjecutaSpAccion();
        }

        //EDITA POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
        {
            var dataItem = rGdvPolizas.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                string sAsiContEncId = dataItem.GetDataKeyValue("asiContEncId").ToString();
                Response.Redirect("~/CG/Poliza.aspx?AC=" + sAsiContEncId);
            }
        }

        //IMPRIME POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            ImprimePoliza();
        }

    }
    private void LlenaGridPolizas() {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_asientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@contRefCve", DbType.String, 2, ParameterDirection.Input, "CG");
        if (rCboConcepto.SelectedIndex != -1) {
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        }
        if (rCboSituacion.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@polSit", DbType.String, 3, ParameterDirection.Input, rCboSituacion.SelectedValue);
        }

        string sFecha = rDateFecha.SelectedDate.ToString();
        if (sFecha != "")
        {
            DateTime dt = Convert.ToDateTime(sFecha);
            sFecha = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
            ProcBD.AgregarParametrosProcedimiento("@polFec", DbType.String, 10, ParameterDirection.Input, sFecha);
        }

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPolizas, ds);
    }

    private void EjecutaSpAccion()
    {
        try
        {
            int CountItems = 0;
            int CountItems_Sit01 = 0;
            int CountItems_Sit02 = 0;
            int CountItems_Sit00 = 0;

            string smaMSGTip = "";
            string smaMSGDes = "";


            foreach (GridDataItem i in rGdvPolizas.SelectedItems)
            {

                var dataItem = this.rGdvPolizas.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    Int32 RegId = Convert.ToInt32(dataItem["asiContEncId"].Text);
                    //string sValiProcCve = Convert.ToString(Session["TipoCptoOpe"]) + "OPE";
                    string sitCve = dataItem["polSit"].Text;
                    string maUser = LM.sValSess(this.Page, 1);

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_EXPROACPoliza";
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, RegId);
                    ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, "");
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                    ProcBD.AgregarParametrosProcedimiento("@SitCve", DbType.String, 3, ParameterDirection.Input, sitCve);
                    ProcBD.AgregarParametrosProcedimiento("@acciId", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value.ToString());
                    ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, dataItem["polCve"].Text);
                   

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        smaMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        smaMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        if (smaMSGTip == "0")
                        {
                            CountItems_Sit00 += 1;
                        }
                        if (smaMSGTip == "1")
                        {
                            CountItems_Sit01 += 1;
                        }
                        if (smaMSGTip == "2")
                        {
                            CountItems_Sit02 += 1;
                        }
                    }
                    CountItems += 1;
                }

            }

            if (CountItems == 1)
            {
                ShowAlert(smaMSGTip, smaMSGDes);
            }
            else
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0000", ref smaMSGTip, ref smaMSGDes);

                if (CountItems_Sit01 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> Correctos: " + Convert.ToString(CountItems_Sit01);
                }
                if (CountItems_Sit02 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> Errores: " + Convert.ToString(CountItems_Sit02);
                }
                if (CountItems_Sit00 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> No validas: " + Convert.ToString(CountItems_Sit00);
                }
                ShowAlert(smaMSGTip, smaMSGDes);
            }


            if (CountItems_Sit01 + CountItems_Sit02 > 0)
            {
                LlenaGridPolizas();
            }



        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

    }

    private void EjecutaSpAccionEliminar()
    {

        try
        {

            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";

            foreach (GridDataItem i in rGdvPolizas.SelectedItems)
            {

                var dataItem = rGdvPolizas.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string sAsiContEncId = dataItem.GetDataKeyValue("asiContEncId").ToString();
                    //

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_AsientoContable";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, sAsiContEncId);
                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }

                            MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        string MsgError = ex.Message.Trim();
                    }


                }

                CountItems += 1;
            }

            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = MsgItemsElim + " " + CountItems.ToString();
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }
                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    LlenaGridPolizas();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {

                    LlenaGridPolizas();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }


            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }
                    string sMSGTip = "";
                    string sMSGRegNoElim = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0006", ref sMSGTip, ref sMSGRegNoElim);
                    sMsgAlert = sMsgAlert + sMSGRegNoElim + " " + CantItemsElimFalse.ToString();

                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    LlenaGridPolizas();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {
                    LlenaGridPolizas();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }

    private void EjecutaSpAccionValidacion()
    {
        int CountItems = 0;
        string smaMSGTip = "";
        string smaMSGDes = "";

        foreach (GridDataItem i in rGdvPolizas.SelectedItems)
        {
            var dataItem = rGdvPolizas.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {
                Int32 folioId = Convert.ToInt32(dataItem["asiContEncId"].Text);
                string sitCve = dataItem["polSit"].Text;
                string sValiProcCve = "CGASICONT";
                string maUser = LM.sValSess(this.Page, 1);

                try
                {
                    DataSet ds = new DataSet();
                    ds = FNValida.dsEXPROOpe_ValidacionesProcesos(Pag_sConexionLog, Pag_sCompania, folioId, sValiProcCve, maUser, sitCve);
                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        smaMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        smaMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    string MsgError = ex.Message.Trim();
                }

                CountItems += 1;
            }
        }

        if (CountItems == 1)
        {
            ShowAlert(smaMSGTip, smaMSGDes);
        }
        else
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VALI000", ref smaMSGTip, ref smaMSGDes);
            ShowAlert(smaMSGTip, smaMSGDes);
        }

        LlenaGridPolizas();
    }

    private void ImprimePoliza()
    {
        string UrlPdf = "";
        string fil = "";
        string doc = "poliza";

        //DSdatos Grales
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptPolizas";
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
        ProcBD1.NombreProcedimiento = "sp_RptPolizas";
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

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VALIDAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //GENERA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //APLICA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //EDITA POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //IMPRIME POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPolizas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
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