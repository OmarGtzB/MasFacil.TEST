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
using System.Windows.Forms;

public partial class DC_MttoCtaContableEstucturaCod : System.Web.UI.Page
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

    //=====> EVENTOS CONTROLES
    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rTxtNivel.Text = dataItem["ctaContEstCodNiv"].Text;
            rTxtDescripcion.Text = dataItem["ctaContEstCodDes"].Text;
            rTxtLongitud.Text = dataItem["ctaContEstCodPosLon"].Text;
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
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {
        //EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
 
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

        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = false;
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

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        ControlesAccionGral();

        if (hdfBtnAccion.Value != "")
        {
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
        else {
            rTxtNivel.Text = "";
            rTxtDescripcion.Text = "";
            rTxtLongitud.Text = "";
            rTxtNivel.Enabled = false;
            rTxtDescripcion.Enabled = false;
            rTxtLongitud.Enabled = false;
        }



    }

    private void ControlesAccionGral()
    {
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rTxtLongitud.CssClass = "cssTxtEnabled";
        this.rTxtDescripcion.Enabled = false;
        this.rTxtLongitud.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

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

                Int64 newElement;
                newElement = rGdvInformacion.Items.Count + 1;

                rTxtDescripcion.Enabled = true;
                rTxtLongitud.Enabled = true;

                rTxtNivel.Text = newElement.ToString();
                rTxtDescripcion.Text  = "";
                rTxtLongitud.Text = "";
                          
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = false;

                rTxtDescripcion.Enabled = true;
                rTxtLongitud.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

        }


        if (Result == false)
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;
            //this.rTxtCve.Text = "";
            //this.rTxtDes.Text = "";
            //this.rTxtAbr.Text = "";
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

    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentaContableEstructuraCodigo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodNiv", DbType.Int64 , 0, ParameterDirection.Input, rTxtNivel.Text);
            ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodPosLon", DbType.Int64, 0, ParameterDirection.Input, rTxtLongitud.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
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

                   
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CuentaContableEstructuraCodigo";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodNiv", DbType.Int64, 0, ParameterDirection.Input, rTxtNivel.Text);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
                        ProcBD.AgregarParametrosProcedimiento("@ctaContEstCodPosLon", DbType.Int64, 0, ParameterDirection.Input, rTxtLongitud.Text);
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

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentaContableEstructuraCodigo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref this.rGdvInformacion, ds);
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
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

            if (rTxtNivel.Text.Trim() == "")
            {
                rTxtNivel.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNivel.CssClass = "cssTxtEnabled"; }
            

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rTxtLongitud.Text.Trim() == "")
            {
                rTxtLongitud.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtLongitud.CssClass = "cssTxtEnabled"; }


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

            if (rTxtNivel.Text.Trim() == "")
            {
                rTxtNivel.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNivel.CssClass = "cssTxtEnabled"; }


            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rTxtLongitud.Text.Trim() == "")
            {
                rTxtLongitud.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtLongitud.CssClass = "cssTxtEnabled"; }


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

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }



    #endregion


}