using System;

using Xamarin.Forms;
using System.Net.Http;

namespace HTTPS
{
	public class App : Application
	{
		Label lblHostdata = new Label();
		public App ()
		{
			lblHostdata.Text="No HTTPS data yet";
			lblHostdata.XAlign = TextAlignment.Center	;

			MainPage = new ContentPage 
			{
				Content = new StackLayout 
				{
					VerticalOptions = LayoutOptions.Center,
					Children = 
					{
						lblHostdata
					}
				}
			};
			// fetch data from the server
			fetch ();
		}

		//====================================================================
		private async void fetch()
		{
			// fetch data from the http server and show it in the lblHostdata label
			try
			{
				var URI = "https://www.example.com/"; 	
				var URIStem = "";

				var client = new System.Net.Http.HttpClient();

				client.BaseAddress = new Uri(URI);

				// this will fire a ServerCertificateValidationCallback event, handled at the 
				// native platform level
				var response = await client.GetAsync(URIStem);

				response.EnsureSuccessStatusCode ();
				var retdata = response.Content.ReadAsStringAsync ().Result;

				lblHostdata.Text = retdata;
			}

			catch (HttpRequestException e) {
				lblHostdata.Text = String.Format("{0}",e);
			}
			catch (Exception ex) {
				lblHostdata.Text = String.Format("{0}",ex);
			}
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}

}