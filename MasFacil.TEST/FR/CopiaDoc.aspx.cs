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

public partial class FR_CopiaDoc : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

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

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION


    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionCopiar();
    }

    #endregion

    #region METODO
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        ValorUrl();
    }

    public void InicioPagina()
    {
        hdfDocCve.Value = "";
        LlenaDatos();
        Foliador();
    }

    public void ValorUrl()
    {
        if (Request.QueryString["Regid"] != null && Request.QueryString["Regid"] != "")
        {
            Pag_Regid = Request.QueryString["Regid"];
        }
    }

    public void LlenaDatos()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 59);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Pag_Regid);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            lblDoc.Text = ds.Tables[0].Rows[0]["docDes"].ToString();
            lblFolio.Text = ds.Tables[0].Rows[0]["docRegFolio"].ToString();
            lblDes.Text = ds.Tables[0].Rows[0]["docRegDes"].ToString();
            lblCliente.Text = ds.Tables[0].Rows[0]["clieNom"].ToString();
            hdfDocCve.Value = ds.Tables[0].Rows[0]["docCve"].ToString();

        }
        DateTime dat1 = DateTime.Now;
        lblFecha.Text = System.String.Format("{0:d}", Convert.ToDateTime(dat1));
    }

    private void Foliador()
    {
        string tipFolio = "", valFolio = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Documentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, hdfDocCve.Value);
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

    private void EjecutaAccionCopiar()
    {

        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            string maUser = LM.sValSess(this.Page, 1);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_EXPRODoc_DocumentoCopia";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(Pag_Regid));
        ProcBD.AgregarParametrosProcedimiento("@docRegFolio_Copia", DbType.String, 10, ParameterDirection.Input, rTxtFolio.Text);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String,20, ParameterDirection.Input, maUser);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            ShowAlert(sEjecEstatus, sEjecMSG);

                string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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

    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";

        if (rTxtFolio.Text == "") {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            return sResult;
        }
            
        return sResult;
    }

    #endregion


}