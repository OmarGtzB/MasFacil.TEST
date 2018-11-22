using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;


public partial class DC_MttoCptoGuiasContabilizacion : System.Web.UI.Page
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
    private string Pag_sCpto;

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
        EjecutaAccionLimpiar();

    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rBtnSustituciones_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnSustituciones";
        ControlesAccion();
    }

    protected void rGdv_GuiaContable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EditGuiaConta();
          
        }
    }


    #endregion

    #region METODO

    private void InicioPagina()
    {
        LimpiaControles();
        DesHabilita(false);
        if (Pag_sCpto != "")
        {
            llenaCombos();
            LLenaGrid();
            DatosCpto();
        }
        hdfBtnAccion.Value = "";
        ControlesAccion();
        rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_GuiaContable.AllowMultiRowSelection = true;
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
        Pag_sCpto = Convert.ToString(Session["folio_Selection"]);
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_GuiaContable.AllowMultiRowSelection = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rCboCodificacion.BorderColor = System.Drawing.Color.Transparent;

        MoviCargo.Enabled = false;
        MoviAbono.Enabled = false;
        SustitucionesSI.Enabled = false;
        SustitucionesNO.Enabled = false;
        rCboCodificacion.Enabled = false;
        rTxtDescripcion.Enabled = false;
        rCboReferencia1.Enabled = false;
        rCboReferencia2.Enabled = false;
        rCboReferencia3.Enabled = false;
        rCboReferencia4.Enabled = false;
        rCboMovimiento.Enabled = false;
        rCboVencimiento.Enabled = false;
        rCboCentroCosto.Enabled = false;
        rCboTipCambio.Enabled = false;
        rCboImporte.Enabled = false;

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
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString() &&
         hdfBtnAccion.Value != "rBtnSustituciones"
          )
        {
            MoviCargo.Enabled = false;
            MoviAbono.Enabled = false;
            SustitucionesSI.Enabled = false;
            SustitucionesNO.Enabled = false;
            rCboCodificacion.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboCentroCosto.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;


            MoviCargo.Checked = true;
            MoviAbono.Checked = false;
            SustitucionesSI.Checked = true;
            SustitucionesNO.Checked = false;
            rCboCodificacion.ClearSelection();
            rTxtDescripcion.Text = "";
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboCentroCosto.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
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
                this.rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_GuiaContable.MasterTableView.ClearSelectedItems();

                MoviCargo.Enabled = true;
                MoviAbono.Enabled = true;
                SustitucionesSI.Enabled = true;
                SustitucionesNO.Enabled = true;
                rCboCodificacion.Enabled = true;
                rTxtDescripcion.Enabled = true;
                rCboReferencia1.Enabled = true;
                rCboReferencia2.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboMovimiento.Enabled = true;
                rCboVencimiento.Enabled = true;
                rCboCentroCosto.Enabled = true;
                rCboTipCambio.Enabled = true;
                rCboImporte.Enabled = true;

                MoviCargo.Checked = true;
                MoviAbono.Checked = false;
                SustitucionesSI.Checked = true;
                SustitucionesNO.Checked = false;
                rCboCodificacion.ClearSelection();
                rTxtDescripcion.Text = "";
                rCboReferencia1.ClearSelection();
                rCboReferencia2.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboMovimiento.ClearSelection();
                rCboVencimiento.ClearSelection();
                rCboCentroCosto.ClearSelection();
                rCboTipCambio.ClearSelection();
                rCboImporte.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_GuiaContable.AllowMultiRowSelection = false;

                MoviCargo.Enabled = true;
                MoviAbono.Enabled = true;
                SustitucionesSI.Enabled = true;
                SustitucionesNO.Enabled = true;
                rCboCodificacion.Enabled = true;
                rTxtDescripcion.Enabled = true;
                rCboReferencia1.Enabled = true;
                rCboReferencia2.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboMovimiento.Enabled = true;
                rCboVencimiento.Enabled = true;
                rCboCentroCosto.Enabled = true;
                rCboTipCambio.Enabled = true;
                rCboImporte.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //SUSTITUCIONES
            if (hdfBtnAccion.Value == "rBtnSustituciones")
            {
                if (SustitucionesSI.Checked == true)
                {
                    var dataItem = rGdv_GuiaContable.SelectedItems[0] as GridDataItem;
                    string GuiContId;
                    if (dataItem != null)
                    {
                        Int64 Pag_sidM = 0;
                        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                        {
                            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                        }
                        GuiContId = dataItem.GetDataKeyValue("cptoGuiContId").ToString();
                        Response.Redirect("MttoCptoGuiasContabilizacionSus.aspx?cptoId=" + Pag_sCpto + "&cptoGui=" + GuiContId + "&cta=" + rCboCodificacion.SelectedValue + " " + rCboCodificacion.Text + "&idM=" + Pag_sidM

                            + "&vlhhd=" + hdfBtnAccion.Value);
                    }
                }
                else {
                    string sResult = "", sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1039", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                }
                
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_GuiaContable.AllowMultiRowSelection = true;
                rGdv_GuiaContable.MasterTableView.ClearSelectedItems();

                MoviCargo.Enabled = false;
                MoviAbono.Enabled = false;
                SustitucionesSI.Enabled = false;
                SustitucionesNO.Enabled = false;
                rCboCodificacion.Enabled = false;
                rTxtDescripcion.Enabled = false;
                rCboReferencia1.Enabled = false;
                rCboReferencia2.Enabled = false;
                rCboReferencia3.Enabled = false;
                rCboReferencia4.Enabled = false;
                rCboMovimiento.Enabled = false;
                rCboVencimiento.Enabled = false;
                rCboCentroCosto.Enabled = false;
                rCboTipCambio.Enabled = false;
                rCboImporte.Enabled = false;

                MoviCargo.Checked = true;
                MoviAbono.Checked = false;
                SustitucionesSI.Checked = true;
                SustitucionesNO.Checked = false;
                rCboCodificacion.ClearSelection();
                rTxtDescripcion.Text = "";
                rCboReferencia1.ClearSelection();
                rCboReferencia2.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboMovimiento.ClearSelection();
                rCboVencimiento.ClearSelection();
                rCboCentroCosto.ClearSelection();
                rCboTipCambio.ClearSelection();
                rCboImporte.ClearSelection();
            }
        }


        if (Result == false)
        {
            MoviCargo.Enabled = false;
            MoviAbono.Enabled = false;
            SustitucionesSI.Enabled = false;
            SustitucionesNO.Enabled = false;
            rCboCodificacion.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboCentroCosto.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;

            MoviCargo.Checked = true;
            MoviAbono.Checked = false;
            SustitucionesSI.Checked = true;
            SustitucionesNO.Checked = false;
            rCboCodificacion.ClearSelection();
            rTxtDescripcion.Text = "";
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboCentroCosto.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_GuiaContable.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_GuiaContable, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //SUSTITUCIONES
        if (hdfBtnAccion.Value == "rBtnSustituciones")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_GuiaContable, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_GuiaContable, GvVAS, ref sMSGTip, ref sResult) == false)
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
            MoviCargo.Checked = true;
            MoviAbono.Checked = false;
            SustitucionesSI.Checked = true;
            SustitucionesNO.Checked = false;
            rCboCodificacion.ClearSelection();
            rTxtDescripcion.Text = "";
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboCentroCosto.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_GuiaContable.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rCboCodificacion.BorderColor = System.Drawing.Color.Transparent;

            MoviCargo.Enabled = false;
            MoviAbono.Enabled = false;
            SustitucionesSI.Enabled = false;
            SustitucionesNO.Enabled = false;
            rCboCodificacion.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboCentroCosto.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;

            MoviCargo.Checked = true;
            MoviAbono.Checked = false;
            SustitucionesSI.Checked = true;
            SustitucionesNO.Checked = false;
            rCboCodificacion.ClearSelection();
            rTxtDescripcion.Text = "";
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboCentroCosto.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
        rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_GuiaContable.AllowMultiRowSelection = true;
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
                rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_GuiaContable.AllowMultiRowSelection = true;
            }
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                rGdv_GuiaContable.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_GuiaContable.AllowMultiRowSelection = true;
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaSpAcciones()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContable";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int32, 0, ParameterDirection.Input, Pag_sCpto);
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContCoA", DbType.Int32, 0, ParameterDirection.Input, ValChecksMovi());

            ///SUSTITUCIONES
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContSus", DbType.Int32, 0, ParameterDirection.Input, ValChecksSus());

            //////////////COMBO PENDIENTE///////////////
            //else if
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, rCboCodificacion.SelectedValue);

            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());

            if (rCboReferencia1.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_01", DbType.Int32, 0, ParameterDirection.Input, rCboReferencia1.SelectedValue);
            }
            if (rCboReferencia2.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_02", DbType.Int32, 0, ParameterDirection.Input, rCboReferencia2.SelectedValue);
            }
            if (rCboReferencia3.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_03", DbType.Int32, 0, ParameterDirection.Input, rCboReferencia3.SelectedValue);
            }
            if (rCboReferencia4.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_04", DbType.Int32, 0, ParameterDirection.Input, rCboReferencia4.SelectedValue);
            }

            if (rCboCentroCosto.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_CCO", DbType.Int32, 0, ParameterDirection.Input, rCboCentroCosto.SelectedValue);
            }
            if (rCboMovimiento.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_01", DbType.Int32, 0, ParameterDirection.Input, rCboMovimiento.SelectedValue);
            }
            if (rCboVencimiento.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_02", DbType.Int32, 0, ParameterDirection.Input, rCboVencimiento.SelectedValue);
            }

            if (rCboTipCambio.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_TipCam", DbType.Int32, 0, ParameterDirection.Input, rCboTipCambio.SelectedValue);
            }



            if (rCboImporte.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Imp_01", DbType.Int32, 0, ParameterDirection.Input, rCboImporte.SelectedValue);
            }



            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdv_GuiaContable.SelectedItems[0] as GridDataItem;
                string GuiContId = dataItem.GetDataKeyValue("cptoGuiContId").ToString();
                ProcBD.AgregarParametrosProcedimiento("@cptoGuiContId", DbType.Int64, 0, ParameterDirection.Input, GuiContId);
            }

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    InicioPagina();
                    LLenaGrid();
                }
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


            foreach (GridDataItem i in rGdv_GuiaContable.SelectedItems)
            {

                var dataItem = rGdv_GuiaContable.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string ContSusId = dataItem.GetDataKeyValue("cptoGuiContId").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContable";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@cptoGuiContId", DbType.Int64, 0, ParameterDirection.Input, ContSusId);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
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
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
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
                    InicioPagina();
                    //LlenaGrid();

                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                    //LlenarGrid();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    #endregion
    
    #region FUNCIONES
    public void llenaCombos()
    {
        //Referencias 10
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Str10", ref rCboReferencia1, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Str10", ref rCboReferencia2, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Str10", ref rCboReferencia3, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Str10", ref rCboReferencia4, true, false);
        //Fechas
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Fec", ref rCboMovimiento, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Fec", ref rCboVencimiento, true, false);
        //Tipo de Cambio 
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Fact", ref rCboTipCambio, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Str10", ref rCboCentroCosto, true, false);
        FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, Pag_sCpto, "Imp", ref rCboImporte, true, false);
        FnCtlsFillIn.RadComboBox_CodificacionCuentas(Pag_sConexionLog, Pag_sCompania, ref rCboCodificacion, true, false);

    }

    private void DesHabilita(bool des)
    {
        rCboReferencia1.Enabled = des;
        rCboReferencia2.Enabled = des;
        rCboReferencia3.Enabled = des;
        rCboReferencia4.Enabled = des;
        rCboMovimiento.Enabled = des;
        rCboVencimiento.Enabled = des;
        rCboCentroCosto.Enabled = des;
        rCboImporte.Enabled = des;
        rCboTipCambio.Enabled = des;
        rTxtDescripcion.Enabled = des;
        MoviCargo.Enabled = des;
        MoviAbono.Enabled = des;
        SustitucionesSI.Enabled = des;
        SustitucionesNO.Enabled = des;
        rCboCodificacion.Enabled = des;



    }

    private void LimpiaControles()
    {
        rCboReferencia1.ClearSelection();
        rCboReferencia2.ClearSelection();
        rCboReferencia3.ClearSelection();
        rCboReferencia4.ClearSelection();
        rCboMovimiento.ClearSelection();
        rCboVencimiento.ClearSelection();
        rCboCentroCosto.ClearSelection();
        rCboImporte.ClearSelection();
        rCboTipCambio.ClearSelection();
        rCboCodificacion.ClearSelection();
        rTxtDescripcion.Text = "";
        MoviCargo.Checked = true;
        MoviAbono.Checked = false;
        SustitucionesSI.Checked = true;
        SustitucionesNO.Checked = false;
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


            if (rTxtDescripcion.Text == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rCboCodificacion.SelectedValue == "")
            {
                rCboCodificacion.CssClass = "cssTxtInvalid";
                rCboCodificacion.BorderWidth = Unit.Pixel(1);
                rCboCodificacion.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCodificacion.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {


            if (rTxtDescripcion.Text == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rCboCodificacion.SelectedValue == "")
            {
                rCboCodificacion.CssClass = "cssTxtInvalid";
                rCboCodificacion.BorderWidth = Unit.Pixel(1);
                rCboCodificacion.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCodificacion.BorderColor = System.Drawing.Color.Transparent; }


            if (rGdv_GuiaContable.SelectedItems.Count == 0)
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

            if (rGdv_GuiaContable.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }





        return sResult;
    }

    public void LLenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, Pag_sCpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_GuiaContable, ds);
    }



    public int ValChecksMovi()
    {
        if (MoviCargo.Checked == true)
        {
            return 1;

        }
        else if (MoviAbono.Checked == true)
        {
            return 2;
        }
        return 0;
    }

    public int ValChecksSus()
    {
        if (SustitucionesSI.Checked == true)
        {
            return 1;

        }
        else if (SustitucionesNO.Checked == true)
        {
            return 2;
        }
        return 0;
    }

    private void DatosCpto()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_sCpto);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        radLabelConcepto.Text = ds.Tables[0].Rows[0]["cptoId"].ToString();
        radLabelConceptoDes.Text = ds.Tables[0].Rows[0]["cptoDes"].ToString();
    }
    
    private void EditGuiaConta()
    {
        var dataItem = rGdv_GuiaContable.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string GuiContId = dataItem.GetDataKeyValue("cptoGuiContId").ToString();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoGuiaContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoGuiContId", DbType.String, 10, ParameterDirection.Input, GuiContId);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //Movimiento CARGO ABONO
            if (ds.Tables[0].Rows[0]["cptoGuiContCoA"].ToString() == "1")
            {
                MoviCargo.Checked = true;
                MoviAbono.Checked = false;
            }
            if (ds.Tables[0].Rows[0]["cptoGuiContCoA"].ToString() == "2")
            {
                MoviCargo.Checked = false;
                MoviAbono.Checked = true;
            }

            //Sustitucines SI / NO
            if (ds.Tables[0].Rows[0]["cptoGuiContSus"].ToString() == "1")
            {
                SustitucionesSI.Checked = true;
                SustitucionesNO.Checked = false;
            }
            if (ds.Tables[0].Rows[0]["cptoGuiContSus"].ToString() == "2")
            {
                SustitucionesSI.Checked = false;
                SustitucionesNO.Checked = true;
            }

            //comb orCboCodificaciónCont///////////////////////////////////////////7
            if (ds.Tables[0].Rows[0]["ctaContCve"].ToString() != "")
            {
                rCboCodificacion.SelectedValue = ds.Tables[0].Rows[0]["ctaContCve"].ToString();
            }
            else
            {
                rCboCodificacion.ClearSelection();
            }


            //descipcion
            if (ds.Tables[0].Rows[0]["cptoGuiContDes"].ToString() != "")
            {
                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["cptoGuiContDes"].ToString();
            }
            else
            {
                rTxtDescripcion.Text = "";
            }

            //Referencia 1
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref10_01"].ToString() != "")
            {
                rCboReferencia1.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_01"].ToString();
            }
            else
            {
                rCboReferencia1.ClearSelection();
            }
            //Referencia 2
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref10_02"].ToString() != "")
            {
                rCboReferencia2.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_02"].ToString();
            }
            else
            {
                rCboReferencia2.ClearSelection();
            }
            //Referencia 3
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref10_03"].ToString() != "")
            {
                rCboReferencia3.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_03"].ToString();
            }
            else
            {
                rCboReferencia3.ClearSelection();
            }
            //Referencia 4
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref10_04"].ToString() != "")
            {
                rCboReferencia4.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_04"].ToString();
            }
            else
            {
                rCboReferencia4.ClearSelection();
            }

            //CENTRO DE COSTOS
            if (ds.Tables[0].Rows[0]["cptoConfId_Ref10_CCO"].ToString() != "")
            {
                rCboCentroCosto.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_CCO"].ToString();
            }
            else
            {
                rCboCentroCosto.ClearSelection();
            }

            //FECHA 1
            if (ds.Tables[0].Rows[0]["cptoConfId_Fec_01"].ToString() != "")
            {
                rCboMovimiento.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_01"].ToString();
            }
            else
            {
                rCboMovimiento.ClearSelection();
            }

            //FECHA 2
            if (ds.Tables[0].Rows[0]["cptoConfId_Fec_02"].ToString() != "")
            {
                rCboVencimiento.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_02"].ToString();
            }
            else
            {
                rCboVencimiento.ClearSelection();
            }

            //Tipo Cambio
            if (ds.Tables[0].Rows[0]["cptoConfId_Fac_TipCam"].ToString() != "")
            {
                rCboTipCambio.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_TipCam"].ToString();
            }
            else
            {
                rCboTipCambio.ClearSelection();
            }

            //Tipo Cambio
            if (ds.Tables[0].Rows[0]["cptoConfId_Imp_01"].ToString() != "")
            {
                rCboImporte.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Imp_01"].ToString();
            }
            else
            {
                rCboImporte.ClearSelection();
            }



        }
    }





    #endregion











}