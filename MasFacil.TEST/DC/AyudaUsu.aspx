<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AyudaUsu.aspx.cs" Inherits="DC_AyudaUsu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssMPForm.css" rel="stylesheet" type="text/css" />


    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    
        
    <style type="text/css">
        
        #example {
    text-align: center;
}
 
#example .demo-container {
    display: inline-block;
    text-align: left;
    padding: 50px 40px 35px 40px;
}
 
.demo-container .RadTabStrip {
    margin-left: 8px;
}
 
.demo-container .RadTabStrip .rtsLevel1 {
    border: none;
}
 
.demo-container .RadTabStrip .tab {
    background-color: #9ab5c1;
    width: 80px;
    height: 70px;
    border-right: 1px solid #FFF;
    text-align: center;
    vertical-align: middle;
}
 
.demo-container .RadTabStrip .tab.overviewTab {
    border-top-left-radius: 5px;
}
 
.demo-container .RadTabStrip .tab.priceTab {
    background-color: #6991a3;
    border-top-right-radius: 5px;
}
 
.demo-container .RadTabStrip .tab.selectedTab {
    background-color: #f77462;
}
 
.demo-container .RadTabStrip .tab.hoveredTab {
    background-color: #81a3b2;
}
 
.demo-container .RadTabStrip .tab.hoveredTab.selectedTab {
    background-color: #f77462;
}
 
.demo-container .RadTabStrip .tab .rtsLink {
    border: none;
    padding: 12px 0 0 0;
}
 
.demo-container .RadTabStrip .tab .rtsTxt {
    color: white;
    font-family: 'Segoe UI';
    font-size: 14px;
}
 
    .demo-container .RadTabStrip .tab .rtsTxt:before {
        display: block;
        height: 22px;
        width: 22px;
        border-radius: 15px;
        background-color: #FFF;
        color: #f77462;
        /*font-family: TelerikWebUI;*/
        font-size: 16px;
        text-align: center;
        margin: 0 auto 1px auto;
        padding: 4px;
    }
 
    .demo-container .RadTabStrip .tab.overviewTab .rtsTxt:before {
        content: "\e0f9";
    }
 
    .demo-container .RadTabStrip .tab.attractionsTab .rtsTxt:before {
        content: "\e07c";
    }
 
    .demo-container .RadTabStrip .tab.locationTab .rtsTxt:before {
        content: "\e125";
    }
 
    .demo-container .RadTabStrip .tab.calendarTab .rtsTxt:before {
        content: "\e089";
    }
 
    .demo-container .RadTabStrip .tab.priceTab .rtsTxt:before {
        content: "$";
        /*font-family: 'Futura Bk BT';*/
        font-size: 20px;
        font-weight: 700;
        padding-top: 4px;
        height: 28px;
        width: 28px;
        padding: 1px;
    }

       .fnt{
        content: "$";
        /*font-family: 'Futura Bk BT';*/
        font-size: 20px;
        font-weight: 700;
        padding-top: 4px;
        height: 28px;
        width: 28px;
        padding: 1px;
    }
       
    </style>
  
        
