using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;
using System.Collections.Generic;

using Bitmap = Android.Graphics.Bitmap;

namespace AppCompat
{
	public class WikipediaApi
	{
		static readonly HttpClient client = new HttpClient ();

		static readonly Tuple<double, double>[] Locations = new Tuple<double, double>[] {
			// Republic of Congo
			Tuple.Create (-4.316667, 15.316667),
			// Brazil
			Tuple.Create (-15.783333, -47.866667),
			// Indonesia
			Tuple.Create (-6.168056, 106.818611),
		};

		static Dictionary<string, string> introductionCache = new Dictionary<string, string> ();
		static Dictionary<string, Bitmap> monkeyCache = new Dictionary<string, Bitmap> ();

		const string BaseUri = "https://fr.wikipedia.org/w/api.php?action=query&prop=extracts&exintro&explaintext&format=xml&exlimit=1&titles=";
		const string BaseImageUri = "https://fr.wikipedia.org/w/api.php?action=query&prop=pageimages&piprop=thumbnail&pithumbsize=200&format=xml&titles=";

		public static async Task<string> FetchIntroductionFor (string term)
		{
			if (introductionCache.ContainsKey (term))
				return introductionCache [term];
			var uri = BaseUri + WebUtility.UrlEncode (term);
			var content = await client.GetStringAsync (uri);
			return introductionCache[term] = await Task.Run (
				() => (string)XDocument.Parse (content).Root.Descendants ("extract").FirstOrDefault ()
			);
		}

		public static async Task<Bitmap> FetchMonkeyImage (string term)
		{
			if (monkeyCache.ContainsKey (term))
				return monkeyCache [term];
			var uri = BaseImageUri + WebUtility.UrlEncode (term);
			var content = await client.GetStringAsync (uri);
			var img = (string)XDocument.Parse (content).Root.Descendants ("thumbnail").FirstOrDefault ().Attribute ("source");
			return monkeyCache [term] = await Android.Graphics.BitmapFactory.DecodeStreamAsync (await client.GetStreamAsync (img));
		}

		public static Task<Tuple<double, double>> FetchHabitatLocation (string term)
		{
			var loc = Locations [((int)term[0]) % Locations.Length];
			return Task.FromResult (loc);
		}
	}
}

