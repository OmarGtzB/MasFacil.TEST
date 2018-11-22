<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCGBALANCEGRAL.aspx.cs" Inherits="RPT_rptCGBALANCEGRAL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
    <link href="~/css/cssWizard.css" rel="stylesheet" type="text/css" />

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

    <style type="text/css">
            .rcbHeader ul,
            .rcbFooter ul,
            .rcbItem ul,
            .rcbHovered ul,
            .rcbDisabled ul 
            {
                display: inline-block;
                margin: 0;
                padding: 0;
                list-style-type: none;
                vertical-align: middle;
            }
            .col1, .col2, .col3 
            {
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
            <telerik:RadWizard ID="RadWizard1" runat="server" RenderMode="Lightweight" Height="446px" OnClientLoad="OnClientLoad" OnClientButtonClicking="OnClientButtonClicking">
                <WizardSteps>
                    <%--   PASO 1--%>
                    <telerik:RadWizardStep ID="RadWizardStep1" 
                        ImageUrl="~/Imagenes/IcoWizard/icoPaso1.png" 
                        ActiveImageUrl="~/Imagenes/IcoWizard/icoPaso1Active.png"
                        HoveredImageUrl ="~/Imagenes/IcoWizard/icoPaso1Hovered.png"
                        Title="Parametros" runat="server" StepType="Start" ValidationGroup="accountInfo" CausesValidation="true" SpriteCssClass="accountInfo">

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
                                                                    <td style=" width:120px;   background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label1" runat="server"  Text="Año:"></asp:Label> 
                                                                    </td>       
                                                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                                                        <telerik:RadComboBox ID="rCboAnioPeriodo" runat="server" Width="220px" 
                                                                            AutoPostBack="true"
                                                                            HighlightTemplatedItems="true"
                                                                            DropDownCssClass="cssRadComboBox" 
                                                                            DropDownWidth="190px" 
                                                                            Height="190px" 
                                                                            OnSelectedIndexChanged="rCboAnioPeriodo_SelectedIndexChanged"  
                                                                            >
                                                                                <HeaderTemplate>
                                                                                    <table style="width: 190px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
 
                                                                                                <td style="width: 190px;">
                                                                                                    Año
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <table style="width: 190px;"  cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width:190px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "perAnio")%>
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
                                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label3" runat="server" Text="Periodo"></asp:Label> 
                                                                    </td> 
                                                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                                                            <telerik:RadComboBox ID="rCboNumPeriodo" runat="server" Width="100px" 
                                                                            AutoPostBack="true"   
                                                                            HighlightTemplatedItems="true"
                                                                            DropDownCssClass="cssRadComboBox" 
                                                                            DropDownWidth="210px" 
                                                                            Height="190px">
                                                                                <HeaderTemplate>
                                                                                    <table style="width: 210px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 70px;">
                                                                                                    Periodo
                                                                                                </td>
                                                                                                <td style="width: 140px;">
                                                                                                    Descripción
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <table style="width: 210px;"  cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width:70px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "perNum")%>
                                                                                            </td>
                                                                                                <td style="width:140px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "perDes")%>
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

                                                                <%--<tr style=" height:35px;">    
                                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label4" runat="server" Text="Periodo Final"></asp:Label> 
                                                                    </td> 
                                                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                                                            <telerik:RadComboBox ID="rCboNumPeriodoFinal" runat="server" Width="100px" 
                                                                            AutoPostBack="true" 
                                                                            HighlightTemplatedItems="true"
                                                                            DropDownCssClass="cssRadComboBox" 
                                                                            DropDownWidth="210px" 
                                                                            Height="190px">
                                                                                <HeaderTemplate>
                                                                                    <table style="width: 210px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 70px;">
                                                                                                    Periodo
                                                                                                </td>
                                                                                                <td style="width: 140px;">
                                                                                                    Descripción
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <table style="width: 210px;"  cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width:70px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "perNum")%>
                                                                                            </td>
                                                                                                <td style="width:140px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "perDes")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                                                </FooterTemplate>
                                                                            </telerik:RadComboBox>
                                                                    </td> 
                                                                </tr>--%>

                                                                <tr style=" height:35px;">    
                                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label2" runat="server" Text="Nivel Cta. Cont."></asp:Label> 
                                                                    </td> 
                                                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                                                            <telerik:RadComboBox ID="rCboNivelEstCodCta" runat="server" Width="220px" 
                                                                            AutoPostBack="true" 
                                                                            HighlightTemplatedItems="true"
                                                                            DropDownCssClass="cssRadComboBox" 
                                                                            DropDownWidth="210px" 
                                                                            Height="190px">
                                                                                <HeaderTemplate>
                                                                                    <table style="width: 210px" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td style="width: 50px;">
                                                                                                    Nivel
                                                                                                </td>
                                                                                                <td style="width: 160px;">
                                                                                                    Descripción
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <table style="width: 210px;"  cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td style="width:50px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContEstCodNiv")%>
                                                                                            </td>
                                                                                                <td style="width:160px" >
                                                                                                <%# DataBinder.Eval(Container.DataItem, "ctaContEstCodDes")%>
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
                                                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                                                                        <asp:Label ID="Label5" runat="server" Text="Filtro"></asp:Label> 
                                                                    </td> 
                                                                    <td style=" width:260px; background-color:transparent; vertical-align:top;">
                                                                        <telerik:RadTextBox ID="rTxtFiltro" runat="server" Width="220px" 
                                                                            EnabledStyle-CssClass="cssTxtEnabled"
                                                                             DisabledStyle-CssClass ="cssTxtEnabled"
                                                                             HoveredStyle-CssClass="cssTxtHovered"
                                                                             FocusedStyle-CssClass="cssTxtFocused"
                                                                             InvalidStyle-CssClass="cssTxtInvalid"
                                                                             MaxLength="100"
                                                                             ></telerik:RadTextBox>
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


                    <telerik:RadWizardStep runat="server" StepType="Complete" CssClass="complete">
                    <p>Se han realizado los pasos con exito!</p>
                    <p>Por favor espere un momento despues de confirmar la creacion del reporte !</p>
                    <telerik:RadButton RenderMode="Lightweight" ID="rbtnCrearReporte" runat="server"   Text="Crear Reporte"  OnClick="rbtnCrearReporte_Click" ></telerik:RadButton>
                    </telerik:RadWizardStep>


                </WizardSteps>
            </telerik:RadWizard>
        </asp:Panel>


        <asp:HiddenField ID="hdfRptCve" runat="server" />

        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
        </telerik:RadWindowManager>
          
    </div>
    </form>
</body>
</html>
