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

public partial class DC_MttoClieContactos : System.Web.UI.Page
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

        private string PagLoc_folio_Selection;
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

    protected void rCboPais_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = false;
        rCboEntidadFed.Enabled = true;
        if (rCboPais.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        }
        else
        {
            rCboEntidadFed.Enabled = false;
            rCboEntidadFed.ClearSelection();
            rCboProvincia.Enabled = false;
            rCboProvincia.ClearSelection();
            rCboPais.BorderColor = System.Drawing.Color.Transparent;
        }
    }

    protected void rCboEntidadFed_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = true;
        if (rCboEntidadFed.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPais.SelectedValue, rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
        }
        else
        {
            rCboProvincia.Enabled = false;
            rCboProvincia.ClearSelection();
        }
    }

    protected void rGdvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idCON;
        var dataItem = rGdvDatos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value =="")
            {

                hdfIdRegSel.Value = dataItem.GetDataKeyValue("cliConId").ToString();
                rTxtNombre.Text = dataItem["cliConNom"].Text.Replace("&nbsp;", "");
                rTxtApPaterno.Text = dataItem["cliConAPat"].Text.Replace("&nbsp;", "");
                rTxtApMaterno.Text = dataItem["cliConAMat"].Text.Replace("&nbsp;", "");
                rTxtColonia.Text = dataItem["domCol"].Text.Replace("&nbsp;", "");
                rTxtCalle.Text = dataItem["domClle"].Text.Replace("&nbsp;", "");
                rTxtNoInt.Text = dataItem["domNInt"].Text.Replace("&nbsp;", "");
                rTxtNoExt.Text = dataItem["domNExt"].Text.Replace("&nbsp;", "");
                rTxtCallesAledanas.Text = dataItem["domCllsA"].Text.Replace("&nbsp;", "");
                rTxtReferencia.Text = dataItem["domRef"].Text.Replace("&nbsp;", "");
                rTxtTelefono1.Text = dataItem["domTel"].Text.Replace("&nbsp;", "");
                rTxtTelefono2.Text = dataItem["domTel2"].Text.Replace("&nbsp;", "");
                rTxtCodigoPostal.Text = dataItem["domCP"].Text.Replace("&nbsp;", "");
 
           

                IDCLIENTECONTACTO.Text = hdfIdRegSel.Value;

                idCON = Convert.ToInt32(hdfIdRegSel.Value);


                DATAREFCLIE.DataSource = llenadatalistVarRef(1, idCON);
                DATAREFCLIE.DataBind();

                LlenaDLOtrosDatos(idCON); 


                FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPais, true, false);
                rCboPais.SelectedValue = Convert.ToInt64(dataItem["paisCve"].Text).ToString();

                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
                rCboEntidadFed.SelectedValue = dataItem["entFCve"].Text.ToString();

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    if (rCboEntidadFed.SelectedValue == "&nbsp;")
                    {
                        rCboProvincia.Enabled = false;
                    }else
                    {
                        rCboProvincia.Enabled = true;
                    }
                }

                if (dataItem["entFCve"].Text == "&nbsp;")
                {
                   rCboEntidadFed.ClearSelection();
                    rCboProvincia.ClearSelection();
                    if (true)
                    {

                    }
                    rCboEntidadFed.Enabled = true;
                    rCboProvincia.Enabled = false;
                }
                else
                {
                    rCboEntidadFed.ClearSelection();
                    rCboEntidadFed.SelectedValue = dataItem["entFCve"].Text.ToString();
                    FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue,  rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);

                    rCboProvincia.SelectedValue = dataItem["provCve"].Text.ToString();
                }


                //FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
                //rCboProvincia.SelectedValue = dataItem["provCve"].Text.ToString();


                //CLIENTE DEFAULT 
                if ((dataItem["cliConPpal"].Text.Replace("&nbsp;", "")).ToString() == "")
                {
                    ckBxContactoDefatul.Checked = false;
                }
                else
                {
                    ckBxContactoDefatul.Checked = true;
                }


                this.rCboPais.Enabled = false;
                this.rCboEntidadFed.Enabled = false;
                this.rCboProvincia.Enabled = false;
                this.rTxtNombre.Enabled = false;
                this.rTxtApPaterno.Enabled = false;
                this.rTxtApMaterno.Enabled = false;
                this.rTxtColonia.Enabled = false;
                this.rTxtCalle.Enabled = false;
                this.rTxtNoInt.Enabled = false;
                this.rTxtNoExt.Enabled = false;
                this.rTxtCallesAledanas.Enabled = false;
                this.rTxtCodigoPostal.Enabled = false;
                this.rTxtReferencia.Enabled = false;
                rTxtTelefono1.Enabled = false;
                rTxtTelefono2.Enabled = false;
                HanilitaDeshabilita_DatosContactoAndSocial(false);
                HanilitaDeshabilita_DatosContactoAndSocial(false);

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()) {
                //    HanilitaDeshabilita_DatosContactoAndSocial(true);
                //    HanilitaDeshabilita_DatosContactoAndSocial(true);
                //    this.rCboPais.Enabled = true;
                //    this.rCboEntidadFed.Enabled = true;
                //   // this.rCboProvincia.Enabled = true;
                //    this.rTxtNombre.Enabled = true;
                //    this.rTxtApPaterno.Enabled = true;
                //    this.rTxtApMaterno.Enabled = true;
                //    this.rTxtColonia.Enabled = true;
                //    this.rTxtCalle.Enabled = true;
                //    this.rTxtNoInt.Enabled = true;
                //    this.rTxtNoExt.Enabled = true;
                //    this.rTxtCallesAledanas.Enabled = true;
                //    this.rTxtCodigoPostal.Enabled = true;
                //    this.rTxtReferencia.Enabled = true;
                //    this.ckBxContactoDefatul.Enabled = true;
                //}
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    //if (rCboEntidadFed.SelectedValue == "&nbsp;")
                    //{
                    //    rCboProvincia.Enabled = false;
                    //}
                    //else
                    //{
                    //    rCboProvincia.Enabled = true;
                    //}
                    //if (rCboPais.SelectedValue != "")
                    //{
                    //    rCboEntidadFed.Enabled = true;
                    //}
                    //else
                    //{
                    //    rCboEntidadFed.Enabled = false;
                    //}
                    //if (rCboEntidadFed.SelectedValue != "")
                    //{
                    //    rCboProvincia.Enabled = true;
                    //}
                    //else
                    //{
                    //    rCboProvincia.Enabled = false;
                    //}
                }

                
                
            }
        }
    }




    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
        HanilitaDeshabilita_DatosContactoAndSocial(true);
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
       
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        //{
        //    rCboEntidadFed.Enabled = false;
        //    rCboProvincia.Enabled = false;
        //}
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
        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
    }

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";

        rCboPais.EmptyMessage = "Seleccionar";
        rCboEntidadFed.EmptyMessage = "Seleccionar";
        rCboProvincia.EmptyMessage = "Seleccionar";

        ControlesAccion();
        LlenaDatos();
        FnCtlsFillIn.RadComboBox_Paises (Pag_sConexionLog, ref rCboPais, true, false);
        ClearDatalist();
         
        LlenaDLOtrosDatos(0);

        HanilitaDeshabilita_DatosContactoAndSocial(false);

        LlenaGrid();
        DATAREFCLIE.DataSource = llenadatalistVarRef(1,0);
        DATAREFCLIE.DataBind();
        rCboPais.BorderWidth = Unit.Pixel(1);
        rCboPais.BorderColor = System.Drawing.Color.Transparent;

        rCboEntidadFed.BorderWidth = Unit.Pixel(1);
        rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;

        rCboProvincia.BorderWidth = Unit.Pixel(1);
        rCboProvincia.BorderColor = System.Drawing.Color.Transparent;

        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            rLblSubClient.Visible = true;
        }
        else
        {
            rLblSubClie.Visible = false;
            rLblSubClient.Visible = false;
        }
        
        LimpiarControles();
        rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDatos.AllowMultiRowSelection = true;

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


    private void LlenaDatos() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDatosCartera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblClave.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveClie"]);
            rLblSubClient.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveSubClie"]);
            rLblDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0]["clieNom"]);
        }
        else {
            rLblClave.Text = "";
            rLblSubClient.Text = "";
            rLblDescripcion.Text = "";
        }
    }

 
    public void LlenaDLOtrosDatos(int iCliConId)
    {
        DataListOtrosDatos.DataSource = dsOtrosDatos(iCliConId);
        DataListOtrosDatos.RepeatColumns = 2;
        DataListOtrosDatos.DataBind();
    }
    DataSet dsOtrosDatos(int iCliConId)
    {
        DataSet ds = new DataSet();
 

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteContactosOtrosDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int32, 0, ParameterDirection.Input, iCliConId);
        ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "CLIECONT");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;
    }

    public void GuardaOtrosDatos(int iCliConId)
    {
        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var valTxt = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            string DatCve = DataListOtrosDatos.DataKeys[Countkeyarray].ToString();


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteContactosOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int32, 0, ParameterDirection.Input, iCliConId);

            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "CLIECONT");
            ProcBD.AgregarParametrosProcedimiento("@otroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            Countkeyarray += 1;

        }


    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteContactos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvDatos, ds);

      
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
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDatos.AllowMultiRowSelection = true;
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }

    }


    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteContactos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliConNom", DbType.String, 50, ParameterDirection.Input, rTxtNombre.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cliConAPat", DbType.String, 50, ParameterDirection.Input, rTxtApPaterno.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cliConAMat", DbType.String, 50, ParameterDirection.Input, rTxtApMaterno.Text.Trim());

            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, rCboPais.SelectedValue);
            if (rCboEntidadFed.SelectedValue != "" && rCboEntidadFed.SelectedValue != "&nbsp;")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, rCboEntidadFed.SelectedValue);
            }
            if (rCboProvincia.SelectedValue != "" && rCboProvincia.SelectedValue != "&nbsp;")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, rCboProvincia.SelectedValue);

            }
            if (rTxtCodigoPostal.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, 50, ParameterDirection.Input, rTxtCodigoPostal.Text.Trim());
            }
            if (rTxtColonia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, 50, ParameterDirection.Input, rTxtColonia.Text.Trim());
            }
            if (rTxtCalle.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, 50, ParameterDirection.Input, rTxtCalle.Text.Trim());
            }
            if (rTxtCallesAledanas.Text.Trim()!= "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCallesAledanas.Text.Trim());
            }
            if (rTxtReferencia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtReferencia.Text.Trim());
            }
            if (rTxtTelefono1.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 50, ParameterDirection.Input, rTxtTelefono1.Text.Trim());
            }
            if (rTxtTelefono2.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 50, ParameterDirection.Input, rTxtTelefono2.Text.Trim());
            }



            if (rTxtNoInt.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, rTxtNoInt.Text.Trim());
            }
            if (rTxtNoExt.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, rTxtNoExt.Text.Trim());
            }


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, hdfIdRegSel.Value);
            }

            if (ckBxContactoDefatul.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@cliConPpal", DbType.Int64, 0, ParameterDirection.Input, 1);

            }
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            //recorrerRefe();

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1")
                {
                    Int32 sprovConId = Convert.ToInt32(ds.Tables[0].Rows[0]["cliConId"].ToString());
               
                    recorrerRefe(sprovConId);
                    GuardaOtrosDatos(sprovConId);
                    InicioPagina();
                    LimpiarControles();
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
        try
        {

            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";

            foreach (GridDataItem i in rGdvDatos.SelectedItems)
            {

                var dataItem = rGdvDatos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string sCve = dataItem.GetDataKeyValue("cliConId").ToString();
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ClienteContactos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        //ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, (20), ParameterDirection.Input, PagLoc_folio_Selection);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, sCve);


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
                    LlenaGrid();
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

                    LlenaGrid();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        this.rGdvDatos.ClientSettings.Selecting.AllowRowSelect = false;
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

 

    private DataSet llenadatalistVarRef(Int64 revaTip,int valor)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, "CLIECONT");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int64, 0, ParameterDirection.Input, revaTip);
        ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, valor);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }


    public void recorrerRefe(Int32  valor)
    {
        if (PagLoc_folio_Selection == "")
        {
            hdfBtnAccion.Value = Convert.ToString(1);
        }
        
        int unomas = 0;
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            unomas = unomas+1;
            var references = dli.FindControl("txtrefclie") as RadTextBox;

            try
            {

                if (references.Text.Trim() != "")
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

                    ProcBD.NombreProcedimiento = "sp_ClienteContactosRefVar";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, "CLIECONT");
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int64, 0, ParameterDirection.Input, unomas);
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, references.Text);
                    ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, valor);
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }else
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

                    ProcBD.NombreProcedimiento = "sp_ClienteContactosRefVar";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 3);
                    ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, "CLIECONT");
                    ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int64, 0, ParameterDirection.Input, unomas);
                    ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, references.Text);
                    ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, valor);
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                }
                

            }
            catch (Exception ex)
            {
                ShowAlert("2", ex.ToString());
            }
        }
    }





    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        rCboPais.BorderWidth = Unit.Pixel(1);
        rCboPais.BorderColor = System.Drawing.Color.Transparent;

        rCboEntidadFed.BorderWidth = Unit.Pixel(1);
        rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;

        rCboProvincia.BorderWidth = Unit.Pixel(1);
        rCboProvincia.BorderColor = System.Drawing.Color.Transparent;

        
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }
            
            if (rCboPais.SelectedIndex == -1)
            {
                rCboPais.CssClass = "cssTxtInvalid";
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            } else { rCboPais.BorderColor = System.Drawing.Color.Red; }
            
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvDatos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            

            if (rCboPais.SelectedIndex == -1)
            {
                rCboPais.CssClass = "cssTxtInvalid";
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }else { rCboPais.CssClass = "cssTxtEnabled"; }
            
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvDatos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }


    public void Desa_ValRef()
    {
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            var references = dli.FindControl("txtrefclie") as RadTextBox;
            references.Enabled = false;
        }
    }

    public void Enabled_ValRef1(bool HA)
    {
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            var references = dli.FindControl("txtrefclie") as RadTextBox;
            references.Enabled =  HA;
        }
    }

    public void ClearDatalist()
    {
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            var references = dli.FindControl("txtrefclie") as RadTextBox;
            references.Text = "";
        }
        

            foreach (DataListItem dliDatos in DataListOtrosDatos.Items)
        {
            var references = dliDatos.FindControl("txt_OtrosDatos") as RadTextBox;
            references.Text = "";
        }
    }

  

    public void HanilitaDeshabilita_DatosContactoAndSocial(Boolean opc) {
        foreach (DataListItem dli in this.DataListOtrosDatos.Items)
        {
            var references = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            references.Enabled = opc;
        }
        
     foreach (DataListItem dliClie in this.DATAREFCLIE.Items)
        {
            var references = dliClie.FindControl("txtrefclie") as RadTextBox;
            references.Enabled = opc;
        }


    }
    private DataSet dsDatosContactoAndSocial(int opc, string sCiaCve, int iDatCSTip, int iDatCSEmpPe, int iCliConId)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteContactos_DatosContactoAndSocial";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
        ProcBD.AgregarParametrosProcedimiento("@datCSTip", DbType.Int64, 0, ParameterDirection.Input, iDatCSTip);
        ProcBD.AgregarParametrosProcedimiento("@datCSEmpPer", DbType.Int64, 0, ParameterDirection.Input, iDatCSEmpPe);
        ProcBD.AgregarParametrosProcedimiento("@cliConId", DbType.Int64, 0, ParameterDirection.Input, iCliConId);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtNombre.CssClass = "cssTxtEnabled";
        rCboPais.BorderColor = System.Drawing.Color.Transparent;


        rTxtNombre.Enabled = false;
        rTxtApPaterno.Enabled = false;
        rTxtApMaterno.Enabled = false;
        ckBxContactoDefatul.Enabled = false;
        rCboPais.Enabled = false;
        rCboProvincia.Enabled = false;
        rCboEntidadFed.Enabled = false;
        rTxtColonia.Enabled = false;
        rTxtCalle.Enabled = false;
        rTxtNoExt.Enabled = false;
        rTxtNoInt.Enabled = false;
        rTxtCallesAledanas.Enabled = false;
        rTxtCodigoPostal.Enabled = false;
        rTxtReferencia.Enabled = false;
        rTxtTelefono1.Enabled = false;
        rTxtTelefono2.Enabled = false;
        HanilitaDeshabilita_DatosContactoAndSocial(false);

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
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvDatos.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvDatos.MasterTableView.ClearSelectedItems();


                rTxtNombre.Enabled = true;
                rTxtApPaterno.Enabled = true;
                rTxtApMaterno.Enabled = true;
                ckBxContactoDefatul.Enabled = true;
                rCboPais.Enabled = true;
                //rCboProvincia.Enabled = true;
                //rCboEntidadFed.Enabled = true;
                rTxtColonia.Enabled = true;
                rTxtCalle.Enabled = true;
                rTxtNoExt.Enabled = true;
                rTxtNoInt.Enabled = true;
                rTxtCallesAledanas.Enabled = true;
                rTxtCodigoPostal.Enabled = true;
                rTxtReferencia.Enabled = true;
                rTxtTelefono1.Enabled = true;
                rTxtTelefono2.Enabled = true;
                HanilitaDeshabilita_DatosContactoAndSocial(true);

                rTxtNombre.Text = "";
                rTxtApPaterno.Text = "";
                rTxtApMaterno.Text = "";
                ckBxContactoDefatul.Checked = false;
                rCboPais.ClearSelection();
                rCboProvincia.ClearSelection();
                rCboEntidadFed.ClearSelection();
                rTxtColonia.Text = "";
                rTxtCalle.Text = "";
                rTxtNoExt.Text = "";
                rTxtNoInt.Text = "";
                rTxtCallesAledanas.Text = "";
                rTxtCodigoPostal.Text = "";
                rTxtReferencia.Text = "";
                rTxtTelefono1.Text = "";
                rTxtTelefono2.Text = "";
                ClearDatalist();


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvDatos.AllowMultiRowSelection = false;
                rTxtNombre.Enabled = true;
                rTxtApPaterno.Enabled = true;
                rTxtApMaterno.Enabled = true;
                ckBxContactoDefatul.Enabled = true;
                rCboPais.Enabled = true;
                rCboProvincia.Enabled = true;
                rCboEntidadFed.Enabled = true;
                rTxtColonia.Enabled = true;
                rTxtCalle.Enabled = true;
                rTxtNoExt.Enabled = true;
                rTxtNoInt.Enabled = true;
                rTxtCallesAledanas.Enabled = true;
                rTxtCodigoPostal.Enabled = true;
                rTxtReferencia.Enabled = true;
                rTxtTelefono1.Enabled = true;
                rTxtTelefono2.Enabled = true;
                HanilitaDeshabilita_DatosContactoAndSocial(true);

                if (rCboPais.SelectedValue != "")
                {
                    rCboPais.Enabled = true;
                    rCboEntidadFed.Enabled = true;
                    rCboProvincia.Enabled = false;
                }
                if (rCboEntidadFed.SelectedValue != "")
                {
                    rCboEntidadFed.Enabled = true;
                    rCboProvincia.Enabled = true;
                }

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvDatos.AllowMultiRowSelection = true;
                rGdvDatos.MasterTableView.ClearSelectedItems();

                rTxtNombre.Enabled = false;
                rTxtApPaterno.Enabled = false;
                rTxtApMaterno.Enabled = false;
                ckBxContactoDefatul.Enabled = false;
                rCboPais.Enabled = false;
                rCboProvincia.Enabled = false;
                rCboEntidadFed.Enabled = false;
                rTxtColonia.Enabled = false;
                rTxtCalle.Enabled = false;
                rTxtNoExt.Enabled = false;
                rTxtNoInt.Enabled = false;
                rTxtCallesAledanas.Enabled = false;
                rTxtCodigoPostal.Enabled = false;
                rTxtReferencia.Enabled = false;
                rTxtTelefono1.Enabled = false;
                rTxtTelefono2.Enabled = false;
                HanilitaDeshabilita_DatosContactoAndSocial(false);

                rTxtNombre.Text = "";
                rTxtApPaterno.Text = "";
                rTxtApMaterno.Text = "";
                ckBxContactoDefatul.Checked = false;
                rCboPais.ClearSelection();
                rCboProvincia.ClearSelection();
                rCboEntidadFed.ClearSelection();
                rTxtColonia.Text = "";
                rTxtCalle.Text = "";
                rTxtNoExt.Text = "";
                rTxtNoInt.Text = "";
                rTxtCallesAledanas.Text = "";
                rTxtCodigoPostal.Text = "";
                rTxtReferencia.Text = "";
                rTxtTelefono1.Text = "";
                rTxtTelefono2.Text = "";
                ClearDatalist();
            }
        }

        if (Result == false)
        {
            rTxtNombre.Enabled = false;
            rTxtApPaterno.Enabled = false;
            rTxtApMaterno.Enabled = false;
            ckBxContactoDefatul.Enabled = false;
            rCboPais.Enabled = false;
            rCboProvincia.Enabled = false;
            rCboEntidadFed.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNoExt.Enabled = false;
            rTxtNoInt.Enabled = false;
            rTxtCallesAledanas.Enabled = false;
            rTxtCodigoPostal.Enabled = false;
            HanilitaDeshabilita_DatosContactoAndSocial(false);

            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            ckBxContactoDefatul.Checked = false;
            rCboPais.ClearSelection();
            rCboProvincia.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            ClearDatalist();
        }


    }


    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvDatos.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDatos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvDatos, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            ckBxContactoDefatul.Checked = false;
            rCboPais.ClearSelection();
            rCboProvincia.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            ClearDatalist();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvDatos.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtNombre.CssClass = "cssTxtEnabled";
            rCboPais.BorderColor = System.Drawing.Color.Transparent;


            rTxtNombre.Enabled = false;
            rTxtApPaterno.Enabled = false;
            rTxtApMaterno.Enabled = false;
            ckBxContactoDefatul.Enabled = false;
            rCboPais.Enabled = false;
            rCboProvincia.Enabled = false;
            rCboEntidadFed.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNoExt.Enabled = false;
            rTxtNoInt.Enabled = false;
            rTxtCallesAledanas.Enabled = false;
            rTxtCodigoPostal.Enabled = false;
            rTxtReferencia.Enabled = false;
            HanilitaDeshabilita_DatosContactoAndSocial(false);

            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            ckBxContactoDefatul.Checked = false;
            rCboPais.ClearSelection();
            rCboProvincia.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            ClearDatalist();

            if (rCboEntidadFed.SelectedValue == "&nbsp;")
            {
                rCboProvincia.Enabled = false;
            }
            else
            {
                rCboProvincia.Enabled = true;
            }
            if (rCboPais.SelectedValue != "")
            {
                rCboEntidadFed.Enabled = true;
            }
            else
            {
                rCboEntidadFed.Enabled = false;
            }
            if (rCboEntidadFed.SelectedValue != "")
            {
                rCboProvincia.Enabled = true;
            }
            else
            {
                rCboProvincia.Enabled = false;
            }


         

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }

    private void LimpiarControles()
    {
        rTxtNombre.Text = "";
        rTxtApPaterno.Text = "";
        rTxtApMaterno.Text = "";
        ckBxContactoDefatul.Checked = false;
        rCboPais.ClearSelection();
        rCboProvincia.ClearSelection();
        rCboEntidadFed.ClearSelection();
        rTxtColonia.Text = "";
        rTxtCalle.Text = "";
        rTxtNoExt.Text = "";
        rTxtNoInt.Text = "";
        rTxtCallesAledanas.Text = "";
        rTxtCodigoPostal.Text = "";
        rTxtReferencia.Text = "";
        rTxtTelefono1.Text = "";
        rTxtTelefono2.Text = "";
        ClearDatalist();
    }

    #endregion








}