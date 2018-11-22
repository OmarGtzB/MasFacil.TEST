<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="DiarioDocumento.aspx.cs" Inherits="FR_DiarioDocumento" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
      <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
      <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
      <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
      <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
      <link href="../css/styles.css" rel="stylesheet" type="text/css" />
      <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
       <script type="text/javascript">
            var bPreguntar = true;
            window.onbeforeunload = preguntarAntesDeSalir;

            function preguntarAntesDeSalir() {
                if (bPreguntar)
                    $("#divdoc").remove();
               // alert('hace algo');
            }
         
          </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
            
                <telerik:RadImageButton ID="rBtnVizualizarDoc"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVisualizarDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVisualizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVisualizarHovered.png"   ToolTip="Visualizacion"  Text="" OnClick="rBtnVizualizarDoc_Click" ></telerik:RadImageButton>
                 
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>


            </asp:Panel>                              



            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr>                              
                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Documento"></asp:Label>
                                    </td>
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                         
                                        <telerik:RadComboBox ID="rCboDocumento" runat="server" Width="130px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox"  ZIndex="99999"
                                                    DropDownWidth="300px" 
                                                    Height="200px"  OnSelectedIndexChanged="rCboDocumento_SelectedIndexChanged" >
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        Documento
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "docCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
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
                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Moneda"></asp:Label>
                                    </td>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboMoneda" runat="server" Width="150px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  OnSelectedIndexChanged="rCboMoneda_SelectedIndexChanged"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                         </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "monCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
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
                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fec. Ini."></asp:Label>
                                    </td>
                                    <td style=" width:110px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="105px"  AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" MinDate="01-01-1970" ZIndex="9999999"  PopupDirection="TopLeft" ></telerik:RadDatePicker>
                                     </td>
                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                                    </td>
                                    <td style=" width:110px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="105px"  AutoPostBack="true"  OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" MaxDate="01-01-2070"   ZIndex="9999999"  PopupDirection="TopLeft">

                                        </telerik:RadDatePicker>
                                    </td>
                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
                                    </td>
                                     <%--Combo de Clientes y Subclientes--%>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboClientes" runat="server" Width="150px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  OnSelectedIndexChanged="rCboClientes_SelectedIndexChanged"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        Clave
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "cliCveClie")%>
                                                                </td>
                                                                <td style="width: 200px;">
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
                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="rLblSubClie" runat="server" Text="SubCliente" Enabled="false"></asp:Label>
                                    </td>
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">
                                             <telerik:RadComboBox ID="rCboSubClientes" runat="server" Width="150px" AutoPostBack="true" 
                                                        HighlightTemplatedItems="true"
                                                        DropDownCssClass="cssRadComboBox" 
                                                        DropDownWidth="300px" 
                                                        Height="200px"  Enabled="false" OnSelectedIndexChanged="rCboSubClientes_SelectedIndexChanged">
                                                        <HeaderTemplate>
                                                            <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 100px;">
                                                                            Clave
                                                                        </td>
                                                                        <td style="width: 200px;">
                                                                            Descripción
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width:100px" >
                                                                        <%# DataBinder.Eval(Container.DataItem, "cliCveSubClie")%>
                                                                    </td>
                                                                    <td style="width: 200px;">
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
       
                            </table> 
                            <table border="0" cellpadding="0"  cellspacing="0" style=" border-style:none;  width:100%;  text-align:left; background-color:transparent;">
                                <tr> 
                                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                                        <asp:CheckBox ID="checkConsolidacionMoneda" runat="server" Text="Consolidación Moneda" Checked="false"   />                                                                 
                                    </td>    
                                </tr>
                            </table> 
                            </div>
                        </fieldset>

                    </td>

                </tr>
            </table>



            
            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:500px; background-color:transparent; vertical-align:top;">

                         <fieldset style=" height:385px;">
                            
                            <div id="divdoc01" style="width:100%; height:415px; z-index:1; display:table; position:static; background-color:transparent;" align="center" > 

                            <div id="divdoc" style="width:99%; height:413px; z-index:1; display:table; position:static; background-color:transparent;" align="center" > 
                                
                                  <iframe id="doc"  runat="server" width="100%" height="380px" src=""  > 
                              <%--      <object id="objeto" runat="server" type="application/pdf"  data="RelacionDocumento.aspx.cs"  >
                                        <embed id="prueba1"  runat="server" type="application/pdf"   src="RegistoDocumentos.aspx" />
                                    </object>--%>
                                </iframe>

                                
                            
                            </div>
                        </div>

                        </fieldset>

                    </td>
                </tr>
            </table>



            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>
             <asp:HiddenField ID="hdfBtnAccion" runat="server" />
          
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

     
</asp:Content>

