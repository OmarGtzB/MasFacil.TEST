<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoDerivados.aspx.cs" Inherits="DC_MttoCptoDerivados" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="~/css/cssControles.css" rel="stylesheet" type="text/css" />
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />

     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
 
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>

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
                        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"           ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"   ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"     ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"   Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"       ToolTip ="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
                    </asp:Panel>
                   <div style="height:10px; ">
                    </div>
                    <fieldset >
                        <telerik:RadLabel ID="RadlabelOrigenID" runat="server" Text=""></telerik:RadLabel>
                        <telerik:RadLabel ID="RadlabelOrigenDes" runat="server" Text=""></telerik:RadLabel>
                    </fieldset>

                     <asp:Table runat="server" Width="100%">
                         <asp:TableRow>
                             <asp:TableCell>
                                 <fieldset >
                                       <legend>Concepto Derivado</legend>
                                        <div >
                                            <telerik:RadTextBox ID="RadtxtBuscar" runat="server"
                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                DisabledStyle-CssClass ="cssTxtEnabled"
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused"
                                                InvalidStyle-CssClass="cssTxtInvalid"
                                                ></telerik:RadTextBox>&nbsp;&nbsp;
                                            <telerik:RadButton ID="rbtBuscar" runat="server" Text="Buscar" OnClick="rbtBuscar_Click"></telerik:RadButton>
                                            <br />
                                            <br />
                                            <telerik:RadGrid ID="rGdvConceptos"  Height="280px" runat="server"
                                                AutoGenerateColumns ="false" 
                                                CssClass="Grid" 
                                                Skin="Office2010Silver"    ClientSettings-Selecting-AllowRowSelect="false" OnSelectedIndexChanged="rGdvConceptos_SelectedIndexChanged"
                                                  >
                                                <MasterTableView DataKeyNames="cptoDerCptoID"  
                                                     AutoGenerateColumns="False"  
                                                      CssClass="GridTable"  >
                                                    <Columns>
                                                         <telerik:GridBoundColumn DataField="cptoDerCptoID"  HeaderText="Clave"  HeaderStyle-Width="115px" ItemStyle-Width="20px" />
                                                         <telerik:GridBoundColumn DataField="cptoDerCptoDes" HeaderText="Descripción"  HeaderStyle-Width="115px"  ItemStyle-Width="20px" />
                               
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
                                    </fieldset>
                             </asp:TableCell>
                             <asp:TableCell>
                                  <fieldset >
                                       <legend>Mapeo de Concepto</legend>
                                        <div  >
                                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Conf. Derivado"></telerik:RadLabel>&nbsp;&nbsp;

                                            <telerik:RadComboBox ID="rCboDerivado"  Width="116px" DropDownCssClass="cssRadComboBox" 
                                                DropDownWidth="330px" Height="200px" HighlightTemplatedItems="true" runat="server"
                                                 OnSelectedIndexChanged="rCboDerivado_SelectedIndexChanged" AutoPostBack="true" Enabled="false"     >
                                                <HeaderTemplate>
                                                        <table id="tabbb" runat="server" style="width:205px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td id="sec" runat="server" style="width:60px;">
                                                                    Sec
                                                                </td>
                                                                <td style="width:135px;">
                                                                    Tipo Dato
                                                                </td>
                                                                <td style="width: 10px;">
                                                                    Descripción
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 400px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:43px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoConfSec")%>
                                                                </td>
                                                                <td style="width:85px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "listTipDatoCptoCve")%>
                                                                </td>
                                                                <td style="width: 230px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>&nbsp;&nbsp;&nbsp;&nbsp;





                                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Conf. Origen"></telerik:RadLabel>&nbsp;&nbsp;
                                            <telerik:RadComboBox ID="rCboOrigen" Width="116px" DropDownCssClass="cssRadComboBox" 
                                                 DropDownWidth="330px" Height="200px" HighlightTemplatedItems="true" runat="server" AutoPostBack="true" Enabled ="false" >
                                                <HeaderTemplate>
                                                        <table style="width:205px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:60px;">
                                                                    Sec
                                                                </td>
                                                                <td style="width:135px;">
                                                                    Tipo Dato
                                                                </td>
                                                                <td style="width:10px;">
                                                                    Descripción
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width:400px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:43px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoConfSec")%>
                                                                </td>
                                                                <td style="width:85px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "listTipDatoCptoCve")%>
                                                                </td>
                                                                <td style="width:230px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>

                                            <br />
                                            <br />
                                            <telerik:RadGrid ID="rGdvDerivados" runat="server" Height="278px"  AutoGenerateColumns ="false" 
                                                CssClass="Grid" 
                                                Skin="Office2010Silver"  OnSelectedIndexChanged="rGdvDerivados_SelectedIndexChanged"
                                                  >
                                                <MasterTableView>
                                                    <Columns>

                                                         <telerik:GridBoundColumn DataField="cptoConfDesDerivado" HeaderText="Conf. Derivado"  HeaderStyle-Width="140px" ItemStyle-Width="140px" />
                                                         <telerik:GridBoundColumn DataField="listTipDatoCptoCve" HeaderText="Tipo de Dato"  HeaderStyle-Width="200px"  ItemStyle-Width="200px" />
                                                         <telerik:GridBoundColumn DataField="cptoConfDesOrigen" HeaderText="Conf. Origen"  HeaderStyle-Width="140px"  ItemStyle-Width="140px" />
                                                        <telerik:GridBoundColumn DataField="cptoDerMapId" HeaderText="ID_REGISTRO"  HeaderStyle-Width="0px"  ItemStyle-Width="140px" Visible="true" />
                               
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
                                    </fieldset>
                             </asp:TableCell>
                         </asp:TableRow>
                     </asp:Table>
                     
                    
                 <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
                </asp:Panel>
                <asp:HiddenField ID="hdfBtnAccion" runat="server" />

                     <asp:HiddenField ID="SecDerivado" runat="server" />
                     <asp:HiddenField ID="SecOrigen" runat="server" />
                     <asp:HiddenField ID="TipoDato" runat="server" />
                 </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
   
    </form>
</body>
</html>
