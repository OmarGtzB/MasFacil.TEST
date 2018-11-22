using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;


public partial class SAT_CatalogoMonedas : System.Web.UI.Page
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
    private string Pag_sConexionAyu;
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

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    protected void rGdv_CatMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdv_CatMoneda.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string val = dataItem["satMonCve"].Text;
            string val1 = dataItem["satMonDes"].Text;
            int valInt = Convert.ToInt32(dataItem["satMonDec"].Text);
            decimal valInt1 = Convert.ToDecimal(dataItem["satMonPorcVari"].Text);

            rTxtCve.Text = val;
            rTxtDes.Text = val1;

            rTxtDecimales.Text = valInt.ToString();
            rTxtVariacion.Text = valInt1.ToString();

        }
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

    #region FUNCIONES
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
            ProcBD.NombreProcedimiento = "sp_SATMonedas";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@satMonCve", DbType.String, 3, ParameterDirection.Input, rTxtCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@satMonDes", DbType.String, 50, ParameterDirection.Input, this.rTxtDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@satMonDec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rTxtDecimales.Text));
            ProcBD.AgregarParametrosProcedimiento("@satMonPorcVari", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rTxtVariacion.Text));

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

            foreach (GridDataItem i in rGdv_CatMoneda.SelectedItems)
            {

                var dataItem = rGdv_CatMoneda.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string satMonCve = dataItem.GetDataKeyValue("satMonCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_SATMonedas";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@satMonCve", DbType.String, 3, ParameterDirection.Input, satMonCve);


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


    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtDecimales.Text = "";
            this.rTxtVariacion.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_CatMoneda.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_CatMoneda.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
            rTxtDecimales.CssClass = "cssTxtEnabled";
            rTxtVariacion.CssClass = "cssTxtEnabled";

            rTxtCve.Enabled = false;
            rTxtDecimales.Enabled = false;
            rTxtDes.Enabled = false;
            rTxtVariacion.Enabled = false;

            rTxtCve.Text = "";
            rTxtDes.Text = "";
            rTxtDecimales.Text = "";
            rTxtVariacion.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }
    #endregion

    #region METODOS

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_CatMoneda.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_CatMoneda, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_CatMoneda, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rTxtCve.Text.Trim() == "")
            {
                rTxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rTxtDecimales.Text.Trim() == "")
            {
                rTxtDecimales.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDecimales.CssClass = "cssTxtEnabled"; }

            if (rTxtVariacion.Text.Trim() == "")
            {
                rTxtVariacion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtVariacion.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_CatMoneda.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            if (rTxtCve.Text.Trim() == "")
            {
                rTxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rTxtDecimales.Text.Trim() == "")
            {
                rTxtDecimales.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDecimales.CssClass = "cssTxtEnabled"; }

            if (rTxtVariacion.Text.Trim() == "")
            {
                rTxtVariacion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtVariacion.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_CatMoneda.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }
        return sResult;
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sConexionAyu = Convert.ToString(Session["ConexionAyu"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }

    private void InicioPagina()
    {

        LlenaGrid();
        hdfBtnAccion.Value = "";
        ControlesAccion();
        rGdv_CatMoneda.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_CatMoneda.AllowMultiRowSelection = true;
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
        ProcBD.NombreProcedimiento = "sp_SATMonedas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_CatMoneda, ds);
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
        this.rGdv_CatMoneda.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        rTxtDecimales.CssClass = "cssTxtEnabled";
        rTxtVariacion.CssClass = "cssTxtEnabled";

        this.rTxtCve.Enabled = false;
        this.rTxtDes.Enabled = false;
        this.rTxtDecimales.Enabled = false;
        this.rTxtVariacion.Enabled = false;

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
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtDecimales.Enabled = false;
            this.rTxtVariacion.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDecimales.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtVariacion.Text = "";
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
                this.rGdv_CatMoneda.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_CatMoneda.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;
                this.rTxtDecimales.Enabled = true;
                this.rTxtVariacion.Enabled = true;

                this.rTxtCve.Text = "";
                this.rTxtDecimales.Text = "";
                this.rTxtDes.Text = "";
                this.rTxtVariacion.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_CatMoneda.AllowMultiRowSelection = false;
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;
                this.rTxtDecimales.Enabled = true;
                this.rTxtVariacion.Enabled = true;
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
                rGdv_CatMoneda.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_CatMoneda.AllowMultiRowSelection = true;
                rGdv_CatMoneda.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                this.rTxtDecimales.Enabled = false;
                this.rTxtVariacion.Enabled = false;

                this.rTxtCve.Text = "";
                this.rTxtDecimales.Text = "";
                this.rTxtDes.Text = "";
                this.rTxtVariacion.Text = "";
            }
        }


        if (Result == false)
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtDecimales.Enabled = false;
            this.rTxtVariacion.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDecimales.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtVariacion.Text = "";
        }


    }
    #endregion







}       