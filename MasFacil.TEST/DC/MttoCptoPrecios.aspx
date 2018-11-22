<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoPrecios.aspx.cs" Inherits="DC_MttoCptoPrecios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
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
        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton> 
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >   
            <div style="width:100%; display:table; position:static; background-color:transparent;" >
                <table border="0" style=" text-align:left; background-color:transparent;" >
                    <tr style="height:18px;">
                        <td style=" width:100%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="Concepto"></telerik:RadLabel> &nbsp;
                            <telerik:RadLabel ID="rLblCptoId" runat="server" Text=""></telerik:RadLabel>
                            <telerik:RadLabel ID="rLblcptoDes" runat="server" Text=""></telerik:RadLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>


        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Valores</legend>  
            <div align="center" style=" width:100%; display:table; background-color:transparent;" class="auto-style3" >
                <table border="0" style=" width:450px; text-align:left; background-color:transparent;" >
                    <tr >
                        <td style=" width:200px; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Valida existencias en:"></telerik:RadLabel> 
                        </td>
                        <td style=" width:250px; background-color:transparent;"> 
                            <asp:RadioButton ID="rbvalExisApli" runat="server" Checked="true" GroupName="valExis" Text="Aplica" />&nbsp;&nbsp;
                            <asp:RadioButton  ID="rbvalExisCapt" runat="server" Checked="false" GroupName="valExis" Text="Captura" />
                        </td>
                    </tr>
                </table>
                <table border="0" style=" width:450px; text-align:left; background-color:transparent;" >
                    <tr >
                        <td style=" width:200px; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Determina Otro Precio:"></telerik:RadLabel> 
                        </td>
                        <td style=" width:250px; background-color:transparent;"> 
                            <asp:RadioButton ID="rbOtroPrecSi" runat="server" Checked="true" GroupName="OtroPrec" Text="Si" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton  ID="rbOtroPrecNo" runat="server" Checked="false" GroupName="OtroPrec" Text="No" />
                        </td>
                    </tr>
                </table>
                <table border="0" style=" width:450px; text-align:left; background-color:transparent;" >
                    <tr >
                        <td style=" width:200px; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Guardar en factor:"></telerik:RadLabel> 
                        </td>
                        <td style=" width:250px; background-color:transparent; "> 
                            <telerik:RadComboBox ID="rCboFactores" width="200px" runat="server" HighlightTemplatedItems="true" 
                                DropDownWidth="240px" Height="150px"
                                                     DropDownCssClass="cssRadComboBox"  >            
                                  <HeaderTemplate>
                                        <table style="width: 220px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                     <td style="width: 80px;">
                                                        Secuencia
                                                    </td>
                                                    <td style="width: 140px;">
                                                        Descipción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 220px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 80px;">
                                                   <%# DataBinder.Eval(Container.DataItem, "cptoConfSec") %>
                                                    </td>
                                                     <td style="width: 140px;">
                                                     <%# DataBinder.Eval(Container.DataItem, "cptoConfDes") %>
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





        <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>
        </div>
 
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </ContentTemplate>
    </asp:UpdatePanel>


    </form>
</body>
</html>
