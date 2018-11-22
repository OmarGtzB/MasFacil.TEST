<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="Polizas.aspx.cs" Inherits="CG_Polizas" %>

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
            <%--$find("<%= RadAjaxManager1.ClientID %>").ajaxRequest();  --%> 
        }
   </script> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnNuevo"       runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png"          Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png"        Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"          ToolTip="Nuevo"  Text ="" Visible="false" OnClick="rBtnNuevo_Click"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Edita Poliza"  Text="" Visible="false" OnClick="rBtnPolizaEdit_Click" ></telerik:RadImageButton>
                 <telerik:RadImageButton ID="rBtnEliminar"    runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" Visible="false" OnClick="rBtnEliminar_Click"  ></telerik:RadImageButton>
                
                <telerik:RadImageButton ID="rBtnValidacion"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnValidacionDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnValidacion.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnValidacionHovered.png"   ToolTip="Validacion"  Text="" Visible="false" OnClick="rBtnValidacion_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtVerErr"       runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresDisabled.png"     Image-Url="~/Imagenes/IcoBotones/IcoBtnVerErrores.png"   Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnVerErroresHovered.png"  ToolTip="Ver Errores"  Text="" Visible="false"  OnClick="rBtVerErr_Click" ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnGenera"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnProcesarDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnProcesar.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnProcesarHovered.png"  ToolTip="Genera"  Text="" Visible="false" OnClick="rBtnGenera_Click" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnAplica"      runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarDisabled.png"      Image-Url="~/Imagenes/IcoBotones/IcoBtnAutorizar.png"    Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnAutorizarHovered.png"  ToolTip="Aplica"  Text="" Visible="false" OnClick="rBtnAplica_Click" ></telerik:RadImageButton>
                

                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Poliza"  Text="" Visible="false" OnClick="rBtnPolizaImp_Click"  ></telerik:RadImageButton>

                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
            </asp:Panel>     



            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                           
                            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">
                                <tr   >                              

                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label5" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:315px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="290px" AutoPostBack ="true" OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged"
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


                                    <td style=" width:75px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label7" runat="server" Text="Situacion"></asp:Label>
                                    </td>

                                    <td style=" width:175px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadComboBox ID="rCboSituacion" runat="server" Width="150px"  AutoPostBack="true" OnSelectedIndexChanged="rCboSituacion_SelectedIndexChanged"
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


                                    <td style=" width:55px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td style=" width:125px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="rDateFecha" runat="server" Width="105px"  AutoPostBack="true" OnSelectedDateChanged="rDateFecha_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>

                                    <td style=" width:10px; background-color:transparent; vertical-align:top;">

                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                    </td>

                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                        
                                    </td>
                                    <td style=" width:200px; background-color:transparent; vertical-align:top;">

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
                            <legend>Polizas</legend>
                            <div style="width:100%; height:310px;  display:table; position:static; background-color:transparent;" align="center" > 
                                <div>
                                      <telerik:RadGrid ID="rGdvPolizas" 
                                                    runat="server"
                                                    AutoGenerateColumns="False" 
                                                    Width="1250px" Height="285px" 
                                                    CssClass="Grid"
                                                    skin="Office2010Silver"  >
                                                    <MasterTableView   DataKeyNames="asiContEncId" AutoGenerateColumns="False"  CssClass="GridTable"  > 
         
                                            <Columns>
                                                 <telerik:GridBoundColumn DataField="asiContEncId" HeaderStyle-Width="100px" HeaderText="asiContEncId" ItemStyle-Width="100px"  Display="false" />
                                                 <telerik:GridBoundColumn DataField="polSit"   HeaderStyle-Width="208px" HeaderText="polSit" ItemStyle-Width="100px"  Display="false" />
                                                                                                
                                                 <telerik:GridBoundColumn DataField="polCve"      HeaderStyle-Width="90px" HeaderText="A. Contable" ItemStyle-Width="90px"   />
                                                 <telerik:GridBoundColumn DataField="polDes"  HeaderStyle-Width="300px" HeaderText="polDes" ItemStyle-Width="300px"  />
                                                 <telerik:GridBoundColumn  DataField="cptoId" HeaderStyle-Width="290px" HeaderText="cptoId" ItemStyle-Width="290px"  />
                                                 <telerik:GridBoundColumn DataField="polFec"   HeaderStyle-Width="80px" HeaderText="Fecha" ItemStyle-Width="80px" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polSumCgo"   HeaderStyle-Width="100px" HeaderText="Cargos" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polSumAbo"   HeaderStyle-Width="100px" HeaderText="Abonos" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polCifCtrl"   HeaderStyle-Width="100px" HeaderText="Cifra Control" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Center"/>

                                                 <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="70px"  ItemStyle-Width="70px" 
                                                                         DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                         ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Estatus">
                                                 </telerik:GridImageColumn> 
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
                                        <td style=" width:60px; background-color:transparent;  vertical-align:top;">
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                     
                                        </td>    
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                        </td>
                                        <td style=" width:70px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image1" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitR.png" />
                                            <asp:Label ID="Label1" runat="server" Text="Reg."></asp:Label>
                                        </td>
                                 
                                        <td style=" width:90px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image7" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGV.png" />
                                            <asp:Label ID="Label12" runat="server" Text="Gen. Val."></asp:Label>
                                        </td>
                                        <td style=" width:95px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image5" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitGE.png" />
                                            <asp:Label ID="Label9" runat="server" Text="Gen. Error"></asp:Label>
                                        </td>
                                        <td style=" width:80px; background-color:transparent;  vertical-align:top;">
                                            <asp:Image ID="Image4" runat="server" Width="12px" Height="12px" ImageUrl="~/Imagenes/icoSituacion/imgSitI.png" />
                                            <asp:Label ID="Label4" runat="server" Text="Incorp."></asp:Label>
                                        </td>
   

                                        </tr>
                                    </table>     
                                </div>                         
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>







            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>

            <asp:HiddenField ID="hdfRawUrl" runat="server" />
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />

             <telerik:RadWindow ID="RadWindowVerErrores" runat="server" OnClientClose="closeRadWindow"  Title="Errores" Width="700px" Height="440px" Modal="true" VisibleStatusbar="false" VisibleTitlebar="true" Behaviors="Close"  >               
            </telerik:RadWindow>


        </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

