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

public partial class DC_MttoSituacionesCrediticias : System.Web.UI.Page
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
    private string PagLoc_folio_Selection;
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
        //LimpiarUi();
        //ControlesAccion();
        EjecutaAccionLimpiar();
    }



    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }


    //=====> EVENTOS OTROS
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {

    }


    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
    }
    private void InicioPagina()
    {
        
        hdfBtnAccion.Value = "";
        ControlesAccion();
            
        //Botones Normales
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        loadGrid();
        rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_SituacionCred.AllowMultiRowSelection = true;
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
    private void LimpiarUi()
    {

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            rTxtSitCreCve.Text = "";
            rTxtSitCreDes.Text = "";
            rTxtSitCreAbr.Text = "";

            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;

            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;

        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rTxtSitCreCve.Text = "";
            rTxtSitCreDes.Text = "";
            rTxtSitCreAbr.Text = "";

            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;

            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;

            rGdv_SituacionCred.MasterTableView.ClearSelectedItems();
        }
    }


    private void loadGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SituacionCrediticia";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdv_SituacionCred, ds);
    }

    private void spSeguridadAlmacen()
    {
        if (rGdv_SituacionCred.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_SituacionCred.Items.Count; i++)
            {

                try
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rTxtSitCreCve.Text);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rGdv_SituacionCred.Items[i].Cells[3].Text.Trim());
                    ProcBD.AgregarParametrosProcedimiento("@seAINivAut", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@secMaUsu", DbType.Int64, 0, ParameterDirection.Input, i);

                    if (rGdv_SituacionCred.Items[i].Selected == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@isSelected", DbType.Int64, 0, ParameterDirection.Input, 1);
                    }

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds.Tables.Count > 0)
                    {

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }

            }
        }
    }

    //private void ControlesAccion()
    //{
    //    //===> CONTROLES GENERAL

    //    rTxtSitCreCve.CssClass = "cssTxtEnabled";
    //    rTxtSitCreDes.CssClass = "cssTxtEnabled";
    //    rTxtSitCreAbr.CssClass = "cssTxtEnabled";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


    //    //===> CONTROLES POR ACCION

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        this.rTxtSitCreCve.Text = "";
    //        this.rTxtSitCreAbr.Text = "";
    //        this.rTxtSitCreDes.Text = "";
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

    //        this.rTxtSitCreCve.Enabled = true;
    //        this.rTxtSitCreAbr.Enabled = true;
    //        this.rTxtSitCreDes.Enabled = true;
            
    //        rBtnApliClie.Enabled = true;
    //        rBtnApliPart.Enabled = true;
    //        rBtnCaliNor.Enabled = true;
    //        rBtnCaliRes.Enabled = true;
            
    //        rGdv_SituacionCred.MasterTableView.ClearSelectedItems();
    //        rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = false;
    //        for (int i = 0; i < rGdv_SituacionCred.Items.Count; i++)
    //        {
    //            rGdv_SituacionCred.Items[i].SelectableMode = GridItemSelectableMode.None;
    //        }
    //        rGdv_SituacionCred.AllowMultiRowSelection = false;

    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {

    //        this.rTxtSitCreCve.Text = "";
    //        this.rTxtSitCreAbr.Text = "";
    //        this.rTxtSitCreDes.Text = "";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        this.rTxtSitCreCve.Enabled = false;
    //        this.rTxtSitCreAbr.Enabled = true;
    //        this.rTxtSitCreDes.Enabled = true;

    //        rBtnApliClie.Enabled = true;
    //        rBtnApliPart.Enabled = true;
    //        rBtnCaliNor.Enabled = true;
    //        rBtnCaliRes.Enabled = true;


    //        rGdv_SituacionCred.MasterTableView.ClearSelectedItems();
    //        rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
    //        for (int i = 0; i < rGdv_SituacionCred.Items.Count; i++)
    //        {
    //            rGdv_SituacionCred.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //        }

    //        rGdv_SituacionCred.AllowMultiRowSelection = false;
    //    }

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {


    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        this.rTxtSitCreCve.Enabled = false;
    //        this.rTxtSitCreAbr.Enabled = false;
    //        this.rTxtSitCreDes.Enabled = false;

    //        rBtnApliClie.Enabled = false;
    //        rBtnApliPart.Enabled = false;
    //        rBtnCaliNor.Enabled = false;
    //        rBtnCaliRes.Enabled = false;


    //        rGdv_SituacionCred.MasterTableView.ClearSelectedItems();
    //        rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
    //        for (int i = 0; i < rGdv_SituacionCred.Items.Count; i++)
    //        {
    //            rGdv_SituacionCred.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //        }
    //        rGdv_SituacionCred.AllowMultiRowSelection = true;


    //    }



    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {

    //        this.rTxtSitCreCve.Text = "";
    //        this.rTxtSitCreAbr.Text = "";
    //        this.rTxtSitCreDes.Text = "";

    //        this.rTxtSitCreCve.Enabled = false;
    //        this.rTxtSitCreDes.Enabled = false;
    //        this.rTxtSitCreAbr.Enabled = false;
            

    //    }
    //    else
    //    {
    //        //Datos_folio_Selection();
    //    }

    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //   )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //    if (hdfBtnAccion.Value == "")
    //    {

    //        this.rTxtSitCreCve.Text = "";
    //        this.rTxtSitCreAbr.Text = "";
    //        this.rTxtSitCreDes.Text = "";

    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //        this.rTxtSitCreCve.Enabled = false;
    //        this.rTxtSitCreAbr.Enabled = false;
    //        this.rTxtSitCreDes.Enabled = false;
    //        rBtnApliClie.Enabled = false;
    //        rBtnApliPart.Enabled = false;
    //        rBtnCaliNor.Enabled = false;
    //        rBtnCaliRes.Enabled = false;

    //        rGdv_SituacionCred.MasterTableView.ClearSelectedItems();
    //        rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = false;
    //    }

    //}

    private void Datos_folio_Selection()
    {
        DataSet ds = new DataSet();
        ds = dsDatosAlmacen();
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rTxtSitCreCve.Text = ds.Tables[0].Rows[0]["almCve"].ToString();
            rTxtSitCreDes.Text = ds.Tables[0].Rows[0]["almDes"].ToString();
            rTxtSitCreAbr.Text = ds.Tables[0].Rows[0]["almAbr"].ToString();
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
                EjecutaSpAcciones();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

            InicioPagina();

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }



    }
    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SituacionCrediticia";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@siCrCve", DbType.String, 10, ParameterDirection.Input, rTxtSitCreCve.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@siCrDes", DbType.String, 50, ParameterDirection.Input, rTxtSitCreDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@siCrAbr", DbType.String, 15, ParameterDirection.Input, rTxtSitCreAbr.Text.Trim());

            if (rBtnApliClie.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@siCrApli", DbType.Int64, 1, ParameterDirection.Input, 1);
            }
            else if (rBtnApliPart.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@siCrApli", DbType.Int64, 1, ParameterDirection.Input, 2);
            }


            if (rBtnCaliNor.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@siCrCali", DbType.Int64, 1, ParameterDirection.Input, 1);
            }
            else if (rBtnCaliRes.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@siCrCali", DbType.Int64, 1, ParameterDirection.Input, 2);
            }
            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                
                ShowAlert(sEjecEstatus, sEjecMSG);
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

            foreach (GridDataItem i in rGdv_SituacionCred.SelectedItems)
            {

                var dataItem = rGdv_SituacionCred.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_SituacionCrediticia";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@siCrCve", DbType.String, 10, ParameterDirection.Input, dataItem.GetDataKeyValue("siCrCve").ToString());
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
                    //LlenaGridAlmacenes();
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
                    sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    sMsgAlert = sMsgAlert + "Registros no eliminados" + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGridAlmacenes();
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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtSitCreCve.Text.Trim() == "")
            {
                rTxtSitCreCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSitCreCve.CssClass = "cssTxtEnabled"; }


            if (rTxtSitCreDes.Text.Trim() == "")
            {
                rTxtSitCreDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSitCreDes.CssClass = "cssTxtEnabled"; }

            if (rTxtSitCreAbr.Text.Trim() == "")
            {
                rTxtSitCreAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSitCreAbr.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdv_SituacionCred.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtSitCreCve.Text.Trim() == "")
            {
                rTxtSitCreCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtSitCreDes.Text.Trim() == "")
            {
                rTxtSitCreDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSitCreDes.CssClass = "cssTxtEnabled"; }

            if (rTxtSitCreAbr.Text.Trim() == "")
            {
                rTxtSitCreAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtSitCreAbr.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //Eliminar
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdv_SituacionCred.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
        }


            return sResult;
    }

    private DataSet dsDatosAlmacen()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Almacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    #endregion
    

    protected void rGdv_SituacionCred_SelectedIndexChanged(object sender, EventArgs e)
    {

        var dataItem = rGdv_SituacionCred.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
                rTxtSitCreCve.Text = dataItem["siCrCve"].Text;
                rTxtSitCreDes.Text = dataItem["siCrDes"].Text;
                rTxtSitCreAbr.Text = dataItem["siCrAbr"].Text;

                if (dataItem["desApli"].Text.Trim() == "A Nivel Cliente")
                {
                    rBtnApliClie.Checked = true;
                    rBtnApliPart.Checked = false;
                }
                else if (dataItem["desApli"].Text.Trim() == "A Nivel Partida")
                {
                    rBtnApliClie.Checked = false;
                    rBtnApliPart.Checked = true;
                }

                if (dataItem["desCali"].Text.Trim() == "Situación Normal")
                {
                    rBtnCaliNor.Checked = true;
                    rBtnCaliRes.Checked = false;
                }
                else if (dataItem["desCali"].Text.Trim() == "Situación Restrictiva")
                {
                    rBtnCaliNor.Checked = false;
                    rBtnCaliRes.Checked = true;
                }


           // }
        }
        
    }



    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL 
        this.rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtSitCreCve.CssClass = "cssTxtEnabled";
        rTxtSitCreDes.CssClass = "cssTxtEnabled";
        rTxtSitCreAbr.CssClass = "cssTxtEnabled";
        

        this.rTxtSitCreCve.Enabled = false;
        this.rTxtSitCreDes.Enabled = false;
        this.rTxtSitCreAbr.Enabled = false;
        rBtnApliClie.Enabled = false;
        rBtnApliPart.Enabled = false;
        rBtnCaliNor.Enabled = false;
        rBtnCaliRes.Enabled = false;

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
            this.rTxtSitCreCve.Enabled = false;
            this.rTxtSitCreDes.Enabled = false;
            this.rTxtSitCreAbr.Enabled = false;
            rBtnApliClie.Enabled = false;
            rBtnApliPart.Enabled = false;
            rBtnCaliNor.Enabled = false;
            rBtnCaliRes.Enabled = false;

            this.rTxtSitCreCve.Text = "";
            this.rTxtSitCreDes.Text = "";
            this.rTxtSitCreAbr.Text = "";
            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;
            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;
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
                this.rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_SituacionCred.MasterTableView.ClearSelectedItems();

                this.rTxtSitCreCve.Enabled = true;
                this.rTxtSitCreDes.Enabled = true;
                this.rTxtSitCreAbr.Enabled = true;
                rBtnApliClie.Enabled = true;
                rBtnApliPart.Enabled = true;
                rBtnCaliNor.Enabled = true;
                rBtnCaliRes.Enabled = true;

                this.rTxtSitCreCve.Text = "";
                this.rTxtSitCreDes.Text = "";
                this.rTxtSitCreAbr.Text = "";
                rBtnApliClie.Checked = true;
                rBtnApliPart.Checked = false;
                rBtnCaliNor.Checked = true;
                rBtnCaliRes.Checked = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_SituacionCred.AllowMultiRowSelection = false;

                this.rTxtSitCreCve.Enabled = false;
                this.rTxtSitCreDes.Enabled = true;
                this.rTxtSitCreAbr.Enabled = true;
                rBtnApliClie.Enabled = true;
                rBtnApliPart.Enabled = true;
                rBtnCaliNor.Enabled = true;
                rBtnCaliRes.Enabled = true;

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
                rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_SituacionCred.AllowMultiRowSelection = true;
                rGdv_SituacionCred.MasterTableView.ClearSelectedItems();

                this.rTxtSitCreCve.Enabled = false;
                this.rTxtSitCreDes.Enabled = false;
                this.rTxtSitCreAbr.Enabled = false;
                rBtnApliClie.Enabled = false;
                rBtnApliPart.Enabled = false;
                rBtnCaliNor.Enabled = false;
                rBtnCaliRes.Enabled = false;

                this.rTxtSitCreCve.Text = "";
                this.rTxtSitCreDes.Text = "";
                this.rTxtSitCreAbr.Text = "";
                rBtnApliClie.Checked = true;
                rBtnApliPart.Checked = false;
                rBtnCaliNor.Checked = true;
                rBtnCaliRes.Checked = false;
            }
        }


        if (Result == false)
        {
            this.rTxtSitCreCve.Enabled = false;
            this.rTxtSitCreDes.Enabled = false;
            this.rTxtSitCreAbr.Enabled = false;
            rBtnApliClie.Enabled = false;
            rBtnApliPart.Enabled = false;
            rBtnCaliNor.Enabled = false;
            rBtnCaliRes.Enabled = false;

            this.rTxtSitCreCve.Text = "";
            this.rTxtSitCreDes.Text = "";
            this.rTxtSitCreAbr.Text = "";
            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;
            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;

            rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_SituacionCred.AllowMultiRowSelection = true;
            hdfBtnAccion.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_SituacionCred.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_SituacionCred, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_SituacionCred, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rTxtSitCreCve.Text = "";
            this.rTxtSitCreDes.Text = "";
            this.rTxtSitCreAbr.Text = "";
            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;
            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_SituacionCred.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtSitCreCve.CssClass = "cssTxtEnabled";
            rTxtSitCreDes.CssClass = "cssTxtEnabled";
            rTxtSitCreAbr.CssClass = "cssTxtEnabled";
           

            rTxtSitCreCve.Enabled = false;
            rTxtSitCreDes.Enabled = false;
            rTxtSitCreAbr.Enabled = false;
            rBtnApliClie.Enabled = false;
            rBtnApliPart.Enabled = false;
            rBtnCaliNor.Enabled = false;
            rBtnCaliRes.Enabled = false;

            rTxtSitCreCve.Text = "";
            rTxtSitCreDes.Text = "";
            rTxtSitCreAbr.Text = "";
            rBtnApliClie.Checked = true;
            rBtnApliPart.Checked = false;
            rBtnCaliNor.Checked = true;
            rBtnCaliRes.Checked = false;
            rGdv_SituacionCred.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_SituacionCred.AllowMultiRowSelection = true;
            hdfBtnAccion.Value = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }




}