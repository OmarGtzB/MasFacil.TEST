<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCCCLIELIST.aspx.cs" Inherits="RPT_rptCCCLIELIST" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
       <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssWizard.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
        .rcbHeader ul,
        .rcbFooter ul,
        .rcbItem ul,
        .rcbHovered ul,
        .rcbDisabled ul {
            display: inline-block;
            margin: 0;
            padding: 0;
            list-style-type: none;
            vertical-align: middle;
        }
        .col1, .col2, .col3 {
            width: 50px;
            display: inline-block;
        }
 
 
        html .RadComboBoxDropDown .rcbItem > label,
        html .RadComboBoxDropDown .rcbHovered > label,
        html .RadComboBoxDropDown .rcbDisabled > label,
        html .RadComboBoxDropDown .rcbLoading > label,
        html .RadComboBoxDropDown .rcbCheckAllItems > label,
        html .RadComboBoxDropDown .rcbCheckAllItemsHovered > label {
            display: inline-block;
        }
    </style>

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

        (function () {

            window.pageLoad = function () {
                var $ = $telerik.$;
                var cssSelectors = ["accountInfo", "personalInfo", "contactDetails", "confirmation"];
                var breadCrumbButtons = $(".rwzBreadCrumb .rwzLI");

                for (var i = 0; i < cssSelectors.length; i++) {
                    $(breadCrumbButtons[i]).addClass(cssSelectors[i]);
                }
            }

            window.OnClientLoad = function (sender, args) {
                for (var i = 1; i < sender.get_wizardSteps().get_count() ; i++) {
                    sender.get_wizardSteps().getWizardStep(i).set_enabled(false);
                }
            }

            window.OnClientButtonClicking = function (sender, args) {
                if (!args.get_nextActiveStep().get_enabled()) {
                    args.get_nextActiveStep().set_enabled(true);
                }
            }

            window.AcceptTermsCheckBoxValidation = function (source, args) {
                var termsChecked = $telerik.$("input[id*='AcceptTermsCheckBox']")[0].checked;
                args.IsValid = termsChecked;
            }

            window.UserNameLenghthValidation = function (source, args) {
                var userNameConditions = $telerik.$(".conditions")[0];
                var isValid = (args.Value.length >= 4 && args.Value.length <= 15);
                args.IsValid = isValid;
                $telerik.$(userNameConditions).toggleClass("redColor", !isValid);

            }

            window.PasswordLenghthValidation = function (source, args) {
                var passwordConditions = $telerik.$(".conditions")[1];
                var isValid = args.Value.length >= 6;
                args.IsValid = isValid;
                $telerik.$(passwordConditions).toggleClass("redColor", !isValid);
            }
        })();

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

    <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadWizard1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>


    <asp:Panel ID="pnlBody" runat="server">

