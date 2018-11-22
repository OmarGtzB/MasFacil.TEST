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
using System.Windows.Forms;

public partial class DC_CuentasDeposito : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string PagLoc_ArtCve;
    


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



    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rNoFormato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboFormato.SelectedIndex == -1)
        {
            rNoFormato.ClearSelection();
            rNoFormato.Enabled = false;

        }
        {
            rNoFormato.Enabled = true;
        }
    }
    protected void rCboFormato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboFormato.SelectedValue == "")
        {
            rNoFormato.ClearSelection();
            rNoFormato.Enabled = false;
        }
        else
        {
            rNoFormato.Enabled = true;
        }
    }
protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        //this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = false;
        ////RadImageGallery1.ToolbarSettings.ShowFullScreenButton = true;
        //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        ControlesAccion();
        //limpiar();
        //habilita();

    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        //this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        ////RadImageGallery1.ToolbarSettings.ShowFullScreenButton = true;
        ControlesAccion();
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //deshabilita();
        //limpiar();

    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        //this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        ////RadImageGallery1.ToolbarSettings.ShowFullScreenButton = true;
        ControlesAccion();
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //deshabilita();
        //limpiar();
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
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        //RadImageGallery1.ToolbarSettings.ShowFullScreenButton = false;
        hdfBtnAccion.Value = "";
        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    public void InicioPagina()
    {
        ControlesAccion();
        llena_radgrid();
        deshabilita();
        limpiar();

        llenaCombos();

        desabilitaBtn();
        BtnCss();
        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        hdfBtnAccion.Value = "";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rCboInstitucion.BorderWidth = Unit.Pixel(1);
        rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;

        RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        RGridCuentasDeposito.AllowMultiRowSelection = true;
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
    public void limpiar()
    {
        rtxtClaveCuenta.Text = "";
        rtTxtDescripcion.Text = "";
        rCboInstitucion.ClearSelection();
        rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
        rNoFormato.BorderColor = System.Drawing.Color.Transparent;

        rTxtSucursal.Text = "";
        rTxtFolio.Text = "";
        rCboMoneda.ClearSelection();
        rCboFormato.ClearSelection();
        rNoFormato.ClearSelection();

        txtAreaLeyenda.InnerText = "";
        rTxtNumCuenta.Text = "";
        
        //RadImageGallery1.DataSource = GetDataTable();
        //RadImageGallery1.DataBind();

        //RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
        //RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;


    }




    public void habilita()
    {
        rtxtClaveCuenta.Enabled = true;
        rTxtNumCuenta.Enabled = true;
        rtTxtDescripcion.Enabled = true;
        rCboInstitucion.Enabled = true;
        rTxtSucursal.Enabled = true;
        rTxtFolio.Enabled = true;
        rCboMoneda.Enabled = true;
        //rCboNOFormato.Enabled = true;
        rNoFormato.Enabled = true;
        txtAreaLeyenda.Disabled = true;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rCboFormato.Enabled = true;

        //RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
        //RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;

    }
    public void deshabilita()
    {
        rtxtClaveCuenta.Enabled = false;
        rtTxtDescripcion.Enabled = false;
        rCboInstitucion.Enabled = false;
        rTxtSucursal.Enabled = false;
        rTxtFolio.Enabled = false;
        rCboMoneda.Enabled = false;
        rTxtEstadoEmision.Enabled = false; 
        rCboFormato.Enabled = false;
        //rCboNOFormato.Enabled = false;
        rNoFormato.Enabled = false;
        txtAreaLeyenda.Disabled = true;
        rTxtNumCuenta.Enabled = false;

        //RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
        //RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

    }

    public void llenaCombos()
    {

        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false); 
        
        FnCtlsFillIn.RadComboBox_InstitucionesDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboInstitucion, true, false);

        FnCtlsFillIn.RadComboBox_FormatoCheque(Pag_sConexionLog, Pag_sCompania, ref rCboFormato, true, false);
      



        FnCtlsFillIn.RadComboBox_FormatosImpresion(Pag_sConexionLog, Pag_sCompania, 2, ref rNoFormato, true, false);

     
    }


    public void desabilitaBtn()
    {
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
    }

    public void habilitaBtn()
    {
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }

    //private string validaEjecutaAccion(ref string sMSGTip)
    //{

    //    string sResult = "";
    //    int camposInc = 0;
    //    BtnCss();

    //    // NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        //Clave cDepCve
    //        if (rtxtClaveCuenta.Text.Trim() == "")
    //        {
    //            rtxtClaveCuenta.CssClass = "cssTxtInvalid";
    //            camposInc += 1;
    //        }
    //        else
    //        {
    //            rtxtClaveCuenta.CssClass = "cssTxtEnabled";
    //        }
    //        //Descripcion 
    //        if (rtTxtDescripcion.Text.Trim() == "")
    //        {

    //            rtTxtDescripcion.CssClass = "cssTxtInvalid";
    //            camposInc += 1;
    //        }
    //        else
    //        {
    //            rtTxtDescripcion.CssClass = "cssTxtEnabled";
    //        }
    //        //institucion insDepCve
    //        if (rCboInstitucion.SelectedValue == "")
    //        {

    //            rCboInstitucion.CssClass = "cssTxtInvalid";
    //            rCboInstitucion.BorderWidth = Unit.Pixel(1);
    //            rCboInstitucion.BorderColor = System.Drawing.Color.Red;
    //            camposInc += 1;
    //        }
    //        else
    //        {
    //            rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
    //        }
    //        //sucursal cDepSuc
    //        if (rTxtSucursal.Text.Trim() == "")
    //        {

    //            rTxtSucursal.CssClass = "cssTxtInvalid";
    //            camposInc += 1;
    //        }
    //        else
    //        {
    //            rTxtSucursal.CssClass = "cssTxtEnabled";

    //            //numero de cuenta cDepForNoCta
    //            if (rTxtNumCuenta.Text.Trim() == "")
    //            {

    //                rTxtNumCuenta.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            }
    //            else
    //            {
    //                rTxtNumCuenta.CssClass = "cssTxtEnabled";
    //            }
    //            //Folio DepFolio
    //            if (rTxtFolio.Text.Trim() == "")
    //            {

    //                rTxtFolio.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            }
    //            else
    //            {
    //                rTxtFolio.CssClass = "cssTxtEnabled";
    //            }
    //            if (rCboMoneda.SelectedValue == "")
    //            {

    //                rCboMoneda.CssClass = "cssTxtInvalid";
    //                rCboMoneda.BorderWidth = Unit.Pixel(1);
    //                rCboMoneda.BorderColor = System.Drawing.Color.Red;
    //                camposInc += 1;
    //            }
    //            else
    //            {
    //                rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
    //            }

    //            ///////////Formato Cheque//////////


    //            //if (rCboFormato.SelectedValue != "")
    //            //{
    //            //    if (rNoFormato.SelectedValue == "")
    //            //    {

    //            //        rNoFormato.CssClass = "cssTxtInvalid";
    //            //        rNoFormato.BorderWidth = Unit.Pixel(1);
    //            //        rNoFormato.BorderColor = System.Drawing.Color.Red;
    //            //        camposInc += 1;
    //            //    }
    //            //}



    //            //if (rNoFormato.Text == "")
    //            //{

    //            //    rNoFormato.CssClass = "cssTxtInvalid";
    //            //    camposInc += 1;
    //            //}



    //            if (camposInc > 0)
    //            {
    //                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
    //            }
    //            return sResult;

    //            rBtnGuardar.Enabled = false;
    //            rBtnCancelar.Enabled = false;
    //        }
    //    }

    //        //MODIFICAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //        {
    //            if (RGridCuentasDeposito.SelectedItems.Count == 0)
    //            {
    //                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
    //                return sResult;
    //            }


    //            if (rtxtClaveCuenta.Text.Trim() == "")
    //            {
    //                rtxtClaveCuenta.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            } else {
    //                rtxtClaveCuenta.CssClass = "cssTxtInvalid";
    //            }

    //            if (rtTxtDescripcion.Text.Trim() == "")
    //            {

    //                rtTxtDescripcion.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            } else {
    //                rtTxtDescripcion.CssClass = "cssTxtInvalid";
    //            }
    //            if (rCboInstitucion.SelectedValue == "")
    //            {

    //                rCboInstitucion.CssClass = "cssTxtInvalid";
    //                rCboInstitucion.BorderWidth = Unit.Pixel(1);
    //                rCboInstitucion.BorderColor = System.Drawing.Color.Red;
    //                camposInc += 1;
    //            } else {
    //                rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
    //            }
    //            if (rTxtSucursal.Text.Trim() == "")
    //            {

    //                rTxtSucursal.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            } else {
    //                rTxtSucursal.CssClass = "cssTxtInvalid";
    //            }
    //            if (rTxtNumCuenta.Text.Trim() == "")
    //            {

    //                rTxtNumCuenta.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            } else {
    //                rTxtNumCuenta.CssClass = "cssTxtInvalid";
    //            }
    //            if (rTxtFolio.Text.Trim() == "")
    //            {

    //                rTxtFolio.CssClass = "cssTxtInvalid";
    //                camposInc += 1;
    //            } else {
    //                rTxtFolio.CssClass = "cssTxtInvalid";
    //            }
    //            if (rCboMoneda.SelectedValue == "")
    //            {

    //                rCboMoneda.CssClass = "cssTxtInvalid";
    //                rCboMoneda.BorderWidth = Unit.Pixel(1);
    //                rCboMoneda.BorderColor = System.Drawing.Color.Red;
    //                camposInc += 1;
    //            } else {
    //                rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
    //            }



    //            if (camposInc > 0)
    //            {
    //                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
    //            }
    //            return sResult;
    //        }

    //        //ELIMINAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //        {

    //            if (RGridCuentasDeposito.SelectedItems.Count == 0)
    //            {
    //                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
    //                return sResult;
    //            }


    //            return sResult;
    //        }

    //        return sResult;
    //    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rtxtClaveCuenta.Text.Trim() == "")
            {
                rtxtClaveCuenta.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClaveCuenta.CssClass = "cssTxtEnabled"; }

            if (rtTxtDescripcion.Text.Trim() == "")
            {
                rtTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rCboInstitucion.SelectedValue == "")
            {
                rCboInstitucion.BorderWidth = Unit.Pixel(1);
                rCboInstitucion.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboInstitucion.BorderColor = System.Drawing.Color.Transparent; }

            if (rTxtSucursal.Text.Trim() == "")
            {
                rTxtSucursal.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSucursal.CssClass = "cssTxtEnabled"; }

            if (rTxtNumCuenta.Text.Trim() == "")
            {
                rTxtNumCuenta.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNumCuenta.CssClass = "cssTxtEnabled"; }
            //Folio DepFolio
            if (rTxtFolio.Text.Trim() == "")
            {

                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }

            if (rCboMoneda.SelectedValue == "")
            {
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboMoneda.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboFormato.SelectedValue != "")
            {
                if (rNoFormato.SelectedValue == "")
                {

                    rNoFormato.CssClass = "cssTxtInvalid";
                    rNoFormato.BorderWidth = Unit.Pixel(1);
                    rNoFormato.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }else {
                    rNoFormato.BorderColor = System.Drawing.Color.Transparent;
                }
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (RGridCuentasDeposito.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            if (rtxtClaveCuenta.Text.Trim() == "")
            {
                rtxtClaveCuenta.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClaveCuenta.CssClass = "cssTxtEnabled"; }

            if (rtTxtDescripcion.Text.Trim() == "")
            {
                rtTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rCboInstitucion.SelectedValue == "")
            {
                rCboInstitucion.BorderWidth = Unit.Pixel(1);
                rCboInstitucion.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboInstitucion.BorderColor = System.Drawing.Color.Transparent; }

            if (rTxtSucursal.Text.Trim() == "")
            {
                rTxtSucursal.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSucursal.CssClass = "cssTxtEnabled"; }

            if (rTxtNumCuenta.Text.Trim() == "")
            {
                rTxtNumCuenta.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNumCuenta.CssClass = "cssTxtEnabled"; }
            //Folio DepFolio
            if (rTxtFolio.Text.Trim() == "")
            {

                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }

            if (rCboMoneda.SelectedValue == "")
            {
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboMoneda.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboFormato.SelectedValue != "")
            {
                if (rNoFormato.SelectedValue == "")
                {

                    rNoFormato.CssClass = "cssTxtInvalid";
                    rNoFormato.BorderWidth = Unit.Pixel(1);
                    rNoFormato.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rNoFormato.BorderColor = System.Drawing.Color.Transparent;
                }
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

            if (RGridCuentasDeposito.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
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
                enviar_metVal();
                //pagina_inicio();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }

    }

    public void enviar_metVal()
    {

        
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cDepCve", DbType.String, 10, ParameterDirection.Input, rtxtClaveCuenta.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cDepDes", DbType.String, 100, ParameterDirection.Input, rtTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@insDepCve", DbType.String, 20, ParameterDirection.Input, rCboInstitucion.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@cDepSuc", DbType.String, 50, ParameterDirection.Input, rTxtSucursal.Text);
            ProcBD.AgregarParametrosProcedimiento("@cDepNoCta", DbType.String, 30, ParameterDirection.Input, rTxtNumCuenta.Text);

            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);
           
            ProcBD.AgregarParametrosProcedimiento("@cDepFolio", DbType.String, 20, ParameterDirection.Input, rTxtFolio.Text);

            ProcBD.AgregarParametrosProcedimiento("@EdoEmision", DbType.String,50,ParameterDirection.Input, rTxtEstadoEmision.Text);

            /////////////////////////////////CHEQUE/////////////////////////////////////////////////////////////////

            if (rCboFormato.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cDepForCheqId", DbType.Int64, 0, ParameterDirection.Input, rCboFormato.SelectedValue);
            }


            ///FORMATO CHEQUE///
            if (rNoFormato.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@formImpCve", DbType.String, 10, ParameterDirection.Input, rNoFormato.SelectedValue);

            }


            ///LEYENDA CHEQUE///
            if (txtAreaLeyenda.InnerText != "")
            {
                
                ProcBD.AgregarParametrosProcedimiento("@cDepLeyCheq", DbType.String, 200, ParameterDirection.Input, txtAreaLeyenda.InnerText);
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);





            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {
                    llena_radgrid();
                    limpiar();
                    deshabilita();
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                    desabilitaBtn();
                    hdfBtnAccion.Value = "";
                    this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
                    //RadImageGallery1.ToolbarSettings.ShowFullScreenButton = false;

                    RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
                    RGridCuentasDeposito.AllowMultiRowSelection = true;

                }
                //else
                //{
                //    habilitaBtn();
                //}

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




            foreach (GridDataItem i in RGridCuentasDeposito.SelectedItems)
            {

                var dataItem = RGridCuentasDeposito.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string cDepCve = dataItem["ctaDepCve"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cDepCve", DbType.String, 10, ParameterDirection.Input, cDepCve);
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
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                    this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;

                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    //InicioPagina();
                }
                else
                {
                    
                    llena_radgrid();
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
                    //InicioPagina();
                }
                else
                {
                    llena_radgrid();

                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

      


    }


    public void llena_radgrid()
    {
        //GridView ds = new GridView();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref RGridCuentasDeposito, ds);

    }
    //private void ControlesAccion()
    //{
    //    RGridCuentasDeposito.MasterTableView.ClearSelectedItems();
    //    this.rCboInstitucion.ClearSelection();
    //    this.rCboMoneda.ClearSelection();
    //    this.rCboFormato.ClearSelection();
    //    this.rNoFormato.ClearSelection();

    //    rtxtClaveCuenta.CssClass = "cssTxtEnabled";
    //    rtTxtDescripcion.CssClass = "cssTxtEnabled";
    //    rTxtSucursal.CssClass = "cssTxtEnabled";
    //    rTxtFolio.CssClass = "cssTxtEnabled";
    //    rTxtNumCuenta.CssClass = "cssTxtEnabled";

    //    rCboInstitucion.BorderWidth = Unit.Pixel(1);
    //    rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;

    //    //rCboMoneda.BorderWidth = Unit.Pixel(1);
    //    rCboMoneda.BorderColor = System.Drawing.Color.Transparent;

    //    //rCboFormato.BorderWidth = Unit.Pixel(1);
    //    rCboFormato.BorderColor = System.Drawing.Color.Transparent;

    //    //rNoFormato.BorderWidth = Unit.Pixel(1);
    //    rNoFormato.BorderColor = System.Drawing.Color.Transparent;

    //    rTxtNumCuenta.Text = "";
    //    rtxtClaveCuenta.Text = "";
    //    rtTxtDescripcion.Text = "";
    //    txtAreaLeyenda.Disabled = true;/////////////////////////////////////////
    //    txtAreaLeyenda.Value = "";
    //    //rCboInstitucion.ClearSelection();
    //    //rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
    //    //rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
    //    //rNoFormato.BorderColor = System.Drawing.Color.Transparent;

    //    rTxtSucursal.Text = "";
    //    rTxtFolio.Text = "";
    //    //===> CONTROLES GENERAL

    //    //this.radUnidadMedida.Text = "";
    //    //this.radtxtDescripMed.Text = "";
    //    //this.radtxtFac.Text = "";
    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
    //    //cssEna();

    //    //===> CONTROLES POR ACCION

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rCboFormato.Enabled = true;
    //        this.rCboInstitucion.Enabled = true;
    //        this.rCboMoneda.Enabled = true;
    //        this.rNoFormato.Enabled = true;
    //        this.rtTxtDescripcion.Enabled = true;
    //        this.rtxtClaveCuenta.Enabled = true;
    //        this.rTxtNumCuenta.Enabled = true;
    //        this.rTxtSucursal.Enabled = true;
    //        this.rTxtFolio.Enabled = true;
    //        //RadImageGallery1.ToolbarSettings.ShowFullScreenButton = true;
    //        //this.radUnidadMedida.Enabled = true;
    //        //this.radtxtDescripMed.Enabled = true;
    //        //this.radtxtFac.Enabled = true;
    //        //cssEna();
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.RGridCuentasDeposito.AllowMultiRowSelection = false;
    //        this.rCboFormato.Enabled = false;
    //        this.rCboInstitucion.Enabled = false;
    //        this.rCboMoneda.Enabled = false;
    //        this.rNoFormato.Enabled = false;
    //        this.rtTxtDescripcion.Enabled = false;
    //        this.rtxtClaveCuenta.Enabled = false;
    //        this.rTxtNumCuenta.Enabled = false;
    //        this.rTxtSucursal.Enabled = false;
    //        this.rTxtFolio.Enabled = false;
    //        txtAreaLeyenda.Disabled = true;
    //        //RadImageGallery1.ToolbarSettings.ShowFullScreenButton = true;
    //        //this.radUnidadMedida.Enabled = false;
    //        //this.radtxtDescripMed.Enabled = false;
    //        //this.radtxtFac.Enabled = false;
    //        //cssEna();
    //    }

    //    //ELIMIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.RGridCuentasDeposito.AllowMultiRowSelection = true;
    //        this.rCboFormato.Enabled = false;
    //        this.rCboInstitucion.Enabled = false;
    //        this.rCboMoneda.Enabled = false;
    //        this.rNoFormato.Enabled = false;
    //        this.rtTxtDescripcion.Enabled = false;
    //        this.rtxtClaveCuenta.Enabled = false;
    //        this.rTxtNumCuenta.Enabled = false;
    //        this.rTxtSucursal.Enabled = false;
    //        this.rTxtFolio.Enabled = false;
    //        //RadImageGallery1.ToolbarSettings.ShowFullScreenButton = false;
    //        //this.radUnidadMedida.Enabled = false;
    //        //this.radtxtDescripMed.Enabled = false;
    //        //this.radtxtFac.Enabled = false;
    //        //cssEna();
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rCboFormato.Enabled = false;
    //        this.rCboInstitucion.Enabled = false;
    //        this.rCboMoneda.Enabled = false;
    //        this.rNoFormato.Enabled = false;
    //        this.rtTxtDescripcion.Enabled = false;
    //        this.rtxtClaveCuenta.Enabled = false;
    //        this.rTxtNumCuenta.Enabled = false;
    //        this.rTxtSucursal.Enabled = false;
    //        this.rTxtFolio.Enabled = false;
    //        //this.radUnidadMedida.Enabled = false;
    //        //this.radtxtDescripMed.Enabled = false;
    //        //this.radtxtFac.Enabled = false;


    //        //radUnidadMedida.CssClass = "cssTxtEnabled";
    //        //radtxtDescripMed.CssClass = "cssTxtEnabled";
    //        //radtxtFac.CssClass = "cssTxtEnabled";



    //    }

    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //   )
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



    public void BtnCss()
    {
        rtxtClaveCuenta.CssClass = "cssTxtEnabled";
        rtTxtDescripcion.CssClass = "cssTxtEnabled";
        rCboInstitucion.CssClass = "cssTxtEnabled";
        rTxtSucursal.CssClass = "cssTxtEnabled";
        rTxtFolio.CssClass = "cssTxtEnabled";
        //rCboMoneda.ClearSelection();
        //rCboFormato.ClearSelection();
        //rNoFormato.CssClass = "cssTxtEnabled";
        //txtAreaLeyenda.InnerText = "";
        rTxtNumCuenta.CssClass = "cssTxtEnabled";
        //RadImageGallery1.DataSource = GetDataTable();
        //RadImageGallery1.DataBind();
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent;

        rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;

        rNoFormato.BorderColor = System.Drawing.Color.Transparent;
    }

    
    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        PagLoc_ArtCve = Convert.ToString(Session["folio_Selection"]);
    }

    #endregion
    #endregion

    #region METODOS

    #endregion

    #region METODOS

    public void grid_txt()
    {

        try
        {
            var dataItem = RGridCuentasDeposito.SelectedItems[0] as GridDataItem;
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cDepCve", DbType.String, 10, ParameterDirection.Input, dataItem["ctaDepCve"].Text);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);





            rtxtClaveCuenta.Text = dataItem["ctaDepCve"].Text;

            rCboInstitucion.SelectedValue = ds.Tables[0].Rows[0]["insDepCve"].ToString();

            rTxtSucursal.Text = ds.Tables[0].Rows[0]["ctaDepSuc"].ToString();

            rtTxtDescripcion.Text = ds.Tables[0].Rows[0]["ctaDepDes"].ToString();

            rTxtNumCuenta.Text = ds.Tables[0].Rows[0]["ctaDepNoCta"].ToString();

            rCboMoneda.SelectedValue = ds.Tables[0].Rows[0]["monCve"].ToString();

            //////////////////////////////////////////////////////////////////////////////////////////
            // setFormatInGal(Convert.ToString(ds.Tables[0].Rows[0]["formImpCve"]));
            //////////////////////////////////////////////////////////////////////////////////////////

            rCboFormato.SelectedValue = ds.Tables[0].Rows[0]["ctaDepForCheqId"].ToString();

            rTxtFolio.Text = ds.Tables[0].Rows[0]["ctaDepFolio"].ToString();

            if (ds.Tables[0].Rows[0]["ctaDepLeyCheq"].ToString() == "&nbsp;")
            {
                txtAreaLeyenda.InnerText = "";
            }
            else
            {
                txtAreaLeyenda.InnerText = ds.Tables[0].Rows[0]["ctaDepLeyCheq"].ToString();
            }

            rNoFormato.SelectedValue = ds.Tables[0].Rows[0]["formImpCve"].ToString();
        }
        catch (Exception)
        {

        }

    }


    //private void setFormatInGal(string frmCve)
    //{

    //    int contador = 0;


    //    foreach (ImageGalleryItem item in RadImageGallery1.Items)
    //    {

    //        if (item.Title == frmCve)
    //        {
    //            RadImageGallery1.CurrentItemIndex = contador;
    //        }
    //        else
    //        {
    //            //RadImageGallery1.CurrentItemIndex = 0;
    //        }

    //        contador++;
    //    }

    //    //RadImageGallery1.CurrentItemIndex = 1;
    //}

    public DataView GetDataTable()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_FormatosImpresion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@formImpTip", DbType.Int64, 0, ParameterDirection.Input, 2);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        DataView dataView = new DataView(ds.Tables[0]);
        return dataView;

    }


    protected void RGridCuentasDeposito_SelectedIndexChanged(object sender, EventArgs e)
    {

        limpiar();
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{

        //}
        //else
        //{
            var dataItem = RGridCuentasDeposito.SelectedItems[0] as GridDataItem;
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cDepCve", DbType.String, 10, ParameterDirection.Input, dataItem["ctaDepCve"].Text);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);





            rtxtClaveCuenta.Text = dataItem["ctaDepCve"].Text;

            rCboInstitucion.SelectedValue = ds.Tables[0].Rows[0]["insDepCve"].ToString();

            rTxtSucursal.Text = ds.Tables[0].Rows[0]["ctaDepSuc"].ToString();

            rtTxtDescripcion.Text = ds.Tables[0].Rows[0]["ctaDepDes"].ToString();

            rTxtNumCuenta.Text = ds.Tables[0].Rows[0]["ctaDepNoCta"].ToString();

            rCboMoneda.SelectedValue = ds.Tables[0].Rows[0]["monCve"].ToString();

            rCboFormato.SelectedValue = ds.Tables[0].Rows[0]["ctaDepForCheqId"].ToString();

            rTxtFolio.Text = ds.Tables[0].Rows[0]["ctaDepFolio"].ToString();

        rTxtEstadoEmision.Text = ds.Tables[0].Rows[0]["EdoEmision"].ToString();

            if (ds.Tables[0].Rows[0]["ctaDepLeyCheq"].ToString() == "&nbsp;")
            {
                txtAreaLeyenda.InnerText = "";
            }
            else
            {
                txtAreaLeyenda.InnerText = ds.Tables[0].Rows[0]["ctaDepLeyCheq"].ToString();
            }

            rNoFormato.SelectedValue = ds.Tables[0].Rows[0]["formImpCve"].ToString();

            //setFormatInGal(Convert.ToString(ds.Tables[0].Rows[0]["formImpCve"]));

            //if (hdfBtnAccion.Value != "")
            //{
            //    //Grid_text();
            //    habilita();
            //    rtxtClaveCuendta.Enabled = false;
            //}





        //}


    }


    private DataSet llenadatalistVarRef()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 2);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }


    #endregion


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rtxtClaveCuenta
        this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtClaveCuenta.CssClass = "cssTxtEnabled";
        rtTxtDescripcion.CssClass = "cssTxtEnabled";
        rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
        rTxtSucursal.CssClass = "cssTxtEnabled";
        rTxtNumCuenta.CssClass = "cssTxtEnabled";
        rTxtFolio.CssClass = "cssTxtEnabled";
        rTxtEstadoEmision.CssClass = "cssTxtEnabled";
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
   

        this.rtxtClaveCuenta.Enabled = false;
        this.rtTxtDescripcion.Enabled = false;
        rTxtSucursal.Enabled = false;
        rTxtNumCuenta.Enabled = false;
        rTxtFolio.Enabled = false;
        txtAreaLeyenda.Disabled = false; 
        rCboInstitucion.Enabled = false;
        rCboMoneda.Enabled = false;
        rCboFormato.Enabled = false;
        rNoFormato.Enabled = false;



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
            this.rtxtClaveCuenta.Enabled = false;
            this.rtTxtDescripcion.Enabled = false;
            rTxtSucursal.Enabled = false;
            rTxtNumCuenta.Enabled = false;
            rTxtFolio.Enabled = false;
            txtAreaLeyenda.Disabled = true;
            rCboInstitucion.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboFormato.Enabled = false;
            rNoFormato.Enabled = false;


            this.rtxtClaveCuenta.Text = "";
            this.rtTxtDescripcion.Text = "";
            rTxtSucursal.Text = "";
            rTxtNumCuenta.Text = "";
            rTxtFolio.Text = "";
            txtAreaLeyenda.InnerText = "";
            rCboInstitucion.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboFormato.ClearSelection();
            rNoFormato.ClearSelection();

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
                this.RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = false;
                RGridCuentasDeposito.MasterTableView.ClearSelectedItems();

                this.rtxtClaveCuenta.Enabled = true;
                this.rtTxtDescripcion.Enabled = true;
                rTxtSucursal.Enabled = true;
                rTxtNumCuenta.Enabled = true;
                rTxtFolio.Enabled = true;
                txtAreaLeyenda.Disabled = false;
                rCboInstitucion.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboFormato.Enabled = true;



                this.rtxtClaveCuenta.Text = "";
                this.rtTxtDescripcion.Text = "";
                rTxtSucursal.Text = "";
                rTxtNumCuenta.Text = "";
                rTxtFolio.Text = "";
                txtAreaLeyenda.InnerText = "";
                rCboInstitucion.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboFormato.ClearSelection();
                rNoFormato.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                RGridCuentasDeposito.AllowMultiRowSelection = false;

                this.rtxtClaveCuenta.Enabled = false;
                this.rtTxtDescripcion.Enabled = true;
                rTxtSucursal.Enabled = true;
                rTxtNumCuenta.Enabled = true;
                rTxtFolio.Enabled = true;
                txtAreaLeyenda.Disabled = false;
                rTxtEstadoEmision.Enabled = true;
                rCboInstitucion.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboFormato.Enabled = true;
                rNoFormato.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
                llena_radgrid();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
                RGridCuentasDeposito.AllowMultiRowSelection = true;
                RGridCuentasDeposito.MasterTableView.ClearSelectedItems();

                this.rtxtClaveCuenta.Enabled = false;
                this.rtTxtDescripcion.Enabled = false;
                rTxtSucursal.Enabled = false;
                rTxtNumCuenta.Enabled = false;
                rTxtFolio.Enabled = false;
                txtAreaLeyenda.Disabled = true;
                rCboInstitucion.Enabled = false;
                rCboMoneda.Enabled = false;
                rCboFormato.Enabled = false;
                rNoFormato.Enabled = false;



                this.rtxtClaveCuenta.Text = "";
                this.rtTxtDescripcion.Text = "";
                rTxtSucursal.Text = "";
                rTxtNumCuenta.Text = "";
                rTxtFolio.Text = "";
                txtAreaLeyenda.InnerText = "";
                rCboInstitucion.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboFormato.ClearSelection();
                rNoFormato.ClearSelection();


            }
        }


        if (Result == false)
        {
            this.rtxtClaveCuenta.Enabled = false;
            this.rtTxtDescripcion.Enabled = false;
            rTxtSucursal.Enabled = false;
            rTxtNumCuenta.Enabled = false;
            rTxtFolio.Enabled = false;
            txtAreaLeyenda.Disabled = true;
            rCboInstitucion.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboFormato.Enabled = false;
            rNoFormato.Enabled = false;


            this.rtxtClaveCuenta.Text = "";
            this.rtTxtDescripcion.Text = "";
            rTxtSucursal.Text = "";
            rTxtNumCuenta.Text = "";
            rTxtFolio.Text = "";
            txtAreaLeyenda.InnerText = "";
            rCboInstitucion.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboFormato.ClearSelection();
            rNoFormato.ClearSelection();

            hdfBtnAccion.Value = "";
            RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
            RGridCuentasDeposito.AllowMultiRowSelection = true;


        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = RGridCuentasDeposito.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, RGridCuentasDeposito, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, RGridCuentasDeposito, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rtxtClaveCuenta.Text = "";
            this.rtTxtDescripcion.Text = "";
            rTxtSucursal.Text = "";
            rTxtNumCuenta.Text = "";
            rTxtFolio.Text = "";
            txtAreaLeyenda.InnerText = "";
            rCboInstitucion.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboFormato.ClearSelection();
            rNoFormato.ClearSelection();


        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;

            RGridCuentasDeposito.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rtxtClaveCuenta.CssClass = "cssTxtEnabled";
            rtTxtDescripcion.CssClass = "cssTxtEnabled";
            rCboInstitucion.BorderColor = System.Drawing.Color.Transparent;
            rTxtSucursal.CssClass = "cssTxtEnabled";
            rTxtNumCuenta.CssClass = "cssTxtEnabled";
            rTxtFolio.CssClass = "cssTxtEnabled";
            rCboMoneda.BorderColor = System.Drawing.Color.Transparent;



            rtxtClaveCuenta.Enabled = false;
            rtTxtDescripcion.Enabled = false;
            rTxtSucursal.Enabled = false;
            rTxtNumCuenta.Enabled = false;
            rTxtFolio.Enabled = false;
            txtAreaLeyenda.Disabled = true;
            rCboInstitucion.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboFormato.Enabled = false;
            rNoFormato.Enabled = false;



            rtxtClaveCuenta.Text = "";
            rtTxtDescripcion.Text = "";
            rTxtSucursal.Text = "";
            rTxtNumCuenta.Text = "";
            rTxtFolio.Text = "";
            txtAreaLeyenda.InnerText = "";
            rCboInstitucion.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboFormato.ClearSelection();
            rNoFormato.ClearSelection();

            hdfBtnAccion.Value = "";
            RGridCuentasDeposito.ClientSettings.Selecting.AllowRowSelect = true;
            RGridCuentasDeposito.AllowMultiRowSelection = true;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }


}