using System;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
public partial class DC_MttoCptoMapeoModuloIn : System.Web.UI.Page
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

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_MapeoModuloIn.AllowMultiRowSelection = true;
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }
    
    protected void rGdv_MapeoModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            LimpiaBtn();
            CargaDatosEdita();
           // DesHabilBtn(true);
            rTxtSecuencia.Enabled = false;
        }
        if (hdfBtnAccion.Value == "")
        {
            LimpiaBtn();
            CargaDatosEdita();
            //DesHabilBtn(false);
        }
    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }
    #endregion

    #region FUNCIONES
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
        rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
        DesHabilBtn(false);
        LimpiaBtn();
        if (PagLoc_Cpto != "")
        {
            
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fact", ref rCboTipoCambio, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fec", ref rCboFecha_Mov, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fec", ref rCboFecha_02, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str20", ref rCboArticulo, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fact", ref rCboPrecio, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fact", ref rCboCantidad, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Fact", ref rCboCosto, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Imp", ref rCboImporte, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboAlmacen, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str20", ref rCboLote, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str20", ref rCboSerie, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboProveedor, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboOrdenCompra, true, false);

            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboAlmContra, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str20", ref rCboAduana, true, false);

            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboUnidadMedida, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboCentroCostos, true, false);
            FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia1, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia2, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia3, true, false);
            FnCtlsFillIn.RadComboBox_MapeoConfiguracion(Pag_sConexionLog, Pag_sCompania, PagLoc_Cpto, "Str10", ref rCboReferencia4, true, false);
            DescripcionCpto();

            bool bRefCpto = ConceptoRef();
            lblValidaExist.Visible = bRefCpto;
            rbValidaExistAplica.Visible = bRefCpto;
            rbValidaExistCaptura.Visible = bRefCpto;
            llena_Grid();
        }
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

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
        rCboFecha_Mov.ClearSelection();
        rCboTipoCambio.ClearSelection();
        rCboArticulo.ClearSelection();
        rCboPrecio.ClearSelection();
        rCboCantidad.ClearSelection();
        rCboCosto.ClearSelection();
        rCboImporte.ClearSelection();
        rCboAlmacen.ClearSelection();
        rCboLote.ClearSelection();
        rCboSerie.ClearSelection();
        rCboOrdenCompra.ClearSelection();
        rCboUnidadMedida.ClearSelection();
        rCboCentroCostos.ClearSelection();
        rCboMoneda.ClearSelection();
        rCboAlmContra.ClearSelection();
        rCboAduana.ClearSelection();
        rCboReferencia1.ClearSelection();
        rCboReferencia2.ClearSelection();
        rCboReferencia3.ClearSelection();
        rCboReferencia4.ClearSelection();
        MovimientoAbono.Checked = false;
        MovimientoCargo.Checked = true;
        AplicaNo.Checked = false;
        AplicaSi.Checked = true;
        rTxtSecuencia.CssClass = "cssTxtEnabled";
        rCboFecha_Mov.BorderColor = System.Drawing.Color.Transparent;
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

            if (rCboArticulo.SelectedValue == "")
            {
                rCboArticulo.BorderWidth = Unit.Pixel(1);
                rCboArticulo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
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
            if (rGdv_MapeoModuloIn.SelectedItems.Count == 0)
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

            if (rCboArticulo.SelectedValue == "")
            {
                rCboArticulo.BorderWidth = Unit.Pixel(1);
                rCboArticulo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
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

            if (rGdv_MapeoModuloIn.SelectedItems.Count == 0)
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
            
            foreach (GridDataItem i in rGdv_MapeoModuloIn.SelectedItems)
            {

                var dataItem = rGdv_MapeoModuloIn.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string SecuenciaCve = dataItem.GetDataKeyValue("movMapINSecApli").ToString();
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloIN";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, PagLoc_Cpto);
                        ProcBD.AgregarParametrosProcedimiento("@movMapINSecApli", DbType.Int64, 0, ParameterDirection.Input, SecuenciaCve);

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
        if (AplicaSi.Checked == true)
        {
            tipo = "1";
        }
        else if (AplicaNo.Checked == true)
        {
            tipo = "2";
        }

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloIN";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
            ProcBD.AgregarParametrosProcedimiento("@movMapINSecApli", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(rTxtSecuencia.Text));
            ProcBD.AgregarParametrosProcedimiento("@movMapINCoA", DbType.Int64, 0, ParameterDirection.Input, movimiento);
            ProcBD.AgregarParametrosProcedimiento("@movMapINApliOC", DbType.Int64, 0, ParameterDirection.Input, tipo);


            if (rCboArticulo.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref20_Art", DbType.Int64, 0, ParameterDirection.Input, rCboArticulo.SelectedValue);
            }
            if (rCboAlmacen.SelectedIndex != -1) {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_Alm", DbType.Int64, 0, ParameterDirection.Input, rCboAlmacen.SelectedValue);
            }
            if (rCboUnidadMedida.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_UniMed", DbType.Int64, 0, ParameterDirection.Input, rCboUnidadMedida.SelectedValue);
            }
            if (rCboCantidad.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_Cant", DbType.Int64, 0, ParameterDirection.Input, rCboCantidad.SelectedValue);
            }
            if (rCboCosto.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_Costo", DbType.Int64, 0, ParameterDirection.Input, rCboCosto.SelectedValue);
            }

            if (rCboPrecio.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_Prec", DbType.Int64, 0, ParameterDirection.Input, rCboPrecio.SelectedValue);
            }
            if (rCboImporte.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Imp_Imp", DbType.Int64, 0, ParameterDirection.Input, rCboImporte.SelectedValue);
            }
            if (rCboLote.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref20_Lote", DbType.Int64, 0, ParameterDirection.Input, rCboLote.SelectedValue);
            }
            if (rCboSerie.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref20_Serie", DbType.Int64, 0, ParameterDirection.Input, rCboSerie.SelectedValue);
            }
            if (rCboMoneda.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);
            }
            if (rCboTipoCambio.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fac_TipCam", DbType.Int64, 0, ParameterDirection.Input, rCboTipoCambio.SelectedValue);
            }
            if (rCboCentroCostos.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_CC", DbType.Int64, 0, ParameterDirection.Input, rCboCentroCostos.SelectedValue);
            }
            if (rCboOrdenCompra.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_OrdComp", DbType.Int64, 0, ParameterDirection.Input, rCboOrdenCompra.SelectedValue);
            }
            if (rCboProveedor.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_Prov", DbType.Int64, 0, ParameterDirection.Input, rCboProveedor.SelectedValue);
            }



            if (rCboAlmContra.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_AlmContra", DbType.Int64, 0, ParameterDirection.Input, rCboAlmContra.SelectedValue);
            }
            if (rCboAduana.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref20_Adu", DbType.Int64, 0, ParameterDirection.Input, rCboAduana.SelectedValue);
            }





            if (rCboFecha_Mov.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_Mov", DbType.Int64, 0, ParameterDirection.Input, rCboFecha_Mov.SelectedValue);
            }
            if (rCboFecha_02.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Fec_02", DbType.Int64, 0, ParameterDirection.Input, rCboFecha_02.SelectedValue);
            }
            if (rCboReferencia1.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_01", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia1.SelectedValue);
            }
            if (rCboReferencia2.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_02", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia2.SelectedValue);
            }
            if (rCboReferencia3.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_03", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia3.SelectedValue);
            }
            if (rCboReferencia4.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfId_Ref10_04", DbType.Int64, 0, ParameterDirection.Input, rCboReferencia4.SelectedValue);
            }

            if (ConceptoRef()) {
                int iMovMapINValExis = 2;

                if (rbValidaExistAplica.Checked == true)
                {
                    iMovMapINValExis =1;
                }
                ProcBD.AgregarParametrosProcedimiento("@movMapINValExis", DbType.Int64, 0, ParameterDirection.Input, iMovMapINValExis);
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
                    if (sEjecEstatus == "1")
                    {
                        LimpiaBtn();
                        InicioPagina();
                    }
                    
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

    #endregion

    #region METODOS
    public void CargaDatosEdita()
    {
        var dataItem = rGdv_MapeoModuloIn.SelectedItems[0] as GridDataItem;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloIN";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ProcBD.AgregarParametrosProcedimiento("@movMapINSecApli", DbType.Int64, 0, ParameterDirection.Input, dataItem["movMapINSecApli"].Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        rTxtSecuencia.Text = ds.Tables[0].Rows[0]["movMapINSecApli"].ToString();

        if (ds.Tables[0].Rows[0]["movMapINCoA"].ToString() == "1")
        {
            MovimientoAbono.Checked = false;
            MovimientoCargo.Checked = true;
        }
        else if (ds.Tables[0].Rows[0]["movMapINCoA"].ToString() == "2")
        {
            MovimientoCargo.Checked = false;
            MovimientoAbono.Checked = true;
        }
        
        if (ds.Tables[0].Rows[0]["movMapINApliOC"].ToString() == "1")
        {
            AplicaNo.Checked = false;
            AplicaSi.Checked = true;
        }
        else if (ds.Tables[0].Rows[0]["movMapINApliOC"].ToString() == "2")
        {
            AplicaSi.Checked = false;
            AplicaNo.Checked = true;
        }

 
        rCboArticulo.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref20_Art"].ToString();
        rCboAlmacen.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_Alm"].ToString();
        rCboUnidadMedida.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_UniMed"].ToString();
        rCboCantidad.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_Cant"].ToString();
        rCboCosto.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_Costo"].ToString();
        rCboPrecio.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_Prec"].ToString();
        rCboImporte.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Imp_Imp"].ToString();
        rCboLote.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref20_Lote"].ToString();
        rCboSerie.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref20_Serie"].ToString();
        rCboMoneda.SelectedValue = ds.Tables[0].Rows[0]["monCve"].ToString();
        rCboTipoCambio.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fac_TipCam"].ToString();
        rCboCentroCostos.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_CC"].ToString();
        rCboOrdenCompra.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_OrdComp"].ToString();
        rCboProveedor.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_Prov"].ToString();

        rCboAlmContra.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_AlmContra"].ToString();
        rCboAduana.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref20_Adu"].ToString();

        rCboFecha_Mov.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_Mov"].ToString();
        rCboFecha_02.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Fec_02"].ToString();
        rCboReferencia1.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_01"].ToString();
        rCboReferencia2.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_02"].ToString();
        rCboReferencia3.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_03"].ToString();
        rCboReferencia4.SelectedValue = ds.Tables[0].Rows[0]["cptoConfId_Ref10_04"].ToString();

        if (ConceptoRef()) {
            if (ds.Tables[0].Rows[0]["movMapINValExis"].ToString() == "1")
            {
                rbValidaExistAplica.Checked  = true;
                rbValidaExistCaptura.Checked = false;
            }
            else {
                rbValidaExistAplica.Checked = false;
                rbValidaExistCaptura.Checked = true ;

            }
                
        }
       
    }

    public void llena_Grid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloIN";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_MapeoModuloIn, ds);
    }

    public void DescripcionCpto()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModuloIN";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        radLabelConcepto.Text = ds.Tables[0].Rows[0]["cptoId"].ToString();
        radLabelConceptoDes.Text = ds.Tables[0].Rows[0]["cptoDes"].ToString();
    }

    private void DesHabilBtn(bool Par)
    {
        rCboArticulo.Enabled = Par;
        rCboPrecio.Enabled = Par;
        rCboCantidad.Enabled = Par;
        rCboImporte.Enabled = Par;
        rCboCosto.Enabled = Par;
        rCboAlmacen.Enabled = Par;
        rCboLote.Enabled = Par;
        rCboSerie.Enabled = Par;
        rCboOrdenCompra.Enabled = Par;
        rCboUnidadMedida.Enabled = Par;
        rCboCentroCostos.Enabled = Par;

        rCboAlmContra.Enabled = Par;
        rCboAduana.Enabled = Par;

        rTxtSecuencia.Enabled = Par;
        MovimientoAbono.Enabled = Par;
        MovimientoCargo.Enabled = Par;
        AplicaNo.Enabled = Par;
        AplicaSi.Enabled = Par;
        rCboTipoCambio.Enabled = Par;
        rCboFecha_Mov.Enabled = Par;
        rCboFecha_02.Enabled = Par; 
        rCboReferencia1.Enabled = Par;
        rCboReferencia2.Enabled = Par;
        rCboReferencia3.Enabled = Par;
        rCboReferencia4.Enabled = Par;
        rCboMoneda.Enabled = Par;
        rCboProveedor.Enabled = Par;
        rBtnCancelar.Enabled = Par;
        rBtnGuardar.Enabled = Par;
        rbValidaExistAplica.Enabled = Par;
        rbValidaExistCaptura.Enabled = Par; 
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtSecuencia.CssClass = "cssTxtEnabled";
        rCboArticulo.BorderColor = System.Drawing.Color.Transparent;


        rTxtSecuencia.Enabled = false;
        MovimientoCargo.Enabled = false;
        MovimientoAbono.Enabled = false;
        AplicaSi.Enabled = false;

        AplicaNo.Enabled = false;
        rCboArticulo.Enabled = false;
        rCboFecha_Mov.Enabled = false;
        rCboFecha_02.Enabled = false;
        rCboAlmacen.Enabled = false;
        rCboReferencia1.Enabled = false;
        rCboReferencia2.Enabled = false;
        rCboReferencia3.Enabled = false;
        rCboReferencia4.Enabled = false;
        rCboCantidad.Enabled = false;
        rCboCosto.Enabled = false;
        rCboUnidadMedida.Enabled = false;
        rCboMoneda.Enabled = false;
        rCboPrecio.Enabled = false;
        rCboImporte.Enabled = false;
        rCboTipoCambio.Enabled = false;
        rCboProveedor.Enabled = false; 

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
            AplicaSi.Enabled = false;
            AplicaNo.Enabled = false;
            rCboArticulo.Enabled = false;
            rCboFecha_Mov.Enabled = false;
            rCboFecha_02.Enabled = false;
            rCboAlmacen.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboCantidad.Enabled = false;
            rCboUnidadMedida.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboPrecio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboTipoCambio.Enabled = false;
            rCboProveedor.Enabled = false;

            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            AplicaSi.Checked = true;
            AplicaNo.Checked = false;
            rCboArticulo.ClearSelection();
            rCboFecha_Mov.ClearSelection();
            rCboFecha_02.ClearSelection();
            rCboAlmacen.ClearSelection();
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboCantidad.ClearSelection();
            rCboCosto.ClearSelection();
            rCboUnidadMedida.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboPrecio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboTipoCambio.ClearSelection();
            rCboProveedor.ClearSelection(); 
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_MapeoModuloIn.AllowMultiRowSelection = true;
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_MapeoModuloIn.MasterTableView.ClearSelectedItems();

                rTxtSecuencia.Enabled = true;
                MovimientoCargo.Enabled = true;
                MovimientoAbono.Enabled = true;
                AplicaSi.Enabled = true;
                AplicaNo.Enabled = true;
                rCboArticulo.Enabled = true;
                rCboFecha_Mov.Enabled = true;
                rCboFecha_02.Enabled = true;
                rCboAlmacen.Enabled = true;
                rCboReferencia1.Enabled = true;
                rCboReferencia2.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboCantidad.Enabled = true;
                rCboCosto.Enabled = true;
                rCboUnidadMedida.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboPrecio.Enabled = true;
                rCboImporte.Enabled = true;
                rCboTipoCambio.Enabled = true;
                rCboLote.Enabled = true;
                rCboSerie.Enabled = true;
                rCboOrdenCompra.Enabled = true;

                rCboAlmContra.Enabled = true;
                rCboAduana.Enabled = true;

                rCboCentroCostos.Enabled = true;
                rCboProveedor.Enabled = true;
                rTxtSecuencia.Text = "";
                MovimientoCargo.Checked = true;
                MovimientoAbono.Checked = false;
                AplicaSi.Checked = true;
                AplicaNo.Checked = false;

                rbValidaExistAplica.Enabled = true;
                rbValidaExistCaptura.Enabled = true;

                rCboArticulo.ClearSelection();
                rCboFecha_Mov.ClearSelection();
                rCboFecha_02.ClearSelection();
                rCboAlmacen.ClearSelection();
                rCboReferencia1.ClearSelection();
                rCboReferencia2.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboCantidad.ClearSelection();
                rCboCosto.ClearSelection();
                rCboUnidadMedida.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboPrecio.ClearSelection();
                rCboImporte.ClearSelection();
                rCboTipoCambio.ClearSelection();
                rCboLote.ClearSelection();
                rCboSerie.ClearSelection();
                rCboOrdenCompra.ClearSelection();

                rCboAlmContra.ClearSelection();
                rCboAduana.ClearSelection();
                rCboCentroCostos.ClearSelection();
                rCboProveedor.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_MapeoModuloIn.AllowMultiRowSelection = false;

                rTxtSecuencia.Enabled = false;
                MovimientoCargo.Enabled = true;
                MovimientoAbono.Enabled = true;
                AplicaSi.Enabled = true;
                AplicaNo.Enabled = true;
                rCboArticulo.Enabled = true;
                rCboFecha_Mov.Enabled = true;
                rCboFecha_02.Enabled = true;
                rCboAlmacen.Enabled = true;
                rCboReferencia1.Enabled = true;
                rCboReferencia2.Enabled = true;
                rCboReferencia3.Enabled = true;
                rCboReferencia4.Enabled = true;
                rCboCantidad.Enabled = true;
                rCboCosto.Enabled = true;
                rCboUnidadMedida.Enabled = true;
                rCboMoneda.Enabled = true;
                rCboPrecio.Enabled = true;
                rCboImporte.Enabled = true;
                rCboTipoCambio.Enabled = true;
                rCboLote.Enabled = true;
                rCboSerie.Enabled = true;
                rCboOrdenCompra.Enabled = true;
                rCboAlmContra.Enabled = true;
                rCboAduana.Enabled = true;
                rCboCentroCostos.Enabled = true;
                rCboProveedor.Enabled = true;
                rbValidaExistAplica.Enabled = true;
                rbValidaExistCaptura.Enabled = true;

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
                rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_MapeoModuloIn.AllowMultiRowSelection = true;
                rGdv_MapeoModuloIn.MasterTableView.ClearSelectedItems();

                rTxtSecuencia.Enabled = false;
                MovimientoCargo.Enabled = false;
                MovimientoAbono.Enabled = false;
                AplicaSi.Enabled = false;
                AplicaNo.Enabled = false;
                rCboArticulo.Enabled = false;
                rCboFecha_Mov.Enabled = false;
                rCboAlmacen.Enabled = false;
                rCboReferencia1.Enabled = false;
                rCboReferencia2.Enabled = false;
                rCboReferencia3.Enabled = false;
                rCboReferencia4.Enabled = false;
                rCboCantidad.Enabled = false;
                rCboCosto.Enabled = false;
                rCboUnidadMedida.Enabled = false;
                rCboMoneda.Enabled = false;
                rCboPrecio.Enabled = false;
                rCboImporte.Enabled = false;
                rCboTipoCambio.Enabled = false;

                rTxtSecuencia.Text = "";
                MovimientoCargo.Checked = true;
                MovimientoAbono.Checked = false;
                AplicaSi.Checked = true;
                AplicaNo.Checked = false;
                rCboArticulo.ClearSelection();
                rCboFecha_Mov.ClearSelection();
                rCboAlmacen.ClearSelection();
                rCboReferencia1.ClearSelection();
                rCboReferencia2.ClearSelection();
                rCboReferencia3.ClearSelection();
                rCboReferencia4.ClearSelection();
                rCboCantidad.ClearSelection();
                rCboCosto.ClearSelection();
                rCboUnidadMedida.ClearSelection();
                rCboMoneda.ClearSelection();
                rCboPrecio.ClearSelection();
                rCboImporte.ClearSelection();
                rCboTipoCambio.ClearSelection();
            }
        }


        if (Result == false)
        {
            rTxtSecuencia.Enabled = false;
            MovimientoCargo.Enabled = false;
            MovimientoAbono.Enabled = false;
            AplicaSi.Enabled = false;
            AplicaNo.Enabled = false;
            rCboArticulo.Enabled = false;
            rCboFecha_Mov.Enabled = false;
            rCboAlmacen.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboCantidad.Enabled = false;
            rCboCosto.Enabled = false;
            rCboUnidadMedida.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboPrecio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboTipoCambio.Enabled = false;

            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            AplicaSi.Checked = true;
            AplicaNo.Checked = false;
            rCboArticulo.ClearSelection();
            rCboFecha_Mov.ClearSelection();
            rCboAlmacen.ClearSelection();
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboCantidad.ClearSelection();
            rCboCosto.ClearSelection();
            rCboUnidadMedida.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboPrecio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboTipoCambio.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_MapeoModuloIn.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_MapeoModuloIn, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_MapeoModuloIn, GvVAS, ref sMSGTip, ref sResult) == false)
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
            AplicaSi.Checked = true;
            AplicaNo.Checked = false;
            rCboArticulo.ClearSelection();
            rCboFecha_Mov.ClearSelection();
            rCboAlmacen.ClearSelection();
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboCantidad.ClearSelection();
            rCboCosto.ClearSelection();
            rCboUnidadMedida.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboPrecio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboTipoCambio.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_MapeoModuloIn.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtSecuencia.CssClass = "cssTxtEnabled";
            rCboArticulo.BorderColor = System.Drawing.Color.Transparent;

            rTxtSecuencia.Enabled = false;
            MovimientoCargo.Enabled = false;
            MovimientoAbono.Enabled = false;
            AplicaSi.Enabled = false;
            AplicaNo.Enabled = false;
            rCboArticulo.Enabled = false;
            rCboFecha_Mov.Enabled = false;
            rCboAlmacen.Enabled = false;
            rCboReferencia1.Enabled = false;
            rCboReferencia2.Enabled = false;
            rCboReferencia3.Enabled = false;
            rCboReferencia4.Enabled = false;
            rCboCantidad.Enabled = false;
            rCboCosto.Enabled = false; 
            rCboUnidadMedida.Enabled = false;
            rCboMoneda.Enabled = false;
            rCboPrecio.Enabled = false;
            rCboImporte.Enabled = false;
            rCboTipoCambio.Enabled = false;





            rTxtSecuencia.Text = "";
            MovimientoCargo.Checked = true;
            MovimientoAbono.Checked = false;
            AplicaSi.Checked = true;
            AplicaNo.Checked = false;
            rCboArticulo.ClearSelection();
            rCboFecha_Mov.ClearSelection();
            rCboAlmacen.ClearSelection();
            rCboReferencia1.ClearSelection();
            rCboReferencia2.ClearSelection();
            rCboReferencia3.ClearSelection();
            rCboReferencia4.ClearSelection();
            rCboCantidad.ClearSelection();
            rCboCosto.ClearSelection(); 
            rCboUnidadMedida.ClearSelection();
            rCboMoneda.ClearSelection();
            rCboPrecio.ClearSelection();
            rCboImporte.ClearSelection();
            rCboTipoCambio.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

        rGdv_MapeoModuloIn.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_MapeoModuloIn.AllowMultiRowSelection = true;
    }


    private bool ConceptoRef() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, PagLoc_Cpto);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            if (ds.Tables[0].Rows[0]["contRefCve"].ToString() != "I2") {
                return false; 
            }

        }
        return true;
    }

    #endregion








}