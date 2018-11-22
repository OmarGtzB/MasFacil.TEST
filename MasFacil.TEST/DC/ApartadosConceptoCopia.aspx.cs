using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

using System.Windows.Forms;

public partial class DC_ApartadosConceptoCopia : System.Web.UI.Page
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
    private Int64 Pag_sidM;
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
    
    protected void RadCheckBox_Configuracion_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadCheckBox_GuiaConta_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadCheckBox_MapeoConta_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadCheckBox_MapeoDoc_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadCheckBox_AutoridadUsr_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadCheckBox_Costos_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

    }
    
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {

    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }


    #endregion

    #region METODOS
    private void InicioPagina()
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString();
        obtieneCpto();
        LlenaValores();
    }



    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
            {
                if (ValidaIncluir() == true)
                {
                    EjecutaSpAcciones();
                }else {

                    string Tip = "", Result = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1060", ref Tip, ref Result);
                    ShowAlert(Tip, Result);

                }
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }



    private void EjecutaSpAcciones()
    {
        LlenaValores();
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoCopia";

        ProcBD.AgregarParametrosProcedimiento("@ciaCveOrigen", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoIdOrigen", DbType.Int32, 0, ParameterDirection.Input, Pag_sidM);
        
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int32, 0, ParameterDirection.Input, rTxtClave.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@cptoDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@cptoDefFolVal", DbType.String, 10, ParameterDirection.Input, rcboFoliador.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@cptoDefAsiConFolVal", DbType.String, 10, ParameterDirection.Input, rcboFoliador_AsientoCont.SelectedValue);
        
        ProcBD.AgregarParametrosProcedimiento("@copiaConfiguracion", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_Configuracion));
        ProcBD.AgregarParametrosProcedimiento("@copiaGuiasContabilidad", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_GuiaConta));
        ProcBD.AgregarParametrosProcedimiento("@copiaMapeoDocumento", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_MapeoDoc));
        ProcBD.AgregarParametrosProcedimiento("@copiaMapeoMovimiento", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_MapeoMovi));
        ProcBD.AgregarParametrosProcedimiento("@copiaAutoridadUsuarios", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_AutoridadUsr));
        ProcBD.AgregarParametrosProcedimiento("@copiaCostos", DbType.Int32, 0, ParameterDirection.Input, ValIncluir(RadCheckBox_Costos));
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            
            if (sEjecEstatus == "1")
            {
                //InicioPagina();
                string script = "window.close();";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarpagina", script, true);

            }else
            {
                ShowAlert(sEjecEstatus, sEjecMSG);
            }
            
        }
        
    }


    private int ValIncluir(RadCheckBox CheckBox) {

        if (CheckBox.Checked == true)
        {
            return 1;
        }else {
            return 0;
        }
        
    }



    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //Copia
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {

            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }


            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }
            
            if (this.rcboFoliador.SelectedIndex == -1)
            {
                rcboFoliador.BorderWidth = Unit.Pixel(1);
                rcboFoliador.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rcboFoliador.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
            }
            if (this.rcboFoliador_AsientoCont.SelectedIndex == -1)
            {
                rcboFoliador_AsientoCont.BorderWidth = Unit.Pixel(1);
                rcboFoliador_AsientoCont.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rcboFoliador_AsientoCont.BorderColor = System.Drawing.Color.Transparent;//MODIFICADO-------------------------
            }
            
            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
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
    private void LlenaValores()
    {
        if (Request.QueryString["Cve"] != null && Request.QueryString["Cve"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["Cve"]);
        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        folio_Selection = Convert.ToString(Session["folio_Selection"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void obtieneCpto()
    {
        LlenaValores();
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_sidM);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        //FOLIADOR 1
        if (ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString() == "1")
        {
            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, 1, ref rcboFoliador, true, false);

        }
        else if (ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString() == "2")
        {
            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, 2, ref rcboFoliador, true, false);
        }


        //FOLIADOR 2
        if (ds.Tables[0].Rows[0]["cptoDefAsiConFolTip"].ToString() == "1")
        {
            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, 1, ref rcboFoliador_AsientoCont, true, false);
        }
        else if (ds.Tables[0].Rows[0]["cptoDefAsiConFolTip"].ToString() == "2")
        {
            FnCtlsFillIn.RadComboBox_Foliadores(Pag_sConexionLog, Pag_sCompania, 2, ref rcboFoliador_AsientoCont, true, false);
        }
        
    }

    
    private bool ValidaIncluir() {

        if (RadCheckBox_GuiaConta.Checked == true || RadCheckBox_MapeoMovi.Checked == true)
        {
            if (RadCheckBox_Configuracion.Checked == false)
            {
                return false;  
            }else {
                return true; 
            }
            
        } else {

            if (RadCheckBox_Configuracion.Checked == true || RadCheckBox_MapeoDoc.Checked == true || RadCheckBox_AutoridadUsr.Checked == true ||
            RadCheckBox_Costos.Checked == true)
            {
                return true;  
            }
        }
        return true;
    }




    #endregion





























}