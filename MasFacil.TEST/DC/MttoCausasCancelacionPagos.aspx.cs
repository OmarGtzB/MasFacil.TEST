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

public partial class DC_MttoCausasCancelacionPagos : System.Web.UI.Page
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

    #endregion

    #region EVENTOS


    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //cleanUi();
        EjecutaAccionLimpiar();
    }


    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rGdv_Condiciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        //conPagDiasPrm = rGdv_Condiciones.SelectedItems[0].
        var dataItem = rGdv_Condiciones.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            rTxtClave.Text = dataItem.Cells[2].Text;
            rTxtAbreviatura.Text = dataItem.Cells[3].Text;
            rTxtDescripcion.Text = dataItem.Cells[4].Text;
        }
    }


    #endregion

    #region METODOS

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
                InicioPagina();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
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

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtAbreviatura.Text.Trim() == "")
            {
                rTxtAbreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbreviatura.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_Condiciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtAbreviatura.Text.Trim() == "")
            {
                rTxtAbreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbreviatura.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Condiciones.SelectedItems.Count == 0)
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
            string conPagDiasPrm = "0";
            //conPagDiasPrm = rGdv_Condiciones.SelectedItems[0].
            if (rGdv_Condiciones.SelectedItems.Count > 0)
            {
                var dataItem = rGdv_Condiciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    //conPagDiasPrm = dataItem.GetDataKeyValue("conPagDias").ToString();asdfghj
                    conPagDiasPrm = dataItem["cauCancPagCve"].Text;
                }
            }



            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CausasCancelacionPagos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cauCancPagCve", DbType.String, 10, ParameterDirection.Input, rTxtClave.Text);
            ProcBD.AgregarParametrosProcedimiento("@cauCancPagDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@cauCancPagAbr", DbType.String, 20, ParameterDirection.Input, rTxtAbreviatura.Text);

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




            foreach (GridDataItem i in rGdv_Condiciones.SelectedItems)
            {

                var dataItem = rGdv_Condiciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    //string artEstCodElem = dataItem["artEstCodElem"].Text;

                    string conPagDiasPrm = dataItem["cauCancPagCve"].Text;

                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CausasCancelacionPagos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cauCancPagCve", DbType.String, 10, ParameterDirection.Input, conPagDiasPrm);
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
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        //Imagenes default botones
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        LlenaGrid();
        rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Condiciones.AllowMultiRowSelection = true;
        ControlesAccion();
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

    //    rGdv_Condiciones.MasterTableView.ClearSelectedItems();
    //    rTxtDescripcion.Text = "";
    //    rTxtDescripcion.Enabled = false;
    //    rTxtClave.Text = "";
    //    rTxtClave.Enabled = false;

    //    rTxtAbreviatura.Text = "";
    //    rTxtAbreviatura.Enabled = false;

    //    rBtnGuardar.Enabled = false;
    //    rBtnCancelar.Enabled = false;

    //    rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = false;
    //    rGdv_Condiciones.AllowMultiRowSelection = false;

    //    //===> CONTROLES POR ACCION
    //    // NUEVO 
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdv_Condiciones.AllowMultiRowSelection = false;


    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //        rTxtDescripcion.Enabled = true;
    //        rTxtClave.Enabled = true;
    //        rTxtAbreviatura.Enabled = true;

    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;

    //    }
    //    else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_Condiciones.AllowMultiRowSelection = false;
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //        rTxtDescripcion.Enabled = true;
    //        rTxtClave.Enabled = false;
    //        rTxtAbreviatura.Enabled = true;

    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;

    //    }
    //    else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {

    //        rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_Condiciones.AllowMultiRowSelection = true;
    //        rTxtDescripcion.Enabled = false;
    //        rTxtClave.Enabled = false;
    //        rTxtAbreviatura.Enabled = false;

    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";

    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;

    //    }

    //    else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
    //    {
    //        /*
    //        rTxtDescripcion.Text = "";
    //        rTxtDias.Text = "";
    //        rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = false;
    //        limpiarUi();   
    //     */

    //    }
    //    else
    //    {
    //        //===> Botones GUARDAR - CANCELAR
    //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //           )
    //        {
    //            rBtnGuardar.Enabled = true;
    //            rBtnCancelar.Enabled = true;
    //        }
    //    }


    //}


    #endregion

    #region FUNCIONES


    private void cleanUi()
    {
        rTxtDescripcion.Text = "";
        rTxtClave.Text = "";
        rTxtAbreviatura.Text = "";

        rGdv_Condiciones.MasterTableView.ClearSelectedItems();
    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CausasCancelacionPagos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Condiciones, ds);
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES rTxtAbreviatura
        this.rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";

        this.rTxtClave.Enabled = false;
        this.rTxtDescripcion.Enabled = false;
        this.rTxtAbreviatura.Enabled = false;

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
            this.rTxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            this.rTxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtAbreviatura.Text = "";
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
                this.rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Condiciones.MasterTableView.ClearSelectedItems();

                this.rTxtClave.Enabled = true;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtAbreviatura.Enabled = true;

                this.rTxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtAbreviatura.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Condiciones.AllowMultiRowSelection = false;
                this.rTxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtAbreviatura.Enabled = true;
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
                rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Condiciones.AllowMultiRowSelection = true;
                rGdv_Condiciones.MasterTableView.ClearSelectedItems();
                this.rTxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = false;
                this.rTxtAbreviatura.Enabled = false;
                this.rTxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtAbreviatura.Text = "";
            }
        }


        if (Result == false)
        {
            this.rTxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            this.rTxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtAbreviatura.Text = "";
            hdfBtnAccion.Value = "";
            rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_Condiciones.AllowMultiRowSelection = true;
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Condiciones.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Condiciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Condiciones, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rTxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtAbreviatura.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_Condiciones.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_Condiciones.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtClave.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rTxtAbreviatura.CssClass = "cssTxtEnabled";

            rTxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtAbreviatura.Enabled = false;

            rTxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtAbreviatura.Text = "";
            hdfBtnAccion.Value = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    #endregion











}