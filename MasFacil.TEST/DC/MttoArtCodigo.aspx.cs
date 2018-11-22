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
using System.Data.Entity;
using System.Windows.Forms;

public partial class DC_MttoArtCodigo : System.Web.UI.Page
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

    private int numArticulos;

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
    protected void rGdv_Almacenes_SelectedIndexChanged(object sender, EventArgs e)
    {
        DatosGrid();
    }

    public void DatosGrid() {
        var dataItem = rGdv_Almacenes.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {


            rTxtEstCodElem.Text = dataItem["artEstCodElem"].Text;
            rTxtEstCodDes.Text = dataItem["artEstCodDes"].Text;
            rTxtEstCodLong.Text = dataItem["artEstCodLong"].Text;


            if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
            {

                this.rTxtEstCodLong.Enabled = true;
                this.rTxtEstCodDes.Enabled = true;

                this.rCboAgrupaciones.Enabled = true;
                this.rCboTiposCodigos.Enabled = true;
            }

            foreach (RadComboBoxItem item in rCboTiposCodigos.Items)
            {
                if (item.Text == dataItem["desTip"].Text)
                {
                    rCboTiposCodigos.SelectedIndex = item.Index;
                }
            }


            if (rCboTiposCodigos.SelectedIndex == 0 || rCboTiposCodigos.SelectedIndex == 1)
            {
                if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    rCboAgrupaciones.Enabled = false;
                }

            }
            else if (rCboTiposCodigos.SelectedIndex == 2 || rCboTiposCodigos.SelectedIndex == 3)
            {
                if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    rCboAgrupaciones.Enabled = true;
                }

                foreach (RadComboBoxItem item in rCboAgrupaciones.Items)
                {
                    if (item.Text == dataItem["agrDes"].Text)
                    {
                        rCboAgrupaciones.SelectedIndex = item.Index;
                    }
                }

            }



            if (chkLastSec(Convert.ToInt32(dataItem["artEstCodElem"].Text)))
            {
                if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    rTxtEstCodLong.Enabled = true;
                }

            }
            else
            {
                if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    rTxtEstCodLong.Enabled = false;
                }
            }

        }

    }


    private bool chkLastSec(int pSec)
    {
        bool response = false ;


        if (pSec == rGdv_Almacenes.Items.Count)
        {
            response = true;
        }

        return response;        
    }


    //====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rCboTiposCodigos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //rTxtEstCodDes.Text = rCboTiposCodigos.SelectedIndex.ToString();
        if (rCboTiposCodigos.SelectedIndex == 0 || rCboTiposCodigos.SelectedIndex == 1)
        {
            rCboAgrupaciones.ClearSelection();
            rCboAgrupaciones.Enabled = false;
        }
        else if (rCboTiposCodigos.SelectedIndex == 2 || rCboTiposCodigos.SelectedIndex == 3)
        {
            rCboAgrupaciones.Enabled = true;
        }

        //rTxtEstCodLong.Text = rGdv_Almacenes.Items[rGdv_Almacenes.Items.Count - 1].Cells[7].Text;

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
        setCboTipos();
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();

        //this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
        //for (int i = 0; i < rGdv_Almacenes.Items.Count; i++)
        //{
        //    this.rGdv_Almacenes.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        //}
        //for (int i = 0; i < rGdv_Almacenes.Items.Count - 1; i++)
        //{
        //    this.rGdv_Almacenes.Items[i].SelectableMode = GridItemSelectableMode.None;
        //}

        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        //InicioPagina();
        //cleanUi();
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

    private void cleanUi()
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rTxtEstCodDes.Text = "";
            rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();
            rCboAgrupaciones.Enabled = false;    
        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rTxtEstCodDes.Text = "";
            rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();
            rCboAgrupaciones.Enabled = false;

            for (int i = 0; i < rGdv_Almacenes.Items.Count; i++)
            {
                this.rGdv_Almacenes.Items[i].Selected = false;
            }

        }
        
    }


    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();

        //Imagenes default botones
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        //Comprobar uso de estructura en articulos.  
        chkArticulos();
        LlenaGridAlmacenes();
        
        ControlesAccion();

        LlenaComboTipos();

        LlenaComboAgrupaciones();
        
        setCboTipos();

        chkStateEnable();
        
        rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
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

    private bool chkClvUnica()
    {
        string tipElement;
        int lastIndex;

        lastIndex = rGdv_Almacenes.Items.Count;
        lastIndex -= 1;

        if (rGdv_Almacenes.Items.Count > 0)
        {
            tipElement = rGdv_Almacenes.Items[lastIndex].Cells[4].Text;
            //MessageBox.Show(tipElement);
        }
        else
        {
            tipElement = "";
        }
        

        if (tipElement == "Arbitrario" | tipElement == "Unico")
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void chkArticulos()
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Articulos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            
            numArticulos = ds.Tables[0].Rows.Count;
            
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
            throw;
        }


    }

    private string chkValParmetros()
    {

        try
        {

            DataSet ds = new DataSet();
            
            string parmValInt;

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_parametros";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            
            parmValInt = ds.Tables[0].Rows[0]["parmValInt"].ToString();

            return parmValInt;

        }
        catch (Exception ex)
        {
            
            return ex.ToString();
            throw;
        }


    }


    private void LlenaGridAlmacenes()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloEstructuraCodigo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Almacenes, ds);
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
                rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
       
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }



    }

    private void chkStateEnable()
    {

        if (numArticulos >= 0 | chkClvUnica() == true)
        {

            rBtnNuevo.Enabled = false;
            rBtnModificar.Enabled = false;
            rBtnEliminar.Enabled = false;
            rBtnLimpiar.Enabled = true;

            this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = false;
            this.rTxtEstCodElem.Enabled = false;
            this.rTxtEstCodLong.Enabled = false;
            this.rTxtEstCodDes.Enabled = false;
            this.rCboAgrupaciones.Enabled = false;
            this.rCboTiposCodigos.Enabled = false;
            
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;

            if (chkClvUnica() == true & numArticulos < 1)
            {
                rBtnModificar.Enabled = true;
                rBtnEliminar.Enabled = true;
                
            }
            else if (chkClvUnica() == false & numArticulos < 1)
            {
                rBtnNuevo.Enabled = true;
                rBtnModificar.Enabled = true;
                rBtnEliminar.Enabled = true;
            }

        }

    }

    private void EjecutaSpAcciones()
    {
        Int32 finLong = 0;
        Int32 iniLong = 0;

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rGdv_Almacenes.Items.Count == 0)
            {
                iniLong = 1;
            }
            else
            {
                iniLong = Int32.Parse(rGdv_Almacenes.Items[rGdv_Almacenes.Items.Count - 1].Cells[7].Text) + 1;
            }
        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdv_Almacenes.Items.Count < 2)
            {
                iniLong = 1;
            }
            else
            {
                iniLong = Int32.Parse(rGdv_Almacenes.Items[rGdv_Almacenes.Items.Count - 2].Cells[7].Text) + 1;
            }
        }

        
        finLong = iniLong + (Int32.Parse(rTxtEstCodLong.Text)-1);

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ArticuloEstructuraCodigo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@estCodElem", DbType.Int64, 0, ParameterDirection.Input, rTxtEstCodElem.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@estCodDes", DbType.String, 50, ParameterDirection.Input, rTxtEstCodDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@estCodTip", DbType.Int64, 0, ParameterDirection.Input, rCboTiposCodigos.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@estCodLong", DbType.Int64, 0, ParameterDirection.Input, rTxtEstCodLong.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@estCodIni", DbType.Int64, 0, ParameterDirection.Input, iniLong);
            ProcBD.AgregarParametrosProcedimiento("@estCodFin", DbType.Int64, 0, ParameterDirection.Input, finLong);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rCboAgrupaciones.SelectedValue);
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




            foreach (GridDataItem i in rGdv_Almacenes.SelectedItems)
            {

                var dataItem = rGdv_Almacenes.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string artEstCodElem = dataItem["artEstCodElem"].Text;
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ArticuloEstructuraCodigo";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@estCodElem", DbType.String, 3, ParameterDirection.Input, artEstCodElem);
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
                    //LlenaGridAlmacenes();
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
                else
                {
                    //LlenaGridAlmacenes();
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


    private void LlenaComboTipos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_selTiposCodigosEstructura";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboTiposCodigos, ds, "listPreValInt", "listPreValDes", true, true);
        
        ((Literal)rCboTiposCodigos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTiposCodigos.Items.Count);
        
        //setCboTipos();

    }

    private void setCboTipos()
    {

        int numElementos;

        numElementos = rGdv_Almacenes.Items.Count;

        //MessageBox.Show(numElementos.ToString());

        if (numElementos <= 0 || (numElementos == 1 && hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()))
        {
            //solo independinte y arbitrario
            rCboTiposCodigos.Items[0].Enabled = false;
            rCboTiposCodigos.Items[1].Enabled = true;
            rCboTiposCodigos.Items[2].Enabled = true;
            rCboTiposCodigos.Items[3].Enabled = false;
            
        }
        else if (numElementos >= 1)
        { 
            //solo independinte y arbitrario
            rCboTiposCodigos.Items[0].Enabled = true;
            rCboTiposCodigos.Items[1].Enabled = false;
            rCboTiposCodigos.Items[2].Enabled = true;
            rCboTiposCodigos.Items[3].Enabled = true;
        }
        
    }

    private void LlenaComboAgrupaciones()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupaciones, ds, "agrCve", "agrDes", true, true);

        ((Literal)rCboAgrupaciones.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgrupaciones.Items.Count);


    }



    private string validaEjecutaAccion(ref string sMSGTip)
    {
        
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


            if (rTxtEstCodElem.Text.Trim() == "")
            {
                rTxtEstCodElem.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodElem.CssClass = "cssTxtEnabled";  }


            if (rTxtEstCodDes.Text.Trim() == "")
            {
                rTxtEstCodDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodDes.CssClass = "cssTxtEnabled"; }

            if (rTxtEstCodLong.Text.Trim() == "")
            {
                rTxtEstCodLong.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodLong.CssClass = "cssTxtEnabled"; }

            
            if (rCboTiposCodigos.SelectedIndex == -1)
            {
                rCboTiposCodigos.CssClass = "cssTxtInvalid";
                rCboTiposCodigos.BorderWidth = Unit.Pixel(1);
                rCboTiposCodigos.BorderColor = System.Drawing.Color.Red;
                

                camposInc += 1;
            }
            else { rCboTiposCodigos.BorderColor = System.Drawing.Color.Red; }

            if (rCboAgrupaciones.SelectedIndex == -1 && chkValParmetros() == "1" && rCboTiposCodigos.SelectedIndex > 1)
            {
                rCboAgrupaciones.CssClass = "cssTxtInvalid";
                rCboAgrupaciones.BorderWidth = Unit.Pixel(1);
                rCboAgrupaciones.BorderColor = System.Drawing.Color.Red;
                

                camposInc += 1;
            }
            else { rCboAgrupaciones.BorderColor = System.Drawing.Color.Red; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rTxtEstCodElem.Text.Trim() == "")
            {
                rTxtEstCodElem.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodElem.CssClass = "cssTxtEnabled"; }


            if (rTxtEstCodDes.Text.Trim() == "")
            {
                rTxtEstCodDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodDes.CssClass = "cssTxtEnabled"; }

            if (rTxtEstCodLong.Text.Trim() == "")
            {
                rTxtEstCodLong.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtEstCodLong.CssClass = "cssTxtEnabled"; }


            if (rCboTiposCodigos.SelectedIndex == -1)
            {
                rCboTiposCodigos.CssClass = "cssTxtInvalid";
                rCboTiposCodigos.BorderWidth = Unit.Pixel(1);
                rCboTiposCodigos.BorderColor = System.Drawing.Color.Red;
                rCboTiposCodigos.Focus();

                camposInc += 1;
            }
            else { rCboTiposCodigos.BorderColor = System.Drawing.Color.Red; }

            if (rCboAgrupaciones.SelectedIndex == -1 && chkValParmetros() == "1" && rCboTiposCodigos.SelectedIndex > 1)
            {
                rCboAgrupaciones.CssClass = "cssTxtInvalid";
                rCboAgrupaciones.BorderWidth = Unit.Pixel(1);
                rCboAgrupaciones.BorderColor = System.Drawing.Color.Red;
                rCboAgrupaciones.Focus();

                camposInc += 1;
            }
            else { rCboAgrupaciones.BorderColor = System.Drawing.Color.Red; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Almacenes.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            return sResult;
        }




        return sResult;
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rTxtEstCodElem
        this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtEstCodElem.CssClass = "cssTxtEnabled";
        rTxtEstCodDes.CssClass = "cssTxtEnabled";
        rTxtEstCodLong.CssClass = "cssTxtEnabled";
        rCboTiposCodigos.BorderColor = System.Drawing.Color.Transparent;
        rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;

        this.rTxtEstCodElem.Enabled = false;
        this.rTxtEstCodDes.Enabled = false;
        this.rTxtEstCodLong.Enabled = false;
        rCboTiposCodigos.Enabled = false;
        // rCboAgrupaciones.Enabled = false;

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
            this.rTxtEstCodElem.Enabled = false;
            this.rTxtEstCodDes.Enabled = false;
            this.rTxtEstCodLong.Enabled = false;
            rCboTiposCodigos.Enabled = false;
            rCboAgrupaciones.Enabled = false;

            this.rTxtEstCodElem.Text = "";
            this.rTxtEstCodDes.Text = "";
            this.rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();
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
                this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Almacenes.MasterTableView.ClearSelectedItems();

                Int64 newElement;
                newElement = rGdv_Almacenes.Items.Count + 1;
                this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = false;
                //this.rTxtEstCodElem.Enabled = true;
                this.rTxtEstCodElem.Text = newElement.ToString();

                //this.rTxtEstCodElem.Enabled = true;
                this.rTxtEstCodDes.Enabled = true;
                this.rTxtEstCodLong.Enabled = true;
                rCboTiposCodigos.Enabled = true;
                //rCboAgrupaciones.Enabled = true;

                //this.rTxtEstCodElem.Text = "";
                this.rTxtEstCodDes.Text = "";
                this.rTxtEstCodLong.Text = "";
                rCboTiposCodigos.ClearSelection();
                rCboAgrupaciones.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

                this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
                for (int i = 0; i < rGdv_Almacenes.Items.Count; i++)
                {
                    this.rGdv_Almacenes.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }

                //this.rTxtEstCodElem.Enabled = false;
                //this.rTxtEstCodDes.Enabled = false;
                //this.rTxtEstCodLong.Enabled = false;
                //rCboTiposCodigos.Enabled = false;
                //rCboAgrupaciones.Enabled = false;

                //var dataItem = rGdv_Almacenes.SelectedItems[0] as GridDataItem;
                //if (dataItem != null) {


                //            foreach (RadComboBoxItem item in rCboTiposCodigos.Items)
                //        {
                //            if (item.Text == dataItem["desTip"].Text)
                //            {
                //                rCboTiposCodigos.SelectedIndex = item.Index;
                //            }
                //        }


                //        if (rCboTiposCodigos.SelectedIndex == 0 || rCboTiposCodigos.SelectedIndex == 1)
                //        {
                //            rCboAgrupaciones.Enabled = false;
                //        }
                //        else if (rCboTiposCodigos.SelectedIndex == 2 || rCboTiposCodigos.SelectedIndex == 3)
                //        {
                //            rCboAgrupaciones.Enabled = true;

                //            foreach (RadComboBoxItem item in rCboAgrupaciones.Items)
                //            {
                //                if (item.Text == dataItem["agrDes"].Text)
                //                {
                //                    rCboAgrupaciones.SelectedIndex = item.Index;
                //                }
                //            }

                //        }



                //        if (chkLastSec(Convert.ToInt32(dataItem["artEstCodElem"].Text)))
                //        {
                //            rTxtEstCodLong.Enabled = true;
                //        }
                //        else
                //        {
                //            rTxtEstCodLong.Enabled = false;
                //        }
                //}
                DatosGrid();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                //this.rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;
                //for (int i = 0; i < rGdv_Almacenes.Items.Count; i++)
                //{
                //    this.rGdv_Almacenes.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                //}
                //for (int i = 0; i < rGdv_Almacenes.Items.Count - 1; i++)
                //{
                //    this.rGdv_Almacenes.Items[i].SelectableMode = GridItemSelectableMode.None;
                //}
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;

                rGdv_Almacenes.MasterTableView.ClearSelectedItems();
                this.rTxtEstCodElem.Enabled = false;
                this.rTxtEstCodDes.Enabled = false;
                this.rTxtEstCodLong.Enabled = false;
                rCboTiposCodigos.Enabled = false;
                rCboAgrupaciones.Enabled = false;

                this.rTxtEstCodElem.Text = "";
                this.rTxtEstCodDes.Text = "";
                this.rTxtEstCodLong.Text = "";
                rCboTiposCodigos.ClearSelection();
                rCboAgrupaciones.ClearSelection();

            }
        }


        if (Result == false)
        {
            this.rTxtEstCodElem.Enabled = false;
            this.rTxtEstCodDes.Enabled = false;
            this.rTxtEstCodLong.Enabled = false;
            rCboTiposCodigos.Enabled = false;
            rCboAgrupaciones.Enabled = false;

            this.rTxtEstCodElem.Text = "";
            this.rTxtEstCodDes.Text = "";
            this.rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();

            rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;

            hdfBtnAccion.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Almacenes.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Almacenes, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Almacenes, GvVAS, ref sMSGTip, ref sResult) == false)
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
            // this.rTxtEstCodElem.Text = "";
            this.rTxtEstCodDes.Text = "";
            this.rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();
            rCboAgrupaciones.Enabled = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_Almacenes.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtEstCodElem.CssClass = "cssTxtEnabled";
            rTxtEstCodDes.CssClass = "cssTxtEnabled";
            rTxtEstCodLong.CssClass = "cssTxtEnabled";
            rCboTiposCodigos.BorderColor = System.Drawing.Color.Transparent;
            rCboAgrupaciones.BorderColor = System.Drawing.Color.Transparent;

            rTxtEstCodElem.Enabled = false;
            rTxtEstCodDes.Enabled = false;
            rTxtEstCodLong.Enabled = false;
            rCboTiposCodigos.Enabled = false;
            rCboAgrupaciones.Enabled = false;

            rTxtEstCodElem.Text = "";
            rTxtEstCodDes.Text = "";
            rTxtEstCodLong.Text = "";
            rCboTiposCodigos.ClearSelection();
            rCboAgrupaciones.ClearSelection();

            rGdv_Almacenes.ClientSettings.Selecting.AllowRowSelect = true;

            hdfBtnAccion.Value = "";

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