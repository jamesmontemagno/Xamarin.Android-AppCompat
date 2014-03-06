using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
  [Activity(Label = "Searching is fun!",
        Icon = "@drawable/ic_launcher",
        Theme = "@style/Theme.AppCompat.Light")]
  public class SearchActivity : ActionBarActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);


      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.ProgressBar);

   }



    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      
      return base.OnOptionsItemSelected(item);
    }

    Android.Support.V7.Widget.SearchView searchView;
    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      this.MenuInflater.Inflate(Resource.Menu.menu_search, menu);

      var searchItem = menu.FindItem(Resource.Id.action_search);

      var test = MenuItemCompat.GetActionView(searchItem);
      searchView = test.JavaCast<Android.Support.V7.Widget.SearchView>();

      searchView.QueryTextSubmit += (sender, args) =>
      {
        Toast.MakeText(this, "You searched: " + args.Query, ToastLength.Short).Show();
      
      };


      return base.OnCreateOptionsMenu(menu);
    }


  }
}