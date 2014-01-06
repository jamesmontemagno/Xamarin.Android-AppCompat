using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ActionBar = Android.Support.V7.App.ActionBar;
using Fragment = Android.Support.V4.App.Fragment;

namespace ActionBarCompat
{
    [Activity(Label = "Custom Theme",
        Theme = "@style/Theme.Customab",
        Icon = "@drawable/ic_launcher")]
    public class CustomizedActivity : ActionBarActivity, ActionBar.ITabListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Customized);
            var setting = Android.Support.V7.App.ActionBar.NavigationModeTabs;
            SupportActionBar.NavigationMode = setting;

            var tab = SupportActionBar.NewTab();
            tab.SetText("Tab 1");
            tab.SetTabListener(this);
            SupportActionBar.AddTab(tab);
            var tab2 = SupportActionBar.NewTab();
            tab2.SetText("Tab 2");
            tab2.SetTabListener(this);
            SupportActionBar.AddTab(tab2);
           
            
        }
        



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu_custom, menu);
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

        private Fragment fragment;
        public void OnTabReselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            
        }

        public void OnTabSelected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            if (fragment == null)
            {
                fragment = new SampleTabFragment();
                ft.Add(Android.Resource.Id.Content, fragment, "tag");
            }
            else
            {
                ft.Attach(fragment);
            }
            
        }

        public void OnTabUnselected(ActionBar.Tab tab, Android.Support.V4.App.FragmentTransaction ft)
        {
            if (fragment != null)
                ft.Detach(fragment);
        }
    }
    class SampleTabFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater,
            ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(
                Resource.Layout.NavDrawerFrag, container, false);


            return view;
        }
    }
}