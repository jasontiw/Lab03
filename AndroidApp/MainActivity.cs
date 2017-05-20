using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var result = Task.Factory.StartNew<Task<SALLab03.ResultInfo>>(() =>Validate());
            
            

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog Alert = builder.Create();
            Alert.SetTitle("Verification Result");
            Alert.SetIcon(Resource.Drawable.Icon);
            Alert.SetMessage(
                $"{result.Result.Result.Status}\n{result.Result.Result.Fullname}\n{result.Result.Result.Token}"
                );
            Alert.SetButton("Ok", (s, ev) => { });
            Alert.Show();



            //var Helper = new SharedProject.MySharedCode();
            //new AlertDialog.Builder(this);
            //.SetMessage(Helper.GetFilePath("demo.dat")).
            //Show();

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }

        private async Task<SALLab03.ResultInfo> Validate()
        {
            var ServiceClient =
                new SALLab03.ServiceClient();

            string Email = "XXXXXXX";
            string Password = "XXXXXXXX";
            string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            SALLab03.ResultInfo Result = await ServiceClient.ValidateAsync(
                Email,Password,myDevice);

            return Result;

        }
    }
}

