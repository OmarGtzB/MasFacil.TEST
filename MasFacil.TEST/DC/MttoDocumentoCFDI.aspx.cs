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

public partial class DC_MttoDocumentoCFDI : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_docCve;
    private string Pag_CFDI;
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
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        OpcNuevo();
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
            rCboDocumento.Enabled = false;
            rCboCFDI.Enabled = true;
            rCboCFDI.ClearSelection();

        }
        else if (Request.QueryString["CFDICve"] != null && Request.QueryString["CFDICve"] != "")
        {
            Pag_CFDI = Request.QueryString["CFDICve"];
            rCboDocumento.Enabled = true;
            rCboCFDI.Enabled = false;
            rCboDocumento.ClearSelection();
        }
        else
        {
            ControlesAccion();
        }
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
        //OpcEliminar();
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            rCboCFDI.ClearSelection();
            rCboCFDI.BorderColor = System.Drawing.Color.Transparent;
            rCboCFDI.Enabled = false;
        }
        else if (Request.QueryString["CFDICve"] != null && Request.QueryString["CFDICve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento.Enabled = false;
        }
        else
        {
            rCboCFDI.ClearSelection();
            rCboDocumento.ClearSelection();
            //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();
            ControlesAccion();
        }
        //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvDocumento_CFDI.AllowMultiRowSelection = true;
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
        if (Pag_CFDI != "" && Pag_CFDI != null)
        {
            Response.Redirect("~/DC/MttoImpuestos.aspx");
        }
        else
        {
            InicioPagina();
            //rGdvDocumento_CFDI.Enabled = true;
            //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();
            //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
            //rGdvDocumento_CFDI.AllowMultiRowSelection = false;
        }
    }

    //protected void rGdvDocumento_CFDI_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //var dataItem = rGdvDocumento_CFDI.SelectedItems[0] as GridDataItem;
    //    //if (dataItem != null)
    //    //{
    //    //    rCboCFDI.SelectedValue = dataItem["satUsoCFDICve"].Text;
    //    //    rCboDocumento.SelectedValue = dataItem["docCve"].Text;
    //    //}
    //}

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
        }
        if (Request.QueryString["CFDICve"] != null && Request.QueryString["CFDICve"] != "")
        {
            Pag_CFDI = Request.QueryString["CFDICve"];
        }
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false);
        RCboSatCFDI();
        ControlesAccion();
        LlenaGrid();
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            rCboDocumento.SelectedValue = Pag_docCve;
            rCboDocumento.Enabled = false;
        }
        if (Pag_CFDI != "" && Pag_CFDI != null)
        {
            rCboCFDI.SelectedValue = Pag_CFDI;
            rCboCFDI.Enabled = false;
            rBtnCancelar.Enabled = true;
            //rGdvDocumento_Impuestos.Width = System.Web.UI.WebControls.Unit.Pixel(664);
        }
        //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvDocumento_CFDI.AllowMultiRowSelection = true;


        if (rCboCFDI.SelectedIndex != -1)
        {
            rBtnNuevo.Enabled = false;
        }



    }

    private void RCboSatCFDI()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SATUsoCFDI";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboCFDI, ds, "satUsoCFDICve", "satUsoCFDIDes", true, false);
        ((Literal)rCboCFDI.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboCFDI.Items.Count);

    }

    private void EjecutaAccion()
    {

        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                EjecutaSpAcciones();

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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rCboCFDI.SelectedValue == "")
            {
                rCboCFDI.BorderWidth = Unit.Pixel(1);
                rCboCFDI.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboCFDI.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboDocumento.SelectedValue == "")
            {
                rCboDocumento.BorderWidth = Unit.Pixel(1);
                rCboDocumento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboDocumento.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rCboCFDI.SelectedValue == "")
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SATUsoCFDI_REL";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue.ToString().Trim());
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 5, ParameterDirection.Input, rCboCFDI.SelectedValue.ToString().Trim());

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                }
            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionEliminar()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SATUsoCFDI_REL";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 3, ParameterDirection.Input, rCboCFDI.SelectedValue.ToString());

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
                    InicioPagina();
                    rBtnNuevo.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdvDocumento_Impuestos
        //this.rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        rCboCFDI.BorderColor = System.Drawing.Color.Transparent;

        rCboDocumento.Enabled = false;
        rCboCFDI.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = true;

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
            rCboDocumento.Enabled = false;
            rCboCFDI.Enabled = false;

            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboCFDI.ClearSelection();
            }
            if (Request.QueryString["CFDICve"] != null && Request.QueryString["CFDICve"] != "")
            {
                rCboDocumento.ClearSelection();
            }


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
                //this.rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = false;
                //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();

                rCboDocumento.Enabled = true;
                rCboCFDI.Enabled = true;

                rCboDocumento.ClearSelection();
                rCboCFDI.ClearSelection();

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
                //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdvDocumento_CFDI.AllowMultiRowSelection = true;
                //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();


                rCboDocumento.Enabled = false;
                rCboCFDI.Enabled = false;

                rCboDocumento.ClearSelection();
                rCboCFDI.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboDocumento.Enabled = false;
            rCboCFDI.Enabled = false;
            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboCFDI.ClearSelection();
            }
            if (Request.QueryString["CFDICve"] != null && Request.QueryString["CFDICve"] != "")
            {
                rCboDocumento.ClearSelection();
            }

        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        //int GvSelectItem = rGdvDocumento_CFDI.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        ////MODIFICAR
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
        //    GvVAS = new string[] { "VAL0003", "VAL0008" };
        //    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_CFDI, GvVAS, ref sMSGTip, ref sResult) == false)
        //    {
        //        return sResult;
        //    }
        //}

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //GvVAS = new string[] { "VAL0003" };
            //if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_CFDI, GvVAS, ref sMSGTip, ref sResult) == false)
            //{
            //    return sResult;
            //}
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        if (hdfBtnAccion.Value != "")
        {
            rCboCFDI.ClearSelection();
        }

        //NUEVO
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        //{
        //    rCboDocumento.ClearSelection();
        //    rCboCFDI.ClearSelection();
        //}
        //MODIFICAR
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
        //    //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        //    //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();

        //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //    // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //    rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        //    rCboCFDI.BorderColor = System.Drawing.Color.Transparent;

        //    rCboDocumento.Enabled = false;
        //    rCboCFDI.Enabled = false;

        //    rCboDocumento.ClearSelection();
        //    rCboCFDI.ClearSelection();

        //    rBtnGuardar.Enabled = false;
        //    rBtnCancelar.Enabled = false;
        //}
        //ELIMINAR
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{

        //}

    }

    #endregion

    #region FUNCIONES

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SATUsoCFDI_REL";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        if (Pag_CFDI != "" && Pag_CFDI != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 3, ParameterDirection.Input, Pag_CFDI);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 3, ParameterDirection.Input, "");
        }


        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    
        if (FnValAdoNet.bDSRowsIsFill(ds) == true)
        {
             rCboCFDI.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["satUsoCFDICve"]);
        }

        

        //FnCtlsFillIn.RadGrid(ref rGdvDocumento_CFDI, ds);

    }

    private void OpcNuevo()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvDocumento_CFDI.AllowMultiRowSelection = false;
        //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();
        //rGdvDocumento_CFDI.Enabled = false;
        //rGdvDocumento_CFDI.MasterTableView.Enabled = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = false;

    }

    private void OpcEliminar()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rGdvDocumento_CFDI.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvDocumento_CFDI.AllowMultiRowSelection = true;
        //rGdvDocumento_CFDI.MasterTableView.ClearSelectedItems();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }

    #endregion




}