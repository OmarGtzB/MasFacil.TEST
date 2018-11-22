<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttProvDatosGenerales.aspx.cs" Inherits="DC_MttProvDatosGenerales" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />

    <style>
        @font-face { font-family: 'Standar'; src: url('FutuBk__.ttf'); }
@font-face { font-family: 'Standar'; src: url('FutuBk__.eot'); }


        .demo-container .RadTabStrip.rtsHorizontal .rtsLevel .rtsLink {
            text-align: center;
            font-family:'Standar';
        }
 
        .demo-container .RadTabStrip.rtsVertical .rtsLevel1 .rtsSelected .rtsLink {
            background-color: #cccccc;
                    font-family:'Standar';
        }
 
        .demo-container .RadTabStrip .rtsImg,
        .demo-container .RadTabStripVertical .rtsImg {
            border: 0 none;
            margin-left: -2px;
            margin-top: 7px;
            vertical-align: middle;
                    font-family:'Standar';
        }
 
        .demo-container .RadRating {
            margin-bottom: 22px;
            position: relative;
            top: 15px;
            *top: -3px;
                    font-family:'Standar';
        }

        .demo-container .RadTabStrip .Tab .RadTab{
            width: 30px;
            font-family:'Standar';
        }

    </style>

</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <div>

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
       <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>


    <fieldset   >   
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtClave" runat="server"  Width="120px"  MaxLength="10"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Condición de Pago"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboCondicionesPago" runat="server"  AutoPostBack="false"  
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="220px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 80px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 180px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 80px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "conPagCve") %>
                                                    </td>
                                                    <td style="width: 180px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "conPagDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Nombre"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rtxtNombre" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="Tipo"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">    
                        
                                <telerik:RadComboBox ID="rCboTipo" runat="server"  AutoPostBack="false"  
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="220px" Height="200px">

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                                                 
                    </td>
                </tr>
                <tr>
                     <td style=" width:130px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel16" runat="server" Text="RFC/ID"></telerik:RadLabel>  
                    </td>
                    <td>
                        <telerik:RadTextBox ID="rtxtRFC" runat="server" MaxLength="20"  Width="220px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>

            </table>           

        </div>
    </fieldset>

    <fieldset style="  margin-top:5px;   display: block; text-align:left;" >  
            <legend>Otros Datos</legend>                 
            <div style="width:100%;   height:32px; overflow:auto; background-color:transparent;" >
                <asp:DataList ID="DataListOtrosDatos" runat="server" Enabled="false"  DataKeyField="otroDatCve">
                    <ItemTemplate>
                        <table border="0" style=" width:100%; text-align:left; background-color:transparent;">
                            <tr>
                                <td style=" width:135px;  background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("otroDatDes") %>'  ></telerik:RadLabel>

                                </td>
                                <td style="  width:180px; background-color:transparent;">
                                <telerik:RadTextBox ID="txt_OtrosDatos" runat="server" Width="220px"  
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"  
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused" Text='<%# Eval("OtroDatVal") %>'
                                    InvalidStyle-CssClass="cssTxtInvalid">
                                </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </fieldset>
    
    <div style="margin-top:10px;">
        <telerik:RadTabStrip RenderMode="Lightweight" Orientation="HorizontalTop" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
            <Tabs>
                <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Fiscal" Value="1"></telerik:RadTab>
                <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Notificaciones" Font-Size="Medium" Value="2"></telerik:RadTab>
                <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Expedición" Font-Size="Medium" Value="3"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip> 
    </div>
         
    <fieldset id="fldDirecciones" runat="server" style="background-color:gainsboro;">   
        
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage" >
            <telerik:RadPageView runat="server" ID="RadPageView1">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPais" runat="server" OnSelectedIndexChanged="rCboPais_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel6" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFed" runat="server" OnSelectedIndexChanged="rCboEntidadFed_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel7" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvincia" runat="server" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel8" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="rTxtColonia" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel9" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCalle" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel11" runat="server" Text="No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                        <%--<telerik:RadNumericTextBox ID="rTxtNoExt" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtNoExt" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel10" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                       <%-- <telerik:RadNumericTextBox ID="rTxtNoInt" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtNoInt" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel12" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCallesAledanas" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel13" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rTxtCodigoPostal"  Width="60px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtCodigoPostal" runat="server"  Width="60px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel14" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtReferencia" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  


            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel15" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="rTxtTelefono1" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel19" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="rTxtTelefono2" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>

            </table>           
             <table runat="server" visible ="false"  border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel20" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="rTxtFax" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                             <telerik:RadTextBox ID="iddom" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                    </td>
                </tr>

             </table>     

        </div>

                        </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="RadPageView2">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel17" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboPaisNoti" runat="server" OnSelectedIndexChanged="rCboPaisNoti_SelectedIndexChanged" AutoPostBack="True"  
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel18" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFedNoti" runat="server" OnSelectedIndexChanged="rCboEntidadFedNoti_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"   AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel21" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvinciaNoti" runat="server" AutoPostBack="True"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  AllowCustomText="true"
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel22" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="rTxtColoniaNoti" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel23" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCalleNoti" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel24" runat="server" Text="No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                       <%-- <telerik:RadNumericTextBox ID="rTxtNoExtNoti" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                         <telerik:RadTextBox ID="rTxtNoExtNoti" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel25" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                      <%--  <telerik:RadNumericTextBox ID="rTxtNoIntNoti" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtNoIntNoti" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel26" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCallesAledanasNoti" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel27" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rTxtCodigoPostalNoti"  Width="60px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtCodigoPostalNoti" runat="server"  Width="60px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel28" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtReferenciaNoti" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel29" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="rTxtTelefono1Noti" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel30" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="rTxtTelefono2Noti" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>


            </table>           
             <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel31" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="rTxtFaxNoti" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                     <telerik:RadTextBox ID="iddomNotif" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>        
                    </td>
                </tr>
            </table>  
        </div>

                        </telerik:RadPageView>

            <telerik:RadPageView runat="server" ID="RadPageView3">
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel32" runat="server" Text="País"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadComboBox ID="rCboPaisExp" runat="server" OnSelectedIndexChanged="rCboPaisExp_SelectedIndexChanged" AutoPostBack="True"  
                                                HighlightTemplatedItems="true"
                                                DropDownCssClass="cssRadComboBox"  AllowCustomText="true"
                                                Width="220px" DropDownWidth="290px" Height="200px">
                            <FooterTemplate>
                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                            </FooterTemplate>
                            </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel33" runat="server" Text="Estado"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboEntidadFedExp" runat="server" OnSelectedIndexChanged="rCboEntidadFedExp_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>                    

                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel34" runat="server" Text="Del/Muni"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboProvinciaExp" runat="server" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="220px" DropDownWidth="290px" Height="200px">
                                    <HeaderTemplate>
                                        <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 260px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel35" runat="server" Text="Colonia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                        <telerik:RadTextBox ID="rTxtColoniaExp" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>                              
                    </td>
                </tr>

            </table>           

            <table border="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel36" runat="server" Text="Calle"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCalleExp" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel37" runat="server" Text="No Exterior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px; background-color:transparent;">
                        <%--<telerik:RadNumericTextBox ID="rTxtNoExtExp" runat="server" Width="60px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                         <telerik:RadTextBox ID="rTxtNoExtExp" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel38" runat="server" Text="No Interior"></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rTxtNoIntExp" runat="server"  Width="60px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                           <telerik:RadTextBox ID="rTxtNoIntExp" runat="server"  Width="60px"  MaxLength="30"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel39" runat="server" Text="Calles Aledañas"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtCallesAledanasExp" runat="server"  Width="220px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel40" runat="server" Text="Codigo Postal" ></telerik:RadLabel>  
                    </td>
                    <td style=" width:70px;  background-color:transparent;">                             
                        <%--<telerik:RadNumericTextBox ID="rTxtCodigoPostalExp"  Width="60px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                        <telerik:RadTextBox ID="rTxtCodigoPostalExp" runat="server"  Width="60px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:70px; background-color:transparent;">

                    </td>
                </tr>
            </table>           

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel41" runat="server" Text="Referencia"></telerik:RadLabel>  
                    </td>
                    <td style=" width:590px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtReferenciaExp" runat="server"  Width="580px" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                </tr>
            </table>  

            <table border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel42" runat="server" Text="Teléfono 1"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                            <telerik:RadNumericTextBox ID="rTxtTelefono1Exp" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel43" runat="server" Text="Teléfono 2"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                              <telerik:RadNumericTextBox ID="rTxtTelefono2Exp" runat="server"  Width="220px"  NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>              

                    </td>
                </tr>


            </table>           
            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">
                <tr style="height:25px;">
                    <td style=" width:130px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel44" runat="server" Text="Fax"></telerik:RadLabel>  
                    </td>
                    <td style=" width:230px;  background-color:transparent;">                             
                             <telerik:RadNumericTextBox ID="rTxtFaxExp" runat="server"  Width="220px" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                    </td>
                    <td style=" width:130px; background-color:transparent;">

                    </td>
                    <td style=" width:230px;  background-color:transparent;"> 
                            <telerik:RadTextBox ID="iddomExped" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox> 
                    </td>
                </tr>
            </table> 
        </div>

                        </telerik:RadPageView>

        </telerik:RadMultiPage>

        
    </fieldset>

    <table>
        <tr>
            <td>
                <fieldset  runat="server" style=" float:left; width:354px;">
                <legend>Referencias</legend>
                    <div style="overflow:auto; width:370px; height:80px; " >
                        <asp:DataList ID="dt_referenciasProv" runat="server" DataKeyField="revasec">
                            <ItemTemplate>
                                <table border="0"  cellpadding="0" cellspacing="0" style=" width:350px; text-align:left; background-color:transparent ;">
                                    <tr  >
                                        <td style=" width:120px; background-color:transparent;">
                                            <telerik:RadLabel ID="lbrad" runat="server" Text='<%# Eval("revaDes") %>' Width="100px" ></telerik:RadLabel>
                                        </tb>
                                        <td style=" background-color:transparent; ">  
                                            <telerik:RadTextBox ID="txt_ref_Ref" MaxLength="15"  runat="server" Width="220px"  Text='<%# Eval("revaValRef") %>' 
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid">
                                            </telerik:RadTextBox>
                                        </tb>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </fieldset>  
            </td>
            <td style="float:right;">
                <fieldset  runat="server" style="  width:348px; ">
                    <legend>Variables</legend>
                        <div  style="overflow:auto; width:355px; height:80px;  " >
                            <asp:DataList ID="dt_variablesProv" runat="server" DataKeyField="revasec" >
                                <ItemTemplate>
                                    <table border="0"  cellpadding="0" cellspacing="0" style=" width:350px; background-color:transparent ;">
                                        <tr>
                                            <%--  <td style=" width:100px;  text-align:left; border:3px; background-color:red;">--%>
                                            <td style=" width:110px; background-color:transparent;">
                                                <telerik:RadLabel ID="lbrad" runat="server" Text=' <%# Eval("revaDes") %>' Width="100px" ></telerik:RadLabel>
                                            </tb>
                                            <td style=" width:230px;  background-color:transparent;">  
                                                <telerik:RadNumericTextBox ID="txt_ref_Prov" MaxLength="15"  runat="server" Width="220px" Text='<%# Eval("revaValVar") %>'
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                </telerik:RadNumericTextBox>
                                            </tb>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                </fieldset>
            </td>
        </tr>
    </table>


        <telerik:RadWindowManager ID="RadWindowManager" runat="server"></telerik:RadWindowManager>

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />   
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
          
    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
 
        
        
   </form>
</body>
</html>
