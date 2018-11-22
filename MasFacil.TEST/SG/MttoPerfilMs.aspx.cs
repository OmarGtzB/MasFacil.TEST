using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;

public partial class SG_MttoPerfilMs : System.Web.UI.Page
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
    private string Pag_sCompania;
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

    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        try
        {
            string ext = e.File.GetExtension();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png" || ext == ".JPG")
            {
                BinaryReader reader = new BinaryReader(e.File.InputStream);
                Byte[] data = reader.ReadBytes((int)e.File.InputStream.Length);
                imgPerfil.DataValue = data;
                string valor = Convert.ToBase64String(data);

                arregloImagen.Value = valor;
            }
            else
            {
                string sResult = "", sMSGTip = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1030", ref sMSGTip, ref sResult);
                //ShowAlert(sMSGTip, sResult);

                arregloImagen.Value = "";
            }
        }
        catch (Exception ex)
        {
            //ShowAlert("2", ex.ToString());
        }
    }

    protected void rBtnGuardarPerfil_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (rdtxtNombreUsr.Text != "")
        {
           EjecutaAccion();
        }

        if (arregloImagen.Value != "")
        {
            Guardaimagen();
        }

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    protected void rBtnCancelarPerfil_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        rdtxtNombreUsr.Text = "";
        rdtxtNombreUsr.CssClass = "cssTxtEnabled";
        CargaControlls();


    }
    
    private void EjecutaAccion()
    {

        DataSet ds = new DataSet();
        String sUsuCve = Convert.ToString(Session["user"]);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsuarios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, sUsuCve);
        ProcBD.AgregarParametrosProcedimiento("@maUsuNom", DbType.String, 200, ParameterDirection.Input, rdtxtNombreUsr.Text.Trim());

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            if (sEjecEstatus == "1")
            {
                Session["UsuNom"] = rdtxtNombreUsr.Text.Trim();
                rdtxtNombreUsr.Text = "";
            }
        }
    }

    public void Guardaimagen()
    {
        string maUser = LM.sValSess(this.Page, 1);
        if (arregloImagen.Value != "")
        {
            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_MAUsuarioImagen";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@ImgUsu", DbType.Binary, 0, ParameterDirection.Input, arregloImagen.Value);
                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                if (FnValAdoNet.bDSIsFill(ds))
                {
                    string sEjecEstatus, sEjecMSG = "";
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                }
            }
            catch (Exception ex)
            {
                
            }
            arregloImagen.Value = "";
        }

    }

    private void CargaControlls()
    {

        string maUser = LM.sValSess(this.Page, 1);

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MAUsuarios";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {


            
                rlblUsarios.Text = maUser;
                rdtxtNombreUsr.Text = ds.Tables[0].Rows[0]["maUsuNom"].ToString();


                if (ds.Tables[0].Rows[0]["maUsuFoto"].ToString() == "")
                {
                    //RadBinaryImage1.ImageUrl = "../imagenes/NoArt/sinArticulos.png";
                }
                else
                {
                    byte[] bytes = (byte[])ds.Tables[0].Rows[0]["maUsuFoto"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgPerfil.ImageUrl = "data:image/png;base64," + base64String;
                }


        }

    }

    #endregion

    #region FUNCIONES

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion



 
}