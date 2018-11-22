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

public partial class XP_ConsultaSaldosInteg : System.Web.UI.Page
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
    private string Pag_CliCve;
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

    protected void RdDtFechaConsulta_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        EjecutaAccion();
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
            Pag_CliCve = Request.QueryString["provCve"];
        }
    }
    private void InicioPagina()
    {
        RdDtFechaConsulta.SelectedDate = DateTime.Now;
        LlenaGrid_Datos();
        LlenarUiProveedor(Pag_CliCve.Trim());
    }
    private void LlenaGrid_Datos()
    {
        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "" &&
            Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "" &&
            Request.QueryString["provCve"] != null && Request.QueryString["provCve"] != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConsultaSaldosXPIntegra";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
            ProcBD.AgregarParametrosProcedimiento("@fecCalculo", DbType.String, 10, ParameterDirection.Input, ObtenerFecha());
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, Pag_CliCve.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvIntegracionSaldosXP, ds);
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
    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDtFechaConsulta.SelectedDate);

        //Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        Val_TransFec = dt.Year + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            LlenaGrid_Datos();
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
        if (hdfBtnAccion.Value == "")
        {
            if (ObtenerFecha() == "10101")
            {
                RdDtFechaConsulta.BorderWidth = 1;
                RdDtFechaConsulta.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDtFechaConsulta.BorderWidth = 0;
                RdDtFechaConsulta.BorderColor = System.Drawing.Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }



        return sResult;
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

}