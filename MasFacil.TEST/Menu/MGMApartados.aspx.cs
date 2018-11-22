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


public partial class Menu_MGMApartados : System.Web.UI.Page
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
        if (SM.IsActiveSession(this.Page, true, 0))
        {
                Valores_InicioPag();
                addRadWin();
                if (!IsPostBack)
                {
                    InicioPagina();
                }
            }
        }

    protected void rGdvArticulos_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblSeleccion.Text = "";
        var dataItem = rGdvArticulos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            Session["folio_Selection"] = dataItem.GetDataKeyValue("artCve").ToString();
            lblSeleccion.Text = dataItem.GetDataKeyValue("artCve").ToString() + "  " +  dataItem["artDes"].Text; ;
        }
    }

    protected void rBtnSeleccion_Click(object sender, ImageButtonClickEventArgs e)
    {
        LlenaGridArticulos();
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "1", "Sys.Application.add_load(function(){{FNSeleccionArticulo();}}, 0);", true);
    }
    protected void rbtnNuevoArt_Click(object sender, EventArgs e)
    {
        Session["folio_Selection"] ="";
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

        FNGrales.bTitleDesc(Page, "Mantenimiento Artículo", "PnlMPFormTituloApartado");

        addMenuLocalColumns();
        LlenaGridArticulos();

         Session["folio_Selection"] = "";
        lblSeleccion.Text = "";

        //rBtnSeleccion.Attributes["onclick"] = "FNSeleccionArticulo();";
        string script = "<script language='javascript' type='text/javascript'>Sys.Application.add_load(FNSeleccionArticulo);</script>";
        ClientScript.RegisterStartupScript(this.GetType(), "FNSeleccionArticulo", script);
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




            //string Fn_ = "FNTempo4";
            //string NavigateUrl_ = "";
            //string Title_ = "Articulos";

            //RadWindow rwin2 = new RadWindow();
            //rwin2.ID = Fn_;
            ////rwin2.ReloadOnShow = true;
            //rwin2.NavigateUrl = NavigateUrl_;
            //rwin2.Title = Title_;
            //rwin2.ContentContainer.Controls.Add(this.pnlArticulos);
            //RadWindowManagerPage.Windows.Add(rwin2);



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

    private void LlenaGridArticulos() {
        DataSet ds = dsArticulos();
        FnCtlsFillIn.RadGrid(ref rGdvArticulos, ds);
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
    
    private DataSet dsArticulos() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    #endregion



}