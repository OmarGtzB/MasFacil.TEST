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
using System.IO;

public partial class FR_RelacionDocumento : System.Web.UI.Page
{

    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();


    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private static string Val_Fec_Inicio = "";
    private static string Val_Fec_Fin = "";
    private int SubCliente = 0;


    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    private void InicioPagina()
    {
        Par_Sublicliente();
        llenaFiltros();
        TituloPagina();
        rCboSubClientes.Enabled = false;
    }
    public void ClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboClientes, ds, "cliCveClie", "clieNom", true, false, "");
        ((Literal)rCboClientes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboClientes.Items.Count);

    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void llenaFiltros()
    {
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false, "");
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false, "");
        ClientesCombo();
    }

    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Relacion Documentos", "PnlMPFormTituloApartado");
    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
        Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {

        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }
    }

protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
        Val_Fec_Fin = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        Val_Fec_Fin = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            //LlenaGridDocumentosFiltros();
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");

        }
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    private bool compararFechas()
    {

        if (RdDateFecha_Inicio.SelectedDate > RdDateFecha_Final.SelectedDate)
        {
            return false;
        }
        else if (RdDateFecha_Final.SelectedDate < RdDateFecha_Inicio.SelectedDate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private void MostraPdf()
    {
        string UrlPdf = "";
        string UrlsDocs = "";
        string Tipo = "";
        //Variables para 
        string valdoscombos = "";
        valdoscombos = rCboClientes.SelectedValue + rCboSubClientes.SelectedValue;


        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptDataSetDoc_RelacionDocumento";
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


        ProcBD1.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, valdoscombos);



        ProcBD1.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
        
        
        if (rCboMoneda.SelectedValue != "")
        {
            ProcBD1.AgregarParametrosProcedimiento("@monCve", DbType.String, 10, ParameterDirection.Input, rCboMoneda.SelectedValue);
            ProcBD1.AgregarParametrosProcedimiento("@ValMon", DbType.Int32, 0, ParameterDirection.Input, 2);
        }
        else
        {
            ProcBD1.AgregarParametrosProcedimiento("@ValMon", DbType.Int32, 0, ParameterDirection.Input, 1);
        }

        if (Val_Fec_Inicio != "")
        {
            ProcBD1.AgregarParametrosProcedimiento("@fchIni", DbType.String, 100, ParameterDirection.Input, Val_Fec_Inicio + " 00:00:01");
        }
        else
        {
            ProcBD1.AgregarParametrosProcedimiento("@fchIni", DbType.String, 100, ParameterDirection.Input, "1950-01-01 00:00:00");
        }


        if (Val_Fec_Fin != "")
        {
            ProcBD1.AgregarParametrosProcedimiento("@fchFin", DbType.String, 100, ParameterDirection.Input, Val_Fec_Fin + " 23:59:59");
        }
        else
        {
            ProcBD1.AgregarParametrosProcedimiento("@fchFin", DbType.String, 100, ParameterDirection.Input, "2070-12-31 00:00:00");
        }

    if (checkConsolidacionMoneda.Checked == true)
    {
        ProcBD1.AgregarParametrosProcedimiento("@monCons", DbType.Int64, 0, ParameterDirection.Input, 1);
    }
    else
    {
        ProcBD1.AgregarParametrosProcedimiento("@monCons", DbType.Int64, 0, ParameterDirection.Input, 2);
    }

    
    ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds1.Tables[0].Rows.Count == 0)
        {

            ShowAlert("2", "No hay Documentos Existentes con este tipo de filtro");

            Val_Fec_Inicio = "";
            Val_Fec_Fin = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();

        }
        else
        {


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RptDataSetDoc";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);



            DataSet customerOrders = new DataSet();

            DataTable ordersTable = customerOrders.Tables.Add("Table");

        DataTable ordersTablee = customerOrders.Tables.Add("Tablee");


        DataRow rowTmpe = ordersTablee.NewRow();
        if (ordersTable.Columns.Count == 0)
        {
            ordersTablee.Clear();
            ordersTablee.Columns.Add("ciaImgLogo");
        }
        DataRow rowTmp = ordersTable.NewRow();

        if (ordersTable.Columns.Count == 0)
            {
                customerOrders.Clear();
                ordersTable.Columns.Add("ciaDes");
                ordersTable.Columns.Add("Emisor_ciaRSoc");
                ordersTable.Columns.Add("ClleNum");
                ordersTable.Columns.Add("domNExt");
                ordersTable.Columns.Add("domNInt");
                ordersTable.Columns.Add("docCve");
                ordersTable.Columns.Add("monCve");
                ordersTable.Columns.Add("ciaImgLogo");
                ordersTable.Columns.Add("formImpRpt");
                ordersTable.Columns.Add("opcGlbDet");
            }
            rowTmp["ciaDes"] = ds.Tables[0].Rows[0]["CiaDes"].ToString();
            rowTmp["Emisor_ciaRSoc"] = ds.Tables[0].Rows[0]["ciaRFis"].ToString();
            rowTmp["ClleNum"] = ds.Tables[0].Rows[0]["domClle"].ToString();
            rowTmp["domNExt"] = ds.Tables[0].Rows[0]["domNExt"].ToString();
            rowTmp["domNInt"] = ds.Tables[0].Rows[0]["domNInt"].ToString();

            rowTmp["ciaImgLogo"] = ds.Tables[0].Rows[0]["ciaImgLogo"];

            if (rCboMoneda.SelectedValue != "")
            {
                rowTmp["monCve"] = rCboMoneda.SelectedValue.Trim().ToString();
            }
            else
            {
                rowTmp["monCve"] = "Todas";
            }

            if (rCboDocumento.SelectedValue != "")
            {
                rowTmp["docCve"] = rCboDocumento.SelectedValue.Trim().ToString();
            }
            else
            {
                rowTmp["docCve"] = "Todos";
            }


            if (RelaDocsGlobal.Checked == true)
            {
                rowTmp["opcGlbDet"] = 0;
                Tipo = "Global";
            }
            else if (RelaDocsDetalle.Checked == true)
            {
                rowTmp["opcGlbDet"] = 1;
                Tipo = "Detalle";
            }

            rowTmp["formImpRpt"] = "rptRelacion";


            ordersTable.Rows.Add(rowTmp);
            ordersTablee.Rows.Add(rowTmpe);

        byte[] bytes = oWSRpt.byte_FormatoDoc(customerOrders, ds1);


            UrlPdf = "Relacion" + "-" + Tipo + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
            "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";




            string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
            System.IO.File.WriteAllBytes(apppath, bytes);

            doc.Attributes["src"] = "..//Temp//" + UrlPdf;

        }


    }/////


    protected void rBtnVizualizarDoc_Click(object sender, ImageButtonClickEventArgs e)
    {
        MostraPdf();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rCboDocumento.ClearSelection();
        rCboMoneda.ClearSelection();
        RdDateFecha_Inicio.Clear();
        RdDateFecha_Final.Clear();
        doc.Attributes["src"] = "";
        RelaDocsDetalle.Checked = false;
        RelaDocsGlobal.Checked = true;
        rCboClientes.ClearSelection();
        rCboSubClientes.ClearSelection();
        rCboSubClientes.Enabled = false;

    }
    private void Par_Sublicliente()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "CLIECVE");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int16, 10, ParameterDirection.Input, 6);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        SubCliente = Convert.ToInt32(ds.Tables[0].Rows[0]["parmValInt"].ToString());

        if (SubCliente == 1)
        {
            SubClientesCombo();
            rCboSubClientes.Enabled = true;
        }
        else
        {
            rCboSubClientes.Enabled = false;
            rLblSubClie.Enabled = false;
            rCboSubClientes.Visible = false;
            rLblSubClie.Visible = false;
        }


    }

    protected void rCboClientes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
        Par_Sublicliente();
        if (SubCliente == 1)
        {
            SubClientesCombo();
            rCboSubClientes.Enabled = true;
        }
        else
        {
            rCboSubClientes.Enabled = false;
            rLblSubClie.Enabled = false;
            rCboSubClientes.Visible = false;
            rLblSubClie.Visible = false;
        }
    }
    public void SubClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCveClie", DbType.String, 20, ParameterDirection.Input, rCboClientes.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboSubClientes, ds, "cliCveSubClie", "clieNom", true, false, "");
        ((Literal)rCboSubClientes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSubClientes.Items.Count);
        rCboSubClientes.Enabled = true;

    }



    protected void rCboMoneda_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
    }

    protected void rCboSubClientes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
    }

    protected void rCboDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        doc.Attributes["src"] = "";
    }

    protected void RelaDocsGlobal_CheckedChanged(object sender, EventArgs e)
    {
        doc.Attributes["src"] = "";

    }

    protected void RelaDocsDetalle_CheckedChanged(object sender, EventArgs e)
    {
        doc.Attributes["src"] = "";
    }
}