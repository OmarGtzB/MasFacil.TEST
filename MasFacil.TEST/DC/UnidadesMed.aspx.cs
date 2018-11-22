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

public partial class DC_UnidadesMed : System.Web.UI.Page
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
        hdfBtnAccion.Value = "";

        ControlesAccion();
        llenadata_unidadmedida();

        gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
        gdv_UnidadMed.AllowMultiRowSelection = true;

        LlenaCboSATUnidadMedida();
        PermisoBotones();
    }
    private void PermisoBotones() {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(Page, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }

    #region BOTONES_ABC
    //NUEVO//
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        llenadata_unidadmedida();
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
    #endregion
    
    public void llenadata_unidadmedida()
    {
        //GridView ds = new GridView();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_UnidadMedidas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 5);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref gdv_UnidadMed, ds);
     
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
                EnviarSpUniMed();
                InicioPagina();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
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

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (radUnidadMedida.Text.Trim() == "")
            {
                radUnidadMedida.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            //if (rTxtAbreviatura.Text.Trim() == "")
            //{
            //    rTxtAbreviatura.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}

            if (radtxtDescripMed.Text.Trim() == "")
            {
                radtxtDescripMed.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (radtxtFac.Text.Trim() == "")
            {
                radtxtFac.CssClass = "cssTxtInvalid";
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

            if (gdv_UnidadMed.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (radUnidadMedida.Text.Trim() == "")
            {
                radUnidadMedida.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            //if (rTxtAbreviatura.Text.Trim() == "")
            //{
            //    rTxtAbreviatura.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            if (radtxtDescripMed.Text.Trim() == "")
            {
                radtxtDescripMed.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (radtxtFac.Text.Trim() == "")
            {
                radtxtFac.CssClass = "cssTxtInvalid";
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

            if (gdv_UnidadMed.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            
            return sResult;
        }
        
        return sResult;
    }


    public void EnviarSpUniMed()
    {
        try
        {
            //GridView ds = new GridView();
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_UnidadMedidas";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, radUnidadMedida.Text);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@uniMedDes", DbType.String, 50, ParameterDirection.Input, radtxtDescripMed.Text);
            ProcBD.AgregarParametrosProcedimiento("@uniMedAbr", DbType.String, 20, ParameterDirection.Input, rTxtAbreviatura.Text);
            ProcBD.AgregarParametrosProcedimiento("@uniMedFact", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radtxtFac.Text));
            if (rCboClaveSat.SelectedIndex != -1) {
                ProcBD.AgregarParametrosProcedimiento("@satUniMedCve", DbType.String, 3, ParameterDirection.Input, rCboClaveSat.SelectedValue);
            }

       

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            llenadata_unidadmedida();
            Limpiartxt();
            txt_desa();

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

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        cssEna();
        EjecutaAccion();
        //EnviarSpUniMed();
    }

    //Funcion para LIMPIAR los textbox 

    private void LlenaCboSATUnidadMedida() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SATUnidadMedida";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboClaveSat, ds, "satUniMedCve", "satUniMedNom", true, false);
        ((Literal)rCboClaveSat.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboClaveSat.Items.Count);

    }


    //Funion que pone el contenido del RadGrid dentro de los textbox de Unidad de Medida
    protected void gdv_UnidadMed_SelectedIndexChanged(object sender, EventArgs e)
    {

        var dataItem = gdv_UnidadMed.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            rCboClaveSat.ClearSelection();

            string val = dataItem["uniMedDes"].Text;
            string VAL2 = dataItem["uniMedAbr"].Text;
            string satUniMed = dataItem["satUniMedCve"].Text;
            radUnidadMedida.Text = dataItem["uniMedCve"].Text;
            radtxtFac.Text = dataItem["uniMedFact"].Text;


            if (satUniMed != "" && satUniMed != "&nbsp;")
            {
                rCboClaveSat.SelectedValue = satUniMed;
            }

            if (val == "&nbsp;")
            {
                radtxtDescripMed.Text = "";
            }
            else
            {
                radtxtDescripMed.Text = dataItem["uniMedDes"].Text;
            }
            if (VAL2 == "&nbsp;")
            {
                rTxtAbreviatura.Text = "";
            }
            else
            {
                rTxtAbreviatura.Text = dataItem["uniMedAbr"].Text;
            }
        }

    }

    public void rad_to_txt()
    {
       
    }


    #region LimpiarControles

    public void Limpiartxt()
    {
        radUnidadMedida.Text = "";
        radtxtDescripMed.Text = "";
        rTxtAbreviatura.Text = "";
        radtxtFac.Text = "";
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        ControlesAccion();
        hdfBtnAccion.Value = "";


        


    }


    public void txt_desa()
    {
        radUnidadMedida.Enabled = false;
        radtxtDescripMed.Enabled = false;
        rTxtAbreviatura.Enabled = false;
        radtxtFac.Enabled = false;
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = false;
    }

    public void habil_btn_nuevo()
    {
        radUnidadMedida.Enabled = true;
        radtxtDescripMed.Enabled = true;
        rTxtAbreviatura.Enabled = true;
        radtxtFac.Enabled = true;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = false;
    }

    public void habil_btn_edit()
    {
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
    }


    
    public void habil_btn_elim()
    {

        radUnidadMedida.Enabled = false;
        radtxtDescripMed.Enabled = false;
        rTxtAbreviatura.Enabled = false;
        radtxtFac.Enabled = false;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
    }

    #endregion


    public void cssEna()
    {
        radUnidadMedida.CssClass = "cssTxtEnabled";
        radtxtDescripMed.CssClass = "cssTxtEnabled";
        radtxtFac.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";
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




            foreach (GridDataItem i in gdv_UnidadMed.SelectedItems)
            {

                var dataItem = gdv_UnidadMed.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string suniMedCve = dataItem["uniMedCve"].Text;
                    string suniMedFact = dataItem["uniMedFact"].Text;
                    string suniMedDes = dataItem["uniMedDes"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_UnidadMedidas";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, (6), ParameterDirection.Input, suniMedCve);
                        ProcBD.AgregarParametrosProcedimiento("@uniMedFact", DbType.Decimal, (15), ParameterDirection.Input, suniMedFact);
                        ProcBD.AgregarParametrosProcedimiento("@uniMedAbr", DbType.String, (20), ParameterDirection.Input, rTxtAbreviatura.Text);
                        ProcBD.AgregarParametrosProcedimiento("@uniMedDes", DbType.String, (50), ParameterDirection.Input, suniMedDes);
                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds)) {

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

                sEstatusAlert = EstatusItemsElim ;
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
                  
                    llenadata_unidadmedida();
                }
                

            }
           else if (CountItems > 1) {


                if (CantItemsElimTrue > 0) {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0) {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "") {
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
                    llenadata_unidadmedida();
                }

            }

        }
        catch (Exception ex)
        { 
            string MsgError = ex.Message.Trim();
        }
        
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = false;


    }

    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }





    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        radUnidadMedida.CssClass = "cssTxtEnabled";
        radtxtDescripMed.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";
        this.radUnidadMedida.Enabled = false;
        this.radtxtDescripMed.Enabled = false;
        this.rTxtAbreviatura.Enabled = false;
        this.radtxtFac.Enabled = false;
        rCboClaveSat.Enabled = false;

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
                this.gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = false;
                gdv_UnidadMed.MasterTableView.ClearSelectedItems();
                this.radUnidadMedida.Enabled = true;
                this.radtxtDescripMed.Enabled = true;
                this.rTxtAbreviatura.Enabled = true;
                radtxtFac.Enabled = true;
                rCboClaveSat.Enabled = true;
                this.radUnidadMedida.Text = "";
                this.radtxtDescripMed.Text = "";
                this.rTxtAbreviatura.Text = "";
                radtxtFac.Text = "";
                rCboClaveSat.ClearSelection();
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                gdv_UnidadMed.AllowMultiRowSelection = false;
                this.radUnidadMedida.Enabled = false;
                this.radtxtDescripMed.Enabled = true;
                this.rTxtAbreviatura.Enabled = true;
                radtxtFac.Enabled = true;
                rCboClaveSat.Enabled = true;
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
                gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
                gdv_UnidadMed.AllowMultiRowSelection = true;
                gdv_UnidadMed.MasterTableView.ClearSelectedItems();
                this.radUnidadMedida.Enabled = false ;
                this.radtxtDescripMed.Enabled = false ;
                this.rTxtAbreviatura.Enabled = false;
                radtxtFac.Enabled = false;
                rCboClaveSat.Enabled = false;
                this.radUnidadMedida.Text = "";
                this.radtxtDescripMed.Text = "";
                this.rTxtAbreviatura.Text = "";
                radtxtFac.Text = "";
                rCboClaveSat.ClearSelection();
            }
        }


        if (Result == false)
        {
            this.radUnidadMedida.Enabled = false;
            this.radtxtDescripMed.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            this.radUnidadMedida.Text = "";
            this.radtxtDescripMed.Text = "";
            this.rTxtAbreviatura.Text = "";
            radtxtFac.Text = "";
        }


    }


    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = gdv_UnidadMed.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, gdv_UnidadMed, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, gdv_UnidadMed, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.radUnidadMedida.Text = "";
            this.radtxtDescripMed.Text = "";
            this.rTxtAbreviatura.Text = "";
            this.radtxtFac.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            gdv_UnidadMed.ClientSettings.Selecting.AllowRowSelect = true;
            gdv_UnidadMed.AllowMultiRowSelection = true;
            gdv_UnidadMed.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            radUnidadMedida.CssClass = "cssTxtEnabled";
            radtxtDescripMed.CssClass = "cssTxtEnabled";
            rTxtAbreviatura.CssClass = "cssTxtEnabled";
            radtxtFac.CssClass = "cssTxtEnabled";

            radUnidadMedida.Enabled = false;
            radtxtDescripMed.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            radtxtFac.Enabled = false;

            radUnidadMedida.Text = "";
            radtxtDescripMed.Text = "";
            rTxtAbreviatura.Text = "";
            radtxtFac.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }


}





