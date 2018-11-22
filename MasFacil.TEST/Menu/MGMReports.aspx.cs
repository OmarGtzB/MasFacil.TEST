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

public partial class Menu_MGMReports : System.Web.UI.Page
{

    #region VARIABLES
        MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
        MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

        MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();

        ws.Servicio oWS = new ws.Servicio();

        MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();

        private string Pag_sCompania;
        private string Pag_sConexionLog;
        private string Pag_sSessionLog;
        private string Pag_sidM;
    

    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void RadButton1_Click(object sender, EventArgs e)
    {

        RadButton btn = sender as RadButton;
        string sNavigateUrl = btn.Text;

        if (sNavigateUrl != "") {
            rWinReports.NavigateUrl = sNavigateUrl;

            string script = "function f(){$find(\"" + rWinReports.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (Session["SitReports"].ToString().Trim() == "1") {
            Response.Redirect("~/RPT/Visor.aspx");
        } else if (Session["SitReports"].ToString().Trim() == "2") {
            ShowAlert("2", "No se encontro información con los parametros seleccionados.");
        }
            
     
        
    }

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Request.QueryString["idM"];
        }
    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }
    public void InicioPagina()
    {
        Session["RPTVISOR_name"] = "";
        TituloPagina();
        addMenuLocalColumns();
    }
    private void TituloPagina()
    {

        if (Pag_sidM == "67")
        {
            FNGrales.bTitleDesc(Page, "Reportes Cuentas por Cobrar");
        }
        else if (Pag_sidM == "98") {
            FNGrales.bTitleDesc(Page, "Reportes Cuentas por Pagar");
        }
        else if (Pag_sidM == "117")
        {
            FNGrales.bTitleDesc(Page, "Reportes Contabilidad General");
        }
        else if (Pag_sidM == "88")
        {
            FNGrales.bTitleDesc(Page, "Reportes Inventarios");
        }
        else if (Pag_sidM == "128")
        {
            FNGrales.bTitleDesc(Page, "Reportes Ventas");
        }
        else
        {
            FNGrales.bTitleDesc(Page, "Reportes");
        }
    }
    private void addMenuLocalColumns()
    {

        DataSet ds = new DataSet();
        ds = dsSelMenuLocal();

        DataListMenu.DataSource = ds;
        Int32 num;
        num = ds.Tables[0].Rows.Count;

        if (num == 1)
        {

            DataListMenu.RepeatColumns = 1;
            DataListMenu.DataBind();


        }
        else if (num == 2)
        {

            DataListMenu.RepeatColumns = 2;

            DataListMenu.DataBind();


        }
        else if (num == 3)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 4)
        {


            DataListMenu.RepeatColumns = 2;
            DataListMenu.DataBind();

        }
        else if (num == 5)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 6)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else
        {

            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    private DataSet dsSelMenuLocal()
    {
        DataSet ds = new DataSet();
        String sUsuCve = Convert.ToString(Session["user"]);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SelMAMenu";
        ProcBD.AgregarParametrosProcedimiento("@maMenuTipo", DbType.Int64, 0, ParameterDirection.Input, 3);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
        ProcBD.AgregarParametrosProcedimiento("@maMenuIdP", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@sessionlog", DbType.String, 30, ParameterDirection.Input, Pag_sSessionLog);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            return ds;
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        return ds;
    }

    #endregion



}