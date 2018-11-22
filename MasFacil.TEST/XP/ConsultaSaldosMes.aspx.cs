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

public partial class XP_ConsultaSaldoMes : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_pPerAnio;
    private string Pag_pPerNum;
    private string Pag_ProvCve;
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
    protected void rBtnSaldoMov_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
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

        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "")
        {
            Pag_pPerAnio = Request.QueryString["pPerAnio"];
        }

        if (Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "")
        {
            Pag_pPerNum = Request.QueryString["pPerNum"];
        }

        if (Request.QueryString["provCve"] != null && Request.QueryString["provCve"] != "")
        {
            Pag_ProvCve = Request.QueryString["provCve"];
        }
    }
    private void InicioPagina()
    {
        LlenarUiProveedor(Pag_ProvCve.Trim());
        LlenaGrid();
    }
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnSaldoMov.Image.Url = "~/Imagenes/IcoBotones/IcoBtnMovimientos.png";

        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucion(true);
        }
        else
        {
            ControlesAccionEjecucion(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //rBtnSaldoMov
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
            {
                EjecutaAccion();
            }
        }

        if (Result == false)
        {
        }
    }

    private void EjecutaAccion()
    {


        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);

        if (msgValidacion == "")
        {

            var dataItem = rGdvSaldos.SelectedItems[0] as GridDataItem;
            string perAnio = dataItem["perAnio"].Text;
            string PerNum = dataItem["perNum"].Text;



            // SALDO MOVIMIENTO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
            {
                string sScript = "<script>openPopUpSaldosMov('" + perAnio + "',' " + PerNum + "',' " + Pag_ProvCve + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private void LlenaGrid()
    {
        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "" &&
            Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "" &&
            Request.QueryString["provCve"] != null && Request.QueryString["provCve"] != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConsultaSaldosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 41);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, Pag_ProvCve.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvSaldos, ds);
        }
    }

    private void LlenarUiProveedor(string provCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@provCveClie", DbType.String, 20, ParameterDirection.Input, provCve.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                LblProvNombre.Text = ds.Tables[0].Rows[0]["provCve"].ToString() + " " + ds.Tables[0].Rows[0]["provNom"].ToString();
            }
            else
            {
                LblProvNombre.Text = "";
            }
        }
        catch (Exception ex)
        {
            LblProvNombre.Text = "";
            string MsgError = ex.Message.Trim();
        }

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvSaldos.SelectedItems.Count;
        string[] GvVAS;

        //rBtnSaldoMov
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvSaldos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        return sResult;
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
        {

            if (rGdvSaldos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }
    #endregion
}