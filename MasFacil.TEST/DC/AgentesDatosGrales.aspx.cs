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

public partial class DC_AgentesDatosGrales : System.Web.UI.Page
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
    private string PagLoc_AgeCve;
    
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
                //fillCboAgrupacionesDato();
            }
        }

    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnModificar.Enabled = true;
        rBtnLimpiar.Enabled = true;
        ControlesAccion();
        disEnableUi(2);
        LimpiarUi();
        if (rCboPaises.SelectedValue != "")
        {
            rCboEstado.Enabled = true;
        }
        else
        {
            rCboEstado.Enabled = false;
        }
        if (rCboEstado.SelectedValue != "")
        {
            rCboPoblacion.Enabled = true;
        }
        else
        {
            rCboPoblacion.Enabled = false;
        }
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()

           )
        {
            LimpiarUi();
        }
        else
        {

        }
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        //fillCboAgrupacionesDato();
    }

    //Eventos RadioButtons
    protected void rBtnTrueE_Click(Object sender,EventArgs e)
    {
        rCboTrabajador.Enabled = true;
        
    }

    protected void rBtnFalseE_Click(Object sender,EventArgs e)
    {
        rCboTrabajador.ClearSelection();
        rCboTrabajador.Enabled = false;
        
    }

    protected void rCboPaises_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboPaises.SelectedValue != "")
        {
            llenarCboEntidades(rCboPaises.SelectedValue.ToString());
            rCboEstado.ClearSelection();
            rCboEstado.Enabled = true;
            rCboPoblacion.ClearSelection();
            rCboPoblacion.Enabled = false;
        }else
        {
            rCboPoblacion.ClearSelection();
            rCboPoblacion.Enabled = false;
            rCboEstado.ClearSelection();
            rCboEstado.Enabled = false;

        }
        
    }


    protected void rCboEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboEstado.SelectedValue != "")
        {
            llenarCboProvincias(rCboPaises.SelectedValue.ToString(),   rCboEstado.SelectedValue.ToString());
            rCboPoblacion.ClearSelection();
            rCboPoblacion.Enabled = true;
        }else
        {
            rCboPoblacion.ClearSelection();
            rCboPoblacion.Enabled = false;
        }
       
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        PagLoc_AgeCve = Convert.ToString(Session["folio_Selection"]);

        //PagLoc_AgeCve = "04";


        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }

    private void InicioPagina()
    {

        rTxtNExt.Text = "";

        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        llenar_cboAgentes();
        llenar_cboTrabajadores();
        
        llenar_cboPais();

        rCboPaises.EmptyMessage = "Seleccionar";
        rCboEstado.EmptyMessage = "Seleccionar";
        rCboPoblacion.EmptyMessage = "Seleccionar";

        //Comprobar si es nuevo o se editara


        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();

        if (PagLoc_AgeCve == "")
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
            rBtnModificar.Enabled = false;
            LimpiarUi();
            disEnableUi(1);
        }
        else
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
            LlenarUi(PagLoc_AgeCve);
            disEnableUi(3);
            rBtnLimpiar.Enabled = true;
            rBtnModificar.Enabled = true;
        }
        LlenaDLOtrosDatos();
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

    private void disEnableUi(int opc)
    {
        //1 = New - A 
        if (opc == 1)
        {
            rTxtAgente.Enabled = true;
            
            rTxtNombre.Enabled = true;
            rTxtAbr.Enabled = true;

            rCboAgentes.Enabled = true;
            rBtnTrueE.Enabled = true;
            rBtnFalseE.Enabled = true;
            rCboTrabajador.Enabled = true;
            
            rCboPaises.Enabled = true;
            rCboEstado.Enabled = false;
            rCboPoblacion.Enabled = false;
            rTxtColonia.Enabled = true;
            rTxtCalle.Enabled = true;
            rTxtNExt.Enabled = true;
            rTxtNInt.Enabled = true;
            rTxtCllsA.Enabled = true;
            rTxtRef.Enabled = true;
            rTxtCP.Enabled = true;

            rTxtTel1.Enabled = true;
            rTxtTel2.Enabled = true;
            rTxtFax.Enabled = true;

            disEnableUiRefVar(1);

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

        }
        else if (opc == 2) // Modificar Cliente
        {

            rTxtAgente.Enabled = false;
            
            rTxtNombre.Enabled = true;
            rTxtAbr.Enabled = true;


            rCboAgentes.Enabled = true;
            rBtnTrueE.Enabled = true;
            rBtnFalseE.Enabled = true;

            if (rBtnTrueE.Checked == true)
            {
                rCboTrabajador.Enabled = true;
            }

            


            rCboPaises.Enabled = true;
            rCboEstado.Enabled = true;
            rCboPoblacion.Enabled = true;
            rTxtColonia.Enabled = true;
            rTxtCalle.Enabled = true;
            rTxtNExt.Enabled = true;
            rTxtNInt.Enabled = true;
            rTxtCllsA.Enabled = true;
            rTxtRef.Enabled = true;
            rTxtCP.Enabled = true;

            rTxtTel1.Enabled = true;
            rTxtTel2.Enabled = true;
            rTxtFax.Enabled = true;

            disEnableUiRefVar(1);

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

        }
        if (opc == 3)
        {

            rTxtAgente.Enabled = false;
            
            rTxtNombre.Enabled = false;
            rTxtAbr.Enabled = false;

            rCboAgentes.Enabled = false;
            rBtnTrueE.Enabled = false;
            rBtnFalseE.Enabled = false;
            rCboTrabajador.Enabled = false;

            rCboPaises.Enabled = false;
            rCboEstado.Enabled = false;
            rCboPoblacion.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNExt.Enabled = false;
            rTxtNInt.Enabled = false;
            rTxtCllsA.Enabled = false;
            rTxtRef.Enabled = false;
            rTxtCP.Enabled = false;

            rTxtTel1.Enabled = false;
            rTxtTel2.Enabled = false;
            rTxtFax.Enabled = false;

            disEnableUiRefVar(2);

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;

        }
    }

    private void disEnableUiRefVar(int opc)
    {
        if (opc == 1)
        {
            foreach (DataListItem dli in dt_referencias.Items)
            {
                var valRef = dli.FindControl("txt_ref") as RadTextBox;
                valRef.Enabled = true;

            }
            foreach (DataListItem dli in dt_variables.Items)
            {
                var valRef = dli.FindControl("txt_var") as RadNumericTextBox;
                valRef.Enabled = true;

            }
            DataListOtrosDatos.Enabled = true;
        }
        if (opc == 2)
        {

            foreach (DataListItem dli in dt_referencias.Items)
            {
                var valRef = dli.FindControl("txt_ref") as RadTextBox;
                valRef.Enabled = false;

            }
            foreach (DataListItem dli in dt_variables.Items)
            {
                var valRef = dli.FindControl("txt_var") as RadNumericTextBox;
                valRef.Enabled = false;

            }
            DataListOtrosDatos.Enabled = false;

        }

    }


    private void ControlesAccion()
    {

        //===> CONTROLES POR ACCION
        // LIMPIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
        {
            LimpiarUi();
        }

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            LimpiarUi();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            //Habilitar campos editables

        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //Creo no hay boton de eliminar   
        }


        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
           )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
        else
        {
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
        //Validar Campos
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
                //EjecutaSpAccionEliminar();
            }

        }
        else
        {
            //ShowAlert("2", msgValidacion);
            //MessageBox.Show(msgValidacion);
            ShowAlert("2", msgValidacion);
        }



    }

    private void EjecutaSpAcciones()
    {

        int radButtonState;

        if (rCboTrabajador.Enabled == true)
        {
            radButtonState = 1;
        }
        else
        {
            radButtonState = 2;
        }


        try
        {
            DataSet ds = new DataSet();
            string cliCve;

            cliCve = rTxtAgente.Text.Trim();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Agentes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            
            ProcBD.AgregarParametrosProcedimiento("@ageNom", DbType.String, 200, ParameterDirection.Input, rTxtNombre.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ageAbr", DbType.String, 15, ParameterDirection.Input, rTxtAbr.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ageTip", DbType.Int64, 1, ParameterDirection.Input, rCboAgentes.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ageTraCia", DbType.Int64, 1, ParameterDirection.Input, radButtonState);
            ProcBD.AgregarParametrosProcedimiento("@ageTraCve", DbType.String, 20, ParameterDirection.Input, rCboTrabajador.SelectedValue);


            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaises.SelectedValue);
            if (rCboEstado.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEstado.SelectedValue);
            }
            if (rCboPoblacion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboPoblacion.SelectedValue);

            }
            if (rTxtCP.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCP.Text.Trim());
            }
            if (rTxtColonia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColonia.Text.Trim());
            }
            if (rTxtCalle.Text.Trim() !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalle.Text.Trim());
            }
            if (rTxtCllsA.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCllsA.Text.Trim());
            }
            
           

            if(rTxtNInt.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNInt.Text.Trim());
            }
            if(rTxtNExt.Text.Trim() !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNExt.Text.Trim());
            }

            if (rTxtRef.Text.Trim() !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtRef.Text.Trim());
            }
            if (rTxtTel1.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, rTxtTel1.Text.Trim());
            }
            if (rTxtTel2.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, rTxtTel2.Text.Trim());
            }
            if (rTxtFax.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, rTxtFax.Text.Trim());
            }
            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                GuardaOtrosDatos();
                EjecutaSpRefVar();
                
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

                if (sEjecEstatus == "1")
                {
                    PagLoc_AgeCve = cliCve;
                    Session["folio_Selection"] = cliCve;
                    InicioPagina();
                }




                ShowAlert(sEjecEstatus, sEjecMSG);
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void fillCboAgrupacionesDato()
    {
        foreach (DataListItem dli in dt_variables.Items)
        {

            //Obtener agrupacion de la secuencia

            var valAgrCve = dli.FindControl("rCboPrueba") as RadComboBox;


            valAgrCve.EmptyMessage = "Seleccionar";
            valAgrCve.DataTextField = "revaDes";
            valAgrCve.DataValueField = "revaDes";
            valAgrCve.DataSource = llenadatalistVarRef(1);
            valAgrCve.DataBind();
            valAgrCve.ClearSelection();
            valAgrCve.Text = string.Empty;


            valAgrCve.DataBind();

            //Llamar obtenerDatoAgrupacion(agrCve)

            //llenar cbo t dar databind


        }

    }

    private void EjecutaSpRefVar()
    {

        string cliCve;

        cliCve = rTxtAgente.Text;
        
        try
        {

            foreach (DataListItem dli in dt_referencias.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_ref") as RadTextBox;
                secRef = dli.ItemIndex + 1;

                //MessageBox.Show(references.Text);
                if (valRef.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_AgenteRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "AGENT");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, valRef.Text);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
                else
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_AgenteRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                    ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "AGENT");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
            }




            foreach (DataListItem dli in dt_variables.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_var") as RadNumericTextBox;
                secRef = dli.ItemIndex + 1;

                //MessageBox.Show(references.Text);
                if (valRef.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_AgenteRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "AGENT");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valRef.Text));
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }else
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_AgenteRefVar";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                    ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "AGENT");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
            }


        }


        catch (Exception EX)
        {
            MessageBox.Show(EX.ToString());
            throw;
        }

    }

    private void LimpiarUi()
    {

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rTxtAgente.Text = "";
            
        }
        
        rTxtAgente.CssClass = "cssTxtEnabled";
        rTxtNombre.CssClass = "cssTxtEnabled";
        rTxtAbr.CssClass = "cssTxtEnabled";

        rCboPaises.BorderColor = System.Drawing.Color.Transparent;
        rCboEstado.BorderColor = System.Drawing.Color.Transparent;
        rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;
        

        rTxtAbr.Text = "";
        rTxtNombre.Text = "";
        rCboPaises.ClearSelection();
        rCboEstado.ClearSelection();
        rCboEstado.Enabled = false;
        rCboPoblacion.ClearSelection();
        rCboPoblacion.Enabled = false;
        rTxtColonia.Text = "";
        rTxtCalle.Text = "";
        rTxtNInt.Text = "";
        rTxtNExt.Text = "";
        rTxtCllsA.Text = "";
        rTxtCP.Text = "";
        rTxtRef.Text = "";

        rTxtTel1.Text = "";
        rTxtTel2.Text = "";
        rTxtFax.Text = "";

        
        //rTxtTelex.Text = "";

        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();

        /*

        foreach (DataListItem dli in dt_referencias.Items)
        {
            var valRef = dli.FindControl("txt_ref") as RadTextBox;
            valRef.Text = "";

        }
        foreach (DataListItem dli in dt_variables.Items)
        {
            var valRef = dli.FindControl("txt_var") as RadNumericTextBox;
            valRef.Value = 0.00;

        }

        */

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            LlenarUi(PagLoc_AgeCve);
            rCboEstado.Enabled = true;
            rCboPoblacion.Enabled = true;
        }


    }

    private void LlenarUi(string cliCve)
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Agentes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                rTxtAgente.Text = ds.Tables[0].Rows[0]["ageCve"].ToString();
                
                rTxtAbr.Text = ds.Tables[0].Rows[0]["ageAbr"].ToString();
                rTxtNombre.Text = ds.Tables[0].Rows[0]["ageNom"].ToString();
                
                rCboAgentes.SelectedValue = ds.Tables[0].Rows[0]["ageTip"].ToString();

                if (ds.Tables[0].Rows[0]["ageTraCia"].ToString() == "1")
                {
                    rBtnTrueE.Checked = true;
                    rBtnFalseE.Checked = false;
                    rCboTrabajador.SelectedValue = ds.Tables[0].Rows[0]["ageTraCve"].ToString();
                }
                else
                {
                    rBtnTrueE.Checked = false;
                    rBtnFalseE.Checked = true;
                    rCboTrabajador.ClearSelection();
                }

                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaises, true, false);
                rCboPaises.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaises.SelectedValue.ToString(), ref rCboEstado, true, false);
                rCboEstado.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPaises.SelectedValue,  rCboEstado.SelectedValue.ToString(), ref rCboPoblacion, true, false);
                rCboPoblacion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
            

                rTxtColonia.Text = ds.Tables[0].Rows[0]["domCol"].ToString();
                rTxtCalle.Text = ds.Tables[0].Rows[0]["domClle"].ToString();
                rTxtNInt.Text = ds.Tables[0].Rows[0]["domNInt"].ToString();
                rTxtNExt.Text = ds.Tables[0].Rows[0]["domNExt"].ToString();
                rTxtCllsA.Text = ds.Tables[0].Rows[0]["domCllsA"].ToString();
                rTxtCP.Text = ds.Tables[0].Rows[0]["domCP"].ToString();
                rTxtRef.Text = ds.Tables[0].Rows[0]["domRef"].ToString();

                rTxtTel1.Text = ds.Tables[0].Rows[0]["domTel"].ToString();
                rTxtTel2.Text = ds.Tables[0].Rows[0]["domTel2"].ToString();
                rTxtFax.Text = ds.Tables[0].Rows[0]["domFax"].ToString();
            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }


    public void llenar_cboAgentes()
    {
        FnCtlsFillIn.RadComboBox_TiposAgentes(Pag_sConexionLog, Pag_sCompania, ref rCboAgentes, true, true, "1");
    }

    public void llenar_cboTrabajadores()
    {
        FnCtlsFillIn.RadComboBox_Trabajadores(Pag_sConexionLog, Pag_sCompania, ref rCboTrabajador, true, true, "1",1);
    }

    public void llenar_cboPais()
    {
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaises, true, false);
    }

    private void llenarCboEntidades(string paisId)
    {

        FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, paisId.ToString(), ref rCboEstado, true, false);

    }

    private void llenarCboProvincias(string pais, string entId)
    {
        FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, pais.ToString(), entId.ToString(), ref rCboPoblacion, true, false);
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
            if (rTxtAgente.Text.Trim() == "")
            {
                rTxtAgente.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAgente.CssClass = "cssTxtEnabled"; }
            
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }
            
            if (rCboPaises.SelectedValue == "")
            {
                rCboPaises.CssClass = "cssTxtInvalid";
                rCboPaises.BorderWidth = Unit.Pixel(1);
                rCboPaises.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;

            }
            else
            {
                rCboPaises.BorderColor = System.Drawing.Color.Transparent;

                
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
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }

            if (rCboPaises.SelectedValue == "")
            {
                rCboPaises.CssClass = "cssTxtInvalid";
                rCboPaises.BorderWidth = Unit.Pixel(1);
                rCboPaises.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;

            }
            else
            {
                rCboPaises.BorderColor = System.Drawing.Color.Transparent;

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
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, PagLoc_AgeCve);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "AGENT");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

        //dt_referencias.DataSource = ds;
        //dt_referencias.DataBind();

    }
    DataSet dsOtrosDatos()
    {
        DataSet ds = new DataSet();
     
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgenteOtrosDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, PagLoc_AgeCve);
        ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "AGENT");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    public void GuardaOtrosDatos()
    {
        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var valTxt = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            string DatCve = DataListOtrosDatos.DataKeys[Countkeyarray].ToString();
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AgenteOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, PagLoc_AgeCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "AGENT");
            ProcBD.AgregarParametrosProcedimiento("@otroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            Countkeyarray += 1;
        }
    }

    public void LlenaDLOtrosDatos()
    {
        DataListOtrosDatos.DataSource = dsOtrosDatos();
        DataListOtrosDatos.RepeatColumns = 2;
        DataListOtrosDatos.DataBind();
    }

    #endregion

}