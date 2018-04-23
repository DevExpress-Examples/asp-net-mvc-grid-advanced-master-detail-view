Imports Microsoft.VisualBasic
Imports System
Imports Microsoft.Web.WebPages.OAuth

Namespace AdvancedMasterDetail
	Public NotInheritable Class AuthConfig
		Private Sub New()
		End Sub
		Public Shared Sub RegisterAuth()
			' To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			' you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

			'OAuthWebSecurity.RegisterMicrosoftClient(
			'    clientId: "",
			'    clientSecret: "");

			'OAuthWebSecurity.RegisterTwitterClient(
			'    consumerKey: "",
			'    consumerSecret: "");

			'OAuthWebSecurity.RegisterFacebookClient(
			'    appId: "",
			'    appSecret: "");

			'OAuthWebSecurity.RegisterGoogleClient();
		End Sub
	End Class
End Namespace