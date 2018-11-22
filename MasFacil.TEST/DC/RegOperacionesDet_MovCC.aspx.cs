using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;


public partial class DC_RegOperacionesDet_MovCC : System.Web.UI.Page
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
    private static string Pag_MonedaParaLog;


    #endregion
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
        FnCtlsFillIn.RabComboBox_Clientes(Pag_sConexionLog, Pag_sCompania, ref rCboCuenta, true, false);

        ParametroMoneda();
        hdfBtnAccionMov.Value = Session[("hdfBtnAccionMov")].ToString();

        if (hdfBtnAccionMov.Value == "2")
        {
            ModificarMovimiento();
        }
    }


    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //EjecutaAccion();
        RecuperaValoresControlls();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    protected void rCboMoneda_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboMoneda.SelectedValue == Pag_MonedaParaLog)
        {
            RdNmrc_tipodcambio.Text = ParametroMonedaVal();
        }
        else
        {
            RdNmrc_tipodcambio.Text = ObtieneTipodeCambio();
        }
    }

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
        
        if (RdioBtnCargo.Checked == true)
        {
            dr["movCoADes"] = "Cargo";
            dr["movCoA"] = "1";
        }
        if (RdioBtnAbono.Checked == true)
        {
            dr["movCoADes"] = "Abono";
            dr["movCoA"] = "2";
        }

        if (TipoCtaDeposito.Checked == true)
        {
            dr["movTipApliADes"] = "Cta.Deposito";
            dr["movTipApli"] = "1";
        }
        if (TipoProveedor.Checked == true)
        {
            dr["movTipApliADes"] = "Cliente";
            dr["movTipApli"] = "2";
        }

        if (rCboCuenta.SelectedValue != "")
        {
            dr["movRef10_CodApli"] = rCboCuenta.SelectedValue;
        }

        if (radtxt_principal.Text != "")
        {
            dr["movRef10_Princ"] = radtxt_principal.Text;
        }

        if (radtxt_aplicacion.Text != "")
        {
            dr["movRef10_Apli"] = radtxt_aplicacion.Text;
        }

        if (radtxt_referencia3.Text != "")
        {
            dr["movRef10_03"] = radtxt_referencia3.Text;
        }

        if (radtxt_referencia4.Text != "")
        {
            dr["movRef10_04"] = radtxt_referencia4.Text;
        }


        if (ObtenerFechaMovimiento() == "1/01/01")
        {
            dr["movFec_Mov"] = DBNull.Value;
        }
        else
        {
            dr["movFec_Mov"] = RdDatePckrFecha_Movimiento.SelectedDate;
        }

        if (ObtenerFechaVencimiento() == "1/01/01")
        {
            dr["movFec_Venc"] = DBNull.Value;
        }
        else
        {
            dr["movFec_Venc"] = RdDatePckrFecha_Vencimiento.SelectedDate;
        }

        if (rCboMoneda.SelectedValue != "")
        {
            dr["monCve"] = rCboMoneda.SelectedValue;
            dr["monDes"] = rCboMoneda.Text;
        }

        if (RdNmrc_tipodcambio.Text != "")
        {
            dr["movFac_TipCam"] = RdNmrc_tipodcambio.Text;
        }

        if (RdNmrc_importe.Text != "")
        {
            dr["movImp_Imp"] = RdNmrc_importe.Text;
        }

        if (Rdtxt_descripcion.Text != "")
        {
            dr["movRef40_Des"] = Rdtxt_descripcion.Text;
        }

        if (Session["FolioSession"].ToString() != "")
        {
            dr["movFolio"] = Session["FolioSession"].ToString();
        }


        dsMovDet.Tables[0].Rows.Add(dr);
        dsMovDet.Tables[0].AcceptChanges();
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    
    private void ModificarMovimiento()
    {
        string Pag_sMovDetId = Session["movIDSession"].ToString();
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        DataRow[] drMovDet;
        drMovDet = dsMovDet.Tables[0].Select("movID = " + Pag_sMovDetId);


        if (drMovDet[0]["movCoADes"].ToString() == "Cargo")
        {
            RdioBtnCargo.Checked = true;
            RdioBtnAbono.Checked = false;
        }
        if (drMovDet[0]["movCoADes"].ToString() == "Abono")
        {
            RdioBtnCargo.Checked = false;
            RdioBtnAbono.Checked = true;
        }

        //tipo de aplicacion
        if (drMovDet[0]["movTipApliADes"].ToString() == "Cta.Deposito")
        {
            TipoCtaDeposito.Checked = true;
            TipoProveedor.Checked = false;
        }
        if (drMovDet[0]["movTipApliADes"].ToString() == "Proveedor")
        {
            TipoCtaDeposito.Checked = false;
            TipoProveedor.Checked = true;
        }

        rCboCuenta.SelectedValue = drMovDet[0]["movRef10_CodApli"].ToString();

        radtxt_principal.Text = drMovDet[0]["movRef10_Princ"].ToString();

        radtxt_aplicacion.Text = drMovDet[0]["movRef10_Apli"].ToString();

        radtxt_referencia3.Text = drMovDet[0]["movRef10_03"].ToString();

        radtxt_referencia4.Text = drMovDet[0]["movRef10_04"].ToString();

        if (drMovDet[0]["movFec_Mov"].ToString() == "")
        {
            drMovDet[0]["movFec_Mov"] = System.DBNull.Value;
        }
        else
        {
            RdDatePckrFecha_Movimiento.SelectedDate = Convert.ToDateTime(drMovDet[0]["movFec_Mov"].ToString());
        }

        if (drMovDet[0]["movFec_Venc"].ToString() == "")
        {
            drMovDet[0]["movFec_Venc"] = System.DBNull.Value;
        }
        else
        {
            RdDatePckrFecha_Vencimiento.SelectedDate = Convert.ToDateTime(drMovDet[0]["movFec_Venc"].ToString());
        }

        if (drMovDet[0]["movFac_TipCam"].ToString() == "0")
        {
            drMovDet[0]["movFac_TipCam"] = 0.00;
        }
        else
        {
            RdNmrc_tipodcambio.Text = drMovDet[0]["movFac_TipCam"].ToString();
        }

        if (drMovDet[0]["movImp_Imp"].ToString() == "0")
        {
            drMovDet[0]["movImp_Imp"] = 0.00;
        }
        else
        {
            RdNmrc_importe.Text = drMovDet[0]["movImp_Imp"].ToString();
        }

        if (drMovDet[0]["movRef40_Des"].ToString() == "")
        {
            drMovDet[0]["movRef40_Des"] = System.DBNull.Value; ;
        }
        else
        {
            Rdtxt_descripcion.Text = drMovDet[0]["movRef40_Des"].ToString();
        }

        if (drMovDet[0]["monCve"].ToString() != "")
        {
            rCboMoneda.SelectedValue = drMovDet[0]["monCve"].ToString();
        }
        else
        {
            rCboMoneda.ClearSelection();
        }
    }


    private void ModificaDataSet()
    {
        string Pag_sMovDetId = Session["movIDSession"].ToString();
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsMovDetSession"];

        DataRow[] drMovDet;
        drMovDet = dsMovDet.Tables[0].Select("movID = " + Pag_sMovDetId);




        if (RdioBtnCargo.Checked == true)
        {
            drMovDet[0]["movCoADes"] = "Cargo";
            drMovDet[0]["movCoA"] = "1";
        }
        if (RdioBtnAbono.Checked == true)
        {
            drMovDet[0]["movCoADes"] = "Abono";
            drMovDet[0]["movCoA"] = "2";
        }

        //tipo de aplicacion
        if (TipoCtaDeposito.Checked == true)
        {
            drMovDet[0]["movTipApliADes"] = "Cta.Deposito";
            drMovDet[0]["movTipApli"] = "1";
        }
        if (TipoProveedor.Checked == true)
        {
            drMovDet[0]["movTipApliADes"] = "Cliente";
            drMovDet[0]["movTipApli"] = "2";
        }

        if (rCboCuenta.SelectedValue != "")
        {
            drMovDet[0]["movRef10_CodApli"] = rCboCuenta.SelectedValue;
        }
        else
        {
            drMovDet[0]["movRef10_CodApli"] = "";
        }

        if (radtxt_principal.Text != "")
        {
            drMovDet[0]["movRef10_Princ"] = radtxt_principal.Text;
        }
        else
        {
            drMovDet[0]["movRef10_Princ"] = "";
        }

        if (radtxt_aplicacion.Text != "")
        {
            drMovDet[0]["movRef10_Apli"] = radtxt_aplicacion.Text;
        }
        else
        {
            drMovDet[0]["movRef10_Apli"] = "";
        }

        if (radtxt_referencia3.Text != "")
        {
            drMovDet[0]["movRef10_03"] = radtxt_referencia3.Text;
        }
        else
        {
            drMovDet[0]["movRef10_03"] = "";
        }

        if (radtxt_referencia4.Text != "")
        {
            drMovDet[0]["movRef10_04"] = radtxt_referencia4.Text;
        }
        else
        {
            drMovDet[0]["movRef10_04"] = "";
        }


        if (RdDatePckrFecha_Movimiento.SelectedDate != null)
        {
            drMovDet[0]["movFec_Mov"] = RdDatePckrFecha_Movimiento.SelectedDate;
        }
        else
        {
            drMovDet[0]["movFec_Mov"] = DBNull.Value;
        }


        if (RdDatePckrFecha_Vencimiento.SelectedDate != null)
        {
            drMovDet[0]["movFec_Venc"] = RdDatePckrFecha_Vencimiento.SelectedDate;
        }
        else
        {
            drMovDet[0]["movFec_Venc"] = DBNull.Value;
        }


        if (RdNmrc_tipodcambio.Text != "")
        {
            drMovDet[0]["movFac_TipCam"] = RdNmrc_tipodcambio.Text;
        }
        else
        {
            drMovDet[0]["movFac_TipCam"] = DBNull.Value;
        }




        if (RdNmrc_importe.Text != "")
        {
            drMovDet[0]["movImp_Imp"] = RdNmrc_importe.Text;
        }
        else
        {
            drMovDet[0]["movImp_Imp"] = DBNull.Value;
        }


        if (Rdtxt_descripcion.Text != "")
        {
            drMovDet[0]["movRef40_Des"] = Rdtxt_descripcion.Text;
        }
        else
        {
            drMovDet[0]["movRef40_Des"] = "";
        }

        if (rCboMoneda.SelectedValue != "")
        {
            drMovDet[0]["monCve"] = rCboMoneda.SelectedValue;
            drMovDet[0]["monDes"] = rCboMoneda.Text;
        }
        else
        {
            drMovDet[0]["monCve"] = "";
            drMovDet[0]["monDes"] = "";
        }


        dsMovDet.Tables[0].AcceptChanges();
        Session["dsMovDetSession"] = dsMovDet;

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    public string ObtenerFechaMovimiento()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDatePckrFecha_Movimiento.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    public string ObtenerFechaVencimiento()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDatePckrFecha_Vencimiento.SelectedDate);

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

            if (rCboCuenta.SelectedValue.Trim() == "")
            {
                rCboCuenta.CssClass = "cssTxtInvalid";
                rCboCuenta.BorderWidth = Unit.Pixel(1);
                rCboCuenta.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboCuenta.BorderColor = System.Drawing.Color.Transparent; }

            if (radtxt_principal.Text.Trim() == "")
            {
                radtxt_principal.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { radtxt_principal.CssClass = "cssTxtEnabled"; }

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
}