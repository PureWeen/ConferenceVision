using Android.Support.V7.Widget;
using ConferenceVision.Views;
using ConferenceVision.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.Support.V7.Graphics.Drawable;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(CustomMasterDetailPageRenderer))]
namespace ConferenceVision.Droid.Renderers
{
    public class CustomMasterDetailPageRenderer : MasterDetailPageRenderer
    {

        public CustomMasterDetailPageRenderer(Context context) : base(context)
        {

        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {

                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as global::Android.Widget.ImageButton;
                    var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                    if (drawerArrow == null)
                        continue;

                    bool displayBack = false;
                    var app = Xamarin.Forms.Application.Current;

                    var detailPage = (app.MainPage as MasterDetailPage).Detail;
                    var navPageLevel = detailPage.Navigation.NavigationStack.Count;
                    if (navPageLevel > 1)
                        displayBack = true;

                    if (!displayBack)
                        ChangeIcon(imageButton, Resource.Drawable.menu);
                }
            }
        }

        private void ChangeIcon(Android.Widget.ImageButton imageButton, int id)
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                imageButton.SetImageDrawable(Context.GetDrawable(id));
            imageButton.SetImageResource(id);
        }
    }
}