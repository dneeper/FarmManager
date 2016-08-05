using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace FarmOEManager.WebServices.DataObjects
{
    [DataContract]
    public class MemberData : Java.Lang.Object, IParcelable
    {
        [DataMember]
        public string ErrorInfo;

        [DataMember]
        public int Farm;

        [DataMember]
        public string FirstName;

        [DataMember]
        public string LastName;

        [DataMember]
        public string Group;

        [DataMember]
        public string Password;

        [DataMember]
        public int PickupDay;

        [DataMember]
        public bool Valid;

        public MemberData(string errorInfo, int farm, string firstName, string lastName, string group,
            string password, int pickupDay, bool valid)
        {
            ErrorInfo = errorInfo;
            Farm = farm;
            FirstName = firstName;
            LastName = lastName;
            Group = group;
            Password = password;
            PickupDay = pickupDay;
            Valid = valid;
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteString(ErrorInfo);
            dest.WriteInt(Farm);
            dest.WriteString(FirstName);
            dest.WriteString(LastName);
            dest.WriteString(Group);
            dest.WriteString(Password);
            dest.WriteInt(PickupDay);
            dest.WriteInt(Valid ? 1 : 0);
        }

        [ExportField("CREATOR")]
        public static MemberDataParcelableCreator InitializeCreator()
        {
            return new MemberDataParcelableCreator();
        }
    }

    public class MemberDataParcelableCreator : Java.Lang.Object, IParcelableCreator
    {
        public Java.Lang.Object CreateFromParcel(Parcel source)
        {
            return new MemberData(source.ReadString(),
                source.ReadInt(),
                source.ReadString(),
                source.ReadString(),
                source.ReadString(),
                source.ReadString(),
                source.ReadInt(),
                source.ReadInt() == 1 ? true : false);
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            return new Java.Lang.Object[size];
        }
    }
}