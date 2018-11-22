<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArtCodAlterno.aspx.cs" Inherits="DC_MttoArtCodAlterno" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
            </asp:Panel>                                                                                  
      
         
            <div>
            <fieldset >   
                <div style="width:100%; display:table; position:static; background-color:transparent;"   >

                        <table border="0" style=" width:318px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:140px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="" BackColor=""   ></telerik:RadLabel>  
                                </td>
                                <td style=" width:300px;  background-color:transparent;">                             
                                    <telerik:RadLabel ID="rLblDescripcion" runat="server"  Text=""  ></telerik:RadLabel>    
                                </td>
                            </tr>
                        </table>

                </div>
            </fieldset>
            </div>
        


           <div>
            <fieldset   >
            <telerik:RadLabel ID="rLblClaveAlterna" runat="server">Codificación alterna:</telerik:RadLabel>    
            <telerik:RadTextBox ID="rTxtClaveAlt" runat="server" Width="179px" Enabled="false" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" >
            </telerik:RadTextBox>

        </fieldset>

  
        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
 
            <telerik:RadGrid ID="rGdv_ClaveAlterna" 
                runat="server"    
                AutoGenerateColumns="False" 
                 OnSelectedIndexChanged="rGdv_ClaveAlterna_SelectedIndexChanged"
                Width="99%"  Height="187px"
                 CssClass="Grid"    
                skin="Office2010Silver"
                 >

                <MasterTableView AutoGenerateColumns="False" DataKeyNames="artCveAlt" CssClass="GridTable" >
                    <NoRecordsTemplate>
                        No se encontraron registros.
                    </NoRecordsTemplate>
                    <Columns>
                 
                        <telerik:GridBoundColumn HeaderText="Codigos Alternos" DataField="artCveAlt" HeaderStyle-Width="540px"  ItemStyle-Width="540px"     />

                    </Columns>
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
                          
               
               
                         
    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
        <telerik:RadImageButton ID="rBtnAceptar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch"  Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
    </asp:Panel>
  
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
      
           </div>
  </div>
    </ContentTemplate>
    </asp:UpdatePanel> 

         <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>


    </form>
</body>
</html>
