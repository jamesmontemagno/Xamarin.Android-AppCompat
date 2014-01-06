using ActionBarCompat.DrawerLayout;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace ActionBarCompat
{
    [Activity(Label = "ActionBarCompat",
        Icon = "@drawable/ic_launcher",
        Theme = "@style/Theme.AppCompat.Light",
        MainLauncher = true)]
	public class MainActivity : ActionBarActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            var next = new Intent(this, typeof(ShareActivity));
            FindViewById<Button>(Resource.Id.button).Click += (sender, args) => StartActivity(next);
            FindViewById<Button>(Resource.Id.button_dark).Click += (sender, args) => StartActivity(new Intent(this, typeof(ThemeDarkMainActivity)));
            FindViewById<Button>(Resource.Id.button_dark_ab).Click += (sender, args) => StartActivity(new Intent(this, typeof(ThemeDarkABMainActivity)));
            FindViewById<Button>(Resource.Id.button_dark_custom).Click += (sender, args) => StartActivity(new Intent(this, typeof(CustomizedActivity)));
            FindViewById<Button>(Resource.Id.button_nav_drawer).Click += (sender, args) => StartActivity(new Intent(this, typeof(DrawerLayoutActivity)));
        }


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.main_menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}


		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.action_edit:
				Toast.MakeText (this, "You pressed edit action!", ToastLength.Short).Show ();
				break;
			case Resource.Id.action_save:
				Toast.MakeText (this, "You pressed save action!", ToastLength.Short).Show ();
				break;
			}
			return base.OnOptionsItemSelected(item);
		}

	}
}


