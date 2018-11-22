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

public partial class DC_MttoCtaContableAutUsuarios : System.Web.UI.Page
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
    private string PagLoc_Folio;
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
    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rCboUsuario.SelectedValue = dataItem["maUsuCve"].Text;
            if (dataItem["segCtaContAut"].Text == "1")
            {
                rBtnConsultar.Checked = true;
                rBtnRegistrar.Checked = false;
                rBtnGenerar.Checked = false;
            }
            else if (dataItem["segCtaContAut"].Text == "2")
            {
                rBtnConsultar.Checked = false;
                rBtnRegistrar.Checked = true;
                rBtnGenerar.Checked = false;
            }
            else if (dataItem["segCtaContAut"].Text == "3")
            {
                rBtnConsultar.Checked = false;
                rBtnGenerar.Checked = true;
                rBtnRegistrar.Checked = false;
            }
        }
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

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
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
        PagLoc_Folio = Convert.ToString(Session["folio_Selection"]);
    }
    private void InicioPagina()
    {
        LlenaCboUsuario();
        hdfBtnAccion.Value = "";
        LlenaGvd();
        ControlesAccion();
        LLenaDatosCta();
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
    private void LLenaDatosCta()
    {
        DataSet ds = new DataSet();
        ds = dsCuentasContables();

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblCveDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ctaContCveFotmat"]).Trim() + ' ' + Convert.ToString(ds.Tables[0].Rows[0]["ctaContNom"]).Trim();
        }
        else
        {
            rLblCveDes.Text = "";
        }
    }
    private void LlenaCboUsuario()
    {
        FnCtlsFillIn.RabComboBox_UsuariosSegDoc(Pag_sConexionLog, Pag_sCompania, ref rCboUsuario, true, false);
    }
    public void LlenaGvd()
    {
        if (PagLoc_Folio != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentasContablesSeguridad";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String , 20, ParameterDirection.Input, PagLoc_Folio);
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

        rCboUsuario.BorderColor = System.Drawing.Color.Transparent;

        this.rCboUsuario.Enabled = false;
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
            this.rCboUsuario.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = false;


            this.rCboUsuario.ClearSelection();
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


                this.rCboUsuario.Enabled = true;
                this.rBtnConsultar.Enabled = true;
                this.rBtnRegistrar.Enabled = true;
                this.rBtnGenerar.Enabled = true;


                this.rCboUsuario.ClearSelection();
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

                this.rCboUsuario.Enabled = false;
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

                this.rCboUsuario.Enabled = false;
                this.rBtnConsultar.Enabled = false;
                this.rBtnRegistrar.Enabled = false;
                this.rBtnGenerar.Enabled = true;

                this.rCboUsuario.ClearSelection();
                this.rBtnConsultar.Checked = true;
                this.rBtnRegistrar.Checked = false;
                this.rBtnGenerar.Checked = false;
            }
        }


        if (Result == false)
        {
            this.rCboUsuario.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = true;

            this.rCboUsuario.ClearSelection();
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;
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
            ProcBD.NombreProcedimiento = "sp_CuentasContablesSeguridad";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, PagLoc_Folio);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuario.SelectedValue);


            if (rBtnConsultar.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segCtaContAut", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else if (rBtnConsultar.Checked == false)
            {

            }
            if (rBtnRegistrar.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segCtaContAut", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            else if (rBtnRegistrar.Checked == false)
            {

            }
            if (rBtnGenerar.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segCtaContAut", DbType.Int64, 0, ParameterDirection.Input, 3);
            }
            else if (rBtnGenerar.Checked == false)
            {

            }


            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
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
            if (dataItem != null)
            {
                
                try
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_CuentasContablesSeguridad";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, PagLoc_Folio);
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
                InicioPagina();
            }

        }

    }

    private void EjecutaAccionLimpiar()
    {

        rGdvInformacion.MasterTableView.ClearSelectedItems();
        this.rCboUsuario.ClearSelection();

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.rBtnConsultar.Checked = true;
            this.rBtnRegistrar.Checked = false;
            this.rBtnGenerar.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboUsuario.BorderColor = System.Drawing.Color.Transparent;

            this.rCboUsuario.Enabled = false;
            this.rBtnConsultar.Enabled = false;
            this.rBtnRegistrar.Enabled = false;
            this.rBtnGenerar.Enabled = true;

           
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


    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    DataSet dsCuentasContables()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasContables";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, PagLoc_Folio);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rCboUsuario.SelectedIndex == -1)
            {
                rCboUsuario.BorderWidth = Unit.Pixel(1);
                rCboUsuario.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboUsuario.BorderColor = System.Drawing.Color.Transparent; }

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

            if (rCboUsuario.SelectedIndex == -1)
            {
                rCboUsuario.CssClass = "cssTxtInvalid";
                rCboUsuario.Focus();
                camposInc += 1;
            }
            else { rCboUsuario.CssClass = "cssTxtEnabled"; }

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

    #endregion
}