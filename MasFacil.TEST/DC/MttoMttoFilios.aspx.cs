using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;

public partial class DC_MttoMttoFilios : System.Web.UI.Page
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


    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {

        //LimpiaControles();
        //rCmboTipo.BorderColor = System.Drawing.Color.Transparent;
        //rCmboFormato.BorderColor = System.Drawing.Color.Transparent;

        //rTxtDescripcion.CssClass = "cssTxtEnabled";
        //rTxtClave.CssClass = "cssTxtEnabled";
        //rdNumerico.CssClass = "cssTxtEnabled";

        //rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();
        //rGdv_FolioManual.MasterTableView.ClearSelectedItems();


        EjecutaAccionLimpiar();
       
            if (rCboManFol.SelectedValue == "2")
            {
            //    if (rCmboTipo.SelectedValue == "1")
            //    {
            //        rdNumerico.Visible = true;
            //        rTxtCaracter.Visible = false;
            //        rTxtCaracter.Visible = false;
            //    //rdNumerico.Enabled = true;
            //}
            //    if (rCmboTipo.SelectedValue == "2")
            //    {
            //        rTxtCaracter.Visible = true;
            //        rdNumerico.Visible = false;
            //        // rTxtCaracter.Enabled = true;
            //    }


            }
     



    }

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "";
        rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();
        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FolioAutomatico.AllowMultiRowSelection = true;

        rGdv_FolioManual.MasterTableView.ClearSelectedItems();
        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FolioManual.AllowMultiRowSelection = true;
        ControlesAccion();
        //InicioPagina();
    }

    protected void rCboManFol_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        TipFol();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        GridVisible();

        if (rCboManFol.SelectedValue == "2")
        {
            if (rCmboTipo.SelectedValue == "1")
            {
                rdNumerico.Visible = true;
                rTxtValor.Visible = false;
                rTxtValor.Text = "";
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                rdNumerico.Visible = false;
                rdNumerico.Text = "";
                rTxtValor.Visible = true;
            }
        }
        else
        {
            rdNumerico.Visible = false;
            rdNumerico.Text = "";
            rTxtValor.Visible = false;
            rTxtValor.Text = "";

        }
        LimpiaControles();

    }

    protected void rGdv_FolioAutomatico_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rCmboTipo.SelectedValue == "1")
        {
            rdNumerico.Visible = true;
            rTxtValor.Visible = false;
            rTxtValor.Text = "";
        }
        if (rCmboTipo.SelectedValue == "2")
        {
            rdNumerico.Visible = false;
            rdNumerico.Text = "";
            rTxtValor.Visible = true;
        }

        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
            //DesHabilita(true);
            rTxtClave.Enabled = false;

            if (rCmboTipo.SelectedValue == "1")
            {
                rdNumerico.Visible = true;
                //rdNumerico.Enabled = true;
                rTxtValor.Visible = false;
                rTxtValor.Text = "";
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                rdNumerico.Visible = false;
                rdNumerico.Text = "";
                rTxtValor.Visible = true;
               // rTxtValor.Enabled = true;
            }

        //}
        //if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{


            var dataItem = rGdv_FolioAutomatico.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                string skey = dataItem.GetDataKeyValue("folCve").ToString();

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Folios";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, skey);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);



                rTxtClave.Text = ds.Tables[0].Rows[0]["folCve"].ToString();

                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["folioDes"].ToString();


                rCmboTipo.SelectedValue = ds.Tables[0].Rows[0]["folTip"].ToString();


                if (rCmboTipo.SelectedValue == "1")
                {

                    rdNumerico.Text = ds.Tables[0].Rows[0]["folValIntStr"].ToString();
                    rdNumerico.Visible = true;
                    rTxtValor.Visible = false;
                    rTxtValor.Text = "";

                }
                if (rCmboTipo.SelectedValue == "2")
                {
                    rTxtValor.Text = ds.Tables[0].Rows[0]["folValIntStr"].ToString();
                    rdNumerico.Visible = false;
                    rdNumerico.Text = "";
                    rTxtValor.Visible = true;
                }



                rTxtValor.Text = ds.Tables[0].Rows[0]["folValIntStr"].ToString();

                if (ds.Tables[0].Rows[0]["formFolCve"].ToString() != "")
                {
                    rCmboFormato.SelectedValue = ds.Tables[0].Rows[0]["formFolCve"].ToString();
                }
                else
                {
                    rCmboFormato.ClearSelection();
                }


                rTxtPrefijo.Text = ds.Tables[0].Rows[0]["formFolPrefijo"].ToString();

                rTxtSubfijo.Text = ds.Tables[0].Rows[0]["formFolSufijo"].ToString();
            }
        //}

    }

    protected void rGdv_FolioManual_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //DesHabilita(true);
            rTxtClave.Enabled = false;

        }
        var dataItem = rGdv_FolioManual.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string skey = dataItem.GetDataKeyValue("formFolCve").ToString();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Folios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@formFolCve", DbType.String, 10, ParameterDirection.Input, skey);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            
            rTxtClave.Text = ds.Tables[0].Rows[0]["formFolCve"].ToString();

            rTxtDescripcion.Text = ds.Tables[0].Rows[0]["formFolDes"].ToString();
            
            rTxtLongitud.Text = ds.Tables[0].Rows[0]["formFolLon"].ToString();
            
            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "1")
            {
                AlineaDerecha.Checked = false;
                AlineaIzquierda.Checked = true;
            }
            if (ds.Tables[0].Rows[0]["formFolPos"].ToString() == "2")
            {
                AlineaIzquierda.Checked = false;
                AlineaDerecha.Checked = true;
            }

            if (ds.Tables[0].Rows[0]["formFolChar"].ToString() != "")
            {
                rTxtCaracter.Text = ds.Tables[0].Rows[0]["formFolChar"].ToString();
            }
            else
            {
                rTxtCaracter.Text = "";
            }
            
        }
    }

    protected void rCmboTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCmboTipo.SelectedValue == "1")
        {
            rdNumerico.Visible = true;
            rTxtValor.Visible = false;
            rTxtValor.Text = "";
        }
        if (rCmboTipo.SelectedValue == "2")
        {
            rdNumerico.Visible = false;
            rdNumerico.Text = "";
            rTxtValor.Visible = true;
        }
    }

    #endregion


    #region METODOS

    private void InicioPagina()
    {
        LimpiaControles();
        CargarCombos();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        LLenaGrid(51);

        rGdv_FolioAutomatico.Visible = true;
        rGdv_FolioManual.Visible = false;
        rTxtCaracter.Visible = false;

        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FolioAutomatico.AllowMultiRowSelection = true;


        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FolioManual.AllowMultiRowSelection = true;
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

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
            }


            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            
            //Manual
            if (rCboManFol.SelectedValue == "1")
            {
                if (rTxtLongitud.Text.Trim() == "")
                {
                    rTxtLongitud.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtLongitud.CssClass = "cssTxtEnabled"; }

                if (rTxtCaracter.Text.Trim() == "")
                {
                    rTxtCaracter.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCaracter.CssClass = "cssTxtEnabled"; }

            }


            //AUTOMATICO

            if (rCboManFol.SelectedValue == "2")
            {
                if (rCmboTipo.SelectedValue == "2")
                {
                    if (rTxtValor.Text.Trim() == "")
                    {
                        rTxtValor.CssClass = "cssTxtInvalid";
                        camposInc += 1;
                    }
                    else
                    { rTxtValor.CssClass = "cssTxtEnabled"; }
                }

                if (rCmboTipo.SelectedValue == "1")
                {
                    if (rdNumerico.Text.Trim() == "")
                    {
                        rdNumerico.CssClass = "cssTxtInvalid";
                        camposInc += 1;
                    }
                    else
                    { rdNumerico.CssClass = "cssTxtEnabled"; }
                }


                if (rCmboTipo.Text == "")
                {
                    rCmboTipo.CssClass = "cssTxtInvalid";
                    rCmboTipo.BorderWidth = Unit.Pixel(1);
                    rCmboTipo.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                { rCmboTipo.BorderColor = System.Drawing.Color.Transparent; }



            }

            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
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
            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
            }


            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            //Manual
            if (rCboManFol.SelectedValue == "1")
            {
                if (rTxtLongitud.Text.Trim() == "")
                {
                    rTxtLongitud.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtLongitud.CssClass = "cssTxtEnabled"; }

                if (rTxtCaracter.Text.Trim() == "")
                {
                    rTxtCaracter.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCaracter.CssClass = "cssTxtEnabled"; }

                if (rGdv_FolioManual.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

            }


            //AUTOMATICO
            //AUTOMATICO

            if (rCboManFol.SelectedValue == "2")
            {
                if (rCmboTipo.SelectedValue == "2")
                {
                    if (rTxtValor.Text.Trim() == "")
                    {
                        rTxtValor.CssClass = "cssTxtInvalid";
                        camposInc += 1;
                    }
                    else
                    { rTxtValor.CssClass = "cssTxtEnabled"; }
                }

                if (rCmboTipo.SelectedValue == "1")
                {
                    if (rdNumerico.Text.Trim() == "")
                    {
                        rdNumerico.CssClass = "cssTxtInvalid";
                        camposInc += 1;
                    }
                    else
                    { rdNumerico.CssClass = "cssTxtEnabled"; }
                }


                if (rCmboTipo.Text == "")
                {
                    rCmboTipo.CssClass = "cssTxtInvalid";
                    rCmboTipo.BorderWidth = Unit.Pixel(1);
                    rCmboTipo.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                { rCmboTipo.BorderColor = System.Drawing.Color.Transparent; }



            }

            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
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
            if (rCboManFol.SelectedValue == "1")
            {

                if (rGdv_FolioManual.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                return sResult;
            }
            if (rCboManFol.SelectedValue == "2")
            {

                if (rGdv_FolioAutomatico.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                return sResult;
            }

        }




        return sResult;
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
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private void EjecutaSpAccionEliminar()
    {
        if (rCboManFol.SelectedValue == "1")
        {
            EliminarFolioManual();
        }
        else
        {
            EliminarFolAutomatico();
        }

    }

    //private void ControlesAccion()
    //{

    //    rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();
    //    rGdv_FolioManual.MasterTableView.ClearSelectedItems();

    //    rGdv_FolioAutomatico.AllowMultiRowSelection = false;
    //    rGdv_FolioManual.AllowMultiRowSelection = false;

    //    //Limpia Controles
    //    LimpiaControles();

    //    //Deshabilita Controles
    //    DesHabilita(false);

    //    if (rCboManFol.SelectedValue == "2")
    //    {
    //        if (rCmboTipo.SelectedValue == "1")
    //        {
    //            rdNumerico.Visible = true;
    //            rTxtValor.Visible = false;
    //            rTxtValor.Text = "";
    //        }
    //        if (rCmboTipo.SelectedValue == "2")
    //        {
    //            rdNumerico.Visible = false;
    //            rdNumerico.Text = "";
    //            rTxtValor.Visible = true;
    //        }
    //    }


    //    rCmboTipo.BorderColor = System.Drawing.Color.Transparent;
    //    rCmboFormato.BorderColor = System.Drawing.Color.Transparent;
    //    rCboManFol.BorderColor = System.Drawing.Color.Transparent;

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    rTxtClave.CssClass = "cssTxtEnabled";
    //    rTxtDescripcion.CssClass = "cssTxtEnabled";
    //    rTxtValor.CssClass = "cssTxtEnabled";
    //    rTxtPrefijo.CssClass = "cssTxtEnabled";
    //    rTxtSubfijo.CssClass = "cssTxtEnabled";
    //    rTxtLongitud.CssClass = "cssTxtEnabled";
    //    rTxtCaracter.CssClass = "cssTxtEnabled";
    //    rdNumerico.CssClass = "cssTxtEnabled";

    //    rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
    //    rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = false;

    //        if (rCboManFol.SelectedValue == "2")
    //        {
    //            if (rCmboTipo.SelectedValue == "1")
    //            {
    //                rdNumerico.Visible = true;
    //                rTxtCaracter.Visible = false;
    //                rdNumerico.Enabled = true;
    //            }
    //            if (rCmboTipo.SelectedValue == "2")
    //            {
    //                rTxtCaracter.Visible = true;
    //                rdNumerico.Visible = false;
    //                rTxtCaracter.Enabled = true;
    //            }
    //        }

    //        DesHabilita(true);
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FolioAutomatico.AllowMultiRowSelection = false;
    //        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FolioManual.AllowMultiRowSelection = false;


    //    }

    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FolioAutomatico.AllowMultiRowSelection = true;
    //        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FolioManual.AllowMultiRowSelection = true;
    //        DesHabilita(false);
    //        LimpiaControles();
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
    //        //rtxtCve.Enabled = false;
    //        //rTxtDes.Enabled = false;
    //        //rTxtAbr.Enabled = false;
    //        //rTxtSigno.Enabled = false;
    //    }


    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //       )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //}

    private void EjecutaSpAcciones()
    {
        if (rCboManFol.SelectedValue == "1")
        {
            EjecutaSpAccionesManual();
        }
        else
        {
            EjecutaSpAccionesAutomatico();
        }
    }


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
        this.rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtLongitud.CssClass = "cssTxtEnabled";
        rTxtCaracter.CssClass = "cssTxtEnabled";
        rTxtValor.CssClass = "cssTxtEnabled";
        rdNumerico.CssClass = "cssTxtEnabled";
        rCmboTipo.BorderColor = System.Drawing.Color.Transparent;
        rCboManFol.BorderColor = System.Drawing.Color.Transparent;

        rTxtClave.Enabled = false;
        rTxtDescripcion.Enabled = false;
        rTxtLongitud.Enabled = false;
        AlineaIzquierda.Enabled = false;
        AlineaDerecha.Enabled = false;
        rTxtCaracter.Enabled = false;
        rCmboTipo.Enabled = false;
        rTxtValor.Enabled = false;
        rdNumerico.Enabled = false;
        rCmboFormato.Enabled = false;
        rTxtPrefijo.Enabled = false;
        rTxtSubfijo.Enabled = false;

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


            rTxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtLongitud.Enabled = false;
            AlineaIzquierda.Enabled = false;
            AlineaDerecha.Enabled = false;
            rTxtCaracter.Enabled = false;
            rCmboTipo.Enabled = false;
            rTxtValor.Enabled = false;
            rdNumerico.Enabled = false;
            rCmboFormato.Enabled = false;
            rTxtPrefijo.Enabled = false;
            rTxtSubfijo.Enabled = false;


            rTxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtLongitud.Text = "";
            rTxtPrefijo.Text = "";
            rTxtSubfijo.Text = "";
            rTxtCaracter.Text = "";
            rdNumerico.Text = "";
            rTxtValor.Text = "";
            rCmboFormato.ClearSelection();
            rCmboTipo.ClearSelection();
            AlineaIzquierda.Checked = true;
            AlineaDerecha.Checked = false;




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
                this.rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();

                this.rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_FolioManual.MasterTableView.ClearSelectedItems();

                rTxtClave.Enabled = true;
                rTxtDescripcion.Enabled = true;
                rTxtLongitud.Enabled = true;
                AlineaIzquierda.Enabled = true;
                AlineaDerecha.Enabled = true;
                rTxtCaracter.Enabled = true;
                rCmboTipo.Enabled = true;
                rTxtValor.Enabled = true;
                rdNumerico.Enabled = true;
                rCmboFormato.Enabled = true;
                rTxtPrefijo.Enabled = true;
                rTxtSubfijo.Enabled = true;

                rTxtClave.Text = "";
                rTxtDescripcion.Text = "";
                rTxtLongitud.Text = "";
                rTxtPrefijo.Text = "";
                rTxtSubfijo.Text = "";
                rTxtCaracter.Text = "";
                rdNumerico.Text = "";
                rTxtValor.Text = "";
                rCmboFormato.ClearSelection();
                rCmboTipo.ClearSelection();
                AlineaIzquierda.Checked = true;
                AlineaDerecha.Checked = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_FolioAutomatico.AllowMultiRowSelection = false;

                rGdv_FolioManual.AllowMultiRowSelection = false;

                rTxtClave.Enabled = false;
                rTxtDescripcion.Enabled = true;
                rTxtLongitud.Enabled = true;
                AlineaIzquierda.Enabled = true;
                AlineaDerecha.Enabled = true;
                rTxtCaracter.Enabled = true;
                rCmboTipo.Enabled = true;
                rTxtValor.Enabled = true;
                rdNumerico.Enabled = true;
                rCmboFormato.Enabled = true;
                rTxtPrefijo.Enabled = true;
                rTxtSubfijo.Enabled = true;

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
                rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_FolioAutomatico.AllowMultiRowSelection = true;
                rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();

                rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_FolioManual.AllowMultiRowSelection = true;
                rGdv_FolioManual.MasterTableView.ClearSelectedItems();

                rTxtClave.Enabled = false;
                rTxtDescripcion.Enabled = false;
                rTxtLongitud.Enabled = false;
                AlineaIzquierda.Enabled = false;
                AlineaDerecha.Enabled = false;
                rTxtCaracter.Enabled = false;
                rCmboTipo.Enabled = false;
                rTxtValor.Enabled = false;
                rdNumerico.Enabled = false;
                rCmboFormato.Enabled = false;
                rTxtPrefijo.Enabled = false;
                rTxtSubfijo.Enabled = false;

                rTxtClave.Text = "";
                rTxtDescripcion.Text = "";
                rTxtLongitud.Text = "";
                rTxtPrefijo.Text = "";
                rTxtSubfijo.Text = "";
                rTxtCaracter.Text = "";
                rdNumerico.Text = "";
                rTxtValor.Text = "";
                rCmboFormato.ClearSelection();
                rCmboTipo.ClearSelection();
                AlineaIzquierda.Checked = true;
                AlineaDerecha.Checked = false;
            }
        }


        if (Result == false)
        {
            rTxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtLongitud.Enabled = false;
            AlineaIzquierda.Enabled = false;
            AlineaDerecha.Enabled = false;
            rTxtCaracter.Enabled = false;
            rCmboTipo.Enabled = false;
            rTxtValor.Enabled = false;
            rdNumerico.Enabled = false;
            rCmboFormato.Enabled = false;
            rTxtPrefijo.Enabled = false;
            rTxtSubfijo.Enabled = false;

            rTxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtLongitud.Text = "";
            rTxtPrefijo.Text = "";
            rTxtSubfijo.Text = "";
            rTxtCaracter.Text = "";
            rdNumerico.Text = "";
            rTxtValor.Text = "";
            rCmboFormato.ClearSelection();
            rCmboTipo.ClearSelection();
            AlineaIzquierda.Checked = true;
            AlineaDerecha.Checked = false;
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItemMAN = rGdv_FolioManual.SelectedItems.Count;

        int GvSelectItemAUT = rGdv_FolioAutomatico.SelectedItems.Count;



        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Manual
            if (rCboManFol.SelectedValue == "1")
            {
                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioManual, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

                //ELIMINAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    GvVAS = new string[] { "VAL0003" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioManual, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }

            //AUTOMATICO
            if (rCboManFol.SelectedValue == "2")
            {
                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioAutomatico, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

                //ELIMINAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    GvVAS = new string[] { "VAL0003" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioAutomatico, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }



        }
        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //Manual
            if (rCboManFol.SelectedValue == "1")
            {
                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioManual, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

                //ELIMINAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    GvVAS = new string[] { "VAL0003" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioManual, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }

            //AUTOMATICO
            if (rCboManFol.SelectedValue == "2")
            {
                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    GvVAS = new string[] { "VAL0003", "VAL0008" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioAutomatico, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

                //ELIMINAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    GvVAS = new string[] { "VAL0003" };
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FolioAutomatico, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }
        }





        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rTxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtLongitud.Text = "";
            rTxtPrefijo.Text = "";
            rTxtSubfijo.Text = "";
            rTxtCaracter.Text = "";
            rdNumerico.Text = "";
            rTxtValor.Text = "";
            rCmboFormato.ClearSelection();
            rCmboTipo.ClearSelection();
            AlineaIzquierda.Checked = true;
            AlineaDerecha.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
           

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtClave.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rTxtLongitud.CssClass = "cssTxtEnabled";
            rTxtCaracter.CssClass = "cssTxtEnabled";
            rTxtValor.CssClass = "cssTxtEnabled";
            rdNumerico.CssClass = "cssTxtEnabled";
            rCmboTipo.BorderColor = System.Drawing.Color.Transparent;
            rCboManFol.BorderColor = System.Drawing.Color.Transparent;

            rTxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtLongitud.Enabled = false;
            AlineaIzquierda.Enabled = false;
            AlineaDerecha.Enabled = false;
            rTxtCaracter.Enabled = false;
            rCmboTipo.Enabled = false;
            rTxtValor.Enabled = false;
            rdNumerico.Enabled = false;
            rCmboFormato.Enabled = false;
            rTxtPrefijo.Enabled = false;
            rTxtSubfijo.Enabled = false;

            rTxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtLongitud.Text = "";
            rTxtPrefijo.Text = "";
            rTxtSubfijo.Text = "";
            rTxtCaracter.Text = "";
            rdNumerico.Text = "";
            rTxtValor.Text = "";
            rCmboFormato.ClearSelection();
            rCmboTipo.ClearSelection();
            AlineaIzquierda.Checked = true;
            AlineaDerecha.Checked = false;

            rGdv_FolioAutomatico.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_FolioAutomatico.AllowMultiRowSelection = true;
            rGdv_FolioAutomatico.MasterTableView.ClearSelectedItems();

            rGdv_FolioManual.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_FolioManual.AllowMultiRowSelection = true;
            rGdv_FolioManual.MasterTableView.ClearSelectedItems();
            hdfBtnAccion.Value = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
      

    }


    #endregion


    #region FUNCIONES

    private void EliminarFolAutomatico()
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


            foreach (GridDataItem i in rGdv_FolioAutomatico.SelectedItems)
            {

                var dataItem = rGdv_FolioAutomatico.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string key = dataItem.GetDataKeyValue("folCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Folios";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, key);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@formManAut", DbType.Int32, 0, ParameterDirection.Input, 2);

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

    private void EliminarFolioManual()
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


            foreach (GridDataItem i in rGdv_FolioManual.SelectedItems)
            {

                var dataItem = rGdv_FolioManual.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string key = dataItem.GetDataKeyValue("formFolCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Folios";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@formManAut", DbType.Int32, 0, ParameterDirection.Input, 1);
                        ProcBD.AgregarParametrosProcedimiento("@formFolCve", DbType.String, 10, ParameterDirection.Input, key);

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
                    LimpiaControles();
                    LLenaGrid(52);
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {
                    LimpiaControles();
                    LLenaGrid(52);
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
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
                    LimpiaControles();
                    LLenaGrid(52);
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {
                    LimpiaControles();
                    LLenaGrid(52);
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    public void CargarCombos()
    {

        if (rCmboTipo.SelectedValue == "")
        {
            RadComboBoxItem item = new RadComboBoxItem();
            RadComboBoxItem item0 = new RadComboBoxItem();
            item.Text = "Numerico";
            item.Value = "1";

            item0.Text = "Caracter";
            item0.Value = "2";

            rCmboTipo.Items.Add(item);
            rCmboTipo.Items.Add(item0);

            rCmboTipo.ClearSelection();
        }







((Literal)rCmboTipo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCmboTipo.Items.Count);
        rCmboTipo.SelectedIndex = -1;

        FnCtlsFillIn.RadComboBox_ManejoFolios(Pag_sConexionLog, Pag_sCompania, ref rCboManFol, true, true, "1");
        rCboManFol.SelectedIndex = 1;

        FnCtlsFillIn.RadComboBox_FormatoFolios(Pag_sConexionLog, Pag_sCompania, ref rCmboFormato, true, false);
        TipFol();

    }

    private int TipFolCmbo()
    {
        if (rCmboTipo.SelectedValue == "1")
        {
            return 1;
        }

        if (rCmboTipo.SelectedValue == "2")
        {
            return 2;
        }
        return 0;

    }


    private void EjecutaSpAccionesAutomatico()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Folios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@folCve", DbType.String, 10, ParameterDirection.Input, rTxtClave.Text);
            ProcBD.AgregarParametrosProcedimiento("@folDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@folTip", DbType.Int32, 0, ParameterDirection.Input, TipFolCmbo());
            ProcBD.AgregarParametrosProcedimiento("@formManAut", DbType.Int32, 0, ParameterDirection.Input, 2);





            if (rCmboTipo.SelectedValue == "1")
            {
                ProcBD.AgregarParametrosProcedimiento("@folValInt", DbType.Int64, 0, ParameterDirection.Input, rdNumerico.Text);
            }
            if (rCmboTipo.SelectedValue == "2")
            {
                ProcBD.AgregarParametrosProcedimiento("@folValStr", DbType.String, 200, ParameterDirection.Input, rTxtValor.Text);
            }




            if (rCmboFormato.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@formFolCve", DbType.String, 10, ParameterDirection.Input, rCmboFormato.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@folManForm", DbType.Int64, 1, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@folManForm", DbType.Int64, 1, ParameterDirection.Input, 0);
            }



            if (rTxtPrefijo.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@formFolPrefijo", DbType.String, 5, ParameterDirection.Input, rTxtPrefijo.Text);
            }

            if (rTxtSubfijo.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@formFolSubfijo", DbType.String, 5, ParameterDirection.Input, rTxtSubfijo.Text);
            }

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    //LLenaGrid(51);
                    InicioPagina();

                }
            }

        }
        catch (Exception ex)
        {
            ShowAlert("1", ex.ToString());
            //string MsgError = ex.Message.Trim();
        }
    }


    private void EjecutaSpAccionesManual()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Folios";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@formFolCve", DbType.String, 10, ParameterDirection.Input, rTxtClave.Text);
            ProcBD.AgregarParametrosProcedimiento("@formFolDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);

            ProcBD.AgregarParametrosProcedimiento("@formFolLon", DbType.Int64, 0, ParameterDirection.Input, rTxtLongitud.Text);
            ProcBD.AgregarParametrosProcedimiento("@formFolPos", DbType.Int64, 0, ParameterDirection.Input, Alineacion());

            ProcBD.AgregarParametrosProcedimiento("@formFolChar", DbType.String, 1, ParameterDirection.Input, rTxtCaracter.Text);
            ProcBD.AgregarParametrosProcedimiento("@formManAut", DbType.Int32, 0, ParameterDirection.Input, 1);


            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    //LimpiaControles();
                    //InicioPagina();
                    LimpiaControles();
                    LLenaGrid(52);
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }

    private int Alineacion()
    {
        if (AlineaDerecha.Checked == true)
        {
            return 2;
        }
        if (AlineaIzquierda.Checked == true)
        {
            return 1;
        }

        return 0;
    }

    private void TipFol()
    {
        if (rCboManFol.SelectedValue == "1")
        {
            FolManual();
        }
        else
        {
            FolAutomatico();
        }

    }

    private void GridVisible()
    {
        if (rCboManFol.SelectedValue == "1")
        {
            rGdv_FolioManual.Visible = true;
            rGdv_FolioAutomatico.Visible = false;
            LLenaGrid(52);
        }
        if (rCboManFol.SelectedValue == "2")
        {
            rGdv_FolioManual.Visible = false;
            rGdv_FolioAutomatico.Visible = true;
            LLenaGrid(51);
        }

    }
    private void FolManual()
    {
        rdlblValor.Visible = false;
        rTxtValor.Visible = false;

        rdlblFormato.Visible = false;
        rCmboFormato.Visible = false;

        rdlblPrefijo.Visible = false;
        rTxtPrefijo.Visible = false;

        rdlblSubfijo.Visible = false;
        rTxtSubfijo.Visible = false;

        rdlblTipo.Visible = false;
        rCmboTipo.Visible = false;
        rdNumerico.Visible = false;


        rdlblLongitud.Visible = true;
        rTxtLongitud.Visible = true;

        rdlblAlineacion.Visible = true;
        AlineaDerecha.Visible = true;
        AlineaIzquierda.Visible = true;
        rdlblCaracter.Visible = true;
        rTxtCaracter.Visible = true;


    }

    private void FolAutomatico()
    {
        rdlblValor.Visible = true;


        rdlblFormato.Visible = true;
        rCmboFormato.Visible = true;

        rdlblPrefijo.Visible = true;
        rTxtPrefijo.Visible = true;

        rdlblSubfijo.Visible = true;
        rTxtSubfijo.Visible = true;

        rdlblTipo.Visible = true;
        rCmboTipo.Visible = true;


        /////////////////
        rdlblLongitud.Visible = false;
        rTxtLongitud.Visible = false;


        rdlblAlineacion.Visible = false;
        AlineaDerecha.Visible = false;
        AlineaIzquierda.Visible = false;

        rdlblCaracter.Visible = false;
        rTxtCaracter.Visible = false;

        if (rCmboTipo.SelectedValue == "1")
        {
            rdNumerico.Visible = true;
            rTxtValor.Visible = false;
        }
        if (rCmboTipo.SelectedValue == "2")
        {
            rTxtValor.Visible = true;
            rdNumerico.Visible = false;
        }
    }

    private void DesHabilita(bool des)
    {
        rTxtClave.Enabled = des;
        rdlblValor.Enabled = des;
        rTxtValor.Enabled = des;
        rdlblFormato.Enabled = des;
        rCmboFormato.Enabled = des;
        rdlblPrefijo.Enabled = des;
        rTxtPrefijo.Enabled = des;
        rdlblSubfijo.Enabled = des;
        rTxtSubfijo.Enabled = des;
        rdlblLongitud.Enabled = des;
        rTxtLongitud.Enabled = des;
        rdlblTipo.Enabled = des;
        rCmboTipo.Enabled = des;
        rdlblAlineacion.Enabled = des;
        AlineaDerecha.Enabled = des;
        AlineaIzquierda.Enabled = des;
        rdlblCaracter.Enabled = des;
        rTxtCaracter.Enabled = des;
        rTxtDescripcion.Enabled = des;
        rdNumerico.Enabled = des;


    }

    private void LimpiaControles()
    {
        AlineaDerecha.Checked = true;
        AlineaIzquierda.Checked = false;
        rTxtValor.Text = "";
        rTxtPrefijo.Text = "";
        rTxtSubfijo.Text = "";
        rTxtLongitud.Text = "";
        rTxtCaracter.Text = "";
        rCmboTipo.ClearSelection();
        rCmboFormato.ClearSelection();
        rTxtClave.Text = "";
        rTxtDescripcion.Text = "";
        rdNumerico.Text = "";

    }


    private void LLenaGrid(int Val)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Folios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, Val);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (Val == 51)
        {
            FnCtlsFillIn.RadGrid(ref rGdv_FolioAutomatico, ds);
        }
        if (Val == 52)
        {
            FnCtlsFillIn.RadGrid(ref rGdv_FolioManual, ds);
        }

    }



    #endregion









}