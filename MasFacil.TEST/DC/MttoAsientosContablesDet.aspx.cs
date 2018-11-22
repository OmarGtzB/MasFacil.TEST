using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;


public partial class DC_MttoAsientosContablesDet : System.Web.UI.Page
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
    private string Pag_sSessionLog;
    private string Pag_asiContDetId;
    private string Pag_asiContEncId;

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
        hdfBtnAccion.Value = Request.QueryString["hdf"];
        Pag_asiContDetId = Request.QueryString["AsienCont"];
    }
    private void InicioPagina()
    {
        FnCtlsFillIn.RadComboBox_CodificacionCuentas(Pag_sConexionLog, Pag_sCompania, ref rCboCodificacion, true, false);
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
        //ValorUrl();
        RdNumricImporte.Text = "0.0";
        if (hdfBtnAccion.Value == "2")
        {
            RdNumricImporte.Text = "";
            ModificarMovimiento();
        }
    }
    private void ModificarMovimiento()
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
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
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
        if (hdfBtnAccion.Value == "1")
        {
            NuevoMovimiento();
        }
        if (hdfBtnAccion.Value == "2")
        {
            ModificaDataSet();
        }
    }
    private void NuevoMovimiento()
    {
        Pag_asiContEncId = Request.QueryString["asiContEncId"];

        DataSet dsMovDet = new DataSet();
        dsMovDet = (DataSet)Session["dsAsientosSession"];
        DataRow dr = dsMovDet.Tables[0].NewRow();

        int maxSec = 1;
        int maxasiContDetId = 1;

        if (dsMovDet.Tables[0].Rows.Count > 0)
        {
            maxSec = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(polDetSec)", "")) + 1;
            maxasiContDetId = Convert.ToInt32(dsMovDet.Tables[0].Compute("max(asiContDetId)", "")) + 1;
        }

        dr["asiContDetId"] = maxasiContDetId;
        dr["asiContEncId"] = Pag_asiContEncId;
        dr["polDetSec"] = maxSec;

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
            dr["ctaContCveDes"] = rCboCodificacion.Text;
            dr["ctaContCve"] = rCboCodificacion.SelectedValue;
        }
        else
        {
            rCboCodificacion.ClearSelection();
            dr["ctaContCveDes"] = "";
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

        if (ObtenerFechaMovimiento() == "1/01/01")
        {
            dr["polDetFecMov"] = DBNull.Value;
        }
        else
        {
            dr["polDetFecMov"] = RadDateMovimiento.SelectedDate;
        }

        if (ObtenerFechaVencimiento() == "1/01/01")
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
    private void ModificaDataSet()
    {
        Pag_asiContDetId = Request.QueryString["AsienCont"];

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

        if (ObtenerFechaMovimiento() == "1/01/01")
        {
            drMovDet[0]["polDetFecMov"] = DBNull.Value;
        }
        else
        {
            drMovDet[0]["polDetFecMov"] = RadDateMovimiento.SelectedDate;
        }

        if (ObtenerFechaVencimiento() == "1/01/01")
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
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
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
            return sResult;
        }

        return sResult;
    }
    public string ObtenerFechaMovimiento()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RadDateMovimiento.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }
    public string ObtenerFechaVencimiento()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RadDateVencimiento.SelectedDate);

        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    #endregion








}