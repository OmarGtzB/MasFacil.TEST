using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.VisualStyles;

public partial class DC_MttoCompanias : System.Web.UI.Page
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
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        folio_Selection = Convert.ToString(Session["folio_Selection"]);
        listTipDatoCptoCve = Convert.ToString(Session["listTipDatoCptoCve"]);
    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

    }
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
       
        DesHabil(true);
        DesHabilNotif(true);
        DesHabilExped(true);
        DeshabilCombos();
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
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        traeDatoCia();
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

        DatosDom(Convert.ToInt32(RadTab));

        traeImagen();
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //rCboProvincia.Enabled = true;
            //rCboProvinciaExped.Enabled = true;
            //rCboProvinciaNotif.Enabled = true;

            rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;
            rCboProvincia.BorderColor = System.Drawing.Color.Transparent;

            rCboEntidadFedNotif.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaNotif.BorderColor = System.Drawing.Color.Transparent;

            rCboEntidadFedExped.BorderColor = System.Drawing.Color.Transparent;
            rCboProvinciaExped.BorderColor = System.Drawing.Color.Transparent;
            DeshabilCombos();


            //Combos Fiscal
            if (rCboPais.SelectedValue=="")
            {
                rCboPais.ClearSelection();
            }

            if (rCboEntidadFed.SelectedValue == "")
            {
                rCboEntidadFed.ClearSelection();
            }else
            {
                rCboEntidadFed.Enabled = true;
            }

            if (rCboProvincia.SelectedValue == "")
            {
                rCboProvincia.ClearSelection();
            }else
            {
                rCboProvincia.Enabled = true;
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



            // DeshabilCombos();
        }



    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        LimpiaControl();
        InicioPagina();
        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var references = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            references.Text = "";
        }
    }   


    //selecionar pais fiscal//
    protected void rCboPais_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = false;
        rCboEntidadFed.Enabled = true;

        if (rCboPais.SelectedValue !="")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        }else
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
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue,  rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
        }else
        {
            rCboProvincia.Enabled = false;
            rCboProvincia.ClearSelection();
        }
        
    }

    //selecionar pais NOTIFICACIONES//

    protected void rCboPaisNotif_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvinciaNotif.ClearSelection();
        rCboProvinciaNotif.Enabled = false;
        rCboEntidadFedNotif.Enabled = true;

        if (rCboPaisNotif.SelectedValue!="")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisNotif.SelectedValue.ToString(), ref rCboEntidadFedNotif, true, false);
        }
        else
        {
            rCboEntidadFedNotif.Enabled = false;
            rCboEntidadFedNotif.ClearSelection();
            rCboProvinciaNotif.Enabled = false;
            rCboProvinciaNotif.ClearSelection();
           
            //LimpiaControlExped();
            LimpiaControlNotif();

            //quitar bordes de validador
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
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPaisNotif.SelectedValue,  rCboEntidadFedNotif.SelectedValue.ToString(), ref rCboProvinciaNotif, true, false);
        }
        else
        {
            rCboProvinciaNotif.Enabled = false;
            rCboProvinciaNotif.ClearSelection();
        }
    }



    //selecionar pais EXPEDICIONES//
    protected void rCboPaisExped_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        rCboProvinciaExped.ClearSelection();
        rCboProvinciaExped.Enabled = false;
        rCboEntidadFedExped.Enabled = true;
        
        if (rCboPaisExped.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPaisExped.SelectedValue.ToString(), ref rCboEntidadFedExped, true, false);
        }else
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
        rCboProvinciaExped.ClearSelection();
        rCboProvinciaExped.Enabled = true;
        
        if (rCboEntidadFedExped.SelectedValue!="")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisExped.SelectedValue,   rCboEntidadFedExped.SelectedValue.ToString(), ref rCboProvinciaExped, true, false);
        }else
        {
            rCboProvinciaExped.ClearSelection();
            rCboProvinciaExped.Enabled = false;
        }
    }



    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
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

    #endregion

    #region METODOS
    private void InicioPagina()
    {
        traeDatoCia();
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPais, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisNotif, true, false);
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaisExped, true, false);
        rCboPais.BorderColor = System.Drawing.Color.Transparent;
        rCboPaisNotif.BorderColor = System.Drawing.Color.Transparent;
        rCboPaisExped.BorderColor = System.Drawing.Color.Transparent;

        rCboPais.EmptyMessage = "Seleccionar";
        rCboEntidadFed.EmptyMessage = "Seleccionar";
        rCboProvincia.EmptyMessage = "Seleccionar";


        rCboPaisNotif.EmptyMessage = "Seleccionar";
        rCboEntidadFedNotif.EmptyMessage = "Seleccionar";
        rCboProvinciaNotif.EmptyMessage = "Seleccionar";

        rCboPaisExped.EmptyMessage = "Seleccionar";
        rCboEntidadFedExped.EmptyMessage = "Seleccionar";
        rCboProvinciaExped.EmptyMessage = "Seleccionar";

        DatosDom(1);
        DatosDom(2);
        DatosDom(3);
        LlenaDLOtrosDatos();
        DesHabil(false);
        DesHabilNotif(false);
        DesHabilExped(false);
        hdfBtnAccion.Value = "";
        traeImagen();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        RadTabStrip1.SelectedIndex = 0;
        RadMultiPage1.SelectedIndex = 0;
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

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
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


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //FISCAL
            if (rCboPais.SelectedValue == "")
            {
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            
            //if (txt_colonia.Text != "" || txt_calle.Text != "" || rdNumericExterior.Text != "" || rdNumericInterior.Text != ""
            //  || txt_callesAle.Text != "" || rdNumericCodigoPostal.Text != "" || txt_referencias.Text != "" || txt_telefono1.Text != ""
            //  || txt_telefono2.Text != "" || txt_fax.Text != "")
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            //    ShowAlert("2", "Para Guardar La informacion es necesaro seleccionar un Pais");
            //    return sResult;
            //}


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

        return sResult;
    }

    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Companias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ciaDes", DbType.String, 50, ParameterDirection.Input, txt_nombre.Text);
            ProcBD.AgregarParametrosProcedimiento("@ciaAbr", DbType.String, 10, ParameterDirection.Input, txt_abreviatura.Text);
            ProcBD.AgregarParametrosProcedimiento("@ciaRSoc", DbType.String, 50, ParameterDirection.Input, txt_razonSocial.Text);
            ProcBD.AgregarParametrosProcedimiento("@ciaRFis", DbType.String, 50, ParameterDirection.Input, txt_regFiscal.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {

                    if (rCboPais.SelectedValue != "")
                    {
                        GuardarDom(1);
                    }
                    if (rCboPaisNotif.SelectedValue != "")
                    {
                    GuardarDom(2);
                    }
                    if (rCboPaisExped.SelectedValue != "")
                    {
                    GuardarDom(3);
                    }
                    EliminaDom();
                    GuardaOtrosDatos();
                    Guardaimagen();
                    LimpiaControl();
                    LimpiaControlExped();
                    LimpiaControlNotif();
                    InicioPagina();
                    

                    
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    #endregion

    #region FUNCIONES
    
    public void Guardaimagen()
    {
        if (arregloImagen.Value != "")
        {
            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

                ProcBD.NombreProcedimiento = "sp_CompaniaImagen";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@ciaImgLogo", DbType.Binary, 0, ParameterDirection.Input, arregloImagen.Value);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            arregloImagen.Value = "";
            //csValorCadenaImg.CadenaImagen = "";
        }

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
            ProcBD.NombreProcedimiento = "sp_CompaniaOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaOtroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "CIA");
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            Countkeyarray += 1;

        }


    }

    public void LlenaDLOtrosDatos()
    {
        DataListOtrosDatos.DataSource = FNDatos.dsOtrosDatos(Pag_sConexionLog, Pag_sCompania, "CIA");
        DataListOtrosDatos.RepeatColumns = 2;
        DataListOtrosDatos.DataBind();
    }
    
    public void traeDatoCia()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Companias";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        txt_clave.Text = Convert.ToString(ds.Tables[0].Rows[0]["ciaCve"]);
        txt_nombre.Text = Convert.ToString(ds.Tables[0].Rows[0]["ciaDes"]);
        txt_abreviatura.Text = Convert.ToString(ds.Tables[0].Rows[0]["ciaAbr"]);
        txt_razonSocial.Text = Convert.ToString(ds.Tables[0].Rows[0]["ciaRSoc"]);
        txt_regFiscal.Text = Convert.ToString(ds.Tables[0].Rows[0]["ciaRFis"]);
    }

    public void DatosDom(int CveDom)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Companias";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int32, 0, ParameterDirection.Input, CveDom);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (CveDom == 1)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                rdNumericCodigoPostal.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCP"]);
                txt_colonia.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCol"]);
                txt_calle.Text = Convert.ToString(ds.Tables[0].Rows[0]["domClle"]);
                txt_callesAle.Text = Convert.ToString(ds.Tables[0].Rows[0]["domCllsA"]);
                rdNumericInterior.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNInt"]);
                rdNumericExterior.Text = Convert.ToString(ds.Tables[0].Rows[0]["domNExt"]);
                txt_referencias.Text = Convert.ToString(ds.Tables[0].Rows[0]["domRef"]);
                txt_telefono1.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel"]);
                txt_telefono2.Text = Convert.ToString(ds.Tables[0].Rows[0]["domTel2"]);
                txt_fax.Text = Convert.ToString(ds.Tables[0].Rows[0]["domFax"]);
                
                if (ds.Tables[0].Rows[0]["domId"].ToString() != "")
                {
                    iddom.Text = Convert.ToString(ds.Tables[0].Rows[0]["domId"]);
                }

                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPais, true, false);
                if (Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]) != "")
                {
                    rCboPais.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["paisCve"]);
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()) {
                        rCboEntidadFed.Enabled = true;
                    }
                    FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
                }
                else
                {
                    rCboPais.ClearSelection();
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]) == "")
                {
                    rCboEntidadFed.ClearSelection();
                    rCboProvincia.ClearSelection();
                    if (true)
                    {

                    }
                    rCboEntidadFed.Enabled = true;
                    rCboProvincia.Enabled = false;
                }
                else
                {
                    rCboEntidadFed.ClearSelection();
                    rCboEntidadFed.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["entFCve"]);
                    FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue,   rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
                    rCboProvincia.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["provCve"]);
                }
 

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
                // rCboEntidadFedNotif.Enabled = true;
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
                // rCboEntidadFedExped.Enabled = true;

                FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPaisExped.SelectedValue,   rCboEntidadFedExped.SelectedValue.ToString(), ref rCboProvinciaExped, true, false);
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
        txt_nombre.Enabled = ena;
        txt_abreviatura.Enabled = ena;
        txt_razonSocial.Enabled = ena;
        txt_regFiscal.Enabled = ena;

        txt_colonia.Enabled = ena;
        txt_calle.Enabled = ena;
        rdNumericExterior.Enabled = ena;
        rdNumericInterior.Enabled = ena;
        txt_callesAle.Enabled = ena;
        rdNumericCodigoPostal.Enabled = ena;
        txt_referencias.Enabled = ena;
        txt_telefono1.Enabled = ena;
        txt_telefono2.Enabled = ena;
        txt_fax.Enabled = ena;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        rBtnGuardar.Enabled = ena;
        rBtnCancelar.Enabled = ena;
        rCboPais.Enabled = ena;
        rCboEntidadFed.Enabled = ena;
        rCboProvincia.Enabled = ena;
        rCboPaisNotif.Enabled = ena;
        rCboEntidadFedNotif.Enabled = ena;
        rCboProvinciaNotif.Enabled = ena;
        rCboPaisExped.Enabled = ena;
        rCboEntidadFedExped.Enabled = ena;
        rCboProvinciaExped.Enabled = ena;
        RadAsyncUpload1.Enabled = ena;




        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var references = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            references.Enabled = ena;
        }

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

    public void LimpiaControl()
    {
        txt_colonia.Text = "";
        txt_calle.Text = "";
        rdNumericExterior.Text = "";
        rdNumericInterior.Text = "";
        txt_callesAle.Text = "";
        txt_referencias.Text = "";
        rdNumericCodigoPostal.Text = "";
        txt_telefono1.Text = "";
        txt_telefono2.Text = "";
        txt_fax.Text = "";
        rCboPais.ClearSelection();
        rCboEntidadFed.ClearSelection();
        rCboProvincia.ClearSelection();

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

    }
    
    public void GuardarDom(int CveDom)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Companias";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 5);

        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (CveDom == 1)
        {
            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPais.SelectedValue);
            if (rCboEntidadFed.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFed.SelectedValue);
            }
            if (rCboProvincia.SelectedValue !="")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvincia.SelectedValue);

            }

            if (txt_colonia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, txt_colonia.Text.Trim());
            }
            if (txt_calle.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, txt_calle.Text.Trim());
            }
            if (rdNumericCodigoPostal.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rdNumericCodigoPostal.Text.Trim());
            }
            
            
            ProcBD.AgregarParametrosProcedimiento("@TipoDomId", DbType.Int64, 0, ParameterDirection.Input, CveDom);
            if (rdNumericInterior.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rdNumericInterior.Text.Trim());
            }
            if (rdNumericExterior.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rdNumericExterior.Text.Trim());
            }
            if (txt_callesAle.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, txt_callesAle.Text.Trim());
            }
            if (txt_referencias.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, txt_referencias.Text.Trim());
            }
            if (txt_telefono1.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, txt_telefono1.Text.Trim());
            }
            if (txt_telefono2.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, txt_telefono2.Text.Trim());
            }
            if (txt_fax.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, txt_fax.Text.Trim());
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
            if (rCboProvinciaNotif.SelectedValue !="")
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
    
    public void traeImagen()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();


        ProcBD.NombreProcedimiento = "sp_CompaniaImagen";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (ds.Tables[0].Rows.Count != 0)
        {

            if (ds.Tables[0].Rows[0]["ciaImgLogo"].ToString() == null)
            {
                //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
            }
            else
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["ciaImgLogo"];

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                RadBinaryImage1.ImageUrl = "data:image/png;base64," + base64String;
                arregloImagen.Value = base64String;
                Session["SessionImgLogo"] = base64String;
            }

        }
        else
        {
            //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
        }




    }
    
    public void GuardaimagenUser()
    {
        String sUsuCve = Convert.ToString(Session["user"]);

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_ArticuloImagenes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 34);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, "USUMGM01");
            ProcBD.AgregarParametrosProcedimiento("@ArtImg", DbType.Binary, 0, ParameterDirection.Input, Session["SessionImgLogo"]);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);



        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());

        }
        arregloImagen.Value = "";
        //csValorCadenaImg.CadenaImagen = "";
    }


    public void EliminaDom()
    {

        if (rCboPais.SelectedValue == "")
        {
            if (iddom.Text != "")
            {

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Companias";
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
                ProcBD.NombreProcedimiento = "sp_Companias";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
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
                ProcBD.NombreProcedimiento = "sp_Companias";
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
        if (rCboPais.SelectedValue !="")
        {
            rCboEntidadFed.Enabled = true;
        }else
        {
            rCboEntidadFed.Enabled = false;
        }
        if (rCboEntidadFed.SelectedValue != "")
        {
            rCboProvincia.Enabled = true;
        }else
        {
            rCboProvincia.Enabled = false;
        }
        ///
        if (rCboPaisNotif.SelectedValue != "")
        {
            rCboEntidadFedNotif.Enabled = true;
        }else
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
        }else
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

    #endregion




}