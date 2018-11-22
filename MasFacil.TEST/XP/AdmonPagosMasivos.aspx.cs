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

public partial class XP_AdmonPagosMasivos : System.Web.UI.Page
{

    #region VARIABLES

    ws.Servicio oWS = new ws.Servicio();
    
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_cptoId;

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

    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //if (rGdvPagosPend.ClientSettings.Selecting.AllowRowSelect)
        //{
            loadGrid();
        //}
    }

    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        
        if (true)
        {
            loadGrid();
        }

        chkSitPer();

        //if (rGdvPagosPend.ClientSettings.Selecting.AllowRowSelect)
        //{
        //    loadGrid();
        //}

    }

    protected void rBtnManPagFact_CheckedChanged(object sender, EventArgs e)
    {
        calcTotRegs();
    }

    protected void rBtnManPagProv_CheckedChanged(object sender, EventArgs e)
    {
        calcTotRegs();
    }

    protected void rTxtProvCve_TextChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void rTxtProvNom_TextChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void RdDateFecha_InicioVenc_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        loadGrid();
    }

    protected void RdDateFecha_FinalVenc_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        loadGrid();
    }

    protected void RdDateFecha_InicioProg_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        loadGrid();
    }

    protected void RdDateFecha_FinalProg_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        loadGrid();
    }

    protected void rTxtImpIni_TextChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void rTxtImpFin_TextChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    //protected void rGdvPagosPend_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //((sender as Telerik.Web.UI.RadGrid).NamingContainer as Telerik.Web.UI.GridItem).Selected = (sender as Telerik.Web.UI.RadGrid).se;
    //    bool checkHeader = true;
    //    foreach (GridDataItem dataItem in rGdvPagosPend.MasterTableView.Items)
    //    {
    //        if (!(dataItem.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
    //        {
    //            checkHeader = false;
    //            break;
    //        }
    //    }
    //    GridHeaderItem headerItem = rGdvPagosPend.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.Header)[0] as GridHeaderItem;
    //    (headerItem.FindControl("headerChkbox") as System.Web.UI.WebControls.CheckBox).Checked = checkHeader;

    //    calcTotRegs();
    //}

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }
    
    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        ((sender as System.Web.UI.WebControls.CheckBox).NamingContainer as Telerik.Web.UI.GridItem).Selected = (sender as System.Web.UI.WebControls.CheckBox).Checked;
        bool checkHeader = true;
        foreach (GridDataItem dataItem in rGdvPagosPend.MasterTableView.Items)
        {
            if (!(dataItem.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                checkHeader = false;
                break;
            }
        }
        GridHeaderItem headerItem = rGdvPagosPend.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.Header)[0] as GridHeaderItem;
        (headerItem.FindControl("headerChkbox") as System.Web.UI.WebControls.CheckBox).Checked = checkHeader;

        calcTotRegs();

    }
    protected void ToggleSelectedState(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox headerCheckBox = (sender as System.Web.UI.WebControls.CheckBox);
        foreach (GridDataItem dataItem in rGdvPagosPend.MasterTableView.Items)
        {
            (dataItem.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked = headerCheckBox.Checked;
            dataItem.Selected = headerCheckBox.Checked;
        }

        calcTotRegs();
    }

    //GRET, se agrega evento para el boton que hace visible solo los  items seleccionados
    protected void rBtnGuardar2_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (rGdvPagosPend.SelectedItems.Count > 0)
        {
            ocultaItems();
        }
        else
        {
            ShowAlert("2", "No has seleccionado ningún registro");
        }
    }
    //

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_cptoId = Convert.ToString(Session["cptoId"]);
    }

    public void InicioPagina()
    {
        cleanUi();
        llenaCombos();
        controlPaso(0);
    }

    private void cleanUi()
    {

        rCboConcepto.ClearSelection();
        RdDateFecha.Clear();
        rCboCuenta.ClearSelection();
        rTxtDescripcion.Text = "";
        rBtnManPagProv.Checked = true;

        rTxtProvCve.Text = "";
        rTxtProvNom.Text = "";
        RdDateFecha_InicioVenc.Clear();
        RdDateFecha_FinalVenc.Clear();
        RdDateFecha_InicioProg.Clear();
        RdDateFecha_FinalProg.Clear();
        rTxtImpIni.Text = "";
        rTxtImpFin.Text = "";

        rGdvPagosPend.DataSource = null;
        rGdvPagosPend.DataBind();

    }

    private void llenaCombos()
    {
        RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);
        RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);

        RadComboBox_CuentasDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboCuenta, true, false);
    }

    private void controlPaso(int paso)
    {

        rCboCuenta.Enabled = false;
        rTxtDescripcion.Enabled = false;
        rBtnManPagProv.Enabled = false;
        rBtnManPagFact.Enabled = false;

        rTxtProvCve.Enabled = false;
        rTxtProvNom.Enabled = false;
        RdDateFecha_InicioVenc.Enabled = false;
        RdDateFecha_FinalVenc.Enabled = false;
        RdDateFecha_InicioProg.Enabled = false;
        RdDateFecha_FinalProg.Enabled = false;
        rTxtImpIni.Enabled = false;
        rTxtImpFin.Enabled = false;

        rGdvPagosPend.ClientSettings.Selecting.AllowRowSelect = false;
        //rGdvPagosPend.MasterTableView.ClearSelectedItems();
        rGdvPagosPend.AllowMultiRowSelection = false;

        //GRET
        rBtnGuardar2.Enabled = false;
        //rBtnGuardar.Enabled = false;
        rBtnGuardar.Visible = false;
        rBtnCancelar.Enabled = false;


        if (paso == 1)
        {
            rCboCuenta.Enabled = true;
            RdDateFecha.Enabled = true;
            rTxtDescripcion.Enabled = true;
            rBtnManPagProv.Enabled = true;
            rBtnManPagFact.Enabled = true;

            rTxtProvCve.Enabled = true;
            rTxtProvNom.Enabled = true;
            RdDateFecha_InicioVenc.Enabled = true;
            RdDateFecha_FinalVenc.Enabled = true;
            RdDateFecha_InicioProg.Enabled = true;
            RdDateFecha_FinalProg.Enabled = true;
            rTxtImpIni.Enabled = true;
            rTxtImpFin.Enabled = true;

            rGdvPagosPend.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvPagosPend.MasterTableView.ClearSelectedItems();
            rGdvPagosPend.AllowMultiRowSelection = true;
            //GRET
            rBtnGuardar2.Enabled = true;           
            //
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }


        foreach (GridDataItem item in rGdvPagosPend.Items)
        {
            item.SelectableMode = GridItemSelectableMode.ServerSide;
        }

        
    }
    
    private void loadGrid()
    {
        if (rCboConcepto.SelectedValue != "")
        {
            try
            {
                //Obtener Datos del Formulario para Consulta
                string Val_Fec_InicioVenc = "";
                string Val_Fec_FinalVenc = "";
                string Val_Fec_InicioProg = "";
                string Val_Fec_FinalProg = "";

                decimal pImpIni = 0;
                decimal pImpFin = 999999999999999;

                if (RdDateFecha_InicioVenc.SelectedDate != null)
                {
                    DateTime dt = Convert.ToDateTime(RdDateFecha_InicioVenc.SelectedDate);
                    Val_Fec_InicioVenc = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
                }
                else
                {
                    Val_Fec_InicioVenc = "1990-01-01 00:00:00";
                }

                if (RdDateFecha_FinalVenc.SelectedDate != null)
                {
                    DateTime dt = Convert.ToDateTime(RdDateFecha_FinalVenc.SelectedDate);
                    Val_Fec_FinalVenc = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
                }
                else
                {
                    Val_Fec_FinalVenc = "2070-01-01 00:00:00";
                }

                if (RdDateFecha_InicioProg.SelectedDate != null)
                {
                    DateTime dt = Convert.ToDateTime(RdDateFecha_InicioProg.SelectedDate);
                    Val_Fec_InicioProg = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
                }
                else
                {
                    Val_Fec_InicioProg = "1990-01-01 00:00:00";
                }

                if (RdDateFecha_FinalProg.SelectedDate != null)
                {
                    DateTime dt = Convert.ToDateTime(RdDateFecha_FinalProg.SelectedDate);
                    Val_Fec_FinalProg = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
                }
                else
                {
                    Val_Fec_FinalProg = "2070-01-01 00:00:00";
                }

                if (rTxtImpIni.Text != "")
                {
                    pImpIni = Convert.ToDecimal(rTxtImpIni.Text);
                }

                if (rTxtImpFin.Text != "")
                {
                    pImpFin = Convert.ToDecimal(rTxtImpFin.Text);
                }

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_PartidasPendientesPago";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, rTxtProvCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@provNom", DbType.String, 100, ParameterDirection.Input, rTxtProvNom.Text);
                ProcBD.AgregarParametrosProcedimiento("@FecVencIni", DbType.String, 100, ParameterDirection.Input, Val_Fec_InicioVenc);
                ProcBD.AgregarParametrosProcedimiento("@FecVencFin", DbType.String, 100, ParameterDirection.Input, Val_Fec_FinalVenc);
                ProcBD.AgregarParametrosProcedimiento("@FecProgIni", DbType.String, 100, ParameterDirection.Input, Val_Fec_InicioProg);
                ProcBD.AgregarParametrosProcedimiento("@FecProgFin", DbType.String, 100, ParameterDirection.Input, Val_Fec_FinalProg);
                ProcBD.AgregarParametrosProcedimiento("@ImpIni", DbType.Decimal, 15, ParameterDirection.Input, pImpIni);
                ProcBD.AgregarParametrosProcedimiento("@ImpFin", DbType.Decimal, 15, ParameterDirection.Input, pImpFin);

                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                rGdvPagosPend.DataSource = ds;
                rGdvPagosPend.DataBind();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }

    private void calcTotRegs()
    {

        string newMsgLbl = "";
        string currentProv = "";
        int countFacturas = 0;
        int countProveedores = 0;
        int countPagos = 0;

        foreach (GridDataItem itemToCount in rGdvPagosPend.Items)
        {

            if (itemToCount.Selected)
            {
                countFacturas++;
                if (itemToCount.Cells[3].Text != currentProv)
                {
                    countProveedores++;
                    if (rBtnManPagProv.Checked && itemToCount.Cells[3].Text != currentProv)
                    {
                        countPagos++;
                    }
                }
                currentProv = itemToCount.Cells[3].Text;
                if (rBtnManPagFact.Checked)
                {
                    countPagos++;
                }
            }
            
        }
        
        newMsgLbl = "Se han incluido: " + countProveedores + " Proveedor(es) | Se han seleccionado: " + countPagos + " Pasivo(s) | Se han generado: " + countFacturas + " Factura(s) ";
        lblTotPagos.Text = newMsgLbl;

    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            EjecutaSpAcciones();
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private void EjecutaSpAcciones()
    {

        string lastProv = "";
        bool boolHead = true;
        int cortePago = 1;

        if (rBtnManPagProv.Checked)
        {
            cortePago = 0;
        }

        string FtransId = "";
        int FsecTrans = 0;
        decimal sumImpProv = 0;

        string FfolTrans = "";

        Int64 FpagXPId = 0;

        string Val_TransFec = ObtenerFecha();


        foreach (GridDataItem item in rGdvPagosPend.SelectedItems)
        {

            if (cortePago == 1 || (cortePago == 0 && boolHead == true && lastProv != item.Cells[3].Text))
            {

                //Variables
                string maUser = LM.sValSess(this.Page, 1);
                string Folio = obtenFolio();

                FsecTrans = 1;
                sumImpProv = 0;

                FtransId = spTransHead(maUser, Folio, Val_TransFec);

                if (chkConfigCptoXP())
                {
                    spTransDetail(FtransId, FsecTrans, item);
                    GuardaMovimientos(FtransId, Folio);
                }

                FpagXPId = spPagosXP(Folio, item.Cells[3].Text, item.Cells[8].Text, FtransId);
                //GRET 
                //boolHead = false;
                //lastProv = item.Cells[3].Text;
                //

                FfolTrans = Folio;

                sumImpProv += Convert.ToDecimal(item.Cells[8].Text);

                //GRET, Se agrega llamada a metodo para guardar session y folio del registro seleccionado para posteriormente filtrar el registro en Administracion de pagos 
                GuardaSesionFolioXP(FfolTrans);
                //

            }
            else
            {

                if (lastProv == item.Cells[3].Text)
                {
                    FsecTrans++;

                    if (chkConfigCptoXP())
                    {
                        spTransDetail(FtransId, FsecTrans, item);
                        GuardaMovimientos(FtransId, FfolTrans);
                    }

                    lastProv = item.Cells[3].Text;
                    sumImpProv += Convert.ToDecimal(item.Cells[8].Text);
                }
                else
                {
                    //Actualizar ultimo PagosXP con el acumulado del importe
                    spPagosXPact(FpagXPId, sumImpProv.ToString(), FfolTrans, Val_TransFec, lastProv, FtransId);

                    //Variables
                    string maUser = LM.sValSess(this.Page, 1);
                    string Folio = obtenFolio();
                    //string Val_TransFec = ObtenerFecha();

                    FsecTrans = 1;
                    sumImpProv = 0;

                    FtransId = spTransHead(maUser, Folio, Val_TransFec);

                    if (chkConfigCptoXP())
                    {
                        spTransDetail(FtransId, FsecTrans, item);
                        GuardaMovimientos(FtransId, Folio);
                    }

                    FpagXPId = spPagosXP(Folio, item.Cells[3].Text, item.Cells[8].Text, FtransId);

                    boolHead = true;
                    lastProv = item.Cells[3].Text;
                    sumImpProv += Convert.ToDecimal(item.Cells[8].Text);

                    FfolTrans = Folio;
                }
            }
        }
    }

    private void spTransDetail(string transId, Int64 pSecTrans, GridDataItem rowItem)
    {
        int ValEliminar = 0;
        DataSet ds = new DataSet();


        string colCodApli = getColumn("cptoConfId_Ref10_CodApli");
        string colRefAplic = getColumn("cptoConfId_Ref10_Aplic");
        string colImp = getColumn("cptoConfId_Imp_Imp");
        string colFecMov = getColumn("cptoConfId_Fec_Mov");
        string colFecVenc = getColumn("cptoConfId_Fec_Venc");


        ValEliminar += 1;

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, pSecTrans);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetId"]));
        //La moneda de donde sale¿?
        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, getMonCpto());
        ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, 2);

        ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, "R");

        if (colCodApli != "" && colCodApli != colRefAplic)
        {
            ProcBD.AgregarParametrosProcedimiento("@" + colCodApli, DbType.String, 40, ParameterDirection.Input, rowItem["provCve"].Text);
        }

        if (colRefAplic != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@" + colRefAplic, DbType.String, 40, ParameterDirection.Input, rowItem["movRef10_Princ"].Text);
        }

        if (colImp != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@" + colImp, DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(rowItem["partPenPagImpAbo"].Text));
        }

        if (colFecMov != "" && colFecMov != colFecVenc)
        {
            DateTime date1 = new DateTime();
            string DetFec_01;
            date1 = Convert.ToDateTime(RdDateFecha.SelectedDate);
            DetFec_01 = date1.ToString("yyyy-MM-dd h:mm tt");
            ProcBD.AgregarParametrosProcedimiento("@" + colFecMov, DbType.String, 100, ParameterDirection.Input, DetFec_01.Substring(0, 10));

        }

        if (colFecVenc != "")
        {
            DateTime date2 = new DateTime();
            string DetFec_02;
            date2 = Convert.ToDateTime(rowItem["partPenPagFecVenc"].Text);
            DetFec_02 = date2.ToString("yyyy-MM-dd h:mm tt");
            ProcBD.AgregarParametrosProcedimiento("@" + colFecVenc, DbType.String, 100, ParameterDirection.Input, DetFec_02.Substring(0, 10));
        }

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds != null)
        {

        }

    }

    //GRET, Se agrega metodo para guardar session y folio del registro seleccionado para posteriormente filtrar el registro en Administracion de pagos   
    private void GuardaSesionFolioXP(String pagXPFolio)
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Session_XPFolio";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@pag_sConexionLog", DbType.String, 200, ParameterDirection.Input,Pag_sConexionLog);
            ProcBD.AgregarParametrosProcedimiento("@pag_sSessionLog", DbType.String, 200, ParameterDirection.Input, Pag_sSessionLog);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 200, ParameterDirection.Input, pagXPFolio);
            ProcBD.AgregarParametrosProcedimiento("@pag_cptoId", DbType.String, 200, ParameterDirection.Input, Pag_cptoId);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);            

        }
        catch (Exception e)
        {

        }
    }
    //
    private void GuardaMovimientos(string transId, string Folio)
    {

        try
        {
            string maUser = LM.sValSess(this.Page, 1);
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXMovimientos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@transFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, transId);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }
        catch (Exception ex)
        {
        }
    }

    private void spPagosXPact(Int64 pagXPId, string pImporte, string Folio, string fchPrmSp, string pProvCve, string prmTranId)
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.Int64, 0, ParameterDirection.Input, pagXPId);

            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepCve", DbType.String, 10, ParameterDirection.Input, rCboCuenta.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFec", DbType.String, 100, ParameterDirection.Input, fchPrmSp);
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, pProvCve);
            ProcBD.AgregarParametrosProcedimiento("@pagXPDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@pagXPNum", DbType.String, 10, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@pagXPImp", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(pImporte));
            ProcBD.AgregarParametrosProcedimiento("@pagXPSitEnt", DbType.String, 10, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@pagXPSit", DbType.String, 10, ParameterDirection.Input, "R");
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(prmTranId));

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjectransId, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1")
                {
                    //ShowAlert(sEjecEstatus, sEjecMSG);
                    //InicioPagina();
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    //GRET, metodo que oculta items que no son seleccionados
    private void ocultaItems()
    {
        foreach (GridDataItem item in rGdvPagosPend.Items)
        {
            if (item.Selected)
            {
                item.Visible = true;
            }
            else
            {
                item.Visible = false;
            }

        }

        rBtnGuardar.Visible = true;
        rBtnGuardar2.Visible = false;
    }
    //

    #endregion

    #region FUNCIONES

    public bool RabComboBox_ConceptoReferenciaTipo_SegUsuario(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 102);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }

    public bool RadComboBox_CuentasDeposito(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "ctaDepCve", "ctaDepDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }

    private bool chkSitPer()
    {
        bool response = true;
        
        try
        {
            DataSet ds = new DataSet();

            ds = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, RdDateFecha.SelectedDate.Value);
       
            if (FnValAdoNet.bDSIsFill(ds))
            {

                string sit = "";

                sit = ds.Tables[0].Rows[0]["maMSGTip"].ToString();

                if (sit == "1")
                {
                    //MessageBox.Show("EL PERIODO ESTA ABIERTO");
                    controlPaso(1);
                }
                else if (sit != "1")
                {

                    //ShowAlert("3", "EL PERIODO ESTA CERRADO");
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(sEjecEstatus, sEjecMSG);

                    rGdvPagosPend.DataSource = null;
                    rGdvPagosPend.DataBind();
                    controlPaso(0);
                    
                    //LimpiarUi();

                }
            }
            else
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                
                controlPaso(0);
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
            throw;
        }



        return response;
    }

    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    public string obtenFolio()
    {
        string tipFolio = "", valFolio = "", Response = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            tipFolio = ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString();
            valFolio = ds.Tables[0].Rows[0]["cptoDefFolVal"].ToString();
            
            Response = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio) , "");
        }

        return Response;

    }

    private string spTransHead(string maUser, string Folio, string Val_TransFec)
    {
        string response = "";

        try
        {
            
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesEncabezado";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@transFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@transFec", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ProcBD.AgregarParametrosProcedimiento("@transDes", DbType.String, 100, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@maUsuCveReg", DbType.String, 20, ParameterDirection.Input, maUser);
            ProcBD.AgregarParametrosProcedimiento("@transFecReg", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                response = ds.Tables[0].Rows[0]["transId"].ToString();
            }

        }
        catch (Exception ex)
        {
            response = "";
            throw;
        }

        return response;
    }

    private bool chkConfigCptoXP()
    {
        bool response = false;

        DataSet dsTransDet = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MovimientosXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        dsTransDet = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (dsTransDet != null)
        {
            if (dsTransDet.Tables.Count > 0)
            {
                if (dsTransDet.Tables[0].Rows.Count > 0)
                {
                    response = true;
                }
            }
        }

        return response;

    }

    private string getColumn(string cptoConfRef)
    {

        string response = "";

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MovimientosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, Convert.ToInt64(rCboConcepto.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@refColumnMov", DbType.String, 100, ParameterDirection.Input, cptoConfRef);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response = ds.Tables[0].Rows[0]["columnName"].ToString();
                    }
                    else
                    {
                        response = "";
                    }

                }
                else
                {
                    response = "";
                }
            }
            else
            {
                response = "";
            }


        }
        catch (Exception ex)
        {
            response = "";

        }

        return response;

    }

    private Int64 spPagosXP(string Folio, string pProvCve, string pImporte, string prmTranId)
    {

        Int64 response = 0;

        try
        {
            string fchPrmSp;

            fchPrmSp = "";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Year.ToString();
            fchPrmSp += "-";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Month.ToString().PadLeft(2, '0');
            fchPrmSp += "-";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Day.ToString().PadLeft(2, '0');
            fchPrmSp += " 00:00:00";

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepCve", DbType.String, 10, ParameterDirection.Input, rCboCuenta.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFec", DbType.String, 100, ParameterDirection.Input, fchPrmSp);
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, pProvCve);
            ProcBD.AgregarParametrosProcedimiento("@pagXPDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@pagXPNum", DbType.String, 10, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@pagXPImp", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(pImporte));
            ProcBD.AgregarParametrosProcedimiento("@pagXPSitEnt", DbType.String, 10, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@pagXPSit", DbType.String, 10, ParameterDirection.Input, "R");
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(prmTranId));
            //ProcBD.AgregarParametrosProcedimiento("@pagXPFecReg", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjectransId, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                response = Convert.ToInt64(ds.Tables[0].Rows[0]["pagXPId"].ToString());

                if (sEjecEstatus == "1")
                {
                    //ShowAlert(sEjecEstatus, sEjecMSG);
                    //InicioPagina();
                }
            }

        }
        catch (Exception)
        {
            response = 0;
            throw;
        }

        return response;

    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        if (rCboCuenta.SelectedValue == "")
        {
            rCboCuenta.CssClass = "cssTxtInvalid";
            rCboCuenta.BorderWidth = Unit.Pixel(1);
            rCboCuenta.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else { rCboCuenta.BorderColor = System.Drawing.Color.Transparent; }

        if (RdDateFecha.SelectedDate.ToString() == "")
        {
            RdDateFecha.CssClass = "cssTxtInvalid";
            RdDateFecha.BorderWidth = Unit.Pixel(1);
            RdDateFecha.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else
        {
            RdDateFecha.CssClass = "cssTxtEnabled";
            RdDateFecha.BorderWidth = Unit.Pixel(1);
            RdDateFecha.BorderColor = System.Drawing.Color.Transparent;
        }

        if (rCboConcepto.SelectedValue == "")
        {
            rCboConcepto.CssClass = "cssTxtInvalid";
            rCboConcepto.BorderWidth = Unit.Pixel(1);
            rCboConcepto.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else { rCboConcepto.BorderColor = System.Drawing.Color.Transparent; }

        if (rTxtDescripcion.Text == "")
        {
            rTxtDescripcion.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else
        {
            rTxtDescripcion.CssClass = "cssTxtEnabled";
        }

        if (rGdvPagosPend.SelectedItems.Count == 0)
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            return sResult;
        }

        if (camposInc > 0)
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }

        return sResult;
    }

    private string getMonCpto()
    {

        string sMonCve = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModulo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(rCboConcepto.SelectedValue));
        ProcBD.AgregarParametrosProcedimiento("@movMapCCSecApli", DbType.Int64, 0, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            sMonCve = ds.Tables[1].Rows[0]["monCve"].ToString();

            if (sMonCve != "")
            {
                return sMonCve;
            }
            else
            {
                //Moneda Default          
                return MonedaDefault();
            }
        }
        else
        {
            //Moneda Default
            return MonedaDefault();
        }
    }
    
    private string MonedaDefault()
    {
        string sMonCve = "";
        decimal dMonTipoCambio = 0;
        FNGrales.bTipoCambioMonedaDefault(Pag_sConexionLog, Pag_sCompania, ref sMonCve, ref dMonTipoCambio);
        return sMonCve;
    }

    #endregion
    
}