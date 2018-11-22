using System.Windows.Forms;
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
public partial class SG_SGPerfilvsUsuario : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;


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

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        if (rbtnPerfil.Checked == true)
        {
            //GUARDA DEL GRID USUARIOS
            EjecutaAccionPerfil();
        }
        if (rbtnUsuario.Checked == true)
        {
            EjecutaAccionUsuario();
        }
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        if (rbtnPerfil.Checked == true)
        {
            ControlesAccion();
        }
        if (rbtnUsuario.Checked == true)
        {
            ControlesAccionPerfil();
        }
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {

    }
    protected void rbtnPerfil_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPerfil.Checked == true)
        {
            rGdvPerfil.Visible = false;
            rCboUsuario.Visible = false;

            rGdvUsuario.Visible = true;
            rCboPerfil.Visible = true;
            LlenaGrid();
        }
    }
    protected void rbtnUsuario_CheckedChanged(object sender, EventArgs e)
    {
    
        if (rbtnUsuario.Checked == true)
        {

            rGdvPerfil.Visible = true;
            rCboUsuario.Visible = true;
            CargaCombosUsuarios();
            //LlenaGridPerfiles();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuario.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvPerfil, ds);



            for (int i = 0; i < rGdvPerfil.Items.Count; i++)
            {
                rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
            }

            foreach (GridDataItem dataItem in rGdvPerfil.Items)
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



            rGdvUsuario.Visible = false;
            rCboPerfil.Visible = false;
            if (hdfBtnAccion.Value == "2")
            {
                rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;

                for (int i = 0; i < rGdvPerfil.Items.Count; i++)
                {
                    rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }

                rGdvPerfil.AllowMultiRowSelection = true;

                rGdvPerfil.Enabled = true;
                rGdvPerfil.MasterTableView.Enabled = true;
            }
        }
    }
    protected void rCboPerfil_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGrid();
        if (hdfBtnAccion.Value == "2")
        {
            rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdvUsuario.Items.Count; i++)
            {
                rGdvUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdvUsuario.AllowMultiRowSelection = true;
            rGdvUsuario.Enabled = true;
            rGdvUsuario.MasterTableView.Enabled = true;
        }
    }
    protected void rCboUsuario_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuario.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPerfil, ds);

        foreach (GridDataItem dataItem in rGdvPerfil.Items)
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


    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        ControlesAccion();
        LlenaComboPerfiles();
        LlenaGrid();
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

    private void ControlesAccion()
    {
       

        //===> CONTROLES GENERAL
        //rGdvPerfil.MasterTableView.ClearSelectedItems();
        //rGdvUsuario.MasterTableView.ClearSelectedItems();


        rGdvPerfil.Enabled = false;
        rGdvPerfil.MasterTableView.Enabled = false;
        rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = false;

        rGdvUsuario.Enabled = false;
        rGdvUsuario.MasterTableView.Enabled = false;
        rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = false;

        //rCboPerfil.ClearSelection();
        //rCboUsuario.ClearSelection();

        rCboPerfil.BorderColor = System.Drawing.Color.Transparent;
        rCboUsuario.BorderColor = System.Drawing.Color.Transparent;

        rGdvUsuario.ClientSettings.Selecting.UseClientSelectColumnOnly = true;
        rGdvPerfil.ClientSettings.Selecting.UseClientSelectColumnOnly = true;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        this.rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
        this.rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
        //===> CONTROLES POR ACCION
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
            //this.rGdvIconterms.ClientSettings.Selecting.AllowRowSelect = false;
            //this.rtxtClave.Enabled = true;
            //this.rTxtDescripcion.Enabled = true;
            //this.rTxtAbre.Enabled = true;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

            rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
            for (int i = 0; i < rGdvPerfil.Items.Count; i++)
            {
                rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            rGdvPerfil.AllowMultiRowSelection = true;
            rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;

            for (int i = 0; i < rGdvPerfil.Items.Count; i++)
            {
                rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }
            rGdvPerfil.AllowMultiRowSelection = true;
            rGdvPerfil.Enabled = true;
            rGdvPerfil.MasterTableView.Enabled = true;




            rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
            for (int i = 0; i < rGdvUsuario.Items.Count; i++)
            {
                rGdvUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }
            rGdvUsuario.AllowMultiRowSelection = true;
            rGdvUsuario.Enabled = true;
            rGdvUsuario.MasterTableView.Enabled = true;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
            //this.rGdvIconterms.ClientSettings.Selecting.AllowRowSelect = true;
            //this.rGdvIconterms.AllowMultiRowSelection = true;

            //this.rtxtClave.Enabled = false;
            //this.rTxtDescripcion.Enabled = false;
            //this.rTxtAbre.Enabled = false;
        }


        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;

            rCboPerfil.Enabled = true;
            rCboUsuario.Enabled = true;
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
    private void ControlesAccionPerfil()
    {
        //===> CONTROLES GENERAL
        //rGdvPerfil.MasterTableView.ClearSelectedItems();
        //rGdvUsuario.MasterTableView.ClearSelectedItems();

        rGdvPerfil.Enabled = false;
        rGdvPerfil.MasterTableView.Enabled = false;
        rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = false;
        rCboPerfil.BorderColor = System.Drawing.Color.Transparent;

      
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        this.rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
        this.rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
        //===> CONTROLES POR ACCION
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
            //this.rGdvIconterms.ClientSettings.Selecting.AllowRowSelect = false;
            //this.rtxtClave.Enabled = true;
            //this.rTxtDescripcion.Enabled = true;
            //this.rTxtAbre.Enabled = true;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

            rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
            for (int i = 0; i < rGdvPerfil.Items.Count; i++)
            {
                rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }
            rGdvPerfil.AllowMultiRowSelection = true;
            rGdvPerfil.Enabled = true;
            rGdvPerfil.MasterTableView.Enabled = true;
            


            rGdvUsuario.ClientSettings.Selecting.AllowRowSelect = true;
            for (int i = 0; i < rGdvUsuario.Items.Count; i++)
            {
                rGdvUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }
            rGdvUsuario.AllowMultiRowSelection = true;
            rGdvUsuario.Enabled = true;
            rGdvUsuario.MasterTableView.Enabled = true;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
            //this.rGdvIconterms.ClientSettings.Selecting.AllowRowSelect = true;
            //this.rGdvIconterms.AllowMultiRowSelection = true;

            //this.rtxtClave.Enabled = false;
            //this.rTxtDescripcion.Enabled = false;
            //this.rTxtAbre.Enabled = false;
        }


        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
               hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            rGdvPerfil.ClientSettings.Selecting.AllowRowSelect = true;
            rCboPerfil.Enabled = true;
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
    private void EjecutaAccionUsuario()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccionUsuario(ref sMSGTip);
        if (msgValidacion == "")
        {


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAccionesUsuario();
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
    private string validaEjecutaAccionUsuario(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            //if (rGdvPerfil.SelectedItems.Count == 0)
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            //    return sResult;
            //}

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

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {



            return sResult;
        }




        return sResult;
    }


    ////////////////////////////////////////////////////////
    private void EjecutaAccionPerfil()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccionPerfil(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //GUARDA DEL GRID USUARIOS
                EjecutaSpAccionesPerfil();
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
    private string validaEjecutaAccionPerfil(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            //if (rGdvUsuario.SelectedItems.Count == 0)
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            //    return sResult;
            //}

            if (rCboPerfil.SelectedIndex == -1)
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



            return sResult;
        }




        return sResult;
    }

    //GUARDA DEL GRID USUARIOS
    private void EjecutaSpAccionesPerfil()
    {
        int CountItems = 0;
        EliminaUsu_Perf();
        string sEjecEstatus = "", sEjecMSG = "";

        foreach (GridDataItem x in  rGdvUsuario.SelectedItems) {
            var dataItem = rGdvUsuario.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null) {
                string maUsuCve = dataItem.GetDataKeyValue("maUsuCve").ToString();

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, maUsuCve);
                ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rCboPerfil.SelectedValue);
                
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {
                   
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    //if (sEjecEstatus == "1")
                    //{
                    //    InicioPagina();
                    //}
                    //ShowAlert(sEjecEstatus, sEjecMSG);

                }
            }
            CountItems += 1;
        }
        if (rGdvUsuario.SelectedItems.Count == 0)
        {
            string sMSGTip = "", sResult = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0001", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
            hdfBtnAccion.Value = "";
            ControlesAccion();
        }
        if (sEjecEstatus == "1")
        {
            ShowAlert(sEjecEstatus, sEjecMSG);
            hdfBtnAccion.Value = "";
            ControlesAccion();
        }
        
    }
    private void EliminaUsu_Perf() {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rCboPerfil.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }
    private void EliminaUsu_Usu()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuario.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }


    private void EjecutaSpAccionesUsuario()
    {
        int CountItems = 0;
        EliminaUsu_Usu();
        string sEjecEstatus = "", sEjecMSG = "";

        foreach (GridDataItem x in rGdvPerfil.SelectedItems)
        {
            var dataItem = rGdvPerfil.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {
                string maPerfCve = dataItem.GetDataKeyValue("maPerfCve").ToString();

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboUsuario.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, maPerfCve);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {

                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    //if (sEjecEstatus == "1")
                    //{
                    //    InicioPagina();
                    //}
                    //ShowAlert(sEjecEstatus, sEjecMSG);

                }
            }
            CountItems += 1;
        }
        if (rGdvPerfil.SelectedItems.Count == 0)
        {
            string sMSGTip = "", sResult = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0001", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
            hdfBtnAccion.Value = "";
            ControlesAccion();
        }
        if (sEjecEstatus == "1")
        {
            ShowAlert(sEjecEstatus, sEjecMSG);
            hdfBtnAccion.Value = "";
            ControlesAccion();
        }


    }

    //////////////////////////////////////////////////



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
    }
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsu_MAPerf";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rCboPerfil.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvUsuario, ds);

        for (int i = 0; i < rGdvUsuario.Items.Count; i++)
        {
            rGdvUsuario.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }





        foreach (GridDataItem dataItem in rGdvUsuario.Items)
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


    }





    //private void LlenaGridPerfiles()
    //{
    //    DataSet ds = new DataSet();
    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_MAPerfiles";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
    //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    //    FnCtlsFillIn.RadGrid(ref rGdvPerfil, ds);


    //    for (int i = 0; i < rGdvPerfil.Items.Count; i++)
    //    {
    //        rGdvPerfil.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
    //    }
      
        
    //}


    private void CargaCombosUsuarios()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsuarios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref this.rCboUsuario, ds, "maUsuCve", "maUsuNom", true, true);
        ((Literal)rCboUsuario.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboUsuario.Items.Count);

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

 



}