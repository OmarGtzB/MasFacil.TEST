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

public partial class DC_MttoDescuentoListaP : System.Web.UI.Page
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
    private string Pag_docCve;
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
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
            rCboDocumento.Enabled = false;
            rCboListaPrecio.Enabled = true;
            rCboListaPrecio.ClearSelection();

        }
        else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            Pag_lisPreCve = Request.QueryString["lisPreCve"];
            rCboDocumento.Enabled = true;
            rCboListaPrecio.Enabled = false;
            rCboDocumento.ClearSelection();
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
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            rCboListaPrecio.ClearSelection();
            rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;
            rCboListaPrecio.Enabled = false;
        }
        else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento.Enabled = false;
        }
        else
        {
            rCboListaPrecio.ClearSelection();
            rCboDocumento.ClearSelection();
            rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();
            ControlesAccion();
        }
        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_ListaP.AllowMultiRowSelection = true;
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            rCboListaPrecio.ClearSelection();
            rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;

        }
        else if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        }
        else
        {
            rCboListaPrecio.ClearSelection();
            rCboDocumento.ClearSelection();
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
            Response.Redirect("~/DC/MttoListaPrecios.aspx");
        }
        else
        {
            InicioPagina();
            rGdvDocumento_ListaP.Enabled = true;
            rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();
            rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDocumento_ListaP.AllowMultiRowSelection = false;
        }
    }

    protected void rGdvDocumento_ListaP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == "")
        {
            var dataItem = rGdvDocumento_ListaP.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                rCboListaPrecio.SelectedValue = dataItem["lisPreCve"].Text;
                rCboDocumento.SelectedValue = dataItem["docCve"].Text;
            }
        }
    }
    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
        }
        if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
        {
            Pag_lisPreCve = Request.QueryString["lisPreCve"];
        }
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false);
        FnCtlsFillIn.RadComboBox_ListaPrecios(Pag_sConexionLog, Pag_sCompania, ref rCboListaPrecio, true, false);
        ControlesAccion();
        LlenaGrid();
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            rCboDocumento.SelectedValue = Pag_docCve;
            rCboDocumento.Enabled = false;
        }
        if (Pag_lisPreCve != "" && Pag_lisPreCve != null)
        {
            rCboListaPrecio.SelectedValue = Pag_lisPreCve;
            rCboListaPrecio.Enabled = false;
            rBtnCancelar.Enabled = true;
            rGdvDocumento_ListaP.Width = System.Web.UI.WebControls.Unit.Pixel(802);
        }
        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_ListaP.AllowMultiRowSelection = true;
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
    //    rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();

    //    rCboListaPrecio.ClearSelection();
    //    rCboDocumento.ClearSelection();
    //    rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
    //    rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;
    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
    //    rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
    //    rGdvDocumento_ListaP.AllowMultiRowSelection = false;

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = false;
    //        rCboDocumento.Enabled = true;
    //        rCboListaPrecio.Enabled = true;
    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvDocumento_ListaP.AllowMultiRowSelection = true;
    //        rGdvDocumento_ListaP.Enabled = false;
    //        rCboDocumento.Enabled = false;
    //        rCboListaPrecio.Enabled = false;
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvDocumento_ListaP.AllowMultiRowSelection = true;
    //        rCboDocumento.Enabled = false;
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


            if (rCboDocumento.SelectedValue == "")
            {
                rCboDocumento.BorderWidth = Unit.Pixel(1);
                rCboDocumento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboDocumento.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvDocumento_ListaP.SelectedItems.Count == 0)
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
            ProcBD.NombreProcedimiento = "sp_Documento_ListaP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboListaPrecio.SelectedValue));

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


            foreach (GridDataItem i in rGdvDocumento_ListaP.SelectedItems)
            {

                var dataItem = rGdvDocumento_ListaP.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Documento_ListaP";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(dataItem["lisPreCve"].Text));

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
        ProcBD.NombreProcedimiento = "sp_Documento_ListaP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        if (Pag_lisPreCve != "" && Pag_lisPreCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, Pag_lisPreCve);
        }
        //else
        //{
        //    ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int32, 0, ParameterDirection.Input, null);
        //}

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDocumento_ListaP, ds);

    }
    private void OpcNuevo()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_ListaP.AllowMultiRowSelection = false;
        rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();
        rGdvDocumento_ListaP.Enabled = false;
        rGdvDocumento_ListaP.MasterTableView.Enabled = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = false;

    }

    private void OpcEliminar()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_ListaP.AllowMultiRowSelection = true;
        rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rCboListaPrecio
        this.rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        rCboListaPrecio.BorderColor = System.Drawing.Color.Transparent;

        rCboDocumento.Enabled = false;
        rCboListaPrecio.Enabled = false;

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
            rCboDocumento.Enabled = false;
            rCboListaPrecio.Enabled = false;

            if (Request.QueryString["lisPreCve"] != null && Request.QueryString["lisPreCve"] != "")
            {
                rCboListaPrecio.ClearSelection();
            }
            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboDocumento.ClearSelection();
            }


        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();

                rCboDocumento.Enabled = true;
                rCboListaPrecio.Enabled = true;

                rCboDocumento.ClearSelection();
                rCboListaPrecio.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            //    rGdvDocumento_ListaP.AllowMultiRowSelection = false;
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
                rGdvDocumento_ListaP.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDocumento_ListaP.AllowMultiRowSelection = true;
                rGdvDocumento_ListaP.MasterTableView.ClearSelectedItems();


                rCboDocumento.Enabled = false;
                rCboListaPrecio.Enabled = false;

                rCboDocumento.ClearSelection();
                rCboListaPrecio.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboDocumento.Enabled = false;
            rCboListaPrecio.Enabled = false;
            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboListaPrecio.ClearSelection();
            }
            if (Request.QueryString["impCve"] != null && Request.QueryString["lisPreCve"] != "")
            {
                rCboDocumento.ClearSelection();
            }

        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDocumento_ListaP.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_ListaP, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_ListaP, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }


    #endregion




















}