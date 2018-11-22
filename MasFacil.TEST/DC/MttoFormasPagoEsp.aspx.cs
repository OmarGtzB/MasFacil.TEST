//using System;

//using System.Web.UI.WebControls;
//using System.Data;
//using System.Windows.Forms;
//using Telerik.Web.UI;
//using System.IO;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Windows.Forms;

public partial class DC_MttoFormasPagoEsp : System.Web.UI.Page
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
            //Recuperar Valores de Sesion
            Valores_InicioPag();
            if (!IsPostBack)
            {
                //Iniciar Formulario
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
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        //    hdfBtnAccion.Value == "")
        //{
        //    txt_clave.Text = "";
        //    txt_descripcion.Text = "";
        //    txt_abreviatura.Text = "";
        //    txt_clave.CssClass = "cssTxtEnabled";
        //    txt_descripcion.CssClass = "cssTxtEnabled";
        //    txt_abreviatura.CssClass = "cssTxtEnabled";
        //    rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();

        //}
        ////if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        ////{
        ////    txt_clave.Text = "";
        ////    txt_descripcion.Text = "";
        ////    txt_abreviatura.Text = "";
        ////    MuestraInformaciontxt();
        ////}
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{
        //    rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();
        // }
        EjecutaAccionLimpiar();

    }


    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        hdfBtnAccion.Value = "";
        txt_clave.Text = "";
        txt_descripcion.Text = "";
        txt_abreviatura.Text = "";
    }

    protected void rGdv_FormasPagoEspecial_SelectedIndexChanged(object sender, EventArgs e)
    {
        MuestraInformaciontxt();
    }

    #endregion

    #region METODOS

    private void InicioPagina()
    {
        LlenarGrid();


        txt_clave.Enabled = false;
        txt_descripcion.Enabled = false;
        txt_abreviatura.Enabled = false;
        rBtnCancelar.Enabled = false;
        rBtnGuardar.Enabled = false;


        rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FormasPagoEspecial.AllowMultiRowSelection = false;
        rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        txt_clave.CssClass = "cssTxtEnabled";
        txt_descripcion.CssClass = "cssTxtEnabled";
        txt_abreviatura.CssClass = "cssTxtEnabled";
        hdfBtnAccion.Value = "";

        rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;
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
    }

    //private void ControlesAccion()
    //{
    //    //===> CONTROLES GENERAL

    //    txt_clave.CssClass = "cssTxtEnabled";
    //    txt_descripcion.CssClass = "cssTxtEnabled";
    //    txt_abreviatura.CssClass = "cssTxtEnabled";

    //    txt_clave.Text = "";
    //    txt_descripcion.Text = "";
    //    txt_abreviatura.Text = "";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    //===> CONTROLES POR ACCION

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();
    //        rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = false;
    //        txt_clave.Enabled = true;
    //        txt_abreviatura.Enabled = true;
    //        txt_descripcion.Enabled = true;

    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {

    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();
    //        rGdv_FormasPagoEspecial.AllowMultiRowSelection = false;
    //        rGdv_FormasPagoEspecial.Enabled = true;
    //        rGdv_FormasPagoEspecial.MasterTableView.Enabled = true;
    //        rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;


    //    }

    //    //Eliminar
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";

    //        rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;
    //        rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();

    //        txt_clave.Enabled = false;
    //        txt_abreviatura.Enabled = false;
    //        txt_descripcion.Enabled = false;

    //        rBtnCancelar.Enabled = true;
    //        rBtnGuardar.Enabled = true;

    //    }

    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        //this.RadDatePickerFecha.Clear();
    //        //this.rTxtTCC.Text = "";
    //        //this.rTxtTCP.Text = "";
    //        //this.rTxtTCP.Text = "";

    //        //this.RadDatePickerFecha.Enabled = false;
    //        //this.rTxtTCC.Enabled = false;
    //        //this.rTxtTCP.Enabled = false;
    //        //this.rTxtTCP.Enabled = false;



    //    }
    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //   )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }

    //}
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

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (txt_clave.Text.Trim() == "")
            {
                txt_clave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_clave.CssClass = "cssTxtEnabled"; }


            if (txt_abreviatura.Text.Trim() == "")
            {
                txt_abreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_abreviatura.CssClass = "cssTxtEnabled"; }

            if (txt_descripcion.Text.Trim() == "")
            {
                txt_descripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_descripcion.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdv_FormasPagoEspecial.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (txt_clave.Text.Trim() == "")
            {
                txt_clave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_clave.CssClass = "cssTxtEnabled"; }


            if (txt_abreviatura.Text.Trim() == "")
            {
                txt_abreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_abreviatura.CssClass = "cssTxtEnabled"; }

            if (txt_descripcion.Text.Trim() == "")
            {
                txt_descripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { txt_descripcion.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }



        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_FormasPagoEspecial.SelectedItems.Count == 0)
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

    #endregion

    #region FUNCIONES

    private void LlenarGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoFormasPagoEspecial";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdv_FormasPagoEspecial, ds);

    }

    private void EjecutaSpAcciones()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MttoFormasPagoEspecial";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@forPagEspCve", DbType.String, 10, ParameterDirection.Input, txt_clave.Text.Trim());

            ProcBD.AgregarParametrosProcedimiento("@forPagEspDes", DbType.String, 100, ParameterDirection.Input, txt_descripcion.Text.Trim());

            ProcBD.AgregarParametrosProcedimiento("@forPagEspAbr", DbType.String, 100, ParameterDirection.Input, txt_abreviatura.Text.Trim());



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

                    txt_clave.Enabled = false;
                    txt_abreviatura.Enabled = false;
                    txt_descripcion.Enabled = false;

                    txt_clave.Text = "";
                    txt_abreviatura.Text = "";
                    txt_descripcion.Text = "";

                    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                    LlenarGrid();
                    rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;


                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }


    }

    public void MuestraInformaciontxt()
    {
        var dataItem = rGdv_FormasPagoEspecial.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            ////if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            ////    hdfBtnAccion.Value == "")
            ////{

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                //{
                //    txt_clave.Enabled = false;
                //    txt_descripcion.Enabled = true;
                //    txt_abreviatura.Enabled = true;
                //}

                txt_clave.Text = dataItem["forPagEspCve"].Text;
                txt_descripcion.Text = dataItem["forPagEspDes"].Text;
                txt_abreviatura.Text = dataItem["forPagEspAbr"].Text;


            //}
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


            foreach (GridDataItem i in rGdv_FormasPagoEspecial.SelectedItems)
            {

                var dataItem = rGdv_FormasPagoEspecial.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string forPagEspCve = dataItem.GetDataKeyValue("forPagEspCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MttoFormasPagoEspecial";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@forPagEspCve", DbType.String, 10, ParameterDirection.Input, forPagEspCve);
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
                    LlenarGrid();

                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                    LlenarGrid();

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

        //===> CONTROLES GENERAL txt_descripcion
        this.rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        txt_clave.CssClass = "cssTxtEnabled";
        txt_descripcion.CssClass = "cssTxtEnabled";
        txt_abreviatura.CssClass = "cssTxtEnabled";

        this.txt_clave.Enabled = false;
        this.txt_descripcion.Enabled = false;
        this.txt_abreviatura.Enabled = false;

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
            this.txt_clave.Enabled = false;
            this.txt_descripcion.Enabled = false;
            this.txt_abreviatura.Enabled = false;
            this.txt_clave.Text = "";
            this.txt_descripcion.Text = "";
            this.txt_abreviatura.Text = "";
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
                this.rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();

                this.txt_clave.Enabled = true;
                this.txt_descripcion.Enabled = true;
                this.txt_abreviatura.Enabled = true;

                this.txt_clave.Text = "";
                this.txt_descripcion.Text = "";
                this.txt_abreviatura.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_FormasPagoEspecial.AllowMultiRowSelection = false;
                this.txt_clave.Enabled = false;
                this.txt_descripcion.Enabled = true;
                this.txt_abreviatura.Enabled = true;
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
                rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;
                rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();
                this.txt_clave.Enabled = false;
                this.txt_descripcion.Enabled = false;
                this.txt_abreviatura.Enabled = false;
                this.txt_clave.Text = "";
                this.txt_descripcion.Text = "";
                this.txt_abreviatura.Text = "";

            }
        }


        if (Result == false)
        {
            this.txt_clave.Enabled = false;
            this.txt_descripcion.Enabled = false;
            this.txt_abreviatura.Enabled = false;
            this.txt_clave.Text = "";
            this.txt_descripcion.Text = "";
            this.txt_abreviatura.Text = "";
            hdfBtnAccion.Value = "";

            rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_FormasPagoEspecial.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FormasPagoEspecial, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_FormasPagoEspecial, GvVAS, ref sMSGTip, ref sResult) == false)
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
            this.txt_clave.Text = "";
            this.txt_descripcion.Text = "";
            this.txt_abreviatura.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_FormasPagoEspecial.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            txt_clave.CssClass = "cssTxtEnabled";
            txt_descripcion.CssClass = "cssTxtEnabled";
            txt_abreviatura.CssClass = "cssTxtEnabled";

            txt_clave.Enabled = false;
            txt_descripcion.Enabled = false;
            txt_abreviatura.Enabled = false;

            txt_clave.Text = "";
            txt_descripcion.Text = "";
            txt_abreviatura.Text = "";
            hdfBtnAccion.Value = "";

            rGdv_FormasPagoEspecial.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_FormasPagoEspecial.AllowMultiRowSelection = true;
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