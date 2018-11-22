using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;

public partial class FR_CancelaDoc : System.Web.UI.Page
{

    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_Regid;
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
    protected void rCboDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string tipFolio = "", valFolio = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Documentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            tipFolio = ds.Tables[0].Rows[0]["docFolTip"].ToString();
            valFolio = ds.Tables[0].Rows[0]["folVal"].ToString();

            rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), "");

            if (tipFolio == "1")
            {
                rTxtFolio.Enabled = true;
            }
            else if (tipFolio == "2")
            {
                rTxtFolio.Enabled = false;
            }

        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);


    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        //ControlesAccion();
        //EjecutaAccionLimpiar();
    }


    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
        InicioPagina();
    }


    #endregion


    #region METODO
    public void InicioPagina() {

        ValorUrl();
        LlenaDatos();
        LLenaCombo();
    }

    public void LlenaDatos() {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 59);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Pag_Regid);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds)) {

            lblDoc.Text = ds.Tables[0].Rows[0]["docDes"].ToString();
            lblFolio.Text = ds.Tables[0].Rows[0]["docRegFolio"].ToString();
            lblDes.Text = ds.Tables[0].Rows[0]["docRegDes"].ToString();
            lblCliente.Text = ds.Tables[0].Rows[0]["clieNom"].ToString();

           
        }
        DateTime dat1 = DateTime.Now;

        lblFecha.Text = System.String.Format("{0:d}", Convert.ToDateTime(dat1));
    }

    #endregion

    #region FUNCIONES
    public void ValorUrl()
    {
        if (Request.QueryString["Regid"] != null && Request.QueryString["Regid"] != "")
        {
            Pag_Regid = Request.QueryString["Regid"];
        }
    }

    private void EjecutaSpAcciones()
    {

        try
        {
            ValorUrl();
            string maUser = LM.sValSess(this.Page, 1);
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRODoc_DocumentoCancela";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_Regid));
            ProcBD.AgregarParametrosProcedimiento("@DocCve_Contra", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@docRegFolio_Contra", DbType.String, 10, ParameterDirection.Input, rTxtFolio.Text);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }


        }
        //}
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
             EjecutaSpAcciones();
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
        
            if (rCboDocumento.Text.Trim() == "")
            {
                rCboDocumento.CssClass = "cssTxtInvalid";
                rCboDocumento.BorderWidth = Unit.Pixel(1);
                rCboDocumento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboDocumento.BorderColor = System.Drawing.Color.Transparent; }

            if (rTxtFolio.Text.Trim() == "")
            {
            rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtFolio.CssClass = "cssTxtEnabled"; }
            
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

        return sResult;
    }

    public void LLenaCombo() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Pag_Regid);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadComboBox(ref this.rCboDocumento, ds, "docCve", "docDes", false, false);
            ((Literal)rCboDocumento.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboDocumento.Items.Count);
        }
    }
    #endregion


 
}