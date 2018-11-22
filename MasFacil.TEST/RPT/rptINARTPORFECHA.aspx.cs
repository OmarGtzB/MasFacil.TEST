using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;

public partial class RPT_rptINARTPORFECHA : System.Web.UI.Page
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
        FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, false);
    }
     
    //======================== Controles Paso 2 Niveles ========================
    //=> Nivel Agrupacion 1
    protected void rCboNivel1AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel1Agr);
        if (rCboNivel1AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel1AgrTipo.SelectedValue), ref rCboNivel1Agr, true, false);
        }
    }
    //=> Nivel Agrupacion 2
    protected void rCboNivel2AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel2Agr);
        if (rCboNivel2AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel2AgrTipo.SelectedValue), ref rCboNivel2Agr, true, false);
        }
    }
    //=> Nivel Agrupacion 3
    protected void rCboNivel3AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboNivel3Agr);
        if (rCboNivel3AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboNivel3AgrTipo.SelectedValue), ref rCboNivel3Agr, true, false);
        }
    }


    //======================== Controles Paso 3 Filtros ========================
    //=>Filtro 1
    protected void rCboFiltro1AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1Agr);
        RadComboBoxItems(ref rCboFiltro1Agr);

        RadComboBoxItems(ref rCboFiltro1AgrDato);
        rCboFiltro1AgrDato.ClearCheckedItems();

        if (rCboFiltro1AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro1AgrTipo.SelectedValue), ref rCboFiltro1Agr, true, false);
        }
    }
    protected void rCboFiltro1Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1AgrDato);
        RadComboBoxItems(ref rCboFiltro1AgrDato);
        if (rCboFiltro1Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro1AgrDato, rCboFiltro1Agr.SelectedValue, Convert.ToInt32(rCboFiltro1AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }

    //=>Filtro 2
    protected void rCboFiltro2AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro2Agr);
        RadComboBoxItems(ref rCboFiltro2Agr);
        RadComboBoxItems(ref rCboFiltro2AgrDato);
        if (rCboFiltro2AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro2AgrTipo.SelectedValue), ref rCboFiltro2Agr, true, false);
        }
    }
    protected void rCboFiltro2Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro2AgrDato);
        RadComboBoxItems(ref rCboFiltro2AgrDato);
        if (rCboFiltro2Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro2AgrDato, rCboFiltro2Agr.SelectedValue, Convert.ToInt32(rCboFiltro2AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }

    //=>Filtro 3
    protected void rCboFiltro3AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro3Agr);
        RadComboBoxItems(ref rCboFiltro3Agr);
        RadComboBoxItems(ref rCboFiltro3AgrDato);
        if (rCboFiltro3AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro3AgrTipo.SelectedValue), ref rCboFiltro3Agr, true, false);
        }
    }
    protected void rCboFiltro3Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro3AgrDato);
        RadComboBoxItems(ref rCboFiltro3AgrDato);
        if (rCboFiltro3Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro3AgrDato, rCboFiltro3Agr.SelectedValue, Convert.ToInt32(rCboFiltro3AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }
    }

    //========================= EJECUTAR EL REPORTE =========================
    protected void rbtnCrearReporte_Click(object sender, EventArgs e)
    {
        DataSet dsReports = new DataSet();
        DataSet dsInputGral = new DataSet();
        DataSet dsInputAux = new DataSet();

        //Agregar filtros a las tablas de paso
        FiltroPaso();
        FiltrosAgrupacionPaso(1, rCboFiltro1AgrTipo, rCboFiltro1Agr, rCboFiltro1AgrDato);
        FiltrosAgrupacionPaso(2, rCboFiltro2AgrTipo, rCboFiltro2Agr, rCboFiltro2AgrDato);
        FiltrosAgrupacionPaso(3, rCboFiltro3AgrTipo, rCboFiltro3Agr, rCboFiltro3AgrDato);

        //Obtener informacion de los Dataset Utilizados en los reportes
        dsInputGral = dsReportsInputGral();
        dsInputAux = dsReportsInputAux();

        if (FnValAdoNet.bDSRowsIsFill(dsInputGral))
        {
            dsReports = oWSRpt.ds_Reports(dsInputGral, dsInputAux);
            Session["Reports_dsByte"] = dsReports;
            Session["SitReports"] = "1";

            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        else
        {
            Session["SitReports"] = "2";
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        hdfRptCve.Value = "INARTPORFECHA";
        Session["Reports_dsByte"] = null;
        Session["SitReports"] = "0";
        InicioLlenaControles();
    }
    private void InicioLlenaControles()
    {
        FnCtlsFillIn.RabComboBox_PeriodoAnios(Pag_sConexionLog, Pag_sCompania, ref rCboAnioPeriodo, true, true);

        if (rCboAnioPeriodo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, true);
        }
        rCboNumPeriodo.SelectedIndex = -1;

        rDateFecCalculo.SelectedDate = DateTime.Now;

        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboNivel3AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, hdfRptCve.Value, "", ref rCboFiltro3AgrTipo, true, false);
        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulo, true, false);
        FnCtlsFillIn.RabComboBox_Almacen(Pag_sConexionLog, Pag_sCompania, ref rCboAlmacen, true, false);
 
    }
    private void RadComboBoxItems(ref RadComboBox objRadComboBox)
    {
        objRadComboBox.DataSource = null;
        objRadComboBox.DataBind();
        objRadComboBox.ClearSelection();
        objRadComboBox.Text = string.Empty;

        foreach (RadComboBoxItem item in objRadComboBox.Items)
        {
            item.Checked = false;
            item.Text = "";
            item.Visible = false;
        }
    ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + "0";
    }
    private void FiltroPaso()
    {
        ShowCheckedItemsArt();
        ShowCheckedItemsAlm();
    }
    private void FiltrosAgrupacionPaso(int iNivel, RadComboBox objAgrTip, RadComboBox objAgr, RadComboBox objAgrDato)
    {
        string maUser = LM.sValSess(this.Page, 1);
        var collection = objAgrDato.CheckedItems;
        if (collection.Count != 0)
        {
            string sAgrDato = "";
            foreach (var item in collection)
            {
                sAgrDato = item.Value.ToString();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_rptFiltroAgrupacion_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, hdfRptCve.Value);
                ProcBD.AgregarParametrosProcedimiento("@rptNiv", DbType.Int64, 0, ParameterDirection.Input, iNivel);
                ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, objAgrTip.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, objAgr.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, sAgrDato);
                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
        else
        {
            if (objAgr.SelectedIndex != -1)
            {
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_rptFiltroAgrupacion_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, hdfRptCve.Value);
                ProcBD.AgregarParametrosProcedimiento("@rptNiv", DbType.Int64, 0, ParameterDirection.Input, iNivel);
                ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, objAgrTip.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, objAgr.SelectedValue);
                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }
    private void ShowCheckedItemsArt()
    {
        string maUser = LM.sValSess(this.Page, 1);
        var collection = rCboArticulo.CheckedItems;

        if (collection.Count != 0)
        {
            string artCve = "";
            foreach (var item in collection)
            {
                artCve = item.Value.ToString();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_filtros_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, hdfRptCve.Value.ToString() + "-artCve");
                ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 20, ParameterDirection.Input, artCve);

                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }
    private void ShowCheckedItemsAlm()
    {
        string maUser = LM.sValSess(this.Page, 1);
        var collection = rCboAlmacen.CheckedItems;

        if (collection.Count != 0)
        {

            string almCve = "";
            foreach (var item in collection)
            {
                almCve = item.Value.ToString();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_filtros_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, hdfRptCve.Value.ToString() + "-almCve");
                ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 20, ParameterDirection.Input, almCve);

                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }
    #endregion

    #region FUNCIONES
    private DataSet dsReportsInputGral()
    {
        DataSet ds = new DataSet();
        string maUser = LM.sValSess(this.Page, 1);
        string stored = "sp_rpt" + hdfRptCve.Value.Trim();


        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = stored;
 
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int64, 0, ParameterDirection.Input, rCboAnioPeriodo.SelectedValue);
        if (rCboNumPeriodo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int64, 0, ParameterDirection.Input, rCboNumPeriodo.SelectedValue);
        }
        ProcBD.AgregarParametrosProcedimiento("@Fecha", DbType.String, 10, ParameterDirection.Input, sObtenerFechaCalculo());
        ProcBD.AgregarParametrosProcedimiento("@Refer", DbType.String, 10, ParameterDirection.Input, rCboFechaReferencia.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@Registros", DbType.Int64, 0, ParameterDirection.Input, iTipoRegistros());
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);

        if (rCboNivel1AgrTipo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel1AgrTip", DbType.Int64, 0, ParameterDirection.Input, rCboNivel1AgrTipo.SelectedValue);
        }
        if (rCboNivel1Agr.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel1AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel1Agr.SelectedValue);
        }

        if (rCboNivel2AgrTipo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel2AgrTip", DbType.Int64, 0, ParameterDirection.Input, rCboNivel2AgrTipo.SelectedValue);
        }
        if (rCboNivel2Agr.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel2AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel2Agr.SelectedValue);
        }

        if (rCboNivel3AgrTipo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel3AgrTip", DbType.Int64, 0, ParameterDirection.Input, rCboNivel3AgrTipo.SelectedValue);
        }
        if (rCboNivel3Agr.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel3AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel3Agr.SelectedValue);
        }

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private DataSet dsReportsInputAux()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDatosGenerales";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, hdfRptCve.Value);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private string sObtenerFechaCalculo()
    {
        string sResult = "";

        string Fec = rDateFecCalculo.SelectedDate.ToString();
        if (Fec != "")
        {
            DateTime dt = Convert.ToDateTime(Fec);
            sResult = dt.Year + dt.Month.ToString().PadLeft(2, '0')  + dt.Day.ToString().PadLeft(2, '0');

        }
        return sResult;
    }
    private int iTipoRegistros()
    {
        int iResult = 0;
        if (rptlNARTPORFECHAVencidos.Checked == true)
        {
            iResult = 1;
        }
        if (rptlNARTPORFECHAPorvencer.Checked == true)
        {
            iResult = 2;
        }
        return iResult;
    } 
    #endregion

}