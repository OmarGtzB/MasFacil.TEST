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

public partial class DC_MttoArticuloRefVar : System.Web.UI.Page
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
    
    protected void rCboTipoDato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGrid(1);
        LlenaGrid(2);
    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        if (rCboTipoDato.SelectedValue != "")
        {
            ControlesAccion();
        }
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        if (rCboTipoDato.SelectedValue != "")
        {
            ControlesAccion();
        }
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        if (rCboTipoDato.SelectedValue != "")
        {
            ControlesAccion();
        }
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    protected void rGdvReferencias_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvReferencias.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            txtRefSecuencia.Text = dataItem["revasec"].Text;
            txtRefDescripcion.Text = dataItem["revaDes"].Text;
        }
    }

    protected void rGdvVariables_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvVariables.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            txtVarSecuencia.Text = dataItem["revasec"].Text;
            txtVarDescripcion.Text = dataItem["revaDes"].Text;
        }
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "";
        ControlesAccion();
        InicioPagina();

        rGdvReferencias.DataSource = null;
        rGdvReferencias.DataBind();

        rGdvVariables.DataSource = null;
        rGdvVariables.DataBind();
    }
    #endregion
    
    #region FUNCIONES
    private void InicioPagina()
    {
        FnCtlsFillIn.RadComboBox_ReferenciasVariablesTipo(Pag_sConexionLog, ref rCboTipoDato, true, false);
        ControlesAccion();

        rGdvReferencias.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvReferencias.AllowMultiRowSelection = true;

        rGdvVariables.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvVariables.AllowMultiRowSelection = true;

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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            txtRefSecuencia.CssClass = "cssTxtEnabled";
            txtRefDescripcion.CssClass = "cssTxtEnabled";
            txtVarSecuencia.CssClass = "cssTxtEnabled";
            txtVarDescripcion.CssClass = "cssTxtEnabled";

            if (txtRefSecuencia.Text == "" && txtRefDescripcion.Text == "" && txtVarSecuencia.Text == "" && txtVarDescripcion.Text == "")
            {
                txtRefSecuencia.CssClass = "cssTxtInvalid";
                txtRefDescripcion.CssClass = "cssTxtInvalid";
                txtVarSecuencia.CssClass = "cssTxtInvalid";
                txtVarDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (txtRefSecuencia.Text != "" || txtRefDescripcion.Text != "")
            {
                if (txtRefSecuencia.Text.Trim() == "")
                {
                    txtRefSecuencia.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtRefSecuencia.CssClass = "cssTxtEnabled"; }

                if (txtRefDescripcion.Text.Trim() == "")
                {
                    txtRefDescripcion.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtRefDescripcion.CssClass = "cssTxtEnabled"; }
            }

            if (txtVarSecuencia.Text != "" || txtVarDescripcion.Text != "")
            {
                if (txtVarSecuencia.Text.Trim() == "")
                {
                    txtVarSecuencia.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtVarSecuencia.CssClass = "cssTxtEnabled"; }

                if (txtVarDescripcion.Text.Trim() == "")
                {
                    txtVarDescripcion.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtVarDescripcion.CssClass = "cssTxtEnabled"; }
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
            if (rGdvReferencias.SelectedItems.Count == 0 && rGdvVariables.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (rGdvReferencias.SelectedItems.Count != 0)
            {
                if (txtRefSecuencia.Text.Trim() == "")
                {
                    txtRefSecuencia.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtRefSecuencia.CssClass = "cssTxtEnabled"; }

                if (txtRefDescripcion.Text.Trim() == "")
                {
                    txtRefDescripcion.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtRefDescripcion.CssClass = "cssTxtEnabled"; }
            }

            //if (rGdvVariables.SelectedItems.Count == 0)
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            //    return sResult;
            //}
            if (rGdvVariables.SelectedItems.Count != 0)
            {
                if (txtVarSecuencia.Text.Trim() == "")
                {
                    txtVarSecuencia.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtVarSecuencia.CssClass = "cssTxtEnabled"; }

                if (txtVarDescripcion.Text.Trim() == "")
                {
                    txtVarDescripcion.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { txtVarDescripcion.CssClass = "cssTxtEnabled"; }
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

            if (rGdvReferencias.SelectedItems.Count == 0 && rGdvVariables.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            //if (rGdvVariables.SelectedItems.Count == 0)
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            //    return sResult;
            //}

            return sResult;
        }

        return sResult;
    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvReferencias.SelectedItems.Count;
        int GvSelectItemVar = rGdvVariables.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvReferencias.SelectedItems.Count == 0 && rGdvVariables.SelectedItems.Count == 0)
            {
                if (rGdvReferencias.SelectedItems.Count >= 0)
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvReferencias, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }
                if (rGdvVariables.SelectedItems.Count >= 0)
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvVariables, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }
            }




        }




        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvReferencias.SelectedItems.Count == 0 && rGdvVariables.SelectedItems.Count == 0)
            {
                GvVAS = new string[] { "VAL0003" };
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvReferencias, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }

                GvVAS = new string[] { "VAL0003" };
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvVariables, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }

        }
        return sResult;
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void LlenaGrid(int revatip)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ReferenciasVariables";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.Trim().ToString());
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int64, 0, ParameterDirection.Input, revatip);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            //1 referencias, 2 variables
            if (revatip == 1)
            {
                FnCtlsFillIn.RadGrid(ref rGdvReferencias, ds);
            }
            else if (revatip == 2)
            {
                FnCtlsFillIn.RadGrid(ref rGdvVariables, ds);
            }
        }
    }
    
    #endregion

    #region METODOS
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvReferencias.ClientSettings.Selecting.AllowRowSelect = true;
        this.rGdvVariables.ClientSettings.Selecting.AllowRowSelect = true;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        txtRefSecuencia.CssClass = "cssTxtEnabled";
        txtRefDescripcion.CssClass = "cssTxtEnabled";
        txtVarSecuencia.CssClass = "cssTxtEnabled";
        txtVarDescripcion.CssClass = "cssTxtEnabled";

        this.txtRefSecuencia.Enabled = false;
        this.txtRefDescripcion.Enabled = false;
        this.txtVarSecuencia.Enabled = false;
        this.txtVarDescripcion.Enabled = false;

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
            this.txtRefSecuencia.Enabled = false;
            this.txtRefDescripcion.Enabled = false;
            this.txtVarSecuencia.Enabled = false;
            this.txtVarDescripcion.Enabled = false;

            this.txtRefSecuencia.Text = "";
            this.txtRefDescripcion.Text = "";
            this.txtVarSecuencia.Text = "";
            this.txtVarDescripcion.Text = "";
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
                this.rGdvReferencias.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvReferencias.MasterTableView.ClearSelectedItems();

                this.rGdvVariables.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvVariables.MasterTableView.ClearSelectedItems();

                this.txtRefSecuencia.Enabled = true;
                this.txtRefDescripcion.Enabled = true;
                this.txtVarSecuencia.Enabled = true;
                this.txtVarDescripcion.Enabled = true;

                this.txtRefSecuencia.Text = "";
                this.txtRefDescripcion.Text = "";
                this.txtVarSecuencia.Text = "";
                this.txtVarDescripcion.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvReferencias.AllowMultiRowSelection = false;
                rGdvVariables.AllowMultiRowSelection = false;

                if (rGdvReferencias.SelectedItems.Count != 0)
                {
                    this.txtRefSecuencia.Enabled = false;
                    this.txtRefDescripcion.Enabled = true;
                }

                if (rGdvVariables.SelectedItems.Count != 0)
                {
                    this.txtVarSecuencia.Enabled = false;
                    this.txtVarDescripcion.Enabled = true;
                }


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
                rGdvReferencias.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvReferencias.AllowMultiRowSelection = true;
                rGdvReferencias.MasterTableView.ClearSelectedItems();

                rGdvVariables.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvVariables.AllowMultiRowSelection = true;
                rGdvVariables.MasterTableView.ClearSelectedItems();

                this.txtRefSecuencia.Enabled = false;
                this.txtRefDescripcion.Enabled = false;
                this.txtVarSecuencia.Enabled = false;
                this.txtVarDescripcion.Enabled = false;

                this.txtRefSecuencia.Text = "";
                this.txtRefDescripcion.Text = "";
                this.txtVarSecuencia.Text = "";
                this.txtVarDescripcion.Text = "";
            }
        }


        if (Result == false)
        {
            this.txtRefSecuencia.Enabled = false;
            this.txtRefDescripcion.Enabled = false;
            this.txtVarSecuencia.Enabled = false;
            this.txtVarDescripcion.Enabled = false;

            this.txtRefSecuencia.Text = "";
            this.txtRefDescripcion.Text = "";
            this.txtVarSecuencia.Text = "";
            this.txtVarDescripcion.Text = "";
        }


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
                if (txtRefSecuencia.Text != "" && txtRefDescripcion.Text != "")
                {
                    EjecutaSpAccionesReferencias();
                }
                if (txtVarSecuencia.Text != "" && txtVarDescripcion.Text != "")
                {
                    EjecutaSpAccionesVariables();
                }
                hdfBtnAccion.Value = "";
                ControlesAccion();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                if (rGdvReferencias.SelectedItems.Count >= 1)
                {
                    EjecutaSpAccionEliminarReferencias();
                    LlenaGrid(1);
                    txtRefSecuencia.Text = "";
                    txtRefDescripcion.Text = "";
                }
                if (rGdvVariables.SelectedItems.Count >= 1)
                {
                    EjecutaSpAccionEliminarVariables();
                    LlenaGrid(2);
                    txtVarSecuencia.Text = "";
                    txtVarDescripcion.Text = "";
                }

            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private void EjecutaSpAccionesReferencias()
    {
        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ReferenciasVariables";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(txtRefSecuencia.Text));
            ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@revaDes", DbType.String, 50, ParameterDirection.Input, txtRefDescripcion.Text);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();


                ShowAlert(sEjecEstatus, sEjecMSG);


                if (sEjecEstatus == "1")
                {
                    LlenaGrid(1);
                }
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionesVariables()
    {
        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ReferenciasVariables";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(txtVarSecuencia.Text));
            ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@revaDes", DbType.String, 50, ParameterDirection.Input, txtVarDescripcion.Text);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();


                ShowAlert(sEjecEstatus, sEjecMSG);



                if (sEjecEstatus == "1")
                {
                    LlenaGrid(2);
                }
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    
    private void EjecutaSpAccionEliminarReferencias()
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

            foreach (GridDataItem i in rGdvReferencias.SelectedItems)
            {

                var dataItem = rGdvReferencias.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string srevasec = dataItem.GetDataKeyValue("revasec").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ReferenciasVariables";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, srevasec);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.ToString());
                        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 1);
                        //ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, sAlmCve);

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
                    // InicioPagina();
                }
                else
                {
                    //InicioPagina();
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
                    //InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionEliminarVariables()
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

            foreach (GridDataItem i in rGdvVariables.SelectedItems)
            {

                var dataItem = rGdvVariables.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string srevasec = dataItem.GetDataKeyValue("revasec").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ReferenciasVariables";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, srevasec);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.ToString());
                        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 2);

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
                    //InicioPagina();
                }
                else
                {
                    //InicioPagina();
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
                    //InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    
    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.txtRefSecuencia.Text = "";
            this.txtRefDescripcion.Text = "";
            this.txtVarSecuencia.Text = "";
            this.txtVarDescripcion.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvReferencias.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvReferencias.MasterTableView.ClearSelectedItems();

            rGdvVariables.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvVariables.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            txtRefSecuencia.CssClass = "cssTxtEnabled";
            txtRefDescripcion.CssClass = "cssTxtEnabled";
            txtVarSecuencia.CssClass = "cssTxtEnabled";
            txtVarDescripcion.CssClass = "cssTxtEnabled";

            this.txtRefSecuencia.Enabled = false;
            this.txtRefDescripcion.Enabled = false;
            this.txtVarSecuencia.Enabled = false;
            this.txtVarDescripcion.Enabled = false;

            this.txtRefSecuencia.Text = "";
            this.txtRefDescripcion.Text = "";
            this.txtVarSecuencia.Text = "";
            this.txtVarDescripcion.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    #endregion










}