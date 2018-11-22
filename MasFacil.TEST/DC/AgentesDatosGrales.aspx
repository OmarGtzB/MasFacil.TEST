<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentesDatosGrales.aspx.cs" Inherits="DC_AgentesDatosGrales" %>
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
            valCliente = sender.get_value();
            newlenght = 10 - parseInt(valCliente.length);
            document.getElementById("rTxtSubClie").setAttribute("MaxLength", newlenght);
        }

        function setMaxLenghtSubClie(sender, eventArg) {
            var newlenght;
            var valCliente;
            valCliente = sender.get_value();
            
            newlenght = 10 - parseInt(valCliente.length);
            
            document.getElementById("rTxtCliente").setAttribute("MaxLength", newlenght);
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="false" ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>

        <div style="height:10px;">

        </div>


        <fieldset style="  margin-top:5px;   display: block; text-align:left;" >   
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="Clave" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox Value="" Text="" ID="rTxtAgente" Width="100px" runat="server" MaxLength="20"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" >
                                
                            </telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel10" runat="server" Text="Abreviatura" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtAbr" width="190px" runat="server" MaxLength="64"
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
                            <telerik:RadLabel ID="RadLabel9" runat="server" Text="Nombre" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtNombre" width="180px" runat="server" MaxLength="200"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">

                            <telerik:RadLabel ID="RadLabel7" runat="server" Text="Tipo Agente" width="120px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboAgentes" width="190px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" >            
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
                        
                    </tr>
        
                </table>
            </div>
        </fieldset>




        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel19" runat="server" Text="¿Es empleado de la compañía?" width="200px"></telerik:RadLabel>

                            <asp:RadioButton ID="rBtnTrueE" runat="server" GroupName="isEmployee" Text="Si" Checked="true" OnCheckedChanged="rBtnTrueE_Click" AutoPostBack="true"/>
                            <asp:RadioButton ID="rBtnFalseE" runat="server" GroupName="isEmployee" Text="No" OnCheckedChanged="rBtnFalseE_Click" AutoPostBack="true"/>
                            
                        </td>

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel11" runat="server" Text="Trabajador" width="120px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboTrabajador" width="190px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" >            
                                  <HeaderTemplate>
                                        <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 75px;">
                                                        Id
                                                    </td>
                                                    <td style="width: 275px;">
                                                        Nombre
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 75px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "empleado") %>
                                                    </td>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "nombre") %>
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

          <fieldset style="  margin-top:5px;   display: block; text-align:left;" >  
            <legend>Otros Datos</legend>                 
            <div style="width:100%;   height:32px; overflow:auto; background-color:transparent;" >
                <asp:DataList ID="DataListOtrosDatos" runat="server" Enabled="false"  DataKeyField="otroDatCve">
                    <ItemTemplate>
                        <table border="0" style=" width:100%; text-align:left; background-color:transparent;">
                            <tr>
                                <td style=" width:130px;  background-color:transparent;">
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



        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
            
            <legend>Dirección</legend>  
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;"> 
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="País" width="120px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPaises" width="180px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox"  OnSelectedIndexChanged="rCboPaises_SelectedIndexChanged" AutoPostBack="True" AllowCustomText="true" >            
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
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Estado" width="120px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboEstado"  width="190px" DropDownWidth="220px" Height="200px" runat="server" HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox" OnSelectedIndexChanged="rCboEstado_SelectedIndexChanged" AutoPostBack="True"  AllowCustomText="true">            
                                <HeaderTemplate>
                                        <table style="width: 220px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 265px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 275px;">
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

                    <tr style="height:18px;">

                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Del/Muni" width="120px"></telerik:RadLabel>
                            <telerik:RadComboBox ID="rCboPoblacion" width="180px" DropDownWidth="290px" Height="200px"  runat="server" HighlightTemplatedItems="true" DropDownCssClass="cssRadComboBox" InvalidStyle-CssClass="cssTxtInvalid"  AutoPostBack="True" AllowCustomText="true" >            
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
                            <telerik:RadLabel ID="RadLabel18" runat="server" Text="Colonia" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtColonia" width="190px" runat="server" MaxLength="64"
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
                            <telerik:RadLabel ID="RadLabel12" runat="server" Text="Calle" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtCalle" width="180px" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <%-- NumberFormat-DecimalDigits="0"--%>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="No Ext" width="120px"></telerik:RadLabel>
                        <%--    <telerik:RadNumericTextBox width="60px" ID="rTxtNExt" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
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
                            <telerik:RadLabel ID="RadLabel5" runat="server" Text="No Int" width="55px"></telerik:RadLabel>
                            <%--<telerik:RadNumericTextBox width="65px" ID="rTxtNInt" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>--%>
                             <telerik:RadTextBox ID="rTxtNInt" Width="60px" runat="server" MaxLength="30"
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
                            <telerik:RadLabel ID="RadLabel13" runat="server" Text="Calles Aledañas" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox width="180px"  ID="rTxtCllsA" runat="server" MaxLength="50"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>

                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel14" runat="server" Text="Codigo Postal" width="120px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox width="190px" ID="rTxtCP" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>

                    </tr>

                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel15" runat="server" Text="Referencia" width="120px"></telerik:RadLabel>
                            <telerik:RadTextBox ID="rTxtRef"  width="180px" runat="server" MaxLength="64"
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


                <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel16" runat="server" Text="Teléfono 1" width="120px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="rTxtTel1" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="4" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel17" runat="server" Text="Teléfono 2" width="120px"></telerik:RadLabel>
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

                <table runat="server"  visible ="false" border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                    <tr style="height:18px;">
                        <td style=" width:50%; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel8" runat="server" Text="Fax" width="120px"></telerik:RadLabel>
                            <telerik:RadNumericTextBox ID="rTxtFax" width="180px" runat="server" NumberFormat-GroupSeparator="" NumberFormat-GroupSizes="8" NumberFormat-DecimalDigits="0" MaxLength="64"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:50%; background-color:transparent;">
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>

        <fieldset  runat="server" style=" text-align:left; float:left;  width:47%; height:60px; display: block;"  >
            <legend>Referencias</legend>
            <div style=" overflow:auto; width:100%; height:60px; background-color:transparent ;" id="divClave"  >
                <asp:DataList ID="dt_referencias" runat="server" DataKeyField="revasec">
                    <ItemTemplate>
                        <telerik:RadLabel ID="lbradref" runat="server" Width="120px" Text='<%# Eval("revaDes") %>' ></telerik:RadLabel>  
                        <telerik:RadTextBox ID="txt_ref" runat="server" Text='<%# Eval("revaValRef") %>' Width="190px" 
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

        <fieldset style=" text-align:left; float:right; width:48%; height:60px; display: block; ">
            <legend>Variables</legend>
            <div style=" overflow:auto; width:100%; height:60px; background-color:transparent ;" >
                <asp:DataList ID="dt_variables" runat="server" DataKeyField="revasec" >
                    <ItemTemplate>
                        <telerik:RadLabel ID="lbradvar" runat="server" Width="115px" Text='<%# Eval("revaDes") %>' ></telerik:RadLabel>  
                        <telerik:RadNumericTextBox style=" text-align:right;" ID="txt_var" runat="server" Text='<%# Eval("revaValVar") %>' Width="190px"
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
             </ContentTemplate>
    </asp:UpdatePanel> 

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