<%--  <div class="demo-container">
        <div class="wizardHeader"></div>--%>

     <telerik:RadWizard ID="RadWizard1" runat="server"
            RenderMode="Lightweight" Height="420px" OnClientLoad="OnClientLoad" OnClientButtonClicking="OnClientButtonClicking">

            
            <WizardSteps>
            <%--Parametros--%>
                  <telerik:RadWizardStep ID="RadWizardStep1" 
                     ImageUrl="~/Imagenes/IcoWizard/icoPaso1.png" 
                     ActiveImageUrl="~/Imagenes/IcoWizard/icoPaso1Active.png"
                     HoveredImageUrl ="~/Imagenes/IcoWizard/icoPaso1Hovered.png"
                     Title="Parametros" runat="server" StepType="Start" ValidationGroup="accountInfo" CausesValidation="true" SpriteCssClass="accountInfo">

                       <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                            <tr  style="width:100%; ">
                                <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                                    <fieldset style="">
                                        <div style="width:100%; height:250px; display:table; position:static; background-color:transparent;" align="center" > 
                                            <br />
                                            <table border="0" cellpadding="0" cellspacing="0" style=" width:630px;   border-style:none; text-align:left; background-color:transparent;">
                                                    <tr style=" height:35px; background-color:transparent;">     
                                                        <td style=" width:380px; background-color:transparent; vertical-align:top;" >


                                                          <table border="0" cellpadding="0" cellspacing="0" style=" width:380px;   border-style:none; text-align:left; background-color:transparent;">
                     
                                                                <tr style=" height:35px; background-color:transparent;">   
                                                                    <td style=" width:130px; background-color:transparent; vertical-align:top;">
                                                                        Formato
                                                                    </td>                           
                                                                    <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                                        <telerik:RadComboBox ID="rCboFormato" runat="server" Width="200px" 
                                                                            AutoPostBack="true"
                                                                            HighlightTemplatedItems="true"
                                                                            DropDownCssClass="cssRadComboBox" 
                                                                            DropDownWidth="210px" 
                                                                            Height="100px"
                                                                             OnSelectedIndexChanged="rCboFormato_SelectedIndexChanged"
                                                                               >
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Value ="CCCLIELIST" Text="Lista" />
                                                                                    <telerik:RadComboBoxItem Value ="CCCLIEDIR"  Text="Directorio" />
                                                                                    <telerik:RadComboBoxItem Value ="CCCLIEEXP"  Text="Expediente" />
                                                                                </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                   
                                                                </tr>

                                                          </table> 

                                                          <table border="0" cellpadding="0" cellspacing="0" style=" width:380px;   border-style:none; text-align:left; background-color:transparent;">

                                                                <tr style=" height:35px;  background-color:transparent; ">     
                                                                    <td style=" width:130px; background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label37" runat="server" Text="Ordenamiento:"></asp:Label> 
                                                                    </td>       
                                                                    <td style=" width:240px;  background-color:transparent;" >
                                                                            <asp:RadioButton ID="rBtnCheckOrdenClienteNum" runat="server" GroupName="ClienteOrden"   Text="Cliente"   Checked="true"   />
                                                                    </td>
                                                                    <td style="  width:300px; background-color:transparent;" >
                                                                            <asp:RadioButton ID="rBtnCheckOrdenClienteNom"  runat="server"   GroupName="ClienteOrden" Text="Nombre"   Checked="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr style=" height:35px; background-color:transparent;">   
                                                                    <td style=" width:20px; background-color:transparent; vertical-align:top;">
                                                                    </td>                           
                                                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                                                                        <asp:RadioButton ID="rBtnCheckAscendente" runat="server" GroupName="AscDes"  Text="Ascendente"  Checked="true" AutoPostBack="false"  />
                                                                    </td>                           
                                                                    <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                        <asp:RadioButton ID="rBtnCheckDescendente" runat="server" GroupName="AscDes"  Text="Descendente"  Checked="false" AutoPostBack="false"  />
                                                                    </td>
                                                                </tr>
                                                            </table>   



                                                        </td>
                                                        <td style=" width:250px; background-color:gainsboro; vertical-align:top;">
                                                            <asp:Label ID="lblIns" runat="server" Text="Instrucciones"></asp:Label><br />
                                                            <asp:Label ID="lbl" runat="server" Text="Debe seleccionar los parametros."></asp:Label>
                                                        </td>
                                                    </tr>
                                                  </table>

                                           


                                           
                                        </div>
                                    </fieldset>

                                </td>
                            </tr>
                        </table>
                  </telerik:RadWizardStep>

                <%--Niveles--%>
                 <telerik:RadWizardStep 
                     ImageUrl="~/Imagenes/IcoWizard/icoPaso2.png" 
                     ActiveImageUrl="~/Imagenes/IcoWizard/icoPaso2Active.png"
                     DisabledImageUrl="~/Imagenes/IcoWizard/icoPaso2Disable.png" 
                     HoveredImageUrl ="~/Imagenes/IcoWizard/icoPaso2Hovered.png"
                     Title="Niveles" runat="server" StepType="Step" ValidationGroup="personalInfo" SpriteCssClass="personalInfo">

                       <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                                <tr  style="width:100%;">
                                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                                        <fieldset style="">
                                            <div style="width:100%; height:250px; display:table; position:static; background-color:transparent;" align="center" > 
                                                <br />

                                                <table border="0" cellpadding="0" cellspacing="0" style=" width:630px;   border-style:none; text-align:left; background-color:transparent;">
                                                    <tr style=" height:35px;">     
                                                        <td style=" width:380px; background-color:transparent; vertical-align:top;" >

                                                               <table border="0" cellpadding="0" cellspacing="0" style=" width:380px;   border-style:none; text-align:left; background-color:transparent;">
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label28" runat="server" Text="Nivel 1:"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboNivel1AgrTipo" runat="server" 
                                                                               Width="160px" 
                                                                               Height="130px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="160px"
                                                                               OnSelectedIndexChanged="rCboNivel1AgrTipo_SelectedIndexChanged"
                                                                               AllowCustomText="true"
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td style="width: 190px;">
                                                                                                        Descipcion
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label29" runat="server" Text="Agrupación"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboNivel1Agr" runat="server"  
                                                                               Width="240px"
                                                                               Height="160px" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="280px"                                                          
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label30" runat="server" Text="Nivel 2"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                            <telerik:RadComboBox ID="rCboNivel2AgrTipo" runat="server" 
                                                                                Width="160px"
                                                                                Height="130px" 
                                                                                AutoPostBack="true" 
                                                                                HighlightTemplatedItems="true"
                                                                                DropDownCssClass="cssRadComboBox" 
                                                                                DropDownWidth="160px"
                                                                                OnSelectedIndexChanged="rCboNivel2AgrTipo_SelectedIndexChanged"
                                                                                AllowCustomText="true"
                                                                                >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
 
                                                                                                    <td style="width: 190px;">
                                                                                                        Descipcion
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label31" runat="server" Text="Agrupación"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboNivel2Agr" runat="server"  
                                                                               Width="240px"
                                                                               Height="160px" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="280px"                                                          
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label34" runat="server" Text="Nivel 3"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                            <telerik:RadComboBox ID="rCboNivel3AgrTipo" runat="server" 
                                                                                Width="160px"
                                                                                Height="130px" 
                                                                                AutoPostBack="true" 
                                                                                HighlightTemplatedItems="true"
                                                                                DropDownCssClass="cssRadComboBox" 
                                                                                DropDownWidth="160px"
                                                                                OnSelectedIndexChanged="rCboNivel3AgrTipo_SelectedIndexChanged"
                                                                                AllowCustomText="true"
                                                                                >
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
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label35" runat="server" Text="Agrupación"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:280px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboNivel3Agr" runat="server"  
                                                                               Width="240px"
                                                                               Height="160px" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="280px"                                                          
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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

                                                        </td>   
                                                                                  
                                                        <td style=" width:250px; background-color:gainsboro; vertical-align:top;">
                                                            <asp:Label ID="Label32" runat="server" Text="Instrucciones"></asp:Label><br />
                                                            <asp:Label ID="Label33" runat="server" Text="Debe seleccionar los niveles de agrupación."></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>                          

                                            </div>
                                        </fieldset>

                                    </td>
                                </tr>
                            </table>

                 </telerik:RadWizardStep>
                 <%--Filtros--%>
                 <telerik:RadWizardStep 
                    ImageUrl="~/Imagenes/IcoWizard/icoPaso3.png" 
                     ActiveImageUrl="~/Imagenes/IcoWizard/icoPaso3Active.png"
                     DisabledImageUrl="~/Imagenes/IcoWizard/icoPaso3Disable.png" 
                     HoveredImageUrl ="~/Imagenes/IcoWizard/icoPaso3Hovered.png"
                     Title="Filtros" runat="server" StepType="Step" ValidationGroup="ContactDetails" SpriteCssClass="contactDetails">

                       <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                                <tr  style="width:100%;">
                                    <td style=" width:100%; background-color:transparent; vertical-align:top;">

                                        <fieldset style="">
                                            <div style="width:100%; height:250px; display:table; position:static; background-color:transparent;" align="center" > 
                                                

                                                <table border="0" cellpadding="0" cellspacing="0" style=" width:630px;   border-style:none; text-align:left; background-color:transparent;">
                                                    <tr style=" height:35px;">     
                                                        <td style=" width:380px; background-color:transparent; vertical-align:top;" >

                                                               <table border="0" cellpadding="0" cellspacing="0" style="    border-style:none; text-align:left; background-color:transparent;">
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:58px; background-color:transparent; vertical-align:initial;">
                                                                            <asp:Label ID="Label38" runat="server" Text="Cliente"></asp:Label> 
                                                                        </td>       

                                                                        <td style=" width:185px; background-color:transparent; vertical-align:top;">
                                                                               <telerik:RadComboBox ID="rCboClientes" runat="server"  
                                                                                   Width="160px" 
                                                                                   Height="230px"  OnCheckAllCheck="rCboClientes_CheckAllCheck" OnItemChecked="rCboClientes_ItemChecked"
                                                                                   AutoPostBack="false" 
                                                                                   HighlightTemplatedItems="true"
                                                                                   DropDownCssClass="cssRadComboBox" 
                                                                                   DropDownWidth="500px"            
                                                                                   CheckBoxes="true" 
                                                                                   EnableCheckAllItemsCheckBox="true" LabelCssClass="RadComboBoxDropDown"
                                                                                   >
                                                                                        <HeaderTemplate>
                                                                                            <table style="width: 400px" cellspacing="0" cellpadding="0" >
                                                                                                <tr>
                                                                                                     <td style="width:150px; vertical-align:middle";  >
                                                                                                        Clave
                                                                                                    </td>
                                                                                                     <td style="width:350px; vertical-align:middle";  >
                                                                                                        Descripción
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <ul>
                                                                                                <li class="col1" style="width:100px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "cliCve")%></li>
                                                                                               <li class="col2" style="width:290px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "clieNom")%></li>
                                                                                            </ul>
                                                                                        </ItemTemplate>

                                                                                        <FooterTemplate>
                                                                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                        </FooterTemplate>
                                                                               </telerik:RadComboBox>  
                                                                        </td>
                                                                        <td style=" width:95px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="lblSubClientes" runat="server" Text="SubCliente"  Visible="false" ></asp:Label> 
                                                                        </td>  
                                                                        <td style=" width:250px; background-color:transparent; vertical-align:top;">
                                                                        <telerik:RadComboBox ID="rCboSubClientes" runat="server"  Enabled="false"  
                                                                                    Width="280px"
                                                                                    Height="230px" 
                                                                                    AutoPostBack="true" 
                                                                                    HighlightTemplatedItems="true"
                                                                                    DropDownCssClass="cssRadComboBox" 
                                                                                    DropDownWidth="450px"            
                                                                                    CheckBoxes="true" 
                                                                                    EnableCheckAllItemsCheckBox="true" LabelCssClass="RadComboBoxDropDown"                                                            
                                                                                    >
                                                                                       <HeaderTemplate>
                                                                                            <table style="width: 360px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td style="width: 110px;">
                                                                                                        Clave
                                                                                                    </td>
                                                                                                    <td style="width: 250px;">
                                                                                                        Descripción
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <ul>
                                                                                                <li class="col1" style="width:100px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "cliCve")%></li>
                                                                                                <li class="col2" style="width:290px;   vertical-align:middle; " ><%# DataBinder.Eval(Container.DataItem, "clieNom")%></li>
                                                                                            </ul>
                                                                                        </ItemTemplate>
                                                                                  
                                                                                        <FooterTemplate>
                                                                                            <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                        </FooterTemplate>
                                                                        </telerik:RadComboBox>   
                                                                        </td>
                                                                    </tr>

                                                                </table>   

                                                               <table border="0" cellpadding="0" cellspacing="0" style=" width:630px;   border-style:none; text-align:left; background-color:transparent;">
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label9" runat="server" Text="Filtro 1"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro1AgrTipo" runat="server" 
                                                                               Width="160px" 
                                                                               Height="130px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="160px"
                                                                               AllowCustomText="true"
                                                                               OnSelectedIndexChanged="rCboFiltro1AgrTipo_SelectedIndexChanged"
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td style="width: 190px;">
                                                                                                        Descipcion
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
                                                                                                </td>
 
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                           </telerik:RadComboBox>
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label5" runat="server" Text="Agrupación"></asp:Label> 
                                                                        </td>
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro1Agr" runat="server"  
                                                                               Width="280px"
                                                                               Height="160px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="300px"  
                                                                               AllowCustomText="true" 
                                                                               OnSelectedIndexChanged="rCboFiltro1Agr_SelectedIndexChanged"
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                                                    <tr style=" height:35px;">     
                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label6" runat="server" Text="Dato"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                             <telerik:RadComboBox ID="rCboFiltro1AgrDato" runat="server" 
                                                                                 Width="280px"
                                                                                 Height="160px"
                                                                                 HighlightTemplatedItems="true"
                                                                                 DropDownCssClass="cssRadComboBox" 
                                                                                 DropDownWidth="300px"
                                                                                 CheckBoxes="true" 
                                                                                 EnableCheckAllItemsCheckBox="true"  
                                                                                 >
                                                                                 <HeaderTemplate>
                                                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width: 10px;">
                                                                                            </td>  
                                                                                            <td style="width: 270px;">
                                                                                                Clave - Descripción 
                                                                                            </td>                                                                                            
                                                                                        </tr>
                                                                                    </table>
                                                                                 </HeaderTemplate>
                                                                                 
                                                                                 <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                             </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=" height:35px;">     

                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                             <asp:Label ID="Label7" runat="server" Text="Filtro 2"></asp:Label> 
                                                                        </td>
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro2AgrTipo" runat="server" 
                                                                               Width="160px" 
                                                                               Height="130px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="160px"
                                                                               AllowCustomText="true"
                                                                               OnSelectedIndexChanged="rCboFiltro2AgrTipo_SelectedIndexChanged"
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td style="width: 190px;">
                                                                                                        Descipcion
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
                                                                                                </td>
 
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                           </telerik:RadComboBox>
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label8" runat="server" Text="Agrupación"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro2Agr" runat="server"  
                                                                               Width="280px"
                                                                               Height="130px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="280px"   
                                                                               AllowCustomText="true"  
                                                                               OnSelectedIndexChanged="rCboFiltro2Agr_SelectedIndexChanged"                                                               
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 10px;">
                                                                                                     
                                                                                                </td>  
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 180px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>

  
                                                                                                <td style="width: 100px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 180px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                                                    <tr style=" height:35px;">     

                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label10" runat="server" Text="Dato"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                             <telerik:RadComboBox ID="rCboFiltro2AgrDato" runat="server" 
                                                                                 Width="280px"
                                                                                 Height="160px"
                                                                                 HighlightTemplatedItems="true"
                                                                                 DropDownCssClass="cssRadComboBox" 
                                                                                 DropDownWidth="300px"
                                                                                 CheckBoxes="true" 
                                                                                 EnableCheckAllItemsCheckBox="true"  
                                                                                 >
                                                                                 <HeaderTemplate>
                                                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width: 10px;">
                                                                                            </td>  
                                                                                            <td style="width: 270px;">
                                                                                                Clave - Descripción 
                                                                                            </td>                                                                                            
                                                                                        </tr>
                                                                                    </table>
                                                                                 </HeaderTemplate>
                                                                                 
                                                                                 <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                             </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style=" height:35px;">     

                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label11" runat="server" Text="Filtro 3" ></asp:Label> 
                                                                        </td>
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro3AgrTipo" runat="server" 
                                                                               Width="160px" 
                                                                               Height="130px" 
                                                                               AutoPostBack="true" 
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="160px"
                                                                               AllowCustomText="true"
                                                                               OnSelectedIndexChanged="rCboFiltro3AgrTipo_SelectedIndexChanged"
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                                <tr>
                                                                                                    <td style="width: 190px;">
                                                                                                        Descipcion
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width:190px" >
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrTipDes")%>
                                                                                                </td>
 
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                           </telerik:RadComboBox>
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label12" runat="server" Text="Agrupación" Visible="true"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                           <telerik:RadComboBox ID="rCboFiltro3Agr" runat="server"  
                                                                               Width="280px"
                                                                               Height="160px" 
                                                                               AutoPostBack="true"
                                                                               HighlightTemplatedItems="true"
                                                                               DropDownCssClass="cssRadComboBox" 
                                                                               DropDownWidth="300px"     
                                                                               AllowCustomText="true"   
                                                                               OnSelectedIndexChanged="rCboFiltro3Agr_SelectedIndexChanged"                                                   
                                                                               >
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    Clave
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    Descipcion
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 280px;"  cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 80px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                                                                </td>                                                                                            
                                                                                                <td style="width: 200px;">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes")%>
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
                                                                    <tr style=" height:35px;">     

                                                                        <td style=" width:70px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:200px; background-color:transparent; vertical-align:top;">
                                                                            
                                                                        </td>
                                                                        <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                                                            <asp:Label ID="Label27" runat="server" Text="Dato" Visible="true"></asp:Label> 
                                                                        </td>       
                                                                        <td style=" width:300px; background-color:transparent; vertical-align:top;">
                                                                             <telerik:RadComboBox ID="rCboFiltro3AgrDato" runat="server" 
                                                                                 Width="280px"
                                                                                 Height="160px"
                                                                                 HighlightTemplatedItems="true"
                                                                                 DropDownCssClass="cssRadComboBox" 
                                                                                 DropDownWidth="300px"
                                                                                 CheckBoxes="true" 
                                                                                 EnableCheckAllItemsCheckBox="true"  
                                                                                 >
                                                                                 <HeaderTemplate>
                                                                                    <table style="width: 280px" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width: 10px;">
                                                                                            </td>  
                                                                                            <td style="width: 270px;">
                                                                                                Clave - Descripción 
                                                                                            </td>                                                                                            
                                                                                        </tr>
                                                                                    </table>
                                                                                 </HeaderTemplate>
                                                                                 
                                                                                 <FooterTemplate>
                                                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                    </FooterTemplate>
                                                                             </telerik:RadComboBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>      

                                                        </td>   
                                      
                                                    </tr>

                                                </table>                          

                                            
                                            </div>
                                        </fieldset>

                                    </td>
                                </tr>
                            </table>

                 </telerik:RadWizardStep>
 
                <telerik:RadWizardStep runat="server" StepType="Complete" CssClass="complete">

 
                                            <p>Se han realizado los pasos con exito!</p>
                                            <p>Por favor espere un momento despues de confirmar la creacion del reporte !</p>
                                            <telerik:RadButton RenderMode="Lightweight" ID="rbtnEjecutaReporte" runat="server" OnClick="rbtnEjecutaReporte_Click" Text="Crear Reporte"></telerik:RadButton>

 

                </telerik:RadWizardStep>



             </WizardSteps>
        </telerik:RadWizard>

<%-- </div> --%>     

    </asp:Panel>
        
   

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>    


    </div>
    </form>
</body>
</html>
