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

public partial class Menu_MGMMenu : System.Web.UI.Page
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
            if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "") {
                Pag_sidM = Request.QueryString["idM"];
            }   
        }

        private void InicioPagina()
        {

        Session["TipoCptoOpe"] = "";

        if (Pag_sidM == "2")
        {
            FNGrales.bTitleDesc(Page, "Inventarios");
            Session["TipoCptoOpe"] = "IN";
        }

        if (Pag_sidM == "3")
        {
            FNGrales.bTitleDesc(Page, "Cuentas por Cobrar");
            Session["TipoCptoOpe"] = "CC";
        }

        if (Pag_sidM == "4")
        {
            FNGrales.bTitleDesc(Page, "Cuentas por Pagar");
            Session["TipoCptoOpe"] = "XP";
        }


        if (Pag_sidM == "61")
        {
            FNGrales.bTitleDesc(Page, "Contabilidad");
            Session["TipoCptoOpe"] = "CG";
        }




        if (Pag_sidM == "5")
        {
            FNGrales.bTitleDesc(Page, "Ventas");
        }


        if (Pag_sidM == "6")
        {
            FNGrales.bTitleDesc(Page, "Preferencias");
        }

        if (Pag_sidM == "70")
        {
            FNGrales.bTitleDesc(Page, "Seguridad");
        }

        if (Pag_sidM == "157")
        {
            FNGrales.bTitleDesc(Page, "Compras");
        }

        if (Pag_sidM == "158")
        {
            FNGrales.bTitleDesc(Page, "Nómina");
        }


        addMenuLocal();
        }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
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

    private void addMenuLocal() {

        DataSet ds = new DataSet();
        ds = dsSelMenuLocal();

        DataList1.DataSource = ds;
        Int32 num;
        num = ds.Tables[0].Rows.Count;

        if (num == 1)
        {

            DataList1.RepeatColumns = 1;
            DataList1.DataBind();


        }
        else if (num == 2)
        {

            DataList1.RepeatColumns = 2;

            DataList1.DataBind();


        }
        else if (num == 3)
        {


            DataList1.RepeatColumns = 3;
            DataList1.DataBind();

        }
        else if (num == 4)
        {


            DataList1.RepeatColumns = 2;
            DataList1.DataBind();

        }
        else if (num == 5)
        {


            DataList1.RepeatColumns = 5;
            DataList1.DataBind();

        }
        else if (num == 6)
        {


            DataList1.RepeatColumns = 3;
            DataList1.DataBind();

        }

        else if (num == 7)
        {


            DataList1.RepeatColumns = 4;
            DataList1.DataBind();

        }
        else if (num == 8)
        {


            DataList1.RepeatColumns = 4;
            DataList1.DataBind();

        }
        else if (num == 9)
        {


            DataList1.RepeatColumns = 5;
            DataList1.DataBind();

        }
        else if (num == 10)
        {


            DataList1.RepeatColumns = 5;
            DataList1.DataBind();

        }
        else {

            DataList1.RepeatColumns = 6;
            DataList1.DataBind();

        }
    }

    #endregion

    #region FUNCIONES
        private DataSet dsSelMenuLocal() {

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


    private void RWindowsCompaniasTEMPORAL()
    {

        Panel Panel1 = new Panel();
        Label lbl = new Label();
        RadComboBox RCboCompanias = new RadComboBox();
        RadButton rBtnAceptar = new RadButton();


        rBtnAceptar.Text = "Aceptar";

        DataSet ds = new DataSet();

        FnCtlsFillIn.RadComboBox(ref RCboCompanias, ds, "ciaCve", "ciaDes", true, true, Convert.ToString(Session["Compania"]));


        RadComboBox cbo;
        cbo = ((RadComboBox)Master.FindControl("RCbo_Companias"));


        Panel1.Controls.Add(cbo);
        Panel1.Controls.Add(RCboCompanias);
        Panel1.Controls.Add(rBtnAceptar);

        string Fn = "RADCIA";
        string NavigateUrl = "";
        string Title = "Compañia prueba";

        RadWindow rwin = new RadWindow();


        rwin.ID = Fn;
        rwin.NavigateUrl = NavigateUrl;
        rwin.Title = Title;
        rwin.ContentContainer.Controls.Add(Panel1);



        RadWindowManagerPage.Windows.Add(rwin);

    }

 


}