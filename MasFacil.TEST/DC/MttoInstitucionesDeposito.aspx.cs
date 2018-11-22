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

public partial class DC_MttoInstitucionesDeposito : System.Web.UI.Page
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
        LlenaGrid();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        rtxtClave.Text = "";
        rTxtDescripcion.Text = "";
        rTxtRazonSocial.Text = "";
        rTxtRFC.Text = "";
        rGdvDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDeposito.AllowMultiRowSelection = true;
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
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_InstitucionesDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDeposito, ds);
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


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

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvDeposito.SelectedItems.Count == 0)
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



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvDeposito.SelectedItems.Count == 0)
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
        ProcBD.NombreProcedimiento = "sp_InstitucionesDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@insDepCve", DbType.String, 20, ParameterDirection.Input, rtxtClave.Text);
        ProcBD.AgregarParametrosProcedimiento("@insDepDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
        ProcBD.AgregarParametrosProcedimiento("@insDepRazSoc", DbType.String, 500, ParameterDirection.Input, rTxtRazonSocial.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@insDepRFC", DbType.String, 50, ParameterDirection.Input, rTxtRFC.Text.Trim());

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
        try
        {
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";


            foreach (GridDataItem i in rGdvDeposito.SelectedItems)
            {

                var dataItem = rGdvDeposito.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string insDepCve = dataItem.GetDataKeyValue("insDepCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_InstitucionesDeposito";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@insDepCve", DbType.String, 8, ParameterDirection.Input, insDepCve);
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
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    protected void rGdvDeposito_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvDeposito.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtRazonSocial.Enabled = false;

            rtxtClave.Text = dataItem["insDepCve"].Text;
            rTxtDescripcion.Text = dataItem["insDepDes"].Text;

            if (dataItem["insDepRazSoc"].Text != "&nbsp;")
            {
                rTxtRazonSocial.Text = dataItem["insDepRazSoc"].Text;
            }
            else
            {
                rTxtRazonSocial.Text = "";
            }
            if (dataItem["insDepRFC"].Text != "&nbsp;")
            {
                rTxtRFC.Text = dataItem["insDepRFC"].Text;
            }
            else
            {
                rTxtRFC .Text = "";
            }

        }
    }
    #endregion

    #region Funciones
    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvDeposito.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rtxtClave.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtRazonSocial.CssClass = "cssTxtEnabled";
        rTxtRFC.CssClass = "cssTxtEnabled";
        this.rtxtClave.Enabled = false;
        this.rTxtDescripcion.Enabled = false;
        this.rTxtRazonSocial.Enabled = false;
        this.rTxtRFC.Enabled = false;

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



    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDeposito.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDeposito, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDeposito, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtRazonSocial.Text = "";
        }
        else {
            InicioPagina(); 

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
                this.rGdvDeposito.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDeposito.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = true;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtRazonSocial.Enabled = true;
                rTxtRFC.Enabled = true;

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtRazonSocial.Text = "";
                rTxtRFC.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvDeposito.AllowMultiRowSelection = false;

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtRazonSocial.Enabled = true;
                rTxtRFC.Enabled = true; 

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
                rGdvDeposito.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDeposito.AllowMultiRowSelection = true;
                rGdvDeposito.MasterTableView.ClearSelectedItems();

                this.rtxtClave.Enabled = false;
                this.rTxtDescripcion.Enabled = true;
                this.rTxtRazonSocial.Enabled = true;
                rtxtClave.Enabled = true; 

                this.rtxtClave.Text = "";
                this.rTxtDescripcion.Text = "";
                this.rTxtRazonSocial.Text = "";
                rTxtRFC.Text = "";

            }
        }


        if (Result == false)
        {
            rGdvDeposito.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDeposito.AllowMultiRowSelection = true;
            this.rtxtClave.Enabled = false;
            this.rTxtDescripcion.Enabled = false;
            this.rTxtRazonSocial.Enabled = false;
            rTxtRFC.Enabled = false;

            this.rtxtClave.Text = "";
            this.rTxtDescripcion.Text = "";
            this.rTxtRazonSocial.Text = "";
            rTxtRFC.Text = "";
        }
    }
    #endregion


}