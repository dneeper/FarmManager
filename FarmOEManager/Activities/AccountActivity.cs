using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FarmOEUtils;
using Utils;

namespace FarmOEManager.Activities
{
    [Activity(Label = "Account Information")]
    public class AccountActivity : ActivityEx
    {
        public const int GET_ACCOUNT_REQUEST = 0;

        private EditText _FirstName;
        private EditText _LastName;
        private EditText _Password;
        private EditText _AdminPassword;
        private Spinner _FarmSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Account);

            GetActivityControls();

            SetFarmSpinnerAdapter();

            SetHomeButtonAsSoftBackButton();
        }

        protected override void OnStart()
        {
            var accountPreferences = new AccountPreferences(this);

            _FirstName.Text = accountPreferences.FirstName;
            _LastName.Text = accountPreferences.LastName;
            _Password.Text = accountPreferences.Password;
            _AdminPassword.Text = accountPreferences.AdminPassword;
            _FarmSpinner.SetSelection((int)accountPreferences.Farm);

            base.OnStart();
        }

        protected override BaseRespType OnBackButtonSelected(BackButtonType type)
        {
            var accountPreferences = new AccountPreferences(this)
            {
                FirstName = _FirstName.Text,
                LastName = _LastName.Text,
                Password = _Password.Text,
                AdminPassword = _AdminPassword.Text,
                Farm = (Farms.Farm)_FarmSpinner.SelectedItemPosition
            };

            accountPreferences.Save();

            SetResult(Result.Ok);

            return BaseRespType.PassToBase;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return (base.OnOptionsItemSelected(item));
        }

        private void GetActivityControls()
        {
            _FirstName = FindViewById<EditText>(Resource.Id.accountFirstName);
            _LastName = FindViewById<EditText>(Resource.Id.accountLastName);
            _Password = FindViewById<EditText>(Resource.Id.accountPassword);
            _AdminPassword = FindViewById<EditText>(Resource.Id.accountAdminPassword);
            _FarmSpinner = FindViewById<Spinner>(Resource.Id.accountFarm);
        }

        private void SetFarmSpinnerAdapter()
        {
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem,
              Farms.GetStringArray());

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            _FarmSpinner.Adapter = adapter;
        }
    }
}