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
using System.Configuration;
using System.IO;

public partial class DC_AyudaUsu : System.Web.UI.Page
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
    private string Pag_sConexionAyu;
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
    protected void rBtnBuscar_Click(object sender, ImageButtonClickEventArgs e)
    {
        try
        {
            if (ValidaBusqueda() == true)
            {
                valHftBuscarPDF.Value = rtxtFiltro.Text;
                iframeReport.Visible = false;
                DataListPDF.Visible = true;
                RlblNumRegPdf.Visible = true;
                divPortada.Visible = false;

                RadTabStrip1.Tabs.FindTabByText("Imagenes").Selected = false;
                RadTabStrip1.Tabs.FindTabByText("Video").Selected = false;
                RadTabStrip1.Tabs.FindTabByText("PDF").Selected = true;
                RadPageVideo.Selected = false;
                RadPageImg.Selected = false;
                RadPagePdf.Selected = true;

                DataSet ds = new DataSet();
                ds = dsAyudas("");
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    LlenaPdf(ds.Tables[0]);
                    LlenaImagenes(ds.Tables[1]);
                    LlenaVideos(ds.Tables[2]);
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }

    }
    protected void rBtnMostrarPDF_Click(object sender, EventArgs e)
    {
        string valCve = "";
        RadButton btn = (RadButton)sender;

        foreach (DataListItem dlConf in DataListPDF.Items)
        {
            var objRadButton = dlConf.FindControl("rBtnMostrar") as RadButton;
            if (objRadButton.Value == btn.Value)
            {
                valCve = objRadButton.Value;
                break;
            }
        }

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 10, ParameterDirection.Input, valCve.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maAyuFile"];
            string nameRpt = "Help" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
            "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";
            string apppath = Server.MapPath("~/Temp") + "\\" + nameRpt;
            System.IO.File.WriteAllBytes(apppath, bytes);
            iframeReport.Attributes["src"] = "..//Temp//" + nameRpt;
        }

        DataListPDF.Visible = false;
        iframeReport.Visible = true;
        RlblNumRegPdf.Visible = false;
        IMG_DIV.Visible = true;
    }
    protected void rBtnMostrarIMG_Click(object sender, ImageButtonClickEventArgs e)
    {
        try
        {
            RadImageButton btn = (RadImageButton)sender;
            RdMimg.Image.Url = btn.Image.Url;
            IMG_DIV.Visible = false;
            DivImg.Visible = true;
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }
    }
    protected void rBtnOcultarIMG_Click(object sender, ImageButtonClickEventArgs e)
    {
        IMG_DIV.Visible = true;
        DivImg.Visible = false;
    }

    protected void rBtnSincronizarAyuda_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        ActualizaAyuda();
    }
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sConexionAyu = Convert.ToString(Session["ConexionAyu"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

    }
    private void InicioPagina()
    {
        ActualizaAyuda_Count();
    } 
    private void Limpiar()
    {
        try
        {
            DataListPDF.Visible = true;
            RlblNumRegPdf.Visible = true;
            divPortada.Visible = true;
            iframeReport.Visible = false;
            
            RadTabStrip1.Tabs.FindTabByText("PDF").Selected = false;
            RadTabStrip1.Tabs.FindTabByText("Imagenes").Selected = false;
            RadTabStrip1.Tabs.FindTabByText("Video").Selected = false;
        }
        catch (Exception ex)
        {
            ShowAlert("2", ex.ToString());
        }

    }

    private void LlenaPdf(DataTable dt)
    {
        DataListPDF.DataSource = dt;
        DataListPDF.DataBind();
        RlblNumRegPdf.ForeColor = System.Drawing.Color.Gray;
        if (dt.Rows.Count.ToString() == "1")
        {
            RlblNumRegPdf.Text = dt.Rows.Count.ToString() + " Resultado Encontrado";
        }
        else
        {
            RlblNumRegPdf.Text = dt.Rows.Count.ToString() + " Resultados Encontrados";
        }
    }
    private void LlenaImagenes(DataTable dt)
    {
      
        DataListImagenes.DataSource = dt;
        DataListImagenes.RepeatColumns = 3;
        DataListImagenes.DataBind();
        RlblNumRegImagenes.ForeColor = System.Drawing.Color.Gray;
        if (dt.Rows.Count.ToString() == "1")
        {
            RlblNumRegImagenes.ForeColor = System.Drawing.Color.Gray;
            RlblNumRegImagenes.Text = dt.Rows.Count.ToString() + " Resultado Encontrado";
        }
        else
        {
            RlblNumRegImagenes.Text = dt.Rows.Count.ToString() + " Resultados Encontrados";
        }

        //foreach (DataListItem dlConf in DataListImagenes.Items)
        //{
        //    //byte[] bytes = (byte[])dt.Rows[rowVal]["maAyuFile"];
        //    //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        //    var objRadImageButton = dlConf.FindControl("RdBnryImaagenes") as RadImageButton;
        //    //objRadImageButton.Image.Url = "data:image/png;base64," + base64String;
        //    string maAyuCve = dt.Rows[rowVal]["maAyuCve"].ToString();
        //    objRadImageButton.Image.Url = "data:image/png;base64," + base64StringAyuda(maAyuCve);
        //    rowVal++;
        //}
    }
    private void LlenaVideos(DataTable dt)
    {
        int rowVal = 0;
        DataListVideos.DataSource = dt;
        DataListVideos.RepeatColumns = 3;
        DataListVideos.DataBind();
        RlblNumRegVideo.ForeColor = System.Drawing.Color.Gray;
        if (dt.Rows.Count.ToString() == "1")
        {
            RlblNumRegVideo.Text = dt.Rows.Count.ToString() + " Resultado Encontrado";
        }
        else
        {
            RlblNumRegVideo.Text = dt.Rows.Count.ToString() + " Resultados Encontrados";
        }

        foreach (DataListItem dlConf in DataListVideos.Items)
        {
            var objRadMediaPlayer = dlConf.FindControl("RdMdiaVideos") as RadMediaPlayer;
            objRadMediaPlayer.ToolBar.HDButton.Style.Add("display", "none");
            objRadMediaPlayer.ToolBar.SubtitlesButton.Style.Add("display", "none");
        }
    }
    private void ActualizaAyuda_Count()
    {
        DataSet ds = new DataSet(); 
        ds = dsActualizacionesAyuda(51);
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            lblActualizaciones.Text = "Existen " + ds.Tables[0].Rows.Count.ToString() + " actualizaciones de ayuda. ¿Desea sinconizar con la Central?";
            string script = "function f(){$find(\"" + rWinActualizaciones.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
     }


    private void ActualizaAyuda() {
        DataSet ds = new DataSet();
        ds = dsActualizacionesAyuda(52);
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {


            int rowVal = 0;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                string maAyuCve = ds.Tables[0].Rows[rowVal]["maAyuCve"].ToString();
                string maAyuTipCve = ds.Tables[0].Rows[rowVal]["maAyuTipCve"].ToString();
                DateTime maAyuFec = Convert.ToDateTime(ds.Tables[0].Rows[rowVal]["maAyuFec"]) ;
                byte[] bytes_maAyuFile = (byte[])ds.Tables[0].Rows[rowVal]["maAyuFile"];
                string sExt = ".png";
                if (maAyuTipCve == "AYUVID") {
                    sExt= ".mp4";
                }

                string base64String = Convert.ToBase64String(bytes_maAyuFile, 0, bytes_maAyuFile.Length);
                string nameArchivoByte = maAyuCve + sExt;
 
                string apppath = "";
                apppath = Server.MapPath("~/Ayuda") + "\\" + nameArchivoByte;

                if (File.Exists(apppath)) {
                    File.Delete(apppath);
                }
 
                


                System.IO.File.WriteAllBytes(apppath, bytes_maAyuFile);

                if (File.Exists(apppath))
                {
                    Ejecuta_AyudaIdentificador(maAyuCve, maAyuFec);
                }

                



                rowVal += 1;
            }
            ShowAlert("1", "Sincronizacion Terminada.");
        }

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES

    private bool ValidaBusqueda()
    {
        if (rtxtFiltro.Text != "")
        {
            rtxtFiltro.CssClass = "cssTxtEnabled";
            return true;
        }
        else
        {
            rtxtFiltro.CssClass = "cssTxtInvalid";
            //Limpiar();
            return false;
        }
    }
    private DataSet dsAyudas(string maAyuTipCve) {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
        if (maAyuTipCve != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@maAyuTipCve", DbType.String, 50, ParameterDirection.Input, maAyuTipCve);
        }    
        ProcBD.AgregarParametrosProcedimiento("@Filtro", DbType.String, 50, ParameterDirection.Input, rtxtFiltro.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        return ds;
    }
    private string base64StringAyuda(string maAyuTipCve)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAAyuda";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 50, ParameterDirection.Input, maAyuTipCve); 
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);

        byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maAyuFile"];
        string base64String =  Convert.ToBase64String(bytes, 0, bytes.Length);
        return base64String;
    }

    private DataSet dsActualizacionesAyuda(Int32 opc)
    {
        DataSet ds = new DataSet();
        string maAyuIde = sidentificador();

        if (maAyuIde != "") {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAAyudaIdentificador";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@maAyuIden", DbType.String, 20, ParameterDirection.Input, maAyuIde);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        }
        return ds;
    }

    private void Ejecuta_AyudaIdentificador(String maAyuCve, DateTime maAyuFec)
    {
        DataSet ds = new DataSet();
        string maAyuIde = sidentificador();

        if (maAyuIde != "")
        {
            string fec = maAyuFec.Year.ToString() + "/" + maAyuFec.Month.ToString() + "/" + maAyuFec.Day.ToString();
            fec += " " + maAyuFec.TimeOfDay.ToString();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MAAyudaIdentificador";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@maAyuIden", DbType.String, 20, ParameterDirection.Input, maAyuIde);
            ProcBD.AgregarParametrosProcedimiento("@maAyuCve", DbType.String, 10, ParameterDirection.Input, maAyuCve);
            ProcBD.AgregarParametrosProcedimiento("@maAyuFec", DbType.String, 30, ParameterDirection.Input, fec);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionAyu);
        }

    }

    private String sidentificador() {
        string sValor = "";
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAIdentificador";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds)) {
            sValor = ds.Tables[0].Rows[0]["maIden"].ToString();
        }

        return sValor;
    }
    #endregion







}
