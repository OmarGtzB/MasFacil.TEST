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

public partial class DC_MttoDocumentoImpuesto : System.Web.UI.Page
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
    private string Pag_impCve;
    private string Pag_RawUrl_Return;

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
            rCboImpuestos.Enabled = true;
            rCboImpuestos.ClearSelection();

        }
        else if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
        {
            Pag_impCve = Request.QueryString["impCve"];
            rCboDocumento.Enabled = true;
            rCboImpuestos.Enabled = false;
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
            rCboImpuestos.ClearSelection();
            rCboImpuestos.BorderColor = System.Drawing.Color.Transparent;
            rCboImpuestos.Enabled = false;
        }
        else if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento.Enabled = false;
        }
        else
        {
            rCboImpuestos.ClearSelection();
            rCboDocumento.ClearSelection();
            rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();
            ControlesAccion();
        }
        rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_Impuestos.AllowMultiRowSelection = true;
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();

        if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
        {
            rCboImpuestos.ClearSelection();
            rCboImpuestos.BorderColor = System.Drawing.Color.Transparent;

        }
        else if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
        {
            rCboDocumento.ClearSelection();
            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        }
        else
        {
            rCboImpuestos.ClearSelection();
            rCboDocumento.ClearSelection();
        }
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (Pag_impCve != "" && Pag_impCve != null)
        {
            //Response.Redirect("~/DC/MttoImpuestos.aspx");
            Response.Redirect(Pag_RawUrl_Return);
        }
        else
        {
            InicioPagina();
            rGdvDocumento_Impuestos.Enabled = true;
            rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();
            rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDocumento_Impuestos.AllowMultiRowSelection = false;
        }
    }

    protected void rGdvDocumento_Impuestos_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvDocumento_Impuestos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rCboImpuestos.SelectedValue = dataItem["impCve"].Text;
            rCboDocumento.SelectedValue = dataItem["docCve"].Text;
        }
    }


    #endregion

    #region METODOS

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
        if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
        {
            Pag_impCve = Request.QueryString["impCve"];
        }
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false);
        FnCtlsFillIn.RadComboBox_Impuestos(Pag_sConexionLog, Pag_sCompania, ref rCboImpuestos, true, false);
        ControlesAccion();
        LlenaGrid();
        if (Pag_docCve != "" && Pag_docCve != null)
        {
            rCboDocumento.SelectedValue = Pag_docCve;
            rCboDocumento.Enabled = false;
        }
        if (Pag_impCve != "" && Pag_impCve != null)
        {
            rCboImpuestos.SelectedValue = Pag_impCve;
            rCboImpuestos.Enabled = false;
            rBtnCancelar.Enabled = true;
            //rGdvDocumento_Impuestos.Width = System.Web.UI.WebControls.Unit.Pixel(664);
        }
        rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_Impuestos.AllowMultiRowSelection = true;

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

            if (rCboImpuestos.SelectedValue == "")
            {
                rCboImpuestos.BorderWidth = Unit.Pixel(1);
                rCboImpuestos.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboImpuestos.BorderColor = System.Drawing.Color.Transparent; }


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

            if (rGdvDocumento_Impuestos.SelectedItems.Count == 0)
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
            ProcBD.NombreProcedimiento = "sp_DocumentoImpuestos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue.ToString().Trim());
            ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, rCboImpuestos.SelectedValue.ToString().Trim());

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


            foreach (GridDataItem i in rGdvDocumento_Impuestos.SelectedItems)
            {

                var dataItem = rGdvDocumento_Impuestos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_DocumentoImpuestos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text.ToString().Trim());
                        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, dataItem["impCve"].Text.ToString().Trim());

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


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdvDocumento_Impuestos
        this.rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
        rCboImpuestos.BorderColor = System.Drawing.Color.Transparent;

        rCboDocumento.Enabled = false;
        rCboImpuestos.Enabled = false;

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
            rCboImpuestos.Enabled = false;

            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboImpuestos.ClearSelection();
            }
            if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
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
                this.rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();

                rCboDocumento.Enabled = true;
                rCboImpuestos.Enabled = true;

                rCboDocumento.ClearSelection();
                rCboImpuestos.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            //    rGdvDocumento_Impuestos.AllowMultiRowSelection = false;
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
                rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDocumento_Impuestos.AllowMultiRowSelection = true;
                rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();


                rCboDocumento.Enabled = false;
                rCboImpuestos.Enabled = false;

                rCboDocumento.ClearSelection();
                rCboImpuestos.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboDocumento.Enabled = false;
            rCboImpuestos.Enabled = false;
            if (Request.QueryString["docCve"] != null && Request.QueryString["docCve"] != "")
            {
                rCboImpuestos.ClearSelection();
            }
            if (Request.QueryString["impCve"] != null && Request.QueryString["impCve"] != "")
            {
                rCboDocumento.ClearSelection();
            }

        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDocumento_Impuestos.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_Impuestos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDocumento_Impuestos, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rCboImpuestos.ClearSelection();
        }
        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            // rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            rCboImpuestos.BorderColor = System.Drawing.Color.Transparent;

            rCboDocumento.Enabled = false;
            rCboImpuestos.Enabled = false;

            rCboDocumento.ClearSelection();
            rCboImpuestos.ClearSelection();

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

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoImpuestos";
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
        if (Pag_impCve != "" && Pag_impCve != null)
        {
            ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, Pag_impCve);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, "");
        }


        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDocumento_Impuestos, ds);

    }

    private void OpcNuevo()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_Impuestos.AllowMultiRowSelection = false;
        rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();
        rGdvDocumento_Impuestos.Enabled = false;
        rGdvDocumento_Impuestos.MasterTableView.Enabled = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = false;

    }

    private void OpcEliminar()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rGdvDocumento_Impuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDocumento_Impuestos.AllowMultiRowSelection = true;
        rGdvDocumento_Impuestos.MasterTableView.ClearSelectedItems();
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
    }

    #endregion





}