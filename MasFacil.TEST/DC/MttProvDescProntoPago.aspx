<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttProvDescProntoPago.aspx.cs" Inherits="DC_MttProvDescProntoPago" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
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
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>


    <fieldset >   
    <legend>Rango de Dias</legend>
        <div style="display:table; background-color:transparent;" align="center"   >

            <table border="0" style=" width:375px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:30px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="De"></telerik:RadLabel>  
                    </td>
                    <td style=" width:60px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtDe"
                                                            runat="server" Width="40px" 
                                                            DisabledStyle-CssClass ="cssTxtEnabled" 
                                                            EnabledStyle-CssClass="cssTxtEnabled" 
                                                            FocusedStyle-CssClass="cssTxtFocused"   
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                            MaxLength ="4"
                                                            MinValue="1" 
                                                            MaxValue="999999999"                                       
                         >
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                                  
                    </td>
                    <td style=" width:30px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="A"></telerik:RadLabel>  
                    </td>
                    <td style=" width:100px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtA"  runat="server" Width="40px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                            NumberFormat-DecimalDigits="0" 
                                                            MaxLength ="4"
                                                            MinValue="1" 
                                                            MaxValue="999999999"   
                         >
                         <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                
              
                    <td style=" width:50px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descuento"></telerik:RadLabel>  
                    </td>
                    <td style=" width:105px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="rTxtDesc" runat="server" Width="100px" 
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                            dataFormatString="{#:###,###.##}" 
                         ></telerik:RadNumericTextBox>
                    </td>
                    </tr>
            
            </table>           

        </div>
    </fieldset>

 
        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >   
  
          <telerik:RadGrid ID="rGdv_Descuento"  
                           runat="server" 
                           AutoGenerateColumns="False"  
                           OnSelectedIndexChanged="rGdv_Almacenes_SelectedIndexChanged"
                           Width="400px" Height="245px" 
                           CssClass="Grid" 
                           Skin="Office2010Silver" 
                               >
                <MasterTableView DataKeyNames="provCve" AutoGenerateColumns="False" CssClass="GridTable">
                    <NoRecordsTemplate>No se encontraron registros.</NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="De"         DataField="provDPPLimInf" HeaderStyle-Width="100px" dataFormatString="{0:###,###.##}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"/>
                        <telerik:GridBoundColumn HeaderText="A"          DataField="provDPPLimSup" HeaderStyle-Width="100px" dataFormatString="{0:###,###.##}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderText="Descuento"  DataField="provDPPPorc" HeaderStyle-Width="180px" dataFormatString="{0:###,##0.00}" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Right" />                                           
                    </Columns>
                </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="245px" />
                    </ClientSettings>
            </telerik:RadGrid>

        </div>                 


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


