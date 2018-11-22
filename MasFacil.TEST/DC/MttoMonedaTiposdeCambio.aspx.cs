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
public partial class MttoMonedaTiposdeCambio : System.Web.UI.Page
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
    private string Pag_MonedaSelected;
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

    protected void rGdv_TipoCambio_SelectedIndexChanged(object sender, EventArgs e)
    {
        EdicionDatos();
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        Response.Redirect(Pag_RawUrl_Return);
    }



    #endregion

    #region METODOS
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        TraeDatos();
        LlenarGrid();
 

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rTxtTCC.Text = "";
        rTxtTCM.Text = "";
        rTxtTCP.Text = "";

        rGdv_TipoCambio.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_TipoCambio.AllowMultiRowSelection = true;
        ControlesAccion();
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
        Pag_MonedaSelected = Convert.ToString(Session["Pag_MonedaSelected"]);
        Pag_RawUrl_Return = "~" + Convert.ToString(Session["RawUrl_Return"]);
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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (RadDatePickerFecha.SelectedDate == null)
            {
                RadDatePickerFecha.CssClass = "cssTxtInvalid";
                RadDatePickerFecha.CssClass = "cssTxtInvalid";
                RadDatePickerFecha.BorderWidth = Unit.Pixel(1);
                RadDatePickerFecha.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
                camposInc += 1;
            }
            else { RadDatePickerFecha.BorderColor = System.Drawing.Color.Transparent; }


            if (rTxtTCC.Text.Trim() == "" && rTxtTCP.Text.Trim() == "" && rTxtTCM.Text.Trim() == "")
            {
                if (rTxtTCC.Text.Trim() == "")
                {
                    rTxtTCC.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCC.CssClass = "cssTxtEnabled"; }




                if (rTxtTCP.Text.Trim() == "")
                {
                    rTxtTCP.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCP.CssClass = "cssTxtEnabled"; }




                if (rTxtTCM.Text.Trim() == "")
                {
                    rTxtTCM.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCM.CssClass = "cssTxtEnabled"; }
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
            //Modificar

            if (rGdv_TipoCambio.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                return sResult;
            }




            if (RadDatePickerFecha.SelectedDate == null)
            {
                RadDatePickerFecha.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RadDatePickerFecha.CssClass = "cssTxtEnabled"; }


            if (rTxtTCC.Text.Trim() == "" && rTxtTCP.Text.Trim() == "" && rTxtTCM.Text.Trim() == "")
            {
                if (rTxtTCC.Text.Trim() == "")
                {
                    rTxtTCC.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCC.CssClass = "cssTxtEnabled"; }




                if (rTxtTCP.Text.Trim() == "")
                {
                    rTxtTCP.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCP.CssClass = "cssTxtEnabled"; }




                if (rTxtTCM.Text.Trim() == "")
                {
                    rTxtTCM.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTCM.CssClass = "cssTxtEnabled"; }
            }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_TipoCambio.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
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




            foreach (GridDataItem i in rGdv_TipoCambio.SelectedItems)
            {

                var dataItem = rGdv_TipoCambio.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string monTCId = dataItem.GetDataKeyValue("monTCId").ToString();
                    try
                    {

                        DataSet ds = new DataSet();

                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MonedaTipoCambio";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Pag_MonedaSelected);
                        ProcBD.AgregarParametrosProcedimiento("@monTCId", DbType.String, 100, ParameterDirection.Input, monTCId);


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
                    LlenarGrid();
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

    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MonedaTipoCambio";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Pag_MonedaSelected);
            ProcBD.AgregarParametrosProcedimiento("@monTCFec", DbType.String, 100, ParameterDirection.Input, SelectedFecha());

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                string monTCId;
                var dataItem = rGdv_TipoCambio.SelectedItems[0] as GridDataItem;
                monTCId = dataItem["monTCId"].Text;
                ProcBD.AgregarParametrosProcedimiento("@monTCId", DbType.Int16, 0, ParameterDirection.Input, monTCId);

            }
            if (rTxtTCC.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monTCC", DbType.Decimal, 15, ParameterDirection.Input, rTxtTCC.Text);
            }
            if (rTxtTCP.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monTCP", DbType.Decimal, 15, ParameterDirection.Input, rTxtTCP.Text);
            }
            if (rTxtTCM.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monTCM", DbType.Decimal, 15, ParameterDirection.Input, rTxtTCM.Text);
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

                    rBtnGuardar.Enabled = false;
                    rBtnCancelar.Enabled = false;
                    hdfBtnAccion.Value = "";
                    RadDatePickerFecha.Clear();
                    rTxtTCC.Enabled = false;
                    rTxtTCP.Enabled = false;
                    rTxtTCM.Enabled = false;

                    rTxtTCC.CssClass = "cssTxtEnabled";
                    rTxtTCP.CssClass = "cssTxtEnabled";
                    rTxtTCM.CssClass = "cssTxtEnabled";

                    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }


    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }



    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_TipoCambio.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_TipoCambio.MasterTableView.ClearSelectedItems();


                this.RadDatePickerFecha.Enabled = true;
                this.rTxtTCC.Enabled = true;
                this.rTxtTCP.Enabled = true;
                this.rTxtTCM.Enabled = true;

                this.RadDatePickerFecha.Clear();
                this.rTxtTCC.Text = "";
                this.rTxtTCP.Text = "";
                this.rTxtTCM.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

                this.RadDatePickerFecha.Enabled = false;
                this.rTxtTCC.Enabled = true;
                this.rTxtTCP.Enabled = true;
                this.rTxtTCM.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
                hdfBtnAccion.Value = "";
            }
            

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_TipoCambio.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_TipoCambio.AllowMultiRowSelection = true;
                rGdv_TipoCambio.MasterTableView.ClearSelectedItems();


                this.RadDatePickerFecha.Enabled = false;
                this.rTxtTCC.Enabled = false;
                this.rTxtTCP.Enabled = false;
                this.rTxtTCM.Enabled = false;

                this.RadDatePickerFecha.Clear();
                this.rTxtTCC.Text = "";
                this.rTxtTCP.Text = "";
                this.rTxtTCM.Text = "";
            }
        }


        if (Result == false)
        {
            this.RadDatePickerFecha.Enabled = false;
            this.rTxtTCC.Enabled = false;
            this.rTxtTCP.Enabled = false;
            this.rTxtTCM.Enabled = false;

            this.RadDatePickerFecha.Clear();
            this.rTxtTCC.Text = "";
            this.rTxtTCP.Text = "";
            this.rTxtTCM.Text = "";
        }
    }

    #endregion

    #region FUNCIONES

    private void TraeDatos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Monedas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Pag_MonedaSelected);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            radlb_Descripcionmoneda.Text = ds.Tables[0].Rows[0]["monDes"].ToString();
        }

    }

    private void LlenarGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MonedaTipoCambio";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Pag_MonedaSelected);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSIsFill(ds))
        {
            FnCtlsFillIn.RadGrid(ref rGdv_TipoCambio, ds);

        }

    }

    public string SelectedFecha()
    {

        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RadDatePickerFecha.SelectedDate);

        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;

    }

    public void EdicionDatos()
    {
        var dataItem = rGdv_TipoCambio.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            //    hdfBtnAccion.Value == "")
            //{
                string Fec1;
                Fec1 = dataItem["monTCFec"].Text;


                RadDatePickerFecha.SelectedDate = Convert.ToDateTime(Fec1);


                if (dataItem["monTCC"].Text != "&nbsp;")
                {
                    rTxtTCC.Text = dataItem["monTCC"].Text;
                }
                else
                {
                    rTxtTCC.Text = "";
                }

                if (dataItem["monTCP"].Text != "&nbsp;")
                {
                    rTxtTCP.Text = dataItem["monTCP"].Text;
                }
                else
                {
                    rTxtTCP.Text = "";

                }

                if (dataItem["monTCM"].Text != "&nbsp;")
                {
                    rTxtTCM.Text = dataItem["monTCM"].Text;
                }
                else
                {
                    rTxtTCM.Text = "";
                }


            //}

        }

    }






    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdv_TipoCambio
        this.rGdv_TipoCambio.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        RadDatePickerFecha.BorderColor = System.Drawing.Color.Transparent;
        rTxtTCC.CssClass = "cssTxtEnabled";
        rTxtTCP.CssClass = "cssTxtEnabled";
        rTxtTCM.CssClass = "cssTxtEnabled";

        this.RadDatePickerFecha.Enabled = false;
        this.rTxtTCC.Enabled = false;
        this.rTxtTCP.Enabled = false;
        this.rTxtTCM.Enabled = false;

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
            this.RadDatePickerFecha.Enabled = false;
            this.rTxtTCC.Enabled = false;
            this.rTxtTCP.Enabled = false;
            this.rTxtTCM.Enabled = false;

            this.RadDatePickerFecha.Clear();
            this.rTxtTCC.Text = "";
            this.rTxtTCP.Text = "";
            this.rTxtTCM.Text = "";
        }
    }
    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.RadDatePickerFecha.Clear();
            this.rTxtTCC.Text = "";
            this.rTxtTCP.Text = "";
            this.rTxtTCM.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_TipoCambio.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_TipoCambio.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            RadDatePickerFecha.BorderColor = System.Drawing.Color.Transparent;
            rTxtTCC.CssClass = "cssTxtInvalid";
            rTxtTCP.CssClass = "cssTxtEnabled";
            rTxtTCM.CssClass = "cssTxtEnabled";

            this.RadDatePickerFecha.Enabled = false;
            this.rTxtTCC.Enabled = false;
            this.rTxtTCP.Enabled = false;
            this.rTxtTCM.Enabled = false;

            this.RadDatePickerFecha.Clear();
            this.rTxtTCC.Text = "";
            this.rTxtTCP.Text = "";
            this.rTxtTCM.Text = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = true;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_TipoCambio.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_TipoCambio, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_TipoCambio, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }


    #endregion







}