<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoMonedaTiposdeCambio.aspx.cs" Inherits="MttoMonedaTiposdeCambio" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
</head>
<html xmlns="http://www.w3.org/1999/xhtml">

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

    <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>      
    </asp:Panel>
        <fieldset> 
           <legend>Moneda</legend>
            <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
             <tr style="height:18px;"> 
                 <td>
                      <telerik:RadLabel  ID="RadLabel5" runat="server" Text="Moneda" BackColor="transparent" Width="90px"></telerik:RadLabel> 
                     <telerik:RadLabel ID="radlb_Descripcionmoneda" runat="server" Text="Valor"></telerik:RadLabel> 
                 </td>
              </tr>
            </table>
           
            
       </fieldset>


            <fieldset> 
            <legend>Tipo de Cambio</legend>

           <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;"> 
                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Fecha"  Enabled="false" ></telerik:RadLabel>  
                            </td>
                                <td style=" width:220px;  background-color:transparent;">                            
                                  <telerik:RadDatePicker ID="RadDatePickerFecha" runat="server" Enabled="false"  ></telerik:RadDatePicker>
                            </td>

                            <td style=" width:100px; background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Diario"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;" >                             
                                <telerik:RadNumericTextBox ID="rTxtTCC" runat="server" Enabled="false"  
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid">
                                <NumberFormat AllowRounding="False" DecimalDigits="4"/>  
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr style="height:18px;">
                            <td style=" width:100px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel2" runat="server" Text="Promedio"></telerik:RadLabel>  
                            </td>
                            <td style=" width:220px;  background-color:transparent;">                             
                                <telerik:RadNumericTextBox ID="rTxtTCP" runat="server" Enabled="false" 
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid">
                                <NumberFormat AllowRounding="False" DecimalDigits="4"/>  
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style=" width:100px; background-color:transparent;">
                                 <telerik:RadLabel ID="RadLabel4" runat="server" Text="Mensual"></telerik:RadLabel>  
                            </td>
                            <td style=" width:110px;  background-color:transparent;">                             
                                <telerik:RadNumericTextBox ID="rTxtTCM" runat="server" Enabled="false" 
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid">
                                <NumberFormat AllowRounding="False" DecimalDigits="4"/>  
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
              

                    </table>

                </div>

            </fieldset>


              <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
                    <telerik:RadGrid ID="rGdv_TipoCambio"  OnSelectedIndexChanged="rGdv_TipoCambio_SelectedIndexChanged"
                        runat="server" 
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="640px" Height="260px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver">

                      <MasterTableView DataKeyNames="monTCId" AutoGenerateColumns="False" CssClass="GridTable" >

                       <Columns >
                           
                                <telerik:GridBoundColumn DataField="monTCId"  HeaderText="Clave"  HeaderStyle-Width="1px" ItemStyle-Width="1px" Visible="true" />
                                <telerik:GridBoundColumn DataField="monDesMon" HeaderText="Descripcion"  HeaderStyle-Width="200px"  ItemStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="monTCFec"  HeaderText="Fecha" HeaderStyle-Width="100px" ItemStyle-Width="100px" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="monTCC" HeaderText="Diario" HeaderStyle-Width="100px"  ItemStyle-Width="100px" DataFormatString="{0:###,##0.0000}" ItemStyle-HorizontalAlign="Right" />  
                                <telerik:GridBoundColumn DataField="monTCP" HeaderText="Promedio" HeaderStyle-Width="100px"  ItemStyle-Width="100px" DataFormatString="{0:###,##0.0000}" ItemStyle-HorizontalAlign="Right" />  
                                <telerik:GridBoundColumn DataField="monTCM" HeaderText="Mensual" HeaderStyle-Width="100px"  ItemStyle-Width="100px" DataFormatString="{0:###,##0.0000}" ItemStyle-HorizontalAlign="Right" />        
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="260px"     />
                    </ClientSettings>
                </telerik:RadGrid>  
         </div>    

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
