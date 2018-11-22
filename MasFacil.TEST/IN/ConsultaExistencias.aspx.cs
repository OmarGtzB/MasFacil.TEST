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
using System.Data.Entity;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
public partial class IN_ConsultaExistencias : System.Web.UI.Page
{


    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();
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
    protected void rBtnExisAlm_CheckedChanged(object sender, EventArgs e)
    {
        Consulta_Consolidado();
        LlenaGrid();
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnSaldo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString();
        ControlesAccion();
    }
    protected void rBtnSaldoMov_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }
    
    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rCboAnioPeriodo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboAnioPeriodo.SelectedValue != "")
        {
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, true);
        }

        if (rCboAnioPeriodo.SelectedValue != "" && rCboNumPeriodo.SelectedValue != "")
        {
            ShowCheckedItemsDatoCve(rCboFiltro1AgrDato);
            LlenaGrid();
        }
    }


    protected void rCboNumPeriodo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboAnioPeriodo.SelectedValue != "" && rCboNumPeriodo.SelectedValue != "")
        {
            ShowCheckedItemsDatoCve(rCboFiltro1AgrDato);
            LlenaGrid();
        }
        
    }
    protected void rCboArticulo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ShowCheckedItemsDatoCve(rCboFiltro1AgrDato);
        LlenaGrid();
    }

    //FILTRO 1
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

        LlenaGrid();

        rCboFiltro1Agr.Enabled = true;
    }
    protected void rCboFiltro1Agr_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FNClear.RadComboBox(ref rCboFiltro1AgrDato);
        RadComboBoxItems(ref rCboFiltro1AgrDato);
        if (rCboFiltro1Agr.SelectedIndex != -1)
        {
            FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboFiltro1AgrDato, rCboFiltro1Agr.SelectedValue, Convert.ToInt32(rCboFiltro1AgrTipo.SelectedValue), true, false, "", "agrDatoCve", "agrDatoCve_DatoDes");
        }

        LlenaGrid();

        rCboFiltro1AgrDato.Enabled = true;

    }


    //FILTROS POR AGRUPACIONES
    protected void rCboFiltro1AgrDato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        eliminaCheck();
        ShowCheckedItemsDatoCve(rCboFiltro1AgrDato);
        LlenaGrid();
    }
    
    protected void rCboFiltro1AgrDato_ItemChecked(object sender, RadComboBoxItemEventArgs e)
    {

    }

    protected void rCboFiltro1AgrDato_CheckAllCheck(object sender, RadComboBoxCheckAllCheckEventArgs e)
    {
        eliminaCheck();
        ShowCheckedItemsDatoCve(rCboFiltro1AgrDato);
        LlenaGrid();
    }

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }


    public void InicioPagina()
    {
        TituloPagina();
        InicioLlenaControles();
        Consulta_Consolidado();
        
        PermisoBotones();
        FnCtlsFillIn.RadComboBox_RptNiveles_AgrTipo(Pag_sConexionLog, "INRELEXIS", "", ref rCboFiltro1AgrTipo, true, false);
        LlenaGrid();

        rCboFiltro1Agr.ClearSelection();
        rCboFiltro1Agr.ClearCheckedItems();
        rCboFiltro1Agr.Enabled = false;


        rCboFiltro1AgrDato.ClearSelection();
        rCboFiltro1AgrDato.ClearCheckedItems();
        rCboFiltro1AgrDato.Enabled = false;

    }



    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }




    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Consulta de Existencias", "PnlMPFormTituloApartado");
    }
    private void InicioLlenaControles()
    {
        FnCtlsFillIn.RabComboBox_PeriodoAnios(Pag_sConexionLog, Pag_sCompania, ref rCboAnioPeriodo, true, true);
        if (rCboAnioPeriodo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, true);
        }
        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulo, true, false);

        rBtnExisAlmSi.Checked = true;
        rBtnExisAlmNo.Checked = false;
    }

    private void LlenaGrid()
    {
        string maUser = LM.sValSess(this.Page, 1);

        int iPerAnio = 0;
        int iPerNum = 0;
        if (rCboAnioPeriodo.SelectedIndex != -1)
        {
            iPerAnio = Convert.ToInt16(rCboAnioPeriodo.SelectedValue);
        }
        if (rCboNumPeriodo.SelectedIndex != -1)
        {
            iPerNum = Convert.ToInt16(rCboNumPeriodo.SelectedValue);
        }
        int iConsolidado = Consulta_Consolidado();
        
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConsultaExistencias";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 40);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, iPerAnio);
        ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, iPerNum);
        ProcBD.AgregarParametrosProcedimiento("@consolidado", DbType.Int64, 0, ParameterDirection.Input, iConsolidado);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, "FAgrDatoConsulExisIN"); // filtroAgrupacionesInventario

        if (rCboArticulo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
        }


        //FILTRO1
        if (rCboFiltro1AgrTipo.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@agrTipo", DbType.Int32, 0, ParameterDirection.Input, rCboFiltro1AgrTipo.SelectedValue);
        }

        //Agrupacion
        if (rCboFiltro1Agr.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rCboFiltro1Agr.SelectedValue);
        }

        //DATOS DE AGRUPACION
        if (rCboFiltro1AgrDato.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, rCboFiltro1AgrDato.SelectedValue);
        }

 
        

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdExistencias, ds);
    }


        private void ShowCheckedItemsDatoCve(RadComboBox comboBox)
        {
            string maUser = LM.sValSess(this.Page, 1);
            var collection = comboBox.CheckedItems;

            if (collection.Count != 0)
            {

                string agrDatoCve = "";
                foreach (var item in collection)
                {
                    agrDatoCve = item.Value.ToString();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_filtros_Paso";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                    ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, "FAgrDatoConsulExisIN");
                    ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 20, ParameterDirection.Input, agrDatoCve.Trim());

                    oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
            }
        }

        private void eliminaCheck() {

            string maUser = LM.sValSess(this.Page, 1);
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_filtros_Paso";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
            ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, "FAgrDatoConsulExisIN");

            oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnSaldoMensual.Image.Url = "~/Imagenes/IcoBotones/IcoBtnSaldos.png";
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
            //rBtnSaldoMensual
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
            {
                EjecutaAccion();
            }

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
            var dataItem = rGdExistencias.SelectedItems[0] as GridDataItem;
            string sArtCve = dataItem["artCve"].Text;
            string sAlmCve = dataItem["almCve"].Text;
            string sRefSelect = dataItem["artCve"].Text.Trim() + " " + dataItem["artDes"].Text.Trim();
            string sRefSelect_almDes =  dataItem["almDes"].Text.Trim();
            
            if (sAlmCve == "&nbsp;")
            {
                sAlmCve = "";
                sRefSelect += " / " + "Consolidado";
            }
            else {
                sRefSelect += " / " + sRefSelect_almDes;
            }
            //SALDOS MENSUAL
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
            {
                string sScript = "<script>openPopUpSaldosMes('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "','" + sArtCve + "','" + sAlmCve + "','" + Consulta_Consolidado().ToString() + "','" + sRefSelect + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }
            // SALDO MOVIMIENTO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
            {
                string sScript = "<script>openPopUpSaldosMov('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "','" + sArtCve + "','" + sAlmCve + "','" + Consulta_Consolidado().ToString() + "','" + sRefSelect + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private void EjecutaAccionLimpiar()
    {
        InicioPagina();
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

    #region FUNCIONES
    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
        {
            if (rGdExistencias.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            return sResult;
        }
        return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdExistencias.SelectedItems.Count;
        string[] GvVAS;

        //rBtnSaldoMensual
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdExistencias, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //rBtnSaldoMov
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdExistencias, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //rBtnSaldoAntig
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdExistencias, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VER rBtnSaldoInteg
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdExistencias, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        return sResult;
    }
    private int Consulta_Consolidado() {
        int result = 2;
        rGdExistencias.MasterTableView.Columns[3].Display = true;
        if (rBtnExisAlmNo.Checked == true)
        {
            result = 1;
            rGdExistencias.MasterTableView.Columns[3].Display = false;
        }
        return result;
    }

    public bool RadComboBoxItems(ref RadComboBox objRadComboBox)
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

        return true;
    }

    #endregion


}