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

public partial class Menu_MGMApartadosEstandar : System.Web.UI.Page
{

    #region VARIABLES
        MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
        ws.Servicio oWS = new ws.Servicio();

        MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
        MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
        MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
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

    protected void rGdvRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblSeleccion.Text = "";
        var dataItem = this.rGdvRegistros.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            Session["folio_Selection"] = dataItem.GetDataKeyValue("Cve").ToString();
            lblSeleccion.Text = dataItem.GetDataKeyValue("Cve").ToString() + "  " + dataItem["Descripcion"].Text; 
        }
       
    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnSeleccion_Click(object sender, ImageButtonClickEventArgs e)
    {
        LlenaGridSeleccionRegistros();
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "1", "Sys.Application.add_load(function(){{FNSeleccionRegistroApartadoEstandar();}}, 0);", true);
    }


    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
       
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "1", "Sys.Application.add_load(function(){{FNSeleccionRegistroApartadoEstandar();}}, 0);", true);

    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {

    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //EjecutaAccion();

        //Session["folio_Selection"] = "";
        //lblSeleccion.Text = "";
        //RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "1", "FNSeleccionRegistroApartadoEstandar();}}, 0);", true);

    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //InicioPagina();
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

    private void InicioPagina()
    {

        TituloApartado();

        addMenuLocalColumns();
        LlenaGridSeleccionRegistros();

        Session["folio_Selection"] = "";
        lblSeleccion.Text = "";

        string script = "<script language='javascript' type='text/javascript'>Sys.Application.add_load(FNSeleccionRegistroApartadoEstandar);</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "FNSeleccionRegistroApartadoEstandar", script);
    }

    private void TituloApartado() {

        if (Pag_sidM == "10") {
            FNGrales.bTitleDesc(Page, "Mantenimiento Clientes", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "11")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Articulo", "PnlMPFormTituloApartado");
        }

    }


    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
        RadWinMenu();
    }

    private void RadWinMenu()
    {

        DataSet ds = new DataSet();
        ds = dsSelMenuLocal();

        if (FnValAdoNet.bDSIsFill(ds))
        {

            DataRow[] DrwEjecFun = ds.Tables[0].Select("maOperTipo = 2");
            foreach (DataRow Drw in DrwEjecFun)
            {

                string Fn = Convert.ToString(Drw["maOperEjecFn"]).ToString().Trim();
                string NavigateUrl = Convert.ToString(Drw["maOperEjecFnNavUrl"]).ToString().Trim();
                string Title = Convert.ToString(Drw["maMenuDes"]).ToString().Trim();

                RadWindow rwin = new RadWindow();
                rwin.ID = Fn;
                rwin.ReloadOnShow = true;
                rwin.NavigateUrl = NavigateUrl;
                rwin.Title = Title;
                RadWindowManagerPage.Windows.Add(rwin);
            }

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
        else {

            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
    }

    private void LlenaGridSeleccionRegistros()
    {
        DataSet ds = dsSeleccionRegistros();
        if (FnValAdoNet.bDSIsFill(ds)) {
            FnCtlsFillIn.RadGrid(ref this.rGdvRegistros, ds);
        }
       
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

    private DataSet dsSeleccionRegistros()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MGMApartadosEstandar";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@idMenu", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    #endregion

}