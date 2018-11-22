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
using System.Net.Mail;
using System.Text.RegularExpressions;

 
using System.Net;
 

using System.Net.Mime;
using System.Text;
/// <summary>
/// Summary description for csEMail
/// </summary>
/// 

namespace MGMEMail
{
  
    public class EMailConfig
    {
        MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
        ws.Servicio oWS = new ws.Servicio();

        private static String _servidor = "";
        private static Int32  _puerto = 0;
        private static String _correoUsuario = "";
        private static String _correoContrasenia = "";
        private static String _emisor = "";
        private static Boolean _SSL = true;
        public static String Servidor
        {
            get
            {
                return _servidor;
            }
            set
            {
                _servidor = value;
            }
        }
        public static Int32 Puerto
        {
            get
            {
                return _puerto;
            }
            set
            {
                _puerto = value;
            }
        }
        public static String CorreoUsuario
        {
            get
            {
                return _correoUsuario;
            }
            set
            {
                _correoUsuario = value;
            }
        }
        public static String CorreoContrasenia
        {
            get
            {
                return _correoContrasenia;
            }
            set
            {
                _correoContrasenia = value;
            }
        }
        public static String Emisor
        {
            get
            {
                return _emisor;
            }
            set
            {
                _emisor = value;
            }
        }
        public static Boolean SSL
        {
            get
            {
                return _SSL;
            }
            set
            {
                _SSL = value;
            }
        }
        public void valoresEMailConfig(string sPag_sConexionLog)
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConfiguracionMail";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 50);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds)) {
                EMailConfig.Servidor = ds.Tables[0].Rows[0]["confMailServSMTP"].ToString();
                EMailConfig.Puerto = Convert.ToInt32(ds.Tables[0].Rows[0]["confMailPuerSMTP"]);
                EMailConfig.CorreoUsuario = ds.Tables[0].Rows[0]["confMailCtaCorreo"].ToString();
                EMailConfig.CorreoContrasenia = ds.Tables[0].Rows[0]["confMailPwrCorreo"].ToString();
                EMailConfig.Emisor = ds.Tables[0].Rows[0]["confMailEmisor"].ToString();
                EMailConfig.SSL = Convert.ToBoolean(ds.Tables[0].Rows[0]["confMailEnableSsl"]); 
            }
        }
    }
    
    public class EMail
    {
        MGM.Correo.CorreoElectronico MGMMail = new MGM.Correo.CorreoElectronico();
        MGMEMail.EMailConfig FnEMailConfig = new MGMEMail.EMailConfig();
        MGMEMail.EMailHtml FnEMailHtml = new MGMEMail.EMailHtml();
    
         public bool Enviar(string sPag_sConexionLog, string sCorreoDestinatario, string sCorreoCopia)
        {
            Boolean bResult = true;
            //FnEMailConfig.valoresEMailConfig(sPag_sConexionLog);

            String[,] datos;
            datos = new String[0, 0];
            string sTextoHTML = FnEMailHtml.Estandar("Comprobante Fiscal Digital", "Folio: " + "190", "emmanuel", "santiago tex", "Se Anexan sus comprobantes Fiscales.");

            string envio = MGMMail.Enviar("smtp.gmail.com", 587, true, "Management", "mgm.soporte.desarrollo@gmail.com", "Soporte2017@", "CORREO TEST", "ehernandez@Inso.com.mx,", "ehernandez@Inso.com.mx,", "", datos, sTextoHTML);
            //FNEnvia.Enviar("smtp.gmail.com", 587, true, "Management", "mgm.soporte.desarrollo@gmail.com", "Soporte2017@", "CORREO TEST", "ehernandez@Inso.com.mx,", "", "", datos, sTextoHTML);

            return bResult;
        }

        public string EnviarCFDI(string sPag_sConexionLog, string sCiaCve, string sCiaDes, string sUsuarioEmisor, string sCorreoDestinatario, string sCorreoCopia, string sNombreCFDIXML, Byte[] byteXML, Byte[] bytePDF)
        {
            string sResult = "";
            FnEMailConfig.valoresEMailConfig(sPag_sConexionLog);


            String[,] ArrayArchivos;
            ArrayArchivos = new String[2, 2];
            ArrayArchivos[0, 0] = Convert.ToBase64String(byteXML, 0, byteXML.Length);
            ArrayArchivos[0, 1] = sNombreCFDIXML+ ".xml";
            ArrayArchivos[1, 0] = Convert.ToBase64String(bytePDF, 0, bytePDF.Length);
            ArrayArchivos[1, 1] = sNombreCFDIXML + ".pdf"; ;
            
            string sAsunto = "Comprobante Fiscal Digital: " + sNombreCFDIXML;
            string sTextoHTML = FnEMailHtml.Estandar("Comprobante Fiscal Digital", "Folio: " + sNombreCFDIXML, sUsuarioEmisor, sCiaDes, "Se Anexan sus comprobantes Fiscales.");

            sResult = MGMMail.Enviar(EMailConfig.Servidor
                                   , EMailConfig.Puerto
                                   , EMailConfig.SSL
                                   , EMailConfig.Emisor
                                   , EMailConfig.CorreoUsuario
                                   , EMailConfig.CorreoContrasenia
                                   , sAsunto
                                   , sCorreoDestinatario
                                   , sCorreoCopia
                                   , ""
                                   , ArrayArchivos
                                   , sTextoHTML);
            return sResult;
        }

    }

    public class EMailHtml {

        public string Estandar(string sTitulo, string sAsunto, string sEmisor, string sCompania, string sDetalle)
        {
            MailMessage mail = new MailMessage();
            string sHtml = "";
            sHtml += "<html>";
            sHtml += "<body style='width:100%; background-color:#F5F5F5;'>";
            sHtml += " <div align= 'center' style = 'width:100%'>";
            sHtml += "          <table  border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "             style = 'width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
            sHtml += "               background - color:white'>";
            sHtml += "                  <tr>";
            sHtml += "                      <td >";
            sHtml += "                          <img src='cid:imagen' width='620' heigh='350'/>";
            sHtml += "                        </td>";
            sHtml += "                    </tr>";
            sHtml += "            </table>";
            sHtml += "   </div>";
            sHtml += " <div align= 'center' style = 'width:100%'>";
            sHtml += "          <table  border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "             style = 'width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
            sHtml += "               background - color:white'>";
            sHtml += "                  <tr>";
            sHtml += "                      <td style='color:#1d6baf;'>";
            sHtml += "                          <div style = 'text-align:left; font-size:22px; font-weight: bold; float:left; width:540px; height: 25px;'>";
            sHtml += "                            <b>" + sCompania + "</b><br>";
            sHtml += "                           </div> ";
            sHtml += "                      " + sEmisor;
            sHtml += "                      </td >";
            sHtml += "                    </tr>";
            sHtml += "                    <tr>";
            sHtml += "                        <td style='color:#424242;'>";
            sHtml += "                          <P ALIGN='justify'>Por medio de nuestra Plataforma Tecnológica le informamos que <b>INNVENTA Soluciones S.A. de C.V</b> le ha enviado un documento CFDI</P>";
            sHtml += "                        </td>";
            sHtml += "                    </tr>";
            sHtml += "            </table>";
            sHtml += "  <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "    style = 'width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central; ";
            sHtml += "            border-radius: 5px; border: 1px solid #355C82; background-color:white' >";
            sHtml += "  <tr>";
            sHtml += "    <td>";
            sHtml += "      <div align = 'center' style = 'width:100%; margin-top:10px; ' >";
            sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0' ";
            sHtml += "                  style = '  width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static;";
            sHtml += "                  background-color:#1d6baf;' >";
            sHtml += "          <tr >";
            sHtml += "              <td style = 'border-bottom: 1px solid #355C82;  ' >";
            sHtml += "                  <div style = 'text-align:left; font-size:22px; color:white; font-weight: bold; float:left; width:580px; height: 25px;'>";
            sHtml += "                      " + sTitulo;
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'text-align:left; font-size:18px; color:white; font-weight: bold; float:left; width:580px; height: 25px;'>";
            sHtml += "                      " + sAsunto;
            sHtml += "                  </div>";
            sHtml += "              </td>";
            sHtml += "          </tr>";

            //sHtml += "          </table>";
            //sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0'";
            //sHtml += "              style = '  width:620px; height:300px; margin:0px; font-family:Arial; color:#355C82; position:static;";
            //sHtml += "              background-color:#AAC2D9'>";

            sHtml += "          <tr style='font-family:Arial; color:#355C82; background-color:#AAC2D9' >";
            sHtml += "             <td style = 'border-bottom: 1px solid #424242;' >";
            sHtml += "                 <br> <div style = 'text-align:left; font-size:13px; color:#424242; font-weight: bold;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      &nbsp; EMITIDO POR:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = ' margin-left:10px;  text-align:left;  color:#424242; font-size:12px;  float:left; width:580px; height: 20px;' >";
            sHtml += "                 &nbsp;    <b> " + sEmisor + "</b><br><br>";
            sHtml += "                  </div >";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          </table>";

            sHtml += "          <br>";
            sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0' ";
            sHtml += "              style = 'width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
            sHtml += "              background-color:transparent ' >";
            sHtml += "          <tr>";
            sHtml += "              <td>";
            sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold; color:#424242; float:left; width:580px; height: 20px;' >";
            sHtml += "                      COMPAÑIA:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'margin-left:10px;  text-align:left; font-size:12px; color:#424242; float:left; width:580px; height: 20px;' >";
            sHtml += "                    <b> " + sCompania + "</b>";
            sHtml += "                  </div>";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          <tr>";
            sHtml += "              <td>";
            sHtml += "                  <br/>";
            sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold; color:#424242; float:left; width:580px; height: 20px;' >";
            sHtml += "                      DETALLE:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'margin-left:10px;  text-align:left; font-size:12px; color:#424242; float:left; width:580px; height: 20px;' >";
            sHtml += "                     <b> " + sDetalle + "</b>";
            sHtml += "                  </div>";
            sHtml += "                  <br/>";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          </table>";
            sHtml += "          <br/>";
            sHtml += "      </div>";
            sHtml += "    </td>";
            sHtml += "   </tr>";
            sHtml += "  </table>";
            sHtml += " <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "      style = 'padding-top:10px; width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
            sHtml += "      background-color:transparent' > ";
            sHtml += "  <tr>";
            sHtml += "      <td>";
            sHtml += "          No responda a este mensaje ya que ha sido generado automáticamente para su información.";
            sHtml += "      </td >";
            sHtml += "  </tr>";
            sHtml += " </table>";
            sHtml += " <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "     style = 'padding-top:10px; width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
            sHtml += "     background - color:transparent'>";
            sHtml += "   <tr>";
            sHtml += "       <td style='color:#1d6baf;'>";
            sHtml += "              <b>Atentamente</b><br>";
            sHtml += "              INNVENTA Soluciones, S.A.DE C.V.. <br>";
            sHtml += "         <b>Correo: </b> aortiz@innventa.com.mx <br>";
            sHtml += "          <b>Tel:</b> 55-50-85-05";
            sHtml += "      </td>";
            sHtml += "   </tr>";
            sHtml += "   <tr>";
            sHtml += "      <td>";
            sHtml += "          <hr style='width:610px; border-color:#1d6baf; border-width:3px;'/>";
            sHtml += "      </td> ";
            sHtml += "   </tr>";
            sHtml += "   <tr>";
            sHtml += "       <td style='color:#1d6baf;'>";
            sHtml += "          <b>Aviso de Confidencialidad</b>";
            sHtml += "          </td>";
            sHtml += "       </tr>";
            sHtml += "       <tr>";
            sHtml += "          <td style='color:#777777;'>";
            sHtml += "           <P ALIGN='justify'> Este correo electrónico y/o el material adjunto es para uso exclusivo de la persona o entidad a la que expresamente se le ha enviado, y puede contener información confidencial o material privilegiado.Si usted no es el destinatario legítimo del mismo, por favor repórtelo inmediatamente al remitente del correo y bórrelo. Cualquier revisión, retransmisión, difusión o cualquier otro uso de este correo, por personas o entidades distintas a las del destinatario legítimo, queda expresamente prohibido. Este correo electrónico no pretende ni debe ser considerado como constitutivo de ninguna relación legal, contractual o de otra índole similar.</P>";
            sHtml += "  </td>";
            sHtml += "   </tr>";
            sHtml += "    <tr>";
            sHtml += "     <td style='color:#777777;'>";
            sHtml += "        <P ALIGN='justify'> <b>INNVENTA</b> y ciertos nombres de productos utilizados aquí son marcas comerciales o marcas registradas de <b>INNVENTA</b> y/o una de sus subsidiarias o afiliadas. Vea Marcas para las marcas apropiadas. Cualquier otra marca registrada aquí contenida es propiedad de sus respectivos dueños</P>";
            sHtml += "      </td>";
            sHtml += "   </tr>";
            sHtml += "   <tr>";
            sHtml += "       <td style='color:#777777;'>";
            sHtml += "          Copyright © 2017 <b>INNVENTA</b> and/ or its subsidiaries or affiliates. All rights reserved.";
            sHtml += "        </td>";
            sHtml += "    </tr>";
            sHtml += "   </table>";
            sHtml += "   </div>";
            sHtml += "   </body>";
            sHtml += "</html>";

            return sHtml;
        }
        public string EstandarV1(string sTitulo, string sAsunto, string sEmisor, string sCompania, string sDetalle)
        {
            string sHtml = "";

            sHtml += "<body style='width:100%; background-color:#F5F5F5;' >";
            sHtml += "<div align= 'center' style = 'width:100%' >";

            sHtml += "  <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "    style = 'width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central; ";
            sHtml += "            border-radius: 5px; border: 1px solid #355C82; background-color:white' >";
            sHtml += "  <tr>";
            sHtml += "    <td>";
            sHtml += "      <div align = 'center' style = 'width:100%; margin-top:10px; ' >";
            sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "                  style = '  width:600px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static;";
            sHtml += "                  background-color:transparent ' >";
            sHtml += "          <tr >";
            sHtml += "              <td style = 'border-bottom: 1px solid #355C82;' >";
            sHtml += "                  <div style = 'text-align:left; font-size:22px; color:Gray; font-weight: bold; float:left; width:580px; height: 25px;' >";
            sHtml += "                      " + sTitulo;
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'text-align:left; font-size:18px; font-weight: bold; float:left; width:580px; height: 25px;' >";
            sHtml += "                      " + sAsunto;
            sHtml += "                  </div>";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          </table>";
            sHtml += "          <br/>";

            sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0'";
            sHtml += "              style = '  width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
            sHtml += "              background-color:transparent ' >";
            sHtml += "          <tr>";
            sHtml += "              <td style = 'border-bottom: 1px solid #355C82;' >";
            sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      Emitido por:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = ' margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      " + sEmisor;
            sHtml += "                  </div >";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          </table>";
            sHtml += "          <br/>";

            sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0' ";
            sHtml += "              style = 'width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
            sHtml += "              background-color:transparent ' >";
            sHtml += "          <tr>";
            sHtml += "              <td>";
            sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      Compañia:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      " + sCompania;
            sHtml += "                  </div>";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          <tr>";
            sHtml += "              <td>";
            sHtml += "                  <br/>";
            sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      Detalle:";
            sHtml += "                  </div>";
            sHtml += "                  <div style = 'margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
            sHtml += "                      " + sDetalle;
            sHtml += "                  </div>";
            sHtml += "                  <br/>";
            sHtml += "              </td>";
            sHtml += "          </tr>";
            sHtml += "          </table>";
            sHtml += "          <br/>";

            sHtml += "      </div>";
            sHtml += "    </td>";
            sHtml += "   </tr>";
            sHtml += "  </table>";

            sHtml += " <table border = '0' cellpadding = '0' cellspacing = '0' ";
            sHtml += "      style = 'padding-top:10px; width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
            sHtml += "      background-color:transparent' > ";
            sHtml += "  <tr>";
            sHtml += "      <td>";
            sHtml += "          No responda a este mensaje ya que ha sido generado automáticamente para su información.";
            sHtml += "      </td >";
            sHtml += "  </tr>";
            sHtml += " </table>";

            sHtml += "</div>";
            sHtml += "</body>";

            return sHtml;
        }

    }



}
