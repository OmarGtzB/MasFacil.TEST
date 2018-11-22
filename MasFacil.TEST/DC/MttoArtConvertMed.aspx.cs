using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using Telerik.Web.UI;
using System.Windows.Forms;


public partial class DC_MttoArtConvertMed : System.Web.UI.Page
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
    protected void radGrid_conversiones_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = radGrid_conversiones.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string val = dataItem["uniMedCve"].Text;
            radCbo_Unidad.SelectedValue = dataItem["uniMedCve"].Text;
            radtxt_fact.Text = dataItem["artConMedFact"].Text;
        }
    }
    protected void radCbo_Unidad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_UnidadMedidas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 10, ParameterDirection.Input, radCbo_Unidad.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        radtxt_fact.Text = Convert.ToString(ds.Tables[0].Rows[0]["uniMedFact"]);
        radtxt_fact.Enabled = true;

    }



    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
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
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //radtxt_fact.CssClass = "cssTxtInvalid";
        //radCbo_Unidad.BorderWidth = Unit.Pixel(1);
        //radCbo_Unidad.BorderColor = System.Drawing.Color.Transparent;
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
    public void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        ControlesAccion();

        FnCtlsFillIn.RadComboBox_UnidadesMedida(Pag_sConexionLog, Pag_sCompania, ref radCbo_Unidad, true, false);
        pintar_txt();
        radtxt_fact.Text = "";
        LlenarGrid();

        radGrid_conversiones.ClientSettings.Selecting.AllowRowSelect = true;
        radGrid_conversiones.AllowMultiRowSelection = true;

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
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        radtxt_fact.CssClass = "cssTxtEnabled";
        radtxt_fact.Enabled = false;
        radCbo_Unidad.Enabled = false;
        radCbo_Unidad.BorderWidth = Unit.Pixel(1);
        radCbo_Unidad.BorderColor = System.Drawing.Color.Transparent;
   
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

    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                radGrid_conversiones.ClientSettings.Selecting.AllowRowSelect = false;
                radGrid_conversiones.MasterTableView.ClearSelectedItems();
     
                radtxt_fact.Enabled = true;
                radtxt_fact.Text = "";
                radCbo_Unidad.Enabled = true;
                radCbo_Unidad.BorderWidth = Unit.Pixel(1);
                radCbo_Unidad.BorderColor = System.Drawing.Color.Transparent;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                radtxt_fact.Enabled = true;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }


            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

        }

        if (Result == false)
        {
        
                InicioPagina();
       
            
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
                InicioPagina();
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
    private void EjecutaAccionLimpiar()
    {


        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            radGrid_conversiones.MasterTableView.ClearSelectedItems();
            radCbo_Unidad.ClearSelection();
            radtxt_fact.Text = "";
            radtxt_fact.Enabled = false;
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        }
        else {
            radGrid_conversiones.ClientSettings.Selecting.AllowRowSelect = true;
            radGrid_conversiones.MasterTableView.ClearSelectedItems();
            radCbo_Unidad.ClearSelection();
            radtxt_fact.Text = "";
            radtxt_fact.Enabled = true;

        }



    }
    public void EjecutaSpAcciones()
    {
        try
        {
            //GridView ds = new GridView();
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ArticuloConvercionMedidas";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, (6), ParameterDirection.Input, radCbo_Unidad.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@artConMedFact", DbType.Decimal, (15), ParameterDirection.Input, Convert.ToDecimal(radtxt_fact.Text));
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            LlenarGrid();



            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {

                    hdfBtnAccion.Value = "";

                    ControlesAccion();
                }
                else
                {

                }
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();

        }
    }
    public void EjecutaSpAccionEliminar()
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




            foreach (GridDataItem i in radGrid_conversiones.SelectedItems)
            {

                var dataItem = radGrid_conversiones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string suniMedCve = dataItem["uniMedCve"].Text;
                    //string suniMedFact = dataItem["artConMedFact"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ArticuloConvercionMedidas";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
                        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, (6), ParameterDirection.Input, suniMedCve);
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
                        MessageBox.Show(ex.ToString());
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
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                }
                else
                {

                    LlenarGrid();
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
                    LlenarGrid();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }



    }
    public void LlenarGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloConvercionMedidas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 4);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref radGrid_conversiones, ds);

    }
    public void pintar_txt()
    {
        if (PagLoc_ArtCve == "")
        {
            lbl_codigArt.Text = "";
            lbl_descripArt.Text = "";
            lbl_UniMed_Art.Text = "";
            lbl_uni_MedDes.Text = "";
        }
        else
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Articulos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            lbl_codigArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artCve"]);
            lbl_descripArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artDes"]);
            lbl_UniMed_Art.Text = Convert.ToString(ds.Tables[0].Rows[0]["uniMedCve"]);
            lbl_uni_MedDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["uniMedDes"]);
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
            if (radCbo_Unidad.Text.Trim() == "")
            {
                radCbo_Unidad.CssClass = "cssTxtInvalid";

                radCbo_Unidad.BorderWidth = Unit.Pixel(1);
                radCbo_Unidad.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }

            if (radtxt_fact.Text.Trim() == "")
            {
                radtxt_fact.CssClass = "cssTxtInvalid";
                camposInc += 1;
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

            if (radGrid_conversiones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (radCbo_Unidad.Text.Trim() == "")
            {
                radCbo_Unidad.BorderWidth = Unit.Pixel(1);
                radCbo_Unidad.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }

            if (radtxt_fact.Text.Trim() == "")
            {
                radtxt_fact.CssClass = "cssTxtInvalid";
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

            if (radGrid_conversiones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;

            }

            return sResult;
        }

        return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = radGrid_conversiones.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, radGrid_conversiones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, radGrid_conversiones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;

    }


    #endregion































































}