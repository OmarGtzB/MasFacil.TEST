using System;
using System.Data;
using System.IO;
 
public partial class Login : System.Web.UI.Page
{

    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    protected void Page_Load(object sender, EventArgs e)
    {
        SM.DeactivateSession(this.Page); 
    }
    protected void Click_RBtn_Login(object sender, EventArgs e)
    {
        Autentication(txt_usuario.Text ,this.txt_pasword.Text);
    }
    protected void Autentication(string sUsrName, string sUsrPassword)
    {
        
        string strConexLog = "", strConexAyuda ="",  strSessionLog = "",   ResultMsg = "" ;
        MGMSecurityVar.LoginResult LogResult = new MGMSecurityVar.LoginResult();
 
        LM.LoginUsuario ( sUsrName, sUsrPassword, str_SendConexionAutentifica(sUsrName, sUsrPassword), ref strConexLog, ref strSessionLog, ref ResultMsg,ref LogResult,ref strConexAyuda);

        if (LogResult == MGMSecurityVar.LoginResult.OtherError || LogResult == MGMSecurityVar.LoginResult.PasswordError)
        {
            lbl_LoginError.Visible = true;
            lbl_LoginError.Text = ResultMsg;
        }
        else if (LogResult == MGMSecurityVar.LoginResult.UsrNoExist)
        {
            lbl_LoginError.Visible = true;
            lbl_LoginError.Text = ResultMsg;
        }
        else if (LogResult == MGMSecurityVar.LoginResult.Succeed)
        {

            lbl_LoginError.Visible = false;
            valores_Login(strConexLog, strSessionLog, strConexAyuda);
            Response.Redirect("~/Home.aspx");

        }
        else {
            SM.DeactivateSession(this.Page);
            Response.Redirect("~/Login.aspx");
        }

    }
    private string str_SendConexionAutentifica(string sUsrName, string sUsrPassword)
    {
        string strResul = "";
        try
        {
            StreamReader sr = new StreamReader(Server.MapPath("./App_Data/configuracion/config.txt"));
            string[] sConfig = sr.ReadToEnd().Split('|');
            strResul = LM.str_CadenaAutentifica(sConfig[1], sUsrName, sUsrPassword, sConfig[2]);
        }
        catch (Exception ex)
        {
            return "";
        }

        return strResul;
    }
    private void valores_Login(string sConexLog, string sSessionLog,string strConexAyuda) {

        Session[MGMSecurityVar.Session.LoginInfo] = sConexLog;
        Session[MGMSecurityVar.SessionUser.maUsuSess] = sSessionLog;

        string[] aDatosSession = sSessionLog.Split('|');

        Session["Compania"] = "";
        Session["Conexion"] = sConexLog;
        Session["SessionLog"] = aDatosSession[0];

        Session["UsuNom"] = aDatosSession[2];//Temporal
        Session["user"] = txt_usuario.Text.Trim(); //Temporal
        Session["ConexionAyu"] = strConexAyuda;
        Session["ConexionCorp"] = strConexAyuda;
    }

}