using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;
public partial class DC_MttoCptoGuiasContabilizacionSus : System.Web.UI.Page
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

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    private string Pag_cptoTip;
    private string Pag_cptoGui;
    private string Pag_cta;
    private string Pag_valhd;

    #endregion
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
    private void InicioPagina()
    {



        ControlesAccion();
        if (Pag_cptoTip != "")
        {
            InfoCpto();
            LlenarGrid();
            FnCtlsFillIn.RadComboBox_TipoSusCont(Pag_sConexionLog, Pag_sCompania, ref rCboTipoReferecia, true, false, "");

        }

        //if (Pag_valhd == "2")
        //{
        //    pnlBtnsAcciones.Visible = true;
        //}else
        //{
        //    pnlBtnsAcciones.Visible = false;
        //}
        ControlesAccion();
        LimpiaControles();
        DesHabilita(false);
        lblCta.Text = Pag_cta;
        
        rBtnCancelar.Enabled = true;

        rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_GuiaContableSus.AllowMultiRowSelection = true;
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
        FNBtn.MAPerfiles_Operacion_Acciones(Page, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        Pag_cptoTip = Request.QueryString["cptoId"];
        Pag_cptoGui = Request.QueryString["cptoGui"];
        Pag_cta = Request.QueryString["cta"];
        Pag_valhd = Request.QueryString["vlhhd"];

    }



    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        Response.Redirect("MttoCptoGuiasContabilizacion.aspx?idM=" + Pag_sidM);
    }

    private void LlenarGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContableSustituciones";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@cptoGuiContId", DbType.Int64, 0, ParameterDirection.Input, Pag_cptoGui);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_GuiaContableSus, ds);
    }

    public void InfoCpto()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_cptoTip);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        radLabelConcepto.Text = ds.Tables[0].Rows[0]["cptoId"].ToString();
        radLabelConceptoDes.Text = ds.Tables[0].Rows[0]["cptoDes"].ToString();
    }

    protected void rCboTipoReferecia_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LLenaCombosTip();
        rCboTipoReferecia.CssClass = "cssTxtEnabled";
        rCboTipoReferecia.BorderWidth = Unit.Pixel(1);
        rCboTipoReferecia.BorderColor = System.Drawing.Color.White;

        rCboSecReferencia.CssClass = "cssTxtEnabled";
        rCboSecReferencia.BorderWidth = Unit.Pixel(1);
        rCboSecReferencia.BorderColor = System.Drawing.Color.White;

        rCboAgrupacion.CssClass = "cssTxtEnabled";
        rCboAgrupacion.BorderWidth = Unit.Pixel(1);
        rCboAgrupacion.BorderColor = System.Drawing.Color.White;

        rCboColeccion.CssClass = "cssTxtEnabled";
        rCboColeccion.BorderWidth = Unit.Pixel(1);
        rCboColeccion.BorderColor = System.Drawing.Color.White;


        RadNumInicial.CssClass = "cssTxtEnabled";
        RadNumFinal.CssClass = "cssTxtEnabled";
        RadNumPosiciones.CssClass = "cssTxtEnabled";

        rCboSecReferencia.ClearSelection();
        rCboAgrupacion.ClearSelection();
        rCboColeccion.ClearSelection();
        
    }

    public void LLenaCombosTip()
    {
        rCboSecReferencia.Enabled = false;
        rCboAgrupacion.Enabled = false;
        rCboColeccion.Enabled = false;

        

        if (rCboTipoReferecia.SelectedValue == "1" || rCboTipoReferecia.SelectedValue == "2" || rCboTipoReferecia.SelectedValue == "3")
        {

            if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png" || rBtnNuevo.Image.Url == "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png")
            {
                rCboSecReferencia.Enabled = true;
                rCboAgrupacion.Enabled = false;
                rCboColeccion.Enabled = false;
            }
            if (rCboTipoReferecia.SelectedValue == "1")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str10", ref rCboSecReferencia, true, false);
            }
            if (rCboTipoReferecia.SelectedValue == "2")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str20", ref rCboSecReferencia, true, false);
            }
            if (rCboTipoReferecia.SelectedValue == "3")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str40", ref rCboSecReferencia, true, false);
            }
        }

        if (rCboTipoReferecia.SelectedValue == "4" || rCboTipoReferecia.SelectedValue == "5" || rCboTipoReferecia.SelectedValue == "6"
            || rCboTipoReferecia.SelectedValue == "7" || rCboTipoReferecia.SelectedValue == "8" || rCboTipoReferecia.SelectedValue == "9")
        {

            if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png" || rBtnNuevo.Image.Url == "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png")
            {
                rCboSecReferencia.Enabled = false;
                rCboAgrupacion.Enabled = true;
                rCboColeccion.Enabled = false;
            }

            if (rCboTipoReferecia.SelectedValue == "4")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 1, ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "5")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 5,  ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "6")
            {
                //
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 2,  ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "7")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 3,  ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "8")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 33,  ref rCboAgrupacion, true, false, "");
            }

        }

        if (rCboTipoReferecia.SelectedValue == "9")
        {


            if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png" || rBtnNuevo.Image.Url == "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png")
            {
                rCboSecReferencia.Enabled = false;
                rCboAgrupacion.Enabled = false;
                rCboColeccion.Enabled = true;
            }
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 33, ref rCboAgrupacion, true, false, "");
        }

    }

    public void LLenaCombosTipEna()
    {
        rCboSecReferencia.Enabled = false;
        rCboAgrupacion.Enabled = false;
        rCboColeccion.Enabled = false;



        if (rCboTipoReferecia.SelectedValue == "1" || rCboTipoReferecia.SelectedValue == "2" || rCboTipoReferecia.SelectedValue == "3")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()
                || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rCboSecReferencia.Enabled = true;
                rCboAgrupacion.Enabled = false;
                rCboColeccion.Enabled = false;
            }


            if (rCboTipoReferecia.SelectedValue == "1")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str10", ref rCboSecReferencia, true, false);
            }
            if (rCboTipoReferecia.SelectedValue == "2")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str20", ref rCboSecReferencia, true, false);
            }
            if (rCboTipoReferecia.SelectedValue == "3")
            {
                FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_cptoTip, "Str40", ref rCboSecReferencia, true, false);
            }
        }

        if (rCboTipoReferecia.SelectedValue == "4" || rCboTipoReferecia.SelectedValue == "5" || rCboTipoReferecia.SelectedValue == "6"
            || rCboTipoReferecia.SelectedValue == "7" || rCboTipoReferecia.SelectedValue == "8" || rCboTipoReferecia.SelectedValue == "9")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()
                || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rCboSecReferencia.Enabled = false;
                rCboAgrupacion.Enabled = true;
                rCboColeccion.Enabled = false;
            }

            if (rCboTipoReferecia.SelectedValue == "4")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 1, ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "5")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 5, ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "6")
            {
                //
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 2, ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "7")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 3, ref rCboAgrupacion, true, false, "");
            }
            if (rCboTipoReferecia.SelectedValue == "8")
            {
                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 33, ref rCboAgrupacion, true, false, "");
            }

        }

        if (rCboTipoReferecia.SelectedValue == "9")
        {


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()
                || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rCboSecReferencia.Enabled = false;
                rCboAgrupacion.Enabled = false;
                rCboColeccion.Enabled = true;
            }
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, 33, ref rCboAgrupacion, true, false, "");
        }

    }

    private void EjecutaSpAcciones()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContableSustituciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(Pag_cptoGui));

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdv_GuiaContableSus.SelectedItems[0] as GridDataItem;
                string GuiContId = dataItem.GetDataKeyValue("cptoGuiContSusId").ToString();
                ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusId", DbType.Int64, 0, ParameterDirection.Input, GuiContId);
            }

            if (rCboTipoReferecia.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@sustTipId", DbType.Int64, 0, ParameterDirection.Input, rCboTipoReferecia.SelectedValue);
            }


            if (rCboSecReferencia.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref_Sec", DbType.Int64, 0, ParameterDirection.Input, rCboSecReferencia.SelectedValue);
            }
            if (rCboAgrupacion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rCboAgrupacion.SelectedValue.Trim());
            }
            if (rCboColeccion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@colCve", DbType.String, 3, ParameterDirection.Input, rCboColeccion.SelectedValue);
            }


            //////////////POSICIONES///////////////
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusPosIni", DbType.Int64, 0, ParameterDirection.Input, RadNumInicial.Text);
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusPosFin", DbType.Int64, 0, ParameterDirection.Input, RadNumFinal.Text);
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusPosNum", DbType.Int64, 0, ParameterDirection.Input, RadNumPosiciones.Text);



           

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                    LlenarGrid();
                    
                    ControlesAccion();
                    rBtnCancelar.Enabled = true;
                }
            }



        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
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

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
            }
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


            if (rCboTipoReferecia.SelectedValue == "")
            {
                rCboTipoReferecia.CssClass = "cssTxtInvalid";
                rCboTipoReferecia.BorderWidth = Unit.Pixel(1);
                rCboTipoReferecia.BorderColor = System.Drawing.Color.Red;
            }
            else { rCboTipoReferecia.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboSecReferencia.Enabled == true)
            {
                if (rCboSecReferencia.SelectedValue == "")
                {
                    rCboSecReferencia.CssClass = "cssTxtInvalid";
                    rCboSecReferencia.BorderWidth = Unit.Pixel(1);
                    rCboSecReferencia.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboSecReferencia.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (rCboAgrupacion.Enabled == true)
            {
                if (rCboAgrupacion.SelectedValue == "")
                {
                    rCboAgrupacion.CssClass = "cssTxtInvalid";
                    rCboAgrupacion.BorderWidth = Unit.Pixel(1);
                    rCboAgrupacion.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboAgrupacion.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (rCboColeccion.Enabled == true)
            {
                if (rCboColeccion.SelectedValue == "")
                {
                    rCboColeccion.CssClass = "cssTxtInvalid";
                    rCboColeccion.BorderWidth = Unit.Pixel(1);
                    rCboColeccion.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboColeccion.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (RadNumInicial.Text == "")
            {
                RadNumInicial.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }else
            {  RadNumInicial.CssClass = "cssTxtEnabled"; }

            if (RadNumFinal.Text == "")
            {
                RadNumFinal.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            { RadNumFinal.CssClass = "cssTxtEnabled"; }

            if (RadNumPosiciones.Text == "")
            {
                RadNumPosiciones.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            { RadNumPosiciones.CssClass = "cssTxtEnabled"; }




            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {


            if (rCboTipoReferecia.SelectedValue == "")
            {
                rCboTipoReferecia.CssClass = "cssTxtInvalid";
                rCboTipoReferecia.BorderWidth = Unit.Pixel(1);
                rCboTipoReferecia.BorderColor = System.Drawing.Color.Red;
            }
            else { rCboTipoReferecia.CssClass = "cssTxtEnabled"; }




            if (rCboSecReferencia.Enabled == true)
            {
                if (rCboSecReferencia.SelectedValue == "")
                {
                    rCboSecReferencia.CssClass = "cssTxtInvalid";
                    rCboSecReferencia.BorderWidth = Unit.Pixel(1);
                    rCboSecReferencia.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboSecReferencia.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (rCboAgrupacion.Enabled == true)
            {
                if (rCboAgrupacion.SelectedValue == "")
                {
                    rCboAgrupacion.CssClass = "cssTxtInvalid";
                    rCboAgrupacion.BorderWidth = Unit.Pixel(1);
                    rCboAgrupacion.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboAgrupacion.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (rCboColeccion.Enabled == true)
            {
                if (rCboColeccion.SelectedValue == "")
                {
                    rCboColeccion.CssClass = "cssTxtInvalid";
                    rCboColeccion.BorderWidth = Unit.Pixel(1);
                    rCboColeccion.BorderColor = System.Drawing.Color.Red;

                    camposInc += 1;
                }
                else { rCboColeccion.BorderColor = System.Drawing.Color.Transparent; }
            }

            if (RadNumInicial.Text == "")
            {
                RadNumInicial.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            { RadNumInicial.CssClass = "cssTxtEnabled"; }

            if (RadNumFinal.Text == "")
            {
                RadNumFinal.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            { RadNumFinal.CssClass = "cssTxtEnabled"; }

            if (RadNumPosiciones.Text == "")
            {
                RadNumPosiciones.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            { RadNumPosiciones.CssClass = "cssTxtEnabled"; }
            if (rGdv_GuiaContableSus.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }
        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_GuiaContableSus.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        

        return sResult;
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


            foreach (GridDataItem i in rGdv_GuiaContableSus.SelectedItems)
            {

                var dataItem = rGdv_GuiaContableSus.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string ContSusId = dataItem.GetDataKeyValue("cptoGuiContSusId").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContableSustituciones ";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusId", DbType.Int64, 0, ParameterDirection.Input, ContSusId);
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
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                }
                else
                {
                    hdfBtnAccion.Value = "";
                    InicioPagina();
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
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                    LlenarGrid();

                }
                else
                {
                    hdfBtnAccion.Value = "";
                    //LlenaGrid();
                    InicioPagina();
                    LlenarGrid();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();

    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {

       // LimpiaControles();
       // rGdv_GuiaContableSus.MasterTableView.ClearSelectedItems();

        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
        //    DesHabilita(false);
        //    rCboTipoReferecia.Enabled = false;
        //    rCboSecReferencia.Enabled = false;
        //    rCboAgrupacion.Enabled = false;
        //    rCboColeccion.Enabled = false;
        //}

        EjecutaAccionLimpiar();

    }

    //private void ControlesAccion()
    //{

    //    rGdv_GuiaContableSus.MasterTableView.ClearSelectedItems();

    //    //Limpia Controles
    //    LimpiaControles();
        

    //    //Deshabilita Controles
    //    DesHabilita(false);

    //    RadNumInicial.CssClass = "cssTxtEnabled";
    //    RadNumFinal.CssClass = "cssTxtEnabled";
    //    RadNumPosiciones.CssClass = "cssTxtEnabled";

    //    rCboTipoReferecia.BorderColor = System.Drawing.Color.White;
    //    rCboSecReferencia.BorderColor = System.Drawing.Color.White;
    //    rCboAgrupacion.BorderColor = System.Drawing.Color.White;
    //    rCboColeccion.BorderColor = System.Drawing.Color.White;

    //    rCboTipoReferecia.Enabled = false;
    //    rCboSecReferencia.Enabled = false;
    //    rCboAgrupacion.Enabled = false;
    //    rCboColeccion.Enabled = false;

     

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = false;
    //        DesHabilita(true);
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_GuiaContableSus.AllowMultiRowSelection = false;
    //        //DesHabilita(true);
    //        rCboTipoReferecia.Enabled = false;
    //    }

    //    //ELIMIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_GuiaContableSus.AllowMultiRowSelection = true;
    //        DesHabilita(false);
    //        LimpiaControles();
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
    //        //rtxtCve.Enabled = false;
    //        //rTxtDes.Enabled = false;
    //        //rTxtAbr.Enabled = false;
    //        //rTxtSigno.Enabled = false;
    //    }


    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //       )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //}

    private void DesHabilita(bool des)
    {
        rCboTipoReferecia.Enabled = des;
        //rCboSecReferencia.Enabled = des;
        //rCboAgrupacion.Enabled = des;
        //rCboColeccion.Enabled = des;

        RadNumInicial.Enabled = des;
        RadNumFinal.Enabled = des;
        RadNumPosiciones.Enabled = des;

    }

    private void LimpiaControles()
    {
        rCboTipoReferecia.ClearSelection();
        rCboSecReferencia.ClearSelection();
        rCboAgrupacion.ClearSelection();
        rCboColeccion.ClearSelection();
        RadNumInicial.Text = "";
        RadNumFinal.Text = "";
        RadNumPosiciones.Text = "";
    }


    protected void rGdv_GuiaContable_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EditGuiaConta();
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    DesHabilita(true);
            //}

          
        }
    }

    private void EditGuiaConta()
    {
        var dataItem = rGdv_GuiaContableSus.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string GuiContSusId = dataItem.GetDataKeyValue("cptoGuiContSusId").ToString();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContableSustituciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSusId", DbType.String, 10, ParameterDirection.Input, GuiContSusId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

           //tipo de referencia
            if (ds.Tables[0].Rows[0]["sustTipId"].ToString() != "")
            {
                rCboTipoReferecia.SelectedValue = ds.Tables[0].Rows[0]["sustTipId"].ToString();
                if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    rCboTipoReferecia.Enabled = true;
                }
            }
            else
            {
                rCboTipoReferecia.ClearSelection();
            }
            LLenaCombosTip();
            //Sec Referencia
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref_Sec"].ToString() != "")
            {

                rCboSecReferencia.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref_Sec"].ToString();
            }
            else
            {
                rCboSecReferencia.ClearSelection();
            }



            ////Agrupacion
            if (ds.Tables[0].Rows[0]["agrCve"].ToString() != "")
            {

                rCboAgrupacion.SelectedValue = ds.Tables[0].Rows[0]["agrCve"].ToString();
            }
            else
            {
                rCboAgrupacion.ClearSelection();
            }



            //Coleccion
            if (ds.Tables[0].Rows[0]["colCve"].ToString() != "")
            {

                rCboColeccion.SelectedValue = ds.Tables[0].Rows[0]["colCve"].ToString();
            }
            else
            {
                rCboColeccion.ClearSelection();
            }

            

            //Inicial
            if (ds.Tables[0].Rows[0]["cptoGuiContSusPosIni"].ToString() != "")
            {

                RadNumInicial.Text = ds.Tables[0].Rows[0]["cptoGuiContSusPosIni"].ToString();
            }
            else
            {
                RadNumInicial.Text = "";
            }

            //Final
            if (ds.Tables[0].Rows[0]["cptoGuiContSusPosFin"].ToString() != "")
            {

                RadNumFinal.Text = ds.Tables[0].Rows[0]["cptoGuiContSusPosFin"].ToString();
            }
            else
            {
                RadNumFinal.Text = "";
            }

            //POSICION
            if (ds.Tables[0].Rows[0]["cptoGuiContSusPosNum"].ToString() != "")
            {
                RadNumPosiciones.Text = ds.Tables[0].Rows[0]["cptoGuiContSusPosNum"].ToString();
            }
            else
            {
                RadNumPosiciones.Text = "";
            }
        }
    }

    private bool PosIni_Fin()
    {
        int ini, fin;
        ini = Convert.ToInt32(RadNumInicial.Text);
        fin = Convert.ToInt32(RadNumFinal.Text);

        if (ini > fin)
        {
            return false;
        }
        else if (fin < ini)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public void Posiciones()
    {

        if (PosIni_Fin() != true)
        {
            RadNumFinal.Text = "";
            RadWindowManagerPage.RadAlert("La Posicion Final no puede ser Menor a la Inicial", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }
    }



    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboTipoReferecia.BorderColor = System.Drawing.Color.Transparent;
        rCboSecReferencia.BorderColor = System.Drawing.Color.Transparent;
        rCboAgrupacion.BorderColor = System.Drawing.Color.Transparent;
        rCboColeccion.BorderColor = System.Drawing.Color.Transparent;
        RadNumInicial.CssClass = "cssTxtEnabled";
        RadNumFinal.CssClass = "cssTxtEnabled";
        RadNumPosiciones.CssClass = "cssTxtEnabled";

        
        rCboTipoReferecia.Enabled = false;
        rCboSecReferencia.Enabled = false;
        rCboAgrupacion.Enabled = false;
        rCboColeccion.Enabled = false;
        RadNumInicial.Enabled = false;
        RadNumFinal.Enabled = false;
        RadNumPosiciones.Enabled = false;


        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


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

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rCboTipoReferecia.Enabled = false;
            rCboSecReferencia.Enabled = false;
            rCboAgrupacion.Enabled = false;
            rCboColeccion.Enabled = false;
            RadNumInicial.Enabled = false;
            RadNumFinal.Enabled = false;
            RadNumPosiciones.Enabled = false;

            rCboTipoReferecia.ClearSelection();
            rCboSecReferencia.ClearSelection();
            rCboAgrupacion.ClearSelection();
            rCboColeccion.ClearSelection();
            RadNumInicial.Text = "";
            RadNumFinal.Text = "";
            RadNumPosiciones.Text = "";
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_GuiaContableSus.MasterTableView.ClearSelectedItems();



                //rCboTipoReferecia.Enabled = true;
                //rCboSecReferencia.Enabled = true;
                //rCboAgrupacion.Enabled = true;
                //rCboColeccion.Enabled = true;
                //RadNumInicial.Enabled = true;
                //RadNumFinal.Enabled = true;
                //RadNumPosiciones.Enabled = true;

                DesHabilita(true);

                rCboTipoReferecia.ClearSelection();
                rCboSecReferencia.ClearSelection();
                rCboAgrupacion.ClearSelection();
                rCboColeccion.ClearSelection();
                RadNumInicial.Text = "";
                RadNumFinal.Text="";
                RadNumPosiciones.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_GuiaContableSus.AllowMultiRowSelection = false;
                rCboTipoReferecia.Enabled = true;
                //rCboSecReferencia.Enabled = true;
                //rCboAgrupacion.Enabled = true;
                //rCboColeccion.Enabled = true;
                //RadNumInicial.Enabled = true;
                //RadNumFinal.Enabled = true;
                //RadNumPosiciones.Enabled = true;
                //LLenaCombosTip();


                if (rCboSecReferencia.SelectedValue != "")
                {
                    rCboSecReferencia.Enabled = true;
                }else {
                    rCboSecReferencia.Enabled = false;
                }
                if (rCboSecReferencia.SelectedValue != "")
                {
                    rCboSecReferencia.Enabled = true;
                }
                else
                {
                    rCboSecReferencia.Enabled = false;
                }
                if (rCboAgrupacion.SelectedValue != "")
                {
                    rCboAgrupacion.Enabled = true;
                }
                else
                {
                    rCboAgrupacion.Enabled = false;
                }
                

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;


            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_GuiaContableSus.AllowMultiRowSelection = true;
                rGdv_GuiaContableSus.MasterTableView.ClearSelectedItems();

                rCboTipoReferecia.Enabled = false;
                rCboSecReferencia.Enabled = false;
                rCboAgrupacion.Enabled = false;
                rCboColeccion.Enabled = false;
                RadNumInicial.Enabled = false;
                RadNumFinal.Enabled = false;
                RadNumPosiciones.Enabled = false;

                rCboTipoReferecia.ClearSelection();
                rCboSecReferencia.ClearSelection();
                rCboAgrupacion.ClearSelection();
                rCboColeccion.ClearSelection();
                RadNumInicial.Text = "";
                RadNumFinal.Text = "";
                RadNumPosiciones.Text = "";
            }
        }


        if (Result == false)
        {

            rCboTipoReferecia.Enabled = false;
            rCboSecReferencia.Enabled = false;
            rCboAgrupacion.Enabled = false;
            rCboColeccion.Enabled = false;
            RadNumInicial.Enabled = false;
            RadNumFinal.Enabled = false;
            RadNumPosiciones.Enabled = false;

            rCboTipoReferecia.ClearSelection();
            rCboSecReferencia.ClearSelection();
            rCboAgrupacion.ClearSelection();
            rCboColeccion.ClearSelection();
            RadNumInicial.Text = "";
            RadNumFinal.Text = "";
            RadNumPosiciones.Text = "";
            hdfBtnAccion.Value = "";
            rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_GuiaContableSus.AllowMultiRowSelection = true;
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_GuiaContableSus.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_GuiaContableSus, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_GuiaContableSus, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rCboTipoReferecia.ClearSelection();
            rCboSecReferencia.ClearSelection();
            rCboAgrupacion.ClearSelection();
            rCboColeccion.ClearSelection();
            RadNumInicial.Text = "";
            RadNumFinal.Text = "";
            RadNumPosiciones.Text = "";

            rCboSecReferencia.Enabled = false;
            rCboAgrupacion.Enabled = false;
            rCboColeccion.Enabled = false;


        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_GuiaContableSus.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboTipoReferecia.BorderColor = System.Drawing.Color.Transparent;
            rCboSecReferencia.BorderColor = System.Drawing.Color.Transparent;
            rCboAgrupacion.BorderColor = System.Drawing.Color.Transparent;
            rCboColeccion.BorderColor = System.Drawing.Color.Transparent;
            RadNumInicial.CssClass = "cssTxtEnabled";
            RadNumFinal.CssClass = "cssTxtEnabled";
            RadNumPosiciones.CssClass = "cssTxtEnabled";

            rCboTipoReferecia.Enabled = false;
            rCboSecReferencia.Enabled = false;
            rCboAgrupacion.Enabled = false;
            rCboColeccion.Enabled = false;
            RadNumInicial.Enabled = false;
            RadNumFinal.Enabled = false;
            RadNumPosiciones.Enabled = false;

            rCboTipoReferecia.ClearSelection();
            rCboSecReferencia.ClearSelection();
            rCboAgrupacion.ClearSelection();
            rCboColeccion.ClearSelection();
            RadNumInicial.Text = "";
            RadNumFinal.Text = "";
            RadNumPosiciones.Text = "";
            hdfBtnAccion.Value = "";
            rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_GuiaContableSus.AllowMultiRowSelection = true;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            hdfBtnAccion.Value = "";
            rGdv_GuiaContableSus.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_GuiaContableSus.AllowMultiRowSelection = true;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }



}