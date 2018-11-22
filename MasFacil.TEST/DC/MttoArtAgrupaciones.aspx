<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MttoArtAgrupaciones.aspx.cs" Inherits="DC_MttoArtAgrupaciones" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

     <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

         .cspnlBtnsAcciones {
             background-color:transparent; 
               /*background-color:red;*/ 
             text-align:left;
  
         }
         .cspnlBtnsaplicaAccion {
             background-color:transparent; 
               /*background-color:red;*/ 
             text-align:right;
  
         }
        html, body {
            overflow: hidden;
        }
}
        /*.auto-style1 {
            height: 44px;
        }
        .auto-style2 {
            float: left;
            width: 102px;
            height: 60px;
        }
        .auto-style3 {
            margin-right: 0px;
        }
        .auto-style4 {
            height: 164px;
            width: 511px;
        }
        .auto-style5 {
            height: 82px;
            width: 515px;
        }
        .auto-style6 {
            height: 68px;
            width: 515px;
        }
        .auto-style7 {
            left: 86px;
            top: 4px;
        }
        .auto-style8 {
            left: -74px;
            top: 5px;
        }*/
        .auto-style1 {
            width: 152px;
        }
        .auto-style2 {
            width: 93px;
        }
        .auto-style3 {
            width: 61px;
        }
        .auto-style4 {
            width: 53px;
        }
        .auto-style6 {
            width: 514px;
            height: 22px;
        }
    </style>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
                                                                                
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server">
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="35px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text ="" OnClick="rBtnNuevo_Click"></telerik:RadImageButton><%-- OnClick="rBtnNuevo_Click"  --%>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="35px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="35px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"  Text="" OnClick="rBtnLimpiar_Click"></telerik:RadImageButton>
                </asp:Panel>                                                                                  
                 
                  
          
            <fieldset style="  margin-top:5px;   display: block; text-align:left;" >   
            <legend>Articulo</legend>
                <%--<div style="width:100%; display:table; position:static; background-color:transparent;" align="center" >--%>

                        <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                            <tr style="height:18px;">
                                <td style=" width:140px; background-color:transparent;">
                                    <telerik:RadLabel ID="rLblClave" runat="server" Text="Clave"></telerik:RadLabel>  
                                </td>
                                <td style=" width:540px;  background-color:transparent;">                             
                                    <telerik:RadLabel ID="rLblDescripcion" runat="server" Text="Descripcion"></telerik:RadLabel>    
                                </td>
                            </tr>
                        </table>

