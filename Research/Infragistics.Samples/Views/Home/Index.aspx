<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MvcSite.Master" Inherits="Infragistics.Web.Core.Framework.Mvc.ViewPage<Infragistics.Samples.Models.ProductFamilyViewModel>" %>

<%@ Register Assembly="Infragistics.Web.SampleBrowser.Core.Framework" Namespace="Infragistics.Web.SampleBrowser.Core.Framework.Web.UI.Controls"
    TagPrefix="IGCustom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <%--<%= Model.ProductControls.PageTitle %>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="content" runat="server">
    <% 
		this.overview.ContentCollection = Model.ProductControls; 
    %>
    <div class="content_area samples">
        <h2>
            <%= this.GetGlobalResourceObject("SampleBrowserResources","Featured_Controls") %></h2>
		<IGCustom:ProductControlsOverviewControl ID="overview" runat="server" />
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="beforeClosingBody" runat="server">
</asp:Content>
