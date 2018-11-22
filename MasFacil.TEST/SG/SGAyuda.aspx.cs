using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;

public partial class SG_SGAyuda : System.Web.UI.Page
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
    private string Pag_sConexionAyu;
    private string Pag_sSessionLog;
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
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
       
    }

        protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {
        LlenaGridFiltro();
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

    protected void rCboTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadBinaryImage1.ImageUrl = "";
        this.RadAsyncUpload1.Enabled = true;

    }

    protected void rGdv_Ayuda_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaDatos();
    }
    #endregion
    
    #region METODOS

    
    private void InicioPagina()
    {
        LlenaComboTipo();
        LlenaGrid();
        ControlesAccion();
        RadBinaryImage1.ImageUrl = "";
        rGdv_Ayuda.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Ayuda.AllowMultiRowSelection = true;
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
        Pag_sConexionAyu = Convert.ToString(Session["ConexionAyu"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_Ayuda.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Ayuda.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;
                this.rCboTipo.Enabled = true;
                this.txtDesExt.Disabled = false;
                this.RadAsyncUpload1.Enabled = false;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";
                this.rCboTipo.ClearSelection();
                this.txtDesExt.InnerText = "";
                RadBinaryImage1.ImageUrl = "";
                arregloImagen.Value = "";


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Ayuda.AllowMultiRowSelection = false;

                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;
                this.rCboTipo.Enabled = true;
                this.txtDesExt.Disabled = false;
                this.RadAsyncUpload1.Enabled = true;

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
                rGdv_Ayuda.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Ayuda.AllowMultiRowSelection = true;
                rGdv_Ayuda.MasterTableView.ClearSelectedItems();
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                this.rCboTipo.Enabled = false;
                this.txtDesExt.Disabled = true;
                this.RadAsyncUpload1.Enabled = false;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";
                this.rCboTipo.ClearSelection();
                this.txtDesExt.InnerText = "";
                RadBinaryImage1.ImageUrl = "";
                arregloImagen.Value = "";

            }
        }


        if (Result == false)
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rCboTipo.Enabled = false;
            this.txtDesExt.Disabled = true;
            this.RadAsyncUpload1.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rCboTipo.ClearSelection();
            this.txtDesExt.InnerText = "";
            RadBinaryImage1.ImageUrl = "";
            arregloImagen.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Ayuda.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Ayuda, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Ayuda, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rCboTipo.ClearSelection();
            this.txtDesExt.InnerText = "";
            RadBinaryImage1.DataValue = null;
            arregloImagen.Value = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_Ayuda.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_Ayuda.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
            //rCboTipo.CssClass = "cssTxtEnabled";
            //txtDesExt.CssClass = "cssTxtEnabled";
            //rTxtAbr.CssClass = "cssTxtEnabled";

            rTxtCve.Enabled = false;
            rTxtDes.Enabled = true;
            rCboTipo.Enabled = true;
            txtDesExt.Disabled = true;
            RadAsyncUpload1.Enabled = false;

            rTxtCve.Text = "";
            rTxtDes.Text = "";
            rCboTipo.ClearSelection();
            txtDesExt.InnerText = "";
            RadBinaryImage1.DataValue = null;
            arregloImagen.Value = "";


            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

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
                hdfBtnAccion.Value = "";
                InicioPagina();
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

        //NUEVO Y MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //CLAVE
            if (rTxtCve.Text.Trim() == "")
            {
                rTxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtCve.CssClass = "cssTxtEnabled"; }

            //DESCRIPCION 
            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            //DESCRIPCION EXTENDIDA
            //if (txtDesExt.InnerText.Trim() == "")
            //{
            //    txtDesExt.CO
            //    txtDesExt.BorderColor = Color.Red;
            //    camposInc += 1;
            //}
            //else { 

            //}

            //TIPO
            if (rCboTipo.SelectedValue == "")
            {

                rCboTipo.CssClass = "cssTxtInvalid";
                rCboTipo.BorderWidth = Unit.Pixel(1);
                rCboTipo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            { rCboTipo.BorderColor = System.Drawing.Color.Transparent; }

            //ARCHIVO CARGADO
            if (arregloImagen.Value == "")
            {
                RadBinaryImage1.BorderWidth = Unit.Pixel(1);
                RadBinaryImage1.BorderColor = Color.Red;

                camposInc += 1;
            }
            else
            { RadBinaryImage1.BorderColor = Color.Transparent; }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{

        //    if (rGdv_Ayuda.SelectedItems.Count == 0)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //        return sResult;
        //    }


        //    if (rTxtCve.Text.Trim() == "")
        //    {
        //        rTxtCve.CssClass = "cssTxtInvalid";
        //        camposInc += 1;
        //    }
        //    else { rTxtCve.CssClass = "cssTxtEnabled"; }

        //    if (rTxtDes.Text.Trim() == "")
        //    {
        //        rTxtDes.CssClass = "cssTxtInvalid";
        //        camposInc += 1;
        //    }
        //    else { rTxtDes.CssClass = "cssTxtEnabled"; }

        //    if (camposInc > 0)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        //    }
        //    return sResult;

        //}

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Ayuda.SelectedItems.Count == 0)
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
        if (arregloImagen.Value != "")
        {
            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAAyuda";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text.Trim());
                ProcBD.AgregarParametrosProcedimiento("@maAyuDes", DbType.String, 50, ParameterDirection.Input, rTxtDes.Text);
                ProcBD.AgregarParametrosProcedimiento("@maAyuDesExt", DbType.String, 300, ParameterDirection.Input, txtDesExt.InnerText);
                ProcBD.AgregarParametrosProcedimiento("@maAyuTipCve", DbType.String, 10, ParameterDirection.Input, rCboTipo.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@maAyuFile", DbType.Binary, 0, ParameterDirection.Input, arregloImagen.Value);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
                string sResult = "", sMSGTip = "", sMsg = "";
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    sMsg = ds.Tables[0].Rows[0]["MSG"].ToString();
                    FNDatos.dsMAMensajes(Pag_sConexionLog, sMsg, ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                }
                if (sMSGTip == "1")
                {
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                    arregloImagen.Value = "";
                }
            }
            catch (Exception ex)
            {
                ShowAlert("2",ex.ToString());
            }
            arregloImagen.Value = "";
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

            foreach (GridDataItem i in rGdv_Ayuda.SelectedItems)
            {

                var dataItem = rGdv_Ayuda.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string AyuCve = dataItem.GetDataKeyValue("maAyuCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MAAyuda";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 10, ParameterDirection.Input, AyuCve);
                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            string sResult = "", sMSGTip = "", sMsg = "";

                            sMsg = ds.Tables[0].Rows[0]["MSG"].ToString();
                            FNDatos.dsMAMensajes(Pag_sConexionLog, sMsg, ref sMSGTip, ref sResult);




                            EstatusItemsElim = sMSGTip.ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = sResult.ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = sResult.ToString();
                            }

                            MsgItemsElim = sResult.ToString();

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
                    //InicioPagina();
                }
                else
                {
                    //InicioPagina();
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
                    //InicioPagina();
                }
                else
                {
                    //InicioPagina();
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

        //===> CONTROLES GENERAL rGdv_Ayuda
        this.rGdv_Ayuda.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        rCboTipo.BorderColor = System.Drawing.Color.Transparent;
        RadBinaryImage1.BorderColor = Color.Transparent;

        this.rTxtCve.Enabled = false;

        this.rTxtDes.Enabled = false;
        this.rCboTipo.Enabled = false;
        this.txtDesExt.Disabled = true;
        this.RadAsyncUpload1.Enabled = false;

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
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rCboTipo.Enabled = false;
            this.txtDesExt.Disabled = true;
            this.RadAsyncUpload1.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rCboTipo.ClearSelection();
            this.txtDesExt.InnerText = "";
            RadBinaryImage1.DataValue = null;
            arregloImagen.Value = "";
        }
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    public void CargaDatos()
    {
        var dataItem = rGdv_Ayuda.SelectedItems[0] as GridDataItem;
        string maAyuCve = dataItem.GetDataKeyValue("maAyuCve").ToString();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 10, ParameterDirection.Input, maAyuCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);


        if (FnValAdoNet.bDSIsFill(ds))
        {

            rTxtCve.Text = ds.Tables[0].Rows[0]["maAyuCve"].ToString();
            rTxtDes.Text = ds.Tables[0].Rows[0]["maAyuDes"].ToString();
            txtDesExt.InnerText = ds.Tables[0].Rows[0]["maAyuDesExt"].ToString();
            rCboTipo.SelectedValue = ds.Tables[0].Rows[0]["maAyuTipCve"].ToString();

            if (ds.Tables[0].Rows[0]["maAyuFile"].ToString() != null)
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maAyuFile"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                if (rCboTipo.SelectedValue == "AYUIMG")
                {
                    RadBinaryImage1.ImageUrl = "data:image/png;base64," + base64String;
                }
                if (rCboTipo.SelectedValue == "AYUPDF")
                {
                    RadBinaryImage1.ImageUrl = "~/Imagenes/IcoAyuda/icoPdf.png";
                }
                if (rCboTipo.SelectedValue == "AYUVID")
                {
                    RadBinaryImage1.ImageUrl = "~/Imagenes/IcoAyuda/icoVideo.png";
                }

                arregloImagen.Value = base64String;


                //sourceid.Attributes["src"] = "data:video/mp4;base64," + base64String;
                //sourceid.DataBind();

            }
            else
            {

            }

        }
        else
        {
            //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
        }

    }

    private void LlenaComboTipo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyudaTipos";
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        FnCtlsFillIn.RadComboBox(ref this.rCboTipo, ds, "maAyuTipCve", "maAyuTipAbr", false, false);
        ((Literal)rCboTipo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipo.Items.Count);

    }

    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        if (rCboTipo.SelectedValue == "AYUIMG")
        {
            ValImagen(e);
        }
        else if (rCboTipo.SelectedValue == "AYUPDF")
        {
            ValPdf(e);
        }
        else if (rCboTipo.SelectedValue == "AYUVID")
        {
            ValVideo(e);
        }
    }


    private void ValImagen(FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png" || ext == ".JPG")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);

                RadBinaryImage1.DataValue = data;
                string valor = Convert.ToBase64String(data);
                arregloImagen.Value = valor;
                Session["SessionImgLogo"] = valor;
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);

                Session["SessionImgLogo"] = "";
            }
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }

    }

    private void ValPdf(FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".pdf" || ext == ".PDF")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                RadBinaryImage1.ImageUrl = "~/Imagenes/IcoMGMMenu/IcoMenuExportarPDF.png";
                string valor = Convert.ToBase64String(data);
                arregloImagen.Value = valor;
                Session["SessionImgLogo"] = valor;
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);

                Session["SessionImgLogo"] = "";
            }
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }


    }

    private void ValVideo(FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".mp4" || ext == ".MP4" || ext == ".avi" || ext == ".AVI" || ext == ".mpeg" || ext == ".MPEG" || ext == ".wmv" || ext == ".WMV" || ext == ".mov" || ext == ".MOV")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                RadBinaryImage1.ImageUrl = "~/Imagenes/IcoAyuda/icoVideo.png";
                string valor = Convert.ToBase64String(data);
                arregloImagen.Value = valor;
                Session["SessionImgLogo"] = valor;
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);
                Session["SessionImgLogo"] = "";
            }
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }
    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref rGdv_Ayuda, ds);
        }

    }
    private void LlenaGridFiltro()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@Filtro", DbType.String, 50, ParameterDirection.Input, rtxtFiltro.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref rGdv_Ayuda, ds);
        }
    }


    #endregion

    
}