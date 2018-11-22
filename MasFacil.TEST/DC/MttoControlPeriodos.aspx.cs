    
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

public partial class DC_MttoControlPeriodos : System.Web.UI.Page
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
        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;

    }

    protected void RadNumPeriodo_TextChanged(object sender, EventArgs e)
    {
        if (RdDateFecha_Inicio.SelectedDate != null)
        {
            ValidaPeriodo(1);
        }
        if (RdDateFecha_Final.SelectedDate != null)
        {
            ValidaPeriodo(2); ;
        }
    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ObtieneFechaInicio();
        if (RadNumAño.Text != "")
        {
            ValidaAñoFecha(1);
        }
        if (RadNumPeriodo.Text != "")
        {
            ValidaPeriodo(1);
        }

        if (RdDateFecha_Final.SelectedDate != null)
        {
            if (ObtieneFechaInicio() == ObtieneFechaFinal())
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1032", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Inicio.Clear();
            }
        }


    }
    
    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ObtieneFechaFinal();

        if (RadNumAño.Text != "")
        {
            ValidaAñoFecha(2);
        }
        if (RadNumPeriodo.Text != "")
        {
            ValidaPeriodo(2);
        }

        if (RdDateFecha_Inicio.SelectedDate != null)
        {
            if (ObtieneFechaInicio() == ObtieneFechaFinal())
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1032", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Final.Clear();
            }
        }
    }
    
    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{
            //    this.rTxtDes.Enabled = true;
            //    this.RadNumPeriodo.Enabled = false;
            //    this.rTxtDes.Enabled = true;
            //    RadNumPeriodo.Enabled = false;
            //    RadNumAño.Enabled = false;
            //    this.CheckReg.Enabled = true;
            //    RdDateFecha_Inicio.Enabled = true;
            //    RdDateFecha_Final.Enabled = true;
            //}
                var dataItem2 = this.rGdvInformacion.SelectedItems[0] as GridDataItem;
                if (dataItem2 != null)
                {



                    string bVal = dataItem2["perSit"].Text;
                     if(bVal=="1")
                    {
                        CheckReg.Checked = true;
                    }
                    else
                    {
                        CheckReg.Checked = false;
                    }


                    RadNumPeriodo.Text = Convert.ToString(dataItem2["perNum"].Text);
                    RadNumAño.Text = Convert.ToString(dataItem2["perAnio"].Text);

                    rTxtDes.Text = Convert.ToString(dataItem2["perDes"].Text);

                    RdDateFecha_Inicio.SelectedDate = Convert.ToDateTime(dataItem2["perFecIni"].Text);
                    RdDateFecha_Final.SelectedDate = Convert.ToDateTime(dataItem2["perFecFin"].Text);
                }
           
        }
    }

    protected void RadNumAño_TextChanged(object sender, EventArgs e)
    {
        if (RadNumAño.Text != "")
        {

            if (RdDateFecha_Inicio.SelectedDate != null)
            {
                ValidaAñoFecha(1);
            }
            if (RdDateFecha_Final.SelectedDate != null)
            {
                ValidaAñoFecha(2); ;
            }

        }

    }



    #endregion


    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }
    
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        LlenaGrid();
        ControlesAccion();
        Limpiartxt();
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


    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        RadNumAño.CssClass = "cssTxtEnabled";
        RadNumPeriodo.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;


        RadNumAño.Enabled = false;
        RadNumPeriodo.Enabled = false;
        rTxtDes.Enabled = false;
        RdDateFecha_Inicio.Enabled = false;
        RdDateFecha_Final.Enabled = false;
        CheckReg.Enabled = false;

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

    private bool compararFechas()
    {

        if (RdDateFecha_Inicio.SelectedDate > RdDateFecha_Final.SelectedDate)
        {
            return false;
        }
        else if (RdDateFecha_Final.SelectedDate < RdDateFecha_Inicio.SelectedDate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    
    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Periodos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);
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
            ProcBD.NombreProcedimiento = "sp_Periodos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int64, 0, ParameterDirection.Input, RadNumAño.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int64, 0, ParameterDirection.Input, RadNumPeriodo.Text.Trim());
  
            ProcBD.AgregarParametrosProcedimiento("@perDes", DbType.String, 50, ParameterDirection.Input, rTxtDes.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@perFecIni", DbType.String, 100, ParameterDirection.Input, ObtieneFechaInicio());
            ProcBD.AgregarParametrosProcedimiento("@perFecFin", DbType.String, 100, ParameterDirection.Input, ObtieneFechaFinal());
            if (CheckReg.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@perSit", DbType.String, 1, ParameterDirection.Input, "1");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@perSit", DbType.String, 1, ParameterDirection.Input, "2");
            }
          
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                if (sEjecEstatus == "1")
                {
                    Limpiartxt();
                    InicioPagina();
                }
            }


        }
        //}
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    //}
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

                    string monCve = dataItem.GetDataKeyValue("perAnio").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Periodos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int64, 0, ParameterDirection.Input, dataItem["perAnio"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int64, 0, ParameterDirection.Input, dataItem["perNum"].Text);

 

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
                else
                {
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


            if (this.RadNumPeriodo.Text == "")
            {
                RadNumPeriodo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                RadNumPeriodo.CssClass = "cssTxtEnabled";//MODIFICADO-------------------------
            }
            if (this.RadNumAño.Text == "")
            {

                RadNumAño.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                RadNumAño.CssClass = "cssTxtEnabled";//MODIFICADO-------------------------
            }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }


            if ( Convert.ToString(RdDateFecha_Inicio.SelectedDate) == "")
            {
                RdDateFecha_Inicio.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
            }

            if ( Convert.ToString(RdDateFecha_Final.SelectedDate) == "")
            {
                RdDateFecha_Final.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;
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

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (RadNumPeriodo.Text == "")
            {
                RadNumPeriodo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                RadNumPeriodo.CssClass = "cssTxtEnabled";//MODIFICADO-------------------------
            }
            if (RadNumAño.Text == "")
            {

                RadNumAño.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                RadNumAño.CssClass = "cssTxtEnabled";//MODIFICADO-------------------------
            }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }
            if (Convert.ToString(RdDateFecha_Inicio.SelectedDate) == "")
            {
                RdDateFecha_Inicio.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
            }

            if (Convert.ToString(RdDateFecha_Final.SelectedDate) == "")
            {
                RdDateFecha_Final.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;
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

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }


    private void ValidaAñoFecha(int TipFec)
    {
        string Val_Fec = "";
        DateTime dt;
        if (TipFec == 1)
        {
            dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);

            Val_Fec = dt.Year.ToString();

            if (RadNumAño.Text != Val_Fec)
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1033", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Inicio.Clear();
            }
        }
        if (TipFec == 2)
        {
            dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);

            Val_Fec = dt.Year.ToString();

            if (RadNumAño.Text != Val_Fec)
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1034", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Final.Clear();
            }
        }
        
        
    }

    private void ValidaPeriodo(int TipFec)
    {
        string Val_Fec = "";
        DateTime dt;
        if (TipFec == 1)
        {
            dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);

            Val_Fec = dt.Month.ToString();

            if (RadNumPeriodo.Text != Val_Fec)
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1035", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Inicio.Clear();
            }
        }
        if (TipFec == 2)
        {
            dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);

            Val_Fec = dt.Month.ToString();

            if (RadNumPeriodo.Text != Val_Fec)
            {
                string sMSGTip = "";
                string sMSG = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1035", ref sMSGTip, ref sMSG);
                ShowAlert(sMSGTip, sMSG);
                RdDateFecha_Final.Clear();
            }
        }


    }

    private void AsignaFecha()
    {
        string Val_Fec = "";
        DateTime dt;

        dt = Convert.ToDateTime(System.DateTime.Now);

        Val_Fec = dt.Year.ToString();
        RadNumAño.Text = Val_Fec;
    }



    public string ObtieneFechaInicio()
    {
        string Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);

        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            return Val_Fec_Inicio;
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
            return Val_Fec_Inicio = "";
        }
    }
    public string ObtieneFechaFinal()
    {
        string Val_Fec_Final = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        Val_Fec_Final = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            return Val_Fec_Final;
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
            return Val_Fec_Final = "";
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

                RadNumAño.Enabled = true;
                RadNumPeriodo.Enabled = true;
                rTxtDes.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
                CheckReg.Enabled = true;

                RadNumAño.Text = "";
                RadNumPeriodo.Text = "";
                rTxtDes.Text = "";
                RdDateFecha_Inicio.Clear();
                RdDateFecha_Final.Clear();
                CheckReg.Checked = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                RadNumAño.Enabled = false;
                RadNumPeriodo.Enabled = false;
                rTxtDes.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
                CheckReg.Enabled = true;

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
                

                RadNumAño.Enabled = false;
                RadNumPeriodo.Enabled = false;
                rTxtDes.Enabled = false;
                RdDateFecha_Inicio.Enabled = false;
                RdDateFecha_Final.Enabled = false;
                CheckReg.Enabled = false;

                RadNumAño.Text = "";
                RadNumPeriodo.Text = "";
                rTxtDes.Text = "";
                RdDateFecha_Inicio.Clear();
                RdDateFecha_Final.Clear();
                CheckReg.Checked = false;
                
            }
        }

        if (Result == false)
        {
            RadNumAño.Enabled = false;
            RadNumPeriodo.Enabled = false;
            rTxtDes.Enabled = false;
            RdDateFecha_Inicio.Enabled = false;
            RdDateFecha_Final.Enabled = false;
            CheckReg.Enabled = false;

            RadNumAño.Text = "";
            RadNumPeriodo.Text = "";
            rTxtDes.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            CheckReg.Checked = false;
            hdfBtnAccion.Value = "";
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
            
            RadNumAño.Text = "";
            RadNumPeriodo.Text = "";
            rTxtDes.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            CheckReg.Checked = false;
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            RadNumAño.CssClass = "cssTxtEnabled";
            RadNumPeriodo.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
            RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
            RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;


            RadNumAño.Enabled = false;
            RadNumPeriodo.Enabled = false;
            rTxtDes.Enabled = false;
            RdDateFecha_Inicio.Enabled = false;
            RdDateFecha_Final.Enabled = false;
            CheckReg.Enabled = false;
            
            RadNumAño.Text = "";
            RadNumPeriodo.Text = "";
            rTxtDes.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            CheckReg.Checked = false;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            hdfBtnAccion.Value = "";
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
        }

        //ELIMIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }


    public void Limpiartxt()
    {
        RadNumAño.Text = "";
        RadNumPeriodo.Text = "";
        rTxtDes.Text = "";
        RdDateFecha_Inicio.Clear();
        RdDateFecha_Final.Clear();
        CheckReg.Checked = false;
    }

    #endregion






}