<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="Visor.aspx.cs" Inherits="RPT_Visor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
      <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
      <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
      <link href="../css/styles.css" rel="stylesheet" type="text/css" />
      <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
       

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnExpPDF"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportPDFDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnExportPDF.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportPDFHovered.png"          ToolTip="Exportar a PDF"  Text =""  OnClick="rBtnExpPDF_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnExpXLS"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportExcelDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnExportExcel.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportExcelHovered.png"          ToolTip="Exportar a Excel"  Text =""  OnClick="rBtnExpXLS_Click"></telerik:RadImageButton>
            </asp:Panel>     


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:500px; background-color:transparent; vertical-align:top;">

                         <fieldset style=" height:450px;">
                           
                                  <iframe id="iframeReport"  style=" width:100%; height:100%; " runat="server" src=""  > 
                                  </iframe>

                        </fieldset>

                    </td>
                </tr>
            </table>


 
</asp:Content>

