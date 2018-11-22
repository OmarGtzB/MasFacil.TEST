using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_Default : System.Web.UI.Page
{
    ws.Servicio oWS = new ws.Servicio();
 
    MGMEMail.EMail FnEMail = new MGMEMail.EMail();

    MGM.Correo.CorreoElectronico MGMMail = new MGM.Correo.CorreoElectronico();
    MGMEMail.EMailConfig FnEMailConfig = new MGMEMail.EMailConfig();
    MGMEMail.EMailHtml FnEMailHtml = new MGMEMail.EMailHtml();
    protected void Page_Load(object sender, EventArgs e)
    {


        string sTextoHTML = "";// FnEMailHtml.Estandar("Comprobante Fiscal Digital", "nombre", "emisor", "compania", "Se Anexan sus comprobantes Fiscales.");
        formato(ref   sTextoHTML);

        //sTextoHTML = formato2();

        String[,] datos;
        datos = new String[0, 0];
        string envio = MGMMail.Enviar("smtp.gmail.com", 587, true, "Management", "mgm.soporte.desarrollo@gmail.com", "Soporte2017@", "CORREO TEST", "ehernandez@Inso.com.mx,", "", "mensaje ", datos, sTextoHTML);

    }


    private void formato(ref string sHtml)
    {

        sHtml = "";

        //sHtml += "< html  >";
        //sHtml += "< head runat = 'server' >";
        //sHtml += " < title ></ title >";
        //sHtml += " < style type = 'text/css' >";
        //sHtml += " </ style >";
        //sHtml += "</ head >";

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
        sHtml += "                      Comprobante Fiscal Digital";
        sHtml += "                  </div>";
        sHtml += "                  <div style = 'text-align:left; font-size:18px; font-weight: bold; float:left; width:580px; height: 25px;' >";
        sHtml += "                      Folio 59";
        sHtml += "                  </div>";
        sHtml += "              </td>";
        sHtml += "          </tr>";
        sHtml += "          </table>";
        sHtml += "          <br/>";

        sHtml += "          <table border = '0' cellpadding = '0' cellspacing = '0'";
        sHtml += "              style = '  width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
        sHtml += "              background-color:transparent ' >";
        sHtml += "          <tr>";
        sHtml += "              <tdstyle = 'border-bottom: 1px solid #355C82;' >";
        sHtml += "                  <div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Emitido por:";
        sHtml += "                  </div>";
        sHtml += "                  <div style = ' margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Emmanuel";
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
        sHtml += "                      Desarrollo";
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
        sHtml += "                      Se Anexan sus comprobantes Fiscales. ";
        sHtml += "                  </div>";
        sHtml += "              </td>";
        sHtml += "          </tr>";
        sHtml += "          </table>";
        sHtml += "          <br/>";

        sHtml += "      </div>";
        sHtml += "    </td>";
        sHtml += "   </tr>";
        sHtml += "  </table>";

        sHtml += " <table border = '0' cellpadding = '0' cellspacing = '0' ";
        sHtml += "      style = 'padding-top:5px; width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
        sHtml += "      background-color:transparent' > ";
        sHtml += "  <tr>";
        sHtml += "      <td>";
        sHtml += "          No responda a este mensaje ya que ha sido generado automáticamente para su información.";
        sHtml += "      </td >";
        sHtml += "  </tr>";
        sHtml += " </table>";

        sHtml += "</div>";
        sHtml += "</body>";
        //sHtml += "</ html >";











    }


    string formato2() {

        string html = "";
        html = " <table border='0' cellpadding='0' cellspacing='0'  style='width:660px; margin:0px; font-size:12px; font-family:Eurostile; position:static;'>";
        html += "   <tr align='center'  style='width:660px; color:#1C6393; font-size:13px;'>";
        html += "       <td style='width: 10px; position: static; height: 8px;'> </td>";
        html += "       <td style=' border-bottom: 1px solid #96a4c3; position: static; height: 8px;' colspan='2' >";
        html += "           <div style = ' text-align:left; font-size:30px; color:Gray; font-weight: bold; float:left; width:580px; height: 39px;'>";
        html += "ptro_Asunto";
        html += "           </div>";
        html += "       </td>";
        html += "       <td style='width: 10px; position: static; height: 8px;'> </td>";
        html += "   </tr>";

        html += "   <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:25px;'>";
        html += "       <td style=' width:10px; position:static; '>&nbsp;</td>";
        html += "       <td style=' text-align:left; color:#1C6393; font-weight: bold; position:static; font-size:25px;' colspan='2'>";
        html += " ptro_Asunto_detalle";
        html += "       </td>";
        html += "       <td style=' width:10px; position:static;'> &nbsp;</td>";
        html += "  </tr>";

        html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px'>";
        html += "      <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td>";
        html += "      <td style='position: static;width: 340px;'> &nbsp;</td>";
        html += "      <td style=' width:340px; position:static; font-weight: bold;''>&nbsp;</td>";
        html += "      <td style=' width:10px; position:static;'>&nbsp;</td>";
        html += "  </tr>";

        html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px'>";
        html += "      <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td>";
        html += "      <td style='position: static;' colspan='2'>&nbsp;</td>";
        html += "      <td style=' width:10px; position:static;'>&nbsp;</td>";
        html += "  </tr>";

        html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px;'>";
        html += "      <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td>";
        html += "      <td  style=' background-color:#EBEBEB; text-align:left;  border-radius: 5px; border:  1px solid #96a4c3; position:static; width: 340px;'>";
        html += "           <div style='color:#1C6393; font-weight: bold; font-size:13px;'> &nbsp;Emitido por:</div>    ";
        html += "           <div> &nbsp;&nbsp; " + "MyUsrDesEmision" + " <br /></div>";
        html += "      </td>";
        html += "      <td style=' width:340px; position:static; font-weight: bold;'>&nbsp;</td>";
        html += "      <td style=' width:10px; position:static;'> &nbsp;</td>";
        html += "  </tr>";

        html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px'>";
        html += "     <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td>";
        html += "     <td  style=' text-align:left;' >&nbsp;</td>";
        html += "     <td style=' width:340px; position:static; font-weight: bold;''>&nbsp;</td>";
        html += "     <td style=' width:10px; position:static;'>&nbsp;</td>";
        html += "  </tr>";
        html += "</table>";

        html += "<table border='0' cellpadding='0' cellspacing='0'  style='width:665px; margin:0px; font-size:12px; font-family:Eurostile; position:static;'>";
        html += "  <tr align='center'  style='width:600px;  margin:0px;  font-size:13px;'>";
        html += "      <td style=' width:5px; position:static; font-weight: bold;'></td>";
        html += "      <td  style=' width:600px; text-align:left;  border-radius: 5px; border:  1px solid #96a4c3;background-color:#EBEBEB; position:static; color:#1C6393; width: 600px;'> ";
        html += "         <div style='color:#1C6393; font-weight: bold; font-size:13px;'>  ";
        html += "            " + "ptro_Motivo" + "</br>";
        html += "            <a href='" + "ptro_URL_Login" + "'>" + "ptro_URL_Login_Etiqueta" + "</a></br>";
        html += "            El proceso requiere que la autorizaci—n se otorgue dentro de los siguientes 5 d’as h‡biles</br>";
        html += "         </div>";
        html += "     </td>";
        html += "  </tr>";
        html += "</table>";

        //html += "<table border='0' cellpadding='0' cellspacing='0'  style='width:665px; margin:0px; font-size:12px; font-family:Eurostile; position:static;'>"
        //html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px'>"
        //html += "      <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td>"
        //html += "      <td  style=' text-align:left;' >&nbsp;</td>"
        //html += "      <td style=' width:340px; position:static; font-weight: bold;''>&nbsp;</td>"
        //html += "      <td style=' width:10px; position:static;'>&nbsp;</td>"
        //html += "  </tr>"

        //html += "  <tr  style='width:700px;  margin:0px; color:#1C6393; font-size:13px'>"
        //html += "     <td style=' width:10px; position:static; '> &nbsp;</td>"
        //html += "     <td align='center' style=' text-align:left;  border-radius: 5px; background-color:#EBEBEB; border-radius: 5px; border:  1px solid #96a4c3; position:static;' colspan='2'>"

        //html += "         <table  border='0'  cellpadding='0' cellspacing='0'  style='  vertical-align:top; text-align:left; width:660px;  font-size:12px; font-family:Eurostile; position:static;'>"
        //html += "	            <tr style='width:660px; color:#1C6393; font-size:13px;'>"
        //html += "                   <td align='center' style=' width:330px; height:100px;  position:static;  vertical-align: top; font-size:13px;'>"
        //html += "                               <div style='  text-align:left;color:#1C6393; font-weight: bold; width: 313px;'> <br /> &nbsp;&nbsp;Compania:</div>    "
        //html += "                               <div style='  text-align:left; color:#1C6393; font-weight: normal; width: 313px;'>     &nbsp;&nbsp;  " & myCompania & "  </div>&nbsp;"
        //html += "                               <div style='  text-align:left; color:#1C6393; font-weight: bold; width: 313px;'>       <br />&nbsp;&nbsp;Detalle:</div>    "
        //html += "                                <div style='  text-align:left;  color:#1C6393; font-weight: normal; width: 313px;'>    &nbsp;" & DiasSol & " dia(s) <br /> &nbsp; " & myFecha & " </div>&nbsp;"
        //'html += "                                <div style='  text - align:left; color:#1C6393; font-weight: normal; width: 313px;'>    &nbsp; " & myFecha & " </div>&nbsp;"
        //html += "                                <div style=' text-align:left; color:#1C6393; font-weight: bold; width: 313px;'>        &nbsp; Motivo:</div>    "
        //html += "                                <div style='  text-align:left; color:#1C6393; font-weight: normal; width: 313px;'>     &nbsp; " & MotivoRechazo & " </div>    "
        //html += "                        <br />"
        //html += "                    </td>"
        //html += "                    <td align='center' style=' background-color:#EBEBEB; width:330px; height:100px; position:static;  vertical-align: top;'>"
        //html += "                        &nbsp;<div style='color:#1C6393; font-weight: bold; width: 307px; text-align:left;'> &nbsp; <br />"
        //html += "                        <div style='color:#1C6393;  width: 307px; text-align:left; font-weight:normal;'> &nbsp; </div>    "
        //html += "                        </div>    "
        //html += "                    </td>"
        //html += "                </tr>"
        //html += "          </table>"
        //html += "     </td>"
        //html += "     <td style=' width:10px; position:static;'>&nbsp;</td>"
        //html += "  </tr>"

        //html += "  <tr align='center'  style='width:700px;  margin:0px; color:#1C6393; font-size:13px;'>"
        //html += "      <td style=' width:10px; position:static; font-weight: bold;'>&nbsp;</td> <td   > &nbsp;</td>"
        //html += "      <td style=' width:340px; position:static; font-weight: bold;''>&nbsp;</td>"
        //html += "      <td style=' width:10px; position:static;'>&nbsp;</td>"
        //html += "  </tr>"
        //html += "</table>"

        //html += "<table  border='0'  cellpadding='0' cellspacing='0'  style='  vertical-align:top; text-align:left; width:665px;  font-size:12px; font-family:Eurostile; position:static;'>"
        //html += "  <tr align='center'  style='width: 660px;  margin:0px; font-size:13px'>"
        //html += "     <td style=' width:3px; position:static; font-weight: bold;'></td>"
        //html += "     <td  style=' width:600px; text-align:left;  border-radius: 5px; border:  1px solid #96a4c3;background-color:#EBEBEB; position:static; color:#1C6393; width: 600px;'> "
        //html += "        <div style='color:#000000; font-weight: bold; font-size:14px; text-align:justify;'>  " & ptro_general & "</br></div>"
        //html += "     </td>"
        //html += "  </tr>"
        //html += "</table>";

        return html;
    }
}