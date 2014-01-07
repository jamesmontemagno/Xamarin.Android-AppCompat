using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Fragment = Android.Support.V4.App.Fragment;
using ListFragment = Android.Support.V4.App.ListFragment;

namespace AppCompat
{
	[Activity (Label = "AppCompat", MainLauncher = true, Theme = "@android:style/Theme.Light")]
	public class MainActivity : Android.Support.V4.App.FragmentActivity
	{
		ListFragment list;
		ArrayAdapter adapter;

		ViewPagerFragment viewPager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			RequestWindowFeature (WindowFeatures.NoTitle);

			SetContentView (Resource.Layout.Main);

			list = new ListFragment ();

			SupportFragmentManager
				.BeginTransaction ()
				.Add (Resource.Id.fragmentHost, list)
				.Commit ();

			LoadList ();

			var btn = FindViewById<Button> (Resource.Id.btn);
//			btn.Text = "Switch View";
			btn.Click += (sender, e) => {
				list.SetEmptyText ("The monkeys ran off");
				adapter.Clear ();
//				viewPager = new ViewPagerFragment ();
//				SupportFragmentManager
//					.BeginTransaction ()
//					.Replace (Resource.Id.fragmentHost, viewPager)
//					.Commit ();
//				((View)sender).Visibility = ViewStates.Gone;
			};
		}

		async void LoadList ()
		{
			adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, new string[] {
				"Tamarin", "Chimpanzé", "Gorille", "Singe", "Babouin"
			});

			// Simulate network fetching
			await Task.Delay (5000);
			list.ListAdapter = adapter;
		}
	}
}


