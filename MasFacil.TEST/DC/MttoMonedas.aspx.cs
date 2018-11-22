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
public partial class DC_MttoMonedas : System.Web.UI.Page
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
    private string Pag_MonedaParaLog;
    private string Pag_MonedaSelected;

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
    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()||
            //    hdfBtnAccion.Value == "")
            //{

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                //{
                //    rtxtCve.Enabled = false;
                //    rTxtDes.Enabled = true;
                //    rTxtAbr.Enabled = true;
                //    rTxtSigno.Enabled = true;
                //    rTxtSiglas.Enabled = true;
                //}
                string skey = dataItem.GetDataKeyValue("monCve").ToString();
                string val = dataItem["monSig"].Text;
                string val1 = dataItem["monAbr"].Text;
                string val2 = dataItem["monSigl"].Text;
                rtxtCve.Text = dataItem["monCve"].Text;
                rTxtDes.Text = dataItem["monDes"].Text;

                if (dataItem["satMonCve"].Text != "&nbsp;")
                {
                    rCboSatMoneda.SelectedValue = dataItem["satMonCve"].Text;
                }else {
                    rCboSatMoneda.ClearSelection();
                }
                
            if (val == "&nbsp;")
                {
                    rTxtSigno.Text = "";
                }
                else
                {
                    rTxtSigno.Text = dataItem["monSig"].Text;
                }
                if (val1 == "&nbsp;")
                {
                    rTxtAbr.Text = "";
                }
                else
                {
                    rTxtAbr.Text = dataItem["monAbr"].Text;
                }
                if (val2 == "&nbsp;")
                {
                    rTxtSiglas.Text = "";
                }
                else
                {
                    rTxtSiglas.Text = dataItem["monSigl"].Text;
                }


            //}
        }

                //     MonedaPar();
                //if (dataItem["monCve"].Text != Pag_MonedaParaLog)
                //{
                //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() || hdfBtnAccion.Value == "")
                //    {
                //        rBtnMonedaCve.Visible = true;
                //        Session["Pag_MonedaSelected"] = dataItem["monCve"].Text;
                //    }

                //}
                //else
                //{
                //    //string sResult = "", sMSGTip = "";
                //    //FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1038", ref sMSGTip, ref sResult);
                //    //ShowAlert(sMSGTip, sResult);
                //}






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
        //rBtnMonedaCve.Visible = false;
        //ControlesAccion();
        EjecutaAccionLimpiar();
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
    
    protected void rBtnMonedaCve_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnMonedaCve";
        ControlesAccion();
    }

    #endregion
    
    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_MonedaSelected = Convert.ToString(Session["Pag_MonedaSelected"]);
    }

    private void InicioPagina()
    {
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        hdfBtnAccion.Value = "";
        ControlesAccion();
  
        LlenaGrid();
        MonedaPar();

        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;

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
    private void RCboSatMonedas()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SATMonedas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboSatMoneda, ds, "satMonCve", "satMonDes", true, false);
        ((Literal)rCboSatMoneda.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSatMoneda.Items.Count);

    }
    public void MonedaPar()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametros";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "TIPOCAMBIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 10, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        Pag_MonedaParaLog = ds.Tables[0].Rows[0]["parmValStr"].ToString();
        
        
    }    
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Monedas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);
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
            ProcBD.NombreProcedimiento = "sp_Monedas";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rtxtCve.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@monDes", DbType.String, 50, ParameterDirection.Input, rTxtDes.Text);
            ProcBD.AgregarParametrosProcedimiento("@monAbr", DbType.String, 20, ParameterDirection.Input, rTxtAbr.Text);

            if (rCboSatMoneda.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@satMonCve", DbType.String, 3, ParameterDirection.Input, rCboSatMoneda.SelectedValue);
            }
            
            if (this.rTxtSigno.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monSig", DbType.String, 10, ParameterDirection.Input, rTxtSigno.Text.Trim());

            }

            if (rTxtSiglas.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monSigl", DbType.String, 3, ParameterDirection.Input, rTxtSiglas.Text.Trim());
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdvInformacion.AllowMultiRowSelection = true;
                }
                
            }


        }
        //}
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

            
            foreach (GridDataItem i in rGdvInformacion.SelectedItems)
            {

                var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string monCve = dataItem.GetDataKeyValue("monCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Monedas";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, monCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                        //ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, sAlmCve);

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


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else {
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
                else {
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
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        rTxtAbr.CssClass = "cssTxtEnabled";
        rTxtSiglas.CssClass = "cssTxtEnabled";
        rCboSatMoneda.BorderColor = System.Drawing.Color.Transparent;

        this.rtxtCve.Enabled = false;
        this.rTxtDes.Enabled = false;
        this.rTxtAbr.Enabled = false;
        rTxtSiglas.Enabled = false;
        rCboSatMoneda.Enabled = false;

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
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString() &&
        hdfBtnAccion.Value != "rBtnMonedaCve"
          )
        {
            this.rtxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbr.Enabled = false;
            this.rTxtAbr.Enabled = false;
            rTxtSiglas.Enabled = false;
            rTxtSigno.Enabled = false;
            rCboSatMoneda.Enabled = false;

            this.rtxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtAbr.Text = "";
            rTxtSiglas.Text = "";
            rTxtSigno.Text = "";
            rCboSatMoneda.ClearSelection();
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
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                this.rtxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;
                this.rTxtAbr.Enabled = true;
                rTxtSiglas.Enabled = true;
                rTxtSigno.Enabled = true;
                rCboSatMoneda.Enabled = true;

                this.rtxtCve.Text = "";
                this.rTxtDes.Text = "";
                this.rTxtAbr.Text = "";
                rTxtSiglas.Text = "";
                rTxtSigno.Text = "";
                rCboSatMoneda.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                this.rtxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;
                this.rTxtAbr.Enabled = true;
                rTxtSiglas.Enabled = true;
                rTxtSigno.Enabled = true;
                rCboSatMoneda.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //TIPO DE CAMBIO MONEDA
            if (hdfBtnAccion.Value == "rBtnMonedaCve")
            {
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = false;

                this.rtxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                this.rTxtAbr.Enabled = false;
                rTxtSiglas.Enabled = false;
                rTxtSigno.Enabled = false;
                rCboSatMoneda.Enabled = false;

                var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
                MonedaPar();
                if (dataItem["monCve"].Text != Pag_MonedaParaLog)
                {
                    Session["Pag_MonedaSelected"] = dataItem["monCve"].Text;
                    Session["hdfBtnAccionMon"] = hdfBtnAccion.Value;
                    Session["RawUrl_Return"] = hdfRawUrl.Value;
                    Response.Redirect("~/DC/MttoMonedaTiposdeCambio.aspx?idM=" + Request.QueryString["idM"]);
                }
                else
                {
                    string sResult = "", sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1038", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                }

            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                this.rtxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                this.rTxtAbr.Enabled = false;
                rTxtSiglas.Enabled = false;
                rTxtSigno.Enabled = false;
                rCboSatMoneda.Enabled = false;


                this.rtxtCve.Text = "";
                this.rTxtDes.Text = "";
                this.rTxtAbr.Text = "";
                rTxtSiglas.Text = "";
                rTxtSigno.Text = "";
                rCboSatMoneda.ClearSelection();
            }
        }


        if (Result == false)
        {
            this.rtxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbr.Enabled = false;
            rTxtSiglas.Enabled = false;
            rTxtSigno.Enabled = false;
            rCboSatMoneda.Enabled = false;

            this.rtxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtAbr.Text = "";
            rTxtSiglas.Text = "";
            rTxtSigno.Text = "";
            rCboSatMoneda.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvInformacion.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //Tipo de Cambio MONEDA
        if (hdfBtnAccion.Value == "rBtnMonedaCve")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rtxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtAbr.Text = "";
            rTxtSiglas.Text = "";
            rTxtSigno.Text = "";
            rCboSatMoneda.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;

            rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rtxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
            rTxtAbr.CssClass = "cssTxtEnabled";
            rTxtSiglas.CssClass = "cssTxtEnabled";
            rTxtSigno.CssClass = "cssTxtEnabled";
            rCboSatMoneda.BorderColor = System.Drawing.Color.Transparent;

            rtxtCve.Enabled = false;
            rTxtDes.Enabled = false;
            rTxtAbr.Enabled = false;
            rTxtSiglas.Enabled = false;
            rTxtSigno.Enabled = false;
            rCboSatMoneda.Enabled = false;

            rtxtCve.Text = "";
            rTxtDes.Text = "";
            rTxtAbr.Text = "";
            rTxtSiglas.Text = "";
            rTxtSigno.Text = "";
            rCboSatMoneda.ClearSelection();

            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            hdfBtnAccion.Value = "";
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

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

            //if (this.rCboAlmacen.SelectedIndex == -1)
            //{
            //    camposInc += 1;
            //}

            if (rtxtCve.Text.Trim() == "")
            {
                rtxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }

            //if (rCboSatMoneda.SelectedValue != "")
            //{
            //    rCboSatMoneda.BorderWidth = Unit.Pixel(1);
            //    rCboSatMoneda.BorderColor = System.Drawing.Color.Red;
            //    camposInc += 1;
            //}
            //else { rCboSatMoneda.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rtxtCve.Text.Trim() == "")
            {
                rtxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rtxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rTxtAbr.Text.Trim() == "")
            {
                rTxtAbr.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbr.CssClass = "cssTxtEnabled"; }

            //if (rCboSatMoneda.SelectedIndex == -1)
            //{
            //    rCboSatMoneda.BorderWidth = Unit.Pixel(1);
            //    rCboSatMoneda.BorderColor = System.Drawing.Color.Red;
            //    camposInc += 1;
            //}
            //else { rCboSatMoneda.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }
    #endregion
    
}