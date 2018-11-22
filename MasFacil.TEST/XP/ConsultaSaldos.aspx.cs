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

public partial class XP_ConsultaSaldos : System.Web.UI.Page
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
    protected void rBtnSaldoAntig_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString();
        ControlesAccion();

    }
    protected void rBtnSaldoInteg_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rCboAnioPeriodo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, true);

        if (rCboAnioPeriodo.SelectedValue != "" && rCboNumPeriodo.SelectedValue != "")
        {
            LlenaGrid();
        }
    }
    protected void rCboNumPeriodo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboAnioPeriodo.SelectedValue != "" && rCboNumPeriodo.SelectedValue != "")
        {
            LlenaGrid();
        }
    }
    protected void rCboProveedores_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGrid();
    }

    protected void rCboProveedores_ItemChecked(object sender, RadComboBoxItemEventArgs e)
    {
        LlenaGrid();
    }

    protected void rCboProveedores_CheckAllCheck(object sender, RadComboBoxCheckAllCheckEventArgs e)
    {
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
        LlenaGrid();
    }
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Consulta de Saldos Cuentas por Pagar", "PnlMPFormTituloApartado");
    }
    private void InicioLlenaControles()
    {
        FnCtlsFillIn.RabComboBox_PeriodoAnios(Pag_sConexionLog, Pag_sCompania, ref rCboAnioPeriodo, true, true);
        if (rCboAnioPeriodo.SelectedIndex != -1)
        {
            FnCtlsFillIn.RabComboBox_PeriodoNum(Pag_sConexionLog, Pag_sCompania, Convert.ToInt16(rCboAnioPeriodo.SelectedValue), ref rCboNumPeriodo, true, true);
        }
        FnCtlsFillIn.RabComboBox_Proveedores(Pag_sConexionLog, Pag_sCompania, ref rCboProveedores, true, false);
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
        ShowCheckedItems(rCboProveedores);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConsultaSaldosXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 40);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, iPerAnio);
        ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, iPerNum);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, "CONSULTASALDOXP");

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvSaldos, ds);
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnSaldoMensual.Image.Url = "~/Imagenes/IcoBotones/IcoBtnSaldos.png";
        rBtnSaldoMov.Image.Url = "~/Imagenes/IcoBotones/IcoBtnMovimientos.png";
        rBtnSaldoAntig.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAntiguedad.png";
        rBtnSaldoInteg.Image.Url = "~/Imagenes/IcoBotones/IcoBtnIntegracion.png";


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

            //rBtnSaldoAntig
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString())
            {
                EjecutaAccion();
            }

            //rBtnSaldoInteg
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString())
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
            string provCve = dataItem["provCve"].Text;

            //SALDOS MENSUAL
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
            {
                string sScript = "<script>openPopUpSaldosMes('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "',' " + provCve + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }

            // SALDO MOVIMIENTO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
            {
                string sScript = "<script>openPopUpSaldosMov('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "',' " + provCve + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }

            // SALDO ANTIGUEDAD
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString())
            {
                string sScript = "<script>openPopUpSaldosAntiguedad('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "',' " + provCve + "')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key", sScript, false);
            }

            // SALDO INTEGRACION
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString())
            {
                string sScript = "<script>openPopUpSaldosIntegra('" + rCboAnioPeriodo.SelectedValue + "',' " + rCboNumPeriodo.SelectedValue + "',' " + provCve + "')</script>";
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
                ProcBD.NombreProcedimiento = "sp_filtros_Paso";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@FilCve", DbType.String, 20, ParameterDirection.Input, "CONSULTASALDOXP");
                ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 20, ParameterDirection.Input, cliCve);

                oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }


        }

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

            if (rGdvSaldos.SelectedItems.Count == 0)
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
        int GvSelectItem = rGdvSaldos.SelectedItems.Count;
        string[] GvVAS;

        //rBtnSaldoMensual
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMensual).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvSaldos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //rBtnSaldoMov
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoMov).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvSaldos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //rBtnSaldoAntig
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoAntig).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvSaldos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VER rBtnSaldoInteg
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.SaldoInteg).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvSaldos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        return sResult;
    }

    #endregion


}