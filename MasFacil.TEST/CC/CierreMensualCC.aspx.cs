using System;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class CC_CierreMensualCC : System.Web.UI.Page
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
    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        rCboAnio.ClearSelection();
        rCboMes.ClearSelection();
        rCboMes.Enabled = false;
        rGdvCierreMensual.MasterTableView.ClearSelectedItems();
    }
    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        if (validaEjecutaAccion() == 0)
        {
            EjecutaSpAcciones();
        }
        else
        {
            string sMSGTip = "", sMsgAlert = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sMsgAlert);
            ShowAlert(sMSGTip, sMsgAlert);
        }
    }
    protected void rCboAnio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboMes.Enabled = true;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Periodos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 101);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, rCboAnio.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboMes, ds, "perNum", "perDes", true, false);
        ((Literal)rCboMes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboMes.Items.Count);
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
        LlenaGrid();
        LlenaComboAnio();
        hdfBtnAccion.Value = "";
        rGdvCierreMensual.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvCierreMensual.AllowMultiRowSelection = false;

        rCboAnio.ClearSelection();
        rCboMes.ClearSelection();
        rCboMes.Enabled = false;
        rGdvCierreMensual.MasterTableView.ClearSelectedItems();
    }
    public void LlenaGrid()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CierreMensual";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@moduCve", DbType.String, 10, ParameterDirection.Input, "CC");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvCierreMensual, ds);
    }
    private void LlenaComboAnio()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Periodos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 100);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboAnio, ds, "perAnio", "perAnio", true, false);
        ((Literal)rCboAnio.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAnio.Items.Count);

    }
    private void EjecutaSpAcciones()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CierreMensual";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.String, 10, ParameterDirection.Input, rCboAnio.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.String, 100, ParameterDirection.Input, rCboMes.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@moduCve", DbType.String, 2, ParameterDirection.Input, "CC");
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            ShowAlert(sEjecEstatus, sEjecMSG);
            if (sEjecEstatus == "1")
            {
                InicioPagina();
            }
        }
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    private int validaEjecutaAccion()
    {

        int camposInc = 0;

        if (rCboAnio.SelectedValue == "")
        {
            rCboAnio.BorderWidth = Unit.Pixel(1);
            rCboAnio.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else { rCboAnio.BorderColor = System.Drawing.Color.Transparent; }



        if (rCboMes.SelectedValue == "")
        {
            rCboMes.BorderWidth = Unit.Pixel(1);
            rCboMes.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else { rCboMes.BorderColor = System.Drawing.Color.Transparent; }

        return camposInc;

    }

    #endregion
}