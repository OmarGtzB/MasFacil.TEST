﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGPerfil.aspx.cs" Inherits="SG_SGPerfil" %>

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
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click"  ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text=""  OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text=""   OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>      
                </asp:Panel>
                     <fieldset>  
           <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style="  text-align:left; background-color:transparent ;">
                        <tr >
                            <td style=" background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                            </td>
                                <td style=" width:130px; background-color:transparent;">                             
                                <telerik:RadTextBox ID="rtxtClave" runat="server" Width="115px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"       
                                            MaxLength="10"   Enabled="false" >
                                </telerik:RadTextBox> 
                            </td>

                            <td style=" background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                            </td>
                            <td style=" background-color:transparent;">                             
                                <telerik:RadTextBox ID="rTxtDescripcion" runat="server" Width="200px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"  Enabled="false"                                   
                                ></telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr >
                            <td style=" background-color:transparent;">
                                   <telerik:RadLabel ID="RadLabel2" runat="server" Text="Activo"></telerik:RadLabel>  
                            </td>
                            <td style=" width:130px; background-color:transparent;">                             
                                <asp:CheckBox ID="CheckReg" runat="server"  BackColor="White" /> 
                            </td>
                        </tr>
              

                    </table>

                </div>

            </fieldset>
         <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">

                    <telerik:RadGrid ID="rGdvPerfiles"  OnSelectedIndexChanged="rGdvImpuestos_SelectedIndexChanged"
                        runat="server"
                           AllowMultiRowSelection="true"
                           AutoGenerateColumns="False" 
                           Width="510px" Height="280px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver">
                        <MasterTableView DataKeyNames="maPerfCve"  
                                         AutoGenerateColumns="False"  
                                          CssClass="GridTable" >

                         <Columns >
                                <telerik:GridBoundColumn DataField="maPerfCve"  HeaderText="Clave"  HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                <telerik:GridBoundColumn DataField="maPerfDes" HeaderText="Descripción"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" />
                                <telerik:GridBoundColumn DataField="maPerfStsDes"  HeaderText="Situación" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                             <telerik:GridBoundColumn DataField="maPerfSts"  HeaderText="Situación" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="false" />               
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
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
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
