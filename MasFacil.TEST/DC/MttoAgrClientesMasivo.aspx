<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoAgrClientesMasivo.aspx.cs" Inherits="DC_MttoAgrClientesMasivo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
      <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
      <script src="../Scripts/JSGeneral.js" type="text/javascript"></script>
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
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
    <div>
        <fieldset >     
            <legend>Agrupación/Dato Clientes</legend>
                <table>
                    <tr>
                        <td>
                        <telerik:RadTextBox  runat="server" ID="radTxtBoxBusqueda" Width="290px"  Enabled="false"
                            EnabledStyle-CssClass="cssTxtEnabled" 
                            DisabledStyle-CssClass ="cssTxtEnabled" 
                            HoveredStyle-CssClass="cssTxtHovered"
                            FocusedStyle-CssClass="cssTxtFocused"
                            InvalidStyle-CssClass="cssTxtInvalid" MaxLength="40" ></telerik:RadTextBox>
                        <telerik:RadImageButton Font-Names="Futura Bk BT"  ID="rBtnBuscar"  runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrL="~/Imagenes/icoAyuda/Buscar.png" Image-Url="~/Imagenes/icoAyuda/Buscar.png" Image-HoveredUrl="~/Imagenes/icoAyuda/Buscar.png"    ToolTip="Buscar"  Text =""  AutoPostBack="true"  Enabled="true"  OnClick="rBtnBuscar_Click" ></telerik:RadImageButton>
                        </td>
                    </tr>
                </table>
                <table  style=" text-align:left; background-color:transparent;">
                    <tr>
                    <td >
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text=" Agrupación:">
                           
                        </telerik:RadLabel>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rCboAgrupaciones" runat="server" Width="200px"  
                                HighlightTemplatedItems="true" AutoPostBack="true"  OnSelectedIndexChanged="rCboAgrupaciones_SelectedIndexChanged"
                                DropDownCssClass="cssRadComboBox" 
                                DropDownWidth="220px" 
                                Height="160px"  >
                                    <HeaderTemplate>
                                        <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 80px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 70px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:80px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "agrCve")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDes") %>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                                    </FooterTemplate>
                            </telerik:RadComboBox>


                    </td>
                    <td>
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text=" Dato:"> </telerik:RadLabel>
                    </td>
                    <td>
                       <%-- <telerik:RadComboBox ID="rCboAgrupacionesDatos" runat="server" AutoPostBack="True">
                        </telerik:RadComboBox>--%>
                          <telerik:RadComboBox ID="rCboAgrupacionesDatos" runat="server" Width="200px"   OnSelectedIndexChanged="rCboAgrupacionesDatos_SelectedIndexChanged"
                                HighlightTemplatedItems="true"  AutoPostBack="true" Enabled="false"
                                DropDownCssClass="cssRadComboBox" 
                                DropDownWidth="220px" 
                                Height="160px"  >
                                    <HeaderTemplate>
                                        <table style="width: 150px" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="width: 80px;">
                                                        Clave
                                                    </td>
                                                    <td style="width: 70px;">
                                                        Descripción
                                                    </td>
                                                </tr>
                                            </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 250px;"  cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width:80px" >
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoCve")%>
                                                </td>
                                                <td style="width: 170px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "agrDatoDes") %>
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
                <table style="background-color:transparent; width:100%;  text-align:left; " >
                        <tr>
                            <td  style=" width:100px; background-color:transparent; ">
                                <asp:RadioButton ID="RdioBtnRelacionados" runat="server" GroupName="ArtAlm" Text="Relacionados" OnCheckedChanged="RdioBtnRelacionados_CheckedChanged" Enabled="false"  AutoPostBack="true" Checked="false"/>
                            </td>
                            <td  style=" width:120px; background-color:transparent;">
                                <asp:RadioButton ID="RdioBtnNoRel" runat="server" GroupName="ArtAlm"   Text="No Relacionados"  OnCheckedChanged="RdioBtnNoRel_CheckedChanged"  Enabled="false" AutoPostBack="true" Checked="false" />
                            </td>
                            <td  style=" width:60px; background-color:transparent; ">
                                <asp:RadioButton ID="RdioBtnTodos" runat="server" GroupName="ArtAlm"   Text="Todos"  OnCheckedChanged="RdioBtnTodos_CheckedChanged" Enabled="false" AutoPostBack="true" Checked="true" />
                            </td> 
                        </tr>
                        <tr>
                            <td>
                                  <telerik:RadCheckBox ID="CheckBoxTodos" runat="server" OnClick="CheckBoxTodos_Click" Text="Todos" ></telerik:RadCheckBox>
                            </td>
                        </tr>
                    </table>
                    <%--DATALIST--%>
                  <div style=" overflow:auto; width:570px; height:290px; background-color:transparent ;" >
                   <table style="border: 1px" >
                  <asp:DataList ID="DataListClie" runat="server" DataKeyField="cliCve"     >
                      <ItemTemplate>
                            <tr >
                                <td >
                                    <telerik:RadCheckBox ID="CheckBoxArt" runat="server" OnCheckedChanged="CheckBoxArt_CheckedChanged" Value='<%# Eval("cliCve") %>' ></telerik:RadCheckBox>
                                </td>
                                <td  style=" width:200px; height:1px; ">
                                    <telerik:RadLabel ID="lblcliCve" runat="server" Text='<%# Eval("cliCve") %>'    ></telerik:RadLabel>
                                </td>
                                <td  style=" width:280px; height:1px;  ">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("clieNom") %>' ></asp:Label>
                                    <telerik:RadLabel ID="lblValCheck" runat="server" Text='<%# Eval("valCheck") %>' Visible="false"></telerik:RadLabel>
                                </td>
                            </tr>
                      </ItemTemplate>
                  </asp:DataList>
                  </table>
                  </div>
          </fieldset>
           <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server" HorizontalAlign="Right">
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text =""  OnClick="rBtnGuardar_Click"    OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png" ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click"  OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
           </asp:Panel>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>
     <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
    </telerik:RadWindowManager>
    </form>
</body>
</html>