</head>
<body   style="   text-align:left;   background-color:transparent;  margin-left:15px; height:200px; " onclose="myFunction()"   >
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" BackColor="White" Transparency="30"></telerik:RadAjaxLoadingPanel>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >
            <AjaxSettings>

                             <telerik:AjaxSetting AjaxControlID="rBtnSincronizarAyuda">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                <telerik:AjaxSetting AjaxControlID="rBtnBuscar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"  ></telerik:RadAjaxLoadingPanel>


        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>

            <div id="divGral">
            
            <asp:Panel ID="pnlBody" runat="server">

            <br />        
            <asp:Panel ID="pnl" runat="server">
                <telerik:RadTextBox ID="rtxtFiltro" runat="server" Width="450px" MaxLength="50"
                EnabledStyle-CssClass="cssTxtEnabled"
                DisabledStyle-CssClass ="cssTxtEnabled"
                HoveredStyle-CssClass="cssTxtHovered"
                FocusedStyle-CssClass="cssTxtFocused"
                InvalidStyle-CssClass="cssTxtInvalid" ></telerik:RadTextBox>
                <telerik:RadImageButton  ID="rBtnBuscar"  runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrL="~/Imagenes/icoAyuda/Buscar.png" Image-Url="~/Imagenes/icoAyuda/Buscar.png" Image-HoveredUrl="~/Imagenes/icoAyuda/Buscar.png"    ToolTip="Buscar"  Text ="" OnClick="rBtnBuscar_Click" AutoPostBack="true"  Enabled="true" ></telerik:RadImageButton>
            </asp:Panel>

               
                <table>
                    <tr>
                        <td>
                            <div  style=" background-color:transparent; width:660px;" >
                                <telerik:RadTabStrip  Width="745px"  RenderMode="Lightweight" Orientation="HorizontalTop" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1"      > 
                                    <Tabs>
                                        <telerik:RadTab  Text="PDF" Font-Size="Larger" ImageUrl="~/Imagenes/icoAyuda/pdf.png"  Value="1" Height="15px"    ForeColor="#5E79AF" ></telerik:RadTab>
                                        <telerik:RadTab  Text="Imagenes" Font-Size="Larger" ImageUrl="~/Imagenes/icoAyuda/imagen.png" Value="2" Height="15px"   ForeColor="#5E79AF"     ></telerik:RadTab>
                                        <telerik:RadTab  Text="Video" Font-Size="Larger" ImageUrl="~/Imagenes/icoAyuda/video.png"   Value="3" Height="15px"    ForeColor="#5E79AF"    ></telerik:RadTab>   
                                    </Tabs>
                                </telerik:RadTabStrip>
                            </div>
                        </td>
                        <td style="background-color:transparent;">
                           <telerik:RadImageButton  ID="rdbtnAtras"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"  ToolTip="Buscar"  Text =""  AutoPostBack="true"  Enabled="true" Visible="false" ></telerik:RadImageButton>
                        </td>
                    </tr>
                </table>
                
            <div style="background-color:transparent; width:99%;">
                    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="outerMultiPage"   Width="750px" >
                        <%--PDF--%>
                        <telerik:RadPageView runat="server" ID="RadPagePdf" >
                       <%-- DIV PORTADA MR.CORBATIN--%>
                            <iframe id="iframeReport" style="width: 755px; height:380px;" runat="server" src="" visible="false"></iframe>
                            <table style=" text-align:right; background-color:transparent; "  >
                                <tr style=" width:99%; background-color:transparent; ">
                                    <td style=" width:99%; background-color:transparent; " >
                                        <telerik:RadLabel ID="RadLabel5" runat="server"  BackColor="Transparent"  Text="z" Width="560px"  ForeColor="Transparent"  ></telerik:RadLabel>
                                        <telerik:RadLabel ID="RlblNumRegPdf" runat="server"  ForeColor="Gray" Font-Size="Small"   Width="170px"  ></telerik:RadLabel>
                                    </td>
                                </tr>
                            </table>
                            <asp:DataList ID="DataListPDF"  runat="server" DataKeyField="maAyuCve" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                       <fieldset style="color:darkgray; border-color:darkgray;">
                                            <table border="0" style="width: 725px; text-align: left; background-color:Transparent;" >
                                                <tr>
                                                    <td>
                                                        <telerik:RadButton  runat="server" Font-Names="Standar" ID="rBtnMostrar" ForeColor="Blue" Skin="Metro" BorderColor="Transparent" BackColor="Transparent"  Font-Size="12"  Enabled="true"
                                                            Text='<%# Eval("maAyuDes") %>' 
                                                            Value='<%# Eval("maAyuCve") %>'
                                                            OnClick="rBtnMostrarPDF_Click">
                                                        </telerik:RadButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style=" padding-left:20px">
                                                        <telerik:RadLabel ID="RadLabel1" runat="server" Text='<%# Eval("maAyuDesExt") %>' Width="700px" style="TEXT-ALIGN:justify"  ForeColor="Gray" Font-Size="Small"></telerik:RadLabel>
                                                    </td>
                                                </tr>
                                                <tr style="background-color:red; height:8px;"></tr>
                                            </table>
                                         </fieldset>
                                    </ItemTemplate>
                                </asp:DataList>
                        </telerik:RadPageView>
            
                        <%-- IMAGENES--%>
                        <telerik:RadPageView  runat="server" ID="RadPageImg">
                        <div id="IMG_DIV" runat="server" style="background-color:transparent; ">
                            <table style="background-color:transparent; text-align:right;">
                                <tr style="background-color:transparent;">
                                    <td style="background-color:transparent;  ">
                                        <telerik:RadLabel ID="RadLabel6" runat="server"  BackColor="Transparent"  Text="." Width="560px"  ForeColor="Transparent"  ></telerik:RadLabel>
                                        <telerik:RadLabel ID="RlblNumRegImagenes" runat="server"  ForeColor="Gray" Font-Size="Small"  BackColor="Transparent" Width="170px" ></telerik:RadLabel>
                                    </td>
                                </tr>
                            </table>

                                <asp:DataList ID="DataListImagenes" runat="server" DataKeyField="maAyuCve">
                                    <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style=" width:40px;" >
                                            </td>
                                            <td>
                                                <telerik:RadImageButton  ID="RdBnryImaagenes" Font-Names="Standar"  
                                                    runat ="server" Width="200px" Height="100px" 
                                                    Image-Sizing="Stretch"   
                                                    OnClick="rBtnMostrarIMG_Click" 
                                                    Value='<%# Eval("maAyuCve") %>' 
                                                    Image-Url='<%# Eval("maAyuPath") %>' 
                                                    AutoPostBack="true" Enabled="true">
                                                </telerik:RadImageButton>
                                                <telerik:RadLabel  ID="rlblDescrip" runat="server" Text='<%# Eval("maAyuDes") %>' ForeColor="#084d71"  Width="220px" BackColor="transparent" ></telerik:RadLabel>
                                                <br />
                                                <telerik:RadLabel ID="rlblDescripExt" runat="server" Text='<%# Eval("maAyuDesExt") %>' style="TEXT-ALIGN:justify" BackColor="transparent"  ForeColor="Gray" Font-Size="Small"  Height="60px"  Width="220px"  ></telerik:RadLabel>
                                            </td>
                                        </tr>
                                    </table>



                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                            <%--DIV HACER GRANDE LAS IMAGENES Y OCULTARLO--%>
                          <div id="DivImg" runat="server" style="background-color:transparent; " visible="false">
                           <telerik:RadImageButton  ID="RdMimg"  runat ="server" Width="750px" Height="400px" Image-Sizing="Stretch"   AutoPostBack="true" Enabled="true"  OnClick="rBtnOcultarIMG_Click"  ></telerik:RadImageButton>
                           </div>
                        </telerik:RadPageView>

                        <%--VIDEOS--%>
                        <telerik:RadPageView runat="server" ID="RadPageVideo" >
                            <table style="text-align:right;">
                                <tr>
                                    <td>
                                        <telerik:RadLabel ID="RadLabel7" runat="server"  BackColor="Transparent"  Text="z" Width="560px"  ForeColor="Transparent"  ></telerik:RadLabel>
                                        <telerik:RadLabel ID="RlblNumRegVideo" runat="server"  ForeColor="Gray" Font-Size="Small"  BackColor="Transparent" Width="170px" ></telerik:RadLabel>
                                    </td>
                                </tr>
                            </table>

                            <asp:DataList ID="DataListVideos" runat="server" DataKeyField="maAyuCve">
                                <ItemTemplate>
                                    <div class="demo-container" style="background-color:Transparent;  width:250px;">


                                         <telerik:RadMediaPlayer RenderMode="Lightweight" ID="RdMdiaVideos" runat="server" AutoPlay="false" MimeType="video/mp4"  HDActive="false"  Source='<%# Eval("maAyuPath") %>'
                                            Height="110px" ToolbarDocked Width="220px"> 
                                            


                                        </telerik:RadMediaPlayer>


                                        <telerik:RadLabel ID="RadLabel4" runat="server" Text='<%# Eval("maAyuDes") %>' ForeColor="#084d71"  Width="220px"></telerik:RadLabel>
                                        <br /><telerik:RadLabel  ID="RadLabel2" runat="server" Text='<%# Eval("maAyuDesExt") %>' style="TEXT-ALIGN:justify"  Height="100px"  ForeColor="Gray" Font-Size="Small"   Width="218px" ></telerik:RadLabel>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>

                <%--PORTADA--%>
                 <div runat="server" id="divPortada" >
                        <table>
                            <tr>
                                <td style="background-color:transparent; width:75px;"></td>
                                <td style="background-color:Transparent;">
                                    <telerik:RadBinaryImage ID="RdBnryCorbatin" runat="server" Height="315px" ImageAlign="Left" ImageUrl="~/Imagenes/icoAyuda/MrCorbatin.png" Width="175px" />
                                </td>
                                <td style="background-color:Transparent; width:40px;"></td>
                                <td style="background-color:transparent; width:430px;">
                                    <telerik:RadLabel ID="RadLabel8" runat="server" BackColor="Transparent" Font-Size="X-Large" ForeColor="Gray" Text="Bienvenido al Centro de Ayuda rápida de MAS+Fácil . " >
                                    </telerik:RadLabel>
                                    <telerik:RadLabel ID="RadLabel11" runat="server" BackColor="Transparent" Font-Size="Smaller" ForeColor="Transparent" Text=".">
                                    </telerik:RadLabel>
                                    <telerik:RadLabel ID="RadLabel9" runat="server" BackColor="Transparent"   Font-Size="Medium" ForeColor="Gray" style="TEXT-ALIGN:justify" Text="¿Necesitas ayuda con nuestra aplicación?. Te proporcionamos un menú de ayuda donde podrás encontrar recursos como PDFs, Imágenes y Videos que te ayudaran a guiarte dentro del sistema de forma rápida.">
                                    </telerik:RadLabel>
                                    <telerik:RadLabel ID="RadLabel10" runat="server" BackColor="Transparent" Font-Size="Smaller" ForeColor="Transparent" Height="50px" Text=".">
                                    </telerik:RadLabel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="valHftBuscarPDF"   runat="server" />
                    <asp:HiddenField ID="valHftBuscarIMG"   runat="server" />
                    <asp:HiddenField ID="valHftBuscarVID"   runat="server" />
                    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
                    </telerik:RadWindowManager>




                    <telerik:RadWindow runat="server"    ID="rWinActualizaciones"  Width="285px" Height="140px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Title="Actualizaciones"  Behaviors="Close" >
                    <ContentTemplate>
                        <div>
                            <div>
                                <fieldset runat="server">
                                    <div style ="padding: 5px 5px 5px 5px;">
                                        <asp:Label ID="lblActualizaciones" runat="server" Text="Existen actualizaciones de ayuda. ¿Desea sinconizar con la Central?"></asp:Label>
                                    </div>
                                </fieldset>
                                <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >
                                    <telerik:RadImageButton ID="rBtnSincronizarAyuda"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""     OnClick="rBtnSincronizarAyuda_Click"   ></telerik:RadImageButton>
                                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png" ToolTip="Cancelar"  Text=""  ></telerik:RadImageButton>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                 </telerik:RadWindow>


            </asp:Panel>
            </div>

            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

    </form>
</body>
</html>
