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

public partial class DC_MttoArtAgrupaciones : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();


    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    private string PagLoc_ArtCve;
    private int Pag_agrTipId;

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


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        this.rGdv_ArtAgrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        this.rBtnGuardar.Enabled = true;
        this.rBtnCancelar.Enabled = true;


    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        InicioPagina();
        rCboAgrupacionesDatos.ClearSelection();

    }
    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        rCboAgrupacionesDatos.ClearSelection();

    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();

    }
    protected void rCboAgrupaciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        ds =dsAgrupacionesDatos();

        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupacionesDatos, ds, "agrDatoCve", "agrDatoDes", false, false);
    }
    protected void rCboAgrupacionesDatos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

   //dsAgrupaciones();

    }
    protected void rbtnGuardarAgrupacion_Click(object sender, EventArgs e)
    {
        EjecutaAccion();

    }
    protected void rbtnBorrarAgrupacion_Click(object sender, EventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        this.rGdv_ArtAgrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        this.rBtnGuardar.Enabled = true;
        this.rBtnCancelar.Enabled = true;

    }
    #endregion
    #region METODOS
    private void EjecutaAccion()
    {

        string msgValidacion = validaEjecutaAccion();
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
                InicioPagina();
            }

        }
        else
        {
            RadWindowManager1.RadAlert(msgValidacion, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert0.png");
        }
    }
    private void EjecutaSpAcciones()
    {
        try {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAgrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rCboAgrupaciones.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 10, ParameterDirection.Input, rCboAgrupacionesDatos.SelectedValue);
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
    private void ControlesAccion()
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
            this.rGdv_ArtAgrupaciones.ClientSettings.Selecting.AllowRowSelect = false;
            InicioPagina();
        }
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
            this.rGdv_ArtAgrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
            InicioPagina();
        }
        this.rCboAgrupaciones.Enabled = true;
        this.rCboAgrupacionesDatos.Enabled = true;
        this.rBtnGuardar.Enabled = true;
        this.rBtnCancelar.Enabled = true;
        Limpiartxt();
        //rBtnGuardar.Click += new EventHandler(this.GreetingBtn_Click);

    }
    public void Limpiartxt()
    {
        rTxagrTipId.Text = "";
        rTxtagrCve.Text = "";
        rTxtagrDatoCve.Text = "";
        rTxtciaCve.Text = "";

    }

    private void LlenaComboAgrupaciones()//BIENE DE LA TABLA AGRUPACIONES
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAgrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 0);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupaciones, ds, "agrCve", "agrDes", false, false);
    }

    //ok
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
        this.rGdv_ArtAgrupaciones.ClientSettings.Selecting.AllowRowSelect = false;
        LlenaGridArtAgrupaciones();
        LlenaComboAgrupaciones();
        rCboAgrupacionesDatos.Items.Clear();
        rCboAgrupacionesDatos.DataSource = null;
        LlenaDatosArticulo();
        dsAgrupacionesDatos();
        rCboAgrupaciones.Enabled = false;
        rCboAgrupacionesDatos.Enabled = false;
        rCboAgrupacionesDatos.ClearSelection();
        rBtnCancelar.Enabled = false;
        rBtnGuardar.Enabled = false;
        rCboAgrupacionesDatos.Text = string.Empty;

    }
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

    private void LlenaGridArtAgrupaciones()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAgrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_ArtAgrupaciones, ds);
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




            foreach (GridDataItem i in rGdv_ArtAgrupaciones.SelectedItems)
            {

                var dataItem = rGdv_ArtAgrupaciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    rTxtagrCve.Text = dataItem["agrCve"].Text;
                    rTxtagrDatoCve.Text = dataItem["agrDatoCve"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ArticuloAgrupaciones";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rTxtagrCve.Text.Trim());
                        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 10, ParameterDirection.Input, rTxtagrDatoCve.Text.Trim());
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
                    LlenaGridArtAgrupaciones();
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
                    sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    sMsgAlert = sMsgAlert + "Registros no eliminados" + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else {
                    LlenaGridArtAgrupaciones();
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
        RadWindowManager1.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion
    #region FUNCIONES
    DataSet dsAgrupacionesDatos()//LLENA AGRUPACIONES DATOS    
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAgrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rCboAgrupaciones.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    private string validaEjecutaAccion()
    {

        string sResult = "";

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_ArtAgrupaciones.SelectedItems.Count == 0)
            {
                sResult = "No se han seleccionado registros por eliminar.";
                return sResult;
            }


            return sResult;
        }




        return sResult;
    }

    #endregion

}
