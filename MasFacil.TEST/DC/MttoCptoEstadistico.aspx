<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoEstadistico.aspx.cs" Inherits="DC_MttoCptoEstadistico" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""  ></telerik:RadImageButton>    
    </asp:Panel>
            <fieldset  >       

                <div style="width:100%; display:table;  position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel2" runat="server" Text="Concepto"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadNumericTextBox ID="rTxtConcepto" runat="server" Width="100px"  
                                                            NumberFormat-GroupSeparator=""
                                                            NumberFormat-GroupSizes="8" 
                                           NumberFormat-DecimalDigits="0" MaxLength="4" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"  
                                 ></telerik:RadNumericTextBox> 
                            </td>
                            <td style=" width:100px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel4" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"                                    
                                ></telerik:RadTextBox> 
                            </td>

                        </tr>

                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel5" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtAbreviatura" runat="server" Width="100px"  
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                ></telerik:RadTextBox>
                            </td>
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Moneda"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadComboBox ID="rCboMoneda" Width="200px" DropDownCssClass="cssRadComboBox" 
                                      DropDownWidth="330px" Height="200px" HighlightTemplatedItems="true" runat="server">
                                    <HeaderTemplate>
                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 70px;">
                                                    Clave
                                                </td>
                                                <td style="width: 230px;">
                                                    Descripción
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:70px">
                                                    <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                </td>
                                                <td style="width: 230px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "monDes") %>
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


     
          <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
                  
                <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                        OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged" 
                        Width="660px" Height="225px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                   <MasterTableView DataKeyNames="cptoEstId"  
                              AutoGenerateColumns="False"  
                                   CssClass="GridTable" >

                         <Columns >
                           
                                <telerik:GridBoundColumn DataField="cptoEstId"  HeaderText="Concepto"  HeaderStyle-Width="165px" ItemStyle-Width="140px" />
                                <telerik:GridBoundColumn DataField="cptoEstDes" HeaderText="Descripción"  HeaderStyle-Width="225px"  ItemStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="cptoEstAbr"  HeaderText="Abreviatura" HeaderStyle-Width="125px" ItemStyle-Width="100px" dataFormatString="{0:###,##0.00}"  ItemStyle-HorizontalAlign="Right"/>
                                <telerik:GridBoundColumn DataField="monCve" HeaderText="Moneda" HeaderStyle-Width="125px"  ItemStyle-Width="100px" dataFormatString="{0:###,##0.00}"  ItemStyle-HorizontalAlign="Right" />
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
         </div>    

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
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

