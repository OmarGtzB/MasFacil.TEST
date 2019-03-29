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
using System.Globalization;


public partial class FR_RegDocumentoNuevo : System.Web.UI.Page
{

 
    #region VARIABLES

    // Gets a NumberFormatInfo associated with the en-US culture.
    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
    DateTime localDate = DateTime.Now;
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();
    
    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_sIdDocReg;
    private string Pag_sActionValue;
    private string Pag_RawUrl_Return;

    //Variables de Configuracion de Documento
    DataSet dsDocConfig = new DataSet();
    DataTable dtLayTotales = new DataTable();
    
    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    // Botones Partidas 

    //-- frmNewPartida
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        frmNewPartida.Visible = true;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = false;



        hdfBtnAccionP.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAcciconDet();
        rFchEntPart.SelectedDate = rFchDoc.SelectedDate;
        PrecioCapturable();
    }
    protected void rImgBtnAceptarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionDetalle();

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_RegistoDetalle.AllowMultiRowSelection = true;
    }
    protected void rImgBtnCancelarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        frmNewPartida.Visible = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();

    }
    
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        frmNewPartida.Visible = true;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = false;
        hdfBtnAccionP.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAcciconDet();

        PrecioCapturable();
        if (bAplicaTextil() == true) {
            rTxtPartPrec.Enabled = true;
        }
    }

    private void PrecioCapturable() {
       string  sValor = FNParam.sParametroValInt(Pag_sConexionLog, Pag_sCompania, "PRECVE", 1);
 
        if (sValor == "1")
        {
            rTxtPartPrec.Enabled = true;
        }
        else
        if (sValor == "2")
        {
            rTxtPartPrec.Enabled = false;
        }
        else if (sValor == "3")
        {
            rTxtPartPrec.Enabled = true;
        }
        else
        {
            rTxtPartPrec.Enabled = false;
        }
    }




    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        frmNewPartida.Visible = true;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = false;
        hdfBtnAccionP.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAcciconDet();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //hdfBtnAccionP.Value = "";
        EjecutaAccionLimpiar();
    }
    protected void rBtnEditDesc_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionP.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.ModificarDet).ToString();
        ControlesAcciconDet();
    }
    

    // Botones Partidas Textil
    protected void rBtnNuevoPartidaTexil_Click(object sender, ImageButtonClickEventArgs e)
    {
        txtTextilOperacion.Text = "";
        frmNewPartida.Visible = false;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = true;
        
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarRegistro.png";
        rBtnNuevoPartidaTexil.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
 
    }
    protected void rBtnNuevoPartidaTexil_Ok_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (txtTextilOperacion.Text != "")
        {
            if (RegistrosPartidaTexil() == true)
            {
                frmNewPartidaTextil.Visible = false;
                rBtnNuevoPartidaTexil.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            }
            else
            {
                ShowAlert("2", "No se encontraron Registros");
            }
        }
        else
        {
            ShowAlert("1", "Para Continuar debe Capturar el No de Operación.");
        }

    }
    private Boolean RegistrosPartidaTexil()
    {
        Boolean bResult = false;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoTextilPartidas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
	ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docTextilOper", DbType.Int64, 0, ParameterDirection.Input, txtTextilOperacion.Text.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds.Tables[0].Rows.Count > 0)
        {


            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    hdfBtnAccionP.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
                    ControlesAcciconDet();
                    rFchEntPart.SelectedDate = rFchDoc.SelectedDate;

                    Decimal iCantidad = Convert.ToDecimal(row["docTextilTotMet"].ToString());
                    rCboArticulo.SelectedValue = row["docTextilNoTel"].ToString();
                    rTxtPartCant.Text = iCantidad.ToString();
                    rTxtPartPrec.Text = PrecioArticulo_ListaPrecios(rCboArticulo.SelectedValue, iCantidad).ToString();


                    frmNewPartida.Visible = true;

                    EjecutaAccionDetalle();

                    rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdv_RegistoDetalle.AllowMultiRowSelection = true;
                }
            }
            bResult = true;

        }
        else
        {
            bResult = false;
        }
        return bResult;
    }


    // Botones Ejecuta Accion
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        String docClave;
        docClave = rCboDocumento.SelectedValue.ToString();

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            //getFolDoc(docClave);
            getFolDocNuevo(docClave);
        }
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (rCboDocumento.SelectedValue != "")
        {
            cancelarExiTeo();
        }
        Response.Redirect(Pag_RawUrl_Return);
    }






    protected void rCboCliente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //Reset Grid al cambiar Cliente
        CargarTabla("");
        setTotales();
        calcTotales();
        clcDescGlbl();
        setTotales();
        calcTotales();
        clcImpuGlbl();
        setTotales();
        calcTotales();
        //*
        
        LlenaComboDirEntrega();
        if (ParListadePrecios() == "AGR" && rCboMoneda.SelectedValue != "")
        {
            rCboLstPrecios.Enabled = true;
            //LlenaComboLstPrecios(52);
            enableUI(2);
            
        }

        rCboMetodoPago.Enabled = true;
        getSatMetPag();
        
    }




    //MASH
    protected void rCboIncoterm_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string docCveIncoterm = rCboIncoterm.SelectedValue;

    }
    //MASH

   
    protected void rCboFormaPago_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string docCveFormaPago = rCboFormaPago.SelectedValue;

    }


    protected void rCboDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        //MASH  obtiene documento para verificar si tiene Incoterms = 1, llena y hace visible el combo 
        string docCve;
        docCve = rCboDocumento.SelectedValue;
       if (getIncoterm() == "1")
        {

            FnCtlsFillIn.RabComboBox_Incoterm(Pag_sConexionLog, Pag_sCompania, ref rCboIncoterm, true, false, "");
            rLblIncoterm.Visible = true;
            rCboIncoterm.Visible = true;
        }
        else
        {
            rCboViaEmbarque.ClearSelection();
            rLblIncoterm.Visible = false;
            rCboIncoterm.Visible = false;
        }




        //getDocConfig(e.Value);
        bAplicaTextil();

        //getFolDoc(e.Value);
        setLayGridPartidas(e.Value);

        getImpuestos(e.Value);
        getDescuentos(e.Value);

        setLayDescuentos(e.Value);

        if (rCboDocumento.SelectedValue != "")
        {
            rFchDoc.Enabled = true;
        }
        else
        {
            rFchDoc.Enabled = false;
        }

        if (rCboDocumento.SelectedValue != "" && rFchDoc.Text != "")
        {
            enableUI(2);
        }

        string  ParLista = "";
        ParLista = ParListadePrecios();
        
        if (ParLista == "DOC")
        {
            ParListadePreciosPorDocumento();
        }
        else if (ParLista == "AGR")
        {
            rCboLstPrecios.Enabled = true;
            enableUI(3);
        }
        else if (ParLista == "DEFAULT")
        {
            //YA SEA EN DOCUMENTOS O EN MONEDA

            if (rCboLstPrecios.SelectedValue != "")
            {
                pnlBtnsAcciones.Enabled = true;
                pnlBtnsAcciones.Visible = true;

            }
        }


        if (rCboLstPrecios.SelectedValue != "" && rCboCliente.SelectedValue != "" && rCboMoneda.SelectedValue != "")
        {
            enableUI(4);
            pnlBtnsAcciones.Enabled = true;

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

            frmNewPartida.Visible = false;
        }
        
        loadLayTotales();

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {
            reSetTotales();
        }

        getOpcCFDI();

        if (getOpcCFDI() == "1")
        {
            rLblMetodoPago.Visible = true;
            rCboMetodoPago.Visible = true;
        }
        else
        {
            rLblMetodoPago.Visible = false;
            rCboMetodoPago.Visible = false;
        }


        sitPer();

        cargaDl("");


    }
    protected void rGdv_RegistoDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {
    
        var dataItem = rGdv_RegistoDetalle.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            rCboArticulo.SelectedValue = dataItem["artCve"].Text;

            rTxtCustomDescArt.Text = dataItem["artDes"].Text;

            LlenaComboAlmacen();

            rCboAlmacen.SelectedValue = dataItem["almCve"].Text;

                if (dataItem["docRegPartCant"].Text != "&nbsp;")
                {
                    rTxtPartCant.Text = dataItem["docRegPartCant"].Text;
                }
                else
                {
                    rTxtPartCant.Text = "1";
                }

                if (dataItem["docRegPartFecEnt"].Text != "&nbsp;")
                {
                    rFchEntPart.DisplayText = dataItem["docRegPartFecEnt"].Text;
                    rFchEntPart.SelectedDate = Convert.ToDateTime(dataItem["docRegPartFecEnt"].Text);
                }
                else
                {
                    rFchEntPart.SelectedDate = null;
                }

                if (dataItem["docRegPartPrec"].Text != "&nbsp;")
                {
                    rTxtPartPrec.Text = dataItem["docRegPartPrec"].Text.ToString().Remove(0, 1);
                }
                else
                {
                    rTxtPartPrec.Text = "0.00";
                }




            /////////////////////////////////////////////////////////////////////////////////////////////

            ///LOTE///
            if (dataItem["docRegPartLote"].Text != "&nbsp;")
            {
                rTxtPartLote.Text = dataItem["docRegPartLote"].Text.ToString();
            }
            else
            {
                rTxtPartLote.Text = "";
            }

            ///SERIE///
            if (dataItem["docRegPartSerie"].Text != "&nbsp;")
            {
                rTxtPartSerie.Text = dataItem["docRegPartSerie"].Text.ToString();
            }
            else
            {
                rTxtPartSerie.Text = "";
            }

            /////////////////////////////////////////////////////////////////////////////////////////////



            //rCboAlmacen.Enabled = true;
            //rFchEntPart.Enabled = true;
            //rTxtPartCant.Enabled = true;
            //rTxtPartPrec.Enabled = true;

            //}
            frmNewPartida.Visible = true;

            //if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ModificarDet).ToString())
            //{
            //    frmNewPartida.Visible = false;
            //    frmCustomDescArt.Visible = true;
            //}

        }

    }



    protected void rTxtFolio_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tipFolio = "", valFolio = "";

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                tipFolio = ds.Tables[0].Rows[0]["docFolTip"].ToString();
                valFolio = ds.Tables[0].Rows[0]["folVal"].ToString();

                rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), rTxtFolio.Text);

                if (tipFolio == "1")
                {
                    rTxtFolio.Enabled = true;
                }
                else if (tipFolio == "2")
                {
                    rTxtFolio.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {


        }
    }

    protected void rCboLstPrecios_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //Nuevo al cambiar listas
        CargarTabla("");
        setTotales();
        calcTotales();
        clcDescGlbl();
        setTotales();
        calcTotales();
        clcImpuGlbl();
        setTotales();
        calcTotales();
        //*********************************************
        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();


        if (valVigenciaLst(e.Value))
        {
            fillCboArticulos();
            pnlBtnsAcciones.Enabled = true;
            enableUI(4);

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
            hdfBtnAccionP.Value = "";
        }
        else
        {
            //ShowAlert("3", "Lista de Precios No Vigente");
            string sMSGTip = "", sResult= "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1028", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);

            rCboLstPrecios.ClearSelection();
            pnlBtnsAcciones.Enabled = false;

        }

    }
    
    protected void rCboArticulo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        if (ValidaCodigoAlt() == true)
        {
            LlenaComboAlmacen();


            if (rCboLstPrecios.SelectedValue != "")
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ListaPrecios";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);

                if (rTxtPartCant.Text != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@artCant", DbType.Int64, 0, ParameterDirection.Input, rTxtPartCant.Text);
                }
                else
                {
                    rTxtPartCant.Text = "1";
                    ProcBD.AgregarParametrosProcedimiento("@artCant", DbType.Int64, 0, ParameterDirection.Input, rTxtPartCant.Text);
                }


                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        DataSet ds1 = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD1.NombreProcedimiento = "sp_ListaPrecios";
                        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
                        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD1.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
                        ProcBD1.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
                        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                        rTxtPartPrec.Text = ds1.Tables[0].Rows[0]["lisPrecio"].ToString();

                    }
                    else
                    {
                        rTxtPartPrec.Text = ds.Tables[0].Rows[0]["lisPrecio"].ToString();

                    }

                }


            }


            enableUI(5);
        }
        

    }

    protected void rTxtPartCant_TextChanged(object sender, EventArgs e)
    {
        if (rCboLstPrecios.SelectedValue != "" && rTxtPartCant.Text != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@artCant", DbType.Decimal, 0, ParameterDirection.Input, rTxtPartCant.Text);
            
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataSet ds1 = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD1.NombreProcedimiento = "sp_ListaPrecios";
                    ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
                    ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD1.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
                    ProcBD1.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
                    ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                    rTxtPartPrec.Text = ds1.Tables[0].Rows[0]["lisPrecio"].ToString();

                }
                else
                {
                    rTxtPartPrec.Text = ds.Tables[0].Rows[0]["lisPrecio"].ToString();

                }
            }
            
        }
    }

    protected void rFchDoc_TextChanged(object sender, EventArgs e)
    {
        sitPer();
    }

    protected void rCboMoneda_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //*******************************
        CargarTabla("");
        setTotales();
        calcTotales();
        clcDescGlbl();
        setTotales();
        calcTotales();
        clcImpuGlbl();
        setTotales();
        calcTotales();
        //****


        enableUI(2);
        string ParLista = "";
        ParLista = ParListadePrecios();
        if (ParLista == "DOC")
        {
            //rCboLstPrecios.Enabled = true;
            ParListadePreciosPorDocumento();
        }
        else if (ParLista == "AGR")
        {
            //rCboLstPrecios.Enabled = true;
            //LlenaComboLstPrecios(52);
        }else if (ParLista == "DEFAULT") {
            //YA SEA EN DOCUMENTOS O EN MONEDA

            if (rCboLstPrecios.SelectedValue != "")
            {
                pnlBtnsAcciones.Enabled = true;
                pnlBtnsAcciones.Visible = true;

            }
        }

        //enableUi(2);
       // pnlBtnsAcciones.Enabled = true;
    }


    protected void rImgBtnAceptarC_Click(object sender, ImageButtonClickEventArgs e)
    {

        setCustomDescArt();
        rGdv_RegistoDetalle.DataBind();

        rTxtCustomDescArt.Text = "";
        
        hdfBtnAccionP.Value = "";
        ControlesAcciconDet();

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_RegistoDetalle.AllowMultiRowSelection = true;
    }

    protected void rImgBtnCancelarC_Click(object sender, ImageButtonClickEventArgs e)
    {
        frmCustomDescArt.Visible = false;
        rTxtCustomDescArt.Text = "";

        rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    }
    //GRET
    //Evento para combo descuento 
    protected void rCboDescuento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboDocumento.SelectedValue == "")
        {
            ShowAlert("2", "Debe seleccionar un documento");
            rCboDescuento.ClearSelection();
        }
    }

    //
    #endregion

    #region METODOS

    //GRET
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        
    }
        //
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_RawUrl_Return = Convert.ToString(Session["RawUrl_Return"]);

        Pag_sIdDocReg = Convert.ToString(Session["Valor_DocCve"]);
        hdfBtnAccion.Value = Convert.ToString(Session["Valor_btn"]);

        //hdfBtnAccion.Value = "2";
        //Pag_sIdDocReg = "4294";
    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }



    public void InicioPagina()
    {
        //MASH
        rLblIncoterm.Visible = false;
        rCboIncoterm.Visible = false;

        bAplicaTextil();

        divRef.Visible = false;
        divVar.Visible = false;

        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        DateTime sysFecha = DateTime.Today;
        string hoy = "";

        rFchDoc.SelectedDate = sysFecha;
        hoy = sysFecha.Day.ToString().PadLeft(2, '0') + "/" + sysFecha.Month.ToString().PadLeft(2, '0') + "/" + sysFecha.Year.ToString() + " 00:00:00 a.m.";
        rFchDoc.DataBind();

        if (rCboDocumento.SelectedValue != "")
        {

            rFchDoc.Enabled = true;
        }
        else
        {
            rFchDoc.Enabled = false;
        }

        TituloPagina();

        //dtTmpPart = new DataTable();
        Session["dtTmpPartFR"] = null;

        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false, "");
        FnCtlsFillIn.RabComboBox_Clientes(Pag_sConexionLog, Pag_sCompania, ref rCboCliente, true, false, "");
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false, "");
        FnCtlsFillIn.RabComboBox_ViasEmbarque(Pag_sConexionLog, Pag_sCompania, ref rCboViaEmbarque, true, false, "");
        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulo, true, false, "");
        //MASH
        FnCtlsFillIn.RadComboBox_FormaPago(Pag_sConexionLog, Pag_sCompania, ref rCboFormaPago, true, false, "");

        //Metodos de Pago
        FnCtlsFillIn.RabComboBox_SatMetodoPago(Pag_sConexionLog, ref rCboMetodoPago, true, false, "");

        FnCtlsFillIn.RabComboBox_ListaPrecios(Pag_sConexionLog, Pag_sCompania, ref rCboLstPrecios, true, false, "");
        FnCtlsFillIn.RabComboBox_Almacen(Pag_sConexionLog, Pag_sCompania, ref rCboAlmacen, true, false, "");
        FnCtlsFillIn.RabComboBox_DireccionesEntrega(Pag_sConexionLog, Pag_sCompania, "", ref rCboDireccionEntrega, true, false, "");

        //Se agrega llamada a metodo para llenar el combo de descuentos
        RabComboBox_Descuentos(Pag_sConexionLog, Pag_sCompania, ref rCboDescuento, true, false);
        //GRET, uso cfdi
        //RadComboBox_UsoCfdi
        FnCtlsFillIn.RadComboBox_UsoCfdi(Pag_sConexionLog, Pag_sCompania, ref rCboUsoCFDI, true, false, "");
        //
        rCboDireccionEntrega.EmptyMessage = "Seleccionar";
        //rCboDescuento.EmptyMessage = "Seleccionar";
        frmNewPartida.Visible = false;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = false;

        ParFormatodeFecha();
        rFchEntPart.DateFormat = "yyyy-MM-dd";

        rFchDoc.DisplayDateFormat = ParFormatodeFecha();
        rFchEntPart.DisplayDateFormat = ParFormatodeFecha();

        rFchDoc.DateFormat = "yyyy-MM-dd";

        Session["dtTmpAlmFR"] = null;
        createTableTmpAlm();


        if (Pag_sIdDocReg == "")
        {
            CargarTabla("");

        }
        else
        {
            ParListadePrecios();

            LlenarUi(Pag_sIdDocReg);
            CargarTabla(Pag_sIdDocReg);


            setLayGridPartidas(rCboDocumento.SelectedValue);

            enableUi(2);



            getImpuestos(rCboDocumento.SelectedValue);
            //GRET
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                getDescuentosModificar(Pag_sIdDocReg);
            }
            //
            clcDescGlbl();
            clcImpuGlbl();


        }

        ControlesAccion();



        ParManejaDescuentos();

        ParListadePrecios();



        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            rFchDoc.SelectedDate = sysFecha;
            hoy = sysFecha.Day.ToString().PadLeft(2, '0') + "/" + sysFecha.Month.ToString().PadLeft(2, '0') + "/" + sysFecha.Year.ToString() + " 00:00:00 a.m.";
            rFchDoc.DataBind();

            enableUI(1);


            setTotales();
            calcTotales();
            clcDescGlbl();
            setTotales();
            calcTotales();
            clcImpuGlbl();
            setTotales();
            calcTotales();

        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {
            enableUI(2);

            rCboLstPrecios.Enabled = true;
            rCboDocumento.Enabled = false;
            //GRET, se vacia el combo si el documento no trae cargado un descuento en la modificación
            rCboDescuento.Enabled = true;
            //

            foreach (DataListItem dtd in dt_descuentos.Items)
            {       
                
                      
                var varDesDes = dtd.FindControl("rlblDscDes") as RadLabel;

                // ProcBD.AgregarParametrosProcedimiento("@DesPorcen", DbType.Decimal, 10, ParameterDirection.Input, varDscTas.Text);
                //rCboDescuento.EmptyMessage = ds.Tables[0].Rows[0]["DesDes"].ToString();

                if (varDesDes.Text == "")
                {
                    rCboDescuento.EmptyMessage = "Seleccionar";
                }
                else
                {
                    rCboDescuento.EmptyMessage = varDesDes.Text;
                }
            }

            if (rCboDocumento.SelectedIndex != -1)
            {
                rCboDescuento.Enabled = true;
               

            }

            setLayDescuentos(rCboDocumento.SelectedValue);

            loadLayTotales();

            setTotales();
            calcTotales();
            clcDescGlbl();
            setTotales();
            calcTotales();
            clcImpuGlbl();
            setTotales();
            calcTotales();

            fillCboArticulos();

            if (rCboLstPrecios.Visible && rCboLstPrecios.SelectedValue != "")
            {
                pnlBtnsAcciones.Enabled = true;
            }

            if (getOpcCFDI() == "1")
            {
                rLblMetodoPago.Visible = true;
                rCboMetodoPago.Visible = true;
            }
            else
            {
                rLblMetodoPago.Visible = false;
                rCboMetodoPago.Visible = false;
            }


            //MASH
            string CveIncortem = rCboIncoterm.SelectedValue;
         

                if (getIncoterm() == "1")
                {

                    FnCtlsFillIn.RabComboBox_Incoterm(Pag_sConexionLog, Pag_sCompania, ref rCboIncoterm, true, false, "");
                    rLblIncoterm.Visible = true;
                    rCboIncoterm.Visible = true;
                }
                else
                {
                    rCboIncoterm.ClearSelection();
                    rLblIncoterm.Visible = false;
                    rCboIncoterm.Visible = false;
                }

            if (CveIncortem != "")
            {

                 rCboIncoterm.SelectedValue = CveIncortem;
            }







            cargaDl(Pag_sIdDocReg);
        }



        getEtiquetasPartida(1);
        getEtiquetasPartida(2);




        //Desarrollo Para copia de Documento

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {
            rCboDocumento.Enabled = true;
            getFolDoc(rCboDocumento.SelectedValue);
            rFchDoc.Enabled = true;

        }

        hdfBtnAccionP.Value = "";
        ControlesAcciconDet();
        frmNewPartida.Visible = true;
        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_RegistoDetalle.AllowMultiRowSelection = true;




    }












    private void enableUI(int paso)
    {

        if (paso == 1)
        {
            rCboDocumento.Enabled = true;

            rCboCliente.Enabled = false;
            rCboDireccionEntrega.Enabled = false;

            rCboViaEmbarque.Enabled = false;
            rCboMoneda.Enabled = false;

            rTxtFolio.Enabled = false;

            //GRET
            //Se agrega combo descuento
            rCboDescuento.Enabled = false;
            //

            rTxtDescripcion.Enabled = false;
            rCboLstPrecios.Enabled = false;


            pnlBtnsAcciones.Enabled = false;
            rGdv_RegistoDetalle.Enabled = false;

        }
        else if (paso == 2)
        {
            rCboCliente.Enabled = true;
            rCboDireccionEntrega.Enabled = true;

            rCboViaEmbarque.Enabled = true;
            rCboMoneda.Enabled = true;

            //GRET
            //Se agrega combo descuento

            if (dt_descuentos.Items.Count > 0)
            {
                rCboDescuento.Enabled = false;
            }
            else
            {
                rCboDescuento.Enabled = true;
            }
            //

            rTxtDescripcion.Enabled = true;

            if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rCboDocumento.Enabled = true;
            }else {
                rCboMoneda.Enabled = false;
                rCboCliente.Enabled = false;
            }
            
         


            //if (ParListadePrecios() == "AGR")
            //{
            //    if (rCboCliente.SelectedValue != "" && rCboMoneda.SelectedValue != "")
            //    {
            //        rCboLstPrecios.Enabled = true;
            //    }
                
            //    pnlBtnsAcciones.Enabled = false;
                
            //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            //    rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

            //    frmNewPartida.Visible = false;

            //}
            //else
            //{
            //    //rCboLstPrecios.Enabled = true;
            //    //pnlBtnsAcciones.Enabled = true;
            //}
            
            rGdv_RegistoDetalle.Enabled = true;

        }
        else if (paso == 3)
        {


            
            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;

            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;

            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;

        }
        else if (paso == 4)
        {
            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;

            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;

            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;

        }
        else if (paso == 5)
        {
            rCboArticulo.Enabled = true;
            rCboAlmacen.Enabled = true;

            rTxtPartLote.Enabled = true;
            rTxtPartSerie.Enabled = true;

            rFchEntPart.Enabled = true;
            rTxtPartCant.Enabled = true;
           // rTxtPartPrec.Enabled = true;

        }

    }
    
    private void setLayDescuentos(string docCve)
    {

        string tipDescDoc = getDocConfig(docCve, 4);

        if (tipDescDoc == "1")
        {
            rGdv_RegistoDetalle.Columns[10].Visible = true;
            dt_descuentos.Visible = false;
        }
        else if (tipDescDoc == "2")
        {
            rGdv_RegistoDetalle.Columns[10].Visible = false;
            dt_descuentos.Visible = true;
        }
        else if (tipDescDoc == "3")
        {
            rGdv_RegistoDetalle.Columns[10].Visible = true;
            dt_descuentos.Visible = true;
        }
        else if (tipDescDoc == "4")
        {
            rGdv_RegistoDetalle.Columns[10].Visible = false;
            dt_descuentos.Visible = false;
        }

        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();
        
    }




    private void ControlesAccion()
    {

        //===> CONTROLES POR ACCION
        // NUEVO 
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            
            rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardar.png";
            rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelar.png";

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

            enableUi(1);            
            LimpiarUi();

        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            
            enableUi(2);

            rBtnGuardar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnGuardar.png";
            rBtnCancelar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancelar.png";

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

            //LimpiarUi();
        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
        
        }
        
    }

   
    private void enableUi(int opc)
    {
        //Formato Nuevo - Todo Habilitado
        if (opc == 0)
        {
            //Campos de Informacion General
            rCboDocumento.Enabled = false;
            rTxtFolio.Enabled = false;
            //GRET
            //Se agrega combo descuento
            rCboDescuento.Enabled = false;
            //
            rTxtDescripcion.Enabled = false;
            //Campos Cabecero de Documento
            rCboCliente.Enabled = false;
            rCboDireccionEntrega.Enabled = false;
            rCboViaEmbarque.Enabled = false;
            rCboMoneda.Enabled = false;

            rCboMetodoPago.Enabled = false;
            
        }
        else if (opc == 1)
        {
            rCboDocumento.Enabled = true;
            rTxtDescripcion.Enabled = true;
            rCboCliente.Enabled = true;
            rCboViaEmbarque.Enabled = true;
            rCboMoneda.Enabled = true;
        }
        else if (opc == 2)
        {

            rCboDocumento.Enabled = false;
            rTxtFolio.Enabled = false;
            //GRET
            //Se agrega combo descuento
            rCboDescuento.Enabled = false;
            //
            rTxtDescripcion.Enabled = true;
            rCboViaEmbarque.Enabled = true;

            rCboMetodoPago.Enabled = true;

            rCboMoneda.Enabled = false;
            rCboCliente.Enabled = false;

            

        }

    }

    private void LimpiarUi()
    {

        if (true)
        {

            rCboDocumento.ClearSelection();
            rTxtFolio.Text = "";
            //rFchDoc.Text = "";
            rTxtDescripcion.Text = "";
            rCboCliente.ClearSelection();
            rCboDireccionEntrega.ClearSelection();
            rCboViaEmbarque.ClearSelection();
            rCboMoneda.ClearSelection();

            CargarTabla("");

            //rLblImpBruto.Text = "$0.00";
            //rLblImpDescuento.Text = "$0.00";
            //rLblImpSubTotal.Text = "$0.00";
            //rLblImpImpuesto.Text = "$0.00";
            //rLblImpTotal.Text = "$0.00";
            

            getImpuestos("");
            getDescuentos("");

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        }
    }

    private void TituloPagina()
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            FNGrales.bTitleDesc(Page, "Nuevo Registro de Documento", "PnlMPFormTitulo");
        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            FNGrales.bTitleDesc(Page, "Editar Registro de Documento", "PnlMPFormTitulo");
        }

    }
    
    private void LlenaComboClientes()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboCliente, ds, "cliCve", "clieNom", false, false);
        ((Literal)rCboCliente.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboCliente.Items.Count);

    }
    
    private void LlenaComboAlmacen()
    {
        
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
        //ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
        
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAlmacen, ds, "almCve", "almDes", false, false);
        ((Literal)rCboAlmacen.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAlmacen.Items.Count);

    }
    
    private void LlenaComboDirEntrega()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboDireccionEntrega, ds, "cliDirEntId", "cliDirEntDom", false, false);
        ((Literal)rCboDireccionEntrega.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboDireccionEntrega.Items.Count);

        rCboDireccionEntrega.Enabled = true;
    }
    

    
    private void CargarTabla(string docRegId)
    {

        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistroPartidas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.String, 10, ParameterDirection.Input, docRegId);
        
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        dtTmpPart = ds.Tables[0];

        FnCtlsFillIn.RadGrid(ref rGdv_RegistoDetalle, ds);

        Session["dtTmpPartFR"] = dtTmpPart;

        //Cargar Tabla de control existencias sin almacen

        DataTable dtTmpAlm = new DataTable();
        dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

        foreach (DataRow rowCol in ds.Tables[0].Rows)
        {
            DataRow rowTmp = dtTmpAlm.NewRow();
            rowTmp["artTmp"] = rowCol["artCve"].ToString();
            rowTmp["almTmp"] = rowCol["almCve"].ToString();
            rowTmp["canTmp"] = rowCol["docRegPartCant"].ToString();
            dtTmpAlm.Rows.Add(rowTmp);
        }

        

    }
    //MASH
    private void getFormaPago(string docCve, string docRegFolio)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_FormaPago";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCve);
            ProcBD.AgregarParametrosProcedimiento("@docRegFolio", DbType.String, 10, ParameterDirection.Input, docRegFolio);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt_impuestos.DataSource = ds;
                dt_impuestos.DataBind();

            }
            else
            {
                dt_impuestos.DataSource = ds;
                dt_impuestos.DataBind();
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("3", ex.ToString());
            //dt_impuestos.DataBind();
        }

    }



    private void getImpuestos(string docCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Impuestos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt_impuestos.DataSource = ds;
                dt_impuestos.DataBind();

            }
            else
            {
                dt_impuestos.DataSource = ds;
                dt_impuestos.DataBind();
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("3", ex.ToString());
            //dt_impuestos.DataBind();
        }
        
    }

    //GRET METODO para obtener descuentos al modificar
    private void getDescuentosModificar(string docRegFolio)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Descuentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegFolio", DbType.String, 10, ParameterDirection.Input, docRegFolio);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt_descuentos.DataSource = ds;
                dt_descuentos.DataBind();

                //GRET
                //Validacion para asignar a combo descuento el primer descuento configurado anteriormente en preferencias
                

                if (rCboDocumento.SelectedIndex != -1)
                {
                    rCboDescuento.Enabled = true;
                    rCboDescuento.EmptyMessage = ds.Tables[0].Rows[0]["DesDes"].ToString();

                }
                //

            }
            else
            {
                dt_descuentos.DataSource = ds;
                dt_descuentos.DataBind();
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("3", ex.ToString());
            //dt_descuentos.DataBind();
        }



    }
    //
    private void getDescuentos(string docCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Descuentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCve);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

                dt_descuentos.DataSource = ds;
                dt_descuentos.DataBind();

                //GRET
                //Validacion para asignar a combo descuento el primer descuento configurado anteriormente en preferencias
                //rCboDescuento.Enabled = true;
                                
                if (rCboDocumento.SelectedIndex != -1)
                {
                    rCboDescuento.EmptyMessage = ds.Tables[0].Rows[0]["DesDes"].ToString();                   
                                     
                }
                //

            }
            else
            {
                dt_descuentos.DataSource = ds;
                dt_descuentos.DataBind();
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("3", ex.ToString());
            //dt_descuentos.DataBind();
        }



    }

    private void getFolDoc(string docCve)
    {

        try
        {
            string tipFolio = "", valFolio = "";

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                tipFolio = ds.Tables[0].Rows[0]["docFolTip"].ToString();
                valFolio = ds.Tables[0].Rows[0]["folVal"].ToString();
                
                rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), "");

                if (tipFolio == "1")
                {
                    rTxtFolio.Enabled = true;
                }
                else if (tipFolio == "2")
                {
                    rTxtFolio.Enabled = false;
                }

            }
        }
        catch (Exception ex) 
        {

            
        }

        

    }

    //Se agrega metodo getFolDocNuevo para corregir el sato de folios  
    private void getFolDocNuevo(string docCve)
    {

        try
        {
            string tipFolio = "", valFolio = "";

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                tipFolio = ds.Tables[0].Rows[0]["docFolTip"].ToString();
                valFolio = ds.Tables[0].Rows[0]["folVal"].ToString();

                rTxtFolio.Text = FNGrales.sFoliosAutManNuevo(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), "");

                if (tipFolio == "1")
                {
                    rTxtFolio.Enabled = true;
                }
                else if (tipFolio == "2")
                {
                    rTxtFolio.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {


        }



    }
    //

    private void setLayGridPartidas(string docCve)
    {

        //Formato frmNewPartida

        rGdv_RegistoDetalle.Columns[1].Visible = chkSitOpcFormat(docCve, "DPART_ARTCVE");
        rGdv_RegistoDetalle.Columns[2].Visible = chkSitOpcFormat(docCve, "DPART_ARTDES");
        rGdv_RegistoDetalle.Columns[3].Visible = chkSitOpcFormat(docCve, "DPART_SERLOT");
        rGdv_RegistoDetalle.Columns[4].Visible = chkSitOpcFormat(docCve, "DPART_ALM");
        rGdv_RegistoDetalle.Columns[5].Visible = chkSitOpcFormat(docCve, "DPART_FecEnt");
        rGdv_RegistoDetalle.Columns[6].Visible = chkSitOpcFormat(docCve, "DPART_CANT");
        rGdv_RegistoDetalle.Columns[7].Visible = chkSitOpcFormat(docCve, "DPART_UNIMED");
        rGdv_RegistoDetalle.Columns[8].Visible = chkSitOpcFormat(docCve, "DPART_PRECUNIT");
        rGdv_RegistoDetalle.Columns[11].Visible = chkSitOpcFormat(docCve, "DPART_IMPIMP");
        rGdv_RegistoDetalle.Columns[12].Visible = chkSitOpcFormat(docCve, "DPART_IMPTOTAL");

        rGdv_RegistoDetalle.Columns[15].Visible = chkSitOpcFormat(docCve, "DPART_LOTE");
        rGdv_RegistoDetalle.Columns[16].Visible = chkSitOpcFormat(docCve, "DPART_SERIE");

        rLblAlmacenTag.Visible = chkSitOpcFormat(docCve, "DPART_ALM");
        rCboAlmacen.Visible = chkSitOpcFormat(docCve, "DPART_ALM");
        
        rTxtPartLote.Visible = chkSitOpcFormat(docCve, "DPART_LOTE");
        lbl_lote.Visible = chkSitOpcFormat(docCve, "DPART_LOTE");

        rTxtPartSerie.Visible = chkSitOpcFormat(docCve, "DPART_SERIE");
        lbl_serie.Visible = chkSitOpcFormat(docCve, "DPART_SERIE");
        
        rFchEntPartTag.Visible = chkSitOpcFormat(docCve, "DPART_FecEnt");
        rFchEntPart.Visible = chkSitOpcFormat(docCve, "DPART_FecEnt");



    }

    private void rstFrmNewPart()
    {
        DateTime sysFecha = DateTime.Today;
        rTxtPartPrec.Text = "0";
        rTxtPartCant.Text = "1";
        rFchEntPart.SelectedDate = sysFecha;
        rFchEntPart.DataBind();
        rCboArticulo.ClearSelection();

        rTxtPartLote.Text = "";
        rTxtPartSerie.Text = "";
        
        rCboAlmacen.ClearSelection();
        rCboArticulo.Focus();
    }

    private void reSetTotales()
    {

        setTotales();

        calcTotales();

        clcDescGlbl();

        setTotales();

        calcTotales();

        clcImpuGlbl();

        setTotales();

        calcTotales();
    }

    private void rutinaLoka()
    {

        double cantAcum;

        cantAcum = Convert.ToDouble(rTxtPartCant.Text);

        try
        {

            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ValidarExistencias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (cantAcum > 0)
                    {
                        if (Convert.ToDouble(row["exiTeorica"].ToString()) <= cantAcum)
                        {
                            addNewPart(rCboArticulo.SelectedValue, Convert.ToInt64(rCboLstPrecios.SelectedValue), Convert.ToDouble(row["exiTeorica"].ToString()), row["almCve"].ToString());
                            rGdv_RegistoDetalle.DataBind();
                            cantAcum -= Convert.ToDouble(row["exiTeorica"].ToString());
                        }
                        else
                        {
                            addNewPart(rCboArticulo.SelectedValue, Convert.ToInt64(rCboLstPrecios.SelectedValue), Convert.ToDouble(cantAcum), row["almCve"].ToString());
                            rGdv_RegistoDetalle.DataBind();
                            cantAcum -= cantAcum;
                        }
                    }



                }

            }

        }
        catch (Exception ex)
        {
            ShowAlert("3",ex.ToString());
        }



    }

    private void createTableTmpAlm()
    {
        DataTable dtTmpAlm = new DataTable();
        //dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

        if (dtTmpAlm.Columns.Count == 0)
        {
            dtTmpAlm.Columns.Add("artTmp");
            dtTmpAlm.Columns.Add("almTmp");
            dtTmpAlm.Columns.Add("canTmp");
        }

        Session["dtTmpAlmFR"] = dtTmpAlm;

    }

    private void addNewPart(string artCve)
    {

        double valImpBrt = 0;
        string tipDescDoc = getDocConfig(rCboDocumento.SelectedValue, 4);
        string prueba = "";

        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        try
        {

        
        //Sumar Primer SubtotalGlobal

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString() != "&nbsp;")
                {
                    valImpBrt += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString().Remove(0, 1));
                }
                else
                {
                    valImpBrt += 0;
                }

            }
        }

        DataTable dtTmpAlm = new DataTable();
        dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

        //Añadir Nueva Partida al grid


        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, artCve);

        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);

        ProcBD.AgregarParametrosProcedimiento("@cantidad", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rTxtPartCant.Text));

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            DataRow row = dtTmpPart.NewRow();
            DataRow rowTmp = dtTmpAlm.NewRow();

            row["docRegPartId"] = 1;
            row["artCve"] = ds.Tables[0].Rows[0]["artCve"].ToString();
            rowTmp["artTmp"] = ds.Tables[0].Rows[0]["artCve"].ToString();
            
            row["artDes"] = ds.Tables[0].Rows[0]["artDes"].ToString();





                if (rTxtPartLote.Text != "")
                {
                    row["docRegPartLote"] = rTxtPartLote.Text;
                }

                if (rTxtPartSerie.Text != "")
                {
                    row["docRegPartSerie"] = rTxtPartSerie.Text;
                }






            row["almCve"] = rCboAlmacen.SelectedValue;



            rowTmp["almTmp"] = rCboAlmacen.SelectedValue;

            if (rFchEntPart.DisplayText != rFchEntPart.EmptyMessage)
            {
                row["docRegPartFecEnt"] = rFchEntPart.DisplayText;
            }
            else
            {
                row["docRegPartFecEnt"] = null;
            }


            row["docRegPartCant"] = rTxtPartCant.Text;
            rowTmp["canTmp"] = rTxtPartCant.Text;
            row["uniMedCve"] = ds.Tables[0].Rows[0]["uniMedCve"].ToString();
            row["docRegPartPrec"] = rTxtPartPrec.DisplayText;


                //Calcular PU X C

            double partImp;
            double partImpu;
            double partDesc;
            double partNeto;

            double tmpDesc;
            double tmpImpu;


            partImp = Convert.ToDouble(rTxtPartCant.Text) * Convert.ToDouble(rTxtPartPrec.Text);
            row["docRegPartImpBrut"] = partImp.ToString("C", nfi);


            //row["docRegPartDescuento"] = ds.Tables[0].Rows[0]["DesPorcen"].ToString();

            partDesc = Convert.ToDouble(ds.Tables[0].Rows[0]["DesPorcen"].ToString());

            tmpDesc = partImp * (partDesc / 100);

            row["docRegPartImpDescPart"] = tmpDesc.ToString("C", nfi);

            partDesc = 100 - partDesc;

            partDesc /= 100;

            partNeto = partImp * Convert.ToDouble(partDesc);
            //partNeto = Math.Round(partImp * Convert.ToDouble(partDesc), 2, MidpointRounding.AwayFromZero);

            //if (rGdv_RegistoDetalle.Columns[10].Visible == true)
            //{
            //    partNeto = partImp * Convert.ToDouble(partDesc);

            //}
            //else
            //{
            //    partNeto = partImp;
            //}

            //Aqui hay que calcular descuentos globales para calcular impuestos en partida

            //***************************************************

            if (tipDescDoc == "2" || tipDescDoc == "3")
            {

                    //row["docRegPartImpDescDoc"] = clcDescGlbl(partNeto).ToString("C", nfi);
                    //partNeto -= clcDescGlbl(partNeto);

                    row["docRegPartImpDescDoc"] = Math.Round(clcDescGlbl(partNeto),2,MidpointRounding.AwayFromZero).ToString("C", nfi);
                    partNeto -= Math.Round(clcDescGlbl(partNeto), 2, MidpointRounding.AwayFromZero);
                    //partNeto *= Math.Round(clcDescGlbl(partNeto), 2, MidpointRounding.AwayFromZero);
                }

            row["docRegPartImpSubDDes"] = partNeto.ToString("C", nfi);

            partImpu = Convert.ToDouble(ds.Tables[0].Rows[0]["impTas"].ToString());

            partImpu /= 100;

            //-----------------------------
            //GRET
            //row["docRegPartImpImpuDoc"] = clcImpuGlbl(partNeto).ToString("C", nfi);
            row["docRegPartImpImpuDoc"] = clcImpuGlbl(partNeto).ToString("C20", nfi).TrimEnd('0');
            double tmpSumFact = 0;
            tmpSumFact = clcImpuGlbl(partNeto);
            //***********************************

            tmpImpu = partNeto * Convert.ToDouble(partImpu);

            row["docRegPartImpImpuEsp"] = tmpImpu.ToString("C", nfi);

            partImpu = 1 + partImpu;

            partNeto *= Convert.ToDouble(partImpu);
            //partNeto *= Math.Round(Convert.ToDouble(partImpu), 2, MidpointRounding.AwayFromZero);



            //Aqui hay que calcular Impuestos globales para calcular impuestos en partida e ImporteFacturado Part

            //***************************************************


            partNeto += tmpSumFact;
            
            //**************************************************

            row["docRegPartImpFact"] = partNeto.ToString("C", nfi);
            //row["docRegPartImpFact"] = Math.Round(partNeto, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);


            valImpBrt += partNeto;

            dtTmpPart.Rows.Add(row);

            dtTmpAlm.Rows.Add(rowTmp);

        }



        //rLblImpBruto.Text = valImpBrt.ToString("C", nfi);

        //setTotal(0, valImpBrt);


        //rLblImpTotal.Text = rLblImpBruto.Text;

        //Calcular Descuentos e Impuestos

        if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
        {
            actExiTeorica(Convert.ToInt32(rTxtPartCant.Text), 2, rCboArticulo.SelectedValue, rCboAlmacen.SelectedValue);
        }

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
        {
            this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();


        
        Session["dtTmpPartFR"] = dtTmpPart;
        Session["dtTmpAlmFR"] = dtTmpAlm;
            //rGdv_RegistoDetalle.DataSource = dtTmpPart;
            //DataSet dsFill = new DataSet();
            //dsFill.Tables.Add(dtTmpPart.Copy());
            //FnCtlsFillIn.RadGrid(ref rGdv_RegistoDetalle, dsFill);
        }
        catch (Exception ex)
        {
            string Mensaje = ex.Message.ToString();
            throw;
        }

    }

    private void addNewPart(string artCve, Int64 lisPreCve, double cantidad, string almacen)
    {

        double valImpBrt = 0;
        string tipDescDoc = getDocConfig(rCboDocumento.SelectedValue, 4);

        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        //Sumar Primer SubtotalGlobal

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString() != "&nbsp;")
                {
                    valImpBrt += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString().Remove(0, 1));
                }
                else
                {
                    valImpBrt += 0;
                }

            }
        }



        //Añadir Nueva Partida al grid

        DataTable dtTmpAlm = new DataTable();
        dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, artCve);

        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, lisPreCve);

        ProcBD.AgregarParametrosProcedimiento("@cantidad", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(cantidad));

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            DataRow row = dtTmpPart.NewRow();
            DataRow rowTmp = dtTmpAlm.NewRow();


            row["docRegPartId"] = 1;

            row["artCve"] = ds.Tables[0].Rows[0]["artCve"].ToString();
            rowTmp["artTmp"] = ds.Tables[0].Rows[0]["artCve"].ToString();

            row["artDes"] = ds.Tables[0].Rows[0]["artDes"].ToString();
            row["serieLote"] = "";
            //row["almCve"] = ds.Tables[0].Rows[0]["almCve"].ToString();
            row["almCve"] = almacen;
            rowTmp["almTmp"] = almacen.ToString();

            if (rFchEntPart.DisplayText != rFchEntPart.EmptyMessage)
            {
                row["docRegPartFecEnt"] = rFchEntPart.DisplayText;
            }
            else
            {
                row["docRegPartFecEnt"] = null;
            }

            row["docRegPartCant"] = cantidad;
            rowTmp["canTmp"] = cantidad.ToString();

            row["uniMedCve"] = ds.Tables[0].Rows[0]["uniMedCve"].ToString();

            row["docRegPartPrec"] = rTxtPartPrec.DisplayText;






            row["docRegPartLote"] = rTxtPartLote.Text;
            row["docRegPartSerie"] = rTxtPartLote.Text;





            //Calcular PU X C
            double partImp;
            double partImpu;
            double partDesc;
            double partNeto;

            double tmpDesc;
            double tmpImpu;


            partImp = Convert.ToDouble(cantidad) * Convert.ToDouble(rTxtPartPrec.Text);
            row["docRegPartImpBrut"] = partImp.ToString("C", nfi);


            //row["docRegPartDescuento"] = ds.Tables[0].Rows[0]["DesPorcen"].ToString();

            partDesc = Convert.ToDouble(ds.Tables[0].Rows[0]["DesPorcen"].ToString());

            tmpDesc = partImp * (partDesc / 100);

            row["docRegPartImpDescPart"] = tmpDesc.ToString("C", nfi);

            partDesc = 100 - partDesc;

            partDesc /= 100;

            partNeto = partImp * Convert.ToDouble(partDesc);

            //if (rGdv_RegistoDetalle.Columns[10].Visible == true)
            //{
            //    partNeto = partImp * Convert.ToDouble(partDesc);

            //}
            //else
            //{
            //    partNeto = partImp;
            //}

            //row["docRegPartDescuento"] = partNeto.ToString();


            //row["docRegPartImpuesto"] = ds.Tables[0].Rows[0]["impTas"].ToString();

            if (tipDescDoc == "2" || tipDescDoc == "3")
            {

                row["docRegPartImpDescDoc"] = clcDescGlbl(partNeto).ToString("C", nfi);
                partNeto -= clcDescGlbl(partNeto);
            }

            row["docRegPartImpSubDDes"] = partNeto.ToString("C", nfi);

            partImpu = Convert.ToDouble(ds.Tables[0].Rows[0]["impTas"].ToString());

            partImpu /= 100;


            tmpImpu = partNeto * Convert.ToDouble(partImpu);

            row["docRegPartImpImpuEsp"] = tmpImpu.ToString("C", nfi);

            partImpu = 1 + partImpu;

            partNeto *= Convert.ToDouble(partImpu);

            //Aqui hay que calcular Impuestos globales para calcular impuestos en partida e ImporteFacturado Part

            //***************************************************

            row["docRegPartImpImpuDoc"] = clcImpuGlbl(partNeto).ToString("C", nfi);
            partNeto += clcImpuGlbl(partNeto);

            //**************************************************


            row["docRegPartImpFact"] = partNeto.ToString("C", nfi);

            valImpBrt += partNeto;

            dtTmpPart.Rows.Add(row);

            dtTmpAlm.Rows.Add(rowTmp);

        }

        rGdv_RegistoDetalle.DataSource = dtTmpPart;

        //rLblImpBruto.Text = valImpBrt.ToString("C", nfi);

        //setTotal(0, valImpBrt);


        //rLblImpTotal.Text = rLblImpBruto.Text;

        //Calcular Descuentos e Impuestos

        if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
        {
            actExiTeorica(Convert.ToInt32(cantidad), 2, rCboArticulo.SelectedValue, almacen);
        }

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
        {
            this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();
        
        Session["dtTmpPartFR"] = dtTmpPart;
        Session["dtTmpAlmFR"] = dtTmpAlm;

    }

    private void clcDescImpuGlbl()
    {

        //Obtener Importe Bruto
        double impBruto;
        double impAcumulado;
        double totDescuentos = 0;
        double totImpuestos = 0;

        double tasaDesc;
        double tasaImpu;

        double result;


        impBruto = getSubTotales(1);

        //Descuentos

        foreach (DataListItem dli in dt_descuentos.Items)
        {
            var dscTasa = dli.FindControl("rlblDscTas") as RadLabel;

            tasaDesc = Convert.ToDouble(dscTasa.Text);

            result = impBruto * tasaDesc / 100;

            var dscPorcent = dli.FindControl("rlblDscPor") as RadLabel;


            //dscPorcent.Text = result.ToString();

            dscPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);

            impAcumulado = impBruto - result;

            impBruto = impAcumulado;

            totDescuentos += result;

            //rLblImpSubTotal.Text = impAcumulado.ToString();

            //rLblImpSubTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }

        //rLblImpDescuento.Text = totDescuentos.ToString();
        //rLblImpDescuento.Text = Math.Round(totDescuentos, 2, MidpointRounding.AwayFromZero).ToString();

        impBruto = getSubTotales(2);

        //Impuestos

        foreach (DataListItem dli in dt_impuestos.Items)
        {
            var impuTasa = dli.FindControl("rlblImpTas") as RadLabel;

            tasaImpu = Convert.ToDouble(impuTasa.Text);

            result = impBruto * tasaImpu / 100;

            var impuPorcent = dli.FindControl("rlblImpPor") as RadLabel;


            impuPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);

            impAcumulado = impBruto + result;

            impBruto = impAcumulado;

            totImpuestos += result;


            //rLblImpTotal.Text = impAcumulado.ToString();

            //rLblImpTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }

        //rLblImpImpuesto.Text = totImpuestos.ToString();
        //rLblImpImpuesto.Text = Math.Round(totImpuestos, 2, MidpointRounding.AwayFromZero).ToString();



    }

    private void clcDescGlbl()
    {

        //Obtener Importe Bruto
        double impBruto;
        double impAcumulado;
        double totDescuentos = 0;
        double totImpuestos = 0;

        double tasaDesc;
        double tasaImpu;

        double result;


        impBruto = getSubTotales(1);

        //Descuentos

        foreach (DataListItem dli in dt_descuentos.Items)
        {
            var dscTasa = dli.FindControl("rlblDscTas") as RadLabel;

            tasaDesc = Convert.ToDouble(dscTasa.Text);

            result = impBruto * tasaDesc / 100;

            var dscPorcent = dli.FindControl("rlblDscPor") as RadLabel;


            //dscPorcent.Text = result.ToString();

            dscPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);

            impAcumulado = impBruto - result;

            impBruto = impAcumulado;

            totDescuentos += result;

            //rLblImpSubTotal.Text = impAcumulado.ToString();

            //rLblImpSubTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }




    }

    private void clcImpuGlbl()
    {

        //Obtener Importe Bruto
        double impBruto;
        double impAcumulado;
        double totDescuentos = 0;
        double totImpuestos = 0;

        double tasaDesc;
        double tasaImpu;

        double result;

        impBruto = getSubTotales(2);

        //Impuestos

        foreach (DataListItem dli in dt_impuestos.Items)
        {
            var impuTasa = dli.FindControl("rlblImpTas") as RadLabel;

            tasaImpu = Convert.ToDouble(impuTasa.Text);

            result = impBruto * tasaImpu / 100;

            var impuPorcent = dli.FindControl("rlblImpPor") as RadLabel;

            //GRET, se comenta la siguiente linea y  se agrega nuevas lineas de codigo para corregir error en el timbrado por descuadre en inportes de impuestos y descuentos
            //impuPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);
            result = (double)TruncateDecimal((decimal)result, 2);
            impuPorcent.Text = result.ToString("C20", nfi).TrimEnd('0');
            impAcumulado = impBruto + result;

            impBruto = impAcumulado;

            totImpuestos += result;


            //rLblImpTotal.Text = impAcumulado.ToString();

            //rLblImpTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }

        //rLblImpImpuesto.Text = totImpuestos.ToString();
        //rLblImpImpuesto.Text = Math.Round(totImpuestos, 2, MidpointRounding.AwayFromZero).ToString();



    }

    private void ediNewPart()
    {

        double valImpBrt = 0;

        string tipDescDoc = getDocConfig(rCboDocumento.SelectedValue, 4);

        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        DataTable dtTmpAlm = new DataTable();
        dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

        //Recuperar Valores de Grid

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Selected == true)
                {


                    //Restar existencias Teoricas

                    if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
                    {
                        actExiTeorica(Convert.ToInt32(rGdv_RegistoDetalle.Items[i].Cells[8].Text), 1, rGdv_RegistoDetalle.Items[i].Cells[3].Text, dtTmpPart.Rows[i][6].ToString().Trim());
                    }




                    dtTmpPart.Rows[i]["docRegPartId"] = 1;
                    dtTmpPart.Rows[i]["artCve"] = rCboArticulo.SelectedValue;
                    dtTmpAlm.Rows[i]["artTmp"] = rCboArticulo.SelectedValue;

                    ///
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_Articulos";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
                    ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
                    ProcBD.AgregarParametrosProcedimiento("@cantidad", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rTxtPartCant.Text));
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    double partImp;
                    double partImpu;
                    double partDesc;
                    double partNeto;
                    double tmpDesc;
                    double tmpImpu;

                    //DataRow row = dtTmpPart.NewRow();
                    partImp = Convert.ToDouble(rTxtPartCant.Text) * Convert.ToDouble(rTxtPartPrec.Text);
                    dtTmpPart.Rows[i]["docRegPartImpBrut"] = partImp.ToString("C", nfi);


                    // CALCULA DESCUENTO
                    partDesc = Convert.ToDouble(ds.Tables[0].Rows[0]["DesPorcen"].ToString());

                    tmpDesc = partImp * (partDesc / 100);

                    dtTmpPart.Rows[i]["docRegPartImpDescPart"] = tmpDesc.ToString("C", nfi);

                    partDesc = 100 - partDesc;

                    partDesc /= 100;

                    partNeto = partImp * Convert.ToDouble(partDesc);

                    //if (rGdv_RegistoDetalle.Columns[10].Visible == true)
                    //{
                    //    partNeto = partImp * Convert.ToDouble(partDesc);

                    //}
                    //else
                    //{
                    //    partNeto = partImp;
                    //}


                    if (tipDescDoc == "2" || tipDescDoc == "3")
                    {

                        dtTmpPart.Rows[i]["docRegPartImpDescDoc"] = clcDescGlbl(partNeto).ToString("C", nfi);
                        partNeto -= clcDescGlbl(partNeto);
                    }

                    dtTmpPart.Rows[i]["docRegPartImpSubDDes"] = partNeto.ToString("C", nfi);

                    // CALCULA DESCUENTOS

                    partImpu = Convert.ToDouble(ds.Tables[0].Rows[0]["impTas"].ToString());

                    partImpu /= 100;


                    //-----------------------------
                    dtTmpPart.Rows[i]["docRegPartImpImpuDoc"] = clcImpuGlbl(partNeto).ToString("C", nfi);
                    double tmpSumFact = 0;
                    tmpSumFact = clcImpuGlbl(partNeto);
                    //***********************************

                    tmpImpu = partNeto * Convert.ToDouble(partImpu);

                    dtTmpPart.Rows[i]["docRegPartImpImpuEsp"] = tmpImpu.ToString("C", nfi);

                    partImpu = 1 + partImpu;

                    partNeto *= Convert.ToDouble(partImpu);


                    //Aqui hay que calcular Impuestos globales para calcular impuestos en partida e ImporteFacturado Part

                    //***************************************************

                    
                    partNeto += tmpSumFact;

                    //**************************************************


                    dtTmpPart.Rows[i]["docRegPartImpFact"] = partNeto.ToString("C", nfi);

                    valImpBrt += partNeto;

                    // dtTmpPart.Rows.Add(row);

                    dtTmpPart.Rows[i]["artDes"] = rGdv_RegistoDetalle.Items[i].Cells[4].Text;








                    if (rTxtPartLote.Text != "")
                    {
                        dtTmpPart.Rows[i]["docRegPartLote"] = rTxtPartLote.Text;
                    }
                    else
                    {
                        dtTmpPart.Rows[i]["docRegPartLote"] = "";
                    }

                    if (rTxtPartSerie.Text != "")
                    {
                        dtTmpPart.Rows[i]["docRegPartSerie"] = rTxtPartSerie.Text;
                    }
                    else
                    {
                        dtTmpPart.Rows[i]["docRegPartSerie"] = "";
                    }








                    if (rCboAlmacen.Visible)
                    {
                        dtTmpPart.Rows[i]["almCve"] = rCboAlmacen.SelectedValue;
                        dtTmpAlm.Rows[i]["almTmp"] = rCboAlmacen.SelectedValue;
                    }
                    else
                    {
                        //dtTmpPart.Rows[i]["almCve"] = dtTmpPart.Rows[i][6].ToString().Trim();
                    }
                    


                    if (rFchEntPart.DisplayText != rFchEntPart.EmptyMessage)
                    {
                        dtTmpPart.Rows[i]["docRegPartFecEnt"] = rFchEntPart.DisplayText;
                    }
                    else
                    {
                        dtTmpPart.Rows[i]["docRegPartFecEnt"] = null;
                    }

                    dtTmpPart.Rows[i]["docRegPartCant"] = rTxtPartCant.Text;
                    dtTmpAlm.Rows[i]["canTmp"] = rTxtPartCant.Text;

                    dtTmpPart.Rows[i]["uniMedCve"] = dtTmpPart.Rows[i][7].ToString().Trim();
                    dtTmpPart.Rows[i]["docRegPartPrec"] = rTxtPartPrec.DisplayText;



                    partImp = Convert.ToDouble(rTxtPartCant.Text) * Convert.ToDouble(rTxtPartPrec.Text);
                    dtTmpPart.Rows[i]["docRegPartImpBrut"] = partImp.ToString("C", nfi);

                    partImp = Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString().Remove(0, 1));

                    //valImpBrt += partImp;


                    if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
                    {
                        if (rCboAlmacen.Visible)
                        {
                            actExiTeorica(Convert.ToInt32(rTxtPartCant.Text), 2, rCboArticulo.SelectedValue, rCboAlmacen.SelectedValue);
                        }else
                        {
                            actExiTeorica(Convert.ToInt32(rTxtPartCant.Text), 2, rCboArticulo.SelectedValue, dtTmpPart.Rows[i][6].ToString().Trim());
                        }
                        
                    }



                }
                else if (rGdv_RegistoDetalle.Items[i].Selected == false)
                {

                    valImpBrt += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString().Remove(0, 1));

                }
            }
        }


        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
        {
            this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();

        Session["dtTmpPartFR"] = dtTmpPart;
        
    }

    private void delNewPart()
    {
        double valImpBrt = 0;
        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];
        dtTmpPart.AcceptChanges();

        DataTable dtTmpAlm = new DataTable();
        dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];
        dtTmpAlm.AcceptChanges();


        //Recuperar Valores de Grid

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Selected == true)
                {

                    if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
                    {
                        actExiTeorica(Convert.ToInt32(rGdv_RegistoDetalle.Items[i].Cells[8].Text), 1, rGdv_RegistoDetalle.Items[i].Cells[3].Text, dtTmpPart.Rows[i][6].ToString().Trim());
                    }


                    dtTmpPart.Rows[i].Delete();
                    dtTmpAlm.Rows[i].Delete();

                }
                else if (rGdv_RegistoDetalle.Items[i].Selected == false)
                {
                    /*

                    DataRow row = dtTmpPart.NewRow();


                    row["docRegPartId"] = 1;

                    row["artCve"] = rGdv_RegistoDetalle.Items[i].Cells[3].Text;
                    row["artDes"] = rGdv_RegistoDetalle.Items[i].Cells[4].Text;
                    row["serieLote"] = rGdv_RegistoDetalle.Items[i].Cells[5].Text;
                    row["almCve"] = rGdv_RegistoDetalle.Items[i].Cells[6].Text;
                    row["docRegPartFecEnt"] = rGdv_RegistoDetalle.Items[i].Cells[7].Text;
                    row["docRegPartCant"] = rGdv_RegistoDetalle.Items[i].Cells[8].Text;
                    row["uniMedCve"] = rGdv_RegistoDetalle.Items[i].Cells[9].Text;
                    row["docRegPartPrec"] = rGdv_RegistoDetalle.Items[i].Cells[10].Text;
                    row["docRegPartImpBrut"] = rGdv_RegistoDetalle.Items[i].Cells[11].Text;

                    row["docRegPartDescuento"] = rGdv_RegistoDetalle.Items[i].Cells[12].Text;
                    row["docRegPartImpuesto"] = rGdv_RegistoDetalle.Items[i].Cells[13].Text;
                    row["docRegPartImpNeto"] = rGdv_RegistoDetalle.Items[i].Cells[14].Text;

                    

                    dtTmpPart.Rows.Add(row);

                    */
                    if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
                    {
                        valImpBrt += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString().Remove(0, 1));
                    }


                }


            }
        }

        dtTmpPart.AcceptChanges();
        dtTmpAlm.AcceptChanges();

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
        {
            this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerSide;
        }

        rGdv_RegistoDetalle.DataSource = dtTmpPart;
        rGdv_RegistoDetalle.DataBind();

        Session["dtTmpPartFR"] = dtTmpPart;
        Session["dtTmpAlmFR"] = dtTmpAlm;

    }

    private void chkColumns()
    {

        if (rGdv_RegistoDetalle.Items[0].Cells[8].Text == "")
        {
            rGdv_RegistoDetalle.Items[0].Cells[8].Visible = false;
        }

    }

    private void cancelarExiTeo()
    {

        string docCve;

        if (rCboDocumento.SelectedIndex != -1)
        {

            docCve = rCboDocumento.SelectedValue;

            if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1" && chkSitOpcFormat(docCve, "DPART_ALM"))
            {

                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    actExiTeorica(Convert.ToInt64(rGdv_RegistoDetalle.Items[i].Cells[8].Text), 1, rGdv_RegistoDetalle.Items[i].Cells[3].Text, rGdv_RegistoDetalle.Items[i].Cells[6].Text);
                }

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {

                    CargarTabla(Pag_sIdDocReg);

                    for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                    {
                        actExiTeorica(Convert.ToInt64(rGdv_RegistoDetalle.Items[i].Cells[8].Text), 2, rGdv_RegistoDetalle.Items[i].Cells[3].Text, rGdv_RegistoDetalle.Items[i].Cells[6].Text);
                    }

                }
            }
            else if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1" )
            {
                DataTable dtTmpAlm = new DataTable();
                dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];
                //ES DE AQUI!!!
                foreach (DataRow row in dtTmpAlm.Rows)
                {
                    actExiTeorica(Convert.ToInt64(row["canTmp"].ToString()), 1, row["artTmp"].ToString(), row["almTmp"].ToString());
                }

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {

                    CargarTabla(Pag_sIdDocReg);

                    DataTable dtTmpPart = new DataTable();
                    dtTmpPart = (DataTable)Session["dtTmpPartFR"];

                    for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                    {
                        actExiTeorica(Convert.ToInt64(rGdv_RegistoDetalle.Items[i].Cells[8].Text), 2, rGdv_RegistoDetalle.Items[i].Cells[3].Text, dtTmpPart.Rows[i][6].ToString().Trim());
                    }

                }

            }


        }




    }

    private void EjecutaSpAcciones()
    {
        bool itsCopy;

        try
        {
            DataSet ds = new DataSet();
            Int64 newDocRegId = 0;

            rFchDoc.DisplayDateFormat = "yyyy-MM-dd";

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
            {
                hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
                itsCopy = true;
            }
            else
            {
                itsCopy = false;
            }


            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            //Cambiar Por Variable de session

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(Pag_sIdDocReg));
            }

            ProcBD.AgregarParametrosProcedimiento("@docRegFolio", DbType.String, 10, ParameterDirection.Input, rTxtFolio.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@docRegDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);

            //Obtener Agentes automaticamente

            if (getAgent("1") != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@ageCveVen", DbType.String, 10, ParameterDirection.Input, getAgent("1"));
            }
            if (getAgent("2") != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@ageCveCob", DbType.String, 10, ParameterDirection.Input, getAgent("2"));
            }

            //Obtener Condicion de Pago
            if (getPagDias() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@conPagCve", DbType.String , 10, ParameterDirection.Input, getPagDias());
            }


            if (rCboDireccionEntrega.SelectedIndex != -1) {
                ProcBD.AgregarParametrosProcedimiento("@cliDirEntId", DbType.Int64, 0, ParameterDirection.Input, rCboDireccionEntrega.SelectedValue);
            }



            ProcBD.AgregarParametrosProcedimiento("@viaEmbCve", DbType.String, 7, ParameterDirection.Input, rCboViaEmbarque.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);

            //Obtener del label
            ProcBD.AgregarParametrosProcedimiento("@docRegFec", DbType.String, 100, ParameterDirection.Input, rFchDoc.DisplayText + " 00:00:00");

            //Obtener del foot
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpBrut", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal("SubTotal Bruto"));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpDescPart", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal(getEtiquetasPartida(1)));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpDescDoc", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal("Descuento(s)"));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpSubDDes", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal("SubTotal después de Descuentos"));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpImpuEsp", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal(getEtiquetasPartida(2)));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpImpuDoc", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal("Impuesto(s)"));
            ProcBD.AgregarParametrosProcedimiento("@docRegTotImpFact", DbType.Decimal, 15, ParameterDirection.Input, getImpTotal("Total a Pagar"));

            ProcBD.AgregarParametrosProcedimiento("@docRegObs", DbType.String, 200, ParameterDirection.Input, "");

            if (getOpcCFDI() == "1")
            {
                ProcBD.AgregarParametrosProcedimiento("@satMetPagCve", DbType.String, 2, ParameterDirection.Input, rCboMetodoPago.SelectedValue);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@satMetPagCve", DbType.String, 2, ParameterDirection.Input, getSatMetPag());
            }

            ProcBD.AgregarParametrosProcedimiento("@docFlujoTrabId", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@docRegSit", DbType.Int64, 0, ParameterDirection.Input, 1);

            if (rCboLstPrecios.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
            }

            //MASH

            if (rCboIncoterm.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@IncotermCve", DbType.String, 10, ParameterDirection.Input, rCboIncoterm.SelectedValue);
            }

            //MASH
           
             if (rCboFormaPago.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@FormaPagoCve", DbType.String, 10, ParameterDirection.Input, rCboFormaPago.SelectedValue);
            }

            //GRET, se agregan parametros   @DscDes,@DscTas,@DscPor a stor sp_DocumentoRegistro para guardar descuento
            if (rCboDescuento.SelectedIndex != -1 )
            {
               
                foreach (DataListItem dtd in dt_descuentos.Items)
                {              
                    var varDscTas = dtd.FindControl("rlblDscTas") as RadLabel;                   

                    ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rCboDescuento.SelectedValue);
                    ProcBD.AgregarParametrosProcedimiento("@DesDes", DbType.String, 10, ParameterDirection.Input,rCboDescuento.SelectedItem.Text);
                    ProcBD.AgregarParametrosProcedimiento("@DesPorcen", DbType.Decimal, 10, ParameterDirection.Input, varDscTas.Text);

                }
            }

            if (rCboUsoCFDI.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@satUsoCFDICve", DbType.String, 100, ParameterDirection.Input, rCboUsoCFDI.SelectedValue);
            }
                //


                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
				//Se agrega folio a mensaje cuando se genera el documento
                sEjecMSG = sEjecMSG + "<br><br><b>Folio: " + rTxtFolio.Text.Trim() + "</b></br></br>";

                if (sEjecEstatus == "1")
                {

                    newDocRegId = Convert.ToInt64(ds.Tables[0].Rows[0]["docRegId"].ToString());

                    EjecutaSpPartidas(newDocRegId);

//Se agregan condiciones solo para generar folio cuando sea Alta  de documento y no volver a generar folio cuando sea Modificacion
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                    {

                        guardaDtFolio(Convert.ToString(newDocRegId));

                        guardaDt(Convert.ToString(newDocRegId));
                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        guardaDt(Convert.ToString(newDocRegId));
                    }

                    ejecutaSpValidar(newDocRegId, rCboDocumento.SelectedValue);

                    //AWS

                    Pag_sIdDocReg = newDocRegId.ToString();

                    if (itsCopy)
                    {
                        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString();
                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                    {
                        Session["Valor_DocCve"] = "";
                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        Session["Valor_DocCve"] = Pag_sIdDocReg;
                    }


                    rCboLstPrecios.DataBind();
                    rCboLstPrecios.ClearSelection();
                    rCboLstPrecios.Items.Clear();
                    ((Literal)rCboLstPrecios.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboLstPrecios.Items.Count);


                    InicioPagina();
                 
                    rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdv_RegistoDetalle.AllowMultiRowSelection = true;
                    rGdv_RegistoDetalle.Enabled = true;
                  

                }

                ShowAlert(sEjecEstatus, sEjecMSG);

            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpPartidas(Int64 docRegId)
    {
        try
        {

            DataTable dtTmpPart = new DataTable();
            dtTmpPart = (DataTable)Session["dtTmpPartFR"];

            rGdv_RegistoDetalle.DataSource = dtTmpPart;
            rGdv_RegistoDetalle.DataBind();


            if (rGdv_RegistoDetalle.Items.Count > 0)
            {
                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    DataSet ds = new DataSet();

                    rFchEntPart.DisplayDateFormat = "yyyy-MM-dd";


                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_DocumentoRegistroPartidas";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

                    ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, docRegId);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartNum", DbType.Int64, 0, ParameterDirection.Input, i + 1);
                    ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rGdv_RegistoDetalle.Items[i].Cells[3].Text.Trim());

                    if (rGdv_RegistoDetalle.Items[i].Cells[4].Text.Trim() != "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@artDes", DbType.String, 120, ParameterDirection.Input, rGdv_RegistoDetalle.Items[i].Cells[4].Text.Trim());
                    }
                    else
                    {
                        ProcBD.AgregarParametrosProcedimiento("@artDes", DbType.String, 120, ParameterDirection.Input, dtTmpPart.Rows[i][5].ToString());
                    }

                    if (dtTmpPart.Rows[i][6].ToString().Trim() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, dtTmpPart.Rows[i][6].ToString());
                    }


                    //ProcBD.AgregarParametrosProcedimiento("@serieLote", DbType.String, 200, ParameterDirection.Input, rGdvPartidas.Items[i].Cells[5].Text.Trim());


                    if (rGdv_RegistoDetalle.Items[i].Cells[9].Text.Trim() != "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, rGdv_RegistoDetalle.Items[i].Cells[9].Text.Trim());
                    }
                    else if (rGdv_RegistoDetalle.Items[i].Cells[9].Text.Trim() == "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, dtTmpPart.Rows[i][7].ToString());
                    }
                    else
                    {
                        ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, dtTmpPart.Rows[i][7].ToString());
                    }


                    if (rGdv_RegistoDetalle.Items[i].Cells[10].Text.Trim() != "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartPrec", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(rGdv_RegistoDetalle.Items[i].Cells[10].Text.Trim().Remove(0, 1)));
                    }
                    else
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartPrec", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i][10].ToString().Trim().Remove(0, 1)));
                    }

                    string X = rGdv_RegistoDetalle.Items[i].Cells[8].Text.Trim();
                    if (rGdv_RegistoDetalle.Items[i].Cells[8].Text.Trim() != "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartCant", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(rGdv_RegistoDetalle.Items[i].Cells[8].Text.Trim()));
                    }
                    else
                    {
                        //ProcBD.AgregarParametrosProcedimiento("@docRegPartCant", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i][8].ToString().Trim() ));
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartCant", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartCant"].ToString().Trim()));
                    }

                    //ProcBD.AgregarParametrosProcedimiento("@docRegPartCantSurt", DbType.Decimal, 0, ParameterDirection.Input, 1);
                    //ProcBD.AgregarParametrosProcedimiento("@docRegPartCantPend", DbType.Decimal, 0, ParameterDirection.Input, 1);



                    if (rGdv_RegistoDetalle.Items[i].Cells[7].Text.Trim() != "&nbsp;")
                    {

                        string fechaGrid = rGdv_RegistoDetalle.Items[i].Cells[7].Text.Trim();
                        string fechaParam = "";
                        DateTime oDate = DateTime.Parse(fechaGrid);

                        fechaParam = oDate.Year.ToString() + "-" + oDate.Month.ToString().PadLeft(2, '0') + "-" + oDate.Day.ToString().PadLeft(2, '0') + " 00:00:00";

                        ProcBD.AgregarParametrosProcedimiento("@docRegPartFecEnt", DbType.String, 100, ParameterDirection.Input, fechaParam);
                    }
                    
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpBrut", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpBrut"].ToString().Trim().Remove(0, 1)));
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpDescPart", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpDescPart"].ToString().Trim().Remove(0, 1)));

                    if (dtTmpPart.Rows[i]["docRegPartImpDescDoc"].ToString() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartImpDescDoc", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpDescDoc"].ToString().Trim().Remove(0, 1)));
                    }

                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpSubDDes", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpSubDDes"].ToString().Trim().Remove(0, 1)));
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpImpuEsp", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpImpuEsp"].ToString().Trim().Remove(0, 1)));
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpImpuDoc", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpImpuDoc"].ToString().Trim().Remove(0, 1)));
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartImpFact", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(dtTmpPart.Rows[i]["docRegPartImpFact"].ToString().Trim().Remove(0, 1)));

                    ProcBD.AgregarParametrosProcedimiento("@docRegPartObs", DbType.String, 0, ParameterDirection.Input, "DESDE UI");
                    ProcBD.AgregarParametrosProcedimiento("@docRegPartSit", DbType.Int64, 0, ParameterDirection.Input, 1);





                    //LOTE//
                    if (dtTmpPart.Rows[i]["docRegPartLote"].ToString().Trim() != "&nbsp;" || dtTmpPart.Rows[i]["docRegPartLote"].ToString().Trim() != "" )
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartLote", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dtTmpPart.Rows[i]["docRegPartLote"].ToString().Trim()));
                    }



                    //SERIE//
                    if (dtTmpPart.Rows[i]["docRegPartSerie"].ToString().Trim() != "&nbsp;" || dtTmpPart.Rows[i]["docRegPartSerie"].ToString().Trim() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docRegPartSerie", DbType.String, 20, ParameterDirection.Input, Convert.ToString(dtTmpPart.Rows[i]["docRegPartSerie"].ToString().Trim()));
                    }

                    if (txtTextilOperacion.Text.Trim() != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@docTextilOper", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64 (txtTextilOperacion.Text.Trim() ) );
                    }


                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds.Tables.Count > 0)
                    {

                    }
                }






            }

            Session["dtTmpPartFR"] = dtTmpPart;

        }
        catch (Exception ex)
        {
            string Mensaje;
            Mensaje = ex.Message.ToString(); 
            
        }

    }

    private void LlenarUi(string docRegId)
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                //Primer campo folio, traer uno nuevo
                //rTxtSubClie.Text = ds.Tables[0].Rows[0]["cliCveSubClie"].ToString();

                foreach (RadComboBoxItem item in rCboDocumento.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["docCve"].ToString())
                    {
                        rCboDocumento.SelectedIndex = item.Index;
                    }
                }

                //Ser dinamico por si es copia , calcular , este solo recupera info
                rTxtFolio.Text = ds.Tables[0].Rows[0]["docRegFolio"].ToString();
                //rFchDoc.DisplayText = ds.Tables[0].Rows[0]["docRegFec"].ToString().Substring(0,10);
                rFchDoc.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["docRegFec"].ToString());
                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["docRegDes"].ToString();

                foreach (RadComboBoxItem item in rCboCliente.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["cliCve"].ToString())
                    {
                        rCboCliente.SelectedIndex = item.Index;
                    }
                }

                LlenaComboDirEntrega();

                foreach (RadComboBoxItem item in rCboDireccionEntrega.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["cliDirEntId"].ToString())
                    {
                        rCboDireccionEntrega.SelectedIndex = item.Index;
                    }
                }

                foreach (RadComboBoxItem item in rCboViaEmbarque.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["viaEmbCve"].ToString())
                    {
                        rCboViaEmbarque.SelectedIndex = item.Index;
                    }
                }
                 
                foreach (RadComboBoxItem item in rCboMoneda.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["monCve"].ToString())
                    {
                        rCboMoneda.SelectedIndex = item.Index;
                    }
                }


                foreach (RadComboBoxItem item in rCboMetodoPago.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["satMetPagCve"].ToString())
                    {
                        rCboMetodoPago.SelectedIndex = item.Index;
                    }
                }


                if (ParListadePrecios() == "DOC")
                {
                    rCboLstPrecios.Enabled = true;
                    //LlenaComboLstPrecios(51);
                    ParListadePreciosPorDocumento();
                }
                else if (ParListadePrecios() == "AGR")
                {
                    //rCboLstPrecios.Enabled = true;
                    LlenaComboLstPrecios(52);
                }

               
                foreach (RadComboBoxItem item in rCboLstPrecios.Items)
                {
                    if (item.Value == ds.Tables[0].Rows[0]["lisPreCve"].ToString())
                    {
                        rCboLstPrecios.SelectedIndex = item.Index;
                    }
                }
            }

            //MASH
            DataSet ds1 = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBDMS = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBDMS.NombreProcedimiento = "sp_DocumentoRegistro";
            //MASH  se cambia 51 por 56
            ProcBDMS.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
            ProcBDMS.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBDMS.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds1 = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBDMS.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            //MASH
            //MASH Recupera los valores de Catalogos Cve Incoterm y Forma de Pago 
            String incoCve = "", incoDes = "", satFormaPagCve = "", satFormaPagDes = "";
            incoCve = ds1.Tables[0].Rows[0]["incoCve"].ToString();
            incoDes = ds1.Tables[0].Rows[0]["incoDes"].ToString();
            satFormaPagCve = ds1.Tables[0].Rows[0]["satFormaPagCve"].ToString();
            satFormaPagDes = ds1.Tables[0].Rows[0]["satFormaPagDes"].ToString();
            rCboFormaPago.SelectedValue = ds1.Tables[0].Rows[0]["satFormaPagCve"].ToString();
            rCboIncoterm.SelectedValue = ds1.Tables[0].Rows[0]["incoCve"].ToString();



        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
            {
                EjecutaSpAcciones();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }


    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void actExiTeorica(Int64 cantidad, int opc, string articulo, string almacen)
    {


        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ValidarExistencias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 100, ParameterDirection.Input, articulo);
            ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 100, ParameterDirection.Input, almacen);

            ProcBD.AgregarParametrosProcedimiento("@ExistRecibidas", DbType.Int64, 0, ParameterDirection.Input, cantidad);
            ProcBD.AgregarParametrosProcedimiento("@Calcu", DbType.Int64, 0, ParameterDirection.Input, opc);


            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

            }

        }
        catch (Exception ex)
        {
            
        }

    }

    private void loadLayTotales()
    {

        //Crear el Datatsource con un dtTemporal

        string tipDescDoc = getDocConfig(rCboDocumento.SelectedValue, 4);

        dtLayTotales.Columns.Add("totCve");
        dtLayTotales.Columns.Add("totDes");
        dtLayTotales.Columns.Add("sigCve");

        DataRow row = dtLayTotales.NewRow();

        row["totCve"] = "0";
        row["totDes"] = "SubTotal Bruto";
        row["sigCve"] = "=";

        dtLayTotales.Rows.Add(row);


        //---------

        if (tipDescDoc == "1" || tipDescDoc == "3")
        {
            row = dtLayTotales.NewRow();

            row["totCve"] = "1";
            row["totDes"] = getEtiquetasPartida(1);
            row["sigCve"] = "-";

            dtLayTotales.Rows.Add(row);
        }

        //---------

        if (tipDescDoc == "2" || tipDescDoc == "3")
        {

            row = dtLayTotales.NewRow();

            row["totCve"] = "2";
            row["totDes"] = "Descuento(s)";
            row["sigCve"] = "-";

            dtLayTotales.Rows.Add(row);
        }

        //--------

        row = dtLayTotales.NewRow();

        row["totCve"] = "3";
        row["totDes"] = "SubTotal después de Descuentos";
        row["sigCve"] = "=";

        dtLayTotales.Rows.Add(row);

        //----------------

        //if (chkSitOpcFormat(rCboDocumento.SelectedValue, "DPART_IMPIMP"))
        //{
            row = dtLayTotales.NewRow();

            row["totCve"] = "4";
            row["totDes"] = getEtiquetasPartida(2);
            row["sigCve"] = "+";

            dtLayTotales.Rows.Add(row);
        //}




        //----------------

        row = dtLayTotales.NewRow();

        row["totCve"] = "5";
        row["totDes"] = "Impuesto(s)";
        row["sigCve"] = "+";

        dtLayTotales.Rows.Add(row);


        //----------------

        row = dtLayTotales.NewRow();

        row["totCve"] = "6";
        row["totDes"] = "Total a Pagar";
        row["sigCve"] = "=";

        dtLayTotales.Rows.Add(row);


        dt_totales.DataSource = dtLayTotales;
        dt_totales.DataBind();


    }

    private void setTotal(int posicion, double importe)
    {
        string search;

        if (posicion == 0)
        {
            search = "SubTotal Bruto";
        }
        else
        {
            search = "Null";
        }


        foreach (DataListItem dli in dt_totales.Items)
        {

            var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;
            var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
            var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;

            if (search == varTotalTag.Text.Trim())
            {
                varTotalImp.Text = importe.ToString();
            }

        }

    }

    private void setTotales()
    {

        double subTotal1 = 0;

        double descPart = 0;

        double impuPart = 0;

        //Recorrer Lay de Totales
        foreach (DataListItem dli in dt_totales.Items)
        {

            var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;
            var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
            var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;

            //Recuperar Primer Subtotal

            if (varTotalTag.Text == "SubTotal Bruto")
            {
                if (rGdv_RegistoDetalle.Items.Count > 0)
                {
                    for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                    {

                        if (rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString() != "&nbsp;")
                        {
                            subTotal1 += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString().Remove(0, 1));
                        }
                        else
                        {
                            subTotal1 += 0;
                        }

                    }
                }

                varTotalImp.Text = subTotal1.ToString();

            }
            else if (varTotalTag.Text == getEtiquetasPartida(1))
            {

                if (rGdv_RegistoDetalle.Items.Count > 0)
                {
                    for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                    {

                        if (rGdv_RegistoDetalle.Items[i].Cells[12].Text.ToString() != "&nbsp;")
                        {
                            descPart += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[12].Text.ToString().Remove(0, 1));
                        }
                        else
                        {
                            descPart += 0;
                        }

                    }
                }

                varTotalImp.Text = descPart.ToString("C", nfi);

            }
            else if (varTotalTag.Text == getEtiquetasPartida(2))
            {

                //if (rGdv_RegistoDetalle.Items.Count > 0)
                //{
                //    for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                //    {
                //        //Aqui cambiar para que lea desde tabla temproal no grid
                //        if (rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString() != "&nbsp;")
                //        {
                //            impuPart += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[14].Text.ToString().Remove(0, 1));
                //        }
                //        else
                //        {
                //            impuPart += 0;
                //        }

                //    }
                //}

                DataTable dtTmpPart = new DataTable();
                dtTmpPart = (DataTable)Session["dtTmpPartFR"];

                foreach (DataRow itemRow in dtTmpPart.Rows)
                {
                  if (itemRow["docRegPartImpImpuEsp"].ToString().Trim() != "")
                    {
                        impuPart += Convert.ToDouble(itemRow["docRegPartImpImpuEsp"].ToString().Remove(0, 1));
                    }
                    else
                    {
                        impuPart += 0;
                    }
                }

                varTotalImp.Text = impuPart.ToString("C", nfi);

            }
            else if (varTotalTag.Text == "Descuento(s)")
            {

                double totDescuentos = 0;

                foreach (DataListItem dliDesc in dt_descuentos.Items)
                {
                    var dscPorcent = dliDesc.FindControl("rlblDscPor") as RadLabel;

                    totDescuentos += Convert.ToDouble(dscPorcent.Text.ToString().Remove(0, 1));

                }
                //GRET
                //varTotalImp.Text = Math.Round(totDescuentos, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);
                totDescuentos = (double)TruncateDecimal((decimal)totDescuentos, 2);
                varTotalImp.Text = totDescuentos.ToString("C20", nfi).TrimEnd('0');

            }
            else if (varTotalTag.Text == "Impuesto(s)")
            {

                double totImpuestos = 0;

                foreach (DataListItem dliImpu in dt_impuestos.Items)
                {
                    var impPorcent = dliImpu.FindControl("rlblImpPor") as RadLabel;









                    totImpuestos += Convert.ToDouble(impPorcent.Text.ToString().Remove(0, 1));

                }
                //GRET
                //varTotalImp.Text = Math.Round(totImpuestos, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);
                totImpuestos = (double)TruncateDecimal((decimal)totImpuestos, 2);
                varTotalImp.Text = totImpuestos.ToString("C20", nfi).TrimEnd('0');

            }





        }


    }

    private void calcTotales()
    {

        double acumulado = getSubTotal1();




        foreach (DataListItem dli in dt_totales.Items)
        {


            var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;

            var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
            var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;


            if (varTotalSig.Text == "=")
            {
                varTotalImp.Text = acumulado.ToString("C", nfi);
            }
            else if (varTotalSig.Text == "+")
            {
                acumulado += Convert.ToDouble(varTotalImp.Text.ToString().Remove(0, 1));
            }
            else if (varTotalSig.Text == "-")
            {
                acumulado -= Convert.ToDouble(varTotalImp.Text.ToString().Remove(0, 1));
            }

        }


    }

    private void setCustomDescArt()
    {
        DataTable dtTmpPart = new DataTable();
        dtTmpPart = (DataTable)Session["dtTmpPartFR"];

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Selected == true)
                {
                    dtTmpPart.Rows[i]["artDes"] = rTxtCustomDescArt.Text;
                }
            }

            rGdv_RegistoDetalle.DataSource = dtTmpPart;
        }

        Session["dtTmpPartFR"] = dtTmpPart;

    }

    private void ejecutaSpValidar(Int64 docRegId, string docCveVal)
    {

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRODoc";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, docRegId);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, docCveVal);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            
        }
        catch (Exception ex)
        {
            ShowAlert("2",ex.ToString());
        }
        
    }
    
    private void EjecutaAccionDetalle()
    {

        string sMSGTip = "";

        string msgValidacion = validaEjecutaAccionDetalle(ref sMSGTip);
        if (msgValidacion == "")
        {   
		
		//GRET
        //Se agrega llamada a metodo para establecer descuentos 

            if (rCboDescuento.SelectedIndex != -1)
            {
                EjecutaAplicaDescuento();
                getDescuentos(rCboDocumento.SelectedValue);
                loadLayTotales();
                EjecutaSpAccionEliminar();
            }

        //

            EjecutaAccionDetalle_Part();
            reSetTotales();
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private void EjecutaAccionDetalle_Part()
    {
        if (rCboAlmacen.Visible == true && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            addNewPart(rCboArticulo.SelectedValue);
        }
        else if (rCboAlmacen.Visible == false && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() && getDocConfig(rCboDocumento.SelectedValue, 5) == "2")
        {
            addNewPart(rCboArticulo.SelectedValue);
        }
        else if (rCboAlmacen.Visible == false && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() && getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
        {
            rutinaLoka();
        }
        else if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            ediNewPart();
        }
        else if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            delNewPart();
        }

        hdfBtnAccionP.Value = "";

        ControlesAcciconDet();
        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_RegistoDetalle.AllowMultiRowSelection = true;

    }
    
    private void fillCboArticulos()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            FnCtlsFillIn.RadComboBox(ref rCboArticulo, ds, "artCve", "artDes", true, false, "");
            ((Literal)rCboArticulo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboArticulo.Items.Count);

        }
        catch (Exception)
        {
            
        }

    }

    private void sitPer()
    {
        try
        {
            DataSet ValPeriodo = new DataSet();
            ValPeriodo = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, Convert.ToDateTime(rFchDoc.SelectedDate));

            string maMSGTip = ValPeriodo.Tables[0].Rows[0]["maMSGTip"].ToString().Trim();
            string maMSGDes = ValPeriodo.Tables[0].Rows[0]["maMSGDes"].ToString().Trim();

            if (maMSGTip != "1")
            {
                //DateTime sysFecha = DateTime.Today;
                rFchDoc.SelectedDate = null;
                rFchDoc.DataBind();

                ShowAlert(maMSGTip, maMSGDes);
                enableUI(1);

                rBtnGuardar.Enabled = false;

            }
            else
            {
                if (rCboDocumento.SelectedValue != "" && rFchDoc.Text != "")
                {
                    enableUI(2);
                }

                rBtnGuardar.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    //GRET
    //Se agrega metodo para llenado de combo descuentos
    public bool RabComboBox_Descuentos(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Descuentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "DesCve", "DesDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    //

    //GRET
    //Se agrega metodo para asignacion de descuento en registro de documento y eliminacion de descuento en BD al guardar el documento 

    private void EjecutaAplicaDescuento()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Descuento_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rCboDescuento.SelectedValue);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

               // ShowAlert(sEjecEstatus, sEjecMSG);
                //if (sEjecEstatus == "1")
                //{
                //    hdfBtnAccion.Value = "";
                //    InicioPagina();
                //}
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
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Descuento_Documentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input,3);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rCboDescuento.SelectedValue.ToString());
            //ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
            //ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, dataItem["DesCve"].Text);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //if (FnValAdoNet.bDSIsFill(ds))
            //{

            //    EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            //    if (EstatusItemsElim == "1")
            //    {
            //        CantItemsElimTrue += 1;
            //        MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            //    }
            //    else
            //    {
            //        CantItemsElimFalse += 1;
            //        MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            //    }

            //    MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            //}
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }

    //

    #endregion

    #region FUNCIONES
        //GRET, Se agrega funcion para truncar decimales y corregir error en timbrado por descuadre en importes de decuentos e Impuestos
    public decimal TruncateDecimal(decimal value, int precision)
    {
        decimal step = (decimal)Math.Pow(10, precision);
        decimal tmp = Math.Truncate(step * value);
        return tmp / step;
    }

    private string getOpcCFDI()
    {
        string response = "";

        //Obtenemos etiqueta descuentos

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGeneraCFDI";

        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            response = ds.Tables[0].Rows[0]["docGenCFDIOpc"].ToString();
        }
        else
        {
            response = "1";
        }

        return response;

    }

    //MASH

    private string getIncoterm()
    {
        string response = "";

        //Obtenemos etiqueta descuentos

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoIncoterm";

        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            response = ds.Tables[0].Rows[0]["docIncoterm"].ToString();
        }
        else
        {
            response = "0";
        }

        return response;

    }
    protected string getDocConfig(string docCve, int opc)
    {
        string response = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, docCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (opc == 1)
            {
                response = ds.Tables[0].Rows[0]["docFolTip"].ToString();
            }
            else if (opc == 2)
            {
                response = ds.Tables[0].Rows[0]["folVal"].ToString();
            }
            else if (opc == 3)
            {
                response = ds.Tables[0].Rows[0]["docReqAut"].ToString();
            }
            else if (opc == 4)
            {
                response = ds.Tables[0].Rows[0]["docDescGlb"].ToString();
            }
            else if (opc == 5)
            {
                response = ds.Tables[0].Rows[0]["docValExis"].ToString();
            }
            
            //GRET
            //Validacion para hacer visible al combo Descuento si el documento maneja descuentos
            if (ds.Tables[0].Rows[0]["docDescGlb"].ToString() == "2")
            {
                tdrcboDescuento.Visible = true;
                lblrcboDescuento.Visible = true;
            }
            else
            {
                tdrcboDescuento.Visible = false;
                lblrcboDescuento.Visible = false;
            }
            //
        }

        return response;

    }

    private bool chkSitOpcFormat(string docCve, string docOpcPantCve)
    {
        string result;


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoFormatoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, docCve);
        ProcBD.AgregarParametrosProcedimiento("@DocOpcPantCve", DbType.String, 15, ParameterDirection.Input, docOpcPantCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = ds.Tables[0].Rows[0]["docFormRegSit"].ToString();

            if (result == "1")
            {
                return true;
            }
            else if (result == "2")
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
    
    private bool chkExiGenerales()
    {

        try
        {


            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ValidarExistencias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ExiTotal"].ToString() != "")
                {
                    if (Convert.ToDecimal(rTxtPartCant.Text) <= Convert.ToDecimal(ds.Tables[0].Rows[0]["ExiTotal"].ToString()))
                    {
                        //"Si hay existencias"
                        return true;

                    }
                    else
                    {

                        //ShowAlert("2", "No hay existencias suficientes para el articulo seleccionado");
                        return false;
                    }
                }else
                {
                    return false;
                }

                
            }
            else
            {

                //ShowAlert("2", "No hay existencias en Ningun Almacen");
                return false;
            }


        }
        catch (Exception ex)
        {
            
            return false;
        }


    }

    //private bool ValidarExistencias()
    //{

    //    try
    //    {
            
    //        DataSet ds = new DataSet();

    //        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //        ProcBD.NombreProcedimiento = "sp_ValidarExistencias";
    //        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
    //        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);
    //        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 20, ParameterDirection.Input, rCboAlmacen.SelectedValue);
    //        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            if (Convert.ToDecimal(rTxtPartCant.Text) <= Convert.ToDecimal(ds.Tables[0].Rows[0]["ExiCantidad"].ToString()))
    //            {
    //                return true;
    //            }
    //            else
    //            {

    //                //ShowAlert("2", "No hay existencias suficientes para el articulo seleccionado");
    //                return false;
    //            }
    //        }
    //        else
    //        {

    //            //ShowAlert("2", "No hay existencias en el Almacen Seleccionado");
    //            return false;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
            
    //        return false;
    //    }

    //}

    private bool ValidarExistencias()
    {

        try
        {

            decimal ediCant = 0;
            decimal cantEdi = 0;

            string rowAlm = "";

            int indexTmpAlm = 0;

            DataTable dtTmpAlm = new DataTable();
            dtTmpAlm = (DataTable)Session["dtTmpAlmFR"];

            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                foreach (GridDataItem item in rGdv_RegistoDetalle.Items)
                {

                    if (item.Selected == true)
                    {

                        break;
                    }

                    indexTmpAlm++;

                }



                var dataItem = rGdv_RegistoDetalle.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    
                    if (dataItem["docRegPartCant"].Text != "&nbsp;")
                    {
                        cantEdi = Convert.ToDecimal( dataItem["docRegPartCant"].Text);

                        //como obtener el almacen de la tabla temporal

                        //rowAlm = dataItem["almCve"].Text;
                        rowAlm = dtTmpAlm.Rows[indexTmpAlm]["almTmp"].ToString();
                    }
                    
                }
            }

            if (cantEdi != 0 && rowAlm == rCboAlmacen.SelectedValue)
            {
                ediCant = Convert.ToDecimal(rTxtPartCant.Text) - cantEdi;
            }
            else if(cantEdi != 0 && rowAlm == dtTmpAlm.Rows[indexTmpAlm]["almTmp"].ToString() && rCboAlmacen.Visible == false)
            {
                ediCant = Convert.ToDecimal(rTxtPartCant.Text) - cantEdi;
            }
            else
            {
                ediCant = Convert.ToDecimal(rTxtPartCant.Text);
            }
            
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ValidarExistencias";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rCboArticulo.SelectedValue);


            if (rCboAlmacen.Visible)
            {
                ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 20, ParameterDirection.Input, rCboAlmacen.SelectedValue);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 20, ParameterDirection.Input, dtTmpAlm.Rows[indexTmpAlm]["almTmp"].ToString());
            }
            
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ediCant <= Convert.ToDecimal(ds.Tables[0].Rows[0]["ExiCantidad"].ToString()))
                {
                    return true;
                }
                else
                {

                    //ShowAlert("2", "No hay existencias suficientes para el articulo seleccionado");
                    return false;
                }
            }
            else
            {

                //ShowAlert("2", "No hay existencias en el Almacen Seleccionado");
                return false;
            }


        }
        catch (Exception ex)
        {

            return false;
        }

    }


    private bool AutorizacionArt(string artCve)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, artCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {

            if (ds.Tables[0].Rows[0]["artManPart"].ToString() != "5")
            {

                return true;

            }
            else
            {

                return false;
            }

        }
        else
        {
            return false;
        }

    }

    private double clcDescGlbl(double impBruto)
    {

        //Obtener Importe Bruto

        double impAcumulado;
        double totDescuentos = 0;
        double totImpuestos = 0;

        double tasaDesc;
        double tasaImpu;

        double result;



        //Descuentos

        foreach (DataListItem dli in dt_descuentos.Items)
        {
            var dscTasa = dli.FindControl("rlblDscTas") as RadLabel;

            tasaDesc = Convert.ToDouble(dscTasa.Text);

            result = impBruto * tasaDesc / 100;

            var dscPorcent = dli.FindControl("rlblDscPor") as RadLabel;


            //dscPorcent.Text = result.ToString();

            //dscPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);

            impAcumulado = impBruto - result;

            impBruto = impAcumulado;

            totDescuentos += result;

            //rLblImpSubTotal.Text = impAcumulado.ToString();

            //rLblImpSubTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }


        return totDescuentos;

    }

    private double clcImpuGlbl(double impBruto)
    {
        
        //Obtener Importe Bruto

        double impAcumulado;
        double totDescuentos = 0;
        double totImpuestos = 0;

        double tasaDesc;
        double tasaImpu;

        double result;



        //Descuentos

        foreach (DataListItem dli in dt_impuestos.Items)
        {
            var dscTasa = dli.FindControl("rlblImpTas") as RadLabel;

            tasaDesc = Convert.ToDouble(dscTasa.Text);

            result = impBruto * tasaDesc / 100;

            var dscPorcent = dli.FindControl("rlblImpPor") as RadLabel;


            //dscPorcent.Text = result.ToString();

            //dscPorcent.Text = Math.Round(result, 2, MidpointRounding.AwayFromZero).ToString("C", nfi);

            impAcumulado = impBruto - result;

            impBruto = impAcumulado;

            totDescuentos += result;

            //rLblImpSubTotal.Text = impAcumulado.ToString();

            //rLblImpSubTotal.Text = Math.Round(impAcumulado, 2, MidpointRounding.AwayFromZero).ToString();

        }
        //GRET
        totDescuentos = (double)TruncateDecimal((decimal)totDescuentos, 2);

        return totDescuentos;

    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //hdfBtnAccion.Value = "1";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rCboDocumento.SelectedValue == "")
            {
                rCboDocumento.CssClass = "cssTxtInvalid";
                rCboDocumento.BorderWidth = Unit.Pixel(1);
                rCboDocumento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboCliente.SelectedValue != "")
            {
                rCboCliente.BorderColor = System.Drawing.Color.Transparent;

                //if (rCboDireccionEntrega.SelectedValue == "")
                //{
                //    rCboDireccionEntrega.CssClass = "cssTxtInvalid";
                //    rCboDireccionEntrega.BorderWidth = Unit.Pixel(1);
                //    rCboDireccionEntrega.BorderColor = System.Drawing.Color.Red;
                //    camposInc += 1;
                //}
                //else
                //{
                //    rCboDireccionEntrega.BorderColor = System.Drawing.Color.Transparent;
                //}

            }
            else
            {
                rCboCliente.CssClass = "cssTxtInvalid";
                rCboCliente.BorderWidth = Unit.Pixel(1);
                rCboCliente.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }


            if (rCboViaEmbarque.SelectedValue == "")
            {
                rCboViaEmbarque.CssClass = "cssTxtInvalid";
                rCboViaEmbarque.BorderWidth = Unit.Pixel(1);
                rCboViaEmbarque.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboViaEmbarque.BorderColor = System.Drawing.Color.Transparent;
            }

            if (rCboMoneda.SelectedValue == "")
            {
                rCboMoneda.CssClass = "cssTxtInvalid";
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
            }

            if (rTxtFolio.Text == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
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
            if (rCboDocumento.SelectedValue == "")
            {
                rCboDocumento.CssClass = "cssTxtInvalid";
                rCboDocumento.BorderWidth = Unit.Pixel(1);
                rCboDocumento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboDocumento.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboCliente.SelectedValue != "")
            {
                rCboCliente.BorderColor = System.Drawing.Color.Transparent;

                //if (rCboDireccionEntrega.SelectedValue == "")
                //{
                //    rCboDireccionEntrega.CssClass = "cssTxtInvalid";
                //    rCboDireccionEntrega.BorderWidth = Unit.Pixel(1);
                //    rCboDireccionEntrega.BorderColor = System.Drawing.Color.Red;
                //    camposInc += 1;
                //}
                //else
                //{
                //    rCboDireccionEntrega.BorderColor = System.Drawing.Color.Transparent;
                //}

            }
            else
            {
                rCboCliente.CssClass = "cssTxtInvalid";
                rCboCliente.BorderWidth = Unit.Pixel(1);
                rCboCliente.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }


            if (rCboViaEmbarque.SelectedValue == "")
            {
                rCboViaEmbarque.CssClass = "cssTxtInvalid";
                rCboViaEmbarque.BorderWidth = Unit.Pixel(1);
                rCboViaEmbarque.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboViaEmbarque.BorderColor = System.Drawing.Color.Transparent;
            }

            if (rCboMoneda.SelectedValue == "")
            {
                rCboMoneda.CssClass = "cssTxtInvalid";
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rTxtFolio.Text == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            //if (gdv_UnidadMed.SelectedItems.Count == 0)
            //{
            //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            //    return sResult;
            //}

            return sResult;
        }

        return sResult;
    }
    
    private decimal getImpTotal(string clave)
    {
        decimal result = 0;

        foreach (DataListItem dli in dt_totales.Items)
        {
            var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;
            var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
            var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;

            if (varTotalTag.Text == clave)
            {
                result = Convert.ToDecimal(varTotalImp.Text.Remove(0, 1));
            }


        }

        return result;

    }

    private string getAgent(string ageTip)
    {
        string ageCve = "";
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAgentes";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 10, ParameterDirection.Input, rCboCliente.SelectedValue);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ageTip == "1")
                    {
                        ageCve = ds.Tables[0].Rows[0]["ageCveVen"].ToString();
                    }
                    else if (ageTip == "2")
                    {
                        ageCve = ds.Tables[0].Rows[0]["ageCveCob"].ToString();
                    }
                    else
                    {
                        ageCve = "";
                    }
                }

            }
            
        }
        else
        {
            ageCve = "";
        }


        return ageCve;
    }

    private string getPagDias()
    {
        string response = "";

        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                response = ds.Tables[0].Rows[0]["conPagCve"].ToString();
            }
        }

        return response;
    }

    private string getSatMetPag()
    {
        string response = "";

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rCboMetodoPago.SelectedValue = ds.Tables[0].Rows[0]["satMetPagCve"].ToString();
                    response = ds.Tables[0].Rows[0]["satMetPagCve"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            response = "";
        }

        return response;

    }

    private bool valVigenciaLst(string lisPreCve)
    {

        try
        {
            DataSet ds = new DataSet();

            string pDocFec = "";

            pDocFec += rFchDoc.SelectedDate.Value.Year.ToString() + "/" + rFchDoc.SelectedDate.Value.Month.ToString().PadLeft(2, '0') + "/" + rFchDoc.SelectedDate.Value.Day.ToString().PadLeft(2, '0');

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, lisPreCve);
            ProcBD.AgregarParametrosProcedimiento("@RegDocFec", DbType.String, 100, ParameterDirection.Input, pDocFec);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {

                //ShowAlert("2", "No hay existencias suficientes para el articulo seleccionado");
                return true;

            }
            else
            {

                //ShowAlert("2", "No hay existencias en el Almacen Seleccionado");
                return false;
            }



        }
        catch (Exception ex)
        {
            
            return false;
        }

    }
    
    private double getSubTotal1()
    {
        double acum = 0;

        if (rGdv_RegistoDetalle.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
            {

                if (rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString() != "&nbsp;")
                {
                    acum += Convert.ToDouble(rGdv_RegistoDetalle.Items[i].Cells[11].Text.ToString().Remove(0, 1));
                }
                else
                {
                    acum += 0;
                }

            }
        }

        return acum;

    }

    private double getSubTotales(int opc)
    {
        double bace = 0;

        if (opc == 1)
        {
            foreach (DataListItem dli in dt_totales.Items)
            {

                var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;
                var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
                var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;

                //Recuperar Primer Subtotal

                if (varTotalTag.Text == "SubTotal Bruto")
                {
                    bace += Convert.ToDouble(varTotalImp.Text.ToString().Remove(0, 1));
                }
                else if (varTotalTag.Text == getEtiquetasPartida(1))
                {
                    bace -= Convert.ToDouble(varTotalImp.Text.ToString().Remove(0, 1));
                }
            }


        }
        else if (opc == 2)
        {

            foreach (DataListItem dli in dt_totales.Items)
            {

                var varTotalTag = dli.FindControl("rlblTotalTag") as RadLabel;
                var varTotalSig = dli.FindControl("rlblTotalSig") as RadLabel;
                var varTotalImp = dli.FindControl("rlblTotalImp") as RadLabel;

                //Recuperar Primer Subtotal

                if (varTotalTag.Text == "SubTotal después de Descuentos")
                {
                    bace += Convert.ToDouble(varTotalImp.Text.ToString().Remove(0, 1));
                }
            }


        }

        return bace;

    }
    

    private string validaEjecutaAccionDetalle(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //Modificar
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rGdv_RegistoDetalle.SelectedItems.Count == 0 && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }


            //Validar Primero seleccion de Articulo
            if (rCboArticulo.SelectedValue == "")
            {

                rCboArticulo.CssClass = "cssTxtInvalid";
                rCboArticulo.BorderWidth = Unit.Pixel(1);
                rCboArticulo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;

            }
            else
            {
                rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }


            // Obtener Valor de Definicion de Documentos
            if (getDocConfig(rCboDocumento.SelectedValue, 5) == "1")
            {

                //Comprobar Seleccion de Controles
                if (rCboAlmacen.Visible == true && rCboAlmacen.SelectedIndex != -1)
                {
                    //Validar Definicion del Articulo
                    if (AutorizacionArt(rCboArticulo.SelectedValue) == true)
                    {
                        //Ahora si a Validar existencias
                        if (ValidarExistencias() == false)
                        {
                            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1025", ref sMSGTip, ref sResult);
                            return sResult;
                        }
                    }
                }
                else if (rCboAlmacen.Visible == false && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                {

                    if (chkExiGenerales() == false)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1026", ref sMSGTip, ref sResult);
                        return sResult;
                    }
                }
                else if (rCboAlmacen.Visible == false && hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()
                    )
                {

                    //Ahora si a Validar existencias
                    if (ValidarExistencias() == false)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1025", ref sMSGTip, ref sResult);
                        return sResult;
                    }


                }
                else
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1027", ref sMSGTip, ref sResult);

                    //Validar Primero seleccion de Articulo
                    if (rCboAlmacen.SelectedValue == "")
                    {
                        rCboAlmacen.CssClass = "cssTxtInvalid";
                        rCboAlmacen.BorderWidth = Unit.Pixel(1);
                        rCboAlmacen.BorderColor = System.Drawing.Color.Red;
                    }

                    return sResult;
                }


            }


        }


        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_RegistoDetalle.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

        }


        return sResult;

    }

    #endregion




    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            
            rCboArticulo.ClearSelection();
            rCboAlmacen.ClearSelection();
            rCboIncoterm.ClearSelection();  
            rTxtPartLote.Text = "";
            rTxtPartSerie.Text = "";


            rFchEntPart.Text = "";
            rTxtPartCant.Text = "";
            rTxtPartPrec.Text = "";

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }

        //MODIFICAR
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarRegistro.png";


            rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
            
            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;
            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;

            rCboArticulo.ClearSelection();
            rCboAlmacen.ClearSelection();
            rTxtPartLote.Text = "";
            rTxtPartSerie.Text = "";
            rFchEntPart.Text = "";
            rTxtPartCant.Text = "";
            rTxtPartPrec.Text = "";

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

            rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_RegistoDetalle.AllowMultiRowSelection = true;
            hdfBtnAccionP.Value = "";
        }
        //MODIFICAR DESCRIPCION
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ModificarDet).ToString())
        {
            rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarRegistro.png";


            rCboArticulo.BorderColor = System.Drawing.Color.Transparent;

            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;
            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;

            rCboArticulo.ClearSelection();
            rCboAlmacen.ClearSelection();
            rFchEntPart.Text = "";
            rTxtPartCant.Text = "";
            rTxtPartPrec.Text = "";
            rTxtPartLote.Text = "";
            rTxtPartSerie.Text = "";

            frmNewPartida.Visible = true;
            frmCustomDescArt.Visible = false;
            frmNewPartidaTextil.Visible = false;

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;

            rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_RegistoDetalle.AllowMultiRowSelection = true;
            hdfBtnAccionP.Value = "";
        }

        //ELIMINAR
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_RegistoDetalle.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_RegistoDetalle, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_RegistoDetalle, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //MODIFICAR DESCRIPCION
        if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ModificarDet).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_RegistoDetalle, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }
    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();

                
                rCboArticulo.Enabled = true;
                rCboAlmacen.Enabled = true;
                rFchEntPart.Enabled = true;
                rTxtPartCant.Enabled = true;
                rTxtPartLote.Enabled = true;
                rTxtPartSerie.Enabled = true;
                // rTxtPartPrec.Enabled = true;

                rCboArticulo.ClearSelection();
                rCboAlmacen.ClearSelection();
                rFchEntPart.Text = "";
                rTxtPartCant.Text = "1";
                rTxtPartPrec.Text = "";
                rTxtPartLote.Text = "";
                rTxtPartSerie.Text = "";

                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.None;
                }

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_RegistoDetalle.AllowMultiRowSelection = false;
                rCboArticulo.Enabled = false;
                rCboAlmacen.Enabled = true;
                rFchEntPart.Enabled = true;
                rTxtPartCant.Enabled = true;
                rTxtPartLote.Enabled = true;
                rTxtPartSerie.Enabled = true;
                // rTxtPartPrec.Enabled = true;


                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMINAR
            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                delNewPart();

                rCboArticulo.Enabled = false;
                rCboAlmacen.Enabled = false;
                rFchEntPart.Enabled = false;
                rTxtPartCant.Enabled = false;
                rTxtPartPrec.Enabled = false;
                rTxtPartLote.Enabled = false;
                rTxtPartSerie.Enabled = false;


                rCboArticulo.ClearSelection();
                rCboAlmacen.ClearSelection();
                rFchEntPart.Text = "";
                rTxtPartCant.Text = "";
                rTxtPartPrec.Text = "";
                rTxtPartLote.Text = "";
                rTxtPartSerie.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;

                rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_RegistoDetalle.AllowMultiRowSelection = true;

                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }

            }

            //MODIFICAR DESCRICION
            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ModificarDet).ToString())
            {

                rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarRegistroSelected.png";

                //rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();
                rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_RegistoDetalle.AllowMultiRowSelection = false;

                for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
                {
                    this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }

                if (frmNewPartida.Visible)
                {
                    frmNewPartida.Visible = false;
                }


                frmNewPartida.Visible = false;
                frmCustomDescArt.Visible = true;
                frmNewPartidaTextil.Visible = false; 

            }

            //LIMPIAR
            if (hdfBtnAccionP.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_RegistoDetalle.AllowMultiRowSelection = true;
                rGdv_RegistoDetalle.MasterTableView.ClearSelectedItems();

                rCboArticulo.Enabled = false;
                rCboAlmacen.Enabled = false;
                rFchEntPart.Enabled = false;
                rTxtPartCant.Enabled = false;
                rTxtPartPrec.Enabled = false;
                rTxtPartLote.Enabled = false;
                rTxtPartSerie.Enabled = false;



                rCboArticulo.ClearSelection();
                rCboAlmacen.ClearSelection();
                rFchEntPart.Text = "";
                rTxtPartCant.Text = "";
                rTxtPartPrec.Text = "";
                rTxtPartLote.Text = "";
                rTxtPartSerie.Text = "";
            }
        }


        if (Result == false)
        {
            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;
            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;


            rCboArticulo.ClearSelection();
            rCboAlmacen.ClearSelection();
            rFchEntPart.Text = "";
            rTxtPartCant.Text = "";
            rTxtPartPrec.Text = "";
            rTxtPartLote.Text = "";
            rTxtPartSerie.Text = "";

            rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_RegistoDetalle.AllowMultiRowSelection = true;

        }


    }
    private void ControlesAcciconDet()
    {
        string sMSGTip = "";
        string msgValidacion = "";
        //rstFrmNewPart();
        //===> CONTROLES GENERAL
        this.rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnEditDesc.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarRegistro.png";
        rBtnNuevoPartidaTexil.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rCboArticulo.BorderColor = System.Drawing.Color.Transparent;

        rCboArticulo.Enabled = false;
        rCboAlmacen.Enabled = false;
        rFchEntPart.Enabled = false;
        rTxtPartCant.Enabled = false;
        rTxtPartPrec.Enabled = false;
        rTxtPartLote.Enabled = false;
        rTxtPartSerie.Enabled = false;

        frmNewPartida.Visible = true;
        frmCustomDescArt.Visible = false;
        frmNewPartidaTextil.Visible = false; 

        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;

        rGdv_RegistoDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_RegistoDetalle.AllowMultiRowSelection = true;

        for (int i = 0; i < rGdv_RegistoDetalle.Items.Count; i++)
        {
            this.rGdv_RegistoDetalle.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }



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
        if (hdfBtnAccionP.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccionP.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccionP.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccionP.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rCboArticulo.Enabled = false;
            rCboAlmacen.Enabled = false;
            rFchEntPart.Enabled = false;
            rTxtPartCant.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rTxtPartLote.Enabled = false;
            rTxtPartSerie.Enabled = false;

            rCboArticulo.ClearSelection();
            rCboAlmacen.ClearSelection();
            rFchEntPart.Text = "";
            rTxtPartCant.Text = "";
            rTxtPartPrec.Text = "";

            rTxtPartLote.Text = "";
            rTxtPartSerie.Text = "";
        }
    }
    private bool ValidaCodigoAlt() {
       
        string sCveBusqueda = "";
        if (rCboArticulo.SelectedIndex != -1)
        {
            sCveBusqueda = rCboArticulo.SelectedValue;
        }
        else
        {
            sCveBusqueda = rCboArticulo.Text;
        }

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 62);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CveBusqueda", DbType.String, 20, ParameterDirection.Input, sCveBusqueda);
        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboLstPrecios.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSIsFill(ds))
        {
            string ValCombo = ds.Tables[0].Rows[0]["artCve"].ToString();
            rCboArticulo.ClearSelection();
            rCboArticulo.SelectedValue = ValCombo;
            rTxtPartCant.Text = "1";
            return true;
        }
        else
        {
            rCboArticulo.Text = "";
            rTxtPartPrec.Text = "";
            string sMSGTip = "", sResult = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1040", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
            return false;
        }

    }
    private string ParListadePrecios()
    {
        string result;
        string response = "";

        result = FNParam.sParametroValInt(Pag_sConexionLog, Pag_sCompania, "LPRCVE", 1);


        //LISTA DEFAULT
        if (result == "1")
        {
            //Recuperar de Parametros Generales, lista de precios Default
            ParListadePreciosDefault();

            rLblLstPreciosTag.Visible = false;
            rCboLstPrecios.Visible = false;
            rCboLstPrecios.Enabled = false;
            response = "DEFAULT";

        }
        //LISTA POR DOCUMENTO
        else if (result == "2")
        {
            rCboLstPrecios.Visible = true;
            rLblLstPreciosTag.Visible = true;
            rTxtPartPrec.Enabled = false;
            
            //LlenaComboLstPrecios(51);
            response = "DOC";

           ManejodePrecios();

        }
        //LISTA POR AGRUPCION
        else if (result == "3")
        {
            rLblLstPreciosTag.Visible = true;
            rCboLstPrecios.Visible = true;
            response = "AGR";
            LlenaComboLstPrecios(52);
            ManejodePrecios();
        }
        else
        {
            rCboLstPrecios.Enabled = false;
        }

        return response;
    }
    private void ParListadePreciosDefault()
    {
        FnCtlsFillIn.RabComboBox_ListaPrecios(Pag_sConexionLog, Pag_sCompania, ref rCboLstPrecios, true, false, "");
        string response = "";

        response = FNParam.sParametroValInt(Pag_sConexionLog, Pag_sCompania, "LPRCVE", 2);

        foreach (RadComboBoxItem item in rCboLstPrecios.Items)
        {
            if (item.Value == response)
            {
                rCboLstPrecios.SelectedValue = item.Value;
            }
        }
    }
    private void ParListadePreciosPorDocumento()
    {
        if (rCboDocumento.SelectedValue != "" && rCboMoneda.SelectedValue != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 62);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    FnCtlsFillIn.RadComboBox(ref this.rCboLstPrecios, ds, "lisPreCve", "lisPreDes", false, false);
                    ((Literal)rCboLstPrecios.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboLstPrecios.Items.Count);
                    rCboLstPrecios.SelectedValue = ds.Tables[0].Rows[0]["lisPreCve"].ToString();
                    pnlBtnsAcciones.Enabled = true;
                    fillCboArticulos();
                }
                else
                {
                   
                    string sMSGTip = "", sResult = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1041", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                    rCboLstPrecios.ClearSelection();
                    rCboLstPrecios.Items.Clear();
                    
                    ((Literal)rCboLstPrecios.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboLstPrecios.Items.Count);
                    //rCboLstPrecios.Enabled = false;
                    pnlBtnsAcciones.Enabled = false;
                }
            }
            else
            {
                string sMSGTip = "", sResult = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1041", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);
                pnlBtnsAcciones.Enabled = false;
            }
        }

    }
    private void LlenaComboLstPrecios(Int64 opc)
    {
        if (rCboCliente.SelectedValue != "" && rCboMoneda.SelectedValue != "")
        {
            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ListaPrecios";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                //ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rCboDocumento.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, rCboCliente.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);

                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
               
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        FnCtlsFillIn.RadComboBox(ref this.rCboLstPrecios, ds, "lisPreCve", "lisPreDes", false, false);
                        ((Literal)rCboLstPrecios.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboLstPrecios.Items.Count);
                        rCboLstPrecios.SelectedValue = ds.Tables[0].Rows[0]["lisPreCve"].ToString();
                        pnlBtnsAcciones.Enabled = true;
                        fillCboArticulos();
                    }
                    else
                    {
                        string sMSGTip = "", sResult = "";
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1042", ref sMSGTip, ref sResult);
                        ShowAlert(sMSGTip, sResult);
                        rCboLstPrecios.ClearSelection();
                        rCboLstPrecios.Items.Clear();
                        ((Literal)rCboLstPrecios.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboLstPrecios.Items.Count);
                        //rCboLstPrecios.Enabled = false;Footer
                        pnlBtnsAcciones.Enabled = false;
                    }
                }
                else
                {
                    string sMSGTip = "", sResult = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1042", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                    pnlBtnsAcciones.Enabled = false;
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
    private string getEtiquetasPartida(int opc)
    {
        string response = "";

        if (opc == 1)
        {
            response = FNParam.sParametroValStr(Pag_sConexionLog, Pag_sCompania, "DOCREG", 1);
            if (rGdv_RegistoDetalle.Columns[10].HeaderText != response)
            {
                rGdv_RegistoDetalle.Columns[10].HeaderText = response;
                rGdv_RegistoDetalle.DataBind();
            }
        }
        else if (opc == 2)
        {
            response = FNParam.sParametroValStr(Pag_sConexionLog, Pag_sCompania, "DOCREG", 2);
            if (rGdv_RegistoDetalle.Columns[12].HeaderText != response)
            {
                rGdv_RegistoDetalle.Columns[12].HeaderText = response;
                rGdv_RegistoDetalle.DataBind();
            }
        }

        return response;
    }
    private string ParFormatodeFecha()
    {
        string response = "";

        response = FNParam.sParametroValStr(Pag_sConexionLog, Pag_sCompania, "DOCREG", 3);

        return response;
    }
    private void ParManejaDescuentos()
    {
        string result;

        result = FNParam.sParametroValInt(Pag_sConexionLog, Pag_sCompania, "DESCVE", 1);

        if (result == "1")
        {
            dt_descuentos.Visible = true;
        }
        else if (result == "2")
        {
            dt_descuentos.Visible = false;
        }
        else
        {
            dt_descuentos.Visible = false;
        }

    }
    private void ManejodePrecios()
    {

        string response;

        response = FNParam.sParametroValInt(Pag_sConexionLog, Pag_sCompania, "PRECVE", 1);

        //OPCION DESACTIVADA DEJA DE FUNCIONA -OPCION DE PARAMETRO -CAPTURA-
        //if (response == "1")
        //{
        //    rCboLstPrecios.Visible = false;
        //    rLblLstPreciosTag.Visible = false;
        //    //rTxtPartPrec.Enabled = true;

        //}
        //else
        if (response == "2")
        {
            rLblLstPreciosTag.Visible = false;
            rCboLstPrecios.Visible = false;
            rCboLstPrecios.Enabled = false;

        }
        else if (response == "3")
        {
            rLblLstPreciosTag.Visible = true;
            rCboLstPrecios.Visible = true;
            rCboLstPrecios.Enabled = true;


        }
        else
        {
            rLblLstPreciosTag.Visible = false;
            rCboLstPrecios.Visible = false;
            //rTxtPartPrec.Enabled = true;
        }

    }




    /////////////////////////  CARGA DE DATALIST  ////////////////////////////////////


     
    private DataSet cargaDatalistds(Int32 revatip, string docRegId) {
    
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, revatip);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.String, 10, ParameterDirection.Input, docRegId);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (revatip == 1)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                divRef.Visible = true;
            }
            else
            {
                divRef.Visible = false;
            }
        }


        if (revatip == 2)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                divVar.Visible = true;
            }
            else
            {
                divVar.Visible = false;
            }
        }



        return ds;
        
    }





    private void cargaDl(string docRegId) {
        DataListRef.DataSource = cargaDatalistds(1, docRegId);
        DataListRef.RepeatColumns = 2;
        DataListRef.DataBind();

        DataListVar.DataSource = cargaDatalistds(2, docRegId);
        DataListVar.RepeatColumns = 2;
        DataListVar.DataBind();
    }



    private void guardaDt(string docRegId) {

    

        foreach (DataListItem dli in DataListRef.Items)
        {
            Int32 secRef;

            var valRef = dli.FindControl("rTxt_listRef") as RadTextBox;
            secRef = dli.ItemIndex + 2;

            //if (valRef.Text.Trim() != "")
            //{
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoRegistroRefVar";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.String, 20, ParameterDirection.Input, docRegId);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);

            ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
            if (valRef.Text != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, valRef.Text);
            }
            ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //}


        }



         foreach (DataListItem dli in DataListVar.Items)
            {
                Int32 secRef;

                var valRef = dli.FindControl("rTxt_listVar") as RadNumericTextBox;
                secRef = dli.ItemIndex + 1;

                //if (valRef.Text.Trim() != "")
                //{

                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_DocumentoRegistroRefVar";

                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.String, 20, ParameterDirection.Input, docRegId);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                    ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 5, ParameterDirection.Input, rCboDocumento.SelectedValue);
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                    if (valRef.Text != "")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(valRef.Text));
                    }
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


                //}


            }


    }


