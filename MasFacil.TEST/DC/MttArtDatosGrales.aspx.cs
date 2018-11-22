using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.VisualStyles;





public partial class DC_Articulo : System.Web.UI.Page
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





    private DataSet dsitems;

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string PagLoc_ArtCve;
    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {

        if (SM.IsActiveSession(this.Page))
        {

            Valores_InicioPag();
            if (!IsPostBack)
            {

                pag_inicio();

            }

        }

    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        //rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        parmSecValorHabilDesa();
        art_pintar_txt();
        cssEna();
        habil();
        habil_ValRef();
        rBtnLimpiar.Enabled = false;
        ControlesAccion();


    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
        //lbl_artic_mensaje.Text = "";
        cssEna();

        //pag_inicio();
        //Limpiar_ValRef();
        //limpiar_txt();

        if (PagLoc_ArtCve == "")
        {
            Limpiar_ValRef();
            limpiar_txt();
            RadBinaryImage1.ImageUrl = "";
        }
        else
        {
            dt_variables.DataSource = llenadatalistVarRef(2);
            dt_variables.DataBind();
            dt_referencias.DataSource = llenadatalistVarRef(1);
            dt_referencias.DataBind();

            if (rad_TxtdescArt.Enabled == false)
            {
                Desa_ValRef();
            }
            else
            {
                habil_ValRef();
            }

            art_pintar_txt();
        }
        
    }
    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        string ext = e.File.GetExtension();
        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
        {
            BinaryReader reader = new BinaryReader(e.File.InputStream);
            Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
            RadBinaryImage1.DataValue = data;
            string valor = Convert.ToBase64String(data);
            arregloImagen.Value = valor;
            csValorCadenaImg.CadenaImagen = valor;


        }
        else
        {
            RadWindowManager.RadAlert("Formato incorrecto", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + "2" + ".png");

            // arregloImagen.Value = "";
            csValorCadenaImg.CadenaImagen = "";
        }

        //this.RadBinaryImage1.Width = Unit.Pixel(83);
        //this.RadBinaryImage1.Height = Unit.Pixel(95);

        //using (Stream stream = e.File.InputStream)
        //{
        //    byte[] imageData = new byte[stream.Length];
        //    stream.Read(imageData, 0, (int)stream.Length);
        //    this.RadBinaryImage1.DataValue = imageData;
        //    string valor = Convert.ToBase64String(imageData);

        //    csValoresTmp.Name = valor;


        //}

    }

    protected void ButtbrowseButtonon1_Click(object sender, EventArgs e)
    {
        // Se crea el OpenFileDialog
        OpenFileDialog dialog = new OpenFileDialog();
        // Se muestra al usuario esperando una acción
        DialogResult result = dialog.ShowDialog();

        // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
        // la mostramos en el PictureBox de la inferfaz
        if (result == DialogResult.OK)
        {
            //PictureBox.Image = Image.FromFile(dialog.FileName);
        }
    }

    protected void rTxtPrueba_TextChanged(object sender, EventArgs e)
    {
        setLabelCodeArt();
    }

    protected void rCboPrueba_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        int indexItem;
        indexItem = 0;
        int selectedIndex;
        selectedIndex = 0;


        var objCombo = sender as RadComboBox;


        //Obtener index seleccionado
        //foreach (DataListItem dli in DATALISTPRUEBA.Items)
        //{
        //    var hdfagrCve = dli.FindControl("hdfSecArt") as HiddenField;
        //    var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;

        //    if (e.OldValue != cboagrCve.SelectedValue && cboagrCve.SelectedValue == e.Value  )
        //    {
        //        //hdfSecArtAct.Value = hdfagrCve.Value;
        //        break;
        //    }

        //    selectedIndex++;

        //}


        selectedIndex = objCombo.TabIndex;

        //habilitar y desabilitar en funcion del selected index

        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {
            var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;

            var txtagrCve = dli.FindControl("rTxtPrueba") as RadTextBox;

            if (dli.ItemIndex > selectedIndex)
            {
                cboagrCve.Text = "";
                cboagrCve.ClearSelection();
                cboagrCve.Enabled = false;

                txtagrCve.Text = "";
                txtagrCve.Enabled = false;

            }
            
        }


        //Cargar y habilitar combo correspondiente
        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {

            var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;
            var valEstCod = dli.FindControl("EstCodTip") as HiddenField;

            var txtagrCve = dli.FindControl("rTxtPrueba") as RadTextBox;

            if (cboagrCve.Enabled == true)
            {
                indexItem++;
            }
            else if (cboagrCve.Enabled == false)
            {
                if (valEstCod.Value.ToString() == "4")
                {
                    fillCboAgrupacionesDato(indexItem, e.Value.ToString());
                }
                else
                {
                    fillCboAgrupacionesDato(indexItem, "");
                }

                txtagrCve.Enabled = true;

                break;
            }

        }

        setLabelCodeArt();


    }

    protected void FNSeleccionRegistroApartadoEstandarCodigo_Load(object sender, EventArgs e)
    {

        //Inicia preparacion formClvArticulo
        llenaDatalistCombo();

        //llenar y habilitar primer control - duro comboindependiente
        fillCboAgrupacionesDato(0, "");

        //Asignar propiedades de acuerdo al tipo a los combos
        setPropiedadesCboAgr();

    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (PagLoc_ArtCve == "")
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
            validaLongtud();
        }
        else
        {
            validaLongtud();

        }

        //EjecutaAccion();

    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {

        pag_inicio();
        ControlesAccion();
        hdfBtnAccion.Value = "";
        csValorCadenaImg.CadenaImagen = "";
        claveDesa();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";

        if (PagLoc_ArtCve == "")
        {
            RadBinaryImage1.ImageUrl = "";
        }



    }
    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_ArtCve = Convert.ToString(Session["folio_Selection"]);

    }

    public void pag_inicio()
    {

        hdfBtnAccion.Value = "";
        ControlesAccion();
        FnCtlsFillIn.RadComboBox_UnidadesMedida(Pag_sConexionLog, Pag_sCompania, ref rad_Cbouddmed, true, false);
        FnCtlsFillIn.RadComboBox_Clasificacion(Pag_sConexionLog, Pag_sCompania, ref rad_Dldclasif, true, false);
        FnCtlsFillIn.RadComboBox_Existencias(Pag_sConexionLog, Pag_sCompania, ref rad_Cboexi, true, false);
        FnCtlsFillIn.RadComboBox_MetVal(Pag_sConexionLog, Pag_sCompania, ref radCmboMetVal, true, false);
        limpiar_txt();
        allcombos.Text = "";
        art_pintar_txt();
        parmSecValor();
        long_ref_val();

        Limpiar_ValRef();
        Desa_ValRef();

        dt_variables.DataSource = llenadatalistVarRef(2);
        dt_variables.DataBind();
        dt_referencias.DataSource = llenadatalistVarRef(1);
        dt_referencias.DataBind();
        //Inicia preparacion formClvArticulo
        llenaDatalistCombo();

        //Prueba Asignacion de tabindex.
        setTabCbo();

        //llenar y habilitar primer control - duro comboindependiente
        fillCboAgrupacionesDato(0, "");

        //Asignar propiedades de acuerdo al tipo a los combos
        setPropiedadesCboAgr();
        rBtnModificar.Image.Url = "../Imagenes/IcoBotones/IcoBtnModificar.png";


        if (PagLoc_ArtCve == "")
        {
            habil();
            rBtnModificar.Enabled = false;
            //radtxt_articuloTXT.Visible = false;
            habil_ValRef();

            rad_Cbouddmed.EmptyMessage = "Seleccionar";
            rad_Cbouddmed.ClearSelection();
            rad_Cbouddmed.Text = string.Empty;
            rBtnLimpiar.Enabled = true;
            parmSecValorHabilDesa();
            parmSecValor();
            allcombos.Text = "";
        }
        else
        {
            desa();
            rBtnModificar.Enabled = true;
            claveDesa();
            Desa_ValRef();

        }
        
        PermisoBotones();

        foreach (DataListItem dli in DATALISTPRUEBA.Items) {
        
            var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;

            var txtArtBox = dli.FindControl("rTxtPrueba") as RadTextBox;

            if (cboagrCve.Visible == true)
            {
                txtArtBox.Enabled = false;
                break;
            }else {
                txtArtBox.Enabled = true;
                break;
            }
        }


          
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
    private void setTabCbo()
    {
        Int16 count = 0;
        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {
            var rCboPrueba = dli.FindControl("rCboPrueba") as RadComboBox;
            rCboPrueba.TabIndex = count;
            count++;
        }
    }

    public void recorrerRefe()
    {
        if (PagLoc_ArtCve == "")
        {
            hdfBtnAccion.Value = Convert.ToString(1);
        }

        int unomas = 0;
        foreach (DataListItem dli in dt_referencias.Items)
        {
            unomas = unomas + 1;
            var references = dli.FindControl("txt_ref") as RadTextBox;

            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

                ProcBD.NombreProcedimiento = "sp_ArticuloRefVar";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, "ART");
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, unomas);
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 1);
                if (references.Text != "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@revaValRef", DbType.String, 15, ParameterDirection.Input, references.Text);
                }

                ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, allcombos.Text);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }
            catch (Exception ex)
            {
                ShowAlert("2", ex.ToString());
            }
        }
    }

    public void recorrerVari()
    {
        if (PagLoc_ArtCve == "")
        {
            hdfBtnAccion.Value = Convert.ToString(1);
        }


        int unomas = 0;
        foreach (DataListItem dli in dt_variables.Items)
        {
            unomas = unomas + 1;
            var references = dli.FindControl("txt_var") as RadNumericTextBox;

            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ArticuloRefVar";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, 10, ParameterDirection.Input, "ART");
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 0, ParameterDirection.Input, unomas);
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 0, ParameterDirection.Input, 2);
                if (references.Text != "" && references.Text != "0")
                {
                    ProcBD.AgregarParametrosProcedimiento("@revaValVar", DbType.Decimal, (15), ParameterDirection.Input, Convert.ToDecimal(references.Text));
                }
                ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, allcombos.Text);



                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            }
            catch (Exception ex)
            {
                ShowAlert("2", ex.ToString());
            }
        }

    }

    public void claveDesa()
    {

        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {
            var references = dli.FindControl("rCboPrueba") as RadComboBox;
            DATALISTPRUEBA.Enabled = false;
        }


    }

    public void claveHabil()
    {

        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {
            var references = dli.FindControl("rCboPrueba") as RadComboBox;
            DATALISTPRUEBA.Enabled = true;
        }


    }

    public void Desa_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Enabled = false;
        }


        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Enabled = false;
        }
    }

    public void long_ref_val()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            //references.MaxLength = 15;
        }


        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            //references.MaxLength = 10;
        }

    }
    public void habil_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Enabled = true;
        }
        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Enabled = true;
        }
    }
    public void Limpiar_ValRef()
    {
        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.Text = "";
        }

        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.Text = "";
        }
    }
    public void CSSRefVar()
    {

        foreach (DataListItem dli in dt_variables.Items)
        {
            var references = dli.FindControl("txt_ref") as RadTextBox;
            references.CssClass = "cssTxtEnabled";
        }

        foreach (DataListItem dli in dt_referencias.Items)
        {
            var references = dli.FindControl("txt_var") as RadNumericTextBox;
            references.CssClass = "cssTxtEnabled";
        }

    }





    public void cmbo_css()
    {
        rad_Dldclasif.BorderWidth = Unit.Pixel(1);
        rad_Dldclasif.BorderColor = System.Drawing.Color.Transparent;

        rad_Cbouddmed.BorderWidth = Unit.Pixel(1);
        rad_Cbouddmed.BorderColor = System.Drawing.Color.Transparent;

        radCmboMetVal.BorderWidth = Unit.Pixel(1);
        radCmboMetVal.BorderColor = System.Drawing.Color.Transparent;

    }
    public void limpiar_txt()
    {
        rad_Dldclasif.Text = "";

        //radtxt_articulo.Text.Trim() = "";
        rad_TxtdescArt.Text = "";
        rad_TxtAbrev.Text = "";
        rad_Txtlargo.Text = "";
        rad_Txtancho.Text = "";
        rad_Txtalt.Text = "";
        rad_Txtpeso.Text = "";

        radTxtVlorRepo.Text = "";
        radtxtUltmPre.Text = "";
        radtxtCstEtndr.Text = "";

        rad_area_desExted.InnerText = "";
        rad_Dldclasif.EmptyMessage = "Seleccionar";
        rad_Dldclasif.ClearSelection();
        rad_Dldclasif.Text = string.Empty;
        rad_Cbouddmed.EmptyMessage = "Seleccionar";
        rad_Cbouddmed.ClearSelection();
        rad_Cbouddmed.Text = string.Empty;

        rad_Cboexi.EmptyMessage = "Seleccionar";
        rad_Cboexi.ClearSelection();
        rad_Cboexi.Text = string.Empty;

        radCmboMetVal.EmptyMessage = "Seleccionar";
        radCmboMetVal.ClearSelection();
        radCmboMetVal.Text = string.Empty;

        lbl_artic_mensaje.Text = "";


        rad_Dldclasif.BorderWidth = Unit.Pixel(1);
        rad_Dldclasif.BorderColor = System.Drawing.Color.Transparent;

        rad_Cbouddmed.BorderWidth = Unit.Pixel(1);
        rad_Cbouddmed.BorderColor = System.Drawing.Color.Transparent;
    }

    public void desa()
    {

        //radtxt_articuloTXT.Enabled = false;
        //radtxt_articulo.Enabled = false;
        rad_TxtdescArt.Enabled = false;
        rad_TxtAbrev.Enabled = false;
        rad_Txtlargo.Enabled = false;
        rad_Txtancho.Enabled = false;
        rad_Txtalt.Enabled = false;
        rad_Txtpeso.Enabled = false;
        rad_Dldclasif.Enabled = false;
        rad_Cbouddmed.Enabled = false;
        rad_Cboexi.Enabled = false;
        // rad_txt_metodoVal.Enabled = false;
        rad_area_desExted.Disabled = true;
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        RadAsyncUpload1.Enabled = false;



        radTxtVlorRepo.Enabled = false;
        radtxtUltmPre.Enabled = false;
        radtxtCstEtndr.Enabled = false;
        radCmboMetVal.Enabled = false;


    }

    public void habil()
    {

        //radtxt_articuloTXT.Enabled = false;
        //radtxt_articulo.Enabled = false;
        rad_TxtdescArt.Enabled = true;
        rad_TxtAbrev.Enabled = true;
        rad_Txtlargo.Enabled = true;
        rad_Txtancho.Enabled = true;
        rad_Txtalt.Enabled = true;
        rad_Txtpeso.Enabled = true;
        rad_Dldclasif.Enabled = true;
        rad_Cbouddmed.Enabled = true;
        rad_Cboexi.Enabled = true;
        //rad_txt_metodoVal.Enabled = true;
        rad_area_desExted.Disabled = false;
        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;
        RadAsyncUpload1.Enabled = true;


        radTxtVlorRepo.Enabled = true;
        radtxtUltmPre.Enabled = true;
        radtxtCstEtndr.Enabled = true;
        radCmboMetVal.Enabled = true;

    }

    public void cssEna()
    {

        rad_TxtdescArt.CssClass = "cssTxtEnabled";
        rad_TxtAbrev.CssClass = "cssTxtEnabled";
        //rad_Txtlargo.CssClass = "cssTxtEnabled";
        //rad_Txtancho.CssClass = "cssTxtEnabled";
        //rad_Txtalt.CssClass = "cssTxtEnabled";
        //rad_Txtpeso.CssClass = "cssTxtEnabled";

        allcombos.BorderColor = System.Drawing.Color.Transparent;


    }




    public void imagen()
    {

        if (PagLoc_ArtCve == "")
        {
            hdfBtnAccion.Value = "1";
        }
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_ArticuloImagenes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, allcombos.Text);
            ProcBD.AgregarParametrosProcedimiento("@ArtImg", DbType.Binary, 0, ParameterDirection.Input, csValorCadenaImg.CadenaImagen);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());

        }
        csValorCadenaImg.CadenaImagen = "";
    }


    public void traeImagen()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();


        ProcBD.NombreProcedimiento = "sp_ArticuloImagenes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        //if (FnValAdoNet.bDSIsFill(ds))
        //   {
        //    MessageBox.Show("tiene info");
        //}

        if (ds.Tables[0].Rows.Count > 0)
        {

            if (ds.Tables[0].Rows[0]["ArtImg"].ToString() == null)
            {
                //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
            }
            else
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["ArtImg"];

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                RadBinaryImage1.ImageUrl = "data:image/png;base64," + base64String;

                csValorCadenaImg.CadenaImagen = base64String;
            }

        }
        else
        {
            //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
        }




    }



    public void guardar_art()
    {

        try
        {
            if (PagLoc_ArtCve == "")
            {
                hdfBtnAccion.Value = "1";
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_Articulos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, allcombos.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@artDes", DbType.String, 50, ParameterDirection.Input, rad_TxtdescArt.Text);
            ProcBD.AgregarParametrosProcedimiento("@artAbr", DbType.String, 15, ParameterDirection.Input, rad_TxtAbrev.Text);
            ProcBD.AgregarParametrosProcedimiento("@artDesExt", DbType.String, 200, ParameterDirection.Input, rad_area_desExted.InnerText);
            ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, rad_Cbouddmed.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@artClasi", DbType.String, 1, ParameterDirection.Input, rad_Dldclasif.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@artManPart", DbType.String, 1, ParameterDirection.Input, rad_Cboexi.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@metValId", DbType.Int64, 2, ParameterDirection.Input, radCmboMetVal.SelectedValue);

            if (radtxtUltmPre.Text == "")
            {
                radtxtUltmPre.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artUltPreComp", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radtxtUltmPre.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artUltPreComp", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radtxtUltmPre.Text));
            }

            if (radtxtCstEtndr.Text == "")
            {
                radtxtCstEtndr.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artCosEsta", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radtxtCstEtndr.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artCosEsta", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radtxtCstEtndr.Text));
            }


            if (radTxtVlorRepo.Text == "")
            {
                radTxtVlorRepo.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artValRepo", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radTxtVlorRepo.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artValRepo", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(radTxtVlorRepo.Text));
            }



            /////////////////
            if (rad_Txtlargo.Text == "")
            {
                rad_Txtlargo.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artMedLarg", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtlargo.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artMedLarg", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtlargo.Text));
            }

            if (rad_Txtancho.Text == "")

            {
                rad_Txtancho.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artMedAnch", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtancho.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artMedAnch", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtancho.Text));
            }
            if (rad_Txtalt.Text == "")
            {
                rad_Txtalt.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artMedAlto", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtalt.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artMedAlto", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtalt.Text));
            }
            if (rad_Txtpeso.Text == "")
            {
                rad_Txtpeso.Text = "0.00";
                ProcBD.AgregarParametrosProcedimiento("@artMedPeso", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtpeso.Text));
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@artMedPeso", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rad_Txtpeso.Text));
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //limpiar_txt(); 
            desa();
            Desa_ValRef();

            if (FnValAdoNet.bDSIsFill(ds))
            {
                recorrerRefe();
                recorrerVari();
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                ShowAlert(sEjecEstatus, sEjecMSG);

                if (ds.Tables[0].Rows[0]["maMSGTip"].ToString() == "1")
                {
                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                    claveDesa();

                    if (RadBinaryImage1.ImageUrl != "")
                    {
                        imagen();
                    }

                    csValorCadenaImg.CadenaImagen = "";

                    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";


                }


                if (ds.Tables[0].Rows[0]["maMSGTip"].ToString() == "2")
                {
                    //Inicia preparacion formClvArticulo
                    llenaDatalistCombo();

                    //llenar y habilitar primer control - duro comboindependiente
                    fillCboAgrupacionesDato(0, "");

                    //Asignar propiedades de acuerdo al tipo a los combos
                    setPropiedadesCboAgr();
                    //limpiar_txt();
                    habil();
                    habil_ValRef();
                    claveHabil();
                    //allcombos.Text = "";
                }
                else
                {

                    if (sEjecEstatus == "3")
                    {
                        habil_ValRef();
                        claveHabil();
                        habil();
                        radCmboMetVal.Enabled = true;
                        radtxtCstEtndr.Enabled = true;
                        radTxtVlorRepo.Enabled = true;
                        radtxtUltmPre.Enabled = true;
                    }
                    else
                    {

                        if (hdfBtnAccion.Value == Convert.ToString(1))
                        {

                            PagLoc_ArtCve = Convert.ToString(allcombos.Text.Trim());

                            Session["folio_Selection"] = Convert.ToString(allcombos.Text.Trim());

                            desa();

                            art_pintar_txt();
                            claveDesa();
                            rBtnModificar.Enabled = true;
                            //ControlesAccion();
                            rBtnGuardar.Enabled = false;
                            rBtnCancelar.Enabled = false;
                            desa();
                            radCmboMetVal.Enabled = false;
                            radtxtCstEtndr.Enabled = false;
                            radtxtUltmPre.Enabled = false;
                            radTxtVlorRepo.Enabled = false;

                        }
                        else
                        {

                            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                        }



                    }



                    ////////////////////////////
                }
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        //recorrerVari();

    }

    public void art_pintar_txt()
    {

        if (PagLoc_ArtCve == "")
        {
            if (allcombos.Text == "")
            {
                limpiar_txt();
                radCmboMetVal.ClearSelection();
                rad_Cboexi.ClearSelection();

            }
            else
            {
                PagLoc_ArtCve = allcombos.Text.Trim();
            }

            Limpiar_ValRef();



            //desa();

        }
        else
        {
            limpiar_txt();


            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Articulos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            allcombos.Text = Convert.ToString(ds.Tables[0].Rows[0]["artCve"]);
            //radtxt_articulo.DataTextField = Convert.ToString(ds.Tables[0].Rows[0]["artCve"]);
            rad_TxtdescArt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artDes"]);
            rad_TxtAbrev.Text = Convert.ToString(ds.Tables[0].Rows[0]["artAbr"]);
            rad_Cbouddmed.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["uniMedCve"]);
            rad_Dldclasif.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["artClasi"]);
            rad_area_desExted.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["artDesExt"]);
            rad_Txtlargo.Text = Convert.ToString(ds.Tables[0].Rows[0]["artMedLarg"]);
            rad_Txtalt.Text = Convert.ToString(ds.Tables[0].Rows[0]["artMedAlto"]);
            rad_Txtpeso.Text = Convert.ToString(ds.Tables[0].Rows[0]["artMedPeso"]);
            rad_Txtancho.Text = Convert.ToString(ds.Tables[0].Rows[0]["artMedAnch"]);
            radtxtCstEtndr.Text = Convert.ToString(ds.Tables[0].Rows[0]["artCosEsta"]);
            radTxtVlorRepo.Text = Convert.ToString(ds.Tables[0].Rows[0]["artValRepo"]);
            radtxtUltmPre.Text = Convert.ToString(ds.Tables[0].Rows[0]["artUltPreComp"]);

            if (Convert.ToString(ds.Tables[0].Rows[0]["artManPart"]) == "")
            {
                rad_Cboexi.ClearSelection();

            }
            else
            {
                rad_Cboexi.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["artManPart"]);
            }


            if (Convert.ToString(ds.Tables[0].Rows[0]["metValCve"]) == "0")
            {
                radCmboMetVal.ClearSelection();
            }
            else
            {
                radCmboMetVal.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["metValCve"]);
            }

            traeImagen();

        }
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManager.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }





    private void EjecutaAccion()
    {
        cssEna();
        cmbo_css();
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                  hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                guardar_art();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                //EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }



    }



    private void fillCboAgrupacionesDato(int indexItem, string fltAgrDato)
    {
        int longitud, acumulado;
        acumulado = 0;
        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {



            if (dli.ItemIndex == indexItem)
            {
                DataSet ds = new DataSet();

                var valAgrCve = dli.FindControl("hdn_AgrCve") as HiddenField;
                var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;
                var valEstCod = dli.FindControl("EstCodTip") as HiddenField;


                if (valEstCod.Value == "4")
                {
                    acumulado += LongElemen(dli.ItemIndex + 1);
                    ds = ObtenerDatoAgr(valAgrCve.Value, fltAgrDato, acumulado);

                }
                else
                {
                    longitud = LongElemen(dli.ItemIndex + 1);

                    ds = ObtenerDatoAgr(valAgrCve.Value, fltAgrDato, longitud);

                }




                cboagrCve.Enabled = true;

                //cboagrCve.EmptyMessage = "Seleccionar";
                //cboagrCve.DataTextField = "agrDatoDes";
                //cboagrCve.DataValueField = "agrDatoCve";
                //cboagrCve.DataSource = ds.Tables[0];

                //try
                //{
                //    cboagrCve.DataBind();
                //}
                //catch (Exception ex)
                //{

                //    MessageBox.Show(ex.ToString());
                //}


                FnCtlsFillIn.RadComboBox(ref cboagrCve, ds, "agrDatoCve", "agrDatoDes", true, false);
                ((Literal)cboagrCve.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(cboagrCve.Items.Count);

                cboagrCve.ClearSelection();
                cboagrCve.Text = string.Empty;

            }
            else
            {

                acumulado += LongElemen(dli.ItemIndex + 1);






            }


        }

    }

    public void llenaDatalistCombo()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ArticuloEstructuraCodigo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        DATALISTPRUEBA.DataSource = ds;
        DATALISTPRUEBA.DataBind();


    }


    private int LongElemen(int elemento)
    {
        Int16 regresar;
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ArticuloEstructuraCodigo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@estCodElem", DbType.Int16, 2, ParameterDirection.Input, elemento);

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);



            regresar = Convert.ToInt16(ds.Tables[0].Rows[0]["artEstCodLong"]);

            return regresar;


        }
        catch (Exception)

        {
            regresar = -1;
            return regresar;
        }







    }





    private void ControlesAccion()
    {
        //MODIFICAR
       if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            rBtnLimpiar.Enabled = true;
        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
            hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
              )
        {

            desa();
            Desa_ValRef();
            //cssEna();
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        }

        //===> Botones GUARDAR - CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
       )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        }
        else
        {
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        }
        
    }

    public void parmSecValor()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        parmsec.Value = Convert.ToString(ds.Tables[0].Rows[0]["parmValInt"]);


        if (parmsec.Value == "1")
        {

        }
        else if (parmsec.Value == "2")
        {
            DataSet ds1 = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBDC = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBDC.NombreProcedimiento = "sp_Articulos";
            ProcBDC.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBDC.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBDC.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            radCmboMetVal.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["parmValInt"]);




        }

    }

    private void setPropiedadesCboAgr()
    {
        foreach (DataListItem dli in DATALISTPRUEBA.Items) 
        {

            var valAgrCve = dli.FindControl("hdn_AgrCve") as HiddenField;
            var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;
            var valEstCod = dli.FindControl("EstCodTip") as HiddenField;

            var txtArtBox = dli.FindControl("rTxtPrueba") as RadTextBox;

            if (valEstCod.Value.ToString() == "1" | valEstCod.Value.ToString() == "2")
            {
                cboagrCve.AllowCustomText = true;


                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_Articulos";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                cboagrCve.MaxLength = Convert.ToInt16(ds.Tables[0].Rows[0]["artEstCodLong"]);
                txtArtBox.MaxLength = Convert.ToInt16(ds.Tables[0].Rows[0]["artEstCodLong"]);

                txtArtBox.Visible = true;
                cboagrCve.Visible = false;
                //no permitir elegir
            }
            else
            {
                cboagrCve.AllowCustomText = false;
                //si permitir elegir

                txtArtBox.Visible = false;
                cboagrCve.Visible = true;
            }

        }


    }

    public void parmSecValorHabilDesa()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        parmsec.Value = Convert.ToString(ds.Tables[0].Rows[0]["parmValInt"]);


        if (parmsec.Value == "1")
        {
            radCmboMetVal.ClearSelection();
            radCmboMetVal.Enabled = true;
            radtxtCstEtndr.Enabled = true;
            radtxtCstEtndr.Text = "";
            radTxtVlorRepo.Enabled = true;
            radTxtVlorRepo.Text = "";
            radtxtUltmPre.Enabled = true;
            radtxtUltmPre.Text = "";
        }
        else if (parmsec.Value == "2")
        {
            //DataSet ds1 = new DataSet();
            //MAAK.Procedimientos.ProcedimientoAlmacenado serProcc = new MAAK.Procedimientos.ProcedimientoAlmacenado();
            //serProcc.NombreProcedimiento = "sp_Articulos";
            //serProcc.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            //serProcc.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            //ds1 = oWS.ObtenerDatasetDesdeProcedimiento(serProcc.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //radCmboMetVal.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["parmValInt"]);
            radCmboMetVal.ClearSelection();
            radCmboMetVal.Enabled = false;
            radtxtCstEtndr.Enabled = false;
            radtxtCstEtndr.Text = "";
            radTxtVlorRepo.Enabled = false;
            radTxtVlorRepo.Text = "";
            radtxtUltmPre.Enabled = false;
            radtxtUltmPre.Text = "";

        }


    }

    public void validaLongtud()
    {

        EjecutaAccion();


        //int longTotal = 0, numcarac;

        //DataSet ds = new DataSet();
        //MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        //ProcBD.NombreProcedimiento = "sp_Articulos";
        //ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
        //ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        //longTotal = Convert.ToInt32(ds.Tables[0].Rows[0]["artEstCodLong"]);

        //numcarac = allcombos.Text.Length;



        //if (numcarac < longTotal)
        //{
        //    ShowAlert("2", "La longitud de la clave es menor de lo permitido");
        //    allcombos.BorderWidth = Unit.Pixel(1);
        //    allcombos.BorderColor = System.Drawing.Color.Red;


        //}
        //else
        //{
        //    EjecutaAccion();
        //}

        //if (numcarac > longTotal)
        //{
        //    ShowAlert("2", "La longitud de la clave es mayor de lo permitido");
        //    allcombos.BorderWidth = Unit.Pixel(1);
        //    allcombos.BorderColor = System.Drawing.Color.Red;
        //}



    }



    private void setLabelCodeArt()
    {
        allcombos.Text = null;

        foreach (DataListItem dli in DATALISTPRUEBA.Items)
        {
            var cboagrCve = dli.FindControl("rCboPrueba") as RadComboBox;
            var valEstCod = dli.FindControl("EstCodTip") as HiddenField;

            var txtagrCve = dli.FindControl("rTxtPrueba") as RadTextBox;

            if (cboagrCve.Enabled == true & (cboagrCve.SelectedIndex >= 0 | cboagrCve.AllowCustomText == true))
            {
                if (valEstCod.Value.Trim().ToString() == "4")
                {
                    allcombos.Text = cboagrCve.SelectedValue.Trim().ToString();
                }
                else if (valEstCod.Value.Trim().ToString() == "1" | valEstCod.Value.Trim().ToString() == "2")
                {
                    allcombos.Text += txtagrCve.Text;
                }
                else
                {
                    allcombos.Text += cboagrCve.SelectedValue.Trim().ToString();
                    //allcombos.Text += cboagrCve.SelectedValue.TRIM.ToString.();
                }
            }
        }
    }

    public void guardaImgdb(string filefoto)
    {

        try
        {
            MemoryStream ms = new MemoryStream();
            FileStream fs = new FileStream(filefoto, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            ms.SetLength(fs.Length);
            fs.Read(ms.GetBuffer(), 0, (int)fs.Length);

            byte[] arrImg = ms.GetBuffer();
            ms.Flush();
            fs.Close();

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_ArticuloRefVar";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, allcombos.Text);
            ProcBD.AgregarParametrosProcedimiento("@ArtImg", DbType.Binary, 20, ParameterDirection.Input, arrImg);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        }
        catch (Exception)
        {

            throw;
        }



    }

    #endregion

    #region FUNCIONES
    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }

    private string validaEjecutaAccion(ref string sMSGTip)

    {


        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {


            if (allcombos.Text == "")
            {

                allcombos.BorderWidth = Unit.Pixel(1);
                allcombos.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }

            if (rad_TxtdescArt.Text.Trim() == "")
            {
                rad_TxtdescArt.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            if (rad_TxtAbrev.Text.Trim() == "")
            {
                rad_TxtAbrev.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }

            ////
            if (rad_Cbouddmed.SelectedValue.Trim() == "")
            {
                rad_Cbouddmed.CssClass = "cssTxtInvalid";
                rad_Cbouddmed.BorderWidth = Unit.Pixel(1);
                rad_Cbouddmed.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            if (rad_Dldclasif.SelectedValue.Trim() == "")
            {
                rad_Dldclasif.CssClass = "cssTxtInvalid";
                rad_Dldclasif.BorderWidth = Unit.Pixel(1);
                rad_Dldclasif.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            if (radCmboMetVal.SelectedValue.Trim() == "")
            {
                radCmboMetVal.CssClass = "cssTxtInvalid";
                radCmboMetVal.BorderWidth = Unit.Pixel(1);
                radCmboMetVal.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
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

            if (PagLoc_ArtCve == "")
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }
            if (allcombos.Text == "")
            {
                allcombos.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }

            if (rad_TxtdescArt.Text.Trim() == "")
            {
                rad_TxtdescArt.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }


            if (rad_TxtAbrev.Text.Trim() == "")
            {
                rad_TxtAbrev.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            if (rad_Cbouddmed.SelectedValue.Trim() == "")
            {
                rad_Cbouddmed.CssClass = "cssTxtInvalid";
                rad_Cbouddmed.BorderWidth = Unit.Pixel(1);
                rad_Cbouddmed.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            if (rad_Dldclasif.SelectedValue.Trim() == "")
            {
                rad_Dldclasif.CssClass = "cssTxtInvalid";
                rad_Dldclasif.BorderWidth = Unit.Pixel(1);
                rad_Dldclasif.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            if (radCmboMetVal.SelectedValue.Trim() == "")
            {
                radCmboMetVal.CssClass = "cssTxtInvalid";
                radCmboMetVal.BorderWidth = Unit.Pixel(1);
                radCmboMetVal.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }
        return sResult;

    }

    private DataTable GetData()
    {
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_agrupaciones_Articulos";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataTable data = new DataTable();

        if (FnValAdoNet.bDSIsFill(ds))
        {
            data = ds.Tables[0];

        }

        return data;
    }

    private DataSet llenadatalistVarRef(Int32 revaTip)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, PagLoc_ArtCve);
        ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "ART");
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }

    private DataSet ObtenerDatoAgr(String argDato, string fltAgrDato, int longitud)
    {

        int opc;
        if (fltAgrDato != "")
        {
            opc = 51;
        }
        else
        {
            opc = 51;
        }


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, (1), ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 20, ParameterDirection.Input, argDato);
        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, fltAgrDato.Trim());
        ProcBD.AgregarParametrosProcedimiento("@longElem", DbType.Int32, (1), ParameterDirection.Input, longitud);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }

    private DataSet dsautocomplete()
    {
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_agrupaciones_Articulos";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    private DataSet ObtenerDatoAgr(String argDato)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int32, (1), ParameterDirection.Input, 1);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 20, ParameterDirection.Input, argDato);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }
    #endregion




    
    




    

}