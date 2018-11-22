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

public partial class Menu_MGMApartadosEstandar : System.Web.UI.Page
{

    #region VARIABLES
        MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
        MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
        ws.Servicio oWS = new ws.Servicio();

        MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
        MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
        MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
        MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
        MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
        private string Pag_sConexionLog;
        private string Pag_sSessionLog;
        private string Pag_sidM;
    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page, true, 0))
        {
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void rGdvRegistros_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblSeleccion.Text = "";
        var dataItem = this.rGdvRegistros.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            // NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {

                Session["folio_Selection"] = dataItem.GetDataKeyValue("Cve").ToString();
                lblSeleccion.Text = dataItem.GetDataKeyValue("Cve").ToString() + "  " + dataItem["Descripcion"].Text;
                string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }


 
        }

    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnSeleccion_Click(object sender, ImageButtonClickEventArgs e)
    {
        rTxtBusqueda.Text = "";
        LlenaGridSeleccionRegistros();
        //RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "1", "Sys.Application.add_load(function(){{FNSeleccionRegistroApartadoEstandar();}}, 0);", true);

        string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {
        DataSet ds = dsSeleccionRegistrosFiltro();
        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref this.rGdvRegistros, ds);
        }
    }
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();

        string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
        {
            Pag_sCompania = Convert.ToString(Session["Compania"]);
            Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
            Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
            if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
            {
                Pag_sidM = Request.QueryString["idM"];
            }
        }
    private void InicioPagina()
    {
        TituloApartado();

        //addMenuLocalColumns();
        LlenaGridSeleccionRegistros();

        Session["folio_Selection"] = "";
        lblSeleccion.Text = "";

        hdfBtnAccion.Value = "";
        ControlesAccion();


        //string script = "<script language='javascript' type='text/javascript'>Sys.Application.add_load(FNSeleccionRegistroApartadoEstandar);</script>";
        //ClientScript.RegisterStartupScript(this.GetType(), "FNSeleccionRegistroApartadoEstandar", script);

        string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        rGdvRegistros.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvRegistros.AllowMultiRowSelection = true;

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

        Int64 TBwidth = 220;


        dtrBtnNuevo.Visible = rBtnNuevo.Visible;
        if (dtrBtnNuevo.Visible == false ) {
            TBwidth = TBwidth - 55;
        }
        
        dtrBtnModificar.Visible = rBtnModificar.Visible;
        if (dtrBtnModificar.Visible == false)
        {
            TBwidth = TBwidth - 55;
        }
 
        dtrBtnEliminar.Visible = rBtnEliminar.Visible;
        if (dtrBtnEliminar.Visible == false)
        {
            TBwidth = TBwidth - 55;
        } 

        tbBotones.Style.Value   = TBwidth.ToString() +" px;";
    }


    private void TituloApartado()
    {


        if (Pag_sidM == "9")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Almacenes", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "10")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Clientes", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "11")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Articulo", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "12")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Proveedores", "PnlMPFormTituloApartado");
        }


        if (Pag_sidM == "33")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Agentes", "PnlMPFormTituloApartado");
        }


        if (Pag_sidM == "39")
        {
            FNGrales.bTitleDesc(Page, "Mantenimiento Conceptos", "PnlMPFormTituloApartado");
        }

        if (Pag_sidM == "115")
        {
            FNGrales.bTitleDesc(Page, "Cuentas Contables", "PnlMPFormTituloApartado");
        }
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

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
                //this.rGdvRegistros.ClientSettings.EnablePostBackOnRowClick = false;
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                Session["folio_Selection"] = "";
                lblSeleccion.Text = "";

                //string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                addMenuLocalColumns();
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //rGdvRegistros.ClientSettings.EnablePostBackOnRowClick = false;
                EjecutaAccion();
            }


            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

        }


        if (Result == false)
        {
            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                this.rGdvRegistros.ClientSettings.Selecting.AllowRowSelect = true;
            }
        }




    }

    private void EjecutaAccionLimpiar()
    {
        rTxtBusqueda.Text = "";
        DataSet ds = dsSeleccionRegistrosFiltro();
        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref this.rGdvRegistros, ds);
        }
    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
        RadWinMenu();
    }

    private void RadWinMenu()
    {

        DataSet ds = new DataSet();
        ds = dsSelMenuLocal();

        if (FnValAdoNet.bDSIsFill(ds))
        {

            DataRow[] DrwEjecFun = ds.Tables[0].Select("maOperTipo = 2");
            foreach (DataRow Drw in DrwEjecFun)
            {

                string Fn = Convert.ToString(Drw["maOperEjecFn"]).ToString().Trim();
                string NavigateUrl = Convert.ToString(Drw["maOperEjecFnNavUrl"]).ToString().Trim();
                string Title = Convert.ToString(Drw["maMenuDes"]).ToString().Trim();

                RadWindow rwin = new RadWindow();
                rwin.ID = Fn;
                rwin.ReloadOnShow = true;
                rwin.NavigateUrl = NavigateUrl;
                rwin.Title = Title;
           
                RadWindowManagerPage.Windows.Add(rwin);
            }

        }


    }

    private void addMenuLocalColumns()
    {

        DataSet ds = new DataSet();
        ds = dsSelMenuLocal();

        DataListMenu.DataSource = ds;
        Int32 num;
        num = ds.Tables[0].Rows.Count;

        if (num == 1)
        {

            DataListMenu.RepeatColumns = 1;
            DataListMenu.DataBind();


        }
        else if (num == 2)
        {

            DataListMenu.RepeatColumns = 2;

            DataListMenu.DataBind();


        }
        else if (num == 3)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 4)
        {


            DataListMenu.RepeatColumns = 2;
            DataListMenu.DataBind();

        }
        else if (num == 5)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else if (num == 6)
        {


            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
        else {

            DataListMenu.RepeatColumns = 3;
            DataListMenu.DataBind();

        }
    }

    private void LlenaGridSeleccionRegistros()
    {
        DataSet ds = dsSeleccionRegistros();
        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref this.rGdvRegistros, ds);
        }  
    }

    private void EjecutaAccion()
    {


        string msgValidacion = validaEjecutaAccion();
        if (msgValidacion == "")
        {

            if ( hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                lblSeleccion.Text = "";
                var dataItem = this.rGdvRegistros.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {

             
                        Session["folio_Selection"] = dataItem.GetDataKeyValue("Cve").ToString();
                        lblSeleccion.Text = dataItem.GetDataKeyValue("Cve").ToString() + "  " + dataItem["Descripcion"].Text;
                        string script = "function f(){$find(\"" + FNSeleccionRegistroApartadoEstandar.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        addMenuLocalColumns();

                }
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
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

            foreach (GridDataItem i in rGdvRegistros.SelectedItems)
            {

                var dataItem = rGdvRegistros.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string sCve = dataItem["Cve"].Text;
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MGMApartadosEstandar";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@idMenu", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@folio", DbType.String, 50, ParameterDirection.Input, sCve);
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
                else {
                    sMsgAlert = MsgItemsElim;
                }


                

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else {
                    InicioPagina();
                }

                ShowAlert(sEstatusAlert, sMsgAlert);
            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    sMsgAlert = sMsgAlert + "Registros no eliminados" + " " + CantItemsElimFalse.ToString();
                }

                
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else {
                    InicioPagina();
                }
                ShowAlert(sEstatusAlert, sMsgAlert);
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

    #region FUNCIONES
    private DataSet dsSelMenuLocal()
    {

        DataSet ds = new DataSet();
        String sUsuCve = Convert.ToString(Session["user"]);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SelMAMenu";
        ProcBD.AgregarParametrosProcedimiento("@maMenuTipo", DbType.Int64, 0, ParameterDirection.Input, 3);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
        ProcBD.AgregarParametrosProcedimiento("@maMenuIdP", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@sessionlog", DbType.String, 30, ParameterDirection.Input, Pag_sSessionLog);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSIsFill(ds))
        {
            return ds;
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

        return ds;
    }
    private DataSet dsSeleccionRegistros()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MGMApartadosEstandar";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@idMenu", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private DataSet dsSeleccionRegistrosFiltro()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MGMApartadosEstandar";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@idMenu", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@folio", DbType.String, 50, ParameterDirection.Input, rTxtBusqueda.Text.Trim().ToString());

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private string validaEjecutaAccion()
    {

        string sResult = "";

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdvRegistros.SelectedItems.Count == 0)
            {
                sResult = "No se han seleccionado registros.";
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvRegistros.SelectedItems.Count == 0)
            {
                sResult = "No se han seleccionado registros.";
                return sResult;
            } 
        }

        return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvRegistros.SelectedItems.Count;
        string[] GvVAS;

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvRegistros, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvRegistros, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;

    }


    #endregion


    protected void RadButton1_Click(object sender, EventArgs e)
    {

        //if (Page.IsPostBack) System.Threading.Thread.Sleep(3000);
        RadButton btn = sender as RadButton;
        string x = btn.NavigateUrl;
    }

}