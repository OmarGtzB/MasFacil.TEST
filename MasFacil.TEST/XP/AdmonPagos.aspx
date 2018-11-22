<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="AdmonPagos.aspx.cs" Inherits="XP_AdmonPagos" %>

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
            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();   
        }  


   </script> 
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnMasivo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Pagos Masivos"  Text =""  OnClick="rBtnMasivo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"    ToolTip="Modificar"  Text=""  OnClick="rBtnModificar_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnValidacion"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validación" OnClick="rBtnValidacion_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"       runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  OnClick="rBtVerErr_Click" Text="" Visible="false" ></telerik:RadImageButton>        
                <telerik:RadImageButton ID="rBtnGeneraCheque"      runat="server"  Width="32px" Height="32px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnChequeDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnCheque.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnChequeHovered.png"  ToolTip="Genera Cheque"  OnClick="rBtnGeneraCheque_Click" OnClientClicking="OnClientClic_ConfirmOK" Text="" Visible="true" ></telerik:RadImageButton>
                

                <%-- Se cambia nombre de boton cancela cheque por reversa cheque y sus funciones --%>
                <%--<telerik:RadImageButton ID="rBtnCancelaCheque"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnChequeCancelDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnChequeCancel.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnChequeCancelHovered.png"  ToolTip="Cancela Cheque"  OnClick="rBtnCancelaCheque_Click" OnClientClicking="OnClientClic_ConfirmOK" Text="" Visible="true" ></telerik:RadImageButton>--%>
                <telerik:RadImageButton ID="rBtnReversaCheque"      runat="server"  Width="36px" Height="32px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnChequeReversaDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnChequeReversa.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnChequeReversaHovered.png"  ToolTip="Reversa Cheque"  OnClick="rBtnReversaCheque_Click" OnClientClicking="OnClientClic_ConfirmOK" Text="" Visible="true" ></telerik:RadImageButton>
                <%-- ----------------------------------------------------------- --%>

                <telerik:RadImageButton ID="rBtnGenera"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Genera Póliza"  OnClick="rBtnGenera_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rbtnImpPoliChe"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImpPoliCheDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImpPoliChe.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImpPoliCheHovered.png"  ToolTip="Imprime Póliza Cheque"  OnClick="rbtnImpPoliChe_Click" OnClientClicking="OnClientClic_ConfirmOKImpChe" Text="" Visible="true" ></telerik:RadImageButton>
                
                <%-- GRET,Se agrega boton de Cancela cheque --%>
                <telerik:RadImageButton ID="rBtnCancelaCheque"      runat="server"  Width="32px" Height="32px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnChequeCancelDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnChequeCancel.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnChequeCancelHovered.png"  ToolTip="Cancela Cheque"  OnClick="rBtnCancelaCheque_Click" OnClientClicking="OnClientClic_ConfirmOK" Text="" Visible="true" ></telerik:RadImageButton>
                <%--  --%>

                <telerik:RadImageButton ID="rBtnAplica"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aplica"  OnClick="rBtnAplica_Click" Text="" OnClientClicking="OnClientClic_ConfirmOK" Visible="false"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnPolizaEdit" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificarPoliza.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarPolizaHovered.png"  ToolTip="Edita Póliza"  OnClick="rBtnPolizaeEdit_Click" Text="" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Póliza"  OnClick="rBtnPolizaImp_Click" Text="" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
            </asp:Panel>


            <table border="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;"> 

                           
                            <table border="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr>                              
                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label6" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:160px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="150px" AutoPostBack="true" 
                                             OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"   >
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoDes") %>
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
                                        <asp:Label ID="Label5" runat="server" Text="Cuenta"></asp:Label>
                                    </td>
                                    <td style=" width:160px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboCuenta" runat="server" Width="150px" AutoPostBack="true"
                                              OnSelectedIndexChanged="rCboCuenta_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  >
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "ctaDepCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "ctaDepDes") %>
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
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha_Inicio" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Inicio_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>
                                    <td style=" width:65px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label10" runat="server" Text="Fec. Fin."></asp:Label>
                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadDatePicker ID="RdDateFecha_Final" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_Final_SelectedDateChanged" ></telerik:RadDatePicker>
                                    </td>

                                    <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label13" runat="server" Text="Proveedor"></asp:Label>
                                    </td>

                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadTextBox ID="rTxtBeneficiario" Visible="false" runat="server" AutoPostBack="true" OnTextChanged="rTxtBeneficiario_TextChanged"></telerik:RadTextBox>

                                        <telerik:RadComboBox ID="rCboBeneficiario" runat="server" Width="130px" AutoPostBack="true"
                                              OnSelectedIndexChanged="rCboCuenta_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  >
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "provCve")%>
                                                                </td>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "provNom") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>

                                        <%--se agrega combo para filtrar por Beneficiario --%>

                                        <td style=" width:80px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label17" runat="server" Text="Beneficiario"></asp:Label>
                                    </td>

                                   <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <telerik:RadComboBox ID="rCboBeneficiario2" runat="server" Width="130px" AutoPostBack="true"
                                              OnSelectedIndexChanged="rCboBeneficiario2_SelectedIndexChanged1"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <%--<td style="width: 100px;">
                                                                        Clave
                                                                    </td>--%>
                                                                    <td style="width: 200px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                           
                                                              <%--  <td style="width:50px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "pagXPBenef")%>
                                                                </td>--%>
                                                                      <td style="width:200px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "pagXPBenef")%>
                                                                </td>
                                                               
                                                                
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>
                                            </td>
                                        <%---------------%>

                            

                                    <td style=" width:40px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Sit"></asp:Label>
                                    </td>

                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">

                                        <telerik:RadComboBox ID="rCboSituacion" runat="server" Width="100px" AutoPostBack="true" 
                                             OnSelectedIndexChanged="rCboSituacion_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="220px" 
                                                    Height="200px"    >
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
                                                                    <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
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

                    <td style=" width:1200px; background-color:transparent; vertical-align:top;">

                        <fieldset style="">
                            <legend>Operaciones</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 

                                <div>


                                      <telerik:RadGrid ID="rGdvOperaciones" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    <MasterTableView   DataKeyNames="pagXPId" AutoGenerateColumns="False"  CssClass="GridTable" ItemStyle-Font-Size="9" AlternatingItemStyle-Font-Size="9" HeaderStyle-Font-Size="11" > 
         
                                            <Columns>
                                                 
                                                <telerik:GridBoundColumn DataField="cptoDes2"     HeaderStyle-Width="100px" HeaderText="Concepto" ItemStyle-Width="100px"   />
                                                <telerik:GridBoundColumn DataField="pagXPFolio"  HeaderStyle-Width="50px" HeaderText="Folio"  ItemStyle-Width="80px" />
                                                <telerik:GridBoundColumn DataField="transId"  HeaderStyle-Width="75px" HeaderText="Sit" ItemStyle-Width="100px" Display="false"   /> 
                                                <telerik:GridBoundColumn DataField="asiContEncId"  HeaderStyle-Width="75px" HeaderText="Sit" ItemStyle-Width="100px" Display="false"   /> 
                                                <telerik:GridBoundColumn DataField="transPolCve"   HeaderStyle-Width="50px" HeaderText="Poliza" ItemStyle-Width="80px"   />
                                                <telerik:GridBoundColumn DataField="pagXPNum"   HeaderStyle-Width="80px" HeaderText="N° de Cheque" ItemStyle-Width="80px"   />
                                                <telerik:GridBoundColumn DataField="pagXPBenef"    HeaderStyle-Width="180px" HeaderText="Beneficiario" ItemStyle-Width="100px"   />                                                  

                                                 <telerik:GridBoundColumn DataField="provDes"    HeaderStyle-Width="180px" HeaderText="Proveedor" ItemStyle-Width="100px"   />
                                                <telerik:GridBoundColumn DataField="ctaDepDes"   HeaderStyle-Width="150px" HeaderText="Cuenta" ItemStyle-Width="100px" />
                                                <telerik:GridBoundColumn DataField="pagXPFec"    HeaderStyle-Width="60px" HeaderText="Fecha" ItemStyle-Width="100px"  DataFormatString="{0:d}" />
                                                <telerik:GridBoundColumn DataField="pagXPImp"    HeaderStyle-Width="75px" HeaderText="Importe" ItemStyle-Width="100px" DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  />
                                                <telerik:GridBoundColumn DataField="pagXPSit"    HeaderStyle-Width="15px" HeaderText="pagXPSit" ItemStyle-Width="15px" Display="false"  />
                                                <telerik:GridBoundColumn DataField="cptoId"    HeaderStyle-Width="15px" HeaderText="cptoId" ItemStyle-Width="15px" Display="false"  />
                                                <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="40px"  ItemStyle-Width="40px" 
                                                                     DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                     ImageAlign="Middle" ImageHeight="14px" ImageWidth="14px" HeaderText="Sit">     </telerik:GridImageColumn>  
                                                <telerik:GridBoundColumn DataField="pagXPSit"  HeaderStyle-Width="75px" HeaderText="Sit" ItemStyle-Width="100px" Display="false"   />                                         
                                            </Columns>
                                            <NoRecordsTemplate> No se encontraron registros.</NoRecordsTemplate>
                                        </MasterTableView>
                                        <HeaderStyle CssClass="GridHeaderStyle"/>
                                        <ItemStyle CssClass="GridRowStyle"/>
                                        <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                                        <selecteditemstyle CssClass="GridSelectedItem"></selecteditemstyle>
 
                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                                            <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"  ScrollHeight="330px"    />
                                            
                                        </ClientSettings>
                                    </telerik:RadGrid>


                                </div>
                                <div style="width:100%;  display:table; position:static; background-color:transparent;" align="right" > 
                                     <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; font-size:14px;   text-align:left; background-color:transparent;">
                                        <tr   >   
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image1" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitR.png" />
                                            <asp:Label ID="Label1" runat="server" Text="Reg."></asp:Label>
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image2" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitV.png" />
                                            <asp:Label ID="Label2" runat="server" Text="Val."></asp:Label>
                                        </td>

                                            <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image8" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitCHE.png" />
                                            <asp:Label ID="Label14" runat="server" Text="Che."></asp:Label>
                                        </td>

                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image3" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitG.png" />
                                            <asp:Label ID="Label3" runat="server" Text="Gen."></asp:Label>
                                        </td>
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image7" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGV.png" />
                                            <asp:Label ID="Label12" runat="server" Text="Gen. Val."></asp:Label>
                                        </td>

                                              <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image9" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitCHI.png" />
                                            <asp:Label ID="Label15" runat="server" Text="CHi."></asp:Label>
                                        </td>

                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image5" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGE.png" />
                                            <asp:Label ID="Label9" runat="server" Text="Gen. Error"></asp:Label>
                                        </td>
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image4" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitI.png" />
                                            <asp:Label ID="Label4" runat="server" Text="Incorp."></asp:Label>
                                        </td>

                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image6" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitE.png" />
                                            <asp:Label ID="Label11" runat="server" Text="Error."></asp:Label>
                                        </td>
                                        <%--Se agrega icono de la situacion Inc. Error en el pie de Grid View--%>
                                        <td style=" width:80px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image10" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitIE.png" />
                                            <asp:Label ID="Label16" runat="server" Text="Inc. Error"></asp:Label>
                                        </td>
                                        </tr>
                                    </table>     
                                </div>                         
                            </div>


                        </fieldset>

                    </td>

                </tr>
            </table>


            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"   OnClientClicking ="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmOK"  Visible="false" ></telerik:RadImageButton>
            </asp:Panel>    
         <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Ver Errores"  >               
        </telerik:RadWindow>

        <telerik:RadWindow ID="RadWindowMasivos" runat="server" OnClientClose="closeRadWindow" NavigateUrl="AdmonPagosMasivos.aspx" Width="800px" Height="590px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close" Title="Pagos Masivos"  >               
        </telerik:RadWindow>

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
        </div>

            
                 <script type="text/javascript">
    function OnClientClicking(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function(argument) {
            if (argument) {
                this.click();
            }
        });
        var text = "Esta seguro que desea realizar el cambio?";
        radconfirm(text, callBackFunction, 300, 100, null, "Title");
        args.set_cancel(true);
    }
</script>
        </ContentTemplate>
    </asp:UpdatePanel>

                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   
                   OnAjaxRequest="RadAjaxManager1_AjaxRequest"  >  
                    <AjaxSettings>  
                    
                    </AjaxSettings>  
        </telerik:RadAjaxManager>

</asp:Content>

