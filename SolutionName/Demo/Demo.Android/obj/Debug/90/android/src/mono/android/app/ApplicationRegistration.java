package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("Demo.Droid.MainApplication, Demo.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc640cdf32c7cc109f2c.MainApplication.class, crc640cdf32c7cc109f2c.MainApplication.__md_methods);
		
	}
}
