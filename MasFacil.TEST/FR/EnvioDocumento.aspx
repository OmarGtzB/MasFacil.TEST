<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnvioDocumento.aspx.cs" Inherits="FR_EnvioDocumento" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
    <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

    <script>

        function myFunction() {
            var input = '{ "api_key":"79cef7b5b162a928c24206cc26ae1279","id_user":"589ccee30b212ab4278b923d"}';
            var endpointAddress = "https://sync.paybook.com/v1";
            var url = endpointAddress + "/sessions?api_key=79cef7b5b162a928c24206cc26ae1279";
            $.ajax({
                type: 'POST',
                url: url,
                contentType: 'application/json',
                data: input,
                success: function(result) {
                    //alert(JSON.stringify(result));

                    var result = JSON.stringify(result)

                    $("#jsonPaybook").val(result);

                    //alert($("#jsonPaybook").val());

                }
            });
        }

        
    </script>

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

        <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
        
                <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>


                     <telerik:AjaxSetting AjaxControlID="rBtnGuardar">
                        <UpdatedControls>

                            <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>

                 </AjaxSettings>
        </telerik:RadAjaxManager>

         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>

       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                
        <asp:Panel ID="pnlBody" runat="server">

                <telerik:RadImageButton ID="rBtnLimpiar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnBitacora" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCopiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnCopiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCopiarHovered.png"  ToolTip="Ultima Bitacora"  Text=""  OnClick="rBtnBitacora_Click" ></telerik:RadImageButton>
        
                <fieldset style="margin-top:5px;" > 
         
                    <legend>Parametros</legend>  
                    <div style="display:table; background-color:transparent;" class="auto-style3" >

                        <table border="0" style=" width:100%; text-align:left; background-color:transparent ;">
                            <tr >
                                <td style=" width:50%; background-color:transparent;"> 
                                    <telerik:RadLabel ID="RadLabel11" runat="server" Text="Documento" width="120px"></telerik:RadLabel>
                                    <telerik:RadComboBox ID="rCboTipoDoc"  width="250px" 
                                        runat="server" HighlightTemplatedItems="true" DropDownWidth="350px" 
                                        Height="350px" DropDownCssClass="cssRadComboBox" AutoPostBack="True"  AllowCustomText="true">            
                                          <HeaderTemplate>
                                                <table style="width: 180px">
                                                        <tr>
                                                             <td style="width: 95px;">
                                                                Clave
                                                            </td>
                                                            <td style="width: 95px;">
                                                                Descripción
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <table style="width: 180px;">
                                                        <tr>
                                                            <td style="width: 90px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docCve") %>
                                                            </td>
                                                             <td style="width: 90px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "docDes") %>
                                                            </td>
                                                        </tr>
                                                    </table>

                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                </td>
                                <td></td>
                                <td style=" width:50%; background-color:transparent;"> 
                                    <telerik:RadLabel ID="RadLabel2" runat="server" Text="Folio" width="120px"></telerik:RadLabel>                             
                                    <telerik:RadTextBox ID="rTxtFolio" runat="server" 
                                            
                                             AutoPostBack="true"
                                            MaxLength="20" Width="250px"
                                            EnabledStyle-CssClass="cssTxtEnabled"
                                            DisabledStyle-CssClass ="cssTxtEnabled"
                                            HoveredStyle-CssClass="cssTxtHovered"
                                            FocusedStyle-CssClass="cssTxtFocused"
                                            InvalidStyle-CssClass="cssTxtInvalid" 
                                         ></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                           
                                <td style=" width:50%; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel9" runat="server" Text="Cliente" width="120px"></telerik:RadLabel>
                                    <telerik:RadComboBox ID="rCboCliente" width="180px" runat="server" HighlightTemplatedItems="true" DropDownWidth="350px" Height="350px"
                                                     DropDownCssClass="cssRadComboBox" AutoPostBack="True" OnSelectedIndexChanged="rCboCliente_SelectedIndexChanged"   >            
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
                                                        <%# DataBinder.Eval(Container.DataItem, "cliCve") %>
                                                    </td>
                                                    <td style="width: 275px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "clieNom") %>
                                                    </td>
                                  
                                                </tr>
                                            </table>

                                    </ItemTemplate>
                                  <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>
                                </td>
                                <td></td>
                                <td style=" width:50%; background-color:transparent;">
                                    <telerik:RadLabel ID="RadLabel12" runat="server" Text="Sub Cliente" width="120px"></telerik:RadLabel>
                                    <telerik:RadComboBox ID="rCboSubCliente" width="180px" runat="server" HighlightTemplatedItems="true" DropDownWidth="350px" Height="350px"
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
                                                            <%# DataBinder.Eval(Container.DataItem, "cliCveSubClie") %>
                                                        </td>
                                                        <td style="width: 275px;">
                                                            <%# DataBinder.Eval(Container.DataItem, "clieNom") %>
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
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>

                                <td>
                                    <asp:Label ID="Label8" Width="120px" runat="server" Text="Fec. Ini."></asp:Label>
                                    <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="240px" AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" ></telerik:RadDatePicker>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label10"  Width="120px"  runat="server" Text="Fec. Fin."></asp:Label>
                                    <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="240px"  AutoPostBack="true" MaxDate="01-01-2070" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" ></telerik:RadDatePicker>
                                </td>
                            </tr>


                        </table>
                    </div>


                    
   
                </fieldset>


                <br />


                <telerik:RadGrid ID="rGdv_Documentos" 
                                           runat="server" 
                                           AutoGenerateColumns="False" 

                                           Width="100%"   Height="230px"   
                                           CssClass="Grid" 
                                           Skin="Office2010Silver">

                                <MasterTableView DataKeyNames="docCve"  AutoGenerateColumns="false" CssClass="GridTable"   >
                                    <Columns> 
                                        <telerik:GridBoundColumn HeaderText="Documento"    DataField="docCve"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" ></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Descripción"  DataField="docCmpMsgTxt"   HeaderStyle-Width="120px"  ItemStyle-Width="120px"  ></telerik:GridBoundColumn>

                                        <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="docCmpMsgUrlSit"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                                                     DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Sit">
                                            </telerik:GridImageColumn>

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
                
                

                <div style="width:100%; display:table; position:static; background-color:transparent;">
                    <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">                        
                        <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                        <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel" ></telerik:RadImageButton>
                    </asp:Panel>
                </div>

                <%--<input type="button" name="name" value="isbutton" onclick="myFunction();" />--%>

           <%-- </ContentTemplate> 
        </asp:UpdatePanel>   --%> 

        </asp:Panel>

        <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        <asp:HiddenField ID="hdfSecuencia" runat="server" />

        <asp:HiddenField ID="jsonPaybook" runat="server" />
        
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>

    </form>
</body>
</html>
