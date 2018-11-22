
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


public partial class DC_MttoDocAutorizacion : System.Web.UI.Page
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
    private string Pag_docCve;
   
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
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnCancelar.Enabled = true;
            rBtnGuardar.Enabled = true;
            loadGridUsr("");
            rTxtBusquedaUsr.Text = "";
            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
            {
                rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdv_SeguridadUsuario.AllowMultiRowSelection = true;

            rGdv_SeguridadUsuario.Enabled = true;
            rGdv_SeguridadUsuario.MasterTableView.Enabled = true;
            rCboUsuarios.ClearSelection();
            this.CheckReg.Checked = false;
            this.CheckCance.Checked = false;
            this.CheckProce.Checked = false;
            this.CheckVal.Checked = false;
            this.CheckAut.Checked = false;
        }
        else
        {
            InicioPagina();
        }


    }

    protected void rGdv_SeguridadUsuario_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var dataItem = rGdv_SeguridadUsuario.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                this.rCboDocumentos.Enabled = false;
                this.rCboUsuarios.Enabled = false;
                this.CheckReg.Enabled = true;
                this.CheckCance.Enabled = true;
                this.CheckProce.Enabled = true;
                this.CheckVal.Enabled = true;
                this.CheckAut.Enabled = true;

                var dataItem2 = this.rGdv_SeguridadUsuario.SelectedItems[0] as GridDataItem;
                if (dataItem2 != null)
                {

                    Boolean bAut = Convert.ToBoolean(dataItem2["segDocAut"].Text);
                    Boolean bProce = Convert.ToBoolean(dataItem2["segDocProc"].Text);

                    Boolean bReg1 = Convert.ToBoolean(dataItem2["segDocReg"].Text);
                    Boolean bCanc = Convert.ToBoolean(dataItem2["segDocCanc"].Text);
                    Boolean bVal = Convert.ToBoolean(dataItem2["segDocVal"].Text);



                    rCboDocumentos.SelectedValue = dataItem2["docCve"].Text;
                    rCboUsuarios.SelectedValue = dataItem2["maUsuCve"].Text;

                    CheckAut.Checked = bAut;
                    CheckProce.Checked = bProce;

                    CheckReg.Checked = bReg1;
                    CheckCance.Checked = bCanc;
                    CheckVal.Checked = bVal;



                }


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
        InicioPagina();
    }


    //=====> EVENTOS OTROS
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {

        DataSet ds = dsSeleccionRegistrosFiltro();
        if (FnValAdoNet.bDSIsFill(ds))
         {
            FnCtlsFillIn.RadGrid(ref this.rGdv_SeguridadUsuario, ds);
        }



    }

    private DataSet dsSeleccionRegistrosFiltro()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@fltrUsr", DbType.String, 50, ParameterDirection.Input, rTxtBusquedaUsr.Text.Trim().ToString());
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    private void LlenaGridDocAutorizacion()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_SeguridadUsuario, ds);
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        

        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
        }
    }
    private void InicioPagina()
    { 
        LlenaComboBox();
        hdfBtnAccion.Value = "";

        ControlesAccion();
            rTxtBusquedaUsr.Text = "";

        LlenaGridDocAutorizacion();
         if (Pag_docCve != "" && Pag_docCve != null)
        {
            rCboDocumentos.SelectedValue = Pag_docCve;
            rCboDocumentos.Enabled = false;
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
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }


    private void LlenaComboBox()
    {

        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumentos, true, false);
        FnCtlsFillIn.RabComboBox_UsuariosSegDoc(Pag_sConexionLog, Pag_sCompania, ref rCboUsuarios, true, false);
    }

    private void loadGridUsr(string fltrUsr)
    {


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@fltrUsr", DbType.String, 50, ParameterDirection.Input, fltrUsr);
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (fltrUsr == "")
        {
            DataSet CopiaDs = new DataSet();
            CopiaDs = (DataSet)Session["CopiaDs"];
            CopiaDs = ds;
        }



        FnCtlsFillIn.RadGrid(ref rGdv_SeguridadUsuario, ds);

        //    //Habilitar seleccion

        rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_SeguridadUsuario.Items.Count; i++)
        {
            rGdv_SeguridadUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_SeguridadUsuario.AllowMultiRowSelection = true;



        rGdv_SeguridadUsuario.Columns[3].Visible = false;
    }



    private void spSeguridadAlmacenModificar()
    {
        DataSet CopiaDs = new DataSet();
        CopiaDs = (DataSet)Session["CopiaDs"];

        foreach (DataRow i in CopiaDs.Tables[0].Rows)
        {

            foreach (GridDataItem x in rGdv_SeguridadUsuario.Items)
            {
                //MessageBox.Show(x.Cells[3].Text.ToString());


                if (i[0].ToString() == x.Cells[3].Text.ToString())
                {

                    //MessageBox.Show("las dos coinciden" + i[0].ToString() + a.ToString());

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_SeguridadAlmacenes";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rCboDocumentos.SelectedValue);

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
        else
        {

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
                        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, rCboDocumentos.SelectedValue);

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
        //this.rCboDocumentos.ClearSelection();
        this.rCboUsuarios.ClearSelection();

        rGdv_SeguridadUsuario.MasterTableView.ClearSelectedItems();

        this.CheckReg.Checked = false;
        this.CheckCance.Checked = false;
        this.CheckProce.Checked = false;
        this.CheckVal.Checked = false;
        this.CheckAut.Checked = false;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //===> CONTROLES POR ACCION

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
            rTxtBusquedaUsr.Text = "";
            rTxtBusquedaUsr.Enabled = true;
            rBtnBuscar.Enabled = true;

            rCboDocumentos.BorderColor = System.Drawing.Color.Transparent;
            rCboUsuarios.BorderColor = System.Drawing.Color.Transparent;

            LlenaGridDocAutorizacion();

            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = false;
            this.rGdv_SeguridadUsuario.AllowMultiRowSelection = false;//cambio

            rGdv_SeguridadUsuario.Enabled = false;
            this.rCboDocumentos.Enabled = false;
            this.rCboUsuarios.Enabled = true;
            this.CheckReg.Enabled = true;
            this.CheckCance.Enabled = true;
            this.CheckProce.Enabled = true;
            this.CheckVal.Enabled = true;
            this.CheckAut.Enabled = true;

            this.CheckReg.Checked = false;
            this.CheckCance.Checked = false;
            this.CheckProce.Checked = false;
            this.CheckVal.Checked = false;
            this.CheckAut.Checked = false;

        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            rTxtBusquedaUsr.Text = "";
            rTxtBusquedaUsr.Enabled = true;

            rCboDocumentos.BorderColor = System.Drawing.Color.Transparent;
            rCboUsuarios.BorderColor = System.Drawing.Color.Transparent;

            LlenaGridDocAutorizacion();

            this.rCboDocumentos.Enabled = false;
            this.rCboUsuarios.Enabled = false;
            this.CheckReg.Enabled = false;
            this.CheckCance.Enabled = false;
            this.CheckProce.Enabled = false;
            this.CheckVal.Enabled = false;
            this.CheckAut.Enabled = false;

            this.CheckReg.Checked = false;
            this.CheckCance.Checked = false;
            this.CheckProce.Checked = false;
            this.CheckVal.Checked = false;
            this.CheckAut.Checked = false;


            rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;
            this.rGdv_SeguridadUsuario.AllowMultiRowSelection = false;//cambio


        }
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
            this.rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;
            this.rGdv_SeguridadUsuario.AllowMultiRowSelection = true;

            LlenaGridDocAutorizacion();

            rTxtBusquedaUsr.Text = "";

            rCboDocumentos.BorderColor = System.Drawing.Color.Transparent;
            rCboUsuarios.BorderColor = System.Drawing.Color.Transparent;

            this.rCboDocumentos.Enabled = false;
            this.rCboUsuarios.Enabled = false;
            this.CheckReg.Enabled = false;
            this.CheckCance.Enabled = false;
            this.CheckProce.Enabled = false;
            this.CheckVal.Enabled = false;
            this.CheckAut.Enabled = false;

            this.CheckReg.Checked = false;
            this.CheckCance.Checked = false;
            this.CheckProce.Checked = false;
            this.CheckVal.Checked = false;
            this.CheckAut.Checked = false;
        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            this.CheckProce.Enabled = false;
            this.rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = false;
            this.rCboDocumentos.Enabled = false;
            this.rCboUsuarios.Enabled = false;
            this.CheckReg.Enabled = false;
            this.CheckCance.Enabled = false;
            this.CheckProce.Enabled = false;
            this.CheckVal.Enabled = false;
            this.CheckAut.Enabled = false;

            this.CheckReg.Checked = false;
            this.CheckCance.Checked = false;
            this.CheckProce.Checked = false;
            this.CheckVal.Checked = false;
            this.CheckAut.Checked = false;


            rTxtBusquedaUsr.Enabled = true;
            rBtnBuscar.Enabled = true;

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
            ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumentos.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuarios.SelectedValue);
           if(CheckReg.Checked==true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocReg", DbType.Byte, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocReg", DbType.Byte, 1, ParameterDirection.Input, 0);
            }
            if (CheckAut.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocAut", DbType.Byte, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocAut", DbType.Byte,1, ParameterDirection.Input, 0);
            }
            if (CheckProce.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocProc", DbType.Byte, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocProc", DbType.Byte, 1, ParameterDirection.Input, 0);
            }
            if (CheckCance.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocCanc", DbType.Byte, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocCanc", DbType.Byte, 1, ParameterDirection.Input, 0);
            }
            if (CheckVal.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocVal", DbType.Byte, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@segDocVal", DbType.Byte, 1, ParameterDirection.Input, 0);
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
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
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




            foreach (GridDataItem i in rGdv_SeguridadUsuario.SelectedItems)
            {

                var dataItem = rGdv_SeguridadUsuario.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    //string sAlmCve = dataItem.GetDataKeyValue("maUsuCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, dataItem["maUsuCve"].Text);

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
                    //LlenaGrid();
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
                    //LlenaGrid();
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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (this.rCboDocumentos.SelectedIndex == -1)
            {

                rCboDocumentos.BorderWidth = Unit.Pixel(1);
                rCboDocumentos.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDocumentos.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
            }

            if (this.rCboUsuarios.SelectedIndex == -1)
            {

                rCboUsuarios.BorderWidth = Unit.Pixel(1);
                rCboUsuarios.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboUsuarios.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
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

            if (rGdv_SeguridadUsuario.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (this.rCboDocumentos.SelectedIndex == -1)
            {

                rCboDocumentos.BorderWidth = Unit.Pixel(1);
                rCboDocumentos.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDocumentos.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
            }
            if (this.rCboUsuarios.SelectedIndex == -1)
            {

                rCboUsuarios.BorderWidth = Unit.Pixel(1);
                rCboUsuarios.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboUsuarios.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
            }
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        return sResult;
    }

    private DataSet dsDatosAlmacen()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Almacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    private void OpcNuevo()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_SeguridadUsuario.AllowMultiRowSelection = false;
        rGdv_SeguridadUsuario.MasterTableView.ClearSelectedItems();
        rGdv_SeguridadUsuario.Enabled = false;
        rGdv_SeguridadUsuario.MasterTableView.Enabled = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rGdv_SeguridadUsuario.ClientSettings.Selecting.AllowRowSelect = false;

    }
    #endregion









}

























