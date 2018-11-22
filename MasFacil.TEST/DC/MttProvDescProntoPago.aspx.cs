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


public partial class DC_MttProvDescProntoPago : System.Web.UI.Page
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
    private string PagLoc_ArtCve;
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
    protected void rGdv_Almacenes_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdv_Descuento.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
                //this.rTxtDe.Enabled = false;//quitarlo para que no aparesca abilitado
                //this.rTxtDesc.Enabled = true;
                //this.rTxtA.Enabled = false;

                rTxtDe.Text = dataItem["provDPPLimInf"].Text;
                rTxtA.Text = dataItem["provDPPLimSup"].Text;
                rTxtDesc.Text = dataItem["provDPPPorc"].Text;
            //}


        }
    }


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


    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_ArtCve = Convert.ToString(Session["folio_Selection"]);
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";

        ControlesAccion();
        LlenaGridDescuento();
        rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Descuento.AllowMultiRowSelection = true;

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
    //    rGdv_Descuento.MasterTableView.ClearSelectedItems();
    //    this.rTxtDe.Text = "";
    //    this.rTxtDesc.Text = "";
    //    this.rTxtA.Text = "";
    //    this.rTxtDe.Text = "0";
    //    this.rTxtDesc.Text = "0";
    //    this.rTxtA.Text = "0";
    //    rTxtDe.CssClass = "cssTxtEnabled";
    //    rTxtA.CssClass = "cssTxtEnabled";
    //    rTxtDesc.CssClass = "cssTxtEnabled";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    //===> CONTROLES POR ACCION

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rTxtDe.Enabled = true;
    //        this.rTxtDesc.Enabled = true;
    //        this.rTxtA.Enabled = true;
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        this.rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdv_Descuento.AllowMultiRowSelection = false;
    //        this.rTxtDe.Enabled = false;
    //        this.rTxtDesc.Enabled = false;
    //        this.rTxtA.Enabled = false;
    //    }

    //    //ELIMIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        this.rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdv_Descuento.AllowMultiRowSelection = true;
    //        this.rTxtDe.Enabled = false;
    //        this.rTxtDesc.Enabled = false;
    //        this.rTxtA.Enabled = false;
    //    }

    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        this.rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rTxtDe.Enabled = false;
    //        this.rTxtA.Enabled = false;
    //        this.rTxtDesc.Enabled = false;
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
    //    else {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }

    //}
    private void LlenaGridDescuento()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ProveedorDescProntoPago";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_ArtCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Descuento, ds);
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

    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ProveedorDescProntoPago";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
 
                ProcBD.AgregarParametrosProcedimiento("@provDPPLimInf", DbType.Decimal, 15, ParameterDirection.Input, rTxtDe.Text.Trim());
                ProcBD.AgregarParametrosProcedimiento("@provDPPLimSup", DbType.Decimal, 15, ParameterDirection.Input, rTxtA.Text.Trim());

            ProcBD.AgregarParametrosProcedimiento("@provDPPPorc", DbType.Decimal, 15, ParameterDirection.Input, rTxtDesc.Text.Trim());
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




            foreach (GridDataItem i in rGdv_Descuento.SelectedItems)
            {

                var dataItem = rGdv_Descuento.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    //string sAlmCve = dataItem["provCve"].Text;
                    string rTxtDe = dataItem["provDPPLimInf"].Text;
                    string rTxtA = dataItem["provDPPLimSup"].Text;
                    string rTxtDesc = dataItem["provDPPPorc"].Text;
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ProveedorDescProntoPago";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_ArtCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@provDPPLimInf", DbType.Decimal, 0, ParameterDirection.Input, dataItem["provDPPLimInf"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@provDPPLimSup", DbType.Decimal, 0, ParameterDirection.Input, dataItem["provDPPLimSup"].Text);
                        //ProcBD.AgregarParametrosProcedimiento("@provDPPPorc", DbType.Decimal, 15, ParameterDirection.Input, sAlmCve);
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
                else {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else {
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
                else {
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

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


            if (rTxtDe.Text.Trim() == "")
            {
                rTxtDe.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDe.CssClass = "cssTxtEnabled"; }

            if (rTxtDe.Text.Trim() == "0")
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1007", ref sMSGTip, ref sResult);
                return sResult;
            }
            else { rTxtDe.CssClass = "cssTxtEnabled"; }
            if (rTxtA.Text.Trim() == "")
            {
                rTxtA.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtA.CssClass = "cssTxtEnabled"; }
            if (rTxtA.Text.Trim() == "0")
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1007", ref sMSGTip, ref sResult);
                return sResult;
            }
            else { rTxtA.CssClass = "cssTxtEnabled"; }

            if (rTxtDesc.Text.Trim() == "")
            {
                rTxtDesc.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDesc.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_Descuento.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtDe.Text.Trim() == "")
            {
                rTxtDe.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtA.Text.Trim() == "")
            {
                rTxtA.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtA.CssClass = "cssTxtEnabled"; }

            if (rTxtDesc.Text.Trim() == "")
            {
                rTxtDesc.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDesc.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Descuento.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            return sResult;
        }




        return sResult;
    }

    #endregion



    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdv_Descuento
        rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Descuento.AllowMultiRowSelection = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtDe.CssClass = "cssTxtEnabled";
        rTxtA.CssClass = "cssTxtEnabled";
        rTxtDesc.CssClass = "cssTxtEnabled";

        this.rTxtDe.Enabled = false;
        this.rTxtA.Enabled = false;
        this.rTxtDesc.Enabled = false;

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
            this.rTxtDe.Enabled = false;
            this.rTxtA.Enabled = false;
            this.rTxtDesc.Enabled = false;

            this.rTxtDe.Text = "";
            this.rTxtA.Text = "";
            this.rTxtDesc.Text = "";
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
                this.rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Descuento.MasterTableView.ClearSelectedItems();

                this.rTxtDe.Enabled = true;
                this.rTxtA.Enabled = true;
                this.rTxtDesc.Enabled = true;

                this.rTxtDe.Text = "";
                this.rTxtA.Text = "";
                this.rTxtDesc.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Descuento.AllowMultiRowSelection = false;
                this.rTxtDe.Enabled = false;
                this.rTxtA.Enabled = true;
                this.rTxtDesc.Enabled = true;
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
                rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Descuento.AllowMultiRowSelection = true;
                rGdv_Descuento.MasterTableView.ClearSelectedItems();

                this.rTxtDe.Enabled = false;
                this.rTxtA.Enabled = false;
                this.rTxtDesc.Enabled = false;

                this.rTxtDe.Text = "";
                this.rTxtA.Text = "";
                this.rTxtDesc.Text = "";
            }
        }


        if (Result == false)
        {
            this.rTxtDe.Enabled = false;
            this.rTxtA.Enabled = false;
            this.rTxtDesc.Enabled = false;

            this.rTxtDe.Text = "";
            this.rTxtA.Text = "";
            this.rTxtDesc.Text = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Descuento.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Descuento, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Descuento, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Descuento.AllowMultiRowSelection = true;
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            this.rTxtDe.Text = "";
            this.rTxtA.Text = "";
            this.rTxtDesc.Text = "";
            rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = false;
            rGdv_Descuento.AllowMultiRowSelection = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_Descuento.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_Descuento.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtDe.CssClass = "cssTxtEnabled";
            rTxtA.CssClass = "cssTxtEnabled";
            rTxtDesc.CssClass = "cssTxtEnabled";

            rTxtDe.Enabled = false;
            rTxtA.Enabled = false;
            rTxtDesc.Enabled = false;

            this.rTxtDe.Text = "";
            this.rTxtA.Text = "";
            this.rTxtDesc.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            hdfBtnAccion.Value = "";
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }


}



