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

public partial class DC_MttoAlmacenArticulos : System.Web.UI.Page
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
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();

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
            // addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }

    }
    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "close", "Close();", true);
    }

    protected void rBtnGuardarRw_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (ValidaDatosDef() == 0)
        {
            CargarDatosDefault();
        }else {
            string script = "function f(){$find(\"" + rdWindowArtAlm.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
    }

    protected void rBtnCancelarRw_Click(object sender, ImageButtonClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "close", "Close();", true);
    }



    protected void RdioBtnRelacionados_CheckedChanged(object sender, EventArgs e)
    {
        // llenaArt(54);
        // CargarCheck();
        // radTxtBoxBusqueda.Text = "";

        DataSet dsAlmArt = new DataSet();
        DataSet dsAlmArtRel = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        dsAlmArtRel = dsAlmArt.Clone();
        //dsAlmArtRel.Tables[0].Clear();

        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {
        //&& Convert.ToString(drConfCpto["artAlmCanMaxInv"]) != ""
        //    && Convert.ToString(drConfCpto["artAlmCanMinInv"]) != "" && Convert.ToString(drConfCpto["artAlmPunReo"]) != ""
            if (Convert.ToString(drConfCpto["valCheck"]) == "1" )
            {
                dsAlmArtRel.Tables[0].ImportRow(drConfCpto);
            }
        }
        DataListArt.DataSource = dsAlmArtRel;
        DataListArt.DataBind();
        
        int rowVal = 0;
        string ValMax, ValMin, ValReOrden;

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            ValMax = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmCanMaxInv"].ToString();
            ValMin = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmCanMinInv"].ToString();
            ValReOrden = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmPunReo"].ToString();

            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            objRadNumericMaximo.Text = ValMax;
            objRadNumericMinimo.Text = ValMin;
            objRadNumericReOrden.Text = ValReOrden;

            rowVal++;
        }
        
        //CargarCheck();
        CargarCheckAll();

        CheckBoxTodos.Checked = true;
        radTxtBoxBusqueda.Text="";
    }
    
    protected void RdioBtnTodos_CheckedChanged(object sender, EventArgs e)
    {
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        DataListArt.DataSource = dsAlmArt;
        DataListArt.DataBind();

        if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
        {
            CargarDatosDefault();
        }
        radTxtBoxBusqueda.Text = "";

        CheckBoxTodos.Checked = false;

        PintaRdNumeric(dsAlmArt);
        
        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {

            if (Convert.ToString(drConfCpto["valCheck"]) == "1")
            {
                foreach (DataListItem dlConf in DataListArt.Items)
                {
                    var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
                    if (objRadCheckBox.Value == Convert.ToString(drConfCpto["artCve"]))
                    {
                        objRadCheckBox.Checked = true;
                    }
                    else
                    {
                        if (objRadCheckBox.Checked == true)
                        {

                        }
                        else
                        {
                            objRadCheckBox.Checked = false;
                        }

                    }
                }
            }

        }


        int Countkeyarray = 0, CountCheck =0;
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            Session["IDartCve"] = DataListArt.DataKeys[Countkeyarray].ToString();
            Countkeyarray += 1;
        }

        foreach (DataListItem dlConf in DataListArt.Items) {
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            if (objRadCheckBox.Checked==true)
            {
                CountCheck += 1;
            }
        }

        if (Countkeyarray == CountCheck)
        {
            CheckBoxTodos.Checked = true;
        }else {
            CheckBoxTodos.Checked = false;
        }

    }

    protected void RdioBtnNoRel_CheckedChanged(object sender, EventArgs e)
    {
        // llenaArt(54);
        // CargarCheck();
        // radTxtBoxBusqueda.Text = "";

        DataSet dsAlmArt = new DataSet();
        DataSet dsAlmArtRel = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        dsAlmArtRel = dsAlmArt.Clone();
        //dsAlmArtRel.Tables[0].Clear();

        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {
        //&& Convert.ToString(drConfCpto["artAlmCanMaxInv"]) == "" && Convert.ToString(drConfCpto["artAlmCanMinInv"]) == "" && Convert.ToString(drConfCpto["artAlmPunReo"]) == ""
            if (Convert.ToString(drConfCpto["valCheck"]) == "" )
            {
                dsAlmArtRel.Tables[0].ImportRow(drConfCpto);
            }
        }
        DataListArt.DataSource = dsAlmArtRel;
        DataListArt.DataBind();







        int rowVal = 0;
        string ValMax, ValMin, ValReOrden;

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            ValMax = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmCanMaxInv"].ToString();
            ValMin = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmCanMinInv"].ToString();
            ValReOrden = dsAlmArtRel.Tables[0].Rows[rowVal]["artAlmPunReo"].ToString();

            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            objRadNumericMaximo.Text = ValMax;
            objRadNumericMinimo.Text = ValMin;
            objRadNumericReOrden.Text = ValReOrden;

            rowVal++;
        }

        //CargarCheck();

        CheckBoxTodos.Checked = false;
        radTxtBoxBusqueda.Text = "";
    }
    
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];


        //TODOS LOS ARTICULOS
        if (RdioBtnTodos.Checked == true)
        {   
            llenaBusqueda(55);
            // CargarCheck();
            if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
            {
                CargarDatosDefault();
            }
        }

        //ARTICULOS RELACIONADOS
        if (RdioBtnRelacionados.Checked == true)
        {   //Solo Articulos Relacionados
            llenaBusqueda(56);
            //CargarCheck();
            if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
            {
                CargarDatosDefault();
            }
        }


        //ARTICULOS NO RELACIONADOS
        if (RdioBtnNoRel.Checked == true)
        {   //Solo Articulos Relacionados
            llenaBusqueda(57);
            //CargarCheck();
            if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
            {
                CargarDatosDefault();
            }
        }


        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {
            if (Convert.ToString(drConfCpto["valCheck"]) == "1")
            {
                foreach (DataListItem dlConf in DataListArt.Items)
                {
                    var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

                    if (objRadCheckBox.Value == Convert.ToString(drConfCpto["artCve"]))
                    {
                        objRadCheckBox.Checked = true;
                    }
                    else
                    {
                        if (objRadCheckBox.Checked == true)
                        {

                        }
                        else
                        {
                            objRadCheckBox.Checked = false;
                        }
                    }
                }
            }
        }


   
    }

    protected void CheckBoxTodos_Click(object sender, EventArgs e)
    {

        //if (CheckBoxTodos.Checked == true)
        //{
        //    foreach (DataListItem dlConf in DataListArt.Items)
        //    {
        //        var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
        //        objRadCheckBox.Checked = true;
        //    }
        //}

        //if (CheckBoxTodos.Checked == false)
        //{
        //    foreach (DataListItem dlConf in DataListArt.Items)
        //    {
        //        var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
        //        objRadCheckBox.Checked = false;
        //    }
        //}

        //DataSet dsAlmArt = new DataSet();
        //dsAlmArt = (DataSet)Session["dsAlmArtSession"];
        //foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        //{
        //    if (CheckBoxTodos.Checked == true)
        //    {
        //        drConfCpto["valCheck"] = "1";
        //    }
        //    else if (CheckBoxTodos.Checked == false)
        //    {
        //        drConfCpto["valCheck"] = "";
        //    }
        //}

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        #region CHECK
        if (CheckBoxTodos.Checked == true)
        {
            foreach (DataListItem dlConf in DataListArt.Items)
            {
                var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
                objRadCheckBox.Checked = true;

                foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
                {
                    if (CheckBoxTodos.Checked == true)
                    {
                        if (drConfCpto["artCve"].ToString() == objRadCheckBox.Value.ToString())
                        {
                            drConfCpto["valCheck"] = "1";
                        }

                    }
                    else if (CheckBoxTodos.Checked == false)
                    {
                        if (drConfCpto["artCve"].ToString() == objRadCheckBox.Value.ToString())
                        {
                            drConfCpto["valCheck"] = "";
                        }
                    }
                }

            }
        }

        if (CheckBoxTodos.Checked == false)
        {
            foreach (DataListItem dlConf in DataListArt.Items)
            {
                var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
                objRadCheckBox.Checked = false;

                foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
                {
                    if (CheckBoxTodos.Checked == true)
                    {
                        if (drConfCpto["artCve"].ToString() == objRadCheckBox.Value.ToString())
                        {
                            drConfCpto["valCheck"] = "1";
                        }

                    }
                    else if (CheckBoxTodos.Checked == false)
                    {
                        if (drConfCpto["artCve"].ToString() == objRadCheckBox.Value.ToString())
                        {
                            drConfCpto["valCheck"] = "";
                        }
                    }
                }

            }
        }
    }
   #endregion

    protected void CheckBoxArt_CheckedChanged(object sender, EventArgs e)
    {
        RadCheckBox rcheck = (RadCheckBox)sender;

        string Pag_sartCve;

        Pag_sartCve = rcheck.Value;
        Session["IDartCve"] = Pag_sartCve.ToString();

        string rcheckStr = rcheck.Value.ToString();

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        DataRow[] drAlmArt;
        drAlmArt = dsAlmArt.Tables[0].Select("artCve = '" + rcheckStr + "' ");
        if (rcheck.Checked == true)
        {
            drAlmArt[0]["valCheck"] = "1";
        }
        else
        {
            drAlmArt[0]["valCheck"] = "";
        }
        

        int Countkeyarray = 0, CountCheck = 0;
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            Session["IDartCve"] = DataListArt.DataKeys[Countkeyarray].ToString();
            Countkeyarray += 1;
        }

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            if (objRadCheckBox.Checked == true)
            {
                CountCheck += 1;
            }
        }

        if (Countkeyarray == CountCheck)
        {
            CheckBoxTodos.Checked = true;
        }else {
            CheckBoxTodos.Checked = false;
        }

    }

    protected void rNumMaximo_TextChanged(object sender, EventArgs e)
    {

        RadNumericTextBox RadNumericMax = (RadNumericTextBox)sender;
        string Pag_sartCve;
        Pag_sartCve = RadNumericMax.ToolTip.ToString();
        
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        DataRow[] drAlmArt;
        drAlmArt = dsAlmArt.Tables[0].Select("artCve = '" + Pag_sartCve + "' ");
        drAlmArt[0]["artAlmCanMaxInv"] = Convert.ToDecimal(RadNumericMax.Text);
    }

    protected void rNumMinimo_TextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox RadNumericMin = (RadNumericTextBox)sender;
        string Pag_sartCve;
        Pag_sartCve = RadNumericMin.ToolTip.ToString();

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];
        DataRow[] drAlmArt;
        drAlmArt = dsAlmArt.Tables[0].Select("artCve = '" + Pag_sartCve + "' ");
        drAlmArt[0]["artAlmCanMinInv"] = Convert.ToDecimal(RadNumericMin.Text);

    }

    protected void rNumReorden_TextChanged(object sender, EventArgs e)
    {
        RadNumericTextBox RadNumericReorden = (RadNumericTextBox)sender;
        string Pag_sartCve;
        Pag_sartCve = RadNumericReorden.ToolTip.ToString();
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];
        DataRow[] drAlmArt;
        drAlmArt = dsAlmArt.Tables[0].Select("artCve = '" + Pag_sartCve + "' ");
        drAlmArt[0]["artAlmPunReo"] = Convert.ToDecimal(RadNumericReorden.Text);
    }


    #endregion

    #region FUNCION

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_folio_Selection = Convert.ToString(Session["folio_Selection"]);
    }

    private void InicioPagina()
    {
        string script = "function f(){$find(\"" + rdWindowArtAlm.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //DatosAlmacen();
        llenaArt(53);
        CargarCheck();
        RdioBtnTodos.Checked = true;
        RdioBtnRelacionados.Checked = false;
        RdioBtnNoRel.Checked = false;
    }

    private void EjecutaAccion()
    {

        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            EjecutaSpAcciones();
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        LimpiarCss();
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;


            if (objRadCheckBox.Checked == true)
            {
                if (objRadNumericMaximo.Text == "")
                {
                    objRadNumericMaximo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    objRadNumericMaximo.CssClass = "cssTxtEnabled";
                }

                if (objRadNumericMinimo.Text == "")
                {
                    objRadNumericMinimo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    objRadNumericMinimo.CssClass = "cssTxtEnabled";
                }

                if (objRadNumericReOrden.Text == "")
                {
                    objRadNumericReOrden.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    objRadNumericReOrden.CssClass = "cssTxtEnabled";
                }

            }

        }



        if (camposInc > 0)
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }

        return sResult;


    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaSpAcciones()
    {
        string sEjecEstatus = "", sEjecMSG = "", cadena = "", cEstatus = "", cMSJ = "";
        string[] separadas;
        cadena = EliminarAlm();
        separadas = cadena.Split(',');
        cEstatus = separadas[0];
        cMSJ = separadas[1];

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];
        
        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {
            //var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            //var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            //var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;
            //var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            //var objRadLabel = dlConf.FindControl("lblArtCve") as RadLabel;
            
            if (Convert.ToString(drConfCpto["valCheck"]) == "1")
            {
                try
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["artCve"]));
                    ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@artAlmCanMaxInv", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["artAlmCanMaxInv"]));
                    ProcBD.AgregarParametrosProcedimiento("@artAlmCanMinInv", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["artAlmCanMinInv"]));
                    ProcBD.AgregarParametrosProcedimiento("@artAlmPunReo", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["artAlmPunReo"]));

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                        //ShowAlert(sEjecEstatus, sEjecMSG);
                    }
                }
                catch (Exception ex)
                {
                    string MsgError = ex.Message.Trim();
                }
            }

        }

        if (sEjecEstatus != "" && sEjecMSG != "")
        {
            ShowAlert(sEjecEstatus, sEjecMSG);
        }
        else
        {
            ShowAlert(cEstatus, cMSJ);
        }
        if (sEjecEstatus == "1")
        {
            LimpiaDtalist();
            llenaArt(53);
            CargarCheck();
            RdioBtnTodos.Checked = true;
            RdioBtnRelacionados.Checked = false;
            RdioBtnNoRel.Checked=false;

            radTxtBoxBusqueda.Text = "";
            if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
            {
                CargarDatosDefault();
            }
        }


        ///
        int Countkeyarray = 0, CountCheck = 0;
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            Session["IDartCve"] = DataListArt.DataKeys[Countkeyarray].ToString();
            Countkeyarray += 1;
        }

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            if (objRadCheckBox.Checked == true)
            {
                CountCheck += 1;
            }
        }

        if (Countkeyarray == CountCheck)
        {
            CheckBoxTodos.Checked = true;
        }else {

            CheckBoxTodos.Checked = false;
        }

    }

    #endregion

    #region METODO
    private void LimpiarCss()
    {
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            objRadNumericMaximo.CssClass = "cssTxtEnabled";
            objRadNumericMinimo.CssClass = "cssTxtEnabled";
            objRadNumericReOrden.CssClass = "cssTxtEnabled";

        }
    }

    private void llenaArt(int opc)
    {
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];
        
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            Session["dsAlmArtSession"] = ds;
            dsAlmArt = (DataSet)Session["dsAlmArtSession"];
            DataListArt.DataSource = dsAlmArt;
            DataListArt.DataBind();
        }
        PintaRdNumeric(ds);
        
    }
    
    private void CargarCheck()
    {
    
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericTextBox = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

            if (objRadNumericTextBox.Text != "")
            {
                objRadCheckBox.Checked = true;
            }
            else
            {
                objRadCheckBox.Checked = false;
            }
        }


    }

    private void CargarCheckAll()
    {
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
            objRadCheckBox.Checked = true;
        }
    }

    private void CargarDatosDefault()
    {
        DataSet dsAlmArt;
        Int16 ValMax, ValMin, ValReOrden;
        ValMax = Convert.ToInt16(RadNumericMaximoDef.Text);
        ValMin = Convert.ToInt16(RadNumericMinimoDef.Text);
        ValReOrden = Convert.ToInt16(RadNumericReOrdenDef.Text);



        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

            if (objRadCheckBox.Checked == false)
            {
                objRadNumericMaximo.Text = ValMax.ToString();
                objRadNumericMinimo.Text = ValMin.ToString();
                objRadNumericReOrden.Text = ValReOrden.ToString();
            }
        }

        CargaValorDs(ValMax, ValMin, ValReOrden);

    
    }

    private int ValidaDatosDef()
    {

        Int32 camposInc = 0;
        if (RadNumericMaximoDef.Text.Trim() == "")
        {
            RadNumericMaximoDef.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else { RadNumericMaximoDef.CssClass = "cssTxtEnabled"; }

        if (RadNumericMinimoDef.Text.Trim() == "")
        {
            RadNumericMinimoDef.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else { RadNumericMinimoDef.CssClass = "cssTxtEnabled"; }


        if (RadNumericReOrdenDef.Text.Trim() == "")
        {
            RadNumericReOrdenDef.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else { RadNumericReOrdenDef.CssClass = "cssTxtEnabled"; }

        return camposInc;

    }

    private string EliminarAlm()
    {
            DataSet ds = new DataSet();
            string sEjecEstatus = "", sEjecMSG = "";

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            return sEjecEstatus + "," + sEjecMSG;
            }
            else
            {
                return sEjecEstatus + "," + sEjecMSG;
            }
    }

    private void LimpiaDtalist()
    {

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

            objRadNumericMaximo.Text = "";
            objRadNumericMinimo.Text = "";
            objRadNumericReOrden.Text = "";

        }

    }


    private void llenaBusqueda(int opc)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);

        if (radTxtBoxBusqueda.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@BusquedaCve", DbType.String, 20, ParameterDirection.Input, radTxtBoxBusqueda.Text.Trim());
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@BusquedaCve", DbType.String, 20, ParameterDirection.Input, radTxtBoxBusqueda.Text);
        }

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            DataListArt.DataSource = ds;
            DataListArt.DataBind();
    
        }
        int rowVal = 0;
        string ValMax, ValMin, ValReOrden;

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            foreach (DataListItem dlConf in DataListArt.Items)
            {
                ValMax = ds.Tables[0].Rows[rowVal]["artAlmCanMaxInv"].ToString();
                ValMin = ds.Tables[0].Rows[rowVal]["artAlmCanMinInv"].ToString();
                ValReOrden = ds.Tables[0].Rows[rowVal]["artAlmPunReo"].ToString();

                var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
                var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
                var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

                objRadNumericMaximo.Text = ValMax;
                objRadNumericMinimo.Text = ValMin;
                objRadNumericReOrden.Text = ValReOrden;

                rowVal++;
            }
        }
        else
        {
            DataListArt.DataSource = ds;
            DataListArt.DataBind();
        }
        CargarCheck();

        if (RadNumericMaximoDef.Text != "" && RadNumericMinimoDef.Text != "" && RadNumericReOrdenDef.Text != "")
        {
            CargarDatosDefault();
        }
       

    
    }

    private void CargaValorDs(decimal max, decimal min, decimal reorden)
    {

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsAlmArtSession"];

        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {

            if (drConfCpto["valCheck"].ToString() == "")
            {
                drConfCpto["artAlmCanMaxInv"] = max;
                drConfCpto["artAlmCanMinInv"] = min;
                drConfCpto["artAlmPunReo"] = reorden;
            }
        }
    }

    private void PintaRdNumeric(DataSet ds)
    {

        int rowVal = 0;
        string ValMax, ValMin, ValReOrden;

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            ValMax = ds.Tables[0].Rows[rowVal]["artAlmCanMaxInv"].ToString();
            ValMin = ds.Tables[0].Rows[rowVal]["artAlmCanMinInv"].ToString();
            ValReOrden = ds.Tables[0].Rows[rowVal]["artAlmPunReo"].ToString();

            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            objRadNumericMaximo.Text = ValMax;
            objRadNumericMinimo.Text = ValMin;
            objRadNumericReOrden.Text = ValReOrden;

            rowVal++;
        }

    }
    
     private void CargaDatosIni() {
     
        DataSet dsAlmArt;
        Int16 ValMax, ValMin, ValReOrden;
        ValMax = Convert.ToInt16(RadNumericMaximoDef.Text);
        ValMin = Convert.ToInt16(RadNumericMinimoDef.Text);
        ValReOrden = Convert.ToInt16(RadNumericReOrdenDef.Text);
        
        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
            var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
            var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;

            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

            if (objRadCheckBox.Checked == false)
            {
                objRadNumericMaximo.Text = ValMax.ToString();
                objRadNumericMinimo.Text = ValMin.ToString();
                objRadNumericReOrden.Text = ValReOrden.ToString();
            }
        }

        CargaValorDs(ValMax, ValMin, ValReOrden);
    }



    #endregion






    //private void EjecutaSpAcciones()
    //{
    //    string sEjecEstatus = "", sEjecMSG = "";
    //    EliminarAlm();
    //    foreach (DataListItem dlConf in DataListArt.Items)
    //    {

    //        var objRadNumericMaximo = dlConf.FindControl("rNumMaximo") as RadNumericTextBox;
    //        var objRadNumericMinimo = dlConf.FindControl("rNumMinimo") as RadNumericTextBox;
    //        var objRadNumericReOrden = dlConf.FindControl("rNumReOrden") as RadNumericTextBox;
    //        var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;
    //        var objRadLabel = dlConf.FindControl("lblArtCve") as RadLabel;

    //        if (objRadCheckBox.Checked == true)
    //        {
    //            try
    //            {
    //                DataSet ds = new DataSet();

    //                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //                ProcBD.NombreProcedimiento = "sp_ArticuloAlmacenes";
    //                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);

    //                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //                ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, objRadLabel.Text);
    //                ProcBD.AgregarParametrosProcedimiento("@almCve", DbType.String, 3, ParameterDirection.Input, PagLoc_folio_Selection);
    //                ProcBD.AgregarParametrosProcedimiento("@artAlmCanMaxInv", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(objRadNumericMaximo.Text));
    //                ProcBD.AgregarParametrosProcedimiento("@artAlmCanMinInv", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(objRadNumericMinimo.Text));
    //                ProcBD.AgregarParametrosProcedimiento("@artAlmPunReo", DbType.Decimal, 0, ParameterDirection.Input, Convert.ToDecimal(objRadNumericReOrden.Text));

    //                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //                if (FnValAdoNet.bDSIsFill(ds))
    //                {

    //                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
    //                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

    //                    //ShowAlert(sEjecEstatus, sEjecMSG);
    //                }


    //            }
    //            catch (Exception ex)
    //            {
    //                string MsgError = ex.Message.Trim();
    //            }
    //        }

    //    }
    //    ShowAlert(sEjecEstatus, sEjecMSG);
    //    if (sEjecEstatus == "1")
    //    {
    //        LimpiaDtalist();
    //        llenaArt(53);
    //        CargarCheck();
    //        RdioBtnTodos.Checked = true;
    //        RdioBtnRelacionados.Checked = false;
    //        radTxtBoxBusqueda.Text = "";
    //        if (RadNumericMaximoDef.Text != "")
    //        {
    //            CargarDatosDefault();
    //        }
    //    }

    //}






}



