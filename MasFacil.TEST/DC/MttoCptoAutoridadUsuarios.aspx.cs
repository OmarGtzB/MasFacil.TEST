
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

public partial class DC_MttoCptoAutoridadUsuarios : System.Web.UI.Page
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
    private string folio_Selection;
    //private string conConfSec;
    private string listTipDatoCptoCve;



    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {

        if (SM.IsActiveSession(this.Page))
        {
            //Recuperar Valores de Sesion
            Valores_InicioPag();
            if (!IsPostBack)
            {
                //Iniciar Formulario
                InicioPagina();

            }
        }

    }

    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {

        bool rBtnRelleno11;
        bool rBtnTogImpDscS;

        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //    this.rCboTipoDato.Enabled = false;
            //this.rBtnConsultar.Enabled = true;
            //this.rBtnRegistrar.Enabled = true;
            //this.rBtnGenerar.Enabled = true;
            rTxtsegCptoId.Text = dataItem["segCptoId"].Text;
            rCboTipoDato.SelectedValue = dataItem["maUsuCve"].Text;


            if (dataItem["segCptoAut"].Text == "1")
            {
                rBtnConsultar.Checked = true;
                rBtnRegistrar.Checked = false;
                rBtnGenerar.Checked = false;
            }
            else if (dataItem["segCptoAut"].Text == "2")
            {
                rBtnConsultar.Checked = false;
                rBtnRegistrar.Checked = true;
                rBtnGenerar.Checked = false;
            }
            else if (dataItem["segCptoAut"].Text == "3")
            {
                rBtnConsultar.Checked = false;
                rBtnGenerar.Checked = true;
                rBtnRegistrar.Checked = false;

            }
            //disEnableUi(2);


        }

        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{
        //    this.rCboTipoDato.Enabled = false;
        //    this.rBtnConsultar.Enabled = false;
        //    this.rBtnRegistrar.Enabled = false;
        //    this.rBtnGenerar.Enabled = false;
        //    this.rCboTipoDato.ClearSelection();
        //    this.rBtnConsultar.Checked = true;
        //    this.rBtnRegistrar.Checked = false;
        //    rBtnGenerar.Checked = false;

        //}
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
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnModificar.Enabled = true;
        ControlesAccion();

    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //ControlesAccion();
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()

        //   )
        //{
        //    LimpiarUi();
        //}
        //else
        //{

        //}

        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //InicioPagina();
        //this.rCboTipoDato.ClearSelection();
        //rGdvInformacion.MasterTableView.ClearSelectedItems();
        //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
        //this.rGdvInformacion.AllowMultiRowSelection = false;
        //this.rCboTipoDato.Enabled = false;
        //this.rBtnConsultar.Enabled = false;
        //this.rBtnRegistrar.Enabled = false;
        //this.rBtnCancelar.Enabled = false;
        //this.rBtnGuardar.Enabled = false;

        //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        InicioPagina();

    }






    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);


        folio_Selection = Convert.ToString(Session["folio_Selection"]);


        listTipDatoCptoCve = Convert.ToString(Session["listTipDatoCptoCve"]);


    }

    private void InicioPagina()
    {
        llenaComboUsuarios();
        hdfBtnAccion.Value = "";
        llenadata_Grid();
        ControlesAccion();
        llenaDatCpto();

        ////LlenaComboTiposDato();


        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;


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

    private void llenaComboUsuarios()
    {
         FnCtlsFillIn.RabComboBox_UsuariosSegDoc(Pag_sConexionLog, Pag_sCompania, ref rCboTipoDato, true, false);
    }

    private void llenaDatCpto()
    {
        if (folio_Selection != "") {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                rLblCptoId.Text = folio_Selection;
                rLblcptoDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoDes"]);
            }
        }
    }


    private void disEnableUi(int opc)
    {
        //1 = New - A 
        if (opc == 1)
        {


        }
        else if (opc == 2) // Modificar Cliente
        {
            this.rCboTipoDato.Enabled = true;
            rBtnConsultar.Enabled = true;
            rBtnRegistrar.Enabled = true;


        }
        if (opc == 3)
        {

            rBtnConsultar.Enabled = false;
            rBtnRegistrar.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;

        }



    }


 

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
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
                llenadata_Grid();
                InicioPagina();

            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                llenadata_Grid();
                InicioPagina();
            }



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
                    ProcBD.NombreProcedimiento = "sp_CptoAutoridadUsuarios";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboTipoDato.SelectedValue);


                    if (rBtnConsultar.Checked == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@segCptoAut", DbType.Int64, 0, ParameterDirection.Input, 1);
                    }
                    else if (rBtnConsultar.Checked == false)
                    {

                    }
                    if (rBtnRegistrar.Checked == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@segCptoAut", DbType.Int64, 0, ParameterDirection.Input, 2);
                    }
                    else if (rBtnRegistrar.Checked == false)
                    {

                    }
                    if (rBtnGenerar.Checked == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@segCptoAut", DbType.Int64, 0, ParameterDirection.Input, 3);
                    }
                    else if (rBtnGenerar.Checked == false)
                    {

                    }


                    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (FnValAdoNet.bDSIsFill(ds))
                    {

                        //EjecutaSpRefVar();

                        string sEjecEstatus, sEjecMSG = "";

                        if (ds.Tables.Count == 1)
                        {
                            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                        }
                        else
                        {
                            sEjecEstatus = ds.Tables[1].Rows[0]["maMSGTip"].ToString();
                            sEjecMSG = ds.Tables[1].Rows[0]["maMSGDes"].ToString();
                        }

                        //if (sEjecEstatus == "1")
                        //{
                        //    InicioPagina();
                        //}




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
        int CountItems = 0;
        int CantItemsElimTrue = 0;
        int CantItemsElimFalse = 0;
        string EstatusItemsElim = "";
        string MsgItemsElim = "";
        string MsgItemsElimTrue = "";
        string MsgItemsElimFalse = "";

        foreach (GridDataItem i in rGdvInformacion.SelectedItems)
        {
            var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
            //var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {


                //conConfSec = dataItem["cptoConfSec"].Text;
                this.rCboTipoDato.SelectedValue = dataItem["maUsuCve"].Text;

                try
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_CptoAutoridadUsuarios";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, dataItem["maUsuCve"].Text);
                    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
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
                //LlenaGridAlmacenes();
                InicioPagina();
            }

        }

    }




    public void llenadata_Grid()
    {
        if (folio_Selection != "") {
           
      
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CptoAutoridadUsuarios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);
        }
    }





    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL 
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;

        this.rCboTipoDato.Enabled = false;
        this.rBtnConsultar.Enabled = false;
        this.rBtnRegistrar.Enabled = false;
        this.rBtnGenerar.Enabled = false;

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
            this.rCboTipoDato.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = false;


            this.rCboTipoDato.ClearSelection();
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;
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
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvInformacion.MasterTableView.ClearSelectedItems();


                this.rCboTipoDato.Enabled = true;
                this.rBtnConsultar.Enabled = true;
                this.rBtnRegistrar.Enabled = true;
                this.rBtnGenerar.Enabled = true;


                this.rCboTipoDato.ClearSelection();
                this.rBtnConsultar.Checked = true;
                this.rBtnRegistrar.Checked = false;
                this.rBtnGenerar.Checked = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                this.rCboTipoDato.Enabled = false;
                this.rBtnConsultar.Enabled = true;
                this.rBtnRegistrar.Enabled = true;
                this.rBtnGenerar.Enabled = true;

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
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                this.rCboTipoDato.Enabled = false;
                this.rBtnConsultar.Enabled = false;
                this.rBtnRegistrar.Enabled = false;
                this.rBtnGenerar.Enabled = true;

                this.rCboTipoDato.ClearSelection();
                this.rBtnConsultar.Checked = true;
                this.rBtnRegistrar.Checked = false;
                this.rBtnGenerar.Checked = false;
            }
        }


        if (Result == false)
        {
            this.rCboTipoDato.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = true;

            this.rCboTipoDato.ClearSelection();
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvInformacion.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
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

            this.rCboTipoDato.ClearSelection();
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;

            rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;

            this.rCboTipoDato.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = true;

            this.rCboTipoDato.ClearSelection();
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            hdfBtnAccion.Value = "";

            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
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


        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {



            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.BorderWidth = Unit.Pixel(1);
                rCboTipoDato.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboTipoDato.BorderColor = System.Drawing.Color.Transparent; }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.CssClass = "cssTxtInvalid";
                rCboTipoDato.Focus();

                camposInc += 1;
            }
            else { rCboTipoDato.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        ////ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }
        return sResult;
    }


    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 55);

        return ds;
    }

    #endregion





}