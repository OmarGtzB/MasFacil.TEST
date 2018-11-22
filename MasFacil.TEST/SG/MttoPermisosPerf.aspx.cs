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

public partial class SG_MttoPermisosPerf : System.Web.UI.Page
{
    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();


    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    //MGMTimbrado.CFDI FNTimbrado = new MGMTimbrado.CFDI();
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
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }
    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
        if (rCboModulo.SelectedValue == "")
        {
            SaveTree.Value = "1";
        }else {
            SaveTree.Value = "0";
        }
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //EjecutaAccionLimpiar();
        InicioPagina();
        LlenaDtlist(0);
        RadCheckTodos.Checked = false;

    }

    protected void rCboModulo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (hdfBtnAccion.Value != "")
        {
            if (SaveTree.Value == "1")
            {
                ValModulFirst.Value = rCboModulo.SelectedValue;
                SaveTree.Value = "0";
                if (rCboModulo.SelectedValue != "")
                {
                    CargaTree(rCboModulo.SelectedValue.ToString());
                }

                if (rGdvPerfiles.SelectedItems.Count == 1)
                {
                    var dataItem = rGdvPerfiles.SelectedItems[0] as GridDataItem;
                    string val = dataItem["maPerfCve"].Text;
                    CargaTreeds(val);
                }
                ValModul.Value = rCboModulo.SelectedValue;

            }
            else
            {
                string sMSGTip = "", sResult = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1056", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);

                rCboModulo.SelectedValue = ValModulFirst.Value;
            }
        }else{
            ValModulFirst.Value = rCboModulo.SelectedValue;
            if (rCboModulo.SelectedValue != "")
            {
                CargaTree(rCboModulo.SelectedValue.ToString());
            }

            if (rGdvPerfiles.SelectedItems.Count == 1)
            {
                var dataItem = rGdvPerfiles.SelectedItems[0] as GridDataItem;
                string val = dataItem["maPerfCve"].Text;
                CargaTreeds(val);
                
            }
        }
  
    }

    protected void rGdvPerfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdvPerfiles.SelectedItems[0] as GridDataItem;

        string PerfCve = dataItem["maPerfCve"].Text.ToString();

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, PerfCve);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        
        
        if (FnValAdoNet.bDSIsFill(ds)) {
        
            string Cve="", Des = "",Comen = "", Sit="";

            Cve= ds.Tables[0].Rows[0]["maPerfCve"].ToString();
            Des = ds.Tables[0].Rows[0]["maPerfDes"].ToString();
            Comen = ds.Tables[0].Rows[0]["maPerfComent"].ToString();
            Sit = ds.Tables[0].Rows[0]["maPerfSts"].ToString();

            rTxtCve.Text = Cve;
            rTxtDes.Text = Des;

            if (Comen != "&nbsp;")
            {
                rTxtComentarios.InnerText = Comen;
            }
            else
            {
                rTxtComentarios.InnerText = "";
            }
        }
  
        string val = dataItem["maPerfSts"].Text; 
        //string valComent = dataItem["maPerfComent"].Text;

        //if (dataItem != null)
        //{
        //    rTxtCve.Text = dataItem["maPerfCve"].Text;
        //    rTxtDes.Text = dataItem["maPerfDes"].Text;
        //}

        //if (valComent != "&nbsp;")
        //{
        //    rTxtComentarios.InnerText = valComent;
        //}else {
        //    rTxtComentarios.InnerText = "";
        //}

        if (val == "1")
        {
            CheckRegActivo.Checked = true;
            CheckRegInactivo.Checked = false;
        }
        if (val == "2")
        {
            CheckRegInactivo.Checked = true;
            CheckRegActivo.Checked = false;
        }

        CargaTreeds(dataItem["maPerfCve"].Text.ToString());
        LlenaDtlist(0);
        RadCheckTodos.Checked = false;
        SessBtns();
        

    }
    
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        LlenaDtlist(0);
        RadCheckTodos.Checked = false;
    }



    protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e)
    {
    
        DataTable dt = new DataTable();
        dt = (DataTable)Session["Sessionds"];
        BtnAccion(RadTreeView1.SelectedValue);
        LlenaDtlist(Convert.ToInt32(RadTreeView1.SelectedValue));
        Session["ValNode"] = RadTreeView1.SelectedValue;
        string ValNode = Session["ValNode"].ToString();

        foreach (RadTreeNode RtNd in RadTreeView1.CheckedNodes)
        {
            if (RtNd.Checked == true)
            {
                if (RtNd.Value == RadTreeView1.SelectedValue)
                {
                    Session["ValNode"] = RadTreeView1.SelectedValue;
                }
            }
        }

        ClearDtalist();
        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            DataRow dr = dt.Rows[i];
            if (dr["MaMenu"].ToString() == ValNode)
            {
                foreach (DataListItem dli in DataListBtn.Items)
                {
                    var RadCheckBox1 = dli.FindControl("RadCheckBox3") as RadCheckBox;
                    if (RadCheckBox1.Value == dr["accId"].ToString())
                    {
                        RadCheckBox1.Checked = true;
                    }

                }
            }
        }
        CheckAll();
    }

    protected void RadCheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["Sessionds"];
        RadCheckBox rcheck = (RadCheckBox)sender;
        string ValNode = Session["ValNode"].ToString();
        int VALCHE = 0;
        if (rcheck.Checked == true)
        {
            DataRow workRow = dt.NewRow();

            if (RadTreeView1.CheckedNodes.Count != 0)
            {


                foreach (RadTreeNode RtNd in RadTreeView1.CheckedNodes)
                {
                    if (RtNd.Checked == true)
                    {
                       
                        if (RtNd.Value == RadTreeView1.SelectedValue)
                        {
                            workRow["MaMenu"] = ValNode;
                            workRow["accId"] = rcheck.Value.ToString();
                            dt.Rows.Add(workRow);
                            VALCHE += 1;
                        }
                    }
                }

                if (VALCHE ==0)
                {
                    rcheck.Checked = false;
                }
            }else {
                rcheck.Checked = false;
            }
        }
        else
        {
        //ELIMINAR DEL DATASET///
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["MaMenu"].ToString() == ValNode && dr["accId"].ToString() == rcheck.Value.ToString())
                {
                    dr.Delete();
                    dt.AcceptChanges();
                }
            }
        }

        CheckAll();
    }

    protected void RadCheckTodos_CheckedChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["ValNode"] as string)) {
        
            DataTable dt = new DataTable();
            dt = (DataTable)Session["Sessionds"];
            string ValNode = Session["ValNode"].ToString();
            

            if (RadCheckTodos.Checked == true)
            {
                foreach (RadTreeNode RtNd in RadTreeView1.CheckedNodes)
                {
                    if (RtNd.Checked == true)
                    {
                        if (RtNd.Value == RadTreeView1.SelectedValue)
                        {
                            foreach (DataListItem dli in DataListBtn.Items)
                            {
                                var RadCheckBox1 = dli.FindControl("RadCheckBox3") as RadCheckBox;

                                if (RadCheckBox1.Checked == false)
                                {
                                    DataRow workRow = dt.NewRow();
                                    workRow["MaMenu"] = ValNode;
                                    workRow["accId"] = RadCheckBox1.Value.ToString();
                                    dt.Rows.Add(workRow);
                                    dt.AcceptChanges();
                                    RadCheckBox1.Checked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        RadCheckTodos.Checked = false;
                    }
                }
            }







            if (RadCheckTodos.Checked == false) {

                foreach (DataListItem dli in DataListBtn.Items)
                {
                    var RadCheckBox1 = dli.FindControl("RadCheckBox3") as RadCheckBox;
                    RadCheckBox1.Checked = false;

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["MaMenu"].ToString() == ValNode && dr["accId"].ToString() == RadCheckBox1.Value.ToString())
                        {
                            dr.Delete();
                            dt.AcceptChanges();
                        }
                    }
                }
           }
        }

    }

    #endregion

    #region FUNCIONES

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvPerfiles.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPerfiles, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvPerfiles, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtCve.Text.Trim() == "")
            {
                rTxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }



            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvPerfiles.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtCve.Text.Trim() == "")
            {
                rTxtCve.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtCve.CssClass = "cssTxtEnabled"; }

            if (rTxtDes.Text.Trim() == "")
            {
                rTxtDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDes.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvPerfiles.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }




        return sResult;
    }

    private int ValCheck()
    {
        if (CheckRegActivo.Checked == true)
        {
            return 1;
        }
        else
        {

            return 2;
        }
    }
    #endregion
    
    #region METODOS
    public void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        TituloPagina();
        LlenaComboModulo();
        LlenaGrid();
        ControlesAccion();
        rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvPerfiles.AllowMultiRowSelection = true;
        CargaTree("");
        //RadTreeView1.Enabled = false;
        SaveTree.Value = "1";
        LlenaSession();
        SessBtns();

        foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
        {
            //RtNd.Checked = false;
            RtNd.Enabled = false;
        }


        
        //PermisoBotones();
    }


    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }


    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }

    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Perfiles", "PnlMPFormTituloApartado");
    }

    private void LlenaTree()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


    }
    
    private void LlenaComboModulo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAModulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref this.rCboModulo, ds, "maModuCve", "maModuDes", true, false);
        ((Literal)rCboModulo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboModulo.Items.Count);
    }
    
    private void CargaTree(string Cbo)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAMenu";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maModuCve", DbType.String, 10, ParameterDirection.Input, rCboModulo.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@maMenuDes", DbType.String, 20, ParameterDirection.Input, rCboModulo.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            RadTreeView1.DataTextField = "maOperDes";
            RadTreeView1.DataValueField = "maMenuId";

            RadTreeView1.DataFieldID = "maMenuId";
            RadTreeView1.DataFieldParentID = "maMenuIdP";
            
            RadTreeView1.DataSource = ds.Tables[0];
            RadTreeView1.DataBind();
        }
    }

    public void GuardaTreeView()
    {

        EliminarPerf();

        foreach (RadTreeNode RtNd in RadTreeView1.CheckedNodes)
        {
            if (RtNd.Checkable == true)
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAPerf_MAMenu";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(RtNd.Value));

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
        }
    
    }

    private void EliminarPerf()
    {
        foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes()) {
            if (RtNd.Checked == false)
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAPerf_MAMenu";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania); 
                ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(RtNd.Value));
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            }
        }
    }

    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvPerfiles, ds);
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";
        CheckRegActivo.Checked = true;
        CheckRegInactivo.Checked = false;

        CheckRegActivo.Enabled = false;
        CheckRegInactivo.Enabled = false;

        this.rTxtCve.Enabled = false;
        this.rTxtDes.Enabled = false;
        RadTreeView1.Enabled = true;
        rTxtComentarios.Disabled = true;

        DataListBtn.Enabled = false;
        RadCheckTodos.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        RadTreeView1.CheckBoxes = false;

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
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            this.rTxtComentarios.Disabled = true;
            RadTreeView1.CheckBoxes = false;

            CheckRegActivo.Enabled = false;
            CheckRegInactivo.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
            this.rTxtComentarios.InnerText = "";

            CheckRegActivo.Checked = true;
            CheckRegInactivo.Checked = false;


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
                this.rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvPerfiles.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;
                this.rTxtComentarios.Disabled = false;

                RadTreeView1.CheckBoxes = true;

                CheckRegActivo.Enabled = true;
                CheckRegInactivo.Enabled = true;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";

                CheckRegActivo.Checked = true;
                CheckRegInactivo.Checked = false;

                this.rTxtComentarios.InnerText = "";
                foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
                {
                    RtNd.Checked = false;
                }
                LlenaDtlist(0);
                RadCheckTodos.Checked = false;
                DataListBtn.Enabled = true;
                RadCheckTodos.Enabled = true;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvPerfiles.AllowMultiRowSelection = false;
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;
                this.rTxtComentarios.Disabled = false;
                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                RadTreeView1.Enabled = true;
                RadCheckTodos.Enabled = true;
                CheckRegActivo.Enabled = true;
                CheckRegInactivo.Enabled = true;
                
                DataListBtn.Enabled = true;

                RadTreeView1.CheckBoxes = true;



                var dataItem = rGdvPerfiles.SelectedItems[0] as GridDataItem;

                CargaTreeds(dataItem["maPerfCve"].Text.ToString());
                //LlenaDtlist(0);


                CheckAll();

                SessBtns();


            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvPerfiles.AllowMultiRowSelection = true;
                rGdvPerfiles.MasterTableView.ClearSelectedItems();
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;
                RadTreeView1.Enabled = false;
                CheckRegActivo.Enabled = false;
                CheckRegInactivo.Enabled = false;
                RadTreeView1.CheckBoxes = true;
                this.rTxtComentarios.Disabled = true;
                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";
                CheckRegActivo.Checked = true;
                CheckRegInactivo.Checked = false;
                this.rTxtComentarios.InnerText = "";
                foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
                {
                    RtNd.Checked = false;
                }
            }
        }


        if (Result == false)
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;
            RadTreeView1.Enabled = false;
            CheckRegActivo.Enabled = false;
            CheckRegInactivo.Enabled = false;
            DataListBtn.Enabled = false;
            RadCheckTodos.Enabled = false;
            this.rTxtComentarios.Disabled = true;
            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";

            CheckRegActivo.Checked = true;
            CheckRegInactivo.Checked = false;

            this.rTxtComentarios.InnerText = "";
            foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
            {
                RtNd.Checked = false;
            }
        }


    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";

            CheckRegActivo.Checked = true;
            CheckRegInactivo.Checked = false;

            this.rTxtComentarios.InnerText = "";
            foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
            {
                RtNd.Checked = false;
            }
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvPerfiles.ClientSettings.Selecting.AllowRowSelect = true;

            rGdvPerfiles.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";


            rTxtCve.Enabled = false;
            rTxtDes.Enabled = false;
            rTxtComentarios.Disabled = true;
            RadTreeView1.Enabled = false;
            CheckRegActivo.Enabled = false;
            CheckRegInactivo.Enabled = false;

            CheckRegActivo.Checked = true;
            CheckRegInactivo.Checked = false;

            rTxtCve.Text = "";
            rTxtDes.Text = "";
            rTxtComentarios.InnerText = "";
            foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
            {
                RtNd.Checked = false;
            }

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }

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
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
        }
    }

    private void EjecutaSpAcciones()
    {
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
        ProcBD.AgregarParametrosProcedimiento("@maPerfDes", DbType.String, 100, ParameterDirection.Input, rTxtDes.Text);
        ProcBD.AgregarParametrosProcedimiento("@maPerfComent", DbType.String, 500, ParameterDirection.Input, rTxtComentarios.InnerText.ToString());
        ProcBD.AgregarParametrosProcedimiento("@maPerfSts", DbType.Int32, 0, ParameterDirection.Input, ValCheck());
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            ShowAlert(sEjecEstatus, sEjecMSG);
            if (sEjecEstatus == "1")
            {
                
                GuardaTreeView();
                GuardabtnAccion();
                EliminarPerf();
                SaveTree.Value = "1";
                SessBtns();

                //LlenaDtlist(Convert.ToInt32(RadTreeView1.SelectedValue));
                CheckAll();




                if (hdfBtnAccion.Value == "1")
                {
                    ControlesAccion();
                    InicioPagina();
                    LlenaDtlist(0);
                    RadCheckTodos.Checked = false;
                }

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    hdfBtnAccion.Value = "2";
                }else {
                    hdfBtnAccion.Value = "";
                }

                //ControlesAccion();
                //InicioPagina();
                

               

            }
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


            foreach (GridDataItem i in rGdvPerfiles.SelectedItems)
            {

                var dataItem = rGdvPerfiles.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string maPerfCve = dataItem.GetDataKeyValue("maPerfCve").ToString();
                    try
                    {
                    
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_MAPerfiles";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, maPerfCve);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


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
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    
    private void CargaTreeds(string valCve)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerf_MAMenu";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, valCve);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        RadTreeView1.ExpandAllNodes();

        foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
        {
            RtNd.Checked = false;
            RtNd.BackColor = System.Drawing.Color.Transparent;
        }

        foreach (RadTreeNode RtNd in RadTreeView1.GetAllNodes())
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string valNum = row["maMenuId"].ToString();
                if (valNum != "1")
                {
                    if (valNum == RtNd.Value)
                    {
                        RtNd.Checked = true;
                        RtNd.BackColor = System.Drawing.Color.Azure;
                    }
                    else
                    {
                        //RtNd.Checked = false;
                    }
                }
            }
        }
    }

    private void BtnAccion(string ValOperCve)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAOperacion_Acciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int32, 0, ParameterDirection.Input, ValOperCve);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string valor1 = "";
                valor1 = Convert.ToString(row["accId"]);
            }
        }
    }

    private void LlenaDtlist(int ValOperCve)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Acciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int32, 0, ParameterDirection.Input, ValOperCve);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            DataListBtn.DataSource = ds;
            DataListBtn.DataBind();
        }
    }

    private void LlenaSession()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAOperacion_Acciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            Session["SessMAMenu"] = ds;
        }

    }

    private void ClearDtalist()
    {

        foreach (DataListItem dli in DataListBtn.Items)
        {
            var RadCheckBox1 = dli.FindControl("RadCheckBox3") as RadCheckBox;
            RadCheckBox1.Checked = false;
        }
    }

    private void SessBtns()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAOperacion_Acciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (FnValAdoNet.bDSIsFill(ds))
        {
            Session["Sessionds"] = ds.Tables[0];
        }
    }

    private void CheckAll()
    {
        int valCheck = 0;


        foreach (DataListItem dli in DataListBtn.Items)
        {
            var RadCheckBox1 = dli.FindControl("RadCheckBox3") as RadCheckBox;
            if (RadCheckBox1.Checked == true)
            {
                valCheck += 1;
            }

            if (DataListBtn.Items.Count == valCheck)
            {
                RadCheckTodos.Checked = true;
            }
            else
            {
                RadCheckTodos.Checked = false;
            }
        }
    }

    private void GuardabtnAccion()
    {

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt = (DataTable)Session["Sessionds"];
        EliminaPerfil();

        foreach (DataRow drConfCpto in dt.Rows)
        {
            //if (drConfCpto["maOperCve"].ToString() != "")
            //{
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAPerfiles_Operacion_AccionesMtto";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@maMenuId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["maMenu"]));
                ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["accId"]));
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@Permiso", DbType.Int32, 0, ParameterDirection.Input, 1);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                    //ShowAlert(sEjecEstatus, sEjecMSG);
                }
            //}
            
        }
       
    }

    private void EliminaPerfil()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAPerfiles_Operacion_AccionesMtto";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
        ProcBD.AgregarParametrosProcedimiento("@maPerfCve", DbType.String, 10, ParameterDirection.Input, rTxtCve.Text);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        if (rCboModulo.SelectedValue != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@maModuCve", DbType.String, 10, ParameterDirection.Input, rCboModulo.SelectedValue);
        }
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    }

    #endregion










}