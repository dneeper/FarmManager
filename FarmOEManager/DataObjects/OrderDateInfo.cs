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
using Java.Lang;

namespace FarmOEManager.WebServices.DataObjects
{
    [DataContract]
    public class OrderDateInfo : Java.Lang.Object, IParcelable
    {
        [DataMember]
        public OrderDate orderDate;

        [DataMember]
        public int memberCount;

        [DataMember]
        public int orderCount;

        public OrderDateInfo()
        {
            orderDate = new OrderDate();
        }

        public OrderDateInfo(int Year, int Month, int Day, int memberCount, int orderCount)
        {
            orderDate = new OrderDate(Year, Month, Day);
            this.memberCount = memberCount;
            this.orderCount = orderCount;
        }

        public override string ToString()
        {
            DateTime date = new DateTime(orderDate.Year, orderDate.Month, orderDate.Day);
            return date.ToShortDateString();
        }

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        {
            dest.WriteInt(orderDate.Year);
            dest.WriteInt(orderDate.Month);
            dest.WriteInt(orderDate.Day);
            dest.WriteInt(memberCount);
            dest.WriteInt(orderCount);
        }

        [ExportField("CREATOR")]
        public static OrderDateInfoParcelableCreator InitializeCreator()
        {
            return new OrderDateInfoParcelableCreator();
        }
    }

    public class OrderDateInfoParcelableCreator : Java.Lang.Object, IParcelableCreator
    {
        public Java.Lang.Object CreateFromParcel(Parcel source)
        {
            return new OrderDateInfo(source.ReadInt(),
                source.ReadInt(),
                source.ReadInt(),
                source.ReadInt(),
                source.ReadInt());
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            return new Java.Lang.Object[size];
        }
    }
}