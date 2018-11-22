using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;

public partial class DC_MttoParametros : System.Web.UI.Page
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
        hdfBtnAccion.Value = "";
        ControlesAccion();
        ControlValor();
    }

    protected void rGdv_Parametros_SelectedIndexChanged(object sender, EventArgs e)
    {
    
        var dataItem = rGdv_Parametros.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            if (dataItem["parmCve"].Text != "&nbsp;")
            {
                rTxtParametro.Text = dataItem["parmCve"].Text;
            }
            else
            {
                rTxtParametro.Text = "";
            }

            if (dataItem["parmSec"].Text != "&nbsp;")
            {
                rTxtNumSecuencia.Text = dataItem["parmSec"].Text;
            }
            else
            {
                rTxtNumSecuencia.Text = "";
            }

            if (dataItem["parmDes"].Text != "&nbsp;")
            {
                rTxtDescripcion.Text = dataItem["parmDes"].Text;
            }
            else
            {
                rTxtDescripcion.Text = "";
            }


            if (dataItem["parmValTip"].Text.Trim() == "Int" || dataItem["parmValTip"].Text.Trim() == "int")
            {
                rCmboTipo.SelectedIndex = 0;
                rdNumerico_Valor.Text = dataItem["PerVal"].Text;
                rdNumerico_Valor.NumberFormat.DecimalDigits = 0;
                rdNumerico_Valor.Visible = true;
                rTxt_Valor.Visible = false;
                RdDateFecha_Valor.Visible = false;
            }
            if (dataItem["parmValTip"].Text.Trim() == "str" || dataItem["parmValTip"].Text.Trim() == "Str")
            {
                rCmboTipo.SelectedIndex = 1;
                rTxt_Valor.Text = sValorString( dataItem["PerVal"].Text);
                rTxt_Valor.Visible = true;
                rdNumerico_Valor.Visible = false;
                RdDateFecha_Valor.Visible = false;
                rdNumerico_Valor.NumberFormat.DecimalDigits = 0;
            }
            if (dataItem["parmValTip"].Text.Trim() == "dec" || dataItem["parmValTip"].Text.Trim() == "Dec")
            {
                rCmboTipo.SelectedIndex = 2;
                rdNumerico_Valor.Visible = true;
                rdNumerico_Valor.NumberFormat.DecimalDigits = 2;
                rdNumerico_Valor.Text = Convert.ToDecimal(dataItem["PerVal"].Text).ToString();
                rTxt_Valor.Visible = false;
                RdDateFecha_Valor.Visible = false;


            }
            if (dataItem["parmValTip"].Text.Trim() == "Fec")
            {
                RdDateFecha_Valor.SelectedDate = Convert.ToDateTime(dataItem["PerVal"].Text);
                rCmboTipo.SelectedIndex = 3;
                RdDateFecha_Valor.Visible = true;
                rTxt_Valor.Visible = false;
                rdNumerico_Valor.Visible = false;
                rdNumerico_Valor.NumberFormat.DecimalDigits = 0;

            }
        }
        rTxtParametro.CssClass = "cssTxtEnabled";
        rTxtNumSecuencia.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxt_Valor.CssClass = "cssTxtEnabled";
        rdNumerico_Valor.CssClass = "cssTxtEnabled";
        RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);

    }

    protected void rCmboTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ControlValor();
    }

    #endregion

    #region METODOS

    private void InicioPagina()
    {
        if (rCmboTipo.SelectedValue == "")
        {
            RadComboBoxItem item = new RadComboBoxItem();
            RadComboBoxItem item0 = new RadComboBoxItem();
            RadComboBoxItem item1 = new RadComboBoxItem();
            RadComboBoxItem item2 = new RadComboBoxItem();

            item.Text = "Numérico";
            item.Value = "1";

            item0.Text = "Carácter";
            item0.Value = "2";

            item1.Text = "Decimal";
            item1.Value = "3";

            item2.Text = "Fecha";
            item2.Value = "4";

            rCmboTipo.Items.Add(item);
            rCmboTipo.Items.Add(item0);
            rCmboTipo.Items.Add(item1);
            rCmboTipo.Items.Add(item2);

            rCmboTipo.ClearSelection();
        }

        ((Literal)rCmboTipo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCmboTipo.Items.Count);
            rCmboTipo.SelectedIndex = -1;
            LLenaGrid();
            hdfBtnAccion.Value = "";
            ControlesAccion();
            ControlValor();
        rGdv_Parametros.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Parametros.AllowMultiRowSelection = true;

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
                // EjecutaSpAccionEliminar();
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
            if (rTxtParametro.Text.Trim() == "")
            {
                rTxtParametro.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtParametro.CssClass = "cssTxtEnabled";
            }


            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtDescripcion.CssClass = "cssTxtEnabled";
            }

            if (rCmboTipo.SelectedValue == "1")
            {
                if (rdNumerico_Valor.Text.Trim() == "")
                {
                    rdNumerico_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rdNumerico_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                if (rTxt_Valor.Text.Trim() == "")
                {
                    rTxt_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rTxt_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "3")
            {
                if (rdNumerico_Valor.Text.Trim() == "")
                {
                    rdNumerico_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rdNumerico_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "4")
            {
                if (ObtieneFecha() == "")
                {
                    RdDateFecha_Valor.BorderWidth = Unit.Pixel(1);
                    RdDateFecha_Valor.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
                    RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);
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
            //Modificar

            if (rGdv_Parametros.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                return sResult;
            }


            if (rTxtParametro.Text.Trim() == "")
            {
                rTxtParametro.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtParametro.CssClass = "cssTxtEnabled";
            }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtDescripcion.CssClass = "cssTxtEnabled";
            }

            if (rCmboTipo.SelectedValue == "1")
            {
                if (rdNumerico_Valor.Text.Trim() == "")
                {
                    rdNumerico_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rdNumerico_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                if (rTxt_Valor.Text.Trim() == "")
                {
                    rTxt_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rTxt_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "3")
            {
                if (rdNumerico_Valor.Text.Trim() == "")
                {
                    rdNumerico_Valor.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    rdNumerico_Valor.CssClass = "cssTxtEnabled";
                }
            }
            if (rCmboTipo.SelectedValue == "4")
            {
                if (ObtieneFecha() == "")
                {
                    RdDateFecha_Valor.BorderWidth = Unit.Pixel(1);
                    RdDateFecha_Valor.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
                    RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);
                }
            }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Parametros.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }

    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametros";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            if (rTxtParametro.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, rTxtParametro.Text);
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int32, 0, ParameterDirection.Input, rTxtNumSecuencia.Text);
            }
            if (rTxtDescripcion.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmDes", DbType.String, 100, ParameterDirection.Input, rTxtDescripcion.Text);
            }
            if (rCmboTipo.SelectedValue == "1")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Int");
                ProcBD.AgregarParametrosProcedimiento("@parmValInt", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rdNumerico_Valor.Text));
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Str");
                ProcBD.AgregarParametrosProcedimiento("@parmValStr", DbType.String, 50, ParameterDirection.Input, rTxt_Valor.Text.ToString());
            }
            if (rCmboTipo.SelectedValue == "3")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Dec");
                ProcBD.AgregarParametrosProcedimiento("@parmValDec", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rdNumerico_Valor.Text));

            }
            if (rCmboTipo.SelectedValue == "4")
            {
                ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Fec");
                ProcBD.AgregarParametrosProcedimiento("@parmValDate", DbType.String, 100, ParameterDirection.Input, ObtieneFecha());
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
                    hdfBtnAccion.Value = "";
                    Limpiartxt();
                    InicioPagina();

                }
            }
        }
        catch (Exception ex)
        {

        }


    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }


    private void ControlesAccion()
    {
        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdv_Parametros.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtParametro.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rdNumerico_Valor.CssClass = "cssTxtEnabled";
        rTxtNumSecuencia.CssClass = "cssTxtEnabled";
        RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);

        rTxtParametro.Enabled = false;
        rTxtNumSecuencia.Enabled = false;
        rTxtDescripcion.Enabled = false;
        rCmboTipo.Enabled = false;
        rTxt_Valor.Enabled = false;
        rdNumerico_Valor.Enabled = false;
        RdDateFecha_Valor.Enabled = false;

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

    }

    #endregion

    #region FUNCIONES
    private void ControlValor()
    {
        if (rCmboTipo.SelectedValue == "1")
        {
            rdNumerico_Valor.Visible = true;
            rTxt_Valor.Visible = false;
            RdDateFecha_Valor.Visible = false;
            rdNumerico_Valor.NumberFormat.DecimalDigits = 0;
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();
        }
        if (rCmboTipo.SelectedValue == "2")
        {
            rTxt_Valor.Visible = true;
            rdNumerico_Valor.Visible = false;
            RdDateFecha_Valor.Visible = false;
            rdNumerico_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();
        }
        if (rCmboTipo.SelectedValue == "3")
        {
            rdNumerico_Valor.Visible = true;
            rdNumerico_Valor.NumberFormat.DecimalDigits = 2;
            rTxt_Valor.Visible = false;
            RdDateFecha_Valor.Visible = false;
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();
        }
        if (rCmboTipo.SelectedValue == "4")
        {
            RdDateFecha_Valor.Visible = true;
            rdNumerico_Valor.Visible = false;
            rdNumerico_Valor.NumberFormat.DecimalDigits = 0;
            rTxt_Valor.Visible = false;
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();
        }


        rTxtParametro.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rdNumerico_Valor.CssClass = "cssTxtEnabled";
        rTxtNumSecuencia.CssClass = "cssTxtEnabled";
        RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);
    }
    private void LLenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Parametros, ds);

    }

    public string ObtieneFecha()
    {
        string Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Valor.SelectedDate);

        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        if (Val_Fec_Inicio == "1/01/01")
        {
            return Val_Fec_Inicio = "";
        }

        return Val_Fec_Inicio;
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_Parametros.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Parametros.MasterTableView.ClearSelectedItems();

                rTxtParametro.Enabled = true;
               // rTxtNumSecuencia.Enabled = true;
                rTxtDescripcion.Enabled = true;
                rCmboTipo.Enabled = true;
                rTxt_Valor.Enabled = true;
                rdNumerico_Valor.Enabled = true;
                RdDateFecha_Valor.Enabled = true;
                
                rTxtParametro.Text = "";
                rTxtNumSecuencia.Text = "";
                rTxtDescripcion.Text = "";
                rCmboTipo.ClearSelection();
                rTxt_Valor.Text = "";
                rdNumerico_Valor.Text = "";
                RdDateFecha_Valor.Clear();
                
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Parametros.AllowMultiRowSelection = false;

                rTxtParametro.Enabled = false;
                rTxtNumSecuencia.Enabled = false;
                rTxtDescripcion.Enabled = true;
                rCmboTipo.Enabled = true;
                rTxt_Valor.Enabled = true;
                rdNumerico_Valor.Enabled = true;
                RdDateFecha_Valor.Enabled = true;
                
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_Parametros.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Parametros.AllowMultiRowSelection = true;
                rGdv_Parametros.MasterTableView.ClearSelectedItems();

                rTxtParametro.Enabled = false;
                rTxtNumSecuencia.Enabled = false;
                rTxtDescripcion.Enabled = false;
                rCmboTipo.Enabled = false;
                rTxt_Valor.Enabled = false;
                rdNumerico_Valor.Enabled = false;
                RdDateFecha_Valor.Enabled = false;
                
                rTxtParametro.Text = "";
                rTxtNumSecuencia.Text = "";
                rTxtDescripcion.Text = "";
                rCmboTipo.ClearSelection();
                rTxt_Valor.Text = "";
                rdNumerico_Valor.Text = "";
                RdDateFecha_Valor.Clear();

            }
        }


        if (Result == false)
        {
            rTxtParametro.Enabled = false;
            rTxtNumSecuencia.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rCmboTipo.Enabled = false;
            rTxt_Valor.Enabled = false;
            rdNumerico_Valor.Enabled = false;
            RdDateFecha_Valor.Enabled = false;

            rTxtParametro.Text = "";
            rTxtNumSecuencia.Text = "";
            rTxtDescripcion.Text = "";
            rCmboTipo.ClearSelection();
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();

        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Parametros.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Parametros, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Parametros, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rTxtParametro.Text = "";
            rTxtNumSecuencia.Text = "";
            rTxtDescripcion.Text = "";
            rCmboTipo.ClearSelection();
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_Parametros.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_Parametros.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtParametro.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rdNumerico_Valor.CssClass = "cssTxtEnabled";
            rTxtNumSecuencia.CssClass = "cssTxtEnabled";
            RdDateFecha_Valor.BorderColor = System.Drawing.Color.Transparent;
            RdDateFecha_Valor.BorderWidth = Unit.Pixel(0);
        

            rTxtParametro.Enabled = false;
            rTxtNumSecuencia.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rCmboTipo.Enabled = false;
            rTxt_Valor.Enabled = false;
            rdNumerico_Valor.Enabled = false;
            RdDateFecha_Valor.Enabled = false;


            rTxtParametro.Text = "";
            rTxtNumSecuencia.Text = "";
            rTxtDescripcion.Text = "";
            rCmboTipo.ClearSelection();
            rTxt_Valor.Text = "";
            rdNumerico_Valor.Text = "";
            RdDateFecha_Valor.Clear();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

        ControlValor();
    }

    public void Limpiartxt()
    {
        rTxtParametro.Text = "";
        rTxtNumSecuencia.Text = "";
        rTxtDescripcion.Text = "";
        rCmboTipo.ClearSelection();
        rTxt_Valor.Text = "";
        rdNumerico_Valor.Text = "";
        RdDateFecha_Valor.Clear();
    }


    private string sValorString( string sValor) {
        string sCadena = sValor;

        if (sCadena == "&nbsp;") {
            sCadena = "";
        }
        return sCadena;
    }
    #endregion





















}