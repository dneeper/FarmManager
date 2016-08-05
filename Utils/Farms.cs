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

namespace Utils
{
  public class Farms
  {
    private const string YoderName = "YODER";
    private const string JubileeMeadowsName = "JUBILEE MEADOWS";
    private const string HighlandHavenName = "HIGHLAND HAVEN";
    private const string Farm2UName = "FARM 2U";

    public enum Farm
    {
      Yoder = 0,
      JubileeMeadows = 1,
      HighlandHaven = 2,
      Farm2U = 3
    }

    public static string[] GetStringArray()
    {
      var farmList = new List<string> {YoderName, JubileeMeadowsName, HighlandHavenName, Farm2UName};

      return farmList.ToArray();
    }
  }
}