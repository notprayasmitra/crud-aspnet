<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="crud_aspnet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="padding-top: 30px">
        <h2>Student Management System (CRUD)</h2>
        <hr />

        <div class="row">
            <div class="col-md-4">
                <h4>Manage Student</h4>
                <asp:HiddenField ID="hfStudentId" runat="server" />

                <div class="form-group">
                    <label>First Name:</label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>