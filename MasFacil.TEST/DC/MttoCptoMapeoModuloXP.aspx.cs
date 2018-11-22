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
public partial class DC_MttoCptoMapeoModuloXP : System.Web.UI.Page
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

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string folio_Selection;
    private string PagLoc_Cpto;

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
        folio_Selection = Convert.ToString(Session["folio_Selection"]);
        PagLoc_Cpto = Convert.ToString(Session["folio_Selection"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void InicioPagina()
    {
       
        hdfBtnAccion.Value = "";
        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        DesHabilBtn(false);
        LimpiaBtn();
        if (PagLoc_Cpto != "")
        {
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboSecuenciaCodigo, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferenciasPrincipal, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferenciasAplicacion, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia3, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia4, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fec", ref rCboMovimiento, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fec", ref rCboVencimiento, true, false);
            FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fact", ref rCboTipCambio, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Imp", ref rCboImporte, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str40", ref rCboDescripcion, true, false);
            DescripcionCpto();
            llena_Grid();
        }
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_MapeoModulo.AllowMultiRowSelection = true;

        PermisoBotones();
    }

    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(Page, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }

    public void LimpiaBtn()
    {

        rTxtSecuencia.Text = "";
        rCboSecuenciaCodigo.ClearSelection();
        rCboMoneda.ClearSelection();
        rCboReferenciasPrincipal.ClearSelection();
        rCboReferenciasAplicacion.ClearSelection();
        rCboReferencia3.ClearSelection();
        rCboReferencia4.ClearSelection();
        rCboMovimiento.ClearSelection();
        rCboVencimiento.ClearSelection();
        rCboTipCambio.ClearSelection();
        rCboImporte.ClearSelection();
        rCboDescripcion.ClearSelection();
        MovimientoAbono.Checked = false;
        MovimientoCargo.Checked = true;
        TipoCliente.Checked = false;
        TipoCtaDeposito.Checked = true;
        rTxtSecuencia.CssClass = "cssTxtEnabled";
        rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Transparent;
    }

    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        ControlesAccion();

    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_MapeoModulo.AllowMultiRowSelection = true;
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        InicioPagina();

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
                EjecutaSpAcciones();

            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }



    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rTxtSecuencia.Text.Trim() == "")
            {
                rTxtSecuencia.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSecuencia.CssClass = "cssTxtEnabled"; }

            if (rCboSecuenciaCodigo.SelectedValue == "")
            {
                rCboSecuenciaCodigo.BorderWidth = Unit.Pixel(1);
                rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdv_MapeoModulo.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (rTxtSecuencia.Text.Trim() == "")
            {
                rTxtSecuencia.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSecuencia.CssClass = "cssTxtEnabled"; }

            if (rCboSecuenciaCodigo.SelectedValue == "")
            {
                rCboSecuenciaCodigo.BorderWidth = Unit.Pixel(1);
                rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Transparent;
            }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_MapeoModulo.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }

    private void EjecutaSpAccionEliminar()
    {

        try
        {

            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";




            foreach (GridDataItem i in rGdv_MapeoModulo.SelectedItems)
            {

                var dataItem = rGdv_MapeoModulo.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string SecuenciaCve = dataItem.GetDataKeyValue("movMapXPSecApli").ToString();
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloXP";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, PagLoc_Cpto);
                        ProcBD.AgregarParametrosProcedimiento("@movMapCCSecApli", DbType.Int64, 0, ParameterDirection.Input, SecuenciaCve);

                        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }

                            MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        string MsgError = ex.Message.Trim();
                    }


                }

                CountItems += 1;
            }





            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = MsgItemsElim + " " + CountItems.ToString();
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                }


            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    string sMSGTip = "";
                    string sMSGRegNoElim = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0006", ref sMSGTip, ref sMSGRegNoElim);
                    sMsgAlert = sMsgAlert + sMSGRegNoElim + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void DesHabilBtn(bool Par)
    {
        rTxtSecuencia.Enabled = Par;
        MovimientoAbono.Enabled = Par;
        MovimientoCargo.Enabled = Par;
        TipoCliente.Enabled = Par;
        TipoCtaDeposito.Enabled = Par;
        rCboSecuenciaCodigo.Enabled = Par;
        rCboReferenciasPrincipal.Enabled = Par;
        rCboReferenciasAplicacion.Enabled = Par;
        rCboReferencia3.Enabled = Par;
        rCboReferencia4.Enabled = Par;
        rCboMovimiento.Enabled = Par;
        rCboVencimiento.Enabled = Par;
        rCboMoneda.Enabled = Par;
        rCboTipCambio.Enabled = Par;
        rCboImporte.Enabled = Par;
        rCboDescripcion.Enabled = Par;
        rBtnCancelar.Enabled = Par;
        rBtnGuardar.Enabled = Par;
    }

    private void EjecutaSpAcciones()
    {
        string movimiento = "0", tipo = "0";

        if (MovimientoCargo.Checked == true)
        {
            movimiento = "1";
        }
        else if (MovimientoAbono.Checked == true)
        {
            movimiento = "2";
        }
        if (TipoCtaDeposito.Checked == true)
        {
            tipo = "1";
        }
        else if (TipoCliente.Checked == true)
        {
            tipo = "2";
        }

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloXP";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);

            ProcBD.AgregarParametrosProcedimiento("@movMapCCSecApli", DbType.Int64, 0, ParameterDirection.Input, rTxtSecuencia.Text);
            ProcBD.AgregarParametrosProcedimiento("@movMapCCCoA", DbType.Int64, 0, ParameterDirection.Input, movimiento);
            ProcBD.AgregarParametrosProcedimiento("@movMapCCTipApli", DbType.Int64, 0, ParameterDirection.Input, tipo);
            ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_CodApli", DbType.Int64, 0, ParameterDirection.Input, rCboSecuenciaCodigo.SelectedValue);


            if (rCboReferenciasAplicacion.SelectedValue != "")
            {

                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_Aplic", DbType.Int64, 0, ParameterDirection.Input, rCboReferenciasAplicacion.SelectedValue);
            }
            if (rCboReferenciasPrincipal.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_Princ", DbType.Int64, 0, ParameterDirection.Input, rCboReferenciasPrincipal.SelectedValue);
            }
            if (rCboReferencia3.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_03", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia3.SelectedValue);
            }
            if (rCboReferencia4.SelectedValue != "")
            {

                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_04", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia4.SelectedValue);
            }
            if (rCboMovimiento.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_Mov", DbType.Int64, 0, ParameterDirection.Input, rCboMovimiento.SelectedValue);
            }
            if (rCboVencimiento.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_Venc", DbType.Int64, 0, ParameterDirection.Input, rCboVencimiento.SelectedValue);
            }
            if (rCboMoneda.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);
            }
            if (rCboImporte.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Imp_Imp", DbType.Int64, 0, ParameterDirection.Input, rCboImporte.SelectedValue);
            }
            if (rCboTipCambio.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_TipCam", DbType.Int64, 0, ParameterDirection.Input, rCboTipCambio.SelectedValue);
            }
            if (rCboDescripcion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref40_Des", DbType.Int64, 0, ParameterDirection.Input, rCboDescripcion.SelectedValue);
            }



            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {
                    LimpiaBtn();
                    InicioPagina();
                }
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }


    //private void ControlesAccion()
    //{

    //    //===> CONTROLES GENERAL
    //    //this.rCboAlmacen.ClearSelection();
    //    //rGdvInformacion.MasterTableView.ClearSelectedItems();
    //    //this.rTxtLocalizacion.Text = "";
    //    //this.rTxtMaximo.Text = "0";
    //    //this.rTxtMinimo.Text = "0";
    //    //this.rTxtReorden.Text = "0";

    //    //rTxtLocalizacion.CssClass = "cssTxtEnabled";
    //    //rTxtMaximo.CssClass = "cssTxtEnabled";
    //    //rTxtMinimo.CssClass = "cssTxtEnabled";
    //    //rTxtReorden.CssClass = "cssTxtEnabled";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        DesHabilBtn(true);
    //        LimpiaBtn();
    //        rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();
    //        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdv_MapeoModulo.AllowMultiRowSelection = false;
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        DesHabilBtn(true);
    //        rTxtSecuencia.Enabled = false;
    //        LimpiaBtn();
    //        rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();
    //        //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        //this.rGdvInformacion.AllowMultiRowSelection = false;
    //        //this.rCboAlmacen.Enabled = false;
    //        //this.rTxtLocalizacion.Enabled = false;
    //        //this.rTxtMaximo.Enabled = false;
    //        //this.rTxtMinimo.Enabled = false;
    //        //this.rTxtReorden.Enabled = false;
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_MapeoModulo.AllowMultiRowSelection = true;
    //        LimpiaBtn();
    //        DesHabilBtn(false);
    //        rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();
    //        //this.rCboAlmacen.Enabled = false;
    //        //this.rTxtLocalizacion.Enabled = false;
    //        //this.rTxtMaximo.Enabled = false;
    //        //this.rTxtMinimo.Enabled = false;
    //        //this.rTxtReorden.Enabled = false;
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //        //this.rCboAlmacen.Enabled = false;
    //        //this.rTxtLocalizacion.Enabled = false;
    //        //this.rTxtMaximo.Enabled = false;
    //        //this.rTxtMinimo.Enabled = false;
    //        //this.rTxtReorden.Enabled = false;
    //    }


    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //       )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //}

    protected void rGdv_MapeoModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            LimpiaBtn();
            CargaDatosEdita();
        }
        if (hdfBtnAccion.Value == "")
        {
            LimpiaBtn();
            CargaDatosEdita();
            //DesHabilBtn(false);
        }
    }

    public void CargaDatosEdita()
    {

        var dataItem = rGdv_MapeoModulo.SelectedItems[0] as GridDataItem;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ProcBD.AgregarParametrosProcedimiento("@movMapCCSecApli", DbType.Int64, 0, ParameterDirection.Input, dataItem["movMapXPSecApli"].Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        rTxtSecuencia.Text = ds.Tables[0].Rows[0]["movMapXPSecApli"].ToString();

        if (ds.Tables[0].Rows[0]["movMapXPCoA"].ToString() == "1")
        {
            MovimientoAbono.Checked = false;
            MovimientoCargo.Checked = true;

        }
        else if (ds.Tables[0].Rows[0]["movMapXPCoA"].ToString() == "2")
        {
            MovimientoCargo.Checked = false;
            MovimientoAbono.Checked = true;
        }




        if (ds.Tables[0].Rows[0]["movMapXPTipApli"].ToString() == "1")
        {
            TipoCliente.Checked = false;
            TipoCtaDeposito.Checked = true;
        }
        else if (ds.Tables[0].Rows[0]["movMapXPTipApli"].ToString() == "2")
        {
            TipoCtaDeposito.Checked = false;
            TipoCliente.Checked = true;
        }




        rCboSecuenciaCodigo.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_CodApli"].ToString();
        rCboReferenciasAplicacion.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_Aplic"].ToString();
        rCboReferenciasPrincipal.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_Princ"].ToString();
        rCboReferencia3.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_03"].ToString();
        rCboReferencia4.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_04"].ToString();
        rCboMovimiento.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_Mov"].ToString();
        rCboVencimiento.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_Venc"].ToString();
        rCboMoneda.SelectedValue = ds.Tables[0].Rows[0]["monCve"].ToString();
        rCboImporte.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Imp_Imp"].ToString();
        rCboTipCambio.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_TipCam"].ToString();
        rCboDescripcion.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref40_Des"].ToString();

    }

    public void llena_Grid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_MapeoModulo, ds);

    }

    public void DescripcionCpto()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    
            radLabelConcepto.Text = ds.Tables[0].Rows[0]["cptoId"].ToString();
            radLabelConceptoDes.Text = ds.Tables[0].Rows[0]["cptoDes"].ToString();
            
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtSecuencia.CssClass = "cssTxtEnabled";
        rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Red;

        rTxtSecuencia.Enabled = false;
        MovimientoCargo.Enabled = false;
        MovimientoAbono.Enabled = false;
        TipoCtaDeposito.Enabled = false;
        TipoCliente.Enabled = false;
        rCboSecuenciaCodigo.Enabled = false;
        rCboReferenciasPrincipal.Enabled = false;
        rCboReferenciasAplicacion.Enabled = false;
        rCboReferencia3.Enabled = false;
        rCboReferencia4.Enabled = false;
        rCboMovimiento.Enabled = false;
        rCboVencimiento.Enabled = false;
        rCboMoneda.Enabled = false;
        rCboTipCambio.Enabled = false;
        rCboImporte.Enabled = false;
        rCboDescripcion.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


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

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rTxtSecuencia.Enabled = false;
            MovimientoCargo.Enabled = false;
            MovimientoAbono.Enabled = false;
            TipoCtaDeposito.Enabled = false;
            TipoCliente.Enabled = false;
            rCboSecuenciaCodigo.Enabled = false;
            rCboReferenciasPrincipal.Enabled = false;
            rCboReferenciasAplicacion.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboDescripcion.Enabled = false;


            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            TipoCtaDeposito.Checked = true;
            TipoCliente.Checked = false;
            rCboSecuenciaCodigo.ClearSelection();
            rCboReferenciasPrincipal.ClearSelection();
            rCboReferenciasAplicacion.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboDescripcion.ClearSelection();
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_MapeoModulo.AllowMultiRowSelection = true;
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();

                rTxtSecuencia.Enabled = true;
                MovimientoCargo.Enabled = true;
                MovimientoAbono.Enabled = true;
                TipoCtaDeposito.Enabled = true;
                TipoCliente.Enabled = true;
                rCboSecuenciaCodigo.Enabled = true;
                rCboReferenciasPrincipal.Enabled = true;
                rCboReferenciasAplicacion.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboMovimiento.Enabled = true;
                rCboVencimiento.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboTipCambio.Enabled = true;
                rCboImporte.Enabled = true;
                rCboDescripcion.Enabled = true;

                rTxtSecuencia.Text = "";
                MovimientoCargo.Checked = true;
                MovimientoAbono.Checked = false;
                TipoCtaDeposito.Checked = true;
                TipoCliente.Checked = false;
                rCboSecuenciaCodigo.ClearSelection();
                rCboReferenciasPrincipal.ClearSelection();
                rCboReferenciasAplicacion.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboMovimiento.ClearSelection();
                rCboVencimiento.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboTipCambio.ClearSelection();
                rCboImporte.ClearSelection();
                rCboDescripcion.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_MapeoModulo.AllowMultiRowSelection = false;

                rTxtSecuencia.Enabled = false;
                MovimientoCargo.Enabled = true;
                MovimientoAbono.Enabled = true;
                TipoCtaDeposito.Enabled = true;
                TipoCliente.Enabled = true;
                rCboSecuenciaCodigo.Enabled = true;
                rCboReferenciasPrincipal.Enabled = true;
                rCboReferenciasAplicacion.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboMovimiento.Enabled = true;
                rCboVencimiento.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboTipCambio.Enabled = true;
                rCboImporte.Enabled = true;
                rCboDescripcion.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_MapeoModulo.AllowMultiRowSelection = true;
                rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();

                rTxtSecuencia.Enabled = false;
                MovimientoCargo.Enabled = false;
                MovimientoAbono.Enabled = false;
                TipoCtaDeposito.Enabled = false;
                TipoCliente.Enabled = false;
                rCboSecuenciaCodigo.Enabled = false;
                rCboReferenciasPrincipal.Enabled = false;
                rCboReferenciasAplicacion.Enabled = false;
                rCboReferencia3.Enabled = false;
                rCboReferencia4.Enabled = false;
                rCboMovimiento.Enabled = false;
                rCboVencimiento.Enabled = false;
                rCboMoneda.Enabled = false;
                rCboTipCambio.Enabled = false;
                rCboImporte.Enabled = false;
                rCboDescripcion.Enabled = false;

                rTxtSecuencia.Text = "";
                MovimientoCargo.Checked = true;
                MovimientoAbono.Checked = false;
                TipoCtaDeposito.Checked = true;
                TipoCliente.Checked = false;
                rCboSecuenciaCodigo.ClearSelection();
                rCboReferenciasPrincipal.ClearSelection();
                rCboReferenciasAplicacion.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboMovimiento.ClearSelection();
                rCboVencimiento.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboTipCambio.ClearSelection();
                rCboImporte.ClearSelection();
                rCboDescripcion.ClearSelection();
            }
        }


        if (Result == false)
        {
            rTxtSecuencia.Enabled = false;
            MovimientoCargo.Enabled = false;
            MovimientoAbono.Enabled = false;
            TipoCtaDeposito.Enabled = false;
            TipoCliente.Enabled = false;
            rCboSecuenciaCodigo.Enabled = false;
            rCboReferenciasPrincipal.Enabled = false;
            rCboReferenciasAplicacion.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboDescripcion.Enabled = false;

            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            TipoCtaDeposito.Checked = true;
            TipoCliente.Checked = false;
            rCboSecuenciaCodigo.ClearSelection();
            rCboReferenciasPrincipal.ClearSelection();
            rCboReferenciasAplicacion.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboDescripcion.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_MapeoModulo.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_MapeoModulo, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_MapeoModulo, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            TipoCtaDeposito.Checked = true;
            TipoCliente.Checked = false;
            rCboSecuenciaCodigo.ClearSelection();
            rCboReferenciasPrincipal.ClearSelection();
            rCboReferenciasAplicacion.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboDescripcion.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_MapeoModulo.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtSecuencia.CssClass = "cssTxtEnabled";
            rCboSecuenciaCodigo.BorderColor = System.Drawing.Color.Red;

            rTxtSecuencia.Enabled = false;
            MovimientoCargo.Enabled = false;
            MovimientoAbono.Enabled = false;
            TipoCtaDeposito.Enabled = false;
            TipoCliente.Enabled = false;
            rCboSecuenciaCodigo.Enabled = false;
            rCboReferenciasPrincipal.Enabled = false;
            rCboReferenciasAplicacion.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboMovimiento.Enabled = false;
            rCboVencimiento.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboTipCambio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboDescripcion.Enabled = false;

            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            TipoCtaDeposito.Checked = true;
            TipoCliente.Checked = false;
            rCboSecuenciaCodigo.ClearSelection();
            rCboReferenciasPrincipal.ClearSelection();
            rCboReferenciasAplicacion.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboMovimiento.ClearSelection();
            rCboVencimiento.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboTipCambio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboDescripcion.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

        rGdv_MapeoModulo.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_MapeoModulo.AllowMultiRowSelection = true;

    }



}