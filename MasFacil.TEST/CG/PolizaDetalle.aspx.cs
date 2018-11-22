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

public partial class CG_PolizaDetalle : System.Web.UI.Page
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

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_smaUser;

    private string Pag_asiContEncId;
    private string Pag_asiContDetId; 

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

    //=====> EVENTOS CONTROLES

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    
    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_smaUser = LM.sValSess(this.Page, 1);

        Pag_asiContEncId = "";
        if (Request.QueryString["asiContEncId"] != null && Request.QueryString["asiContEncId"] != "")
        {
            Pag_asiContEncId = Request.QueryString["asiContEncId"];
        }
        Pag_asiContDetId = "";
        if (Request.QueryString["asiContDetId"] != null && Request.QueryString["asiContDetId"] != "")
        {
            Pag_asiContDetId = Request.QueryString["asiContDetId"];
        }
    }
    public void InicioPagina()
    {
        FnCtlsFillIn.RadComboBox_CodificacionCuentas(Pag_sConexionLog, Pag_sCompania, ref rCboCodificacion, true, false);
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
        RdNumricImporte.Text = "0.0";

        if (Request.QueryString["hdfBtnAccionDet"] != null && Request.QueryString["hdfBtnAccionDet"] != "")
        {
            hdfBtnAccionDet.Value = Request.QueryString["hdfBtnAccionDet"];
        }


        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            ModificarAsientoContableDetalle();
        }

    }
    private void ModificarAsientoContableDetalle()
    {
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsAsientosSession"];

        DataRow[] drMovDet;
        drMovDet = dsMovDet.Tables[0].Select("asiContDetId = " + Pag_asiContDetId);


        if (drMovDet[0]["polDetCoA"].ToString() == "1")
        {
            MoviCargo.Checked = true;
            MoviAbono.Checked = false;
        }
        if (drMovDet[0]["polDetCoA"].ToString() == "2")
        {
            MoviCargo.Checked = false;
            MoviAbono.Checked = true;
        }

        //tipo de aplicacion

        if (drMovDet[0]["ctaContCve"].ToString() != "")
        {
            rCboCodificacion.SelectedValue = drMovDet[0]["ctaContCve"].ToString();
        }
        else
        {
            rCboCodificacion.ClearSelection();
        }

        if (drMovDet[0]["polDetDes"].ToString() != "")
        {
            rTxtDescripcion.Text = drMovDet[0]["polDetDes"].ToString();
        }
        else
        {
            rTxtDescripcion.Text = "";
        }

        if (drMovDet[0]["polDetRef01"].ToString() != "")
        {
            rdtxtReferencia01.Text = drMovDet[0]["polDetRef01"].ToString();
        }
        else
        {
            rdtxtReferencia01.Text = "";
        }
        if (drMovDet[0]["polDetRef02"].ToString() != "")
        {
            rdtxtReferencia02.Text = drMovDet[0]["polDetRef02"].ToString();
        }
        else
        {
            rdtxtReferencia02.Text = "";
        }

        if (drMovDet[0]["polDetRef03"].ToString() != "")
        {
            rdtxtReferencia03.Text = drMovDet[0]["polDetRef03"].ToString();
        }
        else
        {
            rdtxtReferencia03.Text = "";
        }

        if (drMovDet[0]["polDetRef04"].ToString() != "")
        {
            rdtxtReferencia04.Text = drMovDet[0]["polDetRef04"].ToString();
        }
        else
        {
            rdtxtReferencia04.Text = "";
        }

        if (drMovDet[0]["polDetFecMov"].ToString() == "")
        {
            drMovDet[0]["polDetFecMov"] = System.DBNull.Value;
        }
        else
        {
            RadDateMovimiento.SelectedDate = Convert.ToDateTime(drMovDet[0]["polDetFecMov"].ToString());
        }

        if (drMovDet[0]["polDetFecVenc"].ToString() == "")
        {
            drMovDet[0]["polDetFecVenc"] = System.DBNull.Value;
        }
        else
        {
            RadDateVencimiento.SelectedDate = Convert.ToDateTime(drMovDet[0]["polDetFecVenc"].ToString());
        }


        if (drMovDet[0]["CCCve"].ToString() != "")
        {
            rdtxtCentroCosto.Text = drMovDet[0]["CCCve"].ToString();
        }
        else
        {
            rdtxtCentroCosto.Text = "";
        }

        if (drMovDet[0]["polDetTipCam"].ToString() == "0")
        {
            drMovDet[0]["polDetTipCam"] = 0.00;
        }
        else
        {
            rdtxtTipoCambio.Text = drMovDet[0]["polDetTipCam"].ToString();
        }

        if (drMovDet[0]["polDetImp"].ToString() == "0")
        {
            drMovDet[0]["polDetImp"] = 0.00;
        }
        else
        {
            RdNumricImporte.Text = drMovDet[0]["polDetImp"].ToString();
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

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
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
        if (hdfBtnAccionDet.Value == "1")
        {
            NuevoRegAContDet();
        }
        if (hdfBtnAccionDet .Value == "2")
        {
            ModifRegAContDet();
        }
    }
    private void NuevoRegAContDet()
    {
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsAsientosSession"];
        DataRow dr = dsMovDet.Tables[0].NewRow();

        int maxSec = 1;
        int maxDetID = 1;

        if (dsMovDet.Tables[0].Rows.Count > 0)
        {
            maxSec = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(polDetSec)", "")) + 1;
            maxDetID = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(asiContDetId)", "")) + 1;
        }



        if (Pag_asiContEncId != "")
        {
            dr["asiContEncId"] = Pag_asiContEncId;
        }


        dr["polDetSec"] = maxSec;
        dr["asiContDetId"] = maxDetID;


        //Movimiento
        if (MoviCargo.Checked == true)
        {
            dr["polDetCoADes"] = "Cargo";
            dr["polDetCoA"] = "1";
        }
        if (MoviAbono.Checked == true)
        {
            dr["polDetCoADes"] = "Abono";
            dr["polDetCoA"] = "2";
        }

        if (rCboCodificacion.SelectedValue != "")
        {
            
            dr["ctaContCve"] = rCboCodificacion.SelectedValue;
            dr["ctaContCveFotmat"] = FNGrales.sCtaContCveFotmat(Pag_sConexionLog, Pag_sCompania, rCboCodificacion.SelectedValue);
            dr["ctaContDes"] = rCboCodificacion.Text;
        }
        else
        {
            rCboCodificacion.ClearSelection();
            dr["ctaContDes"] = "";
            dr["ctaContCve"] = "";
        }

        if (rTxtDescripcion.Text != "")
        {
            dr["polDetDes"] = rTxtDescripcion.Text;
        }
        else
        {
            dr["polDetDes"] = "";
        }

        if (rdtxtReferencia01.Text != "")
        {
            dr["polDetRef01"] = rdtxtReferencia01.Text;
        }
        else
        {
            dr["polDetRef01"] = "";
        }

        if (rdtxtReferencia02.Text != "")
        {
            dr["polDetRef02"] = rdtxtReferencia02.Text;
        }
        else
        {
            dr["polDetRef02"] = "";
        }

        if (rdtxtReferencia03.Text != "")
        {
            dr["polDetRef03"] = rdtxtReferencia03.Text;
        }
        else
        {
            dr["polDetRef03"] = "";
        }

        if (rdtxtReferencia04.Text != "")
        {
            dr["polDetRef04"] = rdtxtReferencia04.Text;
        }
        else
        {
            dr["polDetRef04"] = "";
        }

        if (rdtxtCentroCosto.Text != "")
        {
            dr["CCCve"] = rdtxtCentroCosto.Text;
        }

        if (rdtxtTipoCambio.Text != "")
        {
            dr["polDetTipCam"] = rdtxtTipoCambio.Text;
        }

        if (RdNumricImporte.Text != "")
        {
            dr["polDetImp"] = RdNumricImporte.Text;
        }

        if (rCboMoneda.SelectedValue != "")
        {
            dr["monCve"] = rCboMoneda.SelectedValue;
            dr["monDes"] = rCboMoneda.Text;
        }

        if (ObtenerFechaMovimiento() == "")
        {
            dr["polDetFecMov"] = DBNull.Value;
        }
        else
        {
            dr["polDetFecMov"] = RadDateMovimiento.SelectedDate;
        }

        if (ObtenerFechaVencimiento() == "")
        {
            dr["polDetFecVenc"] = DBNull.Value;
        }
        else
        {
            dr["polDetFecVenc"] = RadDateVencimiento.SelectedDate;
        }


        dsMovDet.Tables[0].Rows.Add(dr);
        dsMovDet.Tables[0].AcceptChanges();
        dsMovDet = (DataSet)Session["dsAsientosSession"];

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }
    private void ModifRegAContDet()
    {
        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsAsientosSession"];

        DataRow[] drMovDet;

        drMovDet = dsMovDet.Tables[0].Select("asiContDetId = " + Pag_asiContDetId);

        if (MoviCargo.Checked == true)
        {
            drMovDet[0]["polDetCoADes"] = "Cargo";
            drMovDet[0]["polDetCoA"] = "1";
        }
        if (MoviAbono.Checked == true)
        {
            drMovDet[0]["polDetCoADes"] = "Abono";
            drMovDet[0]["polDetCoA"] = "2";
        }
        if (rCboCodificacion.SelectedValue != "")
        {       
            drMovDet[0]["ctaContCve"] = rCboCodificacion.SelectedValue;
            drMovDet[0]["ctaContCveFotmat"] = FNGrales.sCtaContCveFotmat(Pag_sConexionLog, Pag_sCompania, rCboCodificacion.SelectedValue);
            drMovDet[0]["ctaContDes"] = rCboCodificacion.Text;   
        }
  
        if (rTxtDescripcion.Text != "")
        {
            drMovDet[0]["polDetDes"] = rTxtDescripcion.Text;
        }
        else
        {
            drMovDet[0]["polDetDes"] = "";
        }

        if (rdtxtReferencia01.Text != "")
        {
            drMovDet[0]["polDetRef01"] = rdtxtReferencia01.Text;
        }
        else
        {
            drMovDet[0]["polDetRef01"] = "";
        }

        if (rdtxtReferencia02.Text != "")
        {
            drMovDet[0]["polDetRef02"] = rdtxtReferencia02.Text;
        }
        else
        {
            drMovDet[0]["polDetRef02"] = "";
        }


        if (rdtxtReferencia03.Text != "")
        {
            drMovDet[0]["polDetRef03"] = rdtxtReferencia03.Text;
        }
        else
        {
            drMovDet[0]["polDetRef03"] = "";
        }


        if (rdtxtReferencia04.Text != "")
        {
            drMovDet[0]["polDetRef04"] = rdtxtReferencia04.Text;
        }
        else
        {
            drMovDet[0]["polDetRef04"] = "";
        }

        if (rdtxtCentroCosto.Text != "")
        {
            drMovDet[0]["CCCve"] = rdtxtCentroCosto.Text;
        }
        else
        {
            drMovDet[0]["CCCve"] = "";
        }


        if (rdtxtTipoCambio.Text != "")
        {
            drMovDet[0]["polDetTipCam"] = rdtxtTipoCambio.Text;
        }
        else
        {
            drMovDet[0]["polDetTipCam"] = DBNull.Value;

        }

        if (RdNumricImporte.Text != "")
        {
            drMovDet[0]["polDetImp"] = RdNumricImporte.Text;
        }
        else
        {
            drMovDet[0]["polDetImp"] = DBNull.Value;
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

        if (ObtenerFechaMovimiento() == "")
        {
            drMovDet[0]["polDetFecMov"] = DBNull.Value;
        }
        else
        {
            drMovDet[0]["polDetFecMov"] = RadDateMovimiento.SelectedDate;
        }

        if (ObtenerFechaVencimiento() == "")
        {
            drMovDet[0]["polDetFecVenc"] = DBNull.Value;
        }
        else
        {
            drMovDet[0]["polDetFecVenc"] = RadDateVencimiento.SelectedDate;
        }

        dsMovDet.Tables[0].AcceptChanges();
        Session["dsAsientosSession"] = dsMovDet;

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO and MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rCboCodificacion.SelectedValue == "")
            {
                rCboCodificacion.CssClass = "cssTxtInvalid";
                rCboCodificacion.BorderWidth = Unit.Pixel(1);
                rCboCodificacion.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboCodificacion.BorderColor = System.Drawing.Color.Transparent; }

            if (rTxtDescripcion.Text == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            if (RdNumricImporte.Text == "")
            {
                RdNumricImporte.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdNumricImporte.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            else {
                if (RdNumricImporte.Text != "")
                {

                    decimal dImporte = decimal.Parse(RdNumricImporte.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
                    if (dImporte <= 0) {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1043", ref sMSGTip, ref sResult);
                    }
                }

            }
            return sResult;
        }

        return sResult;
    }
    public string ObtenerFechaMovimiento()
    {
        string Val_TransFec = RadDateMovimiento.SelectedDate.ToString();
        if (Val_TransFec != "")
        {
            DateTime dt = Convert.ToDateTime(Val_TransFec);
            Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        }
        return Val_TransFec;
    }
    public string ObtenerFechaVencimiento()
    {
        string Val_TransFec = RadDateVencimiento.SelectedDate.ToString();
        if (Val_TransFec != "")
        {
            DateTime dt = Convert.ToDateTime(Val_TransFec);
            Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        }
        return Val_TransFec;
    }
    #endregion

}