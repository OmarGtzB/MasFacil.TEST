using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;

public partial class DC_RegOperacionesDet_MovIN : System.Web.UI.Page
{

    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_MonedaParaLog;


    #endregion

    #region Eventos
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
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }
    protected void rCboMoneda_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ParametroMoneda();
        if (rCboMoneda.SelectedValue == Pag_MonedaParaLog)
        {
            radTxtTipoCambio.Text = ParametroMonedaVal();
        }
        else
        {
            radTxtTipoCambio.Text = ObtieneTipodeCambio();
        }

    }
    public string ParametroMonedaVal()
    {
        string ValorParametroMoneda;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "TIPOCAMBIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 10, ParameterDirection.Input, 2);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count != 0)
        {
            return ValorParametroMoneda = ds.Tables[0].Rows[0]["parmValDec"].ToString();
        }
        else
        {
            return ValorParametroMoneda = "0";
        }
    }
    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            if (hdfBtnAccionMov.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccionMov.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                RecuperaValoresControlls();
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }
    public void RecuperaValoresControlls()
    {
        if (hdfBtnAccionMov.Value == "1")
        {
            NuevoMovimiento();
        }
        if (hdfBtnAccionMov.Value == "2")
        {
            ModificaDataSet();
        }

    }
    private void NuevoMovimiento()
    {
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsMovDetSession"];
        DataRow dr = dsMovDet.Tables[0].NewRow();
 
        int maxSec = 1;
        int maxMovDetID = 1;

        if (dsMovDet.Tables[0].Rows.Count > 0)
        {
            maxSec = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(movSec)", "")) + 1;
            maxMovDetID = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(movID)", "")) + 1;
        }

        dr["movID"] = maxMovDetID;
        dr["movSec"] = maxSec;


        //MOVIMIENTO
        if (MovimientoCargo.Checked == true)
        {
            dr["movCoADes"] = "Cargo";
            dr["movCoA"] = "1";
        }
        if (MovimientoAbono.Checked == true)
        {
            dr["movCoADes"] = "Abono";
            dr["movCoA"] = "2";
        }

        //tipo de aplicacion
        if (AplicaSi.Checked == true)
        {
            dr["movTipApliADes"] = "Si";
            dr["movTipApli"] = "1";
        }
        if (AplicaNo.Checked == true)
        {
            dr["movTipApliADes"] = "No";
            dr["movTipApli"] = "2";
        }
 
        //Articulo
        if (rCboArticulo.SelectedIndex != -1)
        {
            dr["movRef20_Art"] = rCboArticulo.SelectedValue;
            dr["artDes"] = rCboArticulo.Text;
        }
        //Almacen
        if (rCboAlmacen.SelectedIndex != -1)
        {
            dr["movRef10_Alm"] = rCboAlmacen.SelectedValue;
            dr["almDes"] = rCboAlmacen.Text;
        }
        //Unidad de Medida
        if (rCboUnidadMedida.SelectedIndex != -1)
        {
            dr["movRef10_UniMed"] = rCboUnidadMedida.SelectedValue;
            dr["uniMed_Des"] = rCboAlmacen.Text;
        }
        //Cantidad
        if (radTxtCantidad.Text.Trim() != "")
        {
            dr["movFac_Cant"] = radTxtCantidad.Text;
        }
        //Costo
        if (radTxtCantidad.Text.Trim() != "")
        {
            dr["movFac_Costo"] = radTxtCosto.Text;
        }
        //Precio
        if (radTxtPrecio.Text.Trim() != "")
        {
            dr["movFac_Prec"] = radTxtPrecio.Text;
        }
        //Importe
        if (radTxtImporte.Text.Trim() != "")
        {
            dr["movImp_Imp"] = radTxtImporte.Text;
        }
        //Lote
        if (rTxtLote.Text.Trim() != "")
        {
            dr["movRef20_Lote"] = rTxtLote.Text;
        }
        //Serie
        if (rTxtSerie.Text.Trim() != "")
        {
            dr["movRef20_Serie"] = rTxtSerie.Text;
        }
        //Moneda
        if (rCboMoneda.SelectedIndex != -1)
        {
            dr["monCve"] = rCboMoneda.SelectedValue;
            dr["monDes"] = rCboMoneda.Text;
        }
        //Tipo de Cambio
        if (radTxtTipoCambio.Text.Trim() != "")
        {
            dr["movFac_TipCam"] = radTxtTipoCambio.Text;
        }
        //Centro de Contos
        if (rTxtCentroCostos.Text.Trim() != "")
        {
            dr["movRef10_CC"] = rTxtCentroCostos.Text;
        }
        //Orden de Compra
        if (rTxtOrdenCompra.Text.Trim() != "")
        {
            dr["movRef10_OrdComp"] = rTxtOrdenCompra.Text;
        }
        //Proveedor
        if (rCboProveedor.SelectedIndex != -1)
        {
            dr["movRef10_Prov"] = rCboProveedor.SelectedValue;
        }
        
     ////////////
        //ADUANA
        if (rTxtAduana.Text.Trim() != "")
        {
            dr["movRef20Adu"] = rTxtAduana.Text;
        }
        //ALMACEN CONTRA
        if (rCboAlmContra.SelectedIndex != -1)
        {
            dr["movRef10AlmContra"] = rCboAlmContra.SelectedValue;
            dr["almContraDes"] = rCboAlmContra.Text;
        }
     ///////////

        //Fecha Movimiento
        if (ObtenerFecha(RdDatePckrFecha) == "1/01/01")
        {
            dr["movFec_Mov"] = DBNull.Value;
        }
        else
        {
            dr["movFec_Mov"] = RdDatePckrFecha.SelectedDate;
        }
        //Fecha 02
        if (ObtenerFecha(RdDatePckrFecha02) == "1/01/01")
        {
            dr["movFec_02"] = DBNull.Value;
        }
        else
        {
            dr["movFec_02"] = RdDatePckrFecha.SelectedDate;
        }

        //Referencia 1
        if (rTxtReferencia1.Text.Trim() != "")
        {
            dr["movRef10_01"] = rTxtReferencia1.Text;
        }
        else
        {
            dr["movRef10_01"] = System.DBNull.Value;
        }
        //Referencia 2
        if (rTxtReferencia2.Text.Trim() != "")
        {
            dr["movRef10_02"] = rTxtReferencia2.Text;
        }
        else
        {
            dr["movRef10_02"] = System.DBNull.Value;
        }
        //Referencia 3
        if (rTxtReferencia3.Text.Trim() != "")
        {
            dr["movRef10_03"] = rTxtReferencia3.Text;
        }
        else
        {
            dr["movRef10_03"] = System.DBNull.Value;
        }
        //Referencia 4
        if (rTxtReferencia4.Text.Trim() != "")
        {
            dr["movRef10_04"] = rTxtReferencia4.Text;
        }
        else
        {
            dr["movRef10_04"] = System.DBNull.Value;
        }

        //ALMACEN CONTRA
        if (rCboAlmContra.SelectedIndex != -1)
        {
            dr["movRef10AlmContra"] = rCboAlmContra.SelectedValue;
            dr["almContraDes"] = rCboAlmContra.Text;
        }

        //ADUANA
        if (rTxtAduana.Text.Trim() != "")
        {
            dr["movRef20Adu"] = rTxtAduana.Text;
        }
        else
        {
            dr["movRef20Adu"] = System.DBNull.Value;
        }



        if (Session["FolioSession"].ToString() != "")
        {
            dr["movFolio"] = Session["FolioSession"].ToString();
        }

        dsMovDet.Tables[0].Rows.Add(dr);
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }




    private void ModificaDataSet()
    {
        string Pag_sMovDetId = Session["movIDSession"].ToString();
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        DataRow[] drMovDet;
        drMovDet = dsMovDet.Tables[0].Select("movID = " + Pag_sMovDetId);
        
        if (MovimientoCargo.Checked == true)
        {
            drMovDet[0]["movCoADes"] = "Cargo";
            drMovDet[0]["movCoA"] = "1";
        }
        if (MovimientoAbono.Checked == true)
        {
            drMovDet[0]["movCoADes"] = "Abono";
            drMovDet[0]["movCoA"] = "2";
        }

        //tipo de aplicacion
        if (AplicaSi.Checked == true)
        {
            drMovDet[0]["movTipApliADes"] = "Si";
            drMovDet[0]["movTipApli"] = "1";
        }
        if (AplicaNo.Checked == true)
        {
            drMovDet[0]["movTipApliADes"] = "No";
            drMovDet[0]["movTipApli"] = "2";
        }


        //--------------------------------
        //Articulo
        if (rCboArticulo.SelectedIndex != -1)
        {
            drMovDet[0]["movRef20_Art"] = rCboArticulo.SelectedValue;
            drMovDet[0]["artDes"] = rCboArticulo.Text;
        }
        else
        {
            drMovDet[0]["movRef20_Art"] = "";
            drMovDet[0]["artDes"] = "";
        }
        //Almacen
        if (rCboAlmacen.SelectedIndex != -1)
        {
            drMovDet[0]["movRef10_Alm"] = rCboAlmacen.SelectedValue;
            drMovDet[0]["almDes"] = rCboAlmacen.Text;
        }
        else
        {
            drMovDet[0]["movRef10_Alm"] = "";
            drMovDet[0]["almDes"] = "";
        }
        //Unidad de Medida
        if (rCboUnidadMedida.SelectedIndex != -1)
        {
            drMovDet[0]["movRef10_UniMed"] = rCboUnidadMedida.SelectedValue;
            drMovDet[0]["uniMed_Des"] = rCboAlmacen.Text;
        }
        else
        {
            drMovDet[0]["movRef10_UniMed"] = "";
            drMovDet[0]["uniMed_Des"] = "";
        }
        //Cantidad
        if (radTxtCantidad.Text.Trim() != "")
        {
            drMovDet[0]["movFac_Cant"] = radTxtCantidad.Text;
        }
        else {
            drMovDet[0]["movFac_Cant"] = System.DBNull.Value;
        }
        //Costo
        if (radTxtCosto.Text.Trim() != "")
        {
            drMovDet[0]["movFac_Costo"] = radTxtCosto.Text;
        }
        else
        {
            drMovDet[0]["movFac_Costo"] = System.DBNull.Value;
        }
        //Precio
        if (radTxtPrecio.Text.Trim() != "")
        {
            drMovDet[0]["movFac_Prec"] = radTxtPrecio.Text;
        }
        else {
            drMovDet[0]["movFac_Prec"] = System.DBNull.Value;
        }
        //Importe
        if (radTxtImporte.Text.Trim() != "")
        {
            drMovDet[0]["movImp_Imp"] = radTxtImporte.Text;
        }
        else {
            drMovDet[0]["movImp_Imp"] = System.DBNull.Value;
        }
        //Lote
        if (rTxtLote.Text.Trim() != "")
        {
            drMovDet[0]["movRef20_Lote"] = rTxtLote.Text;
        }
        else {
            drMovDet[0]["movRef20_Lote"] = System.DBNull.Value;
        }
        //Serie
        if (rTxtSerie.Text.Trim() != "")
        {
            drMovDet[0]["movRef20_Serie"] = rTxtSerie.Text;
        }
        else
        {
            drMovDet[0]["movRef20_Serie"] = System.DBNull.Value;
        }
        //Moneda
        if (rCboMoneda.SelectedIndex != -1)
        {
            drMovDet[0]["monCve"] = rCboMoneda.SelectedValue;
            drMovDet[0]["monDes"] = rCboMoneda.Text;
        }
        else
        {
            drMovDet[0]["monCve"] = System.DBNull.Value;
            drMovDet[0]["monDes"] = "";
        }
        //Tipo de Cambio
        if (radTxtTipoCambio.Text.Trim() != "")
        {
            drMovDet[0]["movFac_TipCam"] = radTxtTipoCambio.Text;
        }
        else
        {
            drMovDet[0]["movFac_TipCam"] = System.DBNull.Value;
        }
        //Centro de Contos
        if (rTxtCentroCostos.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_CC"] = rTxtCentroCostos.Text;
        }
        else
        {
            drMovDet[0]["movRef10_CC"] = System.DBNull.Value;
        }
        //Orden de Compra
        if (rTxtOrdenCompra.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_OrdComp"] = rTxtOrdenCompra.Text;
        }
        else
        {
            drMovDet[0]["movRef10_OrdComp"] = System.DBNull.Value;
        }
        //Proveedor
        if (rCboProveedor.SelectedIndex != -1)
        {
            drMovDet[0]["movRef10_Prov"] = rCboProveedor.SelectedValue;
        }
        else
        {
            drMovDet[0]["movRef10_Prov"] = System.DBNull.Value;
        }
        //Fecha Movimiento
        if (ObtenerFecha(RdDatePckrFecha) == "1/01/01")
        {
            drMovDet[0]["movFec_Mov"] = DBNull.Value;
        }
        else
        {
            drMovDet[0]["movFec_Mov"] = RdDatePckrFecha.SelectedDate;
        }
        //Fecha 02
        if (ObtenerFecha(RdDatePckrFecha02) == "1/01/01")
        {
            drMovDet[0]["movFec_02"] = DBNull.Value;
        }
        else
        {
            drMovDet[0]["movFec_02"] = RdDatePckrFecha.SelectedDate;
        }
        //Referencia 1
        if (rTxtReferencia1.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_01"] = rTxtReferencia1.Text;
        }
        else
        {
            drMovDet[0]["movRef10_01"] = System.DBNull.Value;
        }
        //Referencia 2
        if (rTxtReferencia2.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_02"] = rTxtReferencia2.Text;
        }
        else
        {
            drMovDet[0]["movRef10_02"] = System.DBNull.Value;
        }
        //Referencia 3
        if (rTxtReferencia3.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_03"] = rTxtReferencia3.Text;
        }
        else
        {
            drMovDet[0]["movRef10_03"] = System.DBNull.Value;
        }
        //Referencia 4
        if (rTxtReferencia4.Text.Trim() != "")
        {
            drMovDet[0]["movRef10_04"] = rTxtReferencia4.Text;
        }
        else
        {
            drMovDet[0]["movRef10_04"] = System.DBNull.Value;
        }

        //ALMACEN CONTRA
        if (rCboAlmContra.SelectedIndex != -1)
        {
            drMovDet[0]["movRef10AlmContra"] = rCboAlmContra.SelectedValue;
            drMovDet[0]["almContraDes"] = rCboAlmContra.Text;
        }

        //ADUANA
        if (rTxtAduana.Text.Trim() != "")
        {
            drMovDet[0]["movRef20Adu"] = rTxtAduana.Text;
        }
        else
        {
            drMovDet[0]["movRef20Adu"] = System.DBNull.Value;
        }


        //--------------------------------
        dsMovDet.Tables[0].AcceptChanges();
        Session["dsMovDetSession"] = dsMovDet;

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }




    public string ObtenerFecha( RadDatePicker obj)
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(obj.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO and MODIFICAR
        if (hdfBtnAccionMov.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccionMov.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rCboArticulo.SelectedValue.Trim() == "")
            {
                rCboArticulo.CssClass = "cssTxtInvalid";
                rCboArticulo.BorderWidth = Unit.Pixel(1);
                rCboArticulo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboArticulo.BorderColor = System.Drawing.Color.Transparent; }

            //if (radtxt_principal.Text.Trim() == "")
            //{
            //    radtxt_principal.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { radtxt_principal.CssClass = "cssTxtEnabled"; }

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

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        //folio_Selection = Convert.ToString(Session["folio_Selection"]);
        //PagLoc_Cpto = Convert.ToString(Session["folio_Selection"]);
        //Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void InicioPagina()
    {
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);

        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulo, true, false);

        FnCtlsFillIn.RadComboBox_UnidadesMedida(Pag_sConexionLog, Pag_sCompania, ref rCboUnidadMedida, true, false);

        FnCtlsFillIn.RabComboBox_Almacen(Pag_sConexionLog, Pag_sCompania, ref rCboAlmacen, true, false);
        FnCtlsFillIn.RabComboBox_Proveedores(Pag_sConexionLog, Pag_sCompania, ref rCboProveedor, true, false);

        FnCtlsFillIn.RabComboBox_Almacen(Pag_sConexionLog, Pag_sCompania, ref rCboAlmContra, true, false);

        hdfBtnAccionMov.Value = Session[("hdfBtnAccionMov")].ToString();
        if (hdfBtnAccionMov.Value == "2")
        {
            ModificarMovimiento();
        }
    }


    #endregion

    #region FUNCIONES
    public void ParametroMoneda()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "TIPOCAMBIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 10, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds.Tables[0].Rows.Count != 0)
        {
            Pag_MonedaParaLog = ds.Tables[0].Rows[0]["parmValStr"].ToString();
        }
    }

    public string ObtieneTipodeCambio()
    {
        string ValorParametroMoneda;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MonedaTipoCambio";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count != 0)
        {
            return ValorParametroMoneda = ds.Tables[0].Rows[0]["monTCC"].ToString();
        }
        else
        {
            return ValorParametroMoneda = "0";
        }
    }


    private void ModificarMovimiento()
    {
        string Pag_sMovDetId = Session["movIDSession"].ToString();
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        DataRow[] drMovDet;
        drMovDet = dsMovDet.Tables[0].Select("movID = " + Pag_sMovDetId);

        //Movimiento
        if (drMovDet[0]["movCoADes"].ToString() == "Cargo")
        {
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
        }
        if (drMovDet[0]["movCoADes"].ToString() == "Abono")
        {
            MovimientoCargo.Checked = false;
            MovimientoAbono.Checked = true;
        }

        //Aplica O.C
        if (drMovDet[0]["movTipApliADes"].ToString() == "Si")
        {
            AplicaSi.Checked = true;
            AplicaNo.Checked = false;
        }
        if (drMovDet[0]["movTipApliADes"].ToString() == "No")
        {
            AplicaSi.Checked = false;
            AplicaNo.Checked = true;
        }
 
        //Articulo
        rCboArticulo.SelectedValue = drMovDet[0]["movRef20_Art"].ToString();
        //Almacen
        rCboAlmacen.SelectedValue = drMovDet[0]["movRef10_Alm"].ToString();
        //Unidad de Medida
        rCboUnidadMedida.SelectedValue = drMovDet[0]["movRef10_UniMed"].ToString();
        //Cantidad
        if (drMovDet[0]["movFac_Cant"].ToString() != "")
        {
            radTxtCantidad.Text = drMovDet[0]["movFac_Cant"].ToString();
        }
        else
        {
            radTxtCantidad.Text = "";
        }
        //Costo
        if (drMovDet[0]["movFac_Costo"].ToString() != "")
        {
            radTxtCosto.Text = drMovDet[0]["movFac_Costo"].ToString();
        }
        else
        {
            radTxtCosto.Text = "";
        }
        //Precio
        if (drMovDet[0]["movFac_Prec"].ToString() != "")
        {
            radTxtPrecio.Text = drMovDet[0]["movFac_Prec"].ToString();
        }
        else
        {
            radTxtPrecio.Text = "";
        }
        //Importe
        if (drMovDet[0]["movImp_Imp"].ToString() != "")
        {
            radTxtImporte.Text = drMovDet[0]["movImp_Imp"].ToString();
        }
        else
        {
            radTxtImporte.Text = "";
        }
        //Lote
        if (drMovDet[0]["movRef20_Lote"].ToString() != "")
        {
            rTxtLote.Text = drMovDet[0]["movRef20_Lote"].ToString();
        }
        else
        {
            rTxtLote.Text = "";
        }
        //Serie
        if (drMovDet[0]["movRef20_Serie"].ToString() != "")
        {
            rTxtSerie.Text = drMovDet[0]["movRef20_Serie"].ToString();
        }
        else
        {
            rTxtSerie.Text = "";
        }
        //Moneda
        rCboMoneda.SelectedValue = drMovDet[0]["monCve"].ToString();
        //Tipo de Cambio
        if (drMovDet[0]["movFac_TipCam"].ToString() != "")
        {
            radTxtTipoCambio.Text = drMovDet[0]["movFac_TipCam"].ToString();
        }
        else
        {
            radTxtTipoCambio.Text = "";
        }
        //Centro de Contos
        if (drMovDet[0]["movRef10_CC"].ToString() != "")
        {
            rTxtCentroCostos.Text = drMovDet[0]["movRef10_CC"].ToString();
        }
        else
        {
            rTxtCentroCostos.Text = "";
        }
        //Orden de Compra
        if (drMovDet[0]["movRef10_OrdComp"].ToString() != "")
        {
            rTxtOrdenCompra.Text = drMovDet[0]["movRef10_OrdComp"].ToString();
        }
        else
        {
            rTxtOrdenCompra.Text = "";
        }
        //Proveedor
        rCboProveedor.SelectedValue = drMovDet[0]["movRef10_Prov"].ToString();

        //Fecha Movimiento
        if (drMovDet[0]["movFec_Mov"].ToString() == "")
        {
            drMovDet[0]["movFec_Mov"] = System.DBNull.Value;
        }
        else
        {
            RdDatePckrFecha.SelectedDate = Convert.ToDateTime(drMovDet[0]["movFec_Mov"].ToString());
        }

        //Fecha 2
        if (drMovDet[0]["movFec_02"].ToString() == "")
        {
            drMovDet[0]["movFec_02"] = System.DBNull.Value;
        }
        else
        {
            RdDatePckrFecha02.SelectedDate = Convert.ToDateTime(drMovDet[0]["movFec_02"].ToString());
        }

        if (drMovDet[0]["movRef10_01"].ToString() != "")
        {
            rTxtReferencia1.Text = drMovDet[0]["movRef10_01"].ToString();
        }
        else
        {
            rTxtReferencia1.Text = "";
        }

        if (drMovDet[0]["movRef10_02"].ToString() != "")
        {
            rTxtReferencia2.Text = drMovDet[0]["movRef10_02"].ToString();
        }
        else
        {
            rTxtReferencia2.Text = "";
        }

        if (drMovDet[0]["movRef10_03"].ToString() != "")
        {
            rTxtReferencia3.Text = drMovDet[0]["movRef10_03"].ToString();
        }
        else
        {
            rTxtReferencia3.Text = "";
        }

        if (drMovDet[0]["movRef10_04"].ToString() != "")
        {
            rTxtReferencia4.Text = drMovDet[0]["movRef10_04"].ToString();
        }
        else
        {
            rTxtReferencia4.Text = "";
        }

        //ALMACEN CONTRA
        if (drMovDet[0]["movRef10AlmContra"].ToString() != "")
        {
            rCboAlmContra.SelectedValue = drMovDet[0]["movRef10AlmContra"].ToString();
        }
        else
        {
            rCboAlmContra.ClearSelection();
        }

        //ADUANA
        if (drMovDet[0]["movRef20Adu"].ToString() != "")
        {
            rTxtAduana.Text = drMovDet[0]["movRef20Adu"].ToString();
        }
        else
        {
            rTxtAduana.Text = "";
        }




    }

    #endregion






}