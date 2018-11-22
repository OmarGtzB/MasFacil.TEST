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


public partial class DC_MttoCptoPrecios : System.Web.UI.Page
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
    private string folio_Selection;
    private string listTipDatoCptoCve;

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
        InicioPagina();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        folio_Selection = Convert.ToString(Session["folio_Selection"]);
        listTipDatoCptoCve = Convert.ToString(Session["listTipDatoCptoCve"]);
    }

    private void InicioPagina()
    {
        InformacionConcepto();
        LlenaComboSecuenc();
        
        hdfBtnAccion.Value = "";
        ControlesAccion();
        if (folio_Selection.ToString() !="")
        {
            InformacionCosto();
        }
        

        if (rBtnNuevo.Enabled == true)
        {
            rBtnModificar.Enabled = false;
            rBtnEliminar.Enabled = false;
        }
        else
        {
            rBtnModificar.Enabled = true;
            rBtnEliminar.Enabled = true;
        }
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

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboFactores.BorderColor = System.Drawing.Color.Transparent;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        ControlesAccionEjecucion(true);

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            this.rCboFactores.Enabled = false;
            this.rbvalExisApli.Enabled = false;
            this.rbvalExisCapt.Enabled = false;
            this.rbOtroPrecSi.Enabled = false;
            this.rbOtroPrecNo.Enabled = false;

             
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

                this.rCboFactores.Enabled = true;
                this.rCboFactores.ClearSelection();
                 
                this.rbvalExisApli.Enabled = true;
                this.rbvalExisCapt.Enabled = true;
                this.rbOtroPrecSi.Enabled = true;
                this.rbOtroPrecNo.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

                this.rCboFactores.Enabled = true;

                this.rbvalExisApli.Enabled = true;
                this.rbvalExisCapt.Enabled = true;
                this.rbOtroPrecSi.Enabled = true;
                this.rbOtroPrecNo.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

        }
    }

    private void InformacionConcepto()
    {
        if (folio_Selection != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                rLblCptoId.Text = folio_Selection;
                rLblcptoDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoDes"]);
            }
        }
    }
    private void LlenaComboSecuenc()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 62);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, "Fact");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboFactores, ds, "cptoConfSec", "cptoConfDes", true, false);
        ((Literal)rCboFactores.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboFactores.Items.Count);


    }
    private void InformacionCosto() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoCostoConfig";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 20, ParameterDirection.Input, folio_Selection);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {

            if (Convert.ToInt64(ds.Tables[0].Rows[0]["cptoCostValExis"]) == 1)
            {
                rbvalExisApli.Checked = true;
                rbvalExisCapt.Checked = false;
            }
            else {
                rbvalExisApli.Checked = false;
                rbvalExisCapt.Checked = true;
            }

            if (Convert.ToInt64(ds.Tables[0].Rows[0]["cptoCostOtroPrec"]) == 1)
            {
                rbOtroPrecSi.Checked = true;
                rbOtroPrecNo.Checked = false;
            }
            else
            {
                rbOtroPrecSi.Checked = false;
                rbOtroPrecNo.Checked = true;
            }

            rCboFactores.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoCostSecFact"]);
 
            this.rBtnNuevo.Enabled = false;
        }
        else {
            
            rbvalExisApli.Checked = true;
            rbvalExisCapt.Checked = false;

            rbOtroPrecSi.Checked = true;
            rbOtroPrecNo.Checked = false;

            rCboFactores.ClearSelection();
            rCboFactores.SelectedIndex = -1;

            this.rBtnNuevo.Enabled = true;
            this.rBtnNuevo.Enabled = true;
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
                EjecutaSpAcciones();
                InicioPagina();
                InformacionCosto();
            }


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAcciones();
                InicioPagina();
                InformacionCosto();
            }

        }
        else {
            RadWindowManagerPage.RadAlert(msgValidacion, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert2.png");
        }

    }

    private void EjecutaSpAcciones()
    {
        try
        {


            Int64 valExisApli = 1;
            Int64 OtroPrec = 1;

            if (rbvalExisApli.Checked == false)
            {
                valExisApli = 2;
            }
            if (rbOtroPrecSi.Checked == false )
            {
                OtroPrec = 2;
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoCostoConfig";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
            ProcBD.AgregarParametrosProcedimiento("@cptoCostValExis", DbType.Int64, 0, ParameterDirection.Input, valExisApli);
            ProcBD.AgregarParametrosProcedimiento("@cptoCostOtroPrec", DbType.Int64, 0, ParameterDirection.Input, OtroPrec);
            ProcBD.AgregarParametrosProcedimiento("@cptoCostSecFact", DbType.Int64, 0, ParameterDirection.Input, rCboFactores.SelectedValue.ToString());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
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

    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO---MOFIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rCboFactores.SelectedValue == "")
            {
                rCboFactores.CssClass = "cssTxtInvalid";
                rCboFactores.BorderWidth = Unit.Pixel(1);
                rCboFactores.BorderColor = System.Drawing.Color.Red;
                rCboFactores.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboFactores.BorderColor = System.Drawing.Color.Transparent; }

            

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }

        return sResult;
    }

    #endregion

}