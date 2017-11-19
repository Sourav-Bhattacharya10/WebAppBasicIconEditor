<%@ Page Title="Home" Language="C#" MasterPageFile="~/BasicIconEditorSite.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebAppBasicIconEditor.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Designs/CustomCss/HomePage.css" />

    <script type="text/javascript" src="Designs/CustomJs/HomePage.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Page Header--%>
    <div class="display-4 text-center">
        Basic Icon Editor
    </div>
    <br />
    <div class="row">
        <div class="col">

        </div>
        <div class="col">
            <asp:FileUpload ID="ImageFileUpload" runat="server" Width="800"/>
        </div>
        <div class="col">

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col">

        </div>
        <div class="col">
            <center>
                <asp:Button ID="UploadButton" runat="server" Text="Upload Seleted Image" OnClick="UploadButton_Click"/>
            </center>
        </div>
        <div class="col">

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col">

        </div>
        <div class="col">
            <center id="CenterTag">
                <asp:Image ID="Image1" runat="server" Width="200" Height="200" ImageUrl="~/Images/TransparentBackground.png" CssClass="BackgroundTransparent"/>
                <asp:Image ID="LoadedImage" runat="server" Width="200" Height="200" CssClass="RealImage"/>
            </center>
        </div>
        <div class="col">

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col">

        </div>
        <div class="col">
            <center>
                <asp:Button ID="TransparentButton" runat="server" Text="Make Transparent" OnClick="TransparentButton_Click"/>
            </center>
        </div>
        <div class="col">
            <center>
                Select the color : <input id="CP1" name="CP1" type="color" />
            </center>
        </div>
        <div class="col">
            <center>
                <asp:Button ID="ColorButton" runat="server" Text="Apply Selected Color" OnClick="ColorButton_Click"/>
            </center>
        </div>
        <div class="col">

        </div>
    </div>
    <br />
    <div class="row">
        <div class="col"></div>
        <div class="col">
            <center>
                <asp:Button ID="DownloadButton" runat="server" Text="Download Processed Image" OnClick="DownloadButton_Click"/>
            </center>
        </div>
        <div class="col"></div>
    </div>
</asp:Content>
