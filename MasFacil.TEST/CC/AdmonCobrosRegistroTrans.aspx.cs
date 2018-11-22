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

public partial class CC_AdmonCobrosRegistroTrans : System.Web.UI.Page
{
    #region VARIABLES

    ws.Servicio oWS = new ws.Servicio();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_cptoId;

    //private static DataTable dtFormToGrid = new DataTable();

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
        
        if (divForm.Visible)
        {
            RecuperarValoresDeDataList();
        }
        else
        {
            addRowFromGrid();
        }
        
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (divForm.Visible)
        {
            InicioPagina();
        }
        else
        {

            if (rGdvPagosPend.Items.Count > 0)
            {
                for (int i = 0; i < rGdvPagosPend.Items.Count; i++)
                {
                    rGdvPagosPend.Items[i].Cells[6].Text = rGdvPagosPend.Items[i].Cells[5].Text;
                }
            }


            rGdvPagosPend.MasterTableView.ClearSelectedItems();

        }
    }

    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (divForm.Visible)
        {
            saveFormValues();
            loadGridPP(51);
        }

        divForm.Visible = false;

        lblApli.Visible = true;
        rTxtImpApli.Visible = true;
        rImgBtnAceptarP.Visible = true;

        divGrid.Visible = true;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnBuscar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnRastreoSelected.png";

    }

    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {

        divForm.Visible = true;

        lblApli.Visible = false;
        rTxtImpApli.Visible = false;
        rImgBtnAceptarP.Visible = false;
        divGrid.Visible = false;


        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnBuscar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnRastreo.png";
    }

    protected void rImgBtnAceptarP_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (rGdvPagosPend.Items.Count > 0)
        {
            for (int i = 0; i < rGdvPagosPend.Items.Count; i++)
            {

                if (rGdvPagosPend.Items[i].Selected == true)
                {
                    rGdvPagosPend.Items[i].Cells[6].Text = rTxtImpApli.DisplayText.Remove(0, 1);
                }
            }
        }

    }

    protected void rGdvPagosPend_SelectedIndexChanged(object sender, EventArgs e)
    {

        string toPass = "";

        toPass = rGdvPagosPend.SelectedItems[0].Cells[5].Text;

        rTxtImpApli.Text = toPass;
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
                else
                {
                    objRadTextBox.Text = objRadTextBox.Text.PadLeft(objRadTextBox.MaxLength, Convert.ToChar(RadCheckBox_Justificacion.Text));
                }

                objRadTextBox.Text = objRadTextBox.Text.Substring(0, objRadTextBox.MaxLength);

                objRadTextBox.DataBind();

            }

            Countkeyarray += 1;
        }
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
        hdfBtnAccionDet.Value = Session[("hdfBtnAccionDetCC")].ToString();
        //hdfBtnAccionDet.Value = "1";
        LlenaCboMoneda();
        VisualizaModal();
        loadGridPP(50);


        divForm.Visible = true;
        lblApli.Visible = false;
        rTxtImpApli.Visible = false;
        rImgBtnAceptarP.Visible = false;
        divGrid.Visible = false;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
        rBtnBuscar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnRastreo.png";

        if (hdfBtnAccionDet.Value == "2")
        {
            rBtnBuscar.Enabled = false;
        }


        setStyleDataList(getColumn("cptoConfId_Ref10_CodApli"));
        setStyleDataList(getColumn("cptoConfId_Ref10_Aplic"));
        //setStyleDataList(getColumn("cptoConfId_Ref10_Princ"));
        setStyleDataList(getColumn("cptoConfId_Imp_Imp"));
        setStyleDataList(getColumn("cptoConfId_Fec_Mov"));
        setStyleDataList(getColumn("cptoConfId_Fec_Venc"));


        //setValueDataList(getColumn("cptoConfId_Ref10_CodApli"), Convert.ToString(Session["refPrincCC"]));
        setValueDataList(getColumn("cptoConfId_Fec_Mov"), Convert.ToString(Session["fchMovCC"]));

        //Crear Estructura de la tabla para pasar Valores

        Session["dtFormToGrid"] = null;
        DataTable dtFormToGrid = new DataTable();
        
        if (dtFormToGrid.Columns.Count == 0)
        {
            dtFormToGrid.Columns.Add("column");
            dtFormToGrid.Columns.Add("value");
        }

        Session["dtFormToGrid"] = dtFormToGrid;

    }

    private void setStyleDataList(string columnGet)
    {
        string response = "";
        string sValor = "";
        int Countkeyarray = 0;

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

            if (columnGet == keysDataList_colum)
            {
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


                objRadTextBox.BorderWidth = Unit.Pixel(1);
                objRadTextBox.BorderColor = System.Drawing.Color.Blue;
                objRadNumericTextBox.BorderWidth = Unit.Pixel(1);
                objRadNumericTextBox.BorderColor = System.Drawing.Color.Blue;
                objRadComboBox.BorderWidth = Unit.Pixel(1);
                objRadComboBox.BorderColor = System.Drawing.Color.Blue;
                objRadDatePicker.BorderWidth = Unit.Pixel(1);
                objRadDatePicker.BorderColor = System.Drawing.Color.Blue;


            }

            Countkeyarray++;

        }

        response = sValor;

        //return response;
    }
    
    private void setValueDataList(string columnGet, string valueSet)
    {

        int Countkeyarray = 0;

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

            if (columnGet == keysDataList_colum)
            {
                //Se obtiene el valor del control de edicion (Textbox o Combo)
                if (objRadTextBox.Visible == true)
                {
                    objRadTextBox.Text = valueSet;
                }

                if (objRadNumericTextBox.Visible == true)
                {
                    objRadNumericTextBox.Text = valueSet;
                }

                if (objRadDatePicker.Visible == true)
                {

                    objRadDatePicker.SelectedDate = Convert.ToDateTime(valueSet);
                    /*
                    sValor = "";
                    string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                    if (Val_Fec != "")
                    {
                        DateTime dt = Convert.ToDateTime(Val_Fec);
                        Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                        sValor = Val_Fec;
                    }
                    */
                }

                if (objRadComboBox.Visible == true)
                {
                    objRadComboBox.SelectedValue = valueSet;

                }

            }

            Countkeyarray++;

        }


    }
    
    private void LlenaCboMoneda()
    {
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
            sMonCve = ds.Tables[1].Rows[0]["monCve"].ToString();

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


            string ProgCveVal = "";

            ProgCveVal = drConfigCpto["cptoConfProgCve"].ToString();

            foreach (DataListItem DtalistFila in DataList1.Items)
            {
                var objRadComboBox = DtalistFila.FindControl("RadComboBox") as RadComboBox;
                var objRadTextBox = DtalistFila.FindControl("RadTextBox") as RadTextBox;
                var objRadNumericTextBox = DtalistFila.FindControl("RadNumericTextBox") as RadNumericTextBox;
                var objRadDatePicker = DtalistFila.FindControl("RadDatePicker") as RadDatePicker;

                if (controlador.ToString() == Row)
                {
                    objRadComboBox.Visible = false;
                    objRadTextBox.Visible = false;
                    objRadNumericTextBox.Visible = false;
                    objRadDatePicker.Visible = false;


                    if (colum_listTipDatoCptoCve == "Imp" || colum_listTipDatoCptoCve == "Fac")
                    {
                        objRadNumericTextBox.Visible = true;
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

                    if (cptoConfProgCve != "")
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
        string Pag_sTransDetId = Session["dataItemSessionCC"].ToString();
        DataSet dsTransDet = new DataSet();

        //recorrer configuracion del cpto del dataset de configuracion
        foreach (DataRow drConfCpto in dsConfConcepto.Tables[0].Rows)
        {
            // se obtiene el nombre de la columna
            string colum_listTipDatoCptoCve = drConfCpto["listTipDatoCptoCve"].ToString().Substring(0, 3);


            string snameColumConfcpto = drConfCpto["ColumTrans"].ToString();
            string snameProgCve = drConfCpto["cptoConfProgCve"].ToString();

            //dataset de la variable de sesion a local
            dsTransDet = (DataSet)Session["dsTransDetCCSession"];

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
        int CoutTipoCaptura = 0;
        int CoutProgValida = 0;

        DataSet dsTransDet = new DataSet();
        int Countkeyarray = 0;
        dsTransDet = (DataSet)Session["dsTransDetCCSession"];
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
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    if (sEjecEstatus != "1")
                    {
                        CoutProgValida += 1;

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
        Session["dsTransDetCCSession"] = dsTransDet;



        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
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
            else
            {

                if (CoutTipoCaptura > 0)
                {
                    ShowAlert("2", "Campos obligarotios");
                }

                if (CoutProgValida > 0)
                {
                    ShowAlert("2", "Valores no validos");
                }

            }
        }


    }
    
    private void RecuperaValoresDataListModificar()
    {
        int CoutTipoCaptura = 0;
        int CoutProgValida = 0;
        string Pag_sTransDetId = Session["dataItemSessionCC"].ToString();

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetCCSession"];

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
                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                        if (sEjecEstatus != "1")
                        {
                            CoutProgValida += 1;

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


        if ((CoutTipoCaptura + CoutProgValida) == 0)
        {
            Session["dsTransDetCCSession"] = dsTransDet;
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
                    ShowAlert("2", "Valores no validos");
                }

            }
        }

    }
    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void saveFormValues()
    {
        string response = "";
        string sValor = "";
        int Countkeyarray = 0;

        DataTable dtFormToGrid = new DataTable();
        dtFormToGrid = (DataTable)Session["dtFormToGrid"];

        dtFormToGrid.Rows.Clear();

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

            if (true)
            {

                //Se obtiene el valor del control de edicion (Textbox o Combo)
                if (objRadTextBox.Visible == true && objRadComboBox.BorderColor != System.Drawing.Color.Blue)
                {
                    DataRow row = dtFormToGrid.NewRow();
                    row["column"] = keysDataList_colum;
                    row["value"] = objRadTextBox.Text;
                    dtFormToGrid.Rows.Add(row);
                    //sValor = objRadTextBox.Text;
                }

                if (objRadNumericTextBox.Visible == true && objRadComboBox.BorderColor != System.Drawing.Color.Blue)
                {
                    DataRow row = dtFormToGrid.NewRow();
                    row["column"] = keysDataList_colum;
                    row["value"] = objRadNumericTextBox.Text;
                    dtFormToGrid.Rows.Add(row);
                    //sValor = objRadNumericTextBox.Text;
                }

                if (objRadDatePicker.Visible == true && objRadDatePicker.BorderColor != System.Drawing.Color.Blue)
                {
                    sValor = "";
                    string Val_Fec = objRadDatePicker.SelectedDate.ToString();
                    if (Val_Fec != "")
                    {
                        DateTime dt = Convert.ToDateTime(Val_Fec);
                        Val_Fec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

                        DataRow row = dtFormToGrid.NewRow();
                        row["column"] = keysDataList_colum;
                        row["value"] = Val_Fec;
                        dtFormToGrid.Rows.Add(row);
                        //sValor = Val_Fec;
                    }
                }

                if (objRadComboBox.Visible == true && objRadComboBox.BorderColor != System.Drawing.Color.Blue)
                {
                    DataRow row = dtFormToGrid.NewRow();
                    row["column"] = keysDataList_colum;
                    row["value"] = objRadComboBox.SelectedValue;
                    dtFormToGrid.Rows.Add(row);
                    //sValor = objRadComboBox.SelectedValue;

                }

            }

            Countkeyarray++;

        }

        Session["dtFormToGrid"] = dtFormToGrid;

    }

    private void loadGridPP(int opc)
    {

        string prmCliCve = getValueDataList(getColumn("cptoConfId_Ref10_CodApli"));
        string prmRefPrinc = getValueDataList(getColumn("cptoConfId_Ref10_Aplic"));
        //string prmRefPrinc = "";
        string prmImpAbo = getValueDataList(getColumn("cptoConfId_Imp_Imp"));
        string prmFecMov = getValueDataList(getColumn("cptoConfId_Fec_Mov"));
        string prmFecVenc = getValueDataList(getColumn("cptoConfId_Fec_Venc"));

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PartidasPendientesCobro";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, Pag_cptoId);

            if (prmCliCve != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 10, ParameterDirection.Input, prmCliCve);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 10, ParameterDirection.Input, "");
            }


            if (prmRefPrinc != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Princ", DbType.String, 10, ParameterDirection.Input, prmRefPrinc);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Princ", DbType.String, 10, ParameterDirection.Input, "");
            }

            if (prmImpAbo != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@partPenCobImpAbo", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(prmImpAbo));
            }


            if (prmFecMov != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@partPenCobFecMov", DbType.String, 100, ParameterDirection.Input, prmFecMov.Replace('/', '-') + " 00:00:00");
            }

            if (prmFecVenc != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@partPenCobFecVenc", DbType.String, 100, ParameterDirection.Input, prmFecVenc.Replace('/', '-') + " 00:00:00");
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                rGdvPagosPend.DataSource = ds;
                rGdvPagosPend.DataBind();
            }

        }
        catch (Exception ex)
        {
            ShowAlert("1", ex.ToString());
            throw;
        }
    }

    private void addRowFromGrid()
    {

        if (rGdvPagosPend.Items.Count > 0)
        {
            if (rGdvPagosPend.SelectedItems.Count == 0)
            {
                ShowAlert("2", "No hay registros seleccionados");
            }
            else
            {

                for (int i = 0; i < rGdvPagosPend.Items.Count; i++)
                {

                    if (rGdvPagosPend.Items[i].Selected == true)
                    {

                        DataSet dsTransDet = new DataSet();

                        dsTransDet = (DataSet)Session["dsTransDetCCSession"];
                        DataRow dr = dsTransDet.Tables[0].NewRow();

                        //Agregar Valores Form

                        DataTable dtFormToGrid = new DataTable();

                        dtFormToGrid = (DataTable)Session["dtFormToGrid"];

                        foreach (DataRow rowFG in dtFormToGrid.Rows)
                        {
                            if (rowFG["value"].ToString() != "")
                            {
                                dr[rowFG["column"].ToString()] = rowFG["value"].ToString();
                            }

                        }


                        if (getColumn("cptoConfId_Ref10_CodApli").Trim() != "")
                        {
                            dr[getColumn("cptoConfId_Ref10_CodApli")] = rGdvPagosPend.Items[i].Cells[7].Text;
                        }

                        /*
                       if (getColumn("cptoConfId_Ref10_Princ").Trim() != "")
                       {
                           dr[getColumn("cptoConfId_Ref10_Princ")] = rGdvPagosPend.Items[i].Cells[2].Text;
                       }
                       */

                        if (getColumn("cptoConfId_Ref10_Aplic").Trim() != "")
                        {
                            dr[getColumn("cptoConfId_Ref10_Aplic")] = rGdvPagosPend.Items[i].Cells[2].Text;
                        }

                        if (getColumn("cptoConfId_Imp_Imp").Trim() != "")
                        {
                            dr[getColumn("cptoConfId_Imp_Imp")] = rGdvPagosPend.Items[i].Cells[6].Text;
                        }


                        if (getColumn("cptoConfId_Fec_Mov").Trim() != "")
                        {
                            dr[getColumn("cptoConfId_Fec_Mov")] = Convert.ToDateTime(Session["fchMovCC"]);
                        }

                        if (getColumn("cptoConfId_Fec_Venc").Trim() != "")
                        {
                            dr[getColumn("cptoConfId_Fec_Venc")] = rGdvPagosPend.Items[i].Cells[4].Text;
                        }




                        dr["monCve"] = rCboMoneda.SelectedValue;
                        dr["monDes"] = rCboMoneda.Text;
                        dr["ciaCve"] = Pag_sCompania;
                        dr["cptoId"] = Pag_cptoId;



                        int maxSec = 1;
                        int maxTransDetID = 1;
                        if (dsTransDet.Tables[0].Rows.Count > 0)
                        {
                            maxSec = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetSec)", "")) + 1;
                            maxTransDetID = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetId)", "")) + 1;
                        }

                        dr["transDetId"] = maxTransDetID;
                        dr["transDetSec"] = maxSec;


                        dsTransDet.Tables[0].Rows.Add(dr);

                        Session["dsTransDetCCSession"] = dsTransDet;



                    }
                }

                string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);


            }


        }
        else
        {
            ShowAlert("2", "No hay registros seleccionados");
        }

    }
    
    #endregion

    #region FUNCIONES

    private string getValueDataList(string columnGet)
    {
        string response = "";
        string sValor = "";
        int Countkeyarray = 0;

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

            if (columnGet == keysDataList_colum)
            {
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

            }

            Countkeyarray++;

        }

        response = sValor;

        return response;
    }

    private string getColumn(string cptoConfRef)
    {

        string response = "";

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MovimientosCC";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, Pag_cptoId);
            ProcBD.AgregarParametrosProcedimiento("@refColumnMov", DbType.String, 100, ParameterDirection.Input, cptoConfRef);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response = ds.Tables[0].Rows[0]["columnName"].ToString();
                    }
                    else
                    {
                        response = "";
                    }

                }
                else
                {
                    response = "";
                }
            }
            else
            {
                response = "";
            }


        }
        catch (Exception ex)
        {
            response = "";

        }

        return response;

    }

    #endregion
    
}
 