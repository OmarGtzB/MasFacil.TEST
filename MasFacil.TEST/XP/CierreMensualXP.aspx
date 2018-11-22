<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CierreMensualXP.aspx.cs" Inherits="XP_CierreMensualXP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
     <link href="~/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />
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

        <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
            <ContentTemplate>
                 <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" Height="34px">
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text=""   OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>      
                 </asp:Panel>
                <div>
                    <fieldset> 
                        <legend>Parámetros de Cierre</legend> 
                        <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >
                            
                            <table border="0" style=" width:350px; text-align:left; background-color:transparent ;">
                                <tr style="height:18px;">
                                    <td style=" width:50px; background-color:transparent;">
                                        <asp:Label runat="server" Text="Año"></asp:Label>
                                    </td>
                                    <td style=" width:120px; background-color:transparent;">
                                     <telerik:RadComboBox ID="rCboAnio" runat="server" Width="90px" Enabled="true" OnSelectedIndexChanged="rCboAnio_SelectedIndexChanged"
                                        HighlightTemplatedItems="true" 
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="120px" Height="120px" 
                                        AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 100px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Año
                                                        </td>
    
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100px" cellspacing="0" cellpadding="0">
                                                     <tr>
                                                        <td style="width: 100px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "perAnio") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Literal runat="server" ID="RadComboItemsCount" />
                                            </FooterTemplate>
                                       </telerik:RadComboBox>
                                    </td>
                                    <td style=" width:50px; background-color:transparent;">
                                        <asp:Label runat="server" Text="Mes"></asp:Label>
                                    </td>
                                    <td style=" width:130px; background-color:transparent;">
                                     <telerik:RadComboBox ID="rCboMes" runat="server" Width="100px" Enabled="false"
                                        HighlightTemplatedItems="true" 
                                        DropDownCssClass="cssRadComboBox"  
                                        DropDownWidth="120px" Height="210px" 
                                        AutoPostBack="True" >
                                            <HeaderTemplate>
                                                <table style="width: 100px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Mes
                                                        </td>      
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                        <%# DataBinder.Eval(Container.DataItem, "perDes") %>
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



                     <table>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rGdvCierreMensual"
                                    runat="server"
                                    AllowMultiRowSelection="true"
                                    AutoGenerateColumns="False" 
                                    Width="380px" Height="300px"   
                                    CssClass="Grid" 
                                    Skin="Office2010Silver">
                                    <MasterTableView DataKeyNames="perNum" AutoGenerateColumns="False" CssClass="GridTable" >
                                        <Columns >
                                                <telerik:GridBoundColumn DataField="perAnio" HeaderText="Año"  HeaderStyle-Width="80px"  ItemStyle-Width="80px" />

                                                <telerik:GridBoundColumn DataField="perNum"  HeaderText="Mes" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="false" />
                                                <telerik:GridBoundColumn DataField="perDes"  HeaderText="Mes" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                            

                                                <telerik:GridBoundColumn DataField="moduCve"  HeaderText="modulo"  HeaderStyle-Width="80px" ItemStyle-Width="80px"  Display="false" />

                                                <telerik:GridBoundColumn DataField="perCieSit" HeaderText="Periodo Cerrado"  HeaderStyle-Width="100px"  ItemStyle-Width="100px" Display="false"  />

                                                <telerik:GridBoundColumn DataField="perCieFec"  HeaderText="FechaCierre" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="false" />  
                                                
                                                <telerik:GridImageColumn DataType="System.String"  DataImageUrlFields="imgSit"  HeaderStyle-Width="50px"  ItemStyle-Width="100px" 
                                                                          ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                         ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Situacion"></telerik:GridImageColumn> 
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
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="225px"     />
                                        </ClientSettings>
                                </telerik:RadGrid>  
                            </td>
                        </tr>
                     </table>
                </div>
                       <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                            <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""    OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                        </asp:Panel>
                        <asp:HiddenField ID="hdfBtnAccion" runat="server" />  
            </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>

    </form>
</body>
</html>
