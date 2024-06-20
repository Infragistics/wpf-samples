<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MvcOverviewSite.Master"
    Inherits="Infragistics.Web.Core.Framework.Mvc.ComponentViewPage<Infragistics.Samples.Models.ComponentViewModel>" %>

<%@ Register Assembly="Infragistics.Web.SampleBrowser.Core.Framework" Namespace="Infragistics.Web.SampleBrowser.Core.Framework.Web.UI.Controls"
    TagPrefix="IGCustom" %>
<asp:Content ContentPlaceHolderID="title" runat="server">
    <%= Model.Component.Name %>
</asp:Content>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="rightPodContentPlaceHolder" runat="server">
    <% this.contentControl.InnerHtml = Model.ComponentKeyFeatures; %>
    <div class="sb-content">
        <IGCustom:HtmlControl ID="contentControl" runat="server" />
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="beforeClosingBody" runat="server">
</asp:Content>
