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

public partial class DC_MttoAgrArticulosMasivo : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    #endregion

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

    protected void rCboAgrupaciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaCboDatosAgrupacion();
        rCboAgrupacionesDatos.Enabled = true;
        // CargaDataList01();
    }

    private void LlenaComboAgrupaciones()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupaciones, ds, "agrCve", "agrDes", false, false);
        ((Literal)rCboAgrupaciones.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgrupaciones.Items.Count);
        rCboAgrupaciones.DataBind();
    }

    private void LlenaCboDatosAgrupacion()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 10, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupacionesDatos, ds, "agrDatoCve", "agrDatoDes", true, false);
        ((Literal)rCboAgrupacionesDatos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgrupacionesDatos.Items.Count);

    }



    private DataSet CargaDataList(int opc)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 10, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, this.rCboAgrupacionesDatos.SelectedValue.Trim().ToString());
        if (radTxtBoxBusqueda.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@BusquedaCve", DbType.String, 20, ParameterDirection.Input, radTxtBoxBusqueda.Text.Trim());
        }else {
            ProcBD.AgregarParametrosProcedimiento("@BusquedaCve", DbType.String, 20, ParameterDirection.Input, "");
        }
        
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            DataListArt.DataSource = ds;
            DataListArt.DataBind();
            CheckVal();
            CheckAll();
            return ds;
        }else {
            return ds;
        }
        

      
    }


    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void InicioPagina()
    {
        LlenaComboAgrupaciones();
        RdioBtnTodos.Checked = true;
        RdioBtnRelacionados.Checked = false;
        RdioBtnNoRel.Checked = false;
    }


    protected void RdioBtnRelacionados_CheckedChanged(object sender, EventArgs e)
    {
        if (rCboAgrupaciones.SelectedValue != "" && rCboAgrupacionesDatos.SelectedValue != "")
        {
            DataSet dsRelAgrAr;
            DataSet dsAlmArtClone;
            dsRelAgrAr = (DataSet)Session["dsRelAgrArSession"];

            dsAlmArtClone = dsRelAgrAr.Clone();

            foreach (DataRow drConfCpto in dsRelAgrAr.Tables[0].Rows)
            {
                if (Convert.ToString(drConfCpto["valCheck"]) == "1")
                {
                    dsAlmArtClone.Tables[0].ImportRow(drConfCpto);
                }
            }
            DataListArt.DataSource = dsAlmArtClone;
            DataListArt.DataBind();

            CheckVal();
            CheckBoxTodos.Checked = true;
            radTxtBoxBusqueda.Text = "";
        }
    }

    protected void RdioBtnNoRel_CheckedChanged(object sender, EventArgs e)
    {
        if (rCboAgrupaciones.SelectedValue != "" && rCboAgrupacionesDatos.SelectedValue != "") {
        
            DataSet dsRelAgrAr;
            DataSet dsAlmArtClone;
            dsRelAgrAr = (DataSet)Session["dsRelAgrArSession"];

            dsAlmArtClone = dsRelAgrAr.Clone();

            foreach (DataRow drConfCpto in dsRelAgrAr.Tables[0].Rows)
            {
                if (Convert.ToString(drConfCpto["valCheck"]) == "0")
                {
                    dsAlmArtClone.Tables[0].ImportRow(drConfCpto);
                }
            }
            DataListArt.DataSource = dsAlmArtClone;
            DataListArt.DataBind();

            CheckVal();
            CheckBoxTodos.Checked = false;
            radTxtBoxBusqueda.Text = "";
        }
    }

    protected void RdioBtnTodos_CheckedChanged(object sender, EventArgs e)
    {
        if (rCboAgrupaciones.SelectedValue != "" && rCboAgrupacionesDatos.SelectedValue != "") {
        
            DataSet dsAlmArt = new DataSet();
            dsAlmArt = (DataSet)Session["dsRelAgrArSession"];
            DataListArt.DataSource = dsAlmArt;
            DataListArt.DataBind();
            CheckBoxTodos.Checked = false;
            CheckVal();

            radTxtBoxBusqueda.Text = "";


            CheckAll();
        }
    }

    protected void CheckBoxArt_CheckedChanged(object sender, EventArgs e)
    {
        RadCheckBox rcheck = (RadCheckBox)sender;

        string Pag_sartCve;

        Pag_sartCve = rcheck.Value;
        Session["IDartCve"] = Pag_sartCve.ToString();

        string rcheckStr = rcheck.Value.ToString();

        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsRelAgrArSession"];

        DataRow[] drAlmArt;
        drAlmArt = dsAlmArt.Tables[0].Select("artCve = '" + rcheckStr + "' ");
        if (rcheck.Checked == true)
        {
            drAlmArt[0]["valCheck"] = "1";
        }
        else
        {
            drAlmArt[0]["valCheck"] = "0";
        }



        CheckAll();
    }

    protected void rCboAgrupacionesDatos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet dsRelAgrAr = new DataSet();
        dsRelAgrAr = CargaDataList(53);
        Session["dsRelAgrArSession"] = dsRelAgrAr;

        RdioBtnTodos.Checked = true;
        RdioBtnNoRel.Checked = false;
        RdioBtnRelacionados.Checked = false;

        RdioBtnTodos.Enabled = true;
        RdioBtnNoRel.Enabled = true;
        RdioBtnRelacionados.Enabled = true;
        radTxtBoxBusqueda.Enabled = true;

        CheckBoxTodos.Enabled = true;
    }

    private void CheckVal() {

        foreach (DataListItem dlConf in DataListArt.Items)
        {
            var objRadLabel = dlConf.FindControl("lblValCheck") as RadLabel;
            var objRadCheckBox = dlConf.FindControl("CheckBoxArt") as RadCheckBox;

            if (objRadLabel.Text == "1")
            {
                objRadCheckBox.Checked = true;
            }
            else
            {
                objRadCheckBox.Checked = false;
            }
        }
    }




    protected void CheckBoxTodos_Click(object sender, EventArgs e)
    {
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsRelAgrArSession"];

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
                            drConfCpto["valCheck"] = "0";
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
                            drConfCpto["valCheck"] = "0";
                        }
                    }
                }

            }
        }
        #endregion
        




        

        //foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        //{
        //    if (CheckBoxTodos.Checked == true)
        //    {
        //        if (drConfCpto["valCheck"].ToString() == "1")
        //        {

        //        }
        //        drConfCpto["valCheck"] = "1";
        //    }
        //    else if (CheckBoxTodos.Checked == false)
        //    {
        //        drConfCpto["valCheck"] = "";
        //    }
        //}



    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        LlenaCboDatosAgrupacion();
        rCboAgrupaciones.ClearSelection();
        rCboAgrupacionesDatos.Enabled = false;
        RdioBtnRelacionados.Enabled = false;
        RdioBtnNoRel.Enabled = false;
        RdioBtnTodos.Enabled = false;

        radTxtBoxBusqueda.Enabled = false;
        
        DataListArt.DataSource = null;
        DataListArt.DataBind();

        CheckBoxTodos.Enabled = false;
    }

    private void EjecutaAccion(){

        EjecutaSpAcciones(); 
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
        dsAlmArt = (DataSet)Session["dsRelAgrArSession"];

        foreach (DataRow drConfCpto in dsAlmArt.Tables[0].Rows)
        {
         

            if (Convert.ToString(drConfCpto["valCheck"]) == "1")
            {
                try
                {
                    DataSet ds = new DataSet();

                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 10, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
                    ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, this.rCboAgrupacionesDatos.SelectedValue.Trim().ToString());
                    ProcBD.AgregarParametrosProcedimiento("@cveFolio", DbType.String, 30, ParameterDirection.Input, Convert.ToString(drConfCpto["artCve"]));
                    ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, 0, ParameterDirection.Input, 1);
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
        }else {
            ShowAlert(cEstatus, cMSJ);
            
        }
       
        if (sEjecEstatus == "1" || cEstatus == "1")
        {
            radTxtBoxBusqueda.Text = "";
            CargaDataList(53);
            RdioBtnTodos.Checked = true;
            RdioBtnRelacionados.Checked = false;
            RdioBtnNoRel.Checked = false;

            
           
        }

        CheckAll();

    }

    private string EliminarAlm()
    {

        //try
        //{
            DataSet ds = new DataSet();
            string sEjecEstatus="", sEjecMSG = "";

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 10, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, this.rCboAgrupacionesDatos.SelectedValue.Trim().ToString());
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, 0, ParameterDirection.Input, 1);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                return sEjecEstatus + "," + sEjecMSG;

            }else {
            return sEjecEstatus + "," + sEjecMSG;
        }

                
            
            
        //}
        //catch (Exception ex)
        //{
        //    string MsgError = ex.Message.Trim();
        //}

    }


    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {
       
        if (rCboAgrupaciones.SelectedValue != "" && rCboAgrupacionesDatos.SelectedValue != "")
        {
        
        DataSet dsAlmArt = new DataSet();
        dsAlmArt = (DataSet)Session["dsRelAgrArSession"];

        //TODOS LOS ARTICULOS
        if (RdioBtnTodos.Checked == true)
            {
                CargaDataList(53);
                if (radTxtBoxBusqueda.Text == "")
                {
                    DataListArt.DataSource = dsAlmArt;
                    DataListArt.DataBind();
                }
            }

            //ARTICULOS RELACIONADOS
            if (RdioBtnRelacionados.Checked == true) 
            {   //Solo Articulos Relacionados

                CargaDataList(55);
                if (radTxtBoxBusqueda.Text == "") {
                    DataSet dsRelAgrAr;
                    DataSet dsAlmArtClone;
                    dsRelAgrAr = (DataSet)Session["dsRelAgrArSession"];
                    dsAlmArtClone = dsRelAgrAr.Clone();
                    foreach (DataRow drConfCpto in dsRelAgrAr.Tables[0].Rows)
                    {
                        if (Convert.ToString(drConfCpto["valCheck"]) == "1")
                        {
                            dsAlmArtClone.Tables[0].ImportRow(drConfCpto);
                        }
                    }
                    DataListArt.DataSource = dsAlmArtClone;
                    DataListArt.DataBind();
                }
            }

            //ARTICULOS NO RELACIONADOS
            #region ARTICULOS NO RELACIONADOS
            if (RdioBtnNoRel.Checked == true)
            {   //Solo Articulos Relacionados
                CargaDataList(54);

                if (radTxtBoxBusqueda.Text == "")
                {
                    DataSet dsRelAgrAr;
                    DataSet dsAlmArtClone;
                    dsRelAgrAr = (DataSet)Session["dsRelAgrArSession"];
                    dsAlmArtClone = dsRelAgrAr.Clone();
                foreach (DataRow drConfCpto in dsRelAgrAr.Tables[0].Rows)
                {


                    if (Convert.ToString(drConfCpto["valCheck"]) == "0")
                    {
                        dsAlmArtClone.Tables[0].ImportRow(drConfCpto);
                    }
                }
                    DataListArt.DataSource = dsAlmArtClone;
                    DataListArt.DataBind();
                }
            }
            #endregion

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
    }

    private void CheckAll() {

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
        }
        else
        {
            CheckBoxTodos.Checked = false;
        }
    }
}