<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CierreIN.aspx.cs" Inherits="IN_CierreIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
     <link href="/css/cssControles.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
     <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
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

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
        <div>

            <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar" Text="" OnClick="rBtnLimpiar_Click"  ></telerik:RadImageButton>    
            </asp:Panel>

            <fieldset>   
            <legend>Parametros de Cierre</legend>
                <div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >

                    <table border="0" style=" width:320px; text-align:left; background-color:transparent ;">
                        <tr style="height:18px;">
                            <td style=" width:30px; background-color:transparent;">
                                <telerik:RadLabel ID="lblanio" runat="server" Text="Año"></telerik:RadLabel>  
                            </td>
                            <td style=" width:120px;  background-color:transparent;">                             
                                        <telerik:RadComboBox ID="rCboAnioPeriodo" runat="server" Width="100px"  
                                                    HighlightTemplatedItems="true"
                                                    DropDownCssClass="cssRadComboBox" 
                                                    DropDownWidth="120px" 
                                                    Height="170px" >
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
                                                            <table style="width: 100px;"  cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width:100px" >
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
                            <td style=" width:200px; background-color:transparent;">
                                <asp:CheckBox ID="CheckCierraPeriodos" runat="server" Checked="false" Text="Cierra Calendario"  />
                            </td>
        
                        </tr>
                    </table>           
            
                </div>
            </fieldset>

     
            <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  
                    <telerik:RadGrid ID="rGdvInformacion" 
                        runat="server"
                        AutoGenerateColumns ="false" 
                        Width="330px" Height="200px" 
                        CssClass="Grid" 
                        skin="Office2010Silver"  
                          >

                        <MasterTableView AutoGenerateColumns="False"  
                                          CssClass="GridTable" >
                         <Columns >
                            <telerik:GridBoundColumn DataField="perAnio"  HeaderText="Año" HeaderStyle-Width="100px" ItemStyle-Width="50px"/>   
                            <telerik:GridImageColumn DataType="System.String" DataImageUrlFields="situacion"  HeaderStyle-Width="60px"  ItemStyle-Width="60px" 
                                                     DataAlternateTextField="ContactName" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                     ImageAlign="Middle" ImageHeight="11px" ImageWidth="11px" HeaderText="Periodo Cerrado">  
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
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="200px"     />
                    </ClientSettings>

                </telerik:RadGrid>  
            </div>    

            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click" OnClientClicking="OnClientClic_ConfirmOK"  ></telerik:RadImageButton>
            </asp:Panel>

        </div>
        </ContentTemplate>
        </asp:UpdatePanel>  

    <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>


    </form>
</body>
</html>
