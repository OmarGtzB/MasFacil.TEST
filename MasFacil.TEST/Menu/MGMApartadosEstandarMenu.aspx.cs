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

public partial class Menu_MGMApartadosEstandarMenu : System.Web.UI.Page
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




        addMenuLocal();
    }

    private void TituloApartado()
    {


        if (Pag_sidM == "106")
        {
            FNGrales.bTitleDesc(Page, "Preferencias Comerciales", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "107")
        {
            FNGrales.bTitleDesc(Page, "Preferencias Financieras", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "108")
        {
            FNGrales.bTitleDesc(Page, "Preferencias Generales", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "127")
        {
            FNGrales.bTitleDesc(Page, "Preferencias Contables", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "129")
        {
            FNGrales.bTitleDesc(Page, "SAT", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "87" )
        {
            FNGrales.bTitleDesc(Page, "Procesos Inventarios", "PnlMPFormTituloApartado");
        }

        if ( Pag_sidM == "91" )
        {
            FNGrales.bTitleDesc(Page, "Procesos Cuentas por Cobrar", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "95")
        {
            FNGrales.bTitleDesc(Page, "Procesos Cuentas por Pagar", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "103")
        {
            FNGrales.bTitleDesc(Page, "Procesos Contabilidad General", "PnlMPFormTituloApartado");
        }

    }

    private void addRadWin()
    {
        //FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
        RadWinMenu();


        //FnCtrlsRadWindows.cRadWindowsArticulos(Page, ref RadWindowManagerPage); 

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



                //Control micontrol = LoadControl("nombre.aspx");
                //this.Controls.Add(micontrol);

                //string Fn = Convert.ToString(Drw["maOperEjecFn"]).ToString().Trim();
                //string NavigateUrl = Convert.ToString(Drw["maOperEjecFnNavUrl"]).ToString().Trim();
                //string Title = Convert.ToString(Drw["maMenuDes"]).ToString().Trim();

                //RadWindow rwin = new RadWindow();
                //rwin.ID = Fn;
                //rwin.ReloadOnShow = true;
                //rwin.NavigateUrl = NavigateUrl;
                //rwin.Title = Title;
                //rwin.ContentContainer.Controls.Add(micontrol);
                //RadWindowManagerPage.Windows.Add(rwin);






            }


        }


    }


    private void addMenuLocal()
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

        else if (num == 7)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 8)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 9)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 10)
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


    #endregion


    #region FUNCIONES
    private DataSet dsSelMenuLocal()
    {

        DataSet ds = new DataSet();
        String sUsuCve = Convert.ToString(Session["user"]);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SelMAMenu";
        ProcBD.AgregarParametrosProcedimiento("@maMenuTipo", DbType.Int64, 0, ParameterDirection.Input, 2);
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