using System;
using Android.App;
using Android.Runtime;

namespace withLuckAndWisdomProject.Android
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
    }
}
