using System.Windows.Forms;
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


public partial class CG_ConsultaSaldosMov : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_pPerAnio;
    private string Pag_pPerNum;
    //private string Pag_CliCve;
    private string Pag_ctaContCve;
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

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "")
        {
            Pag_pPerAnio = Request.QueryString["pPerAnio"];
        }

        if (Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "")
        {
            Pag_pPerNum = Request.QueryString["pPerNum"];
        }

        if (Request.QueryString["ctaContCve"] != null && Request.QueryString["ctaContCve"] != "")
        {
            Pag_ctaContCve = Request.QueryString["ctaContCve"];
        }
        //if (Request.QueryString["cliCve"] != null && Request.QueryString["cliCve"] != "")
        //{
        //    Pag_CliCve = Request.QueryString["cliCve"];
        //}
    }

    private void InicioPagina()
    {

        //Titulo();
        //LlenarUiCliente(Pag_CliCve);
        Descripcion_CuentaContable(Pag_ctaContCve.Trim());
        LlenaGrid();
    }
    //private void Titulo()
    //{
    //    DataSet ds = new DataSet();

    //    Int64 iOpc = 71;
    //    if (Request.QueryString["pMovAcum"] != null && Request.QueryString["pMovAcum"] == "1")
    //    {
    //        iOpc = 72;
    //    }

    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_Periodos";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, iOpc);
    //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //    ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
    //    ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
    //    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    //    if (FnValAdoNet.bDSRowsIsFill(ds))
    //    {
    //        lblTitlePage.Text = ds.Tables[0].Rows[0]["Titulo"].ToString();
    //    }
    //}

    private void LlenaGrid()
    {
        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "" &&
            Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "" &&
            Request.QueryString["ctaContCve"] != null && Request.QueryString["ctaContCve"] != "")


            //Request.QueryString["cliCve"] != null && Request.QueryString["cliCve"] != "")
        {
            Int64 iOpc = 42;
           
            if (Request.QueryString["pMovAcum"] != null && Request.QueryString["pMovAcum"] == "1")
            {
                iOpc = 43;
            }
            else { iOpc = 43; }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConsultaSaldosCG";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, iOpc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, Pag_ctaContCve.Trim());
            //ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, Pag_CliCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvSaldos, ds);
        }
    }
    private void Descripcion_CuentaContable(string sCtaContable)
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasContables";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, sCtaContable);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                LblCtaContable.Text = ds.Tables[0].Rows[0]["ctaContCveFotmat"].ToString() + " " + ds.Tables[0].Rows[0]["ctaContNom"].ToString();
            }
            else
            {
                LblCtaContable.Text = "";
            }
        }
        catch (Exception ex)
        {
            LblCtaContable.Text = "";
            string MsgError = ex.Message.Trim();
        }

    }
    //private void LlenarUiCliente(string cliCve)
    //{

    //    try
    //    {
    //        DataSet ds = new DataSet();

    //        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //        ProcBD.NombreProcedimiento = "sp_Clientes";

    //        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
    //        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
    //        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //        if (FnValAdoNet.bDSRowsIsFill(ds))
    //        {
    //            LblClieNombre.Text = ds.Tables[0].Rows[0]["cliCveClie"].ToString() + ds.Tables[0].Rows[0]["cliCveSubClie"].ToString() + " " + ds.Tables[0].Rows[0]["clieNom"].ToString();
    //        }
    //        else
    //        {
    //            LblClieNombre.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LblClieNombre.Text = "";
    //        string MsgError = ex.Message.Trim();
    //    }


    #endregion
}