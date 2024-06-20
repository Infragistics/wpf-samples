<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MvcSample.Master"
	Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<%= Model.Head %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%= Model.Body %>
</asp:Content>
<asp:Content ContentPlaceHolderID="beforeClosingBodyPlaceHolder" runat="server">
</asp:Content>
