<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArtSustitutos.aspx.cs" Inherits="DC_MttoArtSustitutos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
    <form id="form2" runat="server">    
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
                <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK" Visible="false" ></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
            </asp:Panel>                                                                                 

 
            <div>
            <fieldset >   
                <div style="width:100%; display:table; position:static; background-color:transparent;"   >

                        <table border="0" style=" width:550px; text-align:left; background-color:transparent;">
                            <tr style="height:18px;">
                                <td style=" width:100px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="Clave" BackColor=""  ></telerik:RadLabel>  
                                </td>
                                <td style=" width:450px;  background-color:transparent;">                             
                                    <telerik:RadLabel ID="rLblDescripcion" runat="server"   BackColor="" Text=""></telerik:RadLabel>    
                                </td>
                            </tr>
                        </table>

                </div>
            </fieldset>
            </div>
   

    <div>
        <fieldset >

        <telerik:RadLabel ID="rLblArtSustitutos" runat="server">Sustituto:</telerik:RadLabel>
         <telerik:RadComboBox ID="rCboArtSustitutos" runat="server"  AutoPostBack="True"  Enabled="false" 
                              HighlightTemplatedItems="true"
                              DropDownCssClass="cssRadComboBox" 
                              DropDownWidth="390px" 
                               Height="200px"   Width="250px"  
                              OnSelectedIndexChanged="rCboArtSustitutos_SelectedIndexChanged"  >
              <HeaderTemplate>
                        <table style="width: 360px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 160px;">
                                        Clave
                                    </td>
                                    <td style="width: 200px;">
                                        Descripción
                                    </td>
                                </tr>
                            </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <table style="width: 360px;"  cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width:160px" >
                                        <%# DataBinder.Eval(Container.DataItem, "artCve")%>
                                    </td>
                                    <td style="width: 200px;">
                                        <%# DataBinder.Eval(Container.DataItem, "artDes") %>
                                    </td>
                                  
                                </tr>
                            </table>

                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:Literal runat="server" ID="RadComboItemsCount" />
                    </FooterTemplate>
         </telerik:RadComboBox>    
            
           
            <telerik:RadTextBox ID="rTxtEliartCveSus" runat="server" Visible = "false" Enabled="false" ></telerik:RadTextBox><br /><br />

            </fieldset>

        <div style="width:100%; margin-top:5px; display:table; position:static; background-color:transparent;" align="center" >  

            <telerik:RadGrid ID="rGdv_ArtSustitutos"  
                runat="server"  
                AutoGenerateColumns ="false"
                OnSelectIndexChanged="rGdv_ArtSustitutos_SelectedIndexChanged" 
                OnDataBound="rGdv_ArtSustitutos_DataBound"
                Width="99%"  Height="170px"  
                 CssClass="Grid" 
                Skin="Office2010Silver" >
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="artCve" CssClass="GridTable"  >
                    <NoRecordsTemplate>
                        No se encontraron registros.
                    </NoRecordsTemplate>
                    <Columns>
                 
                        <telerik:GridBoundColumn DataField="artCveSus" HeaderStyle-Width="200px" HeaderText="Clave" ItemStyle-Width="200px" />
                         <telerik:GridBoundColumn DataField="artDesSus" HeaderStyle-Width="340px" HeaderText="Descripción" ItemStyle-Width="340px" />
                    </Columns>
                </MasterTableView>
                    <HeaderStyle CssClass="GridHeaderStyle"/>
                    <ItemStyle CssClass="GridRowStyle"/>
                    <AlternatingItemStyle  CssClass="GridAlternatingRowStyle" />
                    <selecteditemstyle CssClass="GridSelectedItem"/>
                    <FooterStyle CssClass="GridFooterStyle" />

                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true" >
                        <Selecting AllowRowSelect="true" EnableDragToSelectRows="False" ></Selecting>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true"    ScrollHeight="280px"     />
                    </ClientSettings>

            </telerik:RadGrid>


   </div>                 
              




         
            </div>
           
           

        <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
            <telerik:RadImageButton ID="rBtnGuardar"  runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"  Text ="" OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
            <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text="" OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
        </asp:Panel>


  
        <asp:HiddenField ID="hdfBtnAccion" runat="server" />


                  </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
   <telerik:RadWindowManager ID="RadWindowManager2" runat="server"></telerik:RadWindowManager>


    </form>
</body>
</html>
