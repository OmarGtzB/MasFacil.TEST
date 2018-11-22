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

public partial class DC_MttProvDatosGenerales : System.Web.UI.Page
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
    private string PagLoc_folio_Selection;
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


    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        FnCtlsFillIn.RabComboBox_CondicionesPago(Pag_sConexionLog, Pag_sCompania, ref rCboCondicionesPago, true, false);
        FnCtlsFillIn.RadComboBox_TiposProveedor(Pag_sConexionLog, Pag_sCompania, ref rCboTipo, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPais, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisNoti, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisExp, true, false);
        dt_referenciasProv.DataSource = llenadatalistVarRef(1);
        dt_referenciasProv.DataBind();
        dt_variablesProv.DataSource = llenadatalistVarRef(2);
        dt_variablesProv.DataBind();

        rCboPais.EmptyMessage = "Seleccionar";
        rCboEntidadFed.EmptyMessage = "Seleccionar";
        rCboProvincia.EmptyMessage = "Seleccionar";

        rCboPaisNoti.EmptyMessage = "Seleccionar";
        rCboEntidadFedNoti.EmptyMessage = "Seleccionar";
        rCboProvinciaNoti.EmptyMessage = "Seleccionar";


        rCboPaisExp.EmptyMessage = "Seleccionar";
        rCboEntidadFedExp.EmptyMessage = "Seleccionar";
        rCboProvinciaExp.EmptyMessage = "Seleccionar";

        if (PagLoc_folio_Selection == "")
        {
            habilita();
            habil_ValRef();
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            rBtnModificar.Enabled = false;

            rCboEntidadFed.Enabled = false;
            rCboProvincia.Enabled = false;

            rCboEntidadFedNoti.Enabled = false;
            rCboProvinciaNoti.Enabled = false;

            rCboEntidadFedExp.Enabled = false;
            rCboProvinciaExp.Enabled = false;


        }
        else
        {
            rTxtClave.Enabled = false;
            rtxtNombre.Enabled = false;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            deshabilita();
            Desa_ValRef();
            pintaDatos();

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

    //combos DOM FISCAL
    protected void rCboPais_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        //rCboProvincia.ClearSelection();
        //rCboProvincia.Enabled = false;
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = false;
        rCboEntidadFed.Enabled = true;

        if (rCboPais.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        }
        else
        {
            rCboEntidadFed.Enabled = false;
            rCboEntidadFed.ClearSelection();
            rCboProvincia.Enabled = false;
            rCboProvincia.ClearSelection();
            LimpiaControl();
            //quitar bordes de validador
            rCboPais.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;
            rCboProvincia.BorderColor = System.Drawing.Color.Transparent;
        }


    }
    protected void rCboEntidadFed_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = true;
        if (rCboEntidadFed.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue, rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
        }
        else
        {
            rCboProvincia.Enabled = false;
            rCboProvincia.ClearSelection();
        }
    }
    
    //COMBOS DOM NOTIFICACIONES
    protected void rCboPaisNoti_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNoti.SelectedValue.ToString(), ref rCboEntidadFedNoti, true, false);
        //rCboProvinciaNoti.ClearSelection();
        //rCboProvinciaNoti.Enabled = false;
        rCboProvinciaNoti.ClearSelection();
        rCboProvinciaNoti.Enabled = false;
        rCboEntidadFedNoti.Enabled = true;

        if (rCboPaisNoti.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNoti.SelectedValue.ToString(), ref rCboEntidadFedNoti, true, false);
        }
        else
        {
            rCboEntidadFedNoti.Enabled = false;
            rCboEntidadFedNoti.ClearSelection();
            rCboProvinciaNoti.Enabled = false;
            rCboProvinciaNoti.ClearSelection();
            LimpiaControlNoti();

            //quitar bordes de validador
            rCboPaisNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNoti.BorderColor = System.Drawing.Color.Transparent;

        }
    }
    protected void rCboEntidadFedNoti_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvinciaNoti.ClearSelection();
        rCboProvinciaNoti.Enabled = true;

        if (rCboEntidadFedNoti.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisNoti.SelectedValue, rCboEntidadFedNoti.SelectedValue.ToString(), ref rCboProvinciaNoti, true, false);
        }
        else
        {
            rCboProvinciaNoti.Enabled = false;
            rCboProvinciaNoti.ClearSelection();
        }
    }

    //COMBOS DOM EXPEDICION
    protected void rCboPaisExp_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExp.SelectedValue.ToString(), ref rCboEntidadFedExp, true, false);
        //rCboProvinciaExp.ClearSelection();
        //rCboProvinciaExp.Enabled = false;
        rCboProvinciaExp.ClearSelection();
        rCboProvinciaExp.Enabled = false;
        rCboEntidadFedExp.Enabled = true;

        if (rCboPaisExp.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExp.SelectedValue.ToString(), ref rCboEntidadFedExp, true, false);
        }
        else
        {
            rCboEntidadFedExp.Enabled = false;
            rCboEntidadFedExp.ClearSelection();
            rCboProvinciaExp.Enabled = false;
            rCboProvinciaExp.ClearSelection();
            LimpiaControlExp();

            //quitar bordes de validador
            rCboPaisExp.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedExp.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExp.BorderColor = System.Drawing.Color.Transparent;

        }
    }
    protected void rCboEntidadFedExp_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvinciaExp.ClearSelection();
        rCboProvinciaExp.Enabled = true;

        if (rCboEntidadFedExp.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisExp.SelectedValue,   rCboEntidadFedExp.SelectedValue.ToString(), ref rCboProvinciaExp, true, false);
        }
        else
        {
            rCboProvinciaExp.ClearSelection();
            rCboProvinciaExp.Enabled = false;
        }
    }







    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
        
        pintaDatos();
        DeshabilCombos();
        
    }

    //public void DeshabilCombos()
    //{
    //    if (rCboEntidadFed.SelectedValue == "")
    //    {
    //        rCboEntidadFed.Enabled = false;
    //    }else
    //    {
    //        rCboEntidadFed.Enabled = true;
    //    }

    //    if (rCboProvincia.SelectedValue == "")
    //    {
    //        rCboProvincia.Enabled = false;
    //    }else
    //    {
    //        rCboProvincia.Enabled = true;
    //    }


    //    //notificaciones
    //    if (rCboEntidadFedNoti.SelectedValue == "")
    //    {
    //        rCboEntidadFedNoti.Enabled = false;
    //    }else
    //    {
    //        rCboEntidadFedNoti.Enabled = true;
    //    }

    //    if (rCboProvinciaNoti.SelectedValue == "")
    //    {
    //        rCboProvinciaNoti.Enabled = false;
    //    }else
    //    {
    //        rCboProvinciaNoti.Enabled = true;
    //    }

    //    //expedicion
    //    if (rCboEntidadFedExp.SelectedValue == "")
    //    {
    //        rCboEntidadFedExp.Enabled = false;
    //    }else
    //    {
    //        rCboEntidadFedExp.Enabled = true;
    //    }

    //    if (rCboProvinciaExp.SelectedValue == "")
    //    {
    //        rCboProvinciaExp.Enabled = false;
    //    }else
    //    {
    //        rCboProvinciaExp.Enabled = true;
    //    }

    //}

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        
        if (PagLoc_folio_Selection == "")
        {
            limpiar();
            Limpiar_ValRef();
            rCboTipo.ClearSelection();
            rCboCondicionesPago.ClearSelection();
            rCboPais.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rCboProvincia.ClearSelection();
            rTxtClave.Text = "";
            rtxtNombre.Text="";
            cssena();
            
        }
        else
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                int RadTab = 0;
                RadTab = Convert.ToInt32(RadTabStrip1.SelectedTab.Value);
                if (RadTab == 1)
                {
                    LimpiaControl();

                }
                else if (RadTab == 2)
                {
                    LimpiaControlNoti();
                }
                else if (RadTab == 3)
                {
                    LimpiaControlExp();
                }
               // habil_ValRef();
                DeshabilCombos();

                dt_referenciasProv.DataSource = llenadatalistVarRef(1);
                dt_referenciasProv.DataBind();

                dt_variablesProv.DataSource = llenadatalistVarRef(2);
                dt_variablesProv.DataBind();


            }


            pintaDatos();

            
            rCboPais.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;
            rCboProvincia.BorderColor = System.Drawing.Color.Transparent;

            rCboPaisNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNoti.BorderColor = System.Drawing.Color.Transparent;

            rCboPaisExp.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedExp.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExp.BorderColor = System.Drawing.Color.Transparent;
       



        }

    }


    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //InicioPagina();
        hdfBtnAccion.Value = "";
        if (PagLoc_folio_Selection == "")
        {
            limpiar();
            Limpiar_ValRef();
            deshabilita();
            Desa_ValRef();
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnCancelar.Enabled = false;
            rBtnGuardar.Enabled = false;

        }
        else
        {
            pintaDatos();
            dt_referenciasProv.DataSource = llenadatalistVarRef(1);
            dt_referenciasProv.DataBind();

            dt_variablesProv.DataSource = llenadatalistVarRef(2);
            dt_variablesProv.DataBind();
            deshabilita();
            Desa_ValRef();
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnCancelar.Enabled = false;
            rBtnGuardar.Enabled = false;




            rCboPais.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;
            rCboProvincia.BorderColor = System.Drawing.Color.Transparent;

            rCboPaisNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedNoti.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNoti.BorderColor = System.Drawing.Color.Transparent;

            rCboPaisExp.BorderColor = System.Drawing.Color.Transparent;
            rCboEntidadFedExp.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExp.BorderColor = System.Drawing.Color.Transparent;

            rCboPais.BorderWidth = Unit.Pixel(0);
            rCboEntidadFed.BorderWidth = Unit.Pixel(0);
            rCboProvincia.BorderWidth = Unit.Pixel(0);

            rCboPaisNoti.BorderWidth = Unit.Pixel(0);
            rCboEntidadFedNoti.BorderWidth = Unit.Pixel(0);
            rCboProvinciaNoti.BorderWidth = Unit.Pixel(0);

            rCboPais.BorderWidth = Unit.Pixel(0);
            rCboEntidadFed.BorderWidth = Unit.Pixel(0);
            rCboProvincia.BorderWidth = Unit.Pixel(0);
            


        }

    }

    #endregion
    
    #region METODOS

    #endregion

    #region FUNCIONES



    private DataSet llenadatalistVarRef(Int32 revaTip)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "PROV");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }


    public void guarda()
    {

        if (hdfBtnAccion.Value == "")
        {
            hdfBtnAccion.Value = "1";
        }
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, rTxtClave.Text);
            ProcBD.AgregarParametrosProcedimiento("@provNom", DbType.String, 50, ParameterDirection.Input, rtxtNombre.Text);
            ProcBD.AgregarParametrosProcedimiento("@conPagCve", DbType.String, 10, ParameterDirection.Input, rCboCondicionesPago.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@provTip", DbType.Int32, 2, ParameterDirection.Input, rCboTipo.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@provRegFis", DbType.String, 20, ParameterDirection.Input, rtxtRFC.Text);
            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                GuardaOtrosDatos();
                EjecutaSpRefVar();

                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();//modificacion de la tabla
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();//modificacion de la tabla

                ShowAlert(sEjecEstatus, sEjecMSG);

                

                if (sEjecEstatus == "1")
                {
                    if (rCboPais.SelectedValue != "")
                    {
                        GuardarDom(1);
                    }
                    if (rCboPaisNoti.SelectedValue != "")
                    {
                        GuardarDom(2);
                    }
                    if (rCboPaisExp.SelectedValue != "")
                    {
                        GuardarDom(3);
                    }
                    EliminaDom();


                    Desa_ValRef();
                    deshabilita();
                    PagLoc_folio_Selection = Convert.ToString(rTxtClave.Text.Trim());

                    Session["folio_Selection"] = Convert.ToString(rTxtClave.Text.Trim());
                    ControlesAccion();
                    Desa_ValRef();
                    deshabilita();
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                 
                    rBtnGuardar.Enabled = false;
                    rBtnCancelar.Enabled = false;
                    rBtnModificar.Enabled = true;
              
                }
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaAccion()
    {
        cssena();
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() || hdfBtnAccion.Value == "")
            {
                guarda();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                //EjecutaSpAccionEliminar();
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
        if (hdfBtnAccion.Value == "")
        {


            if (rTxtClave.Text == "")
            {

                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }


            if (rtxtNombre.Text.Trim() == "")
            {
                rtxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtNombre.CssClass = "cssTxtEnabled"; }

            if (rCboCondicionesPago.SelectedValue == "")
            {
                rCboCondicionesPago.CssClass = "cssTxtInvalid";
                rCboCondicionesPago.BorderWidth = Unit.Pixel(1);
                rCboCondicionesPago.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboCondicionesPago.BorderColor = System.Drawing.Color.Transparent;
                rCboCondicionesPago.BorderWidth = Unit.Pixel(0);
            }
            if (rCboTipo.SelectedValue.Trim() == "")
            {
                rCboTipo.CssClass = "cssTxtInvalid";
                rCboTipo.BorderWidth = Unit.Pixel(1);
                rCboTipo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboTipo.BorderColor = System.Drawing.Color.Transparent;
                rCboTipo.BorderWidth = Unit.Pixel(0);
            }

            if (rCboPais.SelectedValue == "")
            {
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboPais.BorderColor = System.Drawing.Color.Transparent;
                rCboPais.BorderWidth = Unit.Pixel(0);
            }
            if (rCboPaisNoti.SelectedValue == "")
            {
                if (rTxtColoniaNoti.Text != "" || rTxtCalleNoti.Text != "" || rTxtNoExtNoti.Text != "" || rTxtNoIntNoti.Text != ""
              || rTxtCallesAledanasNoti.Text != "" || rTxtCodigoPostalNoti.Text != "" || rTxtReferenciaNoti.Text != "" || rTxtTelefono1Noti.Text != ""
              || rTxtTelefono2Noti.Text != "" || rTxtFaxNoti.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }


            if (rCboPaisExp.SelectedValue == "")
            {
                if (rTxtColoniaExp.Text != "" || rTxtCalleExp.Text != "" || rTxtNoExtExp.Text != "" || rTxtNoIntExp.Text != ""
                             || rTxtCallesAledanasExp.Text != "" || rTxtCodigoPostalExp.Text != "" || rTxtReferenciaExp.Text != "" || rTxtTelefono1Exp.Text != ""
                             || rTxtTelefono2Exp.Text != "" || rTxtFaxExp.Text != "")
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
            if (PagLoc_folio_Selection == "")
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtClave.Text == "")
            {

                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rtxtNombre.Text.Trim() == "")
            {
                rtxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtNombre.CssClass = "cssTxtEnabled"; }
            
            if (rCboCondicionesPago.SelectedValue == "")
            {
                rCboCondicionesPago.CssClass = "cssTxtInvalid";
                rCboCondicionesPago.BorderWidth = Unit.Pixel(1);
                rCboCondicionesPago.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboCondicionesPago.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboTipo.SelectedValue.Trim() == "")
            {
                rCboTipo.CssClass = "cssTxtInvalid";
                rCboTipo.BorderWidth = Unit.Pixel(1);
                rCboTipo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboTipo.BorderColor = System.Drawing.Color.Transparent;
            }

            
          
            if (rCboPais.SelectedValue == "")
            {
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboPais.BorderColor = System.Drawing.Color.Transparent;
                rCboPais.BorderWidth = Unit.Pixel(0);
            }
            if (rCboPaisNoti.SelectedValue == "")
            {
                if (rTxtColoniaNoti.Text != "" || rTxtCalleNoti.Text != "" || rTxtNoExtNoti.Text != "" || rTxtNoIntNoti.Text != ""
              || rTxtCallesAledanasNoti.Text != "" || rTxtCodigoPostalNoti.Text != "" || rTxtReferenciaNoti.Text != "" || rTxtTelefono1Noti.Text != ""
              || rTxtTelefono2Noti.Text != "" || rTxtFaxNoti.Text != "")
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1037", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }


            if (rCboPaisExp.SelectedValue == "")
            {
                if (rTxtColoniaExp.Text != "" || rTxtCalleExp.Text != "" || rTxtNoExtExp.Text != "" || rTxtNoIntExp.Text != ""
                             || rTxtCallesAledanasExp.Text != "" || rTxtCodigoPostalExp.Text != "" || rTxtReferenciaExp.Text != "" || rTxtTelefono1Exp.Text != ""
                             || rTxtTelefono2Exp.Text != "" || rTxtFaxExp.Text != "")
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
        return sResult;

    }



    private void EjecutaSpRefVar()
    {

        try
        {

            foreach (DataListItem dli in dt_referenciasProv.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_ref_Ref") as RadTextBox;
                secRef = dli.ItemIndex + 1;
                if (valRef.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ProveedoresRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);


                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "PROV");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, valRef.Text);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
                else
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ProveedoresRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                    ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "PROV");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


                }
            }




            foreach (DataListItem dli in dt_variablesProv.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("txt_ref_Prov") as RadNumericTextBox;
                secRef = dli.ItemIndex + 1;

                //MessageBox.Show(references.Text);
                if (valRef.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ProveedoresRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "PROV");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valRef.Text));
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
                else
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ProveedoresRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                    ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 5, ParameterDirection.Input, "PROV");
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


                }


            }


        }


        catch (Exception)
        {

            throw;
        }

    }
    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManager.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    public void pintaDatos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        
        rTxtClave.Text = PagLoc_folio_Selection;
        rtxtNombre.Text = Convert.ToString(ds.Tables[3].Rows[0]["provNom"]);
        rCboCondicionesPago.SelectedValue = Convert.ToString(ds.Tables[3].Rows[0]["conPagCve"]);
        rCboTipo.SelectedValue = Convert.ToString(ds.Tables[3].Rows[0]["provTip"]);


        rCboPais.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
        FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        rCboEntidadFed.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
        FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue,   rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
        rCboProvincia.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
        rtxtRFC.Text = Convert.ToString(ds.Tables[3].Rows[0]["provRegFis"]);




        //FISCAL
        if (ds.Tables[0].Rows.Count > 0)
        {
            rTxtColonia.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCol"]);
            rTxtCalle.Text = Convert.ToString(ds.Tables[0].Rows[0]["domClle"]);
            rTxtNoInt.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNInt"]);
            rTxtNoExt.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNExt"]);
            rTxtCallesAledanas.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCllsA"]);
            rTxtCodigoPostal.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCP"]);
            rTxtReferencia.Text = Convert.ToString(ds.Tables[0].Rows[0]["domRef"]);
            rTxtTelefono1.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel"]);
            rTxtTelefono2.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel2"]);
            rTxtFax.Text = Convert.ToString(ds.Tables[0].Rows[0]["domFax"]);

            ////pendiente
            iddom.Text = Convert.ToString(ds.Tables[0].Rows[0]["domId"]);
            
           
        }
        //NOTFICACION
        if (ds.Tables[1].Rows.Count > 0)
        {
            rTxtColoniaNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domCol"]);
            rTxtCalleNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domClle"]);
            rTxtNoIntNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domNInt"]);
            rTxtNoExtNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domNExt"]);
            rTxtCallesAledanasNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domCllsA"]);
            rTxtCodigoPostalNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domCP"]);
            rTxtReferenciaNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domRef"]);
            rTxtTelefono1Noti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domTel"]);
            rTxtTelefono2Noti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domTel2"]);
            rTxtFaxNoti.Text = Convert.ToString(ds.Tables[1].Rows[0]["domFax"]);
            iddomNotif.Text = Convert.ToString(ds.Tables[1].Rows[0]["domId"]);

            if (Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]).Trim() != "")
            {
                rCboPaisNoti.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNoti.SelectedValue.ToString(), ref rCboEntidadFedNoti, true, false);
                rCboEntidadFedNoti.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["entFCve"]);
                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPaisNoti.SelectedValue,  rCboEntidadFedNoti.SelectedValue.ToString(), ref rCboProvinciaNoti, true, false);
                rCboProvinciaNoti.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["provCve"]);
            }
            else
            {
                rCboPaisNoti.ClearSelection();
                rCboEntidadFedNoti.ClearSelection();
                rCboProvinciaNoti.ClearSelection();

            }
        }

        //expedicion
        if (ds.Tables[2].Rows.Count > 0)
        {
            rTxtColoniaExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domCol"]);
            rTxtCalleExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domClle"]);
            rTxtNoIntExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domNInt"]);
            rTxtNoExtExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domNExt"]);
            rTxtCallesAledanasExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domCllsA"]);
            rTxtCodigoPostalExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domCP"]);
            rTxtReferenciaExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domRef"]);
            rTxtTelefono1Exp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domTel"]);
            rTxtTelefono2Exp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domTel2"]);
            rTxtFaxExp.Text = Convert.ToString(ds.Tables[2].Rows[0]["domFax"]);
            iddomExped.Text = Convert.ToString(ds.Tables[2].Rows[0]["domId"]);

            if (Convert.ToString(ds.Tables[2].Rows[0]["paisCve"]).Trim() != "")
            {
                rCboPaisExp.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["paisCve"]);
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExp.SelectedValue.ToString(), ref rCboEntidadFedExp, true, false);
                rCboEntidadFedExp.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["entFCve"]);
                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPaisExp.SelectedValue,   rCboEntidadFedExp.SelectedValue.ToString(), ref rCboProvinciaExp, true, false);
                rCboProvinciaExp.SelectedValue = Convert.ToString(ds.Tables[2].Rows[0]["provCve"]);
            }
            else
            {
                rCboPaisExp.ClearSelection();
                rCboEntidadFedExp.ClearSelection();
                rCboProvinciaExp.ClearSelection();
            }

        }

        
    }

    public void deshabilita()
    {
        rTxtClave.Enabled = false;
        rtxtNombre.Enabled = false;
        rCboCondicionesPago.Enabled = false;
        rCboTipo.Enabled = false;
        rtxtRFC.Enabled = false;

        rCboPais.Enabled = false;
        rCboEntidadFed.Enabled = false;
        rCboProvincia.Enabled = false;
        rTxtColonia.Enabled = false;
        rTxtCalle.Enabled = false;
        rTxtNoInt.Enabled = false;
        rTxtNoExt.Enabled = false;
        rTxtCallesAledanas.Enabled = false;
        rTxtCodigoPostal.Enabled = false;
        rTxtReferencia.Enabled = false;
        rTxtTelefono1.Enabled = false;
        rTxtTelefono2.Enabled = false;
        rTxtFax.Enabled = false;

        rCboPaisNoti.Enabled = false;
        rCboEntidadFedNoti.Enabled = false;
        rCboProvinciaNoti.Enabled = false;
        rTxtColoniaNoti.Enabled = false;
        rTxtCalleNoti.Enabled = false;
        rTxtNoIntNoti.Enabled = false;
        rTxtNoExtNoti.Enabled = false;
        rTxtCallesAledanasNoti.Enabled = false;
        rTxtCodigoPostalNoti.Enabled = false;
        rTxtReferenciaNoti.Enabled = false;
        rTxtTelefono1Noti.Enabled = false;
        rTxtTelefono2Noti.Enabled = false;
        rTxtFaxNoti.Enabled = false;
        
        rCboPaisExp.Enabled = false;
        rCboEntidadFedExp.Enabled = false;
        rCboProvinciaExp.Enabled = false;
        rTxtColoniaExp.Enabled = false;
        rTxtCalleExp.Enabled = false;
        rTxtNoIntExp.Enabled = false;
        rTxtNoExtExp.Enabled = false;
        rTxtCallesAledanasExp.Enabled = false;
        rTxtCodigoPostalExp.Enabled = false;
        rTxtReferenciaExp.Enabled = false;
        rTxtTelefono1Exp.Enabled = false;
        rTxtTelefono2Exp.Enabled = false;
        rTxtFaxExp.Enabled = false;

    }

    public void habilita()
    {
        rtxtNombre.Enabled = true;
        rCboCondicionesPago.Enabled = true;
        rCboTipo.Enabled = true;
        rCboPais.Enabled = true;
        rCboEntidadFed.Enabled = true;
        rCboProvincia.Enabled = true;
        rTxtColonia.Enabled = true;
        rTxtCalle.Enabled = true;
        rTxtNoInt.Enabled = true;
        rTxtNoExt.Enabled = true;
        rTxtCallesAledanas.Enabled = true;
        rTxtCodigoPostal.Enabled = true;
        rTxtReferencia.Enabled = true;
        rTxtTelefono1.Enabled = true;
        rTxtTelefono2.Enabled = true;
        rTxtFax.Enabled = true;
        rtxtRFC.Enabled = true;


        rCboPaisNoti.Enabled = true;
        rCboEntidadFedNoti.Enabled = true;
        rCboProvinciaNoti.Enabled = true;
        rTxtColoniaNoti.Enabled = true;
        rTxtCalleNoti.Enabled = true;
        rTxtNoIntNoti.Enabled = true;
        rTxtNoExtNoti.Enabled = true;
        rTxtCallesAledanasNoti.Enabled = true;
        rTxtCodigoPostalNoti.Enabled = true;
        rTxtReferenciaNoti.Enabled = true;
        rTxtTelefono1Noti.Enabled = true;
        rTxtTelefono2Noti.Enabled = true;
        rTxtFaxNoti.Enabled = true;

        rCboPaisExp.Enabled = true;
        rCboEntidadFedExp.Enabled = true;
        rCboProvinciaExp.Enabled = true;
        rTxtColoniaExp.Enabled = true;
        rTxtCalleExp.Enabled = true;
        rTxtNoIntExp.Enabled = true;
        rTxtNoExtExp.Enabled = true;
        rTxtCallesAledanasExp.Enabled = true;
        rTxtCodigoPostalExp.Enabled = true;
        rTxtReferenciaExp.Enabled = true;
        rTxtTelefono1Exp.Enabled = true;
        rTxtTelefono2Exp.Enabled = true;
        rTxtFaxExp.Enabled = true;


    }
    public void limpiar()
    {
        rCboCondicionesPago.Text = "";
        rCboTipo.Text = "";
        rCboPais.Text = "";
        rCboEntidadFed.Text = "";
        rCboProvincia.Text = "";
        rTxtColonia.Text = "";
        rTxtCalle.Text = "";
        rTxtNoInt.Text = "";
        rTxtNoExt.Text = "";
        rTxtCallesAledanas.Text = "";
        rTxtCodigoPostal.Text = "";
        rTxtReferencia.Text = "";
        rTxtTelefono1.Text = "";
        rTxtTelefono2.Text = "";
        rTxtFax.Text = "";
        rtxtRFC.Text = "";
        rCboPais.ClearSelection();
    }

    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        
        //===> CONTROLES POR ACCION
        //NUEVO
        
        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";


            habilita();
            habil_ValRef();
        }




        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
            //this.rCboAlmacen.Enabled = false;
            //this.rTxtLocalizacion.Enabled = false;
            //this.rTxtMaximo.Enabled = false;
            //this.rTxtMinimo.Enabled = false;
            //this.rTxtReorden.Enabled = false;
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

    public void habil_ValRef()
    {
        foreach (DataListItem dli in dt_referenciasProv.Items)
        {
            var references = dli.FindControl("txt_ref_Ref") as RadTextBox;
            references.Enabled = true;
        }
        foreach (DataListItem dli in dt_variablesProv.Items)
        {
            var references = dli.FindControl("txt_ref_Prov") as RadNumericTextBox;
            references.Enabled = true;
        }

        DataListOtrosDatos.Enabled = true;
    }

    public void Desa_ValRef()
    {
        foreach (DataListItem dli in dt_referenciasProv.Items)
        {
            var references = dli.FindControl("txt_ref_Ref") as RadTextBox;
            references.Enabled = false;
        }


        foreach (DataListItem dli in dt_variablesProv.Items)
        {
            var references = dli.FindControl("txt_ref_Prov") as RadNumericTextBox;
            references.Enabled = false;
        }

        DataListOtrosDatos.Enabled = false;
    }
    public void Limpiar_ValRef()
    {
        foreach (DataListItem dli in dt_referenciasProv.Items)
        {
            var references = dli.FindControl("txt_ref_Ref") as RadTextBox;
            references.Text = "";
        }

        foreach (DataListItem dli in dt_variablesProv.Items)
        {
            var references = dli.FindControl("txt_ref_Prov") as RadNumericTextBox;
            references.Text = "";
        }
    }
    public void CSSRefVar()
    {

        foreach (DataListItem dli in dt_variablesProv.Items)
        {
            var references = dli.FindControl("txt_ref_Ref") as RadTextBox;
            references.CssClass = "cssTxtEnabled";
        }

        foreach (DataListItem dli in dt_referenciasProv.Items)
        {
            var references = dli.FindControl("txt_ref_Prov") as RadNumericTextBox;
            references.CssClass = "cssTxtEnabled";
        }

    }

    public void cssena()
    {
        rtxtNombre.CssClass = "cssTxtEnabled";
        rTxtClave.CssClass = "cssTxtEnabled";
        
        rCboCondicionesPago.BorderColor = System.Drawing.Color.Transparent;
        rCboTipo.BorderColor = System.Drawing.Color.Transparent;
        rCboProvincia.BorderColor = System.Drawing.Color.Transparent;


    }

    public void LimpiaControl()
    {
        rTxtColonia.Text = "";
        rTxtCalle.Text = "";
        rTxtNoExt.Text = "";
        rTxtNoInt.Text = "";
        rTxtCallesAledanas.Text = "";
        rTxtReferencia.Text = "";
        rTxtCodigoPostal.Text = "";
        rTxtTelefono1.Text = "";
        rTxtTelefono2.Text = "";
        rTxtFax.Text = "";
        rCboPais.ClearSelection();
        rCboEntidadFed.ClearSelection();
        rCboProvincia.ClearSelection();

    }

    public void LimpiaControlNoti()
    {
        rTxtColoniaNoti.Text = "";
        rTxtCalleNoti.Text = "";
        rTxtNoExtNoti.Text = "";
        rTxtNoIntNoti.Text = "";
        rTxtCallesAledanasNoti.Text = "";
        rTxtReferenciaNoti.Text = "";
        rTxtCodigoPostalNoti.Text = "";
        rTxtTelefono1Noti.Text = "";
        rTxtTelefono2Noti.Text = "";
        rTxtFaxNoti.Text = "";
        rCboPaisNoti.ClearSelection();
        rCboEntidadFedNoti.ClearSelection();
        rCboProvinciaNoti.ClearSelection();
    }
    public void LimpiaControlExp()
    {
        rTxtColoniaExp.Text = "";
        rTxtCalleExp.Text = "";
        rTxtNoExtExp.Text = "";
        rTxtNoIntExp.Text = "";
        rTxtCallesAledanasExp.Text = "";
        rTxtReferenciaExp.Text = "";
        rTxtCodigoPostalExp.Text = "";
        rTxtTelefono1Exp.Text = "";
        rTxtTelefono2Exp.Text = "";
        rTxtFaxExp.Text = "";
        rCboPaisExp.ClearSelection();
        rCboEntidadFedExp.ClearSelection();
        rCboProvinciaExp.ClearSelection();

    }

    //nuevas funciones

    public void GuardarDom(int CveDom)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Domicilio";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 5);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

       ProcBD.AgregarParametrosProcedimiento("@provedorCve", DbType.String, 10, ParameterDirection.Input, rTxtClave.Text);
        if (CveDom == 1)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPais.SelectedValue);
            if (rCboEntidadFed.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFed.SelectedValue);
            }
            if (rCboProvincia.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvincia.SelectedValue);

            }
            if (rTxtColonia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColonia.Text.Trim());
            }
            if (rTxtCalle.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalle.Text.Trim());
            }
            if (rTxtCodigoPostal.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCodigoPostal.Text.Trim());
            }

            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rTxtNoInt.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNoInt.Text.Trim());
            }
            if (rTxtNoExt.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNoExt.Text.Trim());
            }
            if (rTxtCallesAledanas.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCallesAledanas.Text.Trim());
            }
            if (rTxtReferencia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtReferencia.Text.Trim());
            }
            if (rTxtTelefono1.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, rTxtTelefono1.Text.Trim());
            }
            if (rTxtTelefono2.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, rTxtTelefono2.Text.Trim());
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
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaisNoti.SelectedValue);
            if (rCboEntidadFedNoti.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFedNoti.SelectedValue);
            }
            if (rCboProvinciaNoti.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvinciaNoti.SelectedValue);
            }
            if (rTxtColoniaNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColoniaNoti.Text.Trim());
            }
            if (rTxtCalleNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalleNoti.Text.Trim());
            }
            if (rTxtCodigoPostalNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCodigoPostalNoti.Text.Trim());
            }

            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rTxtNoIntNoti.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNoIntNoti.Text.Trim());
            }
            if (rTxtNoExtNoti.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNoExtNoti.Text.Trim());
            }
            if (rTxtCallesAledanasNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCallesAledanasNoti.Text.Trim());
            }
            if (rTxtReferenciaNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtReferenciaNoti.Text.Trim());
            }
            if (rTxtTelefono1Noti.Text.Trim()!= "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, rTxtTelefono1Noti.Text.Trim());
            }
            if (rTxtTelefono2Noti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, rTxtTelefono2Noti.Text.Trim());
            }
            if (rTxtFaxNoti.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, rTxtFaxNoti.Text.Trim());
            }
            
            
            if (iddomNotif.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int64, 0, ParameterDirection.Input, iddomNotif.Text.Trim());
            }
        }
        else if (CveDom == 3)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPaisExp.SelectedValue);
            if (rCboEntidadFedExp.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFedExp.SelectedValue);
            }
            if (rCboProvinciaExp.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvinciaExp.SelectedValue);
            }
            if (rTxtColoniaExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColoniaExp.Text.Trim());
            }
            if (rTxtCalleExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalleExp.Text.Trim());
            }
            if (rTxtCodigoPostalExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCodigoPostalExp.Text.Trim());
            }
            
            
            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rTxtNoIntExp.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNoIntExp.Text.Trim());
            }
            if (rTxtNoExtExp.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNoExtExp.Text.Trim());
            }
            if (rTxtCallesAledanasExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCallesAledanasExp.Text.Trim());
            }
            if (rTxtReferenciaExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtReferenciaExp.Text.Trim());
            }
            if (rTxtTelefono1Exp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, rTxtTelefono1Exp.Text.Trim());
            }
            if (rTxtTelefono2Exp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, rTxtTelefono2Exp.Text.Trim());
            }
            if (rTxtFaxExp.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, rTxtFaxExp.Text.Trim());
            }
            
            if (iddomExped.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int64, 0, ParameterDirection.Input, iddomExped.Text.Trim());
            }
        }

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


    }


    public void EliminaDom()
    {

        if (rCboPais.SelectedValue == "")
        {
            if (iddom.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int32, 0, ParameterDirection.Input, iddom.Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }

        }
        if (rCboPaisNoti.SelectedValue == "")
        {
            if (iddomNotif.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@domId", DbType.Int32, 0, ParameterDirection.Input, iddomNotif.Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
        }
        if (rCboPaisExp.SelectedValue == "")
        {
            if (iddomExped.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
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
        if (rCboPais.SelectedValue != "")
        {
            rCboEntidadFed.Enabled = true;
        }
        else
        {
            rCboEntidadFed.Enabled = false;
        }
        if (rCboEntidadFed.SelectedValue != "")
        {
            rCboProvincia.Enabled = true;
        }
        else
        {
            rCboProvincia.Enabled = false;
        }
        ///
        if (rCboPaisNoti.SelectedValue != "")
        {
            rCboEntidadFedNoti.Enabled = true;
        }
        else
        {
            rCboEntidadFedNoti.Enabled = false;
        }
        if (rCboEntidadFedNoti.SelectedValue != "")
        {
            rCboProvinciaNoti.Enabled = true;
        }
        else
        {
            rCboProvinciaNoti.Enabled = false;
        }
        ///
        if (rCboPaisExp.SelectedValue != "")
        {
            rCboEntidadFedExp.Enabled = true;
        }
        else
        {
            rCboEntidadFedExp.Enabled = false;
        }
        if (rCboEntidadFedExp.SelectedValue != "")
        {
            rCboProvinciaExp.Enabled = true;
        }
        else
        {
            rCboProvinciaExp.Enabled = false;
        }


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

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ProveedorOtrosDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "PROV");
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
            ProcBD.NombreProcedimiento = "sp_ProveedorOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "PROV");
            ProcBD.AgregarParametrosProcedimiento("@otroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            Countkeyarray += 1;
        }
    }

    #endregion




}