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

public partial class FR_ConsultaAcumulados : System.Web.UI.Page
{

    #region VARIABLES
    ws.Servicio oWS = new ws.Servicio();
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
   
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
     

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
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        //protected override void OnInit(EventArgs e)
        //{
        //base.OnInit(e);
        //if (!IsPostBack)
        //{
 

        DataSet ds = new DataSet();
        ds = (DataSet)Session["dsConsultaAcum_Config"];

        if (Convert.ToString(Session["Config"]) == "0")
        {
            if (FnValAdoNet.bDSIsFill(ds))
            {
                Session["Config"] = "1";
                rPivoGrid.Fields.Clear();

                rPivoGrid.FilterHeaderZoneText = "Coloque campos de filtro aquí";
                rPivoGrid.ColumnHeaderZoneText = "Coloque campos de columna aquí";
                rPivoGrid.RowHeaderZoneText  = "Coloque los campos de fila aquí";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    bool bPermiteOperaciones = Convert.ToBoolean(dr["permiteOperaciones"]);
                    if (bPermiteOperaciones)
                    {
                        PivotGridAggregateField FieldData = new PivotGridAggregateField();
                        rPivoGrid.Fields.Add(FieldData);
                        FieldData.DataField = "C" + dr["numeroColumna"].ToString().Trim();
                        FieldData.Caption = dr["tituloColumna"].ToString().Trim();
                        FieldData.UniqueName = "C" + dr["numeroColumna"].ToString().Trim();
                        FieldData.DataFormatString = "{0:N}";
       
                    }
                    else
                    {
                        PivotGridRowField RowField = new PivotGridRowField();
                        rPivoGrid.Fields.Add(RowField);
                        RowField.DataField = "C" + dr["numeroColumna"].ToString().Trim();
                        RowField.Caption = dr["tituloColumna"].ToString().Trim();
                    }
                }
            }
        }
      //}
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnExpXLS_Click(object sender, ImageButtonClickEventArgs e)
    {
        //rPivoGrid.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), "Xlsx");
        //rPivoGrid.ExportSettings.IgnorePaging = true;
        rPivoGrid.ExportToExcel();
    }
    protected void rBtnConsultaWizard_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaWizardConsultaAcumulados();
    }
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        LlenaConsulta();
        DataSet ds = new DataSet();
        ds = (DataSet)Session["dsConsultaAcum"];

        if (FnValAdoNet.bDSRowsIsFill(ds) == false) {
            ShowAlert("2", "No se encontro información con los parametros seleccionados.");
        }

       
    }
    protected void RadPivotGrid1_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
    {
        LlenaConsulta();
    }
    protected void rPivoGrid_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
    {
        //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#5E79AF");
        PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
        if (modelDataCell != null)
        {
            AddStylesToDataCells(modelDataCell, e);
        }

        if (modelDataCell.TableCellType == PivotGridTableCellType.RowHeaderCell)
        {
            AddStylesToRowHeaderCells(modelDataCell, e);
        }

        if (modelDataCell.TableCellType == PivotGridTableCellType.ColumnHeaderCell)
        {
            AddStylesToColumnHeaderCells(modelDataCell, e);
        }

        if (modelDataCell.IsGrandTotalCell)
        {
            //GranTotal Titulos
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(126, 126, 126); 
            e.ExportedCell.Style.Font.Bold = true;
        }

        if (IsTotalDataCell(modelDataCell))
        {
            //Totales Rows
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(206, 206, 206); 
            e.ExportedCell.Style.Font.Bold = true;
            AddBorders(e);
        }

        if (IsGrandTotalDataCell(modelDataCell))
        {
            //GrandTotal Valores
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(126, 126, 126); 
            e.ExportedCell.Style.Font.Bold = true;
            AddBorders(e);
        }
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion


    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    public void InicioPagina()
    {
        Session["dsConsultaAcum"] = null;
        Session["dsConsultaAcum_Config"] = null;
        Session["Config"] = "0";
        TituloPagina();
        EjecutaWizardConsultaAcumulados();
    }
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Consulta Acumulados", "PnlMPFormTituloApartado");
    }
    private void EjecutaWizardConsultaAcumulados() {
        string script = "function f(){$find(\"" + rWinReports.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }


    #endregion



    #region FUNCIONES
    private void LlenaConsulta() {
         
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        ds = (DataSet)Session["dsConsultaAcum"];
        if (FnValAdoNet.bDSIsFill(ds)) {
            dt = ds.Tables[0];
            rPivoGrid.DataSource = dt;
        }
    }
    #endregion


    #region ExportPivot

    ///  Agrega estilos a datosde las  celdas 
    private void AddStylesToDataCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
    {
       
        if (modelDataCell.Data != null && modelDataCell.Data.GetType() == typeof(decimal))
        {
            //decimal value = Convert.ToDecimal(modelDataCell.Data);
            //if (value > 10)
            //{
            //    e.ExportedCell.Style.BackColor = mycol;

            //    AddBorders(e);
            //}

            e.ExportedCell.Format = "###,###.00";
            e.ExportedCell.Style.HorizontalAlign = HorizontalAlign.Right;
        }
    }

    ///  Agrega estilos a celdas de encabezado de fila
    private void AddStylesToRowHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
    {
        //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#5E79AF");

        

        if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
        {
            e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 120D;
        }

        if (modelDataCell.IsTotalCell)
        {
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(206, 206, 206); 
            e.ExportedCell.Style.Font.Bold = true;
        }
        else
        {
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(249, 239, 189);
        }

        AddBorders(e);
    }

    /// Agrega estilos a las celdas de encabezado de columna
    private void AddStylesToColumnHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
    {
        //System.Drawing.Color mycol = System.Drawing.ColorTranslator.FromHtml("#5E79AF");
        if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
        {
            e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 110D;
        }

        if (modelDataCell.IsTotalCell)
        {
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(255, 236, 79); 
            e.ExportedCell.Style.Font.Bold = true;
        }
        else
        {
            e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(249, 239, 189); 
        }
        AddBorders(e);
    }


    private bool IsTotalDataCell(PivotGridBaseModelCell modelDataCell)
    {
        return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
           (modelDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell ||
             modelDataCell.CellType == PivotGridDataCellType.RowTotalDataCell ||
             modelDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal);
    }

    private static void AddBorders(PivotGridCellExportingArgs e)
    {
        e.ExportedCell.Style.BorderBottomColor = System.Drawing.Color.FromArgb(128, 128, 128);
        e.ExportedCell.Style.BorderBottomWidth = new Unit(1);
        //e.ExportedCell.Style.BorderBottomStyle =  BorderStyle.Solid;

        e.ExportedCell.Style.BorderRightColor = System.Drawing.Color.FromArgb(128, 128, 128);
        e.ExportedCell.Style.BorderRightWidth = new Unit(1);
        //e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;

        e.ExportedCell.Style.BorderLeftColor = System.Drawing.Color.FromArgb(128, 128, 128);
        e.ExportedCell.Style.BorderLeftWidth = new Unit(1);
        //e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid;

        e.ExportedCell.Style.BorderTopColor = System.Drawing.Color.FromArgb(128, 128, 128);
        e.ExportedCell.Style.BorderTopWidth = new Unit(1);
        //e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid;
    }

    private bool IsGrandTotalDataCell(PivotGridBaseModelCell modelDataCell)
    {
        return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
            (modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell ||
                modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal ||
                modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal ||
                modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell ||
                modelDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal);

    }

    #endregion

}