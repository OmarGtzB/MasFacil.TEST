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

public partial class FR_WizardConsultaAcumulados : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();

    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.Clean FNClear = new MGMControls.Clean();

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
    protected void rCboAnioPeriodo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodoInicial, true, false);
        FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodoFinal, true, false);
    }
    protected void rCboAgentes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboAgentes.SelectedValue != "")
        {
            LlenaCboClientesvw(rCboAgentes.SelectedValue.ToString());
        }
        else
        {
            LlenaCboClientesvw("");
        }

    }
    protected void rCboNivel1AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel1Agr);
        if (rCboNivel1AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel1AgrTipo.SelectedValue), ref rCboNivel1Agr, true, false);
        }
    }
    protected void rCboNivel2AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel2Agr);
        if (rCboNivel2AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel2AgrTipo.SelectedValue), ref rCboNivel2Agr, true, false);
        }
    }
    protected void rCboNivel3AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel3Agr);
        if (rCboNivel3AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel3AgrTipo.SelectedValue), ref rCboNivel3Agr, true, false);
        }
    }
    protected void rCboFiltro1AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1Agr);
        FNClear.RadComboBox(ref rCboFiltro1AgrDato);
        if (rCboFiltro1AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro1AgrTipo.SelectedValue), ref rCboFiltro1Agr, true, false);
        }
    }
    protected void rCboFiltro2AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro2Agr);
        if (rCboFiltro2AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro2AgrTipo.SelectedValue), ref rCboFiltro2Agr, true, false);
        }
    }
    protected void rCboFiltro3AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro3Agr);
        if (rCboFiltro3AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro3AgrTipo.SelectedValue), ref rCboFiltro3Agr, true, false);
        }
    }
    protected void rCboFiltro1Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1AgrDato);
        if (rCboFiltro1Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro1AgrDato, rCboFiltro1Agr.SelectedValue, Convert.ToInt32(rCboFiltro1AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }
    protected void rCboFiltro2Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro2AgrDato);
        if (rCboFiltro2Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro2AgrDato, rCboFiltro2Agr.SelectedValue, Convert.ToInt32(rCboFiltro2AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }
    protected void rCboFiltro3Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro3AgrDato);
        if (rCboFiltro3Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro3AgrDato, rCboFiltro3Agr.SelectedValue, Convert.ToInt32(rCboFiltro3AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }
    protected void NewRegistrationButton_Click(object sender, EventArgs e)
    {
        EjecutaSp_ObtenColumnaReporte();
        EjecutaSp();
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
        hdfRptCve.Value = "CONSULTAACUM";
        Session["Reports_dsByte"] = null;
        Session["SitReports"] = "0";
        Session["Config"] = "0";
        InicioLlenaControles();
    }
    private void InicioLlenaControles()
    {

        LlenaCboReportes();
        FnCtlsFillIn.RabComboBox_PeriodoAnios(Pag_sConexionLog, Pag_sCompania, ref rCboAnioPeriodo, true, true);
        if (rCboAnioPeriodo.SelectedIndex != -1) {
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodoInicial, true, true);
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodoFinal, true, true);
        }

        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel3AgrTipo, true, false);

        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro3AgrTipo, true, false);

        // EXTRAS //

        //Agentes
        LlenaCboAgentes();

        //CLIENTES
        LlenaCboClientesvw("");
        
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumentos, true, false);
        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulos, true, false);

    }
    private void LlenaCboReportes()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporte";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboReportes, ds, "idReporte", "nombre", true, true);
        ((Literal)rCboReportes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboReportes.Items.Count);

    }
    private void LlenaCboAgentes()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgentes, ds, "ageCve", "ageNom", true, true);
        ((Literal)rCboAgentes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgentes.Items.Count);
        rCboAgentes.ClearSelection();
    }
    private void LlenaCboClientesvw(String ageCve)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 59);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        if (ageCve != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 10, ParameterDirection.Input, ageCve);
        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboClientes, ds, "cliCve", "clieNom", true, true);
        ((Literal)rCboClientes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboClientes.Items.Count);
        rCboClientes.ClearSelection();
    }
    private void EjecutaSp()
    {

        string maUser = LM.sValSess(this.Page, 1);
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ObtenDetalleReporte_RangoPeriodos";
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, rCboReportes.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@compania", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@añio", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(rCboAnioPeriodo.SelectedValue));



        if (rCboNumPeriodoInicial.SelectedIndex == -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@periodoIni", DbType.Int64, 0, ParameterDirection.Input, 0);
        }else {
            ProcBD.AgregarParametrosProcedimiento("@periodoIni", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(rCboNumPeriodoInicial.SelectedValue));
        }

        if (rCboNumPeriodoFinal.SelectedIndex == -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@periodoFin", DbType.Int64, 0, ParameterDirection.Input, 0);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@periodoFin", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(rCboNumPeriodoFinal.SelectedValue));
        }

        // FILTROS SIMPLES
        if (rCboAgentes.SelectedIndex != -1) {
            ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 10, ParameterDirection.Input, rCboAgentes.SelectedValue);
        }

        if (rCboClientes.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboClientes.SelectedValue);
        }

        if (rCboDocumentos.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumentos.SelectedValue);
        }

        if (rCboArticulos.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulos.SelectedValue);
        }

        ProcBD.AgregarParametrosProcedimiento("@agrCve1", DbType.String, 3, ParameterDirection.Input, rCboFiltro1Agr.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@agrDato1", DbType.String, 20, ParameterDirection.Input, rCboFiltro1AgrDato.SelectedValue);

        ProcBD.AgregarParametrosProcedimiento("@agrCve2", DbType.String, 3, ParameterDirection.Input, rCboFiltro2Agr.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@agrDato2", DbType.String, 20, ParameterDirection.Input, rCboFiltro1AgrDato.SelectedValue);

        ProcBD.AgregarParametrosProcedimiento("@agrCve3", DbType.String, 3, ParameterDirection.Input, rCboFiltro3Agr.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@agrDato3", DbType.String, 20, ParameterDirection.Input, rCboFiltro1AgrDato.SelectedValue);

        ProcBD.AgregarParametrosProcedimiento("@usuario", DbType.String, 20, ParameterDirection.Input, maUser);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            Session["dsConsultaAcum"] = ds;
            //ScriptManager.RegisterStartupScript(this, GetType(), "close", "Close();", true);

            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
      

    }
    private void EjecutaSp_ObtenColumnaReporte() {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ObtenColumnaReporte";
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, rCboReportes.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        Session["dsConsultaAcum_Config"] = ds;
    }

    #endregion





 
}