using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;

namespace ActionBarCompat
{
    [Activity(Label = "UpNavigation", Icon = "@drawable/ic_launcher", Theme = "@style/Theme.AppCompat.Light")]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "actionbarcompat.MainActivity")]
    public class UpNavigationActivity : ActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.UpNavigation);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
        }



        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                {
                    var upIntent = NavUtils.GetParentActivityIntent(this);
					if (NavUtils.ShouldUpRecreateTask(this, upIntent))
					{
						// This activity is NOT part of this app's task, so create a new task
						// when navigating up, with a synthesized back stack.
						Android.Support.V4.App.TaskStackBuilder.Create(this).
							AddNextIntentWithParentStack(upIntent).StartActivities();
					}
					else
					{
						// This activity is part of this app's task, so simply
						// navigate up to the logical parent activity.
						NavUtils.NavigateUpTo(this, upIntent); 
					}
                }
                return true;

            }
            return base.OnOptionsItemSelected(item);
        }
    }
}