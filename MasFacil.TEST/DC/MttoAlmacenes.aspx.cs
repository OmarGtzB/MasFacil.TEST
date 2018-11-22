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


public partial class DC_MttoAlmacenes : System.Web.UI.Page
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
    private static DataSet CopiaDs = new DataSet();
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


    //=====> EVENTOS CONTROLES

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

       
        //rTxtBusquedaUsr.Text = "";
        //DataSet ds = dsSeleccionRegistrosFiltro();
        //if (FnValAdoNet.bDSIsFill(ds))
        //{
        //    FnCtlsFillIn.RadGrid(ref this.rGdv_SeguridadUsuario, ds);
        //}
        //ControlesAccion();

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnCancelar.Enabled = true;
            rBtnGuardar.Enabled = true;
            loadGridUsr("");
            Datos_folio_Selection();
            rTxtBusquedaUsr.Text = "";
            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdv_SeguridadUsuario.AllowMultiRowSelection = true;

            rGdv_SeguridadUsuario.Enabled = true;
            rGdv_SeguridadUsuario.MasterTableView.Enabled = true;
        }
        else
        {
            InicioPagina();
        }
      
        //    Datos_folio_Selection();
        //    //loadGridUsr();

        //    rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

        //    for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
        //    {
        //        rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        //    }

        //    rGdv_SeguridadUsuario.AllowMultiRowSelection = true;

        //    rGdv_SeguridadUsuario.Enabled = true;
        //    rGdv_SeguridadUsuario.MasterTableView.Enabled = true;

        //}
        //else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        //{
        //    ControlesAccion();
        //    InicioPagina();
        //}

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


    //=====> EVENTOS OTROS
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {

        //DataSet ds = dsSeleccionRegistrosFiltro();
       // if (FnValAdoNet.bDSIsFill(ds))
       /// {
            //FnCtlsFillIn.RadGrid(ref this.rGdv_SeguridadUsuario, ds);
        //}
       loadGridUsr(rTxtBusquedaUsr.Text.Trim());

        ControlesAccion();


    }

    private DataSet dsSeleccionRegistrosFiltro()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        //ProcBD.AgregarParametrosProcedimiento("@idMenu", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rTxtAlmCve.Text.Trim().ToString());
        ProcBD.AgregarParametrosProcedimiento("@fltrUsr", DbType.String, 50, ParameterDirection.Input, rTxtBusquedaUsr.Text.Trim().ToString());

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
    }
    private void InicioPagina()
    {
        //loadGridUsr();
        if (PagLoc_folio_Selection == "")
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
            rGdv_SeguridadUsuario.Columns[3].Visible = true;
            loadGridUsr("");
           // loadGridUsr();
            ControlesAccion();
            rTxtBusquedaUsr.Text = "";
            rTxtAlmCve.Text = "";
            rTxtAlmDes.Text = "";
            rTxtAlmAbr.Text = "";

        }
        else
        {
            hdfBtnAccion.Value = "";
            ControlesAccion();

            Datos_folio_Selection();

            rGdv_SeguridadUsuario.Columns[3].Visible = true;
            loadGridUsr("");

            rGdv_SeguridadUsuario.Enabled = false;
            //rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = false;

            rGdv_SeguridadUsuario.MasterTableView.Enabled = false;

            //rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;


            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
            }

            rTxtBusquedaUsr.Text = "";

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

    //private void loadGridUsr()
    //{
    //    DataSet ds = dsSeleccionRegistrosFiltro();
    //    if (FnValAdoNet.bDSIsFill(ds))
    //    {
    //        FnCtlsFillIn.RadGrid(ref this.rGdv_SeguridadUsuario, ds);
    //    }
    //}
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////MOSTRAR DATOS
    /// </summary>
    /// <param name="fltrUsr"></param>
    private void loadGridUsr(string fltrUsr)
    {
        rGdv_SeguridadUsuario.Columns[3].Visible = true;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@fltrUsr", DbType.String, 50, ParameterDirection.Input, fltrUsr);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (fltrUsr == "")
        {
            CopiaDs = ds;
        }



        FnCtlsFillIn.RadGrid(ref rGdv_SeguridadUsuario, ds);

        //Habilitar seleccion

        rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
        {
            rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_SeguridadUsuario.AllowMultiRowSelection = true;


        foreach (GridDataItem dataItem in rGdv_SeguridadUsuario.Items)
        {

            if (dataItem != null)
            {

                if (dataItem["chkSeg"].Text == "True")
                {
                    dataItem.Selected = true;
                }
                else if (dataItem["chkSeg"].Text == "False")
                {
                    dataItem.Selected = false;
                }

            }
        }

        rGdv_SeguridadUsuario.Columns[3].Visible = false;
    }



    private void spSeguridadAlmacenModificar()
    {

        foreach (DataRow i in CopiaDs.Tables[0].Rows)
        {

            foreach (GridDataItem x in rGdv_SeguridadUsuario.Items)
            {
                //MessageBox.Show(x.Cells[3].Text.ToString());
                
                
                if (i[0].ToString() == x.Cells[3].Text.ToString()  )
                {
                    
                    //MessageBox.Show("las dos coinciden" + i[0].ToString() + a.ToString());

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rTxtAlmCve.Text);

                    //NOMBRE DEL USUARIO
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, x.Cells[3].Text.ToString());

                    //NIVEL DE SEGURIDAD EN EL ALMACEN
                    ProcBD.AgregarParametrosProcedimiento("@seAINivAut", DbType.Int64, 0, ParameterDirection.Input, 1);


                    //ProcBD.AgregarParametrosProcedimiento("@secMaUsu", DbType.Int64, 0, ParameterDirection.Input, 1);
                    
                  
                    if (x.Selected == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@isSelected", DbType.Int64, 0, ParameterDirection.Input, 1);
                    }

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                    
                }

            }

        }

    }




    private void spSeguridadAlmacen()
    {
    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() && rTxtBusquedaUsr.Text != "")
    {
            spSeguridadAlmacenModificar();
    }
    else { 

            if (rGdv_SeguridadUsuario.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                try
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rTxtAlmCve.Text);

                    //NOMBRE DEL USUARIO
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rGdv_SeguridadUsuario.Items[i].Cells[3].Text.Trim());

                    //NIVEL DE SEGURIDAD EN EL ALMACEN
                    ProcBD.AgregarParametrosProcedimiento("@seAINivAut", DbType.Int64, 0, ParameterDirection.Input, 1);
                    
                    ProcBD.AgregarParametrosProcedimiento("@secMaUsu", DbType.Int64, 0, ParameterDirection.Input, i + 1);
                    
                    if (rGdv_SeguridadUsuario.Items[i].Selected == true)
                    {
                        ProcBD.AgregarParametrosProcedimiento("@isSelected", DbType.Int64, 0, ParameterDirection.Input, 1);
                    }

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds.Tables.Count > 0)
                    {
                        
                    }
                    
                    }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }

            }
        }
      }
    }


    private void ControlesAccion()
    {
        //===> CONTROLES GENERAL

        rTxtAlmCve.CssClass = "cssTxtEnabled";
        rTxtAlmDes.CssClass = "cssTxtEnabled";
        rTxtAlmAbr.CssClass = "cssTxtEnabled";

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        
        //===> CONTROLES POR ACCION

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

            this.rTxtAlmCve.Enabled = true;
            this.rTxtAlmAbr.Enabled = true;
            this.rTxtAlmDes.Enabled = true;
            rBtnModificar.Enabled = false;
            
            rTxtBusquedaUsr.Enabled = true;
            rBtnBuscar.Enabled = true;

            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdv_SeguridadUsuario.AllowMultiRowSelection = true;

            rGdv_SeguridadUsuario.Enabled = true;
            rGdv_SeguridadUsuario.MasterTableView.Enabled = true;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            this.rTxtAlmCve.Enabled = false;
            this.rTxtAlmAbr.Enabled = true;
            this.rTxtAlmDes.Enabled = true;

            rTxtBusquedaUsr.Enabled = true;
            rBtnBuscar.Enabled = true;

            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdv_SeguridadUsuario.AllowMultiRowSelection = true;
            
            rGdv_SeguridadUsuario.Enabled = true;
            rGdv_SeguridadUsuario.MasterTableView.Enabled = true;

        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {

            this.rTxtAlmCve.Text = "";
            this.rTxtAlmAbr.Text = "";
            this.rTxtAlmDes.Text = "";

            this.rTxtAlmCve.Enabled = false;
            this.rTxtAlmDes.Enabled = false;
            this.rTxtAlmAbr.Enabled = false;
            rBtnModificar.Enabled = true;

            rTxtBusquedaUsr.Enabled = true;
            rBtnBuscar.Enabled = true;
            
        }
        else {
            Datos_folio_Selection();
         }

        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() 
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

    private void Datos_folio_Selection() {
        DataSet ds = new DataSet();
        ds = dsDatosAlmacen();
        if (FnValAdoNet.bDSRowsIsFill(ds)) {
            rTxtAlmCve.Text = ds.Tables[0].Rows[0]["almCve"].ToString();
            rTxtAlmDes.Text = ds.Tables[0].Rows[0]["almDes"].ToString();
            rTxtAlmAbr.Text = ds.Tables[0].Rows[0]["almAbr"].ToString();
        }
    }

    private void EjecutaAccion() {

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
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }



    }
    private void EjecutaSpAcciones() {

        try
        {
            DataSet ds = new DataSet();
    
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Almacenes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rTxtAlmCve.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@almDes", DbType.String, 50, ParameterDirection.Input, rTxtAlmDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@almAbr", DbType.String, 20, ParameterDirection.Input, rTxtAlmAbr.Text.Trim());

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1") {

                    spSeguridadAlmacen();

                    PagLoc_folio_Selection = rTxtAlmCve.Text.Trim();
                    Session["folio_Selection"] = rTxtAlmCve.Text.Trim();
                }

                ShowAlert(sEjecEstatus, sEjecMSG);
            }

         
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    private void ShowAlert(string Estatus, string  MSG) {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }


    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip) {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtAlmCve.Text.Trim() == "")
            {
                rTxtAlmCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }else {rTxtAlmCve.CssClass = "cssTxtEnabled";}


            if (rTxtAlmDes.Text.Trim() == "")
            {
                rTxtAlmDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAlmDes.CssClass = "cssTxtEnabled"; }

            if (rTxtAlmAbr.Text.Trim() == "")
            {
                rTxtAlmAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAlmAbr.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0) {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rTxtAlmCve.Text.Trim() == "")
            {
                rTxtAlmCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtAlmDes.Text.Trim() == "")
            {
                rTxtAlmDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAlmDes.CssClass = "cssTxtEnabled"; }

            if (rTxtAlmAbr.Text.Trim() == "")
            {
                rTxtAlmAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAlmAbr.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        return sResult;
    }

    private DataSet dsDatosAlmacen() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Almacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    #endregion

    protected void rGdv_SeguridadUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {


      //  if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{



          //  var a = rGdv_SeguridadUsuario.SelectedItems[0].OwnerTableView.DataKeyValues[rGdv_SeguridadUsuario.SelectedItems[0].ItemIndex]["maUsuCve"];

            //MessageBox.Show(a.ToString());





        }

    }




















