<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCtaContableDatosGrales.aspx.cs" Inherits="DC_MttoCtaContableDatosGrales" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

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
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar" Text="" Visible="false" OnClick="rBtnModificar_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click" Text="" ></telerik:RadImageButton>
            </asp:Panel>                              

            <div style=" width:100%; display:table;  ">

                <fieldset style="  margin-top:10px; width:47%;    display: block;  float:left; text-align:left;" >   
                <legend>Cuenta</legend>
                    <div style="width:100%; display:table;  float:left;position:static; background-color:transparent;" align="center" >

                        <table border="0" style=" width:350px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:90px; background-color:transparent;">
                                     <telerik:RadLabel ID="rLblCodigo" runat="server">Codigo</telerik:RadLabel> 
                                </td>
                                <td style=" width:260px; background-color:transparent;">
                                     <telerik:RadTextBox ID="rTxtCodigo" Runat="server" Width="245px"
                                                          EnabledStyle-CssClass="cssTxtEnabled"
                                                          DisabledStyle-CssClass="cssTxtEnabled"
                                                          HoveredStyle-CssClass="cssTxtHovered"
                                                          FocusedStyle-CssClass="cssTxtFocused"
                                                          InvalidStyle-CssClass="cssTxtInvalid"
                                     ></telerik:RadTextBox>                                   </td>
                            </tr>
                            <tr style="height:18px;">
                                <td style=" width:90px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel1" runat="server">Descripcion</telerik:RadLabel> 
                                </td>
                                <td style=" width:260px; background-color:transparent;">
                                     <telerik:RadTextBox ID="rTxtDescripcion" Runat="server" Width="245px"
                                                          EnabledStyle-CssClass="cssTxtEnabled"
                                                          DisabledStyle-CssClass="cssTxtEnabled"
                                                          HoveredStyle-CssClass="cssTxtHovered"
                                                          FocusedStyle-CssClass="cssTxtFocused"
                                                          InvalidStyle-CssClass="cssTxtInvalid"
                                     ></telerik:RadTextBox>   
                                </td>
                            </tr>
                            <tr style="height:18px;">
                                <td style=" width:90px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel2" runat="server">Abreviatura</telerik:RadLabel> 
                                </td>
                                <td style=" width:260px; background-color:transparent;">
                                     <telerik:RadTextBox ID="rTxtAbreviatura" Runat="server" Width="150px"
                                                          EnabledStyle-CssClass="cssTxtEnabled"
                                                          DisabledStyle-CssClass="cssTxtEnabled"
                                                          HoveredStyle-CssClass="cssTxtHovered"
                                                          FocusedStyle-CssClass="cssTxtFocused"
                                                          InvalidStyle-CssClass="cssTxtInvalid"
                                     ></telerik:RadTextBox>   
                                </td>
                            </tr>
                        </table>
                          
                    </div>
                </fieldset>

                <fieldset style="  margin-top:10px; width:47%;    display: block;  float:right; text-align:left;" >   
                <legend>Datos de la Cuenta</legend>
                    <div style="width:100%; display:table;  float:left;position:static; background-color:transparent;" align="center" >

                        <table border="0" style=" width:350px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:100px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel3" runat="server">Naturaleza</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnNat_Acredora" runat="server" GroupName="rBtnNat" Text="Acreedora"  Checked="true"/>
                                    <asp:RadioButton ID="rBtnNat_Deudora"  runat="server" GroupName="rBtnNat" Text="Deudora"/>
                                </td>
                            </tr>
                            <tr style="height:18px;">
                                <td style=" width:100px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel4" runat="server">Tipo de Cta</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboTipoCta" runat="server" Width="240px" 
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="240px" Height="240px" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="rCboTipoCta_SelectedIndexChanged">
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
                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContTip") %>
                                                            </td>
                                                            <td style="width:180px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContTipDes") %>
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
                            <tr style="height:18px;">
                                <td style=" width:100px; background-color:transparent;">
                                     <telerik:RadLabel ID="RadLabel5" runat="server">Subtipo Cta</telerik:RadLabel> 
                                </td>
                                <td style=" width:250px; background-color:transparent;">

                                    <telerik:RadComboBox ID="rCboSubTipoCta" runat="server" Width="240px" 
                                        HighlightTemplatedItems="true"
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="240px" Height="240px" 
                                        AutoPostBack="True" >
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
                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContSubTip") %>
                                                            </td>
                                                            <td style="width:180px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContSubDes") %>
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
            
            </div> 


            <fieldset >   
            <legend>Manejo de la Cuenta</legend>
                <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                        <table border="0" style=" width:730px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:150px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel6" runat="server">Aplica Movimientos</telerik:RadLabel> 
                                </td>
                                <td style=" width:100px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnApliMov_Si" runat="server" GroupName="rBtnApliMov" Text="Si" Checked="true"/>
                                    <asp:RadioButton ID="rBtnApliMov_No" runat="server" GroupName="rBtnApliMov" Text="No" />
                                </td>
                                <td style=" width:150px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel7" runat="server">Integración Saldos</telerik:RadLabel> 
                                </td>
                                <td style=" width:100px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnIntSaldo_Si" runat="server" GroupName="rBtnIntSaldo" Text="Si" Checked="true"/>
                                    <asp:RadioButton ID="rBtnIntSaldo_No" runat="server" GroupName="rBtnIntSaldo" Text="No" />
                                </td>
                                <td style=" width:130px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel8" runat="server">Saldos Promedio</telerik:RadLabel>
                                </td>
                                <td style=" width:100px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnSaldoProm_Si" runat="server" GroupName="rBtnSaldoProm" Text="Si" Checked="true"/>
                                    <asp:RadioButton ID="rBtnSaldoProm_No" runat="server" GroupName="rBtnSaldoProm" Text="No" />
                                </td>
                            </tr>
                        </table>

                        <table border="0" style=" width:730px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:150px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel9" runat="server">Centro de Costos</telerik:RadLabel> 
                                </td>
                                <td style=" width:100px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnCC_Si" runat="server" GroupName="rBtnCC" Text="Si" Checked="true"/>
                                    <asp:RadioButton ID="rBtnCC_No" runat="server" GroupName="rBtnCC" Text="No" />
                                </td>
                                <td style=" width:145px; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel10" runat="server">Tipo de Cambio</telerik:RadLabel> 
                                </td>
                                <td style=" width:335px; background-color:transparent;">
                                    <asp:RadioButton ID="rBtnTipoCambio_Corr" runat="server" GroupName="rBtnTipoCambio" Text="Corriente" Checked="true"/>
                                    <asp:RadioButton ID="rBtnTipoCambio_Prom" runat="server" GroupName="rBtnTipoCambio" Text="Promedio" />
                                    <asp:RadioButton ID="rBtnTipoCambio_Hist" runat="server" GroupName="rBtnTipoCambio" Text="Historico" />
                                </td>
                            </tr>
                        </table>
                </div>
            </fieldset>
   
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"   runat="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
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
