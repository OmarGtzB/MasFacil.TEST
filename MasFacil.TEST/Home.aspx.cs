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



public partial class Home : System.Web.UI.Page
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
    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
        {

            if (SM.IsActiveSession(this.Page,true,0))
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
        }

        private void InicioPagina()
        {


        FNGrales.bTitleDesc(Page, "HOME"); 


        }

        private void addRadWin() {
            //FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
        }

        

    #endregion

    #region FUNCIONES

    #endregion


}