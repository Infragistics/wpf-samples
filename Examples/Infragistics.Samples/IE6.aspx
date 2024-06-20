<%@ Page Language="C#" Inherits="System.Web.UI.Page" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<title>Not supported browser</title>
	<meta charset="utf-8" />
	<link rel="Shortcut Icon" type="image/ico" href="/samplesbrowser/Styles/images/favicon.ico" />
	<!--[if lt IE 9]><script type="text/javascript" src="/samplesbrowser/scripts/js/html5.js"></script><![endif]-->
	<link rel="stylesheet" href="/samplesbrowser/styles/css/reset.css" type="text/css"
		media="screen" />
	<link rel="stylesheet" href="/samplesbrowser/styles/css/style.css" type="text/css"
		media="screen" />
	<link rel="stylesheet" href="/samplesbrowser/styles/css/queries/netbooks-tablets.css"
		type="text/css" media="screen and (max-width: 1007px)" />
	<!--[if IE]><link rel="stylesheet" href="/samplesbrowser/styles/css/style_ie.css" type="text/css" media="screen" /><![endif]-->
	<!--[if IE 7]><link rel="stylesheet" href="/samplesbrowser/styles/css/ie/ie7.css" type="text/css" media="screen" /><![endif]-->
	<!--[if lt IE 7]><link rel="stylesheet" href="/samplesbrowser/styles/css/ie/ie6.css" type="text/css" media="screen" /><![endif]-->
	<style type="text/css">
		ul li
		{
			float: left;
			margin: 20px;
			cursor: pointer;
		}
		ul li a
		{
			display: block;
			width: 128px;
			height: 128px;
		}
		.browser
		{
			width: 128px;
			height: 128px;
			background: url(/samplesbrowser/styles/images/browsers.png);
		}
		.ie
		{
			background-position: 0px -128px;
		}
		.chrome
		{
			background-position: 0px -512px;
		}
		.firefox
		{
			background-position: 0px -256px;
		}
		.safari
		{
			background-position: 0px 0px;
		}
		.opera
		{
			background-position: 0px -384px;
		}
	</style>
</head>
<body>
	<div class="top_nav">
		<div style="width: 1084px; margin: 0 auto;">
			<div id="logo" class="left" style="width: 250px; height: 42px; margin-top: 25px;
				filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/samplesbrowser/styles/images/logo.png');">
			</div>
		</div>
	</div>
	<div class="container clearfix content_area" style="padding-bottom: 0px;">
		<div style="margin-bottom: 150px; height: 400px;">
			<h1>
				Oh no! Your browser is no longer supported.</h1>
			<p style="padding: 10px 0px 20px 0px; border-bottom: 1px solid #656565;">
				We’re sorry, but we don`t support your browser. Please upgrade to a modern browser.</p>
			<span style="color: #656565; font-size: 0.857em; line-height: 1.429em;">Please upgrade
				to one of the following browsers:</span>
			<div style="width: 1024px; margin-top: 100px;">
				<ul style="overflow: auto; color: #656565; font-size: 1em; line-height: 1.429em;
					list-style-type: none; width: 920px; margin: 0 auto;">
					<li class="browser ie"><a href='http://windows.microsoft.com/en-US/internet-explorer/products/ie/home'
						title="Internet Explorer"></a></li>
					<li class="browser chrome"><a href='http://www.google.com/chrome/' title="Google Chrome">
					</a></li>
					<li class="browser firefox"><a href='http://www.mozilla.com/en-US/products/download.html'
						title="Firefox"></a></li>
					<li class="browser safari"><a href='http://www.apple.com/safari/download/' title="Safari">
					</a></li>
					<li class="browser opera"><a href='http://www.opera.com/download/' title="Opera"></a>
					</li>
					<li style="clear: both;"></li>
				</ul>
			</div>
		</div>
	</div>
</body>
</html>
