using System.Windows.Forms;
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

public partial class DC_MttoListaPAgrupacion : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_agrDatoCve;
    private string Pag_lisPreCve;

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
        OpcNuevo();
        if (Request.QueryString["agrDatoCve"] != null && Request.QueryString["agrDatoCve"] != "")
        {
            Pag_agrDatoCve = Request.QueryString["agrDatoCve"];
            rCboTipAgrupacionesDato.Enabled = false;
            rCboListaPrecio.Enabled = true;
            rCboListaPrecio.ClearSelection();
        }
        else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            Pag_lisPreCve = Request.QueryString["lisPreCve"];
            rCboAgrupacionesTipo.Enabled = false;
            rCboAgrupaciones.Enabled = true;
            rCboListaPrecio.Enabled = false;
            rCboAgrupaciones.ClearSelection();
            rCboTipAgrupacionesDato.ClearSelection();
        }
        else
        {
            ControlesAccion();
        }
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
        if (Request.QueryString["agrDatoCve"] != null && Request.QueryString["agrDatoCve"] != "")
        {
            rCboListaPrecio.ClearSelection();
            rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;
            rCboListaPrecio.Enabled = false;
        }
        else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            rCboTipAgrupacionesDato.ClearSelection();
            rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent;
            rCboTipAgrupacionesDato.Enabled = false;

            rCboAgrupaciones.ClearSelection();
            rCboAgrupaciones.Enabled = false;
            rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;

            //rCboAgrupacionesTipo.ClearSelection();
            //rCboAgrupacionesTipo.Enabled = false;
            //rCboAgrupacionesTipo.BorderColor = System.Drawing.Color.Transparent;
        }
        else
        {
            rCboListaPrecio.ClearSelection();
            rCboTipAgrupacionesDato.ClearSelection();
            rCboAgrupacionesTipo.ClearSelection();
            rCboAgrupaciones.ClearSelection();
            rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();
            ControlesAccion();
        }
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (rBtnNuevo.Image.Url == "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png")
        {
        
            rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();

            if (Request.QueryString["agrDatoCve"] != null && Request.QueryString["agrDatoCve"] != "")
            {
                rCboListaPrecio.ClearSelection();
                rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;

            }
            else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
            {
                rCboTipAgrupacionesDato.ClearSelection();
                rCboAgrupaciones.ClearSelection();

                rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent;

                //rCboAgrupaciones.ClearSelection();
                //rCboAgrupacionesTipo.ClearSelection();
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                {
                    rCboAgrupacionesTipo.Enabled = false;
                    rCboTipAgrupacionesDato.Enabled = false;
                    rCboAgrupaciones.Enabled = true;
                }
            
            }
            else
            {
                rCboListaPrecio.ClearSelection();
                rCboTipAgrupacionesDato.ClearSelection();
            }
        }
    }

    protected void rGdvListaP_Agrupaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == "")
        {
            var dataItem = rGdvListaP_Agrupaciones.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                rCboListaPrecio.SelectedValue = dataItem["lisPreCve"].Text;
                
                rCboAgrupacionesTipo.SelectedValue = dataItem["agrTipId"].Text;

                FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue), ref rCboAgrupaciones, true, false);
                rCboAgrupaciones.SelectedValue = dataItem["agrCve"].Text;

                FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboTipAgrupacionesDato, rCboAgrupaciones.SelectedValue, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue), true, false);

                rCboTipAgrupacionesDato.SelectedValue = dataItem["agrDatoCve"].Text;


            }
        }
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (Pag_lisPreCve != "" && Pag_lisPreCve != null)
        {
            Int64 Pag_sidM = 0;
            if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
            {
                Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
            }
            Response.Redirect("~/DC/MttoListaPrecios.aspx?" + "idM=" + Pag_sidM);
        }
        else
        {
            InicioPagina();
            rGdvListaP_Agrupaciones.Enabled = true;
            rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();
            rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvListaP_Agrupaciones.AllowMultiRowSelection = false;
        }
    }

    protected void rCboTipAgrupaciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboAgrupaciones.ClearSelection();
        rCboAgrupaciones.Enabled = true;
      rCboTipAgrupacionesDato.ClearSelection();
        rCboTipAgrupacionesDato.Enabled = false;
      FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue) ,ref rCboAgrupaciones, true, false);
    }
    protected void rCboAgrupacionesTip_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboTipAgrupacionesDato.ClearSelection();
        rCboTipAgrupacionesDato.Enabled = true;
        FnCtlsFillIn.RadComboBox_AgrupaDatos(Pag_sConexionLog, Pag_sCompania, ref rCboTipAgrupacionesDato, rCboAgrupaciones.SelectedValue, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue),true, false);
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            Pag_lisPreCve = Request.QueryString["lisPreCve"];
        }

        if (Request.QueryString["agrDatoCve"] != null && Request.QueryString["agrDatoCve"] != "")
        {
            Pag_agrDatoCve = Request.QueryString["agrDatoCve"];
        }
        
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";

        FnCtlsFillIn.RadComboBox_Agrupa(Pag_sConexionLog,ref rCboAgrupacionesTipo, true, false);
       
        FnCtlsFillIn.RadComboBox_ListaPrecios(Pag_sConexionLog, Pag_sCompania, ref rCboListaPrecio, true, false);
        ControlesAccion();
        LlenaGrid();
        rCboAgrupacionesTipo.SelectedValue = "2";

        FnCtlsFillIn.RadComboBox_AgrupacionAgrTipId(Pag_sConexionLog, Pag_sCompania, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue), ref rCboAgrupaciones, true, false);

        rCboTipAgrupacionesDato.EmptyMessage = "Seleccionar";
        rCboAgrupaciones.EmptyMessage = "Seleccionar";

        if (Pag_agrDatoCve != "" && Pag_agrDatoCve != null)
        {
            rCboTipAgrupacionesDato.SelectedValue = Pag_agrDatoCve;
            rCboTipAgrupacionesDato.Enabled = false;
            rCboAgrupaciones.Enabled = false;
            rCboAgrupacionesTipo.Enabled = false;
        }
        if (Pag_lisPreCve != "" && Pag_lisPreCve != null)
        {
            rCboListaPrecio.SelectedValue = Pag_lisPreCve;
            rCboListaPrecio.Enabled = false;
            rBtnCancelar.Enabled = true;
            rGdvListaP_Agrupaciones.Width = System.Web.UI.WebControls.Unit.Pixel(802);
        }
        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
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

    //private void ControlesAccion()
    //{
    //    //===> CONTROLES GENERAL
    //    rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();

    //    rCboListaPrecio.ClearSelection();
    //    rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;
    //    rCboTipAgrupacionesDato.ClearSelection();
    //    rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent;

    //    rCboAgrupaciones.ClearSelection();
    //    rCboAgrupaciones.Enabled = false;
    //    rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;

    //    //rCboAgrupacionesTipo.ClearSelection();
    //    rCboAgrupacionesTipo.Enabled = false;
    //    rCboAgrupacionesTipo.BorderColor = System.Drawing.Color.Transparent;
    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
    //    rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
    //    rGdvListaP_Agrupaciones.AllowMultiRowSelection = false;

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = false;
    //        rCboAgrupacionesTipo.Enabled = false;
    //        rCboListaPrecio.Enabled = false;
    //        rCboAgrupaciones.Enabled = true;
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
    //        rGdvListaP_Agrupaciones.Enabled = false;
    //        rCboTipAgrupacionesDato.Enabled = false;
    //        rCboListaPrecio.Enabled = false;
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
    //        rCboTipAgrupacionesDato.Enabled = false;
    //        rCboListaPrecio.Enabled = false;
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

    private void EjecutaAccion()
    {

        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
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

            if (rCboListaPrecio.SelectedValue == "")
            {
                rCboListaPrecio.BorderWidth = Unit.Pixel(1);
                rCboListaPrecio.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboTipAgrupacionesDato.SelectedValue == "")
            {
                rCboTipAgrupacionesDato.BorderWidth = Unit.Pixel(1);
                rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvListaP_Agrupaciones.SelectedItems.Count == 0)
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

    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios_AgrupacionesDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboListaPrecio.SelectedValue.Trim()));

            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboAgrupacionesTipo.SelectedValue.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 20, ParameterDirection.Input, rCboAgrupaciones.SelectedValue.Trim());

            ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, rCboTipAgrupacionesDato.SelectedValue.Trim());
            

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
                    LlenaGrid();
                    InicioPagina();
                }
            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


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


            foreach (GridDataItem i in rGdvListaP_Agrupaciones.SelectedItems)
            {

                var dataItem = rGdvListaP_Agrupaciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    string lisPreCve = dataItem["lisPreCve"].Text;

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ListaPrecios_AgrupacionesDatos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, dataItem["agrDatoCve"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(lisPreCve));

                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

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

    #endregion

    #region FUNCIONES
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ListaPrecios_AgrupacionesDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (Pag_lisPreCve != "" && Pag_lisPreCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_lisPreCve));
        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvListaP_Agrupaciones, ds);

    }
    private void OpcNuevo()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvListaP_Agrupaciones.AllowMultiRowSelection = false;
        rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();
        rGdvListaP_Agrupaciones.Enabled = false;
        rGdvListaP_Agrupaciones.MasterTableView.Enabled = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = false;

    }

    private void OpcEliminar()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
        rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }

    #endregion



    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdvListaP_Agrupaciones
        this.rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;
        rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent;

        rCboAgrupaciones.Enabled = false;
        rCboTipAgrupacionesDato.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = true;

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
            rCboAgrupaciones.Enabled = false;
            rCboTipAgrupacionesDato.Enabled = false;

           
            rCboTipAgrupacionesDato.ClearSelection();
            rCboAgrupaciones.ClearSelection();
    


        }
    }



    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO rGdvListaP_Agrupaciones
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();

                rCboAgrupaciones.Enabled = true;
                rCboTipAgrupacionesDato.Enabled = true;

                rCboAgrupaciones.ClearSelection();
                rCboTipAgrupacionesDato.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            //    rGdvListaP_Agrupaciones.AllowMultiRowSelection = false;
            //    this.rTxtCve.Enabled = false;
            //    this.rTxtDes.Enabled = true;
            //    this.rTxtAbr.Enabled = true;
            //    rBtnGuardar.Enabled = true;
            //    rBtnCancelar.Enabled = true;
            //}

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
                rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();


                rCboAgrupaciones.Enabled = false;
                rCboTipAgrupacionesDato.Enabled = false;

                rCboAgrupaciones.ClearSelection();
                rCboTipAgrupacionesDato.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboAgrupaciones.Enabled = false;
            rCboTipAgrupacionesDato.Enabled = false;
            if (Request.QueryString["agrDatoCve"] != null && Request.QueryString["agrDatoCve"] != "")
            {
                rCboTipAgrupacionesDato.ClearSelection();
            }
            if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
            {
                rCboAgrupaciones.ClearSelection();
            }
            rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvListaP_Agrupaciones.AllowMultiRowSelection = true;
            hdfBtnAccion.Value = "";

        }


    }




    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvListaP_Agrupaciones.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvListaP_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvListaP_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rCboAgrupaciones.ClearSelection();
            rCboTipAgrupacionesDato.ClearSelection();
        }
        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvListaP_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvListaP_Agrupaciones.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;
            rCboTipAgrupacionesDato.BorderColor = System.Drawing.Color.Transparent;

            rCboAgrupaciones.Enabled = false;
            rCboTipAgrupacionesDato.Enabled = false;

            rCboAgrupaciones.ClearSelection();
            rCboTipAgrupacionesDato.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }
        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }




}