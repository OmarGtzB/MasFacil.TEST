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
public partial class DC_RegOperacionesDetAbc : System.Web.UI.Page
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
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_cptoId;

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
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        RecuperarValoresDeDataList();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }
    protected void rBtnCopia_Click(object sender, ImageButtonClickEventArgs e)
    {
        AsinarValoresUltimoRegistro();
    }

    #endregion


    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_cptoId = Convert.ToString(Session["cptoId"]);
    }
    public void InicioPagina()
    {
        hdfBtnAccionDet.Value = Session[("hdfBtnAccionDet")].ToString();
        LlenaCboMoneda();
        VisualizaModal();
    }
    private void LlenaCboMoneda() {
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);

        string sMonCve = "";
        decimal dMonTipoCambio = 0;

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttoCptoMapeoModulo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_cptoId);
        ProcBD.AgregarParametrosProcedimiento("@movMapCCSecApli", DbType.Int64, 0, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        Boolean bModuloVisible = false;
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            bModuloVisible = Convert.ToBoolean(ds.Tables[0].Rows[0]["ModuloVisible"]);
            if (ds.Tables[1].Rows.Count > 0) {
                sMonCve = ds.Tables[1].Rows[0]["monCve"].ToString();
            }
            

            if (sMonCve != "")
            {
                //Moneda Configuracion
                rCboMoneda.SelectedValue = sMonCve;
            }
            else
            {
                //Moneda Default          
                FNGrales.bTipoCambioMonedaDefault(Pag_sConexionLog, Pag_sCompania, ref sMonCve, ref dMonTipoCambio);
                rCboMoneda.SelectedValue = sMonCve;
            }

        }
        else
        {
            //Moneda Default
            FNGrales.bTipoCambioMonedaDefault(Pag_sConexionLog, Pag_sCompania, ref sMonCve, ref dMonTipoCambio);
            rCboMoneda.SelectedValue = sMonCve;
        }

        rCboMoneda.Visible = bModuloVisible;
        rlblMoneda.Visible = bModuloVisible;
    }
 
    private void VisualizaModal()
    {
        rBtnCopiar.Enabled = true; 
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_cptoId);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList1.DataSource = ds;
        DataList1.RepeatColumns = 1;
        DataList1.DataBind();

        recorreDatalist(ds);

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnCopiar.Enabled = false;
            AsinarValoresADataList(ds);
        }
    }
    public void recorreDatalist(DataSet dsConfConcepto)
    {

        foreach (DataRow drConfigCpto in dsConfConcepto.Tables[0].Rows)
        {

            string colum_listTipDatoCptoCve = drConfigCpto["listTipDatoCptoCve"].ToString().Substring(0, 3); ;
            string Row = drConfigCpto["Row"].ToString();
            int controlador = 1;
            string cptoConfProgCve = drConfigCpto["cptoConfProgCve"].ToString();


            string   ProgCveVal = "";
 
            ProgCveVal = drConfigCpto["cptoConfProgCve"].ToString();
 
            foreach (DataListItem DtalistFila in DataList1.Items)
            {
                var objRadComboBox = DtalistFila.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = DtalistFila.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = DtalistFila.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = DtalistFila.FindControl("RadDatePicker") as RadDatePicker;

                if (  controlador.ToString() == Row)
                {
                    objRadComboBox.Visible = false;
                    objRadTextBox.Visible = false;
                    objRadNumericTextBox.Visible = false;
                    objRadDatePicker.Visible = false;

                    objRadNumericTextBox.NumberFormat.AllowRounding = false;
                    objRadNumericTextBox.NumberFormat.DecimalDigits = 2;
                    if (colum_listTipDatoCptoCve == "Imp" || colum_listTipDatoCptoCve == "Fac")
                    {
                        objRadNumericTextBox.Visible = true;
                    }
                    if (colum_listTipDatoCptoCve == "Fac")
                    {
                        objRadNumericTextBox.NumberFormat.DecimalDigits = 4;  
                    }



                    if (colum_listTipDatoCptoCve == "Str")
                    {
                        objRadTextBox.Visible = true;
                        objRadTextBox.MaxLength = Convert.ToInt32(drConfigCpto["MaxValue"]);
                    }

                    if (colum_listTipDatoCptoCve == "Fec")
                    {
                        objRadDatePicker.Visible = true;
                    }

                    if (cptoConfProgCve != "" )
                    {
                        objRadComboBox.Visible = true;
                        objRadTextBox.Visible = false;
                        objRadNumericTextBox.Visible = false;

                        DataSet ds1 = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_EjecutaProgramaConsulta";
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, cptoConfProgCve);
                        ds1 = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        objRadComboBox.EmptyMessage = "Seleccionar";
                        objRadComboBox.DataTextField = "Descripcion";
                        objRadComboBox.DataValueField = "Clave";
                        objRadComboBox.DataSource = ds1.Tables[0];
                        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(ds1.Tables[0].Rows.Count);

                        try
                        {
                            objRadComboBox.DataBind();
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                    }

                }

                controlador++;
            }
            
        }
    }
    private void AsinarValoresADataList(DataSet dsConfConcepto)
    {
        string Pag_sTransDetId = Session["dataItemSession"].ToString();
        DataSet dsTransDet = new DataSet();

        //recorrer configuracion del cpto del dataset de configuracion
        foreach (DataRow drConfCpto in dsConfConcepto.Tables[0].Rows)
        {
            // se obtiene el nombre de la columna
            string colum_listTipDatoCptoCve = drConfCpto["listTipDatoCptoCve"].ToString().Substring(0, 3);  


            string snameColumConfcpto = drConfCpto["ColumTrans"].ToString();
            string snameProgCve = drConfCpto["cptoConfProgCve"].ToString();

            //dataset de la variable de sesion a local
            dsTransDet = (DataSet)Session["dsTransDetSession"];

            if (dsTransDet.Tables[0].Rows[0]["monCve"].ToString() != "")
            {
                rCboMoneda.SelectedValue = dsTransDet.Tables[0].Rows[0]["monCve"].ToString();
            }
            else
            {
                rCboMoneda.ClearSelection();
            }
            DataRow[] drTransDet;
            //selecciona el row del dataset de acuerdo al id seleccionado del grid
            drTransDet = dsTransDet.Tables[0].Select("transDetId = " + Pag_sTransDetId);

            //se obtiene el valor de la columna
            string valorTransDet = drTransDet[0][snameColumConfcpto].ToString();

            int Countkeyarray = 0;
            //se recorren los items del datalist
            foreach (DataListItem dlConf in DataList1.Items)
            {

                var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

                //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
                //snameColumConfcpto el nombre que se obtiene desde el dataset
                if (keysDataList_colum == snameColumConfcpto)
                {



                    if (colum_listTipDatoCptoCve == "Imp" || colum_listTipDatoCptoCve == "Fac")
                    {
                        objRadNumericTextBox.Text = valorTransDet;
                    }

                    if (colum_listTipDatoCptoCve == "Str")
                    {
                        objRadTextBox.Text = valorTransDet;
                    }

                    if (colum_listTipDatoCptoCve == "Fec")
                    {
                        if (valorTransDet == "")
                        {
                            objRadDatePicker.SelectedDate = null;
                            objRadDatePicker.Clear();
                        }
                        else {
                            objRadDatePicker.SelectedDate = Convert.ToDateTime(valorTransDet); 

                        }                     
                    }


                    if (snameProgCve != "")
                    {
                        objRadComboBox.SelectedValue = valorTransDet;
                    }

                }
                Countkeyarray += 1;
            }


        }

    }
    private void AsinarValoresUltimoRegistro()
    {
        DataSet dsConfConcepto = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Pag_cptoId);
        dsConfConcepto = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        //string Pag_sTransDetSec = "2";
        DataSet dsTransDet = new DataSet();

        //recorrer configuracion del cpto del dataset de configuracion
        foreach (DataRow drConfCpto in dsConfConcepto.Tables[0].Rows)
        {
            // se obtiene el nombre de la columna
            string colum_listTipDatoCptoCve = drConfCpto["listTipDatoCptoCve"].ToString().Substring(0, 3);


            string snameColumConfcpto = drConfCpto["ColumTrans"].ToString();
            string snameProgCve = drConfCpto["cptoConfProgCve"].ToString();

            //dataset de la variable de sesion a local
            dsTransDet = (DataSet)Session["dsTransDetSession"];

            if (dsTransDet.Tables[0].Rows[0]["monCve"].ToString() != "")
            {
                rCboMoneda.SelectedValue = dsTransDet.Tables[0].Rows[0]["monCve"].ToString();
            }
            else
            {
                rCboMoneda.ClearSelection();
            }
            DataRow[] drTransDet;
            //selecciona el row del dataset de acuerdo al id seleccionado del grid
            //drTransDet = dsTransDet.Tables[0].Select("transDetSec = " + Pag_sTransDetSec);
            drTransDet = dsTransDet.Tables[0].Select("transDetSec=MAX(transDetSec)");

            //se obtiene el valor de la columna
            string valorTransDet = drTransDet[0][snameColumConfcpto].ToString();

            int Countkeyarray = 0;
            //se recorren los items del datalist
            foreach (DataListItem dlConf in DataList1.Items)
            {

                var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

                //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
                //snameColumConfcpto el nombre que se obtiene desde el dataset
                if (keysDataList_colum == snameColumConfcpto)
                {



                    if (colum_listTipDatoCptoCve == "Imp" || colum_listTipDatoCptoCve == "Fac")
                    {
                        objRadNumericTextBox.Text = valorTransDet;
                    }

                    if (colum_listTipDatoCptoCve == "Str")
                    {
                        objRadTextBox.Text = valorTransDet;
                    }

                    if (colum_listTipDatoCptoCve == "Fec")
                    {
                        if (valorTransDet == "")
                        {
                            objRadDatePicker.SelectedDate = null;
                            objRadDatePicker.Clear();
                        }
                        else
                        {
                            objRadDatePicker.SelectedDate = Convert.ToDateTime(valorTransDet);

                        }
                    }


                    if (snameProgCve != "")
                    {
                        objRadComboBox.SelectedValue = valorTransDet;
                    }

                }
                Countkeyarray += 1;
            }


        }

    }
    
    private void RecuperarValoresDeDataList()
    {
        //Cuando la opcion es nuevo registro se deben crear nuevo Row con los valores
        // y agregar el row la tabla del dataset dsTransDet
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            RecuperaValoresDataListNuevo();
        }

        //Cuando la opcion es modificar el registro se deben actualizar los valores 
        //al Row de la tabla del dataset dsTransDet
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            RecuperaValoresDataListModificar();
        }
    }

    private void RecuperaValoresDataListNuevo()
    {
        string sTransDetClaveAux = "";

        int CoutTipoCaptura = 0;
        int CoutProgValida = 0;

        DataSet dsTransDet = new DataSet();
        int Countkeyarray = 0;
        dsTransDet = (DataSet)Session["dsTransDetSession"];
        DataRow dr = dsTransDet.Tables[0].NewRow();

        if (Countkeyarray == 0)
        {
            int maxSec = 1;
            int maxTransDetID = 1;
            if (dsTransDet.Tables[0].Rows.Count > 0)
            {
                maxSec = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetSec)", "")) + 1;
                maxTransDetID = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetId)", "")) + 1;
            }

            dr["transDetId"] = maxTransDetID;

            //comentado
            if (hdfPag_sOpe.Value != "")
            {
                dr["transId"] = hdfPag_sOpe.Value;
            }
            dr["monCve"] = rCboMoneda.SelectedValue;
            dr["monDes"] = rCboMoneda.Text;
            dr["ciaCve"] = Pag_sCompania;
            dr["cptoId"] = Pag_cptoId;
            dr["transDetSec"] = maxSec;
        }
        //se recorren los items del datalist
        foreach (DataListItem dlConf in DataList1.Items)
        {
            var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
            var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
            var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
            var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

            var ObjRadCheckBox_TipoCaptura = dlConf.FindControl("RadCheckBox_TipoCaptura") as RadCheckBox;
            var ObjRadCheckBox_ProgValida = dlConf.FindControl("RadCheckBox_ProgValida") as RadCheckBox;

            //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
            string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
            string sValor = "";

            //Se obtiene el valor del control de edicion (Textbox o Combo)
            if (objRadTextBox.Visible == true)
            {
                sValor = objRadTextBox.Text;
            }

            if (objRadNumericTextBox.Visible == true)
            {
                sValor = objRadNumericTextBox.Text;
            }

            if (objRadDatePicker.Visible == true)
            {
                sValor = "";
                string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                if (Val_Fec != "")
                {
                    DateTime dt = Convert.ToDateTime(Val_Fec);
                    Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                    sValor = Val_Fec;
                }
            }

            if (objRadComboBox.Visible == true)
            {
                sValor = objRadComboBox.SelectedValue;
            }


            //Cuando la opcion es modificar el registro se deben actualizar los valores 
            //al Row de la tabla del dataset dsTransDet

            //comentado
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                if (sValor == "")
                {
                    dr[keysDataList_colum] = System.DBNull.Value;
                }
                else
                {
                    dr[keysDataList_colum] = sValor;
                }
            }

            objRadTextBox.CssClass = "cssTxtEnabled";
            objRadNumericTextBox.CssClass = "cssTxtEnabled";
            objRadComboBox.BorderWidth = Unit.Pixel(1);
            objRadComboBox.BorderColor = System.Drawing.Color.Transparent;
            objRadDatePicker.BorderWidth = Unit.Pixel(1);
            objRadDatePicker.BorderColor = System.Drawing.Color.Transparent;
            if (ObjRadCheckBox_TipoCaptura.Checked == true)
            {
                if (sValor == "")
                {
                    objRadTextBox.CssClass = "cssTxtInvalid";
                    objRadNumericTextBox.CssClass = "cssTxtInvalid";
                    objRadComboBox.BorderWidth = Unit.Pixel(1);
                    objRadComboBox.BorderColor = System.Drawing.Color.Red;
                    objRadDatePicker.BorderWidth = Unit.Pixel(1);
                    objRadDatePicker.BorderColor = System.Drawing.Color.Red;
                    CoutTipoCaptura += 1;
                }
            }

            //if (ObjRadCheckBox_ProgValida.Checked == true)
            //{
            //    string sEjecEstatus, sEjecMSG = "";
            //    DataSet ds = new DataSet();
            //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            //    ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
            //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            //    ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 50, ParameterDirection.Input, sValor);
            //    ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ObjRadCheckBox_ProgValida.Value);
            //    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            //        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            //        if (sEjecEstatus != "1")
            //        {
            //            CoutProgValida += 1;

            //            objRadTextBox.CssClass = "cssTxtInvalid";
            //            objRadNumericTextBox.CssClass = "cssTxtInvalid";
            //            objRadComboBox.BorderWidth = Unit.Pixel(1);
            //            objRadComboBox.BorderColor = System.Drawing.Color.Red;
            //            objRadDatePicker.BorderWidth = Unit.Pixel(1);
            //            objRadDatePicker.BorderColor = System.Drawing.Color.Red;
            //        }
            //    }
            //}


            Countkeyarray += 1;
        }

        Session["dsTransDetSession"] = dsTransDet;



        // =======================================================
        string sEjecMSGErores = "";
        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
            // Ejecuta Programas de Validacion
            FNDatos.ActualizaTransacciones(Pag_sConexionLog, Pag_sCompania, ref dr);

            DataSet dsTransDetModReg;
            dsTransDetModReg = FNDatos.dsTransaccionesAux(Pag_sConexionLog, Pag_sCompania, dr, 2);
            sTransDetClaveAux = dsTransDetModReg.Tables[0].Rows[0]["transDetClaveAux"].ToString();

            Countkeyarray = 0;
            foreach (DataListItem dlConf in DataList1.Items)
            {
                var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

                var ObjRadCheckBox_TipoCaptura = dlConf.FindControl("RadCheckBox_TipoCaptura") as RadCheckBox;
                var ObjRadCheckBox_ProgValida = dlConf.FindControl("RadCheckBox_ProgValida") as RadCheckBox;

                //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
                string sValor = "";

                //Se obtiene el valor del control de edicion (Textbox o Combo)
                if (objRadTextBox.Visible == true)
                {
                    sValor = objRadTextBox.Text;
                }

                if (objRadNumericTextBox.Visible == true)
                {
                    sValor = objRadNumericTextBox.Text;
                }

                if (objRadDatePicker.Visible == true)
                {
                    sValor = "";
                    string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                    if (Val_Fec != "")
                    {
                        DateTime dt = Convert.ToDateTime(Val_Fec);
                        Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                        sValor = Val_Fec;
                    }
                }

                if (objRadComboBox.Visible == true)
                {
                    sValor = objRadComboBox.SelectedValue;
                }


                //Cuando la opcion es modificar el registro se deben actualizar los valores 
                //al Row de la tabla del dataset dsTransDet

                //comentado
        
                objRadTextBox.CssClass = "cssTxtEnabled";
                objRadNumericTextBox.CssClass = "cssTxtEnabled";
                objRadComboBox.BorderWidth = Unit.Pixel(1);
                objRadComboBox.BorderColor = System.Drawing.Color.Transparent;
                objRadDatePicker.BorderWidth = Unit.Pixel(1);
                objRadDatePicker.BorderColor = System.Drawing.Color.Transparent;


                if (ObjRadCheckBox_ProgValida.Checked == true)
                {
                    string sEjecEstatus, sEjecMSG = "";
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 50, ParameterDirection.Input, sValor);
                    ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ObjRadCheckBox_ProgValida.Value);
                    ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, sTransDetClaveAux);
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                        if (sEjecEstatus != "1")
                        {
                            CoutProgValida += 1;
                            sEjecMSGErores += " <br /> * " + sValor.ToString() + "  <br />  " + sEjecMSG + " ";

                            objRadTextBox.CssClass = "cssTxtInvalid";
                            objRadNumericTextBox.CssClass = "cssTxtInvalid";
                            objRadComboBox.BorderWidth = Unit.Pixel(1);
                            objRadComboBox.BorderColor = System.Drawing.Color.Red;
                            objRadDatePicker.BorderWidth = Unit.Pixel(1);
                            objRadDatePicker.BorderColor = System.Drawing.Color.Red;
                        }
                    }
                }


                Countkeyarray += 1;
            }
        }




         

        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
            FNDatos.ActualizaCosto(Pag_sConexionLog, Pag_sCompania, ref dr);
            FNDatos.ActualizaTransacciones(Pag_sConexionLog, Pag_sCompania, ref dr);
            dsTransDet.Tables[0].Rows.Add(dr);
 
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        else
        {

            if (CoutTipoCaptura > 0 && CoutProgValida > 0)
            {
                ShowAlert("2", "Campos obligarotios y valores no validos");
            }
            else {

                if (CoutTipoCaptura > 0)
                {
                    ShowAlert("2", "Campos obligarotios");
                }

                if (CoutProgValida > 0)
                {
                    ShowAlert("2", "Valores no validos " + sEjecMSGErores + "<br />");
                }

            }
        }


    }

    private void RecuperaValoresDataListModificar()
    {
        string sTransDetClaveAux = "";

        int CoutTipoCaptura = 0;
        int CoutProgValida = 0;
        string Pag_sTransDetId = Session["dataItemSession"].ToString();

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetSession"];

        if (rCboMoneda.SelectedValue != "")
        {
            dsTransDet.Tables[0].Rows[0]["monCve"] = rCboMoneda.SelectedValue.ToString();
            dsTransDet.Tables[0].Rows[0]["monDes"] = rCboMoneda.Text.ToString();
        }
        // var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;

        int Countkeyarray = 0;
        //se recorren los items del datalist

        foreach (DataListItem dlConf in DataList1.Items)
        {
            var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
            var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
            var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
            var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

            var ObjRadCheckBox_TipoCaptura = dlConf.FindControl("RadCheckBox_TipoCaptura") as RadCheckBox;
            var ObjRadCheckBox_ProgValida = dlConf.FindControl("RadCheckBox_ProgValida") as RadCheckBox;

            //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
            string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
            string sValor = "";

            //Se obtiene el valor del control de edicion (Textbox o Combo)
            if (objRadTextBox.Visible == true)
            {
                sValor = objRadTextBox.Text;
            }

            if (objRadNumericTextBox.Visible == true)
            {
                sValor = objRadNumericTextBox.Text;
            }

            if (objRadDatePicker.Visible == true)
            {
                sValor = "";
                string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                if (Val_Fec != "")
                {
                    DateTime dt = Convert.ToDateTime(Val_Fec);
                    Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                    sValor = Val_Fec;
                }
            }

            if (objRadComboBox.Visible == true)
            {
                sValor = objRadComboBox.SelectedValue;
            }


            //Cuando la opcion es modificar el registro se deben actualizar los valores 
            //al Row de la tabla del dataset dsTransDet
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                DataRow[] drTransDet;
                drTransDet = dsTransDet.Tables[0].Select("transDetId = " + Pag_sTransDetId);


                if (sValor == "")
                {
                    drTransDet[0][keysDataList_colum] = System.DBNull.Value;
                }
                else
                {
                    drTransDet[0][keysDataList_colum] = sValor;
                }

                objRadTextBox.CssClass = "cssTxtEnabled";
                objRadNumericTextBox.CssClass = "cssTxtEnabled";
                objRadComboBox.BorderWidth = Unit.Pixel(1);
                objRadComboBox.BorderColor = System.Drawing.Color.Transparent;
                objRadDatePicker.BorderWidth = Unit.Pixel(1);
                objRadDatePicker.BorderColor = System.Drawing.Color.Transparent;
                if (ObjRadCheckBox_TipoCaptura.Checked == true)
                {
                    if (sValor == "")
                    {
                        objRadTextBox.CssClass = "cssTxtInvalid";
                        objRadNumericTextBox.CssClass = "cssTxtInvalid";
                        objRadComboBox.BorderWidth = Unit.Pixel(1);
                        objRadComboBox.BorderColor = System.Drawing.Color.Red;
                        objRadDatePicker.BorderWidth = Unit.Pixel(1);
                        objRadDatePicker.BorderColor = System.Drawing.Color.Red;
                        CoutTipoCaptura += 1;
                    }
                }

                //if (ObjRadCheckBox_ProgValida.Checked == true)
                //{
                //    string sEjecEstatus, sEjecMSG = "";
                //    DataSet ds = new DataSet();
                //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                //    ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
                //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
                //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                //    ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 50, ParameterDirection.Input, sValor);
                //    ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ObjRadCheckBox_ProgValida.Value);
                //    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                //        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                //        if (sEjecEstatus != "1")
                //        {
                //            CoutProgValida += 1;

                //            objRadTextBox.CssClass = "cssTxtInvalid";
                //            objRadNumericTextBox.CssClass = "cssTxtInvalid";
                //            objRadComboBox.BorderWidth = Unit.Pixel(1);
                //            objRadComboBox.BorderColor = System.Drawing.Color.Red;
                //            objRadDatePicker.BorderWidth = Unit.Pixel(1);
                //            objRadDatePicker.BorderColor = System.Drawing.Color.Red;
                //        }
                //    }
                //}

            }

            Countkeyarray += 1;
        }


        // =======================================================
        string sEjecMSGErores = "";
        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
            // Ejecuta Programas de Validacion
            DataRow[] drTransDetModReg;
            drTransDetModReg = dsTransDet.Tables[0].Select("transDetId = " + Pag_sTransDetId);
            DataSet dsTransDetModReg;
            dsTransDetModReg = FNDatos.dsTransaccionesAux(Pag_sConexionLog, Pag_sCompania, drTransDetModReg,2);
            sTransDetClaveAux = dsTransDetModReg.Tables[0].Rows[0]["transDetClaveAux"].ToString();   

            Countkeyarray = 0;
            foreach (DataListItem dlConf in DataList1.Items)
            {
                var objRadComboBox = dlConf.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = dlConf.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = dlConf.FindControl("RadDatePicker") as RadDatePicker;

                var ObjRadCheckBox_TipoCaptura = dlConf.FindControl("RadCheckBox_TipoCaptura") as RadCheckBox;
                var ObjRadCheckBox_ProgValida = dlConf.FindControl("RadCheckBox_ProgValida") as RadCheckBox;

                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
                string sValor = "";

                //Se obtiene el valor del control de edicion (Textbox o Combo)
                if (objRadTextBox.Visible == true)
                {
                    sValor = objRadTextBox.Text;
                }

                if (objRadNumericTextBox.Visible == true)
                {
                    sValor = objRadNumericTextBox.Text;
                }

                if (objRadDatePicker.Visible == true)
                {
                    sValor = "";
                    string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                    if (Val_Fec != "")
                    {
                        DateTime dt = Convert.ToDateTime(Val_Fec);
                        Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                        sValor = Val_Fec;
                    }
                }

                if (objRadComboBox.Visible == true)
                {
                    sValor = objRadComboBox.SelectedValue;
                }

                if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    objRadTextBox.CssClass = "cssTxtEnabled";
                    objRadNumericTextBox.CssClass = "cssTxtEnabled";
                    objRadComboBox.BorderWidth = Unit.Pixel(1);
                    objRadComboBox.BorderColor = System.Drawing.Color.Transparent;
                    objRadDatePicker.BorderWidth = Unit.Pixel(1);
                    objRadDatePicker.BorderColor = System.Drawing.Color.Transparent;

                    if (ObjRadCheckBox_ProgValida.Checked == true)
                    {
                        string sEjecEstatus, sEjecMSG = "";
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 50, ParameterDirection.Input, sValor);
                        ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ObjRadCheckBox_ProgValida.Value);
                        ProcBD.AgregarParametrosProcedimiento("@transDetClaveAux", DbType.String, 50, ParameterDirection.Input, sTransDetClaveAux);
                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            if (sEjecEstatus != "1")
                            {
                                CoutProgValida += 1;
                                sEjecMSGErores += " <br /> * " + sValor.ToString() + "  <br />  " + sEjecMSG + " " ;

                                objRadTextBox.CssClass = "cssTxtInvalid";
                                objRadNumericTextBox.CssClass = "cssTxtInvalid";
                                objRadComboBox.BorderWidth = Unit.Pixel(1);
                                objRadComboBox.BorderColor = System.Drawing.Color.Red;
                                objRadDatePicker.BorderWidth = Unit.Pixel(1);
                                objRadDatePicker.BorderColor = System.Drawing.Color.Red;
                            }
                        }
                    }

                }

                Countkeyarray += 1;
            }
        }





        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
            DataRow[] drTransDet;
            drTransDet = dsTransDet.Tables[0].Select("transDetId = " + Pag_sTransDetId);
            FNDatos.ActualizaCosto(Pag_sConexionLog, Pag_sCompania, ref drTransDet);
            FNDatos.ActualizaTransacciones(Pag_sConexionLog, Pag_sCompania, ref drTransDet);


            Session["dsTransDetSession"] = dsTransDet;
            string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        else
        {

            if (CoutTipoCaptura > 0 && CoutProgValida > 0)
            {
                ShowAlert("2", "Campos obligarotios y valores no validos");
            }
            else
            {

                if (CoutTipoCaptura > 0)
                {
                    ShowAlert("2", "Campos obligarotios");
                }

                if (CoutProgValida > 0)
                {
                    ShowAlert("2", "Valores no validos " + sEjecMSGErores + "<br />");
                }

            }
        }

    }


    protected void CadenaAlineaValor_TextChanged(object sender, EventArgs e)
    {
       
        int Countkeyarray = 0;
        foreach (DataListItem dlConf in DataList1.Items)
        {


            var objRadTextBox = dlConf.FindControl("RadTextBox") as RadTextBox;
            var RadCheckBox_Justificacion = dlConf.FindControl("RadCheckBox_Justificacion") as RadCheckBox;


            if (objRadTextBox.Text != "" && RadCheckBox_Justificacion.Text != "")
            {
                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();

                if (RadCheckBox_Justificacion.Checked == true)
                {
                    objRadTextBox.Text = objRadTextBox.Text.PadRight(objRadTextBox.MaxLength, Convert.ToChar(RadCheckBox_Justificacion.Text));
                }
                else {
                    objRadTextBox.Text = objRadTextBox.Text.PadLeft(objRadTextBox.MaxLength, Convert.ToChar(RadCheckBox_Justificacion.Text));
                }

                

                objRadTextBox.Text = objRadTextBox.Text.Substring(0, objRadTextBox.MaxLength);

                objRadTextBox.DataBind();

            }






            Countkeyarray += 1;
        }
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES

    #endregion
    
}