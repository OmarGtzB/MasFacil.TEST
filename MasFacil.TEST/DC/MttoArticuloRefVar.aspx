<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArticuloRefVar.aspx.cs" Inherits="DC_MttoArticuloRefVar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
        <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
        <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
        <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"   ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClientClicking="OnClientClic_ConfirmOK" OnClick="rBtnEliminar_Click" Visible="false"  ></telerik:RadImageButton> 
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton> 
        </asp:Panel>

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Tipo Referencia / Variable</legend>  
            <div style="display:table; background-color:transparent;" class="auto-style3" >
                <table border="0" style=" text-align:left; background-color:transparent;" >
                    <tr>  
                        <td style="  background-color:transparent;"> 
                            <telerik:RadComboBox ID="rCboTipoDato" width="180px" runat="server" HighlightTemplatedItems="true" 
                            DropDownWidth="290px" Height="200px"  OnSelectedIndexChanged="rCboTipoDato_SelectedIndexChanged"
                                DropDownCssClass="cssRadComboBox" AutoPostBack="True" >            
                                    <HeaderTemplate>
                                        <table style="width: 180px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 90px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 90px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                         </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 180px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 90px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "revaCve") %>
                                                    </td>
                                                        <td style="width: 90px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "revaDes") %>
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
                </table>
                       
             </div>
              
           </fieldset>
           <table>
               <tr>
                    <%--REFERENCIAS--%>
                     <td style="width:50%;">
                     <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
                     <legend>Referencias</legend> 
                       <table>
                            <tr>
                                <td>
                                <telerik:RadLabel ID="RadLabel10" runat="server" Text="Secuencia" width="100px" BackColor="transparent"></telerik:RadLabel>
                                </td>
                                <td>
                                     <telerik:RadNumericTextBox ID="txtRefSecuencia" runat="server" 
                                    DisabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" 
                                    HoveredStyle-CssClass="cssTxtHovered"  InvalidStyle-CssClass="cssTxtInvalid" 
                                    MaxLength="1" NumberFormat-DecimalDigits="0"  NumberFormat-GroupSeparator="" 
                                    NumberFormat-GroupSizes="8" width="90px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Descripción" width="100px" BackColor="transparent"></telerik:RadLabel>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtRefDescripcion"  Enabled="false"
                                DisabledStyle-CssClass="cssTxtEnabled" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                FocusedStyle-CssClass="cssTxtFocused" 
                                HoveredStyle-CssClass="cssTxtHovered" 
                                InvalidStyle-CssClass="cssTxtInvalid" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                 <telerik:RadGrid ID="rGdvReferencias" runat="server" AutoGenerateColumns ="false" Width="300px" Height="180px"   CssClass="Grid" OnSelectedIndexChanged="rGdvReferencias_SelectedIndexChanged"  skin="Office2010Silver">

                                    <MasterTableView  DataKeyNames="revasec" AutoGenerateColumns="False" CssClass="GridTable" >
                                     <Columns >
                                            <telerik:GridBoundColumn DataField="revaDes"  HeaderText="Descripción"  HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="revasec"  HeaderText="Secuencia" HeaderStyle-Width="100px" ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" />
                                   </Columns>
                                    <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                                    </MasterTableView>

                                        <HeaderStyle CssClass="GridHeaderStyle"/>
                                        <ItemStyle CssClass="GridRowStyle"/>
                                        <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                        <selecteditemstyle CssClass="GridSelectedItem"/>
                                        <FooterStyle CssClass="GridFooterStyle" />

                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                                        </ClientSettings>

                                    </telerik:RadGrid>  
                                </td>
                            </tr>
                       </table>
                       </fieldset>
                   </td>
                   <%--VARIABLES--%>
                     <td style="width:50%;">
                     <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
                     <legend>Variables</legend> 
                       <table>
                            <tr>
                                <td>
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Secuencia" width="100px" BackColor="transparent"></telerik:RadLabel>
                                </td>
                                <td>
                                     <telerik:RadNumericTextBox ID="txtVarSecuencia" runat="server" 
                                    DisabledStyle-CssClass="cssTxtEnabled" Enabled="false"
                                    EnabledStyle-CssClass="cssTxtEnabled" FocusedStyle-CssClass="cssTxtFocused" 
                                    HoveredStyle-CssClass="cssTxtHovered"  InvalidStyle-CssClass="cssTxtInvalid" 
                                    MaxLength="1" NumberFormat-DecimalDigits="0"  NumberFormat-GroupSeparator="" 
                                    NumberFormat-GroupSizes="8" width="90px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción" width="100px" BackColor="transparent"></telerik:RadLabel>
                                </td>
                                <td>
                                   <telerik:RadTextBox ID="txtVarDescripcion"  Enabled="false"
                                DisabledStyle-CssClass="cssTxtEnabled" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                FocusedStyle-CssClass="cssTxtFocused" 
                                HoveredStyle-CssClass="cssTxtHovered" 
                                InvalidStyle-CssClass="cssTxtInvalid" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                 <telerik:RadGrid ID="rGdvVariables" runat="server" AutoGenerateColumns ="false" Width="300px" Height="180px"   CssClass="Grid"  OnSelectedIndexChanged="rGdvVariables_SelectedIndexChanged" skin="Office2010Silver">

                                    <MasterTableView  DataKeyNames="revasec" AutoGenerateColumns="False"   CssClass="GridTable" >
                                     <Columns>
                                         <telerik:GridBoundColumn DataField="revaDes"  HeaderText="Descripción"  HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                                         <telerik:GridBoundColumn DataField="revasec"  HeaderText="Secuencia" HeaderStyle-Width="100px" ItemStyle-Width="100px"   ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                    <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>    
                                    </MasterTableView>

                                        <HeaderStyle CssClass="GridHeaderStyle"/>
                                        <ItemStyle CssClass="GridRowStyle"/>
                                        <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                        <selecteditemstyle CssClass="GridSelectedItem"/>
                                        <FooterStyle CssClass="GridFooterStyle" />

                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                                        </ClientSettings>

                                    </telerik:RadGrid>  
                                </td>
                            </tr>
                       </table>
                       </fieldset>
                   </td>
               </tr>
           </table>

        <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClientClicking="OnClientClic_ConfirmOK" OnClick="rBtnGuardar_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClientClicking="OnClientClic_ConfirmCancel" OnClick="rBtnCancelar_Click" ></telerik:RadImageButton>
            </asp:Panel>
        </div>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <asp:HiddenField ID="hdfSecuencia" runat="server" />
     
    </ContentTemplate>
    </asp:UpdatePanel>

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
