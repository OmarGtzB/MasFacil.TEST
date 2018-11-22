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

public partial class DC_MttoClieDatosGrales : System.Web.UI.Page
{
    
    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string PagLoc_CliCve;

    //Variables para Parametros
    private int longTotal;
    private int longClie;
    private int longSubClie;
    private string charFiller;
    private int posClie;
    private int posSubClie;

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
        //LimpiarUi();
        DesHabil(true);
        DesHabilNotif(true);
        DesHabilExped(true);
        DeshabilCombos();
        DatosDom(1);
        DatosDom(2);
        DatosDom(3);
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() 

           )
        {
            //LimpiarUi();
        }
        else
        {
            
        }

        int RadTab = 0;
        RadTab = Convert.ToInt32(RadTabStrip1.SelectedTab.Value);

        if (RadTab == 1)
        {
            LimpiaControl();
        }
        else if (RadTab == 2)
        {
            LimpiaControlNotif();
        }
        else if (RadTab == 3)
        {
            LimpiaControlExped();
        }
     
            DatosDom(RadTab);
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rCboEstado.BorderColor = System.Drawing.Color.Transparent;
            rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;

            rCboEntidadFedNotif.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNotif.BorderColor = System.Drawing.Color.Transparent;

            rCboEntidadFedExped.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExped.BorderColor = System.Drawing.Color.Transparent;
            DeshabilCombos();
            //Combos Fiscal
            if (rCboPaises.SelectedValue == "")
            {
                rCboPaises.ClearSelection();
            }

            if (rCboEstado.SelectedValue == "")
            {
                rCboEstado.ClearSelection();
            }
            else
            {
                rCboEstado.Enabled = true;
            }

            if (rCboPoblacion.SelectedValue == "")
            {
                rCboPoblacion.ClearSelection();
            }
            else
            {
                rCboPoblacion.Enabled = true;
            }

            //Combos Notificaciones
            if (rCboPaisNotif.SelectedValue == "")
            {
                rCboPaisNotif.ClearSelection();
            }

            if (rCboEntidadFedNotif.SelectedValue == "")
            {
                rCboEntidadFedNotif.ClearSelection();
            }
            else
            {
                rCboEntidadFedNotif.Enabled = true;
            }

            if (rCboProvinciaNotif.SelectedValue == "")
            {
                rCboProvinciaNotif.ClearSelection();
            }
            else
            {
                rCboProvinciaNotif.Enabled = true;
            }

            //Combos Expedicion
            if (rCboPaisExped.SelectedValue == "")
            {
                rCboPaisExped.ClearSelection();
            }

            if (rCboEntidadFedExped.SelectedValue == "")
            {
                rCboEntidadFedExped.ClearSelection();
            }
            else
            {
                rCboEntidadFedExped.Enabled = true;
            }

            if (rCboProvinciaExped.SelectedValue == "")
            {
                rCboProvinciaExped.ClearSelection();
            }
            else
            {
                rCboProvinciaExped.Enabled = true;
            }
        }
        if (PagLoc_CliCve == "")
        {
            if (RadTab == 1)
            {
                rCboPaises.ClearSelection();
                rCboEstado.ClearSelection();
                rCboEstado.Enabled = false;
                rCboPoblacion.ClearSelection();
                rCboPoblacion.Enabled = false;
            }
            else if (RadTab == 2)
            {
                rCboPaisNotif.ClearSelection();
                rCboEntidadFedNotif.ClearSelection();
                rCboEntidadFedNotif.Enabled = false;
                rCboProvinciaNotif.ClearSelection();
                rCboProvinciaNotif.Enabled = false;
            }
            else if (RadTab == 3)
            {
                rCboPaisExped.ClearSelection();
                rCboEntidadFedExped.ClearSelection();
                rCboEntidadFedExped.Enabled = false;
                rCboProvinciaExped.ClearSelection();
                rCboProvinciaExped.Enabled = false;
            }
        }


      }



    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        LimpiaControl();
        LimpiaControlNotif();
        LimpiaControlExped();
        InicioPagina();
        //fillCboAgrupacionesDato();
    }

    //COMBOS DOM FISCAL
    protected void rCboPaises_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboEstado.ClearSelection();
        rCboEstado.Enabled = true;
        rCboPoblacion.ClearSelection();
        rCboPoblacion.Enabled = false;
       

        if (rCboPaises.SelectedValue !="")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaises.SelectedValue.ToString(), ref rCboEstado, true, false);
        }else
        {
            rCboEstado.Enabled = false;
            rCboEstado.ClearSelection();
            rCboPoblacion.Enabled = false;
            rCboPoblacion.ClearSelection();
            LimpiaControl();
            //quitar bordes de validador
            rCboPaises.BorderColor = System.Drawing.Color.Transparent;
            rCboEstado.BorderColor = System.Drawing.Color.Transparent;
            rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;
        }
     
    }
    
    protected void rCboEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboPoblacion.ClearSelection();
        rCboPoblacion.Enabled = true;

        if (rCboEstado.SelectedValue !="")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaises.SelectedValue,  rCboEstado.SelectedValue.ToString(), ref rCboPoblacion, true, false);
        }else
        {
            rCboPoblacion.ClearSelection();
            rCboPoblacion.Enabled = false;
        }
        


    }

    //COMBOS DOM NOTIFICACION
    protected void rCboPaisNotif_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvinciaNotif.ClearSelection();
        rCboProvinciaNotif.Enabled = false;
        rCboEntidadFedNotif.Enabled = true;
        if (rCboPaisNotif.SelectedValue !="")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNotif.SelectedValue.ToString(), ref rCboEntidadFedNotif, true, false);
        }else
        {
            rCboEntidadFedNotif.Enabled = false;
            rCboEntidadFedNotif.ClearSelection();
            rCboProvinciaNotif.Enabled = false;
            rCboProvinciaNotif.ClearSelection();
            LimpiaControlNotif();
            
            rCboPaisNotif.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedNotif.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNotif.BorderColor = System.Drawing.Color.Transparent;
        }
        
    }

    protected void rCboEntidadFedNotif_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
         rCboProvinciaNotif.ClearSelection();
        rCboProvinciaNotif.Enabled = true;

        if (rCboEntidadFedNotif.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisNotif.SelectedValue,   rCboEntidadFedNotif.SelectedValue.ToString(), ref rCboProvinciaNotif, true, false);
        }
        else
        {
            rCboProvinciaNotif.Enabled = false;
            rCboProvinciaNotif.ClearSelection();
        }
    }

    //COMBOS DOM EXPEDICION

    protected void rCboPaisExped_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //rCboProvinciaExped.ClearSelection();
        //rCboProvinciaExped.Enabled = false;
        //rCboEntidadFedExped.Enabled = true;
        //FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExped.SelectedValue.ToString(), ref rCboEntidadFedExped, true, false);
        rCboProvinciaExped.ClearSelection();
        rCboProvinciaExped.Enabled = false;
        rCboEntidadFedExped.Enabled = true;

        if (rCboPaisExped.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExped.SelectedValue.ToString(), ref rCboEntidadFedExped, true, false);
        }
        else
        {
            rCboEntidadFedExped.Enabled = false;
            rCboEntidadFedExped.ClearSelection();
            rCboProvinciaExped.Enabled = false;
            rCboProvinciaExped.ClearSelection();
            LimpiaControlExped();

            //quitar bordes de validador
            rCboPaisExped.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedExped.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExped.BorderColor = System.Drawing.Color.Transparent;

        }
    }

    protected void rCboEntidadFedExped_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //rCboProvinciaExped.ClearSelection();
        //rCboProvinciaExped.Enabled = true;
        //FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboEntidadFedExped.SelectedValue.ToString(), ref rCboProvinciaExped, true, false);
        rCboProvinciaExped.ClearSelection();
        rCboProvinciaExped.Enabled = true;

        if (rCboEntidadFedExped.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPaisExped.SelectedValue,   rCboEntidadFedExped.SelectedValue.ToString(), ref rCboProvinciaExped, true, false);
        }
        else
        {
            rCboProvinciaExped.ClearSelection();
            rCboProvinciaExped.Enabled = false;
        }
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        PagLoc_CliCve = Convert.ToString(Session["folio_Selection"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);


        //Obtener Variables de Parametros
        
        longClie = getParamIntClie(1);
        longSubClie = getParamIntClie(2);
        posClie = getParamIntClie(3);
        posSubClie = getParamIntClie(4);
        charFiller = getParamStrClie(5);

        //Pasar Valores al Layout
        hdfLongClie.Value = longClie.ToString();
        hdfLongSubClie.Value = longSubClie.ToString();
        hdfCharFormat.Value = charFiller;
        hdfPosClie.Value = posClie.ToString();
        hdfPosSubClie.Value = posSubClie.ToString();

        rTxtCliente.MaxLength = longClie;
        rTxtSubClie.MaxLength = longSubClie;
        
    }
    

    private void InicioPagina()
    {

        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        //llenar_cboPais();
        rCboPaises.EmptyMessage = "Seleccionar";
        rCboEstado.EmptyMessage = "Seleccionar";
        rCboPoblacion.EmptyMessage = "Seleccionar";

        rCboPaisNotif.EmptyMessage = "Seleccionar";
        rCboEntidadFedNotif.EmptyMessage = "Seleccionar";
        rCboProvinciaNotif.EmptyMessage = "Seleccionar";


        rCboPaisExped.EmptyMessage = "Seleccionar";
        rCboEntidadFedExped.EmptyMessage = "Seleccionar";
        rCboProvinciaExped.EmptyMessage = "Seleccionar";


        //Comprobar si es nuevo o se editara


        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();

    

        if (PagLoc_CliCve == "")
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
            rBtnModificar.Enabled = false;
            LimpiarUi();
            disEnableUi(1);
            DesHabil(true);
            DesHabilNotif(true);
            DesHabilExped(true);
            rCboEstado.Enabled = false;
            rCboPoblacion.Enabled = false;

            rCboEntidadFedNotif.Enabled = false;
            rCboProvinciaNotif.Enabled = false;

            rCboEntidadFedExped.Enabled = false;
            rCboProvinciaExped.Enabled = false;

            
        }
        else
        {
            //hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
            LlenarUi(PagLoc_CliCve);
            disEnableUi(3);
            rBtnLimpiar.Enabled = true;
            rBtnModificar.Enabled = true;
            DesHabil(false);
            DesHabilNotif(false);
            DesHabilExped(false);
        }
        LlenaDLOtrosDatos();

        rCboPaises.BorderWidth = Unit.Pixel(0);
        rCboPaises.BorderColor = System.Drawing.Color.Transparent;

        rCboEstado.BorderWidth = Unit.Pixel(0);
        rCboEstado.BorderColor = System.Drawing.Color.Transparent;

        rCboPoblacion.BorderWidth = Unit.Pixel(0);
        rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;

        

        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            rTxtSubClie.Visible = true;
        }
        else {
            rLblSubClie.Visible = false;
            rTxtSubClie.Visible = false;
        }


        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaises, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisNotif, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisExped, true, false);

        LimpiaControl();
        LimpiaControlExped();
        LimpiaControlNotif();

        DatosDom(1);
        DatosDom(2);
        DatosDom(3);


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

    public void LlenaDLOtrosDatos()
    {
        DataListOtrosDatos.DataSource = dsOtrosDatos();
        DataListOtrosDatos.RepeatColumns = 2;
        DataListOtrosDatos.DataBind();
    }

    DataSet dsOtrosDatos()
    {
        DataSet ds = new DataSet();
        string cliCve;
        cliCve = rTxtCliente.Text;
        cliCve += rTxtSubClie.Text;

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteOtrosDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
        ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "CLIE");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    public void GuardaOtrosDatos()
    {
        string cliCve;
        cliCve = rTxtCliente.Text;
        cliCve += rTxtSubClie.Text;


        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var valTxt = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            string DatCve = DataListOtrosDatos.DataKeys[Countkeyarray].ToString();


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);

            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "CLIE");
            ProcBD.AgregarParametrosProcedimiento("@otroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
           
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            Countkeyarray += 1;

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
        
        try
        {
            DataSet ds = new DataSet();
            string cliCve;

            longClie = getParamIntClie(1);
            longSubClie = getParamIntClie(2);

            cliCve = rTxtCliente.Text;
            cliCve += rTxtSubClie.Text;

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Clientes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliCveClie", DbType.StringFixedLength, longClie, ParameterDirection.Input, rTxtCliente.Text);
            ProcBD.AgregarParametrosProcedimiento("@cliCveSubClie", DbType.StringFixedLength, longSubClie, ParameterDirection.Input, rTxtSubClie.Text);
            ProcBD.AgregarParametrosProcedimiento("@clieNom", DbType.String, 200, ParameterDirection.Input, rTxtNombre.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@clieAbr", DbType.String, 15, ParameterDirection.Input, rTxtAbr.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@clieRegFis", DbType.String, 50, ParameterDirection.Input, rTxtReg.Text.Trim());
            
            ProcBD.AgregarParametrosProcedimiento("@longClie", DbType.Int64, 1, ParameterDirection.Input, Convert.ToInt64(hdfLongClie.Value));
            ProcBD.AgregarParametrosProcedimiento("@posClie", DbType.Int64, 1, ParameterDirection.Input, Convert.ToInt64(hdfPosClie.Value));
            ProcBD.AgregarParametrosProcedimiento("@longSclie", DbType.Int64, 1, ParameterDirection.Input, Convert.ToInt64(hdfLongSubClie.Value));
            ProcBD.AgregarParametrosProcedimiento("@posSclie", DbType.Int64, 1, ParameterDirection.Input, Convert.ToInt64(hdfPosSubClie.Value));
            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                 EjecutaSpRefVar();
                GuardaOtrosDatos();

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

                
                
                

                if (sEjecEstatus == "1") {
                    PagLoc_CliCve = cliCve;
                    Session["folio_Selection"] = cliCve;

                    if (rCboPaises.SelectedValue != "")
                    {
                        GuardarDom(1);
                    }
                    if (rCboPaisNotif.SelectedValue !="")
                    {
                        GuardarDom(2);
                    }
                    if (rCboPaisExped.SelectedValue !="")
                    {
                        GuardarDom(3);
                    }

                    EliminaDom();
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


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";



        rCboPaises.BorderWidth = Unit.Pixel(1);
        rCboPaises.BorderColor = System.Drawing.Color.Transparent;

        rCboEstado.BorderWidth = Unit.Pixel(1);
        rCboEstado.BorderColor = System.Drawing.Color.Transparent;

        rCboPoblacion.BorderWidth = Unit.Pixel(1);
        rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;


        if (rTxtCliente.Text.Length + rTxtSubClie.Text.Length > 20)
        {
            sResult = "Clave y SubClave ";

            return sResult;
        }




        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rTxtCliente.Text.Trim() == "")
            {
                rTxtCliente.CssClass = "cssTxtInvalid";
                rTxtCliente.DataBind();
               // rTxtCliente.Focus();
                camposInc += 1;
            }
            else { rTxtCliente.CssClass = "cssTxtEnabled"; }

            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                rTxtNombre.DataBind();
                // rTxtNombre.Focus();
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                rTxtAbr.DataBind();
                //rTxtAbr.Focus();
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }

            if (rCboPaises.SelectedValue == "")
            {
                rCboPaises.BorderWidth = Unit.Pixel(1);
                rCboPaises.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            } else { rCboPaises.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboPaisNotif.SelectedValue == "")
            {
                if (txt_coloniaNotif.Text != "" || txt_calleNotif.Text != "" || rdNumericExteriorNotif.Text != "" || rdNumericInteriorNotif.Text != ""
              || txt_callesAleNotif.Text != "" || rdNumericCodigoPostalNotif.Text != "" || txt_referenciasNotif.Text != "" || txt_telefono1Notif.Text != ""
              || txt_telefono2Notif.Text != "" || txt_faxNotif.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }


            if (rCboPaisExped.SelectedValue == "")
            {
                if (txt_coloniaExped.Text != "" || txt_calleExped.Text != "" || rdNumericExteriorExped.Text != "" || rdNumericInteriorExped.Text != ""
                             || txt_callesAleExped.Text != "" || rdNumericCodigoPostalExped.Text != "" || txt_referenciasExped.Text != "" || txt_telefono1Exped.Text != ""
                             || txt_telefono2Exped.Text != "" || txt_faxExped.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
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
                rTxtNombre.DataBind();
                // rTxtNombre.Focus();
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                rTxtAbr.DataBind();
                // rTxtAbr.Focus();
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }


            if (rCboPaises.SelectedValue == "")
            {
                rCboPaises.BorderWidth = Unit.Pixel(1);
                rCboPaises.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboPaises.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboPaisNotif.SelectedValue == "")
            {
                if (txt_coloniaNotif.Text != "" || txt_calleNotif.Text != "" || rdNumericExteriorNotif.Text != "" || rdNumericInteriorNotif.Text != ""
              || txt_callesAleNotif.Text != "" || rdNumericCodigoPostalNotif.Text != "" || txt_referenciasNotif.Text != "" || txt_telefono1Notif.Text != ""
              || txt_telefono2Notif.Text != "" || txt_faxNotif.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }


            if (rCboPaisExped.SelectedValue == "")
            {
                if (txt_coloniaExped.Text != "" || txt_calleExped.Text != "" || rdNumericExteriorExped.Text != "" || rdNumericInteriorExped.Text != ""
                             || txt_callesAleExped.Text != "" || rdNumericCodigoPostalExped.Text != "" || txt_referenciasExped.Text != "" || txt_telefono1Exped.Text != ""
                             || txt_telefono2Exped.Text != "" || txt_faxExped.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
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
    private void EjecutaSpRefVar()
    {

        string cliCve;
        cliCve = rTxtCliente.Text;
        cliCve += rTxtSubClie.Text;

        try
        {

            foreach (DataListItem dli in dt_referencias.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_ref") as RadTextBox;
                secRef = dli.ItemIndex + 1;

                //if (valRef.Text.Trim() != "")
                //{
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ClienteRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "CLIE");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                    if (valRef.Text != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, valRef.Text);
                    }
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                //}


            }




            foreach (DataListItem dli in dt_variables.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_var") as RadNumericTextBox;
                secRef = dli.ItemIndex + 1;

                //if (valRef.Text.Trim() != "")
                //{

                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ClienteRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "CLIE");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    if (valRef.Text != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valRef.Text));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


                //}


            }


        }


        catch (Exception)
        {

            throw;
        }

    }

    #endregion

    #region FUNCIONES


    private int getParamIntClie(int paramSec)
    {
        int paramValue;
        paramValue = 0;

        try
        {


            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametros";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 20, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "CLIECVE");
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 1, ParameterDirection.Input, paramSec);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {


                paramValue = Int32.Parse(ds.Tables[0].Rows[0]["parmValInt"].ToString());

            }

        }
        catch (Exception ex)
        {
            paramValue = 100;
            throw;
        }
        
        return paramValue;
    }
    
    private string getParamStrClie(int paramSec)
    {
        string paramValue;
        paramValue = "";

        try
        {


            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametros";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 20, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "CLIECVE");
            ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, paramSec);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {


                paramValue = ds.Tables[0].Rows[0]["parmValStr"].ToString();

            }

        }
        catch (Exception ex)
        {
            paramValue = ex.ToString();
            throw;
        }

        return paramValue;
    }

    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_CliCve);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "CLIE");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);
        
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        return ds;

        //dt_referencias.DataSource = ds;
        //dt_referencias.DataBind();

    }

    public void EliminaDom()
    {

        if (rCboPaises.SelectedValue == "")
        {
            if (iddom.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Clientes";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int32, 0, ParameterDirection.Input, iddom.Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }

        }
        if (rCboPaisNotif.SelectedValue == "")
        {
            if (iddomNotif.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Clientes";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 4);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int32, 0, ParameterDirection.Input, iddomNotif.Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
        }
        if (rCboPaisExped.SelectedValue == "")
        {
            if (iddomExped.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Clientes";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 4);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int32, 0, ParameterDirection.Input, iddomExped.Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
        }


    }

    public void DeshabilCombos()
    {
        ////
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
        ///
        if (rCboPaisNotif.SelectedValue != "")
        {
            rCboEntidadFedNotif.Enabled = true;
        }
        else
        {
            rCboEntidadFedNotif.Enabled = false;
        }
        if (rCboEntidadFedNotif.SelectedValue != "")
        {
            rCboProvinciaNotif.Enabled = true;
        }
        else
        {
            rCboProvinciaNotif.Enabled = false;
        }
        ///
        if (rCboPaisExped.SelectedValue != "")
        {
            rCboEntidadFedExped.Enabled = true;
        }
        else
        {
            rCboEntidadFedExped.Enabled = false;
        }
        if (rCboEntidadFedExped.SelectedValue != "")
        {
            rCboProvinciaExped.Enabled = true;
        }
        else
        {
            rCboProvinciaExped.Enabled = false;
        }
        
    }
    public void LimpiaControl()
    {
        rTxtColonia.Text = "";
        rTxtCalle.Text = "";
        rTxtNExt.Text = "";
        rTxtNInt.Text = "";
        rTxtCllsA.Text = "";
        rTxtRef.Text = "";
        rTxtCP.Text = "";
        rTxtTel1.Text = "";
        rTxtTel2.Text = "";
        rTxtFax.Text = "";
        rCboPaises.ClearSelection();
        rCboEstado.ClearSelection();
        rCboPoblacion.ClearSelection();

        rCboPaises.BorderColor = System.Drawing.Color.Transparent;
        rCboPoblacion.BorderColor = System.Drawing.Color.Transparent;
        rCboEstado.BorderColor = System.Drawing.Color.Transparent;

        rTxtCliente.CssClass = "cssTxtEnabled";
        rTxtNombre.CssClass = "cssTxtEnabled";
        rTxtAbr.CssClass = "cssTxtEnabled";
    }

    public void LimpiaControlNotif()
    {
        txt_coloniaNotif.Text = "";
        txt_calleNotif.Text = "";
        rdNumericExteriorNotif.Text = "";
        rdNumericInteriorNotif.Text = "";
        txt_callesAleNotif.Text = "";
        txt_referenciasNotif.Text = "";
        rdNumericCodigoPostalNotif.Text = "";
        txt_telefono1Notif.Text = "";
        txt_telefono2Notif.Text = "";
        txt_faxNotif.Text = "";
        rCboPaisNotif.ClearSelection();
        rCboEntidadFedNotif.ClearSelection();
        rCboProvinciaNotif.ClearSelection();

        rCboEntidadFedNotif.BorderColor = System.Drawing.Color.Transparent;
        rCboProvinciaNotif.BorderColor = System.Drawing.Color.Transparent;
    }

    public void LimpiaControlExped()
    {
        txt_coloniaExped.Text = "";
        txt_calleExped.Text = "";
        rdNumericExteriorExped.Text = "";
        rdNumericInteriorExped.Text = "";
        txt_callesAleExped.Text = "";
        txt_referenciasExped.Text = "";
        rdNumericCodigoPostalExped.Text = "";
        txt_telefono1Exped.Text = "";
        txt_telefono2Exped.Text = "";
        txt_faxExped.Text = "";
        rCboPaisExped.ClearSelection();
        rCboEntidadFedExped.ClearSelection();
        rCboProvinciaExped.ClearSelection();


        rCboEntidadFedExped.BorderColor = System.Drawing.Color.Transparent;
        rCboProvinciaExped.BorderColor = System.Drawing.Color.Transparent;

    }


    public void GuardarDom(int CveDom)
    {

        string cliCve;
        cliCve = rTxtCliente.Text;
        cliCve += rTxtSubClie.Text;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 5);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);

        if (CveDom == 1)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaises.SelectedValue);
            if (rCboEstado.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEstado.SelectedValue);
            }
            if (rCboPoblacion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboPoblacion.SelectedValue);

            }
            if (rTxtColonia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColonia.Text.Trim());
            }
            if (rTxtCalle.Text.Trim() !="" )
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalle.Text.Trim());
            }
            if (rTxtCP.Text.Trim()!="")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCP.Text.Trim());
            }
           
            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);

            if (rTxtNInt.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNInt.Text.Trim());
            }
            if (rTxtNExt.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNExt.Text.Trim());
            }
            if (rTxtCllsA.Text.Trim() !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCllsA.Text.Trim());
            }
            if (rTxtRef.Text.Trim()!="")
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
            
            

            if (iddom.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int64, 0, ParameterDirection.Input, iddom.Text.Trim());
            }
        }
        //notificaciones
        else if (CveDom == 2)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaisNotif.SelectedValue);
            if (rCboEntidadFedNotif.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFedNotif.SelectedValue);
            }
            if (rCboProvinciaNotif.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvinciaNotif.SelectedValue);
            }
            if (txt_coloniaNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, txt_coloniaNotif.Text.Trim());
            }
            if (txt_calleNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, txt_calleNotif.Text.Trim());
            }
            if (rdNumericCodigoPostalNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rdNumericCodigoPostalNotif.Text.Trim());

            }
           
            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rdNumericInteriorNotif.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rdNumericInteriorNotif.Text.Trim());
            }
            if (rdNumericExteriorNotif.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rdNumericExteriorNotif.Text.Trim());
            }
            if (txt_callesAleNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, txt_callesAleNotif.Text.Trim());
            }
            if (txt_referenciasNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, txt_referenciasNotif.Text.Trim());
            }
            if (txt_referenciasNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, txt_referenciasNotif.Text.Trim());
            }
            if (txt_telefono1Notif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, txt_telefono1Notif.Text.Trim());
            }
            if (txt_telefono2Notif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, txt_telefono2Notif.Text.Trim());
            }
            if (txt_faxNotif.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, txt_faxNotif.Text.Trim());
            }
            

            if (iddomNotif.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int64, 0, ParameterDirection.Input, iddomNotif.Text.Trim());
            }
        }
        else if (CveDom == 3)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaisExped.SelectedValue);
            if (rCboEntidadFedExped.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFedExped.SelectedValue);
            }
            if (rCboProvinciaExped.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvinciaExped.SelectedValue);
            }
            if (txt_coloniaExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, txt_coloniaExped.Text.Trim());
            }
            if (txt_calleExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, txt_calleExped.Text.Trim());
            }
            if (rdNumericCodigoPostalExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rdNumericCodigoPostalExped.Text.Trim());
            }
           
           
            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rdNumericInteriorExped.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rdNumericInteriorExped.Text.Trim());
            }
            if (rdNumericExteriorExped.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rdNumericExteriorExped.Text.Trim());
            }
            if (txt_callesAleExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, txt_callesAleExped.Text.Trim());
            }
            if (txt_referenciasExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, txt_referenciasExped.Text.Trim());
            }
            if (txt_telefono1Exped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, txt_telefono1Exped.Text.Trim());
            }
            if (txt_telefono2Exped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, txt_telefono2Exped.Text.Trim());
            }

            if (txt_faxExped.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, txt_faxExped.Text.Trim());
            }
            
            if (iddomExped.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int64, 0, ParameterDirection.Input, iddomExped.Text.Trim());
            }
        }

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }

    public void DatosDom(int CveDom)
    {
        string cliCve;

        cliCve = rTxtCliente.Text;
        cliCve += rTxtSubClie.Text;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 56);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, CveDom);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (CveDom == 1)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                rTxtCP.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCP"]);
                rTxtColonia.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCol"]);
                rTxtCalle.Text = Convert.ToString(ds.Tables[0].Rows[0]["domClle"]);
                rTxtCllsA.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCllsA"]);
                rTxtNInt.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNInt"]);
                rTxtNExt.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNExt"]);
                rTxtRef.Text = Convert.ToString(ds.Tables[0].Rows[0]["domRef"]);
                rTxtTel1.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel"]);
                rTxtTel2.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel2"]);
                rTxtFax.Text = Convert.ToString(ds.Tables[0].Rows[0]["domFax"]);


                if (ds.Tables[0].Rows[0]["domId"].ToString() != "")
                {
                    iddom.Text = Convert.ToString(ds.Tables[0].Rows[0]["domId"]);
                }

                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaises, true, false);
                rCboPaises.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaises.SelectedValue.ToString(), ref rCboEstado, true, false);

                rCboEstado.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]) == "")
                    {
                        rCboEstado.Enabled = true;
                    }
                }
                

                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaises.SelectedValue ,  rCboEstado.SelectedValue.ToString(), ref rCboPoblacion, true, false);
                rCboPoblacion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
                //rCboPoblacion.Enabled = true;
            }
            else
            {
                //LimpiaControl();
            }
        }
        else if (CveDom == 2)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                rdNumericCodigoPostalNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCP"]);
                txt_coloniaNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCol"]);
                txt_calleNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domClle"]);
                txt_callesAleNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCllsA"]);
                rdNumericInteriorNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNInt"]);
                rdNumericExteriorNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNExt"]);
                txt_referenciasNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domRef"]);
                txt_telefono1Notif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel"]);
                txt_telefono2Notif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel2"]);
                txt_faxNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domFax"]);

                if (ds.Tables[0].Rows[0]["domId"].ToString() != "")
                {
                    iddomNotif.Text = Convert.ToString(ds.Tables[0].Rows[0]["domId"]);
                }
                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisNotif, true, false);
                rCboPaisNotif.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNotif.SelectedValue.ToString(), ref rCboEntidadFedNotif, true, false);

                rCboEntidadFedNotif.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]) == "")
                    {
                        rCboEntidadFedNotif.Enabled = true;
                    }
                }
                

                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisNotif.SelectedValue,  rCboEntidadFedNotif.SelectedValue.ToString(), ref rCboProvinciaNotif, true, false);
                rCboProvinciaNotif.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
                // rCboProvinciaNotif.Enabled = true;
            }
            else
            {
                //LimpiaControl();
            }
        }
        else if (CveDom == 3)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                rdNumericCodigoPostalExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCP"]);
                txt_coloniaExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCol"]);
                txt_calleExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domClle"]);
                txt_callesAleExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCllsA"]);
                rdNumericInteriorExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNInt"]);
                rdNumericExteriorExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNExt"]);
                txt_referenciasExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domRef"]);
                txt_telefono1Exped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel"]);
                txt_telefono2Exped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel2"]);
                txt_faxExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domFax"]);

                if (ds.Tables[0].Rows[0]["domId"].ToString() != "")
                {
                    iddomExped.Text = Convert.ToString(ds.Tables[0].Rows[0]["domId"]);
                }


                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisExped, true, false);
                rCboPaisExped.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExped.SelectedValue.ToString(), ref rCboEntidadFedExped, true, false);
                rCboEntidadFedExped.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]) == "")
                    {
                        rCboEntidadFedExped.Enabled = true;
                    }
                }
                

                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisExped.SelectedValue,  rCboEntidadFedExped.SelectedValue.ToString(), ref rCboProvinciaExped, true, false);
                rCboProvinciaExped.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
                //rCboProvinciaExped.Enabled = true;
            }
        }
        else
        {
            //LimpiaControl();
        }


    }


    public void DesHabil(bool ena)
    {


        rTxtColonia.Enabled = ena;
        rTxtCalle.Enabled = ena;
        rTxtNExt.Enabled = ena;
        rTxtNInt.Enabled = ena;
        rTxtCllsA.Enabled = ena;
        rTxtCP.Enabled = ena;
        rTxtRef.Enabled = ena;
        rTxtTel1.Enabled = ena;
        rTxtTel2.Enabled = ena;
        rTxtFax.Enabled = ena;
        rBtnGuardar.Enabled = ena;
        rBtnCancelar.Enabled = ena;
        rCboPaises.Enabled = ena;
        rCboEstado.Enabled = ena;
        rCboPoblacion.Enabled = ena;
        rCboPaisNotif.Enabled = ena;

        //rCboEntidadFedNotif.Enabled = ena;
        //rCboProvinciaNotif.Enabled = ena;
        //rCboPaisExped.Enabled = ena;
        //rCboEntidadFedExped.Enabled = ena;
        //rCboProvinciaExped.Enabled = ena;


    }

    public void DesHabilNotif(bool ena)
    {
        txt_coloniaNotif.Enabled = ena;
        txt_calleNotif.Enabled = ena;
        rdNumericExteriorNotif.Enabled = ena;
        rdNumericInteriorNotif.Enabled = ena;
        txt_callesAleNotif.Enabled = ena;
        rdNumericCodigoPostalNotif.Enabled = ena;
        txt_referenciasNotif.Enabled = ena;
        txt_telefono1Notif.Enabled = ena;
        txt_telefono2Notif.Enabled = ena;
        txt_faxNotif.Enabled = ena;
        rCboPaisNotif.Enabled = ena;
        rCboEntidadFedNotif.Enabled = ena;
        rCboProvinciaNotif.Enabled = ena;
        rCboPaisNotif.Enabled = ena;
        rCboEntidadFedNotif.Enabled = ena;
        rCboProvinciaNotif.Enabled = ena;
        rCboPaisNotif.Enabled = ena;
        rCboEntidadFedNotif.Enabled = ena;
        rCboProvinciaNotif.Enabled = ena;

    }

    public void DesHabilExped(bool ena)
    {
        txt_coloniaExped.Enabled = ena;
        txt_calleExped.Enabled = ena;
        rdNumericExteriorExped.Enabled = ena;
        rdNumericInteriorExped.Enabled = ena;
        txt_callesAleExped.Enabled = ena;
        rdNumericCodigoPostalExped.Enabled = ena;
        txt_referenciasExped.Enabled = ena;
        txt_telefono1Exped.Enabled = ena;
        txt_telefono2Exped.Enabled = ena;
        txt_faxExped.Enabled = ena;
        rCboPaisExped.Enabled = ena;
        rCboEntidadFedExped.Enabled = ena;
        rCboProvinciaExped.Enabled = ena;
        rCboPaisExped.Enabled = ena;
        rCboEntidadFedExped.Enabled = ena;
        rCboProvinciaExped.Enabled = ena;
        rCboPaisExped.Enabled = ena;
        rCboEntidadFedExped.Enabled = ena;
        rCboProvinciaExped.Enabled = ena;

    }


    private void disEnableUi(int opc)
    {
        //1 = New - A 
        if (opc == 1)
        {
            rTxtCliente.Enabled = true;
            rTxtSubClie.Enabled = true;
            rTxtNombre.Enabled = true;
            rTxtAbr.Enabled = true;
            rTxtReg.Enabled = true;

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

            rTxtCliente.Enabled = false;
            rTxtSubClie.Enabled = false;
            rTxtNombre.Enabled = true;
            rTxtAbr.Enabled = true;
            rTxtReg.Enabled = true;

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

            rTxtCliente.Enabled = false;
            rTxtSubClie.Enabled = false;
            rTxtNombre.Enabled = false;
            rTxtAbr.Enabled = false;
            rTxtReg.Enabled = false;

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



    private void LimpiarUi()
    {

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rTxtCliente.Text = "";
            rTxtSubClie.Text = "";
        }


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

        rTxtReg.Text = "";
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

            LlenarUi(PagLoc_CliCve);
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
            ProcBD.NombreProcedimiento = "sp_Clientes";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                rTxtCliente.Text = ds.Tables[0].Rows[0]["cliCveClie"].ToString();
                rTxtSubClie.Text = ds.Tables[0].Rows[0]["cliCveSubClie"].ToString();
                rTxtAbr.Text = ds.Tables[0].Rows[0]["clieAbr"].ToString();
                rTxtNombre.Text = ds.Tables[0].Rows[0]["clieNom"].ToString();

                //foreach (RadComboBoxItem item in rCboPaises.Items)
                //{
                //    if (item.Text == ds.Tables[0].Rows[0]["paisDes"].ToString())
                //    {
                //        rCboPaises.SelectedIndex = item.Index;
                //    }
                //}


                //llenarCboEntidades(Int32.Parse(ds.Tables[0].Rows[0]["paisID"].ToString()));


                //foreach (RadComboBoxItem item in rCboEstado.Items)
                //{
                //    if (item.Text == ds.Tables[0].Rows[0]["entFDes"].ToString())
                //    {
                //        rCboEstado.SelectedIndex = item.Index;
                //    }
                //}

                //llenarCboProvincias(Int32.Parse(ds.Tables[0].Rows[0]["entId"].ToString()));

                //foreach (RadComboBoxItem item in rCboPoblacion.Items)
                //{
                //    if (item.Text == ds.Tables[0].Rows[0]["provDes"].ToString())
                //    {
                //        rCboPoblacion.SelectedIndex = item.Index;
                //    }
                //}

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

                rTxtReg.Text = ds.Tables[0].Rows[0]["clieRegFis"].ToString();

                //ShowAlert(sEjecEstatus, sEjecMSG);
            }





        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }



    #endregion







}