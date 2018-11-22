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


public partial class SG_SGUsuario : System.Web.UI.Page
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

    wsRpt.Service oWSRpt = new wsRpt.Service();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
   // DataTable customerOrders = new DataTable();

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
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        LlenaGrid();
        LlenaComboPerfiles();
        ControlesAccion();

        rTxtClave.Text = "";
        rTxtNombre.Text = "";
        rTxtContrasenia1.Text = "";
        rTxtContrasenia2.Text = "";
        CheckStatus.Checked = false;
        rCboPerfil.ClearCheckedItems();
        rCboPerfil.ClearSelection();
        arregloImagen.Value = "";
        imgPerfil.ImageUrl = null;

        rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvUsuario.AllowMultiRowSelection = true;
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
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvUsuario, ds);

        
    }

    private void LlenaComboPerfiles()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref this.rCboPerfil, ds, "maPerfCve", "maPerfDes", true, true);
        ((Literal)rCboPerfil.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboPerfil.Items.Count);
        rCboPerfil.SelectedIndex = -1;

    }

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

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    #endregion


    #region FUNCIONES

    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtClave.CssClass = "cssTxtEnabled";
        rTxtNombre.CssClass = "cssTxtEnabled";
        rTxtContrasenia1.CssClass = "cssTxtEnabled";
        rTxtContrasenia2.CssClass = "cssTxtEnabled";

        this.rTxtClave.Enabled = false;
        this.rTxtNombre.Enabled = false;
        this.rTxtContrasenia1.Enabled = false;
        this.rTxtContrasenia2.Enabled = false;
        RadAsyncUpload1.Enabled = false;

        CheckStatus.Enabled = false;
        rCboPerfil.Enabled = false;

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



    private bool ValidaPsw() {
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()) {

            if (rTxtContrasenia1.Text == rTxtContrasenia2.Text)
            {
                rTxtContrasenia1.CssClass = "cssTxtEnabled";
                rTxtContrasenia2.CssClass = "cssTxtEnabled";
                return true;
            }
            else
            {
                rTxtContrasenia1.CssClass = "cssTxtInvalid";
                rTxtContrasenia2.CssClass = "cssTxtInvalid";
                ShowAlert("2", "Las contraseñas no coinciden");
                return false;
            }

        }
        return true;
        
    }


    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvUsuario.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvUsuario, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvUsuario, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rTxtClave.Text = "";
            this.rTxtNombre.Text = "";
            this.rTxtContrasenia1.Text = "";
            this.rTxtContrasenia2.Text = "";
            CheckStatus.Checked = false;
            rCboPerfil.ClearCheckedItems();
            rCboPerfil.ClearSelection();

            arregloImagen.Value = "";
            imgPerfil.ImageUrl = null;
            
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvUsuario.AllowMultiRowSelection = true;
            rGdvUsuario.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtClave.CssClass = "cssTxtEnabled";
            rTxtNombre.CssClass = "cssTxtEnabled";
            rTxtContrasenia1.CssClass = "cssTxtEnabled";
            rTxtContrasenia2.CssClass = "cssTxtEnabled";

            rTxtClave.Enabled = false;
            rTxtNombre.Enabled = false;
            rTxtContrasenia1.Enabled = false;
            rTxtContrasenia2.Enabled = false;
            CheckStatus.Enabled = false;
            rCboPerfil.Enabled = false;
            RadAsyncUpload1.Enabled = false;

            rTxtClave.Text = "";
            rTxtNombre.Text = "";
            rTxtContrasenia1.Text = "";
            rTxtContrasenia2.Text = "";
            CheckStatus.Checked = false;
            rCboPerfil.ClearCheckedItems();
            rCboPerfil.ClearSelection();
            arregloImagen.Value = "";
            imgPerfil.ImageUrl = null;


            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

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
                this.rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvUsuario.MasterTableView.ClearSelectedItems();

                this.rTxtClave.Enabled = true;
                this.rTxtNombre.Enabled = true;
                this.rTxtContrasenia1.Enabled = true;
                this.rTxtContrasenia2.Enabled = true;
                CheckStatus.Enabled = true;
                rCboPerfil.Enabled = true;
                RadAsyncUpload1.Enabled = true;

                this.rTxtClave.Text = "";
                this.rTxtNombre.Text = "";
                this.rTxtContrasenia1.Text = "";
                rTxtContrasenia2.Text = "";
                CheckStatus.Checked=false;

                rCboPerfil.ClearCheckedItems();

                arregloImagen.Value = "";
                imgPerfil.ImageUrl = null;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvUsuario.AllowMultiRowSelection = false;
                this.rTxtClave.Enabled = false;
                this.rTxtNombre.Enabled = true;
                this.rTxtContrasenia1.Enabled = true;
                this.rTxtContrasenia2.Enabled = true;
                CheckStatus.Enabled = true;
                rCboPerfil.Enabled = true;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                RadAsyncUpload1.Enabled = true;
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvUsuario.AllowMultiRowSelection = true;
                rGdvUsuario.MasterTableView.ClearSelectedItems();

                this.rTxtClave.Enabled = false;
                this.rTxtNombre.Enabled = false;
                this.rTxtContrasenia1.Enabled = false;
                rTxtContrasenia2.Enabled = false;
                CheckStatus.Enabled = false;
                rCboPerfil.Enabled = false;
                RadAsyncUpload1.Enabled = true;

                this.rTxtClave.Text = "";
                this.rTxtNombre.Text = "";
                this.rTxtContrasenia1.Text = "";
                this.rTxtContrasenia2.Text = "";
                CheckStatus.Checked = false;
                rCboPerfil.ClearSelection();
                rCboPerfil.ClearCheckedItems();
            }
        }


        if (Result == false)
        {
            this.rTxtClave.Enabled = false;
            this.rTxtNombre.Enabled = false;
            this.rTxtContrasenia1.Enabled = false;
            this.rTxtContrasenia2.Enabled = false;
            this.CheckStatus.Checked = false;
            this.rCboPerfil.Enabled = false;
            RadAsyncUpload1.Enabled = false;
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
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                hdfBtnAccion.Value = "";
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
        string PSW;
        if (ValidaPsw() == true)
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                hdfBtnAccion.Value = "4";
            }


            if (rTxtContrasenia1.Text != "" || rTxtContrasenia2.Text != "") {

                if (rTxtContrasenia1.Text == rTxtContrasenia2.Text)
                {
                    hdfBtnPSW.Value = rTxtContrasenia2.Text;
                    MGM.Utilidades.Seguridad.Encriptacion SG = new MGM.Utilidades.Seguridad.Encriptacion();
                    PSW = SG.Encriptar(hdfBtnPSW.Value);
                    hdfBtnPSW.Value = PSW;
                }
            }
              

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAUsuarios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
            ProcBD.AgregarParametrosProcedimiento("@maUsuNom", DbType.String, 200, ParameterDirection.Input, rTxtNombre.Text);
            ProcBD.AgregarParametrosProcedimiento("@maUsuPwr", DbType.String, 100, ParameterDirection.Input, hdfBtnPSW.Value.ToString());
            ProcBD.AgregarParametrosProcedimiento("@maUsuTipo", DbType.Int32, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@maUsuSts", DbType.Int32, 0, ParameterDirection.Input, status());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                Guardaimagen();
                RelacionUsuario();
                hdfBtnAccion.Value = "";
                InicioPagina();
            }
        }
            else
        {


        }

    }

    private Int32 status() {

        if (CheckStatus.Checked == true)
        {
            return 1;
        }else {
            return 0;
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


            foreach (GridDataItem i in rGdvUsuario.SelectedItems)
            {

                var dataItem = rGdvUsuario.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string maUsuCve = dataItem.GetDataKeyValue("maUsuCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MAUsuarios";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
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
                    //InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    //InicioPagina();
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
                    //InicioPagina();
                }
                else
                {
                    //InicioPagina();
                }
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

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtContrasenia1.Text.Trim() == "")
            {
                rTxtContrasenia1.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtContrasenia2.Text.Trim() == "")
            {
                rTxtContrasenia2.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rCboPerfil.CheckedItems.Count == 0)
                {
                rCboPerfil.BorderWidth = Unit.Pixel(1);
                rCboPerfil.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboPerfil.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvUsuario.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
          
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }




            //if (rTxtContrasenia1.Text.Trim() == "")
            //{
            //    rTxtContrasenia1.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}

            //if (rTxtContrasenia2.Text.Trim() == "")
            //{
            //    rTxtContrasenia2.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}

            if (rCboPerfil.CheckedItems.Count == 0)
            {
                rCboPerfil.BorderWidth = Unit.Pixel(1);
                rCboPerfil.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboPerfil.BorderColor = System.Drawing.Color.Transparent; }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvUsuario.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
    }
    #endregion


    #region METODOS



    #endregion


    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png" || ext == ".JPG")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                imgPerfil.DataValue = data;
                string valor = Convert.ToBase64String(data);

                arregloImagen.Value = valor;
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                //ShowAlert(sMSGTip, sResult);
                arregloImagen.Value = "";
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("2", ex.ToString());
        }
    }

    protected void rGdvUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvUsuario.SelectedItems[0] as GridDataItem;
        if (dataItem != null) {
            string skey = dataItem.GetDataKeyValue("maUsuCve").ToString();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAUsuarios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, skey);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (FnValAdoNet.bDSIsFill(ds))
            {
                if (ds.Tables[0].Rows[0]["maUsuFoto"].ToString() != "")
                {
                    byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maUsuFoto"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgPerfil.ImageUrl = "data:image/png;base64," + base64String;
                }else {
                    imgPerfil.ImageUrl = null;
                }
                rTxtClave.Text = ds.Tables[0].Rows[0]["maUsuCve"].ToString();
                rTxtNombre.Text = ds.Tables[0].Rows[0]["maUsuNom"].ToString();

                if (ds.Tables[0].Rows[0]["maUsuSts"].ToString() == "1")
                {
                    CheckStatus.Checked = true;
                }else {
                    CheckStatus.Checked = false;
                }
                rCboPerfil.ClearCheckedItems();
                ComboBoxCheck(skey);

                hdfBtnPSW.Value=ds.Tables[0].Rows[0]["maUsuPwr"].ToString();
                
            }
        }
    }


    private void ComboBoxCheck(string CveUsu) {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input,50);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, CveUsu);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            foreach (RadComboBoxItem item in rCboPerfil.Items)
            {
                if (row["maPerfCve"].ToString() == item.Value)
                {
                    item.Checked = true;
                }
            }
        }

    }


    public void Guardaimagen()
    {
        if (arregloImagen.Value != "")
        {
            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAUsuarioImagen";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
                ProcBD.AgregarParametrosProcedimiento("@ImgUsu", DbType.Binary, 0, ParameterDirection.Input, arregloImagen.Value);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            arregloImagen.Value = "";
        }

    }

    
            private void RelacionUsuario() {

                if (hdfBtnAccion.Value == "4")
                {
                    hdfBtnAccion.Value = "2";
                }

             foreach (var item in rCboPerfil.CheckedItems)
                    {
                        if (item.Checked== true)
                        {
                            DataSet ds = new DataSet();
                            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                            ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
                            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rTxtClave.Text);
                            ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, item.Value.ToString());
                            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                        }
                    } 
                }





    

}