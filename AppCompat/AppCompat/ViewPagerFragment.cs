using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using ViewPager = Android.Support.V4.View.ViewPager;

using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace AppCompat
{
	public class ViewPagerFragment : Fragment
	{
		Marker existingMarker;

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var pager = new ViewPager (inflater.Context) {
				Id = 0x34532,
				Adapter = new MonkeyPageAdapter (ChildFragmentManager),
			};
			pager.PageSelected += async (sender, e) => {
				var map = (SupportMapFragment)FragmentManager.FindFragmentById (Resource.Id.map);
				var monkeyName = ((MonkeyPageAdapter)pager.Adapter).GetMonkeyAtPosition (e.Position);
				var location = await WikipediaApi.FetchHabitatLocation (monkeyName);
				var latLng = new Android.Gms.Maps.Model.LatLng (location.Item1, location.Item2);
				map.Map.AnimateCamera (CameraUpdateFactory.NewLatLng (latLng), 250, null);
				if (existingMarker != null)
					existingMarker.Remove ();
				existingMarker = map.Map.AddMarker (new MarkerOptions ().SetPosition (latLng));
			};

			return pager;
		}

		class MonkeyPageAdapter : Android.Support.V4.App.FragmentPagerAdapter
		{
			static readonly string[] Monkeys = new string[] {
				"Singe", "Bonobo", "Gorille", "Ouistiti"
			};

			public MonkeyPageAdapter (FragmentManager manager) : base (manager)
			{
			}

			public override int Count {
				get {
					return Monkeys.Length;
				}
			}

			public override Fragment GetItem (int position)
			{
				return MonkeyPageFragment.NewInstance (GetMonkeyAtPosition (position));
			}

			public string GetMonkeyAtPosition (int position)
			{
				return Monkeys [position];
			}
		}

		class MonkeyPageFragment : Fragment
		{
			string monkeyName;

			public static MonkeyPageFragment NewInstance (string monkeyName)
			{
				var fragment = new MonkeyPageFragment ();
				fragment.monkeyName = monkeyName;
				return fragment;
			}

			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				return inflater.Inflate (Resource.Layout.MonkeyPageLayout, container, false);
			}

			public async override void OnViewCreated (View view, Bundle savedInstanceState)
			{
				base.OnViewCreated (view, savedInstanceState);

				view.FindViewById<TextView> (Resource.Id.monkeyName).Text = monkeyName.ToUpper ();
				view.FindViewById<TextView> (Resource.Id.monkeySummary).Text =
					await WikipediaApi.FetchIntroductionFor (monkeyName);
				view.FindViewById<ImageView> (Resource.Id.monkeyImg)
					.SetImageBitmap (await WikipediaApi.FetchMonkeyImage (monkeyName));
			}
		}
	}
}

