<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SamplePage.master" AutoEventWireup="true"
	Inherits="Infragistics.Web.SampleBrowser.Core.Framework.Web.UI.Silverlight.SamplePage" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="/samplesbrowser/scripts/sl.debug.js"></script>
	<script type="text/javascript">
    var __param_minRuntimeVersion = "5.0.61118.0";
    var __param_windowless = false;

    function loadedLocalizedString() {
        var localizedStrings = {
            loadingMessage: {
                'en-US': 'Loading Sample...',
                'en-us': 'Loading Sample...',
                'ja': '読み込んでいます...',
                'ja-JP': '読み込んでいます...',
                'ja-jp': '読み込んでいます...',
                'undefined': 'Loading Sample...'
            }
        };

        var loading = localizedStrings['loadingMessage'][__language];

        return loading;
    }

    function onSourceDownloadProgressChanged(sender, eventArgs) {
        sender.findName("Loading").Text = loadedLocalizedString();

        sender.findName("progressBar").Width = eventArgs.progress * (sender.findName("progressBarBackground").Width - 2);
    }

    function onSourceDownloadComplete(sender, eventArgs) {
        sender.findName("Loading").Text = loadedLocalizedString();
        sender.findName("progressBar").Width = (sender.findName("progressBarBackground").Width - 2);
    }
	</script>	
	<asp:PlaceHolder ID="beforeHeaderClosing" runat="server" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<div id="silverlightControlHost" style="width: 720px; height: 520px;">
		<object id="slObject" data="data:application/x-silverlight-2," type="application/x-silverlight-2"
			width="100%" height="100%">
			<param name="source" value="<%= this.Source %>" />
            <param name="splashscreensource" value="/samplesbrowser/splash.xaml" />
            <param name="onsourcedownloadprogresschanged" value="onSourceDownloadProgressChanged" />
            <param name="onsourcedownloadcomplete" value="onSourceDownloadComplete" />
			<param name="onError" value="onSilverlightError" />
			<param name="background" value="white" />
			<param name="minRuntimeVersion" value="5.0.61118.0" />
			<param name="windowless" value="false" />
			<param name="autoUpgrade" value="true" />
			<param name="culture" value="<%= System.Threading.Thread.CurrentThread.CurrentUICulture.Name %>" />
			<a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration: none">
				<img id="fallbackImage" src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight"
					style="border-style: none; width: 100%; height: 100%;" />
			</a>
		</object>
		<iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
			border: 0px"></iframe>
	</div>
	<div id="nonSLMsg">
	</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="beforeClosingBody" runat="server">
	<asp:PlaceHolder ID="beforeClosingBody" runat="server" />	
</asp:Content>