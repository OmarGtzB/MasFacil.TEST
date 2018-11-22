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

public partial class DC_MttoImpuestos : System.Web.UI.Page
{

    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();
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

    protected void rBtnDocumentosImp_Click(object sender, ImageButtonClickEventArgs e)
    {
        string sResult = "", sMSGTip = "";
        if (rGdvImpuestos.SelectedItems.Count > 0)
        {

            if (rGdvImpuestos.SelectedItems.Count > 1)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);
            }
            else
            {
                var dataItem = rGdvImpuestos.SelectedItems[0] as GridDataItem;
                string impCve = dataItem.GetDataKeyValue("impCve").ToString();
                Session["RawUrl_Return"] = hdfRawUrl.Value;
                Response.Redirect("~/DC/MttoDocumentoImpuesto.aspx?" + "impCve=" + impCve + "&idM=" + Request.QueryString["idM"]);
            }
            

        }
        else
        {

            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
        }
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

    protected void rGdvImpuestos_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvImpuestos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                //{
                //    this.rtxtClave.Enabled = false;
                //    this.rTxtDescripcion.Enabled = true;
                //    this.rTxtAbre.Enabled = true;
                //    this.rTxtPorcentaje.Enabled = true;
                //}
                string skey = dataItem.GetDataKeyValue("impCve").ToString();
                string val = dataItem["impTas"].Text;
                string val1 = dataItem["impAbr"].Text;

                rtxtClave.Text = dataItem["impCve"].Text;
                rTxtDescripcion.Text = dataItem["ImpDes"].Text;

                if (dataItem["satImpuCve"].Text != "&nbsp;")
                {
                    rCboSatImpuestos.SelectedValue = dataItem["satImpuCve"].Text;
                }else {
                    rCboSatImpuestos.ClearSelection();
                }

            if (val == "&nbsp;")
                {
                    rTxtPorcentaje.Text = "";
                }
                else
                {
                    rTxtPorcentaje.Text = dataItem["impTas"].Text;
                }
                if (val1 == "&nbsp;")
                {
                    rTxtAbre.Text = "";
                }
                else
                {
                    rTxtAbre.Text = dataItem["impAbr"].Text;
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
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        LlenaGrid();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvImpuestos.AllowMultiRowSelection = true;
        RCboSatMonedas();
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


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvImpuestos.AllowMultiRowSelection = true;
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

            //if (this.rCboAlmacen.SelectedIndex == -1)
            //{
            //    camposInc += 1;
            //}

            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            if (rTxtAbre.Text.Trim() == "")
            {
                rTxtAbre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbre.CssClass = "cssTxtEnabled"; }

            if (rTxtPorcentaje.Text.Trim() == "")
            {
                rTxtPorcentaje.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtPorcentaje.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvImpuestos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rtxtClave.Text.Trim() == "")
            {
                rtxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

            if (rTxtAbre.Text.Trim() == "")
            {
                rTxtAbre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbre.CssClass = "cssTxtEnabled"; }

            if (rTxtPorcentaje.Text.Trim() == "")
            {
                rTxtPorcentaje.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtPorcentaje.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvImpuestos.SelectedItems.Count == 0)
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
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Impuestos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, rtxtClave.Text);
        ProcBD.AgregarParametrosProcedimiento("@impDes", DbType.String, 40, ParameterDirection.Input, rTxtDescripcion.Text);
        ProcBD.AgregarParametrosProcedimiento("@impAbr", DbType.String, 8, ParameterDirection.Input, rTxtAbre.Text);
        ProcBD.AgregarParametrosProcedimiento("@impTas", DbType.Decimal, 5, ParameterDirection.Input, Convert.ToDecimal(rTxtPorcentaje.Text));

        if (rCboSatImpuestos.SelectedIndex != -1)
        {
            ProcBD.AgregarParametrosProcedimiento("@satImpuCve", DbType.String, 3, ParameterDirection.Input, rCboSatImpuestos.SelectedValue);
        }

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            ShowAlert(sEjecEstatus, sEjecMSG);
            InicioPagina();
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


            foreach (GridDataItem i in rGdvImpuestos.SelectedItems)
            {

                var dataItem = rGdvImpuestos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string impCve = dataItem.GetDataKeyValue("impCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Impuestos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 8, ParameterDirection.Input, impCve);
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



    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Impuestos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, "");

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvImpuestos, ds);
    }


    private void ControlesAccion()
    {
        
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvImpuestos.AllowMultiRowSelection = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtAbre.CssClass = "cssTxtEnabled";
        rTxtPorcentaje.CssClass = "cssTxtEnabled";
        rCboSatImpuestos.CssClass = "cssTxtEnabled";

        rtxtClave.Enabled = false;
        rTxtDescripcion.Enabled = false;
        rTxtAbre.Enabled = false;
        rTxtPorcentaje.Enabled = false;
        rCboSatImpuestos.Enabled = false;

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
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtAbre.Enabled = false;
            rTxtPorcentaje.Enabled = false;
            rCboSatImpuestos.Enabled = false;

            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtAbre.Text = "";
            rTxtPorcentaje.Text = "";
            rCboSatImpuestos.ClearSelection();
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
                this.rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvImpuestos.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = true;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtAbre.Enabled = true;
                rTxtPorcentaje.Enabled = true;
                rCboSatImpuestos.Enabled = true;

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtAbre.Text = "";
                rTxtPorcentaje.Text ="";
                rCboSatImpuestos.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvImpuestos.AllowMultiRowSelection = false;

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtAbre.Enabled = true;
                rTxtPorcentaje.Enabled = true;
                rCboSatImpuestos.Enabled = true;

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
                rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvImpuestos.AllowMultiRowSelection = true;
                rGdvImpuestos.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = false;
                this.rTxtAbre.Enabled = false;
                rTxtPorcentaje.Enabled = false;
                rCboSatImpuestos.Enabled = false;

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtAbre.Text = "";
                rTxtPorcentaje.Text = "";
                rCboSatImpuestos.ClearSelection();
            }
        }


        if (Result == false)
        {
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtAbre.Enabled = false;
            rTxtPorcentaje.Enabled = false;
            rCboSatImpuestos.Enabled = false;

            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtAbre.Text = "";
            rTxtPorcentaje.Text = "";
            rCboSatImpuestos.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvImpuestos.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvImpuestos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvImpuestos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvImpuestos.AllowMultiRowSelection = true;
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rtxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtAbre.Text = "";
            rTxtPorcentaje.Text = "";
            rCboSatImpuestos.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvImpuestos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvImpuestos.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rtxtClave.CssClass = "cssTxtEnabled";
            rTxtDescripcion.CssClass = "cssTxtEnabled";
            rTxtAbre.CssClass = "cssTxtEnabled";
            rTxtPorcentaje.CssClass = "cssTxtEnabled";

            rtxtClave.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtAbre.Enabled = false;
            rTxtPorcentaje.Enabled = false;
            rCboSatImpuestos.Enabled = false;

            rtxtClave.Text = "";
            rTxtDescripcion.Text = "";
            rTxtAbre.Text = "";
            rTxtPorcentaje.Text = "";
            rCboSatImpuestos.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }




    #endregion


    private void RCboSatMonedas()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SATImpuesto";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboSatImpuestos, ds, "satImpuCve", "satImpuDes", true, false);
        ((Literal)rCboSatImpuestos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSatImpuestos.Items.Count);

    }
}