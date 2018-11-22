<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="ConsultaAcumulados.aspx.cs" Inherits="FR_ConsultaAcumulados" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
        <link href="../css/styles.css" rel="stylesheet" type="text/css" />   
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

      <script type="text/javascript">
        function closeRadWindow()  
        {  
          $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();
        } 
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
            DefaultLoadingPanelID="RadAjaxLoadingPanel1"  
            OnAjaxRequest="RadAjaxManager1_AjaxRequest" >
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>


        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <table style=" width:100%; background-color:transparent;">
            <tr>
                <td style=" width:50%; text-align:left;  ">
                    <telerik:RadImageButton ID="rBtnExpPDF"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportPDFDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnExportPDF.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportPDFHovered.png"          ToolTip="Exportar a PDF"  Text ="" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnExpXLS"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnExportExcelDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnExportExcel.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnExportExcelHovered.png"          ToolTip="Exportar a Excel"  Text =""  Visible="true" OnClick="rBtnExpXLS_Click" ></telerik:RadImageButton>
                </td>
                <td  style=" width:50%; text-align:right;  ">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
                        <ContentTemplate>
                            <telerik:RadImageButton ID="rBtnConsultaWizard"  runat="server"  AutoPostBack="true" Width="95px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnSeleccion.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnSeleccionHovered.png"  ToolTip="Seleccionar" OnClick="rBtnConsultaWizard_Click"   ></telerik:RadImageButton>                      
                        </ContentTemplate>
                    </asp:UpdatePanel>         
                </td>
            </tr>
            </table>  
        </asp:Panel>   

    <asp:UpdatePanel ID="UpdatePanelMenu" runat="server"  >
        <ContentTemplate>

            <asp:Panel ID="pnlBody" Width="100%" runat="server">
                <telerik:RadPivotGrid id="rPivoGrid" runat="server" Skin="WebBlue" Culture=""
                      AllowSorting="true" AllowFiltering="false" ShowFilterHeaderZone="true"  
                      OnNeedDataSource ="RadPivotGrid1_NeedDataSource"
                      OnPivotGridCellExporting="rPivoGrid_PivotGridCellExporting"
                      Height="430px" Width="1280px" >
                    <ColumnHeaderCellStyle Width="100px"></ColumnHeaderCellStyle>
                    <ClientSettings EnableFieldsDragDrop="true">
                        <Scrolling AllowVerticalScroll="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                    </ClientSettings>
                     
                </telerik:RadPivotGrid> 

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>
                
            <telerik:RadWindow ID="rWinReports" runat="server" OnClientClose="closeRadWindow"    Width="700px" Height="485px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Wizard Reports"  NavigateUrl="~/FR/WizardConsultaAcumulados.aspx" >               
            </telerik:RadWindow>

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel> 



</asp:Content>

