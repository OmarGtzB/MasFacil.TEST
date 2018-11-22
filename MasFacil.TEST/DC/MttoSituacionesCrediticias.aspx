<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoSituacionesCrediticias.aspx.cs" Inherits="DC_MttoSituacionesCrediticias" %>

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
        <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" Visible="false"  OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" Visible="false"  OnClick="rBtnModificar_Click"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
        <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
    </asp:Panel>


    <fieldset>   
    <legend>Datos</legend>
        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

            <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                <tr style="height:18px;">
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>  
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtSitCreCve" runat="server" MaxLength="10"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>  
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtSitCreDes"  Width="250px" runat="server" MaxLength="50"  
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
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Abreviatura"></telerik:RadLabel>  
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                             
                        <telerik:RadTextBox ID="rTxtSitCreAbr" runat="server" MaxLength="15"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                         ></telerik:RadTextBox>
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                             
                        
                    </td>
                </tr>

                <tr style="height:18px;">
                    <td style=" width:100px; background-color:transparent;">
                        <telerik:RadLabel ID="RadLabel4" runat="server" Text="Aplicación"></telerik:RadLabel>  
                    </td>
                    <td style=" width:190px;  background-color:transparent;">                         
                        <asp:RadioButton ID="rBtnApliClie" runat="server" GroupName="sitCreApli" Text="A Nivel Cliente" Checked="true"  AutoPostBack="true"/><br />
                        <asp:RadioButton ID="rBtnApliPart" runat="server" GroupName="sitCreApli" Text="A Nivel Partida"  AutoPostBack="true"/>
                                        
                    </td>
                    <td style=" width:90px; background-color:transparent;">
                         <telerik:RadLabel ID="RadLabel5" runat="server" Text="Calificación"></telerik:RadLabel>  
                    </td>
                    <td style=" width:260px;  background-color:transparent;">                        
                        <asp:RadioButton ID="rBtnCaliNor" runat="server" GroupName="sitCreCali" Text="Situación Normal" Checked="true"  AutoPostBack="true"/><br />
                        <asp:RadioButton ID="rBtnCaliRes" runat="server" GroupName="sitCreCali" Text="Situación Restrictiva/Negativa"  AutoPostBack="true"/>

                    </td>
                </tr>
            </table>           

        </div>
    </fieldset>
         
    <fieldset> 
        
        
         <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">         

          <telerik:RadGrid ID="rGdv_SituacionCred" 
                           runat="server"
                           OnSelectedIndexChanged="rGdv_SituacionCred_SelectedIndexChanged"
                           AutoGenerateColumns="False" 
                           Width="640px" Height="220px"   
                           CssClass="Grid" 
                           Skin="Office2010Silver">

                <MasterTableView DataKeyNames="siCrCve"  AutoGenerateColumns="false" CssClass="GridTable"    >
                    <Columns> 
                        
                        <telerik:GridBoundColumn HeaderText="Clave"  DataField="siCrCve"   HeaderStyle-Width="50px"  ItemStyle-Width="400px" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="siCrDes"   HeaderStyle-Width="160px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>                                       
                        <telerik:GridBoundColumn HeaderText="Abreviatura"  DataField="siCrAbr"   HeaderStyle-Width="80px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>                                       
                        <telerik:GridBoundColumn HeaderText="Aplicación"  DataField="desApli"  HeaderStyle-Width="80px"  ItemStyle-Width="140px" ></telerik:GridBoundColumn>                                       
                        <telerik:GridBoundColumn HeaderText="Calificación"  DataField="desCali"   HeaderStyle-Width="80px"  ItemStyle-Width="170px" ></telerik:GridBoundColumn>                                       

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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>
            </telerik:RadGrid>
        
        </div>    
        
        
                        
     </fieldset>
                      
 
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
