<%@ Page Title="" Language="C#" MasterPageFile="~/MPSecurity.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="uikit/css/uikit.almost-flat.min.css" />
    <link rel="stylesheet" href="uikit/css/uikit.almost-flat.css" />
    <link rel="stylesheet" href="uikit/css/uikit.docs.min.css"/>


        <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
        
        <script src="uikit/js/uikit.min.js"></script>
        <script src="uikit/js/test.js"></script>
        <script src="uikit/js/components/form-password.js"></script>

        <link rel="stylesheet" href="uikit/form-password.css" />
        <link rel="stylesheet" href="uikit/form-password.min.css" /> 

        <link rel="stylesheet" href="uikit/css/components/autocomplete.css" />
        <link rel="stylesheet" href="uikit/css/components/autocomplete.min.css" />

        <link rel="stylesheet" href="uikit/css/components/autocomplete.gradient.css" />
        <link rel="stylesheet" href="uikit/css/components/autocomplete.gradient.min.css" />

        <link rel="stylesheet" href="uikit/css/autocomplete.almost-flat.css" />
        <link rel="stylesheet" href="uikit/css/autocomplete.almost-flat.min.css" />

        <link rel="stylesheet" href="uikit/js/components/autocomplete.js" />
        <link rel="stylesheet" href="uikit/js/components/autocomplete.min.js" />

        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div>

            <div>
                <div class="uk-vertical-align uk-text-center uk-height-1-1">
                
                        <div class="uk-vertical-align-middle" style="width: 250px; margin-top:50px; vertical-align:central">

                       <%-- <img class="uk-margin-bottom" width="140" height="120" src="data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjwhLS0gR2VuZXJhdG9yOiBBZG9iZSBJbGx1c3RyYXRvciAxNi4wLjQsIFNWRyBFeHBvcnQgUGx1Zy1JbiAuIFNWRyBWZXJzaW9uOiA2LjAwIEJ1aWxkIDApICAtLT4NCjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+DQo8c3ZnIHZlcnNpb249IjEuMSIgaWQ9IkViZW5lXzEiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIHg9IjBweCIgeT0iMHB4Ig0KCSB3aWR0aD0iMTQwcHgiIGhlaWdodD0iMTIwcHgiIHZpZXdCb3g9Ii0yOS41IDI3NS41IDE0MCAxMjAiIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgLTI5LjUgMjc1LjUgMTQwIDEyMCIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+DQo8ZyBvcGFjaXR5PSIwLjciPg0KCTxwYXRoIGZpbGw9IiNEOEQ4RDgiIGQ9Ik0tNi4zMzMsMjk4LjY1NHY3My42OTFoOTMuNjY3di03My42OTFILTYuMzMzeiBNNzkuNzg4LDM2NC4zNTVIMS42NTZ2LTU3LjcwOWg3OC4xMzJWMzY0LjM1NXoiLz4NCgk8cG9seWdvbiBmaWxsPSIjRDhEOEQ4IiBwb2ludHM9IjUuODYsMzU4LjE0MSAyMS45NjIsMzQxLjIxNiAyNy45OTUsMzQzLjgyNyA0Ny4wMzIsMzIzLjU2MSA1NC41MjQsMzMyLjUyMyA1Ny45MDUsMzMwLjQ4IA0KCQk3Ni4yMDMsMzU4LjE0MSAJIi8+DQoJPGNpcmNsZSBmaWxsPSIjRDhEOEQ4IiBjeD0iMjQuNDYyIiBjeT0iMzIxLjMyMSIgcj0iNy4wMzQiLz4NCjwvZz4NCjwvc3ZnPg0K" alt="">--%>
                        <img class="uk-margin-bottom" width="250" height="180" src="Images/Iconos/Management.png" alt="">
                        <asp:Panel ID="Panel1" runat="server" class="uk-panel uk-panel-box uk-form">

                            <div class="uk-form-row">
                                <asp:TextBox ID="txt_usuario" runat="server" placeholder="Usuario"  class="uk-width-1-1 uk-form-large" ></asp:TextBox>
                            </div>
                            <div class="uk-form-row">
                                <asp:TextBox ID="txt_pasword" runat="server" TextMode="Password" placeholder="Contraseña" class="uk-width-1-1 uk-form-large" ></asp:TextBox>
                            </div>
                            <div class="uk-form-row" id="btn_login_">
                               <asp:Button ID="RBtn_Login" runat="server" class="uk-width-1-1 uk-button uk-button-primary uk-button-large" Text="Iniciar sesión" OnClick="Click_RBtn_Login" />
                            </div>
                            <div  >
                                <asp:Label ID="lbl_LoginError" Visible="false"  runat="server" Text=""></asp:Label>
                            </div>
                            <div class="uk-form-row uk-text-small">
                                <%--<label class="uk-float-left"><input type="checkbox"> Remember Me</label>
                                <a class="uk-float-right uk-link uk-link-muted" href="#">Forgot Password?</a>--%>
                            </div>
                        </asp:Panel>
                      
                    </div>
                </div>
    
            </div>


        
    </div>
        

</asp:Content>

