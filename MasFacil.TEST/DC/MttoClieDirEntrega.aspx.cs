using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Drawing;


public partial class DC_MttoClieDirEntrega : System.Web.UI.Page
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
    private string Pag_cliCve;
    private string cliDirEntId;

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
                desacontrol();
            }
        }
    }

    //=====> EVENTOS CONTROLES

    protected void rad_domEntre_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rad_domEntre.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()||
                hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
                hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
                hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
                hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                string val = dataItem["cliDirEntCve"].Text;
                string val1 = dataItem["domCol"].Text;
                string val2 = dataItem["domClle"].Text;
                string val3 = dataItem["domNInt"].Text;
                string val4 = dataItem["domNExt"].Text;
                string val5 = dataItem["domCllsA"].Text;
                string val6 = dataItem["domCP"].Text;
                string val7 = dataItem["domRef"].Text;
                string val8 = dataItem["domTel"].Text;
                string val9= dataItem["domTel2"].Text;
                string val10 = dataItem["domFax"].Text;

                if (val == "&nbsp;")
                {
                    txtbox_entrega.Text = "";
                }
                else
                {
                    txtbox_entrega.Text = dataItem["cliDirEntCve"].Text;
                }
                if (val1== "&nbsp;")
                {
                    txt_colonia.Text = "";
                }
                else
                {
                    txt_colonia.Text = dataItem["domCol"].Text;
                }
                if (val2 == "&nbsp;")
                {
                    txtbox_calle.Text = "";
                }
                else
                {
                    txtbox_calle.Text = dataItem["domClle"].Text;
                }

                if (val3 == "&nbsp;")
                {
                    txtbox_NoInter.Text = "";
                }
                else
                {
                    txtbox_NoInter.Text = dataItem["domNInt"].Text;
                }

                if (val4 == "&nbsp;")
                {
                    txtbox_NoExter.Text = "";
                }
                else
                {
                    txtbox_NoExter.Text = dataItem["domNExt"].Text;
                }
                
                if (val5 == "&nbsp;")
                {
                    txtbox_calleAled.Text = "";
                }
                else
                {
                    txtbox_calleAled.Text = dataItem["domCllsA"].Text;
                }
                
                if (val6 == "&nbsp;")
                {
                    txt_codPost.Text = "";
                }
                else
                {
                    txt_codPost.Text = dataItem["domCP"].Text;
                }
                
                if (val7 == "&nbsp;")
                {
                    txt_ref.Text = "";
                }
                else
                {
                    txt_ref.Text = dataItem["domRef"].Text;
                }
                
                if (val8 == "&nbsp;")
                {
                    txtbox_tel2.Text = "";
                }
                else
                {
                    txtbox_tel2.Text = dataItem["domTel"].Text;
                }

                if (val9 == "&nbsp;")
                {
                    txtbox_tel2.Text = "";
                }
                else
                {
                    txtbox_tel1.Text = dataItem["domTel2"].Text;
                }

                if (val10 == "&nbsp;")
                {
                    txtbox_fax.Text = "";
                }
                else
                {
                    txtbox_fax.Text = dataItem["domFax"].Text;
                }
                
                cmbobox_pais.SelectedValue = dataItem["paisCve"].Text.ToString();
                llenar_cboEntidadFed();



                if (dataItem["entFCve"].Text == "&nbsp;")
                {
                    cmbobox_estado.ClearSelection();
                    cmbobox_poblacion.ClearSelection();
                    
                    //cmbobox_estado.Enabled=true;
                    //cmbobox_poblacion.Enabled = false;
                }
                else
                {
                    cmbobox_estado.ClearSelection();
                    cmbobox_estado.SelectedValue = dataItem["entFCve"].Text.ToString();
                    llena_cboPoblacion();
                    cmbobox_poblacion.SelectedValue = dataItem["provCve"].Text.ToString();
                }
                

                
               
                //habilcontrol();
                if (cmbobox_estado.SelectedValue =="")
                {
                    cmbobox_poblacion.Enabled=false;
                }
                txtbox_entrega.Enabled = false ;
            }


        }
        ////if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        ////    hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        ////    hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        ////       hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
        ////{
        ////    this.txtbox_entrega.Enabled = false;
        ////    this.cmbobox_pais.Enabled = false;
        ////    this.cmbobox_estado.Enabled = false;
        ////    this.cmbobox_poblacion.Enabled = false;
        ////    this.txt_colonia.Enabled = false;
        ////    this.txtbox_calle.Enabled = false;
        ////    this.txtbox_NoInter.Enabled = false;
        ////    this.txtbox_NoExter.Enabled = false;
        ////    this.txtbox_calleAled.Enabled = false;
        ////    this.txt_codPost.Enabled = false;
        ////    this.txt_ref.Enabled = false;
        ////    this.txtbox_tel1.Enabled = false;
        ////    this.txtbox_tel2.Enabled = false;
        ////    this.txtbox_fax.Enabled = false;
        ////    rBtnGuardar.Enabled = false;
        ////    rBtnCancelar.Enabled = false;
        ////}
        ////else
        ////{
        ////    rBtnGuardar.Enabled = true;
        ////    rBtnCancelar.Enabled = true;
        ////}

    }

    protected void cmbobox_pais_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cmbobox_pais.SelectedValue != "")
        {
            cmbobox_poblacion.ClearSelection();
            cmbobox_poblacion.Enabled = false;
            cmbobox_estado.Enabled = true;
            cmbobox_estado.ClearSelection();
            llenar_cboEntidadFed();
        }else
        {
            cmbobox_pais.ClearSelection();
            cmbobox_estado.ClearSelection();
            cmbobox_estado.Enabled = false;
            cmbobox_poblacion.ClearSelection();
            cmbobox_poblacion.Enabled = false;
        }
        
    }

    protected void cmbobox_estado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (cmbobox_estado.SelectedValue!= "")
        {
            cmbobox_poblacion.Enabled = true;
            cmbobox_poblacion.ClearSelection();
            llena_cboPoblacion();
        }else
        {
            cmbobox_poblacion.ClearSelection();
            cmbobox_poblacion.Enabled = false;
        }

    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    //Nuevo
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();


    }
    //MODIFICAR//
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    //ELIMINAR//
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();

    }
    //LIMPIAR//
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //cmbobox_pais.ClearSelection();
        //cmbobox_estado.ClearSelection();
        //cmbobox_poblacion.ClearSelection();
        //ControlesAccion();
        //cssEna();
         EjecutaAccionLimpiar();

    }



    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
        cargarGridEntrega();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //cargarGridEntrega();
        //clearcontrol();
        //desacontrol();
        InicioPagina();

    }
    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_cliCve = Convert.ToString(Session["folio_Selection"]);
        cliDirEntId = Convert.ToString(Session["cliDirEntId"]);
    }
    private void InicioPagina()
    {

        hdfBtnAccion.Value = "";
        ControlesAccion();
        DatosCliente();
        llenar_cboPais();
        cargarGridEntrega();
        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;

        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            lbl_subclie.Visible = true;
        }
        else
        {
            rLblSubClie.Visible = false;
            lbl_subclie.Visible = false;
        }
        rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
        rad_domEntre.AllowMultiRowSelection = true;
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


    private string validaEjecutaAccion(ref string sMSGTip)
    {
        cssEna();

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        cmbobox_pais.BorderWidth = Unit.Pixel(1);
        cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;

        cmbobox_estado.BorderWidth = Unit.Pixel(1);
        cmbobox_estado.BorderColor = System.Drawing.Color.Transparent;

        cmbobox_poblacion.BorderWidth = Unit.Pixel(1);
        cmbobox_poblacion.BorderColor = System.Drawing.Color.Transparent;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (txtbox_entrega.Text.Trim() == "")
            {
                txtbox_entrega.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (cmbobox_pais.SelectedValue == "")
            {
                cmbobox_pais.CssClass = "cssTxtInvalid";
                cmbobox_pais.BorderWidth = Unit.Pixel(1);
                cmbobox_pais.BorderColor = System.Drawing.Color.Red;

                cmbobox_pais.Focus();

                camposInc += 1;
            }
            else { cmbobox_pais.CssClass = "cssTxtEnabled"; }
            

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rad_domEntre.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (txtbox_entrega.Text.Trim() == "")
            {
                txtbox_entrega.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (cmbobox_pais.SelectedValue == "")
            {
                cmbobox_pais.CssClass = "cssTxtInvalid";
                cmbobox_pais.BorderWidth = Unit.Pixel(1);
                cmbobox_pais.BorderColor = System.Drawing.Color.Red;

                cmbobox_pais.Focus();

                camposInc += 1;
            }
            else { cmbobox_pais.CssClass = "cssTxtEnabled"; }

            //if (cmbobox_estado.SelectedValue == "")
            //{
            //    cmbobox_estado.CssClass = "cssTxtInvalid";
            //    cmbobox_estado.BorderWidth = Unit.Pixel(1);
            //    cmbobox_estado.BorderColor = System.Drawing.Color.Red;

            //    cmbobox_estado.Focus();

            //    camposInc += 1;
            //}
            //else { cmbobox_estado.CssClass = "cssTxtEnabled"; }

            //if (cmbobox_poblacion.SelectedValue == "")
            //{
            //    cmbobox_poblacion.CssClass = "cssTxtInvalid";
            //    cmbobox_poblacion.BorderWidth = Unit.Pixel(1);
            //    cmbobox_poblacion.BorderColor = System.Drawing.Color.Red;

            //    cmbobox_poblacion.Focus();

            //    camposInc += 1;
            //}
           // else { cmbobox_poblacion.CssClass = "cssTxtEnabled"; }
        
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rad_domEntre.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        return sResult;
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
           
                rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
                rad_domEntre.AllowMultiRowSelection = true;
            }

        }
        else
        {

            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private bool AddDirecEntregaSelect()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, Pag_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string s = ds.Tables[0].Rows[0]["provId"].ToString();
            if (ds.Tables[0].Rows[0]["cliDirEntCve"].ToString() != "")
            {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    public void EjecutaSpAcciones()
    {
       

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, (20), ParameterDirection.Input, Pag_cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);

            //ProcBD.AgregarParametrosProcedimiento("@cliDirEntId", DbType.Int64, 0, ParameterDirection.Input, cliDirEntId);

            ProcBD.AgregarParametrosProcedimiento("@cliDirEntCve", DbType.String, (10), ParameterDirection.Input, txtbox_entrega.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cliDirEntOrd", DbType.Int64, 0, ParameterDirection.Input, 1);

            ProcBD.AgregarParametrosProcedimiento("@paisCve", DbType.String, 2, ParameterDirection.Input, cmbobox_pais.SelectedValue);
            if (cmbobox_estado.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@entFCve", DbType.String, 2, ParameterDirection.Input, cmbobox_estado.SelectedValue);
            }
            if (cmbobox_poblacion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 3, ParameterDirection.Input, cmbobox_poblacion.SelectedValue);

            }
            if (txt_codPost.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, (50), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domCP", DbType.String, (50), ParameterDirection.Input, txt_codPost.Text.Trim());
            }
            if (txt_colonia.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, (50), ParameterDirection.Input,"");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domCol", DbType.String, (50), ParameterDirection.Input, txt_colonia.Text.Trim());
            }
            
             if (txtbox_calle.Text.Trim() == "") {
                    ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, (50), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domClle", DbType.String, (50), ParameterDirection.Input, txtbox_calle.Text.Trim());
            }
            if (txtbox_calleAled.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, (50), ParameterDirection.Input, "");

            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domCllsA", DbType.String, (50), ParameterDirection.Input, txtbox_calleAled.Text.Trim());
            }
            if (txtbox_NoInter.Text.Trim() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNInt", DbType.String, 30, ParameterDirection.Input, txtbox_NoInter.Text.Trim());
            }
       
            if (txtbox_NoExter.Text.Trim()!= "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domNExt", DbType.String, 30, ParameterDirection.Input, txtbox_NoExter.Text.Trim());
            }
   
            if (txt_ref.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, (50), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domRef", DbType.String, (50), ParameterDirection.Input, txt_ref.Text.Trim());
            }
            if (txtbox_tel1.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, (30), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel", DbType.String, (30), ParameterDirection.Input, txtbox_tel1.Text.Trim());
            }
            if (txtbox_tel2.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, (30), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domTel2", DbType.String, (30), ParameterDirection.Input, txtbox_tel2.Text.Trim());
            }
            if (txtbox_fax.Text.Trim() == "")
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, (30), ParameterDirection.Input, "");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, (30), ParameterDirection.Input, txtbox_fax.Text.Trim());
            }
            //if (txtbox_fax.Text == "")
            //{
            //    ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, (30), ParameterDirection.Input, "xx");
            //}
            //else
            //{
            //ProcBD.AgregarParametrosProcedimiento("@domFax", DbType.String, (30), ParameterDirection.Input, txtbox_fax.Text);
            //}



            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "3")
                {


                }else
                {
                    desacontrol();
                    clearcontrol();
                    InicioPagina();

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




            foreach (GridDataItem i in rad_domEntre.SelectedItems)
            {

                var dataItem = rad_domEntre.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string sCve = dataItem.GetDataKeyValue("cliDirEntId").ToString();
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, (20), ParameterDirection.Input, Pag_cliCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);

                        ProcBD.AgregarParametrosProcedimiento("@cliDirEntId", DbType.Int64, 0, ParameterDirection.Input, sCve);
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
                    cargarGridEntrega();
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

                    cargarGridEntrega();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = false;



    }



    //private void ControlesAccion()
    //{

    //    //===> CONTROLES GENERAL
    //    rad_domEntre.MasterTableView.ClearSelectedItems();
    
    //    this.txtbox_entrega.Text = "";
    //    this.txt_colonia.Text = "";
    //    this.txtbox_calle.Text = "";
    //    this.txtbox_NoInter.Text = "";
    //    this.txtbox_NoExter.Text = "";
    //    this.txtbox_calleAled.Text = "";
    //    this.txt_codPost.Text = "";
    //    this.txt_ref.Text = "";
    //    this.txtbox_tel1.Text = "";
    //    this.txtbox_tel2.Text = "";
    //    this.txtbox_fax.Text = "";

    //    this.txtbox_entrega.CssClass = "cssTxtEnabled";
    //    this.txt_colonia.CssClass = "cssTxtEnabled";
    //    this.txtbox_calle.CssClass = "cssTxtEnabled";
    //    this.txtbox_NoInter.CssClass = "cssTxtEnabled";
    //    this.txtbox_NoExter.CssClass = "cssTxtEnabled";
    //    this.txtbox_calleAled.CssClass = "cssTxtEnabled";
    //    this.txt_codPost.CssClass = "cssTxtEnabled";
    //    this.txt_ref.CssClass = "cssTxtEnabled";
    //    this.txtbox_tel1.CssClass = "cssTxtEnabled";
    //    this.txtbox_tel2.CssClass = "cssTxtEnabled";
    //    this.txtbox_fax.CssClass = "cssTxtEnabled";

    //    cmbobox_pais.Enabled = false;
    //    cmbobox_estado.Enabled = false;
    //    cmbobox_poblacion.Enabled = false;

       
    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;

    //    //===> CONTROLES POR ACCION

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.cmbobox_pais.ClearSelection();
    //        this.cmbobox_estado.ClearSelection();
    //        this.cmbobox_poblacion.ClearSelection();
    //        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.txtbox_entrega.Enabled = true;
    //        this.cmbobox_pais.Enabled = true;
    //        //this.cmbobox_estado.Enabled = true;
    //        //this.cmbobox_poblacion.Enabled = true;
    //        this.txt_colonia.Enabled = true;
    //        this.txtbox_calle.Enabled = true;
    //        this.txtbox_NoInter.Enabled = true;
    //        this.txtbox_NoExter.Enabled = true;
    //        this.txtbox_calleAled.Enabled = true;
    //        this.txt_codPost.Enabled = true;
    //        this.txt_ref.Enabled = true;
    //        this.txtbox_tel1.Enabled = true;
    //        this.txtbox_tel2.Enabled = true;
    //        this.txtbox_fax.Enabled = true;
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        ////if (AddDirecEntregaSelect() == false)
    //        ////{
    //            this.cmbobox_pais.ClearSelection();
    //            this.cmbobox_estado.ClearSelection();
    //            this.cmbobox_poblacion.ClearSelection();
    //            this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
    //            this.rad_domEntre.AllowMultiRowSelection = false;
    //            this.txtbox_entrega.Enabled = false;
    //            this.cmbobox_pais.Enabled = false;
    //            this.cmbobox_estado.Enabled = false;
    //            //this.cmbobox_poblacion.Enabled = false;
    //            this.txt_colonia.Enabled = false;
    //            this.txtbox_calle.Enabled = false;
    //            this.txtbox_NoInter.Enabled = false;
    //            this.txtbox_NoExter.Enabled = false;
    //            this.txtbox_calleAled.Enabled = false;
    //            this.txt_codPost.Enabled = false;
    //            this.txt_ref.Enabled = false;
    //            this.txtbox_tel1.Enabled = false;
    //            this.txtbox_tel2.Enabled = false;
    //            this.txtbox_fax.Enabled = false;
    //    }
    //    //ELIMIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        this.cmbobox_pais.ClearSelection();
    //        this.cmbobox_estado.ClearSelection();
    //        this.cmbobox_poblacion.ClearSelection();
    //        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rad_domEntre.AllowMultiRowSelection = true;
    //        this.txtbox_entrega.Enabled = false;
    //        this.cmbobox_pais.Enabled = false;
    //        this.cmbobox_estado.Enabled = false;
    //        this.cmbobox_poblacion.Enabled = false;
    //        this.txt_colonia.Enabled = false;
    //        this.txtbox_calle.Enabled = false;
    //        this.txtbox_NoInter.Enabled = false;
    //        this.txtbox_NoExter.Enabled = false;
    //        this.txtbox_calleAled.Enabled = false;
    //        this.txt_codPost.Enabled = false;
    //        this.txt_ref.Enabled = false;
    //        this.txtbox_tel1.Enabled = false;
    //        this.txtbox_tel2.Enabled = false;
    //        this.txtbox_fax.Enabled = false;
    //    }


    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
 
    //        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.txtbox_entrega.Enabled = false;
    //        this.cmbobox_pais.Enabled = false;
    //        this.cmbobox_estado.Enabled = false;
    //        this.cmbobox_poblacion.Enabled = false;
    //        this.txt_colonia.Enabled = false;
    //        this.txtbox_calle.Enabled = false;
    //        this.txtbox_NoInter.Enabled = false;
    //        this.txtbox_NoExter.Enabled = false;
    //        this.txtbox_calleAled.Enabled = false;
    //        this.txt_codPost.Enabled = false;
    //        this.txt_ref.Enabled = false;
    //        this.txtbox_tel1.Enabled = false;
    //        this.txtbox_tel2.Enabled = false;
    //        this.txtbox_fax.Enabled = false;



    //        txtbox_entrega.CssClass = "cssTxtEnabled";
    //        cmbobox_pais.BorderWidth = Unit.Pixel(1);
    //        cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;
    //        cmbobox_estado.BorderWidth = Unit.Pixel(1);
    //        cmbobox_estado.BorderColor = System.Drawing.Color.Transparent;
    //        cmbobox_poblacion.BorderWidth = Unit.Pixel(1);
    //        cmbobox_poblacion.BorderColor = System.Drawing.Color.Transparent;

    //        txt_colonia.CssClass = "cssTxtEnabled";
    //        txtbox_calle.CssClass = "cssTxtEnabled";
    //        txtbox_NoInter.CssClass = "cssTxtEnabled";
    //        txtbox_NoExter.CssClass = "cssTxtEnabled";
    //        txtbox_calleAled.CssClass = "cssTxtEnabled";
    //        txt_codPost.CssClass = "cssTxtEnabled";
    //        txt_ref.CssClass = "cssTxtEnabled";
    //        txtbox_tel1.CssClass = "cssTxtEnabled";
    //        txtbox_tel2.CssClass = "cssTxtEnabled";
    //        txtbox_fax.CssClass = "cssTxtEnabled";

    //        cmbobox_pais.CssClass = "cssTxtEnabled";
    //        cmbobox_estado.CssClass = "cssTxtEnabled";
    //        cmbobox_poblacion.CssClass = "cssTxtEnabled";

    //        rBtnCancelar.Enabled = false;
    //        rBtnGuardar.Enabled = false;


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




    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion


    #region FUNCIONES

    public void llenar_cboPais()
    {
        FnCtlsFillIn.RadComboBox_Paises(Pag_sConexionLog, ref cmbobox_pais, true, false);
    }

    private void llenar_cboEntidadFed()
    {

        FnCtlsFillIn.RadComboBox_EntidadesFed(Pag_sConexionLog, cmbobox_pais.SelectedValue.ToString(), ref cmbobox_estado, true, false);

    }


    private void llena_cboPoblacion()
    {


        FnCtlsFillIn.RadComboBox_Provincias(Pag_sConexionLog, cmbobox_pais.SelectedValue ,  cmbobox_estado.SelectedValue.ToString(), ref cmbobox_poblacion, true, false);

    }
    public void cssEna()
    {
        txtbox_entrega.CssClass = "cssTxtEnabled";
        txt_colonia.CssClass = "cssTxtEnabled";
        txtbox_calle.CssClass = "cssTxtEnabled";
        txtbox_NoInter.CssClass = "cssTxtEnabled";
        txtbox_NoExter.CssClass = "cssTxtEnabled";
        txtbox_calleAled.CssClass = "cssTxtEnabled";
        txt_codPost.CssClass = "cssTxtEnabled";
        txt_ref.CssClass = "cssTxtEnabled";
        txtbox_tel1.CssClass = "cssTxtEnabled";
        txtbox_tel2.CssClass = "cssTxtEnabled";
        txtbox_fax.CssClass = "cssTxtEnabled";
        cmbobox_pais.BorderWidth = Unit.Pixel(0);
        cmbobox_pais.BackColor = System.Drawing.Color.Transparent;
        cmbobox_estado.BorderWidth = Unit.Pixel(0);
        cmbobox_estado.BackColor = System.Drawing.Color.Transparent;
        cmbobox_poblacion.BorderWidth = Unit.Pixel(0);
        cmbobox_poblacion.BackColor = System.Drawing.Color.Transparent;

    }
    public void DatosCliente()
    {
        //radtxt_articulo.Visible = false;
        if (Pag_cliCve == "")
        {
            desacontrol();
            clearcontrol();
        }
        else
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Clientes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, Pag_cliCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            lbl_Clien.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveClie"]).Replace(" ", "&nbsp;");
            lbl_subclie.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveSubClie"]).Replace(" ", "&nbsp;");
            lbl_Nombre.Text = Convert.ToString(ds.Tables[0].Rows[0]["clieNom"]);
        }
    }
    public void cargarGridEntrega()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteDireccionesEntrega";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, Pag_cliCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rad_domEntre, ds);
    }
    public void desacontrol()
    {
        cmbobox_pais.ClearSelection();
        cmbobox_estado.ClearSelection();
        cmbobox_poblacion.ClearSelection();

        cmbobox_poblacion.Text = string.Empty;
        cmbobox_estado.Text = string.Empty;
        cmbobox_pais.Text = string.Empty;

        cmbobox_pais.EmptyMessage = "Seleccionar";
        cmbobox_estado.EmptyMessage = "Seleccionar";
        cmbobox_poblacion.EmptyMessage = "Seleccionar";

        rBtnCancelar.Enabled = false;
        rBtnGuardar.Enabled = false;

        lbl_Nombre.Enabled = false;
        txtbox_entrega.Enabled = false;
        cmbobox_pais.Enabled = false;
        cmbobox_estado.Enabled = false;
        cmbobox_poblacion.Enabled = false;
        txtbox_calle.Enabled = false;
        txt_colonia.Enabled = false;
        txtbox_calle.Enabled = false;
        txtbox_NoInter.Enabled = false;
        txtbox_NoExter.Enabled = false;
        txtbox_calleAled.Enabled = false;
        txt_codPost.Enabled = false;
        txt_ref.Enabled = false;
        txtbox_tel1.Enabled = false;
        txtbox_tel2.Enabled = false;
        txtbox_fax.Enabled = false;
    }

    public void habilcontrol()
    {

        txtbox_entrega.Enabled = true;
        cmbobox_pais.Enabled = true;
        cmbobox_estado.Enabled = true;
        cmbobox_poblacion.Enabled = true;
        txtbox_calle.Enabled = true;
        txt_colonia.Enabled = true;
        txtbox_calle.Enabled = true;
        txtbox_NoInter.Enabled = true;
        txtbox_NoExter.Enabled = true;
        txtbox_calleAled.Enabled = true;
        txt_codPost.Enabled = true;
        txt_ref.Enabled = true;
        txtbox_tel1.Enabled = true;
        txtbox_tel2.Enabled = true;
        txtbox_fax.Enabled = true;



        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;

    }

    public void clearcontrol()
    {

        txtbox_entrega.Text = "";
        txtbox_calle.Text = "";
        txt_colonia.Text = "";
        txtbox_calle.Text = "";
        txtbox_NoInter.Text = ""; 
        txtbox_NoExter.Text = "";
        txtbox_calleAled.Text = "";
        txt_codPost.Text = "";
        txt_ref.Text = "";
        txtbox_tel1.Text = "";
        txtbox_tel2.Text = ""; ;
        txtbox_fax.Text = "";

        cmbobox_pais.ClearSelection();
        cmbobox_estado.ClearSelection();
        cmbobox_poblacion.ClearSelection();
        cmbobox_pais.BorderWidth = Unit.Pixel(1);
        cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;
        cmbobox_estado.BorderWidth = Unit.Pixel(1);
        cmbobox_estado.BorderColor = System.Drawing.Color.Transparent;
        cmbobox_poblacion.BorderWidth = Unit.Pixel(1);
        cmbobox_poblacion.BorderColor = System.Drawing.Color.Transparent;



    }



    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        txtbox_entrega.CssClass = "cssTxtEnabled";
        cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;

        txtbox_entrega.Enabled = false;
        txt_colonia.Enabled = false;
        txtbox_calle.Enabled = false;
        txtbox_NoExter.Enabled = false;
        txtbox_NoInter.Enabled = false;
        txtbox_calle.Enabled = false;
        txtbox_calleAled.Enabled = false;
        txt_ref.Enabled = false;
        txtbox_tel1.Enabled = false;
        txtbox_tel2.Enabled = false;
        txtbox_fax.Enabled = false;

        cmbobox_pais.Enabled = false;
        cmbobox_estado.Enabled = false;
        cmbobox_poblacion.Enabled = false;


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
            txtbox_entrega.Enabled = false;
            txt_colonia.Enabled = false;
            txtbox_calle.Enabled = false;
            txtbox_NoExter.Enabled = false;
            txtbox_NoInter.Enabled = false;
            txt_codPost.Enabled = false;
            txtbox_calleAled.Enabled = false;
            txt_ref.Enabled = false;
            txtbox_tel1.Enabled = false;
            txtbox_tel2.Enabled = false;
            txtbox_fax.Enabled = false;


            cmbobox_pais.Enabled = false;
            cmbobox_estado.Enabled = false;
            cmbobox_poblacion.Enabled = false;
            cmbobox_pais.ClearSelection();
            cmbobox_estado.ClearSelection();
            cmbobox_poblacion.ClearSelection();
            txtbox_entrega.Text = "";
            txt_colonia.Text = "";
            txtbox_calle.Text = "";
            txtbox_NoExter.Text = "";
            txtbox_NoInter.Text = "";
            txtbox_calleAled.Text = "";
            txt_codPost.Text = "";
            txt_ref.Text = "";
            txtbox_tel1.Text = "";
            txtbox_tel2.Text = "";
            txtbox_fax.Text = "";
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
                this.rad_domEntre.ClientSettings.Selecting.AllowRowSelect = false;
                rad_domEntre.MasterTableView.ClearSelectedItems();

                cmbobox_pais.Enabled = true;
                txtbox_entrega.Enabled = true;
                txt_colonia.Enabled = true;
                txtbox_calle.Enabled = true;
                txtbox_NoExter.Enabled = true;
                txtbox_NoInter.Enabled = true;
                txt_codPost.Enabled = true;
                txtbox_calleAled.Enabled = true;
                txt_ref.Enabled = true;
                txtbox_tel1.Enabled = true;
                txtbox_tel2.Enabled = true;
                txtbox_fax.Enabled = true;

               
                cmbobox_estado.Enabled = false;
                cmbobox_poblacion.Enabled = false;

                cmbobox_pais.ClearSelection();
                cmbobox_estado.ClearSelection();
                cmbobox_poblacion.ClearSelection();
                txtbox_entrega.Text = "";
                txt_colonia.Text = "";
                txtbox_calle.Text = "";
                txtbox_NoExter.Text = "";
                txtbox_NoInter.Text = "";
                txtbox_calleAled.Text = "";
                txt_codPost.Text = "";
                txt_ref.Text = "";
                txtbox_tel1.Text = "";
                txtbox_tel2.Text = "";
                txtbox_fax.Text = "";


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rad_domEntre.AllowMultiRowSelection = false;


                
                txtbox_entrega.Enabled = false; 
                txt_colonia.Enabled = true;
                txtbox_calle.Enabled = true;
                txtbox_NoExter.Enabled = true;
                txtbox_NoInter.Enabled = true;
                txt_codPost.Enabled = true;
                txtbox_calleAled.Enabled = true;
                txt_ref.Enabled = true;
                txtbox_tel1.Enabled = true;
                txtbox_tel2.Enabled = true;
                txtbox_fax.Enabled = true;

                if (cmbobox_pais.SelectedValue != "")
                {
                    cmbobox_pais.Enabled = true;
                    cmbobox_estado.Enabled = true;
                    cmbobox_poblacion.Enabled = false;
                }
                if (cmbobox_estado.SelectedValue != "")
                {
                    cmbobox_estado.Enabled = true;
                    cmbobox_poblacion.Enabled = true;
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
                rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
                rad_domEntre.AllowMultiRowSelection = true;
                rad_domEntre.MasterTableView.ClearSelectedItems();

                txtbox_entrega.Enabled = false;
                txt_colonia.Enabled = false;
                txtbox_calle.Enabled = false;
                txtbox_NoExter.Enabled = false;
                txtbox_NoInter.Enabled = false;
                txt_codPost.Enabled = false;
                txtbox_calleAled.Enabled = false;
                txt_ref.Enabled = false;
                txtbox_tel1.Enabled = false;
                txtbox_tel2.Enabled = false;
                txtbox_fax.Enabled = false;


                cmbobox_pais.Enabled = false;
                cmbobox_estado.Enabled = false;
                cmbobox_poblacion.Enabled = false;

                cmbobox_pais.ClearSelection();
                cmbobox_estado.ClearSelection();
                cmbobox_poblacion.ClearSelection();


                txtbox_entrega.Text = "";
                txt_colonia.Text = "";
                txtbox_calle.Text = "";
                txtbox_NoExter.Text = "";
                txtbox_NoInter.Text = "";
                txtbox_calleAled.Text = "";
                txt_codPost.Text = "";
                txt_ref.Text = "";
                txtbox_tel1.Text = "";
                txtbox_tel2.Text = "";
                txtbox_fax.Text = "";
            }
        }

        if (Result == false)
        {
            txtbox_entrega.Enabled = false;
            txt_colonia.Enabled = false;
            txtbox_calle.Enabled = false;
            txtbox_NoExter.Enabled = false;
            txtbox_NoInter.Enabled = false;
            txt_codPost.Enabled = false;
            txtbox_calleAled.Enabled = false;
            txt_ref.Enabled = false;
            txtbox_tel1.Enabled = false;
            txtbox_tel2.Enabled = false;
            txtbox_fax.Enabled = false;

            cmbobox_pais.Enabled = false;
            cmbobox_estado.Enabled = false;
            cmbobox_poblacion.Enabled = false;

            txtbox_entrega.Text = "";
            txt_colonia.Text = "";
            txtbox_calle.Text = "";
            txtbox_NoExter.Text = "";
            txtbox_NoInter.Text = "";
            txtbox_calleAled.Text = "";
            txt_codPost.Text = "";
            txt_ref.Text = "";
            txtbox_tel1.Text = "";
            txtbox_tel2.Text = "";
            txtbox_fax.Text = "";
        }


    }
    
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rad_domEntre.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rad_domEntre, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rad_domEntre, GvVAS, ref sMSGTip, ref sResult) == false)
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
            txtbox_entrega.Text = "";
            txt_colonia.Text = "";
            txtbox_calle.Text = "";
            txtbox_NoExter.Text = "";
            txtbox_NoInter.Text = "";
            txtbox_calleAled.Text = "";
            txt_codPost.Text = "";
            txt_ref.Text = "";
            txtbox_tel1.Text = "";
            txtbox_tel2.Text = "";
            txtbox_fax.Text = "";


            cmbobox_pais.ClearSelection();
            cmbobox_estado.ClearSelection();
            cmbobox_poblacion.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rad_domEntre.ClientSettings.Selecting.AllowRowSelect = true;
            rad_domEntre.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            txtbox_entrega.CssClass = "cssTxtEnabled";
            cmbobox_pais.BorderColor = System.Drawing.Color.Transparent;

            txtbox_entrega.Enabled = false;
            txt_colonia.Enabled = false;
            txtbox_calle.Enabled = false;
            txtbox_NoExter.Enabled = false;
            txtbox_NoInter.Enabled = false;
            txt_codPost.Enabled = false;
            txtbox_calleAled.Enabled = false;
            txt_ref.Enabled = false;
            txtbox_tel1.Enabled = false;
            txtbox_tel2.Enabled = false;
            txtbox_fax.Enabled = false;


            cmbobox_pais.Enabled = false;
            cmbobox_estado.Enabled = false;
            cmbobox_poblacion.Enabled = false;

            cmbobox_pais.ClearSelection();
            cmbobox_estado.ClearSelection();
            cmbobox_poblacion.ClearSelection();

            txtbox_entrega.Text = "";
            txt_colonia.Text = "";
            txtbox_calle.Text = "";
            txtbox_NoExter.Text = "";
            txtbox_NoInter.Text = "";
            txtbox_calleAled.Text = "";
            txt_codPost.Text = "";
            txt_ref.Text = "";
            txtbox_tel1.Text = "";
            txtbox_tel2.Text = "";
            txtbox_fax.Text = "";

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