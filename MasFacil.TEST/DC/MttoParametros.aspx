<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoParametros.aspx.cs" Inherits="DC_MttoParametros" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click"   Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click"   Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>       
                </asp:Panel>
                  <fieldset style="  margin-top:5px;   display: block; text-align:left;" >
                    <div style="width:100%; display:table; position:static; background-color:transparent;" >
                        <table border="0" style=" text-align:left; background-color:transparent; width:100%" >
                            <tr>
                                <td>
                                    <telerik:RadLabel ID="RadLabel1" runat="server" Text="Parámetro" ></telerik:RadLabel> 
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="rTxtParametro" runat="server" EnabledStyle-CssClass="cssTxtEnabled" Width="140px"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid" MaxLength="10"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Sec"></telerik:RadLabel> 
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="rTxtNumSecuencia" runat="server" Width="25px"  Enabled="false" EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                    
                                </td>
                                <td>
                                    <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel> 
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="rTxtDescripcion" runat="server"  Width="380px" EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td>
                                    <telerik:RadLabel ID="RadLabel4" runat="server" Text="Tipo"></telerik:RadLabel> 
                                </td>
                                <td colspan="3" style="background-color:transparent;">
                                   <telerik:RadComboBox ID="rCmboTipo" runat="server" Width="200px"  AllowCustomText="false" Enabled="true"
                                          HighlightTemplatedItems="true" AutoPostBack="true"  OnSelectedIndexChanged="rCmboTipo_SelectedIndexChanged"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >          
                                      <HeaderTemplate>
                                            <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 190px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <FooterTemplate>
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                        </FooterTemplate>
                                </telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadLabel ID="RadLabel5" runat="server" Text="Valor"></telerik:RadLabel> 
                                </td>
                                <td> 
                                    <telerik:RadTextBox ID="rTxt_Valor" runat="server"  EnabledStyle-CssClass="cssTxtEnabled" Visible="false" Width="220px"  
                                        DisabledStyle-CssClass ="rTxtValor"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>

                                      <telerik:RadNumericTextBox ID="rdNumerico_Valor" runat="server" Enabled="true" Visible="true" Width="220px" 
                                        EnabledStyle-CssClass="cssTxtEnabled" 
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid">
                                         <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                     <telerik:RadDatePicker ID="RdDateFecha_Valor"  runat="server" AutoPostBack="true"  Width="140px"  Visible="false">
                                </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                    </div>
                  </fieldset>
            <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">  
          <telerik:RadGrid ID="rGdv_Parametros"  OnSelectedIndexChanged="rGdv_Parametros_SelectedIndexChanged"
                           runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="820px" Height="260px"   
                           CssClass="Grid"  
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="parmCve"  AutoGenerateColumns="false" CssClass="GridTable"     >
                    <Columns> 
                        <telerik:GridBoundColumn HeaderText="Parámetro"  DataField="parmCve"   HeaderStyle-Width="40px"  ItemStyle-Width="40px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Sec"  DataField="parmSec"   HeaderStyle-Width="15px"  ItemStyle-Width="15px" Display="false" ></telerik:GridBoundColumn>      
                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="parmDes"  HeaderStyle-Width="140px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Valor"  DataField="PerVal"   HeaderStyle-Width="75px"  ItemStyle-Width="75px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Tipo"  DataField="parmValTip"   HeaderStyle-Width="5px"  ItemStyle-Width="5px" Display="false" ></telerik:GridBoundColumn>
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        </div> 
          </div>
             <div style="width:100%; display:table; position:static; background-color:transparent;">
                <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""   OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"   OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
              </asp:Panel>
            </div>
              <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            </ContentTemplate>
         </asp:UpdatePanel>
         <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
