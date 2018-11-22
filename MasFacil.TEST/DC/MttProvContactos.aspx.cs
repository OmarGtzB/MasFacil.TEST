
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

public partial class DC_MttProvContactos : System.Web.UI.Page
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
    //private string PagLoc_cliCve;
    //private string PagLoc_ArtCve;
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
        //FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
        //rCboEntidadFed.ClearSelection();
        //rCboEntidadFed.Enabled = true;
        //rCboProvincia.ClearSelection();
        //rCboProvincia.Enabled = false;
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
            rCboEntidadFed.BorderColor = System.Drawing.Color.Transparent;
            rCboProvincia.BorderColor = System.Drawing.Color.Transparent;
        }

    }

    protected void rCboEntidadFed_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        rCboProvincia.ClearSelection();
        rCboProvincia.Enabled = true;
        if (rCboEntidadFed.SelectedValue != "")
        {
            FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, rCboPais.SelectedValue, rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
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
                hdfBtnAccion.Value == "")
            {

                hdfIdRegSel.Value = dataItem.GetDataKeyValue("provConId").ToString();



                rTxtNombre.Text = dataItem["provConNom"].Text.Replace("&nbsp;", "");
                rTxtApPaterno.Text = dataItem["provConAPat"].Text.Replace("&nbsp;", "");
                rTxtApMaterno.Text = dataItem["provConAMat"].Text.Replace("&nbsp;", "");

                rTxtColonia.Text = dataItem["domCol"].Text.Replace("&nbsp;", "");
                rTxtCalle.Text = dataItem["domClle"].Text.Replace("&nbsp;", "");
                rTxtNoInt.Text = dataItem["domNInt"].Text.Replace("&nbsp;", "");
                rTxtNoExt.Text = dataItem["domNExt"].Text.Replace("&nbsp;", "");
                rTxtCallesAledanas.Text = dataItem["domCllsA"].Text.Replace("&nbsp;", "");
                rTxtReferencia.Text = dataItem["domRef"].Text.Replace("&nbsp;", "");
                rTxtCodigoPostal.Text = dataItem["domCP"].Text.Replace("&nbsp;", "");
                rTxtTelefono1.Text = dataItem["domTel"].Text.Replace("&nbsp;", "");
                rTxtTelefono2.Text = dataItem["domTel2"].Text.Replace("&nbsp;", "");
                rTxtFax.Text = dataItem["domFax"].Text.Replace("&nbsp;", "");

                RadTextProvConId.Text = hdfIdRegSel.Value;

                idCON = Convert.ToInt32(hdfIdRegSel.Value);

                DATAREFCLIE.DataSource = llenadatalistVarRef(1, idCON);
                DATAREFCLIE.DataBind();
                LlenaDLOtrosDatos(idCON);

                DATAREFCLIE.DataSource = llenadatalistVarRef(1, idCON);///UNO TIPO
                DATAREFCLIE.DataBind();

                rCboPais.SelectedValue = dataItem["paisCve"].Text.ToString();
                FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, rCboPais.SelectedValue.ToString(), ref rCboEntidadFed, true, false);
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
                    FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog,rCboPais.SelectedValue, rCboEntidadFed.SelectedValue.ToString(), ref rCboProvincia, true, false);
                    rCboProvincia.SelectedValue = dataItem["provCve"].Text.ToString();
                }


                //this.rCboPais.Enabled = false;
                //this.rCboEntidadFed.Enabled = false;
                //this.rCboProvincia.Enabled = false;

                //this.rTxtNombre.Enabled = false;
                //this.rTxtApPaterno.Enabled = false;
                //this.rTxtApMaterno.Enabled = false;
                //this.rTxtColonia.Enabled = false;
                //this.rTxtCalle.Enabled = false;
                //this.rTxtNoInt.Enabled = false;
                //this.rTxtNoExt.Enabled = false;
                //this.rTxtCallesAledanas.Enabled = false;
                //this.rTxtCodigoPostal.Enabled = false;
                //this.rTxtReferencia.Enabled = false;
                //this.rTxtTelefono1.Enabled = false;
                //this.rTxtTelefono2.Enabled = false;
                //this.rTxtFax.Enabled = false;
                //Desa_ValRef();

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                //{
                    //Habil_ValRef();

                    //this.rCboPais.Enabled = true;
                    //this.rCboEntidadFed.Enabled = true;
                    //this.rCboProvincia.Enabled = true;

                    //this.rTxtNombre.Enabled = true;
                    //this.rTxtApPaterno.Enabled = true;
                    //this.rTxtApMaterno.Enabled = true;
                    //this.rTxtColonia.Enabled = true;
                    //this.rTxtCalle.Enabled = true;
                    //this.rTxtNoInt.Enabled = true;
                    //this.rTxtNoExt.Enabled = true;
                    //this.rTxtCallesAledanas.Enabled = true;
                    //this.rTxtCodigoPostal.Enabled = true;
                    //this.rTxtReferencia.Enabled = true;
                    //this.rTxtTelefono1.Enabled = true;
                    //this.rTxtTelefono2.Enabled = true;
                    //this.rTxtFax.Enabled = true;

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


                //}
                
            }
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

        ControlesAccion();
        //LlenaDatos();
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref rCboPais, true, false);
        LlenaGrid();
        DATAREFCLIE.DataSource = llenadatalistVarRef(1, 0);//siflo fueran variables se le manda el tipo en los parentesis
        DATAREFCLIE.DataBind();
        Desa_ValRef();

        rCboPais.EmptyMessage = "Seleccionar";
        rCboEntidadFed.EmptyMessage = "Seleccionar";
        rCboProvincia.EmptyMessage = "Seleccionar";

        rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvDatos.AllowMultiRowSelection = true;
        LlenaDLOtrosDatos(0);

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


    private void LlenaDatos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@provCveClie", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@provCveClie", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
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
                //InicioPagina();
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
            ShowAlert(sMSGTip, msgValidacion);
        }

    }


    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@provCveClie", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            if (rTxtNombre.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provConNom", DbType.String, 50, ParameterDirection.Input, rTxtNombre.Text.Trim());
            }
            if (rTxtApPaterno.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provConAPat", DbType.String, 50, ParameterDirection.Input, rTxtApPaterno.Text.Trim());
            }
            if (rTxtApMaterno.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provConAMat", DbType.String, 50, ParameterDirection.Input, rTxtApMaterno.Text.Trim());
            }
            

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
            if (rTxtCallesAledanas.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, 50, ParameterDirection.Input, rTxtCallesAledanas.Text.Trim());
            }
            if (rTxtReferencia.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, 50, ParameterDirection.Input, rTxtReferencia.Text.Trim());
            }
            if (rTxtTelefono1.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, 30, ParameterDirection.Input, rTxtTelefono1.Text.Trim());
            }
            if (rTxtTelefono2.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, 30, ParameterDirection.Input, rTxtTelefono2.Text.Trim());
            }
            if (rTxtFax.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, 30, ParameterDirection.Input, rTxtFax.Text.Trim());
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
                ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int64, 0, ParameterDirection.Input, hdfIdRegSel.Value);
            }


            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //recorrerRefe();

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();

                Int64 sprovConId = Convert.ToInt64(ds.Tables[0].Rows[0]["provConId"].ToString());

                GuardaOtrosDatos(sprovConId);
                recorrerRefe(sprovConId);
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    InicioPagina();
                    rGdvDatos.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdvDatos.AllowMultiRowSelection = true;
                    
                }
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

                    string sCve = dataItem.GetDataKeyValue("provConId").ToString();

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        //ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, (20), ParameterDirection.Input, PagLoc_folio_Selection);
                        //ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int64, 0, ParameterDirection.Input, sCve);

                        //ProcBD.AgregarParametrosProcedimiento("@cliDirEntId", DbType.Int64, 0, ParameterDirection.Input, sCve);
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

                    //llenadata_unidadmedida();
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

    public void recorrerRefe(Int64 valor)
    {
        if (PagLoc_folio_Selection == "")
        {
            hdfBtnAccion.Value = Convert.ToString(1);
        }

        int unomas = 0;
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            unomas = unomas + 1;
            var references = dli.FindControl("txtrefclie") as RadTextBox;

            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

                ProcBD.NombreProcedimiento = "sp_ProveedorContactosRefVar";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (10), ParameterDirection.Input, "PROVCONT");
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, (1), ParameterDirection.Input, unomas);
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (1), ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@revavalRef", DbType.String, 15, ParameterDirection.Input, references.Text);
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 20, ParameterDirection.Input, PagLoc_folio_Selection);
                ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int64, 0, ParameterDirection.Input, valor);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    #endregion

    #region FUNCIONES
    private DataSet llenadatalistVarRef(Int32 revaTip, int RadTextProvConId)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, PagLoc_folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (10), ParameterDirection.Input, "PROVCONT");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);
        ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int32, (5), ParameterDirection.Input, RadTextProvConId);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }

            if (rCboPais.SelectedValue == "")
            {
                rCboPais.CssClass = "cssTxtInvalid";
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboPais.BorderColor = System.Drawing.Color.Transparent; }

            
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
            if (rTxtNombre.Text.Trim() == "")
            {
                rTxtNombre.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtNombre.CssClass = "cssTxtEnabled"; }


            if (rCboPais.SelectedValue == "")
            {
                rCboPais.CssClass = "cssTxtInvalid";
                rCboPais.BorderWidth = Unit.Pixel(1);
                rCboPais.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboPais.BorderColor = System.Drawing.Color.Transparent; }


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

    public void Habil_ValRef()
    {
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            var references = dli.FindControl("txtrefclie") as RadTextBox;
            references.Enabled = true;
        }
        DataListOtrosDatos.Enabled = true;
    }

    public void limpiar_ValRef()
    {
        foreach (DataListItem dli in DATAREFCLIE.Items)
        {
            var references = dli.FindControl("txtrefclie") as RadTextBox;
            references.Text = "";
        }
        DataListOtrosDatos.Enabled = false;
    } 

    private void ControlesAccion()
    {
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
        rCboPais.Enabled = false;
        rCboEntidadFed.Enabled = false;
        rCboProvincia.Enabled = false;
        rTxtColonia.Enabled = false;
        rTxtCalle.Enabled = false;
        rTxtNoExt.Enabled = false;
        rTxtNoInt.Enabled = false;
        rTxtCallesAledanas.Enabled = false;
        rTxtCodigoPostal.Enabled = false;
        rTxtReferencia.Enabled = false;
        rTxtTelefono1.Enabled = false;
        rTxtTelefono2.Enabled = false;
        rTxtFax.Enabled = false;
        Desa_ValRef();

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
            rTxtNombre.Enabled = false;
            rTxtApPaterno.Enabled = false;
            rTxtApMaterno.Enabled = false;
            rCboPais.Enabled = false;
            rCboEntidadFed.Enabled = false;
            rCboProvincia.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNoExt.Enabled = false;
            rTxtNoInt.Enabled = false;
            rTxtCallesAledanas.Enabled = false;
            rTxtCodigoPostal.Enabled = false;
            rTxtReferencia.Enabled = false;
            rTxtTelefono1.Enabled = false;
            rTxtTelefono2.Enabled = false;
            rTxtFax.Enabled = false;
            Desa_ValRef();


            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            rCboPais.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rCboProvincia.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            rTxtFax.Text = "";
            limpiar_ValRef();
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
                rCboPais.Enabled = true;
                rCboEntidadFed.Enabled = false;
                rCboProvincia.Enabled = false;
                rTxtColonia.Enabled = true;
                rTxtCalle.Enabled = true;
                rTxtNoExt.Enabled = true;
                rTxtNoInt.Enabled = true;
                rTxtCallesAledanas.Enabled = true;
                rTxtCodigoPostal.Enabled = true;
                rTxtReferencia.Enabled = true;
                rTxtTelefono1.Enabled = true;
                rTxtTelefono2.Enabled = true;
                rTxtFax.Enabled = true;


                rTxtNombre.Text = "";
                rTxtApPaterno.Text = "";
                rTxtApMaterno.Text = "";
                rCboPais.ClearSelection();
                rCboEntidadFed.ClearSelection();
                rCboProvincia.ClearSelection();
                rTxtColonia.Text = "";
                rTxtCalle.Text = "";
                rTxtNoExt.Text = "";
                rTxtNoInt.Text = "";
                rTxtCallesAledanas.Text = "";
                rTxtCodigoPostal.Text = "";
                rTxtReferencia.Text = "";
                rTxtTelefono1.Text = "";
                rTxtTelefono2.Text = "";
                rTxtFax.Text = "";
                limpiar_ValRef();

                Habil_ValRef();
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
                rCboPais.Enabled = true;
                rCboEntidadFed.Enabled = true;
                rCboProvincia.Enabled = true;
                rTxtColonia.Enabled = true;
                rTxtCalle.Enabled = true;
                rTxtNoExt.Enabled = true;
                rTxtNoInt.Enabled = true;
                rTxtCallesAledanas.Enabled = true;
                rTxtCodigoPostal.Enabled = true;
                rTxtReferencia.Enabled = true;
                rTxtTelefono1.Enabled = true;
                rTxtTelefono2.Enabled = true;
                rTxtFax.Enabled = true;
                Habil_ValRef();

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

            //ELIMIAR
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
                rCboPais.Enabled = false;
                rCboEntidadFed.Enabled = false;
                rCboProvincia.Enabled = false;
                rTxtColonia.Enabled = false;
                rTxtCalle.Enabled = false;
                rTxtNoExt.Enabled = false;
                rTxtNoInt.Enabled = false;
                rTxtCallesAledanas.Enabled = false;
                rTxtCodigoPostal.Enabled = false;
                rTxtReferencia.Enabled = false;
                rTxtTelefono1.Enabled = false;
                rTxtTelefono2.Enabled = false;
                rTxtFax.Enabled = false;
                Desa_ValRef();

                rTxtNombre.Text = "";
                rTxtApPaterno.Text = "";
                rTxtApMaterno.Text = "";
                rCboPais.ClearSelection();
                rCboEntidadFed.ClearSelection();
                rCboProvincia.ClearSelection();
                rTxtColonia.Text = "";
                rTxtCalle.Text = "";
                rTxtNoExt.Text = "";
                rTxtNoInt.Text = "";
                rTxtCallesAledanas.Text = "";
                rTxtCodigoPostal.Text = "";
                rTxtReferencia.Text = "";
                rTxtTelefono1.Text = "";
                rTxtTelefono2.Text = "";
                rTxtFax.Text = "";
                limpiar_ValRef();
            }
        }


        if (Result == false)
        {
            rTxtNombre.Enabled = false;
            rTxtApPaterno.Enabled = false;
            rTxtApMaterno.Enabled = false;
            rCboPais.Enabled = false;
            rCboEntidadFed.Enabled = false;
            rCboProvincia.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNoExt.Enabled = false;
            rTxtNoInt.Enabled = false;
            rTxtCallesAledanas.Enabled = false;
            rTxtCodigoPostal.Enabled = false;
            rTxtReferencia.Enabled = false;
            rTxtTelefono1.Enabled = false;
            rTxtTelefono2.Enabled = false;
            rTxtFax.Enabled = false;
            Desa_ValRef();

            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            rCboPais.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rCboProvincia.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            rTxtFax.Text = "";
            limpiar_ValRef();
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
            rCboPais.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rCboProvincia.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            rTxtFax.Text = "";
            limpiar_ValRef();
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
            rCboPais.Enabled = false;
            rCboEntidadFed.Enabled = false;
            rCboProvincia.Enabled = false;
            rTxtColonia.Enabled = false;
            rTxtCalle.Enabled = false;
            rTxtNoExt.Enabled = false;
            rTxtNoInt.Enabled = false;
            rTxtCallesAledanas.Enabled = false;
            rTxtCodigoPostal.Enabled = false;
            rTxtReferencia.Enabled = false;
            rTxtTelefono1.Enabled = false;
            rTxtTelefono2.Enabled = false;
            rTxtFax.Enabled = false;
            Desa_ValRef();

            rTxtNombre.Text = "";
            rTxtApPaterno.Text = "";
            rTxtApMaterno.Text = "";
            rCboPais.ClearSelection();
            rCboEntidadFed.ClearSelection();
            rCboProvincia.ClearSelection();
            rTxtColonia.Text = "";
            rTxtCalle.Text = "";
            rTxtNoExt.Text = "";
            rTxtNoInt.Text = "";
            rTxtCallesAledanas.Text = "";
            rTxtCodigoPostal.Text = "";
            rTxtReferencia.Text = "";
            rTxtTelefono1.Text = "";
            rTxtTelefono2.Text = "";
            rTxtFax.Text = "";
            limpiar_ValRef();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }



    public void LlenaDLOtrosDatos(int iProvConId)
    {
        DataListOtrosDatos.DataSource = dsOtrosDatos(iProvConId);
        DataListOtrosDatos.RepeatColumns = 2;
        DataListOtrosDatos.DataBind();
    }

    DataSet dsOtrosDatos(int iProvConId)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ProveedorContactosOtrosDatos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int32, 0, ParameterDirection.Input, iProvConId);
        ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "PROVCONT");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    public void GuardaOtrosDatos(Int64 iProvConId)
    {
        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListOtrosDatos.Items)
        {
            var valTxt = dli.FindControl("txt_OtrosDatos") as RadTextBox;
            string DatCve = DataListOtrosDatos.DataKeys[Countkeyarray].ToString();
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ProveedorContactosOtrosDatos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@provConId", DbType.Int32, 0, ParameterDirection.Input, iProvConId);
            ProcBD.AgregarParametrosProcedimiento("@otroDatCve", DbType.String, 10, ParameterDirection.Input, DatCve);
            ProcBD.AgregarParametrosProcedimiento("@otroDatTip", DbType.String, 10, ParameterDirection.Input, "PROVCONT");
            ProcBD.AgregarParametrosProcedimiento("@otroDatVal", DbType.String, 200, ParameterDirection.Input, valTxt.Text);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            Countkeyarray += 1;
        }
    }



    #endregion

}