//Se agrega nuevo metodo para guardar folio en registro de documentos
    private void guardaDtFolio(string docRegId)
    {
        DataSet ds = new DataSet();
        int revasec;
        revasec = consultaParamValFolio();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistroRefVar";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.String, 20, ParameterDirection.Input, docRegId);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);

        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
        if (rTxtFolio.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, rTxtFolio.Text.Trim());
        }
        ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, revasec);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    }


    private int consultaParamValFolio()
    {
        DataSet ds = new DataSet();
        int Resul = 0;


        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_parametrosDinamico";

        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "REFFOLIO");
        ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@parmValTip", DbType.String, 5, ParameterDirection.Input, "Int");

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Resul = Int32.Parse(ds.Tables[0].Rows[0]["parmValor"].ToString());
            }
        }
        return Resul;
    }

    #region FUNCIONES


    private bool bAplicaTextil() {
        bool bResult = false;


        if (rCboDocumento.SelectedIndex > -1)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoTextil";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rCboDocumento.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string iValor = ds.Tables[0].Rows[0]["docTextilApli"].ToString();
                    if (iValor == "1")
                    {
                        bResult = true;
                    }
                }
            }
        }


        tableBtnsAcciones_Texil.Visible = bResult; 
        return bResult;
    }


    private decimal PrecioArticulo_ListaPrecios(string sArtCve ,decimal dCantidad)
    {
        decimal Resul = 0;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ListaPreciosDetalle";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64 , 0, ParameterDirection.Input,rCboLstPrecios.SelectedValue );
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, sArtCve);
        ProcBD.AgregarParametrosProcedimiento("@Cantidad", DbType.String, 10, ParameterDirection.Input, dCantidad);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds !=  null) {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Resul = Convert.ToDecimal(ds.Tables[0].Rows[0]["lisPrecio"]);
            }
        }
        return Resul;
    }
    #endregion
    
    protected void rCboUsoCFDI_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
}