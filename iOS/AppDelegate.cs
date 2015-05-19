using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Net;
using System.Threading.Tasks;

namespace HTTPS.iOS
{
	//===============================================================
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			ServicePointManager.ServerCertificateValidationCallback +=  (sender, cert, chain, sslPolicyErrors) => {

				// debug return true to see data returned without dialog
				//return true;


				// dont let this callback return until we have obtained info from the user
				var tcs = new TaskCompletionSource<bool>();
				// show dialog to use on main thread
				BeginInvokeOnMainThread(() =>
					{
						var vc = UIApplication.SharedApplication.KeyWindow.RootViewController ;

						bool ret =  Dialogues.ShowOKCancel (vc,"Some Title","Some Message");
						tcs.SetResult(ret);
					});
				return tcs.Task.Result;
			};

			LoadApplication (new App ());
			return base.FinishedLaunching (app, options);
		}
	}

	//===============================================================
	public static class Dialogues
	{
		public static bool ShowOKCancel (UIViewController parent, string strTitle, string strMsg)
		{
			// method to show an OK/Cancel dialog box and return true for OK, or false for cancel
			var taskCompletionSource = new TaskCompletionSource<bool>();

			var alert = UIAlertController.Create(strTitle,strMsg,UIAlertControllerStyle.Alert);
			// set up button event handlers
			alert.AddAction(UIAlertAction.Create("OK",UIAlertActionStyle.Default,a => taskCompletionSource.SetResult(true)));
			alert.AddAction(UIAlertAction.Create("Cancel",UIAlertActionStyle.Default,a => taskCompletionSource.SetResult(false)));
			// show it
			parent.PresentViewController (alert, true, null);
			return taskCompletionSource.Task.Result;
		}
	}
}