<%--                </div>--%>
            </fieldset>
    
                          <fieldset style=" margin-top:10px;  display: block; text-align:left;" >     
                          <legend>Agrupación/Dato</legend>
         <table border="0" style=" text-align:left; background-color:transparent;" class="auto-style6">
                   <%--          
                    <tr >--%>
                        
                                    
                           <%--     </tr>
                           <tr>--%>
                               <tr>
                                   <td class="auto-style2">
                                       <telerik:RadLabel ID="RadLabel2" runat="server">
                                           Agrupación:
                                       </telerik:RadLabel>
                                   </td>
                                   <td class="auto-style1">
                                       <telerik:RadComboBox ID="rCboAgrupaciones" runat="server" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="rCboAgrupaciones_SelectedIndexChanged">
                                       </telerik:RadComboBox>
                                   </td>
                                   <td class="auto-style4">
                                       <telerik:RadLabel ID="RadLabel1" runat="server">
                                           Dato:
                                       </telerik:RadLabel>
                                   </td>
                                   <td>
                                       <telerik:RadComboBox ID="rCboAgrupacionesDatos" runat="server" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="rCboAgrupacionesDatos_SelectedIndexChanged">
                                       </telerik:RadComboBox>
                                   </td>
                                   <caption>
                                       <%--               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                   </caption>
                   </tr>
                                </table>
                                                             <caption>
                                 
                                   <fieldset style=" margin-top:10px;  display: block; text-align:left;">
                                       <table border="0" style=" width:640px; text-align:left; background-color:transparent ;">
                                           <tr>
                                               <td>
                                                   <scrolling allowscroll="True" frozencolumnscount="2" savescrollposition="true" usestaticheaders="True">
                                                   </scrolling>
                                                   <telerik:RadGrid ID="rGdv_ArtAgrupaciones" runat="server" AllowMultiRowSelection="true" ClientSettings-Scrolling-AllowScroll="true" CssClass="auto-style3" Height="160px" Skin="Office2010Silver" Width="512px">
                                                       <%--OnSelectedIndexChanged="rGdv_Agrupaciones_SelectedIndexChanged"--%>
                                                       <MasterTableView AutoGenerateColumns="False" DataKeyNames="artCve" GroupsDefaultExpanded="true" Width="100px">
                                                           <NoRecordsTemplate>
                                                               No se encontraron registros.
                                                           </NoRecordsTemplate>
                                                           <Columns>
                                                               <telerik:GridBoundColumn DataField="agrCve" HeaderStyle-Width="115px" HeaderText="Clave Agrupación" ItemStyle-Width="09px">
                                                               </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn DataField="agrDes" HeaderStyle-Width="138px" HeaderText="Descripción Agrupación" ItemStyle-Width="68px">
                                                               </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn DataField="agrDatoCve" HeaderStyle-Width="115px" HeaderText="Clave Dato" ItemStyle-Width="79px">
                                                               </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn DataField="agrDatoDes" HeaderStyle-Width="138px" HeaderText="Descripción Dato" ItemStyle-Width="08px">
                                                               </telerik:GridBoundColumn>
                                                               <telerik:GridBoundColumn DataField="ciaCve" HeaderStyle-Width="115px" HeaderText="compmnia" ItemStyle-Width="09px" Visible="false">
                                                               </telerik:GridBoundColumn>
                                                           </Columns>
                                                       </MasterTableView>
                                                       <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                       <ClientSettings EnablePostBackOnRowClick="true" Selecting-AllowRowSelect="true">
                                                           <Selecting AllowRowSelect="True" EnableDragToSelectRows="False" />
                                                           <Scrolling UseStaticHeaders="true" />
                                                           <Animation AllowColumnReorderAnimation="True" />
                                                       </ClientSettings>
                                                       <FilterMenu RenderMode="Lightweight">
                                                       </FilterMenu>
                                                       <HeaderContextMenu RenderMode="Lightweight">
                                                       </HeaderContextMenu>
                                                   </telerik:RadGrid>
                                               </td>
                                           </tr>
                                       </table>
                                   </fieldset> <%--      <div style="width:100%; display:table;  position:static; background-color:transparent;"  >--%>
                                   <asp:Panel ID="pnlBtnsAplicaAccion" runat="server" CssClass="cspnlBtnsAplicaAccion" Width="100%">
                                       <telerik:RadImageButton ID="rBtnGuardar" runat="server" 
    OnClientClicking="OnClientClicking" ButtonType="StandardButton"    Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"   OnClick="rBtnGuardar_Click"  Enabled="false" ToolTip="Guardar" Width="80px">
</telerik:RadImageButton>
                            <telerik:RadImageButton ID="rBtnCancelar"  runat="server" 
    OnClientClicking="OnClientClickk" ButtonType="StandardButton"    Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"   OnClick="rBtnCancelar_Click"  Enabled="false" ToolTip="Cancelar" Width="80px">
</telerik:RadImageButton>
  
                                   </asp:Panel>
                                   <asp:HiddenField ID="hdfBtnAccion" runat="server" />
                                   <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                   </telerik:RadWindowManager>
                                   <telerik:RadTextBox ID="rTxtagrCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                   <telerik:RadTextBox ID="rTxtagrDatoCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                   <telerik:RadTextBox ID="rTxagrTipId" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                                   <telerik:RadTextBox ID="rTxtciaCve" runat="server" Enabled="false" Visible="false">
                                   </telerik:RadTextBox>
                               </caption>
                   </tr>
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
                                   <script type="text/javascript">
    function OnClientClickk(sender, args) {
        var callBackFunction = Function.createDelegate(sender, function(argument) {
            if (argument) {
                this.click();
            }
        });
        var text = "Esta seguro que quiere cancelar la acción?";
        radconfirm(text, callBackFunction, 300, 100, null, "Title");
        args.set_cancel(true);
    }
</script>

    </ContentTemplate>
    </asp:UpdatePanel> 


    </form>
                          


 
</body>

</html>
