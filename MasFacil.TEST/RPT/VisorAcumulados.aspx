<%@ Page Title="" Language="C#" MasterPageFile="~/MPForm.master" AutoEventWireup="true" CodeFile="VisorAcumulados.aspx.cs" Inherits="RPT_VisorAcumulados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMPForm" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMPForm" Runat="Server">

    <telerik:RadPivotGrid ID="rPivoGrid" runat="server" OnNeedDataSource="rPivoGrid_NeedDataSource"   Culture="es"
        AllowSorting="true" AllowFiltering="false" ShowFilterHeaderZone="false"
        Height="500px" Width="1280px" >
        <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                </ClientSettings>
                    <Fields>
 
                     <telerik:PivotGridColumnField DataField="uniMedCve" Caption="Unidad de Medida" />
                      
                         <telerik:PivotGridRowField DataField="artDes" Caption="Articulo"/>
                      <telerik:PivotGridRowField DataField="ciaCve" Caption="Compania"/>
                        
                        <%--<telerik:PivotGridAggregateField ZoneIndex="16" DataField="mi cantidad" DataFormatString="{0:F2}">
                             <celltemplate>
                                <asp:Label ID="Label1" runat="server" Style="color: #aaa">
                                 <%# Container.DataItem %>
                                </asp:Label>
                             </celltemplate>
                        </telerik:PivotGridAggregateField> --%>
                        
                        <telerik:PivotGridAggregateField ZoneIndex="16" DataField="docRegPartCant" Caption="Cantidad" UniqueName="docRegPartCant" DataFormatString="{0:F2}"/> 
                        <telerik:PivotGridAggregateField ZoneIndex="17" DataField="docRegPartPrec" Caption="docRegPartPrec" UniqueName="docRegPartPrec" DataFormatString="{0:F2}"/> 
                        <telerik:PivotGridAggregateField ZoneIndex="18" DataField="docRegPartImpBrut" Caption="docRegPartImpBrut" UniqueName="docRegPartImpBrut" DataFormatString="{0:F2}"/> 
                       
            </Fields>
   
   <ColumnHeaderCellStyle Width="100px"></ColumnHeaderCellStyle>
        <ClientSettings>
            <Scrolling AllowVerticalScroll="true" SaveScrollPosition="true"></Scrolling>
            <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
        </ClientSettings>


    </telerik:RadPivotGrid>
</asp:Content>

