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
    [Activity(Label = "Sharing is fun!",
        Icon = "@drawable/ic_launcher",
        Theme = "@style/Theme.AppCompat.Light")]
	public class ShareActivity : ActionBarActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Share);

            var next = new Intent(this, typeof(SplitActionsActivity));
            FindViewById<Button>(Resource.Id.button).Click += (sender, args) => StartActivity(next);
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

        Android.Support.V7.Widget.ShareActionProvider actionProvider;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.main_menu_share, menu);

            var shareItem = menu.FindItem(Resource.Id.action_share);
            var test = MenuItemCompat.GetActionProvider(shareItem);
            actionProvider = test.JavaCast<Android.Support.V7.Widget.ShareActionProvider>();

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, "ActionBarCompat is Awesome! Support Lib v7 #Xamarin");

            actionProvider.SetShareIntent(intent);


            return base.OnCreateOptionsMenu(menu);
        }


	}
}


