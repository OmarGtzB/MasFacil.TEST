<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="PolizasAcum.aspx.cs" Inherits="CG_PolizasAcum" %>

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

             <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="pnlDatos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="pnlDatos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>                                       
                    </UpdatedControls>
                </telerik:AjaxSetting>               
            </AjaxSettings>
        </telerik:RadAjaxManager>


        <div>
             <asp:Panel ID="pnlDatos" runat="server">          
           
            
            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnPolizaImp"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnImprimirDisabled.png"       Image-Url="~/Imagenes/IcoBotones/IcoBtnImprimir.png"     Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnImprimirHovered.png"  ToolTip="Imprime Poliza"  Text="" Visible="true" OnClick="rBtnPolizaImp_Click"  ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px"   Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"        Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png"      Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>
            </asp:Panel>     


            <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none; width:100%; text-align:left; background-color:transparent ;">
                <tr  style="width:100%;">

                    <td style=" width:100%; background-color:transparent; vertical-align:top;">
                        
                        <fieldset style="">
                            <legend>Filtros</legend>
                            <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" > 

                                <%--GRET, Se agregan Filtros para Prefijo,A.Contable, Descripcion,cptoId y Fecha --%>

                                <table border="0" cellpadding="0" cellspacing="0" style=" border-style:none;   text-align:left; background-color:transparent;">

                                    <tr>

                                    <td style=" width:60px; background-color:transparent; vertical-align:top;">
                                    <asp:Label ID="Label6" runat="server" Text="Prefijo"></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboPrefijo" runat="server" Width="85px" AutoPostBack="true" OnSelectedIndexChanged="rCboPrefijo_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="85px" 
                                                    Height="150px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <%--<td style="width: 100px;">
                                                                        Clave
                                                                    </td>--%>
                                                                    <td style="width: 100px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <%--<td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "ciaCve")%>
                                                                </td>--%>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "PrefCpto") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>

                                    </td>



                                    <td style=" width:85px; background-color:transparent; vertical-align:top;">
                                    <asp:Label ID="Label2" runat="server" Text="A.Contable"></asp:Label>
                                    </td>
                                    <td style=" width:180px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboAContable" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="rCboAContable_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="150px" 
                                                    Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 200px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <%--<td style="width: 100px;">
                                                                        Clave
                                                                    </td>--%>
                                                                    <td style="width: 100px;">
                                                                        Descripción
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 300px;"  cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <%--<td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "ciaCve")%>
                                                                </td>--%>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "PolCve") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                                    </FooterTemplate>
                                            </telerik:RadComboBox>

                                    </td>


                                        <td style=" width:90px; background-color:transparent; vertical-align:top;">
                                    <asp:Label ID="Label3" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td style=" width:330px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboDescripcion" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="rCboDescripcion_SelectedIndexChanged"
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
                                                                <%--<td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "PolCve")%>
                                                                </td>--%>
                                                                <td style="width: 200px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "PolDes") %>
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
                                    <asp:Label ID="Label5" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td style=" width:240px; background-color:transparent; vertical-align:top;">
                
                                         <telerik:RadComboBox ID="rCboConcepto" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="rCboConcepto_SelectedIndexChanged"
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="300px" 
                                                    Height="200px"  >
                                                    <HeaderTemplate>
                                                        <table style="width: 300px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                   <%-- <td style="width: 100px;">
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
                                                                <%--<td style="width:100px" >
                                                                    <%# DataBinder.Eval(Container.DataItem, "cptoId")%>
                                                                </td>--%>
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

                                        <td style=" width:50px; background-color:transparent; vertical-align:top;">
                                        <asp:Label ID="Label8" runat="server" Text="Fecha"></asp:Label>
                                    </td>
                                    <td style=" width:100px; background-color:transparent; vertical-align:top;">
                                         <telerik:RadDatePicker ID="RdDateFecha" runat="server" Width="120px"    AutoPostBack="true" OnSelectedDateChanged="RdDateFecha_SelectedDateChanged" ></telerik:RadDatePicker>
                                     </td>

                                    </tr>

                                </table>

                                <%--  --%>

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
                                                 <telerik:GridBoundColumn DataField="polDes"  HeaderStyle-Width="300px" HeaderText="Descripción" ItemStyle-Width="300px"  />
                                                 <telerik:GridBoundColumn  DataField="cptoId" HeaderStyle-Width="290px" HeaderText="Concepto" ItemStyle-Width="290px"  />
                                                 <telerik:GridBoundColumn DataField="polFec"   HeaderStyle-Width="80px" HeaderText="Fecha" ItemStyle-Width="80px" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polSumCgo"   HeaderStyle-Width="100px" HeaderText="Cargos" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polSumAbo"   HeaderStyle-Width="100px" HeaderText="Abonos" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Center"/>
                                                 <telerik:GridBoundColumn DataField="polCifCtrl"   HeaderStyle-Width="100px" HeaderText="Cifra Control" ItemStyle-Width="100px"  DataFormatString="{0:###,##0.00}" ItemStyle-HorizontalAlign="Right"  HeaderStyle-HorizontalAlign="Center"/>

                                                 <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="imgSit"  HeaderStyle-Width="70px"  ItemStyle-Width="70px" 
                                                                         DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                         ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Estatus" Display="false">
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
</asp:Panel>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

