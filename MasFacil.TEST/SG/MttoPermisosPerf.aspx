<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="MttoPermisosPerf.aspx.cs" Inherits="SG_MttoPermisosPerf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    
    <link href="../css/cssControles.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/MGM/jsForm.js" type="text/javascript"></script>
    <link href="../css/cssCtrlRadComboBox.css" rel="stylesheet" type="text/css" />
 
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
   <link href="/css/cssCtrlRadGrid.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div>
                <asp:Panel ID="pnlBtnsAcciones" Width="100%" CssClass="cspnlBtnsAcciones"  runat="server" >
                    <telerik:RadImageButton ID="rBtnNuevo"     runat ="server" Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnNuevoDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnNuevo.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnNuevoHovered.png"    ToolTip="Nuevo"  Text =""  OnClick="rBtnNuevo_Click"   ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnModificar" runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnModificarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnModificar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnModificarHovered.png"  ToolTip="Modificar"  Text="" OnClick="rBtnModificar_Click"   ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnEliminar"  runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnEliminarDisabled.png"  Image-Url="~/Imagenes/IcoBotones/IcoBtnEliminar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnEliminarHovered.png"  ToolTip="Eliminar"  Text="" OnClick="rBtnEliminar_Click"  OnClientClicking="OnClientClic_ConfirmOK"   ></telerik:RadImageButton>
                    <telerik:RadImageButton ID="rBtnLimpiar"   runat="server"  Width="30px" Height="30px" Image-Sizing="Original" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarDisabled.png"    Image-Url="~/Imagenes/IcoBotones/IcoBtnLimpiar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnLimpiarHovered.png"  ToolTip="Limpiar"   Text=""  OnClick="rBtnLimpiar_Click" ></telerik:RadImageButton>      
                </asp:Panel>
            </div>
            <div>
                <table>
                    <tr>
                        <%-- DATOS PERFIL--%>
                        <td>
                            <fieldset>
                            <legend>Datos Perfil</legend>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadLabel ID="RadLabel1" runat="server" Text="Clave"></telerik:RadLabel>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="rTxtCve" runat="server" MaxLength="10"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadLabel ID="RadLabel2" runat="server" Text="Descripción"></telerik:RadLabel>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="rTxtDes" runat="server" Width="320px"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadLabel ID="RadLabel3" runat="server" Text="Situación"></telerik:RadLabel>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="CheckRegActivo" GroupName="Situacion" Text="Activo" runat="server" Checked="true" />
                                            <asp:RadioButton ID="CheckRegInactivo" GroupName="Situacion" Text="Inactivo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <telerik:RadLabel ID="RadLabel4" runat="server" Text="Comentarios"></telerik:RadLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <textarea id="rTxtComentarios" cols="54" rows="2" runat="server" maxlength="150" ></textarea>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width:100%; display:table; margin-top:5px; position:static; background-color:transparent;" align="center">
                                        <telerik:RadGrid ID="rGdvPerfiles"   OnSelectedIndexChanged="rGdvPerfiles_SelectedIndexChanged"
                                            runat="server"
                                               AllowMultiRowSelection="true"
                                               AutoGenerateColumns="False" 
                                               Width="480px" Height="288px"   
                                               CssClass="Grid" 
                                               Skin="Office2010Silver">
                                            <MasterTableView DataKeyNames="maPerfCve" AutoGenerateColumns="False"  CssClass="GridTable" >
                                            <Columns >
                                                    <telerik:GridBoundColumn DataField="maPerfCve"  HeaderText="Clave"  HeaderStyle-Width="80px" ItemStyle-Width="80px" />
                                                    <telerik:GridBoundColumn DataField="maPerfDes" HeaderText="Descripción"  HeaderStyle-Width="230px"  ItemStyle-Width="230px" />
                                                    <telerik:GridBoundColumn DataField="maPerfStsDes"  HeaderText="Situación" HeaderStyle-Width="100px" ItemStyle-Width="100px" />
                                                    <telerik:GridBoundColumn DataField="maPerfSts"  HeaderText="Situación" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="false" />  
                                                    <telerik:GridBoundColumn DataField="maPerfComent"  HeaderText="Cometarios" HeaderStyle-Width="100px" ItemStyle-Width="100px" Display="false"  />              
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
                                     </div>  
                            </fieldset>
                        </td>

                         <%-- PANTALLA--%>
                        <td>
                        <fieldset>
                       <legend>Pantalla</legend>
                       <div style="width:550px; height:450px; background-color:transparent; overflow:scroll;"  >
                             <table>
                                <tr>
                                    <td>
                                       <telerik:RadLabel ID="RadLabel5" runat="server" Text="Modulo"></telerik:RadLabel>
                                    </td>
                                    <td>
                                    <telerik:RadComboBox ID="rCboModulo" runat="server" Width="180px" AutoPostBack="true"
                                    HighlightTemplatedItems="true" OnSelectedIndexChanged="rCboModulo_SelectedIndexChanged"
                                    DropDownCssClass="cssRadComboBox" 
                                    DropDownWidth="300px" 
                                    Height="200px"   >
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
                                                    <%# DataBinder.Eval(Container.DataItem, "maModuCve")%>
                                                </td>
                                                <td style="width: 200px;">
                                                    <%# DataBinder.Eval(Container.DataItem, "maModuDes") %>
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
                                <tr>
                                    <td colspan="2"  >

                                        <telerik:RadTreeView Enabled="true" ID="RadTreeView1"   SingleExpandPath="true"  runat="server" CheckBoxes="false" TriStateCheckBoxes="true" CheckChildNodes="false" OnNodeClick="RadTreeView1_NodeClick" >
                                        </telerik:RadTreeView>

                                    </td>
                                </tr>
                            </table>
                         </div>
                        </fieldset>
                        </td>

                         <%-- ACCIONES--%>
                        <td>
                            <fieldset>
                            <legend>Acciones</legend>
                                <div style="width:210px; height:450px; background-color:transparent; overflow:scroll;" >
                                    <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadCheckBox ID="RadCheckTodos" runat="server" OnCheckedChanged="RadCheckTodos_CheckedChanged" ></telerik:RadCheckBox>
                                                </td>
                                                <td>
                                                    <telerik:RadLabel ID="RadLabel9" runat="server" Text="Todos"></telerik:RadLabel>
                                                </td>
                                            </tr>
                                            <asp:DataList ID="DataListBtn" runat="server" DataKeyField="accId"    >
                                              <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadCheckBox ID="RadCheckBox3" runat="server" OnCheckedChanged="RadCheckBox3_CheckedChanged" Value='<%# Eval("accId") %>' ></telerik:RadCheckBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadImageButton ID="RadImageButton1" runat ="server" Width="30px" Height="30px" Image-Sizing="Original"  Image-Url='<%# Eval("accImg") %>'    ToolTipText='<%# Eval("accDes") %>'  Text =""    ></telerik:RadImageButton>
                                                        </td>
                                                        <td>
                                                            <telerik:RadLabel ID="RadLabel6" runat="server" Text='<%# Eval("accDes") %>' ></telerik:RadLabel>
                                                        </td>
                                                    </tr>
                                              </ItemTemplate>
                                           </asp:DataList>
                                    </table>
                                </div>
                             </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="pnlBtnsAplicaAccion" Width="100%" CssClass="cspnlBtnsAplicaAccion"  runat="server">
                <telerik:RadImageButton ID="rBtnGuardar"   runat ="server" Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnGuardarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnGuardar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnGuardarHovered.png"    ToolTip="Guardar"    Text=""  OnClick="rBtnGuardar_Click"  OnClientClicking="OnClientClic_ConfirmOK"></telerik:RadImageButton>
                <telerik:RadImageButton ID="rBtnCancelar"  runat="server"  Width="80px" Height="30px" Image-Sizing="Stretch" Image-DisabledUrl="~/Imagenes/IcoBotones/IcoBtnCancelarDisabled.png" Image-Url="~/Imagenes/IcoBotones/IcoBtnCancelar.png" Image-HoveredUrl="~/Imagenes/IcoBotones/IcoBtnCancelarHovered.png"  ToolTip="Cancelar"  Text=""  OnClick="rBtnCancelar_Click" OnClientClicking="OnClientClic_ConfirmCancel"></telerik:RadImageButton>
            </asp:Panel>
            
            <asp:HiddenField ID="hdfBtnAccion" runat="server" />
            <asp:HiddenField ID="SaveTree" runat="server" />
            <asp:HiddenField ID="ValModul" runat="server" />
            <asp:HiddenField ID="ValModulFirst" runat="server" />

            <telerik:RadWindowManager ID="RadWindowManagerPage" runat="server" Title=""  Modal="True" Behaviors="Close" VisibleStatusbar="false"     >
            </telerik:RadWindowManager>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

