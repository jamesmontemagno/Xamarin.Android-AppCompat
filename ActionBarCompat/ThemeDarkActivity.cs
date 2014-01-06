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
        Theme = "@style/Theme.AppCompat")]
	public class ThemeDarkMainActivity : ActionBarActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            var next = new Intent(this, typeof(SplitActionsActivity));
            FindViewById<Button>(Resource.Id.button).Click += (sender, args) => StartActivity(next);
        }


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
            MenuInflater.Inflate(Resource.Menu.main_menu_dark, menu);
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


