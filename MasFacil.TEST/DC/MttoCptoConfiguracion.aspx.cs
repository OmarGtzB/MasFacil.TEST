
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

public partial class DC_MttoCptoConfiguracion : System.Web.UI.Page
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
    private string folio_Selection;
    //private string conConfSec;
    private string listTipDatoCptoCve;



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

    // COMBOS
    protected void rCboTipoDato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        HabilitarControlesTipoDato();

    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnModificar.Enabled = true;
        rBtnLimpiar.Enabled = true;
        ControlesAccion();
 
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();

    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        this.rCboTipoDato.ClearSelection();
        this.rCboValidacion.ClearSelection();
        this.rCboPrograma.ClearSelection();
        rCboProgValida.ClearSelection();

        rCboProgValida.ClearSelection();
        rCboProgValida.Enabled = false;
        //this.rCboAgrupacion.ClearSelection();
        rGdvInformacion.MasterTableView.ClearSelectedItems();
        this.rTxtSolicitud.Text = "";
        this.rTxtRell.Text = "";
        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;
        this.rCboTipoDato.Enabled = false;
        this.rTxtSolicitud.Enabled = false;
        this.rCboValidacion.Enabled = false;
        this.rCboPrograma.Enabled = false;
        rCboProgValida.Enabled = false;
        this.rTxtRell.Enabled = false;
        this.rBtnDerecha.Enabled = false;
        this.rBtnIzquierda.Enabled = false;
        this.rBtnRequerido.Enabled = false;
        this.rBtnOpcional.Enabled = false;
        this.rBtnCancelar.Enabled = false;
        this.rBtnGuardar.Enabled = false;
        this.RadTxtPrgValid.Enabled = false;
        this.RadTxtSecuenc.Enabled = false;
        txtFormula.Enabled = false;
        txtFormula.Text = "";
        this.RadTxtPrgValid.Text = "";
        this.RadTxtSecuenc.Text = "";
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
    }
    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {

        bool rBtnRelleno11;
        bool rBtnTogImpDscS;

        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            
            rCboProgValida.ClearSelection();
            this.rCboPrograma.ClearSelection();
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

            }

            rTxtSolicitud.Text = dataItem["cptoConfDes"].Text;
            rCboTipoDato.SelectedValue = dataItem["listTipDatoCptoCve"].Text;
            this.RadTxtSecuenc.Text = Convert.ToString(dataItem["cptoConfSec"].Text);
            

            if (dataItem["cptoConfProgCve"].Text != "&nbsp;")
            {
                rCboPrograma.SelectedValue = Convert.ToString(dataItem["cptoConfProgCve"].Text);
            }else
            {
                rCboPrograma.ClearSelection();
            }

            if (dataItem["cptoConfProgCveVal"].Text != "&nbsp;")
            {
                rCboProgValida.SelectedValue = Convert.ToString(dataItem["cptoConfProgCveVal"].Text);
            }
            else
            {
                rCboProgValida.ClearSelection();
            }

            if (dataItem["cptoConfTipCap"].Text=="1")
            {
                rBtnRequerido.Checked = true;
                rBtnOpcional.Checked = false;
            }
            else if (dataItem["cptoConfTipCap"].Text == "2")
            {
                rBtnRequerido.Checked = false;
                rBtnOpcional.Checked = true;

            }
            if (dataItem["cptoConfJust"].Text == "1")
            {
                rBtnIzquierda.Checked = true;
                rBtnDerecha.Checked = false;
            }
            else if (dataItem["cptoConfJust"].Text == "2")
            {
                rBtnIzquierda.Checked = false;
                rBtnDerecha.Checked = true;

            }

            if (dataItem["cptoConfRell"].Text == "&nbsp;" || dataItem["cptoConfRell"].Text == " ")
            {
                rTxtRell.Text = "";
            }else
            {
                rTxtRell.Text = dataItem["cptoConfRell"].Text;
            }

            if (dataItem["cptoConfFormula"].Text == "&nbsp;" || dataItem["cptoConfFormula"].Text == " ")
            {
                txtFormula .Text = "";
            }
            else
            {
                txtFormula.Text = dataItem["cptoConfFormula"].Text;
            }






            //disEnableUi(2);

            HabilitarControlesTipoDato();
        }




        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            this.rCboValidacion.Enabled = false;
            this.rCboPrograma.Enabled = false;
            rCboProgValida.Enabled = false;
            rCboProgValida.ClearSelection();
            this.rCboPrograma.ClearSelection();
            //this.rCboAgrupacion.Enabled = false;
            this.rCboTipoDato.Enabled = false;
            this.rTxtSolicitud.Enabled = false;
            this.rTxtRell.Enabled = false;
            this.rBtnDerecha.Enabled = false;
            this.rBtnIzquierda.Enabled = false;
            this.rBtnRequerido.Enabled = false;
            this.rBtnOpcional.Enabled = false;
            this.rCboTipoDato.ClearSelection();
            this.rCboValidacion.ClearSelection();
            //this.rCboAgrupacion.ClearSelection();
            this.rTxtSolicitud.Text = "";
            this.rTxtRell.Text = "";
            this.rBtnDerecha.Checked = false;
            this.rBtnIzquierda.Checked = true;
            this.rBtnRequerido.Checked = true;
            this.rBtnOpcional.Checked = false;
            this.RadTxtSecuenc.Enabled = false;
            this.RadTxtPrgValid.Enabled = false;
            this.RadTxtSecuenc.Text = "";
            this.RadTxtPrgValid.Text = "";
            
        }
    }

    #endregion

    #region METODOS

    private void HabilitarControlesTipoDato() {

        string sClave = rCboTipoDato.SelectedValue;
        pnlPrograma.Enabled = true ;
        pnlFormula.Enabled = true;

        if (sClave == "Fact" || sClave == "Imp" || sClave == "Fec")
        {
            pnlPrograma.Enabled = false;
            rCboPrograma.ClearSelection();
            rCboProgValida.ClearSelection();
        }
        else
        {
            pnlFormula.Enabled = false;
            txtFormula.Text = "";
        }

        if (  sClave == "Fec")
        {
            pnlFormula .Enabled = false;
            txtFormula.Text = ""; 
        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        folio_Selection = Convert.ToString(Session["folio_Selection"]);
        listTipDatoCptoCve = Convert.ToString(Session["listTipDatoCptoCve"]);
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";

        ControlesAccion();

        llenaDatCpto();

        LlenaComboTiposDato();

        llenadata_Grid();

        ComboPrograma();

        ComboProgramaVal();

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
    private void llenaDatCpto()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds != null)
        {
            rLblCptoId.Text = folio_Selection;
            rLblcptoDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoDes"]);
        }
        else
        {

        }
    }
    private void disEnableUi(int opc)
    {
        //1 = New - A 
        if (opc == 1)
        {


        }
        else if (opc == 2) // Modificar Cliente
        {

            this.rCboValidacion.Enabled = true;
            this.rCboPrograma.Enabled = true;
            rCboProgValida.Enabled = true;
            //this.rCboAgrupacion.Enabled = true;
            this.rCboTipoDato.Enabled = true;
            this.rTxtSolicitud.Enabled = true;
            rBtnRequerido.Enabled = true;
            rBtnOpcional.Enabled = true;
            rBtnIzquierda.Enabled = true;
            rBtnDerecha.Enabled = true;
            rTxtRell.Enabled = true;


        }
        if (opc == 3)
        {

            rBtnRequerido.Enabled = false;
            rBtnOpcional.Enabled = false;
            rTxtRell.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;

        }



    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
                llenadata_Grid();
                rBtnCancelar.Enabled = false;
                rBtnGuardar.Enabled = false;
                this.rTxtSolicitud.Enabled = false;
                this.rCboValidacion.Enabled = false;
                this.rCboPrograma.Enabled = false;
                rCboProgValida.Enabled = false;
                this.rTxtRell.Enabled = false;
                this.rBtnDerecha.Enabled = false;
                this.rBtnIzquierda.Enabled = false;
                this.rBtnRequerido.Enabled = false;
                this.rBtnOpcional.Enabled = false;
                this.RadTxtPrgValid.Enabled = false;
                this.RadTxtSecuenc.Enabled = false;
                this.RadTxtSecuenc.Text = "";
                rCboTipoDato.Enabled = false;
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    this.rCboTipoDato.ClearSelection();
                    this.rCboValidacion.ClearSelection();
                    this.rCboPrograma.ClearSelection();
                rCboProgValida.ClearSelection();
                // this.rCboAgrupacion.ClearSelection();
                rGdvInformacion.MasterTableView.ClearSelectedItems();
                    this.rTxtSolicitud.Text = "";
                    this.rTxtRell.Text = "";
                this.RadTxtPrgValid.Text = "";

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                }
                if (hdfBtnAccion.Value == "")
                {
                    this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                }
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;

            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                llenadata_Grid();
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                rBtnCancelar.Enabled = false;
                rBtnGuardar.Enabled = false;
                this.rTxtSolicitud.Enabled = false;
                this.rCboValidacion.Enabled = false;
                this.rCboPrograma.Enabled = false;
                rCboProgValida.Enabled = false;
                this.rTxtRell.Enabled = false;
                this.rBtnDerecha.Enabled = false;
                this.rBtnIzquierda.Enabled = false;
                this.rBtnRequerido.Enabled = false;
                this.rBtnOpcional.Enabled = false;
                rCboTipoDato.Enabled = false;
                this.RadTxtPrgValid.Enabled = false;
                this.RadTxtSecuenc.Enabled = false;
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
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
            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue);

            if (rCboPrograma.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfProgCve", DbType.String, 15, ParameterDirection.Input, rCboPrograma.SelectedValue);
            }
            if (rCboProgValida.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfProgCveVal", DbType.String, 15, ParameterDirection.Input, rCboProgValida.SelectedValue);
            }

            ProcBD.AgregarParametrosProcedimiento("@conConfSec", DbType.Int64, 0, ParameterDirection.Input, RadTxtSecuenc.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cptoConfDes", DbType.String, 50, ParameterDirection.Input, rTxtSolicitud.Text.Trim());

            if (rBtnRequerido.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfTipCap", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else if (rBtnOpcional.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfTipCap", DbType.Int64, 0, ParameterDirection.Input, 2);
            }


            
            if (rBtnIzquierda.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfJust", DbType.String, 10, ParameterDirection.Input, 1);
            }
            else if (rBtnDerecha.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfJust", DbType.String, 10, ParameterDirection.Input, 2);
            }
            
            ProcBD.AgregarParametrosProcedimiento("@cptoConfRell", DbType.String, 1, ParameterDirection.Input, rTxtRell.Text);
            
            ProcBD.AgregarParametrosProcedimiento("@cptoConfOrd", DbType.Int64, 0, ParameterDirection.Input, 1);

            if (txtFormula.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoConfFormula", DbType.String, 500, ParameterDirection.Input, txtFormula.Text);
            }
            
            



            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                //EjecutaSpRefVar();

                string sEjecEstatus, sEjecMSG = "";

                if (ds.Tables.Count == 1)
                {
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                }
                else
                {
                    sEjecEstatus = ds.Tables[1].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[1].Rows[0]["maMSGDes"].ToString();
                }

                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
            
                }

                
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
        //int rBtnValCaptur = 0;
        //int rBtnJust = 0;
        //int rBtnChkDesc = 0;

        //try
        //{
       // int conConfSec; 
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
            //var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {


                //conConfSec = dataItem["cptoConfSec"].Text;
                listTipDatoCptoCve = dataItem["listTipDatoCptoCve"].Text;

                try
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, listTipDatoCptoCve);
                    ProcBD.AgregarParametrosProcedimiento("@conConfSec", DbType.Int64, 0, ParameterDirection.Input, dataItem["cptoConfSec"].Text);

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
                //LlenaGridAlmacenes();
                //InicioPagina();
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
                //LlenaGridAlmacenes();
                InicioPagina();
            }

        }

    }

    
    
    private void LlenaComboTiposDato()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboTipoDato, ds, "listTipDatoCptoCve", "listTipDatoCptoDes", false, false);
        ((Literal)rCboTipoDato.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipoDato.Items.Count);
    }

    private void ComboPrograma()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Programas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input,Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@progVal", DbType.Int64, 0, ParameterDirection.Input, 0);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboPrograma, ds, "progCve", "progDes", true, false);
        ((Literal)rCboPrograma.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboPrograma.Items.Count);
    }

    private void ComboProgramaVal()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Programas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@progVal", DbType.Int64, 0, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboProgValida, ds, "progCve", "progDes", true, false);
        ((Literal)rCboProgValida.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboProgValida.Items.Count);
    }




    public void llenadata_Grid()
    {
        //string cptoConfTipCap = Convert.ToString(ds.Tables[0].Rows[0]["cptoConfTipCap"]).Text;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);
    }
 

    public void llenar_cboTrabajadores()
    {
        //FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPaises, true, false);
        //FnCtlsFillIn.RadComboBox_Trabajadores(Pag_sConexionLog, Pag_sCompania, ref rCboTrabajador, true, true, "1", 1);
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
            if (rTxtSolicitud.Text.Trim() == "")
            {
                rTxtSolicitud.CssClass = "cssTxtInvalid";
                rTxtSolicitud.DataBind();
                rTxtSolicitud.Focus();
                camposInc += 1;
            }
            else { rTxtSolicitud.CssClass = "cssTxtEnabled"; }

            if (RadTxtSecuenc.Text.Trim() == "")
            {
                RadTxtSecuenc.CssClass = "cssTxtInvalid";
                RadTxtSecuenc.DataBind();
                RadTxtSecuenc.Focus();
                camposInc += 1;
            }
            else { RadTxtSecuenc.CssClass = "cssTxtEnabled"; }

            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.BorderWidth = Unit.Pixel(1);
                rCboTipoDato.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else {  rCboTipoDato.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rTxtSolicitud.Text.Trim() == "")
            {
                rTxtSolicitud.CssClass = "cssTxtInvalid";
                rTxtSolicitud.DataBind();
                rTxtSolicitud.Focus();
                camposInc += 1;
            }
            else { rTxtSolicitud.CssClass = "cssTxtEnabled"; }




            if (RadTxtSecuenc.Text.Trim() == "")
            {
                RadTxtSecuenc.CssClass = "cssTxtInvalid";
                RadTxtSecuenc.DataBind();
                RadTxtSecuenc.Focus();
                camposInc += 1;
            }
            else { RadTxtSecuenc.CssClass = "cssTxtEnabled"; }


            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.CssClass = "cssTxtInvalid";
                rCboTipoDato.Focus();

                camposInc += 1;
            }
            else { rCboTipoDato.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        ////ELIMINAR
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        //{

        //    if (rTxtSolicitud.Text.Trim() == "")
        //    {
        //        rTxtSolicitud.CssClass = "cssTxtInvalid";
        //        rTxtSolicitud.DataBind();
        //        rTxtSolicitud.Focus();
        //        camposInc += 1;
        //    }
        //    else { rTxtSolicitud.CssClass = "cssTxtEnabled"; }

        //    if (rTxtRell.Text.Trim() == "")
        //    {
        //        rTxtRell.CssClass = "cssTxtInvalid";
        //        rTxtRell.DataBind();
        //        rTxtRell.Focus();
        //        camposInc += 1;
        //    }
        //    else { rTxtRell.CssClass = "cssTxtEnabled"; }


        //    if (rCboTipoDato.SelectedIndex == -1)
        //    {
        //        rCboTipoDato.CssClass = "cssTxtInvalid";
        //        rCboTipoDato.Focus();

        //        camposInc += 1;
        //    }
        //    else { rCboTipoDato.CssClass = "cssTxtEnabled"; }

        //    if (camposInc > 0)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        //    }
        //    return sResult;
        //}
        return sResult;
    }


    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 55);
        //ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, PagLoc_CliCve);
        //ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "AGENT");
        //ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);

        //ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

        //dt_referencias.DataSource = ds;
        //dt_referencias.DataBind();

    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtSolicitud.CssClass = "cssTxtEnabled";
        RadTxtSecuenc.CssClass = "cssTxtEnabled";
        rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;


        rCboTipoDato.Enabled = false;
        RadTxtSecuenc.Enabled = false;
        rBtnRequerido.Enabled = false;
        rBtnOpcional.Enabled = false;
        rTxtSolicitud.Enabled = false;
        rCboPrograma.Enabled = false;
        rCboProgValida.Enabled = false;
        rBtnIzquierda.Enabled = false;
        rBtnDerecha.Enabled = false;
        rTxtRell.Enabled = false;
        txtFormula.Enabled = false; 

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
            rCboTipoDato.Enabled = false;
            RadTxtSecuenc.Enabled = false;
            rBtnRequerido.Enabled = false;
            rBtnOpcional.Enabled = false;
            rTxtSolicitud.Enabled = false;
            rCboPrograma.Enabled = false;
            rCboProgValida.Enabled = false;
            rBtnIzquierda.Enabled = false;
            rBtnDerecha.Enabled = false;
            rTxtRell.Enabled = false;
            txtFormula.Enabled = false;

            rCboTipoDato.ClearSelection();
            RadTxtSecuenc.Text = "";
            rBtnRequerido.Checked = true;
            rBtnOpcional.Checked = false;
            rTxtSolicitud.Text = "";
            rCboPrograma.ClearSelection();
            rCboProgValida.ClearSelection();
            rBtnIzquierda.Checked = true;
            rBtnDerecha.Checked = false;
            rTxtRell.Text = "";
            txtFormula.Text = "";
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

                rCboTipoDato.Enabled = true;
                RadTxtSecuenc.Enabled = true;
                rBtnRequerido.Enabled = true;
                rBtnOpcional.Enabled = true;
                rTxtSolicitud.Enabled = true;
                rCboPrograma.Enabled = true;
                rCboProgValida.Enabled = true;
                rBtnIzquierda.Enabled = true;
                rBtnDerecha.Enabled = true;
                rTxtRell.Enabled = true;
                txtFormula.Enabled = true;

                rCboTipoDato.ClearSelection();
                RadTxtSecuenc.Text = "";
                rBtnRequerido.Checked = true;
                rBtnOpcional.Checked = false;
                rTxtSolicitud.Text = "";
                rCboPrograma.ClearSelection();
                rCboProgValida.ClearSelection();
                rBtnIzquierda.Checked = true;
                rBtnDerecha.Checked = false;
                rTxtRell.Text = "";
                txtFormula.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                rCboTipoDato.Enabled = false;
                RadTxtSecuenc.Enabled = false;
                rBtnRequerido.Enabled = true;
                rBtnOpcional.Enabled = true;
                rTxtSolicitud.Enabled = true;
                rCboPrograma.Enabled = true;
                rCboProgValida.Enabled = true;
                rBtnIzquierda.Enabled = true;
                rBtnDerecha.Enabled = true;
                rTxtRell.Enabled = true;
                txtFormula.Enabled = true;

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

                rCboTipoDato.Enabled = false;
                RadTxtSecuenc.Enabled = false;
                rBtnRequerido.Enabled = false;
                rBtnOpcional.Enabled = false;
                rTxtSolicitud.Enabled = false;
                rCboPrograma.Enabled = false;
                rCboProgValida.Enabled = false;
                rBtnIzquierda.Enabled = false;
                rBtnDerecha.Enabled = false;
                rTxtRell.Enabled = false;
                txtFormula.Enabled = false;

                rCboTipoDato.ClearSelection();
                RadTxtSecuenc.Text = "";
                rBtnRequerido.Checked = true;
                rBtnOpcional.Checked = false;
                rTxtSolicitud.Text = "";
                rCboPrograma.ClearSelection();
                rCboProgValida.ClearSelection();
                rBtnIzquierda.Checked = true;
                rBtnDerecha.Checked = false;
                rTxtRell.Text = "";
                txtFormula.Text = "";
            }
        }


        if (Result == false)
        {
            rCboTipoDato.Enabled = false;
            RadTxtSecuenc.Enabled = false;
            rBtnRequerido.Enabled = false;
            rBtnOpcional.Enabled = false;
            rTxtSolicitud.Enabled = false;
            rCboPrograma.Enabled = false;
            rCboProgValida.Enabled = false;
            rBtnIzquierda.Enabled = false;
            rBtnDerecha.Enabled = false;
            rTxtRell.Enabled = false;
            txtFormula.Enabled = false;

            rCboTipoDato.ClearSelection();
            RadTxtSecuenc.Text = "";
            rBtnRequerido.Checked = true;
            rBtnOpcional.Checked = false;
            rTxtSolicitud.Text = "";
            rCboPrograma.ClearSelection();
            rCboProgValida.ClearSelection();
            rBtnIzquierda.Checked = true;
            rBtnDerecha.Checked = false;
            rTxtRell.Text = "";
            txtFormula.Text = "";
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
            rCboTipoDato.ClearSelection();
            RadTxtSecuenc.Text = "";
            rBtnRequerido.Checked = true;
            rBtnOpcional.Checked = false;
            rTxtSolicitud.Text = "";
            rCboPrograma.ClearSelection();
            rCboProgValida.ClearSelection();
            rBtnIzquierda.Checked = true;
            rBtnDerecha.Checked = false;
            rTxtRell.Text = "";
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtSolicitud.CssClass = "cssTxtEnabled";
            RadTxtSecuenc.CssClass = "cssTxtEnabled";
            rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;

            rCboTipoDato.Enabled = false;
            RadTxtSecuenc.Enabled = false;
            rBtnRequerido.Enabled = false;
            rBtnOpcional.Enabled = false;
            rTxtSolicitud.Enabled = false;
            rCboPrograma.Enabled = false;
            rCboProgValida.Enabled = false;
            rBtnIzquierda.Enabled = false;
            rBtnDerecha.Enabled = false;
            rTxtRell.Enabled = false;

            rCboTipoDato.ClearSelection();
            RadTxtSecuenc.Text = "";
            rBtnRequerido.Checked = true;
            rBtnOpcional.Checked = false;
            rTxtSolicitud.Text = "";
            rCboPrograma.ClearSelection();
            rCboProgValida.ClearSelection();
            rBtnIzquierda.Checked = true;
            rBtnDerecha.Checked = false;
            rTxtRell.Text = "";

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