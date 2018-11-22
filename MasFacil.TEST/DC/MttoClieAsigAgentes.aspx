
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoClieAsigAgentes.aspx.cs" Inherits="DC_MttoClieAsigAgentes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />


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
                                                                           
                <asp:Panel ID="pnlBtnsAcciones"  CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton><%-- OnClick="rBtnNuevo_Click"  --%>
                      <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
                </asp:Panel>                                                                                  
                 
                  
          
            <fieldset >   
                        <table border="0" style=" width:540px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:70px; background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel5" runat="server">Cliente:</telerik:RadLabel>
                                </td>  
                                <td style=" width:200px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="Cliente"></telerik:RadLabel>     
                                </td>    
                                <td style=" width:70px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblSubClie" runat="server">SubCliente:</telerik:RadLabel>  
                                </td>   
                                <td style=" width:200px; background-color:transparent;">
                                     <telerik:RadLabel ID="rLblSubClient" runat="server" Text="Subcliente"> </telerik:RadLabel>
                                </td>                                                                                                         
                           </tr>
                       </table>

                        <table border="0" style=" width:540px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:70px; background-color:transparent;">
                                        <telerik:RadLabel ID="RadLabel7" runat="server">Nombre:</telerik:RadLabel>
                                </td>  
                                <td style=" width:470px; background-color:transparent;">
                                        <telerik:RadLabel ID="rLblDescripcion" runat="server" Text="Nombre"> </telerik:RadLabel>
                                </td>                                                                                                                     
                           </tr>
                       </table>

            </fieldset>
    
                          <fieldset >  
                              
                          <legend>
                                  Agentes
                                  </legend>
                            
<table>   
                               <tr>
                                   <td >
                                       <telerik:RadLabel ID="RadLabel2" runat="server">
                                           De ventas:
                                       </telerik:RadLabel>
                                   </td>
                                   <td >
                                       <telerik:RadComboBox ID="rCboAgeVentas" runat="server" AutoPostBack="True" Enabled="false" 
                                            HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"
                                            DropDownWidth="390px" Height="200px" >
                                            <HeaderTemplate>
                                                <table style="width: 360px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Clave
                                                        </td>
                                                        <td style="width: 260px;">
                                                            Nombre
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:100px" >
                                                            <%# DataBinder.Eval(Container.DataItem, "ageCve")%>
                                                        </td>
                                                        <td style="width: 260px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "ageNom") %>
                                                        </td>
                                  
                                                    </tr>
                                                </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                        </FooterTemplate>
                                       </telerik:RadComboBox>
                                   </td>
                                   <td >
                                       <telerik:RadLabel ID="RadLabel1" runat="server">
                                           De cobranza:
                                       </telerik:RadLabel>
                                   </td>
                                   <td>
                                       <telerik:RadComboBox ID="rCboCobranza" runat="server" AutoPostBack="True" Enabled="false" 
                                           HighlightTemplatedItems="true"
                                            DropDownCssClass="cssRadComboBox"
                                            DropDownWidth="390px" Height="200px">
                                            <HeaderTemplate>
                                                <table style="width: 360px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Clave
                                                        </td>
                                                        <td style="width: 260px;">
                                                            Nombre
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:100px" >
                                                            <%# DataBinder.Eval(Container.DataItem, "ageCve")%>
                                                        </td>
                                                        <td style="width: 260px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "ageNom") %>
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
                                                         
                                 </fieldset>
      
                                   <asp:Panel ID="pnlBtnsAplicaAccion" runat="server" CssClass="cspnlBtnsAplicaAccion">
                                       <telerik:RadImageButton ID="rBtnGuardar" runat="server" 
    OnClientClicking="OnClientClic_ConfirmOK" ButtonType="StandardButton"    Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"   OnClick="rBtnGuardar_Click"  Enabled="false" ToolTip="Guardar" Width="80px">
</telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar"  runat="server" 
    OnClientClicking="OnClientClic_ConfirmCancel" ButtonType="StandardButton"    Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"   OnClick="rBtnCancelar_Click"  Enabled="false" ToolTip="Cancelar" Width="80px">
</telerik:RadImageButton>
  
                                   </asp:Panel>
                                   <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                                   <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                   </telerik:RadWindowManager>
                                   <telerik:RadTextBox ID="rTxtagrCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                   <telerik:RadTextBox ID="rTxtciaCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                      <telerik:RadTextBox ID="rTxtagrDatoCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                   <telerik:RadTextBox ID="rTxagrTipId" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                      
                    
    </ContentTemplate>
    </asp:UpdatePanel> 


    </form>
                          


 
</body>

</html>
