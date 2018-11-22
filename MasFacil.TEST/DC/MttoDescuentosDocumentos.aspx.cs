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

public partial class MttoDescuentosDocumentos : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_docCve;
    private string Pag_DesCve;
    private string Pag_RawUrl_Return;
    #endregion

    #region Eventos
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

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        RabComboBox_Descuentos(Pag_sConexionLog, Pag_sCompania, ref rCboDescuento, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false);

        LlenaGrid(50);
        rBtnDescuento.Checked = true;
        rBtnDocumento.Checked = false;


        rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();

        rCboDescuento.ClearSelection();
        rCboDocumento.ClearSelection();

        rCboDescuento.Enabled = false;
        rCboDocumento.Enabled = false;


        rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        rCboDescuento.BorderColor = System.Drawing.Color.Transparent;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        if (Pag_docCve != "" && Pag_docCve != null)
        {
            rCboDocumento.SelectedValue = Pag_docCve;
            rCboDocumento.Enabled = false;

        }
        if (Pag_DesCve != "" && Pag_DesCve != null)
        {
            rCboDescuento.SelectedValue = Pag_DesCve;
            rCboDescuento.Enabled = false;
            rBtnCancelar.Enabled = true;
        }
        rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDescuento_Documento.AllowMultiRowSelection = true;
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
        Pag_RawUrl_Return = "~" + Convert.ToString(Session["RawUrl_Return"]);

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
        }
        if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
        {
            Pag_DesCve = Request.QueryString["DesCve"];
        }
    }
    public bool RabComboBox_Descuentos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Descuentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "DesCve", "DesDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }

    protected void rGdvDescuento_Documento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdfBtnAccion.Value == "")
        {
            var dataItem = rGdvDescuento_Documento.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                rCboDescuento.SelectedValue = dataItem["DesCve"].Text;
                rCboDocumento.SelectedValue = dataItem["docCve"].Text;
            }
        }
    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        OpcNuevo();
        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            Pag_docCve = Request.QueryString["docCve"];
            rCboDocumento.Enabled = false;
            rCboDescuento.Enabled = true;
            rCboDescuento.ClearSelection();

        }
        else if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
        {
            Pag_DesCve = Request.QueryString["DesCve"];
            rCboDocumento.Enabled = true;
            rCboDescuento.Enabled = false;
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
            rCboDescuento.ClearSelection();
            rCboDescuento.BorderColor = System.Drawing.Color.Transparent;
            rCboDescuento.Enabled = false;
        }
        else if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento.Enabled = false;
            //EjecutaSpAccionEliminar();
        }
        else
        {
            rCboDescuento.ClearSelection();
            rCboDocumento.ClearSelection();
            rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();
        }
        rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDescuento_Documento.AllowMultiRowSelection = true;
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
       

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            rCboDescuento.ClearSelection();
            rCboDescuento.BorderColor = System.Drawing.Color.Transparent;

        }
        else if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        }
        else
        {
            rCboDescuento.ClearSelection();
            rCboDocumento.ClearSelection();
        }

        rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();

    }

    protected void rBtnDescuento_CheckedChanged(object sender, EventArgs e)
    {
        LlenaGrid(50);
        if (Request.QueryString["docCve"] == null && Request.QueryString["docCve"] == "")
        {
            rCboDescuento.ClearSelection();
            rCboDocumento.ClearSelection();
        }


    }

    protected void rBtnDocumento_CheckedChanged(object sender, EventArgs e)
    {
        LlenaGrid(51);
        if (Request.QueryString["docCve"] == null && Request.QueryString["docCve"] == "")
        {
            rCboDescuento.ClearSelection();
            rCboDocumento.ClearSelection();
        }

    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (hdfBtnAccion.Value == "")
        {
            if (Pag_DesCve != "" && Pag_DesCve != null)
            {
                Response.Redirect(Pag_RawUrl_Return);
                //Response.Redirect("~/DC/MttoDescuentos.aspx");
            }
            else
            {
                InicioPagina();
                rGdvDescuento_Documento.Enabled = true;
                rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();
                rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDescuento_Documento.AllowMultiRowSelection = false;
            }
        }else {
            InicioPagina();
        }
    }
    #endregion

    #region Metodos

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

            if (rCboDescuento.SelectedValue == "")
            {
                rCboDescuento.BorderWidth = Unit.Pixel(1);
                rCboDescuento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboDescuento.BorderColor = System.Drawing.Color.Transparent; }


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

            if (rGdvDescuento_Documento.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
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
            ProcBD.NombreProcedimiento = "sp_Descuento_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rCboDescuento.SelectedValue);

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
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";


            foreach (GridDataItem i in rGdvDescuento_Documento.SelectedItems)
            {

                var dataItem = rGdvDescuento_Documento.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Descuento_Documentos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, dataItem["DesCve"].Text);

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


    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    


    #endregion

    #region FUNCIONES

    private void LlenaGrid(int order)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Descuento_Documentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, order);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        if (Pag_docCve != "" && Pag_docCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_docCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, "");

        }
        if (Pag_DesCve != "" && Pag_DesCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, Pag_DesCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, "");
        }
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDescuento_Documento, ds);

    }


    private void OpcNuevo()
    {
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = false;
        rGdvDescuento_Documento.AllowMultiRowSelection = false;
        rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();
        rGdvDescuento_Documento.Enabled = false;
    }

    private void OpcEliminar()
    {
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDescuento_Documento.AllowMultiRowSelection = true;
        rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdvDescuento_Documento
        this.rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        
        rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        rCboDescuento.BorderColor = System.Drawing.Color.Transparent;
        
        rCboDocumento.Enabled = false;
        rCboDescuento.Enabled = false;

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
            rCboDescuento.Enabled = false;


            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboDescuento.ClearSelection();
            }
            else if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
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
                this.rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();

                rCboDocumento.Enabled = true;
                rCboDescuento.Enabled = true;

                rCboDocumento.ClearSelection();
                rCboDescuento.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            //    rGdvDescuento_Documento.AllowMultiRowSelection = false;
            //    this.rTxtCve.Enabled = false;
            //    this.rTxtDes.Enabled = true;
            //    this.rTxtAbr.Enabled = true;
            //    rBtnGuardar.Enabled = true;
            //    rBtnCancelar.Enabled = true;
            //}

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDescuento_Documento.AllowMultiRowSelection = true;
                rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();
                
               
                rCboDocumento.Enabled = false;
                rCboDescuento.Enabled = false;

                rCboDocumento.ClearSelection();
                rCboDescuento.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboDocumento.Enabled = false;
            rCboDescuento.Enabled = false;
            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboDescuento.ClearSelection();
            }
            else if (Request.QueryString["DesCve"] != null && Request.QueryString["DesCve"] != "")
            {
                rCboDocumento.ClearSelection();
            }          
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDescuento_Documento.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDescuento_Documento, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDescuento_Documento, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rCboDocumento.ClearSelection();
            rCboDescuento.ClearSelection();
        }
        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvDescuento_Documento.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDescuento_Documento.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
           // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboDescuento.BorderColor = System.Drawing.Color.Transparent;
            
            rCboDocumento.Enabled = false;
            rCboDescuento.Enabled = false;

            rCboDocumento.ClearSelection();
            rCboDescuento.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }
        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    #endregion



}
