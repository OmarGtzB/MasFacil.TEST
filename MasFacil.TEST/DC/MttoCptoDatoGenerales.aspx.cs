
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

public partial class DC_MttoCptoDatoGenerales : System.Web.UI.Page
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
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string folio_Selection;

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


    protected void rCboManFol_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManFol.SelectedValue), ref rcboFoliador, true, false);//1
    }

    protected void rCboAsientoCont_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboAsientoCont.SelectedValue), ref rcboFoliador_AsientoCont, true, false);//2
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnModificar.Enabled = true;
        rBtnLimpiar.Enabled = true;
        rCboRefBase.Enabled = true;
        rTxtAbreviatura.Enabled = true;
        rTxtConcepto.Enabled = false;
        rTxtDes.Enabled = true;
        rTxtAbreviatura.Enabled = true;
        rCboAsientoCont.Enabled = true;
        //rTxtSec.Enabled = true;
        rCboManFol.Enabled = true;
        rcboFoliador.Enabled = true;
        rcboFoliador_AsientoCont.Enabled = true;
        //rTxtCalle.Enabled = true;
        //rTxtAsiento.Enabled = true;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        rBtnElimTrans.Enabled = true;
        ckbGeneraCFDI.Enabled = true;

        if (rCboRefBase.SelectedValue != "CG")
        {
            TipoCptoCrea.Enabled = true;
            TipoCptoAplica.Enabled = true;
        }
        else
        {
            TipoCptoCrea.Enabled = false;
            TipoCptoAplica.Enabled = false;
        }


    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {


            if (folio_Selection=="")
            {
                rTxtConcepto.Text = "";
                rTxtDes.Text = "";
                this.rCboRefBase.ClearSelection();
                this.rTxtConcepto.Text = "";
                this.rTxtDes.Text = "";
                this.rTxtAbreviatura.Text = "";
                rTxtAbreviatura.Text = "";
                //rTxtSec.Text = "";
                //rTxtAsiento.Text = "";
                rCboAsientoCont.ClearSelection();
                rCboManFol.ClearSelection();

            rcboFoliador.ClearSelection();
            rcboFoliador_AsientoCont.ClearSelection();
                //rTxtCalle.Text = "";
                //rTxtCalle.CssClass = "cssTxtEnabled";
                //rTxtAsiento.CssClass = "cssTxtEnabled";

            rTxtConcepto.CssClass = "cssTxtEnabled";
                rTxtDes.CssClass = "cssTxtEnabled";
                rTxtAbreviatura.CssClass = "cssTxtEnabled";

                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
            }
            if (rTxtConcepto.Enabled == false && rTxtDes.Enabled == false)
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                //folio_Selection = rTxtConcepto.Text.Trim();
                //Session["folio_Selection"] = rTxtConcepto.Text.Trim();
                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = false;
                rBtnModificar.Enabled = true;
                this.rCboRefBase.Enabled = false;
                this.rTxtConcepto.Enabled = false;
                this.rTxtDes.Enabled = false;
                this.rTxtAbreviatura.Enabled = false;
                rTxtAbreviatura.Enabled = false;
                rCboAsientoCont.Enabled = false;
                rCboManFol.Enabled = false;
            rcboFoliador.Enabled = false;
            rcboFoliador_AsientoCont.Enabled = false;
                //rTxtSec.Enabled = false;
                //rTxtCalle.Enabled = false;
                //rTxtAsiento.Enabled = false;
            rBtnElimTrans.Enabled = false;

        }
        
        else
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EDITAR_DATOS();

                //InicioPagina();

                if (rTxtConcepto.Enabled == false && rTxtDes.Enabled == false)
                {
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    //folio_Selection = rTxtConcepto.Text.Trim();
                    //Session["folio_Selection"] = rTxtConcepto.Text.Trim();
                    rBtnGuardar.Enabled = false;
                    rBtnCancelar.Enabled = false;
                    rBtnModificar.Enabled = true;
                    this.rCboRefBase.Enabled = false;
                    this.rTxtConcepto.Enabled = false;
                    this.rTxtDes.Enabled = false;
                    this.rTxtAbreviatura.Enabled = false;
                    rTxtAbreviatura.Enabled = false;
                    rCboAsientoCont.Enabled = false;
                    rCboManFol.Enabled = false;
                    rcboFoliador.Enabled = false;
                    rcboFoliador_AsientoCont.Enabled = false;
                    //rTxtSec.Enabled = false;
                    //rTxtCalle.Enabled = false;
                    //rTxtAsiento.Enabled = false;
                    rBtnElimTrans.Enabled = false;

                }
            }
        }
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
        folio_Selection = Convert.ToString(Session["folio_Selection"]);

        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }

    private void InicioPagina()
    {
        RegistroDatosConceptos();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        llenar_cboRefBase();
        rCboRefBase.EmptyMessage = "Seleccionar";
        if (folio_Selection == "")
        {
            rTxtConcepto.Text = "";
            rTxtDes.Text = "";
            this.rCboRefBase.ClearSelection();
            this.rTxtConcepto.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtAbreviatura.Text = "";
            rTxtAbreviatura.Text = "";
            //rTxtSec.Text = "";
            //rTxtAsiento.Text = "";
            rCboAsientoCont.ClearSelection();
            rCboManFol.ClearSelection();
            rcboFoliador.ClearSelection();
            rcboFoliador_AsientoCont.ClearSelection();

            rTxtConcepto.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
            rTxtAbreviatura.CssClass = "cssTxtEnabled";

            rBtnModificar.Enabled = false;
            rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
        }

        else
        {
            EDITAR_DATOS();
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            folio_Selection = rTxtConcepto.Text.Trim();
            Session["folio_Selection"] = rTxtConcepto.Text.Trim();
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            rBtnModificar.Enabled = true;
            this.rCboRefBase.Enabled = false;
            this.rTxtConcepto.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            rCboAsientoCont.Enabled = false;
            rCboManFol.Enabled = false;
            rcboFoliador.Enabled = false;
            rcboFoliador_AsientoCont.Enabled = false;
            //rTxtSec.Enabled = false;
            //rTxtCalle.Enabled = false;
            //rTxtAsiento.Enabled = false;
            rBtnElimTrans.Enabled = false;
            TipoCptoAplica.Enabled = false;
            TipoCptoCrea.Enabled = false;
            ckbGeneraCFDI.Enabled = false;


        }

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
        rTxtConcepto.Text = "";
        rTxtDes.Text = "";
        this.rCboRefBase.ClearSelection();
        this.rTxtConcepto.Text = "";
        this.rTxtDes.Text = "";
        this.rTxtAbreviatura.Text = "";
        rTxtAbreviatura.Text = "";
        //rTxtSec.Text = "";
        //rTxtAsiento.Text = "";
        rCboAsientoCont.ClearSelection();
        rCboManFol.ClearSelection();
        rcboFoliador.ClearSelection();
        rcboFoliador_AsientoCont.ClearSelection();
        //rTxtCalle.Text = "";
        ////===> CONTROLES GENERAL
        //rTxtCalle.CssClass = "cssTxtEnabled";
        //rTxtAsiento.CssClass = "cssTxtEnabled";

        rTxtConcepto.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";

        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

        //===> CONTROLES POR ACCION

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (this.rTxtConcepto.Text != "" &&
          this.rTxtDes.Text != "" &&
          this.rTxtAbreviatura.Text != "") ;
            {

                this.rCboRefBase.Enabled = true;
                this.rTxtConcepto.Enabled = true;
                this.rTxtDes.Enabled = true;
                this.rTxtAbreviatura.Enabled = true;
                rTxtAbreviatura.Enabled = true;
                rCboAsientoCont.Enabled = true;
                rCboManFol.Enabled = true;
                rcboFoliador.Enabled = true;
                rcboFoliador_AsientoCont.Enabled = true;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                ckbGeneraCFDI.Enabled = true;

                //rTxtSec.Enabled = true;
                //rTxtCalle.Enabled = true;
                //rTxtAsiento.Enabled = true;
                rBtnElimTrans.Enabled = true;
            }
        } 
        else
        {

            this.rCboRefBase.Enabled = false;
            this.rTxtConcepto.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            rCboAsientoCont.Enabled = false;
            rCboManFol.Enabled = false;
            rcboFoliador.Enabled = false;
            rcboFoliador_AsientoCont.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            rBtnElimTrans.Enabled = false;
            ckbGeneraCFDI.Enabled = false;
            if (rCboManFol.SelectedValue == "2")
            {
                //rTxtSec.Enabled = true;
            }
            if (rCboManFol.SelectedValue == "1")
            {
                //rTxtSec.Enabled = false;
            }

            if (rCboAsientoCont.SelectedValue == "2")
            {
                //rTxtCalle.Enabled = true;
                //rTxtAsiento.Enabled = true;
            }
            if (rCboAsientoCont.SelectedValue == "1")
            {
                //rTxtCalle.Enabled = false;
                //rTxtAsiento.Enabled = false;
            }
        }
          
        

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";


            this.rCboRefBase.Enabled = false;
            this.rTxtConcepto.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            rCboAsientoCont.Enabled = false;
            rCboManFol.Enabled = false;
            rcboFoliador.Enabled = false;
            rcboFoliador_AsientoCont.Enabled = false;
            rBtnElimTrans.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            ckbGeneraCFDI.Enabled = false;
            /////////////////////////////////////////
            if (rCboManFol.SelectedValue == "2")
            {
                //rTxtSec.Enabled = true;
            }
            if (rCboManFol.SelectedValue == "1")
            {
                //rTxtSec.Enabled = false;
            }

            if (rCboAsientoCont.SelectedValue == "2")
            {
                //rTxtCalle.Enabled = true;
                //rTxtAsiento.Enabled = true;
            }
            if (rCboAsientoCont.SelectedValue == "1")
            {
                //rTxtCalle.Enabled = false;
                //rTxtAsiento.Enabled = false;
            }
        }


        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {
            this.rCboRefBase.Enabled = false;
            this.rTxtConcepto.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtAbreviatura.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            //rTxtSec.Enabled = false;
            rCboAsientoCont.Enabled = false;
            rCboManFol.Enabled = false;
            rcboFoliador.Enabled = false;
            rcboFoliador_AsientoCont.Enabled = false;
            //rTxtCalle.Enabled = false;
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            rBtnElimTrans.Enabled = false;
            ckbGeneraCFDI.Enabled = false;
        }

        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
       )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
        else {
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

    }
    private void LimpiarUi()
    {
           EDITAR_DATOS();
    }





    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);

        if (msgValidacion == "")
        {

            if (rTxtDes.Text != "" && rTxtConcepto.Text != "" ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();

        
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }



    }

    private void EjecutaSpAcciones()
    {
        bool docFormRegSit = rBtnElimTrans.Checked;
        
        int idocFormRegSit;

        try
        {
           
                if (rBtnElimTrans.SelectedToggleState.Value == "1")
                {
                    idocFormRegSit = 1;
                }
                else
                {
                    idocFormRegSit = 2;
                }

            
            if (folio_Selection == "")
            {
                hdfBtnAccion.Value = "1";
            }
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(rTxtConcepto.Text.Trim()));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoDes", DbType.String, 50, ParameterDirection.Input, rTxtDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cptoAbr", DbType.String, 20, ParameterDirection.Input, rTxtAbreviatura.Text.Trim());
            if (rCboRefBase.SelectedIndex  > -1  )
            {
                ProcBD.AgregarParametrosProcedimiento("@contRefCve", DbType.String, 2, ParameterDirection.Input,Convert.ToString(rCboRefBase.SelectedValue));
            }
            ProcBD.AgregarParametrosProcedimiento("@cptoDefElimTrans", DbType.Int64, 0, ParameterDirection.Input, idocFormRegSit);
            ProcBD.AgregarParametrosProcedimiento("@cptoDefFolTip", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(rCboManFol.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@cptoDefFolVal", DbType.String, 10, ParameterDirection.Input, Convert.ToString(rcboFoliador.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@cptoDefAsiConFolTip", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(rCboAsientoCont.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@cptoDefAsiConFolVal", DbType.String, 10, ParameterDirection.Input, Convert.ToString(rcboFoliador_AsientoCont.SelectedValue));
            //GRET se comenta if para que se guarden el tipo concepto en BD para CG
            //if (rCboRefBase.SelectedValue != "CG")
            //{
                if (TipoCptoCrea.Checked == true)
                {
                    ProcBD.AgregarParametrosProcedimiento("@cptoTip", DbType.Int64, 0, ParameterDirection.Input, 1);

                }else if (TipoCptoAplica.Checked == true)
                {
                    ProcBD.AgregarParametrosProcedimiento("@cptoTip", DbType.Int64, 0, ParameterDirection.Input, 2);
                }
                
            //}
            //

            if (ckbGeneraCFDI.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoCFDIGen", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else {
                ProcBD.AgregarParametrosProcedimiento("@cptoCFDIGen", DbType.Int64, 0, ParameterDirection.Input, 2);
            }


            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

          
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";

                if (ds.Tables.Count == 1)
                {
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    TipoCptoCrea.Enabled = false;
                    TipoCptoAplica.Enabled = false;
                }
                else
                {
                    sEjecEstatus = ds.Tables[1].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[1].Rows[0]["maMSGDes"].ToString();
                }


                if (sEjecEstatus == "1" || sEjecEstatus == "3")
                {

                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    folio_Selection = rTxtConcepto.Text.Trim();
                    Session["folio_Selection"] = rTxtConcepto.Text.Trim();
           
                    rBtnGuardar.Enabled = false;
                    rBtnCancelar.Enabled = false;
                    rBtnModificar.Enabled=true;
                    this.rCboRefBase.Enabled = false;
                    this.rTxtConcepto.Enabled = false;
                    this.rTxtDes.Enabled = false;
                    this.rTxtAbreviatura.Enabled = false;
                    rTxtAbreviatura.Enabled = false;
                    rBtnElimTrans.Enabled = false;
                    rCboAsientoCont.Enabled = false;
                    rCboManFol.Enabled = false;
                    rcboFoliador.Enabled = false;
                    rcboFoliador_AsientoCont.Enabled = false;
                    ckbGeneraCFDI.Enabled = false; 

                }

                ShowAlert(sEjecEstatus, sEjecMSG);
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }





    public void EDITAR_DATOS()
    {
        string cptoTip = "";
        bool rBtnTogImpDscS;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rTxtConcepto.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rTxtConcepto.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoId"]);
            rTxtDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoDes"]);
            rTxtAbreviatura.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoAbr"]);
            rCboRefBase.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["contRefCve"]);
            rCboManFol.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoDefFolTip"]);
            rCboAsientoCont.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoDefAsiConFolTip"]);
            cptoTip = Convert.ToString(ds.Tables[0].Rows[0]["cptoTip"]);
            
                if (cptoTip == "1")
                {
                    TipoCptoCrea.Checked = true;

                }
                else if (cptoTip == "2")
                {
                    TipoCptoAplica.Checked = true;
                }else
                {
                TipoCptoCrea.Checked = true;
                TipoCptoAplica.Checked = false;
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                if (rCboRefBase.SelectedValue != "CG")
                {
                    TipoCptoCrea.Enabled = true;
                    TipoCptoAplica.Enabled = true;

                }
                else
                {
                    TipoCptoCrea.Enabled = false;
                    TipoCptoAplica.Enabled = false;
                }
            }

          

           
                

            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManFol.SelectedValue), ref rcboFoliador, true, true, Convert.ToString(ds.Tables[0].Rows[0]["cptoDefFolVal"]));
            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboAsientoCont.SelectedValue), ref rcboFoliador_AsientoCont, true, true , Convert.ToString(ds.Tables[0].Rows[0]["cptoDefAsiConFolVal"]));

            //rcboFoliador.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoDefFolVal"]);
            //rcboFoliador_AsientoCont.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoDefAsiConFolVal"]);

            //rBtnElimTrans.SelectedToggleState.Value = Convert.ToString(ds.Tables[0].Rows[0]["cptoDefElimTrans"]);
           
            if (Convert.ToString(ds.Tables[0].Rows[0]["cptoDefElimTrans"]) == "1")
            {
                rBtnElimTrans.SelectedToggleState.Selected = true;
                rBtnElimTrans.Value = "1";
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["cptoDefElimTrans"]) == "2")
            {
                rBtnElimTrans.SelectedToggleState.Selected = false;
                rBtnElimTrans.Value = "2";
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["cptoCFDIGen"]) == "1")
            {
                ckbGeneraCFDI.Checked = true;
            }
            else {
                ckbGeneraCFDI.Checked = false;
            } 


            }

    }



    public void llenar_cboRefBase()
    {
        FnCtlsFillIn.RadComboBox_ReferenciaBase(Pag_sConexionLog, Pag_sCompania, ref rCboRefBase, true, true, "CG");//

        FnCtlsFillIn.RadComboBox_ManejoFolios(Pag_sConexionLog, Pag_sCompania, ref rCboManFol, true, true,"1");//1
        FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManFol.SelectedValue), ref rcboFoliador, true, false);//1

        FnCtlsFillIn.RadComboBox_ManejoFolios(Pag_sConexionLog, Pag_sCompania, ref rCboAsientoCont, true, true, "1");//2
        FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboAsientoCont.SelectedValue), ref rcboFoliador_AsientoCont, true, false);//2
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

    #region FUNCIONES

    private bool RegistroDatosConceptos()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rTxtConcepto.Text = ds.Tables[0].Rows[0]["cptoId"].ToString();
            return true;

        }
        else {
            rTxtConcepto.Text = "";
            return false;
        }

    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        int rBtnVal1 = 0;
        int rBtnTipDoc = 0;
        int rBtnTogCodArtS = 0;
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        // NUEVO
        if (rTxtConcepto.Text!="" && rTxtDes.Text!="")
        {
            if (rTxtConcepto.Text.Trim() == "")
            {
                rTxtConcepto.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtConcepto.CssClass = "cssTxtEnabled"; }


            if (rTxtAbreviatura.Text.Trim() == "")
            {
                rTxtAbreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbreviatura.CssClass = "cssTxtEnabled"; }



            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rCboManFol.SelectedValue == "2")
            {
                //rTxtSec.Enabled = true;
            }

            if (rCboAsientoCont.SelectedValue == "2")
            {
                //rTxtCalle.Enabled = true;
                //rTxtAsiento.Enabled = true;
            }

            if (rCboManFol.SelectedValue == "")
            {
                rCboManFol.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboManFol.CssClass = "cssTxtEnabled"; }

            if (rCboAsientoCont.SelectedValue == "")
            {
                rCboAsientoCont.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboAsientoCont.CssClass = "cssTxtEnabled"; }

            if (rCboRefBase.SelectedValue == "")
            {
                rCboRefBase.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboRefBase.CssClass = "cssTxtEnabled"; }

            if (rCboRefBase.SelectedValue == "")
            {
                rCboRefBase.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboRefBase.CssClass = "cssTxtEnabled"; }

            //if (rCboManFol.SelectedValue == "2" && rTxtSec.Text == "")
            //{
            //    rTxtSec.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtSec.CssClass = "cssTxtEnabled"; }

            //if (rCboAsientoCont.SelectedValue == "2" && rTxtCalle.Text == "")
            //{
            //    rTxtCalle.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtCalle.CssClass = "cssTxtEnabled"; }

            //if (rCboAsientoCont.SelectedValue == "2" && rTxtAsiento.Text == "")
            //{
            //    rTxtAsiento.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtAsiento.CssClass = "cssTxtEnabled"; }

            if (rCboRefBase.SelectedValue == "")
            {

                rCboRefBase.CssClass = "cssTxtInvalid";
                rCboRefBase.BorderWidth = Unit.Pixel(1);
                rCboRefBase.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboRefBase.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboAsientoCont.SelectedValue == "")
            {

                rCboAsientoCont.CssClass = "cssTxtInvalid";
                rCboAsientoCont.BorderWidth = Unit.Pixel(1);
                rCboAsientoCont.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboAsientoCont.BorderColor = System.Drawing.Color.Transparent;
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

            //if (rCboManFol.SelectedValue == "2" && rTxtSec.Text == "")
            //{
            //    rTxtSec.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtSec.CssClass = "cssTxtEnabled"; }

            //if (rCboAsientoCont.SelectedValue == "2" && rTxtCalle.Text == "")
            //{
            //    rTxtCalle.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtCalle.CssClass = "cssTxtEnabled"; }

            //if (rCboAsientoCont.SelectedValue == "2" && rTxtAsiento.Text == "")
            //{
            //    rTxtAsiento.CssClass = "cssTxtInvalid";
            //    camposInc += 1;
            //}
            //else { rTxtAsiento.CssClass = "cssTxtEnabled"; }


            if (rTxtConcepto.Text.Trim() == "")
            {
                rTxtConcepto.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtConcepto.CssClass = "cssTxtEnabled"; }


            if (rTxtAbreviatura.Text.Trim() == "")
            {
                rTxtAbreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbreviatura.CssClass = "cssTxtEnabled"; }



            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }

            if (rCboManFol.SelectedValue == "")
            {
                rCboManFol.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboManFol.CssClass = "cssTxtEnabled"; }

            if (rCboAsientoCont.SelectedValue == "")
            {
                rCboAsientoCont.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboAsientoCont.CssClass = "cssTxtEnabled"; }

            if (rCboRefBase.SelectedValue == "")
            {
                rCboRefBase.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rCboRefBase.CssClass = "cssTxtEnabled"; }

            if (rCboRefBase.SelectedValue == "")
            {

                rCboRefBase.CssClass = "cssTxtInvalid";
                rCboRefBase.BorderWidth = Unit.Pixel(1);
                rCboRefBase.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboRefBase.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboManFol.SelectedValue == "")
            {

                rCboManFol.CssClass = "cssTxtInvalid";
                rCboManFol.BorderWidth = Unit.Pixel(1);
                rCboManFol.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManFol.BorderColor = System.Drawing.Color.Transparent;
            }
            if (rCboAsientoCont.SelectedValue == "")
            {

                rCboAsientoCont.CssClass = "cssTxtInvalid";
                rCboAsientoCont.BorderWidth = Unit.Pixel(1);
                rCboAsientoCont.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboAsientoCont.BorderColor = System.Drawing.Color.Transparent;
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

            return sResult;
        }




        return sResult;
    }



    protected void rBtnElimTrans_CheckedChanged(object sender, EventArgs e)
    {
       // ShowAlert("2", rBtnElimTrans.SelectedToggleState.Selected.ToString());
    }

    #endregion








    protected void rCboRefBase_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboRefBase.SelectedValue != "CG")
        {
            TipoCptoCrea.Enabled = true;
            TipoCptoAplica.Enabled = true;
            TipoCptoCrea.Checked = true;
            TipoCptoAplica.Checked = false;

        }
        else
        {
            TipoCptoCrea.Enabled = false;
            TipoCptoAplica.Enabled = false;
            TipoCptoCrea.Checked = true;
            TipoCptoAplica.Checked = false;
        }
    }

 
}