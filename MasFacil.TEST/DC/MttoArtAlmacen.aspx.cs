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
public partial class DC_MttoArtAlmacen : System.Web.UI.Page
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

    private string PagLoc_ArtCve;
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
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{

                string skey= dataItem.GetDataKeyValue("almCve").ToString();

                //this.rCboAlmacen.Enabled = false;
                //this.rTxtLocalizacion.Enabled = true;
                //this.rTxtMaximo.Enabled = true;
                //this.rTxtMinimo.Enabled = true;
                //this.rTxtReorden.Enabled = true;


                rCboAlmacen.SelectedValue = skey;
                if (dataItem["artAlmLoc"].Text == "&nbsp;")
                {
                    rTxtLocalizacion.Text = "";
                }else
                {
                    rTxtLocalizacion.Text = dataItem["artAlmLoc"].Text;
                }
               
                rTxtMaximo.Text = dataItem["artAlmCanMaxInv"].Text;
                rTxtMinimo.Text = dataItem["artAlmCanMinInv"].Text;
                rTxtReorden.Text = dataItem["artAlmPunReo"].Text;

            //}


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

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
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

    #endregion

    #region METODOS
    private void Valores_InicioPag()
        {
            Pag_sCompania = Convert.ToString(Session["Compania"]);
            Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
            Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

            PagLoc_ArtCve = Convert.ToString(Session["folio_Selection"]);
        }

    private void InicioPagina()
    {
        //hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        hdfBtnAccion.Value = "";

        ControlesAccion();
        LlenaDatosArticulo();
        LlenaComboAlmacenes();
        LlenaGrid();

        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;

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

    //private void ControlesAccion()
    //{

    //    //===> CONTROLES GENERAL
    //    this.rCboAlmacen.ClearSelection();
    //    rGdvInformacion.MasterTableView.ClearSelectedItems();
    //    this.rTxtLocalizacion.Text = "";
    //    this.rTxtMaximo.Text = "0";
    //    this.rTxtMinimo.Text = "0";
    //    this.rTxtReorden.Text = "0";

    //    rTxtLocalizacion.CssClass = "cssTxtEnabled";
    //    rTxtMaximo.CssClass = "cssTxtEnabled";
    //    rTxtMinimo.CssClass = "cssTxtEnabled";
    //    rTxtReorden.CssClass = "cssTxtEnabled";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    //===> CONTROLES POR ACCION
    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rCboAlmacen.Enabled = true;
    //        this.rTxtLocalizacion.Enabled = true;
    //        this.rTxtMaximo.Enabled = true;
    //        this.rTxtMinimo.Enabled = true;
    //        this.rTxtReorden.Enabled = true;
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdvInformacion.AllowMultiRowSelection = false;
    //        this.rCboAlmacen.Enabled = false;
    //        this.rTxtLocalizacion.Enabled = false;
    //        this.rTxtMaximo.Enabled = false;
    //        this.rTxtMinimo.Enabled = false;
    //        this.rTxtReorden.Enabled = false;
    //    }

    //    //ELIMIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdvInformacion.AllowMultiRowSelection = true;
    //        this.rCboAlmacen.Enabled = false;
    //        this.rTxtLocalizacion.Enabled = false;
    //        this.rTxtMaximo.Enabled = false;
    //        this.rTxtMinimo.Enabled = false;
    //        this.rTxtReorden.Enabled = false;
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rCboAlmacen.Enabled = false;
    //        this.rTxtLocalizacion.Enabled = false;
    //        this.rTxtMaximo.Enabled = false;
    //        this.rTxtMinimo.Enabled = false;
    //        this.rTxtReorden.Enabled = false;
    //    }


    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //       )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //}

    private void LlenaDatosArticulo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblClave.Text = Convert.ToString(ds.Tables[0].Rows[0][0]);
            rLblDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0][2]);
        }
        else {
            rLblClave.Text = "";
            rLblDescripcion.Text = "";
        }
    }
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);
    }
    private void LlenaComboAlmacenes() {
        DataSet ds = new DataSet();

        String sUsuCve = Convert.ToString(Session["user"]);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboAlmacen, ds, "almCve", "almDes", true, false, "");
        ((Literal)rCboAlmacen.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAlmacen.Items.Count);

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
            ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, this.rCboAlmacen.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@artAlmCanMaxInv", DbType.Decimal, 0, ParameterDirection.Input, this.rTxtMaximo.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@artAlmCanMinInv", DbType.Decimal, 20, ParameterDirection.Input, this.rTxtMinimo.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@artAlmPunReo", DbType.Decimal, 20, ParameterDirection.Input, rTxtReorden.Text.Trim());

            if (rTxtLocalizacion.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@artAlmLoc", DbType.String, 50, ParameterDirection.Input, this.rTxtLocalizacion.Text.Trim());
            }
            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);
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
 
                    string sAlmCve = dataItem.GetDataKeyValue("almCve").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
                        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, sAlmCve);
                        
                        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

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

            if (this.rCboAlmacen.SelectedIndex ==-1) {
                camposInc += 1;
            }
            

            if (rTxtMaximo.Text.Trim() == "") {
                rTxtMaximo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }else
            {
                rTxtMaximo.CssClass = "cssTxtEnabled";
            }

            if (rTxtMinimo.Text.Trim() == "")
            {
                rTxtMinimo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtMinimo.CssClass = "cssTxtEnabled"; }

            if (rTxtReorden.Text.Trim() == "")
            {
                rTxtReorden.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtReorden.CssClass = "cssTxtEnabled"; }

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

            if (this.rCboAlmacen.SelectedIndex == -1)
            {
                camposInc += 1;
            }


            if (rTxtMaximo.Text.Trim() == "")
            {
                rTxtMaximo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rTxtMinimo.Text.Trim() == "")
            {
                rTxtMinimo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtMinimo.CssClass = "cssTxtEnabled"; }

            if (rTxtReorden.Text.Trim() == "")
            {
                rTxtReorden.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtReorden.CssClass = "cssTxtEnabled"; }

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


    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtMaximo.CssClass = "cssTxtEnabled";
        rTxtMinimo.CssClass = "cssTxtEnabled";
        rTxtReorden.CssClass = "cssTxtEnabled";
        rCboAlmacen.BorderColor = System.Drawing.Color.Transparent;

        this.rCboAlmacen.Enabled = false;
        this.rTxtLocalizacion.Enabled = false;
        this.rTxtMaximo.Enabled = false;
        this.rTxtMinimo.Enabled = false;
        this.rTxtReorden.Enabled = false;

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
            this.rCboAlmacen.Enabled = false;
            this.rTxtLocalizacion.Enabled = false;
            this.rTxtMaximo.Enabled = false;
            this.rTxtMinimo.Enabled = false;
            this.rTxtReorden.Enabled = false;

            
            this.rCboAlmacen.ClearSelection();
            this.rTxtLocalizacion.Text="";
            this.rTxtMaximo.Text = "";
            this.rTxtMinimo.Text = "";
            this.rTxtReorden.Text = "";
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
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

                this.rCboAlmacen.Enabled = true;
                this.rTxtLocalizacion.Enabled = true;
                this.rTxtMaximo.Enabled = true;
                this.rTxtMinimo.Enabled = true;
                this.rTxtReorden.Enabled = true;

                this.rCboAlmacen.ClearSelection();
                this.rTxtLocalizacion.Text = "";
                this.rTxtMaximo.Text = "";
                this.rTxtMinimo.Text = "";
                this.rTxtReorden.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                this.rCboAlmacen.Enabled = false;
                this.rTxtLocalizacion.Enabled = true;
                this.rTxtMaximo.Enabled = true;
                this.rTxtMinimo.Enabled = true;
                this.rTxtReorden.Enabled = true;

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
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                
                this.rCboAlmacen.Enabled = false;
                this.rTxtLocalizacion.Enabled = false;
                this.rTxtMaximo.Enabled = false;
                this.rTxtMinimo.Enabled = false;
                this.rTxtReorden.Enabled = false;

                this.rCboAlmacen.ClearSelection();
                this.rTxtLocalizacion.Text = "";
                this.rTxtMaximo.Text = "";
                this.rTxtMinimo.Text = "";
                this.rTxtReorden.Text = "";
            }
        }


        if (Result == false)
        {
            this.rCboAlmacen.Enabled = false;
            this.rTxtLocalizacion.Enabled = false;
            this.rTxtMaximo.Enabled = false;
            this.rTxtMinimo.Enabled = false;
            this.rTxtReorden.Enabled = false;

            this.rCboAlmacen.ClearSelection();
            this.rTxtLocalizacion.Text = "";
            this.rTxtMaximo.Text = "";
            this.rTxtMinimo.Text = "";
            this.rTxtReorden.Text = "";
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
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
            this.rCboAlmacen.ClearSelection();
            this.rTxtLocalizacion.Text = "";
            this.rTxtMaximo.Text = "";
            this.rTxtMinimo.Text = "";
            this.rTxtReorden.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
            {
            
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;

                rGdvInformacion.MasterTableView.ClearSelectedItems();

                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                rTxtMaximo.CssClass = "cssTxtEnabled";
                rTxtMinimo.CssClass = "cssTxtEnabled";
                rTxtReorden.CssClass = "cssTxtEnabled";
                rCboAlmacen.BorderColor = System.Drawing.Color.Transparent;

                this.rCboAlmacen.Enabled = false;
                this.rTxtLocalizacion.Enabled = false;
                this.rTxtMaximo.Enabled = false;
                this.rTxtMinimo.Enabled = false;
                this.rTxtReorden.Enabled = false;

                this.rCboAlmacen.ClearSelection();
                this.rTxtLocalizacion.Text = "";
                this.rTxtMaximo.Text = "";
                this.rTxtMinimo.Text = "";
                this.rTxtReorden.Text = "";

                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = false;


                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }


}