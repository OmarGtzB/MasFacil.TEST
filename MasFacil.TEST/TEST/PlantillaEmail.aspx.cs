using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_PlantillaEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void formato() {
        
        string sHtml = "";

        sHtml += "< html xmlns = 'http://www.w3.org/1999/xhtml' >";
        sHtml += "< head runat = 'server' >";
        sHtml += " < title ></ title >";
        sHtml += " < style type = 'text/css' >";
        sHtml += " </ style >";
        sHtml += "</ head >";
        
        sHtml += "< body  style = 'width:100%; background-color:#F5F5F5;' >";
        sHtml += "< div align = 'center' style = 'width:100%' >";

        sHtml += "  < table border = '0' cellpadding = '0' cellspacing = '0'";
        sHtml += "    style = 'width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central; ";
        sHtml += "            border - radius: 5px; border: 1px solid #355C82; background-color:white' >";
        sHtml += "  < tr >";
        sHtml += "    < td >";
        sHtml += "      < div align = 'center' style = 'width:100%; margin-top:10px; ' >";
        sHtml += "          < table border = '0' cellpadding = '0' cellspacing = '0'";
        sHtml += "                  style = '  width:600px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static;";
        sHtml += "                  background - color:transparent ' >";
        sHtml += "          < tr >";
        sHtml += "              < td style = 'border-bottom: 1px solid #355C82;' >";
        sHtml += "                  < div style = 'text-align:left; font-size:22px; color:Gray; font-weight: bold; float:left; width:580px; height: 25px;' >";
        sHtml += "                      Comprobante Fiscal Digital";
        sHtml += "                  </ div >";
        sHtml += "                  < div style = 'text-align:left; font-size:18px; font-weight: bold; float:left; width:580px; height: 25px;' >";
        sHtml += "                      Folio 59";
        sHtml += "                  </ div >";
        sHtml += "              </ td >";
        sHtml += "          </ tr >";
        sHtml += "          </ table >";
        sHtml += "          < br />";

        sHtml += "          < table border = '0' cellpadding = '0' cellspacing = '0'";
        sHtml += "              style = '  width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
        sHtml += "              background - color:transparent ' >";
        sHtml += "          < tr >";
        sHtml += "              < td style = 'border-bottom: 1px solid #355C82;' >";
        sHtml += "                  < div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Emitido por:";
        sHtml += "                  </ div >";
        sHtml += "                  < div style = ' margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Emmanuel";
        sHtml += "                  </ div >";
        sHtml += "              </ td >";
        sHtml += "          </ tr >";
        sHtml += "          </ table >";
        sHtml += "          < br />";

        sHtml += "          < table border = '0' cellpadding = '0' cellspacing = '0' ";
        sHtml += "              style = '  width:600px; margin:0px; font-family:Arial; color:#355C82; position:static;";
        sHtml += "              background - color:transparent ' >";
        sHtml += "          < tr >";
        sHtml += "              < td >";
        sHtml += "                  < div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Compañia:";
        sHtml += "                  </ div >";
        sHtml += "                  < div style = ' margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Desarrollo";
        sHtml += "                  </ div >";
        sHtml += "              </ td >";
        sHtml += "          </ tr >";
        sHtml += "          < tr >";
        sHtml += "              < td >";
        sHtml += "                  < br />";
        sHtml += "                  < div style = 'text-align:left; font-size:13px; font-weight: bold;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Detalle:";
        sHtml += "                  </ div >";
        sHtml += "                  < div style = ' margin-left:10px;  text-align:left; font-size:12px;  float:left; width:580px; height: 20px;' >";
        sHtml += "                      Se Anexan sus comprobantes Fiscales. ";
        sHtml += "                  </ div >";
        sHtml += "              </ td >";
        sHtml += "          </ tr >";
        sHtml += "          </ table >";
        sHtml += "          < br />";

        sHtml += "      </div>";
        sHtml += "    </ td >";
        sHtml += "   </ tr >";
        sHtml += "  </ table >";

        sHtml += " < table border = '0' cellpadding = '0' cellspacing = '0' ";
        sHtml += "      style = ' padding-top:5px; width:620px; margin:0px; font-size:12px; font-family:Arial; color:#355C82; position:static; vertical-align:central;";
        sHtml += "      background - color:transparent' > ";
        sHtml += "  < tr >";
        sHtml += "      < td >";
        sHtml += "          No responda a este mensaje ya que ha sido generado automáticamente para su información.";
        sHtml += "      </ td >";
        sHtml += "  </ tr >";
        sHtml += " </ table >";

        sHtml += "</ div >";
        sHtml += "</ body >";
        sHtml += "</ html >";

                
    
    







    }


}