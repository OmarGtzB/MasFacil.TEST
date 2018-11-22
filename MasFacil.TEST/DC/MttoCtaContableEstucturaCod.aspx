<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCtaContableEstucturaCod.aspx.cs" Inherits="DC_MttoCtaContableEstucturaCod" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="/css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
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
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false"  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>    
        </asp:Panel>

    <fieldset>   
    <legend>Nivel</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:610px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:40px; background-color:transparent;">
                        <telerik:RadLabel ID="lblNivel" runat="server" Text="Nivel"></telerik:RadLabel>  
                    </td>
                    <td style=" width:80px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtNivel" runat="server" Width="50px" MaxLength="3"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                            enabled ="false"
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:85px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>  
                    </td>
                    <td style=" width:280px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtDescripcion"  Width="250px" runat="server"  
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>

                    <td style=" width:65px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Longitud"></telerik:RadLabel>  
                    </td>
                    <td style=" width:60px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtLongitud" runat="server" Width="50px" MaxLength="3" InputType="Number" 
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


     
          <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
 
                  
                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                        OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged" 
                        Width="640px" Height="225px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView DataKeyNames="ctaContEstCodNiv"  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >
                         <Columns >
                            <telerik:GridBoundColumn DataField="ciaCve"              HeaderText="ciaCve"               HeaderStyle-Width="50px" ItemStyle-Width="50px" Display="false" />   
                            <telerik:GridBoundColumn DataField="ctaContEstCodNiv"    HeaderText="Nivel"     HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                            <telerik:GridBoundColumn DataField="ctaContEstCodDes"    HeaderText="Descipción"     HeaderStyle-Width="210" ItemStyle-Width="210px" />
                            <telerik:GridBoundColumn DataField="ctaContEstCodPosLon" HeaderText="Longitud"  HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                            <telerik:GridBoundColumn DataField="ctaContEstCodPosIni" HeaderText="P. Inicial"  HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                            <telerik:GridBoundColumn DataField="ctaContEstCodPosFin" HeaderText="P. Final"  HeaderStyle-Width="80px" ItemStyle-Width="80px" />
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
            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />

    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"      >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
