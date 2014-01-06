using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ActionBarCompat
{
    [Activity(Label = "Progress Bar Spins!",
        Theme="@style/Theme.AppCompat.Light",
        Icon = "@drawable/ic_launcher")]
    public class ProgressBarActivity : ActionBarActivity
    {

        bool indeterminateVisible;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Needs to be called before setting the content view
            SupportRequestWindowFeature((int)WindowFeatures.IndeterminateProgress);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ProgressBar);


            var button = FindViewById<Button>(Resource.Id.progress_button);
            button.Click += (sender, e) =>
            {
                // Switch the state of the ProgressBar and set it
                indeterminateVisible = !indeterminateVisible;
                SetSupportProgressBarIndeterminateVisibility(indeterminateVisible);

                // Update the button text
                button.Text = indeterminateVisible ? "Stop Progress" : "Start Progress";

                SupportInvalidateOptionsMenu();
            };

            var next = new Intent(this, typeof(UpNavigationActivity));
            FindViewById<Button>(Resource.Id.button).Click += (sender, args) => StartActivity(next);
		

        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            if (indeterminateVisible)
            {
                menu.RemoveItem(Resource.Id.action_edit);
                menu.RemoveItem(Resource.Id.action_save);
            }

            return base.OnPrepareOptionsMenu(menu);
        }

        Android.Support.V7.Widget.ShareActionProvider actionProvider;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu_share, menu);

            var shareItem = menu.FindItem(Resource.Id.action_share);
            var test = MenuItemCompat.GetActionProvider(shareItem);
            actionProvider = test.JavaCast<Android.Support.V7.Widget.ShareActionProvider>();

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, "ActionBarCompat is Awesome! Support Lib v7 #Xamarin");

            actionProvider.SetShareIntent(intent);


            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_edit:
                    Toast.MakeText(this, "You pressed edit action!", ToastLength.Short).Show();
                    break;
                case Resource.Id.action_save:
                    Toast.MakeText(this, "You pressed save action!", ToastLength.Short).Show();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}