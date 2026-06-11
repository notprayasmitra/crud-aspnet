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
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group" style="margin-top: 10px">
                    <label>Last Name:</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group" style="margin-top: 10px">
                    <label>Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                </div>
                <div class="form-group" style="margin-top: 10px">
                    <label>Major:</label>
                    <asp:TextBox ID="txtMajor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div style="margin-top: 10px">
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary" />
            </div>

            <div class="col-md-8">
                <h4>Student Roster</h4>
                <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="gvStudents_RowDeleting" OnRowCommand="gvStudents_RowCommand"
                    CssClass="table table-striped table-bordered">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="first_name" HeaderText="First Name" />
                        <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="major" HeaderText="Major" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRow" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' CssClass="text-primary">Edit</asp:LinkButton> | 
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="text-danger">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>