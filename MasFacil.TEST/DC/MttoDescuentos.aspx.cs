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
public partial class DC_MttoDescuentos : System.Web.UI.Page
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
    private string Pag_MonedaSelected;

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
        EjecutaAccionLimpiar();

    }
    protected void rBtnDescuentosDoc_Click(object sender, ImageButtonClickEventArgs e)
    {
        string sResult = "", sMSGTip = "";
        if (rGdvDescuento.SelectedItems.Count > 0)
        {

            if (rGdvDescuento.SelectedItems.Count > 1)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);
            }
            else {
                var dataItem = rGdvDescuento.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("DesCve").ToString();
                Session["RawUrl_Return"] = hdfRawUrl.Value;
                Response.Redirect("~/DC/MttoDescuentosDocumentos.aspx?" + "DesCve=" + stransDetId + "&idM=" + Request.QueryString["idM"]);
            }
        }
        else
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
        }
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "";
        InicioPagina();
    }

    protected void rGdvDescuento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EdicionDatos();
        }

    }

    #endregion


    #region METODOS
    private void InicioPagina()
    {
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        LlenarGrid();
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


        rtxtClave.Enabled = false;
        rTxtImporte.Enabled = false;
        rTxtDescripcion.Enabled = false;

        rtxtClave.Text = "";
        rTxtImporte.Text = "";
        rTxtDescripcion.Text = "";

        rtxtClave.CssClass = "cssTxtEnabled";
        rTxtImporte.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rGdvDescuento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDescuento.AllowMultiRowSelection = true;

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
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_MonedaSelected = Convert.ToString(Session["Pag_MonedaSelected"]);
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

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rtxtClave.CssClass = "cssTxtEnabled";
            }

            if (rTxtImporte.Text.Trim() == "")
            {
                rTxtImporte.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtImporte.CssClass = "cssTxtEnabled";
            }


            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtDescripcion.CssClass = "cssTxtEnabled";
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
            //Modificar

            if (rGdvDescuento.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                return sResult;
            }


            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rtxtClave.CssClass = "cssTxtEnabled";
            }

            if (rTxtImporte.Text.Trim() == "")
            {
                rTxtImporte.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtImporte.CssClass = "cssTxtEnabled";
            }


            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtDescripcion.CssClass = "cssTxtEnabled";
            }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvDescuento.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }

    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Descuentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            if (rtxtClave.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rtxtClave.Text);
            }
            if (rTxtImporte.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@DesPorcen", DbType.Decimal, 15, ParameterDirection.Input, rTxtImporte.Text);
            }
            if (rTxtDescripcion.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@DesDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
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
                    hdfBtnAccion.Value = "";
                    InicioPagina();

                }

            }

        }
        catch (Exception ex)
        {

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




            foreach (GridDataItem i in rGdvDescuento.SelectedItems)
            {

                var dataItem = rGdvDescuento.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string DesCve = dataItem.GetDataKeyValue("DesCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();

                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Descuentos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, DesCve);


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
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    hdfBtnAccion.Value = "";
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
    
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvDescuento.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtImporte.CssClass = "cssTxtEnabled";

        this.rtxtClave.Enabled = false;
        this.rTxtDescripcion.Enabled = false;
        this.rTxtImporte.Enabled = false;

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
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtImporte.Enabled = false;

            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtImporte.Text = "";
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
                this.rGdvDescuento.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDescuento.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = true;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtImporte.Enabled = true;

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtImporte.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvDescuento.AllowMultiRowSelection = false;
                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtImporte.Enabled = true;
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
                rGdvDescuento.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDescuento.AllowMultiRowSelection = true;
                rGdvDescuento.MasterTableView.ClearSelectedItems();
                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = false;
                this.rTxtImporte.Enabled = false;
                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtImporte.Text = "";
            }
        }


        if (Result == false)
        {
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtImporte.Enabled = false;
            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtImporte.Text = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDescuento.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDescuento, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDescuento, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtImporte.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvDescuento.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDescuento.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rtxtClave.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rTxtImporte.CssClass = "cssTxtEnabled";

            rtxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtImporte.Enabled = false;

            rtxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtImporte.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    #endregion


    #region FUNCIONES

    private void LlenarGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Descuentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDescuento, ds);
    }

    public void EdicionDatos()
    {
        var dataItem = rGdvDescuento.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            if (dataItem["DesCve"].Text != "&nbsp;")
            {
                rtxtClave.Text = dataItem["DesCve"].Text;
            }
            else
            {
                rtxtClave.Text = "";
            }

            if (dataItem["DesPorcen"].Text.ToString() != "&nbsp;")
            {
                rTxtImporte.Text = dataItem["DesPorcen"].Text.TrimEnd('%');
            }
            else
            {
                rTxtImporte.Text = "";

            }

            if (dataItem["DesDes"].Text != "&nbsp;")
            {
                rTxtDescripcion.Text = dataItem["DesDes"].Text;
            }
            else
            {
                rTxtDescripcion.Text = "";
            }

        }

    }
    #endregion





}