<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnidadesMed.aspx.cs" Inherits="DC_UnidadesMed" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <%--<asp:WebPartManager ID="WebPartManager1" runat="server"></asp:WebPartManager>--%>
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
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text=""  Visible="false" OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"  Visible="false" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
        </asp:Panel>
     

        <fieldset>
            <legend>Datos</legend>

            <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="Label1" runat="server" Text="Clave"  ></telerik:RadLabel>
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                             
                         <telerik:RadTextBox  ID="radUnidadMedida" runat="server" MaxLength="6"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                     Width="170px" Enabled="false"></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="radtxtDescripMed" runat="server" MaxLength="50"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"  Enabled="false" Width="250px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr style="height:18px;">
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Abreviatura"  ></telerik:RadLabel>
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                             
                        <telerik:RadTextBox  ID="rTxtAbreviatura" runat="server" MaxLength="20"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
            
                  width="170px"  >
                                    
            </telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="Label3" runat="server"  Text="Factorial"></telerik:RadLabel>
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                             
                        <telerik:RadNumericTextBox ID="radtxtFac" runat="server"  MaxLength="15"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid"
                                            AllowRounding="false" 
                      Enabled="false">
                <NumberFormat AllowRounding="false"  DecimalDigits="6"  />

</telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>  
            
            <table id="sat" runat ="server" visible="false" border="0" style=" width:640px; text-align:left; background-color:transparent;" >
                <tr style="height:18px;">
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="SAT"  ></telerik:RadLabel>
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                             
                                 <telerik:RadComboBox ID="rCboClaveSat" runat="server" Width="170px"   AllowCustomText="true"
                                                HighlightTemplatedItems="true" AutoPostBack="true"
                                              DropDownCssClass="cssRadComboBox" 
                                              DropDownWidth="260px" 
                                               Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 80px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 70px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:80px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "satUniMedCve")%>
                                                                </td>
                                                                <td style="width: 170px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "satUniMedNom") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                       
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                             

                    </td>
                </tr>
             </table>                      

            </fieldset>

            <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" > 
                 
                  <telerik:RadGrid ID="gdv_UnidadMed" 
                                   runat="server" 
                                   AutoGenerateColumns="False"
                                   OnSelectedIndexChanged="gdv_UnidadMed_SelectedIndexChanged" 
                                   Width="660px"   Height="300px"  
                                   CssClass="Grid" 
                                   Skin="Office2010Silver">
                        <MasterTableView DataKeyNames="ciaCve" AutoGenerateColumns="false" CssClass="GridTable">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="Clave"          HeaderStyle-Width="70px"   DataField="uniMedCve"             ></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Descripción"    HeaderStyle-Width="100px" DataField="uniMedDes"             ></telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Abreviatura"   HeaderStyle-Width="80px"  DataField="uniMedAbr"             ></telerik:GridBoundColumn>                             
                                <telerik:GridBoundColumn HeaderText="Factor"         HeaderStyle-Width="90px" DataField="uniMedFact"  DataType="System.Decimal"   DataFormatString="{0:###,##0.000000}"  ItemStyle-HorizontalAlign="Right" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CFDI" HeaderStyle-Width="50px" DataField="satUniMedCve" ItemStyle-Width="50px" Display="false" />
                                 </Columns>
                            <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                        </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="310px"    />
                    </ClientSettings>
                </telerik:RadGrid>

            </div>  


       

 
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar" Enabled="false"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar" Enabled="false"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
            </asp:Panel>

            <asp:HiddenField ID="hdf_id_Medida" runat="server" />
            <asp:PlaceHolder  ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>       
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server"></telerik:RadWindowManager>
    </form>



</body>
</html>
