<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CatalogoFraccionArancelaria.aspx.cs" Inherits="SAT_CatalogoFraccionArancelaria" %>
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
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div>
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
                <telerik:RadImageButton  ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false"  ></telerik:RadImageButton>
                 <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar" OnClick="rBtnModificar_Click"  Text="" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton  ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"   OnClick="rBtnEliminar_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton  ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>      
            </asp:Panel>

            <fieldset> 
            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >   

                <table border="0" style=" width:540px; text-align:left; background-color:transparent;">
                    <tr style="height:18px;">
                        <td style=" width:50px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                        </td>
                        <td style=" width:130px;  background-color:transparent;">                             
                            <telerik:RadTextBox ID="rtxtCve" runat="server" Width="110px" 
                                        EnabledStyle-CssClass="cssTxtEnabled"
                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                        HoveredStyle-CssClass="cssTxtHovered"
                                        FocusedStyle-CssClass="cssTxtFocused"
                                        InvalidStyle-CssClass="cssTxtInvalid"       
                                        MaxLength="10"   Enabled="false">
                            </telerik:RadTextBox> 
                        </td>
                        <td style=" width:90px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Descripción"></telerik:RadLabel>  
                        </td>
                        <td style=" width:270px;  background-color:transparent;">                             
                            <telerik:RadTextBox ID="rTxtDes" runat="server" Width="260px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"       
                                            Enabled="false"           
                            ></telerik:RadTextBox> 
                        </td>
                    </tr>
                </table>

            </div> 
            </fieldset>

            <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">

                <telerik:RadGrid ID="rGdvSat"   OnSelectedIndexChanged="rGdvSat_SelectedIndexChanged"
                        runat="server"  
                        AllowMultiRowSelection="false"
                        AutoGenerateColumns="False" 
                        Width="550px" Height="220px"   
                        CssClass="Grid" 
                        Skin="Office2010Silver" >
                    <MasterTableView  DataKeyNames="satFracACve"  AutoGenerateColumns="False"   CssClass="GridTable" >
                        <Columns>
                            <telerik:GridBoundColumn DataField="satFracACve" HeaderText="Clave"       HeaderStyle-Width="100px"  ItemStyle-Width="100px"  />
                            <telerik:GridBoundColumn DataField="satFracADes" HeaderText="Descripción" HeaderStyle-Width="300px"  ItemStyle-Width="300px"  />
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


        </div>


        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />


        </ContentTemplate>
         </asp:UpdatePanel>

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
