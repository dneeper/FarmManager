using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Utils
{
  public class MessageBox
  {
    public static void Show(Activity parent, int titleResource, string message,
       EventHandler<DialogClickEventArgs> handler = null)
    {
      var title = parent.GetString(titleResource);

      var alert = new AlertDialog.Builder(parent)
        .SetTitle(title)
        .SetMessage(message)
        .SetPositiveButton(parent.GetString(Resource.String.OkButton), handler);

      alert.Create();
      alert.Show();
    }

    public static void Show(Activity parent, int titleResource, int messageResource,
      EventHandler<DialogClickEventArgs> handler = null)
    {
      var title = parent.GetString(titleResource);
      var message = parent.GetString(messageResource);

      var alert = new AlertDialog.Builder(parent)
        .SetTitle(title)
        .SetMessage(message)
        .SetPositiveButton(parent.GetString(Resource.String.OkButton), handler);

      alert.Create();
      alert.Show();
    }
  }
}