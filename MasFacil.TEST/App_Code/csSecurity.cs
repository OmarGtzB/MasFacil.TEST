using System;
using System.IO;
using Microsoft.VisualBasic;

using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for csSecurity
/// </summary>
/// 
namespace MGMSecurity
{


    public class SessionMangemer {

        private string _UsrInfo;

        public bool IsActiveSession(System.Web.UI.Page i_Page,  bool AllowRedToLogin = true , int BrowserWindows = 1)
        {
            bool result;      
            if (i_Page.Session[MGMSecurityVar.Session.LoginInfo] == null)
            {
                
                if (AllowRedToLogin) {

                    if (BrowserWindows == 0)
                    {
                        i_Page.Response.Redirect("~/login.aspx");
                    }
                    else {
                        i_Page.Response.Redirect("~/SessionExpirada.aspx");
                    }
                }
                result = false;

            }

            else {
                _UsrInfo = Convert.ToString( i_Page.Session[MGMSecurityVar.Session.LoginInfo]);
                result=  true;
            }

            return result;
        }

        public string LoginInfo
        {
            get
            {
                return _UsrInfo;
            }
            set
            {
                _UsrInfo = value;
            }
        }

        public void DeactivateSession(System.Web.UI.Page i_Page) {

            i_Page.Session[MGMSecurityVar.Session.LoginInfo] = null;
            i_Page.Session[MGMSecurityVar.SessionUser.maUsuSess] = null;

            i_Page.Session["Compania"] = "";
            i_Page.Session["Conexion"] = "";
            i_Page.Session["SessionLog"] = "";

            i_Page.Session["UsuNom"] = "";//Temporal
            i_Page.Session["user"] = ""; //Temporal

            i_Page.Session["maUsuSess"] = "";
        }

    }



    public class LoginManeger {

        ws.Servicio oWS = new ws.Servicio();
        MGM.Utilidades.Seguridad.Encriptacion SecurityAES = new MGM.Utilidades.Seguridad.Encriptacion();

        public void  LoginUsuario(string sUsrName, string str_UsrPassword, string str_SendConexionAutentifica, ref string oLoginInfo, ref string oSessionLog, ref string oResultMsg, ref MGMSecurityVar.LoginResult oLogResult,ref string conexAyu)
        {

            if (str_UsrPassword.Trim().Length == 0)
            {
                oResultMsg = "Complete la Informacion";
                oLogResult = MGMSecurityVar.LoginResult.PasswordError;
            }
            else {

                try
                {

                    string sConexion = "";
                    //sConexion = oWS.LoginSistema(str_SendConexionAutentifica);
                    sConexion = oWS.IniciarSesion(str_SendConexionAutentifica); 

                    if (sConexion == "")
                    {
                        oResultMsg = "Usuario o Contraseña Incorrecta";
                        oLogResult = MGMSecurityVar.LoginResult.UsrNoExist;
                    }
                    else {

                        oLoginInfo = sConexion;
                        oSessionLog = Obtener_strWSSessionLog(sUsrName, sConexion);
                        oResultMsg = "Login exitoso";

                        oLogResult = MGMSecurityVar.LoginResult.Succeed;
                        conexAyu = oWS.SesionAyuda();


                    }

                }
                catch (Exception ex)
                {
                    oResultMsg = "Error de conexion";
                    oLogResult = MGMSecurityVar.LoginResult.OtherError;
                }

            }
             
        }

        public string str_CadenaAutentifica(string sConexion, string sUsuario, string sContrasenia, string sTipoUsuario) {
            string sCadenaResul = "";
            try
            {
                string cadena = sConexion + "/" + sUsuario + "/" + sContrasenia + "/" + sTipoUsuario;
                sCadenaResul = SecurityAES.Encriptar(cadena);
                return sCadenaResul;
            }
            catch(Exception ex)
            {
                return "";
            }

        }

        public string Obtener_strWSSessionLog(string strUsuario, string strConexion)
        {
            string strResulS = "";

            try
            {
                string strFechLog = DateTime.Now.ToString("yyyyMMddhhmmss");
                //strResulS = oWS.SessionLog(strUsuario, strFechLog, strConexion);
                strResulS = oWS.ObtenerSesion(strUsuario, strFechLog, strConexion);
                return strResulS;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string sValSess(System.Web.UI.Page i_Page,int iOpc)
        {
            try
            {
                string Result = "";
                 string[] aDatosSession = Convert.ToString(i_Page.Session[MGMSecurityVar.SessionUser.maUsuSess]).Split('|');
                 Result = aDatosSession[iOpc];
                return Result;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

    }


}
