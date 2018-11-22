<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoCptoDatoGenerales.aspx.cs" Inherits="DC_MttoCptoDatoGenerales" %>
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
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 167px;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            height: 28px;
        }
    </style>
    
</head>

<body>
    <form id="form2" runat="server">

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
            <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  OnClick="rBtnModificar_Click" ToolTip="Modificar"  Text="" Visible="false"  ></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" OnClick="rBtnLimpiar_Click"  Text=""></telerik:RadImageButton>    
        </asp:Panel>

        <div style="height:10px;">

        </div>
 
    
        <fieldset style=" display: block; text-align:left;" >   
            
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" text-align:left; background-color:transparent; width:710px" >
                    <tr style="height:18px;">
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel6" runat="server" Text="Concepto" width="120px"></telerik:RadLabel>

                        </td>
                        <td style=" width:230px; background-color:transparent;">
                                <telerik:RadNumericTextBox  ID="rTxtConcepto"
                                                            runat="server" Width="100px" 
                                                            DisabledStyle-CssClass ="cssTxtEnabled" 
                                                            EnabledStyle-CssClass="cssTxtEnabled" 
                                                            FocusedStyle-CssClass="cssTxtFocused"   
                                                            HoveredStyle-CssClass="cssTxtHovered"
                                                            InvalidStyle-CssClass="cssTxtInvalid"
                                                            MaxLength ="4"
                                                            MinValue="0"  >
                                  <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel10" runat="server" Text="Descripción"  ></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">
                            <telerik:RadTextBox ID="rTxtDes" width="200px" runat="server" 
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>

                    </tr>
                    <tr style="height:18px;">

                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel4" runat="server" Text="Abreviatura" width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">
                            <telerik:RadTextBox ID="rTxtAbreviatura" width="190px" runat="server" MaxLength="200"
                                EnabledStyle-CssClass="cssTxtEnabled"
                                DisabledStyle-CssClass ="cssTxtEnabled"
                                HoveredStyle-CssClass="cssTxtHovered"
                                FocusedStyle-CssClass="cssTxtFocused"
                                InvalidStyle-CssClass="cssTxtInvalid" 
                             ></telerik:RadTextBox>
                        </td>
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel9" runat="server" Text="Referencia Base" width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">
                             <telerik:RadComboBox ID="rCboRefBase" width="200px" runat="server" 
                                 HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"  OnSelectedIndexChanged="rCboRefBase_SelectedIndexChanged"
                                 DropDownCssClass="cssRadComboBox" AutoPostBack="True" >            
                                      <HeaderTemplate>
                                            <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                             <td style="width: 90px;">
                                                            Clave
                                                        </td>
                                                        <td style="width: 185px;">
                                                            Descripción
                                                        </td>
                                                    </tr>
                                                </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                                <table style="width: 275px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 90px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "contRefCve") %>
                                                        </td>
                                                        <td style="width: 185px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "contRefDes") %>
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
                </Table>
          
            </div>
        </fieldset>




        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
         <legend>Foliador del Concepto</legend>  

          

            <div style="width:100%; display:table; position:static; background-color:transparent;" >
            
                <table border="0" style=" text-align:left; background-color:transparent; width:710px" >
                    <tr style="height:18px;">
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel19" runat="server" Text="Manejo de folio" width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">                     
                             <telerik:RadComboBox ID="rCboManFol" width="190px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px" DropDownCssClass="cssRadComboBox" AutoPostBack="True" OnSelectedIndexChanged="rCboManFol_SelectedIndexChanged"  >            
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
                                                <table style="width: 170px;"  cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 170px;">
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
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel11" runat="server" Text="Foliador." width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">
                            
                                       <telerik:RadComboBox ID="rcboFoliador" runat="server" Width="200px"  
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioDescripcion") %>
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
                </Table>

            </div>
          </fieldset>          
            
            
            
            

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
         <legend>Asiento Contable</legend>                                   
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" text-align:left; background-color:transparent; width:710px" >
                    <tr style="height:18px;">
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel5" runat="server" Text="Manejo de folio." width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">                     
                            <telerik:RadComboBox ID="rCboAsientoCont" width="190px" runat="server" HighlightTemplatedItems="true" DropDownWidth="290px" Height="200px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" OnSelectedIndexChanged="rCboAsientoCont_SelectedIndexChanged" >            
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
                                            <table style="width: 170px;"  cellspacing="0" cellpadding="0">
                                                <tr>
                                                    </td>
                                                     <td style="width: 170px;">
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
                        <td style=" width:120px; background-color:transparent;">
                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Foliador." width="120px"></telerik:RadLabel>
                        </td>
                        <td style=" width:230px; background-color:transparent;">
                                  <telerik:RadComboBox ID="rcboFoliador_AsientoCont" runat="server" Width="200px"  
                                            HighlightTemplatedItems="true" AutoPostBack="true"
                                          DropDownCssClass="cssRadComboBox" 
                                          DropDownWidth="260px" 
                                           Height="200px"  >
                                                <HeaderTemplate>
                                                    <table style="width: 150px" cellspacing="0" cellpadding="0">
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
                                                            <td style="width:80px" >
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioCve")%>
                                                            </td>
                                                            <td style="width: 170px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "FolioDescripcion") %>
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
                </Table>


                 <table border="0" style=" text-align:left; background-color:transparent; width:710px" >
                    <tr>
                        <td style="width:350px; background-color:Transparent; ">
                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Elimina Transacciones" Width="155px"></telerik:RadLabel>
                            
                                       <telerik:RadButton RenderMode="Lightweight" ID="rBtnElimTrans" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" OnCheckedChanged="rBtnElimTrans_CheckedChanged"
                                            AutoPostBack="false" Width="50px">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState  Text="Si" CssClass="Checked" Value="1" ></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState   Text="No" CssClass="notChecked"  Value="2"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Tipo Concepto" Width="120px"></telerik:RadLabel>
                                <asp:RadioButton ID="TipoCptoCrea" runat="server" GroupName="TipoCpto" Text="Crea" Checked="true" Width="60px" BackColor="Transparent" Enabled="false"  />
                                <asp:RadioButton ID="TipoCptoAplica" runat="server" GroupName="TipoCpto" Text="Aplica"   Enabled="false"  />
                            
                        </td>
                    </tr>
                </table>


            </div>
        </fieldset>

        <fieldset style="  margin-top:5px;   display: block; text-align:left;" > 
         <legend>CFDI</legend>                                   
            <div style="width:100%; display:table; position:static; background-color:transparent;" >

                <table border="0" style=" text-align:left; background-color:transparent; width:710px" >
                    <tr style="height:18px;">
                        <td style=" width:120px; background-color:transparent;">
                               <asp:CheckBox ID="ckbGeneraCFDI" runat="server" Text="Genera CFDI" Checked="false"   />  
                        </td>
                        <td style=" width:230px; background-color:transparent;">                     
     
                        </td>
                        <td style=" width:120px; background-color:transparent;">
 
                        </td>
                        <td style=" width:230px; background-color:transparent;">

                        </td>
                    </tr>
                </Table>


            </div>
        </fieldset>


        <div style="width:100%; display:table; position:static; background-color:transparent;">
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
            </asp:Panel>
        </div>

                <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                  </div>
                   </ContentTemplate>
    </asp:UpdatePanel>  
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>