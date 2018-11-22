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

public partial class DC_MttoAsientosContables : System.Web.UI.Page
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
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();


    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_stransDetId;
    private string Pag_stransId;
    private string Pag_asiContEncId;
    private string Pag_cptoTip;

    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            addRadWin();
            ValorUrl();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rGdvAsientosContables.MasterTableView.ClearSelectedItems();
    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        RecuperaUrl();
        if (Pag_cptoTip == "AdmonCC")
        {
            Response.Redirect("~/CC/AdmonCobros.aspx");
        }
        else if (Pag_cptoTip == "AdmonXP")
        {
            Response.Redirect("~/XP/AdmonPagos.aspx");
        }
        else {
            Response.Redirect("RegOperaciones.aspx?cptoTip=" + Pag_cptoTip);
        }



    }

    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DataSet ValPeriodo = new DataSet();
        ValPeriodo = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, Convert.ToDateTime(RdDateFecha.SelectedDate));

        string maMSGTip = ValPeriodo.Tables[0].Rows[0]["maMSGTip"].ToString().Trim();
        string maMSGDes = ValPeriodo.Tables[0].Rows[0]["maMSGDes"].ToString().Trim();

        if (maMSGTip != "1")
        {
            RdDateFecha.Clear();
            ShowAlert("2", maMSGDes);
        }

    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];
        rGdvAsientosContables.DataBind();
        FnCtlsFillIn.RadGrid(ref rGdvAsientosContables, (DataSet)Session["dsAsientosSession"]);
        CargosAbono();
        hdfBtnAccion.Value = "";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);

        
    }

    public void InicioPagina()
    {
        TituloPagina();
        FnCtlsFillIn.RabComboBox_Modulos(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);
        rCboConcepto.Enabled = false;
        ValorUrl();
        asiContEncId();
        LlenaGrid();
        if (Pag_asiContEncId != null)
        {
            LlenaDatos();
        }
        
        //CargosAbono();
        rGdvAsientosContables.ClientSettings.Selecting.AllowRowSelect = false;
        rGdvAsientosContables.AllowMultiRowSelection = false;

        
    }

    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Edita Poliza", "PnlMPFormTituloApartado");
    }

    public void ValorUrl()
    {
        Pag_stransDetId = Request.QueryString["stransDetId"];
        Pag_stransId = Request.QueryString["stransDetId"];
    }

    public void asiContEncId()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int32, 0, ParameterDirection.Input, Pag_stransId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                Pag_asiContEncId = ds.Tables[0].Rows[0]["asiContEncId"].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();

        }

    }

    private void LlenaDatos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AsientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        
        ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int32, 0, ParameterDirection.Input, Pag_asiContEncId);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        RdBnryImagenSituacion.ImageUrl = ds.Tables[0].Rows[0]["polSit"].ToString();

        if (ds.Tables[0].Rows[0]["polCve"].ToString() != "")
        {
            rTxtAsientoContable.Text = ds.Tables[0].Rows[0]["polCve"].ToString();
        }
        else
        {
            rTxtAsientoContable.Text = "";
        }

        if (ds.Tables[0].Rows[0]["polDes"].ToString() != "")
        {
            rTxtDescripcion.Text = ds.Tables[0].Rows[0]["polDes"].ToString();
        }
        else
        {
            rTxtDescripcion.Text = "";
        }

        rCboConcepto.SelectedValue = ds.Tables[0].Rows[0]["cptoId"].ToString();


        //FECHA UPDATE DE POLIZA
        if (ds.Tables[0].Rows[0]["polFecMod"].ToString() != "")
        {
            RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["polFecMod"].ToString());
        }
        else
        {
            RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["polFec"].ToString());
        }


        //FECHA PARA EL CONTROL polFec
        if (ds.Tables[0].Rows[0]["polFec"].ToString() != "")
        {
            RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["polFec"].ToString());
        }
        else
        {
            RdDateFecha.Clear();
        }

        if (ds.Tables[0].Rows[0]["polFecReg"].ToString() != "")
        {
            polFecReg.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["polFecReg"].ToString());
        }
        else
        {
            polFecReg.Clear();
        }



        if (ds.Tables[0].Rows[0]["polSumCgo"].ToString() != "")
        {
            RdNumricCargos.Text = ds.Tables[0].Rows[0]["polSumCgo"].ToString();
        }
        else
        {
            RdNumricCargos.Text = "";
        }

        if (ds.Tables[0].Rows[0]["polSumAbo"].ToString() != "")
        {
            RdNumricAbonos.Text = ds.Tables[0].Rows[0]["polSumAbo"].ToString();
        }
        else
        {
            RdNumricAbonos.Text = "";
        }

        if (ds.Tables[0].Rows[0]["polCifCtrl"].ToString() != "")
        {
            RdNumricFiltraControl.Text = ds.Tables[0].Rows[0]["polCifCtrl"].ToString();
        }
        else
        {
            RdNumricFiltraControl.Text = "";
        }
        
    }


    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AsientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int32, 10, ParameterDirection.Input, Convert.ToInt32(Pag_asiContEncId));
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        Session["dsAsientosSession"] = ds;
        FnCtlsFillIn.RadGrid(ref rGdvAsientosContables, (DataSet)Session["dsAsientosSession"]);
    }

    

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);


        if (msgValidacion == "")
        {

            //Nuevo
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                asiContEncId();
                RadWindowEditaPoliza.NavigateUrl = "MttoAsientosContablesDet.aspx?hdf=" + hdfBtnAccion.Value + "&asiContEncId=" + Pag_asiContEncId;
                string script = "function f(){$find(\"" + RadWindowEditaPoliza.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //Opcion para mostrar la ventana
                var dataItem = rGdvAsientosContables.SelectedItems[0] as GridDataItem;
                string asiContDetId = dataItem.GetDataKeyValue("asiContDetId").ToString();

                RadWindowEditaPoliza.NavigateUrl = "MttoAsientosContablesDet.aspx?hdf=" + hdfBtnAccion.Value + "&AsienCont=" + asiContDetId;
                string script = "function f(){$find(\"" + RadWindowEditaPoliza.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            if (hdfBtnAccion.Value == "")
            {
                GuardarEncabezado();
                GuardaDetalle();
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EliminaMov();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }
    private void EliminaMov()
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];

        string stransDetId = "";
        var dataItem = rGdvAsientosContables.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //se le asigna el id del row seleccionado del grid
            stransDetId = dataItem.GetDataKeyValue("asiContDetId").ToString();
        }

        for (int i = 0; i < rGdvAsientosContables.Items.Count; i++)
        {
            if (rGdvAsientosContables.Items[i].Selected == true)
            {
                dsTransDet.Tables[0].Rows[i].Delete();

            }
        }
        dsTransDet.Tables[0].AcceptChanges();
        Session["dsAsientosSession"] = dsTransDet;
        FnCtlsFillIn.RadGrid(ref rGdvAsientosContables, (DataSet)Session["dsAsientosSession"]);

        hdfBtnAccion.Value = "";
        ControlesAccion();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        CargosAbono();
    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO and MODIFICAR
        if (hdfBtnAccion.Value == "")
        {

            string fecha = RdDateFecha.SelectedDate.ToString();
            if (fecha == "")
            {
                RdDateFecha.CssClass = "cssTxtInvalid";
                RdDateFecha.BorderWidth = Unit.Pixel(1);
                RdDateFecha.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { RdDateFecha.BorderColor = System.Drawing.Color.Transparent; }
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdvAsientosContables.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }
        
        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvAsientosContables.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }


    private void ControlesAccion()
    {
        ////===> CONTROLES GENERAL

        rGdvAsientosContables.MasterTableView.ClearSelectedItems();
        rGdvAsientosContables.ClientSettings.Selecting.AllowRowSelect = false;
        rGdvAsientosContables.AllowMultiRowSelection = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //===> CONTROLES POR ACCION
        //Nuevo
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rGdvAsientosContables.MasterTableView.ClearSelectedItems();
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
            rGdvAsientosContables.ClientSettings.Selecting.AllowRowSelect = false;
            rGdvAsientosContables.AllowMultiRowSelection = false;
        }
        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvAsientosContables.MasterTableView.ClearSelectedItems();
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            rGdvAsientosContables.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvAsientosContables.AllowMultiRowSelection = false;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            rGdvAsientosContables.MasterTableView.ClearSelectedItems();
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
            rGdvAsientosContables.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvAsientosContables.AllowMultiRowSelection = true;
        }

        
        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            rGdvAsientosContables.MasterTableView.ClearSelectedItems();

        }


        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString()
           )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
        else
        {
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }
    }
    
    private void GuardaDetalle()
    {
        int ValEliminar = 0;
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];

        DataSet ds = new DataSet();
        if (dsTransDet.Tables[0].Rows.Count == 0)
        {
            asiContEncId();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, Pag_asiContEncId);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, 1);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        }

        foreach (DataRow drConfCpto in dsTransDet.Tables[0].Rows)
        {
            ValEliminar += 1;

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);

            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["asiContEncId"]));
            ProcBD.AgregarParametrosProcedimiento("@polDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetSec"]));

            if (drConfCpto["ctaContCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["ctaContCve"]));
            }
            if (drConfCpto["polDetDes"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetDes", DbType.String, 50, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetDes"]));
            }
            if (drConfCpto["CCCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@CCCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["CCCve"]));
            }
            if (drConfCpto["polDetRef01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef01"]));
            }
            if (drConfCpto["polDetRef02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef02"]));
            }
            if (drConfCpto["polDetRef03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef03"]));
            }
            if (drConfCpto["polDetRef04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef04"]));
            }

            //FECHAS
            if (drConfCpto["polDetFecMov"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec_01;
                date1 = Convert.ToDateTime(drConfCpto["polDetFecMov"]);
                DetFec_01 = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@polDetFecMov", DbType.String, 100, ParameterDirection.Input, DetFec_01.Substring(0, 10));
            }

            if (drConfCpto["polDetFecVenc"].ToString() != "")
            {
                DateTime date2 = new DateTime();
                string DetFec_02;
                date2 = Convert.ToDateTime(drConfCpto["polDetFecVenc"]);
                DetFec_02 = date2.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@polDetFecVenc", DbType.String, 100, ParameterDirection.Input, DetFec_02.Substring(0, 10));
            }

            ProcBD.AgregarParametrosProcedimiento("@polDetCoA", DbType.Int32, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetCoA"]));

            if (drConfCpto["polDetImp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetImp", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["polDetImp"]));
            }
            if (drConfCpto["monCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            }
            if (drConfCpto["polDetTipCam"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetTipCam", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetTipCam"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@polDetSit", DbType.String, 3, ParameterDirection.Input, "R");

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                if (sEjecEstatus == "1")
                {
                    InicioPagina();
                }
            }

        }



    }

    private void GuardarEncabezado()
    {
        string maUser = LM.sValSess(this.Page, 1);
        asiContEncId();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AsientoContable";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int32, 0, ParameterDirection.Input, Pag_asiContEncId);
        ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, rTxtAsientoContable.Text);
        ProcBD.AgregarParametrosProcedimiento("@polDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int32, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@polFec", DbType.String, 100, ParameterDirection.Input, ObtenerFecha());

        //
        if (RdNumricCargos.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@polSumCgo", DbType.Decimal, 19, ParameterDirection.Input, RdNumricCargos.Text);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@polSumCgo", DbType.Decimal, 19, ParameterDirection.Input, 0.0);
        }

        if (RdNumricAbonos.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@polSumAbo", DbType.Decimal, 19, ParameterDirection.Input, RdNumricAbonos.Text);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@polSumAbo", DbType.Decimal, 19, ParameterDirection.Input, 0.0);
        }

        if (RdNumricFiltraControl.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@polCifCtrl", DbType.Decimal, 19, ParameterDirection.Input, RdNumricFiltraControl.Text);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@polCifCtrl", DbType.Decimal, 19, ParameterDirection.Input, 0.0);
        }
        ProcBD.AgregarParametrosProcedimiento("@maUsuCveReg", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@polFecReg", DbType.String, 100, ParameterDirection.Input, polFecRegDate());
        ProcBD.AgregarParametrosProcedimiento("@maUsuCveMod", DbType.String, 20, ParameterDirection.Input, maUser);
        ProcBD.AgregarParametrosProcedimiento("@polFecMod", DbType.String, 100, ParameterDirection.Input, ObtenerFecha());
        ProcBD.AgregarParametrosProcedimiento("@polSit", DbType.String, 3, ParameterDirection.Input, "R");
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            //if (sEjecEstatus == "1")
            //{
            //    InicioPagina();
            //}
            ShowAlert(sEjecEstatus, sEjecMSG);
        }

    }





    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }




    public string polFecRegDate()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(polFecReg.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    private void CargosAbono()
    {

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];

        DataSet ds = new DataSet();

        decimal cargo = 0, abono = 0;


        foreach (DataRow drConfCpto in dsTransDet.Tables[0].Rows)
        {

            if (drConfCpto["polDetCoaDes"].ToString() == "Cargo")
            {
                cargo = cargo + Convert.ToDecimal(drConfCpto["polDetImp"]);
            }
            if (drConfCpto["polDetCoaDes"].ToString() == "Abono")
            {
                abono = abono + Convert.ToDecimal(drConfCpto["polDetImp"]);
            }
        }

        RdNumricCargos.Text = cargo.ToString();
        RdNumricAbonos.Text = abono.ToString();

    }


    private void validaPeriodo()
    {
        try
        {
            string Val_TransFec = "";
            DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);
            Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Periodos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@RegDocFec", DbType.String, 100, ParameterDirection.Input, Val_TransFec + " 00:00:00");
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sit = "";

                sit = ds.Tables[0].Rows[0]["perSit"].ToString();

                if (sit == "1")
                {

                }
                else if (sit == "2")
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1012", ref sEjecEstatus, ref sEjecMSG);
                    ShowAlert(sEjecEstatus, sEjecMSG);
                    RdDateFecha.Clear();
                }
            }
            else
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1010", ref sEjecEstatus, ref sEjecMSG);
                ShowAlert(sEjecEstatus, sEjecMSG);
                RdDateFecha.Clear();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            throw;
        }
    }

    public void RecuperaUrl()
    {
        if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        {
            Pag_cptoTip = Request.QueryString["cptoTip"];
        }
        else
        {
            Pag_cptoTip = "0";
        }
    }
    #endregion










}