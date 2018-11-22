using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Telerik.Web.UI;

public partial class DC_MttoMetValuacionAlt : System.Web.UI.Page
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
                pagina_inicio();

            }
        }
    }


    //NUEVO//
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();



    }
    //MODIFICAR//
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();


    }

    //ELIMINAR//
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();

    }

    //LIMPIAR//
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //radCbo_Met.BorderWidth = Unit.Pixel(1);
        //radCbo_Met.BorderColor = Color.Transparent;

        //radCbo_Mone.BorderWidth = Unit.Pixel(1);
        //radCbo_Mone.BorderColor = Color.Transparent;
        //ControlesAccion();

        EjecutaAccionLimpiar();
    }

    protected void radGrid_metVal_SelectedIndexChanged(object sender, EventArgs e)
    {

        rad_to_combo();

    }


    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
        //enviar_metVal();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        pagina_inicio();
    }
    #endregion

    #region METODOS
    public void pagina_inicio()
    {
        llenar_txt_alt();
        llena_comboMet();
        llena_comboMone();
        llena_radMetVal();
        hdfBtnAccion.Value = "";
        ControlesAccion();

        radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;
        radGrid_metVal.AllowMultiRowSelection = true;
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
        PagLoc_ArtCve = Convert.ToString(Session["folio_Selection"]);

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
                enviar_metVal();
                pagina_inicio();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;
                radGrid_metVal.AllowMultiRowSelection = true;
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

        // NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (radCbo_Met.Text.Trim() == "")
            {
                radCbo_Met.BorderWidth = Unit.Pixel(1);
                radCbo_Met.BorderColor = Color.Red;

                radCbo_Met.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                radCbo_Met.BorderColor = Color.Transparent;
            }

            if (radCbo_Mone.Text.Trim() == "")
            {

                radCbo_Mone.BorderWidth = Unit.Pixel(1);
                radCbo_Mone.BorderColor = Color.Red;


                radCbo_Mone.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                radCbo_Mone.BorderColor = Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (radGrid_metVal.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (radCbo_Met.Text.Trim() == "")
            {
                radCbo_Met.BorderWidth = Unit.Pixel(1);
                radCbo_Met.BorderColor = Color.Red;



                radCbo_Met.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (radCbo_Mone.Text.Trim() == "")
            {

                radCbo_Mone.BorderWidth = Unit.Pixel(1);
                radCbo_Mone.BorderColor = Color.Red;


                radCbo_Mone.CssClass = "cssTxtInvalid";
                camposInc += 1;
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

            if (radGrid_metVal.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }

    public void limpiar()
    {
        limpiar_cbo();
        llena_radMetVal();
        rBtnCancelar.Enabled = false;
        rBtnGuardar.Enabled = false;
        radCbo_Met.Enabled = false;
        radCbo_Mone.Enabled = false;
        this.radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = false;


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

            foreach (GridDataItem i in radGrid_metVal.SelectedItems)
            {

                var dataItem = radGrid_metVal.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string sValMetCve = dataItem["metValId"].Text;
                    string sValMonCve = dataItem["monCve"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ArticuloMetodosValuacion";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
                        ProcBD.AgregarParametrosProcedimiento("@metValId", DbType.Int64, 0, ParameterDirection.Input, sValMetCve);
                        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, (2), ParameterDirection.Input, sValMonCve);
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
                    pagina_inicio();
                }
                else
                {
                    llena_radMetVal();
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
                    pagina_inicio();
                }
                else
                {
                    llena_radMetVal();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        llena_radMetVal();
        this.radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = false;


    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL radGrid_metVal
        this.radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        radCbo_Met.BorderColor = Color.Transparent;
        radCbo_Mone.BorderColor = Color.Transparent;

        radCbo_Met.Enabled = false;
        radCbo_Mone.Enabled = false;

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

            radCbo_Met.Enabled = false;
            radCbo_Mone.Enabled = false;

            //limpiar_cbo();7radCbo_Met
            radCbo_Met.ClearSelection();
            radCbo_Mone.ClearSelection();
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
                this.radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = false;
                radGrid_metVal.MasterTableView.ClearSelectedItems();

                radCbo_Met.Enabled = true;
                radCbo_Mone.Enabled = true;

                radCbo_Met.ClearSelection();
                radCbo_Mone.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            //    radGrid_metVal.AllowMultiRowSelection = false;
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
                radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;
                radGrid_metVal.AllowMultiRowSelection = true;
                radGrid_metVal.MasterTableView.ClearSelectedItems();

                radCbo_Met.Enabled = false;
                radCbo_Mone.Enabled = false;

                radCbo_Met.ClearSelection();
                radCbo_Mone.ClearSelection();
            }
        }


        if (Result == false)
        {
            radCbo_Met.Enabled = false;
            radCbo_Mone.Enabled = false;

            radCbo_Met.ClearSelection();
            radCbo_Mone.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = radGrid_metVal.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, radGrid_metVal, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, radGrid_metVal, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {

        //radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;
        //radGrid_metVal.AllowMultiRowSelection = true;
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            radCbo_Met.ClearSelection();
            radCbo_Mone.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            radGrid_metVal.ClientSettings.Selecting.AllowRowSelect = true;

            radGrid_metVal.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            radCbo_Met.BorderColor = Color.Transparent;
            radCbo_Mone.BorderColor = Color.Transparent;

            radCbo_Met.Enabled = false;
            radCbo_Mone.Enabled = false;

            radCbo_Met.ClearSelection();
            radCbo_Mone.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    public void llenar_txt_alt()
    {

        if (PagLoc_ArtCve == "")
        {
            lbl_codigArt.Text = "";
            lbl_descripArt.Text = "";
            radCbo_Met.Text = "-1";
            radCbo_Met.SelectedValue = "";
            radCbo_Mone.Text = "-1";
            radCbo_Mone.SelectedValue = "";

        }
        else
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Articulos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            lbl_codigArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artCve"]);
            lbl_descripArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artDes"]);
            //lbl_UniMed_Art.Text = Convert.ToString(ds.Tables[0].Rows[0]["artDesExt"]);
            //lbl_uni_MedDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["uniMedDes"]);
        }
    }

    public void llena_comboMet()
    {

        FnCtlsFillIn.RadComboBox_MetVal(Pag_sConexionLog, Pag_sCompania, ref radCbo_Met, true, false);
    }

    public void llena_comboMone()
    {
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref radCbo_Mone, true, false);
    }

    public void llena_radMetVal()
    {
        //GridView ds = new GridView();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloMetodosValuacion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 6);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref radGrid_metVal, ds);

    }

    public void enviar_metVal()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ArticuloMetodosValuacion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@metValId", DbType.Int64, 0, ParameterDirection.Input, radCbo_Met.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, (2), ParameterDirection.Input, radCbo_Mone.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            limpiar_cbo();
            llena_radMetVal();
            radCbo_Met.Enabled = false;
            radCbo_Mone.Enabled = false;

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

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        llena_radMetVal();

    }

    public void limpiar_cbo()
    {

        radCbo_Met.EmptyMessage = "Seleccionar";
        radCbo_Met.ClearSelection();
        radCbo_Met.Text = string.Empty;

        radCbo_Mone.EmptyMessage = "Seleccionar";
        radCbo_Mone.ClearSelection();
        radCbo_Mone.Text = string.Empty;


    }

    public void rad_to_combo()
    {
        var dataItem = radGrid_metVal.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            radCbo_Met.SelectedValue = dataItem["metValId"].Text;
            radCbo_Mone.SelectedValue = dataItem["monCve"].Text;
        }

    }


    #endregion































}