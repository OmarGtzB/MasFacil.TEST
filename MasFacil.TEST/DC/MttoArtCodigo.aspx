<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArtCodigo.aspx.cs" Inherits="DC_MttoArtCodigo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
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
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"  Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>


    <fieldset>   
    <legend>Configuración de Elemento</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:30px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Elemento"></telerik:RadLabel>  
                    </td>
                    <td style=" width:30px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtEstCodElem" style=" width:30px;" runat="server" MaxLength="3"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>

                    <td style=" width:30px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>  
                    </td>

                    <td style=" width:150px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtEstCodDes"  Width="150px" runat="server"  
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>

                    
                    <td style=" width:20px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Longitud"></telerik:RadLabel>  
                    </td>
                    <td style=" width:20px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtEstCodLong" InputType="Number" runat="server"
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
                        <telerik:RadLabel ID="lblTipStrc" runat="server" Text="Tipo"></telerik:RadLabel>  
                    </td>
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadComboBox ID="rCboTiposCodigos" runat="server" HighlightTemplatedItems="true"  DropDownWidth="300px"
                                                     DropDownCssClass="cssRadComboBox" OnSelectedIndexChanged="rCboTiposCodigos_SelectedIndexChanged" AutoPostBack="True" >            
                            <HeaderTemplate>
                                <table style="width: 260px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 260px;">
                                                Descripción
                                            </td>
                                        </tr>
                                    </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 260px;">
                                                <%# DataBinder.Eval(Container.DataItem, "listPreValDes") %>
                                            </td>
                                  
                                        </tr>
                                    </table>

                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>

                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Agrupación"></telerik:RadLabel>  
                    </td>
                    <td style=" width:100px; background-color:transparent;">
                        
                        <telerik:RadComboBox ID="rCboAgrupaciones" runat="server" HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" DropDownWidth="330px"  >
                            <HeaderTemplate>
                                <table style="width: 300px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50px;">
                                                Clave
                                            </td>
                                            <td style="width: 250px;">
                                                Descripción
                                            </td>
                                        </tr>
                                    </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 50px;">
                                                <%# DataBinder.Eval(Container.DataItem, "agrCve") %>
                                            </td>
                                            <td style="width: 250px;">
                                                <%# DataBinder.Eval(Container.DataItem, "agrDes") %>
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
                           


          <telerik:RadGrid ID="rGdv_Almacenes" 
                           runat="server"  
                           AutoGenerateColumns="False" 
                           OnSelectedIndexChanged="rGdv_Almacenes_SelectedIndexChanged" 
                           Width="760px" Height="230px"
                           CssClass="Grid" 
                           Skin="Office2010Silver"     
                              >
                <MasterTableView DataKeyNames="artEstCodElem" AutoGenerateColumns="False" CssClass="GridTable">
                    
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Elem."        DataField="artEstCodElem" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />

                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="artEstCodDes"  HeaderStyle-Width="200px"  ItemStyle-Width="200px" />
                        <telerik:GridBoundColumn HeaderText="Tipo"         DataField="desTip"        HeaderStyle-Width="200px"   ItemStyle-Width="200px" />                                           
                        <telerik:GridBoundColumn HeaderText="Long"         DataField="artEstCodLong" HeaderStyle-Width="40px"   ItemStyle-Width="40px" />                                           
                        <telerik:GridBoundColumn HeaderText="Ini."         DataField="artEstCodIni"  HeaderStyle-Width="40px"   ItemStyle-Width="40px" />                                           
                        <telerik:GridBoundColumn HeaderText="Fin"          DataField="artEstCodFin"  HeaderStyle-Width="40px"   ItemStyle-Width="40px" />                                           
                        <telerik:GridBoundColumn HeaderText="Agrupación"   DataField="agrDes"        HeaderStyle-Width="180px"   ItemStyle-Width="180px" />                                           
                    </Columns>
                    <NoRecordsTemplate>No se encontraron registros</NoRecordsTemplate>
                </MasterTableView>

                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="230px"    />
                        <Animation AllowColumnReorderAnimation="True" />
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
