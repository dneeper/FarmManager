using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Utils;

namespace FarmOEManager.Activities
{
  public class AccountPreferences
  {
    public string FirstName;
    public string LastName;
    public string Password;
    public string AdminPassword;
    public Farms.Farm Farm;

    public bool Valid => (FirstName.Length > 0) &&
      (LastName.Length > 0) &&
      (Password.Length > 0) &&
      (AdminPassword.Length > 0);

    private const string PreferenceName = "FarmOrderManager";
    private const string FirstNameKey = "FirstName";
    private const string LastNameKey = "LastName";
    private const string PasswordKey = "Password";
    private const string AdminPasswordKey = "AdminPassword";
    private const string FarmKey = "Farm";

    private readonly Context _Parent;

    public AccountPreferences(Context parent)
    {
      this._Parent = parent;

      var preferences = parent.GetSharedPreferences(PreferenceName, FileCreationMode.Private);

      FirstName = preferences.GetString(FirstNameKey, "");
      LastName = preferences.GetString(LastNameKey, "");
      Password = preferences.GetString(PasswordKey, "");
      AdminPassword = preferences.GetString(AdminPasswordKey, "");
      Farm = (Farms.Farm)preferences.GetInt(FarmKey, 0);
    }

    public void Save()
    {
      var preferences = _Parent.GetSharedPreferences(PreferenceName, FileCreationMode.Private);

      var editor = preferences.Edit();

      editor.PutString(FirstNameKey, FirstName);
      editor.PutString(LastNameKey, LastName);
      editor.PutString(PasswordKey, Password);
      editor.PutString(AdminPasswordKey, AdminPassword);
      editor.PutInt(FarmKey, (int)Farm);

      editor.Apply();
    }
  }
}