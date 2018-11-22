using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;


public partial class SAT_CatalogoUsoCFDI : System.Web.UI.Page
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
        var dataItem = rGdv_UsoCFDI.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string val = dataItem["satUsoCFDICve"].Text;
            string val1 = dataItem["satUsoCFDIDes"].Text;
            string valInt = dataItem["satUsoCFDITipVal"].Text.ToString();

            rTxtCve.Text = val;
            rTxtDes.Text = val1;

            if (valInt == "Fisica")
            {
                rbtnFisica.Checked = true;
                rbtnMoral.Checked = false;
                rbtnMoral.Checked = false;
            }
            else if (valInt == "Moral") {

                rbtnMoral.Checked = true;
                rbtnFisica.Checked = false;
                rbtnAmbas.Checked = false;
            }
            else {
                rbtnAmbas.Checked = true;
                rbtnMoral.Checked = false;
                rbtnFisica.Checked = false;
            }

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
            ProcBD.NombreProcedimiento = "sp_SATUsoCFDI";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 3, ParameterDirection.Input, rTxtCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDIDes", DbType.String, 100, ParameterDirection.Input, this.rTxtDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@satUsoCFDITip", DbType.Int32, 0, ParameterDirection.Input, obtieneValRButon());

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

    public int obtieneValRButon() {
       

        if (rbtnFisica.Checked == true)
        {
            return 1;
        }else if (rbtnMoral.Checked==true) {

            return 2;
        }else if (rbtnAmbas.Checked == true) {
            return 3;
        }

        return 0;
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

            foreach (GridDataItem i in rGdv_UsoCFDI.SelectedItems)
            {

                var dataItem = rGdv_UsoCFDI.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string satUsoCFDICve = dataItem.GetDataKeyValue("satUsoCFDICve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_SATUsoCFDI";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 3, ParameterDirection.Input, satUsoCFDICve);


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
            rbtnFisica.Checked = true;
            rbtnMoral.Checked = false;
            rbtnAmbas.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_UsoCFDI.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_UsoCFDI.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";

            rTxtCve.Enabled = false;
            rTxtDes.Enabled = false;

            rbtnFisica.Enabled = false;
            rbtnMoral.Enabled = false;
            rbtnAmbas.Enabled = false;

            rTxtCve.Text = "";
            rTxtDes.Text = "";
            rbtnFisica.Checked = true;
            rbtnMoral.Checked = false;
            rbtnAmbas.Checked = false;

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
        int GvSelectItem = rGdv_UsoCFDI.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_UsoCFDI, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_UsoCFDI, GvVAS, ref sMSGTip, ref sResult) == false)
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

            //if (rTxtDecimales.Text.Trim() == "")
            //{
            //    rTxtDecimales.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtDecimales.CssClass = "cssTxtEnabled"; }

            //if (rTxtVariacion.Text.Trim() == "")
            //{
            //    rTxtVariacion.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtVariacion.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_UsoCFDI.SelectedItems.Count == 0)
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

            //if (rTxtDecimales.Text.Trim() == "")
            //{
            //    rTxtDecimales.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtDecimales.CssClass = "cssTxtEnabled"; }

            //if (rTxtVariacion.Text.Trim() == "")
            //{
            //    rTxtVariacion.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtVariacion.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_UsoCFDI.SelectedItems.Count == 0)
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
        rGdv_UsoCFDI.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_UsoCFDI.AllowMultiRowSelection = true;
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
        ProcBD.NombreProcedimiento = "sp_SATUsoCFDI";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_UsoCFDI, ds);
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
        this.rGdv_UsoCFDI.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";

        this.rTxtCve.Enabled = false;
        this.rTxtDes.Enabled = false;
        rbtnFisica.Enabled = false;
        rbtnMoral.Enabled = false;
        rbtnAmbas.Enabled = false;

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

            rbtnFisica.Enabled = false;
            rbtnMoral.Enabled = false;
            rbtnAmbas.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            rbtnFisica.Checked = true;
            rbtnMoral.Checked = false;
            rbtnAmbas.Checked = false;

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
                this.rGdv_UsoCFDI.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_UsoCFDI.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;
                rbtnFisica.Enabled = true;
                rbtnMoral.Enabled = true;
                rbtnAmbas.Enabled = true;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";
                rbtnFisica.Checked = true;
                rbtnMoral.Checked = false;
                rbtnAmbas.Checked = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_UsoCFDI.AllowMultiRowSelection = false;
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;

                rbtnFisica.Enabled = true;
                rbtnMoral.Enabled = true;
                rbtnAmbas.Enabled = true;

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
                rGdv_UsoCFDI.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_UsoCFDI.AllowMultiRowSelection = true;
                rGdv_UsoCFDI.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                rbtnFisica.Enabled = false;
                rbtnMoral.Enabled = false;
                rbtnAmbas.Enabled = false;


                rbtnFisica.Checked = true;
                rbtnMoral.Checked = false;
                rbtnAmbas.Checked = false;
            }
        }


        if (Result == false)
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;

            rbtnFisica.Enabled = false;
            rbtnMoral.Enabled = false;
            rbtnAmbas.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            rbtnFisica.Checked = true;
            rbtnMoral.Checked = false;
            rbtnAmbas.Checked = false;
        }


    }
    #endregion
}