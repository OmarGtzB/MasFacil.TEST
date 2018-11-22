<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoMetValuacionAlt.aspx.cs" Inherits="DC_MttoMetValuacionAlt" %>
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
        
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
         <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
           <%-- <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
           --%> <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" style="top: 0px; left: 0px" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
        </asp:Panel>


       <fieldset  >
            <telerik:RadLabel ID="lbl_codigArt" runat="server" Text="" ></telerik:RadLabel>&nbsp
            <telerik:RadLabel ID="lbl_descripArt" runat="server" Text="" ></telerik:RadLabel>
        </fieldset>
     
        <fieldset>
            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Método" ></telerik:RadLabel>
            <telerik:RadComboBox ID="radCbo_Met" runat="server" Enabled="false"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="180px" DropDownWidth="280px" Height="200px"  
                                 AutoPostBack="true">

                <HeaderTemplate>
                    <table style="width: 250px" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 80px;">
                                Clave
                            </td>
                            <td style="width: 170px;">
                                Descripción
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width:80px" >
                                <%# DataBinder.Eval(Container.DataItem, "metValId")%>
                            </td>
                            <td style="width: 170px;">
                                <%# DataBinder.Eval(Container.DataItem, "metValDes") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                </FooterTemplate>
            </telerik:RadComboBox> &nbsp&nbsp


            <telerik:RadLabel ID="lbl_Factor" runat="server" Text="Moneda" ></telerik:RadLabel>
            <telerik:RadComboBox ID="radCbo_Mone" runat="server" AutoPostBack="true" Enabled="false"
                                 HighlightTemplatedItems="true"
                                 DropDownCssClass="cssRadComboBox"  
                                 Width="180px" DropDownWidth="280px" Height="200px">

                <HeaderTemplate>
                    <table style="width: 250px" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 80px;">
                                Clave
                            </td>
                            <td style="width: 170px;">
                                Descripción
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width:80px" >
                                <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                            </td>
                            <td style="width: 170px;">
                                <%# DataBinder.Eval(Container.DataItem, "monDes") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                </FooterTemplate>
            </telerik:RadComboBox>
        </fieldset>
   

        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
      
             <telerik:RadGrid ID="radGrid_metVal"
                 runat="server"
                 AutoGenerateColumns="False" 
                  OnSelectedIndexChanged="radGrid_metVal_SelectedIndexChanged" 
                  Width="560px"   Height="280px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver">

                 <MasterTableView DataKeyNames="ciaCve" AutoGenerateColumns="false"  CssClass="GridTable">
                     <NoRecordsTemplate>No se encontraron registros</NoRecordsTemplate>
                     <Columns>
                        <telerik:GridBoundColumn HeaderText="Clave Método" DataField="metValId" HeaderStyle-Width="100px"  ItemStyle-Width="100px"></telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Descripción" DataField="metValDes" HeaderStyle-Width="170px"  ItemStyle-Width="170px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Clave Moneda" DataField="monCve" HeaderStyle-Width="100px"  ItemStyle-Width="100px"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Descripción" DataField="monDes"  HeaderStyle-Width="170px"  ItemStyle-Width="170px"></telerik:GridBoundColumn>
            
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
                      
          <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
        
         <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"    runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar" Enabled="false" runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>
         <asp:HiddenField ID="hdfBtnAccion" runat="server" />
    </div>
    </ContentTemplate>
</asp:UpdatePanel> 
    </form>
</body>
</html>
