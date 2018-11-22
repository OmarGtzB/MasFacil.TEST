<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoAlmacenArticulos.aspx.cs" Inherits="DC_MttoAlmacenArticulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssWizard.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            GetRadWindow().close();
        }

    </script>

    <style>
        table, th, td , tr{
            border: 0px;
            padding: 0px;
            margin: 0px;
        }
        .LabelCssClass
        {
            color: transparent;
            background-color:transparent;
           
        }
        .RadInput_Default .riLabel
            {
             background-color:transparent;
             color:transparent;
             border-color:transparent;
             text-emphasis-color:transparent;
             
             
            }
         
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                <fieldset runat="server">
                <legend>Almacén Articulos</legend>
                    <table style="background-color:transparent; width:100%;  text-align:left; " >
                            <tr>
                                <td  style=" width:40px; background-color:transparent; ">
                                     <telerik:RadTextBox  runat="server" ID="radTxtBoxBusqueda" Width="230px" 
                                      EnabledStyle-CssClass="cssTxtEnabled" 
                                        DisabledStyle-CssClass ="cssTxtEnabled" 
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid" MaxLength="20" ></telerik:RadTextBox>
                                   
                                    <telerik:RadImageButton Font-Names="Futura Bk BT"  ID="rBtnBuscar"  runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrL="~/Imagenes/icoAyuda/Buscar.png" Image-Url="~/Imagenes/icoAyuda/Buscar.png" Image-HoveredUrl="~/Imagenes/icoAyuda/Buscar.png"    ToolTip="Buscar"  Text =""  AutoPostBack="true"  Enabled="true" OnClick="rBtnBuscar_Click" ></telerik:RadImageButton>
                             
                                </td>
                                <td  style=" width:100px; background-color:transparent; ">
                                    <asp:RadioButton ID="RdioBtnRelacionados" runat="server" GroupName="ArtAlm" Text="Relacionados" OnCheckedChanged="RdioBtnRelacionados_CheckedChanged"  AutoPostBack="true"/>
                                </td>
                                <td  style=" width:120px; background-color:transparent;">
                                    <asp:RadioButton ID="RdioBtnNoRel" runat="server" GroupName="ArtAlm"   Text="No Relacionados" OnCheckedChanged="RdioBtnNoRel_CheckedChanged"  AutoPostBack="true" />
                                </td>
                               <td  style=" width:60px; background-color:transparent; ">
                                   <asp:RadioButton ID="RdioBtnTodos" runat="server" GroupName="ArtAlm"   Text="Todos" OnCheckedChanged="RdioBtnTodos_CheckedChanged" AutoPostBack="true" />
                                </td>
                                 
                            </tr>
                      </table>
                    <table >
                        <tr>
                            <td  style=" width:80px; background-color:transparent;  text-align:left; ">
                                <telerik:RadCheckBox ID="CheckBoxTodos" runat="server" OnClick="CheckBoxTodos_Click" Text="Todos" ></telerik:RadCheckBox>
                            </td>
                            <td  style=" width:280px; background-color:transparent; ">
                            </td>
                            <td  style=" width:80px; background-color:transparent; ">
                                <asp:Label ID="Label2" runat="server" Text="Máximo"></asp:Label>
                            </td>
                           <td  style=" width:80px; background-color:transparent; ">
                                <asp:Label ID="Label3" runat="server" Text="Mínimo"></asp:Label>
                            </td>
                            <td  style=" width:80px; background-color:transparent; ">
                                <asp:Label ID="Label4" runat="server" Text="Reorden"></asp:Label>
                            </td>
                        </tr>
                  </table>


                  <%--DATALIST--%>
                  <div style=" overflow:auto; width:620px; height:360px; background-color:transparent ;" >
                   <table style="border: 1px" >
                  <asp:DataList ID="DataListArt" runat="server" DataKeyField="artCve"     >
                  <ItemTemplate>
                  <tr >
                                <td >
                                    <telerik:RadCheckBox ID="CheckBoxArt" runat="server" OnCheckedChanged="CheckBoxArt_CheckedChanged" Value='<%# Eval("artCve") %>'    ></telerik:RadCheckBox>
                                </td>
                                <td  style=" width:200px; height:1px">
                                    <telerik:RadLabel ID="lblArtCve" runat="server" Text='<%# Eval("artCve") %>'    ></telerik:RadLabel>
                                </td>
                                <td  style=" width:200px; height:1px; ">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("artDes") %>' ></asp:Label>
                                </td>
                                <td >
                                    <telerik:RadNumericTextBox ID="rNumMaximo" runat="server" Width="80px"   
                                        EnabledStyle-CssClass="cssTxtEnabled"  OnTextChanged="rNumMaximo_TextChanged"  
                                        DisabledStyle-CssClass ="cssTxtEnabled" ToolTip='<%# Eval("artCve") %>' 
                                        HoveredStyle-CssClass="cssTxtHovered"   AutoPostBack="true"
                                        FocusedStyle-CssClass="cssTxtFocused"  
                                        InvalidStyle-CssClass="cssTxtInvalid"  >
                                        <NumberFormat DecimalDigits="0"   />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td >
                                    <telerik:RadNumericTextBox ID="rNumMinimo" runat="server" Width="80px"    
                                        EnabledStyle-CssClass="cssTxtEnabled"  OnTextChanged="rNumMinimo_TextChanged"
                                        DisabledStyle-CssClass ="cssTxtEnabled"    ToolTip='<%# Eval("artCve") %>' 
                                        HoveredStyle-CssClass="cssTxtHovered"        AutoPostBack="true"
                                        FocusedStyle-CssClass="cssTxtFocused" 
                                        InvalidStyle-CssClass="cssTxtInvalid"   >
                                        <NumberFormat DecimalDigits="0"  />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td >
                                     <telerik:RadNumericTextBox ID="rNumReorden" runat="server" Width="80px"    
                                        EnabledStyle-CssClass="cssTxtEnabled"  OnTextChanged="rNumReorden_TextChanged"  
                                        DisabledStyle-CssClass ="cssTxtEnabled" ToolTip='<%# Eval("artCve") %>' 
                                        HoveredStyle-CssClass="cssTxtHovered"        AutoPostBack="true"
                                        FocusedStyle-CssClass="cssTxtFocused"  
                                        InvalidStyle-CssClass="cssTxtInvalid"   >
                                        <NumberFormat DecimalDigits="0"  />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                  </ItemTemplate>
                  </asp:DataList>
                  </table>
                  </div>
                 

        
                  
                  <%--DATALIST FIN--%>
                </fieldset>

                   <%--     VENTANA MAX MIN REORDEN--%>
                   <%--     RadWindow              --%>
                    <telerik:RadWindow runat="server"    ID="rdWindowArtAlm"  Width="285px" Height="160px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true"  Behaviors="Close" >
                     <ContentTemplate>
                            <div>
                                <div>
                                    <fieldset runat="server">
                                    <legend>Almacen Articulos</legend>
                                        <table>
                                        <%--<tr>
                                            <td colspan="2">
                                                <telerik:RadLabel ID="lblalmCve" runat="server" ></telerik:RadLabel>
                                                <telerik:RadLabel ID="lblalmDes" runat="server" ></telerik:RadLabel>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                            <telerik:RadLabel ID="RadLabel8" runat="server" Text="Máximo" ></telerik:RadLabel>
                                            </td>
                                            <td>
                                            <telerik:RadLabel ID="RadLabel9" runat="server" Text="Mínimo" ></telerik:RadLabel>
                                            </td>
                                            <td>
                                            <telerik:RadLabel ID="RadLabel10" runat="server" Text="Reorden" ></telerik:RadLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="RadNumericMaximoDef" runat="server" Width="80px"
                                                    EnabledStyle-CssClass="cssTxtEnabled" 
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid" >
                                                      <NumberFormat DecimalDigits="0"  />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="RadNumericMinimoDef" runat="server" Width="80px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                      <NumberFormat DecimalDigits="0"  />
                                                </telerik:RadNumericTextBox> 
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="RadNumericReOrdenDef" runat="server" Width="80px"
                                                    EnabledStyle-CssClass="cssTxtEnabled"
                                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                                    HoveredStyle-CssClass="cssTxtHovered"
                                                    FocusedStyle-CssClass="cssTxtFocused"
                                                    InvalidStyle-CssClass="cssTxtInvalid">
                                                      <NumberFormat DecimalDigits="0"  />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        </table>
                                    </fieldset>
                                   
                                        <asp:Panel ID="Panel1" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" >
                                        <telerik:RadImageButton ID="rBtnGuardarRw"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardarRw_Click"   ></telerik:RadImageButton>
                                        <telerik:RadImageButton ID="rBtnCancelarRw"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png" ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelarRw_Click" Visible="false" ></telerik:RadImageButton>
                                        </asp:Panel>
                                </div>
                            </div>
                     </ContentTemplate>
                 </telerik:RadWindow>
                  <%--     RadWindow   --%>

                 <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" HorizontalAlign="Right">
                    <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png" ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
                 </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
         <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </div>
    </form>
</body>
</html>
