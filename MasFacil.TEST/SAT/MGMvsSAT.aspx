<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGMvsSAT.aspx.cs" Inherits="SAT_MGMvsSAT" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

        <style type="text/css">
        .cssFrame {
            width: 100%;
            height: 167px;
         border: 0px none blue ;
        } 
    </style>

</head>
<body>
    <form id="form1" runat="server"  >
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton> 
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
            </asp:Panel>

            <fieldset >   
                <legend>MGM /SAT</legend>
                <div style="width:100%; display:table; position:static; background-color:transparent;"   >


                        <table border="0" style=" width:750px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:125px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel4" runat="server">Catalogo MGM</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboCatalogoMGM" runat="server" Width="240px" 
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="240px" Height="240px" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="rCboCatalogoMGM_SelectedIndexChanged"
                                        >
                                            <HeaderTemplate>
                                                <table style="width: 240px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:240px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <table style="width: 240px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:240px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "satCatMGMDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                    </telerik:RadComboBox>

                                </td>
                                <td style=" width:125px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel1" runat="server">Catalogo CFDI</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboCatalogoCFDI" runat="server" Width="240px" 
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="260px" Height="240px" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="rCboCatalogoCFDI_SelectedIndexChanged"
                                        >
                                            <HeaderTemplate>
                                                <table style="width: 260px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:100px;">
                                                            Clave
                                                        </td>
                                                        <td style="width:160px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <table style="width: 260px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:100px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "satCatCFDICve") %>
                                                            </td>
                                                            <td style="width:160px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "satCatCFDIMDes") %>
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

            <fieldset >   
                <legend>Relacion de Claves</legend>
                <div style="width:100%; display:table; position:static; background-color:transparent;"   >

                        <table border="0" style=" width:750px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:125px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel5" runat="server">Clave MGM</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboCatMGMCve" runat="server" Width="240px" Enabled="false"
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="240px" Height="240px" 
                                        AutoPostBack="True" 
                                        >
                                            <HeaderTemplate>
                                                <table style="width: 240px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:60px;">
                                                            Clave
                                                        </td>
                                                        <td style="width:180px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <table style="width: 240px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:60px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "Cve") %>
                                                            </td>
                                                            <td style="width:180px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "Des") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                    </telerik:RadComboBox>

                                </td>
                                <td style=" width:125px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel6" runat="server">Clave CFDI</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboCatCFDICve" runat="server" Width="240px"  Enabled="false"
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="240px" Height="240px" 
                                        AutoPostBack="True" 
                                        >
                                            <HeaderTemplate>
                                                <table style="width: 240px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width:60px;">
                                                            Clave
                                                        </td>
                                                        <td style="width:180px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <table style="width: 240px;"  cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:60px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "Cve") %>
                                                            </td>
                                                            <td style="width:180px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "Des") %>
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

            <telerik:RadGrid ID="rGdvInformacion"  OnSelectedIndexChanged="rGdvInformacion_SelectedIndexChanged"
                        runat="server"
                        AutoGenerateColumns ="false" 
                        Width="760px" Height="290px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView  AutoGenerateColumns="False"  DataKeyNames="MGMCve" CssClass="GridTable" >
                             <Columns >
                                <telerik:GridBoundColumn DataField="MGMCve"  HeaderText="Clave"  HeaderStyle-Width="25%" ItemStyle-Width="25%"  />
                                <telerik:GridBoundColumn DataField="MGMDes"  HeaderText="Nombre" HeaderStyle-Width="25%" ItemStyle-Width="25%"  />
                                <telerik:GridBoundColumn DataField="CFDICve"    HeaderText="CFDI" HeaderStyle-Width="25%"  ItemStyle-Width="25%"   />
                                <telerik:GridBoundColumn DataField="CFDIDes" HeaderText="Nombre" HeaderStyle-Width="25%"  ItemStyle-Width="25%"   />                            </Columns>
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


            <div style="width:100%; display:table; position:static; background-color:transparent;">
                <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                    <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
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
