<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoClieDatosGrales.aspx.cs" Inherits="DC_MttoClieDatosGrales" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>

    <script type="text/javascript">

        

        function setMaxLenghtClie(sender, eventArg){
            var newlenght;
            var valCliente;
            valCliente = document.getElementById("rTxtCliente").value;
            newlenght = 10 - parseInt(valCliente.length);
            document.getElementById("rTxtSubClie").setAttribute("MaxLength", newlenght);
        }

        function setMaxLenghtSubClie(sender, eventArg) {
            var newlenght;
            var valCliente;
            valCliente = document.getElementById("rTxtSubClie").value;
            
            newlenght = 10 - parseInt(valCliente.length);
            
            document.getElementById("rTxtCliente").setAttribute("MaxLength", newlenght);
        }

        function formatClie(sender, eventArg) {
            
            var originalValue;
            var longClie;
            var posClie;
            var charFormat;
            var newValue;
            
            originalValue = document.getElementById("rTxtCliente").value;
            longClie = document.getElementById("hdfLongClie").value;
            posClie = document.getElementById("hdfPosClie").value;
            charFormat = document.getElementById("hdfCharFormat").value;

            intLongClie = parseInt(longClie);

            if (posClie == "2") {
                newValue = padding_left(originalValue, charFormat, intLongClie);
            } else if (posClie == "1") {
                newValue = padding_right(originalValue, charFormat, intLongClie);
            }
            else {
                return false;
            }

            document.getElementById("rTxtCliente").value = newValue;
            setMaxLenghtClie();
            
        }


        function formatSubClie(sender, eventArg) {

            var originalValue;
            var longSubClie;
            var posSubClie;
            var charFormat;
            var newValue;

            originalValue = document.getElementById("rTxtSubClie").value;
            longSubClie = document.getElementById("hdfLongSubClie").value;
            posSubClie = document.getElementById("hdfPosSubClie").value;
            charFormat = document.getElementById("hdfCharFormat").value;

            intLongSubClie = parseInt(longSubClie);

            if (posSubClie == "2") {
                newValue = padding_left(originalValue, charFormat, intLongSubClie);
            } else if (posSubClie == "1") {
                newValue = padding_right(originalValue, charFormat, intLongSubClie);
            }
            else {
                return false;
            }

            document.getElementById("rTxtSubClie").value = newValue;
            setMaxLenghtSubClie();

        }





        // left padding s with c to a total of n chars
        function padding_left(s, c, n) {
            if (!s || !c || s.length >= n) {
                return s;
            }
            var max = (n - s.length) / c.length;
            for (var i = 0; i < max; i++) {
                s = c + s;
            }
            return s;
        }

        // right padding s with c to a total of n chars
        function padding_right(s, c, n) {
            if (!s || !c || s.length >= n) {
                return s;
            }
            var max = (n - s.length) / c.length;
            for (var i = 0; i < max; i++) {
                s += c;
            }
            return s;
        }

    </script>

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
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
    <div>
            

            


        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="true" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>
 

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >   
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="Cliente" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox Value="" Text="" ID="rTxtCliente" Width="180px" runat="server" MaxLength="10" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" >
                                <ClientEvents OnBlur="formatClie" />
                            </telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="rLblSubClie" runat="server" Text="Subcliente" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox Value="" Text="" ID="rTxtSubClie" Width="190px" runat="server" MaxLength="9"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" >
                                <ClientEvents OnBlur="formatSubClie" />
                            </telerik:RadTextBox>
                        </td>

                    </tr>

                    <tr >
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel9" runat="server" Text="Nombre" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtNombre" width="180px" runat="server" MaxLength="200"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel10" runat="server" Text="Abreviatura" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtAbr" width="190px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>

                        
                    </tr>

                    <tr>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel11" runat="server" Text="RFC/ID" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtReg" width="180px" runat="server" MaxLength="20"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        
                        </td>

                        <td style=" width:50%; background-color:transparent;">
                        </td>
    
                    </tr>

                            
                </table>
            </div>
        </fieldset>

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >  
            <legend>Otros Datos</legend>                 
            <div style="width:100%;   height:32px; overflow:auto; background-color:transparent;" >
                <asp:DataList ID="DataListOtrosDatos" runat="server" Enabled="false"  DataKeyField="otroDatCve">
                    <ItemTemplate>
                        <table border="0" style=" width:100%; text-align:left; background-color:transparent;">
                            <tr>
                                <td style=" width:120px;  background-color:transparent;">
                                <telerik:RadLabel ID="RadLabel19" runat="server" Text='<%# Eval("otroDatDes") %>'  ></telerik:RadLabel>

                                </td>
                                <td style="  width:180px; background-color:transparent;">
                        <telerik:RadTextBox ID="txt_OtrosDatos" runat="server" Width="180px"  
                                                EnabledStyle-CssClass="cssTxtEnabled"
                                                DisabledStyle-CssClass ="cssTxtEnabled"  
                                                HoveredStyle-CssClass="cssTxtHovered"
                                                FocusedStyle-CssClass="cssTxtFocused" Text='<%# Eval("OtroDatVal") %>'
                                                InvalidStyle-CssClass="cssTxtInvalid">
                        </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                        
                            

                    </ItemTemplate>
                </asp:DataList>
            </div>
        </fieldset>




            <div style="margin-top:10px;">
                <telerik:RadTabStrip RenderMode="Lightweight" Orientation="HorizontalTop" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Silk">
                    <Tabs>
                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Fiscal" Value="1"></telerik:RadTab>
                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Notificaciones" Font-Size="Medium"  Value="2"></telerik:RadTab>
                        <telerik:RadTab ImageUrl="../Images/direccion.png" Text="Expedición" Font-Size="Medium"  Value="3"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip> 
            </div>

            <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0" CssClass="outerMultiPage" BackColor="#E6E6E6" BorderColor="Black" BorderWidth="1px" Width ="655px" >
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    
                   
                    
                    
                <div style="width:100%; display:table; position:static; background-color:transparent;">
            
                    

                    
      <%--  <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Dirección</legend>  
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >--%>

                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr>

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="País" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPaises" width="180px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                  DropDownCssClass="cssRadComboBox" AllowCustomText="true"
                                  OnSelectedIndexChanged="rCboPaises_SelectedIndexChanged" AutoPostBack="true" >            
                                  <HeaderTemplate>
                                        <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "paisDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>


                            
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Estado" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboEstado"  width="190px" DropDownWidth="190px" AllowCustomText="true"
                                 Height="200px" runat="server" HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox" AutoPostBack="True" 
                                OnSelectedIndexChanged="rCboEstado_SelectedIndexChanged" >            
                                <HeaderTemplate>
                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "entFDes") %>
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

                    <tr>

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Del/Muni" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPoblacion" width="180px" DropDownWidth="290px" Height="200px"  AllowCustomText="true"
                                runat="server" HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox" InvalidStyle-CssClass="cssTxtInvalid"  AutoPostBack="True" >            
                                <HeaderTemplate>
                                        <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel18" runat="server" Text="Colonia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtColonia" width="190px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr >

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel12" runat="server" Text="Calle" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtCalle" width="180px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="No Exterior" width="110px"></telerik:RadLabel>
                            <%--<telerik:RadNumericTextBox Width="60px" ID="rTxtNExt" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="rTxtNExt" Width="60px" runat="server" MaxLength="30"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                            <telerik:RadLabel ID="RadLabel5" runat="server" Text="No Int" width="50px"></telerik:RadLabel>
                           <%-- <telerik:RadNumericTextBox width="58px" ID="rTxtNInt" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="rTxtNInt" Width="60px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        
                            
                        </td>

                    </tr>

                    <tr >

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel13" runat="server" Text="Calles Aled." width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox width="180px"  ID="rTxtCllsA" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel14" runat="server" Text="Codigo Postal" width="110px"></telerik:RadLabel>
                            <%--<telerik:RadNumericTextBox width="190px" ID="rTxtCP" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                             <telerik:RadTextBox width="190px"  ID="rTxtCP" runat="server" MaxLength="10"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

                        </td>

                    </tr>

                    <tr >
                        <td style=" width:500px; background-color:transparent;" colspan="2">
                            <telerik:RadLabel ID="RadLabel15" runat="server" Text="Referencia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtRef"  width="508px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>

                        <td style=" width:50%; background-color:transparent;">
                        </td>


                    </tr>

                </table>
        <%--    </div>--%>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel16" runat="server" Text="Teléfono 1" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="rTxtTel1" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel17" runat="server" Text="Teléfono 2" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="rTxtTel2" width="190px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                    </tr>
            </table>     

            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                    <tr >
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel8" runat="server" Text="Fax" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="rTxtFax" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadTextBox ID="iddom" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
       <%--  </fieldset>--%>
            </div>
          </telerik:RadPageView>



                <%--NOTIFICACIONES--%>
            <telerik:RadPageView runat="server" ID="RadPageView2">
        <div style="width:100%; display:table; position:static; background-color:transparent;">
      <%--  <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Dirección</legend>  
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >--%>

                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel7" runat="server" Text="País" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPaisNotif" width="180px" runat="server" HighlightTemplatedItems="true"
                                 DropDownWidth="290px" Height="200px" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  OnSelectedIndexChanged="rCboPaisNotif_SelectedIndexChanged" AutoPostBack="True" >            
                                  <HeaderTemplate>
                                        <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "paisDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                            
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel19" runat="server" Text="Estado" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboEntidadFedNotif" runat="server" OnSelectedIndexChanged="rCboEntidadFedNotif_SelectedIndexChanged" AutoPostBack="True"
                                                     HighlightTemplatedItems="true" AllowCustomText="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="190px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>

                           
                        </td>
                            
                    </tr>

                    <tr style="height:18px;">
                        
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel20" runat="server" Text="Del/Muni" width="110px"></telerik:RadLabel>
                              <telerik:RadComboBox ID="rCboProvinciaNotif"  width="180px" DropDownWidth="190px" Height="200px" runat="server" AllowCustomText="true"
                                 HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox"  AutoPostBack="True" >            
                                <HeaderTemplate>
                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel21" runat="server" Text="Colonia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_coloniaNotif" width="190px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel22" runat="server" Text="Calle" Width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_calleNotif" width="180px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel23" runat="server" Text="No Exterior" Width="110px"></telerik:RadLabel>
                            <%--<telerik:RadNumericTextBox width="60px" ID="rdNumericExteriorNotif" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                             <telerik:RadTextBox ID="rdNumericExteriorNotif"  Width="60px" runat="server" MaxLength="30"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                            <telerik:RadLabel ID="RadLabel24" runat="server" Text="No Int" Width="50px"></telerik:RadLabel>
                           <%-- <telerik:RadNumericTextBox width="58px" ID="rdNumericInteriorNotif" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="rdNumericInteriorNotif" Width="60px" runat="server" MaxLength="30"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        
                            
                        </td>

                    </tr>

                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel25" runat="server" Text="Calles Aled." width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox width="180px"  ID="txt_callesAleNotif" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel26" runat="server" Text="Codigo Postal" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox width="190px" ID="rdNumericCodigoPostalNotif" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>

                    </tr>

                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;"  colspan="2">
                            <telerik:RadLabel ID="RadLabel27" runat="server" Text="Referencia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_referenciasNotif"  width="508px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>

                        <td style=" width:50%; background-color:transparent;">
                        </td>


                    </tr>

                </table>
        <%--    </div>--%>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel28" runat="server" Text="Teléfono 1" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_telefono1Notif" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel29" runat="server" Text="Teléfono 2" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_telefono2Notif" width="190px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                    </tr>
            </table>     

            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel30" runat="server" Text="Fax" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_faxNotif" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadTextBox ID="iddomNotif" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox>    
                        </td>
                    </tr>
                </table>
            </div>
       <%--  </fieldset>--%>
            </div>
          </telerik:RadPageView>


            <%--EXPEDICION--%>
  <telerik:RadPageView runat="server" ID="RadPageView3">
        <div style="width:100%; display:table; position:static; background-color:transparent;">
      <%--  <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            <legend>Dirección</legend>  
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >--%>

                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr >

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel31" runat="server" Text="País" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPaisExped" width="180px" runat="server" AllowCustomText="true"
                                HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" OnSelectedIndexChanged="rCboPaisExped_SelectedIndexChanged" >            
                                  <HeaderTemplate>
                                        <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "paisDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                            
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel32" runat="server" Text="Estado" width="110px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboEntidadFedExped" runat="server" AutoPostBack="True" AllowCustomText="true"
                                OnSelectedIndexChanged="rCboEntidadFedExped_SelectedIndexChanged"
                                                     HighlightTemplatedItems="true"
                                                     DropDownCssClass="cssRadComboBox"  
                                                     Width="190px" DropDownWidth="290px" Height="200px">
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                                 </telerik:RadComboBox>

                           
                        </td>
                            
                    </tr>

                    <tr style="height:18px;">
                        
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel33" runat="server" Text="Del/Muni" width="110px"></telerik:RadLabel>
                              <telerik:RadComboBox ID="rCboProvinciaExped"  width="180px" DropDownWidth="190px" Height="200px" runat="server" AllowCustomText="true"
                                 HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox"  AutoPostBack="True"  >            
                                <HeaderTemplate>
                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 190px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "provDes") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                        
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel34" runat="server" Text="Colonia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_coloniaExped" width="190px" runat="server" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel35" runat="server" Text="Calle" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_calleExped" width="180px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel36" runat="server" Text="No Exterior" width="110px"></telerik:RadLabel>
                          <%--  <telerik:RadNumericTextBox width="60px" ID="rdNumericExteriorExped" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="rdNumericExteriorExped" Width="60px" runat="server" MaxLength="30"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                            <telerik:RadLabel ID="RadLabel37" runat="server" Text="No Int" width="50px"></telerik:RadLabel>
                           <%-- <telerik:RadNumericTextBox width="58px" ID="rdNumericInteriorExped" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                            <telerik:RadTextBox ID="rdNumericInteriorExped" Width="60px" runat="server" MaxLength="30"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        
                            
                        </td>

                    </tr>

                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel38" runat="server" Text="Calles Aled." width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox width="180px"  ID="txt_callesAleExped" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel39" runat="server" Text="Codigo Postal" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox width="190px" ID="rdNumericCodigoPostalExped" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>

                    </tr>

                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;"  colspan="2">
                            <telerik:RadLabel ID="RadLabel40" runat="server" Text="Referencia" width="110px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="txt_referenciasExped"  width="508px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>

                        <td style=" width:50%; background-color:transparent;">
                        </td>


                    </tr>

                </table>
        <%--    </div>--%>
            <div style="width:100%; display:table; position:static; background-color:transparent;">
                <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel41" runat="server" Text="Teléfono 1" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_telefono1Exped" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel42" runat="server" Text="Teléfono 2" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_telefono2Exped" width="190px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                    </tr>
            </table>     

            <table runat="server" visible="false" border="0"  cellpadding="0" cellspacing="0" style=" width:720px; text-align:left; background-color:transparent ;">

                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel43" runat="server" Text="Fax" width="110px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="txt_faxExped" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                                     <telerik:RadTextBox ID="iddomExped" runat="server" Width="200px"  EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled" Enabled="false"
                                            HoveredStyle-CssClass="cssTxtHovered"  Visible="false"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid"></telerik:RadTextBox> 
                        </td>
                    </tr>


                </table>
            </div>
       <%--  </fieldset>--%>
            </div>
          </telerik:RadPageView>


         </telerik:RadMultiPage>


        <fieldset  runat="server" style=" text-align:left; float:left;  width:47%; height:70px; display: block;"  >
            <legend>Referencias</legend>
            <div style=" overflow:auto; width:305px; height:90px; background-color:transparent ;" id="divClave"  >
                <asp:DataList ID="dt_referencias" runat="server" DataKeyField="revasec">
                    <ItemTemplate>
                        <telerik:RadLabel ID="lbradref" runat="server" Width="110px" Text='<%# Eval("revaDes") %>' ></telerik:RadLabel>  
                        <telerik:RadTextBox ID="txt_ref" runat="server" Text='<%# Eval("revaValRef") %>' Width="180px" 
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" 
                            ></telerik:RadTextBox>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </fieldset>

        <fieldset style=" text-align:left; float:right; width:47%; height:70px; display: block; ">
            <legend>Variables</legend>
            <div style=" overflow:auto; width:305px; height:90px; background-color:transparent ;" >
                <asp:DataList ID="dt_variables" runat="server" DataKeyField="revasec" >
                    <ItemTemplate>
                        <telerik:RadLabel ID="lbradvar" runat="server" Width="100px" Text='<%# Eval("revaDes") %>' ></telerik:RadLabel>  
                        <telerik:RadNumericTextBox style=" text-align:right;" ID="txt_var" runat="server" Text='<%# Eval("revaValVar") %>' Width="180px"
                            EnabledStyle-CssClass="cssTxtEnabled"
                            DisabledStyle-CssClass ="cssTxtEnabled"
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" >

                        </telerik:RadNumericTextBox>
                    
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </fieldset>
  

        <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>
        </div>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />

        <!-- Hidden Fields For ClientFormat-->
        <asp:HiddenField ID="hdfLongClie" runat="server" />
        <asp:HiddenField ID="hdfLongSubClie" runat="server" />
        <asp:HiddenField ID="hdfPosClie" runat="server" />
        <asp:HiddenField ID="hdfPosSubClie" runat="server" />
        <asp:HiddenField ID="hdfCharFormat" runat="server" />
        <asp:HiddenField ID="hdfTotLong" runat="server" />


        </div>
        </ContentTemplate>
     </asp:UpdatePanel>
        
            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>
    </form>

</body>
</html>
