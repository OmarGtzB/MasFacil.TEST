<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCausasCancelacionPagos.aspx.cs" Inherits="DC_MttoCausasCancelacionPagos" %>

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
                <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"  Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
            </asp:Panel>

            <fieldset>   

                 <div style="width:100%; display:table; position:static; background-color:transparent;">

                    <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td>
                                <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave" Width="65px"></telerik:RadLabel>  
                            </td>
                            <td>                             
                                <telerik:RadTextBox ID="rTxtClave" Width="150px" runat="server" MaxLength="50"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid" 
                                 ></telerik:RadTextBox>
                            </td>

                            <td>
                                <telerik:RadLabel ID="RadLabel2" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                            </td>

                            <td>                             
                                                            
                                <telerik:RadTextBox ID="rTxtAbreviatura" Width="150px" runat="server" MaxLength="10"
                                    EnabledStyle-CssClass="cssTxtEnabled"
                                    DisabledStyle-CssClass ="cssTxtEnabled"
                                    HoveredStyle-CssClass="cssTxtHovered"
                                    FocusedStyle-CssClass="cssTxtFocused"
                                    InvalidStyle-CssClass="cssTxtInvalid" 
                                 ></telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4">
                                <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción" Width="75px"></telerik:RadLabel>  
                           
                                <telerik:RadTextBox ID="rTxtDescripcion"  Width="423px" runat="server"  
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

            <br/>

            <telerik:RadGrid ID="rGdv_Condiciones" 
                           runat="server"  
                           AutoGenerateColumns="False" 
                           OnSelectedIndexChanged="rGdv_Condiciones_SelectedIndexChanged" 
                           Width="100%" Height="175px"
                           CssClass="Grid" 
                           Skin="Office2010Silver"     
                              >
                <MasterTableView DataKeyNames="cauCancPagCve" AutoGenerateColumns="False" CssClass="GridTable">
                    
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="Clave" DataField="cauCancPagCve" HeaderStyle-Width="75px"   ItemStyle-Width="75px" />
                        <telerik:GridBoundColumn HeaderText="Abreviatura"  DataField="cauCancPagAbr"  HeaderStyle-Width="75px"  ItemStyle-Width="75px" />
                        <telerik:GridBoundColumn HeaderText="Descripción" DataField="cauCancPagDes" HeaderStyle-Width="150px"   ItemStyle-Width="150px" />
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

