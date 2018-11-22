﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;

public partial class RPT_rptXPPROVLIST : System.Web.UI.Page
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

    protected void rCboFormato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }

    //Niveles de Agrupacion
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

    //Filtros de Agrupacion
    protected void rCboFiltro1AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1Agr);
        FNClear.RadComboBox(ref rCboFiltro1AgrDato);
        if (rCboFiltro1AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro1AgrTipo.SelectedValue), ref rCboFiltro1Agr, true, false);
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
    protected void rCboFiltro2AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro2Agr);
        if (rCboFiltro2AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro2AgrTipo.SelectedValue), ref rCboFiltro2Agr, true, false);
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
    protected void rCboFiltro3AgrTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro3Agr);
        if (rCboFiltro3AgrTipo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboFiltro3AgrTipo.SelectedValue), ref rCboFiltro3Agr, true, false);
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

    //Filtro de Proveedor


    //Ejecutar Reporte
    protected void rbtnEjecutaReporte_Click(object sender, EventArgs e)
    {
        CreaReporte();
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

        Session["Reports_dsByte"] = null;
        Session["SitReports"] = "0";

        LlenaCombosTiposAgrupacion();
        Limpia_FiltroProveedores_Paso();
    }
    private void LlenaCombosTiposAgrupacion()
    {
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboNivel1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboNivel2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboNivel3AgrTipo, true, false);

        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboFiltro1AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboFiltro2AgrTipo, true, false);
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, rCboFormato.SelectedValue.ToString(), "", ref rCboFiltro3AgrTipo, true, false);
        
        FnCtlsFillIn.RabComboBox_Proveedores(Pag_sConexionLog, Pag_sCompania, ref rCboProveedores, true, false);
    }

    private void FiltroProveedorPaso()
    {
        ShowCheckedItems(rCboProveedores);
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
                ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
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
                ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
                ProcBD.AgregarParametrosProcedimiento("@rptNiv", DbType.Int64, 0, ParameterDirection.Input, iNivel);
                ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, objAgrTip.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, objAgr.SelectedValue);
                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }
    private void ShowCheckedItems(RadComboBox comboBox)
    {
        string maUser = LM.sValSess(this.Page, 1);
        var collection = comboBox.CheckedItems;
        if (collection.Count != 0)
        {
            string cliCve = "";
            foreach (var item in collection)
            {
                cliCve = item.Value.ToString();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_rptFiltroProveedores_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }
    public void Limpia_FiltroProveedores_Paso()
    {
        string maUser = LM.sValSess(this.Page, 1);
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_rptFiltroProveedores_Paso";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }
    private void CreaReporte()
    {
        ShowCheckedItems(rCboProveedores);

        DataSet dsReports = new DataSet();
        DataSet dsInputGral = new DataSet();
        DataSet dsInputAux = new DataSet();

        //Agregar filtros a las tablas de paso
        FiltroProveedorPaso();
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

    #region FUNCIONES
   
    private DataSet dsReportsInputGral()
    {
        DataSet ds = new DataSet();

        string maUser = LM.sValSess(this.Page, 1);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_rpt" + rCboFormato.SelectedValue.ToString();

        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ProcBD.AgregarParametrosProcedimiento("@orden", DbType.Int32, 0, ParameterDirection.Input, OrdenamientoClie_Nom());
        ProcBD.AgregarParametrosProcedimiento("@ordenAscDesc", DbType.Int32, 0, ParameterDirection.Input, OrdenamientoAscenDesc());

        //NIVEL 1
        if (rCboNivel1AgrTipo.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel1AgrTip", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboNivel1AgrTipo.SelectedValue));
        }
        if (rCboNivel1Agr.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel1AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel1Agr.SelectedValue.ToString().Trim());
        }

        //NIVEL 2
        if (rCboNivel2AgrTipo.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel2AgrTip", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboNivel2AgrTipo.SelectedValue));
        }
        if (rCboNivel2Agr.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel2AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel2Agr.SelectedValue.ToString().Trim());
        }

        //NIVEL 3
        if (rCboNivel3AgrTipo.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel3AgrTip", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboNivel3AgrTipo.SelectedValue));
        }
        if (rCboNivel3Agr.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@nivel3AgrCve", DbType.String, 3, ParameterDirection.Input, rCboNivel3Agr.SelectedValue.ToString().Trim());
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
        ProcBD.AgregarParametrosProcedimiento("@rptCve", DbType.String, 20, ParameterDirection.Input, rCboFormato.SelectedValue.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private int OrdenamientoClie_Nom()
    {

        if (rBtnCheckOrdenProveedorNum.Checked == true)
        {
            return 1;
        }
        else if (rBtnCheckOrdenProveedorNom.Checked == true)
        {
            return 2;
        }
        return 0;
    }
    private int OrdenamientoAscenDesc()
    {

        if (rBtnCheckAscendente.Checked == true)
        {
            return 1;
        }
        else if (rBtnCheckDescendente.Checked == true)
        {
            return 2;
        }
        return 0;
    }

    #endregion



}