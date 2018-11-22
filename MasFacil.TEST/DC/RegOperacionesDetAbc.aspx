<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegOperacionesDetAbc.aspx.cs" Inherits="DC_RegOperacionesDetAbc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            GetRadWindow().close();
        }
    </script>


 

</head>
    
<body>
   
    <form id="form1" runat="server">



    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div align="right" style=" background-color:transparent; position:relative; width:100%;  text-align:right;" >
                    <asp:Panel ID="pnlBtnsAcciones" Width="100%"  CssClass="cspnlBtnsAcciones"  runat="server">

                         <table  id="tbBotones" runat="server"  border="0" style=" margin-left:5px; width:150px; text-align:left; background-color:transparent ;">
                            <tr style="height:15px;">
                                <td  id="dtrBtnCopiar" runat="server"  style=" width:55px; background-color:transparent;">  
                                    <telerik:RadImageButton ID="rBtnCopiar"   runat="server"   Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCopiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnCopiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCopiarHovered.png"  ToolTip="Copiar"  Text="" OnClick="rBtnCopia_Click" ></telerik:RadImageButton>
                                </td>
                            </tr>
                        </table>  

                    </asp:Panel>     
               </div>

     <fieldset style="">
                            <legend>Transacción Detalle</legend>
                        <div style="overflow:auto; width:433px; height:360px; margin-left:1px; background-color:Transparent;" >
                             


                                <table style=" background-color:Transparent;" cellspacing="0" cellpadding="0" >
                                     <tr>
                                         <td style="width:200px; background-color:transparent;">
                                            <telerik:RadLabel ID="rlblMoneda" runat="server" Text="Moneda" BackColor="Transparent"></telerik:RadLabel>
                                         </td>
                                         <td style="width: 205px; background-color:transparent; ">
                                             <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="200px" 
                                                                HighlightTemplatedItems="true"  
                                                              DropDownCssClass="cssRadComboBox" 
                                                              DropDownWidth="260px" 
                                                               Height="200px" >
                                                                    <HeaderTemplate>
                                                                        <table style="width: 140px" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td style="width: 80px;">
                                                                                        Clave
                                                                                    </td>
                                                                                    <td style="width: 170px;">
                                                                                        Descripción
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                         <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td style="width:45px" >
                                                                                    <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                                                </td>
                                                                                <td style="width: 170px;">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "monDes") %>
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
                             <asp:DataList ID="DataList1" runat="server" DataKeyField="columTrans"  >
                               <ItemTemplate>
                                <table style="cellspacing="0" cellpadding="0" >
                                     <tr style="height:26px;">
                                         <td style="width:200px; background-color:transparent;">
                                                <telerik:RadLabel ID="RadLabel1" runat="server" Text='<%# Eval("cptoConfDes") %>'  ></telerik:RadLabel><br />
                                         </td>
                                                <td  style="width: 205px; background-color:transparent; vertical-align:central;  " >
                                                    <telerik:RadTextBox ID="RadTextBox" runat="server"  Width="200px"
                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            FocusedStyle-CssClass="cssTxtFocused"
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                        OnTextChanged="CadenaAlineaValor_TextChanged"
                                                    >
                                                    </telerik:RadTextBox>
                                                    <telerik:RadComboBox ID="RadComboBox" runat="server" Width="200px" AutoPostBack="false"  
                                                                HighlightTemplatedItems="true" 
                                                                DropDownCssClass="cssRadComboBox"
                                                                DropDownWidth="260"   
                                                                Height="200px" Filter="StartsWith">
                                                        <HeaderTemplate>
                                                        <table style="width: 250px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 150px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 250px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                        <ItemTemplate   >
                                                         <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:150px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "Clave")%>
                                                                </td>
                                                                <td style="width: 250px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                      <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount"  />
                                                    </FooterTemplate>
                                                   </telerik:RadComboBox>
                                                    <telerik:RadNumericTextBox ID="RadNumericTextBox" runat="server"   Width="200px"
                                                        EnabledStyle-CssClass="cssTxtEnabled"
                                                        DisabledStyle-CssClass ="cssTxtEnabled"
                                                        HoveredStyle-CssClass="cssTxtHovered"
                                                        FocusedStyle-CssClass="cssTxtFocused"
                                                        InvalidStyle-CssClass="cssTxtInvalid">
                                                    </telerik:RadNumericTextBox>
                                                    <telerik:RadDatePicker ID="RadDatePicker" runat="server" Width="130px" AutoPostBack="false" ></telerik:RadDatePicker>

                                                    <telerik:RadCheckBox ID="RadCheckBox_TipoCaptura" runat="server" Text="RadCheckBox" Checked='<%# Eval("cptoTipCap") %>' Visible="false" ></telerik:RadCheckBox>
                                                    <telerik:RadCheckBox ID="RadCheckBox_ProgValida" runat="server" Text='<%# Eval("cptoConfProgCveVal") %>' Checked='<%# Eval("ProgCveValCheck") %>' Value='<%# Eval("cptoConfProgCveVal") %>' Visible="false"></telerik:RadCheckBox>
                                                    <telerik:RadCheckBox ID="RadCheckBox_Justificacion" runat="server" Text='<%# Eval("cptoConfRell") %>' Checked='<%# Eval("CheckJustIzq") %>' Value='<%# Eval("cptoConfRell") %>'  Visible="false"></telerik:RadCheckBox>
                                               </td>
                                    </tr>
                                </table>
                               </ItemTemplate>
                           </asp:DataList>



                               
                       </div>


                </fieldset>
                           <br />
                        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""   OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
                        </asp:Panel>  



                        <asp:HiddenField ID="hdfBtnAccionDet" runat="server" />
                        <asp:HiddenField ID="hdfPag_sOpe" runat="server" /> 
                     <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
                </telerik:RadWindowManager>



            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>

</body>
        
</html>
