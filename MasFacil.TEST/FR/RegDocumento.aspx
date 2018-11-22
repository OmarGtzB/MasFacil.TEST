<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="RegDocumento.aspx.cs" Inherits="FR_RegDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
   <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />

        <script type="text/javascript">
        function closeRadWindow()  
        {  
          $find("<%= RAJAXMAN1.ClientID %>").ajaxRequest(); 
        }  
        </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <telerik:RadAjaxManager ID="RAJAXMAN1" runat="server" 
        DefaultLoadingPanelID="RadAjaxLoadingPanel1"  
        OnAjaxRequest="RAJAXMAN1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rBtnModificar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnEliminar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnValidacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtVerErr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnAutorizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnProcesar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnCancelarSit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnVizualizar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnCopiar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnCFDITimbre">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnImprimir">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rBtnLimpiar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlBody" />
                </UpdatedControls>
            </telerik:AjaxSetting>



            <telerik:AjaxSetting AjaxControlID="rCboDocumento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboSituacion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RdDateFecha_Inicio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RdDateFecha_Final">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboClientes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rCboSubClientes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "rGdv_Documentos" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rGdv_Documentos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID = "pnlRastreoDoc" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="RAJAXMAN1">  
                            <UpdatedControls>  
                                <telerik:AjaxUpdatedControl ControlID="rGdv_Documentos" />  
                            </UpdatedControls>  
            </telerik:AjaxSetting>  

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Silk"></telerik:RadAjaxLoadingPanel>



    <asp:Panel ID="pnlBody" runat="server">

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
  
                <telerik:RadImageButton ID="rBtnValidacion"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validacion"  Text="" OnClick="rBtnValidacion_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"        runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  Text=""  OnClick="rBtVerErr_Click" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnAutorizar"    runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"   ToolTip="Autorizar"  Text =""  OnClick="rBtnAutorizar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnProcesar"     runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Procesar"  Text=""  OnClick="rBtnProcesar_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelarSit" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelaDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnCancela.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelaHovered.png"   ToolTip="Cancelar"  Text="" OnClick="rBtnCancelarSit_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
 
                <telerik:RadImageButton ID="rBtnVizualizar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVisualizarDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVisualizar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVisualizarHovered.png"   ToolTip="Visualizacion"  Text="" OnClick="rBtnVizualizarDoc_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnImprimir"    runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"    ToolTip="Imprimir"  Text ="" OnClick="rBtnImprimir_Click" OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCopiar"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCopiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnCopiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCopiarHovered.png"  ToolTip="Copiar"  Text="" OnClick="rBtnCopiar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCFDITimbre"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCFDITimbreDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnCFDITimbre.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCFDITimbreHovered.png"  ToolTip="Timbrar"  Text=""  OnClick="rBtnCFDITimbre_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"     runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnMineriaDoc"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnMineriaDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnMineria.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnMineriaHovered.png"   ToolTip="Mineria"  Text="" Visible ="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnRastreo"     Visible="false"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnRastreoDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnRastreo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnRastreoHovered.png"   ToolTip="Rastreo"  Text=""></telerik:RadImageButton>
               
            </asp:Panel>                              



            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 




                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              
                                    <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Documento"></asp:Label>
                                    </td>
                                    <td style=" width:130px; background-color:transparent; vertical-align:top;">
                                         
                                        <telerik:RadComboBox ID="rCboDocumento" runat="server" Width="130px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px" OnSelectedIndexChanged="rCboDocumento_SelectedIndexChanged"  >
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

                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Situacion"></asp:Label>
                                    </td>

                                    <td style=" width:155px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboSituacion" runat="server" Width="150px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="220px" 
                                                    Height="200px" OnSelectedIndexChanged="rCboSituacion_SelectedIndexChanged"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 195px" cellspacing="0" cellpadding="0">

         
                                                                <tr>

                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                    <td style="width: 160px; text-align:left;">
                                                                        Descripción
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 150px;"  cellspacing="0" cellpadding="0">
                                                            <tr>


                                                                <td style="width: 25px;">
                                                                    <asp:Image runat="server" Width="12px" Height="12px" ImageUrl ='<%# DataBinder.Eval(Container.DataItem, "imgSit") %>'  />
                                            
                                                                </td>
                                                                <td style="width: 160px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "docProcDes") %>
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
                                         <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="105px"  OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" AutoPostBack="true" ></telerik:RadDatePicker>
                                     </td>
                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                                    </td>
                                    <td style=" width:110px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="105px"  OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" AutoPostBack="true" MaxDate="01-01-2070" ></telerik:RadDatePicker>
                                    </td>
                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                                    </td>
                                    <td style=" width:170px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboClientes" runat="server" Width="150px" AutoPostBack="true"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  OnSelectedIndexChanged ="rCboClientes_SelectedIndexChanged" >
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
                                        <asp:Label ID="rLblSubClie" runat="server" Text="SubCliente"></asp:Label>
                                    </td>
                                    <td style=" width:150px; background-color:transparent; vertical-align:top;">

                                             <telerik:RadComboBox ID="rCboSubClientes" runat="server" Width="150px" AutoPostBack="true"
                                                        HighlightTemplatedItems="true"
                                                        DropDownCssClass="cssRadComboBox" 
                                                        DropDownWidth="300px" 
                                                        Height="200px" OnSelectedIndexChanged="rCboSubClientes_SelectedIndexChanged"  Enabled="false">
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


                            </div>
                        </fieldset>
                    </td>

                </tr>
            </table>



            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:500px; background-color:transparent; vertical-align:top;">

                        <fieldset style="">
                            <legend>Documentos</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 
                                
                                
                                <telerik:RadGrid ID="rGdv_Documentos" 
                                                runat="server"
                                                AutoGenerateColumns="False" 
                                                Width="920px" Height="285px" 
                                                CssClass="Grid"
                                                skin="Office2010Silver" OnSelectedIndexChanged="rGdv_Documentos_SelectedIndexChanged" >
                                                <MasterTableView   DataKeyNames="docRegid" AutoGenerateColumns="False"  CssClass="GridTable"  >
                                        <Columns>
                                             <telerik:GridBoundColumn DataField="docRegid"   HeaderStyle-Width="0px" HeaderText="Id" ItemStyle-Width="100px"  Display="false" />

                                             <telerik:GridBoundColumn DataField="listPreValDes"   HeaderStyle-Width="70px" HeaderText="Sit" ItemStyle-Width="100px" Display="false"  />
                                             <telerik:GridBoundColumn DataField="docRegSit" HeaderStyle-Width="70px" HeaderText="Sit" ItemStyle-Width="70px"   Display="false" />
                                            <telerik:GridBoundColumn DataField="cliCve" HeaderStyle-Width="70px" HeaderText="Sit" ItemStyle-Width="70px"   Display="false" />
                                             
                                             <telerik:GridBoundColumn DataField="docRegFolio" HeaderStyle-Width="40px" HeaderText="Folio" ItemStyle-Width="40px" />
                                            <telerik:GridBoundColumn DataField="docCve"      HeaderStyle-Width="90px" HeaderText="Documento" ItemStyle-Width="90px"  />
                                             <telerik:GridBoundColumn DataField="docRegDes"  HeaderStyle-Width="220px" HeaderText="Descripcion" ItemStyle-Width="220px"  />
                                             <telerik:GridBoundColumn DataField="clieAbr "  HeaderStyle-Width="100px" HeaderText="Cliente" ItemStyle-Width="250px"  />
                                            <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                                                     DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Sit">
                                            </telerik:GridImageColumn>

                                            <telerik:GridBoundColumn DataField="Timbre" HeaderStyle-Width="70px" HeaderText="Timbre" ItemStyle-Width="70px" Display="false" />
                                        </Columns>
                                        <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                    </MasterTableView>
                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                    <ItemStyle CssClass="GridRowStyle"/>
                                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                    <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"    />
                                    </ClientSettings>
                                </telerik:RadGrid>

                                <div style="width:100%;  display:table; position:static; background-color:transparent;" align="right" > 
                                     <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; font-size:14px;   text-align:left; background-color:transparent;">
                                        <tr   >   
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image1" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit1.png" />
                                            <asp:Label ID="Label1" runat="server" Text="Reg."></asp:Label>
                                        </td>
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image2" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit2.png" />
                                            <asp:Label ID="Label2" runat="server" Text="Aut."></asp:Label>
                                        </td>
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image3" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit3.png" />
                                            <asp:Label ID="Label3" runat="server" Text="Proc."></asp:Label>
                                        </td>
                                        <td style=" width:95px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image4" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit4.png" />
                                            <asp:Label ID="Label4" runat="server" Text="Proc. Parc"></asp:Label>
                                        </td>
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image5" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit5.png" />
                                            <asp:Label ID="Label9" runat="server" Text="Canc."></asp:Label>
                                        </td>
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image6" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit9.png" />
                                            <asp:Label ID="Label11" runat="server" Text="Error."></asp:Label>
                                        </td>
                                        <td style=" width:100px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image7" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/IcoDocProceso/imgSit10.png" />
                                            <asp:Label ID="Label14" runat="server" Text="Error Timbre."></asp:Label>
                                        </td>
                                        </tr>
                                    </table>     
                                </div>
                                
                                 
                            </div>


                        </fieldset>

                    </td>

                    
                    <td style=" width:30px; background-color:transparent;">
                                <fieldset style="width:327px; background-color:transparent;" >
                                    <legend>Rastreo Documentos</legend>
                                       <div style="width:325px; height:310px; background-color:transparent;" >  
                                           <asp:Panel ID="pnlRastreoDoc" runat="server" Width="100%" >


                                           <asp:Table ID="Table1" runat="server"  Width="100%" Height="310px" HorizontalAlign="Center">
                                               <asp:TableRow BackColor="transparent" BorderColor="transparent" Height="100px" >
                                                     <asp:TableCell  HorizontalAlign="Center" BorderColor="Black" BorderStyle="Dotted"  BorderWidth="1px" >
                                                     <asp:DataList ID="DataList2" runat="server">
                                                         <ItemTemplate> 
                                                                <telerik:RadButton  runat="server" Value='<%# Eval("docRegId") %>'
                                                                    ID="btnPadre"  
                                                                    AutoPostBack="true" 
                                                                    RenderMode="Lightweight"    OnClick="btnPadre_Click" 
                                                                    CssClass="btnRastreoPadre">
                                                                                     <contenttemplate> 
                                                                                                <table style=" background-color:transparent; align-content:center; ">
                                                                                                <tr>
                                                                                                    <td style="background-color:transparent; width:100px; height:75px; align-content:center;  font-size:14px;" >
                                                                                                        <br />
                                                                                                        <asp:Image ImageAlign="Middle" ID="Image1" runat="server"   ImageUrl="~/Imagenes/IcoRastreoDocumentos/Documento.png" width="40px" height="40px" BorderWidth="3px" BorderColor="transparent" />
                                                                                                        <br />
                                                                                                        <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Font-Size="Smaller" ForeColor="#ffffff" Text='<%# Eval("docCve") %>' BorderWidth="3px"  BorderColor="transparent"  ></telerik:RadLabel>
                                                                                          
                                                                                                    </td>
                                                                                                </tr>
                                                                                                </table> 
                                                                                        </contenttemplate>
                                                                </telerik:RadButton>
                                                             <telerik:RadToolTip ID="RadToolTipbtnPadre" runat="server" TargetControlID="btnPadre"
                                                                    IsClientID="false" Skin="Windows7" VisibleOnPageLoad="false">
                                                                    <div style="float: left; padding-top: 6px;">
                                                                                <asp:Label CssClass="info" ID="ProductName" runat="server" Style="font-size: 14px;">Información</asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Tipo:</span>
                                                                                <asp:Label CssClass="info" ID="Label12" runat="server"><%# Eval("docCve")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Folio:</span>
                                                                                <asp:Label CssClass="info" ID="Category" runat="server"><%# Eval("docRegFolio")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Moneda:</span>
                                                                                <asp:Label CssClass="info" ID="Label13" runat="server"><%# Eval("monDes")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Cliente:</span>
                                                                                <asp:Label CssClass="info" ID="Label2" runat="server"><%# Eval("cliCve")%></asp:Label>
                                                                                <br />
                                                                            </div>
                                                                </telerik:RadToolTip>
                                                             </ItemTemplate>
                                                       </asp:DataList>
                                                 
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                        
                                               <asp:TableRow BackColor="transparent" Height="100px" HorizontalAlign="Center">
                                                   <asp:TableCell  HorizontalAlign="Center" BorderColor="Black" BorderStyle="Dotted"  BorderWidth="1px" >
                                                             <asp:DataList ID="DataList3" runat="server">
                                                         <ItemTemplate> 
                                                                       <telerik:RadButton  runat="server" Value='<%# Eval("docRegId") %>'
                                                                        ID="btnRastreoActual"  
                                                                        AutoPostBack="true" 
                                                                        RenderMode="Lightweight"     OnClick="btnRastreoActual_Click"
                                                                        CssClass="btnRastreoActual">
                                                                                        <contenttemplate> 
                                                                                                <table style=" background-color:transparent; align-content:center; ">
                                                                                                <tr>
                                                                                                    <td style="background-color:transparent; width:100px; height:75px; align-content:center;  font-size:14px;" >
                                                                                                        <br />
                                                                                                        <asp:Image ImageAlign="Middle" ID="Image1" runat="server"   ImageUrl="~/Imagenes/IcoRastreoDocumentos/Documento.png" width="40px" height="40px" BorderWidth="3px" BorderColor="transparent" />
                                                                                                        <br />
                                                                                                        <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Font-Size="Smaller" ForeColor="#ffffff" Text='<%# Eval("docCve") %>' BorderWidth="3px"  BorderColor="transparent"  ></telerik:RadLabel>
                                                                                          
                                                                                                    </td>
                                                                                                </tr>
                                                                                                </table> 
                                                                                        </contenttemplate>
                                                                    </telerik:RadButton>
                                                                          <telerik:RadToolTip ID="RadToolTipbtnRastreoActual" runat="server" TargetControlID="btnRastreoActual"
                                                                                IsClientID="false" Skin="Windows7" VisibleOnPageLoad="false">
                                                                             <div style="float: left; padding-top:6px;">
                                                                                <asp:Label CssClass="info" ID="ProductName" runat="server" Style="font-size: 14px;">Información</asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Tipo:</span>
                                                                                <asp:Label CssClass="info" ID="Label12" runat="server"><%# Eval("docCve")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Folio:</span>
                                                                                <asp:Label CssClass="info" ID="Category" runat="server"><%# Eval("docRegFolio")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Moneda:</span>
                                                                                <asp:Label CssClass="info" ID="Label13" runat="server"><%# Eval("monDes")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Cliente:</span>
                                                                                <asp:Label CssClass="info" ID="Label2" runat="server"><%# Eval("cliCve")%></asp:Label>
                                                                                <br />
                                                                            </div>
                                                                            </telerik:RadToolTip>
                                                             <telerik:RadToolTipManager RenderMode="Lightweight" runat="server" AnimationDuration="900"
                                                                                       ID="RadToolTipManager1" Width="480px" Height="427px" RelativeTo="Element"
                                                                                       Animation="FlyIn" Position="MiddleRight" >
                                                            </telerik:RadToolTipManager>
                                                             </ItemTemplate>
                                                       </asp:DataList>
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                      
                                               <asp:TableRow BackColor="transparent" BorderColor="transparent" Height="100px">
                                                   <asp:TableCell BorderColor="Black" BorderStyle="Dotted"  BorderWidth="1px" HorizontalAlign="Center" >
                                                       <asp:DataList ID="DataList1" runat="server" >
                                                         <ItemTemplate> 
                                                                       <telerik:RadButton  runat="server" Value='<%# Eval("docRegId") %>'
                                                                        ID="btnDerivado"  
                                                                        AutoPostBack="true" 
                                                                        RenderMode="Lightweight"    OnClick="btnDerivado_Click1"
                                                                        CssClass="btnRastreoDerivados">
                                                                                 <contenttemplate> 
                                                                                                <table style=" background-color:transparent; align-content:center; ">
                                                                                                <tr>
                                                                                                    <td style="background-color:transparent; width:100px; height:75px; align-content:center;  font-size:14px;" >
                                                                                                        <br />
                                                                                                        <asp:Image ImageAlign="Middle" ID="Image1" runat="server"   ImageUrl="~/Imagenes/IcoRastreoDocumentos/Documento.png" width="40px" height="40px" BorderWidth="3px" BorderColor="transparent" />
                                                                                                        <br />
                                                                                                        <telerik:RadLabel ID="radLb_Nombre_menu" runat="server" Font-Size="Smaller" ForeColor="#ffffff" Text='<%# Eval("docCve") %>' BorderWidth="3px"  BorderColor="transparent"  ></telerik:RadLabel>
                                                                                          
                                                                                                    </td>
                                                                                                </tr>
                                                                                                </table> 
                                                                                        </contenttemplate>
                                                                    </telerik:RadButton>
                                                                        <telerik:RadToolTip ID="RadToolTipbtnDerivado" runat="server" TargetControlID="btnDerivado"
                                                                                IsClientID="false" Skin="Windows7" VisibleOnPageLoad="false">
                                                                            <div style="float: left; padding-top: 6px;">
                                                                                <asp:Label CssClass="info" ID="ProductName" runat="server" Style="font-size: 14px;">Información</asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Tipo:</span>
                                                                                <asp:Label CssClass="info" ID="Label12" runat="server"><%# Eval("docCve")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Folio:</span>
                                                                                <asp:Label CssClass="info" ID="Category" runat="server"><%# Eval("docRegFolio")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Moneda:</span>
                                                                                <asp:Label CssClass="info" ID="Label13" runat="server"><%# Eval("monDes")%></asp:Label>
                                                                                <br />
                                                                                <span class='title' style="color: #c98400">Cliente:</span>
                                                                                <asp:Label CssClass="info" ID="Label2" runat="server"><%# Eval("cliCve")%></asp:Label>
                                                                                <br />
                                                                            </div>
                                                                            </telerik:RadToolTip>
                                                             </ItemTemplate>
                                                       </asp:DataList>
                                                   </asp:TableCell>
                                               </asp:TableRow>
                                           </asp:Table>

                                            
                                           </asp:Panel>

                                        </div>
                                </fieldset>
                            </td>


                </tr>
            </table>

            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <%--<telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"   OnClientClicking ="OnClientClic_ConfirmOK" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK" ></telerik:RadImageButton>--%>
            </asp:Panel>    
        

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Ver Errores"  >               
            </telerik:RadWindow>

            <telerik:RadWindow ID="RadWindowCancelarDoc" runat="server" OnClientClose="closeRadWindow"  Width="440px" Height="380px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Cancelar Documento"  >               
            </telerik:RadWindow>

            <telerik:RadWindow ID="RadWindowCopiaDoc" runat="server" OnClientClose="closeRadWindow"  Width="440px" Height="320px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Copia Documento"  >               
            </telerik:RadWindow>

             <asp:HiddenField ID="hdfRawUrl" runat="server" />
       
             <asp:HiddenField ID="hdfBtnAccion" runat="server" />
             <asp:HiddenField ID="btnIdActual" runat="server" />
        </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

 

    </asp:Panel>


</asp:Content>

