
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
using System.Windows.Forms;

public partial class DC_MttoClieDatosCartera : System.Web.UI.Page
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
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();


    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;


    private string PagLoc_cliCve;
    private int Pag_agrTipId;
   
    private int Pag_siCrCve;

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


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        //InicioPagina();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();

    }
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)//editar-
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();


        //this.rGdv_CondiPago.ClientSettings.Selecting.AllowRowSelect = true;
        rCboCondicionPago.Enabled = false;
        rCboSitCrediticia.Enabled = false;
        RadNTxtLimitCredito.Enabled = false;
        RdNmricNumCuenta.Enabled = false;
        rCboMetodoPago.Enabled = false;




    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //ControlesAccion();
        //EjecutaAccionLimpiar();
        DatosCartera_txt();
    }
    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();

    }
    protected void rCboConPago_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void rCboSitCred_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {



    }
    protected void rbtnGuardarDatCartera_Click(object sender, EventArgs e)
    {
        EjecutaAccion();
    }

    //protected void rGdv_CondiPago_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    rCboCondicionPago.Enabled = true;
    //    rCboSitCrediticia.Enabled = true;
    //    RadNTxtLimitCredito.Enabled = true;


    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {

    //    }
   
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

    //    {

    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //        rad_to_combo();
    //    }
    //    else
    //    {
    //        rad_to_combo();
    //    }

    //}
    //public void rad_to_combo()
    //{
    //    //var dataItem = rGdv_CondiPago.SelectedItems[0] as GridDataItem;
    //    //if (dataItem != null)
    //    //{
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

    //    {
    //        //rCboCondicionPago.SelectedValue = DataList<<conPagDias>>.Text.ToString();
    //        //rCboSitCrediticia.SelectedValue = dataItem["siCrCve"].Text.ToString();
    //        //rCboCondicionPago.Text = dataItem["conPagDes"].Text;
    //        //rCboSitCrediticia.Text = dataItem["siCrDes"].Text;
    //        //RadNTxtLimitCredito.Text = dataItem["cliDatCarLimCred"].Text;


    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }


    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        RadNTxtLimitCredito.Enabled = false;
    //        rCboSitCrediticia.Enabled = false;
    //        rCboCondicionPago.Enabled = false;
    //        RdNmricNumCuenta.Enabled = false;
    //        //rCboCondicionPago.SelectedValue = dataItem["conPagDias"].Text.ToString();
    //        //rCboSitCrediticia.SelectedValue = dataItem["siCrCve"].Text.ToString();
    //        //rCboCondicionPago.Text = dataItem["conPagDes"].Text;
    //        //rCboSitCrediticia.Text = dataItem["siCrDes"].Text;
    //        //RadNTxtLimitCredito.Text = dataItem["cliDatCarLimCred"].Text;


    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;

    //    }

    //    //}
    //}
    #endregion
    
    #region METODOS
    private void InicioPagina()
    {
        Valores_InicioPag();

        FnCtlsFillIn.RabComboBox_CondicionesPago(Pag_sConexionLog, Pag_sCompania, ref rCboCondicionPago, true, false);
        FnCtlsFillIn.RabComboBox_SatMetodoPago(Pag_sConexionLog, ref rCboMetodoPago, true, false);

        hdfBtnAccion.Value = "";
        ControlesAccion();
        LlenaDatosCliente();
        LlenaSituaCred();
        
        DatosCartera_txt();

        
        

        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            rLblSubClient.Visible = true;
        }
        else
        {
            rLblSubClie.Visible = false;
            rLblSubClient.Visible = false;
        }


        if (rBtnNuevo.Enabled == true)
        {
            rBtnModificar.Enabled = false;
        }else {
            rBtnModificar.Enabled = true;
        }
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
        PagLoc_cliCve = Convert.ToString(Session["folio_Selection"]);
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
                rCboCondicionPago.Enabled = true;
                rCboSitCrediticia.Enabled = true;
                rCboMetodoPago.Enabled = true;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                RadNTxtLimitCredito.Enabled = true;
                RdNmricNumCuenta.Enabled = true;
                EjecutaSpAcciones();
                InicioPagina();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAcciones();
                InicioPagina();
            }

        }
        else
        {
            RadWindowManager1.RadAlert(msgValidacion, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert2.png");
        }
    }
    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@conPagCve", DbType.String, 10, ParameterDirection.Input, rCboCondicionPago.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@siCrCve", DbType.String, 10, ParameterDirection.Input, rCboSitCrediticia.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@satMetPagCve", DbType.String, 2, ParameterDirection.Input, rCboMetodoPago.SelectedValue);

            if (RdNmricNumCuenta.Text !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@cliDatCatNoCta", DbType.String, 4, ParameterDirection.Input, RdNmricNumCuenta.Text.Trim());
            }
            if (RadNTxtLimitCredito.Text !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@cliDatCarLimCred", DbType.Decimal, 15, ParameterDirection.Input, RadNTxtLimitCredito.Text.Trim());
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
    
    private void LlenaSituaCred()//VIENE DE LA TABLA AGRUPACIONES
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboSitCrediticia, ds, "siCrCve", "siCrDes", true, false);

        ((Literal)rCboSitCrediticia.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSitCrediticia.Items.Count);
    }

   

    public void DatosCartera_txt()

    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {

            string s = ds.Tables[0].Rows[0]["conPagCve"].ToString() ;
            if (ds.Tables[0].Rows[0]["conPagCve"].ToString() != "")
            {


                rCboCondicionPago.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["conPagCve"]);
                rCboSitCrediticia.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["siCrCve"]);
                rCboMetodoPago.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["satMetPagCve"]);
                RadNTxtLimitCredito.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliDatCarLimCred"]);
                RdNmricNumCuenta.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliDatCatNoCta"]);
                this.rBtnNuevo.Enabled = false;
            }
            else {

                rCboCondicionPago.SelectedIndex = -1;
                rCboSitCrediticia.SelectedIndex = -1;
                rCboMetodoPago.SelectedIndex = -1;
                RadNTxtLimitCredito.Text = "";
                RdNmricNumCuenta.Text = "";
                this.rBtnNuevo.Enabled = true;
            }

           
        }
        else {
            rCboCondicionPago.ClearSelection();
            RadNTxtLimitCredito.Text = "";
            rCboSitCrediticia.ClearSelection();
            rCboMetodoPago.ClearSelection();
            RdNmricNumCuenta.Text = "";

            rCboSitCrediticia.SelectedIndex = -1;
            RadNTxtLimitCredito.Text = "";
            this.rBtnNuevo.Enabled = true ;
        }

       


    }

    private void LlenaDatosCliente()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblClave.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveClie"]);
            rLblSubClient.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveSubClie"]);
            rLblDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0]["clieNom"]);
        }
        else {
            rLblClave.Text = "";
            rLblSubClient.Text = "";
            rLblDescripcion.Text = "";
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




            //foreach (GridDataItem i in rGdv_CondiPago.SelectedItems)
            //{

            //    var dataItem = rGdv_CondiPago.SelectedItems[CountItems] as GridDataItem;
            //    if (dataItem != null)
            //{
            DataSet ds = new DataSet();
            rCboCondicionPago.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["conPagCve"]);
            rCboMetodoPago.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["satMetPagCve"]);
            rCboSitCrediticia.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["siCrCve"]);
            RadNTxtLimitCredito.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliDatCatNoCta"]);
            



            try
            {

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();//borrar los ultimos 4
                ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("conPagDes", DbType.String, 50, ParameterDirection.Input, rCboCondicionPago.Text);
                ProcBD.AgregarParametrosProcedimiento("siCrDes", DbType.String, 50, ParameterDirection.Input, rCboSitCrediticia.Text);
                ProcBD.AgregarParametrosProcedimiento("cliDatCarLimCred", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToString(RadNTxtLimitCredito.Text.Trim()));
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



            CountItems += 1;

            {




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
                    //else {
                    //    LlenaGridDatCartera();
                    //}


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
                }
            }
        }
        //else {
        //    LlenaGridDatCartera();
        //}




        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManager1.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboCondicionPago.BorderColor = System.Drawing.Color.Transparent;
        rCboSitCrediticia.BorderColor = System.Drawing.Color.Transparent;
        rCboMetodoPago.BorderColor = System.Drawing.Color.Transparent;

        this.rCboCondicionPago.Enabled = false;
        this.RadNTxtLimitCredito.Enabled = false;
        this.rCboSitCrediticia.Enabled = false;
        this.rCboMetodoPago.Enabled = false;
        this.RdNmricNumCuenta.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        //msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        //if (msgValidacion == "")
        //{
        ControlesAccionEjecucion(true);
        //}
        //else
        //{
        //    ControlesAccionEjecucion(false);
        //    ShowAlert(sMSGTip, msgValidacion);
        //}

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            this.rCboCondicionPago.Enabled = false;
            this.RadNTxtLimitCredito.Enabled = false;
            this.rCboSitCrediticia.Enabled = false;
            this.rCboMetodoPago.Enabled = false;
            this.RdNmricNumCuenta.Enabled = false;

            this.rCboCondicionPago.ClearSelection();
            this.RadNTxtLimitCredito.Text = "";
            this.rCboSitCrediticia.ClearSelection();
            this.rCboMetodoPago.ClearSelection();
            this.RdNmricNumCuenta.Text = "";
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
                //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                //rGdvInformacion.MasterTableView.ClearSelectedItems();

                this.rCboCondicionPago.Enabled = true;
                this.RadNTxtLimitCredito.Enabled = true;
                this.rCboSitCrediticia.Enabled = true;
                this.rCboMetodoPago.Enabled = true;
                this.RdNmricNumCuenta.Enabled = true;

                this.rCboCondicionPago.ClearSelection();
                this.RadNTxtLimitCredito.Text = "";
                this.rCboSitCrediticia.ClearSelection();
                this.rCboMetodoPago.ClearSelection();
                this.RdNmricNumCuenta.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                //rGdvInformacion.AllowMultiRowSelection = false;
                if (rBtnNuevo.Enabled == true)
                {
                    this.rCboCondicionPago.Enabled = true;
                }
                else
                {
                    this.rCboCondicionPago.Enabled = false;
                }

                this.RadNTxtLimitCredito.Enabled = true;
                this.rCboSitCrediticia.Enabled = true;
                this.rCboMetodoPago.Enabled = true;
                this.RdNmricNumCuenta.Enabled = true;

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
                //rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdvInformacion.AllowMultiRowSelection = true;
                //rGdvInformacion.MasterTableView.ClearSelectedItems();

                this.rCboCondicionPago.Enabled = false;
                this.RadNTxtLimitCredito.Enabled = false;
                this.rCboSitCrediticia.Enabled = false;
                this.rCboMetodoPago.Enabled = false;
                this.RdNmricNumCuenta.Enabled = false;

                if (rBtnNuevo.Enabled == true)
                {
                    this.rCboCondicionPago.ClearSelection();
                }
                this.RadNTxtLimitCredito.Text = "";
                this.rCboSitCrediticia.ClearSelection();
                this.rCboMetodoPago.ClearSelection();
                this.RdNmricNumCuenta.Text = "";

            }
        }


        if (Result == false)
        {
            this.rCboCondicionPago.Enabled = false;
            this.RadNTxtLimitCredito.Enabled = false;
            this.rCboSitCrediticia.Enabled = false;
            this.rCboMetodoPago.Enabled = false;
            this.RdNmricNumCuenta.Enabled = false;

            if (rBtnNuevo.Enabled == true)
            {
                this.rCboCondicionPago.ClearSelection();
            }
            this.RadNTxtLimitCredito.Text = "";
            this.rCboSitCrediticia.ClearSelection();
            this.rCboMetodoPago.ClearSelection();
            this.RdNmricNumCuenta.Text = "";
        }


    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rBtnNuevo.Enabled == true)
            {
                this.rCboCondicionPago.ClearSelection();
            }

            this.RadNTxtLimitCredito.Text = "";
            this.rCboSitCrediticia.ClearSelection();
            this.rCboMetodoPago.ClearSelection();
            this.RdNmricNumCuenta.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;

            //rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboCondicionPago.BorderColor = System.Drawing.Color.Transparent;
            rCboSitCrediticia.BorderColor = System.Drawing.Color.Transparent;
            rCboMetodoPago.BorderColor = System.Drawing.Color.Transparent;

            //this.rCboCondicionPago.Enabled = false;
            //this.RadNTxtLimitCredito.Enabled = false;
            //this.rCboSitCrediticia.Enabled = false;
            //this.rCboMetodoPago.Enabled = false;
            //this.RdNmricNumCuenta.Enabled = false;

            if (rBtnNuevo.Enabled == true)
            {
                this.rCboCondicionPago.ClearSelection();
            }
            this.RadNTxtLimitCredito.Text = "";
            this.rCboSitCrediticia.ClearSelection();
            this.rCboMetodoPago.ClearSelection();
            this.RdNmricNumCuenta.Text = "";

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }


    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO---MOFIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
           


            if (rCboCondicionPago.SelectedValue == "")
            {
                rCboCondicionPago.CssClass = "cssTxtInvalid";
                rCboCondicionPago.BorderWidth = Unit.Pixel(1);
                rCboCondicionPago.BorderColor = System.Drawing.Color.Red;
                rCboCondicionPago.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboCondicionPago.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboSitCrediticia.SelectedValue == "")
            {
                rCboSitCrediticia.CssClass = "cssTxtInvalid";
                rCboSitCrediticia.BorderWidth = Unit.Pixel(1);
                rCboSitCrediticia.BorderColor = System.Drawing.Color.Red;
                rCboCondicionPago.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboSitCrediticia.BorderColor = System.Drawing.Color.Transparent; }
            if (rCboMetodoPago.SelectedValue == "")
            {
                rCboMetodoPago.CssClass = "cssTxtInvalid";
                rCboMetodoPago.BorderWidth = Unit.Pixel(1);
                rCboMetodoPago.BorderColor = System.Drawing.Color.Red;
                rCboMetodoPago.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }else
            {
                rCboMetodoPago.BorderColor = System.Drawing.Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            
            return sResult;
        }

        
        //Eliminar
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (RegistroDatosCartera() == false)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            }
        }

        
            return sResult;
    }
    


    private bool RegistroDatosCartera() {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string s = ds.Tables[0].Rows[0]["conPagCve"].ToString();
            if (ds.Tables[0].Rows[0]["conPagCve"].ToString() != "")
            {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false; 
        }

    }
    //ELIMINAR
    #endregion




}




