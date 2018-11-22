using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;
public partial class MPForm : System.Web.UI.MasterPage
{
    #region VARIABLES
        MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
        ws.Servicio oWS = new ws.Servicio();
        MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
        MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
        MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

        private string Pag_sConexionLog;
        private string Pag_sSessionLog;
    #endregion

    #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
        Valores_vsPag();
        LlenaCompanias();
        LogoCompania();
        ImagenUsuario();
        if (!IsPostBack)
        {
            LlenaMenu();
            RadMenu2.Items[0].Text = Convert.ToString(Session["UsuNom"]);
        }

        RWComapania.ContentContainer.Controls.Add(pnlCompanias);

    }

        protected void rBtnAceptar_Click(object sender, EventArgs e)
        {
        //string pageVirtualPath = this.Page.AppRelativeVirtualPath;
        //string pageQueryString = this.Page.ClientQueryString;
        //if (pageQueryString != "")
        //{
        //    pageQueryString = "?" + pageQueryString;
        //}
        //pageVirtualPath = pageVirtualPath + pageQueryString;

        string pageVirtualPath = "~/Home.aspx";

        Session["Compania"] = rCboCompanias.SelectedValue.ToString();
        Response.Redirect(pageVirtualPath);

        LlenaMenu();
        }


    protected void rBtn_Click(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {
    //PERFIL
        if (e.Item.Value == "per")
        {
            string script = "function f(){$find(\"" + RWPerfil.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

    //Compañia
        if (e.Item.Value == "cia")
        {
            string script = "function f(){$find(\"" + RWComapania.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

    //Cerrar Sesion
        if (e.Item.Value == "ses") {
            SM.DeactivateSession(this.Page);
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Redirect("~/login.aspx"); 
        }

        
    }
    

    protected void rdAyuda_Click(object sender, EventArgs e)
    {
        //string script = "function f(){$find(\"" + rWinAyudaUsu.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MGMayudaExterna";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            string url = ds.Tables[0].Rows[0]["LINK"].ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
        }

    }

    //protected void rBtnGuardarPerfil_Click(object sender, ImageButtonClickEventArgs e)
    //{
    //    if (rdtxtNombreUsr.Text != "")
    //    {
    //        EjecutaAccion();
    //    }
    //    if (arregloImagen.Value != "")
    //    {
    //        Guardaimagen();
    //    }
    //}

    //protected void rBtnCancelarPerfil_Click(object sender, ImageButtonClickEventArgs e)
    //{

    //}

    //protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    //{
    //    try
    //    {
    //        string ext = e.File.GetExtension();
    //        if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png" || ext == ".JPG")
    //        {
    //            BinaryReader reader = new BinaryReader(e.File.InputStream);
    //            Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
    //            imgPerfil.DataValue = data;
    //            RadBinaryImage2.DataValue = data;
    //            string valor = Convert.ToBase64String(data);

    //            arregloImagen.Value = valor;
    //        }
    //        else
    //        {
    //            string sResult = "", sMSGTip = "";
    //            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
    //            //ShowAlert(sMSGTip, sResult);

    //            arregloImagen.Value = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //ShowAlert("2", ex.ToString());
    //    }
    //}

    #endregion

    #region METODOS

    private void Valores_vsPag() 
    {
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    private void LlenaCompanias()
        {
            DataSet ds = new DataSet();
            string ciaCve = Convert.ToString(Session["Compania"]);

            String sUsuCve = Convert.ToString(Session["user"]);
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_selCompaniasUsuario";
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@sessionlog", DbType.String, 30, ParameterDirection.Input, Pag_sSessionLog);
            ProcBD.AgregarParametrosProcedimiento("@ciaCveSess", DbType.String, 10, ParameterDirection.Input, ciaCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string ciaCveSess = ds.Tables[0].Rows[0]["ciaCveSess"].ToString();
            if (ciaCveSess != "") {
                ciaCve = ciaCveSess;
            }

            FnCtlsFillIn.RadComboBox(ref rCboCompanias, ds,"ciaCve", "ciaDes",false , true  , ciaCve);

            Session["Compania"] = rCboCompanias.SelectedValue.ToString();
            lblCompaniaMaster.Text = rCboCompanias.SelectedItem.Text.Trim();
            }
            else
            {
                //Response.Redirect("~/Login.aspx");
            }
    }

    public void LogoCompania()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CompaniaImagen";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(Session["Compania"]));
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds)) {
            if (ds.Tables[0].Rows[0]["ciaImgLogo"].ToString() == null)
            {
                //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
            }
            else
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["ciaImgLogo"];

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                RadBinaryImage1.ImageUrl = "data:image/png;base64," + base64String;

            }

        }
 
    }

    private void LlenaMenu() {

            DataSet ds = new DataSet();

            String sUsuCve = Convert.ToString (Session["user"]);

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SelMAMenu";
            ProcBD.AgregarParametrosProcedimiento("@maMenuTipo", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(Session["compania"]));
            ProcBD.AgregarParametrosProcedimiento("@sessionlog", DbType.String, 30, ParameterDirection.Input, Pag_sSessionLog);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                FnCtlsFillIn.cRadMenu(ref RadMenuPrincipal, ds, "maMenuId", "maMenuIdP", "maMenuDes", "maOperEjec");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            
    }

    public void ImagenUsuario()
    {
        String sUsuCve = Convert.ToString(Session["user"]);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsuarios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds)) {
            if (ds.Tables[0].Rows[0]["maUsuFoto"].ToString() == null)
            {
                //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
            }
            else
            {
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maUsuFoto"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                RadBinaryImage2.ImageUrl = "data:image/png;base64," + base64String;
            }
        }

 
    }
    
    //private void ImgPerfil()
    //{
    //    String sUsuCve = Convert.ToString(Session["user"]);
    //    string maUser = LM.sValSess(this.Page, 1);

    //    DataSet ds = new DataSet();
    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_MAUsuarios";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 51);
    //    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
    //    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


    //    if (FnValAdoNet.bDSRowsIsFill(ds))
    //    {
    //        if (ds.Tables[0].Rows[0]["maUsuFoto"].ToString() == null)
    //        {
    //            //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
    //        }
    //        else
    //        {
    //            byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maUsuFoto"];
    //            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
    //            imgPerfil.ImageUrl = "data:image/png;base64," + base64String;

    //        }
    //    }
    //    rlblUsarios.Text = maUser;

    //}

    //private void EjecutaAccion()
    //{

    //    DataSet ds = new DataSet();
    //    String sUsuCve = Convert.ToString(Session["user"]);

    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_MAUsuarios";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
    //    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
    //    ProcBD.AgregarParametrosProcedimiento("@maUsuNom", DbType.String, 200, ParameterDirection.Input, rdtxtNombreUsr.Text.Trim());

    //    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //    if (FnValAdoNet.bDSIsFill(ds))
    //    {
    //        string sEjecEstatus, sEjecMSG = "";
    //        sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
    //        sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
    //        if (sEjecEstatus == "1")
    //        {
    //            Session["UsuNom"] = rdtxtNombreUsr.Text.Trim();
    //            RadMenu2.Items[0].Text = Convert.ToString(Session["UsuNom"]);
    //            rdtxtNombreUsr.Text = "";
    //        }
    //    }
    //}
    
    //public void Guardaimagen()
    //{
    //    string maUser = LM.sValSess(this.Page, 1);
    //    if (arregloImagen.Value != "")
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //            ProcBD.NombreProcedimiento = "sp_MAUsuarioImagen";
    //            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 2);
    //            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
    //            ProcBD.AgregarParametrosProcedimiento("@ImgUsu", DbType.Binary, 0, ParameterDirection.Input, arregloImagen.Value);
    //            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    //            if (FnValAdoNet.bDSIsFill(ds))
    //            {
    //                string sEjecEstatus, sEjecMSG = "";
    //                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
    //                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        arregloImagen.Value = "";
    //    }
    //}


    #endregion

    #region FUNCIONES



    #endregion






}
